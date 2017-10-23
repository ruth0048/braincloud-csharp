﻿//----------------------------------------------------
// brainCloud client source code
// Copyright 2016 bitHeads, inc.
//----------------------------------------------------

using System;
using System.Collections.Generic;
using BrainCloud;
using BrainCloud.Internal;
using JsonFx.Json;

#if !DOT_NET
using UnityEngine;
using BrainCloudUnity;
#else
using System.IO;
using System.IO.IsolatedStorage;
#endif

/// <summary>
/// The BrainCloudWrapper class provides some glue between the Unity environment and the
/// brainCloud C# library. Specifically the BrainCloudWrapper does the following:
/// 
/// 1) Creates and uses a global singleton GameObject to manage it's lifetime across the game
/// 2) Provides an Initialize method which uses the game id, secret, version, and server url
///    defined in the brainCloud Settings Unity window.
/// 3) Provides a few of the authentication types that are supported by brainCloud.
/// 4) For Anonymous authentication, stores the anonymous id and profile id to the Unity player prefs
///    upon successful authentication. This is important as once an anonymous account is created,
///    both the anonymous id and profile id of the account are required to authenticate.
/// 
/// Note that this class is *not* required to use brainCloud - you are free to reimplement the
/// functionality as you see fit. It is simply used as a starting point to get developers off the
/// ground - especially with authentications.
/// 
/// The meat of the BrainCloud api is available by using
/// 
/// BrainCloudWrapper.GetBC()
/// 
/// to grab an instance of the BrainCloudClient. From here you have access to all of the brainCloud
/// API service. So for instance, to execute a read player statistics API you would do the following:
/// 
/// BrainCloudWrapper.GetBC().PlayerStatisticsService.ReadAllUserStats()
/// 
/// Similar services exist for other APIs.
/// 
/// See http://getbraincloud.com/apidocs/ for the full list of brainCloud APIs.
/// </summary>
#if !DOT_NET
public class BrainCloudWrapper : MonoBehaviour
#else
public class BrainCloudWrapper
#endif
{
    #region Deprecated
    
    [Obsolete("Use of the *singleton* has been deprecated. We recommend that you create your own *variable* to hold an instance of the brainCloudWrapper. Explanation here: http://getbraincloud.com/blog")]
    public static BrainCloudWrapper Instance { get { return GetInstance(); } }

    [Obsolete("Use of the *singleton* has been deprecated. We recommend that you create your own *variable* to hold an instance of the brainCloudWrapper. Explanation here: http://getbraincloud.com/blog")]
    public static BrainCloudClient BcClient { get { return Instance.BCClient; } }

    /// <summary>
    /// Gets the singleton instance of the BrainCloudWrapper.
    /// The BrainCloudWrapper object is stored in a Unity Game Object.
    /// </summary>
    /// <returns>The instance</returns>
    [Obsolete("Use of the *singleton* has been deprecated. We recommend that you create your own *variable* to hold an instance of the brainCloudWrapper. Explanation here: http://getbraincloud.com/blog")]
    public static BrainCloudWrapper GetInstance()
    {
        if (!BrainCloudClient.EnableSingletonMode)
#pragma warning disable 162
        {
            throw new Exception(BrainCloudClient.SingletonUseErrorMessage);
        }
#pragma warning restore 162
        
        if (_applicationIsQuitting)
        {
            return null;
        }
        if (_instance == null)
        {
#if !DOT_NET
            _instance = (BrainCloudWrapper)FindObjectOfType(typeof(BrainCloudWrapper));
            if (_instance != null)
            {
                _instance.Reauthenticate();
            }

            if (FindObjectsOfType(typeof(BrainCloudWrapper)).Length > 1)
            {
                Debug.LogError("[Singleton] Something went really wrong " +
                               " - there should never be more than 1 singleton!" +
                               " Reopening the scene might fix it.");
                return _instance;
            }

            if (_instance == null)
            {
                GameObject go = new GameObject(GAMEOBJECT_BRAINCLOUD);
                
                _instance = go.AddComponent<BrainCloudWrapper>();
#pragma warning disable 618
                _instance.BCClient = BrainCloudClient.Get();
#pragma warning restore 618
                
                DontDestroyOnLoad(go);
            }
#else
            _instance = new BrainCloudWrapper(BrainCloudClient.Get());
#endif
            _instance.LoadData();
        }
        return _instance;
    }
    
    /// <summary>
    /// Returns a singleton instance of the BrainCloudClient. All brainCloud APIs are
    /// accessible through the client.
    /// </summary>
    /// <returns>The brainCloud client object</returns>
    [Obsolete("Use of the *singleton* has been deprecated. We recommend that you create your own *variable* to hold an instance of the brainCloudWrapper. Explanation here: http://getbraincloud.com/blog")]
    public static BrainCloudClient GetBC()
    {
        return GetInstance().BCClient;
    }
    
#if !DOT_NET
    /// <summary>
    /// Initializes the brainCloud client. This method uses the parameters as configured
    /// in the Unity brainCloud Settings window.
    /// </summary>
    [Obsolete("Use of the *singleton* has been deprecated. We recommend that you create your own *variable* to hold an instance of the brainCloudWrapper. Explanation here: http://getbraincloud.com/blog")]
    public static void Initialize()
    {
        Initialize(
            BrainCloudSettings.Instance.DispatcherURL,
            BrainCloudSettings.Instance.SecretKey,
            BrainCloudSettings.Instance.GameId,
            BrainCloudSettings.Instance.GameVersion);

        Instance.BCClient.EnableLogging(BrainCloudSettings.Instance.EnableLogging);
    }
#endif
    
    /// <summary>
    /// Initialize the brainCloud client with the passed in parameters. This version of Initialize
    /// overrides the parameters configured in the Unity brainCloud Settings window.
    /// </summary>
    /// <param name="url">The brainCloud server url</param>
    /// <param name="secretKey">The app's secret</param>
    /// <param name="appId">The app's id</param>
    /// <param name="version">The app's version</param>
    [Obsolete("Use of the *singleton* has been deprecated. We recommend that you create your own *variable* to hold an instance of the brainCloudWrapper. Explanation here: http://getbraincloud.com/blog")]
    public static void Initialize(string url, string secretKey, string appId, string version)
    {
        BrainCloudWrapper bcw = GetInstance();
        bcw._lastUrl = url;
        bcw._lastSecretKey = secretKey;
        bcw._lastAppId = appId;
        bcw._lastAppVersion = version;
        bcw.BCClient.Initialize(url, secretKey, appId, version);

        _instance.LoadData();
    }
    
    #endregion
    
    /// <summary>
    /// The key for the user prefs profile id
    /// </summary>
    public static string PREFS_PROFILE_ID = "brainCloud.profileId";

    /// <summary>
    /// The key for the user prefs anonymous id
    /// </summary>
    public static string PREFS_ANONYMOUS_ID = "brainCloud.anonymousId";

    /// <summary>
    /// The key for the user prefs authentication type
    /// </summary>
    public static string PREFS_AUTHENTICATION_TYPE = "brainCloud.authenticationType";

    /// <summary>
    /// The name of the singleton brainCloud game object
    /// </summary>
    public static string GAMEOBJECT_BRAINCLOUD = "BrainCloudWrapper";

    private static BrainCloudWrapper _instance = null;
    private static bool _applicationIsQuitting = false;

    public BrainCloudClient BCClient { get; private set; }

    private string _lastUrl = "";
    private string _lastSecretKey = "";
    private string _lastAppId = "";
    private string _lastAppVersion = "";

    private WrapperData _wrapperData = new WrapperData();

    public bool AlwaysAllowProfileSwitch { get; set; }

    public static string AUTHENTICATION_ANONYMOUS = "anonymous";


    // Name of this wrapper instance. Used for data loading
    public string WrapperName { get; private set; }
    
    /// <summary>
    /// Create the brainCloud Wrapper, which has utility helpers for using the brainCloud API
    /// </summary>
    public BrainCloudWrapper()
    {
        BCClient = new BrainCloudClient();
    }
    
    /// <summary>
    /// Create the brainCloud Wrapper, which has utility helpers for using the brainCloud API
    /// </summary>
    /// <param name="bcBcClient">set wrapper with a specfic brainCloud client</param>
    private BrainCloudWrapper(BrainCloudClient bcBcClient)
    {
        BCClient = bcBcClient;
    }
    
    /// <summary>
    /// Create the brainCloud Wrapper, which has utility helpers for using the brainCloud API
    /// </summary>
    /// <param name="wrapperName">string value used to differentiate saved wrapper data</param>
    public BrainCloudWrapper(string wrapperName)
    {
        BCClient = new BrainCloudClient();
        WrapperName = wrapperName;
    }

    

    public void Update()
    {
        if (BCClient != null)
        {
            BCClient.Update();
        }
    }

#if !DOT_NET
    public void OnDestroy()
    {
        _applicationIsQuitting = true;
    }
#endif

   
#if !DOT_NET
    /// <summary>
    /// Initializes the brainCloud client. This method uses the parameters as configured
    /// in the Unity brainCloud Settings window.
    /// </summary>
    public void Init(String wrapperName = "")
    {
        Init(
            BrainCloudSettings.Instance.DispatcherURL,
            BrainCloudSettings.Instance.SecretKey,
            BrainCloudSettings.Instance.GameId,
            BrainCloudSettings.Instance.GameVersion,
            wrapperName);

        BCClient.EnableLogging(BrainCloudSettings.Instance.EnableLogging);
    }
#endif

    /// <summary>
    /// Initialize the brainCloud client with the passed in parameters. This version of Initialize
    /// overrides the parameters configured in the Unity brainCloud Settings window.
    /// </summary>
    /// <param name="url">The brainCloud server url</param>
    /// <param name="secretKey">The app's secret</param>
    /// <param name="appId">The app's id</param>
    /// <param name="version">The app's version</param>
    public void Init(string url, string secretKey, string appId, string version, string wrapperName = "")
    {
        WrapperName = wrapperName;
        _lastUrl = url;
        _lastSecretKey = secretKey;
        _lastAppId = appId;
        _lastAppVersion = version;
        BCClient.Initialize(url, secretKey, appId, version);

        LoadData();
    }

    /// <summary>
    /// If set to true, profile id is never sent along with non-anonymous authenticates
    /// thereby ensuring that valid credentials always work but potentially cause a profile switch.
    /// If set to false, profile id is passed to the server (if it has been stored) and a profile id
    /// to non-anonymous credential mismatch will cause an error.
    /// </summary>
    /// <param name="enabled">True if we always allow profile switch</param>
    public void SetAlwaysAllowProfileSwitch(bool enabled)
    {
        AlwaysAllowProfileSwitch = enabled;
    }

    #region Authenticate Methods

    /// <summary>
    /// Authenticate a user anonymously with brainCloud - used for apps that don't want to bother
    /// the user to login, or for users who are sensitive to their privacy
    /// 
    /// Note that this method is special in that the anonymous id and profile id
    /// are persisted to the Unity player prefs cache if authentication is successful.
    /// Both pieces of information are required to successfully log into that account
    /// once the user has been created. Failure to store the profile id and anonymous id
    /// once the user has been created results in an inability to log into that account!
    /// For this reason, using other recoverable authentication methods (like email/password, Facebook)
    /// are encouraged.
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateAnonymous(
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity(true);

        BCClient.AuthenticationService.AuthenticateAnonymous(
            true, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user with a custom Email and Password.  Note that the client app
    /// is responsible for collecting (and storing) the e-mail and potentially password
    /// (for convenience) in the client data.  For the greatest security,
    /// force the user to re-enter their password at each login.
    /// (Or at least give them that option).
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    ///
    /// Note that the password sent from the client to the server is protected via SSL.
    /// </remarks>
    /// <param name="email">
    /// The e-mail address of the user
    /// </param>
    /// <param name="password">
    /// The password of the user
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateEmailPassword(
        string email,
        string password,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateEmailPassword(
            email, password, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user via cloud code (which in turn validates the supplied credentials against an external system).
    /// This allows the developer to extend brainCloud authentication to support other backend authentication systems.
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="userid">
    /// The user id
    /// </param>
    /// <param name="token">
    /// The user token (password etc)
    /// </param>
    /// /// <param name="externalAuthName">
    /// The name of the cloud script to call for external authentication
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateExternal(
        string userid,
        string token,
        string externalAuthName,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateExternal(
            userid, token, externalAuthName, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user with brainCloud using their Facebook Credentials
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="externalId">
    /// The facebook id of the user
    /// </param>
    /// <param name="authenticationToken">
    /// The validated token from the Facebook SDK (that will be further
    /// validated when sent to the bC service)
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateFacebook(
        string fbUserId,
        string fbAuthToken,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateFacebook(
            fbUserId, fbAuthToken, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user using their Game Center id
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="gameCenterId">
    /// The user's game center id  (use the playerID property from the local GKPlayer object)
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateGameCenter(
        string gameCenterId,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateGameCenter(
            gameCenterId, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user using a google userid(email address) and google authentication token.
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="userid">
    /// String representation of google+ userid (email)
    /// </param>
    /// <param name="token">
    /// The authentication token derived via the google apis.
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateGoogle(
        string userid,
        string token,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateGoogle(
            userid, token, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user using a steam userid and session ticket (without any validation on the userid).
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="userid">
    /// String representation of 64 bit steam id
    /// </param>
    /// <param name="sessionticket">
    /// The session ticket of the user (hex encoded)
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateSteam(
        string userid,
        string sessionticket,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateSteam(
            userid, sessionticket, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user using a Twitter userid, authentication token, and secret from twitter.
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="userid">
    /// String representation of a Twitter user ID
    /// </param>
    /// <param name="token">
    /// The authentication token derived via the Twitter apis
    /// </param>
    /// <param name="secret">
    /// The secret given when attempting to link with Twitter
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void AuthenticateTwitter(
        string userid,
        string token,
        string secret,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateTwitter(
            userid, token, secret, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Authenticate the user using a userid and password (without any validation on the userid).
    /// Similar to AuthenticateEmailPassword - except that that method has additional features to
    /// allow for e-mail validation, password resets, etc.
    /// </summary>
    /// <remarks>
    /// Service Name - Authenticate
    /// Service Operation - Authenticate
    /// </remarks>
    /// <param name="email">
    /// The e-mail address of the user
    /// </param>
    /// <param name="password">
    /// The password of the user
    /// </param>
    /// <param name="forceCreate">
    /// Should a new profile be created for this user if the account does not exist?
    /// </param>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param> 
    public void AuthenticateUniversal(
        string username,
        string password,
        bool forceCreate,
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        WrapperAuthCallbackObject aco = new WrapperAuthCallbackObject();
        aco._successCallback = success;
        aco._failureCallback = failure;
        aco._cbObject = cbObject;

        InitializeIdentity();

        BCClient.AuthenticationService.AuthenticateUniversal(
            username, password, forceCreate, AuthSuccessCallback, AuthFailureCallback, aco);
    }

    /// <summary>
    /// Re-authenticates the user with brainCloud
    /// </summary>
    /// <param name="success">
    /// The method to call in event of successful login
    /// </param>
    /// <param name="failure">
    /// The method to call in the event of an error during authentication
    /// </param>
    /// <param name="cbObject">
    /// The user supplied callback object
    /// </param>
    public void Reconnect(
        SuccessCallback success = null,
        FailureCallback failure = null,
        object cbObject = null)
    {
        AuthenticateAnonymous(success, failure, cbObject);
    }

    /// <summary>
    /// Method initializes the identity information from the Unity player prefs cache.
    /// This is specifically useful for an Anonymous authentication as Anonymous authentications
    /// require both the anonymous id *and* the profile id. By using the BrainCloudWrapper
    /// AuthenticateAnonymous method, a success callback handler hook will be installed
    /// that will trap the return from the brainCloud server and persist the anonymous id
    /// and profile id.
    /// 
    /// Note that clients are free to implement this logic on their own as well if they 
    /// wish to store the information in another location and/or change the behaviour.
    /// </summary>
    protected virtual void InitializeIdentity(bool isAnonymousAuth = false)
    {
        // retrieve profile and anonymous ids out of the cache
        string profileId = GetStoredProfileId();
        string anonymousId = GetStoredAnonymousId();

        if ((anonymousId != "" && profileId == "") || anonymousId == "")
        {
            anonymousId = BCClient.AuthenticationService.GenerateAnonymousId();
            profileId = "";
            SetStoredAnonymousId(anonymousId);
            SetStoredProfileId(profileId);
        }
        string profileIdToAuthenticateWith = profileId;
        if (!isAnonymousAuth && AlwaysAllowProfileSwitch)
        {
            profileIdToAuthenticateWith = "";
        }
        SetStoredAuthenticationType(isAnonymousAuth ? AUTHENTICATION_ANONYMOUS : "");
        BCClient.InitializeIdentity(profileIdToAuthenticateWith, anonymousId);
    }

    #endregion

    /// <summary>
    /// Gets the stored profile id from user prefs.
    /// </summary>
    /// <returns>The stored profile id.</returns>
    public virtual string GetStoredProfileId()
    {
        return _wrapperData.ProfileId;
    }

    /// <summary>
    /// Sets the stored profile id to user prefs.
    /// </summary>
    /// <param name="profileId">Profile id.</param>
    public virtual void SetStoredProfileId(string profileId)
    {
        _wrapperData.ProfileId = profileId;
        SaveData();
    }

    /// <summary>
    /// Resets the stored profile id to empty string.
    /// </summary>
    public virtual void ResetStoredProfileId()
    {
        _wrapperData.ProfileId = "";
        SaveData();
    }

    /// <summary>
    /// Gets the stored anonymous id from user prefs.
    /// </summary>
    /// <returns>The stored anonymous id.</returns>
    public virtual string GetStoredAnonymousId()
    {
        return _wrapperData.AnonymousId;
    }

    /// <summary>
    /// Sets the stored anonymous id to user prefs.
    /// </summary>
    /// <param name="anonymousId">Anonymous id</param>
    public virtual void SetStoredAnonymousId(string anonymousId)
    {
        _wrapperData.AnonymousId = anonymousId;
        SaveData();
    }

    /// <summary>
    /// Resets the stored anonymous id to empty string.
    /// </summary>
    public virtual void ResetStoredAnonymousId()
    {
        _wrapperData.AnonymousId = "";
        SaveData();
    }

    /// <summary>
    /// Gets the type of the stored authentication.
    /// </summary>
    /// <returns>The stored authentication type.</returns>
    public virtual string GetStoredAuthenticationType()
    {
        return _wrapperData.AuthenticationType;
    }

    /// <summary>
    /// Sets the type of the stored authentication.
    /// </summary>
    /// <param name="authenticationType">Authentication type.</param>
    public virtual void SetStoredAuthenticationType(string authenticationType)
    {
        _wrapperData.AuthenticationType = authenticationType;
        SaveData();
    }

    /// <summary>
    /// Resets the type of the stored authentication to empty string
    /// </summary>
    public virtual void ResetStoredAuthenticationType()
    {
        _wrapperData.AuthenticationType = "";
        SaveData();
    }

    /// <summary>
    /// Provides a way to reauthenticate with the stored anonymous and profile id.
    /// Only works for Anonymous authentications.
    /// </summary>
    protected virtual void Reauthenticate()
    {
        Init(_instance._lastUrl, _instance._lastSecretKey, _instance._lastAppId, _instance._lastAppVersion, WrapperName);
        string authType = GetStoredAuthenticationType();
        if (authType == AUTHENTICATION_ANONYMOUS)
        {
            AuthenticateAnonymous(null, null);
        }
    }

    /// <summary>
    /// Callback for authentication success using the BrainCloudWrapper class.
    /// </summary>
    /// <param name="json">The returned json</param>
    /// <param name="cbObject">The returned callback object</param>
    protected virtual void AuthSuccessCallback(string json, object cbObject)
    {
        // grab the profileId and save it in PlayerPrefs
        Dictionary<string, object> jsonMessage = (Dictionary<string, object>)JsonReader.Deserialize(json);
        Dictionary<string, object> jsonData = (Dictionary<string, object>)jsonMessage["data"];
        string profileId = "";
        if (jsonData.ContainsKey("profileId"))
        {
            profileId = (string)jsonData["profileId"];
        }
        if (profileId != "")
        {
            SetStoredProfileId(profileId);
        }
        if (cbObject != null)
        {            
            WrapperAuthCallbackObject aco = (WrapperAuthCallbackObject)cbObject;
            if (aco._successCallback != null)
            {
                aco._successCallback(json, aco._cbObject);
            }
        }
        
#if UNITY_EDITOR
        BrainCloudUnity.BrainCloudPlugin.ResponseEvent.OnAuthenticateSuccess(json);
#endif
    }

    /// <summary>
    /// Callback for authentication failure using the BrainCloudWrapper class.
    /// </summary>
    /// <param name="statusCode">The status code</param>
    /// <param name="reasonCode">The reason code</param>
    /// <param name="errorJson">The error json</param>
    /// <param name="cbObject">The returned callback object</param>
    protected virtual void AuthFailureCallback(int statusCode, int reasonCode, string errorJson, object cbObject)
    {
        if (cbObject != null)
        {
            WrapperAuthCallbackObject aco = (WrapperAuthCallbackObject)cbObject;
            if (aco._failureCallback != null)
            {
                aco._failureCallback(statusCode, reasonCode, errorJson, aco._cbObject);
            }
        }
        
#if UNITY_EDITOR
        BrainCloudUnity.BrainCloudPlugin.ResponseEvent.OnAuthenticateFailed(string.Format("statusCode[{0}] reasonCode[{1}] errorJson[{2}]", statusCode, reasonCode, errorJson));
#endif
    }

    private void SaveData()
    {
#if DOT_NET
        string prefix = _instanceName.Equals("") ? "" : _instanceName + ".";
        string fileName = prefix + WrapperData.FileName;
        
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore))
        {
            using (StreamWriter writer = new StreamWriter(isoStream))
            {
                string file = JsonWriter.Serialize(_wrapperData);
                writer.WriteLine(file);
            }
        }
#else
        string prefix = WrapperName.Equals("") ? "" : WrapperName + ".";
        PlayerPrefs.SetString(prefix + PREFS_PROFILE_ID, _wrapperData.ProfileId);
        PlayerPrefs.SetString(prefix + PREFS_ANONYMOUS_ID, _wrapperData.AnonymousId);
        PlayerPrefs.SetString(prefix + PREFS_AUTHENTICATION_TYPE, _wrapperData.AuthenticationType);
#endif
    }

    private void LoadData()
    {
#if DOT_NET
        string prefix = _instanceName.Equals("") ? "" : _instanceName + ".";
        string fileName = prefix + WrapperData.FileName;
        
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        if (isoStore.FileExists(fileName))
        {
            string file;

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.Open, isoStore))
            {
                using (StreamReader reader = new StreamReader(isoStream))
                {
                    file = reader.ReadToEnd();
                }
            }

            //parse
            _wrapperData = JsonReader.Deserialize<WrapperData>(file);
        }
#else
        string prefix = WrapperName.Equals("") ? "" : WrapperName + ".";
        _wrapperData.ProfileId = PlayerPrefs.GetString(prefix + PREFS_PROFILE_ID);
        _wrapperData.AnonymousId = PlayerPrefs.GetString(prefix + PREFS_ANONYMOUS_ID);
        _wrapperData.AuthenticationType = PlayerPrefs.GetString(prefix + PREFS_AUTHENTICATION_TYPE);
#endif
    }

    private class WrapperData
    {
        public string ProfileId = "";
        public string AnonymousId = "";
        public string AuthenticationType = "";

        public static readonly string FileName = "BrainCloudWrapper.json";
    }
}
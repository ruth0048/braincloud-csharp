﻿//----------------------------------------------------
// brainCloud client source code
// Copyright 2016 bitHeads, inc.
//----------------------------------------------------

namespace BrainCloud
{
    public static class ReasonCodes
    {
        public const int NO_REASON_CODE = 0;

        public const int INVALID_NOTIFICATION = 20200;

        public const int INVALID_REQUEST = 40001;

        public const int SWITCHING_FACEBOOK_MEMORY = 40201;
        public const int MERGING_MEMORY = 40202;
        public const int RECREATING_ANONYMOUS_MEMORY = 40203;
        public const int MOVING_ANONYMOUS_MEMORY = 40204;
        public const int LOGIN_SECURITY_ERROR = 40209;

        /// <summary>This means that you have provided a profile id
        /// but the identity lookup fails to find an identity entry.
        /// </summary>
        public const int MISSING_IDENTITY_ERROR = 40206;

        /// <summary>This means that you have provided a profile id and an
        /// identity that is matched to a different profile id.
        /// This is where you blank out the profile to allow the switch.
        /// </summary>
        public const int SWITCHING_PROFILES = 40207;

        /// <summary>This means that you provide a blank profile id and the
        /// identity doesn't exist and forecCreate is false.
        ///</summary>
        public const int MISSING_PROFILE_ERROR = 40208;

        /// <summary>Raised when a security error occurs
        /// </summary>
        public const int SECURITY_ERROR = 40209;

        /// <summary>This happens when you try and detach the last non-anonymous
        /// identity from an account with confirmAnonymous set to false.
        /// </summary>
        public const int DOWNGRADING_TO_ANONYMOUS_ERROR = 40210;

        /// <summary>This occurs when you try and attach an identity type that already exists for that profile.
        /// You can have only one facebook identity for a particular profile
        /// </summary>
        public const int DUPLICATE_IDENTITY_TYPE = 40211;


        public const int MERGE_PROFILES = 40212;
        public const int INVALID_PROPERTY_NAME = 40213;
        public const int EMAIL_NOT_VALIDATED = 40214;
        public const int DATABASE_ERROR = 40215;
        public const int PROPERTY_NOT_OVERRIDEABLE = 40216;
        public const int UNKNOWN_AUTH_ERROR = 40217;

        public const int UNABLE_TO_GET_FRIENDS_FROM_FACEBOOK = 40300;
        public const int BAD_SIGNATURE = 40301;

        /// <summary>Cannot validate user
        /// </summary>
        public const int UNABLE_TO_VALIDATE_PLAYER = 40302;
        public const int UNABLE_TO_VALIDATE_USER = UNABLE_TO_VALIDATE_PLAYER;

        /// <summary>Session expired</summary>
        public const int PLAYER_SESSION_EXPIRED = 40303;
        public const int USER_SESSION_EXPIRED = PLAYER_SESSION_EXPIRED;


        /// <summary>SESSION NOT FOUND ERROR</summary>
        public const int NO_SESSION = 40304;

        public const int PLAYER_SESSION_MISMATCH = 40305;
        public const int USER_SESSION_MISMATCH = PLAYER_SESSION_MISMATCH;
        public const int OPERATION_REQUIRES_A_SESSION = 40306;

        /// <summary>User provided the wrong email and / or password</summary>
        public const int TOKEN_DOES_NOT_MATCH_USER = 40307;

        public const int EVENT_CAN_ONLY_SEND_TO_FRIEND_OR_SELF = 40309;
        public const int NOT_FRIENDS = 40310;
        public const int VC_BALANCE_CANNOT_BE_SPECIFIED = 40311;
        public const int VC_LIMIT_EXCEEDED = 40312;
        public const int UNABLE_TO_GET_MY_DATA_FROM_FACEBOOK = 40313;

        /// <summary>This happens when you provide invalid auth type string in either service.</summary>
        public const int INVALID_AUTHENTICATION_TYPE = 40315;

        /// <summary>You must have an active session in order to call this api</summary>
        public const int INVALID_GAME_ID = 40316;
        public const int INVALID_APP_ID = INVALID_GAME_ID;

        /// <summary>This product and receipt have already been claimed</summary>
        public const int APPLE_TRANS_ID_ALREADY_CLAIMED = 40317;

        public const int CLIENT_VERSION_NOT_SUPPORTED = 40318;
        public const int BRAINCLOUD_VERSION_NOT_SUPPORTED = 40319;
        public const int PLATFORM_NOT_SUPPORTED = 40320;
        public const int INVALID_PLAYER_STATISTICS_EVENT_NAME = 40321;
        public const int INVALID_USER_STATISTICS_EVENT_NAME = INVALID_PLAYER_STATISTICS_EVENT_NAME;

        /// <summary>App Version No Longer Supported</summary>
        public const int GAME_VERSION_NOT_SUPPORTED = 40322;
        public const int APP_VERSION_NOT_SUPPORTED = GAME_VERSION_NOT_SUPPORTED;

        public const int BAD_REFERENCE_DATA = 40324;
        public const int MISSING_OAUTH_TOKEN = 40325;
        public const int MISSING_OAUTH_VERIFIER = 40326;
        public const int MISSING_OAUTH_TOKEN_SECRET = 40327;
        public const int MISSING_TWEET = 40328;
        public const int FACEBOOK_PAYMENT_ID_ALREADY_PROCESSED = 40329;
        public const int DISABLED_GAME = 40330;
        public const int DISABLED_APP = DISABLED_GAME;
        public const int MATCH_MAKING_DISABLED = 40331;
        public const int UPDATE_FAILED = 40332;
        public const int INVALID_OPERATION = 40333;  // invalid operation for API call

        /// <summary>Player is currently in a match</summary>
        public const int MATCH_RANGE_ERROR = 40334;

        /// <summary>Player is currently in a match</summary>
        public const int PLAYER_IN_MATCH = 40335;

        /// <summary>Player is currently shielded</summary>
        public const int MATCH_PLAYER_SHIELDED = 40336;

        public const int MATCH_PLAYER_MISSING = 40337;

        /// <summary>Player is currently logged in</summary>
        public const int MATCH_PLAYER_LOGGED_IN = 40338;

        public const int INVALID_ITEM_ID = 40339;
        public const int MISSING_PRICE = 40340;
        public const int MISSING_USER_INFO = 40341;
        public const int MISSING_STEAM_RESPONSE = 40342;
        public const int MISSING_STEAM_TRANSACTION = 40343;
        public const int ENTITY_VERSION_MISMATCH = 40344;
        public const int MISSING_RECORD = 40345;
        public const int INSUFFICIENT_PERMISSIONS = 40346;
        public const int MISSING_IN_QUERY = 40347;
        public const int RECORD_EXPIRED = 40348;
        public const int INVALID_WHERE = 40349;
        public const int S3_ERROR = 40350;
        public const int INVALID_ATTRIBUTES = 40351;
        public const int IMPORT_MISSING_GAME_DATA = 40352;
        public const int IMPORT_MISSING_APP_DATA = IMPORT_MISSING_GAME_DATA;
        public const int IMPORT_SCHEMA_VERSION_TOO_OLD = 40353;
        public const int IMPORT_SCHEMA_VERSION_INVALID = 40355;

        /// <summary>Max Concurrent User Count Reached</summary>
        public const int PLAYER_SESSION_LOGGED_OUT = 40356;
        public const int USER_SESSION_LOGGED_OUT = PLAYER_SESSION_LOGGED_OUT;

        public const int API_HOOK_SCRIPT_ERROR = 40357;
        public const int MISSING_REQUIRED_PARAMETER = 40358;
        public const int INVALID_PARAMETER_TYPE = 40359;
        public const int INVALID_IDENTITY_TYPE = 40360;
        public const int EMAIL_SEND_ERROR = 40361;
        public const int CHILD_ENTITY_PARTIAL_UPDATE_INVALID_DATA = 40362;
        public const int MISSING_SCRIPT = 40363;
        public const int SCRIPT_SECURITY_ERROR = 40364;
        public const int SERVER_SESSION_EXPIRED = 40365;
        public const int STREAM_DOES_NOT_EXIT = 40366;
        public const int STREAM_ACCESS_ERROR = 40367;
        public const int STREAM_COMPLETE = 40368;
        public const int INVALID_STATISTIC_NAME = 40369;
        public const int INVALID_HTTP_REQUEST = 40370;
        public const int GAME_LIMIT_REACHED = 40371;
        public const int APP_LIMIT_REACHED = GAME_LIMIT_REACHED;
        public const int GAME_RUNSTATE_DISABLED = 40372;
        public const int APP_RUNSTATE_DISABLED = GAME_RUNSTATE_DISABLED;
        public const int INVALID_COMPANY_ID = 40373;
        public const int INVALID_PLAYER_ID = 40374;
        public const int INVALID_USER_ID = INVALID_PLAYER_ID;
        public const int INVALID_TEMPLATE_ID = 40375;
        public const int MINIMUM_SEARCH_INPUT = 40376;

        /// <summary>App has to parent to switch profile to</summary>
        public const int MISSING_GAME_PARENT = 40377;
        public const int MISSING_APP_PARENT = MISSING_GAME_PARENT;

        public const int GAME_PARENT_MISMATCH = 40378;
        public const int GAME_APP_MISMATCH = GAME_PARENT_MISMATCH;
        public const int CHILD_PLAYER_MISSING = 40379;
        public const int CHILD_USER_MISSING = CHILD_PLAYER_MISSING;
        public const int MISSING_PLAYER_PARENT = 40380;
        public const int MISSING_USER_PARENT = MISSING_PLAYER_PARENT;
        public const int PLAYER_PARENT_MISMATCH = 40381;
        public const int USER_PARENT_MISMATCH = PLAYER_PARENT_MISMATCH;
        public const int MISSING_PLAYER_ID = 40382;
        public const int MISSING_APP_ID = MISSING_PLAYER_ID;
        public const int DECODE_CONTEXT = 40383;
        public const int INVALID_QUERY_CONTEXT = 40384;
        public const int GROUP_MEMBER_NOT_FOUND = 40385;
        public const int INVALID_SORT = 40386;
        public const int GAME_NOT_FOUND = 40387;
        public const int APP_NOT_FOUND = GAME_NOT_FOUND;
        public const int GAMES_NOT_IN_SAME_COMPANY = 40388;
        public const int APPS_NOT_IN_SAME_COMPANY = GAMES_NOT_IN_SAME_COMPANY;
        public const int IMPORT_NO_PARENT_ASSIGNED = 40389;
        public const int IMPORT_PARENT_CURRENCIES_MISMATCH = 40390;
        public const int INVALID_SUBSTITUION_ENTRY = 40391;
        public const int INVALID_TEMPLATE_STRING = 40392;
        public const int TEMPLATE_SUBSTITUTION_ERROR = 40393;
        public const int INVALID_OPPONENTS = 40394;
        public const int REDEMPTION_CODE_NOT_FOUND = 40395;
        public const int REDEMPTION_CODE_VERSION_MISMATCH = 40396;
        public const int REDEMPTION_CODE_ACTIVE = 40397;
        public const int REDEMPTION_CODE_NOT_ACTIVE = 40398;
        public const int REDEMPTION_CODE_TYPE_NOT_FOUND = 40399;
        public const int REDEMPTION_CODE_INVALID = 40400;
        public const int REDEMPTION_CODE_REDEEMED = 40401;
        public const int REDEMPTION_CODE_REDEEMED_BY_SELF = 40402;
        public const int REDEMPTION_CODE_REDEEMED_BY_OTHER = 40403;
        public const int SCRIPT_EMPTY = 40404;
        public const int ITUNES_COMMUNICATION_ERROR = 40405;
        public const int ITUNES_NO_RESPONSE = 40406;
        public const int ITUNES_RESPONSE_NOT_OK = 40407;
        public const int JSON_PARSING_ERROR = 40408;
        public const int ITUNES_NULL_RESPONSE = 40409;
        public const int ITUNES_RESPONSE_WITH_NULL_STATUS = 40410;
        public const int ITUNES_STATUS_BAD_JSON_RECEIPT = 40411;
        public const int ITUNES_STATUS_BAD_RECEIPT = 40412;
        public const int ITUNES_STATUS_RECEIPT_NOT_AUTHENTICATED = 40413;
        public const int ITUNES_STATUS_BAD_SHARED_SECRET = 40414;
        public const int ITUNES_STATUS_RECEIPT_SERVER_UNAVAILABLE = 40415;
        public const int ITUNES_RECEIPT_MISSING_ITUNES_PRODUCT_ID = 40416;
        public const int PRODUCT_NOT_FOUND_FOR_ITUNES_PRODUCT_ID = 40417;
        public const int DATA_STREAM_EVENTS_NOT_ENABLED = 40418;
        public const int INVALID_DEVICE_TOKEN = 40419;
        public const int ERROR_DELETING_DEVICE_TOKEN = 40420;
        public const int WEBPURIFY_NOT_CONFIGURED = 40421;
        public const int WEBPURIFY_EXCEPTION = 40422;
        public const int WEBPURIFY_FAILURE = 40423;
        public const int WEBPURIFY_NOT_ENABLED = 40424;
        public const int NAME_CONTAINS_PROFANITY = 40425;
        public const int NULL_SESSION = 40426;
        public const int PURCHASE_ALREADY_VERIFIED = 40427;
        public const int GOOGLE_IAP_NOT_CONFIGURED = 40428;
        public const int UPLOAD_FILE_TOO_LARGE = 40429;
        public const int FILE_ALREADY_EXISTS = 40430;
        public const int CLOUD_STORAGE_SERVICE_ERROR = 40431;
        public const int FILE_DOES_NOT_EXIST = 40432;
        public const int UPLOAD_ID_MISSING = 40433;
        public const int UPLOAD_JOB_MISSING = 40434;
        public const int UPLOAD_JOB_EXPIRED = 40435;
        public const int UPLOADER_EXCEPTION = 40436;
        public const int UPLOADER_FILESIZE_MISMATCH = 40437;
        public const int PUSH_NOTIFICATIONS_NOT_CONFIGURED = 40438;
        public const int MATCHMAKING_FILTER_SCRIPT_FAILURE = 40439;
        public const int ACCOUNT_ALREADY_EXISTS = 40440;
        public const int PROFILE_ALREADY_EXISTS = 40441;
        public const int MISSING_NOTIFICATION_BODY = 40442;
        public const int INVALID_SERVICE_CODE = 40443;
        public const int IP_ADDRESS_BLOCKED = 40444;
        public const int UNAPPROVED_SERVICE_CODE = 40445;
        public const int PROFILE_NOT_FOUND = 40446;
        public const int ENTITY_NOT_SHARED = 40447;
        public const int SELF_FRIEND = 40448;
        public const int PARSE_NOT_CONFIGURED = 40449;
        public const int PARSE_NOT_ENABLED = 40450;
        public const int PARSE_REQUEST_ERROR = 40451;
        public const int GROUP_CANNOT_ADD_OWNER = 40452;
        public const int NOT_GROUP_MEMBER = 40453;
        public const int INVALID_GROUP_ROLE = 40454;
        public const int GROUP_OWNER_DELETE = 40455;
        public const int NOT_INVITED_GROUP_MEMBER = 40456;
        public const int GROUP_IS_FULL = 40457;
        public const int GROUP_OWNER_CANNOT_LEAVE = 40458;
        public const int INVALID_INCREMENT_VALUE = 40459;
        public const int GROUP_VERSION_MISMATCH = 40460;
        public const int GROUP_ENTITY_VERSION_MISMATCH = 40461;
        public const int INVALID_GROUP_ID = 40462;
        public const int INVALID_FIELD_NAME = 40463;
        public const int UNSUPPORTED_AUTH_TYPE = 40464;
        public const int CLOUDCODE_JOB_NOT_FOUND = 40465;
        public const int CLOUDCODE_JOB_NOT_SCHEDULED = 40466;
        public const int GROUP_TYPE_NOT_FOUND = 40467;
        public const int MATCHING_GROUPS_NOT_FOUND = 40468;
        public const int GENERATE_CDN_URL_ERROR = 40469;
        public const int INVALID_PROFILE_IDS = 40470;
        public const int MAX_PROFILE_IDS_EXCEEDED = 40471;
        public const int PROFILE_ID_MISMATCH = 40472;
        public const int LEADERBOARD_DOESNOT_EXIST = 40473;
        public const int APP_LICENSING_EXCEEDED = 40474;
        public const int SENDGRID_NOT_INSTALLED = 40475;
        public const int SENDGRID_EMAIL_SEND_ERROR = 40476;
        public const int SENDGRID_NOT_ENABLED_FOR_APP = 40477;
        public const int SENDGRID_GET_TEMPLATES_ERROR = 40478;
        public const int SENDGRID_INVALID_API_KEY = 40479;
        public const int EMAIL_SERVICE_NOT_CONFIGURED = 40480;
        public const int INVALID_EMAIL_TEMPLATE_TYPE = 40481;
        public const int SENDGRID_KEY_EMPTY_OR_NULL = 40482;
        public const int BODY_TEMPLATE_CANNOT_COEXIST = 40483;
        public const int SUBSTITUTION_BODY_CANNOT_COEXIST = 40484;
        public const int INVALID_FROM_ADDRESS = 40485;
        public const int INVALID_FROM_NAME = 40486;
        public const int INVALID_REPLY_TO_ADDRESS = 40487;
        public const int INVALID_REPLY_TO_NAME = 40488;
        public const int FROM_NAME_WITHOUT_FROM_ADDRESS = 40489;
        public const int REPLY_TO_NAME_WITHOUT_REPLY_TO_ADDRESS = 40490;
        public const int CURRENCY_SECURITY_ERROR = 40491;
        public const int INVALID_PEER_CODE = 40492;
        public const int PEER_NO_LONGER_EXISTS = 40493;

        public const int CANNOT_MODIFY_TOURNAMENT_WITH_LEADERBOARD_SERVICE = 40494;
        public const int NO_TOURNAMENT_ASSOCIATED_WITH_LEADERBOARD = 40495;
        public const int TOURNAMENT_NOT_ASSOCIATED_WITH_LEADERBOARD = 40496;
        public const int PLAYER_ALREADY_TOURNAMENT_FOR_LEADERBOARD = 40497;
        public const int PLAYER_EARLY_FOR_JOINING_TOURNAMENT = 40498;
        public const int NO_LEADERBOARD_FOUND = 40499;
        public const int PLAYER_NOT_IN_TIME_RANGE_FOR_POSTSCORE_TOURNAMENT = 40500;
        public const int LEADERBOARD_ID_BAD = 40501;
        public const int SCORE_INPUT_BAD = 40502;
        public const int ROUND_STARTED_EPOCH_INPUT_BAD = 40503;
        public const int TOURNAMENT_CODE_INPUT_BAD = 40504;
        public const int PLAYER_NOT_ENROLLED_IN_TOURNAMENT = 40505;
        public const int LEADERBOARD_VERSION_ID_INVALID = 40506;
        public const int NOT_ENOUGH_BALANCE_TO_JOIN_TOURNAMENT = 40507;
        public const int PARENT_ALREADY_ATTACHED = 40508;
        public const int PEER_ALREADY_ATTACHED = 40509;
        public const int IDENTITY_NOT_ATTACHED_WITH_PARENT = 40510;
        public const int IDENTITY_NOT_ATTACHED_WITH_PEER = 40511;
        public const int LEADERBOARD_SCORE_UPDATE_ERROR = 40512;
        public const int ERROR_CLAIMING_REWARD = 40513;
        public const int NOT_ENOUGH_PARENT_BALANCE_TO_JOIN_TOURNAMENT = 40514;
        public const int NOT_ENOUGH_PEER_BALANCE_TO_JOIN_TOURNAMENT = 40515;
        public const int PLAYER_LATE_FOR_JOINING_TOURNAMENT = 40516;
        public const int VIEWING_REWARD_FOR_NON_PROCESSED_TOURNAMENTS = 40517;
        public const int NO_REWARD_ASSOCIATED_WITH_LEADERBOARD = 40518;
        public const int PROFILE_PEER_NOT_FOUND = 40519;
        public const int LEADERBOARD_IN_ACTIVE_STATE = 40520;
        public const int LEADERBOARD_IN_CALCULATING_STATE = 40521;
        public const int TOURNAMENT_RESULT_PROCESSING_FAILED = 40522;
        public const int TOURNAMENT_REWARDS_ALREADY_CLAIMED = 40523;
        public const int NO_TOURNAMENT_FOUND = 40524;
        public const int UNEXPECTED_ERROR_RANK_ZERO_AFTER_PROCESSING = 40525;
        public const int UNEXPECTED_ERROR_DELETING_TOURNAMENT_LEADERBOARD_SCORE = 40526;
        public const int INVALID_RUN_STATE = 40527;
        public const int LEADERBOARD_SCORE_DOESNOT_EXIST = 40528;
        public const int INITIAL_SCORE_NULL = 40529;
        public const int TOURNAMENT_NOTIFICATIONS_PROCESSING_FAILED = 40530;
        public const int ACL_NOT_READABLE = 40531;
        public const int INVALID_OWNER_ID = 40532;
        public const int IMPORT_MISSING_PEERS_DATA = 40533;
        public const int INVALID_CREDENTIAL = 40534;

        public const int MESSAGING_FEATURE_NOT_CONFIGURED = 40593;
        public const int CHAT_FEATURE_NOT_CONFIGURED = 40594;

        public const int NO_TWITTER_CONSUMER_KEY = 500001;
        public const int NO_TWITTER_CONSUMER_SECRET = 500002;
        public const int INVALID_CONFIGURATION = 500003;
        public const int ERROR_GETTING_REQUEST_TOKEN = 500004;
        public const int ERROR_GETTING_ACCESS_TOKEN = 500005;

        public const int FACEBOOK_ERROR = 500010;
        public const int FACEBOOK_SECRET_MISMATCH = 500011;
        public const int FACEBOOK_AUTHENTICATION_ERROR = 500012;
        public const int FACEBOOK_APPLICATION_TOKEN_REQUEST_ERROR = 500013;
        public const int FACEBOOK_BAD_APPLICATION_TOKEN_SIGNATURE = 500014;

        public const int MONGO_DB_EXCEPTION = 600001;

        /// <summary>
        /// Client defined value for a timeout detected client-side.
        /// </summary>
        public const int CLIENT_NETWORK_ERROR_TIMEOUT = 90001;
        public const int CLIENT_UPLOAD_FILE_CANCELLED = 90100;
        public const int CLIENT_UPLOAD_FILE_TIMED_OUT = 90101;
        public const int CLIENT_UPLOAD_FILE_UNKNOWN = 90102;
        public const int CLIENT_DISABLED = 90200;
        public const int DATABASE_INPUT_TOO_LARGE_ERROR = 40218;
        public const int MISSING_APP_EMAIL_ACCOUNT = 40219;
        public const int INVALID_DATABASE_FIELD_NAME = 40347;
        public const int INVALID_AMOUNT = 40385;
        public const int GLOBAL_ENTITY_SECURITY_ERROR = 40535;
        public const int LEADERBOARD_SECURITY_ERROR = 40536;
        public const int NOT_A_SYSTEM_ENTITY = 40537;
        public const int CONTROLLER_ERROR = 40538;
        public const int EVENT_MISSING = 40539;
        public const int INVALID_XP_LEVEL = 40540;
        public const int INVALID_ITUNES_ID = 40541;
        public const int IMPORT_ERROR = 40542;
        public const int INVALID_ENTITY_TYPE = 40543;
        public const int FORM_ERROR = 40544;
        public const int INVALID_PARENT = 40545;
        public const int INVALID_CURRENCY = 40546;
        public const int INVALID_THRESHHOLD = 40547;
        public const int MATCH_ALREADY_EXISTS = 40548;
        public const int FRIEND_NOT_FOUND = 40549;
        public const int MATCH_NOT_FOUND = 40550;
        public const int MATCH_COMPLETE = 40551;
        public const int MATCH_NOT_STARTED = 40552;
        public const int MATCH_EXPIRED = 40553;
        public const int PLAYER_NOT_IN_MATCH = 40554;
        public const int INVALID_MATCH_VERSION = 40555;
        public const int INVALID_TURN_VERSION = 40556;
        public const int INVALID_DEVICE_TYPE = 40557;
        public const int DUPLICATE_ENTITY = 40558;
        public const int DUPLICATE_EVENT = 40559;
        public const int INVALID_LEADERBOARD_COUNT = 40560;
        public const int DUPLICATE_LEADERBOARD = 40561;
        public const int MICROSOFT_ERROR = 40562;
        public const int DUPLICATE_TOURNAMENT = 40563;
        public const int CREATE_SYSTEM_ENTITY_FAILED = 40564;
        public const int INVALID_MAX_NUM_STREAMS = 40565;
        public const int INVALID_PACKET_ID = 40566;
        public const int HOOK_ERROR = 40567;
        public const int INVALID_STREAM_ID = 40568;
        public const int INVALID_SCAN_CODE = 40569;
        public const int NO_CUSTOM_ENTITY_CONFIG_FOUND = 40570;
        public const int NO_CUSTOM_ENTITY_FOUND = 40571;
        public const int CLOUD_STORAGE_ERROR = 40572;
        public const int NO_CUSTOM_FIELD_CONFIG_FOUND = 40573;
        public const int MISSING_CUSTOM_ENTITY_QUERY = 40574;
        public const int INVALID_CUSTOM_ENTITY_JSON_WHERE = 40575;
        public const int INVALID_CUSTOM_ENTITY_JSON_FIELDS = 40576;
        public const int ENTITY_ID_NOT_CONFIGURED = 40577;
        public const int UNCONFIGURED_CUSTOM_FIELD_ERROR = 40578;
        public const int CUSTOM_ENTITY_SECURITY_ERROR = 40579;
        public const int CUSTOM_ENTITY_PARTIAL_UPDATE_INVALID_DATA = 40580;
        public const int TOURNAMENT_PLAY_HAS_NOT_STARTED = 40581;
        public const int TOURNAMENT_PLAY_HAS_ENDED = 40582;
        public const int NEW_CREDENTIAL_IN_USE = 40583;
        public const int OLD_CREDENTIAL_NOT_OWNED = 40584;
        public const int CLOUD_CODE_SECURITY_ERROR = 40585;
        public const int RTT_SERVER_NOT_FOUND = 40586;
        public const int RTT_CLIENT_NOT_FOUND = 40587;
        public const int NO_RTT_SERVERS_AVAILABLE = 40588;
        public const int PROFILE_SESSION_MISMATCH = 40589;
        public const int WAITING_FOR_ON_DEMAND_TOURNAMENT_TO_START = 40590;
        public const int CDN_URLS_NOT_SUPPORTED = 40591;
        public const int CLOUD_CONTAINER_ERROR = 40592;
        public const int MESSAGE_NOT_FOUND = 40595;
        public const int COLLECTION_CREATE_DISABLED = 40596;
        public const int LEADERBAORD_COLLECTION_CREATE_DISABLED = 40597;
        public const int MESSAGE_VERSION_MISMATCH = 40598;
        public const int MESSAGEBOX_VERSION_MISMATCH = 40599;
        public const int MESSAGE_TOO_LARGE = 40600;
        public const int FEATURE_NOT_ENABLED = 40601;
        public const int CHANNEL_NOT_FOUND = 40603;
        public const int MALFORMED_FORM_DATA = 40604;
        public const int MISSING_LAST_PACKET_RESPONSE = 40605;
        public const int PACKET_IN_PROGRESS = 40606;
        public const int LOBBY_MEMBER_NOT_FOUND = 40607;
        public const int LOBBY_TEAM_NOT_FOUND = 40608;
        public const int LOBBY_ENTRY_QUEUE_MEMBER_NOT_FOUND = 40609;
        public const int INVALID_HEADER_APP_ID = 40610;
        public const int LOBBY_TYPE_NOT_FOUND = 40611;
        public const int LOBBY_TEAM_FULL = 40612;
        public const int TWITTER_AUTH_ERROR = 500006;
        public const int TWITTER_ERROR = 500007;
        public const int NOT_TEAM_ADMIN = 550000;
        public const int NO_TEAM_ACCESS = 550001;
        public const int MISSING_COMPANY_RECORD = 550002;
        public const int TEAM_MEMBER_NOT_FOUND = 550003;
        public const int TEAM_MEMBER_NOT_ENABLED = 550004;
        public const int TEAM_MEMBER_NOT_ACTIVE = 550005;
        public const int TEAM_MEMBER_LOCKED = 550006;
        public const int INVALID_PASSWORD = 550007;
        public const int TOKEN_INVALID = 550008;
        public const int TOKEN_EXPIRED = 550009;
        public const int TEMPLATE_GAME_NOT_FOUND = 550011;
        public const int INVALID_TEMPLATE_GAME_TEAM = 550012;
        public const int BASIC_AUTH_FAILURE = 550013;
        public const int CONCURRENT_LOCK_ERROR = 600002;
    }
}

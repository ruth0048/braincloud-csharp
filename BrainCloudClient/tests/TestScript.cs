using NUnit.Core;
using NUnit.Framework;
using BrainCloud;
using System.Collections.Generic;
using JsonFx.Json;
using System;

namespace BrainCloudTests
{
    [TestFixture]
    public class TestScript : TestFixtureBase
    {
        private readonly string _scriptName = "testScript";

        [Test]
        public void TestRunScript()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.ScriptService.RunScript(
                _scriptName,
                Helpers.CreateJsonPair("testParm1", 1),
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestScheduleScriptUTC()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.ScriptService.ScheduleRunScriptUTC(
                _scriptName,
                Helpers.CreateJsonPair("testParm1", 1),
                DateTime.Now.AddDays(1),
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestScheduleScriptMinutesFromNow()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.ScriptService.ScheduleRunScriptMinutes(
                _scriptName,
                Helpers.CreateJsonPair("testParm1", 1),
                60,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestCancelJob()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.ScriptService.ScheduleRunScriptMinutes(
                _scriptName,
                Helpers.CreateJsonPair("testParm1", 1),
                60,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();

            var data = (Dictionary<string, object>)tr.m_response["data"];
            string jobId = data["jobId"] as string;

            BrainCloudClient.Instance.ScriptService.CancelScheduledScript(
                jobId,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestRunParentScript()
        {
            GoToChildProfile();

            TestResult tr = new TestResult();
            BrainCloudClient.Instance.ScriptService.RunParentScript(
                _scriptName,
                Helpers.CreateJsonPair("testParm1", 1), ParentLevel,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }
    }
}
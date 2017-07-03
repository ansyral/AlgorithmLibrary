namespace XuanLibrary.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using XuanLibrary.Fx;
    using XuanLibrary.Utility;

    using Xunit;

    public class AmbientConfigurationTest
    {
        private const string WorkerConfigurationUri = "WorkerConfigurationUri";
        private const string EncryptKey = "EncryptKey";
        private const string Key_StorageConnectionString = "StorageConnectionString";
        private const string Key_Accessor = "Accessor";
        private const string Key_QueueDefaultMaxMessageCount = "QueueDefaultMaxMessageCount";

        [Fact]
        public void TestBasic()
        {
            // var result = EnDecriptUtility.Encrypt("hello", "12345");
            // check config original content
            string originalValue_StorageConnectionString = "hello";
            string originalValue_Accessor = "Build.DataAccessor.Local.Accessor";
            long originalValue_QueueDefaultMaxMessageCount = 400;
            Assert.Equal(originalValue_StorageConnectionString, Config.StorageConnectionString);
            Assert.Equal(originalValue_Accessor, Config.Accessor);
            Assert.Equal(originalValue_QueueDefaultMaxMessageCount, Config.QueueDefaultMaxMessageCount);

            try
            {
                // update config content
                UpdateConfigFile<Dictionary<string, object>>("TestData/worker.json",
                    content =>
                    {
                        content[Key_QueueDefaultMaxMessageCount] = 300;
                        content[Key_Accessor] = "Next";
                    });
                Thread.Sleep(1500);
                Assert.Equal("Next", Config.Accessor);
                Assert.Equal(300l, Config.QueueDefaultMaxMessageCount);
            }
            finally
            {
                // restore original config content
                UpdateConfigFile<Dictionary<string, object>>("TestData/worker.json",
                    content =>
                    {
                        content[Key_QueueDefaultMaxMessageCount] = originalValue_QueueDefaultMaxMessageCount;
                        content[Key_Accessor] = originalValue_Accessor;
                    });
            }
        }

        private static void UpdateConfigFile<T>(string filePath, Action<T> updater)
        {
            var content = JsonUtility.Deserialize<T>(filePath);
            updater(content);
            JsonUtility.Serialize(filePath, content);
        }

        internal static class Config
        {
            public static readonly AmbientConfiguration Worker = AmbientConfigurationFactory.Create(WorkerConfigurationUri, EncryptKey, true);
            public static readonly AmbientConfigurationEntry<string> StorageConnectionString = Worker.CreateEntry<string>(Key_StorageConnectionString, null, null, true);
            public static readonly AmbientConfigurationEntry<string> Accessor = Worker.CreateEntry(Key_Accessor, "Build.DataAccessor.Azure.Accessor", null, true);
            public static readonly AmbientConfigurationEntry<long> QueueDefaultMaxMessageCount = Worker.CreateEntry(Key_QueueDefaultMaxMessageCount, 500l, null, true);
        }
    }
}

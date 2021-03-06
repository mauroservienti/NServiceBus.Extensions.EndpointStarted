﻿using System;
using System.IO;
using NUnit.Framework;

namespace NServiceBus.Extensions.EndpointStarted.AcceptanceTests.Config
{
    class StorageUtils
    {
        public static string GetAcceptanceTestingTransportStorageDirectory()
        {
            var testRunId = TestContext.CurrentContext.Test.ID;

            string tempDir;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                //can't use bin dir since that will be too long on the build agents
                tempDir = @"c:\temp";
            }
            else
            {
                tempDir = Path.GetTempPath();
            }

            return Path.Combine(tempDir, testRunId);
        }
    }
}
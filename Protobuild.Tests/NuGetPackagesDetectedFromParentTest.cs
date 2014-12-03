﻿namespace Protobuild.Tests
{
    using System.IO;
    using Xunit;

    public class NuGetPackagesDetectedFromParentTest : ProtobuildTest
    {
        [Fact]
        public void GenerationIsCorrect()
        {
            this.SetupTest("NuGetPackagesDetectedFromParent");

            this.Generate("Windows");

            Assert.True(File.Exists(this.GetPath(@"Module.Windows.sln")));
            Assert.True(File.Exists(this.GetPath(@"Submodule\Module.Windows.sln")));
            Assert.True(File.Exists(this.GetPath(@"Submodule\Console\Console.Windows.csproj")));

            var consoleContents = this.ReadFile(@"Submodule\Console\Console.Windows.csproj");

            Assert.Contains("portable-net4+sl5+wp8+win8+wpa81+MonoTouch+MonoAndroid", consoleContents);
            Assert.Contains("Test.dll", consoleContents);
            Assert.Contains("<HintPath>..\\..\\packages", consoleContents);
            Assert.DoesNotContain("<HintPath>packages", consoleContents);
            Assert.DoesNotContain("<HintPath>..\\packages", consoleContents);
        }
    }
}
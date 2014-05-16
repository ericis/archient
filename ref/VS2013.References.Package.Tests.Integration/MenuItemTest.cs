using System;
using System.Globalization;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace Archient.VS2013.References.Package.Tests.Integration
{
    using System.Reflection;

    [TestClass()]
    public class MenuItemTest
    {
        private delegate void ThreadInvoker();

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for lauching the command and closing the associated dialogbox
        ///</summary>
        [TestMethod()]
        [HostType("VS IDE")]
        public void LaunchCommand()
        {
            UIThreadInvoker.Invoke((ThreadInvoker)delegate()
            {
                CommandID menuItemCmd = new CommandID(GuidList.guidReferences_VS2013PackageCmdSet, (int)PkgCmdIDList.myExampleCommandID);

                Type packageType = typeof(ArchientReferencePackage);

                ProvideEditorExtensionAttribute provideEditorExtensionAttribute = 
                    packageType.GetCustomAttribute<ProvideEditorExtensionAttribute>(false);
                
                // Create the DialogBoxListener Thread.
                string expectedDialogBoxText = 
                    string.Format(
                        CultureInfo.CurrentCulture, 
                        "{0}\n\nInside {1}.MenuItemCallback()", 
                        provideEditorExtensionAttribute.DefaultName, 
                        packageType.FullName);

                DialogBoxPurger purger = new DialogBoxPurger(Microsoft.VsSDK.IntegrationTestLibrary.NativeMethods.IDOK, expectedDialogBoxText);

                try
                {
                    purger.Start();

                    TestUtils testUtils = new TestUtils();
                    testUtils.ExecuteCommand(menuItemCmd);
                }
                finally
                {
                    Assert.IsTrue(purger.WaitForDialogThreadToTerminate(), "The dialog box has not shown");
                }
            });
        }

    }
}

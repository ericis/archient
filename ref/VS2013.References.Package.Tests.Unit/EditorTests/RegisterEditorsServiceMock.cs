/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VsSDK.UnitTestLibrary;

namespace Archient.VS2013.References.Package.Tests.Unit.EditorTests
{
    static class RegisterEditorsServiceMock
    {
        private static GenericMockFactory registerEditorFactory;
        private static GenericMockFactory activityLogFactory;

        /// <summary>
        /// Returns an SVsRegisterEditors service that does not implement any methods
        /// </summary>
        /// <returns></returns>
        internal static BaseMock GetRegisterEditorsInstance()
        {
            if (registerEditorFactory == null)
                registerEditorFactory = new GenericMockFactory("SVsRegisterEditors", new[] { typeof(IVsRegisterEditors) });
            BaseMock registerEditor = registerEditorFactory.GetInstance();
            return registerEditor;
        }

        internal static BaseMock GetActivityLogInstance()
        {
            if (activityLogFactory == null)
                activityLogFactory = new GenericMockFactory("SVsActivityLog", new[] { typeof(IVsActivityLog) });
            BaseMock activityLog = activityLogFactory.GetInstance();
            return activityLog;
        }
    }
}

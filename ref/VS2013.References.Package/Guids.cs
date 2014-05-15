// Guids.cs
// MUST match guids.h
using System;

namespace Archient.VS2013.References.Package
{
    static class GuidList
    {
        public const string guidReferences_VS2013PackagePkgString = "739e9346-a4cd-4f9f-952b-c556d710ca16";
        public const string guidReferences_VS2013PackageCmdSetString = "6eb29108-b666-4201-bbc5-614f1062b4b6";
        public const string guidToolWindowPersistanceString = "fef863f3-4afd-44ad-80b4-191d81f8d690";
        public const string guidReferences_VS2013PackageEditorFactoryString = "3e2605e7-8830-45f9-afef-be099a45f239";

        public static readonly Guid guidReferences_VS2013PackageCmdSet = new Guid(guidReferences_VS2013PackageCmdSetString);
        public static readonly Guid guidReferences_VS2013PackageEditorFactory = new Guid(guidReferences_VS2013PackageEditorFactoryString);
    };
}
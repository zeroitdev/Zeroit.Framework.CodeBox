using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Zeroit.Framework.CodeBox")]
[assembly: AssemblyDescription("This is a control for rendering code")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Zeroit Dev")]
[assembly: AssemblyProduct("Zeroit.Framework.CodeBox")]
[assembly: AssemblyCopyright("Copyright © 2017 Zeroit Dev Technologies. All Rights Reserved.")]
[assembly: AssemblyTrademark("ZDT")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f8ac48e7-9378-482d-8c7f-92c8408dd4f2")]

// http://stackoverflow.com/a/65062
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyVersion("4.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("4.0.0")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]

#if (DEBUG)
// For my own personal testing of internal members
[assembly: InternalsVisibleToAttribute("Zeroit.Framework.CodeBox.Test")]
#endif

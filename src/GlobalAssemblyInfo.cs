using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: CLSCompliantAttribute(false)]
[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyProductAttribute("MG Framework")]
[assembly: AssemblyCopyrightAttribute("(c) 2008 - 2012 Mike Grabski All Rights Reserved")]
[assembly: AssemblyVersionAttribute("0.1")]
[assembly: AssemblyInformationalVersionAttribute("0.1")]
[assembly: AssemblyFileVersionAttribute("0.1")]
[assembly: AssemblyDelaySignAttribute(false)]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
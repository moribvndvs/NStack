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

static class AssemblyInfo
{
    public const string PublicKey = @"0024000004800000940000000602000000240000525341310004000001000100515c171c17fee1"
                                     + "64b7f0acd541f70d9671bf8504f10181f735d3b5298712dff24026ff65773e4a3512274d75e5b5"
                                     + "2f710b30e01697b7989107917abc837c84820d5e7288d48b9c338893050f49c81a1efb7e6ec2fc"
                                     + "a31e65209f916250bc3229914859dcf343512adde4632c237d8fe1cee259aace47e255e6354cb1"
                                     + "dfaa5bcf";
}
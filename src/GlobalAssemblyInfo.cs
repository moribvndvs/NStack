#region header
// <copyright file="GlobalAssemblyInfo.cs" company="mikegrabski.com">
//    Copyright 2012 Mike Grabski
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
#endregion

using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: CLSCompliantAttribute(false)]
[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyProductAttribute("NStack")]
[assembly: AssemblyCopyrightAttribute("(c) 2008 - 2012 Mike Grabski")]
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
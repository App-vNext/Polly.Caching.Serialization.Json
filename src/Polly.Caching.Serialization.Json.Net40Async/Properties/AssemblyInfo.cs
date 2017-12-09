using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Polly.Caching.Serialization.Json")]
// [assembly: CLSCompliant(false)] // Because Nito.AsycEx, on which Polly.Net40Async depends, is not CLSCompliant. // Not needed, because GlobalAssemblyInfo.cs also declares  CLSCompliant(false).  However, retained, to document the additional reason Polly.Net40Async is not CLS-compliant, in case GlobalAssemblyInfo.cs every moves to CLSCompliant(true)


[assembly: InternalsVisibleTo("Polly.Caching.Serialization.Json.Net40Async.Specs")]
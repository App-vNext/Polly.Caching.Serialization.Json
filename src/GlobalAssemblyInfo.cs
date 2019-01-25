using System;
using System.Reflection;

[assembly: AssemblyProduct("Polly.Caching.Serialization.Json")]
[assembly: AssemblyCompany("App vNext")]
[assembly: AssemblyDescription("Polly.Caching.Serialization.Json is a Newtonsoft.Json serialization plug-in for the Polly CachePolicy.  Polly is a library that allows developers to express resilience and transient fault handling policies such as Retry, Circuit Breaker, Timeout, Bulkhead Isolation, and Fallback in a fluent and thread-safe manner.")]
[assembly: AssemblyCopyright("Copyright (c) 2019, App vNext")]
[assembly: CLSCompliant(false)] // Because not all of Newtonsoft.Json, on which we depend, is CLSCompliant.

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

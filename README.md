# Polly.Caching.Serialization.Json

This repo contains a Json plugin for the [Polly](https://github.com/App-vNext/Polly) [Cache policy](https://github.com/App-vNext/Polly/wiki/Cache) using [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/).  It targets .NET Standard 1.1 and .NET Standard 2.0.

[![NuGet version](https://badge.fury.io/nu/Polly.Caching.Serialization.Json.svg)](https://badge.fury.io/nu/Polly.Caching.Serialization.Json) [![Build status](https://ci.appveyor.com/api/projects/status/pgd89nfdr9u4ig8m?svg=true)](https://ci.appveyor.com/project/joelhulen/polly-caching-serialization-json) [![Slack Status](http://www.pollytalk.org/badge.svg)](http://www.pollytalk.org)

## What is Polly?

[Polly](https://github.com/App-vNext/Polly) is a .NET resilience and transient-fault-handling library that allows developers to express policies such as Retry, Circuit Breaker, Timeout, Bulkhead Isolation, Cache-aside and Fallback in a fluent and thread-safe manner. Polly targets .NET Standard 1.1 and .NET Standard 2.0. 

Polly is a member of the [.NET Foundation](https://www.dotnetfoundation.org/about)!

**Keep up to date with new feature announcements, tips & tricks, and other news through [www.thepollyproject.org](http://www.thepollyproject.org)**

![](https://raw.github.com/App-vNext/Polly/master/Polly-Logo.png)

# Installing Polly.Caching.Serialization.Json via NuGet

    Install-Package Polly.Caching.Serialization.Json


# Supported targets

Polly.Caching.Serialization.Json supports .NET Standard 1.1 and .NET Standard 2.0.

## Dependencies

Polly.Caching.Serialization.Json requires:

+ [Polly](https://github.com/App-vNext/Polly) v6.0.1 or above
+ [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) v11.0.2 or above

# How to use the Polly.Caching.Serialization.Json plugin

These notes demonstrate how to use the [`Polly.Caching.Serialization.Json`](https://www.nuget.org/packages/polly.caching.serialization.json) serialization plugin in combination with the [`Polly.Caching.IDistributedCache`](https://www.nuget.org/packages/polly.caching.idistributedcache) cache provider plugin, such that you could effectively cache any type from a Polly [Cache policy](https://github.com/App-vNext/Polly/wiki/Cache) to Redis using Microsoft's [Redis implementation](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed) of [`IDistributedCache`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.distributed.idistributedcache).


For some `IDistributedCache distributedCache` instance (perhaps just configured and instantiated, perhaps provided to local code by Dependency Injection):

```csharp
// Create a Newtonsoft.Json.JsonSerializerSettings defining any settings to use for serialization
var serializerSettings = new JsonSerializerSettings()
{
    // Any configuration options
};

// Create a Polly cache policy for caching ProductDetails entities, using that IDistributedCache instance.
var productDetailsCachePolicy = Policy.CacheAsync<ProductDetails>(
    distributedCache.AsAsyncCacheProvider<string>().WithSerializer<ProductDetails, string>(
        new Polly.Caching.Serialization.Json.JsonSerializer<ProductDetails>(serializerSettings)
    ), 
    TimeSpan.FromMinutes(5) // for example
    /* for deeper CachePolicy configuration options: 
    -- see https://github.com/App-vNext/Polly/wiki/Cache#syntax
    -- and https://github.com/App-vNext/Polly.Caching.IDistributedCache */    
    );
```

Usage:

```csharp
string productId = // ... from somewhere
ProductDetails productDetails = await productDetailsCachePolicy.ExecuteAsync(ctx => getProductDetails(productId), 
    new Context(productId) // productId will also be the cache key used in this execution.
); 
```


# Release notes

For details of changes by release see the [change log](CHANGELOG.md).  


# Acknowledgements

* [@reisenberger](https://github.com/reisenberger) - Polly Json serializer using Newtonsoft.Json
* [@seanfarrow](https://github.com/seanfarrow) and [@reisenberger](https://github.com/reisenberger) - Caching and serialization architecture in the main Polly repo

# Instructions for Contributing

Please check out our [Wiki](https://github.com/App-vNext/Polly/wiki/Git-Workflow) for contributing guidelines. We are following the excellent GitHub Flow process, and would like to make sure you have all of the information needed to be a world-class contributor!

Since Polly is part of the .NET Foundation, we ask our contributors to abide by their [Code of Conduct](https://www.dotnetfoundation.org/code-of-conduct).

Also, we've stood up a [Slack](http://www.pollytalk.org) channel for easier real-time discussion of ideas and the general direction of Polly as a whole. Be sure to [join the conversation](http://www.pollytalk.org) today!

# License

Licensed under the terms of the [New BSD License](http://opensource.org/licenses/BSD-3-Clause)

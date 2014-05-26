namespace Archient.Services.Clients

open System

/// <summary>A "tag" interface for classes that use a service.</summary>
/// <typeparam name="TService">The type of the service used by the class.</typeparam>
[<Interface>]
type IUseService<'TService> = interface end

/// <summary>A contract for a class that contains a service.</summary>
/// <typeparam name="TService">The type of the service contained by the class.</typeparam>
[<Interface>]
type IServiceContainer<'TService> = 
    inherit IDisposable

    /// Gets the contained WCF service instance.
    abstract member Service : 'TService with get

/// <summary>A WCF service container.</summary>
/// <typeparam name="TService">The type of the WCF service.</typeparam>
[<Sealed>]
type ServiceContainer<'TService> =
    
    /// <summary>Creates a new WCF service container of the specified type.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    new : 'TService -> ServiceContainer<'TService>

    interface IServiceContainer<'TService>
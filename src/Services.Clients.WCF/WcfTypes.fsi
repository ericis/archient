namespace Archient.Services.Clients

open System

/// Tag interface for extension methods decorator pattern.
[<Interface>]
type IUseService<'TService> = interface end

/// Contract for a WCF service container.
[<Interface>]
type IServiceContainer<'TService> = 
    inherit IDisposable

    /// Gets the contained WCF service instance.
    abstract member Service : 'TService with get

/// <summary>A WCF service container.</summary>
/// <typeparam name="TService">The type of the WCF service.</typeparam>
[<Sealed>]
type ServiceContainer<'TService> =
    
    /// Creates a new WCF service container of the specified type.
    new : 'TService -> ServiceContainer<'TService>

    interface IServiceContainer<'TService>
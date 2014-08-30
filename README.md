# Archient

## What it is...

Archient is a set of code libraries (frameworks) that aim to enable developers through simple and consitent access to .NET technologies. Principles include:

  * Implementation consistency
  * Development guidance
  * Rapid development
  * Minimize code "plumbing"
  * Strategic use of package dependencies

## Libraries

All libraries are currently in experimental "alpha" stage.

  * [Contracts](src/Contracts/): A core set of interfaces shared by Archient's libraries.
  * [MVC](src/Web.Mvc/): Similar functions as Web API for consistent web applications.
    * Routing, Filtering, and Area functions
    * Startup functions
    * View virtualization - Host your views in CMS, cloud storage, or a database!
  * [Web API](src/Web.Api/): Routing and application startup functions for consistent web implementations
  * [Razor](src/Razor/): Planned helper library for embedded CMS. A simple wrapper on top of Microsoft Razor for ASP.NET. *The Microsoft Razor vNext library is actually included until a stable release is made available.
  * [Web Host](src/Web.Host/): Aims to simplify web application hosting between MVC, Web Forms and Web API.  [ASP.NET vNext](http://www.asp.net/vnext) will drastically improve the solution, but many of us will be running .NET 3.5, 4.0, and 4.5 for years to come.
  * [WCF Client](src/Services.Clients.WCF/): Utility functions for calling WCF services, encapsulating the scope of building up and tearing down client proxies
  * [WCF Services](src/Services.WCF/): Provides "Ping" and "Health Check" service and data contracts for building consistent services
  * [Leap Motion](src/LeapMotion/): A simple wrapper over the Leap Motion device's "Controller", extending the API with Reactive Extensions (Rx) for a simple subscription-based model of gesture interactions.
  * [Xunit](src/Test.Xunit/): A simple library of F# function wrappers on top of Xunit to help make F# testing look like F# code

## Visual Studio Extensions

In the near future, Archient will provide a set of quick-start project templates and item templates along with helpful "Wizard" functions to improve the consistency and quality of your .NET project development.

## What it is NOT...

  * A replacement of other popular libraries
  * Archient does not try to supplant other guidance, like [Microsoft's Enterprise Library Patterns and Practices](http://pnp.azurewebsites.net/en-us/)

### Architecture for .NET

Where possible, Archient will extend existing libraries as optional package dependencies to your project.  For example, if you need a general definition and/or implementation of a coding pattern, Archient will seek to provide it.  If you want a specific implementation of that pattern, Archient will attempt to either work with other package developers to implement the core patterns or to provide a wrapper package with an adapter implementation of that specific implementation mapped to the core pattern definitions in Archient.
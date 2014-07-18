# Archient

## What it is...

Archient is a set of code libraries (frameworks) that aim to provide:

  * Implementation consistency
  * Development guidance
  * Rapid development
  * Minimize code "plumbing"
  * Strategic use of package dependencies

## What it is NOT...

  * A replacement of other popular libraries
  * A supplement to other guidance, like [Microsoft's Enterprise Library Patterns and Practices](http://pnp.azurewebsites.net/en-us/)

### Architecture for .NET

Take what you want and leave the rest.  Archient's goal is not to address each of your project's unique needs or the systems that you must code to.  It is designed to address high-level concerns not already addressed by other libraries.

Where possible, Archient will extend existing libraries as optional package dependencies to your project.  For example, if you need a general definition and/or implementation of a coding pattern, Archient will seek to provide it.  If you want a specific implementation of that pattern, Archient will attempt to either work with other package developers to implement the core patterns or to provide a wrapper package with an adapter implementation of that specific implementation mapped to the core pattern definitions in Archient.
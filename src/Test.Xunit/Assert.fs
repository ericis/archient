namespace Archient.Testing

module Assert =
    
    open System

    open Xunit

    let areSame (expected:obj) (actual:obj) =
        ignore <| Assert.Same(expected, actual)

    let areNotSame (expected:obj) (actual:obj) =
        ignore <| Assert.NotSame(expected, actual)

    let areEqual<'T> (expected:'T) (actual:'T) =
        ignore <| Assert.Equal<_>(expected, actual)

    let areNotEqual<'T> (expected:'T) (actual:'T) =
        ignore <| Assert.NotEqual<_>(expected, actual)

    let isTrue (condition:bool) =
        ignore <| Assert.True(condition)

    let isFalse (condition:bool) =
        ignore <| Assert.False(condition)

    let isNull<'T> (instance:'T) =
        ignore <| Assert.Null(instance)

    let isNotNull<'T> (instance:'T) =
        ignore <| Assert.NotNull(instance)

    let isOfType<'T> (includeDerivedTypes:bool) (instance:obj) =
        match includeDerivedTypes with
        | true -> ignore <| isTrue (instance :? 'T)
        | false -> ignore <| Assert.IsType<'T>(instance)

    let isType<'T> (instance:obj) =
        instance |> isOfType<'T> false
        
    let isNotType<'T> (instance:obj) =
        ignore <| Assert.IsNotType<'T>(instance)

    let isAssignableFrom<'T> (instance:obj) =
        ignore <| Assert.IsAssignableFrom<'T>(instance)

    let throws<'T when 'T :> exn> (action:unit->unit) =
        ignore <| Assert.Throws<'T>(fun _ -> action())
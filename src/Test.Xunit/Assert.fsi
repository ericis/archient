namespace Archient.Testing

/// F# assertions library
module Assert =
    
    open System
    open Xunit

    /// <summary>Asserts if the two values are the exact same instances.</summary>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val areSame : expected:obj -> actual:obj -> unit

    /// <summary>Asserts if the two values are the exact same instances.</summary>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val isSame : expected:obj -> actual:obj -> unit
    
    /// <summary>Asserts if the two values are not the exact same instances.</summary>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val areNotSame : expected:obj -> actual:obj -> unit
    
    /// <summary>Asserts if the two values are not the exact same instances.</summary>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val isNotSame : expected:obj -> actual:obj -> unit
    
    /// <summary>Asserts if the two values are equivalent.</summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val areEqual<'T> : expected:'T -> actual:'T -> unit
    
    /// <summary>Asserts if the two values are equivalent.</summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val isEqual<'T> : expected:'T -> actual:'T -> unit
    
    /// <summary>Asserts if the two values are not equivalent.</summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val areNotEqual<'T> : expected:'T -> actual:'T -> unit
    
    /// <summary>Asserts if the two values are not equivalent.</summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    val isNotEqual<'T> : expected:'T -> actual:'T -> unit
    
    /// <summary>Asserts if the condition is <c>true</c>.</summary>
    /// <param name="condition">The condition to check.</param>
    val isTrue : condition:bool -> unit
    
    /// <summary>Asserts if the condition is <c>false</c>.</summary>
    /// <param name="condition">The condition to check.</param>
    val isFalse : condition:bool -> unit
    
    /// <summary>Asserts if the instance is <c>null</c>.</summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="instance">The instance to check.</param>
    val isNull<'T> : instance:'T -> unit
    
    /// <summary>Asserts if the instance is not <c>null</c>.</summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="instance">The instance to check.</param>
    val isNotNull<'T> : instance:'T -> unit
    
    /// <summary>Asserts if the instance is of the specified type, optionally including the inheritance hierarchy of derived types.</summary>
    /// <typeparam name="T">The type to check for.</typeparam>
    /// <param name="includeDerivedTypes">If <c>true</c>, checks if the instance type and all derived types match the specified type.</param>
    /// <param name="instance">The instance to check.</param>
    val isOfType<'T> : includeDerivedTypes:bool -> instance:obj -> unit
    
    /// <summary>Asserts if the instance is directly of the specified type, excluding all derived types.</summary>
    /// <typeparam name="T">The type to check for.</typeparam>
    /// <param name="instance">The instance to check.</param>
    val isType<'T> : instance:obj -> unit
    
    /// <summary>Asserts if the instance is not directly of the specified type, excluding all derived types.</summary>
    /// <typeparam name="T">The type to check for.</typeparam>
    /// <param name="instance">The instance to check.</param>
    val isNotType<'T> : instance:obj -> unit
    
    /// <summary>Asserts if the instance is directly assignable from the specified type.</summary>
    /// <typeparam name="T">The type to check for.</typeparam>
    /// <param name="instance">The instance to check.</param>
    val isAssignableFrom<'T> : instance:obj -> unit
    
    /// <summary>Asserts if the specified action throws the expected exception type.</summary>
    /// <typeparam name="T">The type of exception to expect.</typeparam>
    /// <param name="action">The action to run that is expected to throw an exception.</param>
    val throws<'T when 'T :> exn> : action:(unit->unit) -> unit

    val isGT : comparison:int -> actual:int -> unit
    val isGTE : comparison:int -> actual:int -> unit
    val isLT : comparison:int -> actual:int -> unit
    val isLTE : comparison:int -> actual:int -> unit
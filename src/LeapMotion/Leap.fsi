namespace Archient.Leap

/// F# Leap Motion controller library
module Leap =
    open System
    
    open Leap

    /// Contract for tracking Leap Motion Gestures
    type ILeapGestures =
        inherit IDisposable

        /// Gets an observable collection of gestures
        abstract member Gestures : IObservable<Gesture> with get
        
        /// Gets an observable collection of swipe gestures
        abstract member Swipes : IObservable<SwipeGesture> with get
        
        /// Gets an observable collection of circle gestures
        abstract member Circles : IObservable<CircleGesture> with get
        
        /// Gets an observable collection of key tap gestures
        abstract member KeyTaps : IObservable<KeyTapGesture> with get
        
        /// Gets an observable collection of screen tap gestures
        abstract member ScreenTaps : IObservable<ScreenTapGesture> with get

    /// <summary>Subscribes to an observable collection item</summary>
    /// <typeparam name="t">The type of observable to subscribe to</typeparam>
    /// <param name="subscription">The subscription function</param>
    /// <param name="observable">The observable being subscribed to</param>
    /// <returns>A disposable subscription reference</returns>
    val subscribe<'t> : subscription:('t->unit) -> observable:IObservable<'t> -> IDisposable
    
    /// <summary>Listen to Leap Motion gestures</summary>
    /// <returns>An instance of leap gestures</returns>
    val listen : unit -> ILeapGestures
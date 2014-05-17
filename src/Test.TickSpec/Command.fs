namespace Archient.Testing

open System.Xml

open Xunit.Sdk
open TickSpec

module Command =
    
    /// Creates the test command for the given scenario and method info
    let createTestCommand (scenario:Scenario) (info:IMethodInfo) =
        {
            new ITestCommand with 
                
                override this.Timeout = 0

                override this.ShouldCreateInstance = false 
                
                override this.Execute testClass = 
                    try 
                        scenario.Action.Invoke()
                        PassedResult(info, scenario.ToString()) :> MethodResult 
                    with ex -> 
                        FailedResult(info, ex, scenario.ToString()) :> MethodResult 
                
                override this.DisplayName = scenario.Name

                override this.ToStartXml () = 
                    let doc = XmlDocument() 
                    doc.LoadXml("<dummy/>")
                    let testNode = XmlUtility.AddElement(doc.ChildNodes.[0], "start") 
                    XmlUtility.AddAttribute(testNode, "name", scenario.Name) 
                    XmlUtility.AddAttribute(testNode, "type", info.MethodInfo.ReflectedType.FullName) 
                    XmlUtility.AddAttribute(testNode, "method", info.Name) 
                    testNode
        }
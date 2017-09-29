module DiTester

open Xunit
open SampleTestableClasses

let stubDoStuff = { new IDoStuff with 
                            member this.DoStuffForRealsies(a, b, y) = 
                                4
                        }

[<Fact>]
let ``Christopher wrote this like 5 minutes ago. Prepared, ain't he``() =
    let rd = new RequireDi(stubDoStuff)
    Assert.Equal(2, rd.MethodThatRequiresDi(5))
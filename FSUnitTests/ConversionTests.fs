module ConversionTests

open Xunit
open System
open SampleTestableClasses





[<Fact>]
let ``When value is passed to percentage it is multiplied by 100 and rounds to 8 decimal places``() = 
    let decimalValue = 1.0M
    Assert.Equal(ConversionExtensions.ToPercentageString(decimalValue), "100.00000000%")




[<Fact>]
let ``When null value is passed to percentage the default value is used``() = 
    let decimalValue = new Nullable<decimal>() 
    let defaultValue = new Nullable<decimal>(5.0M) 
    Assert.Equal(ConversionExtensions.ToPercentageString(decimalValue, defaultValue), "500.00000000%")
    
    


[<Fact>]
let ``When null value is passed twice to percentage the default string is used``() = 
    let decimalValue = new Nullable<decimal>() 
    let defaultValue = new Nullable<decimal>() 
    Assert.Equal(ConversionExtensions.ToPercentageString(decimalValue, defaultValue), "")



[<Fact>]
let ``The default string can be overridden``() = 
    let decimalValue = new Nullable<decimal>() 
    let defaultValue = new Nullable<decimal>() 
    Assert.Equal(ConversionExtensions.ToPercentageString(decimalValue, defaultValue, "Superhero"), "Superhero")




[<Fact>]
let ``This isn't particularly important, but is a wonderful example of mocking without a framework``() =
    let disposable = { new IDisposable with 
                            member this.Dispose() = printfn "I disposed!"
                        }
    disposable.Dispose()
    Assert.True(true)







[<Fact>]
let ``Parser removes the Timezone text, and parses the date exactly.``() = 
    let successfulParses = [("Tue Apr 21 15:35:12 EST 2015", new DateTime(2015, 4, 21, 15, 35, 12))
                            ("Tue Apr 21 15:35:12 EDT 2015", new DateTime(2015, 4, 21, 15, 35, 12))
                            ("Tue Apr 21 15:35:12 PST 2015", new DateTime(2015, 4, 21, 15, 35, 12))
                            ("Tue Apr 21 15:35:12 PDT 2015", new DateTime(2015, 4, 21, 15, 35, 12))]
    let testFunc (string, expectedDate) = 
        let a, b = DateTimeZoneAwareParser.TryParseOffset string
        Assert.True(a)
        Assert.Equal(b, expectedDate)
    List.iter testFunc successfulParses 
    
    








[<Fact>]
let ``Parser cannot understand a date time without a timezone``() = 
    let unsuccessfulParses = [("Tue Apr 21 15:35:12 2015")
                              ("Tue Apr 21 15:35:12 ED 2015")
                              ("Tue Apr 21 15:35:122015")]
    let testFunc str = 
        let a, _ = DateTimeZoneAwareParser.TryParseOffset str
        Assert.False(a)
    List.iter testFunc unsuccessfulParses 

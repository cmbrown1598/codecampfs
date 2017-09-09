module GuildedRoseTests

open Xunit
open SampleTestableClasses

[<Literal>]
let item1 = "item1"

type localItem = { Name : string; SellIn : int; Quality: int}

let convert (item : SampleTestableClasses.Item) = 
    { Name = item.Name; SellIn = item.SellIn; Quality = item.Quality }

let toItem li = 
    let item = new Item()
    item.Name <- li.Name
    item.SellIn <- li.SellIn
    item.Quality <- li.Quality;
    item

let sampleItem = { Name = item1; SellIn = 5; Quality = 5}

[<Fact>]
let `` Quality and sellin is decreased on normal items.``() =
    let items = toItem sampleItem
    let sut = new GuildedRose([|items|])
    sut.UpdateQuality()
    Assert.Equal({ sampleItem with SellIn = 4; Quality = 4}, convert (sut.Items.[0]))


[<Fact>]
let `` Once the sell by date has passed, Quality degrades twice as fast.``() =
    let items = toItem { Name = item1; SellIn=0; Quality = 5; }
    let sut = new GuildedRose([|items|])
    sut.UpdateQuality()
    Assert.Equal({ Name = item1; SellIn = -1; Quality = 3; }, convert (sut.Items.[0]))
    
[<Fact>]
let `` Quality doesn't go below Zero.``() =
    let items = toItem { Name = item1; SellIn=5; Quality = 0; }
    let sut = new GuildedRose([|items|])
    sut.UpdateQuality()
    Assert.Equal(0, sut.Items.[0].Quality)

[<Fact>]
let `` Backstage passes increase in quality ``() =
    let items = toItem { Name = Constants.BackstagePasses; SellIn=20; Quality = 5; }
    let sut = new GuildedRose([|items|])
    sut.UpdateQuality()
    Assert.Equal(6, sut.Items.[0].Quality)

[<Fact>]
let `` Backstage passes quality set to 0 after the concert``() =
    let items = toItem { Name = Constants.BackstagePasses; SellIn=0; Quality = 5; }    
    let sut = new GuildedRose([|items|])
    sut.UpdateQuality()
    Assert.Equal(0, sut.Items.[0].Quality)


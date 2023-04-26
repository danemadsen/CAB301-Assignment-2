using Xunit;
using System;

public class Test
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
    
    [Fact]
    public void CompareToTest()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Movie m2 = new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal(-1, m1.CompareTo(m2));
        Assert.NotEqual(0, m1.CompareTo(m2));
        Assert.NotEqual(1, m1.CompareTo(m2));

        Assert.Equal(1, m2.CompareTo(m1));
        Assert.NotEqual(0, m2.CompareTo(m1));
        Assert.NotEqual(-1, m2.CompareTo(m1));

        Assert.Equal(0, m1.CompareTo(m1));
        Assert.NotEqual(1, m1.CompareTo(m1));
        Assert.NotEqual(-1, m1.CompareTo(m1));
    }

    [Fact]
    public void ToStringTest()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal("{\"title\":\"A\",\"genre\":\"Action\",\"classification\":\"G\",\"duration\":1,\"availableCopies\":1}", m1.ToString());
    }

    [Fact]
    public void IsEmptyTest()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.IsEmpty());
    }

    [Fact]
    public void InsertTest()
    {
        MovieCollection mc = new MovieCollection();
        mc.Insert(new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1));
        Assert.False(mc.IsEmpty());
    }

    [Fact]
    public void DeleteTest()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        mc.Insert(m1);
        Assert.False(mc.IsEmpty());
        mc.Delete(m1);
        Assert.True(mc.IsEmpty());
    }

    [Fact]
    public void SearchTest()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Null(mc.Search(m1.Title));
        mc.Insert(m1);
        Assert.Equal(m1, mc.Search(m1.Title));
    }

    [Fact]
    public void NoDVDsTest()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Movie m2 = new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1);
        mc.Insert(m1);
        mc.Insert(m2);
        Assert.Equal(2, mc.NoDVDs());
    }

    [Fact]
    public void ToArrayTest()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        mc.Insert(m1);
        Assert.Equal(new Movie[] {m1}, mc.ToArray());
    }

    [Fact]
    public void ClearTest()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        mc.Insert(m1);
        Assert.False(mc.IsEmpty());
        mc.Clear();
        Assert.True(mc.IsEmpty());
    }
}
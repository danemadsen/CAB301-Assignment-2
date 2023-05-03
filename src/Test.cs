using Xunit;
using System;
using System.Diagnostics;
using System.Linq;

public class Test
{
    static void Main()
    {
        int[] testSizes = { 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };
        IMovieCollection[] collections = new IMovieCollection[testSizes.Length];
        double[] averagedExecutionTime = new double[testSizes.Length];
        int analysisRepetitions = 10;

        for (int i = 0; i < testSizes.Length; i++)
        {
            double[] executionTimes = new double[analysisRepetitions];
            (collections[i], _) = createFilledCollection(testSizes[i]);
            
            for (int j = 0; j < analysisRepetitions; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                collections[i].NoDVDs();
                stopwatch.Stop();
                executionTimes[j] = stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine($"MovieCollection{testSizes[i]}.NoDVDs() took {executionTimes[j]}ms");
            }
            averagedExecutionTime[i] = executionTimes.Average();
            Console.WriteLine($"MovieCollection{testSizes[i]}.NoDVDs() took {averagedExecutionTime[i]}ms on average");
        }
    }
    
    static (IMovieCollection, int) createFilledCollection(int amount)
    {
        IMovieCollection collection = new MovieCollection();
        Random random = new Random();
        int runningTotalDVDs = 0;

        for (int i = 0; i < amount; i++)
        {
            string hexString = i.ToString("X");
            int availableDVDs = random.Next(0, 1000000);
            int totalDVDs = random.Next(availableDVDs, 1000000);
            runningTotalDVDs += totalDVDs;
            collection.Insert(new Movie(hexString, MovieGenre.Action, MovieClassification.G, availableDVDs, totalDVDs));
        }
        return (collection, runningTotalDVDs);
    }

    [Fact]
    public void CompareToFunctionTest_MovieLeft()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Movie m2 = new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal(-1, m1.CompareTo(m2));
        Assert.NotEqual(0, m1.CompareTo(m2));
        Assert.NotEqual(1, m1.CompareTo(m2));
    }

    [Fact]
    public void CompareToFunctionTest_MovieEqual()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Movie m2 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal(0, m1.CompareTo(m2));
        Assert.NotEqual(1, m1.CompareTo(m2));
        Assert.NotEqual(-1, m1.CompareTo(m2));
    }

    [Fact]
    public void CompareToFunctionTest_MovieRight()
    {
        Movie m1 = new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1);
        Movie m2 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal(1, m1.CompareTo(m2));
        Assert.NotEqual(0, m1.CompareTo(m2));
        Assert.NotEqual(-1, m1.CompareTo(m2));
    }

    [Fact]
    public void ToStringBoundaryTest_Minimum()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal("{\"title\":\"A\",\"genre\":\"Action\",\"classification\":\"G\",\"duration\":1,\"availableCopies\":1,\"totalCopies\":1}", m1.ToString());
    }

    [Fact]
    public void ToStringBoundaryTest_Normal()
    {
        Movie m1 = new Movie("Sophies Choice", MovieGenre.Drama, MovieClassification.M, 151, 12137123);
        Assert.Equal("{\"title\":\"Sophies Choice\",\"genre\":\"Drama\",\"classification\":\"M\",\"duration\":151,\"availableCopies\":12137123,\"totalCopies\":12137123}", m1.ToString());
    }

    [Fact]
    public void ToStringBoundaryTest_Maximum()
    {
        Movie m1 = new Movie("ZZZZZZZZZ", MovieGenre.Western, MovieClassification.M15Plus, int.MaxValue, int.MaxValue);
        Assert.Equal("{\"title\":\"ZZZZZZZZZ\",\"genre\":\"Western\",\"classification\":\"M15Plus\",\"duration\":2147483647,\"availableCopies\":2147483647,\"totalCopies\":2147483647}", m1.ToString());
    }


    [Fact]
    public void IsEmptyTest_Empty()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.IsEmpty());
    }

    [Fact]
    public void IsEmptyTest_NotEmpty()
    {
        MovieCollection mc = new MovieCollection();
        mc.Insert(new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1));
        Assert.False(mc.IsEmpty());
    }

    [Fact]
    public void InsertBoundaryTest_Minimum()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.Insert(new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1)));
        Assert.False(mc.IsEmpty());
    }

    [Fact]
    public void InsertBoundaryTest_Normal()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.Insert(new Movie("Sophies Choice", MovieGenre.Drama, MovieClassification.M, 151, 12137123)));
        Assert.False(mc.IsEmpty());
    }

    [Fact]
    public void InsertBoundaryTest_Maximum()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.Insert(new Movie("ZZZZZZZZZ", MovieGenre.Western, MovieClassification.M15Plus, int.MaxValue, int.MaxValue)));
        Assert.False(mc.IsEmpty());
    }

    [Fact]
    public void InsertTest_Duplicate()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.True(mc.Insert(m1));
        Assert.False(mc.IsEmpty());
        Assert.False(mc.Insert(m1));
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
        int[] testSizes = { 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };
        int[] trueTotals = new int[testSizes.Length];
        IMovieCollection[] collections = new IMovieCollection[testSizes.Length];

        for (int i = 0; i < testSizes.Length; i++)
        {
            (collections[i], trueTotals[i]) = createFilledCollection(testSizes[i]);
            
            Assert.Equal(trueTotals[i], collections[i].NoDVDs());
        }
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
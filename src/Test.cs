using Xunit;
using System;
using System.Diagnostics;
using System.Linq;
using System.IO;

public class NoDVDsAnalysis
{
    static void Main()
    {
        int[] testSizes = new int[20];
        int size = 1;

        for (int i = 0; i < testSizes.Length; i++)
        {
            testSizes[i] = size;
            size *= 2;
        }

        IMovieCollection[] collections = new IMovieCollection[testSizes.Length];
        double[] averagedExecutionTime = new double[testSizes.Length];
        int analysisRepetitions = 10;

        // Create a StreamWriter to write the results to a CSV file
        using (StreamWriter writer = new StreamWriter("../analysis_results.csv"))
        {
            // Write the header row of the CSV file
            writer.WriteLine("MovieCollections,AveragedExecutionTime, AveragedTN");

            for (int i = 0; i < testSizes.Length; i++)
            {
                double[] executionTimes = new double[analysisRepetitions];
                collections[i] = createFilledCollection(testSizes[i]);

                for (int j = 0; j < analysisRepetitions; j++)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    collections[i].NoDVDs();
                    stopwatch.Stop();
                    executionTimes[j] = stopwatch.Elapsed.TotalMilliseconds;
                }
                averagedExecutionTime[i] = executionTimes.Average();
                Console.WriteLine($"MovieCollection{testSizes[i]}.NoDVDs() took {averagedExecutionTime[i].ToString("F4")}ms on average");

                // Write the results to the CSV file
                writer.WriteLine($"{testSizes[i]},{averagedExecutionTime[i].ToString("F4")}, {(averagedExecutionTime[i] / testSizes[i]).ToString("F8")}");
            }
        }
    }

    
    static IMovieCollection createFilledCollection(int amount)
    {
        IMovieCollection collection = new MovieCollection();
        for (int i = 0; i < amount; i++)
        {
            string hexString = i.ToString("X");
            collection.Insert(new Movie(hexString, MovieGenre.Action, MovieClassification.G, 1, 10));
        }
        return collection;
    }
}

public class MovieTests
{
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
}

public class MovieCollectionTests
{
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
    public void InsertTest_Normal()
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
    public void DeleteTest_Success()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.True(mc.Insert(m1));
        Assert.False(mc.IsEmpty());
        Assert.True(mc.Delete(m1));
        Assert.True(mc.IsEmpty());
    }

    [Fact]
    public void DeleteTest_EmptyCollectionFailure()
    {
        MovieCollection mc = new MovieCollection();
        Assert.False(mc.Delete(new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1)));
    }

    [Fact]
    public void DeleteTest_NonExistentMovieFailure()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.True(mc.Insert(m1));
        Assert.False(mc.IsEmpty());
        Assert.False(mc.Delete(new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1)));
    }

    [Fact]
    public void SearchTest_Exists()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.True(mc.Insert(m1));
        Assert.Equal(m1, mc.Search(m1.Title));
    }

    [Fact]
    public void SearchTest_DoesNotExist()
    {
        MovieCollection mc = new MovieCollection();
        Assert.Null(mc.Search("A"));
    }

    [Fact]
    public void NoDVDsBoundaryTest_Minimum()
    {
        MovieCollection mc = new MovieCollection();
        Assert.Equal(0, mc.NoDVDs());
    }

    [Fact]
    public void NoDVDsTest_Normal()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.Insert(new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1000)));
        Assert.True(mc.Insert(new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1000)));
        Assert.Equal(2000, mc.NoDVDs());
    }

    [Fact]
    public void NoDVDsBoundaryTest_Maximum()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.Insert(new Movie("A", MovieGenre.Action, MovieClassification.G, 1, (int.MaxValue - 1) / 2)));
        Assert.True(mc.Insert(new Movie("B", MovieGenre.Action, MovieClassification.G, 1, (int.MaxValue - 1) / 2)));
        Assert.Equal(int.MaxValue - 1, mc.NoDVDs()); // -1 because int.MaxValue is odd
    }

    [Fact]
    public void ToArrayTest_Empty()
    {
        MovieCollection mc = new MovieCollection();
        Assert.Equal(new Movie[] {}, mc.ToArray());
    }

    [Fact]
    public void ToArrayTest_Normal()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Movie m2 = new Movie("B", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.True(mc.Insert(m1));
        Assert.True(mc.Insert(m2));
        Assert.Equal(new Movie[] {m1, m2}, mc.ToArray());
    }

    [Fact]
    public void ClearTest_Empty()
    {
        MovieCollection mc = new MovieCollection();
        Assert.True(mc.IsEmpty());
        mc.Clear();
        Assert.True(mc.IsEmpty());
    }
    
    [Fact]
    public void ClearTest_Normal()
    {
        MovieCollection mc = new MovieCollection();
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.True(mc.Insert(m1));
        Assert.False(mc.IsEmpty());
        mc.Clear();
        Assert.True(mc.IsEmpty());
    }
}
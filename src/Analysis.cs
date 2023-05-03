using System;

public class Analysis
{
    static void Main()
    {
        int[] testSizes = { 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };
        int[] trueTotals = new int[testSizes.Length];
        IMovieCollection[] collections = new IMovieCollection[testSizes.Length];

        for (int i = 0; i < testSizes.Length; i++)
        {
            (collections[i], trueTotals[i]) = createFilledCollection(testSizes[i]);
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
}

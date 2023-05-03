//CAB301 assessment 2 - 2023
//An implementation of Movie ADT
using System;
using System.Collections.Generic;



public class Movie : IMovie
{
    private string title;  // the titleof this movie
    private MovieGenre genre;  // the genre of this movie
    private MovieClassification classification; // the classification of this movie
    private int duration; // the duration of this movie in minutes
    private int availablecopies; // the number of DVDs (copies) of this movie that are currently available in the library
    private int totalcopies; // the total number of copies of this movie
 
  

    // a constructor 
    public Movie(string t, MovieGenre g, MovieClassification c, int d, int n)
    {
        title = t;
        genre = g;
        classification = c;
        duration = d;
        availablecopies = n;
        totalcopies = n;
    }

    // another constructor
    public Movie(string t)
    {
        title = t;
    }

    // get and set the tile of this movie
    public string Title { get { return title; } set { title = value; } }

    //get and set the genre of this movie
    public MovieGenre Genre { get { return genre; } set { genre = value; } }

    //get and set the classification of this movie
    public MovieClassification Classification { get { return classification; } set { classification = value; } }

    //get and set the duration of this movie
    public int Duration { get { return duration; } set { duration = value;  } }

    //get and set the number of DVDs of this movie currently available in the library
    public int AvailableCopies { get { return availablecopies; } set { availablecopies = value;  } }

    //get and set the total number of DVDs of this movie in the library
    public int TotalCopies { get { return totalcopies; } set { totalcopies = value;  } }




    //This movie's title is compared to another movie's title 
    //Pre-condition: nil
    //Post-condition:  return -1, if this movie's title is less than another movie's title by dictionary order
    //                 return 0, if this movie's title equals to another movie's title by dictionary order
    //                 return +1, if this movie's title is greater than another movie's title by dictionary order
    public int CompareTo(IMovie another)
    {
        int min = Math.Min(title.Length, another.Title.Length);

        for (int i = 0; i < min; i++)
        {
            if (title[i] < another.Title[i]) return -1;
            else if (title[i] > another.Title[i]) return 1;
        }

        if (title.Length < another.Title.Length) return -1;
        else if (title.Length > another.Title.Length) return 1;
        else return 0;
    }

    //Return a string containing the title, genre, classification, duration, and the number of copies of this movie currently in the library 
    //Pre-condition: nil
    //Post-condition: A string containing the title, genre, classification, duration, and the number of available copies of this movie has been returned
    public override string ToString()
    {
        string result = "{\"title\":\"" + title + "\",";
        
        switch(genre)
        {
            case MovieGenre.Action: result += "\"genre\":\"Action\","; break;
            case MovieGenre.Comedy: result += "\"genre\":\"Comedy\","; break;
            case MovieGenre.History: result += "\"genre\":\"History\","; break;
            case MovieGenre.Drama: result += "\"genre\":\"Drama\","; break;
            case MovieGenre.Western: result += "\"genre\":\"Western\","; break;
        }

        switch (classification)
        {
            case MovieClassification.G: result += "\"classification\":\"G\","; break;
            case MovieClassification.PG: result += "\"classification\":\"PG\","; break;
            case MovieClassification.M: result += "\"classification\":\"M\","; break;
            case MovieClassification.M15Plus: result += "\"classification\":\"M15Plus\","; break;
        }
        
        result += "\"duration\":" + duration + ",";
        result += "\"availableCopies\":" + availablecopies + ",";
        result += "\"totalCopies\":" + totalcopies + "}";
        return result;
    }
}


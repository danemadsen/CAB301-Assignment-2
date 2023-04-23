//CAB301 assessment 2 - 2023
//The specification of Movie ADT
using System;

//Movie genre type
public enum MovieGenre
{
    Action = 1,
    Comedy = 2,
    History = 3,
    Drama = 4,
    Western = 5
}


//Movie classification type 
public enum MovieClassification
{
    G = 1,
    PG = 2,
    M = 3,
    M15Plus = 4
}

public interface IMovie
{
    // get and set the tile of this movie
    string Title 
    {
        get;
        set;
    }

    //get and set the genre of this movie
    MovieGenre Genre 
    {
        get;
        set;
    }

    //get and set the classification of this movie
    MovieClassification Classification 
    {
        get;
        set;
    }

    //get and set the duration of this movie
    int Duration 
    {
        get;
        set;
    }

    //get the number of DVDs of this movie currently available in the library
    int AvailableCopies 
    {
        get;
        set;
    }

    //get and set the total number of DVDs of this movie in the library
    public int TotalCopies 
    {
        get;
        set;
    }



    //This movie's title is compared to another movie's title 
    //Pre-condition: nil
    //Post-condition:  return -1, if this movie's title is less than another movie's title by dictionary order
    //                 return 0, if this movie's title equals to another movie's title by dictionary order
    //                 return +1, if this movie's title is greater than another movie's title by dictionary order
    public int CompareTo(IMovie another);



    //Return a string containing the title, genre, classification, duration, and the number of copies of this movie currently in the library 
    //Pre-condition: nil
    //Post-condition: A string containing the title, genre, classification, duration, and the number of available copies of this movie has been returned
    string ToString();

}


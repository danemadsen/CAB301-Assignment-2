//CAB301 assignment 2 - 2023 
//The specification of MovieCollection ADT


using System;

// invariant: no duplicate in this movie collection and Number >= 0
public interface IMovieCollection
{
	// get the number of movies in this movie collection
	int Number 
	{
		get;
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false. This movie collection remains unchanged and new Number = old Number
	bool IsEmpty();


	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: if the movie was not in this movie collection, the movie has been added into this movie collection, new Number = old Number + 1 and return true; otherwise, new Number = old Number and return false.
	bool Insert(IMovie movie);



	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: if the movie was in this movie collection, the movie has been removed out of this movie collection, new Number - old Number - 1 and return true; otherwise, return false and this movie collection remains unchanged and new Number = old Number.
	bool Delete(IMovie movie);



	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null. New Number = old Number.
	public IMovie? Search(string title);


    // Calculate the totall number of DVDs in this movie collection 
    // Pre-condition: nil
    // Post-condition: return the total number of DVDs in this movie collection. this moive collection remains unchanged, and new Number = old Number.
    public int NoDVDs();


    // Return an array that contains all the movies in this movie collection and the movies in the array are sorted in the dictionary order by movie title
    // Pre-condition: nil
    // Post-condition: return an array of movies that are stored in dictionary order by their titles, this movie collection remains unchanged and new Number = old Number.
    IMovie[] ToArray();

	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies in this movie collection have been removed from this movie collection and new Number = 0. 
	void Clear();

}


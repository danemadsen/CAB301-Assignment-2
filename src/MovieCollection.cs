// CAB301 - assignment 2
// An implementation of MovieCollection ADT
// 2023


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in the binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode? lchild; // reference to its left child 
	private BTreeNode? rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode? LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode? RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicate movie in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode? root; // the root of the binary search tree in which movies are stored 
	private int count; // the number of movies currently stored in this movie collection 
	public int Number { get { return count; } }

	// constructor - create an empty movie collection
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	public bool IsEmpty()
	{
		return root == null && count == 0;
	}

	public bool Insert(IMovie movie){
	    if (this.IsEmpty()) // if the tree is empty
	    {
	        root = new BTreeNode(movie);
	        count++;
	        return true;
	    }

	    BTreeNode? current = root;
	    while (true)
	    {
	        switch(movie?.CompareTo(current.Movie))
	        {
	            case -1: // movie should be inserted to the left of current node
	                if (current.LChild == null)
	                {
	                    current.LChild = new BTreeNode(movie);
	                    count++;
	                    return true;
	                }
	                else current = current.LChild;
	                break;
				case 0: // movie already exists in the collection
	                return false;
	            case 1: // movie should be inserted to the right of current node
	                if (current.RChild == null)
	                {
	                    current.RChild = new BTreeNode(movie);
	                    count++;
	                    return true;
	                }
	                else current = current.RChild;
	                break;
				default:
					throw new Exception("Invalid comparison");
	        }
	    }
	}

	// Needs Rewrite
	public bool Delete(IMovie movie) {
	    if (this.IsEmpty()) return false;
	
	    // search for the node containing the movie to delete
	    BTreeNode? current = root;
	    BTreeNode? parent = null;
	    while (current != null && movie.CompareTo(current.Movie) != 0) 
	    {
	        parent = current;
			switch(movie.CompareTo(current.Movie))
			{
				case -1:
					current = current.LChild;
					break;
				case 0:
					break;
				case 1:
					current = current.RChild;
					break;
				default:
					throw new Exception("Invalid comparison");
			}
	    }

	    if (current == null) return false; // movie to delete not found

	    // if current node has no child or one child, update parent link
	    if (current.LChild == null || current.RChild == null) 
	    {
	        BTreeNode? child = current.LChild ?? current.RChild;

	        if (parent == null) root = child;
	        else if (parent.LChild == current) parent.LChild = child;
	        else parent.RChild = child;

	        count--;
	        return true;
	    }

	    // if current node has two children, find the inorder successor
	    BTreeNode? successor = current.RChild;
	    parent = current;
	    while (successor.LChild != null) 
	    {
	        parent = successor;
	        successor = successor.LChild;
	    }

	    // replace the movie in the current node with that in the successor node
	    current.Movie = successor.Movie;

	    // delete the successor node (which has no left child)
	    if (parent.LChild == successor) parent.LChild = successor.RChild;
	    else parent.RChild = successor.RChild;

	    count--;
	    return true;
	}

	public IMovie? Search(string movietitle)
	{
	    BTreeNode current = root;

	    while (current != null && movietitle.CompareTo(current.Movie.Title) != 0)
	    {
	        switch(movietitle.CompareTo(current.Movie.Title))
			{
				case -1:
					current = current.LChild;
					break;
				case 0:
					break;
				case 1:
					current = current.RChild;
					break;
				default:
					throw new Exception("Invalid comparison");
			}
	    }

	    return current?.Movie;
	}


	// Calculate the totall number of DVDs in this movie collection 
    // Pre-condition: nil
    // Post-condition: return the total number of DVDs in this movie collection. this moive collection remains unchanged, and new Number = old Number.
    public int NoDVDs()
	{
	    int totalDVDs = 0;
		Stack<BTreeNode> stack = new Stack<BTreeNode>();

		BTreeNode? curr = root;
	    while (curr != null || stack.Count > 0)
	    {
	        while (curr != null)
	        {
	            stack.Push(curr);
	            curr = curr.LChild;
	        }

	        curr = stack.Pop();
	        totalDVDs += curr.Movie.TotalCopies;
	        curr = curr.RChild;
	    }

	    return totalDVDs;
	}

	// Needs Rewrite
    public IMovie[] ToArray()
	{
		IMovie[] movies = new IMovie[count];
	    int i = 0;
	    Stack<BTreeNode> stack = new Stack<BTreeNode>();
	    BTreeNode? curr = root;

	    // Traverse the tree in inorder and add each movie to the array
	    while (curr != null || stack.Count > 0)
	    {
	        while (curr != null)
	        {
	            stack.Push(curr);
	            curr = curr.LChild;
	        }

	        curr = stack.Pop();
	        movies[i++] = curr.Movie;
	        curr = curr.RChild;
	    }

	    return movies;
	}


	public void Clear()
	{
		root = null;
		count = 0;
    }
}
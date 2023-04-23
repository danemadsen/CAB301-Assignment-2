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
		return root == null;
	}

	// Needs Rewrite
	public bool Insert(IMovie movie){
	    if (root == null) // if the tree is empty
	    {
	        root = new BTreeNode(movie);
	        count++;
	        return true;
	    }

	    BTreeNode current = root;
	    while (true)
	    {
	        int cmp = String.Compare(movie.Title, current.Movie.Title);

	        if (cmp == 0) // movie already exists in the collection
	            return false;
	        else if (cmp < 0) // movie should be inserted to the left of current node
	        {
	            if (current.LChild == null)
	            {
	                current.LChild = new BTreeNode(movie);
	                count++;
	                return true;
	            }
	            else
	                current = current.LChild;
	        }
	        else // movie should be inserted to the right of current node
	        {
	            if (current.RChild == null)
	            {
	                current.RChild = new BTreeNode(movie);
	                count++;
	                return true;
	            }
	            else
	                current = current.RChild;
	        }
	    }
	}

	// Needs Rewrite
	public bool Delete(IMovie movie) {
	    // base case: empty tree
	    if (root == null) 
	    {
	        return false;
	    }
	
	    // search for the node containing the movie to delete
	    BTreeNode? current = root;
	    BTreeNode? parent = null;
	    while (current != null && !current.Movie.Equals(movie)) 
	    {
	        parent = current;
	        if (String.Compare(movie.Title, current.Movie.Title) < 0)
	        {
	            current = current.LChild;
	        } 
	        else 
	        {
	            current = current.RChild;
	        }
	    }

	    if (current == null) 
	    {
	        return false;
	    }

	    // if current node has no child or one child, update parent link
	    if (current.LChild == null || current.RChild == null) 
	    {
	        BTreeNode? child = current.LChild ?? current.RChild;
	        if (parent == null) 
	        {
	            root = child;
	        } 
	        else if (parent.LChild == current) 
	        {
	            parent.LChild = child;
	        } 
	        else 
	        {
	            parent.RChild = child;
	        }
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
	    if (parent.LChild == successor) 
	    {
	        parent.LChild = successor.RChild;
	    } 
	    else 
	    {
	        parent.RChild = successor.RChild;
	    }
	    count--;
	    return true;
	}

	// Needs Rewrite
	public IMovie? Search(string movietitle)
	{
	    BTreeNode current = root;

	    while (current != null)
	    {
	        int comparison = movietitle.CompareTo(current.Movie.Title);
	        if (comparison == 0)
	        {
	            return current.Movie;
	        }
	        else if (comparison < 0)
	        {
	            current = current.LChild;
	        }
	        else
	        {
	            current = current.RChild;
	        }
	    }

	    return null;
	}



    public int NoDVDs()
	{

    }

   
    public IMovie[] ToArray()
	{

    }


	public void Clear()
	{

    }
}






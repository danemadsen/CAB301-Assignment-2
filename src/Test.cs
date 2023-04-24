using Xunit;

public class Test
{
    [Fact]
    public void CompareToTest()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        for(int i = 0; i < 5; i++)
        {
            Movie m2 = new Movie(('A' + i).ToString(), i % 5 + 1, i % 4 + 1, i, i);
            Assert.Equal(-1, m1.CompareTo(m2));
            Assert.Equal(1, m2.CompareTo(m1));
            Assert.Equal(0, m1.CompareTo(m1));
            m1 = m2;
        }
    }

    [Fact]
    public void ToStringTest()
    {
        Movie m1 = new Movie("A", MovieGenre.Action, MovieClassification.G, 1, 1);
        Assert.Equal("{\"title\":\"A\",\"genre\":\"Action\",\"classification\":\"G\",\"duration\":1,\"availableCopies\":1}", m1.ToString());
    }
}
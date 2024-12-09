namespace List.Tests;
using Lists;

public class UniqueListTests
{
    [Test]
    public void TestElementAlreadyExistsException()
    {
        var uniqueList = new UniqueList();
        uniqueList.Add(1);
        Assert.Throws<ElementExistsException>(() => uniqueList.Add(1));
    }
}

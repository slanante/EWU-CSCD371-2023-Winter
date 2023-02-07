namespace GenericsHomework;
public class Node<T>
{
    public T Content { get; set; }
    private Node NextNode { get; set; }
    private Node? PrevNode { get; set; }

    public Node(T content)
    {
        Content = content;
    }

    public override string ToString()
    {
        return Content.ToString();
    }

    public void Append(T content)
    {
        NextNode = new Node<T>(content);
    }

    public void Clear()
    {
        NextNode = null;
        PrevNode = null;
    }
    public bool Exists(T value)
    {
        Node currNode = this;
        while (currNode != null)
        {
            if (currNode.Content.Equals(value))
            {
                return true;
            }
            currNode = currNode.NextNode;
        }
        return false;
    }
    

}
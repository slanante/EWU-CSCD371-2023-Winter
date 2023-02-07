namespace GenericsHomework;
public class Node<T>
{
    public T Content { get; set; }
    public Node NextNode { get; set; }
    public Node? PrevNode { get; set; }

    public Node(T content)
    {
        Content = content;
    }

    public override string ToString()
    {
        return Content.ToString();
    }

}
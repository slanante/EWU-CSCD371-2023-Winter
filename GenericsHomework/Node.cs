namespace GenericsHomework;
public class Node<T>
{
    public T Content { get; set; }
    public Node<T> NextNode { get; set; }
    public Node<T>? PrevNode { get; set; }

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
        if (Exists(content))
        {
            throw new Exception("The value within Content has already been appended previously.");
        }

        Node<T> newNode = new Node<T>(content);
        NextNode = newNode;
        newNode.PrevNode = this;
    }

    public void Clear()
    {
        NextNode = null;
        PrevNode = null;
    }
    public bool Exists(T content)
    {
        Node<T>currNode = this;
        while (currNode != null)
        {
            if (currNode.Content.Equals(content))
            {
                return true;
            }
            currNode = currNode.NextNode;
        }
        return false;
    }


}
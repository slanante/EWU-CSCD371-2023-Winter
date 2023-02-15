namespace GenericsHomework;
public class Node<T> where T : notnull
{
    public T Content { get; set; }
    public Node<T> NextNode { get; private set; }
    public Node<T>? PrevNode { get; set; }

    public Node(T content)
    {
        Content = content;
        NextNode = this;
    }

    public override string? ToString()
    {
        if (Content != null)
        {
            return Content.ToString()!;
        }
        else
        {
            return null;
        }
        
    }

    public void Append(T content)
    {
        if (Exists(content))
        {
            throw new Exception("The value within Content has already been appended previously.");
        }

        Node<T> newNode = new Node<T>(content);
        Node<T> current = this;

        if (NextNode.Equals(this))
        {
            NextNode = newNode;
            newNode.PrevNode = this;
            return;
        } else {
            while (current.NextNode != current)
            {
                current = current.NextNode;
            }
            current.NextNode = newNode;
            newNode.PrevNode = current;
            return;
        }


    }

    public void Clear()
    {
        NextNode = this;
        PrevNode = null;
    }
    public bool Exists(T content)
    {
        Node<T> currNode = this;
        while (currNode != null)
        {
            if (currNode.Content.Equals(content))
            {
                return true;
            }
            if (currNode.NextNode.Equals(currNode))
            {
                return false;
            }
            currNode = currNode.NextNode;
        }
        return false;
    }


}
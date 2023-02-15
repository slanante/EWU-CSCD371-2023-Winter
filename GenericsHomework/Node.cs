using System;
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
        }
        else
        {
            while (current.NextNode != current)
            {
                current = current.NextNode;
            }
            current.NextNode = newNode;
            newNode.PrevNode = current;
            this.PrevNode = newNode;
            return;
        }


    }

    /*
    To answer the questions on the homework guide
    - Whether it is sufficient to only set Next to itself?

    No, I think simply setting Next to itself is not sufficient.
    We would still have instances that refer to other instances.
    We should ensure garbage collection is handled because
    we like to make good code :)

    Or at least try to...

    */
    public void Clear()
    {
        Node<T> currNode = this;
        List<Node<T>> nodes = new List<Node<T>>();

        while (currNode.NextNode != currNode)
        {
            currNode = currNode.NextNode;
            nodes.Add(currNode);
        }
        foreach (Node<T> node in nodes)
        {
            Dispose(node);
        }
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

    public void Dispose(Node<T> node)
    {
        node.Content = default!; // Wanted to get rid of warnings, we are purposefully setting this to null
        node.PrevNode = null;
        node.NextNode = null!; // Wanted to get rid of warnings, we are purposefully setting this to null
    }



}
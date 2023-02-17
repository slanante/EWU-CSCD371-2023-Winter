using System;
namespace GenericsHomework
{
	public class Circle<T> where T : class
    {
        public T[] Elems { get; set; }
        public string Name { get; set; }
        public Circle(string name = null!, T[] elems = null!)
        {
            Name = name ?? "Venn Diagram";
            Elems = elems ?? default!;
        }

        public override string ToString()
        {
            string result = "";
            foreach (T element in Elems)
            {
                result += element.ToString() + " ";
            }
            return result;
        }

        public T[] Intersect(Circle<T> circle)
        {
            List<T> results = new List<T>();
            foreach (var elem in circle.Elems)
            {
                if (this.Elems.Contains(elem))
                {
                    results.Add(elem);
                }
            }
            return results.ToArray();
        }
    }
}


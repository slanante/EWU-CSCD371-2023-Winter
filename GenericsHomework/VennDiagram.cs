using System;
namespace GenericsHomework
{
	public class VennDiagram<T>
    {
        public List<T> Circles { get; set; } = new();
        public string Name { get; set; }

        public VennDiagram(string name = null!)
        {
            Name = name ?? "Venn Diagram";
        }

        public override string ToString()
        {
            string result = "";
            foreach (T element in Circles)
            {
                result += element!.ToString() + " ";
            }
            return result;
        }
    }
}


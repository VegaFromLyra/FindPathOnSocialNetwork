using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Given a graph that represents a social network, design an algorithm that can show the path/connection between 2 people that are not connected 

// The graph in the main method looks like so

/*                        1
                        /  \
                       2     3
                       \   / | \
                        4    5   6
                        \
                         7
*/

namespace FindPathOnSocialNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Node socialNetwork = new Node(1);
            socialNetwork.Children.Add(new Node(2));
            socialNetwork.Children.Add(new Node(3));

            Node fourNode = new Node(4);
            socialNetwork.Children[0].Children.Add(fourNode);

            socialNetwork.Children[1].Children.Add(fourNode);
            socialNetwork.Children[1].Children.Add(new Node(5));
            socialNetwork.Children[1].Children.Add(new Node(6));

            socialNetwork.Children[0].Children[0].Children.Add(new Node(7));

            Node origin = socialNetwork;
            Node destination = socialNetwork.Children[0].Children[0].Children[0];

            if (FindPath(origin, destination))
            {
                foreach (Node n in destination.Ancestors)
                {
                    Console.Write(n.Data + " ");
                }
                Console.Write(destination.Data);
            }
        }

        static bool FindPath(Node origin, Node destination)
        {
            Queue<Node> queue = new Queue<Node>();

            origin.IsVisited = true;
            queue.Enqueue(origin);

            while (queue.Count > 0)
            {
                Node n = queue.Dequeue();

                if (n == destination)
                {
                    return true;
                }

                foreach (Node child in n.Children)
                {
                    if (!child.IsVisited)
                    {
                        if (n.Ancestors.Count > 0)
                        {
                            child.Ancestors.AddRange(n.Ancestors);
                        }

                        child.Ancestors.Add(n);

                        queue.Enqueue(child);
                        child.IsVisited = true;
                    }
                }
            }

            return false;
        }
    }

    class Node
    {

        public Node(int data)
        {
            Data = data;
            Children = new List<Node>();
            Ancestors = new List<Node>();
        }

        public int Data { private set; get; }

        public bool IsVisited { get; set; }

        public List<Node> Children { get; set; }

        public List<Node> Ancestors { get; set; }
    }
}

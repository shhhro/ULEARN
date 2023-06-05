using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public class TreeNode
        {
            public string Name;
            public Dictionary<string, TreeNode> Nodes;

            public TreeNode(string name)
            { 
                Name = name;
                Nodes = new Dictionary<string, TreeNode>();
            }

            public List<string> GetConclusion(int i, List<string> list)
            {
                if (i >= 0) list.Add(new string(' ', i) + Name);
                i++;
                foreach (var subNode in Nodes.Values.OrderBy(root => root.Name, StringComparer.Ordinal))
                    list = subNode.GetConclusion(i, list);
                return list;
            } 

            public TreeNode GetDirection(string subPath) =>
                Nodes.TryGetValue(subPath, out TreeNode node)
                    ? node : Nodes[subPath] = new TreeNode(subPath);
        }

        public static List<string> Solve(List<string> inputList)
        {
            var treeNode = new TreeNode("");
            foreach (var path in inputList)
            {
                path.Split('\\')
                    .Aggregate(treeNode, (current, subPath) => current.GetDirection(subPath));
            }
            return treeNode.GetConclusion(-1, new List<string>());
        }
    }
}
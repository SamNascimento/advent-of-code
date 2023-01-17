Console.WriteLine("========= Exercício 7 - Desafio 2 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var rootNode = new Node("/", true, null);

Read(rootNode, input);
WriteTree(rootNode);

var nodeList = new List<Node>();
nodeList     = ListNodeSize(rootNode, nodeList);

var avaliableSize  = 70000000L;
var necessarySize  = 30000000L;
var totalSizeOfDir = nodeList.Max(n => n.dirSize);

var goalSize = Math.Abs(avaliableSize - totalSizeOfDir - necessarySize);

var listNodeAboveGoal = nodeList.Where(n => n.dirSize >= goalSize && n.isDir).OrderBy(n => n.dirSize);
var mostAssertiveNode = listNodeAboveGoal.First();

Console.WriteLine("The total size of Dir is: " + totalSizeOfDir);
Console.WriteLine("The goal size of is: " + goalSize);

if (mostAssertiveNode != null)
    Console.WriteLine($"The most assertive Dir for cleaning system is {mostAssertiveNode.dirName} with size {mostAssertiveNode.dirSize}");


#region Functions

void Read(Node node, IEnumerable<string> lines) 
{
    if (!lines.Any()) 
        return;

    var line = lines.First();

    if (line.Substring(0, 1) == "$")
    {
        if (line.Contains("cd"))
        {
            var dirName = line.Replace("$ cd ", "");

            if (dirName == "/")
            {
                Read(rootNode, lines.Skip(1));
            }
            else if (line.Contains(".."))
            {
                Read(node.parent!, lines.Skip(1));
            }
            else
            {
                var searchNode = BuscarNode(node, dirName);

                if (searchNode == null)
                {
                    var newNode = new Node(dirName, true, node);
                    node.child.Add(newNode);
                    Read(newNode, lines.Skip(1));
                }
                else
                {
                    Read(searchNode, lines.Skip(1));
                }
            }
        }
        else if (line.Contains("ls"))
            Read(node, lines.Skip(1));
    }
    else if (line.StartsWith("dir"))
    {
        var dirName = line.Replace("dir ", "");

        var newNode = new Node(dirName, true, node);
        node.child.Add(newNode);
        Read(node, lines.Skip(1));
    }
    else if (char.IsNumber(line[0]))
    {
        var fileLine = line.Split(' ');
        var fileSize = int.Parse(fileLine[0]);
        var fileName = fileLine[1];

        var newNode = new Node(fileName, false, node, fileSize);
        node.child.Add(newNode);
        Read(node, lines.Skip(1));
    }
}

Node? BuscarNode(Node root, string dirName)
{
    var searchNode = root
        .child
        .Where(n => n.dirName == dirName)
        .FirstOrDefault();

    return searchNode;
}

void WriteTree(Node root, int spc = 0)
{
    var spaces = new String(' ', spc);

    if(root.isDir)
        Console.WriteLine($"{spaces}- {root.dirName} (dir)");
    else
        Console.WriteLine($"{spaces}- {root.dirName} (file, size={root.dirSize})");

    foreach (var child in root.child)
        WriteTree(child, spc+2);
}

long Size(Node root)
{
    var nodeSize = 0L;

    nodeSize += root.child.Select(n => n.dirSize).Sum();

    foreach (var child in root.child)
        nodeSize += Size(child);

    return nodeSize;
}

List<Node> ListNodeSize(Node root, List<Node> nodeList)
{
    if (root.isDir)
        root.dirSize = Size(root);

    if (root.dirSize > 0 && root.isDir)
        nodeList.Add(root);

    foreach (var child in root.child)
        ListNodeSize(child, nodeList);

    return nodeList;
}

#endregion

#region Class
class Node
{ 
    public string dirName;
    public long dirSize;
    public bool isDir;
    public List<Node> child = new List<Node>();
    public Node? parent;

    public Node(string name, bool isDirectory, Node? nodeParent, long size = 0)
    {
        dirName = name;
        dirSize = isDirectory ? child.Select(n => n.dirSize).Sum() : size;
        isDir   = isDirectory;
        parent  = nodeParent;
    }

    public void addDirNode(string name, Node? parent)
    {
        this.child.Add(new Node(name, true, parent));
    }

    public void addFileNode(string name, long size, Node? parent)
    {
        this.child.Add(new Node(name, false, parent, size));
    }
}
#endregion
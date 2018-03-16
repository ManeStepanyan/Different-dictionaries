using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// implementation of Dictionary by red-black tree
/// </summary>

namespace RedBlackTree
{
    internal enum COLOR { Red, Black };
    public class RedBlackTreeDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {   // Key's list for collection
        private List<TKey> Keys_List = new List<TKey>();
        // Value's list for collection
        private List<TValue> Key_Values = new List<TValue>();
        // Root of the tree, which is initialized by NIL
        private Node Root = NIL.Instance();
        // List of nodes(for enumerator)
        private List<KeyValuePair<TKey, TValue>> listForNodes = new List<KeyValuePair<TKey, TValue>>();
        public RedBlackTreeDictionary()
        { }
        /// <summary>
        /// Find a value of the given key
        /// </summary>
        /// <param name="key"> key to look for</param>
        /// <returns> value corresponding to given key </returns>
        public TValue Retrieve(TKey key)
        {
            if (ContainsKey(key))
                return GetTheNodeOfKey(key).Value;
            else throw new Exception("No such a key");

        }
        /// <summary>
        /// Helper to get the node of the given key
        /// </summary>
        /// <param name="key"> key whose corresonding node should be returned </param>
        /// <returns> node whose key is equal to given key </returns>
        private Node GetTheNodeOfKey(TKey key)
        {
            if (Keys_List.Contains(key))
            {
                bool IsFound = false;
                Node temp = Root;
                while (!IsFound)
                {
                    if (key.CompareTo(temp.Key) < 0)
                        temp = temp.Left;
                    if (key.CompareTo(temp.Key) > 0)
                        temp = temp.Right;
                    if (key.CompareTo(temp.Key) == 0)
                        IsFound = true;
                }
                return temp;
            }
            else throw new Exception("Key doesn't exist");

        }
        private void LeftRotate(Node nodeToRotate)
        { // setting node which will come in place of nodeToRotate
            Node nodeToReplace = nodeToRotate.Right;
            //turn nodeToReplace's left subtree into nodeToRotate's right subtree
            nodeToRotate.Right = nodeToReplace.Left;
            if (nodeToReplace.Left != NIL.Instance())
            { // if left subtree exists make it's parent nodeToRotate
                nodeToReplace.Left.Parent = nodeToRotate;
            }
            if (nodeToReplace != NIL.Instance())
            { // link nodeToRotate parent to nodeToReplace
                nodeToReplace.Parent = nodeToRotate.Parent;
            }
            if (nodeToRotate.Parent == NIL.Instance())
            { // if nodeToRotate is the root, make nodeToReplace as new root 
                Root = nodeToReplace;
            }
            if (nodeToRotate == nodeToRotate.Parent.Left)
            { // if nodeToRotate is a left child, nodeToReplace has to become left child of nodeToRotate's parent
                nodeToRotate.Parent.Left = nodeToReplace;
            }
            else
            { // if nodeToRotate is a right child, nodeToReplace has to become right child of nodeToRotate's parent
                nodeToRotate.Parent.Right = nodeToReplace;
            }
            // put nodeToRotate on nodeToReplace's left
            nodeToReplace.Left = nodeToRotate;
            // if nodeToRotate is not nil, make it's parent nodeToReplace
            if (nodeToRotate != NIL.Instance())
            {
                nodeToRotate.Parent = nodeToReplace;
            }

        }
        private void RightRotate(Node nodeToRotate)
        { // setting node which will come in place of nodeToRotate
            Node nodeToReplace = nodeToRotate.Left;
            //turn nodeToReplace's right subtree into nodeToRotate's left subtree
            nodeToRotate.Left = nodeToReplace.Right;
            if (nodeToReplace.Right != NIL.Instance())
            { // if right subtree exists make it's parent nodeToRotate
                nodeToReplace.Right.Parent = nodeToRotate;
            }
            if (nodeToReplace != NIL.Instance())
            { // link nodeToRotate parent to nodeToReplace
                nodeToReplace.Parent = nodeToRotate.Parent;
            }
            if (nodeToRotate.Parent == NIL.Instance())
            { // if nodeToRotate is the root, make nodeToReplace as new root 
                Root = nodeToReplace;
            }
            if (nodeToRotate == nodeToRotate.Parent.Right)
            { // if nodeToRotate is a right child, nodeToReplace has to become right child of nodeToRotate's parent
                nodeToRotate.Parent.Right = nodeToReplace;
            }
            else
            { // if nodeToRotate is a left child, nodeToReplace has to become left child of nodeToRotate's parent
                nodeToRotate.Parent.Left = nodeToReplace;
            }
            // put nodeToRotate on nodeToReplace's right
            nodeToReplace.Right = nodeToRotate;
            // if nodeToRotate is not nil, make it's parent nodeToReplace
            if (nodeToRotate != NIL.Instance())
            {
                nodeToRotate.Parent = nodeToReplace;
            }

        }
        /// <summary>
        /// Insert a given node
        /// </summary>
        /// <param name="newNode"> node to be inserted into the tree </param>
        private void Insert(Node newNode)
        { // if tree is empty, make NewNode as the root
            if (Root == NIL.Instance())
            {
                Root = newNode;
                Root.color = COLOR.Black;
                return;
            }
            Node parentOfNewNode = NIL.Instance();
            Node a = Root;
            while (a != NIL.Instance())
            { // finding the right subtree to insert the newNode
                parentOfNewNode = a;
                if (newNode.Key.CompareTo(a.Key) < 0)
                {
                    a = a.Left;
                }
                else a = a.Right;
            }
            newNode.Parent = parentOfNewNode;
            if (parentOfNewNode == NIL.Instance())
                Root = newNode;
            // finding in which subtree of b newNode have to be inserted
            else if (newNode.Key.CompareTo(parentOfNewNode.Key) >= 0)
                parentOfNewNode.Right = newNode;
            else parentOfNewNode.Left = newNode;
            // making inserted node's left and right children NIL and it's color RED
            newNode.Left = NIL.Instance();
            newNode.Right = NIL.Instance();
            newNode.color = COLOR.Red;
            // because coloring red may cause a violation of one of the red-black properties we must guarantee that the red-black properties are preserved
            RBT_InsertFixUp(newNode);

        }
        /// <summary>
        /// to restore the red-black properties
        /// </summary>
        /// <param name="NewAddedNode"> node which was inserted </param>
        private void RBT_InsertFixUp(Node NewAddedNode)
        {
            while (NewAddedNode != Root && NewAddedNode.Parent.color == COLOR.Red)
            { // if parent of new added node is a left child
                if (NewAddedNode.Parent == NewAddedNode.Parent.Parent.Left)
                { // uncle of newAddedNode
                    Node uncle = NewAddedNode.Parent.Parent.Right;
                    if (uncle != NIL.Instance() && uncle.color == COLOR.Red)
                    {
                        NewAddedNode.Parent.color = COLOR.Black;
                        uncle.color = COLOR.Black;
                        NewAddedNode.Parent.Parent.color = COLOR.Red;
                        // make newNode's grandparent as the new newNode and repeat the steps
                        NewAddedNode = NewAddedNode.Parent.Parent;

                    } // if uncle is black and parent of newNode is not black there are 2 cases
                    // 1 case (left right case)
                    else if (NewAddedNode == NewAddedNode.Parent.Right)
                    {
                        NewAddedNode = NewAddedNode.Parent;
                        LeftRotate(NewAddedNode);
                    }

                    else
                    {// case 2 (left left case)
                        NewAddedNode.Parent.color = COLOR.Black;
                        NewAddedNode.Parent.Parent.color = COLOR.Red;
                        RightRotate(NewAddedNode.Parent.Parent);
                    }
                }

                else
                { // if parent of a new added node is right child
                    Node y = NewAddedNode.Parent.Parent.Left; // uncle of newNode
                    if (y != NIL.Instance() && y.color == COLOR.Red)
                    {
                        NewAddedNode.Parent.color = COLOR.Black;
                        y.color = COLOR.Black;
                        NewAddedNode.Parent.Parent.color = COLOR.Red;
                        NewAddedNode = NewAddedNode.Parent.Parent;
                    } // case 1 (right left)
                    else if (NewAddedNode == NewAddedNode.Parent.Left)
                    {
                        NewAddedNode = NewAddedNode.Parent;
                        RightRotate(NewAddedNode);
                    }

                    else
                    { //case 2(right right)
                        NewAddedNode.Parent.color = COLOR.Black;
                        NewAddedNode.Parent.Parent.color = COLOR.Red;
                        LeftRotate(NewAddedNode.Parent.Parent);
                    }

                }
                Root.color = COLOR.Black;
            }

        }
        /// <summary>
        /// passing through the tree by InOrderTraversal
        /// </summary>
        /// <param name="root"> root of a subtree to be passed </param>


        private void InOrderDisplay(Node root)
        {
            if (root != NIL.Instance())
            {
                InOrderDisplay(root.Left);
                Console.Write("Key is {0} and Value is {1} ", root.Key, root.Value);
                InOrderDisplay(root.Right);
            }
        }
        /// <summary>
        /// displaying tree
        /// </summary>
        public void Display()
        {
            if (Root != NIL.Instance()) InOrderDisplay(Root);
            else throw new Exception("No tree");
        }
        /// <summary>
        ///  replaces the subtree rooted at node u with the subtree rooted at v
        /// </summary>
        /// <param name="u"> node to be replaced </param>
        /// <param name="v"> node which subtree will come into the place of u node's subtree </param>

        private void RBTransplant(Node x, Node y)
        { // if u is a root make v as a root
            if (x.Parent == NIL.Instance())
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            // even if u.Parent is NIL we can assign it
            y.Parent = x.Parent;
        }
        /// <summary>
        /// to delete node from the tree
        /// </summary>
        /// <param name="nodeToDelete"> node to be deleted </param>
        private void RBT_Delete(Node nodeToDelete)
        {
            Node node = nodeToDelete;
            /*
            We have to store nodeToDelete's original color, because when we move a node
            into it's postion and the color of nodeToDelete is black we must call DeleteFixup
            */
            COLOR originalColor = node.color;
            Node x;
            // when nodeToDelete has one child we just need to replace nodeToDelete with the child

            if (nodeToDelete.Left == NIL.Instance())
            {
                x = nodeToDelete.Right;
                RBTransplant(nodeToDelete, nodeToDelete.Right);
            }
            else if (nodeToDelete.Right == NIL.Instance())
            {
                x = nodeToDelete.Left;
                RBTransplant(nodeToDelete, nodeToDelete.Left);
            }
            else
            {
                /*  if nodeToDelete has two children we have to find the successor (the smallest key
                 that is larger than nodeToDelete) and move it into nodeToDelete's position */
                node = RBTreeMinimum(nodeToDelete.Right);
                originalColor = node.color;
                x = node.Right;
                //  if successor is right child
                if (node.Parent == nodeToDelete)
                {
                    x.Parent = node;
                }
                else
                { // succesor will move into nodeToDelete's position
                    RBTransplant(node, node.Right);
                    node.Right = nodeToDelete.Right;
                    node.Right.Parent = node;
                }
                RBTransplant(nodeToDelete, node);
                node.Left = nodeToDelete.Left;
                node.Left.Parent = node;
                node.color = nodeToDelete.color;
            }
            // if node which was deleted is black, we restore red-black tree properties
            if (originalColor == COLOR.Black)
            {
                RBT_DeleteFixup(x);
            }
        }
        /// <summary>
        /// Fix up properties of red-black tree which might be violeted
        /// </summary>
        /// <param name="node">node in which subtree violation might occur </param>
        private void RBT_DeleteFixup(Node node)
        {
            Node sibling;
            // move up the tree until we reach a red node or the root
            while (node != Root && node.color == COLOR.Black)
            {
                // if node is left child
                if (node == node.Parent.Left)
                {
                    sibling = node.Parent.Right;
                    if (sibling.color == COLOR.Red)
                    {
                        sibling.color = COLOR.Black;
                        node.Parent.color = COLOR.Red;
                        LeftRotate(node.Parent);
                        sibling = node.Parent.Right;
                    } // if sibling's color is black and both of it's children have black color
                    else if (sibling.Left.color == COLOR.Black && sibling.Right.color == COLOR.Black)
                    {
                        sibling.color = COLOR.Red;
                        node = node.Parent;
                    }
                    else
                    { // if sibling's color is black and right child of sibling has black color
                        if (sibling.Right.color == COLOR.Black)
                        {
                            sibling.Left.color = COLOR.Black;
                            sibling.color = COLOR.Red;
                            RightRotate(sibling);
                            sibling = node.Parent.Right;
                        } // if sibling's color is black and right child of sibling has red color
                        sibling.color = node.Parent.color;
                        node.Parent.color = COLOR.Black;
                        sibling.Right.color = COLOR.Black;
                        LeftRotate(node.Parent);
                        node = Root;
                    }
                }
                else
                { // if node is left child
                    sibling = node.Parent.Left;
                    if (sibling.color == COLOR.Red)
                    {
                        sibling.color = COLOR.Black;
                        node.Parent.color = COLOR.Red;
                        LeftRotate(node.Parent);
                        sibling = node.Parent.Left;
                    }
                    // If sibling's color is black and both of it's children have black color
                    else if (sibling.Right.color == COLOR.Black && sibling.Left.color == COLOR.Black)
                    {
                        sibling.color = COLOR.Red;
                        node = node.Parent;
                    }
                    else
                    { // if sibling's color is black and left child of sibling has black color
                        if (sibling.Left.color == COLOR.Black)
                        {
                            sibling.Right.color = COLOR.Black;
                            sibling.color = COLOR.Red;
                            LeftRotate(sibling);
                            sibling = node.Parent.Left;
                        }
                        // If sibling's color is black and left child of sibling has red color
                        sibling.color = node.Parent.color;
                        node.Parent.color = COLOR.Black;
                        sibling.Left.color = COLOR.Black;
                        RightRotate(node.Parent);
                        node = Root;
                    }
                }
            }
            node.color = COLOR.Black;
        }
        /// <summary>
        /// successor of node x
        /// </summary>
        /// <param name="x"> node which successor has to be found(left subtree) </param>
        /// <returns></returns>
        private Node RBTreeMinimum(Node x)
        {
            while (x.Left != NIL.Instance())
            {
                x = x.Left;
            }
            return x;
        }
        // Gets or sets the element with the specified key
        public TValue this[TKey key] { get { return (ContainsKey(key)) ? Retrieve(key) : throw new Exception("Doesn't Contain Key"); } set { this.Add(key, value); } }
        // Gets an ICollection<TKey> containing the keys of the IDictionary<TKey, TValue>
        public ICollection<TKey> Keys { get { return this.Keys_List; } }
        // Gets an ICollection<TValue> containing the values of the IDictionary<TKey, TValue>
        public ICollection<TValue> Values { get { return this.Key_Values; } }
        //Gets the number of elements contained in the Collection
        public int Count { get { return this.Keys_List.Count; } }
        // whether collection is read-only
        public bool IsReadOnly { get { return false; } }
        // Adds an element with given key and value
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key) == false)
            {
                Node NewNode = new Node
                {
                    Key = key,
                    Value = value,
                    Left = NIL.Instance(),
                    Right = NIL.Instance(),
                    Parent = NIL.Instance()
                };
                Keys_List.Add(key);
                Key_Values.Add(value);
                Insert(NewNode);
                listForNodes.Add(new KeyValuePair<TKey, TValue>(key, value));
            }
            else throw new Exception("Key is already inserted");
        }
        // Adds an item to ICollection
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            if (!ContainsKey(item.Key))
            {
                this.Add(item.Key, item.Value);
            }
            else throw new Exception("Key is already inserted");


        }
        /// <summary>
        /// Removes all items from the tree
        /// </summary>
        public void Clear()
        {
            Root = NIL.Instance();
        }
        /// <summary>
        /// Checking whether the given item is contained in Collection 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            bool res = false;
            if (ContainsKey(item.Key))
            {
                if (Retrieve(item.Key).Equals(item.Value)) res = true;
            }
            return res;
        }

        /// <summary>
        /// Checking whether tree contains the given key
        /// </summary>
        /// <param name="key"> key to look for </param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return Keys_List.Contains(key);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns> enumerator </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Get_List().GetEnumerator();

        }
        private List<KeyValuePair<TKey, TValue>> Get_List()
        {
            return listForNodes;
        }
        /// <summary>
        /// Copies the elements of the ICollection<TKey,TValue> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array"> particular array </param>
        /// <param name="arrayIndex"> start index for array </param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            array = new KeyValuePair<TKey, TValue>[Keys_List.Count + arrayIndex];
            for (int i = arrayIndex; i < array.Length; i++)
            {
                KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(Keys_List[i - arrayIndex], Key_Values[i - arrayIndex]);
                array[i] = item;
            }
        }
        /// <summary>
        /// Remove an item with specified key from the tree
        /// </summary>
        /// <param name="key">key of element which has to be deleted </param>
        /// <returns></returns>

        public bool Remove(TKey key)
        {
            if (Keys_List.Contains(key))
            {
                Key_Values.Remove(GetTheNodeOfKey(key).Value);
                listForNodes.Remove(new KeyValuePair<TKey, TValue>(key, GetTheNodeOfKey(key).Value)); ///esim??
                RBT_Delete(GetTheNodeOfKey(key));
                Keys_List.Remove(key);


                return true;
            }
            return false;
        }/// <summary>
         /// removes item from collection
         /// </summary>
         /// <param name="item"> item to be removed </param>
         /// <returns></returns>

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((Contains(item) && Remove(item.Key)));

        }
        /// <summary>
        /// Gets the value associated with the given key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (ContainsKey(key))
            {
                value = Retrieve(key);
                return true;
            }
            value = default(TValue);
            return false;
        }
        /// <summary>
        /// return an enumerator to iterate through collection
        /// </summary>
        /// <returns> enumerator </returns>

        IEnumerator IEnumerable.GetEnumerator()
        {

            return (IEnumerator)GetEnumerator();
        }

        /// <summary>
        /// Nodes to make a tree
        /// </summary>
        internal class Node
        {  // Key value
            internal TKey Key;
            // Value
            internal TValue Value;
            // Left child
            internal Node Left { get; set; }
            // right child;
            internal Node Right { get; set; }
            // parent
            internal Node Parent { get; set; }
            // color of node
            internal COLOR color;
            /*   public Node(TKey Key, TValue Value)
               {
                   this.Key = Key;
                   this.Value = Value;
                   this.Left = NIL.Instance();
                   this.Right = NIL.Instance();
                   this.Parent = NIL.Instance();
               } */
            public Node() { }


        }
        // NIL leaf 
        internal class NIL
        {
            internal static Node NILinstance;
            internal static Node Instance()
            {
                if (NILinstance == null)
                {
                    NILinstance = new Node
                    { //leaf's color is always black
                        color = COLOR.Black
                    };
                }

                return NILinstance;
            }
        }
    }
}



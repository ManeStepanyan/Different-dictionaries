using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{ /// <summary>
  /// Implementing dictionary with AVL tree
  /// </summary>
  /// <typeparam name="TKey"> Type of key </typeparam>
  /// <typeparam name="TValue">Type of value </typeparam>
    public class AVLDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {
        internal class Node
        {   // Node's Key
            internal TKey Key { get; set; }
            // Node's Value
            internal TValue Value { get; set; }
            // height of tree starting from node
            internal int Height { get; set; }
            // left child
            internal Node left;
            // right child
            internal Node right;
            internal Node(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
            }

        }
        // Key's list for collection
        private List<TKey> Keys_List = new List<TKey>();
        // Value's list for Collection
        private List<TValue> Keys_Values = new List<TValue>();
        // list for nodes
        private List<KeyValuePair<TKey, TValue>> listForNodes = new List<KeyValuePair<TKey, TValue>>();
        // root of the tree
        private Node Root;
        // Gets an ICollection<TKey> containing the keys of the IDictionary<TKey, TValue>
        public ICollection<TKey> Keys
        {
            get
            {
                return Keys_List;
            }
        }
        // Gets an ICollection<TValue> containing the values of the IDictionary<TKey, TValue>
        public ICollection<TValue> Values { get { return Keys_Values; } }
        // Gets the number of elements contained in the Collection
        public int Count { get { return Keys_List.Count; } }
        // whether collection is read-only
        public bool IsReadOnly { get { return false; } }
        // Gets or sets the element with the specified key
        public TValue this[TKey key] { get { return (ContainsKey(key)) ? Retrieve(key) : throw new Exception("no such a key"); } set { this.Add(key, value); } }
        /// <summary>
        /// left rotation of the node
        /// </summary>
        /// <param name="a"> node to be rotated left </param>
        /// <returns>new node after rotation </returns>
        private Node LeftRotate(Node a)
        {
            Node y = a.right;
            Node x = y.left;
            y.left = a;
            a.right = x;
            a.Height = Math.Max(GetHeight(a.left), GetHeight(a.right)) + 1;
            y.Height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;
            return y;
        }
        /// <summary>
        /// get the balance of node
        /// </summary>
        /// <param name="node"> node </param>
        /// <returns> integer which indicates difference between left and right subtree's height of given node </returns>
        int GetBalance(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.left) - GetHeight(node.right);
        }
        /// <summary>
        /// right rotation
        /// </summary>
        /// <param name="a"> node to be rotated </param>
        /// <returns> new noded after rotation</returns>
        private Node RightRotate(Node a)
        {
            Node y = a.left;
            Node x = y.right;
            y.right = a;
            a.left = x;
            a.Height = Math.Max(GetHeight(a.left), GetHeight(a.right)) + 1;
            y.Height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;
            return y;
        }
        /// <summary>
        /// getting the height
        /// </summary>
        /// <param name="node"> node which's height has to be calculated </param>
        /// <returns> height </returns>
        private int GetHeight(Node node)
        {
            int height = 0;
            if (node != null)
            {
                int l = GetHeight(node.left);
                int r = GetHeight(node.right);
                height = Math.Max(l, r) + 1;
            }
            return height;
        }
        /// <summary>
        /// adding a node to the tree, starting from indicated node
        /// </summary>
        /// <param name="node"> root of the subtree </param>
        /// <param name="nodeToInsert"> node to insert </param>
        /// <returns> new node after insertion </returns>
        private Node Insert(Node node, Node nodeToInsert)
        { // if root is null make nodeToInsert as new root
            if (node == null)
            {
                node = nodeToInsert;
                return node;
            }
            // if nodeToInsert's key is smaller than the key of node, we shall search place in left subtree
            if (nodeToInsert.Key.CompareTo(node.Key) < 0)
            {
                node.left = Insert(node.left, nodeToInsert);

            }
            else // otherwise in right subtree
            { node.right = Insert(node.right, nodeToInsert); }
            // updating heights
            node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

            if (GetBalance(node) > 1 && nodeToInsert.Key.CompareTo(node.left.Key) < 0)
                return RightRotate(node);

            // Right Right Case
            if (GetBalance(node) < -1 && nodeToInsert.Key.CompareTo(node.right.Key) > 0)
                return LeftRotate(node);

            // Left Right Case
            if (GetBalance(node) > 1 && nodeToInsert.Key.CompareTo(node.left.Key) > 0)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }

            // Right Left Case
            if (GetBalance(node) < -1 && nodeToInsert.Key.CompareTo(node.right.Key) < 0)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
            }

            // return the node pointer
            return node;

        }
        /// <summary>
        /// displaying tree via InOrder traversal
        /// </summary>
        public void Display()
        {
            if (Root == null) throw new Exception("no tree");
            InOrder(Root);
        }
        /// <summary>
        /// Inorder traversal
        /// </summary>
        /// <param name="node"> root </param>
        private void InOrder(Node node)
        {
            if (node != null)
            {

                InOrder(node.left);
                Console.WriteLine("Key is {0} and value is {1}", node.Key, node.Value);
                InOrder(node.right);
            }

        }
        /// <summary>
        /// find the value of given key
        /// </summary>
        /// <param name="key"> key</param>
        /// <returns> value corresponding to key </returns>
        public TValue Retrieve(TKey key)
        {
            if (ContainsKey(key)) { return GetTheNodeOfKey(key).Value; }

            else throw new Exception("no such a key");

        }
        /// <summary>
        /// deletion of node
        /// </summary>
        /// <param name="node"> root of the subtree </param>
        /// <param name="nodeToDelete">node to be deleted </param>
        /// <returns> root after deletion </returns>
        private Node Delete(Node node, Node nodeToDelete)
        { // nothing to delete
            if (node == null)
                return node;
            // if nodeToDelete's key is smaller than the key of node, call Delete for left subtree
            if (nodeToDelete.Key.CompareTo(node.Key) < 0)
                node.left = Delete(node.left, nodeToDelete);
            // otherwise call Delete for right subtree
            else if ((nodeToDelete.Key.CompareTo(node.Key) > 0))
                node.right = Delete(node.right, nodeToDelete);
            // in case of key is equal to root's key, root has to be deleted   
            else
            { // node that has one child or no child
                if ((node.left == null) || (node.right == null))
                {
                    Node temp = null;
                    if (temp == node.left)
                        temp = node.right;
                    else
                        temp = node.left;

                    // no child
                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else   // one child
                        node = temp;
                }
                else
                {

                    // node has two children
                    // get the successor
                    Node temp = Minimum(node.right);

                    // copying keys
                    node.Key = temp.Key;

                    // deleting the successor
                    node.right = Delete(node.right, temp);
                }
                // if tree has only one element 
                if (node == null)
                    return node;

                // updating height after deletion
                node.Height = Math.Max(GetHeight(node.left), GetHeight(node.right)) + 1;

                // check if node became unbalanced, we have 4 cases:               
                // Left Left Case
                int balance = GetBalance(node);
                if (balance > 1 && GetBalance(node.left) >= 0)
                    return RightRotate(node);

                // Left Right Case
                if (balance > 1 && GetBalance(node.left) < 0)
                {
                    node.left = LeftRotate(node.left);
                    return RightRotate(node);
                }

                // Right Right Case
                if (balance < -1 && GetBalance(node.right) <= 0)
                    return LeftRotate(node);

                // Right Left Case
                if (balance < -1 && GetBalance(node.right) > 0)
                {
                    node.right = RightRotate(node.right);
                    return LeftRotate(node);
                }

            }
            return node;
        }
        /// <summary>
        /// finding the minimum element in leftSubtree (assuming that it exists)
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private Node Minimum(Node root)
        {
            Node current = root;
            while (current.left != null)
                current = current.left;

            return current;
        }
        /// <summary>
        /// getting the node corresponding to the given key
        /// </summary>
        /// <param name="key"> key </param>
        /// <returns> node </returns>
        private Node GetTheNodeOfKey(TKey key)
        {
            if (Keys_List.Contains(key))
            {
                bool IsFound = false;
                Node temp = Root;
                if (Root == null) throw new Exception("No tree");

                while (!IsFound)
                {
                    if (key.CompareTo(temp.Key) < 0)
                        temp = temp.left;
                    else if (key.CompareTo(temp.Key) > 0)
                        temp = temp.right;
                    else IsFound = true;

                }

                return temp;
            }
            else throw new Exception("Key doesn't exist");

        }
        /// <summary>
        /// Checking whether tree contains the given key
        /// </summary>
        /// <param name="key"> key to look for </param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return (Keys_List.Contains(key));
        }
        /// <summary>
        /// Adds an element with given key and value
        /// </summary>
        /// <param name="key"> key to be added </param>
        /// <param name="value"> value to be added </param>

        public void Add(TKey key, TValue value)
        {
            if (!ContainsKey(key))
            {
                Node newNode = new Node(key, value);
                Keys_List.Add(key);
                Keys_Values.Add(value);
                listForNodes.Add(new KeyValuePair<TKey, TValue>(key, value));
                if (Root == null)
                {
                    Root = newNode;
                }
                else
                {
                    Root = Insert(Root, newNode);
                }
            }
            else throw new Exception("Key is already inserted");
        }
        /// <summary>
        /// Remove an item with specified key from the tree
        /// </summary>
        /// <param name="key"> key to be deleted </param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (ContainsKey(key))
            {
                Node a = GetTheNodeOfKey(key);
                Delete(Root, a);
                Keys_List.Remove(key);
                Keys_Values.Remove(a.Value);
                listForNodes.Remove(new KeyValuePair<TKey, TValue>(key, a.Value));
                return true;
            }
            else throw new Exception("no such a key");
        }
        /// <summary>
        ///  Gets the value associated with the given key
        /// </summary>
        /// <param name="key"> key </param>
        /// <param name="value"> value </param>
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
        /// Adds an item to ICollection
        /// </summary>
        /// <param name="item"> item to be added </param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (!ContainsKey(item.Key))
            {
                this.Add(item.Key, item.Value);
            }
            else throw new Exception("Key is already inserted");
        }
        /// <summary>
        /// clearing the tree
        /// </summary>
        public void Clear()
        {
            Root = null;
        }
        /// <summary>
        /// check whether the given item is contained in Collection 
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
        /// Copies the elements of the ICollection<TKey,TValue> to an Array, starting at a particular Array index.
        /// </summary>
        /// </summary>
        /// <param name="array"> particular array </param>
        /// <param name="arrayIndex"> start index for array </param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            array = new KeyValuePair<TKey, TValue>[Keys_List.Count + arrayIndex];
            for (int i = arrayIndex; i < array.Length; i++)
            {
                KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(Keys_List[i - arrayIndex], Keys_Values[i - arrayIndex]);
                array[i] = item;
            }
        }
        /// <summary>
        ///  Removes item from collcection
        /// </summary>
        /// <param name="item"> item to be removed </param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((Contains(item) && Remove(item.Key)));
        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns> enumerator </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Get_List().GetEnumerator();
        }
        /// <summary>
        /// Getting the list of nodes
        /// </summary>
        /// <returns></returns>
        private List<KeyValuePair<TKey, TValue>> Get_List()
        {
            return listForNodes;
        }
        /// <summary>
        /// Return an enumerator to iterate through collection
        /// </summary>
        /// <returns >enumerator </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {

            return (IEnumerator)GetEnumerator();
        }
    }
}

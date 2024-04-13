namespace TreeStructure
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			int[] ints = { 1, 2,2,2, 3, 4, 5, 8, 9 };
			List<int> ints1 = new List<int>(ints);
			Console.WriteLine(ints1.IndexOf(7)); // -1
            Console.WriteLine(ints1.IndexOf(2));
        }
	}
	internal class Node<T>
	{
		public Node<T>? Smaller;
		public Node<T>? Bigger;
		public T Value;
	}
	internal class Tree<R>
	{
		Node<R>? root;
		int rootIndex = 0;
		public int Count { get; private set; } = 0;
		IComparer<R> comparer;
		public Tree(IComparer<R> comparer)
		{
			this.comparer = comparer;
		}
		public void Clear()
		{
			root = null;
		}
		public bool Contains(R value)
		{
			return IndexOf(value) != -1;
		}
		public void Insert(int index, R value)
		{
			throw new Exception("Tree will be unbalanced");
		}
		public void Add(R value)
		{
			Count++;
			if (root == null)
			{
				root = new Node<R>() { Value = value };
				return;
			}
			Node<R> helper = root;
			int compared;
			while (true)
			{
				compared = comparer.Compare(helper.Value, value);
				if (compared >= 0)
				{
					if (helper.Bigger == null)
					{
						helper.Bigger = new Node<R>() { Value = value };
						return;
					}
					helper = helper.Bigger;
					continue;
				}
				if (compared < 0)
				{
					rootIndex++;
					if (helper.Smaller == null)
					{
						helper.Smaller = new Node<R>() { Value = value };
						return;
					}
					helper = helper.Smaller;
					continue;
				}
			}
		}
		public int IndexOf(R value)
		{
			Node<R>? helper = root;
			int index = rootIndex;
			int compared;
			while (true)
			{
				if (helper == null) return -1;
				compared = comparer.Compare(helper.Value, value);
				if (compared > 0)
				{
					helper = helper.Smaller;
					index--;
					continue;
				}
				if (compared < 0)
				{
					helper = helper.Bigger;
					index++;
					continue;
				}
				return index;
			}
		}
	}
}

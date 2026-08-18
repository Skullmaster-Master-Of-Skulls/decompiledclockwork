using System;
using System.Collections.Generic;
using System.IO;

namespace ClockWorkAPI.Collection
{
	// Token: 0x0200003F RID: 63
	public interface ITree<T> : IEnumerableCollectionPair<T>, IDisposable
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600032D RID: 813
		Type DataType { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600032E RID: 814
		// (set) Token: 0x0600032F RID: 815
		IEqualityComparer<T> DataComparer { get; set; }

		// Token: 0x06000330 RID: 816
		void XmlSerialize(Stream stream);

		// Token: 0x06000331 RID: 817
		void Clear();

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000332 RID: 818
		int Count { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000333 RID: 819
		int DirectChildCount { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000334 RID: 820
		INode<T> Root { get; }

		// Token: 0x17000156 RID: 342
		INode<T> this[T o]
		{
			get;
		}

		// Token: 0x06000336 RID: 822
		string ToStringRecursive();

		// Token: 0x06000337 RID: 823
		bool Contains(T item);

		// Token: 0x06000338 RID: 824
		bool Contains(INode<T> item);

		// Token: 0x06000339 RID: 825
		INode<T> InsertChild(T o);

		// Token: 0x0600033A RID: 826
		INode<T> AddChild(T o);

		// Token: 0x0600033B RID: 827
		void InsertChild(ITree<T> tree);

		// Token: 0x0600033C RID: 828
		void AddChild(ITree<T> tree);

		// Token: 0x0600033D RID: 829
		ITree<T> Cut(T o);

		// Token: 0x0600033E RID: 830
		ITree<T> Copy(T o);

		// Token: 0x0600033F RID: 831
		ITree<T> DeepCopy(T o);

		// Token: 0x06000340 RID: 832
		bool Remove(T o);

		// Token: 0x06000341 RID: 833
		ITree<T> Copy();

		// Token: 0x06000342 RID: 834
		ITree<T> DeepCopy();

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000343 RID: 835
		IEnumerableCollectionPair<T> All { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000344 RID: 836
		IEnumerableCollectionPair<T> AllChildren { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000345 RID: 837
		IEnumerableCollectionPair<T> DirectChildren { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000346 RID: 838
		IEnumerableCollectionPair<T> DirectChildrenInReverse { get; }

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000347 RID: 839
		// (remove) Token: 0x06000348 RID: 840
		event EventHandler<NodeTreeDataEventArgs<T>> Validate;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000349 RID: 841
		// (remove) Token: 0x0600034A RID: 842
		event EventHandler Clearing;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600034B RID: 843
		// (remove) Token: 0x0600034C RID: 844
		event EventHandler Cleared;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600034D RID: 845
		// (remove) Token: 0x0600034E RID: 846
		event EventHandler<NodeTreeDataEventArgs<T>> Setting;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600034F RID: 847
		// (remove) Token: 0x06000350 RID: 848
		event EventHandler<NodeTreeDataEventArgs<T>> SetDone;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000351 RID: 849
		// (remove) Token: 0x06000352 RID: 850
		event EventHandler<NodeTreeInsertEventArgs<T>> Inserting;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000353 RID: 851
		// (remove) Token: 0x06000354 RID: 852
		event EventHandler<NodeTreeInsertEventArgs<T>> Inserted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000355 RID: 853
		// (remove) Token: 0x06000356 RID: 854
		event EventHandler Cutting;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000357 RID: 855
		// (remove) Token: 0x06000358 RID: 856
		event EventHandler CutDone;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000359 RID: 857
		// (remove) Token: 0x0600035A RID: 858
		event EventHandler<NodeTreeNodeEventArgs<T>> Copying;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600035B RID: 859
		// (remove) Token: 0x0600035C RID: 860
		event EventHandler<NodeTreeNodeEventArgs<T>> Copied;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600035D RID: 861
		// (remove) Token: 0x0600035E RID: 862
		event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600035F RID: 863
		// (remove) Token: 0x06000360 RID: 864
		event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied;
	}
}

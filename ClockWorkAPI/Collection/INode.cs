using System;

namespace ClockWorkAPI.Collection
{
	// Token: 0x0200003E RID: 62
	public interface INode<T> : IEnumerableCollectionPair<T>, IDisposable
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D8 RID: 728
		T Data { get; set; }

		// Token: 0x060002D9 RID: 729
		string ToStringRecursive();

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002DA RID: 730
		int Depth { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002DB RID: 731
		int BranchIndex { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002DC RID: 732
		int BranchCount { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002DD RID: 733
		int Count { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002DE RID: 734
		int DirectChildCount { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002DF RID: 735
		INode<T> Parent { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002E0 RID: 736
		INode<T> Previous { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002E1 RID: 737
		INode<T> Next { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002E2 RID: 738
		INode<T> Child { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002E3 RID: 739
		ITree<T> Tree { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002E4 RID: 740
		INode<T> Root { get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002E5 RID: 741
		INode<T> Top { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002E6 RID: 742
		INode<T> First { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002E7 RID: 743
		INode<T> Last { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002E8 RID: 744
		INode<T> LastChild { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002E9 RID: 745
		bool IsTree { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002EA RID: 746
		bool IsRoot { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002EB RID: 747
		bool IsTop { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002EC RID: 748
		bool HasParent { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002ED RID: 749
		bool HasPrevious { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002EE RID: 750
		bool HasNext { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002EF RID: 751
		bool HasChild { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002F0 RID: 752
		bool IsFirst { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002F1 RID: 753
		bool IsLast { get; }

		// Token: 0x17000146 RID: 326
		INode<T> this[T item]
		{
			get;
		}

		// Token: 0x060002F3 RID: 755
		bool Contains(INode<T> item);

		// Token: 0x060002F4 RID: 756
		bool Contains(T item);

		// Token: 0x060002F5 RID: 757
		INode<T> InsertPrevious(T o);

		// Token: 0x060002F6 RID: 758
		INode<T> InsertNext(T o);

		// Token: 0x060002F7 RID: 759
		INode<T> InsertChild(T o);

		// Token: 0x060002F8 RID: 760
		INode<T> Add(T o);

		// Token: 0x060002F9 RID: 761
		INode<T> AddChild(T o);

		// Token: 0x060002FA RID: 762
		void InsertPrevious(ITree<T> tree);

		// Token: 0x060002FB RID: 763
		void InsertNext(ITree<T> tree);

		// Token: 0x060002FC RID: 764
		void InsertChild(ITree<T> tree);

		// Token: 0x060002FD RID: 765
		void Add(ITree<T> tree);

		// Token: 0x060002FE RID: 766
		void AddChild(ITree<T> tree);

		// Token: 0x060002FF RID: 767
		ITree<T> Cut(T o);

		// Token: 0x06000300 RID: 768
		ITree<T> Copy(T o);

		// Token: 0x06000301 RID: 769
		ITree<T> DeepCopy(T o);

		// Token: 0x06000302 RID: 770
		bool Remove(T o);

		// Token: 0x06000303 RID: 771
		ITree<T> Cut();

		// Token: 0x06000304 RID: 772
		ITree<T> Copy();

		// Token: 0x06000305 RID: 773
		ITree<T> DeepCopy();

		// Token: 0x06000306 RID: 774
		void Remove();

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000307 RID: 775
		bool CanMoveToParent { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000308 RID: 776
		bool CanMoveToPrevious { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000309 RID: 777
		bool CanMoveToNext { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600030A RID: 778
		bool CanMoveToChild { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600030B RID: 779
		bool CanMoveToFirst { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600030C RID: 780
		bool CanMoveToLast { get; }

		// Token: 0x0600030D RID: 781
		void MoveToParent();

		// Token: 0x0600030E RID: 782
		void MoveToPrevious();

		// Token: 0x0600030F RID: 783
		void MoveToNext();

		// Token: 0x06000310 RID: 784
		void MoveToChild();

		// Token: 0x06000311 RID: 785
		void MoveToFirst();

		// Token: 0x06000312 RID: 786
		void MoveToLast();

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000313 RID: 787
		IEnumerableCollectionPair<T> All { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000314 RID: 788
		IEnumerableCollectionPair<T> AllChildren { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000315 RID: 789
		IEnumerableCollectionPair<T> DirectChildren { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000316 RID: 790
		IEnumerableCollectionPair<T> DirectChildrenInReverse { get; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000317 RID: 791
		// (remove) Token: 0x06000318 RID: 792
		event EventHandler<NodeTreeDataEventArgs<T>> Validate;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000319 RID: 793
		// (remove) Token: 0x0600031A RID: 794
		event EventHandler<NodeTreeDataEventArgs<T>> Setting;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600031B RID: 795
		// (remove) Token: 0x0600031C RID: 796
		event EventHandler<NodeTreeDataEventArgs<T>> SetDone;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600031D RID: 797
		// (remove) Token: 0x0600031E RID: 798
		event EventHandler<NodeTreeInsertEventArgs<T>> Inserting;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600031F RID: 799
		// (remove) Token: 0x06000320 RID: 800
		event EventHandler<NodeTreeInsertEventArgs<T>> Inserted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000321 RID: 801
		// (remove) Token: 0x06000322 RID: 802
		event EventHandler Cutting;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000323 RID: 803
		// (remove) Token: 0x06000324 RID: 804
		event EventHandler CutDone;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000325 RID: 805
		// (remove) Token: 0x06000326 RID: 806
		event EventHandler<NodeTreeNodeEventArgs<T>> Copying;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000327 RID: 807
		// (remove) Token: 0x06000328 RID: 808
		event EventHandler<NodeTreeNodeEventArgs<T>> Copied;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000329 RID: 809
		// (remove) Token: 0x0600032A RID: 810
		event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600032B RID: 811
		// (remove) Token: 0x0600032C RID: 812
		event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied;
	}
}

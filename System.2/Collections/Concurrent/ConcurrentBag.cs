using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020003D2 RID: 978
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SystemThreadingCollection_IProducerConsumerCollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentBag<T> : IProducerConsumerCollection<T>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06002596 RID: 9622 RVA: 0x000AEC0A File Offset: 0x000ACE0A
		[__DynamicallyInvokable]
		public ConcurrentBag()
		{
			this.Initialize(null);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000AEC19 File Offset: 0x000ACE19
		[__DynamicallyInvokable]
		public ConcurrentBag(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection", SR.GetString("ConcurrentBag_Ctor_ArgumentNullException"));
			}
			this.Initialize(collection);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000AEC40 File Offset: 0x000ACE40
		private void Initialize(IEnumerable<T> collection)
		{
			this.m_locals = new ThreadLocal<ConcurrentBag<T>.ThreadLocalList>();
			if (collection != null)
			{
				ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(true);
				foreach (T item in collection)
				{
					threadList.Add(item, false);
				}
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000AECA0 File Offset: 0x000ACEA0
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(true);
			this.AddInternal(threadList, item);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000AECC0 File Offset: 0x000ACEC0
		private void AddInternal(ConcurrentBag<T>.ThreadLocalList list, T item)
		{
			bool flag = false;
			try
			{
				Interlocked.Exchange(ref list.m_currentOp, 1);
				if (list.Count < 2 || this.m_needSync)
				{
					list.m_currentOp = 0;
					Monitor.Enter(list, ref flag);
				}
				list.Add(item, flag);
			}
			finally
			{
				list.m_currentOp = 0;
				if (flag)
				{
					Monitor.Exit(list);
				}
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000AED2C File Offset: 0x000ACF2C
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<!0>.TryAdd(T item)
		{
			this.Add(item);
			return true;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000AED36 File Offset: 0x000ACF36
		[__DynamicallyInvokable]
		public bool TryTake(out T result)
		{
			return this.TryTakeOrPeek(out result, true);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000AED40 File Offset: 0x000ACF40
		[__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			return this.TryTakeOrPeek(out result, false);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000AED4C File Offset: 0x000ACF4C
		private bool TryTakeOrPeek(out T result, bool take)
		{
			ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(false);
			if (threadList == null || threadList.Count == 0)
			{
				return this.Steal(out result, take);
			}
			bool flag = false;
			try
			{
				if (take)
				{
					Interlocked.Exchange(ref threadList.m_currentOp, 2);
					if (threadList.Count <= 2 || this.m_needSync)
					{
						threadList.m_currentOp = 0;
						Monitor.Enter(threadList, ref flag);
						if (threadList.Count == 0)
						{
							if (flag)
							{
								try
								{
								}
								finally
								{
									flag = false;
									Monitor.Exit(threadList);
								}
							}
							return this.Steal(out result, true);
						}
					}
					threadList.Remove(out result);
				}
				else if (!threadList.Peek(out result))
				{
					return this.Steal(out result, false);
				}
			}
			finally
			{
				threadList.m_currentOp = 0;
				if (flag)
				{
					Monitor.Exit(threadList);
				}
			}
			return true;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000AEE1C File Offset: 0x000AD01C
		private ConcurrentBag<T>.ThreadLocalList GetThreadList(bool forceCreate)
		{
			ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_locals.Value;
			if (threadLocalList != null)
			{
				return threadLocalList;
			}
			if (forceCreate)
			{
				object globalListsLock = this.GlobalListsLock;
				lock (globalListsLock)
				{
					if (this.m_headList == null)
					{
						threadLocalList = new ConcurrentBag<T>.ThreadLocalList(Thread.CurrentThread);
						this.m_headList = threadLocalList;
						this.m_tailList = threadLocalList;
					}
					else
					{
						threadLocalList = this.GetUnownedList();
						if (threadLocalList == null)
						{
							threadLocalList = new ConcurrentBag<T>.ThreadLocalList(Thread.CurrentThread);
							this.m_tailList.m_nextList = threadLocalList;
							this.m_tailList = threadLocalList;
						}
					}
					this.m_locals.Value = threadLocalList;
					return threadLocalList;
				}
			}
			return null;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000AEED4 File Offset: 0x000AD0D4
		private ConcurrentBag<T>.ThreadLocalList GetUnownedList()
		{
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				if (threadLocalList.m_ownerThread.ThreadState == System.Threading.ThreadState.Stopped)
				{
					threadLocalList.m_ownerThread = Thread.CurrentThread;
					return threadLocalList;
				}
			}
			return null;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000AEF18 File Offset: 0x000AD118
		private bool Steal(out T result, bool take)
		{
			if (take)
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryTakeSteals();
			}
			else
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryPeekSteals();
			}
			List<int> list = new List<int>();
			for (;;)
			{
				list.Clear();
				bool flag = false;
				ConcurrentBag<T>.ThreadLocalList threadLocalList;
				for (threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
				{
					list.Add(threadLocalList.m_version);
					if (threadLocalList.m_head != null && this.TrySteal(threadLocalList, out result, take))
					{
						return true;
					}
				}
				threadLocalList = this.m_headList;
				foreach (int num in list)
				{
					if (num != threadLocalList.m_version)
					{
						flag = true;
						if (threadLocalList.m_head != null && this.TrySteal(threadLocalList, out result, take))
						{
							return true;
						}
					}
					threadLocalList = threadLocalList.m_nextList;
				}
				if (!flag)
				{
					goto Block_6;
				}
			}
			return true;
			Block_6:
			result = default(T);
			return false;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000AF010 File Offset: 0x000AD210
		private bool TrySteal(ConcurrentBag<T>.ThreadLocalList list, out T result, bool take)
		{
			bool result2;
			lock (list)
			{
				if (this.CanSteal(list))
				{
					list.Steal(out result, take);
					result2 = true;
				}
				else
				{
					result = default(T);
					result2 = false;
				}
			}
			return result2;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000AF064 File Offset: 0x000AD264
		private bool CanSteal(ConcurrentBag<T>.ThreadLocalList list)
		{
			if (list.Count <= 2 && list.m_currentOp != 0)
			{
				SpinWait spinWait = default(SpinWait);
				while (list.m_currentOp != 0)
				{
					spinWait.SpinOnce();
				}
			}
			return list.Count > 0;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000AF0AC File Offset: 0x000AD2AC
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ConcurrentBag_CopyTo_ArgumentNullException"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ConcurrentBag_CopyTo_ArgumentOutOfRangeException"));
			}
			if (this.m_headList == null)
			{
				return;
			}
			bool lockTaken = false;
			try
			{
				this.FreezeBag(ref lockTaken);
				this.ToList().CopyTo(array, index);
			}
			finally
			{
				this.UnfreezeBag(lockTaken);
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000AF128 File Offset: 0x000AD328
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ConcurrentBag_CopyTo_ArgumentNullException"));
			}
			bool lockTaken = false;
			try
			{
				this.FreezeBag(ref lockTaken);
				((ICollection)this.ToList()).CopyTo(array, index);
			}
			finally
			{
				this.UnfreezeBag(lockTaken);
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000AF180 File Offset: 0x000AD380
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			if (this.m_headList == null)
			{
				return new T[0];
			}
			bool lockTaken = false;
			T[] result;
			try
			{
				this.FreezeBag(ref lockTaken);
				result = this.ToList().ToArray();
			}
			finally
			{
				this.UnfreezeBag(lockTaken);
			}
			return result;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000AF1D0 File Offset: 0x000AD3D0
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			if (this.m_headList == null)
			{
				return new List<T>().GetEnumerator();
			}
			bool lockTaken = false;
			IEnumerator<T> result;
			try
			{
				this.FreezeBag(ref lockTaken);
				result = this.ToList().GetEnumerator();
			}
			finally
			{
				this.UnfreezeBag(lockTaken);
			}
			return result;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000AF230 File Offset: 0x000AD430
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000AF238 File Offset: 0x000AD438
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000AF248 File Offset: 0x000AD448
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.m_locals = new ThreadLocal<ConcurrentBag<T>.ThreadLocalList>();
			ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(true);
			foreach (T item in this.m_serializationArray)
			{
				threadList.Add(item, false);
			}
			this.m_headList = threadList;
			this.m_tailList = threadList;
			this.m_serializationArray = null;
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x000AF2A8 File Offset: 0x000AD4A8
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_headList == null)
				{
					return 0;
				}
				bool lockTaken = false;
				int countInternal;
				try
				{
					this.FreezeBag(ref lockTaken);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.UnfreezeBag(lockTaken);
				}
				return countInternal;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000AF2F0 File Offset: 0x000AD4F0
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_headList == null)
				{
					return true;
				}
				bool lockTaken = false;
				bool result;
				try
				{
					this.FreezeBag(ref lockTaken);
					for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
					{
						if (threadLocalList.m_head != null)
						{
							return false;
						}
					}
					result = true;
				}
				finally
				{
					this.UnfreezeBag(lockTaken);
				}
				return result;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x000AF358 File Offset: 0x000AD558
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x000AF35B File Offset: 0x000AD55B
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x000AF36C File Offset: 0x000AD56C
		private object GlobalListsLock
		{
			get
			{
				return this.m_locals;
			}
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000AF374 File Offset: 0x000AD574
		private void FreezeBag(ref bool lockTaken)
		{
			Monitor.Enter(this.GlobalListsLock, ref lockTaken);
			this.m_needSync = true;
			this.AcquireAllLocks();
			this.WaitAllOperations();
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000AF395 File Offset: 0x000AD595
		private void UnfreezeBag(bool lockTaken)
		{
			this.ReleaseAllLocks();
			this.m_needSync = false;
			if (lockTaken)
			{
				Monitor.Exit(this.GlobalListsLock);
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000AF3B4 File Offset: 0x000AD5B4
		private void AcquireAllLocks()
		{
			bool flag = false;
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				try
				{
					Monitor.Enter(threadLocalList, ref flag);
				}
				finally
				{
					if (flag)
					{
						threadLocalList.m_lockTaken = true;
						flag = false;
					}
				}
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000AF404 File Offset: 0x000AD604
		private void ReleaseAllLocks()
		{
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				if (threadLocalList.m_lockTaken)
				{
					threadLocalList.m_lockTaken = false;
					Monitor.Exit(threadLocalList);
				}
			}
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000AF440 File Offset: 0x000AD640
		private void WaitAllOperations()
		{
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				if (threadLocalList.m_currentOp != 0)
				{
					SpinWait spinWait = default(SpinWait);
					while (threadLocalList.m_currentOp != 0)
					{
						spinWait.SpinOnce();
					}
				}
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000AF48C File Offset: 0x000AD68C
		private int GetCountInternal()
		{
			int num = 0;
			checked
			{
				for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
				{
					num += threadLocalList.Count;
				}
				return num;
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000AF4BC File Offset: 0x000AD6BC
		private List<T> ToList()
		{
			List<T> list = new List<T>();
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				for (ConcurrentBag<T>.Node node = threadLocalList.m_head; node != null; node = node.m_next)
				{
					list.Add(node.m_value);
				}
			}
			return list;
		}

		// Token: 0x04002060 RID: 8288
		[NonSerialized]
		private ThreadLocal<ConcurrentBag<T>.ThreadLocalList> m_locals;

		// Token: 0x04002061 RID: 8289
		[NonSerialized]
		private volatile ConcurrentBag<T>.ThreadLocalList m_headList;

		// Token: 0x04002062 RID: 8290
		[NonSerialized]
		private volatile ConcurrentBag<T>.ThreadLocalList m_tailList;

		// Token: 0x04002063 RID: 8291
		[NonSerialized]
		private bool m_needSync;

		// Token: 0x04002064 RID: 8292
		private T[] m_serializationArray;

		// Token: 0x0200080B RID: 2059
		[Serializable]
		internal class Node
		{
			// Token: 0x060044EA RID: 17642 RVA: 0x00120AE7 File Offset: 0x0011ECE7
			public Node(T value)
			{
				this.m_value = value;
			}

			// Token: 0x0400356C RID: 13676
			public readonly T m_value;

			// Token: 0x0400356D RID: 13677
			public ConcurrentBag<T>.Node m_next;

			// Token: 0x0400356E RID: 13678
			public ConcurrentBag<T>.Node m_prev;
		}

		// Token: 0x0200080C RID: 2060
		internal class ThreadLocalList
		{
			// Token: 0x060044EB RID: 17643 RVA: 0x00120AF6 File Offset: 0x0011ECF6
			internal ThreadLocalList(Thread ownerThread)
			{
				this.m_ownerThread = ownerThread;
			}

			// Token: 0x060044EC RID: 17644 RVA: 0x00120B08 File Offset: 0x0011ED08
			internal void Add(T item, bool updateCount)
			{
				ConcurrentBag<T>.Node node;
				checked
				{
					this.m_count++;
					node = new ConcurrentBag<T>.Node(item);
				}
				if (this.m_head == null)
				{
					this.m_head = node;
					this.m_tail = node;
					this.m_version++;
				}
				else
				{
					node.m_next = this.m_head;
					this.m_head.m_prev = node;
					this.m_head = node;
				}
				if (updateCount)
				{
					this.m_count -= this.m_stealCount;
					this.m_stealCount = 0;
				}
			}

			// Token: 0x060044ED RID: 17645 RVA: 0x00120B9C File Offset: 0x0011ED9C
			internal void Remove(out T result)
			{
				ConcurrentBag<T>.Node head = this.m_head;
				this.m_head = this.m_head.m_next;
				if (this.m_head != null)
				{
					this.m_head.m_prev = null;
				}
				else
				{
					this.m_tail = null;
				}
				this.m_count--;
				result = head.m_value;
			}

			// Token: 0x060044EE RID: 17646 RVA: 0x00120C04 File Offset: 0x0011EE04
			internal bool Peek(out T result)
			{
				ConcurrentBag<T>.Node head = this.m_head;
				if (head != null)
				{
					result = head.m_value;
					return true;
				}
				result = default(T);
				return false;
			}

			// Token: 0x060044EF RID: 17647 RVA: 0x00120C34 File Offset: 0x0011EE34
			internal void Steal(out T result, bool remove)
			{
				ConcurrentBag<T>.Node tail = this.m_tail;
				if (remove)
				{
					this.m_tail = this.m_tail.m_prev;
					if (this.m_tail != null)
					{
						this.m_tail.m_next = null;
					}
					else
					{
						this.m_head = null;
					}
					this.m_stealCount++;
				}
				result = tail.m_value;
			}

			// Token: 0x17000FA4 RID: 4004
			// (get) Token: 0x060044F0 RID: 17648 RVA: 0x00120C9F File Offset: 0x0011EE9F
			internal int Count
			{
				get
				{
					return this.m_count - this.m_stealCount;
				}
			}

			// Token: 0x0400356F RID: 13679
			internal volatile ConcurrentBag<T>.Node m_head;

			// Token: 0x04003570 RID: 13680
			private volatile ConcurrentBag<T>.Node m_tail;

			// Token: 0x04003571 RID: 13681
			internal volatile int m_currentOp;

			// Token: 0x04003572 RID: 13682
			private int m_count;

			// Token: 0x04003573 RID: 13683
			internal int m_stealCount;

			// Token: 0x04003574 RID: 13684
			internal volatile ConcurrentBag<T>.ThreadLocalList m_nextList;

			// Token: 0x04003575 RID: 13685
			internal bool m_lockTaken;

			// Token: 0x04003576 RID: 13686
			internal Thread m_ownerThread;

			// Token: 0x04003577 RID: 13687
			internal volatile int m_version;
		}

		// Token: 0x0200080D RID: 2061
		internal enum ListOperation
		{
			// Token: 0x04003579 RID: 13689
			None,
			// Token: 0x0400357A RID: 13690
			Add,
			// Token: 0x0400357B RID: 13691
			Take
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020003D0 RID: 976
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SystemThreadingCollections_BlockingCollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}, Type = {m_collection}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class BlockingCollection<T> : IEnumerable<!0>, IEnumerable, ICollection, IDisposable, IReadOnlyCollection<T>
	{
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000ADD44 File Offset: 0x000ABF44
		[__DynamicallyInvokable]
		public int BoundedCapacity
		{
			[__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_boundedCapacity;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000ADD52 File Offset: 0x000ABF52
		[__DynamicallyInvokable]
		public bool IsAddingCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_currentAdders == int.MinValue;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000ADD69 File Offset: 0x000ABF69
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.IsAddingCompleted && this.m_occupiedNodes.CurrentCount == 0;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000ADD89 File Offset: 0x000ABF89
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_occupiedNodes.CurrentCount;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000ADD9C File Offset: 0x000ABF9C
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return false;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000ADDA5 File Offset: 0x000ABFA5
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000ADDB6 File Offset: 0x000ABFB6
		[__DynamicallyInvokable]
		public BlockingCollection() : this(new ConcurrentQueue<T>())
		{
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000ADDC3 File Offset: 0x000ABFC3
		[__DynamicallyInvokable]
		public BlockingCollection(int boundedCapacity) : this(new ConcurrentQueue<T>(), boundedCapacity)
		{
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000ADDD4 File Offset: 0x000ABFD4
		[__DynamicallyInvokable]
		public BlockingCollection(IProducerConsumerCollection<T> collection, int boundedCapacity)
		{
			if (boundedCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("boundedCapacity", boundedCapacity, SR.GetString("BlockingCollection_ctor_BoundedCapacityRange"));
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int count = collection.Count;
			if (count > boundedCapacity)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_ctor_CountMoreThanCapacity"));
			}
			this.Initialize(collection, boundedCapacity, count);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000ADE38 File Offset: 0x000AC038
		[__DynamicallyInvokable]
		public BlockingCollection(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.Initialize(collection, -1, collection.Count);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000ADE5C File Offset: 0x000AC05C
		private void Initialize(IProducerConsumerCollection<T> collection, int boundedCapacity, int collectionCount)
		{
			this.m_collection = collection;
			this.m_boundedCapacity = boundedCapacity;
			this.m_isDisposed = false;
			this.m_ConsumersCancellationTokenSource = new CancellationTokenSource();
			this.m_ProducersCancellationTokenSource = new CancellationTokenSource();
			if (boundedCapacity == -1)
			{
				this.m_freeNodes = null;
			}
			else
			{
				this.m_freeNodes = new SemaphoreSlim(boundedCapacity - collectionCount);
			}
			this.m_occupiedNodes = new SemaphoreSlim(collectionCount);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000ADEBC File Offset: 0x000AC0BC
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			this.TryAddWithNoTimeValidation(item, -1, default(CancellationToken));
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000ADEDB File Offset: 0x000AC0DB
		[__DynamicallyInvokable]
		public void Add(T item, CancellationToken cancellationToken)
		{
			this.TryAddWithNoTimeValidation(item, -1, cancellationToken);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000ADEE8 File Offset: 0x000AC0E8
		[__DynamicallyInvokable]
		public bool TryAdd(T item)
		{
			return this.TryAddWithNoTimeValidation(item, 0, default(CancellationToken));
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000ADF08 File Offset: 0x000AC108
		[__DynamicallyInvokable]
		public bool TryAdd(T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return this.TryAddWithNoTimeValidation(item, (int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000ADF34 File Offset: 0x000AC134
		[__DynamicallyInvokable]
		public bool TryAdd(T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryAddWithNoTimeValidation(item, millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000ADF58 File Offset: 0x000AC158
		[__DynamicallyInvokable]
		public bool TryAdd(T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryAddWithNoTimeValidation(item, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000ADF6C File Offset: 0x000AC16C
		private bool TryAddWithNoTimeValidation(T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
			}
			if (this.IsAddingCompleted)
			{
				throw new InvalidOperationException(SR.GetString("BlockingCollection_Completed"));
			}
			bool flag = true;
			if (this.m_freeNodes != null)
			{
				CancellationTokenSource cancellationTokenSource = null;
				try
				{
					flag = this.m_freeNodes.Wait(0);
					if (!flag && millisecondsTimeout != 0)
					{
						cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ProducersCancellationTokenSource.Token);
						flag = this.m_freeNodes.Wait(millisecondsTimeout, cancellationTokenSource.Token);
					}
				}
				catch (OperationCanceledException)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
					}
					throw new InvalidOperationException(SR.GetString("BlockingCollection_Add_ConcurrentCompleteAdd"));
				}
				finally
				{
					if (cancellationTokenSource != null)
					{
						cancellationTokenSource.Dispose();
					}
				}
			}
			if (flag)
			{
				SpinWait spinWait = default(SpinWait);
				for (;;)
				{
					int currentAdders = this.m_currentAdders;
					if ((currentAdders & -2147483648) != 0)
					{
						break;
					}
					if (Interlocked.CompareExchange(ref this.m_currentAdders, currentAdders + 1, currentAdders) == currentAdders)
					{
						goto IL_11D;
					}
					spinWait.SpinOnce();
				}
				spinWait.Reset();
				while (this.m_currentAdders != -2147483648)
				{
					spinWait.SpinOnce();
				}
				throw new InvalidOperationException(SR.GetString("BlockingCollection_Completed"));
				IL_11D:
				try
				{
					bool flag2 = false;
					try
					{
						cancellationToken.ThrowIfCancellationRequested();
						flag2 = this.m_collection.TryAdd(item);
					}
					catch
					{
						if (this.m_freeNodes != null)
						{
							this.m_freeNodes.Release();
						}
						throw;
					}
					if (!flag2)
					{
						throw new InvalidOperationException(SR.GetString("BlockingCollection_Add_Failed"));
					}
					this.m_occupiedNodes.Release();
				}
				finally
				{
					Interlocked.Decrement(ref this.m_currentAdders);
				}
			}
			return flag;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000AE12C File Offset: 0x000AC32C
		[__DynamicallyInvokable]
		public T Take()
		{
			T result;
			if (!this.TryTake(out result, -1, CancellationToken.None))
			{
				throw new InvalidOperationException(SR.GetString("BlockingCollection_CantTakeWhenDone"));
			}
			return result;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000AE15C File Offset: 0x000AC35C
		[__DynamicallyInvokable]
		public T Take(CancellationToken cancellationToken)
		{
			T result;
			if (!this.TryTake(out result, -1, cancellationToken))
			{
				throw new InvalidOperationException(SR.GetString("BlockingCollection_CantTakeWhenDone"));
			}
			return result;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000AE186 File Offset: 0x000AC386
		[__DynamicallyInvokable]
		public bool TryTake(out T item)
		{
			return this.TryTake(out item, 0, CancellationToken.None);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000AE195 File Offset: 0x000AC395
		[__DynamicallyInvokable]
		public bool TryTake(out T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return this.TryTakeWithNoTimeValidation(out item, (int)timeout.TotalMilliseconds, CancellationToken.None, null);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000AE1B2 File Offset: 0x000AC3B2
		[__DynamicallyInvokable]
		public bool TryTake(out T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryTakeWithNoTimeValidation(out item, millisecondsTimeout, CancellationToken.None, null);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000AE1C8 File Offset: 0x000AC3C8
		[__DynamicallyInvokable]
		public bool TryTake(out T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryTakeWithNoTimeValidation(out item, millisecondsTimeout, cancellationToken, null);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000AE1DC File Offset: 0x000AC3DC
		private bool TryTakeWithNoTimeValidation(out T item, int millisecondsTimeout, CancellationToken cancellationToken, CancellationTokenSource combinedTokenSource)
		{
			this.CheckDisposed();
			item = default(T);
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
			}
			if (this.IsCompleted)
			{
				return false;
			}
			bool flag = false;
			CancellationTokenSource cancellationTokenSource = combinedTokenSource;
			try
			{
				flag = this.m_occupiedNodes.Wait(0);
				if (!flag && millisecondsTimeout != 0)
				{
					if (combinedTokenSource == null)
					{
						cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ConsumersCancellationTokenSource.Token);
					}
					flag = this.m_occupiedNodes.Wait(millisecondsTimeout, cancellationTokenSource.Token);
				}
			}
			catch (OperationCanceledException)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
				}
				return false;
			}
			finally
			{
				if (cancellationTokenSource != null && combinedTokenSource == null)
				{
					cancellationTokenSource.Dispose();
				}
			}
			if (flag)
			{
				bool flag2 = false;
				bool flag3 = true;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					flag2 = this.m_collection.TryTake(out item);
					flag3 = false;
					if (!flag2)
					{
						throw new InvalidOperationException(SR.GetString("BlockingCollection_Take_CollectionModified"));
					}
				}
				finally
				{
					if (flag2)
					{
						if (this.m_freeNodes != null)
						{
							this.m_freeNodes.Release();
						}
					}
					else if (flag3)
					{
						this.m_occupiedNodes.Release();
					}
					if (this.IsCompleted)
					{
						this.CancelWaitingConsumers();
					}
				}
			}
			return flag;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000AE324 File Offset: 0x000AC524
		[__DynamicallyInvokable]
		public static int AddToAny(BlockingCollection<T>[] collections, T item)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, -1, CancellationToken.None);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000AE333 File Offset: 0x000AC533
		[__DynamicallyInvokable]
		public static int AddToAny(BlockingCollection<T>[] collections, T item, CancellationToken cancellationToken)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, -1, cancellationToken);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000AE33E File Offset: 0x000AC53E
		[__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, 0, CancellationToken.None);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000AE34D File Offset: 0x000AC54D
		[__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return BlockingCollection<T>.TryAddToAnyCore(collections, item, (int)timeout.TotalMilliseconds, CancellationToken.None);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000AE369 File Offset: 0x000AC569
		[__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryAddToAnyCore(collections, item, millisecondsTimeout, CancellationToken.None);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000AE37E File Offset: 0x000AC57E
		[__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryAddToAnyCore(collections, item, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000AE390 File Offset: 0x000AC590
		private static int TryAddToAnyCore(BlockingCollection<T>[] collections, T item, int millisecondsTimeout, CancellationToken externalCancellationToken)
		{
			BlockingCollection<T>.ValidateCollectionsArray(collections, true);
			int num = millisecondsTimeout;
			uint startTime = 0U;
			if (millisecondsTimeout != -1)
			{
				startTime = (uint)Environment.TickCount;
			}
			int num2 = BlockingCollection<T>.TryAddToAnyFast(collections, item);
			if (num2 > -1)
			{
				return num2;
			}
			CancellationToken[] tokens;
			List<WaitHandle> handles = BlockingCollection<T>.GetHandles(collections, externalCancellationToken, true, out tokens);
			while (millisecondsTimeout == -1 || num >= 0)
			{
				num2 = -1;
				using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(tokens))
				{
					handles.Add(cancellationTokenSource.Token.WaitHandle);
					num2 = WaitHandle.WaitAny(handles.ToArray(), num, false);
					handles.RemoveAt(handles.Count - 1);
					if (cancellationTokenSource.IsCancellationRequested)
					{
						if (externalCancellationToken.IsCancellationRequested)
						{
							throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), externalCancellationToken);
						}
						throw new ArgumentException(SR.GetString("BlockingCollection_CantAddAnyWhenCompleted"), "collections");
					}
				}
				if (num2 == 258)
				{
					return -1;
				}
				if (collections[num2].TryAdd(item))
				{
					return num2;
				}
				if (millisecondsTimeout != -1)
				{
					num = BlockingCollection<T>.UpdateTimeOut(startTime, millisecondsTimeout);
				}
			}
			return -1;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000AE49C File Offset: 0x000AC69C
		private static int TryAddToAnyFast(BlockingCollection<T>[] collections, T item)
		{
			for (int i = 0; i < collections.Length; i++)
			{
				if (collections[i].m_freeNodes == null)
				{
					collections[i].TryAdd(item);
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000AE4D0 File Offset: 0x000AC6D0
		private static List<WaitHandle> GetHandles(BlockingCollection<T>[] collections, CancellationToken externalCancellationToken, bool isAddOperation, out CancellationToken[] cancellationTokens)
		{
			List<WaitHandle> list = new List<WaitHandle>(collections.Length + 1);
			List<CancellationToken> list2 = new List<CancellationToken>(collections.Length + 1);
			list2.Add(externalCancellationToken);
			if (isAddOperation)
			{
				for (int i = 0; i < collections.Length; i++)
				{
					if (collections[i].m_freeNodes != null)
					{
						list.Add(collections[i].m_freeNodes.AvailableWaitHandle);
						list2.Add(collections[i].m_ProducersCancellationTokenSource.Token);
					}
				}
			}
			else
			{
				for (int j = 0; j < collections.Length; j++)
				{
					if (!collections[j].IsCompleted)
					{
						list.Add(collections[j].m_occupiedNodes.AvailableWaitHandle);
						list2.Add(collections[j].m_ConsumersCancellationTokenSource.Token);
					}
				}
			}
			cancellationTokens = list2.ToArray();
			return list;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000AE584 File Offset: 0x000AC784
		private static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			if (originalWaitMillisecondsTimeout == 0)
			{
				return 0;
			}
			uint num = (uint)(Environment.TickCount - (int)startTime);
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000AE5B3 File Offset: 0x000AC7B3
		[__DynamicallyInvokable]
		public static int TakeFromAny(BlockingCollection<T>[] collections, out T item)
		{
			return BlockingCollection<T>.TakeFromAny(collections, out item, CancellationToken.None);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000AE5C4 File Offset: 0x000AC7C4
		[__DynamicallyInvokable]
		public static int TakeFromAny(BlockingCollection<T>[] collections, out T item, CancellationToken cancellationToken)
		{
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, -1, true, cancellationToken);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000AE5DD File Offset: 0x000AC7DD
		[__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item)
		{
			return BlockingCollection<T>.TryTakeFromAny(collections, out item, 0);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000AE5E7 File Offset: 0x000AC7E7
		[__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, (int)timeout.TotalMilliseconds, false, CancellationToken.None);
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000AE604 File Offset: 0x000AC804
		[__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, millisecondsTimeout, false, CancellationToken.None);
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000AE61A File Offset: 0x000AC81A
		[__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, millisecondsTimeout, false, cancellationToken);
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000AE62C File Offset: 0x000AC82C
		private static int TryTakeFromAnyCore(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, bool isTakeOperation, CancellationToken externalCancellationToken)
		{
			BlockingCollection<T>.ValidateCollectionsArray(collections, false);
			for (int i = 0; i < collections.Length; i++)
			{
				if (!collections[i].IsCompleted && collections[i].m_occupiedNodes.CurrentCount > 0 && collections[i].TryTake(out item))
				{
					return i;
				}
			}
			return BlockingCollection<T>.TryTakeFromAnyCoreSlow(collections, out item, millisecondsTimeout, isTakeOperation, externalCancellationToken);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000AE680 File Offset: 0x000AC880
		private static int TryTakeFromAnyCoreSlow(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, bool isTakeOperation, CancellationToken externalCancellationToken)
		{
			int num = millisecondsTimeout;
			uint startTime = 0U;
			if (millisecondsTimeout != -1)
			{
				startTime = (uint)Environment.TickCount;
			}
			while (millisecondsTimeout == -1 || num >= 0)
			{
				CancellationToken[] tokens;
				List<WaitHandle> handles = BlockingCollection<T>.GetHandles(collections, externalCancellationToken, false, out tokens);
				if (handles.Count == 0 && isTakeOperation)
				{
					throw new ArgumentException(SR.GetString("BlockingCollection_CantTakeAnyWhenAllDone"), "collections");
				}
				if (handles.Count != 0)
				{
					using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(tokens))
					{
						handles.Add(cancellationTokenSource.Token.WaitHandle);
						int num2 = WaitHandle.WaitAny(handles.ToArray(), num, false);
						if (cancellationTokenSource.IsCancellationRequested && externalCancellationToken.IsCancellationRequested)
						{
							throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), externalCancellationToken);
						}
						if (!cancellationTokenSource.IsCancellationRequested)
						{
							if (num2 == 258)
							{
								break;
							}
							if (collections.Length != handles.Count - 1)
							{
								for (int i = 0; i < collections.Length; i++)
								{
									if (collections[i].m_occupiedNodes.AvailableWaitHandle == handles[num2])
									{
										num2 = i;
										break;
									}
								}
							}
							if (collections[num2].TryTake(out item))
							{
								return num2;
							}
						}
					}
					if (millisecondsTimeout != -1)
					{
						num = BlockingCollection<T>.UpdateTimeOut(startTime, millisecondsTimeout);
						continue;
					}
					continue;
				}
				break;
			}
			item = default(T);
			return -1;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000AE7D4 File Offset: 0x000AC9D4
		[__DynamicallyInvokable]
		public void CompleteAdding()
		{
			this.CheckDisposed();
			if (this.IsAddingCompleted)
			{
				return;
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentAdders = this.m_currentAdders;
				if ((currentAdders & -2147483648) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_currentAdders, currentAdders | -2147483648, currentAdders) == currentAdders)
				{
					goto Block_4;
				}
				spinWait.SpinOnce();
			}
			spinWait.Reset();
			while (this.m_currentAdders != -2147483648)
			{
				spinWait.SpinOnce();
			}
			return;
			Block_4:
			spinWait.Reset();
			while (this.m_currentAdders != -2147483648)
			{
				spinWait.SpinOnce();
			}
			if (this.Count == 0)
			{
				this.CancelWaitingConsumers();
			}
			this.CancelWaitingProducers();
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000AE87F File Offset: 0x000ACA7F
		private void CancelWaitingConsumers()
		{
			this.m_ConsumersCancellationTokenSource.Cancel();
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000AE88C File Offset: 0x000ACA8C
		private void CancelWaitingProducers()
		{
			this.m_ProducersCancellationTokenSource.Cancel();
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000AE899 File Offset: 0x000ACA99
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000AE8A8 File Offset: 0x000ACAA8
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_isDisposed)
			{
				if (this.m_freeNodes != null)
				{
					this.m_freeNodes.Dispose();
				}
				this.m_occupiedNodes.Dispose();
				this.m_isDisposed = true;
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000AE8D7 File Offset: 0x000ACAD7
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			this.CheckDisposed();
			return this.m_collection.ToArray();
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000AE8EA File Offset: 0x000ACAEA
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000AE8F4 File Offset: 0x000ACAF4
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			this.CheckDisposed();
			T[] array2 = this.m_collection.ToArray();
			try
			{
				Array.Copy(array2, 0, array, index, array2.Length);
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("array");
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.GetString("BlockingCollection_CopyTo_NonNegative"));
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_TooManyElems"), "index");
			}
			catch (RankException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_MultiDim"), "array");
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_IncorrectType"), "array");
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_IncorrectType"), "array");
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000AE9F0 File Offset: 0x000ACBF0
		[__DynamicallyInvokable]
		public IEnumerable<T> GetConsumingEnumerable()
		{
			return this.GetConsumingEnumerable(CancellationToken.None);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000AE9FD File Offset: 0x000ACBFD
		[__DynamicallyInvokable]
		public IEnumerable<T> GetConsumingEnumerable(CancellationToken cancellationToken)
		{
			CancellationTokenSource linkedTokenSource = null;
			try
			{
				linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ConsumersCancellationTokenSource.Token);
				while (!this.IsCompleted)
				{
					T t;
					if (this.TryTakeWithNoTimeValidation(out t, -1, cancellationToken, linkedTokenSource))
					{
						yield return t;
					}
				}
			}
			finally
			{
				if (linkedTokenSource != null)
				{
					linkedTokenSource.Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000AEA14 File Offset: 0x000ACC14
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			this.CheckDisposed();
			return this.m_collection.GetEnumerator();
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000AEA27 File Offset: 0x000ACC27
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000AEA30 File Offset: 0x000ACC30
		private static void ValidateCollectionsArray(BlockingCollection<T>[] collections, bool isAddOperation)
		{
			if (collections == null)
			{
				throw new ArgumentNullException("collections");
			}
			if (collections.Length < 1)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_ValidateCollectionsArray_ZeroSize"), "collections");
			}
			if ((!BlockingCollection<T>.IsSTAThread && collections.Length > 63) || (BlockingCollection<T>.IsSTAThread && collections.Length > 62))
			{
				throw new ArgumentOutOfRangeException("collections", SR.GetString("BlockingCollection_ValidateCollectionsArray_LargeSize"));
			}
			for (int i = 0; i < collections.Length; i++)
			{
				if (collections[i] == null)
				{
					throw new ArgumentException(SR.GetString("BlockingCollection_ValidateCollectionsArray_NullElems"), "collections");
				}
				if (collections[i].m_isDisposed)
				{
					throw new ObjectDisposedException("collections", SR.GetString("BlockingCollection_ValidateCollectionsArray_DispElems"));
				}
				if (isAddOperation && collections[i].IsAddingCompleted)
				{
					throw new ArgumentException(SR.GetString("BlockingCollection_CantAddAnyWhenCompleted"), "collections");
				}
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000AEB00 File Offset: 0x000ACD00
		private static bool IsSTAThread
		{
			get
			{
				return Thread.CurrentThread.GetApartmentState() == ApartmentState.STA;
			}
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000AEB10 File Offset: 0x000ACD10
		private static void ValidateTimeout(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if ((num < 0L || num > 2147483647L) && num != -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, string.Format(CultureInfo.InvariantCulture, SR.GetString("BlockingCollection_TimeoutInvalid"), new object[]
				{
					int.MaxValue
				}));
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000AEB74 File Offset: 0x000ACD74
		private static void ValidateMillisecondsTimeout(int millisecondsTimeout)
		{
			if (millisecondsTimeout < 0 && millisecondsTimeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, string.Format(CultureInfo.InvariantCulture, SR.GetString("BlockingCollection_TimeoutInvalid"), new object[]
				{
					int.MaxValue
				}));
			}
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000AEBC1 File Offset: 0x000ACDC1
		private void CheckDisposed()
		{
			if (this.m_isDisposed)
			{
				throw new ObjectDisposedException("BlockingCollection", SR.GetString("BlockingCollection_Disposed"));
			}
		}

		// Token: 0x04002055 RID: 8277
		private IProducerConsumerCollection<T> m_collection;

		// Token: 0x04002056 RID: 8278
		private int m_boundedCapacity;

		// Token: 0x04002057 RID: 8279
		private const int NON_BOUNDED = -1;

		// Token: 0x04002058 RID: 8280
		private SemaphoreSlim m_freeNodes;

		// Token: 0x04002059 RID: 8281
		private SemaphoreSlim m_occupiedNodes;

		// Token: 0x0400205A RID: 8282
		private bool m_isDisposed;

		// Token: 0x0400205B RID: 8283
		private CancellationTokenSource m_ConsumersCancellationTokenSource;

		// Token: 0x0400205C RID: 8284
		private CancellationTokenSource m_ProducersCancellationTokenSource;

		// Token: 0x0400205D RID: 8285
		private volatile int m_currentAdders;

		// Token: 0x0400205E RID: 8286
		private const int COMPLETE_ADDING_ON_MASK = -2147483648;
	}
}

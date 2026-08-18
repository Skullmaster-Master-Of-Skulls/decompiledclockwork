using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200001D RID: 29
	internal sealed class InputQueue<T> : IDisposable where T : class
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004480 File Offset: 0x00002680
		public InputQueue()
		{
			this.itemQueue = new InputQueue<T>.ItemQueue();
			this.readerQueue = new Queue<InputQueue<T>.IQueueReader>();
			this.waiterList = new List<InputQueue<T>.IQueueWaiter>();
			this.queueState = InputQueue<T>.QueueState.Open;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000044B0 File Offset: 0x000026B0
		public InputQueue(Func<Action<AsyncCallback, IAsyncResult>> asyncCallbackGenerator) : this()
		{
			this.AsyncCallbackGenerator = asyncCallbackGenerator;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000044C0 File Offset: 0x000026C0
		public int PendingCount
		{
			get
			{
				object thisLock = this.ThisLock;
				int itemCount;
				lock (thisLock)
				{
					itemCount = this.itemQueue.ItemCount;
				}
				return itemCount;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004508 File Offset: 0x00002708
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004510 File Offset: 0x00002710
		public Action<T> DisposeItemCallback { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004519 File Offset: 0x00002719
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004521 File Offset: 0x00002721
		private Func<Action<AsyncCallback, IAsyncResult>> AsyncCallbackGenerator { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000452A File Offset: 0x0000272A
		private object ThisLock
		{
			get
			{
				return this.itemQueue;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004534 File Offset: 0x00002734
		public IAsyncResult BeginDequeue(TimeSpan timeout, AsyncCallback callback, object state)
		{
			InputQueue<T>.Item item = default(InputQueue<T>.Item);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (!this.itemQueue.HasAvailableItem)
					{
						InputQueue<T>.AsyncQueueReader asyncQueueReader = new InputQueue<T>.AsyncQueueReader(this, timeout, callback, state);
						this.readerQueue.Enqueue(asyncQueueReader);
						return asyncQueueReader;
					}
					item = this.itemQueue.DequeueAvailableItem();
				}
				else if (this.queueState == InputQueue<T>.QueueState.Shutdown)
				{
					if (this.itemQueue.HasAvailableItem)
					{
						item = this.itemQueue.DequeueAvailableItem();
					}
					else if (this.itemQueue.HasAnyItem)
					{
						InputQueue<T>.AsyncQueueReader asyncQueueReader2 = new InputQueue<T>.AsyncQueueReader(this, timeout, callback, state);
						this.readerQueue.Enqueue(asyncQueueReader2);
						return asyncQueueReader2;
					}
				}
			}
			InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
			return new CompletedAsyncResult<T>(item.GetValue(), callback, state);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004620 File Offset: 0x00002820
		public IAsyncResult BeginWaitForItem(TimeSpan timeout, AsyncCallback callback, object state)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (!this.itemQueue.HasAvailableItem)
					{
						InputQueue<T>.AsyncQueueWaiter asyncQueueWaiter = new InputQueue<T>.AsyncQueueWaiter(timeout, callback, state);
						this.waiterList.Add(asyncQueueWaiter);
						return asyncQueueWaiter;
					}
				}
				else if (this.queueState == InputQueue<T>.QueueState.Shutdown && !this.itemQueue.HasAvailableItem && this.itemQueue.HasAnyItem)
				{
					InputQueue<T>.AsyncQueueWaiter asyncQueueWaiter2 = new InputQueue<T>.AsyncQueueWaiter(timeout, callback, state);
					this.waiterList.Add(asyncQueueWaiter2);
					return asyncQueueWaiter2;
				}
			}
			return new CompletedAsyncResult<bool>(true, callback, state);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000046D4 File Offset: 0x000028D4
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000046DC File Offset: 0x000028DC
		public T Dequeue(TimeSpan timeout)
		{
			T result;
			if (!this.Dequeue(timeout, out result))
			{
				throw Fx.Exception.AsError(new TimeoutException(InternalSR.TimeoutInputQueueDequeue(timeout)));
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004710 File Offset: 0x00002910
		public bool Dequeue(TimeSpan timeout, out T value)
		{
			InputQueue<T>.WaitQueueReader waitQueueReader = null;
			InputQueue<T>.Item item = default(InputQueue<T>.Item);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (this.itemQueue.HasAvailableItem)
					{
						item = this.itemQueue.DequeueAvailableItem();
					}
					else
					{
						waitQueueReader = new InputQueue<T>.WaitQueueReader(this);
						this.readerQueue.Enqueue(waitQueueReader);
					}
				}
				else
				{
					if (this.queueState != InputQueue<T>.QueueState.Shutdown)
					{
						value = default(T);
						return true;
					}
					if (this.itemQueue.HasAvailableItem)
					{
						item = this.itemQueue.DequeueAvailableItem();
					}
					else
					{
						if (!this.itemQueue.HasAnyItem)
						{
							value = default(T);
							return true;
						}
						waitQueueReader = new InputQueue<T>.WaitQueueReader(this);
						this.readerQueue.Enqueue(waitQueueReader);
					}
				}
			}
			if (waitQueueReader != null)
			{
				return waitQueueReader.Wait(timeout, out value);
			}
			InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
			value = item.GetValue();
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004814 File Offset: 0x00002A14
		public void Dispatch()
		{
			InputQueue<T>.IQueueReader queueReader = null;
			InputQueue<T>.Item item = default(InputQueue<T>.Item);
			InputQueue<T>.IQueueReader[] array = null;
			InputQueue<T>.IQueueWaiter[] array2 = null;
			bool itemAvailable = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				itemAvailable = (this.queueState != InputQueue<T>.QueueState.Closed && this.queueState != InputQueue<T>.QueueState.Shutdown);
				this.GetWaiters(out array2);
				if (this.queueState != InputQueue<T>.QueueState.Closed)
				{
					this.itemQueue.MakePendingItemAvailable();
					if (this.readerQueue.Count > 0)
					{
						item = this.itemQueue.DequeueAvailableItem();
						queueReader = this.readerQueue.Dequeue();
						if (this.queueState == InputQueue<T>.QueueState.Shutdown && this.readerQueue.Count > 0 && this.itemQueue.ItemCount == 0)
						{
							array = new InputQueue<T>.IQueueReader[this.readerQueue.Count];
							this.readerQueue.CopyTo(array, 0);
							this.readerQueue.Clear();
							itemAvailable = false;
						}
					}
				}
			}
			if (array != null)
			{
				if (InputQueue<T>.completeOutstandingReadersCallback == null)
				{
					InputQueue<T>.completeOutstandingReadersCallback = new Action<object>(InputQueue<T>.CompleteOutstandingReadersCallback);
				}
				ActionItem.Schedule(InputQueue<T>.completeOutstandingReadersCallback, array);
			}
			if (array2 != null)
			{
				InputQueue<T>.CompleteWaitersLater(itemAvailable, array2);
			}
			if (queueReader != null)
			{
				InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				queueReader.Set(item);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000495C File Offset: 0x00002B5C
		public bool EndDequeue(IAsyncResult result, out T value)
		{
			CompletedAsyncResult<T> completedAsyncResult = result as CompletedAsyncResult<T>;
			if (completedAsyncResult != null)
			{
				value = CompletedAsyncResult<T>.End(result);
				return true;
			}
			return InputQueue<T>.AsyncQueueReader.End(result, out value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004988 File Offset: 0x00002B88
		public T EndDequeue(IAsyncResult result)
		{
			T result2;
			if (!this.EndDequeue(result, out result2))
			{
				throw Fx.Exception.AsError(new TimeoutException());
			}
			return result2;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000049B4 File Offset: 0x00002BB4
		public bool EndWaitForItem(IAsyncResult result)
		{
			CompletedAsyncResult<bool> completedAsyncResult = result as CompletedAsyncResult<bool>;
			if (completedAsyncResult != null)
			{
				return CompletedAsyncResult<bool>.End(result);
			}
			return InputQueue<T>.AsyncQueueWaiter.End(result);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000049D8 File Offset: 0x00002BD8
		public void EnqueueAndDispatch(T item)
		{
			this.EnqueueAndDispatch(item, null);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000049E2 File Offset: 0x00002BE2
		public void EnqueueAndDispatch(T item, Action dequeuedCallback)
		{
			this.EnqueueAndDispatch(item, dequeuedCallback, true);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000049ED File Offset: 0x00002BED
		public void EnqueueAndDispatch(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.EnqueueAndDispatch(new InputQueue<T>.Item(exception, dequeuedCallback), canDispatchOnThisThread);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000049FD File Offset: 0x00002BFD
		public void EnqueueAndDispatch(T item, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.EnqueueAndDispatch(new InputQueue<T>.Item(item, dequeuedCallback), canDispatchOnThisThread);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004A0D File Offset: 0x00002C0D
		public bool EnqueueWithoutDispatch(T item, Action dequeuedCallback)
		{
			return this.EnqueueWithoutDispatch(new InputQueue<T>.Item(item, dequeuedCallback));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004A1C File Offset: 0x00002C1C
		public bool EnqueueWithoutDispatch(Exception exception, Action dequeuedCallback)
		{
			return this.EnqueueWithoutDispatch(new InputQueue<T>.Item(exception, dequeuedCallback));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004A2B File Offset: 0x00002C2B
		public void Shutdown()
		{
			this.Shutdown(null);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004A34 File Offset: 0x00002C34
		public void Shutdown(Func<Exception> pendingExceptionGenerator)
		{
			InputQueue<T>.IQueueReader[] array = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Shutdown)
				{
					return;
				}
				if (this.queueState == InputQueue<T>.QueueState.Closed)
				{
					return;
				}
				this.queueState = InputQueue<T>.QueueState.Shutdown;
				if (this.readerQueue.Count > 0 && this.itemQueue.ItemCount == 0)
				{
					array = new InputQueue<T>.IQueueReader[this.readerQueue.Count];
					this.readerQueue.CopyTo(array, 0);
					this.readerQueue.Clear();
				}
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Exception exception = (pendingExceptionGenerator != null) ? pendingExceptionGenerator() : null;
					array[i].Set(new InputQueue<T>.Item(exception, null));
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004B08 File Offset: 0x00002D08
		public bool WaitForItem(TimeSpan timeout)
		{
			InputQueue<T>.WaitQueueWaiter waitQueueWaiter = null;
			bool result = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (this.itemQueue.HasAvailableItem)
					{
						result = true;
					}
					else
					{
						waitQueueWaiter = new InputQueue<T>.WaitQueueWaiter();
						this.waiterList.Add(waitQueueWaiter);
					}
				}
				else
				{
					if (this.queueState != InputQueue<T>.QueueState.Shutdown)
					{
						return true;
					}
					if (this.itemQueue.HasAvailableItem)
					{
						result = true;
					}
					else
					{
						if (!this.itemQueue.HasAnyItem)
						{
							return true;
						}
						waitQueueWaiter = new InputQueue<T>.WaitQueueWaiter();
						this.waiterList.Add(waitQueueWaiter);
					}
				}
			}
			if (waitQueueWaiter != null)
			{
				return waitQueueWaiter.Wait(timeout);
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public void Dispose()
		{
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState != InputQueue<T>.QueueState.Closed)
				{
					this.queueState = InputQueue<T>.QueueState.Closed;
					flag = true;
				}
			}
			if (flag)
			{
				while (this.readerQueue.Count > 0)
				{
					InputQueue<T>.IQueueReader queueReader = this.readerQueue.Dequeue();
					queueReader.Set(default(InputQueue<T>.Item));
				}
				while (this.itemQueue.HasAnyItem)
				{
					InputQueue<T>.Item item = this.itemQueue.DequeueAnyItem();
					this.DisposeItem(item);
					InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004C78 File Offset: 0x00002E78
		private void DisposeItem(InputQueue<T>.Item item)
		{
			T value = item.Value;
			if (value != null)
			{
				if (value is IDisposable)
				{
					((IDisposable)((object)value)).Dispose();
					return;
				}
				Action<T> disposeItemCallback = this.DisposeItemCallback;
				if (disposeItemCallback != null)
				{
					disposeItemCallback(value);
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private static void CompleteOutstandingReadersCallback(object state)
		{
			InputQueue<T>.IQueueReader[] array = (InputQueue<T>.IQueueReader[])state;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Set(default(InputQueue<T>.Item));
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004CF8 File Offset: 0x00002EF8
		private static void CompleteWaiters(bool itemAvailable, InputQueue<T>.IQueueWaiter[] waiters)
		{
			for (int i = 0; i < waiters.Length; i++)
			{
				waiters[i].Set(itemAvailable);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D1C File Offset: 0x00002F1C
		private static void CompleteWaitersFalseCallback(object state)
		{
			InputQueue<T>.CompleteWaiters(false, (InputQueue<T>.IQueueWaiter[])state);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D2C File Offset: 0x00002F2C
		private static void CompleteWaitersLater(bool itemAvailable, InputQueue<T>.IQueueWaiter[] waiters)
		{
			if (itemAvailable)
			{
				if (InputQueue<T>.completeWaitersTrueCallback == null)
				{
					InputQueue<T>.completeWaitersTrueCallback = new Action<object>(InputQueue<T>.CompleteWaitersTrueCallback);
				}
				ActionItem.Schedule(InputQueue<T>.completeWaitersTrueCallback, waiters);
				return;
			}
			if (InputQueue<T>.completeWaitersFalseCallback == null)
			{
				InputQueue<T>.completeWaitersFalseCallback = new Action<object>(InputQueue<T>.CompleteWaitersFalseCallback);
			}
			ActionItem.Schedule(InputQueue<T>.completeWaitersFalseCallback, waiters);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004D83 File Offset: 0x00002F83
		private static void CompleteWaitersTrueCallback(object state)
		{
			InputQueue<T>.CompleteWaiters(true, (InputQueue<T>.IQueueWaiter[])state);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D91 File Offset: 0x00002F91
		private static void InvokeDequeuedCallback(Action dequeuedCallback)
		{
			if (dequeuedCallback != null)
			{
				dequeuedCallback();
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D9C File Offset: 0x00002F9C
		private static void InvokeDequeuedCallbackLater(Action dequeuedCallback)
		{
			if (dequeuedCallback != null)
			{
				if (InputQueue<T>.onInvokeDequeuedCallback == null)
				{
					InputQueue<T>.onInvokeDequeuedCallback = new Action<object>(InputQueue<T>.OnInvokeDequeuedCallback);
				}
				ActionItem.Schedule(InputQueue<T>.onInvokeDequeuedCallback, dequeuedCallback);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004DC4 File Offset: 0x00002FC4
		private static void OnDispatchCallback(object state)
		{
			((InputQueue<T>)state).Dispatch();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004DD4 File Offset: 0x00002FD4
		private static void OnInvokeDequeuedCallback(object state)
		{
			Action action = (Action)state;
			action();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004DF0 File Offset: 0x00002FF0
		private void EnqueueAndDispatch(InputQueue<T>.Item item, bool canDispatchOnThisThread)
		{
			bool flag = false;
			InputQueue<T>.IQueueReader queueReader = null;
			bool flag2 = false;
			InputQueue<T>.IQueueWaiter[] array = null;
			bool itemAvailable = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				itemAvailable = (this.queueState != InputQueue<T>.QueueState.Closed && this.queueState != InputQueue<T>.QueueState.Shutdown);
				this.GetWaiters(out array);
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (canDispatchOnThisThread)
					{
						if (this.readerQueue.Count == 0)
						{
							this.itemQueue.EnqueueAvailableItem(item);
						}
						else
						{
							queueReader = this.readerQueue.Dequeue();
						}
					}
					else if (this.readerQueue.Count == 0)
					{
						this.itemQueue.EnqueueAvailableItem(item);
					}
					else
					{
						this.itemQueue.EnqueuePendingItem(item);
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (array != null)
			{
				if (canDispatchOnThisThread)
				{
					InputQueue<T>.CompleteWaiters(itemAvailable, array);
				}
				else
				{
					InputQueue<T>.CompleteWaitersLater(itemAvailable, array);
				}
			}
			if (queueReader != null)
			{
				InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				queueReader.Set(item);
			}
			if (flag2)
			{
				if (InputQueue<T>.onDispatchCallback == null)
				{
					InputQueue<T>.onDispatchCallback = new Action<object>(InputQueue<T>.OnDispatchCallback);
				}
				ActionItem.Schedule(InputQueue<T>.onDispatchCallback, this);
				return;
			}
			if (flag)
			{
				InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				this.DisposeItem(item);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004F28 File Offset: 0x00003128
		private bool EnqueueWithoutDispatch(InputQueue<T>.Item item)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState != InputQueue<T>.QueueState.Closed && this.queueState != InputQueue<T>.QueueState.Shutdown)
				{
					if (this.readerQueue.Count == 0 && this.waiterList.Count == 0)
					{
						this.itemQueue.EnqueueAvailableItem(item);
						return false;
					}
					this.itemQueue.EnqueuePendingItem(item);
					return true;
				}
			}
			this.DisposeItem(item);
			InputQueue<T>.InvokeDequeuedCallbackLater(item.DequeuedCallback);
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004FC4 File Offset: 0x000031C4
		private void GetWaiters(out InputQueue<T>.IQueueWaiter[] waiters)
		{
			if (this.waiterList.Count > 0)
			{
				waiters = this.waiterList.ToArray();
				this.waiterList.Clear();
				return;
			}
			waiters = null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004FF0 File Offset: 0x000031F0
		private bool RemoveReader(InputQueue<T>.IQueueReader reader)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open || this.queueState == InputQueue<T>.QueueState.Shutdown)
				{
					bool result = false;
					for (int i = this.readerQueue.Count; i > 0; i--)
					{
						InputQueue<T>.IQueueReader queueReader = this.readerQueue.Dequeue();
						if (queueReader == reader)
						{
							result = true;
						}
						else
						{
							this.readerQueue.Enqueue(queueReader);
						}
					}
					return result;
				}
			}
			return false;
		}

		// Token: 0x0400006B RID: 107
		private static Action<object> completeOutstandingReadersCallback;

		// Token: 0x0400006C RID: 108
		private static Action<object> completeWaitersFalseCallback;

		// Token: 0x0400006D RID: 109
		private static Action<object> completeWaitersTrueCallback;

		// Token: 0x0400006E RID: 110
		private static Action<object> onDispatchCallback;

		// Token: 0x0400006F RID: 111
		private static Action<object> onInvokeDequeuedCallback;

		// Token: 0x04000070 RID: 112
		private InputQueue<T>.QueueState queueState;

		// Token: 0x04000071 RID: 113
		private InputQueue<T>.ItemQueue itemQueue;

		// Token: 0x04000072 RID: 114
		private Queue<InputQueue<T>.IQueueReader> readerQueue;

		// Token: 0x04000073 RID: 115
		private List<InputQueue<T>.IQueueWaiter> waiterList;

		// Token: 0x0200006C RID: 108
		private enum QueueState
		{
			// Token: 0x04000235 RID: 565
			Open,
			// Token: 0x04000236 RID: 566
			Shutdown,
			// Token: 0x04000237 RID: 567
			Closed
		}

		// Token: 0x0200006D RID: 109
		private interface IQueueReader
		{
			// Token: 0x060003A4 RID: 932
			void Set(InputQueue<T>.Item item);
		}

		// Token: 0x0200006E RID: 110
		private interface IQueueWaiter
		{
			// Token: 0x060003A5 RID: 933
			void Set(bool itemAvailable);
		}

		// Token: 0x0200006F RID: 111
		private struct Item
		{
			// Token: 0x060003A6 RID: 934 RVA: 0x00011CC0 File Offset: 0x0000FEC0
			public Item(T value, Action dequeuedCallback)
			{
				this = new InputQueue<T>.Item(value, null, dequeuedCallback);
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x00011CCC File Offset: 0x0000FECC
			public Item(Exception exception, Action dequeuedCallback)
			{
				this = new InputQueue<T>.Item(default(T), exception, dequeuedCallback);
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00011CEA File Offset: 0x0000FEEA
			private Item(T value, Exception exception, Action dequeuedCallback)
			{
				this.value = value;
				this.exception = exception;
				this.dequeuedCallback = dequeuedCallback;
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x00011D01 File Offset: 0x0000FF01
			public Action DequeuedCallback
			{
				get
				{
					return this.dequeuedCallback;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060003AA RID: 938 RVA: 0x00011D09 File Offset: 0x0000FF09
			public Exception Exception
			{
				get
				{
					return this.exception;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060003AB RID: 939 RVA: 0x00011D11 File Offset: 0x0000FF11
			public T Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00011D19 File Offset: 0x0000FF19
			public T GetValue()
			{
				if (this.exception != null)
				{
					throw Fx.Exception.AsError(this.exception);
				}
				return this.value;
			}

			// Token: 0x04000238 RID: 568
			private Action dequeuedCallback;

			// Token: 0x04000239 RID: 569
			private Exception exception;

			// Token: 0x0400023A RID: 570
			private T value;
		}

		// Token: 0x02000070 RID: 112
		private class AsyncQueueReader : AsyncResult, InputQueue<T>.IQueueReader
		{
			// Token: 0x060003AD RID: 941 RVA: 0x00011D3C File Offset: 0x0000FF3C
			public AsyncQueueReader(InputQueue<T> inputQueue, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (inputQueue.AsyncCallbackGenerator != null)
				{
					base.VirtualCallback = inputQueue.AsyncCallbackGenerator();
				}
				this.inputQueue = inputQueue;
				if (timeout != TimeSpan.MaxValue)
				{
					this.timer = new IOThreadTimer(InputQueue<T>.AsyncQueueReader.timerCallback, this, false);
					this.timer.Set(timeout);
				}
			}

			// Token: 0x060003AE RID: 942 RVA: 0x00011DA0 File Offset: 0x0000FFA0
			public static bool End(IAsyncResult result, out T value)
			{
				InputQueue<T>.AsyncQueueReader asyncQueueReader = AsyncResult.End<InputQueue<T>.AsyncQueueReader>(result);
				if (asyncQueueReader.expired)
				{
					value = default(T);
					return false;
				}
				value = asyncQueueReader.item;
				return true;
			}

			// Token: 0x060003AF RID: 943 RVA: 0x00011DD2 File Offset: 0x0000FFD2
			public void Set(InputQueue<T>.Item item)
			{
				this.item = item.Value;
				if (this.timer != null)
				{
					this.timer.Cancel();
				}
				base.Complete(false, item.Exception);
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00011E04 File Offset: 0x00010004
			private static void TimerCallback(object state)
			{
				InputQueue<T>.AsyncQueueReader asyncQueueReader = (InputQueue<T>.AsyncQueueReader)state;
				if (asyncQueueReader.inputQueue.RemoveReader(asyncQueueReader))
				{
					asyncQueueReader.expired = true;
					asyncQueueReader.Complete(false);
				}
			}

			// Token: 0x0400023B RID: 571
			private static Action<object> timerCallback = new Action<object>(InputQueue<T>.AsyncQueueReader.TimerCallback);

			// Token: 0x0400023C RID: 572
			private bool expired;

			// Token: 0x0400023D RID: 573
			private InputQueue<T> inputQueue;

			// Token: 0x0400023E RID: 574
			private T item;

			// Token: 0x0400023F RID: 575
			private IOThreadTimer timer;
		}

		// Token: 0x02000071 RID: 113
		private class AsyncQueueWaiter : AsyncResult, InputQueue<T>.IQueueWaiter
		{
			// Token: 0x060003B2 RID: 946 RVA: 0x00011E47 File Offset: 0x00010047
			public AsyncQueueWaiter(TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (timeout != TimeSpan.MaxValue)
				{
					this.timer = new IOThreadTimer(InputQueue<T>.AsyncQueueWaiter.timerCallback, this, false);
					this.timer.Set(timeout);
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011E87 File Offset: 0x00010087
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x00011E90 File Offset: 0x00010090
			public static bool End(IAsyncResult result)
			{
				InputQueue<T>.AsyncQueueWaiter asyncQueueWaiter = AsyncResult.End<InputQueue<T>.AsyncQueueWaiter>(result);
				return asyncQueueWaiter.itemAvailable;
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x00011EAC File Offset: 0x000100AC
			public void Set(bool itemAvailable)
			{
				object obj = this.ThisLock;
				bool flag2;
				lock (obj)
				{
					flag2 = (this.timer == null || this.timer.Cancel());
					this.itemAvailable = itemAvailable;
				}
				if (flag2)
				{
					base.Complete(false);
				}
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x00011F10 File Offset: 0x00010110
			private static void TimerCallback(object state)
			{
				InputQueue<T>.AsyncQueueWaiter asyncQueueWaiter = (InputQueue<T>.AsyncQueueWaiter)state;
				asyncQueueWaiter.Complete(false);
			}

			// Token: 0x04000240 RID: 576
			private static Action<object> timerCallback = new Action<object>(InputQueue<T>.AsyncQueueWaiter.TimerCallback);

			// Token: 0x04000241 RID: 577
			private bool itemAvailable;

			// Token: 0x04000242 RID: 578
			private object thisLock = new object();

			// Token: 0x04000243 RID: 579
			private IOThreadTimer timer;
		}

		// Token: 0x02000072 RID: 114
		private class ItemQueue
		{
			// Token: 0x060003B8 RID: 952 RVA: 0x00011F3E File Offset: 0x0001013E
			public ItemQueue()
			{
				this.items = new InputQueue<T>.Item[1];
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x00011F52 File Offset: 0x00010152
			public bool HasAnyItem
			{
				get
				{
					return this.totalCount > 0;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00011F5D File Offset: 0x0001015D
			public bool HasAvailableItem
			{
				get
				{
					return this.totalCount > this.pendingCount;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060003BB RID: 955 RVA: 0x00011F6D File Offset: 0x0001016D
			public int ItemCount
			{
				get
				{
					return this.totalCount;
				}
			}

			// Token: 0x060003BC RID: 956 RVA: 0x00011F75 File Offset: 0x00010175
			public InputQueue<T>.Item DequeueAnyItem()
			{
				if (this.pendingCount == this.totalCount)
				{
					this.pendingCount--;
				}
				return this.DequeueItemCore();
			}

			// Token: 0x060003BD RID: 957 RVA: 0x00011F99 File Offset: 0x00010199
			public InputQueue<T>.Item DequeueAvailableItem()
			{
				Fx.AssertAndThrow(this.totalCount != this.pendingCount, "ItemQueue does not contain any available items");
				return this.DequeueItemCore();
			}

			// Token: 0x060003BE RID: 958 RVA: 0x00011FBC File Offset: 0x000101BC
			public void EnqueueAvailableItem(InputQueue<T>.Item item)
			{
				this.EnqueueItemCore(item);
			}

			// Token: 0x060003BF RID: 959 RVA: 0x00011FC5 File Offset: 0x000101C5
			public void EnqueuePendingItem(InputQueue<T>.Item item)
			{
				this.EnqueueItemCore(item);
				this.pendingCount++;
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x00011FDC File Offset: 0x000101DC
			public void MakePendingItemAvailable()
			{
				Fx.AssertAndThrow(this.pendingCount != 0, "ItemQueue does not contain any pending items");
				this.pendingCount--;
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x00012000 File Offset: 0x00010200
			private InputQueue<T>.Item DequeueItemCore()
			{
				Fx.AssertAndThrow(this.totalCount != 0, "ItemQueue does not contain any items");
				InputQueue<T>.Item result = this.items[this.head];
				this.items[this.head] = default(InputQueue<T>.Item);
				this.totalCount--;
				this.head = (this.head + 1) % this.items.Length;
				return result;
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x00012070 File Offset: 0x00010270
			private void EnqueueItemCore(InputQueue<T>.Item item)
			{
				if (this.totalCount == this.items.Length)
				{
					InputQueue<T>.Item[] array = new InputQueue<T>.Item[this.items.Length * 2];
					for (int i = 0; i < this.totalCount; i++)
					{
						array[i] = this.items[(this.head + i) % this.items.Length];
					}
					this.head = 0;
					this.items = array;
				}
				int num = (this.head + this.totalCount) % this.items.Length;
				this.items[num] = item;
				this.totalCount++;
			}

			// Token: 0x04000244 RID: 580
			private int head;

			// Token: 0x04000245 RID: 581
			private InputQueue<T>.Item[] items;

			// Token: 0x04000246 RID: 582
			private int pendingCount;

			// Token: 0x04000247 RID: 583
			private int totalCount;
		}

		// Token: 0x02000073 RID: 115
		private class WaitQueueReader : InputQueue<T>.IQueueReader
		{
			// Token: 0x060003C3 RID: 963 RVA: 0x00012111 File Offset: 0x00010311
			public WaitQueueReader(InputQueue<T> inputQueue)
			{
				this.inputQueue = inputQueue;
				this.waitEvent = new ManualResetEvent(false);
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x0001212C File Offset: 0x0001032C
			public void Set(InputQueue<T>.Item item)
			{
				lock (this)
				{
					this.exception = item.Exception;
					this.item = item.Value;
					this.waitEvent.Set();
				}
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x00012188 File Offset: 0x00010388
			public bool Wait(TimeSpan timeout, out T value)
			{
				bool flag = false;
				try
				{
					if (!TimeoutHelper.WaitOne(this.waitEvent, timeout))
					{
						if (this.inputQueue.RemoveReader(this))
						{
							value = default(T);
							flag = true;
							return false;
						}
						this.waitEvent.WaitOne();
					}
					flag = true;
				}
				finally
				{
					if (flag)
					{
						this.waitEvent.Close();
					}
				}
				if (this.exception != null)
				{
					throw Fx.Exception.AsError(this.exception);
				}
				value = this.item;
				return true;
			}

			// Token: 0x04000248 RID: 584
			private Exception exception;

			// Token: 0x04000249 RID: 585
			private InputQueue<T> inputQueue;

			// Token: 0x0400024A RID: 586
			private T item;

			// Token: 0x0400024B RID: 587
			private ManualResetEvent waitEvent;
		}

		// Token: 0x02000074 RID: 116
		private class WaitQueueWaiter : InputQueue<T>.IQueueWaiter
		{
			// Token: 0x060003C6 RID: 966 RVA: 0x00012218 File Offset: 0x00010418
			public WaitQueueWaiter()
			{
				this.waitEvent = new ManualResetEvent(false);
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x0001222C File Offset: 0x0001042C
			public void Set(bool itemAvailable)
			{
				lock (this)
				{
					this.itemAvailable = itemAvailable;
					this.waitEvent.Set();
				}
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x00012274 File Offset: 0x00010474
			public bool Wait(TimeSpan timeout)
			{
				return TimeoutHelper.WaitOne(this.waitEvent, timeout) && this.itemAvailable;
			}

			// Token: 0x0400024C RID: 588
			private bool itemAvailable;

			// Token: 0x0400024D RID: 589
			private ManualResetEvent waitEvent;
		}
	}
}

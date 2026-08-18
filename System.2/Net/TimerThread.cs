using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200021C RID: 540
	internal static class TimerThread
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x0006952C File Offset: 0x0006772C
		static TimerThread()
		{
			TimerThread.s_ThreadEvents = new WaitHandle[]
			{
				TimerThread.s_ThreadShutdownEvent,
				TimerThread.s_ThreadReadyEvent
			};
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000695A4 File Offset: 0x000677A4
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> obj = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (obj)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference value = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(value);
			}
			return timerQueue;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00069614 File Offset: 0x00067814
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> obj = TimerThread.s_NewQueues;
				lock (obj)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj2 in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000697B8 File Offset: 0x000679B8
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000697F8 File Offset: 0x000679F8
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> obj = TimerThread.s_Queues;
			lock (obj)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> obj2 = TimerThread.s_NewQueues;
									lock (obj2)
									{
										for (LinkedListNode<WeakReference> first = TimerThread.s_NewQueues.First; first != null; first = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(first);
											TimerThread.s_Queues.AddLast(first);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode = TimerThread.s_Queues.First;
								while (linkedListNode != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode.Next;
										TimerThread.s_Queues.Remove(linkedListNode);
										linkedListNode = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode = linkedListNode.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int millisecondsTimeout = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num3 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, millisecondsTimeout, false);
								if (num3 == 0)
								{
									break;
								}
								if (num3 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_1AC;
									}
								}
							}
							flag2 = false;
							continue;
							IL_1AC:
							flag2 = false;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							if (Logging.On)
							{
								Logging.PrintError(Logging.Web, "TimerThread#" + Thread.CurrentThread.ManagedThreadId.ToString(NumberFormatInfo.InvariantInfo) + "::ThreadProc() - Exception:" + ex.ToString());
							}
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00069A6C File Offset: 0x00067C6C
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00069A85 File Offset: 0x00067C85
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00069AA4 File Offset: 0x00067CA4
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x040015EE RID: 5614
		private const int c_ThreadIdleTimeoutMilliseconds = 30000;

		// Token: 0x040015EF RID: 5615
		private const int c_CacheScanPerIterations = 32;

		// Token: 0x040015F0 RID: 5616
		private const int c_TickCountResolution = 15;

		// Token: 0x040015F1 RID: 5617
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x040015F2 RID: 5618
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x040015F3 RID: 5619
		private static int s_ThreadState = 0;

		// Token: 0x040015F4 RID: 5620
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x040015F5 RID: 5621
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x040015F6 RID: 5622
		private static WaitHandle[] s_ThreadEvents;

		// Token: 0x040015F7 RID: 5623
		private static int s_CacheScanIteration;

		// Token: 0x040015F8 RID: 5624
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x0200075D RID: 1885
		internal abstract class Queue
		{
			// Token: 0x06004222 RID: 16930 RVA: 0x00112B0C File Offset: 0x00110D0C
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x17000F20 RID: 3872
			// (get) Token: 0x06004223 RID: 16931 RVA: 0x00112B1B File Offset: 0x00110D1B
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x06004224 RID: 16932 RVA: 0x00112B23 File Offset: 0x00110D23
			internal TimerThread.Timer CreateTimer()
			{
				return this.CreateTimer(null, null);
			}

			// Token: 0x06004225 RID: 16933
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x04003238 RID: 12856
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200075E RID: 1886
		internal abstract class Timer : IDisposable
		{
			// Token: 0x06004226 RID: 16934 RVA: 0x00112B2D File Offset: 0x00110D2D
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x17000F21 RID: 3873
			// (get) Token: 0x06004227 RID: 16935 RVA: 0x00112B47 File Offset: 0x00110D47
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000F22 RID: 3874
			// (get) Token: 0x06004228 RID: 16936 RVA: 0x00112B4F File Offset: 0x00110D4F
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x17000F23 RID: 3875
			// (get) Token: 0x06004229 RID: 16937 RVA: 0x00112B57 File Offset: 0x00110D57
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000F24 RID: 3876
			// (get) Token: 0x0600422A RID: 16938 RVA: 0x00112B68 File Offset: 0x00110D68
			internal int TimeRemaining
			{
				get
				{
					if (this.HasExpired)
					{
						return 0;
					}
					if (this.Duration == -1)
					{
						return -1;
					}
					int tickCount = Environment.TickCount;
					int num = (int)(TimerThread.IsTickBetween(this.StartTime, this.Expiration, tickCount) ? Math.Min((uint)(this.Expiration - tickCount), 2147483647U) : 0U);
					if (num >= 2)
					{
						return num;
					}
					return num + 1;
				}
			}

			// Token: 0x0600422B RID: 16939
			internal abstract bool Cancel();

			// Token: 0x17000F25 RID: 3877
			// (get) Token: 0x0600422C RID: 16940
			internal abstract bool HasExpired { get; }

			// Token: 0x0600422D RID: 16941 RVA: 0x00112BC3 File Offset: 0x00110DC3
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04003239 RID: 12857
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x0400323A RID: 12858
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200075F RID: 1887
		// (Invoke) Token: 0x0600422F RID: 16943
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x02000760 RID: 1888
		private enum TimerThreadState
		{
			// Token: 0x0400323C RID: 12860
			Idle,
			// Token: 0x0400323D RID: 12861
			Running,
			// Token: 0x0400323E RID: 12862
			Stopped
		}

		// Token: 0x02000761 RID: 1889
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x06004232 RID: 16946 RVA: 0x00112BCC File Offset: 0x00110DCC
			internal TimerQueue(int durationMilliseconds) : base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x06004233 RID: 16947 RVA: 0x00112C04 File Offset: 0x00110E04
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x06004234 RID: 16948 RVA: 0x00112CD0 File Offset: 0x00110ED0
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode next;
				do
				{
					next = this.m_Timers.Next;
					if (next == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							next = this.m_Timers.Next;
							if (next == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (next.Fire());
				nextExpiration = next.Expiration;
				return true;
			}

			// Token: 0x0400323F RID: 12863
			private IntPtr m_ThisHandle;

			// Token: 0x04003240 RID: 12864
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x02000762 RID: 1890
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x06004235 RID: 16949 RVA: 0x00112D84 File Offset: 0x00110F84
			internal InfiniteTimerQueue() : base(-1)
			{
			}

			// Token: 0x06004236 RID: 16950 RVA: 0x00112D8D File Offset: 0x00110F8D
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x02000763 RID: 1891
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x06004237 RID: 16951 RVA: 0x00112D94 File Offset: 0x00110F94
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock) : base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x06004238 RID: 16952 RVA: 0x00112DBD File Offset: 0x00110FBD
			internal TimerNode() : base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x17000F26 RID: 3878
			// (get) Token: 0x06004239 RID: 16953 RVA: 0x00112DCD File Offset: 0x00110FCD
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x17000F27 RID: 3879
			// (get) Token: 0x0600423A RID: 16954 RVA: 0x00112DD8 File Offset: 0x00110FD8
			// (set) Token: 0x0600423B RID: 16955 RVA: 0x00112DE0 File Offset: 0x00110FE0
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000F28 RID: 3880
			// (get) Token: 0x0600423C RID: 16956 RVA: 0x00112DE9 File Offset: 0x00110FE9
			// (set) Token: 0x0600423D RID: 16957 RVA: 0x00112DF1 File Offset: 0x00110FF1
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x0600423E RID: 16958 RVA: 0x00112DFC File Offset: 0x00110FFC
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x0600423F RID: 16959 RVA: 0x00112E94 File Offset: 0x00111094
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = (this.m_Callback != null);
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						if (Logging.On)
						{
							Logging.PrintError(Logging.Web, "TimerThreadTimer#" + base.StartTime.ToString(NumberFormatInfo.InvariantInfo) + "::Fire() - " + SR.GetString("net_log_exception_in_callback", new object[]
							{
								ex
							}));
						}
					}
				}
				return true;
			}

			// Token: 0x04003241 RID: 12865
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x04003242 RID: 12866
			private TimerThread.Callback m_Callback;

			// Token: 0x04003243 RID: 12867
			private object m_Context;

			// Token: 0x04003244 RID: 12868
			private object m_QueueLock;

			// Token: 0x04003245 RID: 12869
			private TimerThread.TimerNode next;

			// Token: 0x04003246 RID: 12870
			private TimerThread.TimerNode prev;

			// Token: 0x02000918 RID: 2328
			private enum TimerState
			{
				// Token: 0x04003D80 RID: 15744
				Ready,
				// Token: 0x04003D81 RID: 15745
				Fired,
				// Token: 0x04003D82 RID: 15746
				Cancelled,
				// Token: 0x04003D83 RID: 15747
				Sentinel
			}
		}

		// Token: 0x02000764 RID: 1892
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x06004240 RID: 16960 RVA: 0x00112FD8 File Offset: 0x001111D8
			internal InfiniteTimer() : base(-1)
			{
			}

			// Token: 0x17000F29 RID: 3881
			// (get) Token: 0x06004241 RID: 16961 RVA: 0x00112FE1 File Offset: 0x001111E1
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004242 RID: 16962 RVA: 0x00112FE4 File Offset: 0x001111E4
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04003247 RID: 12871
			private int cancelled;
		}
	}
}

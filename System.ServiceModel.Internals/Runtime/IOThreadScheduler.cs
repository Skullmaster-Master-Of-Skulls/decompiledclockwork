using System;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000020 RID: 32
	internal class IOThreadScheduler
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000051C4 File Offset: 0x000033C4
		[SecuritySafeCritical]
		private IOThreadScheduler(int capacity, int capacityLowPri)
		{
			this.slots = new IOThreadScheduler.Slot[capacity];
			this.slotsLowPri = new IOThreadScheduler.Slot[capacityLowPri];
			this.overlapped = new IOThreadScheduler.ScheduledOverlapped();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005210 File Offset: 0x00003410
		[SecurityCritical]
		public static void ScheduleCallbackNoFlow(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			bool flag = false;
			while (!flag)
			{
				try
				{
				}
				finally
				{
					flag = IOThreadScheduler.current.ScheduleCallbackHelper(callback, state);
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005258 File Offset: 0x00003458
		[SecurityCritical]
		public static void ScheduleCallbackLowPriNoFlow(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			bool flag = false;
			while (!flag)
			{
				try
				{
				}
				finally
				{
					flag = IOThreadScheduler.current.ScheduleCallbackLowPriHelper(callback, state);
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000052A0 File Offset: 0x000034A0
		[SecurityCritical]
		private bool ScheduleCallbackHelper(Action<object> callback, object state)
		{
			int num = Interlocked.Add(ref this.headTail, 65536);
			bool flag = IOThreadScheduler.Bits.Count(num) == 0;
			if (flag)
			{
				num = Interlocked.Add(ref this.headTail, 65536);
			}
			if (IOThreadScheduler.Bits.Count(num) == -1)
			{
				throw Fx.AssertAndThrowFatal("Head/Tail overflow!");
			}
			bool flag2;
			bool result = this.slots[num >> 16 & this.SlotMask].TryEnqueueWorkItem(callback, state, out flag2);
			if (flag2)
			{
				IOThreadScheduler value = new IOThreadScheduler(Math.Min(this.slots.Length * 2, 32768), this.slotsLowPri.Length);
				Interlocked.CompareExchange<IOThreadScheduler>(ref IOThreadScheduler.current, value, this);
			}
			if (flag)
			{
				this.overlapped.Post(this);
			}
			return result;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005354 File Offset: 0x00003554
		[SecurityCritical]
		private bool ScheduleCallbackLowPriHelper(Action<object> callback, object state)
		{
			int num = Interlocked.Add(ref this.headTailLowPri, 65536);
			bool flag = false;
			if (IOThreadScheduler.Bits.CountNoIdle(num) == 1)
			{
				int num2 = this.headTail;
				if (IOThreadScheduler.Bits.Count(num2) == -1)
				{
					int num3 = Interlocked.CompareExchange(ref this.headTail, num2 + 65536, num2);
					if (num2 == num3)
					{
						flag = true;
					}
				}
			}
			if (IOThreadScheduler.Bits.CountNoIdle(num) == 0)
			{
				throw Fx.AssertAndThrowFatal("Low-priority Head/Tail overflow!");
			}
			bool flag2;
			bool result = this.slotsLowPri[num >> 16 & this.SlotMaskLowPri].TryEnqueueWorkItem(callback, state, out flag2);
			if (flag2)
			{
				IOThreadScheduler value = new IOThreadScheduler(this.slots.Length, Math.Min(this.slotsLowPri.Length * 2, 32768));
				Interlocked.CompareExchange<IOThreadScheduler>(ref IOThreadScheduler.current, value, this);
			}
			if (flag)
			{
				this.overlapped.Post(this);
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005428 File Offset: 0x00003628
		[SecurityCritical]
		private void CompletionCallback(out Action<object> callback, out object state)
		{
			int num = this.headTail;
			int num2;
			for (;;)
			{
				bool flag = IOThreadScheduler.Bits.Count(num) == 0;
				if (flag)
				{
					num2 = this.headTailLowPri;
					while (IOThreadScheduler.Bits.CountNoIdle(num2) != 0)
					{
						if (num2 == (num2 = Interlocked.CompareExchange(ref this.headTailLowPri, IOThreadScheduler.Bits.IncrementLo(num2), num2)))
						{
							goto Block_2;
						}
					}
				}
				if (num == (num = Interlocked.CompareExchange(ref this.headTail, IOThreadScheduler.Bits.IncrementLo(num), num)))
				{
					if (!flag)
					{
						goto Block_4;
					}
					num2 = this.headTailLowPri;
					if (IOThreadScheduler.Bits.CountNoIdle(num2) == 0)
					{
						goto IL_DD;
					}
					num = IOThreadScheduler.Bits.IncrementLo(num);
					if (num != Interlocked.CompareExchange(ref this.headTail, num + 65536, num))
					{
						goto IL_DD;
					}
					num += 65536;
				}
			}
			Block_2:
			this.overlapped.Post(this);
			this.slotsLowPri[num2 & this.SlotMaskLowPri].DequeueWorkItem(out callback, out state);
			return;
			Block_4:
			this.overlapped.Post(this);
			this.slots[num & this.SlotMask].DequeueWorkItem(out callback, out state);
			return;
			IL_DD:
			callback = null;
			state = null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005518 File Offset: 0x00003718
		[SecurityCritical]
		private bool TryCoalesce(out Action<object> callback, out object state)
		{
			int num = this.headTail;
			int num2;
			for (;;)
			{
				if (IOThreadScheduler.Bits.Count(num) > 0)
				{
					if (num == (num = Interlocked.CompareExchange(ref this.headTail, IOThreadScheduler.Bits.IncrementLo(num), num)))
					{
						break;
					}
				}
				else
				{
					num2 = this.headTailLowPri;
					if (IOThreadScheduler.Bits.CountNoIdle(num2) <= 0)
					{
						goto IL_92;
					}
					if (num2 == (num2 = Interlocked.CompareExchange(ref this.headTailLowPri, IOThreadScheduler.Bits.IncrementLo(num2), num2)))
					{
						goto Block_4;
					}
					num = this.headTail;
				}
			}
			this.slots[num & this.SlotMask].DequeueWorkItem(out callback, out state);
			return true;
			Block_4:
			this.slotsLowPri[num2 & this.SlotMaskLowPri].DequeueWorkItem(out callback, out state);
			return true;
			IL_92:
			callback = null;
			state = null;
			return false;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000055BE File Offset: 0x000037BE
		private int SlotMask
		{
			[SecurityCritical]
			get
			{
				return this.slots.Length - 1;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000055CA File Offset: 0x000037CA
		private int SlotMaskLowPri
		{
			[SecurityCritical]
			get
			{
				return this.slotsLowPri.Length - 1;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000055D8 File Offset: 0x000037D8
		~IOThreadScheduler()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.Cleanup();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005618 File Offset: 0x00003818
		[SecuritySafeCritical]
		private void Cleanup()
		{
			if (this.overlapped != null)
			{
				this.overlapped.Cleanup();
			}
		}

		// Token: 0x0400007B RID: 123
		private const int MaximumCapacity = 32768;

		// Token: 0x0400007C RID: 124
		private static IOThreadScheduler current = new IOThreadScheduler(32, 32);

		// Token: 0x0400007D RID: 125
		private readonly IOThreadScheduler.ScheduledOverlapped overlapped;

		// Token: 0x0400007E RID: 126
		[SecurityCritical]
		private readonly IOThreadScheduler.Slot[] slots;

		// Token: 0x0400007F RID: 127
		[SecurityCritical]
		private readonly IOThreadScheduler.Slot[] slotsLowPri;

		// Token: 0x04000080 RID: 128
		private int headTail = -131072;

		// Token: 0x04000081 RID: 129
		private int headTailLowPri = -65536;

		// Token: 0x02000077 RID: 119
		private static class Bits
		{
			// Token: 0x060003DA RID: 986 RVA: 0x0001273F File Offset: 0x0001093F
			public static int Count(int slot)
			{
				return ((slot >> 16) - slot + 2 & 65535) - 1;
			}

			// Token: 0x060003DB RID: 987 RVA: 0x00012751 File Offset: 0x00010951
			public static int CountNoIdle(int slot)
			{
				return (slot >> 16) - slot + 1 & 65535;
			}

			// Token: 0x060003DC RID: 988 RVA: 0x00012761 File Offset: 0x00010961
			public static int IncrementLo(int slot)
			{
				return (slot + 1 & 65535) | (slot & -65536);
			}

			// Token: 0x060003DD RID: 989 RVA: 0x00012774 File Offset: 0x00010974
			public static bool IsComplete(int gate)
			{
				return (gate & -65536) == gate << 16;
			}

			// Token: 0x04000259 RID: 601
			public const int HiShift = 16;

			// Token: 0x0400025A RID: 602
			public const int HiOne = 65536;

			// Token: 0x0400025B RID: 603
			public const int LoHiBit = 32768;

			// Token: 0x0400025C RID: 604
			public const int HiHiBit = -2147483648;

			// Token: 0x0400025D RID: 605
			public const int LoCountMask = 32767;

			// Token: 0x0400025E RID: 606
			public const int HiCountMask = 2147418112;

			// Token: 0x0400025F RID: 607
			public const int LoMask = 65535;

			// Token: 0x04000260 RID: 608
			public const int HiMask = -65536;

			// Token: 0x04000261 RID: 609
			public const int HiBits = -2147450880;
		}

		// Token: 0x02000078 RID: 120
		private struct Slot
		{
			// Token: 0x060003DE RID: 990 RVA: 0x00012784 File Offset: 0x00010984
			public bool TryEnqueueWorkItem(Action<object> callback, object state, out bool wrapped)
			{
				int num = Interlocked.Increment(ref this.gate);
				wrapped = ((num & 32767) != 1);
				if (wrapped)
				{
					if ((num & 32768) != 0 && IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
					return false;
				}
				this.state = state;
				this.callback = callback;
				num = Interlocked.Add(ref this.gate, 32768);
				if ((num & 2147418112) == 0)
				{
					return true;
				}
				this.state = null;
				this.callback = null;
				if (num >> 16 != (num & 32767) || Interlocked.CompareExchange(ref this.gate, 0, num) != num)
				{
					num = Interlocked.Add(ref this.gate, int.MinValue);
					if (IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
				}
				return false;
			}

			// Token: 0x060003DF RID: 991 RVA: 0x00012850 File Offset: 0x00010A50
			public void DequeueWorkItem(out Action<object> callback, out object state)
			{
				int num = Interlocked.Add(ref this.gate, 65536);
				if ((num & 32768) == 0)
				{
					callback = null;
					state = null;
					return;
				}
				if ((num & 2147418112) == 65536)
				{
					callback = this.callback;
					state = this.state;
					this.state = null;
					this.callback = null;
					if ((num & 32767) != 1 || Interlocked.CompareExchange(ref this.gate, 0, num) != num)
					{
						num = Interlocked.Add(ref this.gate, int.MinValue);
						if (IOThreadScheduler.Bits.IsComplete(num))
						{
							Interlocked.CompareExchange(ref this.gate, 0, num);
							return;
						}
					}
				}
				else
				{
					callback = null;
					state = null;
					if (IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
				}
			}

			// Token: 0x04000262 RID: 610
			private int gate;

			// Token: 0x04000263 RID: 611
			private Action<object> callback;

			// Token: 0x04000264 RID: 612
			private object state;
		}

		// Token: 0x02000079 RID: 121
		[SecurityCritical]
		private class ScheduledOverlapped
		{
			// Token: 0x060003E0 RID: 992 RVA: 0x00012908 File Offset: 0x00010B08
			public ScheduledOverlapped()
			{
				this.nativeOverlapped = new Overlapped().UnsafePack(Fx.ThunkCallback(new IOCompletionCallback(this.IOCallback)), null);
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x00012934 File Offset: 0x00010B34
			private unsafe void IOCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				IOThreadScheduler iothreadScheduler = this.scheduler;
				this.scheduler = null;
				Action<object> action;
				object obj;
				try
				{
				}
				finally
				{
					iothreadScheduler.CompletionCallback(out action, out obj);
				}
				bool flag = true;
				while (flag)
				{
					if (action != null)
					{
						action(obj);
					}
					try
					{
					}
					finally
					{
						flag = iothreadScheduler.TryCoalesce(out action, out obj);
					}
				}
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x00012998 File Offset: 0x00010B98
			public void Post(IOThreadScheduler iots)
			{
				this.scheduler = iots;
				ThreadPool.UnsafeQueueNativeOverlapped(this.nativeOverlapped);
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x000129AD File Offset: 0x00010BAD
			public void Cleanup()
			{
				if (this.scheduler != null)
				{
					throw Fx.AssertAndThrowFatal("Cleanup called on an overlapped that is in-flight.");
				}
				Overlapped.Free(this.nativeOverlapped);
			}

			// Token: 0x04000265 RID: 613
			private unsafe readonly NativeOverlapped* nativeOverlapped;

			// Token: 0x04000266 RID: 614
			private IOThreadScheduler scheduler;
		}
	}
}

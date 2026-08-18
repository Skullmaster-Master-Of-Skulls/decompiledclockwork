using System;
using System.ComponentModel;
using System.Runtime.Interop;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime
{
	// Token: 0x02000021 RID: 33
	internal class IOThreadTimer
	{
		// Token: 0x06000107 RID: 263 RVA: 0x0000563D File Offset: 0x0000383D
		public IOThreadTimer(Action<object> callback, object callbackState, bool isTypicallyCanceledShortlyAfterBeingSet) : this(callback, callbackState, isTypicallyCanceledShortlyAfterBeingSet, 100)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000564C File Offset: 0x0000384C
		public IOThreadTimer(Action<object> callback, object callbackState, bool isTypicallyCanceledShortlyAfterBeingSet, int maxSkewInMilliseconds)
		{
			this.callback = callback;
			this.callbackState = callbackState;
			this.maxSkew = Ticks.FromMilliseconds(maxSkewInMilliseconds);
			this.timerGroup = (isTypicallyCanceledShortlyAfterBeingSet ? IOThreadTimer.TimerManager.Value.VolatileTimerGroup : IOThreadTimer.TimerManager.Value.StableTimerGroup);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005699 File Offset: 0x00003899
		public static long SystemTimeResolutionTicks
		{
			get
			{
				if (IOThreadTimer.systemTimeResolutionTicks == -1L)
				{
					IOThreadTimer.systemTimeResolutionTicks = IOThreadTimer.GetSystemTimeResolution();
				}
				return IOThreadTimer.systemTimeResolutionTicks;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000056B4 File Offset: 0x000038B4
		[SecuritySafeCritical]
		private static long GetSystemTimeResolution()
		{
			int num;
			uint num2;
			uint num3;
			if (UnsafeNativeMethods.GetSystemTimeAdjustment(out num, out num2, out num3) != 0U)
			{
				return (long)((ulong)num2);
			}
			return 150000L;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000056D7 File Offset: 0x000038D7
		public bool Cancel()
		{
			return IOThreadTimer.TimerManager.Value.Cancel(this);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000056E4 File Offset: 0x000038E4
		public void Set(TimeSpan timeFromNow)
		{
			if (timeFromNow != TimeSpan.MaxValue)
			{
				this.SetAt(Ticks.Add(Ticks.Now, Ticks.FromTimeSpan(timeFromNow)));
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005709 File Offset: 0x00003909
		public void Set(int millisecondsFromNow)
		{
			this.SetAt(Ticks.Add(Ticks.Now, Ticks.FromMilliseconds(millisecondsFromNow)));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005721 File Offset: 0x00003921
		public void SetAt(long dueTime)
		{
			IOThreadTimer.TimerManager.Value.Set(this, dueTime);
		}

		// Token: 0x04000082 RID: 130
		private const int maxSkewInMillisecondsDefault = 100;

		// Token: 0x04000083 RID: 131
		private static long systemTimeResolutionTicks = -1L;

		// Token: 0x04000084 RID: 132
		private Action<object> callback;

		// Token: 0x04000085 RID: 133
		private object callbackState;

		// Token: 0x04000086 RID: 134
		private long dueTime;

		// Token: 0x04000087 RID: 135
		private int index;

		// Token: 0x04000088 RID: 136
		private long maxSkew;

		// Token: 0x04000089 RID: 137
		private IOThreadTimer.TimerGroup timerGroup;

		// Token: 0x0200007A RID: 122
		private class TimerManager
		{
			// Token: 0x060003E4 RID: 996 RVA: 0x000129D0 File Offset: 0x00010BD0
			public TimerManager()
			{
				this.onWaitCallback = new Action<object>(this.OnWaitCallback);
				this.stableTimerGroup = new IOThreadTimer.TimerGroup();
				this.volatileTimerGroup = new IOThreadTimer.TimerGroup();
				this.waitableTimers = new IOThreadTimer.WaitableTimer[]
				{
					this.stableTimerGroup.WaitableTimer,
					this.volatileTimerGroup.WaitableTimer
				};
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x00005E5F File Offset: 0x0000405F
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060003E6 RID: 998 RVA: 0x00012A33 File Offset: 0x00010C33
			public static IOThreadTimer.TimerManager Value
			{
				get
				{
					return IOThreadTimer.TimerManager.value;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x00012A3A File Offset: 0x00010C3A
			public IOThreadTimer.TimerGroup StableTimerGroup
			{
				get
				{
					return this.stableTimerGroup;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00012A42 File Offset: 0x00010C42
			public IOThreadTimer.TimerGroup VolatileTimerGroup
			{
				get
				{
					return this.volatileTimerGroup;
				}
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x00012A4C File Offset: 0x00010C4C
			public void Set(IOThreadTimer timer, long dueTime)
			{
				long num = dueTime - timer.dueTime;
				if (num < 0L)
				{
					num = -num;
				}
				if (num > timer.maxSkew)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						IOThreadTimer.TimerGroup timerGroup = timer.timerGroup;
						IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
						if (timer.index > 0)
						{
							if (timerQueue.UpdateTimer(timer, dueTime))
							{
								this.UpdateWaitableTimer(timerGroup);
							}
						}
						else if (timerQueue.InsertTimer(timer, dueTime))
						{
							this.UpdateWaitableTimer(timerGroup);
							if (timerQueue.Count == 1)
							{
								this.EnsureWaitScheduled();
							}
						}
					}
				}
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x00012AF0 File Offset: 0x00010CF0
			public bool Cancel(IOThreadTimer timer)
			{
				object thisLock = this.ThisLock;
				bool result;
				lock (thisLock)
				{
					if (timer.index > 0)
					{
						IOThreadTimer.TimerGroup timerGroup = timer.timerGroup;
						IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
						timerQueue.DeleteTimer(timer);
						if (timerQueue.Count > 0)
						{
							this.UpdateWaitableTimer(timerGroup);
						}
						else
						{
							IOThreadTimer.TimerGroup otherTimerGroup = this.GetOtherTimerGroup(timerGroup);
							if (otherTimerGroup.TimerQueue.Count == 0)
							{
								long now = Ticks.Now;
								long num = timerGroup.WaitableTimer.DueTime - now;
								long num2 = otherTimerGroup.WaitableTimer.DueTime - now;
								if (num > 10000000L && num2 > 10000000L)
								{
									timerGroup.WaitableTimer.Set(Ticks.Add(now, 10000000L));
								}
							}
						}
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x00012BD4 File Offset: 0x00010DD4
			private void EnsureWaitScheduled()
			{
				if (!this.waitScheduled)
				{
					this.ScheduleWait();
				}
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x00012BE4 File Offset: 0x00010DE4
			private IOThreadTimer.TimerGroup GetOtherTimerGroup(IOThreadTimer.TimerGroup timerGroup)
			{
				if (timerGroup == this.volatileTimerGroup)
				{
					return this.stableTimerGroup;
				}
				return this.volatileTimerGroup;
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x00012BFC File Offset: 0x00010DFC
			private void OnWaitCallback(object state)
			{
				WaitHandle[] waitHandles = this.waitableTimers;
				WaitHandle.WaitAny(waitHandles);
				long now = Ticks.Now;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.waitScheduled = false;
					this.ScheduleElapsedTimers(now);
					this.ReactivateWaitableTimers();
					this.ScheduleWaitIfAnyTimersLeft();
				}
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x00012C64 File Offset: 0x00010E64
			private void ReactivateWaitableTimers()
			{
				this.ReactivateWaitableTimer(this.stableTimerGroup);
				this.ReactivateWaitableTimer(this.volatileTimerGroup);
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x00012C80 File Offset: 0x00010E80
			private void ReactivateWaitableTimer(IOThreadTimer.TimerGroup timerGroup)
			{
				IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
				if (timerQueue.Count > 0)
				{
					timerGroup.WaitableTimer.Set(timerQueue.MinTimer.dueTime);
					return;
				}
				timerGroup.WaitableTimer.Set(long.MaxValue);
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x00012CC8 File Offset: 0x00010EC8
			private void ScheduleElapsedTimers(long now)
			{
				this.ScheduleElapsedTimers(this.stableTimerGroup, now);
				this.ScheduleElapsedTimers(this.volatileTimerGroup, now);
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x00012CE4 File Offset: 0x00010EE4
			private void ScheduleElapsedTimers(IOThreadTimer.TimerGroup timerGroup, long now)
			{
				IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
				while (timerQueue.Count > 0)
				{
					IOThreadTimer minTimer = timerQueue.MinTimer;
					long num = minTimer.dueTime - now;
					if (num > minTimer.maxSkew)
					{
						break;
					}
					timerQueue.DeleteMinTimer();
					ActionItem.Schedule(minTimer.callback, minTimer.callbackState);
				}
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x00012D33 File Offset: 0x00010F33
			private void ScheduleWait()
			{
				ActionItem.Schedule(this.onWaitCallback, null);
				this.waitScheduled = true;
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x00012D48 File Offset: 0x00010F48
			private void ScheduleWaitIfAnyTimersLeft()
			{
				if (this.stableTimerGroup.TimerQueue.Count > 0 || this.volatileTimerGroup.TimerQueue.Count > 0)
				{
					this.ScheduleWait();
				}
			}

			// Token: 0x060003F4 RID: 1012 RVA: 0x00012D78 File Offset: 0x00010F78
			private void UpdateWaitableTimer(IOThreadTimer.TimerGroup timerGroup)
			{
				IOThreadTimer.WaitableTimer waitableTimer = timerGroup.WaitableTimer;
				IOThreadTimer minTimer = timerGroup.TimerQueue.MinTimer;
				long num = waitableTimer.DueTime - minTimer.dueTime;
				if (num < 0L)
				{
					num = -num;
				}
				if (num > minTimer.maxSkew)
				{
					waitableTimer.Set(minTimer.dueTime);
				}
			}

			// Token: 0x04000267 RID: 615
			private const long maxTimeToWaitForMoreTimers = 10000000L;

			// Token: 0x04000268 RID: 616
			private static IOThreadTimer.TimerManager value = new IOThreadTimer.TimerManager();

			// Token: 0x04000269 RID: 617
			private Action<object> onWaitCallback;

			// Token: 0x0400026A RID: 618
			private IOThreadTimer.TimerGroup stableTimerGroup;

			// Token: 0x0400026B RID: 619
			private IOThreadTimer.TimerGroup volatileTimerGroup;

			// Token: 0x0400026C RID: 620
			private IOThreadTimer.WaitableTimer[] waitableTimers;

			// Token: 0x0400026D RID: 621
			private bool waitScheduled;
		}

		// Token: 0x0200007B RID: 123
		private class TimerGroup
		{
			// Token: 0x060003F6 RID: 1014 RVA: 0x00012DCF File Offset: 0x00010FCF
			public TimerGroup()
			{
				this.waitableTimer = new IOThreadTimer.WaitableTimer();
				this.waitableTimer.Set(long.MaxValue);
				this.timerQueue = new IOThreadTimer.TimerQueue();
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00012E01 File Offset: 0x00011001
			public IOThreadTimer.TimerQueue TimerQueue
			{
				get
				{
					return this.timerQueue;
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00012E09 File Offset: 0x00011009
			public IOThreadTimer.WaitableTimer WaitableTimer
			{
				get
				{
					return this.waitableTimer;
				}
			}

			// Token: 0x0400026E RID: 622
			private IOThreadTimer.TimerQueue timerQueue;

			// Token: 0x0400026F RID: 623
			private IOThreadTimer.WaitableTimer waitableTimer;
		}

		// Token: 0x0200007C RID: 124
		private class TimerQueue
		{
			// Token: 0x060003F9 RID: 1017 RVA: 0x00012E11 File Offset: 0x00011011
			public TimerQueue()
			{
				this.timers = new IOThreadTimer[4];
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060003FA RID: 1018 RVA: 0x00012E25 File Offset: 0x00011025
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060003FB RID: 1019 RVA: 0x00012E2D File Offset: 0x0001102D
			public IOThreadTimer MinTimer
			{
				get
				{
					return this.timers[1];
				}
			}

			// Token: 0x060003FC RID: 1020 RVA: 0x00012E38 File Offset: 0x00011038
			public void DeleteMinTimer()
			{
				IOThreadTimer minTimer = this.MinTimer;
				this.DeleteMinTimerCore();
				minTimer.index = 0;
				minTimer.dueTime = 0L;
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x00012E64 File Offset: 0x00011064
			public void DeleteTimer(IOThreadTimer timer)
			{
				int num = timer.index;
				IOThreadTimer[] array = this.timers;
				for (;;)
				{
					int num2 = num / 2;
					if (num2 < 1)
					{
						break;
					}
					IOThreadTimer iothreadTimer = array[num2];
					array[num] = iothreadTimer;
					iothreadTimer.index = num;
					num = num2;
				}
				timer.index = 0;
				timer.dueTime = 0L;
				array[1] = null;
				this.DeleteMinTimerCore();
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x00012EB4 File Offset: 0x000110B4
			public bool InsertTimer(IOThreadTimer timer, long dueTime)
			{
				IOThreadTimer[] array = this.timers;
				int num = this.count + 1;
				if (num == array.Length)
				{
					array = new IOThreadTimer[array.Length * 2];
					Array.Copy(this.timers, array, this.timers.Length);
					this.timers = array;
				}
				this.count = num;
				if (num > 1)
				{
					for (;;)
					{
						int num2 = num / 2;
						if (num2 == 0)
						{
							break;
						}
						IOThreadTimer iothreadTimer = array[num2];
						if (iothreadTimer.dueTime <= dueTime)
						{
							break;
						}
						array[num] = iothreadTimer;
						iothreadTimer.index = num;
						num = num2;
					}
				}
				array[num] = timer;
				timer.index = num;
				timer.dueTime = dueTime;
				return num == 1;
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x00012F44 File Offset: 0x00011144
			public bool UpdateTimer(IOThreadTimer timer, long dueTime)
			{
				int index = timer.index;
				IOThreadTimer[] array = this.timers;
				int num = this.count;
				int num2 = index / 2;
				if (num2 == 0 || array[num2].dueTime <= dueTime)
				{
					int num3 = index * 2;
					if (num3 > num || array[num3].dueTime >= dueTime)
					{
						int num4 = num3 + 1;
						if (num4 > num || array[num4].dueTime >= dueTime)
						{
							timer.dueTime = dueTime;
							return index == 1;
						}
					}
				}
				this.DeleteTimer(timer);
				this.InsertTimer(timer, dueTime);
				return true;
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x00012FC4 File Offset: 0x000111C4
			private void DeleteMinTimerCore()
			{
				int num = this.count;
				if (num == 1)
				{
					this.count = 0;
					this.timers[1] = null;
					return;
				}
				IOThreadTimer[] array = this.timers;
				IOThreadTimer iothreadTimer = array[num];
				num = (this.count = num - 1);
				int num2 = 1;
				int num3;
				do
				{
					num3 = num2 * 2;
					if (num3 > num)
					{
						break;
					}
					IOThreadTimer iothreadTimer4;
					int num5;
					if (num3 < num)
					{
						IOThreadTimer iothreadTimer2 = array[num3];
						int num4 = num3 + 1;
						IOThreadTimer iothreadTimer3 = array[num4];
						if (iothreadTimer3.dueTime < iothreadTimer2.dueTime)
						{
							iothreadTimer4 = iothreadTimer3;
							num5 = num4;
						}
						else
						{
							iothreadTimer4 = iothreadTimer2;
							num5 = num3;
						}
					}
					else
					{
						num5 = num3;
						iothreadTimer4 = array[num5];
					}
					if (iothreadTimer.dueTime <= iothreadTimer4.dueTime)
					{
						break;
					}
					array[num2] = iothreadTimer4;
					iothreadTimer4.index = num2;
					num2 = num5;
				}
				while (num3 < num);
				array[num2] = iothreadTimer;
				iothreadTimer.index = num2;
				array[num + 1] = null;
			}

			// Token: 0x04000270 RID: 624
			private int count;

			// Token: 0x04000271 RID: 625
			private IOThreadTimer[] timers;
		}

		// Token: 0x0200007D RID: 125
		private class WaitableTimer : WaitHandle
		{
			// Token: 0x06000401 RID: 1025 RVA: 0x00013089 File Offset: 0x00011289
			[SecuritySafeCritical]
			public WaitableTimer()
			{
				base.SafeWaitHandle = IOThreadTimer.WaitableTimer.TimerHelper.CreateWaitableTimer();
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001309C File Offset: 0x0001129C
			public long DueTime
			{
				get
				{
					return this.dueTime;
				}
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x000130A4 File Offset: 0x000112A4
			[SecuritySafeCritical]
			public void Set(long dueTime)
			{
				this.dueTime = IOThreadTimer.WaitableTimer.TimerHelper.Set(base.SafeWaitHandle, dueTime);
			}

			// Token: 0x04000272 RID: 626
			private long dueTime;

			// Token: 0x020000B3 RID: 179
			[SecurityCritical]
			private static class TimerHelper
			{
				// Token: 0x060004C3 RID: 1219 RVA: 0x00014558 File Offset: 0x00012758
				public static SafeWaitHandle CreateWaitableTimer()
				{
					SafeWaitHandle safeWaitHandle = UnsafeNativeMethods.CreateWaitableTimer(IntPtr.Zero, false, null);
					if (safeWaitHandle.IsInvalid)
					{
						Exception exception = new Win32Exception();
						safeWaitHandle.SetHandleAsInvalid();
						throw Fx.Exception.AsError(exception);
					}
					return safeWaitHandle;
				}

				// Token: 0x060004C4 RID: 1220 RVA: 0x00014593 File Offset: 0x00012793
				public static long Set(SafeWaitHandle timer, long dueTime)
				{
					if (!UnsafeNativeMethods.SetWaitableTimer(timer, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, false))
					{
						throw Fx.Exception.AsError(new Win32Exception());
					}
					return dueTime;
				}
			}
		}
	}
}

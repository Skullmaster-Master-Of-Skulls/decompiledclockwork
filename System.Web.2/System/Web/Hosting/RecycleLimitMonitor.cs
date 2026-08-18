using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200078A RID: 1930
	public class RecycleLimitMonitor : MarshalByRefObject
	{
		// Token: 0x06005C6A RID: 23658 RVA: 0x0013FC11 File Offset: 0x0013DE11
		internal RecycleLimitMonitor()
		{
			this._observers = new List<IObserver<RecycleLimitInfo>>();
			this.GetSingleton();
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x0013FC2C File Offset: 0x0013DE2C
		internal void Start()
		{
			if (RecycleLimitMonitor._defaultDomainSingleton != null && !this._isStarted)
			{
				lock (this)
				{
					if (!this._isStarted)
					{
						RecycleLimitMonitor._defaultDomainSingleton.RegisterProxyAndStart(this, HostingEnvironment.ApplicationID);
						this._isStarted = true;
					}
				}
			}
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x0013FC90 File Offset: 0x0013DE90
		internal void Stop()
		{
			if (RecycleLimitMonitor._defaultDomainSingleton != null && this._isStarted)
			{
				lock (this)
				{
					if (this._isStarted)
					{
						this._isStarted = false;
						RecycleLimitMonitor._defaultDomainSingleton.UnregisterProxyAndStop(this);
					}
				}
			}
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x0013FCF0 File Offset: 0x0013DEF0
		public void Dispose()
		{
			this.Stop();
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x0013FCF8 File Offset: 0x0013DEF8
		internal void Subscribe(IObserver<RecycleLimitInfo> observer)
		{
			if (this._observers != null && observer != null)
			{
				List<IObserver<RecycleLimitInfo>> observers = this._observers;
				lock (observers)
				{
					if (this._observers != null && observer != null)
					{
						this._observers.Add(observer);
					}
				}
			}
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x0013FD54 File Offset: 0x0013DF54
		internal void Unsubscribe(IObserver<RecycleLimitInfo> observer)
		{
			if (this._observers != null && observer != null)
			{
				List<IObserver<RecycleLimitInfo>> observers = this._observers;
				lock (observers)
				{
					if (this._observers != null && observer != null)
					{
						this._observers.Remove(observer);
					}
				}
			}
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x0013FDB4 File Offset: 0x0013DFB4
		internal bool RaiseRecycleLimitEvent(long current, long limit, RecycleLimitNotificationFrequency frequency)
		{
			RecycleLimitInfo recycleLimitInfo = new RecycleLimitInfo(current, limit, frequency);
			if (this._isStarted)
			{
				List<IObserver<RecycleLimitInfo>> observers = this._observers;
				IObserver<RecycleLimitInfo>[] array;
				lock (observers)
				{
					array = this._observers.ToArray();
				}
				foreach (IObserver<RecycleLimitInfo> observer in array)
				{
					try
					{
						observer.OnNext(recycleLimitInfo);
					}
					catch (Exception e)
					{
						Misc.ReportUnhandledException(e, new string[]
						{
							SR.GetString("Unhandled_Monitor_Exception", new object[]
							{
								"RaiseRecycleLimitEvent",
								"RecycleLimitMonitor"
							})
						});
					}
				}
				return recycleLimitInfo.RequestGC;
			}
			return false;
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0013FE80 File Offset: 0x0013E080
		private void GetSingleton()
		{
			ApplicationManager applicationManager = HostingEnvironment.GetApplicationManager();
			if (RecycleLimitMonitor._defaultDomainSingleton == null && applicationManager != null && !AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				object singletonLock = RecycleLimitMonitor._singletonLock;
				lock (singletonLock)
				{
					if (RecycleLimitMonitor._defaultDomainSingleton == null)
					{
						AppDomain defaultAppDomain = applicationManager.GetDefaultAppDomain();
						defaultAppDomain.SetData(RecycleLimitMonitor._pbLimit, AspNetMemoryMonitor.ProcessPrivateBytesLimit);
						defaultAppDomain.DoCallBack(new CrossAppDomainDelegate(RecycleLimitMonitor.RecycleLimitMonitorSingleton.EnsureCreated));
						RecycleLimitMonitor._defaultDomainSingleton = (RecycleLimitMonitor.RecycleLimitMonitorSingleton)defaultAppDomain.GetData(RecycleLimitMonitor._name);
					}
				}
			}
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x040030B0 RID: 12464
		private static readonly string _name = "System.Web.Hosting.RecycleLimitMonitor.RecycleLimitMonitorSingleton";

		// Token: 0x040030B1 RID: 12465
		private static readonly string _pbLimit = RecycleLimitMonitor._name + "+pbLimit";

		// Token: 0x040030B2 RID: 12466
		private static object _singletonLock = new object();

		// Token: 0x040030B3 RID: 12467
		private static RecycleLimitMonitor.RecycleLimitMonitorSingleton _defaultDomainSingleton = null;

		// Token: 0x040030B4 RID: 12468
		private List<IObserver<RecycleLimitInfo>> _observers;

		// Token: 0x040030B5 RID: 12469
		private bool _isStarted;

		// Token: 0x02000A4F RID: 2639
		public class RecycleLimitMonitorSingleton : MarshalByRefObject
		{
			// Token: 0x06006EAF RID: 28335 RVA: 0x0018A434 File Offset: 0x00188634
			public static void EnsureCreated()
			{
				if (AppDomain.CurrentDomain.IsDefaultAppDomain() && RecycleLimitMonitor.RecycleLimitMonitorSingleton._singleton == null)
				{
					object singletonLock = RecycleLimitMonitor.RecycleLimitMonitorSingleton._singletonLock;
					lock (singletonLock)
					{
						if (RecycleLimitMonitor.RecycleLimitMonitorSingleton._singleton == null)
						{
							object data = AppDomain.CurrentDomain.GetData(RecycleLimitMonitor._pbLimit);
							if (data != null)
							{
								RecycleLimitMonitor.RecycleLimitMonitorSingleton._singleton = new RecycleLimitMonitor.RecycleLimitMonitorSingleton((long)data);
								AppDomain.CurrentDomain.SetData(RecycleLimitMonitor._name, RecycleLimitMonitor.RecycleLimitMonitorSingleton._singleton);
							}
						}
					}
				}
			}

			// Token: 0x06006EB0 RID: 28336 RVA: 0x0018A4C0 File Offset: 0x001886C0
			private RecycleLimitMonitorSingleton()
			{
			}

			// Token: 0x06006EB1 RID: 28337 RVA: 0x0018A4FC File Offset: 0x001886FC
			internal RecycleLimitMonitorSingleton(long privateBytesLimit)
			{
				if (privateBytesLimit <= 0L)
				{
					return;
				}
				this._limit = privateBytesLimit;
				this._appManager = ApplicationManager.GetApplicationManager();
				this._pid = (uint)SafeNativeMethods.GetCurrentProcessId();
				this._proxyMonitors = new Dictionary<RecycleLimitMonitor, string>();
				this._minMaxDelta = 2097152L * (long)SystemInfo.GetNumProcessCPUs();
				this.AdjustMaxDeltaAndPressureMarks(this._minMaxDelta);
				this._samples = new long[2];
				this._sampleTimes = new DateTime[2];
				this._useGetProcessMemoryInfo = (VersionInfo.ExeName == "w3wp");
				this._deltaSamples = new long[10];
				this._timer = new Timer(new TimerCallback(this.PBytesMonitorThread), null, -1, this._currentPollInterval);
			}

			// Token: 0x06006EB2 RID: 28338 RVA: 0x0018A5EC File Offset: 0x001887EC
			public void RegisterProxyAndStart(RecycleLimitMonitor proxy, string applicationID)
			{
				if (proxy != null && !string.IsNullOrWhiteSpace(applicationID))
				{
					lock (this)
					{
						this._proxyMonitors.Add(proxy, applicationID);
						this.StartTimer();
					}
				}
			}

			// Token: 0x06006EB3 RID: 28339 RVA: 0x0018A640 File Offset: 0x00188840
			public void UnregisterProxyAndStop(RecycleLimitMonitor proxy)
			{
				if (proxy != null)
				{
					lock (this)
					{
						this._proxyMonitors.Remove(proxy);
						if (this._proxyMonitors.Count == 0)
						{
							this.StopTimer();
						}
					}
				}
			}

			// Token: 0x06006EB4 RID: 28340 RVA: 0x0018A698 File Offset: 0x00188898
			public void Dispose()
			{
				this._disposed = true;
				this.DisposeTimer();
				GC.SuppressFinalize(this);
			}

			// Token: 0x06006EB5 RID: 28341 RVA: 0x0018A6B0 File Offset: 0x001888B0
			private void StartTimer()
			{
				object timerLock = this._timerLock;
				lock (timerLock)
				{
					if (this._timer != null)
					{
						this._timer.Change(this._currentPollInterval, this._currentPollInterval);
					}
				}
			}

			// Token: 0x06006EB6 RID: 28342 RVA: 0x0018A70C File Offset: 0x0018890C
			private void StopTimer()
			{
				object timerLock = this._timerLock;
				lock (timerLock)
				{
					if (this._timer != null)
					{
						this._timer.Change(-1, -1);
					}
				}
			}

			// Token: 0x06006EB7 RID: 28343 RVA: 0x0018A75C File Offset: 0x0018895C
			private void DisposeTimer()
			{
				object timerLock = this._timerLock;
				lock (timerLock)
				{
					if (this._timer != null)
					{
						this._timer.Dispose();
						this._timer = null;
					}
				}
			}

			// Token: 0x06006EB8 RID: 28344 RVA: 0x0018A7B0 File Offset: 0x001889B0
			private void PBytesMonitorThread(object state)
			{
				if (Interlocked.Exchange(ref this._inPBytesMonitorThread, 1) != 0)
				{
					return;
				}
				try
				{
					if (!this._disposed)
					{
						long num = this.NextSample();
						this.Adjust();
						if (num > this._highPressureMark)
						{
							this.CollectInfrequently(num);
						}
					}
				}
				finally
				{
					Interlocked.Exchange(ref this._inPBytesMonitorThread, 0);
				}
			}

			// Token: 0x06006EB9 RID: 28345 RVA: 0x0018A814 File Offset: 0x00188A14
			private long NextSample()
			{
				long num2;
				if (this._useGetProcessMemoryInfo)
				{
					long num;
					UnsafeNativeMethods.GetPrivateBytesIIS6(out num, true);
					num2 = num;
				}
				else
				{
					uint num3 = 0U;
					uint num4;
					UnsafeNativeMethods.GetProcessMemoryInformation(this._pid, out num3, out num4, true);
					num2 = (long)((long)((ulong)num3) << 20);
				}
				this._idx ^= 1;
				this._sampleTimes[this._idx] = DateTime.UtcNow;
				this._samples[this._idx] = num2;
				return num2;
			}

			// Token: 0x06006EBA RID: 28346 RVA: 0x0018A884 File Offset: 0x00188A84
			private void CollectInfrequently(long privateBytes)
			{
				long ticks = DateTime.UtcNow.Subtract(this._inducedGCFinishTime).Ticks;
				bool flag = ticks > this._inducedGCMinInterval;
				RecycleLimitNotificationFrequency frequency = RecycleLimitNotificationFrequency.Medium;
				if (flag || this._howFrequent < 5)
				{
					if (!flag)
					{
						this._howFrequent = Math.Min(5, this._howFrequent + 1);
						frequency = RecycleLimitNotificationFrequency.High;
					}
					else if (this._howFrequent > 1 && ticks > 2L * this._inducedGCMinInterval)
					{
						this._howFrequent = Math.Max(1, this._howFrequent - 1);
						frequency = RecycleLimitNotificationFrequency.Low;
					}
					Stopwatch stopwatch = Stopwatch.StartNew();
					bool flag2 = this.AlertProxyMonitors(privateBytes, this._limit, frequency);
					stopwatch.Stop();
					if (!flag2 || this._appManager.ShutdownInProgress)
					{
						return;
					}
					Stopwatch stopwatch2 = Stopwatch.StartNew();
					GC.Collect();
					stopwatch2.Stop();
					this._inducedGCCount++;
					this._inducedGCFinishTime = DateTime.UtcNow;
					this._inducedGCDurationTicks = stopwatch2.Elapsed.Ticks;
					this._inducedGCPostPrivateBytes = this.NextSample();
					this._inducedGCPrivateBytesChange = privateBytes - this._inducedGCPostPrivateBytes;
					this._inducedGCMinInterval = Math.Max(this._inducedGCDurationTicks * 1000L / 33L, 50000000L);
					if (this._inducedGCPrivateBytesChange * 100L <= privateBytes)
					{
						this._inducedGCMinInterval = Math.Max(this._inducedGCMinInterval, 600000000L);
					}
				}
			}

			// Token: 0x06006EBB RID: 28347 RVA: 0x0018A9E4 File Offset: 0x00188BE4
			private bool AlertProxyMonitors(long current, long limit, RecycleLimitNotificationFrequency frequency)
			{
				bool flag = false;
				KeyValuePair<RecycleLimitMonitor, string>[] array = null;
				lock (this)
				{
					if (this._proxyMonitors.Count == 0)
					{
						this.StopTimer();
						return flag;
					}
					array = this._proxyMonitors.ToArray<KeyValuePair<RecycleLimitMonitor, string>>();
				}
				foreach (KeyValuePair<RecycleLimitMonitor, string> keyValuePair in array)
				{
					try
					{
						LockableAppDomainContext lockableAppDomainContext = this._appManager.GetLockableAppDomainContext(keyValuePair.Value);
						if (lockableAppDomainContext != null)
						{
							LockableAppDomainContext obj = lockableAppDomainContext;
							lock (obj)
							{
								flag |= keyValuePair.Key.RaiseRecycleLimitEvent(current, limit, frequency);
							}
						}
					}
					catch (Exception e)
					{
						Misc.ReportUnhandledException(e, new string[]
						{
							SR.GetString("Unhandled_Monitor_Exception", new object[]
							{
								"RaiseRecycleLimitEvent",
								"RecycleLimitMonitor"
							})
						});
					}
				}
				return flag;
			}

			// Token: 0x06006EBC RID: 28348 RVA: 0x0018AB08 File Offset: 0x00188D08
			private void Adjust()
			{
				long num = this._samples[this._idx];
				long num2 = this._samples[this._idx ^ 1];
				if (num > num2 && num2 > 0L)
				{
					DateTime dateTime = this._sampleTimes[this._idx];
					DateTime value = this._sampleTimes[this._idx ^ 1];
					long num3 = num - num2;
					long num4 = (long)Math.Round(dateTime.Subtract(value).TotalSeconds);
					if (num4 > 0L)
					{
						long num5 = num3 / num4;
						this._deltaSamples[this._idxDeltaSamples] = num5;
						this._idxDeltaSamples = (this._idxDeltaSamples + 1) % 10;
						this.AdjustMaxDeltaAndPressureMarks(num5);
					}
				}
				object timerLock = this._timerLock;
				lock (timerLock)
				{
					if (this._timer != null)
					{
						if (num > this._mediumPressureMark)
						{
							if (this._currentPollInterval > 5000)
							{
								this._currentPollInterval = 5000;
								this._timer.Change(this._currentPollInterval, this._currentPollInterval);
							}
						}
						else if (num > this._lowPressureMark)
						{
							if (this._currentPollInterval > 30000)
							{
								this._currentPollInterval = 30000;
								this._timer.Change(this._currentPollInterval, this._currentPollInterval);
							}
						}
						else if (this._currentPollInterval != 120000)
						{
							this._currentPollInterval = 120000;
							this._timer.Change(this._currentPollInterval, this._currentPollInterval);
						}
					}
				}
			}

			// Token: 0x06006EBD RID: 28349 RVA: 0x0018ACA0 File Offset: 0x00188EA0
			private void AdjustMaxDeltaAndPressureMarks(long delta)
			{
				long num = this._maxDelta;
				if (delta > num)
				{
					num = delta;
				}
				else
				{
					bool flag = true;
					long num2 = this._maxDelta / 4L;
					foreach (long num3 in this._deltaSamples)
					{
						if (num3 > num2)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						num = num2 * 2L;
					}
				}
				num = Math.Max(num, this._minMaxDelta);
				if (this._maxDelta != num)
				{
					this._maxDelta = num;
					this._highPressureMark = Math.Max(this._limit * 9L / 10L, this._limit - this._maxDelta * 2L * 5L);
					this._lowPressureMark = Math.Max(this._limit * 6L / 10L, this._limit - this._maxDelta * 2L * 120L);
					this._mediumPressureMark = Math.Max((this._highPressureMark + this._lowPressureMark) / 2L, this._limit - this._maxDelta * 2L * 30L);
					this._mediumPressureMark = Math.Min(this._highPressureMark, this._mediumPressureMark);
				}
			}

			// Token: 0x06006EBE RID: 28350 RVA: 0x0000298D File Offset: 0x00000B8D
			public override object InitializeLifetimeService()
			{
				return null;
			}

			// Token: 0x04003B3F RID: 15167
			private const int SAMPLE_COUNT = 2;

			// Token: 0x04003B40 RID: 15168
			private const int DELTA_SAMPLE_COUNT = 10;

			// Token: 0x04003B41 RID: 15169
			private const int HIGH_FREQ_INTERVAL_S = 5;

			// Token: 0x04003B42 RID: 15170
			private const int HIGH_FREQ_INTERVAL_MS = 5000;

			// Token: 0x04003B43 RID: 15171
			private const int MEDIUM_FREQ_INTERVAL_S = 30;

			// Token: 0x04003B44 RID: 15172
			private const int MEDIUM_FREQ_INTERVAL_MS = 30000;

			// Token: 0x04003B45 RID: 15173
			private const int LOW_FREQ_INTERVAL_S = 120;

			// Token: 0x04003B46 RID: 15174
			private const int LOW_FREQ_INTERVAL_MS = 120000;

			// Token: 0x04003B47 RID: 15175
			private const int MEGABYTE_SHIFT = 20;

			// Token: 0x04003B48 RID: 15176
			private static RecycleLimitMonitor.RecycleLimitMonitorSingleton _singleton;

			// Token: 0x04003B49 RID: 15177
			private static object _singletonLock = new object();

			// Token: 0x04003B4A RID: 15178
			private int _currentPollInterval = 30000;

			// Token: 0x04003B4B RID: 15179
			private int _inPBytesMonitorThread;

			// Token: 0x04003B4C RID: 15180
			private bool _useGetProcessMemoryInfo;

			// Token: 0x04003B4D RID: 15181
			private uint _pid;

			// Token: 0x04003B4E RID: 15182
			private bool _disposed;

			// Token: 0x04003B4F RID: 15183
			private Timer _timer;

			// Token: 0x04003B50 RID: 15184
			private object _timerLock = new object();

			// Token: 0x04003B51 RID: 15185
			private ApplicationManager _appManager;

			// Token: 0x04003B52 RID: 15186
			private Dictionary<RecycleLimitMonitor, string> _proxyMonitors;

			// Token: 0x04003B53 RID: 15187
			private long _limit;

			// Token: 0x04003B54 RID: 15188
			private long _highPressureMark;

			// Token: 0x04003B55 RID: 15189
			private long _mediumPressureMark;

			// Token: 0x04003B56 RID: 15190
			private long _lowPressureMark;

			// Token: 0x04003B57 RID: 15191
			private long[] _deltaSamples;

			// Token: 0x04003B58 RID: 15192
			private int _idxDeltaSamples;

			// Token: 0x04003B59 RID: 15193
			private long _maxDelta;

			// Token: 0x04003B5A RID: 15194
			private long _minMaxDelta;

			// Token: 0x04003B5B RID: 15195
			private long[] _samples;

			// Token: 0x04003B5C RID: 15196
			private DateTime[] _sampleTimes;

			// Token: 0x04003B5D RID: 15197
			private int _idx;

			// Token: 0x04003B5E RID: 15198
			private int _howFrequent = 1;

			// Token: 0x04003B5F RID: 15199
			private long _inducedGCMinInterval = 50000000L;

			// Token: 0x04003B60 RID: 15200
			private DateTime _inducedGCFinishTime = DateTime.MinValue;

			// Token: 0x04003B61 RID: 15201
			private long _inducedGCDurationTicks;

			// Token: 0x04003B62 RID: 15202
			private int _inducedGCCount;

			// Token: 0x04003B63 RID: 15203
			private long _inducedGCPostPrivateBytes;

			// Token: 0x04003B64 RID: 15204
			private long _inducedGCPrivateBytesChange;
		}
	}
}

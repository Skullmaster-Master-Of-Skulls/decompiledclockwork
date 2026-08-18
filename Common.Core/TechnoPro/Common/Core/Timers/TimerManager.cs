using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using ClockWorkLogger;
using TechnoPro.Common.ICore.Timers;
using TechnoPro.Common.Public.Entities.Timers;

namespace TechnoPro.Common.Core.Timers
{
	// Token: 0x02000033 RID: 51
	public class TimerManager : ITimerManager, IDisposable
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000B321 File Offset: 0x00009521
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000B329 File Offset: 0x00009529
		private IDictionary<string, System.Timers.Timer> Timers { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000B332 File Offset: 0x00009532
		public static ITimerManager Current
		{
			get
			{
				return TimerManager._instance;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B339 File Offset: 0x00009539
		protected TimerManager()
		{
			this.Timers = new Dictionary<string, System.Timers.Timer>();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B364 File Offset: 0x00009564
		public void AddTimer(ClockWorkServerTimer timer)
		{
			System.Timers.Timer timer2 = new System.Timers.Timer
			{
				Interval = timer.TimeInterval
			};
			timer2.Elapsed += timer.TimeElapsedFunc.Invoke;
			timer2.Enabled = timer.Enabled;
			bool flag = !this.Timers.ContainsKey(timer.Name);
			if (flag)
			{
				this._itemsLock.EnterUpgradeableReadLock();
				try
				{
					bool flag2 = !this.Timers.ContainsKey(timer.Name);
					if (flag2)
					{
						this._itemsLock.EnterWriteLock();
						try
						{
							this.Timers.Add(timer.Name, timer2);
						}
						finally
						{
							this._itemsLock.ExitWriteLock();
						}
					}
				}
				finally
				{
					this._itemsLock.ExitUpgradeableReadLock();
				}
				CWLogger.Logger.Debug("TimerManager::AddTimer:: Timer '{0}' was successfully added.", timer.Name);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B464 File Offset: 0x00009664
		public void RemoveTimer(string timerName)
		{
			bool flag = this.Timers.ContainsKey(timerName);
			if (flag)
			{
				this._itemsLock.EnterUpgradeableReadLock();
				try
				{
					bool flag2 = this.Timers.ContainsKey(timerName);
					if (flag2)
					{
						this._itemsLock.EnterWriteLock();
						try
						{
							System.Timers.Timer timer = this.Timers[timerName];
							timer.Enabled = false;
							timer.Dispose();
							this.Timers.Remove(timerName);
						}
						finally
						{
							this._itemsLock.ExitWriteLock();
						}
					}
				}
				finally
				{
					this._itemsLock.ExitUpgradeableReadLock();
				}
				CWLogger.Logger.Debug("TimerManager::RemoveTimer:: Timer '{0}' was successfully removed.", timerName);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B530 File Offset: 0x00009730
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B544 File Offset: 0x00009744
		private void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					foreach (System.Timers.Timer timer in this.Timers.Values)
					{
						timer.Dispose();
					}
					CWLogger.Logger.Debug("TimerManager::Dispose:: All timers have been disposed");
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000068 RID: 104
		private readonly ReaderWriterLockSlim _itemsLock = new ReaderWriterLockSlim();

		// Token: 0x04000069 RID: 105
		private static readonly ITimerManager _instance = new TimerManager();

		// Token: 0x0400006A RID: 106
		protected bool disposed = false;
	}
}

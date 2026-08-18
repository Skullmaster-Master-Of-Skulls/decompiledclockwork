using System;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Management
{
	// Token: 0x0200019E RID: 414
	public sealed class RuleFiringRecord
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00043612 File Offset: 0x00041812
		public DateTime LastFired
		{
			get
			{
				return this._lastFired;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0004361A File Offset: 0x0004181A
		public int TimesRaised
		{
			get
			{
				return this._timesRaised;
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00043622 File Offset: 0x00041822
		internal RuleFiringRecord(HealthMonitoringSectionHelper.RuleInfo ruleInfo)
		{
			this._ruleInfo = ruleInfo;
			this._lastFired = DateTime.MinValue;
			this._timesRaised = 0;
			this._updatingLastFired = 0;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0004364C File Offset: 0x0004184C
		private void UpdateLastFired(DateTime now, bool alreadyLocked)
		{
			TimeSpan t = now - this._lastFired;
			if (t < RuleFiringRecord.TS_ONE_SECOND)
			{
				return;
			}
			if (!alreadyLocked)
			{
				if (Interlocked.CompareExchange(ref this._updatingLastFired, 1, 0) != 0)
				{
					return;
				}
				try
				{
					this._lastFired = now;
					return;
				}
				finally
				{
					Interlocked.Exchange(ref this._updatingLastFired, 0);
				}
			}
			this._lastFired = now;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000436B4 File Offset: 0x000418B4
		internal bool CheckAndUpdate(WebBaseEvent eventRaised)
		{
			DateTime now = DateTime.Now;
			HealthMonitoringManager healthMonitoringManager = HealthMonitoringManager.Manager();
			int num = Interlocked.Increment(ref this._timesRaised);
			if (healthMonitoringManager == null)
			{
				return false;
			}
			if (this._ruleInfo._customEvaluatorType != null)
			{
				IWebEventCustomEvaluator webEventCustomEvaluator = (IWebEventCustomEvaluator)healthMonitoringManager._sectionHelper._customEvaluatorInstances[this._ruleInfo._customEvaluatorType];
				try
				{
					eventRaised.PreProcessEventInit();
					if (!webEventCustomEvaluator.CanFire(eventRaised, this))
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
			}
			if (num < this._ruleInfo._minInstances)
			{
				return false;
			}
			if (num > this._ruleInfo._maxLimit)
			{
				return false;
			}
			if (this._ruleInfo._minInterval == TimeSpan.Zero)
			{
				this.UpdateLastFired(now, false);
				return true;
			}
			if (now - this._lastFired <= this._ruleInfo._minInterval)
			{
				return false;
			}
			bool result;
			lock (this)
			{
				if (now - this._lastFired <= this._ruleInfo._minInterval)
				{
					result = false;
				}
				else
				{
					this.UpdateLastFired(now, true);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04001668 RID: 5736
		internal DateTime _lastFired;

		// Token: 0x04001669 RID: 5737
		internal int _timesRaised;

		// Token: 0x0400166A RID: 5738
		internal int _updatingLastFired;

		// Token: 0x0400166B RID: 5739
		private static TimeSpan TS_ONE_SECOND = new TimeSpan(0, 0, 1);

		// Token: 0x0400166C RID: 5740
		internal HealthMonitoringSectionHelper.RuleInfo _ruleInfo;
	}
}

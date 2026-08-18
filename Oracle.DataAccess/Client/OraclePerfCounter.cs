using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000091 RID: 145
	internal sealed class OraclePerfCounter : IDisposable
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x00045C8B File Offset: 0x00044C8B
		public void Dispose()
		{
			if (this.m_counter != null)
			{
				this.m_counter.RemoveInstance();
				this.m_counter.Dispose();
				this.m_counter = null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00045CB2 File Offset: 0x00044CB2
		internal CounterCreationData CreationData
		{
			get
			{
				return this.m_counterCreationData;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00045CBA File Offset: 0x00044CBA
		internal long CurrentValue
		{
			get
			{
				if (this.m_counter == null)
				{
					return -1L;
				}
				return this.m_counter.RawValue;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00045CD4 File Offset: 0x00044CD4
		[PerformanceCounterPermission(SecurityAction.Assert, Unrestricted = true)]
		internal OraclePerfCounter(string categoryName, string counterName, string counterHelp, PerformanceCounterType countertype, string instanceName)
		{
			try
			{
				this.m_counter = new PerformanceCounter(categoryName, counterName, instanceName, false);
			}
			catch (Exception ex)
			{
				this.m_counter = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) OraclePerfCounter::OraclePerfCounter() -" + ex.Message + "\n"
					});
				}
			}
			try
			{
				this.m_counterCreationData = new CounterCreationData(counterName, counterHelp, countertype);
			}
			catch (Exception ex2)
			{
				this.m_counterCreationData = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) OraclePerfCounter::OraclePerfCounter() CreationData -" + ex2.Message + "\n"
					});
				}
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00045D94 File Offset: 0x00044D94
		internal long Increment()
		{
			long result;
			try
			{
				result = ((this.m_counter != null) ? this.m_counter.Increment() : -1L);
			}
			catch
			{
				this.m_counter = null;
				result = -1L;
			}
			return result;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00045DDC File Offset: 0x00044DDC
		internal long Decrement()
		{
			long result;
			try
			{
				result = ((this.m_counter != null) ? this.m_counter.Decrement() : -1L);
			}
			catch
			{
				this.m_counter = null;
				result = -1L;
			}
			return result;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00045E24 File Offset: 0x00044E24
		internal long IncrementBy(int val)
		{
			long result;
			try
			{
				result = ((this.m_counter != null) ? this.m_counter.IncrementBy((long)val) : -1L);
			}
			catch
			{
				this.m_counter = null;
				result = -1L;
			}
			return result;
		}

		// Token: 0x04000417 RID: 1047
		private PerformanceCounter m_counter;

		// Token: 0x04000418 RID: 1048
		private CounterCreationData m_counterCreationData;
	}
}

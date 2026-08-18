using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000073 RID: 115
	internal class OraclePerfCounter
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00038134 File Offset: 0x00036334
		[PerformanceCounterPermission(SecurityAction.Assert, Unrestricted = true)]
		internal OraclePerfCounter(string counterName, string perfCounterInstanceName)
		{
			try
			{
				this.m_counter = new PerformanceCounter("ODP.NET, Managed Driver", counterName, perfCounterInstanceName, false);
			}
			catch (Exception)
			{
				this.m_counter = null;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00038178 File Offset: 0x00036378
		[PerformanceCounterPermission(SecurityAction.Assert, Unrestricted = true)]
		internal OraclePerfCounter(string counterName)
		{
			try
			{
				this.m_counter = new PerformanceCounter("ODP.NET, Managed Driver", counterName, OraclePerfParams.m_appDomainPfcInstanceName, false);
			}
			catch (Exception)
			{
				this.m_counter = null;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000381C0 File Offset: 0x000363C0
		public long IncrementBy(int value)
		{
			long result;
			try
			{
				if (this.m_higherLevelCounter != null)
				{
					this.m_higherLevelCounter.IncrementBy(value);
				}
				result = ((this.m_counter != null) ? this.m_counter.IncrementBy((long)value) : -1L);
			}
			catch (Exception)
			{
				this.Dispose();
				result = -1L;
			}
			return result;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0003821C File Offset: 0x0003641C
		public long Increment()
		{
			long result;
			try
			{
				if (this.m_higherLevelCounter != null)
				{
					this.m_higherLevelCounter.Increment();
				}
				result = ((this.m_counter != null) ? this.m_counter.Increment() : -1L);
			}
			catch (Exception)
			{
				this.Dispose();
				result = -1L;
			}
			return result;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00038274 File Offset: 0x00036474
		public long Decrement()
		{
			long result;
			try
			{
				if (this.m_higherLevelCounter != null)
				{
					this.m_higherLevelCounter.Decrement();
				}
				result = ((this.m_counter != null) ? this.m_counter.Decrement() : -1L);
			}
			catch (Exception)
			{
				this.Dispose();
				result = -1L;
			}
			return result;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000382CC File Offset: 0x000364CC
		public void Dispose()
		{
			if (this.m_isDisposed)
			{
				return;
			}
			if (this.m_counter != null)
			{
				this.m_counter.RemoveInstance();
				this.m_counter.Dispose();
				this.m_counter = null;
			}
			this.m_higherLevelCounter = null;
			this.m_isDisposed = true;
		}

		// Token: 0x0400069A RID: 1690
		private bool m_isDisposed;

		// Token: 0x0400069B RID: 1691
		public PerformanceCounter m_counter;

		// Token: 0x0400069C RID: 1692
		public OraclePerfCounter m_higherLevelCounter;
	}
}

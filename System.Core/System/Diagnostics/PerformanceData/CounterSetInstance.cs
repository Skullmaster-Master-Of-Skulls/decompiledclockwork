using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics.PerformanceData
{
	// Token: 0x020002A2 RID: 674
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterSetInstance : IDisposable
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x000593FC File Offset: 0x000575FC
		[SecurityCritical]
		internal CounterSetInstance(CounterSet counterSetDefined, string instanceName)
		{
			if (counterSetDefined == null)
			{
				throw new ArgumentNullException("counterSetDefined");
			}
			if (instanceName == null)
			{
				throw new ArgumentNullException("InstanceName");
			}
			if (instanceName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_EmptyInstanceName"), "InstanceName");
			}
			this.m_counterSet = counterSetDefined;
			this.m_instName = instanceName;
			this.m_nativeInst = UnsafeNativeMethods.PerfCreateInstance(this.m_counterSet.m_provider.m_hProvider, ref this.m_counterSet.m_counterSet, this.m_instName, 0U);
			int num = (this.m_nativeInst != null) ? 0 : Marshal.GetLastWin32Error();
			if (this.m_nativeInst != null)
			{
				this.m_counters = new CounterSetInstanceCounterDataSet(this);
				this.m_active = 1;
				return;
			}
			if (num != 87)
			{
				if (num == 183)
				{
					throw new ArgumentException(SR.GetString("Perflib_Argument_InstanceAlreadyExists", new object[]
					{
						this.m_instName,
						this.m_counterSet.m_counterSet
					}), "InstanceName");
				}
				if (num != 1168)
				{
					throw new Win32Exception(num);
				}
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_CounterSetNotInstalled", new object[]
				{
					this.m_counterSet.m_counterSet
				}));
			}
			else
			{
				if (this.m_counterSet.m_instType == CounterSetInstanceType.Single)
				{
					throw new ArgumentException(SR.GetString("Perflib_Argument_InvalidInstance", new object[]
					{
						this.m_counterSet.m_counterSet
					}), "InstanceName");
				}
				throw new Win32Exception(num);
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0005957B File Offset: 0x0005777B
		[SecurityCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0005958C File Offset: 0x0005778C
		[SecurityCritical]
		~CounterSetInstance()
		{
			this.Dispose(false);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000595BC File Offset: 0x000577BC
		[SecurityCritical]
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_counters != null)
			{
				this.m_counters.Dispose();
				this.m_counters = null;
			}
			if (this.m_nativeInst != null && Interlocked.Exchange(ref this.m_active, 0) != 0 && this.m_nativeInst != null)
			{
				CounterSet counterSet = this.m_counterSet;
				lock (counterSet)
				{
					if (this.m_counterSet.m_provider != null)
					{
						uint num = UnsafeNativeMethods.PerfDeleteInstance(this.m_counterSet.m_provider.m_hProvider, this.m_nativeInst);
					}
					this.m_nativeInst = null;
				}
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00059668 File Offset: 0x00057868
		public CounterSetInstanceCounterDataSet Counters
		{
			get
			{
				return this.m_counters;
			}
		}

		// Token: 0x04000BBC RID: 3004
		internal CounterSet m_counterSet;

		// Token: 0x04000BBD RID: 3005
		internal string m_instName;

		// Token: 0x04000BBE RID: 3006
		private int m_active;

		// Token: 0x04000BBF RID: 3007
		private CounterSetInstanceCounterDataSet m_counters;

		// Token: 0x04000BC0 RID: 3008
		[SecurityCritical]
		internal unsafe UnsafeNativeMethods.PerfCounterSetInstanceStruct* m_nativeInst;
	}
}

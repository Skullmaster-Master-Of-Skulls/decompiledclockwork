using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics.PerformanceData
{
	// Token: 0x020002A0 RID: 672
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterSetInstanceCounterDataSet : IDisposable
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x0005889C File Offset: 0x00056A9C
		[SecurityCritical]
		internal unsafe CounterSetInstanceCounterDataSet(CounterSetInstance thisInst)
		{
			this.m_instance = thisInst;
			this.m_counters = new Dictionary<int, CounterData>();
			if (this.m_instance.m_counterSet.m_provider == null)
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_ProviderNotFound", new object[]
				{
					this.m_instance.m_counterSet.m_providerGuid
				}), "ProviderGuid");
			}
			if (this.m_instance.m_counterSet.m_provider.m_hProvider.IsInvalid)
			{
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_NoActiveProvider", new object[]
				{
					this.m_instance.m_counterSet.m_providerGuid
				}));
			}
			this.m_dataBlock = (byte*)((void*)Marshal.AllocHGlobal(this.m_instance.m_counterSet.m_idToCounter.Count * 8));
			if (this.m_dataBlock == null)
			{
				throw new InsufficientMemoryException(SR.GetString("Perflib_InsufficientMemory_InstanceCounterBlock", new object[]
				{
					this.m_instance.m_counterSet.m_counterSet,
					this.m_instance.m_instName
				}));
			}
			int num = 0;
			foreach (KeyValuePair<int, CounterType> keyValuePair in this.m_instance.m_counterSet.m_idToCounter)
			{
				CounterData value = new CounterData((long*)(this.m_dataBlock + num * 8));
				this.m_counters.Add(keyValuePair.Key, value);
				uint num2 = UnsafeNativeMethods.PerfSetCounterRefValue(this.m_instance.m_counterSet.m_provider.m_hProvider, this.m_instance.m_nativeInst, (uint)keyValuePair.Key, (void*)(this.m_dataBlock + num * 8));
				if (num2 != 0U)
				{
					this.Dispose(true);
					if (num2 == 1168U)
					{
						throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_CounterRefValue", new object[]
						{
							this.m_instance.m_counterSet.m_counterSet,
							keyValuePair.Key,
							this.m_instance.m_instName
						}));
					}
					throw new Win32Exception((int)num2);
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00058AD4 File Offset: 0x00056CD4
		[SecurityCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00058AE4 File Offset: 0x00056CE4
		[SecurityCritical]
		~CounterSetInstanceCounterDataSet()
		{
			this.Dispose(false);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00058B14 File Offset: 0x00056D14
		[SecurityCritical]
		private unsafe void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref this.m_disposed, 1) == 0 && this.m_dataBlock != null)
			{
				Marshal.FreeHGlobal((IntPtr)((void*)this.m_dataBlock));
				this.m_dataBlock = null;
			}
		}

		// Token: 0x17000446 RID: 1094
		public CounterData this[int counterId]
		{
			get
			{
				if (this.m_disposed != 0)
				{
					return null;
				}
				CounterData result;
				try
				{
					result = this.m_counters[counterId];
				}
				catch (KeyNotFoundException)
				{
					result = null;
				}
				catch
				{
					throw;
				}
				return result;
			}
		}

		// Token: 0x17000447 RID: 1095
		public CounterData this[string counterName]
		{
			get
			{
				if (counterName == null)
				{
					throw new ArgumentNullException("CounterName");
				}
				if (counterName.Length == 0)
				{
					throw new ArgumentNullException("CounterName");
				}
				if (this.m_disposed != 0)
				{
					return null;
				}
				CounterData result;
				try
				{
					int key = this.m_instance.m_counterSet.m_stringToId[counterName];
					try
					{
						result = this.m_counters[key];
					}
					catch (KeyNotFoundException)
					{
						result = null;
					}
					catch
					{
						throw;
					}
				}
				catch (KeyNotFoundException)
				{
					result = null;
				}
				catch
				{
					throw;
				}
				return result;
			}
		}

		// Token: 0x04000BAF RID: 2991
		internal CounterSetInstance m_instance;

		// Token: 0x04000BB0 RID: 2992
		private Dictionary<int, CounterData> m_counters;

		// Token: 0x04000BB1 RID: 2993
		private int m_disposed;

		// Token: 0x04000BB2 RID: 2994
		[SecurityCritical]
		internal unsafe byte* m_dataBlock;
	}
}

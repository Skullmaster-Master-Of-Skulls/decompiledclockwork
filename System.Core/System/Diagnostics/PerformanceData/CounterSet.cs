using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics.PerformanceData
{
	// Token: 0x020002A1 RID: 673
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class CounterSet : IDisposable
	{
		// Token: 0x06001876 RID: 6262 RVA: 0x00058C38 File Offset: 0x00056E38
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public CounterSet(Guid providerGuid, Guid counterSetGuid, CounterSetInstanceType instanceType)
		{
			if (CounterSet.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Perflib_PlatformNotSupported"));
			}
			if (!PerfProviderCollection.ValidateCounterSetInstanceType(instanceType))
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_InvalidCounterSetInstanceType", new object[]
				{
					instanceType
				}), "instanceType");
			}
			this.m_providerGuid = providerGuid;
			this.m_counterSet = counterSetGuid;
			this.m_instType = instanceType;
			PerfProviderCollection.RegisterCounterSet(this.m_counterSet);
			this.m_provider = PerfProviderCollection.QueryProvider(this.m_providerGuid);
			this.m_lockObject = new object();
			this.m_stringToId = new Dictionary<string, int>();
			this.m_idToCounter = new Dictionary<int, CounterType>();
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00058CE0 File Offset: 0x00056EE0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00058CF0 File Offset: 0x00056EF0
		~CounterSet()
		{
			this.Dispose(false);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00058D20 File Offset: 0x00056F20
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				PerfProviderCollection.UnregisterCounterSet(this.m_counterSet);
				if (this.m_instanceCreated && this.m_provider != null)
				{
					object lockObject = this.m_lockObject;
					lock (lockObject)
					{
						if (this.m_provider != null)
						{
							Interlocked.Decrement(ref this.m_provider.m_counterSet);
							if (this.m_provider.m_counterSet <= 0)
							{
								PerfProviderCollection.RemoveProvider(this.m_providerGuid);
							}
							this.m_provider = null;
						}
					}
				}
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00058DD4 File Offset: 0x00056FD4
		public void AddCounter(int counterId, CounterType counterType)
		{
			if (this.m_provider == null)
			{
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_NoActiveProvider", new object[]
				{
					this.m_providerGuid
				}));
			}
			if (!PerfProviderCollection.ValidateCounterType(counterType))
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_InvalidCounterType", new object[]
				{
					counterType
				}), "counterType");
			}
			if (this.m_instanceCreated)
			{
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_AddCounterAfterInstance", new object[]
				{
					this.m_counterSet
				}));
			}
			object lockObject = this.m_lockObject;
			lock (lockObject)
			{
				if (this.m_instanceCreated)
				{
					throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_AddCounterAfterInstance", new object[]
					{
						this.m_counterSet
					}));
				}
				if (this.m_idToCounter.ContainsKey(counterId))
				{
					throw new ArgumentException(SR.GetString("Perflib_Argument_CounterAlreadyExists", new object[]
					{
						counterId,
						this.m_counterSet
					}), "CounterId");
				}
				this.m_idToCounter.Add(counterId, counterType);
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00058F0C File Offset: 0x0005710C
		public void AddCounter(int counterId, CounterType counterType, string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("CounterName");
			}
			if (counterName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_EmptyCounterName"), "counterName");
			}
			if (!PerfProviderCollection.ValidateCounterType(counterType))
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_InvalidCounterType", new object[]
				{
					counterType
				}), "counterType");
			}
			if (this.m_provider == null)
			{
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_NoActiveProvider", new object[]
				{
					this.m_providerGuid
				}));
			}
			if (this.m_instanceCreated)
			{
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_AddCounterAfterInstance", new object[]
				{
					this.m_counterSet
				}));
			}
			object lockObject = this.m_lockObject;
			lock (lockObject)
			{
				if (this.m_instanceCreated)
				{
					throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_AddCounterAfterInstance", new object[]
					{
						this.m_counterSet
					}));
				}
				if (this.m_stringToId.ContainsKey(counterName))
				{
					throw new ArgumentException(SR.GetString("Perflib_Argument_CounterNameAlreadyExists", new object[]
					{
						counterName,
						this.m_counterSet
					}), "CounterName");
				}
				if (this.m_idToCounter.ContainsKey(counterId))
				{
					throw new ArgumentException(SR.GetString("Perflib_Argument_CounterAlreadyExists", new object[]
					{
						counterId,
						this.m_counterSet
					}), "CounterId");
				}
				this.m_stringToId.Add(counterName, counterId);
				this.m_idToCounter.Add(counterId, counterType);
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000590B8 File Offset: 0x000572B8
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe CounterSetInstance CreateCounterSetInstance(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (instanceName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Perflib_Argument_EmptyInstanceName"), "instanceName");
			}
			if (this.m_provider == null)
			{
				throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_NoActiveProvider", new object[]
				{
					this.m_providerGuid
				}));
			}
			if (!this.m_instanceCreated)
			{
				object lockObject = this.m_lockObject;
				lock (lockObject)
				{
					if (!this.m_instanceCreated)
					{
						if (this.m_provider == null)
						{
							throw new ArgumentException(SR.GetString("Perflib_Argument_ProviderNotFound", new object[]
							{
								this.m_providerGuid
							}), "ProviderGuid");
						}
						if (this.m_provider.m_hProvider.IsInvalid)
						{
							throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_NoActiveProvider", new object[]
							{
								this.m_providerGuid
							}));
						}
						if (this.m_idToCounter.Count == 0)
						{
							throw new InvalidOperationException(SR.GetString("Perflib_InvalidOperation_CounterSetContainsNoCounter", new object[]
							{
								this.m_counterSet
							}));
						}
						uint num = (uint)(sizeof(UnsafeNativeMethods.PerfCounterSetInfoStruct) + this.m_idToCounter.Count * sizeof(UnsafeNativeMethods.PerfCounterInfoStruct));
						byte* ptr = stackalloc byte[(UIntPtr)num];
						if (ptr == null)
						{
							throw new InsufficientMemoryException(SR.GetString("Perflib_InsufficientMemory_CounterSetTemplate", new object[]
							{
								this.m_counterSet,
								num
							}));
						}
						uint num2 = 0U;
						uint num3 = 0U;
						UnsafeNativeMethods.PerfCounterSetInfoStruct* ptr2 = (UnsafeNativeMethods.PerfCounterSetInfoStruct*)ptr;
						ptr2->CounterSetGuid = this.m_counterSet;
						ptr2->ProviderGuid = this.m_providerGuid;
						ptr2->NumCounters = (uint)this.m_idToCounter.Count;
						ptr2->InstanceType = (uint)this.m_instType;
						foreach (KeyValuePair<int, CounterType> keyValuePair in this.m_idToCounter)
						{
							uint num4 = (uint)(sizeof(UnsafeNativeMethods.PerfCounterSetInfoStruct) + (int)(num2 * (uint)sizeof(UnsafeNativeMethods.PerfCounterInfoStruct)));
							if (num4 < num)
							{
								UnsafeNativeMethods.PerfCounterInfoStruct* ptr3 = (UnsafeNativeMethods.PerfCounterInfoStruct*)(ptr + num4);
								ptr3->CounterId = (uint)keyValuePair.Key;
								ptr3->CounterType = (uint)keyValuePair.Value;
								ptr3->Attrib = 1L;
								ptr3->Size = (uint)sizeof(void*);
								ptr3->DetailLevel = 100U;
								ptr3->Scale = 0U;
								ptr3->Offset = num3;
								num3 += ptr3->Size;
							}
							num2 += 1U;
						}
						uint num5 = UnsafeNativeMethods.PerfSetCounterSetInfo(this.m_provider.m_hProvider, ptr2, num);
						if (num5 != 0U)
						{
							if (num5 == 183U)
							{
								throw new ArgumentException(SR.GetString("Perflib_Argument_CounterSetAlreadyRegister", new object[]
								{
									this.m_counterSet
								}), "CounterSetGuid");
							}
							throw new Win32Exception((int)num5);
						}
						else
						{
							Interlocked.Increment(ref this.m_provider.m_counterSet);
							this.m_instanceCreated = true;
						}
					}
				}
			}
			return new CounterSetInstance(this, instanceName);
		}

		// Token: 0x04000BB3 RID: 2995
		private static readonly bool s_platformNotSupported = Environment.OSVersion.Version.Major < 6;

		// Token: 0x04000BB4 RID: 2996
		internal PerfProvider m_provider;

		// Token: 0x04000BB5 RID: 2997
		internal Guid m_providerGuid;

		// Token: 0x04000BB6 RID: 2998
		internal Guid m_counterSet;

		// Token: 0x04000BB7 RID: 2999
		internal CounterSetInstanceType m_instType;

		// Token: 0x04000BB8 RID: 3000
		private readonly object m_lockObject;

		// Token: 0x04000BB9 RID: 3001
		private bool m_instanceCreated;

		// Token: 0x04000BBA RID: 3002
		internal Dictionary<string, int> m_stringToId;

		// Token: 0x04000BBB RID: 3003
		internal Dictionary<int, CounterType> m_idToCounter;
	}
}

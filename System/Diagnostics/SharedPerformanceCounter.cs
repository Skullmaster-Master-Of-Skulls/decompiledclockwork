using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000790 RID: 1936
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	internal sealed class SharedPerformanceCounter
	{
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000FE7C4 File Offset: 0x000FD7C4
		private static ProcessData ProcessData
		{
			get
			{
				if (SharedPerformanceCounter.procData == null)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
					try
					{
						int currentProcessId = NativeMethods.GetCurrentProcessId();
						long startTime = -1L;
						using (SafeProcessHandle safeProcessHandle = SafeProcessHandle.OpenProcess(1024, false, currentProcessId))
						{
							if (!safeProcessHandle.IsInvalid)
							{
								long num;
								NativeMethods.GetProcessTimes(safeProcessHandle, out startTime, out num, out num, out num);
							}
						}
						SharedPerformanceCounter.procData = new ProcessData(currentProcessId, startTime);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return SharedPerformanceCounter.procData;
			}
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000FE854 File Offset: 0x000FD854
		internal SharedPerformanceCounter(string catName, string counterName, string instanceName) : this(catName, counterName, instanceName, PerformanceCounterInstanceLifetime.Global)
		{
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000FE860 File Offset: 0x000FD860
		internal SharedPerformanceCounter(string catName, string counterName, string instanceName, PerformanceCounterInstanceLifetime lifetime)
		{
			this.categoryName = catName;
			this.categoryNameHashCode = SharedPerformanceCounter.GetWstrHashCode(this.categoryName);
			this.categoryData = this.GetCategoryData();
			if (this.categoryData.UseUniqueSharedMemory)
			{
				if (instanceName != null && instanceName.Length > 127)
				{
					throw new InvalidOperationException(SR.GetString("InstanceNameTooLong"));
				}
			}
			else if (lifetime != PerformanceCounterInstanceLifetime.Global)
			{
				throw new InvalidOperationException(SR.GetString("ProcessLifetimeNotValidInGlobal"));
			}
			if (counterName != null && instanceName != null)
			{
				if (!this.categoryData.CounterNames.Contains(counterName))
				{
					return;
				}
				this.counterEntryPointer = this.GetCounter(counterName, instanceName, this.categoryData.EnableReuse, lifetime);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x000FE918 File Offset: 0x000FD918
		private SharedPerformanceCounter.FileMapping FileView
		{
			get
			{
				return this.categoryData.FileMapping;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000FE925 File Offset: 0x000FD925
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000FE93F File Offset: 0x000FD93F
		internal long Value
		{
			get
			{
				if (this.counterEntryPointer == null)
				{
					return 0L;
				}
				return SharedPerformanceCounter.GetValue(this.counterEntryPointer);
			}
			set
			{
				if (this.counterEntryPointer == null)
				{
					return;
				}
				SharedPerformanceCounter.SetValue(this.counterEntryPointer, value);
			}
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000FE958 File Offset: 0x000FD958
		private unsafe int CalculateAndAllocateMemory(int totalSize, out int alignmentAdjustment)
		{
			alignmentAdjustment = 0;
			int num;
			int num2;
			do
			{
				num = *(UIntPtr)this.baseAddress;
				this.ResolveOffset(num, 0);
				num2 = this.CalculateMemory(num, totalSize, out alignmentAdjustment);
				int num3 = (int)(this.baseAddress + (long)num2) & 7;
				int num4 = 8 - num3 & 7;
				num2 += num4;
			}
			while (SafeNativeMethods.InterlockedCompareExchange((IntPtr)this.baseAddress, num2, num) != num);
			return num;
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000FE9B4 File Offset: 0x000FD9B4
		private int CalculateMemory(int oldOffset, int totalSize, out int alignmentAdjustment)
		{
			int num = this.CalculateMemoryNoBoundsCheck(oldOffset, totalSize, out alignmentAdjustment);
			if (num > this.FileView.FileMappingSize || num < 0)
			{
				throw new InvalidOperationException(SR.GetString("CountersOOM"));
			}
			return num;
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000FE9F0 File Offset: 0x000FD9F0
		private int CalculateMemoryNoBoundsCheck(int oldOffset, int totalSize, out int alignmentAdjustment)
		{
			Thread.MemoryBarrier();
			int num = (int)(this.baseAddress + (long)oldOffset) & 7;
			alignmentAdjustment = (8 - num & 7);
			int num2 = totalSize + alignmentAdjustment;
			return oldOffset + num2;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000FEA24 File Offset: 0x000FDA24
		private unsafe int CreateCategory(SharedPerformanceCounter.CategoryEntry* lastCategoryPointer, int instanceNameHashCode, string instanceName, PerformanceCounterInstanceLifetime lifetime)
		{
			int num = 0;
			int num2 = (this.categoryName.Length + 1) * 2;
			int num3 = SharedPerformanceCounter.CategoryEntrySize + SharedPerformanceCounter.InstanceEntrySize + SharedPerformanceCounter.CounterEntrySize * this.categoryData.CounterNames.Count + num2;
			for (int i = 0; i < this.categoryData.CounterNames.Count; i++)
			{
				num3 += (((string)this.categoryData.CounterNames[i]).Length + 1) * 2;
			}
			int num4;
			int num5;
			int num6;
			if (this.categoryData.UseUniqueSharedMemory)
			{
				num4 = 256;
				num3 += SharedPerformanceCounter.ProcessLifetimeEntrySize + num4;
				num5 = *(UIntPtr)this.baseAddress;
				num = this.CalculateMemory(num5, num3, out num6);
				if (num5 == this.InitialOffset)
				{
					lastCategoryPointer->IsConsistent = 0;
				}
			}
			else
			{
				num4 = (instanceName.Length + 1) * 2;
				num3 += num4;
				num5 = this.CalculateAndAllocateMemory(num3, out num6);
			}
			long num7 = this.ResolveOffset(num5, num3 + num6);
			SharedPerformanceCounter.CategoryEntry* ptr;
			SharedPerformanceCounter.InstanceEntry* ptr2;
			if (num5 == this.InitialOffset)
			{
				ptr = num7;
				num7 += (long)(SharedPerformanceCounter.CategoryEntrySize + num6);
				ptr2 = num7;
			}
			else
			{
				num7 += (long)num6;
				ptr = num7;
				num7 += (long)SharedPerformanceCounter.CategoryEntrySize;
				ptr2 = num7;
			}
			num7 += (long)SharedPerformanceCounter.InstanceEntrySize;
			SharedPerformanceCounter.CounterEntry* ptr3 = num7;
			num7 += (long)(SharedPerformanceCounter.CounterEntrySize * this.categoryData.CounterNames.Count);
			if (this.categoryData.UseUniqueSharedMemory)
			{
				SharedPerformanceCounter.ProcessLifetimeEntry* ptr4 = num7;
				num7 += (long)SharedPerformanceCounter.ProcessLifetimeEntrySize;
				ptr3->LifetimeOffset = ptr4 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.ProcessLifetimeEntry);
				SharedPerformanceCounter.PopulateLifetimeEntry(ptr4, lifetime);
			}
			ptr->CategoryNameHashCode = this.categoryNameHashCode;
			ptr->NextCategoryOffset = 0;
			ptr->FirstInstanceOffset = ptr2 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.InstanceEntry);
			ptr->CategoryNameOffset = (int)(num7 - this.baseAddress);
			Marshal.Copy(this.categoryName.ToCharArray(), 0, (IntPtr)num7, this.categoryName.Length);
			num7 += (long)num2;
			ptr2->InstanceNameHashCode = instanceNameHashCode;
			ptr2->NextInstanceOffset = 0;
			ptr2->FirstCounterOffset = ptr3 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
			ptr2->RefCount = 1;
			ptr2->InstanceNameOffset = (int)(num7 - this.baseAddress);
			Marshal.Copy(instanceName.ToCharArray(), 0, (IntPtr)num7, instanceName.Length);
			num7 += (long)num4;
			string text = (string)this.categoryData.CounterNames[0];
			ptr3->CounterNameHashCode = SharedPerformanceCounter.GetWstrHashCode(text);
			SharedPerformanceCounter.SetValue(ptr3, 0L);
			ptr3->CounterNameOffset = (int)(num7 - this.baseAddress);
			Marshal.Copy(text.ToCharArray(), 0, (IntPtr)num7, text.Length);
			num7 += (long)((text.Length + 1) * 2);
			for (int j = 1; j < this.categoryData.CounterNames.Count; j++)
			{
				SharedPerformanceCounter.CounterEntry* ptr5 = ptr3;
				text = (string)this.categoryData.CounterNames[j];
				ptr3++;
				ptr3->CounterNameHashCode = SharedPerformanceCounter.GetWstrHashCode(text);
				SharedPerformanceCounter.SetValue(ptr3, 0L);
				ptr3->CounterNameOffset = (int)(num7 - this.baseAddress);
				Marshal.Copy(text.ToCharArray(), 0, (IntPtr)num7, text.Length);
				num7 += (long)((text.Length + 1) * 2);
				ptr5->NextCounterOffset = ptr3 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
			}
			int num8 = ptr - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CategoryEntry);
			lastCategoryPointer->IsConsistent = 0;
			if (num8 != this.InitialOffset)
			{
				lastCategoryPointer->NextCategoryOffset = num8;
			}
			if (this.categoryData.UseUniqueSharedMemory)
			{
				*(UIntPtr)this.baseAddress = num;
				lastCategoryPointer->IsConsistent = 1;
			}
			return num8;
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000FEDE4 File Offset: 0x000FDDE4
		private unsafe int CreateInstance(SharedPerformanceCounter.CategoryEntry* categoryPointer, int instanceNameHashCode, string instanceName, PerformanceCounterInstanceLifetime lifetime)
		{
			int num = SharedPerformanceCounter.InstanceEntrySize + SharedPerformanceCounter.CounterEntrySize * this.categoryData.CounterNames.Count;
			int num2 = 0;
			int num3;
			int num4;
			int num5;
			if (this.categoryData.UseUniqueSharedMemory)
			{
				num3 = 256;
				num += SharedPerformanceCounter.ProcessLifetimeEntrySize + num3;
				num4 = *(UIntPtr)this.baseAddress;
				num2 = this.CalculateMemory(num4, num, out num5);
			}
			else
			{
				num3 = (instanceName.Length + 1) * 2;
				num += num3;
				for (int i = 0; i < this.categoryData.CounterNames.Count; i++)
				{
					num += (((string)this.categoryData.CounterNames[i]).Length + 1) * 2;
				}
				num4 = this.CalculateAndAllocateMemory(num, out num5);
			}
			num4 += num5;
			long num6 = this.ResolveOffset(num4, num);
			SharedPerformanceCounter.InstanceEntry* ptr = num6;
			num6 += (long)SharedPerformanceCounter.InstanceEntrySize;
			SharedPerformanceCounter.CounterEntry* ptr2 = num6;
			num6 += (long)(SharedPerformanceCounter.CounterEntrySize * this.categoryData.CounterNames.Count);
			if (this.categoryData.UseUniqueSharedMemory)
			{
				SharedPerformanceCounter.ProcessLifetimeEntry* ptr3 = num6;
				num6 += (long)SharedPerformanceCounter.ProcessLifetimeEntrySize;
				ptr2->LifetimeOffset = ptr3 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.ProcessLifetimeEntry);
				SharedPerformanceCounter.PopulateLifetimeEntry(ptr3, lifetime);
			}
			ptr->InstanceNameHashCode = instanceNameHashCode;
			ptr->NextInstanceOffset = 0;
			ptr->FirstCounterOffset = ptr2 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
			ptr->RefCount = 1;
			ptr->InstanceNameOffset = (int)(num6 - this.baseAddress);
			Marshal.Copy(instanceName.ToCharArray(), 0, (IntPtr)num6, instanceName.Length);
			num6 += (long)num3;
			if (this.categoryData.UseUniqueSharedMemory)
			{
				SharedPerformanceCounter.InstanceEntry* ptr4 = this.ResolveOffset(categoryPointer->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
				SharedPerformanceCounter.CounterEntry* ptr5 = this.ResolveOffset(ptr4->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
				ptr2->CounterNameHashCode = ptr5->CounterNameHashCode;
				SharedPerformanceCounter.SetValue(ptr2, 0L);
				ptr2->CounterNameOffset = ptr5->CounterNameOffset;
				for (int j = 1; j < this.categoryData.CounterNames.Count; j++)
				{
					SharedPerformanceCounter.CounterEntry* ptr6 = ptr2;
					ptr2++;
					ptr5 = this.ResolveOffset(ptr5->NextCounterOffset, SharedPerformanceCounter.CounterEntrySize);
					ptr2->CounterNameHashCode = ptr5->CounterNameHashCode;
					SharedPerformanceCounter.SetValue(ptr2, 0L);
					ptr2->CounterNameOffset = ptr5->CounterNameOffset;
					ptr6->NextCounterOffset = ptr2 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
				}
			}
			else
			{
				SharedPerformanceCounter.CounterEntry* ptr7 = null;
				for (int k = 0; k < this.categoryData.CounterNames.Count; k++)
				{
					string text = (string)this.categoryData.CounterNames[k];
					ptr2->CounterNameHashCode = SharedPerformanceCounter.GetWstrHashCode(text);
					ptr2->CounterNameOffset = (int)(num6 - this.baseAddress);
					Marshal.Copy(text.ToCharArray(), 0, (IntPtr)num6, text.Length);
					num6 += (long)((text.Length + 1) * 2);
					SharedPerformanceCounter.SetValue(ptr2, 0L);
					if (k != 0)
					{
						ptr7->NextCounterOffset = ptr2 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
					}
					ptr7 = ptr2;
					ptr2++;
				}
			}
			int firstInstanceOffset = ptr - this.baseAddress / (long)sizeof(SharedPerformanceCounter.InstanceEntry);
			categoryPointer->IsConsistent = 0;
			ptr->NextInstanceOffset = categoryPointer->FirstInstanceOffset;
			categoryPointer->FirstInstanceOffset = firstInstanceOffset;
			if (this.categoryData.UseUniqueSharedMemory)
			{
				*(UIntPtr)this.baseAddress = num2;
				categoryPointer->IsConsistent = 1;
			}
			return num4;
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000FF154 File Offset: 0x000FE154
		private unsafe int CreateCounter(SharedPerformanceCounter.CounterEntry* lastCounterPointer, int counterNameHashCode, string counterName)
		{
			int num = (counterName.Length + 1) * 2;
			int num2 = sizeof(SharedPerformanceCounter.CounterEntry) + num;
			int num4;
			int num3 = this.CalculateAndAllocateMemory(num2, out num4);
			num3 += num4;
			long num5 = this.ResolveOffset(num3, num2);
			SharedPerformanceCounter.CounterEntry* ptr = num5;
			num5 += (long)sizeof(SharedPerformanceCounter.CounterEntry);
			ptr->CounterNameOffset = (int)(num5 - this.baseAddress);
			ptr->CounterNameHashCode = counterNameHashCode;
			ptr->NextCounterOffset = 0;
			SharedPerformanceCounter.SetValue(ptr, 0L);
			Marshal.Copy(counterName.ToCharArray(), 0, (IntPtr)num5, counterName.Length);
			lastCounterPointer->NextCounterOffset = ptr - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
			return num3;
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000FF1F3 File Offset: 0x000FE1F3
		private unsafe static void PopulateLifetimeEntry(SharedPerformanceCounter.ProcessLifetimeEntry* lifetimeEntry, PerformanceCounterInstanceLifetime lifetime)
		{
			if (lifetime == PerformanceCounterInstanceLifetime.Process)
			{
				lifetimeEntry->LifetimeType = 1;
				lifetimeEntry->ProcessId = SharedPerformanceCounter.ProcessData.ProcessId;
				lifetimeEntry->StartupTime = SharedPerformanceCounter.ProcessData.StartupTime;
				return;
			}
			lifetimeEntry->ProcessId = 0;
			lifetimeEntry->StartupTime = 0L;
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000FF230 File Offset: 0x000FE230
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private unsafe static void WaitAndEnterCriticalSection(int* spinLockPointer, out bool taken)
		{
			SharedPerformanceCounter.WaitForCriticalSection(spinLockPointer);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				int num = Interlocked.CompareExchange(ref *spinLockPointer, 1, 0);
				taken = (num == 0);
			}
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000FF26C File Offset: 0x000FE26C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private unsafe static void WaitForCriticalSection(int* spinLockPointer)
		{
			int num = 5000;
			while (num > 0 && *spinLockPointer != 0)
			{
				if (*spinLockPointer != 0)
				{
					Thread.Sleep(1);
				}
				num--;
			}
			if (num == 0 && *spinLockPointer != 0)
			{
				*spinLockPointer = 0;
			}
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000FF2A1 File Offset: 0x000FE2A1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe static void ExitCriticalSection(int* spinLockPointer)
		{
			*spinLockPointer = 0;
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x000FF2A8 File Offset: 0x000FE2A8
		internal static int GetWstrHashCode(string wstr)
		{
			uint num = 5381U;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)wstr.Length))
			{
				num = ((num << 5) + num ^ (uint)wstr[(int)num2]);
				num2 += 1U;
			}
			return (int)num;
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000FF2E0 File Offset: 0x000FE2E0
		private unsafe int GetStringLength(char* startChar)
		{
			char* ptr = startChar;
			ulong num = (ulong)(this.baseAddress + (long)this.FileView.FileMappingSize);
			while (ptr < num - 2UL)
			{
				if (*ptr == '\0')
				{
					return (int)((long)(ptr - startChar));
				}
				ptr++;
			}
			throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000FF330 File Offset: 0x000FE330
		private unsafe bool StringEquals(string stringA, int offset)
		{
			char* ptr = this.ResolveOffset(offset, 0);
			ulong num = (ulong)(this.baseAddress + (long)this.FileView.FileMappingSize);
			int i;
			for (i = 0; i < stringA.Length; i++)
			{
				if (ptr + i != num - 2UL)
				{
					throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
				}
				if (stringA[i] != ptr[i])
				{
					return false;
				}
			}
			if (ptr + i != num - 2UL)
			{
				throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
			}
			return ptr[i] == '\0';
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000FF3C4 File Offset: 0x000FE3C4
		private unsafe SharedPerformanceCounter.CategoryData GetCategoryData()
		{
			SharedPerformanceCounter.CategoryData categoryData = (SharedPerformanceCounter.CategoryData)SharedPerformanceCounter.categoryDataTable[this.categoryName];
			if (categoryData == null)
			{
				lock (SharedPerformanceCounter.categoryDataTable)
				{
					categoryData = (SharedPerformanceCounter.CategoryData)SharedPerformanceCounter.categoryDataTable[this.categoryName];
					if (categoryData == null)
					{
						categoryData = new SharedPerformanceCounter.CategoryData();
						categoryData.FileMappingName = "netfxcustomperfcounters.1.0";
						categoryData.MutexName = this.categoryName;
						RegistryPermission registryPermission = new RegistryPermission(PermissionState.Unrestricted);
						registryPermission.Assert();
						RegistryKey registryKey = null;
						try
						{
							registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\" + this.categoryName + "\\Performance");
							object value = registryKey.GetValue("CategoryOptions");
							if (value != null)
							{
								int num = (int)value;
								categoryData.EnableReuse = ((num & 1) != 0);
								if ((num & 2) != 0)
								{
									categoryData.UseUniqueSharedMemory = true;
									this.InitialOffset = 8;
									categoryData.FileMappingName = "netfxcustomperfcounters.1.0" + this.categoryName;
								}
							}
							object value2 = registryKey.GetValue("FileMappingSize");
							int num2;
							if (value2 != null && categoryData.UseUniqueSharedMemory)
							{
								num2 = (int)value2;
								if (num2 < 32768)
								{
									num2 = 32768;
								}
								if (num2 > 33554432)
								{
									num2 = 33554432;
								}
							}
							else
							{
								num2 = SharedPerformanceCounter.GetFileMappingSizeFromConfig();
								if (categoryData.UseUniqueSharedMemory)
								{
									num2 >>= 2;
								}
							}
							object value3 = registryKey.GetValue("Counter Names");
							byte[] array = value3 as byte[];
							if (array != null)
							{
								ArrayList arrayList = new ArrayList();
								try
								{
									fixed (byte* ptr = array)
									{
										int num3 = 0;
										for (int i = 0; i < array.Length - 1; i += 2)
										{
											if (array[i] == 0 && array[i + 1] == 0 && num3 != i)
											{
												string text = new string((sbyte*)ptr, num3, i - num3, Encoding.Unicode);
												arrayList.Add(text.ToLowerInvariant());
												num3 = i + 2;
											}
										}
									}
								}
								finally
								{
									byte* ptr = null;
								}
								categoryData.CounterNames = arrayList;
							}
							else
							{
								string[] array2 = (string[])value3;
								for (int j = 0; j < array2.Length; j++)
								{
									array2[j] = array2[j].ToLowerInvariant();
								}
								categoryData.CounterNames = new ArrayList(array2);
							}
							if (SharedUtils.CurrentEnvironment == 1)
							{
								categoryData.FileMappingName = "Global\\" + categoryData.FileMappingName;
								categoryData.MutexName = "Global\\" + this.categoryName;
							}
							categoryData.FileMapping = new SharedPerformanceCounter.FileMapping(categoryData.FileMappingName, num2, this.InitialOffset);
							SharedPerformanceCounter.categoryDataTable[this.categoryName] = categoryData;
						}
						finally
						{
							if (registryKey != null)
							{
								registryKey.Close();
							}
							CodeAccessPermission.RevertAssert();
						}
					}
				}
			}
			this.baseAddress = (long)categoryData.FileMapping.FileViewAddress;
			if (categoryData.UseUniqueSharedMemory)
			{
				this.InitialOffset = 8;
			}
			return categoryData;
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000FF6DC File Offset: 0x000FE6DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static int GetFileMappingSizeFromConfig()
		{
			return DiagnosticsConfiguration.PerfomanceCountersFileMappingSize;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000FF6E4 File Offset: 0x000FE6E4
		private static void RemoveCategoryData(string categoryName)
		{
			lock (SharedPerformanceCounter.categoryDataTable)
			{
				SharedPerformanceCounter.categoryDataTable.Remove(categoryName);
			}
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000FF724 File Offset: 0x000FE724
		private unsafe SharedPerformanceCounter.CounterEntry* GetCounter(string counterName, string instanceName, bool enableReuse, PerformanceCounterInstanceLifetime lifetime)
		{
			int wstrHashCode = SharedPerformanceCounter.GetWstrHashCode(counterName);
			int instanceNameHashCode;
			if (instanceName != null && instanceName.Length != 0)
			{
				instanceNameHashCode = SharedPerformanceCounter.GetWstrHashCode(instanceName);
			}
			else
			{
				instanceNameHashCode = SharedPerformanceCounter.SingleInstanceHashCode;
				instanceName = "systemdiagnosticssharedsingleinstance";
			}
			Mutex mutex = null;
			SharedPerformanceCounter.CounterEntry* ptr = null;
			SharedPerformanceCounter.InstanceEntry* ptr2 = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SharedPerformanceCounter.CounterEntry* result;
			try
			{
				SharedUtils.EnterMutexWithoutGlobal(this.categoryData.MutexName, ref mutex);
				SharedPerformanceCounter.CategoryEntry* ptr3;
				while (!this.FindCategory(&ptr3))
				{
					bool flag;
					if (this.categoryData.UseUniqueSharedMemory)
					{
						flag = true;
					}
					else
					{
						SharedPerformanceCounter.WaitAndEnterCriticalSection(&ptr3->SpinLock, out flag);
					}
					if (flag)
					{
						int offset;
						try
						{
							offset = this.CreateCategory(ptr3, instanceNameHashCode, instanceName, lifetime);
						}
						finally
						{
							if (!this.categoryData.UseUniqueSharedMemory)
							{
								SharedPerformanceCounter.ExitCriticalSection(&ptr3->SpinLock);
							}
						}
						ptr3 = this.ResolveOffset(offset, SharedPerformanceCounter.CategoryEntrySize);
						ptr2 = this.ResolveOffset(ptr3->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
						this.FindCounter(wstrHashCode, counterName, ptr2, &ptr);
						return ptr;
					}
				}
				bool flag2;
				while (!this.FindInstance(instanceNameHashCode, instanceName, ptr3, &ptr2, true, lifetime, out flag2))
				{
					SharedPerformanceCounter.InstanceEntry* ptr4 = ptr2;
					bool flag3;
					if (this.categoryData.UseUniqueSharedMemory)
					{
						flag3 = true;
					}
					else
					{
						SharedPerformanceCounter.WaitAndEnterCriticalSection(&ptr4->SpinLock, out flag3);
					}
					if (flag3)
					{
						try
						{
							bool flag4 = false;
							if (enableReuse && flag2)
							{
								flag4 = this.TryReuseInstance(instanceNameHashCode, instanceName, ptr3, &ptr2, lifetime, ptr4);
							}
							if (!flag4)
							{
								int offset2 = this.CreateInstance(ptr3, instanceNameHashCode, instanceName, lifetime);
								ptr2 = this.ResolveOffset(offset2, SharedPerformanceCounter.InstanceEntrySize);
								this.FindCounter(wstrHashCode, counterName, ptr2, &ptr);
								return ptr;
							}
						}
						finally
						{
							if (!this.categoryData.UseUniqueSharedMemory)
							{
								SharedPerformanceCounter.ExitCriticalSection(&ptr4->SpinLock);
							}
						}
					}
				}
				if (this.categoryData.UseUniqueSharedMemory)
				{
					this.FindCounter(wstrHashCode, counterName, ptr2, &ptr);
					result = ptr;
				}
				else
				{
					while (!this.FindCounter(wstrHashCode, counterName, ptr2, &ptr))
					{
						bool flag5;
						SharedPerformanceCounter.WaitAndEnterCriticalSection(&ptr->SpinLock, out flag5);
						if (flag5)
						{
							try
							{
								int offset3 = this.CreateCounter(ptr, wstrHashCode, counterName);
								return this.ResolveOffset(offset3, SharedPerformanceCounter.CounterEntrySize);
							}
							finally
							{
								SharedPerformanceCounter.ExitCriticalSection(&ptr->SpinLock);
							}
						}
					}
					result = ptr;
				}
			}
			finally
			{
				try
				{
					if (ptr != null && ptr2 != null)
					{
						this.thisInstanceOffset = this.ResolveAddress(ptr2, SharedPerformanceCounter.InstanceEntrySize);
					}
				}
				catch (InvalidOperationException)
				{
					this.thisInstanceOffset = -1;
				}
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
			return result;
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x000FF9FC File Offset: 0x000FE9FC
		private unsafe bool FindCategory(SharedPerformanceCounter.CategoryEntry** returnCategoryPointerReference)
		{
			SharedPerformanceCounter.CategoryEntry* ptr = this.ResolveOffset(this.InitialOffset, SharedPerformanceCounter.CategoryEntrySize);
			SharedPerformanceCounter.CategoryEntry* ptr2 = ptr;
			SharedPerformanceCounter.CategoryEntry* ptr3;
			for (;;)
			{
				if (ptr2->IsConsistent == 0)
				{
					this.Verify(ptr2);
				}
				if (ptr2->CategoryNameHashCode == this.categoryNameHashCode && this.StringEquals(this.categoryName, ptr2->CategoryNameOffset))
				{
					break;
				}
				ptr3 = ptr2;
				if (ptr2->NextCategoryOffset == 0)
				{
					goto IL_6C;
				}
				ptr2 = this.ResolveOffset(ptr2->NextCategoryOffset, SharedPerformanceCounter.CategoryEntrySize);
			}
			*(IntPtr*)returnCategoryPointerReference = ptr2;
			return true;
			IL_6C:
			*(IntPtr*)returnCategoryPointerReference = ptr3;
			return false;
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x000FFA7C File Offset: 0x000FEA7C
		private unsafe bool FindCounter(int counterNameHashCode, string counterName, SharedPerformanceCounter.InstanceEntry* instancePointer, SharedPerformanceCounter.CounterEntry** returnCounterPointerReference)
		{
			SharedPerformanceCounter.CounterEntry* ptr = this.ResolveOffset(instancePointer->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
			while (ptr->CounterNameHashCode != counterNameHashCode || !this.StringEquals(counterName, ptr->CounterNameOffset))
			{
				SharedPerformanceCounter.CounterEntry* ptr2 = ptr;
				if (ptr->NextCounterOffset == 0)
				{
					*(IntPtr*)returnCounterPointerReference = ptr2;
					return false;
				}
				ptr = this.ResolveOffset(ptr->NextCounterOffset, SharedPerformanceCounter.CounterEntrySize);
			}
			*(IntPtr*)returnCounterPointerReference = ptr;
			return true;
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x000FFAE0 File Offset: 0x000FEAE0
		private unsafe bool FindInstance(int instanceNameHashCode, string instanceName, SharedPerformanceCounter.CategoryEntry* categoryPointer, SharedPerformanceCounter.InstanceEntry** returnInstancePointerReference, bool activateUnusedInstances, PerformanceCounterInstanceLifetime lifetime, out bool foundFreeInstance)
		{
			SharedPerformanceCounter.InstanceEntry* ptr = this.ResolveOffset(categoryPointer->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
			foundFreeInstance = false;
			if (ptr->InstanceNameHashCode == SharedPerformanceCounter.SingleInstanceHashCode)
			{
				if (this.StringEquals("systemdiagnosticssharedsingleinstance", ptr->InstanceNameOffset))
				{
					if (instanceName != "systemdiagnosticssharedsingleinstance")
					{
						throw new InvalidOperationException(SR.GetString("SingleInstanceOnly", new object[]
						{
							this.categoryName
						}));
					}
				}
				else if (instanceName == "systemdiagnosticssharedsingleinstance")
				{
					throw new InvalidOperationException(SR.GetString("MultiInstanceOnly", new object[]
					{
						this.categoryName
					}));
				}
			}
			else if (instanceName == "systemdiagnosticssharedsingleinstance")
			{
				throw new InvalidOperationException(SR.GetString("MultiInstanceOnly", new object[]
				{
					this.categoryName
				}));
			}
			bool flag = activateUnusedInstances;
			if (activateUnusedInstances)
			{
				int totalSize = SharedPerformanceCounter.InstanceEntrySize + SharedPerformanceCounter.ProcessLifetimeEntrySize + 256 + SharedPerformanceCounter.CounterEntrySize * this.categoryData.CounterNames.Count;
				int oldOffset = *(UIntPtr)this.baseAddress;
				int num2;
				int num = this.CalculateMemoryNoBoundsCheck(oldOffset, totalSize, out num2);
				if (num <= this.FileView.FileMappingSize && num >= 0)
				{
					long num3 = DateTime.Now.Ticks - SharedPerformanceCounter.LastInstanceLifetimeSweepTick;
					if (num3 < SharedPerformanceCounter.InstanceLifetimeSweepWindow)
					{
						flag = false;
					}
				}
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			bool result;
			try
			{
				bool flag2;
				SharedPerformanceCounter.InstanceEntry* ptr2;
				for (;;)
				{
					flag2 = false;
					if (flag && ptr->RefCount != 0)
					{
						flag2 = true;
						this.VerifyLifetime(ptr);
					}
					if (ptr->InstanceNameHashCode == instanceNameHashCode && this.StringEquals(instanceName, ptr->InstanceNameOffset))
					{
						break;
					}
					if (ptr->RefCount == 0)
					{
						foundFreeInstance = true;
					}
					ptr2 = ptr;
					if (ptr->NextInstanceOffset == 0)
					{
						goto IL_334;
					}
					ptr = this.ResolveOffset(ptr->NextInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
				}
				*(IntPtr*)returnInstancePointerReference = ptr;
				SharedPerformanceCounter.CounterEntry* ptr3 = this.ResolveOffset(ptr->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
				SharedPerformanceCounter.ProcessLifetimeEntry* ptr4;
				if (this.categoryData.UseUniqueSharedMemory)
				{
					ptr4 = this.ResolveOffset(ptr3->LifetimeOffset, SharedPerformanceCounter.ProcessLifetimeEntrySize);
				}
				else
				{
					ptr4 = null;
				}
				if (!flag2 && ptr->RefCount != 0)
				{
					this.VerifyLifetime(ptr);
				}
				if (ptr->RefCount != 0)
				{
					if (ptr4 != null && ptr4->ProcessId != 0)
					{
						if (lifetime != PerformanceCounterInstanceLifetime.Process)
						{
							throw new InvalidOperationException(SR.GetString("CantConvertProcessToGlobal"));
						}
						if (SharedPerformanceCounter.ProcessData.ProcessId != ptr4->ProcessId)
						{
							throw new InvalidOperationException(SR.GetString("InstanceAlreadyExists", new object[]
							{
								instanceName
							}));
						}
						if (ptr4->StartupTime != -1L && SharedPerformanceCounter.ProcessData.StartupTime != -1L && SharedPerformanceCounter.ProcessData.StartupTime != ptr4->StartupTime)
						{
							throw new InvalidOperationException(SR.GetString("InstanceAlreadyExists", new object[]
							{
								instanceName
							}));
						}
					}
					else if (lifetime == PerformanceCounterInstanceLifetime.Process)
					{
						throw new InvalidOperationException(SR.GetString("CantConvertGlobalToProcess"));
					}
					return true;
				}
				if (activateUnusedInstances)
				{
					Mutex mutex = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						SharedUtils.EnterMutexWithoutGlobal(this.categoryData.MutexName, ref mutex);
						this.ClearCounterValues(ptr);
						if (ptr4 != null)
						{
							SharedPerformanceCounter.PopulateLifetimeEntry(ptr4, lifetime);
						}
						ptr->RefCount = 1;
						return true;
					}
					finally
					{
						if (mutex != null)
						{
							mutex.ReleaseMutex();
							mutex.Close();
						}
					}
				}
				return false;
				IL_334:
				*(IntPtr*)returnInstancePointerReference = ptr2;
				result = false;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				if (flag)
				{
					SharedPerformanceCounter.LastInstanceLifetimeSweepTick = DateTime.Now.Ticks;
				}
			}
			return result;
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x000FFE7C File Offset: 0x000FEE7C
		private unsafe bool TryReuseInstance(int instanceNameHashCode, string instanceName, SharedPerformanceCounter.CategoryEntry* categoryPointer, SharedPerformanceCounter.InstanceEntry** returnInstancePointerReference, PerformanceCounterInstanceLifetime lifetime, SharedPerformanceCounter.InstanceEntry* lockInstancePointer)
		{
			SharedPerformanceCounter.InstanceEntry* ptr = this.ResolveOffset(categoryPointer->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
			SharedPerformanceCounter.InstanceEntry* ptr3;
			for (;;)
			{
				if (ptr->RefCount == 0)
				{
					long num;
					bool flag;
					if (this.categoryData.UseUniqueSharedMemory)
					{
						num = this.ResolveOffset(ptr->InstanceNameOffset, 256);
						flag = true;
					}
					else
					{
						num = this.ResolveOffset(ptr->InstanceNameOffset, 0);
						int stringLength = this.GetStringLength(num);
						flag = (stringLength == instanceName.Length);
					}
					bool flag2 = lockInstancePointer == ptr || this.categoryData.UseUniqueSharedMemory;
					if (flag)
					{
						bool flag3;
						if (flag2)
						{
							flag3 = true;
						}
						else
						{
							SharedPerformanceCounter.WaitAndEnterCriticalSection(&ptr->SpinLock, out flag3);
						}
						if (flag3)
						{
							try
							{
								char[] array = new char[instanceName.Length + 1];
								instanceName.CopyTo(0, array, 0, instanceName.Length);
								array[instanceName.Length] = '\0';
								Marshal.Copy(array, 0, (IntPtr)num, array.Length);
								ptr->InstanceNameHashCode = instanceNameHashCode;
								*(IntPtr*)returnInstancePointerReference = ptr;
								this.ClearCounterValues(*(IntPtr*)returnInstancePointerReference);
								if (this.categoryData.UseUniqueSharedMemory)
								{
									SharedPerformanceCounter.CounterEntry* ptr2 = this.ResolveOffset(ptr->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
									SharedPerformanceCounter.ProcessLifetimeEntry* lifetimeEntry = this.ResolveOffset(ptr2->LifetimeOffset, SharedPerformanceCounter.ProcessLifetimeEntrySize);
									SharedPerformanceCounter.PopulateLifetimeEntry(lifetimeEntry, lifetime);
								}
								((IntPtr*)returnInstancePointerReference)->RefCount = 1;
								return true;
							}
							finally
							{
								if (!flag2)
								{
									SharedPerformanceCounter.ExitCriticalSection(&ptr->SpinLock);
								}
							}
						}
					}
				}
				ptr3 = ptr;
				if (ptr->NextInstanceOffset == 0)
				{
					break;
				}
				ptr = this.ResolveOffset(ptr->NextInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
			}
			*(IntPtr*)returnInstancePointerReference = ptr3;
			return false;
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x00100010 File Offset: 0x000FF010
		private unsafe void Verify(SharedPerformanceCounter.CategoryEntry* currentCategoryPointer)
		{
			if (!this.categoryData.UseUniqueSharedMemory)
			{
				return;
			}
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutexWithoutGlobal(this.categoryData.MutexName, ref mutex);
				this.VerifyCategory(currentCategoryPointer);
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00100070 File Offset: 0x000FF070
		private unsafe void VerifyCategory(SharedPerformanceCounter.CategoryEntry* currentCategoryPointer)
		{
			int num = *(UIntPtr)this.baseAddress;
			this.ResolveOffset(num, 0);
			if (currentCategoryPointer->NextCategoryOffset > num)
			{
				currentCategoryPointer->NextCategoryOffset = 0;
			}
			else if (currentCategoryPointer->NextCategoryOffset != 0)
			{
				this.VerifyCategory(this.ResolveOffset(currentCategoryPointer->NextCategoryOffset, SharedPerformanceCounter.CategoryEntrySize));
			}
			if (currentCategoryPointer->FirstInstanceOffset != 0)
			{
				if (currentCategoryPointer->FirstInstanceOffset > num)
				{
					SharedPerformanceCounter.InstanceEntry* ptr = this.ResolveOffset(currentCategoryPointer->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
					currentCategoryPointer->FirstInstanceOffset = ptr->NextInstanceOffset;
					if (currentCategoryPointer->FirstInstanceOffset > num)
					{
						currentCategoryPointer->FirstInstanceOffset = 0;
					}
				}
				if (currentCategoryPointer->FirstInstanceOffset != 0)
				{
					this.VerifyInstance(this.ResolveOffset(currentCategoryPointer->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize));
				}
			}
			currentCategoryPointer->IsConsistent = 1;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x00100128 File Offset: 0x000FF128
		private unsafe void VerifyInstance(SharedPerformanceCounter.InstanceEntry* currentInstancePointer)
		{
			int num = *(UIntPtr)this.baseAddress;
			this.ResolveOffset(num, 0);
			if (currentInstancePointer->NextInstanceOffset > num)
			{
				currentInstancePointer->NextInstanceOffset = 0;
				return;
			}
			if (currentInstancePointer->NextInstanceOffset != 0)
			{
				this.VerifyInstance(this.ResolveOffset(currentInstancePointer->NextInstanceOffset, SharedPerformanceCounter.InstanceEntrySize));
			}
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00100178 File Offset: 0x000FF178
		private unsafe void VerifyLifetime(SharedPerformanceCounter.InstanceEntry* currentInstancePointer)
		{
			SharedPerformanceCounter.CounterEntry* ptr = this.ResolveOffset(currentInstancePointer->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
			if (ptr->LifetimeOffset != 0)
			{
				SharedPerformanceCounter.ProcessLifetimeEntry* ptr2 = this.ResolveOffset(ptr->LifetimeOffset, SharedPerformanceCounter.ProcessLifetimeEntrySize);
				if (ptr2->LifetimeType == 1)
				{
					int processId = ptr2->ProcessId;
					long startupTime = ptr2->StartupTime;
					if (processId != 0)
					{
						if (processId == SharedPerformanceCounter.ProcessData.ProcessId)
						{
							if (SharedPerformanceCounter.ProcessData.StartupTime != -1L && startupTime != -1L && SharedPerformanceCounter.ProcessData.StartupTime != startupTime)
							{
								currentInstancePointer->RefCount = 0;
								return;
							}
						}
						else
						{
							using (SafeProcessHandle safeProcessHandle = SafeProcessHandle.OpenProcess(1024, false, processId))
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								if (lastWin32Error == 87 && safeProcessHandle.IsInvalid)
								{
									currentInstancePointer->RefCount = 0;
									return;
								}
								long num;
								long num2;
								if (!safeProcessHandle.IsInvalid && startupTime != -1L && NativeMethods.GetProcessTimes(safeProcessHandle, out num, out num2, out num2, out num2) && num != startupTime)
								{
									currentInstancePointer->RefCount = 0;
									return;
								}
							}
							using (SafeProcessHandle safeProcessHandle2 = SafeProcessHandle.OpenProcess(1048576, false, processId))
							{
								if (!safeProcessHandle2.IsInvalid)
								{
									using (ProcessWaitHandle processWaitHandle = new ProcessWaitHandle(safeProcessHandle2))
									{
										if (processWaitHandle.WaitOne(0, false))
										{
											currentInstancePointer->RefCount = 0;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x001002F8 File Offset: 0x000FF2F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal unsafe long IncrementBy(long value)
		{
			if (this.counterEntryPointer == null)
			{
				return 0L;
			}
			SharedPerformanceCounter.CounterEntry* counterEntry = this.counterEntryPointer;
			return SharedPerformanceCounter.AddToValue(counterEntry, value);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00100320 File Offset: 0x000FF320
		internal long Increment()
		{
			if (this.counterEntryPointer == null)
			{
				return 0L;
			}
			return SharedPerformanceCounter.IncrementUnaligned(this.counterEntryPointer);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x0010033A File Offset: 0x000FF33A
		internal long Decrement()
		{
			if (this.counterEntryPointer == null)
			{
				return 0L;
			}
			return SharedPerformanceCounter.DecrementUnaligned(this.counterEntryPointer);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x00100354 File Offset: 0x000FF354
		internal static void RemoveAllInstances(string categoryName)
		{
			SharedPerformanceCounter sharedPerformanceCounter = new SharedPerformanceCounter(categoryName, null, null);
			sharedPerformanceCounter.RemoveAllInstances();
			SharedPerformanceCounter.RemoveCategoryData(categoryName);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x00100378 File Offset: 0x000FF378
		private unsafe void RemoveAllInstances()
		{
			SharedPerformanceCounter.CategoryEntry* ptr;
			if (!this.FindCategory(&ptr))
			{
				return;
			}
			SharedPerformanceCounter.InstanceEntry* ptr2 = this.ResolveOffset(ptr->FirstInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutexWithoutGlobal(this.categoryData.MutexName, ref mutex);
				for (;;)
				{
					this.RemoveOneInstance(ptr2, true);
					if (ptr2->NextInstanceOffset == 0)
					{
						break;
					}
					ptr2 = this.ResolveOffset(ptr2->NextInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
				}
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00100404 File Offset: 0x000FF404
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal unsafe void RemoveInstance(string instanceName, PerformanceCounterInstanceLifetime instanceLifetime)
		{
			if (instanceName == null || instanceName.Length == 0)
			{
				return;
			}
			int wstrHashCode = SharedPerformanceCounter.GetWstrHashCode(instanceName);
			SharedPerformanceCounter.CategoryEntry* categoryPointer;
			if (!this.FindCategory(&categoryPointer))
			{
				return;
			}
			SharedPerformanceCounter.InstanceEntry* ptr = null;
			bool flag = false;
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutexWithoutGlobal(this.categoryData.MutexName, ref mutex);
				if (this.thisInstanceOffset != -1)
				{
					try
					{
						ptr = this.ResolveOffset(this.thisInstanceOffset, SharedPerformanceCounter.InstanceEntrySize);
						if (ptr->InstanceNameHashCode == wstrHashCode && this.StringEquals(instanceName, ptr->InstanceNameOffset))
						{
							flag = true;
							SharedPerformanceCounter.CounterEntry* ptr2 = this.ResolveOffset(ptr->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
							if (this.categoryData.UseUniqueSharedMemory)
							{
								SharedPerformanceCounter.ProcessLifetimeEntry* ptr3 = this.ResolveOffset(ptr2->LifetimeOffset, SharedPerformanceCounter.ProcessLifetimeEntrySize);
								if (ptr3 != null && ptr3->LifetimeType == 1 && ptr3->ProcessId != 0)
								{
									flag &= (instanceLifetime == PerformanceCounterInstanceLifetime.Process);
									flag &= (SharedPerformanceCounter.ProcessData.ProcessId == ptr3->ProcessId);
									if (ptr3->StartupTime != -1L && SharedPerformanceCounter.ProcessData.StartupTime != -1L)
									{
										flag &= (SharedPerformanceCounter.ProcessData.StartupTime == ptr3->StartupTime);
									}
								}
								else
								{
									flag &= (instanceLifetime != PerformanceCounterInstanceLifetime.Process);
								}
							}
						}
					}
					catch (InvalidOperationException)
					{
						flag = false;
					}
					if (!flag)
					{
						this.thisInstanceOffset = -1;
					}
				}
				bool flag2;
				if (flag || this.FindInstance(wstrHashCode, instanceName, categoryPointer, &ptr, false, instanceLifetime, out flag2))
				{
					if (ptr != null)
					{
						this.RemoveOneInstance(ptr, false);
					}
				}
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x001005B8 File Offset: 0x000FF5B8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private unsafe void RemoveOneInstance(SharedPerformanceCounter.InstanceEntry* instancePointer, bool clearValue)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this.categoryData.UseUniqueSharedMemory)
				{
					while (!flag)
					{
						SharedPerformanceCounter.WaitAndEnterCriticalSection(&instancePointer->SpinLock, out flag);
					}
				}
				instancePointer->RefCount = 0;
				if (clearValue)
				{
					this.ClearCounterValues(instancePointer);
				}
			}
			finally
			{
				if (flag)
				{
					SharedPerformanceCounter.ExitCriticalSection(&instancePointer->SpinLock);
				}
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x00100620 File Offset: 0x000FF620
		private unsafe void ClearCounterValues(SharedPerformanceCounter.InstanceEntry* instancePointer)
		{
			SharedPerformanceCounter.CounterEntry* ptr = null;
			if (instancePointer->FirstCounterOffset != 0)
			{
				ptr = this.ResolveOffset(instancePointer->FirstCounterOffset, SharedPerformanceCounter.CounterEntrySize);
			}
			while (ptr != null)
			{
				SharedPerformanceCounter.SetValue(ptr, 0L);
				if (ptr->NextCounterOffset != 0)
				{
					ptr = this.ResolveOffset(ptr->NextCounterOffset, SharedPerformanceCounter.CounterEntrySize);
				}
				else
				{
					ptr = null;
				}
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x0010067C File Offset: 0x000FF67C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe static long AddToValue(SharedPerformanceCounter.CounterEntry* counterEntry, long addend)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				ulong num = (ulong)((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_hi;
				num <<= 32;
				num |= (ulong)((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_lo;
				num += (ulong)addend;
				((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_hi = (int)(num >> 32);
				((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_lo = (int)(num & (ulong)-1);
				return (long)num;
			}
			return Interlocked.Add(ref counterEntry->Value, addend);
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x001006D2 File Offset: 0x000FF6D2
		private unsafe static long DecrementUnaligned(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				return SharedPerformanceCounter.AddToValue(counterEntry, -1L);
			}
			return Interlocked.Decrement(ref counterEntry->Value);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x001006F0 File Offset: 0x000FF6F0
		private unsafe static long GetValue(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				ulong num = (ulong)((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_hi;
				num <<= 32;
				return (long)(num | (ulong)((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_lo);
			}
			return counterEntry->Value;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x00100726 File Offset: 0x000FF726
		private unsafe static long IncrementUnaligned(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				return SharedPerformanceCounter.AddToValue(counterEntry, 1L);
			}
			return Interlocked.Increment(ref counterEntry->Value);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x00100744 File Offset: 0x000FF744
		private unsafe static void SetValue(SharedPerformanceCounter.CounterEntry* counterEntry, long value)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_lo = (int)(value & (long)((ulong)-1));
				((SharedPerformanceCounter.CounterEntryMisaligned*)counterEntry)->Value_hi = (int)(value >> 32);
				return;
			}
			counterEntry->Value = value;
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x00100779 File Offset: 0x000FF779
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe static bool IsMisaligned(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			return (counterEntry & 7L) != null;
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x00100788 File Offset: 0x000FF788
		private long ResolveOffset(int offset, int sizeToRead)
		{
			if (offset > this.FileView.FileMappingSize - sizeToRead || offset < 0)
			{
				throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
			}
			return this.baseAddress + (long)offset;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x001007C4 File Offset: 0x000FF7C4
		private int ResolveAddress(long address, int sizeToRead)
		{
			int num = (int)(address - this.baseAddress);
			if (num > this.FileView.FileMappingSize - sizeToRead || num < 0)
			{
				throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
			}
			return num;
		}

		// Token: 0x04003463 RID: 13411
		private const int MaxSpinCount = 5000;

		// Token: 0x04003464 RID: 13412
		internal const int DefaultCountersFileMappingSize = 524288;

		// Token: 0x04003465 RID: 13413
		internal const int MaxCountersFileMappingSize = 33554432;

		// Token: 0x04003466 RID: 13414
		internal const int MinCountersFileMappingSize = 32768;

		// Token: 0x04003467 RID: 13415
		internal const int InstanceNameMaxLength = 127;

		// Token: 0x04003468 RID: 13416
		internal const int InstanceNameSlotSize = 256;

		// Token: 0x04003469 RID: 13417
		internal const string SingleInstanceName = "systemdiagnosticssharedsingleinstance";

		// Token: 0x0400346A RID: 13418
		internal const string DefaultFileMappingName = "netfxcustomperfcounters.1.0";

		// Token: 0x0400346B RID: 13419
		internal static readonly int SingleInstanceHashCode = SharedPerformanceCounter.GetWstrHashCode("systemdiagnosticssharedsingleinstance");

		// Token: 0x0400346C RID: 13420
		private static Hashtable categoryDataTable = new Hashtable(StringComparer.Ordinal);

		// Token: 0x0400346D RID: 13421
		private static readonly int CategoryEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.CategoryEntry));

		// Token: 0x0400346E RID: 13422
		private static readonly int InstanceEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.InstanceEntry));

		// Token: 0x0400346F RID: 13423
		private static readonly int CounterEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.CounterEntry));

		// Token: 0x04003470 RID: 13424
		private static readonly int ProcessLifetimeEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.ProcessLifetimeEntry));

		// Token: 0x04003471 RID: 13425
		private static long LastInstanceLifetimeSweepTick;

		// Token: 0x04003472 RID: 13426
		private static long InstanceLifetimeSweepWindow = 300000000L;

		// Token: 0x04003473 RID: 13427
		private static ProcessData procData;

		// Token: 0x04003474 RID: 13428
		internal int InitialOffset = 4;

		// Token: 0x04003475 RID: 13429
		private SharedPerformanceCounter.CategoryData categoryData;

		// Token: 0x04003476 RID: 13430
		private long baseAddress;

		// Token: 0x04003477 RID: 13431
		private unsafe SharedPerformanceCounter.CounterEntry* counterEntryPointer;

		// Token: 0x04003478 RID: 13432
		private string categoryName;

		// Token: 0x04003479 RID: 13433
		private int categoryNameHashCode;

		// Token: 0x0400347A RID: 13434
		private int thisInstanceOffset = -1;

		// Token: 0x02000791 RID: 1937
		private class FileMapping
		{
			// Token: 0x06003C05 RID: 15365 RVA: 0x00100886 File Offset: 0x000FF886
			public FileMapping(string fileMappingName, int fileMappingSize, int initialOffset)
			{
				this.Initialize(fileMappingName, fileMappingSize, initialOffset);
			}

			// Token: 0x17000E1B RID: 3611
			// (get) Token: 0x06003C06 RID: 15366 RVA: 0x00100897 File Offset: 0x000FF897
			internal IntPtr FileViewAddress
			{
				get
				{
					if (this.fileViewAddress.IsInvalid)
					{
						throw new InvalidOperationException(SR.GetString("SharedMemoryGhosted"));
					}
					return this.fileViewAddress.DangerousGetHandle();
				}
			}

			// Token: 0x06003C07 RID: 15367 RVA: 0x001008C4 File Offset: 0x000FF8C4
			private void Initialize(string fileMappingName, int fileMappingSize, int initialOffset)
			{
				SharedUtils.CheckEnvironment();
				SafeLocalMemHandle safeLocalMemHandle = null;
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					string stringSecurityDescriptor = "D:(A;OICI;FRFWGRGW;;;AU)(A;OICI;FRFWGRGW;;;S-1-5-33)";
					if (!SafeLocalMemHandle.ConvertStringSecurityDescriptorToSecurityDescriptor(stringSecurityDescriptor, 1, out safeLocalMemHandle, IntPtr.Zero))
					{
						throw new InvalidOperationException(SR.GetString("SetSecurityDescriptorFailed"));
					}
					NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES();
					security_ATTRIBUTES.lpSecurityDescriptor = safeLocalMemHandle;
					security_ATTRIBUTES.bInheritHandle = false;
					bool flag = false;
					while (!flag)
					{
						this.fileMappingHandle = NativeMethods.CreateFileMapping((IntPtr)(-1), security_ATTRIBUTES, 4, 0, fileMappingSize, fileMappingName);
						if (Marshal.GetLastWin32Error() != 5 || !this.fileMappingHandle.IsInvalid)
						{
							flag = true;
						}
						else
						{
							this.fileMappingHandle.SetHandleAsInvalid();
							this.fileMappingHandle = NativeMethods.OpenFileMapping(2, false, fileMappingName);
							if (Marshal.GetLastWin32Error() != 2 || !this.fileMappingHandle.IsInvalid)
							{
								flag = true;
							}
						}
					}
					if (this.fileMappingHandle.IsInvalid)
					{
						throw new InvalidOperationException(SR.GetString("CantCreateFileMapping"));
					}
					this.fileViewAddress = SafeFileMapViewHandle.MapViewOfFile(this.fileMappingHandle, 2, 0, 0, UIntPtr.Zero);
					if (this.fileViewAddress.IsInvalid)
					{
						throw new InvalidOperationException(SR.GetString("CantMapFileView"));
					}
					NativeMethods.MEMORY_BASIC_INFORMATION memory_BASIC_INFORMATION = default(NativeMethods.MEMORY_BASIC_INFORMATION);
					if (NativeMethods.VirtualQuery(this.fileViewAddress, ref memory_BASIC_INFORMATION, (IntPtr)sizeof(NativeMethods.MEMORY_BASIC_INFORMATION)) == IntPtr.Zero)
					{
						throw new InvalidOperationException(SR.GetString("CantGetMappingSize"));
					}
					this.FileMappingSize = (int)((uint)memory_BASIC_INFORMATION.RegionSize);
				}
				finally
				{
					if (safeLocalMemHandle != null)
					{
						safeLocalMemHandle.Close();
					}
					CodeAccessPermission.RevertAssert();
				}
				SafeNativeMethods.InterlockedCompareExchange(this.fileViewAddress.DangerousGetHandle(), initialOffset, 0);
			}

			// Token: 0x0400347B RID: 13435
			internal int FileMappingSize;

			// Token: 0x0400347C RID: 13436
			private SafeFileMapViewHandle fileViewAddress;

			// Token: 0x0400347D RID: 13437
			private SafeFileMappingHandle fileMappingHandle;
		}

		// Token: 0x02000792 RID: 1938
		private struct CategoryEntry
		{
			// Token: 0x0400347E RID: 13438
			public int SpinLock;

			// Token: 0x0400347F RID: 13439
			public int CategoryNameHashCode;

			// Token: 0x04003480 RID: 13440
			public int CategoryNameOffset;

			// Token: 0x04003481 RID: 13441
			public int FirstInstanceOffset;

			// Token: 0x04003482 RID: 13442
			public int NextCategoryOffset;

			// Token: 0x04003483 RID: 13443
			public int IsConsistent;
		}

		// Token: 0x02000793 RID: 1939
		private struct InstanceEntry
		{
			// Token: 0x04003484 RID: 13444
			public int SpinLock;

			// Token: 0x04003485 RID: 13445
			public int InstanceNameHashCode;

			// Token: 0x04003486 RID: 13446
			public int InstanceNameOffset;

			// Token: 0x04003487 RID: 13447
			public int RefCount;

			// Token: 0x04003488 RID: 13448
			public int FirstCounterOffset;

			// Token: 0x04003489 RID: 13449
			public int NextInstanceOffset;
		}

		// Token: 0x02000794 RID: 1940
		private struct CounterEntry
		{
			// Token: 0x0400348A RID: 13450
			public int SpinLock;

			// Token: 0x0400348B RID: 13451
			public int CounterNameHashCode;

			// Token: 0x0400348C RID: 13452
			public int CounterNameOffset;

			// Token: 0x0400348D RID: 13453
			public int LifetimeOffset;

			// Token: 0x0400348E RID: 13454
			public long Value;

			// Token: 0x0400348F RID: 13455
			public int NextCounterOffset;

			// Token: 0x04003490 RID: 13456
			public int padding2;
		}

		// Token: 0x02000795 RID: 1941
		private struct CounterEntryMisaligned
		{
			// Token: 0x04003491 RID: 13457
			public int SpinLock;

			// Token: 0x04003492 RID: 13458
			public int CounterNameHashCode;

			// Token: 0x04003493 RID: 13459
			public int CounterNameOffset;

			// Token: 0x04003494 RID: 13460
			public int LifetimeOffset;

			// Token: 0x04003495 RID: 13461
			public int Value_lo;

			// Token: 0x04003496 RID: 13462
			public int Value_hi;

			// Token: 0x04003497 RID: 13463
			public int NextCounterOffset;

			// Token: 0x04003498 RID: 13464
			public int padding2;
		}

		// Token: 0x02000796 RID: 1942
		private struct ProcessLifetimeEntry
		{
			// Token: 0x04003499 RID: 13465
			public int LifetimeType;

			// Token: 0x0400349A RID: 13466
			public int ProcessId;

			// Token: 0x0400349B RID: 13467
			public long StartupTime;
		}

		// Token: 0x02000797 RID: 1943
		private class CategoryData
		{
			// Token: 0x0400349C RID: 13468
			public SharedPerformanceCounter.FileMapping FileMapping;

			// Token: 0x0400349D RID: 13469
			public bool EnableReuse;

			// Token: 0x0400349E RID: 13470
			public bool UseUniqueSharedMemory;

			// Token: 0x0400349F RID: 13471
			public string FileMappingName;

			// Token: 0x040034A0 RID: 13472
			public string MutexName;

			// Token: 0x040034A1 RID: 13473
			public ArrayList CounterNames;
		}
	}
}

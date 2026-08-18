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
	// Token: 0x02000504 RID: 1284
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	internal sealed class SharedPerformanceCounter
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000DC3E4 File Offset: 0x000DA5E4
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

		// Token: 0x060030BE RID: 12478 RVA: 0x000DC478 File Offset: 0x000DA678
		internal SharedPerformanceCounter(string catName, string counterName, string instanceName) : this(catName, counterName, instanceName, PerformanceCounterInstanceLifetime.Global)
		{
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000DC484 File Offset: 0x000DA684
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
			if (counterName != null && instanceName != null && this.categoryData.CounterNames.Contains(counterName))
			{
				this.counterEntryPointer = this.GetCounter(counterName, instanceName, this.categoryData.EnableReuse, lifetime);
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000DC53B File Offset: 0x000DA73B
		private SharedPerformanceCounter.FileMapping FileView
		{
			get
			{
				return this.categoryData.FileMapping;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000DC548 File Offset: 0x000DA748
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x000DC562 File Offset: 0x000DA762
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

		// Token: 0x060030C3 RID: 12483 RVA: 0x000DC57C File Offset: 0x000DA77C
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

		// Token: 0x060030C4 RID: 12484 RVA: 0x000DC5D8 File Offset: 0x000DA7D8
		private int CalculateMemory(int oldOffset, int totalSize, out int alignmentAdjustment)
		{
			int num = this.CalculateMemoryNoBoundsCheck(oldOffset, totalSize, out alignmentAdjustment);
			if (num > this.FileView.FileMappingSize || num < 0)
			{
				throw new InvalidOperationException(SR.GetString("CountersOOM"));
			}
			return num;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000DC614 File Offset: 0x000DA814
		private int CalculateMemoryNoBoundsCheck(int oldOffset, int totalSize, out int alignmentAdjustment)
		{
			Thread.MemoryBarrier();
			int num = (int)(this.baseAddress + (long)oldOffset) & 7;
			alignmentAdjustment = (8 - num & 7);
			int num2 = totalSize + alignmentAdjustment;
			return oldOffset + num2;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000DC648 File Offset: 0x000DA848
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
			SharedPerformanceCounter.SafeMarshalCopy(this.categoryName, (IntPtr)num7);
			num7 += (long)num2;
			ptr2->InstanceNameHashCode = instanceNameHashCode;
			ptr2->NextInstanceOffset = 0;
			ptr2->FirstCounterOffset = ptr3 - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
			ptr2->RefCount = 1;
			ptr2->InstanceNameOffset = (int)(num7 - this.baseAddress);
			SharedPerformanceCounter.SafeMarshalCopy(instanceName, (IntPtr)num7);
			num7 += (long)num4;
			string text = (string)this.categoryData.CounterNames[0];
			ptr3->CounterNameHashCode = SharedPerformanceCounter.GetWstrHashCode(text);
			SharedPerformanceCounter.SetValue(ptr3, 0L);
			ptr3->CounterNameOffset = (int)(num7 - this.baseAddress);
			SharedPerformanceCounter.SafeMarshalCopy(text, (IntPtr)num7);
			num7 += (long)((text.Length + 1) * 2);
			for (int j = 1; j < this.categoryData.CounterNames.Count; j++)
			{
				SharedPerformanceCounter.CounterEntry* ptr5 = ptr3;
				text = (string)this.categoryData.CounterNames[j];
				ptr3++;
				ptr3->CounterNameHashCode = SharedPerformanceCounter.GetWstrHashCode(text);
				SharedPerformanceCounter.SetValue(ptr3, 0L);
				ptr3->CounterNameOffset = (int)(num7 - this.baseAddress);
				SharedPerformanceCounter.SafeMarshalCopy(text, (IntPtr)num7);
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

		// Token: 0x060030C7 RID: 12487 RVA: 0x000DC9D0 File Offset: 0x000DABD0
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
			SharedPerformanceCounter.SafeMarshalCopy(instanceName, (IntPtr)num6);
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
					SharedPerformanceCounter.SafeMarshalCopy(text, (IntPtr)num6);
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

		// Token: 0x060030C8 RID: 12488 RVA: 0x000DCD28 File Offset: 0x000DAF28
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
			SharedPerformanceCounter.SafeMarshalCopy(counterName, (IntPtr)num5);
			lastCounterPointer->NextCounterOffset = ptr - this.baseAddress / (long)sizeof(SharedPerformanceCounter.CounterEntry);
			return num3;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000DCDBB File Offset: 0x000DAFBB
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

		// Token: 0x060030CA RID: 12490 RVA: 0x000DCDF8 File Offset: 0x000DAFF8
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

		// Token: 0x060030CB RID: 12491 RVA: 0x000DCE34 File Offset: 0x000DB034
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

		// Token: 0x060030CC RID: 12492 RVA: 0x000DCE69 File Offset: 0x000DB069
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe static void ExitCriticalSection(int* spinLockPointer)
		{
			*spinLockPointer = 0;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000DCE70 File Offset: 0x000DB070
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

		// Token: 0x060030CE RID: 12494 RVA: 0x000DCEA8 File Offset: 0x000DB0A8
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

		// Token: 0x060030CF RID: 12495 RVA: 0x000DCEF4 File Offset: 0x000DB0F4
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

		// Token: 0x060030D0 RID: 12496 RVA: 0x000DCF88 File Offset: 0x000DB188
		private unsafe SharedPerformanceCounter.CategoryData GetCategoryData()
		{
			SharedPerformanceCounter.CategoryData categoryData = (SharedPerformanceCounter.CategoryData)SharedPerformanceCounter.categoryDataTable[this.categoryName];
			if (categoryData == null)
			{
				Hashtable obj = SharedPerformanceCounter.categoryDataTable;
				lock (obj)
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
									byte[] array2;
									byte* value4;
									if ((array2 = array) == null || array2.Length == 0)
									{
										value4 = null;
									}
									else
									{
										value4 = &array2[0];
									}
									int num3 = 0;
									for (int i = 0; i < array.Length - 1; i += 2)
									{
										if (array[i] == 0 && array[i + 1] == 0 && num3 != i)
										{
											string text = new string((sbyte*)value4, num3, i - num3, Encoding.Unicode);
											arrayList.Add(text.ToLowerInvariant());
											num3 = i + 2;
										}
									}
								}
								finally
								{
									byte[] array2 = null;
								}
								categoryData.CounterNames = arrayList;
							}
							else
							{
								string[] array3 = (string[])value3;
								for (int j = 0; j < array3.Length; j++)
								{
									array3[j] = array3[j].ToLowerInvariant();
								}
								categoryData.CounterNames = new ArrayList(array3);
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

		// Token: 0x060030D1 RID: 12497 RVA: 0x000DD2AC File Offset: 0x000DB4AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static int GetFileMappingSizeFromConfig()
		{
			return DiagnosticsConfiguration.PerfomanceCountersFileMappingSize;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000DD2B4 File Offset: 0x000DB4B4
		private static void RemoveCategoryData(string categoryName)
		{
			Hashtable obj = SharedPerformanceCounter.categoryDataTable;
			lock (obj)
			{
				SharedPerformanceCounter.categoryDataTable.Remove(categoryName);
			}
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000DD2F8 File Offset: 0x000DB4F8
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
						bool flag2 = this.FindCounter(wstrHashCode, counterName, ptr2, &ptr);
						return ptr;
					}
				}
				bool flag3;
				while (!this.FindInstance(instanceNameHashCode, instanceName, ptr3, &ptr2, true, lifetime, out flag3))
				{
					SharedPerformanceCounter.InstanceEntry* ptr4 = ptr2;
					bool flag4;
					if (this.categoryData.UseUniqueSharedMemory)
					{
						flag4 = true;
					}
					else
					{
						SharedPerformanceCounter.WaitAndEnterCriticalSection(&ptr4->SpinLock, out flag4);
					}
					if (flag4)
					{
						try
						{
							bool flag5 = false;
							if (enableReuse && flag3)
							{
								flag5 = this.TryReuseInstance(instanceNameHashCode, instanceName, ptr3, &ptr2, lifetime, ptr4);
							}
							if (!flag5)
							{
								int offset2 = this.CreateInstance(ptr3, instanceNameHashCode, instanceName, lifetime);
								ptr2 = this.ResolveOffset(offset2, SharedPerformanceCounter.InstanceEntrySize);
								bool flag2 = this.FindCounter(wstrHashCode, counterName, ptr2, &ptr);
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
					bool flag2 = this.FindCounter(wstrHashCode, counterName, ptr2, &ptr);
					result = ptr;
				}
				else
				{
					while (!this.FindCounter(wstrHashCode, counterName, ptr2, &ptr))
					{
						bool flag6;
						SharedPerformanceCounter.WaitAndEnterCriticalSection(&ptr->SpinLock, out flag6);
						if (flag6)
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

		// Token: 0x060030D4 RID: 12500 RVA: 0x000DD5D4 File Offset: 0x000DB7D4
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

		// Token: 0x060030D5 RID: 12501 RVA: 0x000DD654 File Offset: 0x000DB854
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

		// Token: 0x060030D6 RID: 12502 RVA: 0x000DD6B8 File Offset: 0x000DB8B8
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
					long num3 = DateTime.Now.Ticks - Volatile.Read(ref SharedPerformanceCounter.LastInstanceLifetimeSweepTick);
					if (num3 < 300000000L)
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
						goto IL_31E;
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
				IL_31E:
				*(IntPtr*)returnInstancePointerReference = ptr2;
				result = false;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				if (flag)
				{
					Volatile.Write(ref SharedPerformanceCounter.LastInstanceLifetimeSweepTick, DateTime.Now.Ticks);
				}
			}
			return result;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000DDA44 File Offset: 0x000DBC44
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
								SharedPerformanceCounter.SafeMarshalCopy(instanceName, (IntPtr)num);
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

		// Token: 0x060030D8 RID: 12504 RVA: 0x000DDBA8 File Offset: 0x000DBDA8
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

		// Token: 0x060030D9 RID: 12505 RVA: 0x000DDC08 File Offset: 0x000DBE08
		private unsafe void VerifyCategory(SharedPerformanceCounter.CategoryEntry* currentCategoryPointer)
		{
			int num = *(UIntPtr)this.baseAddress;
			this.ResolveOffset(num, 0);
			int num2 = this.ResolveAddress(currentCategoryPointer, SharedPerformanceCounter.CategoryEntrySize);
			if (num2 >= num)
			{
				currentCategoryPointer->SpinLock = 0;
				currentCategoryPointer->CategoryNameHashCode = 0;
				currentCategoryPointer->CategoryNameOffset = 0;
				currentCategoryPointer->FirstInstanceOffset = 0;
				currentCategoryPointer->NextCategoryOffset = 0;
				currentCategoryPointer->IsConsistent = 0;
				return;
			}
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

		// Token: 0x060030DA RID: 12506 RVA: 0x000DDD00 File Offset: 0x000DBF00
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

		// Token: 0x060030DB RID: 12507 RVA: 0x000DDD50 File Offset: 0x000DBF50
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

		// Token: 0x060030DC RID: 12508 RVA: 0x000DDED0 File Offset: 0x000DC0D0
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

		// Token: 0x060030DD RID: 12509 RVA: 0x000DDEF8 File Offset: 0x000DC0F8
		internal long Increment()
		{
			if (this.counterEntryPointer == null)
			{
				return 0L;
			}
			return SharedPerformanceCounter.IncrementUnaligned(this.counterEntryPointer);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000DDF12 File Offset: 0x000DC112
		internal long Decrement()
		{
			if (this.counterEntryPointer == null)
			{
				return 0L;
			}
			return SharedPerformanceCounter.DecrementUnaligned(this.counterEntryPointer);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000DDF2C File Offset: 0x000DC12C
		internal static void RemoveAllInstances(string categoryName)
		{
			SharedPerformanceCounter sharedPerformanceCounter = new SharedPerformanceCounter(categoryName, null, null);
			sharedPerformanceCounter.RemoveAllInstances();
			SharedPerformanceCounter.RemoveCategoryData(categoryName);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000DDF50 File Offset: 0x000DC150
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

		// Token: 0x060030E1 RID: 12513 RVA: 0x000DDFDC File Offset: 0x000DC1DC
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

		// Token: 0x060030E2 RID: 12514 RVA: 0x000DE190 File Offset: 0x000DC390
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

		// Token: 0x060030E3 RID: 12515 RVA: 0x000DE1F8 File Offset: 0x000DC3F8
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

		// Token: 0x060030E4 RID: 12516 RVA: 0x000DE254 File Offset: 0x000DC454
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

		// Token: 0x060030E5 RID: 12517 RVA: 0x000DE2AA File Offset: 0x000DC4AA
		private unsafe static long DecrementUnaligned(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				return SharedPerformanceCounter.AddToValue(counterEntry, -1L);
			}
			return Interlocked.Decrement(ref counterEntry->Value);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000DE2C8 File Offset: 0x000DC4C8
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

		// Token: 0x060030E7 RID: 12519 RVA: 0x000DE2FE File Offset: 0x000DC4FE
		private unsafe static long IncrementUnaligned(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			if (SharedPerformanceCounter.IsMisaligned(counterEntry))
			{
				return SharedPerformanceCounter.AddToValue(counterEntry, 1L);
			}
			return Interlocked.Increment(ref counterEntry->Value);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000DE31C File Offset: 0x000DC51C
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

		// Token: 0x060030E9 RID: 12521 RVA: 0x000DE351 File Offset: 0x000DC551
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe static bool IsMisaligned(SharedPerformanceCounter.CounterEntry* counterEntry)
		{
			return (counterEntry & 7L) != null;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000DE35C File Offset: 0x000DC55C
		private long ResolveOffset(int offset, int sizeToRead)
		{
			if (offset > this.FileView.FileMappingSize - sizeToRead || offset < 0)
			{
				throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
			}
			return this.baseAddress + (long)offset;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000DE398 File Offset: 0x000DC598
		private int ResolveAddress(long address, int sizeToRead)
		{
			int num = (int)(address - this.baseAddress);
			if (num > this.FileView.FileMappingSize - sizeToRead || num < 0)
			{
				throw new InvalidOperationException(SR.GetString("MappingCorrupted"));
			}
			return num;
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000DE3D4 File Offset: 0x000DC5D4
		private static void SafeMarshalCopy(string str, IntPtr nativePointer)
		{
			char[] array = new char[str.Length + 1];
			str.CopyTo(0, array, 0, str.Length);
			array[str.Length] = '\0';
			Marshal.Copy(array, 0, nativePointer, array.Length);
		}

		// Token: 0x040028C0 RID: 10432
		private const int MaxSpinCount = 5000;

		// Token: 0x040028C1 RID: 10433
		internal const int DefaultCountersFileMappingSize = 524288;

		// Token: 0x040028C2 RID: 10434
		internal const int MaxCountersFileMappingSize = 33554432;

		// Token: 0x040028C3 RID: 10435
		internal const int MinCountersFileMappingSize = 32768;

		// Token: 0x040028C4 RID: 10436
		internal const int InstanceNameMaxLength = 127;

		// Token: 0x040028C5 RID: 10437
		internal const int InstanceNameSlotSize = 256;

		// Token: 0x040028C6 RID: 10438
		internal const string SingleInstanceName = "systemdiagnosticssharedsingleinstance";

		// Token: 0x040028C7 RID: 10439
		internal const string DefaultFileMappingName = "netfxcustomperfcounters.1.0";

		// Token: 0x040028C8 RID: 10440
		internal static readonly int SingleInstanceHashCode = SharedPerformanceCounter.GetWstrHashCode("systemdiagnosticssharedsingleinstance");

		// Token: 0x040028C9 RID: 10441
		private static Hashtable categoryDataTable = new Hashtable(StringComparer.Ordinal);

		// Token: 0x040028CA RID: 10442
		private static readonly int CategoryEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.CategoryEntry));

		// Token: 0x040028CB RID: 10443
		private static readonly int InstanceEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.InstanceEntry));

		// Token: 0x040028CC RID: 10444
		private static readonly int CounterEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.CounterEntry));

		// Token: 0x040028CD RID: 10445
		private static readonly int ProcessLifetimeEntrySize = Marshal.SizeOf(typeof(SharedPerformanceCounter.ProcessLifetimeEntry));

		// Token: 0x040028CE RID: 10446
		private static long LastInstanceLifetimeSweepTick;

		// Token: 0x040028CF RID: 10447
		private const long InstanceLifetimeSweepWindow = 300000000L;

		// Token: 0x040028D0 RID: 10448
		private static volatile ProcessData procData;

		// Token: 0x040028D1 RID: 10449
		internal int InitialOffset = 4;

		// Token: 0x040028D2 RID: 10450
		private SharedPerformanceCounter.CategoryData categoryData;

		// Token: 0x040028D3 RID: 10451
		private long baseAddress;

		// Token: 0x040028D4 RID: 10452
		private unsafe SharedPerformanceCounter.CounterEntry* counterEntryPointer;

		// Token: 0x040028D5 RID: 10453
		private string categoryName;

		// Token: 0x040028D6 RID: 10454
		private int categoryNameHashCode;

		// Token: 0x040028D7 RID: 10455
		private int thisInstanceOffset = -1;

		// Token: 0x02000887 RID: 2183
		private class FileMapping
		{
			// Token: 0x06004588 RID: 17800 RVA: 0x00121ED1 File Offset: 0x001200D1
			public FileMapping(string fileMappingName, int fileMappingSize, int initialOffset)
			{
				this.Initialize(fileMappingName, fileMappingSize, initialOffset);
			}

			// Token: 0x17000FBA RID: 4026
			// (get) Token: 0x06004589 RID: 17801 RVA: 0x00121EE2 File Offset: 0x001200E2
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

			// Token: 0x0600458A RID: 17802 RVA: 0x00121F0C File Offset: 0x0012010C
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
					int num = 14;
					int num2 = 0;
					bool flag = false;
					while (!flag && num > 0)
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
							else
							{
								num--;
								if (num2 == 0)
								{
									num2 = 10;
								}
								else
								{
									Thread.Sleep(num2);
									num2 *= 2;
								}
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

			// Token: 0x0400379E RID: 14238
			internal int FileMappingSize;

			// Token: 0x0400379F RID: 14239
			private SafeFileMapViewHandle fileViewAddress;

			// Token: 0x040037A0 RID: 14240
			private SafeFileMappingHandle fileMappingHandle;
		}

		// Token: 0x02000888 RID: 2184
		private struct CategoryEntry
		{
			// Token: 0x040037A1 RID: 14241
			public int SpinLock;

			// Token: 0x040037A2 RID: 14242
			public int CategoryNameHashCode;

			// Token: 0x040037A3 RID: 14243
			public int CategoryNameOffset;

			// Token: 0x040037A4 RID: 14244
			public int FirstInstanceOffset;

			// Token: 0x040037A5 RID: 14245
			public int NextCategoryOffset;

			// Token: 0x040037A6 RID: 14246
			public int IsConsistent;
		}

		// Token: 0x02000889 RID: 2185
		private struct InstanceEntry
		{
			// Token: 0x040037A7 RID: 14247
			public int SpinLock;

			// Token: 0x040037A8 RID: 14248
			public int InstanceNameHashCode;

			// Token: 0x040037A9 RID: 14249
			public int InstanceNameOffset;

			// Token: 0x040037AA RID: 14250
			public int RefCount;

			// Token: 0x040037AB RID: 14251
			public int FirstCounterOffset;

			// Token: 0x040037AC RID: 14252
			public int NextInstanceOffset;
		}

		// Token: 0x0200088A RID: 2186
		private struct CounterEntry
		{
			// Token: 0x040037AD RID: 14253
			public int SpinLock;

			// Token: 0x040037AE RID: 14254
			public int CounterNameHashCode;

			// Token: 0x040037AF RID: 14255
			public int CounterNameOffset;

			// Token: 0x040037B0 RID: 14256
			public int LifetimeOffset;

			// Token: 0x040037B1 RID: 14257
			public long Value;

			// Token: 0x040037B2 RID: 14258
			public int NextCounterOffset;

			// Token: 0x040037B3 RID: 14259
			public int padding2;
		}

		// Token: 0x0200088B RID: 2187
		private struct CounterEntryMisaligned
		{
			// Token: 0x040037B4 RID: 14260
			public int SpinLock;

			// Token: 0x040037B5 RID: 14261
			public int CounterNameHashCode;

			// Token: 0x040037B6 RID: 14262
			public int CounterNameOffset;

			// Token: 0x040037B7 RID: 14263
			public int LifetimeOffset;

			// Token: 0x040037B8 RID: 14264
			public int Value_lo;

			// Token: 0x040037B9 RID: 14265
			public int Value_hi;

			// Token: 0x040037BA RID: 14266
			public int NextCounterOffset;

			// Token: 0x040037BB RID: 14267
			public int padding2;
		}

		// Token: 0x0200088C RID: 2188
		private struct ProcessLifetimeEntry
		{
			// Token: 0x040037BC RID: 14268
			public int LifetimeType;

			// Token: 0x040037BD RID: 14269
			public int ProcessId;

			// Token: 0x040037BE RID: 14270
			public long StartupTime;
		}

		// Token: 0x0200088D RID: 2189
		private class CategoryData
		{
			// Token: 0x040037BF RID: 14271
			public SharedPerformanceCounter.FileMapping FileMapping;

			// Token: 0x040037C0 RID: 14272
			public bool EnableReuse;

			// Token: 0x040037C1 RID: 14273
			public bool UseUniqueSharedMemory;

			// Token: 0x040037C2 RID: 14274
			public string FileMappingName;

			// Token: 0x040037C3 RID: 14275
			public string MutexName;

			// Token: 0x040037C4 RID: 14276
			public ArrayList CounterNames;
		}
	}
}

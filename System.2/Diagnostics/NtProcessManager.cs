using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004FA RID: 1274
	internal static class NtProcessManager
	{
		// Token: 0x0600304D RID: 12365 RVA: 0x000DA6B8 File Offset: 0x000D88B8
		static NtProcessManager()
		{
			NtProcessManager.valueIds.Add("Handle Count", NtProcessManager.ValueId.HandleCount);
			NtProcessManager.valueIds.Add("Pool Paged Bytes", NtProcessManager.ValueId.PoolPagedBytes);
			NtProcessManager.valueIds.Add("Pool Nonpaged Bytes", NtProcessManager.ValueId.PoolNonpagedBytes);
			NtProcessManager.valueIds.Add("Elapsed Time", NtProcessManager.ValueId.ElapsedTime);
			NtProcessManager.valueIds.Add("Virtual Bytes Peak", NtProcessManager.ValueId.VirtualBytesPeak);
			NtProcessManager.valueIds.Add("Virtual Bytes", NtProcessManager.ValueId.VirtualBytes);
			NtProcessManager.valueIds.Add("Private Bytes", NtProcessManager.ValueId.PrivateBytes);
			NtProcessManager.valueIds.Add("Page File Bytes", NtProcessManager.ValueId.PageFileBytes);
			NtProcessManager.valueIds.Add("Page File Bytes Peak", NtProcessManager.ValueId.PageFileBytesPeak);
			NtProcessManager.valueIds.Add("Working Set Peak", NtProcessManager.ValueId.WorkingSetPeak);
			NtProcessManager.valueIds.Add("Working Set", NtProcessManager.ValueId.WorkingSet);
			NtProcessManager.valueIds.Add("ID Thread", NtProcessManager.ValueId.ThreadId);
			NtProcessManager.valueIds.Add("ID Process", NtProcessManager.ValueId.ProcessId);
			NtProcessManager.valueIds.Add("Priority Base", NtProcessManager.ValueId.BasePriority);
			NtProcessManager.valueIds.Add("Priority Current", NtProcessManager.ValueId.CurrentPriority);
			NtProcessManager.valueIds.Add("% User Time", NtProcessManager.ValueId.UserTime);
			NtProcessManager.valueIds.Add("% Privileged Time", NtProcessManager.ValueId.PrivilegedTime);
			NtProcessManager.valueIds.Add("Start Address", NtProcessManager.ValueId.StartAddress);
			NtProcessManager.valueIds.Add("Thread State", NtProcessManager.ValueId.ThreadState);
			NtProcessManager.valueIds.Add("Thread Wait Reason", NtProcessManager.ValueId.ThreadWaitReason);
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000DA87E File Offset: 0x000D8A7E
		internal static int SystemProcessID
		{
			get
			{
				if (ProcessManager.IsOSOlderThanXP)
				{
					return 8;
				}
				return 4;
			}
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000DA88C File Offset: 0x000D8A8C
		public static int[] GetProcessIds(string machineName, bool isRemoteMachine)
		{
			ProcessInfo[] processInfos = NtProcessManager.GetProcessInfos(machineName, isRemoteMachine);
			int[] array = new int[processInfos.Length];
			for (int i = 0; i < processInfos.Length; i++)
			{
				array[i] = processInfos[i].processId;
			}
			return array;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000DA8C4 File Offset: 0x000D8AC4
		public static int[] GetProcessIds()
		{
			int[] array = new int[256];
			int num;
			while (NativeMethods.EnumProcesses(array, array.Length * 4, out num))
			{
				if (num != array.Length * 4)
				{
					int[] array2 = new int[num / 4];
					Array.Copy(array, array2, array2.Length);
					return array2;
				}
				array = new int[array.Length * 2];
			}
			throw new Win32Exception();
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000DA91A File Offset: 0x000D8B1A
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			return NtProcessManager.GetModuleInfos(processId, false);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000DA924 File Offset: 0x000D8B24
		public static ModuleInfo GetFirstModuleInfo(int processId)
		{
			ModuleInfo[] moduleInfos = NtProcessManager.GetModuleInfos(processId, true);
			if (moduleInfos.Length == 0)
			{
				return null;
			}
			return moduleInfos[0];
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000DA944 File Offset: 0x000D8B44
		private static ModuleInfo[] GetModuleInfos(int processId, bool firstModuleOnly)
		{
			if (processId == NtProcessManager.SystemProcessID || processId == 0)
			{
				throw new Win32Exception(-2147467259, SR.GetString("EnumProcessModuleFailed"));
			}
			SafeProcessHandle safeProcessHandle = SafeProcessHandle.InvalidHandle;
			ModuleInfo[] result;
			try
			{
				safeProcessHandle = ProcessManager.OpenProcess(processId, 1040, true);
				IntPtr[] array = new IntPtr[64];
				GCHandle gchandle = default(GCHandle);
				int num = 0;
				for (;;)
				{
					bool flag = false;
					try
					{
						gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
						flag = NativeMethods.EnumProcessModules(safeProcessHandle, gchandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
						if (!flag)
						{
							bool flag2 = false;
							bool flag3 = false;
							if (!ProcessManager.IsOSOlderThanXP)
							{
								SafeProcessHandle safeProcessHandle2 = SafeProcessHandle.InvalidHandle;
								try
								{
									safeProcessHandle2 = ProcessManager.OpenProcess(NativeMethods.GetCurrentProcessId(), 1024, true);
									if (!SafeNativeMethods.IsWow64Process(safeProcessHandle2, ref flag2))
									{
										throw new Win32Exception();
									}
									if (!SafeNativeMethods.IsWow64Process(safeProcessHandle, ref flag3))
									{
										throw new Win32Exception();
									}
									if (flag2 && !flag3)
									{
										throw new Win32Exception(299, SR.GetString("EnumProcessModuleFailedDueToWow"));
									}
								}
								finally
								{
									if (safeProcessHandle2 != SafeProcessHandle.InvalidHandle)
									{
										safeProcessHandle2.Close();
									}
								}
							}
							for (int i = 0; i < 50; i++)
							{
								flag = NativeMethods.EnumProcessModules(safeProcessHandle, gchandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
								if (flag)
								{
									break;
								}
								Thread.Sleep(1);
							}
						}
					}
					finally
					{
						gchandle.Free();
					}
					if (!flag)
					{
						break;
					}
					num /= IntPtr.Size;
					if (num <= array.Length)
					{
						goto IL_159;
					}
					array = new IntPtr[array.Length * 2];
				}
				throw new Win32Exception();
				IL_159:
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < num; j++)
				{
					try
					{
						ModuleInfo moduleInfo = new ModuleInfo();
						IntPtr handle = array[j];
						NativeMethods.NtModuleInfo ntModuleInfo = new NativeMethods.NtModuleInfo();
						if (!NativeMethods.GetModuleInformation(safeProcessHandle, new HandleRef(null, handle), ntModuleInfo, Marshal.SizeOf(ntModuleInfo)))
						{
							throw new Win32Exception();
						}
						moduleInfo.sizeOfImage = ntModuleInfo.SizeOfImage;
						moduleInfo.entryPoint = ntModuleInfo.EntryPoint;
						moduleInfo.baseOfDll = ntModuleInfo.BaseOfDll;
						StringBuilder stringBuilder = new StringBuilder(1024);
						if (NativeMethods.GetModuleBaseName(safeProcessHandle, new HandleRef(null, handle), stringBuilder, stringBuilder.Capacity * 2) == 0)
						{
							throw new Win32Exception();
						}
						moduleInfo.baseName = stringBuilder.ToString();
						StringBuilder stringBuilder2 = new StringBuilder(1024);
						if (NativeMethods.GetModuleFileNameEx(safeProcessHandle, new HandleRef(null, handle), stringBuilder2, stringBuilder2.Capacity * 2) == 0)
						{
							throw new Win32Exception();
						}
						moduleInfo.fileName = stringBuilder2.ToString();
						if (string.Compare(moduleInfo.fileName, "\\SystemRoot\\System32\\smss.exe", StringComparison.OrdinalIgnoreCase) == 0)
						{
							moduleInfo.fileName = Path.Combine(Environment.SystemDirectory, "smss.exe");
						}
						if (moduleInfo.fileName != null && moduleInfo.fileName.Length >= 4 && moduleInfo.fileName.StartsWith("\\\\?\\", StringComparison.Ordinal))
						{
							moduleInfo.fileName = moduleInfo.fileName.Substring(4);
						}
						arrayList.Add(moduleInfo);
					}
					catch (Win32Exception ex)
					{
						if (ex.NativeErrorCode != 6 && ex.NativeErrorCode != 299)
						{
							throw;
						}
					}
					if (firstModuleOnly)
					{
						break;
					}
				}
				ModuleInfo[] array2 = new ModuleInfo[arrayList.Count];
				arrayList.CopyTo(array2, 0);
				result = array2;
			}
			finally
			{
				if (!safeProcessHandle.IsInvalid)
				{
					safeProcessHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000DACD0 File Offset: 0x000D8ED0
		public static int GetProcessIdFromHandle(SafeProcessHandle processHandle)
		{
			NativeMethods.NtProcessBasicInfo ntProcessBasicInfo = new NativeMethods.NtProcessBasicInfo();
			int num = NativeMethods.NtQueryInformationProcess(processHandle, 0, ntProcessBasicInfo, Marshal.SizeOf(ntProcessBasicInfo), null);
			if (num != 0)
			{
				throw new InvalidOperationException(SR.GetString("CantGetProcessId"), new Win32Exception(num));
			}
			return ntProcessBasicInfo.UniqueProcessId.ToInt32();
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000DAD18 File Offset: 0x000D8F18
		public static ProcessInfo[] GetProcessInfos(string machineName, bool isRemoteMachine)
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			ProcessInfo[] processInfos;
			try
			{
				PerformanceCounterLib performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machineName, new CultureInfo(9));
				processInfos = NtProcessManager.GetProcessInfos(performanceCounterLib);
			}
			catch (Exception ex)
			{
				if (isRemoteMachine)
				{
					throw new InvalidOperationException(SR.GetString("CouldntConnectToRemoteMachine"), ex);
				}
				throw ex;
			}
			return processInfos;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000DAD74 File Offset: 0x000D8F74
		private static ProcessInfo[] GetProcessInfos(PerformanceCounterLib library)
		{
			ProcessInfo[] array = new ProcessInfo[0];
			int num = 5;
			while (array.Length == 0 && num != 0)
			{
				try
				{
					byte[] performanceData = library.GetPerformanceData("230 232");
					array = NtProcessManager.GetProcessInfos(library, 230, 232, performanceData);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(SR.GetString("CouldntGetProcessInfos"), innerException);
				}
				num--;
			}
			if (array.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("ProcessDisabled"));
			}
			return array;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000DADF4 File Offset: 0x000D8FF4
		private static ProcessInfo[] GetProcessInfos(PerformanceCounterLib library, int processIndex, int threadIndex, byte[] data)
		{
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			GCHandle gchandle = default(GCHandle);
			try
			{
				gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
				IntPtr intPtr = gchandle.AddrOfPinnedObject();
				NativeMethods.PERF_DATA_BLOCK.ValidateBeforeRead(intPtr, data.Length, intPtr);
				NativeMethods.PERF_DATA_BLOCK perf_DATA_BLOCK = new NativeMethods.PERF_DATA_BLOCK();
				Marshal.PtrToStructure(intPtr, perf_DATA_BLOCK);
				perf_DATA_BLOCK.Validate(data.Length);
				IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)perf_DATA_BLOCK.HeaderLength);
				NativeMethods.PERF_INSTANCE_DEFINITION perf_INSTANCE_DEFINITION = new NativeMethods.PERF_INSTANCE_DEFINITION();
				NativeMethods.PERF_COUNTER_BLOCK perf_COUNTER_BLOCK = new NativeMethods.PERF_COUNTER_BLOCK();
				for (int i = 0; i < perf_DATA_BLOCK.NumObjectTypes; i++)
				{
					NativeMethods.PERF_OBJECT_TYPE.ValidateBeforeRead(intPtr, data.Length, intPtr2);
					NativeMethods.PERF_OBJECT_TYPE perf_OBJECT_TYPE = new NativeMethods.PERF_OBJECT_TYPE();
					Marshal.PtrToStructure(intPtr2, perf_OBJECT_TYPE);
					perf_OBJECT_TYPE.Validate(data.Length - (int)((long)intPtr2 - (long)intPtr));
					IntPtr intPtr3 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.DefinitionLength);
					IntPtr intPtr4 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.HeaderLength);
					ArrayList arrayList2 = new ArrayList();
					for (int j = 0; j < perf_OBJECT_TYPE.NumCounters; j++)
					{
						NativeMethods.PERF_COUNTER_DEFINITION.ValidateBeforeRead(intPtr, data.Length, intPtr4);
						NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION = new NativeMethods.PERF_COUNTER_DEFINITION();
						Marshal.PtrToStructure(intPtr4, perf_COUNTER_DEFINITION);
						perf_COUNTER_DEFINITION.Validate(data.Length - (int)((long)intPtr4 - (long)intPtr));
						string counterName = library.GetCounterName(perf_COUNTER_DEFINITION.CounterNameTitleIndex);
						if (perf_OBJECT_TYPE.ObjectNameTitleIndex == processIndex)
						{
							perf_COUNTER_DEFINITION.CounterNameTitlePtr = (int)NtProcessManager.GetValueId(counterName);
						}
						else if (perf_OBJECT_TYPE.ObjectNameTitleIndex == threadIndex)
						{
							perf_COUNTER_DEFINITION.CounterNameTitlePtr = (int)NtProcessManager.GetValueId(counterName);
						}
						arrayList2.Add(perf_COUNTER_DEFINITION);
						intPtr4 = (IntPtr)((long)intPtr4 + (long)perf_COUNTER_DEFINITION.ByteLength);
					}
					NativeMethods.PERF_COUNTER_DEFINITION[] array = new NativeMethods.PERF_COUNTER_DEFINITION[arrayList2.Count];
					arrayList2.CopyTo(array, 0);
					for (int k = 0; k < perf_OBJECT_TYPE.NumInstances; k++)
					{
						NativeMethods.PERF_INSTANCE_DEFINITION.ValidateBeforeRead(intPtr, data.Length, intPtr3);
						Marshal.PtrToStructure(intPtr3, perf_INSTANCE_DEFINITION);
						perf_INSTANCE_DEFINITION.Validate(data.Length - (int)((long)intPtr3 - (long)intPtr));
						IntPtr ptr = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.NameOffset);
						string text = Marshal.PtrToStringUni(ptr);
						if (!text.Equals("_Total"))
						{
							IntPtr intPtr5 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength);
							NativeMethods.PERF_COUNTER_BLOCK.ValidateBeforeRead(intPtr, data.Length, intPtr5);
							Marshal.PtrToStructure(intPtr5, perf_COUNTER_BLOCK);
							perf_COUNTER_BLOCK.Validate(data.Length - (int)((long)intPtr5 - (long)intPtr));
							if (perf_OBJECT_TYPE.ObjectNameTitleIndex == processIndex)
							{
								ProcessInfo processInfo = NtProcessManager.GetProcessInfo(perf_OBJECT_TYPE, (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength), array);
								if ((processInfo.processId != 0 || string.Compare(text, "Idle", StringComparison.OrdinalIgnoreCase) == 0) && hashtable[processInfo.processId] == null)
								{
									string text2 = text;
									if (text2.Length == 15)
									{
										if (text.EndsWith(".", StringComparison.Ordinal))
										{
											text2 = text.Substring(0, 14);
										}
										else if (text.EndsWith(".e", StringComparison.Ordinal))
										{
											text2 = text.Substring(0, 13);
										}
										else if (text.EndsWith(".ex", StringComparison.Ordinal))
										{
											text2 = text.Substring(0, 12);
										}
									}
									processInfo.processName = text2;
									hashtable.Add(processInfo.processId, processInfo);
								}
							}
							else if (perf_OBJECT_TYPE.ObjectNameTitleIndex == threadIndex)
							{
								ThreadInfo threadInfo = NtProcessManager.GetThreadInfo(perf_OBJECT_TYPE, (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength), array);
								if (threadInfo.threadId != 0)
								{
									arrayList.Add(threadInfo);
								}
							}
							intPtr3 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength + (long)perf_COUNTER_BLOCK.ByteLength);
						}
					}
					intPtr2 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.TotalByteLength);
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			for (int l = 0; l < arrayList.Count; l++)
			{
				ThreadInfo threadInfo2 = (ThreadInfo)arrayList[l];
				ProcessInfo processInfo2 = (ProcessInfo)hashtable[threadInfo2.processId];
				if (processInfo2 != null)
				{
					processInfo2.threadInfoList.Add(threadInfo2);
				}
			}
			ProcessInfo[] array2 = new ProcessInfo[hashtable.Values.Count];
			hashtable.Values.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000DB280 File Offset: 0x000D9480
		private static ThreadInfo GetThreadInfo(NativeMethods.PERF_OBJECT_TYPE type, IntPtr instancePtr, NativeMethods.PERF_COUNTER_DEFINITION[] counters)
		{
			ThreadInfo threadInfo = new ThreadInfo();
			foreach (NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION in counters)
			{
				long num = NtProcessManager.ReadCounterValue(perf_COUNTER_DEFINITION.CounterType, (IntPtr)((long)instancePtr + (long)perf_COUNTER_DEFINITION.CounterOffset));
				switch (perf_COUNTER_DEFINITION.CounterNameTitlePtr)
				{
				case 11:
					threadInfo.threadId = (int)num;
					break;
				case 12:
					threadInfo.processId = (int)num;
					break;
				case 13:
					threadInfo.basePriority = (int)num;
					break;
				case 14:
					threadInfo.currentPriority = (int)num;
					break;
				case 17:
					threadInfo.startAddress = (IntPtr)num;
					break;
				case 18:
					threadInfo.threadState = (ThreadState)num;
					break;
				case 19:
					threadInfo.threadWaitReason = NtProcessManager.GetThreadWaitReason((int)num);
					break;
				}
			}
			return threadInfo;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000DB350 File Offset: 0x000D9550
		internal static ThreadWaitReason GetThreadWaitReason(int value)
		{
			switch (value)
			{
			case 0:
			case 7:
				return ThreadWaitReason.Executive;
			case 1:
			case 8:
				return ThreadWaitReason.FreePage;
			case 2:
			case 9:
				return ThreadWaitReason.PageIn;
			case 3:
			case 10:
				return ThreadWaitReason.SystemAllocation;
			case 4:
			case 11:
				return ThreadWaitReason.ExecutionDelay;
			case 5:
			case 12:
				return ThreadWaitReason.Suspended;
			case 6:
			case 13:
				return ThreadWaitReason.UserRequest;
			case 14:
				return ThreadWaitReason.EventPairHigh;
			case 15:
				return ThreadWaitReason.EventPairLow;
			case 16:
				return ThreadWaitReason.LpcReceive;
			case 17:
				return ThreadWaitReason.LpcReply;
			case 18:
				return ThreadWaitReason.VirtualMemory;
			case 19:
				return ThreadWaitReason.PageOut;
			default:
				return ThreadWaitReason.Unknown;
			}
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000DB3D8 File Offset: 0x000D95D8
		private static ProcessInfo GetProcessInfo(NativeMethods.PERF_OBJECT_TYPE type, IntPtr instancePtr, NativeMethods.PERF_COUNTER_DEFINITION[] counters)
		{
			ProcessInfo processInfo = new ProcessInfo();
			foreach (NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION in counters)
			{
				long num = NtProcessManager.ReadCounterValue(perf_COUNTER_DEFINITION.CounterType, (IntPtr)((long)instancePtr + (long)perf_COUNTER_DEFINITION.CounterOffset));
				switch (perf_COUNTER_DEFINITION.CounterNameTitlePtr)
				{
				case 0:
					processInfo.handleCount = (int)num;
					break;
				case 1:
					processInfo.poolPagedBytes = num;
					break;
				case 2:
					processInfo.poolNonpagedBytes = num;
					break;
				case 4:
					processInfo.virtualBytesPeak = num;
					break;
				case 5:
					processInfo.virtualBytes = num;
					break;
				case 6:
					processInfo.privateBytes = num;
					break;
				case 7:
					processInfo.pageFileBytes = num;
					break;
				case 8:
					processInfo.pageFileBytesPeak = num;
					break;
				case 9:
					processInfo.workingSetPeak = num;
					break;
				case 10:
					processInfo.workingSet = num;
					break;
				case 12:
					processInfo.processId = (int)num;
					break;
				case 13:
					processInfo.basePriority = (int)num;
					break;
				}
			}
			return processInfo;
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000DB4DC File Offset: 0x000D96DC
		private static NtProcessManager.ValueId GetValueId(string counterName)
		{
			if (counterName != null)
			{
				object obj = NtProcessManager.valueIds[counterName];
				if (obj != null)
				{
					return (NtProcessManager.ValueId)obj;
				}
			}
			return NtProcessManager.ValueId.Unknown;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000DB503 File Offset: 0x000D9703
		private static long ReadCounterValue(int counterType, IntPtr dataPtr)
		{
			if ((counterType & 256) != 0)
			{
				return Marshal.ReadInt64(dataPtr);
			}
			return (long)Marshal.ReadInt32(dataPtr);
		}

		// Token: 0x0400288F RID: 10383
		private const int ProcessPerfCounterId = 230;

		// Token: 0x04002890 RID: 10384
		private const int ThreadPerfCounterId = 232;

		// Token: 0x04002891 RID: 10385
		private const string PerfCounterQueryString = "230 232";

		// Token: 0x04002892 RID: 10386
		internal const int IdleProcessID = 0;

		// Token: 0x04002893 RID: 10387
		private static Hashtable valueIds = new Hashtable();

		// Token: 0x02000883 RID: 2179
		private enum ValueId
		{
			// Token: 0x04003757 RID: 14167
			Unknown = -1,
			// Token: 0x04003758 RID: 14168
			HandleCount,
			// Token: 0x04003759 RID: 14169
			PoolPagedBytes,
			// Token: 0x0400375A RID: 14170
			PoolNonpagedBytes,
			// Token: 0x0400375B RID: 14171
			ElapsedTime,
			// Token: 0x0400375C RID: 14172
			VirtualBytesPeak,
			// Token: 0x0400375D RID: 14173
			VirtualBytes,
			// Token: 0x0400375E RID: 14174
			PrivateBytes,
			// Token: 0x0400375F RID: 14175
			PageFileBytes,
			// Token: 0x04003760 RID: 14176
			PageFileBytesPeak,
			// Token: 0x04003761 RID: 14177
			WorkingSetPeak,
			// Token: 0x04003762 RID: 14178
			WorkingSet,
			// Token: 0x04003763 RID: 14179
			ThreadId,
			// Token: 0x04003764 RID: 14180
			ProcessId,
			// Token: 0x04003765 RID: 14181
			BasePriority,
			// Token: 0x04003766 RID: 14182
			CurrentPriority,
			// Token: 0x04003767 RID: 14183
			UserTime,
			// Token: 0x04003768 RID: 14184
			PrivilegedTime,
			// Token: 0x04003769 RID: 14185
			StartAddress,
			// Token: 0x0400376A RID: 14186
			ThreadState,
			// Token: 0x0400376B RID: 14187
			ThreadWaitReason
		}
	}
}

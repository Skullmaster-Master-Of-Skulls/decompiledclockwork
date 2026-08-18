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
	// Token: 0x02000782 RID: 1922
	internal static class NtProcessManager
	{
		// Token: 0x06003B66 RID: 15206 RVA: 0x000FCCB0 File Offset: 0x000FBCB0
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

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000FCE76 File Offset: 0x000FBE76
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

		// Token: 0x06003B68 RID: 15208 RVA: 0x000FCE84 File Offset: 0x000FBE84
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

		// Token: 0x06003B69 RID: 15209 RVA: 0x000FCEBC File Offset: 0x000FBEBC
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

		// Token: 0x06003B6A RID: 15210 RVA: 0x000FCF12 File Offset: 0x000FBF12
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			return NtProcessManager.GetModuleInfos(processId, false);
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000FCF1C File Offset: 0x000FBF1C
		public static ModuleInfo GetFirstModuleInfo(int processId)
		{
			ModuleInfo[] moduleInfos = NtProcessManager.GetModuleInfos(processId, true);
			if (moduleInfos.Length == 0)
			{
				return null;
			}
			return moduleInfos[0];
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000FCF3C File Offset: 0x000FBF3C
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
						for (int i = 0; i < 10; i++)
						{
							flag = NativeMethods.EnumProcessModules(safeProcessHandle, gchandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
							if (flag)
							{
								break;
							}
							Thread.Sleep(1);
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
						goto IL_B4;
					}
					array = new IntPtr[array.Length * 2];
				}
				throw new Win32Exception();
				IL_B4:
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < num; j++)
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

		// Token: 0x06003B6D RID: 15213 RVA: 0x000FD1D8 File Offset: 0x000FC1D8
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

		// Token: 0x06003B6E RID: 15214 RVA: 0x000FD220 File Offset: 0x000FC220
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
			catch
			{
				if (isRemoteMachine)
				{
					throw new InvalidOperationException(SR.GetString("CouldntConnectToRemoteMachine"));
				}
				throw;
			}
			return processInfos;
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000FD29C File Offset: 0x000FC29C
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
				catch
				{
					throw new InvalidOperationException(SR.GetString("CouldntGetProcessInfos"));
				}
				num--;
			}
			if (array.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("ProcessDisabled"));
			}
			return array;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000FD338 File Offset: 0x000FC338
		private unsafe static ProcessInfo[] GetProcessInfos(PerformanceCounterLib library, int processIndex, int threadIndex, byte[] data)
		{
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			fixed (byte* ptr = data)
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				NativeMethods.PERF_DATA_BLOCK perf_DATA_BLOCK = new NativeMethods.PERF_DATA_BLOCK();
				Marshal.PtrToStructure(intPtr, perf_DATA_BLOCK);
				IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)perf_DATA_BLOCK.HeaderLength);
				NativeMethods.PERF_INSTANCE_DEFINITION perf_INSTANCE_DEFINITION = new NativeMethods.PERF_INSTANCE_DEFINITION();
				NativeMethods.PERF_COUNTER_BLOCK perf_COUNTER_BLOCK = new NativeMethods.PERF_COUNTER_BLOCK();
				for (int i = 0; i < perf_DATA_BLOCK.NumObjectTypes; i++)
				{
					NativeMethods.PERF_OBJECT_TYPE perf_OBJECT_TYPE = new NativeMethods.PERF_OBJECT_TYPE();
					Marshal.PtrToStructure(intPtr2, perf_OBJECT_TYPE);
					IntPtr intPtr3 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.DefinitionLength);
					IntPtr intPtr4 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.HeaderLength);
					ArrayList arrayList2 = new ArrayList();
					for (int j = 0; j < perf_OBJECT_TYPE.NumCounters; j++)
					{
						NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION = new NativeMethods.PERF_COUNTER_DEFINITION();
						Marshal.PtrToStructure(intPtr4, perf_COUNTER_DEFINITION);
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
						Marshal.PtrToStructure(intPtr3, perf_INSTANCE_DEFINITION);
						IntPtr ptr2 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.NameOffset);
						string text = Marshal.PtrToStringUni(ptr2);
						if (!text.Equals("_Total"))
						{
							IntPtr ptr3 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength);
							Marshal.PtrToStructure(ptr3, perf_COUNTER_BLOCK);
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

		// Token: 0x06003B71 RID: 15217 RVA: 0x000FD6EC File Offset: 0x000FC6EC
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

		// Token: 0x06003B72 RID: 15218 RVA: 0x000FD7BC File Offset: 0x000FC7BC
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

		// Token: 0x06003B73 RID: 15219 RVA: 0x000FD844 File Offset: 0x000FC844
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
					processInfo.poolPagedBytes = (long)((int)num);
					break;
				case 2:
					processInfo.poolNonpagedBytes = (long)((int)num);
					break;
				case 4:
					processInfo.virtualBytesPeak = (long)((int)num);
					break;
				case 5:
					processInfo.virtualBytes = (long)((int)num);
					break;
				case 6:
					processInfo.privateBytes = (long)((int)num);
					break;
				case 7:
					processInfo.pageFileBytes = (long)((int)num);
					break;
				case 8:
					processInfo.pageFileBytesPeak = (long)((int)num);
					break;
				case 9:
					processInfo.workingSetPeak = (long)((int)num);
					break;
				case 10:
					processInfo.workingSet = (long)((int)num);
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

		// Token: 0x06003B74 RID: 15220 RVA: 0x000FD958 File Offset: 0x000FC958
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

		// Token: 0x06003B75 RID: 15221 RVA: 0x000FD97F File Offset: 0x000FC97F
		private static long ReadCounterValue(int counterType, IntPtr dataPtr)
		{
			if ((counterType & 256) != 0)
			{
				return Marshal.ReadInt64(dataPtr);
			}
			return (long)Marshal.ReadInt32(dataPtr);
		}

		// Token: 0x040033EE RID: 13294
		private const int ProcessPerfCounterId = 230;

		// Token: 0x040033EF RID: 13295
		private const int ThreadPerfCounterId = 232;

		// Token: 0x040033F0 RID: 13296
		private const string PerfCounterQueryString = "230 232";

		// Token: 0x040033F1 RID: 13297
		internal const int IdleProcessID = 0;

		// Token: 0x040033F2 RID: 13298
		private static Hashtable valueIds = new Hashtable();

		// Token: 0x02000783 RID: 1923
		private enum ValueId
		{
			// Token: 0x040033F4 RID: 13300
			Unknown = -1,
			// Token: 0x040033F5 RID: 13301
			HandleCount,
			// Token: 0x040033F6 RID: 13302
			PoolPagedBytes,
			// Token: 0x040033F7 RID: 13303
			PoolNonpagedBytes,
			// Token: 0x040033F8 RID: 13304
			ElapsedTime,
			// Token: 0x040033F9 RID: 13305
			VirtualBytesPeak,
			// Token: 0x040033FA RID: 13306
			VirtualBytes,
			// Token: 0x040033FB RID: 13307
			PrivateBytes,
			// Token: 0x040033FC RID: 13308
			PageFileBytes,
			// Token: 0x040033FD RID: 13309
			PageFileBytesPeak,
			// Token: 0x040033FE RID: 13310
			WorkingSetPeak,
			// Token: 0x040033FF RID: 13311
			WorkingSet,
			// Token: 0x04003400 RID: 13312
			ThreadId,
			// Token: 0x04003401 RID: 13313
			ProcessId,
			// Token: 0x04003402 RID: 13314
			BasePriority,
			// Token: 0x04003403 RID: 13315
			CurrentPriority,
			// Token: 0x04003404 RID: 13316
			UserTime,
			// Token: 0x04003405 RID: 13317
			PrivilegedTime,
			// Token: 0x04003406 RID: 13318
			StartAddress,
			// Token: 0x04003407 RID: 13319
			ThreadState,
			// Token: 0x04003408 RID: 13320
			ThreadWaitReason
		}
	}
}

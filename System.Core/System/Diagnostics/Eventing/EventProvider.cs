using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing
{
	// Token: 0x020002A8 RID: 680
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventProvider : IDisposable
	{
		// Token: 0x06001894 RID: 6292 RVA: 0x00059AB2 File Offset: 0x00057CB2
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public EventProvider(Guid providerGuid)
		{
			this.m_providerId = providerGuid;
			this.EtwRegister();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00059AC8 File Offset: 0x00057CC8
		[SecurityCritical]
		private void EtwRegister()
		{
			if (EventProvider.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("NotSupported_DownLevelVista"));
			}
			this.m_etwCallback = new UnsafeNativeMethods.EtwEnableCallback(this.EtwEnableCallBack);
			uint num = UnsafeNativeMethods.EventRegister(ref this.m_providerId, this.m_etwCallback, null, ref this.m_regHandle);
			if (num != 0U)
			{
				throw new Win32Exception((int)num);
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00059B22 File Offset: 0x00057D22
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00059B31 File Offset: 0x00057D31
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed == 1)
			{
				return;
			}
			if (Interlocked.Exchange(ref this.m_disposed, 1) != 0)
			{
				return;
			}
			this.m_enabled = 0;
			this.Deregister();
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00059B59 File Offset: 0x00057D59
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00059B64 File Offset: 0x00057D64
		~EventProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00059B94 File Offset: 0x00057D94
		[SecurityCritical]
		private void Deregister()
		{
			if (this.m_regHandle != 0L)
			{
				UnsafeNativeMethods.EventUnregister(this.m_regHandle);
				this.m_regHandle = 0L;
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00059BB2 File Offset: 0x00057DB2
		[SecurityCritical]
		private unsafe void EtwEnableCallBack([In] ref Guid sourceId, [In] int isEnabled, [In] byte setLevel, [In] long anyKeyword, [In] long allKeyword, [In] void* filterData, [In] void* callbackContext)
		{
			this.m_enabled = isEnabled;
			this.m_level = setLevel;
			this.m_anyKeywordMask = anyKeyword;
			this.m_allKeywordMask = allKeyword;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00059BD2 File Offset: 0x00057DD2
		public bool IsEnabled()
		{
			return this.m_enabled != 0;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00059BDF File Offset: 0x00057DDF
		public bool IsEnabled(byte level, long keywords)
		{
			return this.m_enabled != 0 && ((level <= this.m_level || this.m_level == 0) && (keywords == 0L || ((keywords & this.m_anyKeywordMask) != 0L && (keywords & this.m_allKeywordMask) == this.m_allKeywordMask)));
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00059C1C File Offset: 0x00057E1C
		public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
		{
			return EventProvider.t_returnCode;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00059C23 File Offset: 0x00057E23
		private static void SetLastError(int error)
		{
			if (error != 8)
			{
				if (error == 234 || error == 534)
				{
					EventProvider.t_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return;
				}
			}
			else
			{
				EventProvider.t_returnCode = EventProvider.WriteEventErrorCode.NoFreeBuffers;
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00059C48 File Offset: 0x00057E48
		[SecurityCritical]
		private unsafe static string EncodeObject(ref object data, EventProvider.EventData* dataDescriptor, byte* dataBuffer)
		{
			dataDescriptor->Reserved = 0;
			string text = data as string;
			if (text != null)
			{
				dataDescriptor->Size = (uint)((text.Length + 1) * 2);
				return text;
			}
			if (data == null)
			{
				dataDescriptor->Size = 0U;
				dataDescriptor->DataPointer = 0UL;
			}
			else if (data is IntPtr)
			{
				dataDescriptor->Size = (uint)sizeof(IntPtr);
				*(IntPtr*)dataBuffer = (IntPtr)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is int)
			{
				dataDescriptor->Size = 4U;
				*(int*)dataBuffer = (int)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is long)
			{
				dataDescriptor->Size = 8U;
				*(long*)dataBuffer = (long)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is uint)
			{
				dataDescriptor->Size = 4U;
				*(int*)dataBuffer = (int)((uint)data);
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is ulong)
			{
				dataDescriptor->Size = 8U;
				*(long*)dataBuffer = (long)((ulong)data);
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is char)
			{
				dataDescriptor->Size = 2U;
				*(short*)dataBuffer = (short)((char)data);
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is byte)
			{
				dataDescriptor->Size = 1U;
				*dataBuffer = (byte)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is short)
			{
				dataDescriptor->Size = 2U;
				*(short*)dataBuffer = (short)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is sbyte)
			{
				dataDescriptor->Size = 1U;
				*dataBuffer = (byte)((sbyte)data);
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is ushort)
			{
				dataDescriptor->Size = 2U;
				*(short*)dataBuffer = (short)((ushort)data);
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is float)
			{
				dataDescriptor->Size = 4U;
				*(float*)dataBuffer = (float)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is double)
			{
				dataDescriptor->Size = 8U;
				*(double*)dataBuffer = (double)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is bool)
			{
				dataDescriptor->Size = 1U;
				*dataBuffer = (((bool)data) ? 1 : 0);
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is Guid)
			{
				dataDescriptor->Size = (uint)sizeof(Guid);
				*(Guid*)dataBuffer = (Guid)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else if (data is decimal)
			{
				dataDescriptor->Size = 16U;
				*(decimal*)dataBuffer = (decimal)data;
				dataDescriptor->DataPointer = dataBuffer;
			}
			else
			{
				if (!(data is bool))
				{
					text = data.ToString();
					dataDescriptor->Size = (uint)((text.Length + 1) * 2);
					return text;
				}
				dataDescriptor->Size = 1U;
				*dataBuffer = (((bool)data) ? 1 : 0);
				dataDescriptor->DataPointer = dataBuffer;
			}
			return null;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00059F60 File Offset: 0x00058160
		[SecurityCritical]
		public unsafe bool WriteMessageEvent(string eventMessage, byte eventLevel, long eventKeywords)
		{
			if (eventMessage == null)
			{
				throw new ArgumentNullException("eventMessage");
			}
			if (this.IsEnabled(eventLevel, eventKeywords))
			{
				if (eventMessage.Length > 32724)
				{
					EventProvider.t_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				int num;
				fixed (string text = eventMessage)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					num = (int)UnsafeNativeMethods.EventWriteString(this.m_regHandle, eventLevel, eventKeywords, ptr);
				}
				if (num != 0)
				{
					EventProvider.SetLastError(num);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00059FC9 File Offset: 0x000581C9
		public bool WriteMessageEvent(string eventMessage)
		{
			return this.WriteMessageEvent(eventMessage, 0, 0L);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00059FD5 File Offset: 0x000581D5
		public bool WriteEvent(ref EventDescriptor eventDescriptor, params object[] eventPayload)
		{
			return this.WriteTransferEvent(ref eventDescriptor, Guid.Empty, eventPayload);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00059FE4 File Offset: 0x000581E4
		[SecurityCritical]
		public unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, string data)
		{
			uint num = 0U;
			if (data == null)
			{
				throw new ArgumentNullException("dataString");
			}
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				if (data.Length > 32724)
				{
					EventProvider.t_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				EventProvider.EventData eventData;
				eventData.Size = (uint)((data.Length + 1) * 2);
				eventData.Reserved = 0;
				fixed (string text = data)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					Guid activityId = EventProvider.GetActivityId();
					eventData.DataPointer = ptr;
					if (EventProvider.s_preWin7)
					{
						num = UnsafeNativeMethods.EventWrite(this.m_regHandle, ref eventDescriptor, 1U, (void*)(&eventData));
					}
					else
					{
						num = UnsafeNativeMethods.EventWriteTransfer(this.m_regHandle, ref eventDescriptor, (activityId == Guid.Empty) ? null : (&activityId), null, 1U, (void*)(&eventData));
					}
				}
			}
			if (num != 0U)
			{
				EventProvider.SetLastError((int)num);
				return false;
			}
			return true;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005A0B8 File Offset: 0x000582B8
		[SecurityCritical]
		protected unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, int dataCount, IntPtr data)
		{
			uint num;
			if (EventProvider.s_preWin7)
			{
				num = UnsafeNativeMethods.EventWrite(this.m_regHandle, ref eventDescriptor, (uint)dataCount, (void*)data);
			}
			else
			{
				Guid activityId = EventProvider.GetActivityId();
				num = UnsafeNativeMethods.EventWriteTransfer(this.m_regHandle, ref eventDescriptor, (activityId == Guid.Empty) ? null : (&activityId), null, (uint)dataCount, (void*)data);
			}
			if (num != 0U)
			{
				EventProvider.SetLastError((int)num);
				return false;
			}
			return true;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0005A120 File Offset: 0x00058320
		[SecurityCritical]
		public unsafe bool WriteTransferEvent(ref EventDescriptor eventDescriptor, Guid relatedActivityId, params object[] eventPayload)
		{
			uint num = 0U;
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				Guid activityId = EventProvider.GetActivityId();
				int num2 = 0;
				EventProvider.EventData* ptr = null;
				if (eventPayload != null && eventPayload.Length != 0)
				{
					num2 = eventPayload.Length;
					if (num2 > 32)
					{
						throw new ArgumentOutOfRangeException("eventPayload", SR.GetString("ArgumentOutOfRange_MaxArgExceeded", new object[]
						{
							32
						}));
					}
					uint num3 = 0U;
					int num4 = 0;
					int[] array = new int[8];
					string[] array2 = new string[8];
					EventProvider.EventData* ptr2 = stackalloc EventProvider.EventData[checked(unchecked((UIntPtr)num2) * (UIntPtr)sizeof(EventProvider.EventData))];
					ptr = ptr2;
					byte* ptr3 = stackalloc byte[(UIntPtr)(16 * num2)];
					byte* ptr4 = ptr3;
					for (int i = 0; i < eventPayload.Length; i++)
					{
						string text = EventProvider.EncodeObject(ref eventPayload[i], ptr, ptr4);
						ptr4 += 16;
						num3 += ptr->Size;
						ptr++;
						if (text != null)
						{
							if (num4 >= 8)
							{
								throw new ArgumentOutOfRangeException("eventPayload", SR.GetString("ArgumentOutOfRange_MaxStringsExceeded", new object[]
								{
									8
								}));
							}
							array2[num4] = text;
							array[num4] = i;
							num4++;
						}
					}
					if (num3 > 65482U)
					{
						EventProvider.t_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
						return false;
					}
					fixed (string text2 = array2[0])
					{
						char* ptr5 = text2;
						if (ptr5 != null)
						{
							ptr5 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = array2[1])
						{
							char* ptr6 = text3;
							if (ptr6 != null)
							{
								ptr6 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text4 = array2[2])
							{
								char* ptr7 = text4;
								if (ptr7 != null)
								{
									ptr7 += RuntimeHelpers.OffsetToStringData / 2;
								}
								fixed (string text5 = array2[3])
								{
									char* ptr8 = text5;
									if (ptr8 != null)
									{
										ptr8 += RuntimeHelpers.OffsetToStringData / 2;
									}
									fixed (string text6 = array2[4])
									{
										char* ptr9 = text6;
										if (ptr9 != null)
										{
											ptr9 += RuntimeHelpers.OffsetToStringData / 2;
										}
										fixed (string text7 = array2[5])
										{
											char* ptr10 = text7;
											if (ptr10 != null)
											{
												ptr10 += RuntimeHelpers.OffsetToStringData / 2;
											}
											fixed (string text8 = array2[6])
											{
												char* ptr11 = text8;
												if (ptr11 != null)
												{
													ptr11 += RuntimeHelpers.OffsetToStringData / 2;
												}
												fixed (string text9 = array2[7])
												{
													char* ptr12 = text9;
													if (ptr12 != null)
													{
														ptr12 += RuntimeHelpers.OffsetToStringData / 2;
													}
													ptr = ptr2;
													if (array2[0] != null)
													{
														ptr[array[0]].DataPointer = ptr5;
													}
													if (array2[1] != null)
													{
														ptr[array[1]].DataPointer = ptr6;
													}
													if (array2[2] != null)
													{
														ptr[array[2]].DataPointer = ptr7;
													}
													if (array2[3] != null)
													{
														ptr[array[3]].DataPointer = ptr8;
													}
													if (array2[4] != null)
													{
														ptr[array[4]].DataPointer = ptr9;
													}
													if (array2[5] != null)
													{
														ptr[array[5]].DataPointer = ptr10;
													}
													if (array2[6] != null)
													{
														ptr[array[6]].DataPointer = ptr11;
													}
													if (array2[7] != null)
													{
														ptr[array[7]].DataPointer = ptr12;
													}
													text2 = null;
													text3 = null;
													text4 = null;
													text5 = null;
													text6 = null;
													text7 = null;
													text8 = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				if (relatedActivityId == Guid.Empty && EventProvider.s_preWin7)
				{
					num = UnsafeNativeMethods.EventWrite(this.m_regHandle, ref eventDescriptor, (uint)num2, (void*)ptr);
				}
				else
				{
					num = UnsafeNativeMethods.EventWriteTransfer(this.m_regHandle, ref eventDescriptor, (activityId == Guid.Empty) ? null : (&activityId), (relatedActivityId == Guid.Empty && !EventProvider.s_preWin7) ? null : (&relatedActivityId), (uint)num2, (void*)ptr);
				}
			}
			if (num != 0U)
			{
				EventProvider.SetLastError((int)num);
				return false;
			}
			return true;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0005A484 File Offset: 0x00058684
		[SecurityCritical]
		protected unsafe bool WriteTransferEvent(ref EventDescriptor eventDescriptor, Guid relatedActivityId, int dataCount, IntPtr data)
		{
			Guid activityId = EventProvider.GetActivityId();
			uint num = UnsafeNativeMethods.EventWriteTransfer(this.m_regHandle, ref eventDescriptor, (activityId == Guid.Empty) ? null : (&activityId), &relatedActivityId, (uint)dataCount, (void*)data);
			if (num != 0U)
			{
				EventProvider.SetLastError((int)num);
				return false;
			}
			return true;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0005A4D1 File Offset: 0x000586D1
		[SecurityCritical]
		private static Guid GetActivityId()
		{
			return Trace.CorrelationManager.ActivityId;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0005A4DD File Offset: 0x000586DD
		[SecurityCritical]
		public static void SetActivityId(ref Guid id)
		{
			Trace.CorrelationManager.ActivityId = id;
			UnsafeNativeMethods.EventActivityIdControl(2, ref id);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0005A4F8 File Offset: 0x000586F8
		[SecurityCritical]
		public static Guid CreateActivityId()
		{
			Guid result = default(Guid);
			UnsafeNativeMethods.EventActivityIdControl(3, ref result);
			return result;
		}

		// Token: 0x04000BFC RID: 3068
		[SecurityCritical]
		private UnsafeNativeMethods.EtwEnableCallback m_etwCallback;

		// Token: 0x04000BFD RID: 3069
		private long m_regHandle;

		// Token: 0x04000BFE RID: 3070
		private byte m_level;

		// Token: 0x04000BFF RID: 3071
		private long m_anyKeywordMask;

		// Token: 0x04000C00 RID: 3072
		private long m_allKeywordMask;

		// Token: 0x04000C01 RID: 3073
		private int m_enabled;

		// Token: 0x04000C02 RID: 3074
		private Guid m_providerId;

		// Token: 0x04000C03 RID: 3075
		private int m_disposed;

		// Token: 0x04000C04 RID: 3076
		[ThreadStatic]
		private static EventProvider.WriteEventErrorCode t_returnCode;

		// Token: 0x04000C05 RID: 3077
		private static bool s_platformNotSupported = Environment.OSVersion.Version.Major < 6;

		// Token: 0x04000C06 RID: 3078
		private static bool s_preWin7 = Environment.OSVersion.Version.Major < 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 1);

		// Token: 0x04000C07 RID: 3079
		private const int s_basicTypeAllocationBufferSize = 16;

		// Token: 0x04000C08 RID: 3080
		private const int s_etwMaxMumberArguments = 32;

		// Token: 0x04000C09 RID: 3081
		private const int s_etwAPIMaxStringCount = 8;

		// Token: 0x04000C0A RID: 3082
		private const int s_maxEventDataDescriptors = 128;

		// Token: 0x04000C0B RID: 3083
		private const int s_traceEventMaximumSize = 65482;

		// Token: 0x04000C0C RID: 3084
		private const int s_traceEventMaximumStringSize = 32724;

		// Token: 0x02000466 RID: 1126
		public enum WriteEventErrorCode
		{
			// Token: 0x04001329 RID: 4905
			NoError,
			// Token: 0x0400132A RID: 4906
			NoFreeBuffers,
			// Token: 0x0400132B RID: 4907
			EventTooBig
		}

		// Token: 0x02000467 RID: 1127
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		private struct EventData
		{
			// Token: 0x0400132C RID: 4908
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x0400132D RID: 4909
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x0400132E RID: 4910
			[FieldOffset(12)]
			internal int Reserved;
		}

		// Token: 0x02000468 RID: 1128
		private enum ActivityControl : uint
		{
			// Token: 0x04001330 RID: 4912
			EVENT_ACTIVITY_CTRL_GET_ID = 1U,
			// Token: 0x04001331 RID: 4913
			EVENT_ACTIVITY_CTRL_SET_ID,
			// Token: 0x04001332 RID: 4914
			EVENT_ACTIVITY_CTRL_CREATE_ID,
			// Token: 0x04001333 RID: 4915
			EVENT_ACTIVITY_CTRL_GET_SET_ID,
			// Token: 0x04001334 RID: 4916
			EVENT_ACTIVITY_CTRL_CREATE_SET_ID
		}
	}
}

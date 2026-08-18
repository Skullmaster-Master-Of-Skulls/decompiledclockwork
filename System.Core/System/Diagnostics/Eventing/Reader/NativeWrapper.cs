using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C9 RID: 713
	internal class NativeWrapper
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0005D74C File Offset: 0x0005B94C
		[SecurityCritical]
		public static EventLogHandle EvtQuery(EventLogHandle session, string path, string query, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtQuery(session, path, query, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005D788 File Offset: 0x0005B988
		[SecurityCritical]
		public static void EvtSeek(EventLogHandle resultSet, long position, EventLogHandle bookmark, int timeout, UnsafeNativeMethods.EvtSeekFlags flags)
		{
			bool flag = UnsafeNativeMethods.EvtSeek(resultSet, position, bookmark, timeout, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005D7B0 File Offset: 0x0005B9B0
		[SecurityCritical]
		public static EventLogHandle EvtSubscribe(EventLogHandle session, SafeWaitHandle signalEvent, string path, string query, EventLogHandle bookmark, IntPtr context, IntPtr callback, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtSubscribe(session, signalEvent, path, query, bookmark, context, callback, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005D7F4 File Offset: 0x0005B9F4
		[SecurityCritical]
		public static bool EvtNext(EventLogHandle queryHandle, int eventSize, IntPtr[] events, int timeout, int flags, ref int returned)
		{
			bool flag = UnsafeNativeMethods.EvtNext(queryHandle, eventSize, events, timeout, flags, ref returned);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 259)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return lastWin32Error == 0;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005D82C File Offset: 0x0005BA2C
		[SecuritySafeCritical]
		public static void EvtCancel(EventLogHandle handle)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (!UnsafeNativeMethods.EvtCancel(handle))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005D857 File Offset: 0x0005BA57
		[SecurityCritical]
		public static void EvtClose(IntPtr handle)
		{
			UnsafeNativeMethods.EvtClose(handle);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0005D860 File Offset: 0x0005BA60
		[SecurityCritical]
		public static EventLogHandle EvtOpenProviderMetadata(EventLogHandle session, string ProviderId, string logFilePath, int locale, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenPublisherMetadata(session, ProviderId, logFilePath, 0, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0005D89C File Offset: 0x0005BA9C
		[SecurityCritical]
		public static int EvtGetObjectArraySize(EventLogHandle objectArray)
		{
			int result;
			bool flag = UnsafeNativeMethods.EvtGetObjectArraySize(objectArray, out result);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return result;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0005D8C4 File Offset: 0x0005BAC4
		[SecurityCritical]
		public static EventLogHandle EvtOpenEventMetadataEnum(EventLogHandle ProviderMetadata, int flags)
		{
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenEventMetadataEnum(ProviderMetadata, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0005D8F0 File Offset: 0x0005BAF0
		[SecurityCritical]
		public static EventLogHandle EvtNextEventMetadata(EventLogHandle eventMetadataEnum, int flags)
		{
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtNextEventMetadata(eventMetadataEnum, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				if (lastWin32Error != 259)
				{
					EventLogException.Throw(lastWin32Error);
				}
				return null;
			}
			return eventLogHandle;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0005D924 File Offset: 0x0005BB24
		[SecurityCritical]
		public static EventLogHandle EvtOpenChannelEnum(EventLogHandle session, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenChannelEnum(session, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0005D95C File Offset: 0x0005BB5C
		[SecurityCritical]
		public static EventLogHandle EvtOpenProviderEnum(EventLogHandle session, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenPublisherEnum(session, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0005D994 File Offset: 0x0005BB94
		[SecurityCritical]
		public static EventLogHandle EvtOpenChannelConfig(EventLogHandle session, string channelPath, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenChannelConfig(session, channelPath, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		[SecuritySafeCritical]
		public static void EvtSaveChannelConfig(EventLogHandle channelConfig, int flags)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			bool flag = UnsafeNativeMethods.EvtSaveChannelConfig(channelConfig, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0005D9FC File Offset: 0x0005BBFC
		[SecurityCritical]
		public static EventLogHandle EvtOpenLog(EventLogHandle session, string path, PathType flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenLog(session, path, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0005DA34 File Offset: 0x0005BC34
		[SecuritySafeCritical]
		public static void EvtExportLog(EventLogHandle session, string channelPath, string query, string targetFilePath, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			bool flag = UnsafeNativeMethods.EvtExportLog(session, channelPath, query, targetFilePath, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0005DA74 File Offset: 0x0005BC74
		[SecuritySafeCritical]
		public static void EvtArchiveExportedLog(EventLogHandle session, string logFilePath, int locale, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			bool flag = UnsafeNativeMethods.EvtArchiveExportedLog(session, logFilePath, locale, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0005DAB4 File Offset: 0x0005BCB4
		[SecuritySafeCritical]
		public static void EvtClearLog(EventLogHandle session, string channelPath, string targetFilePath, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			bool flag = UnsafeNativeMethods.EvtClearLog(session, channelPath, targetFilePath, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
		[SecurityCritical]
		public static EventLogHandle EvtCreateRenderContext(int valuePathsCount, string[] valuePaths, UnsafeNativeMethods.EvtRenderContextFlags flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtCreateRenderContext(valuePathsCount, valuePaths, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0005DB2C File Offset: 0x0005BD2C
		[SecurityCritical]
		public static void EvtRender(EventLogHandle context, EventLogHandle eventHandle, UnsafeNativeMethods.EvtRenderFlags flags, StringBuilder buffer)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			int capacity;
			int num;
			bool flag = UnsafeNativeMethods.EvtRender(context, eventHandle, flags, buffer.Capacity, buffer, out capacity, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				if (lastWin32Error == 122)
				{
					buffer.Capacity = capacity;
					flag = UnsafeNativeMethods.EvtRender(context, eventHandle, flags, buffer.Capacity, buffer, out capacity, out num);
					lastWin32Error = Marshal.GetLastWin32Error();
				}
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0005DB94 File Offset: 0x0005BD94
		[SecurityCritical]
		public static EventLogHandle EvtOpenSession(UnsafeNativeMethods.EvtLoginClass loginClass, ref UnsafeNativeMethods.EvtRpcLogin login, int timeout, int flags)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtOpenSession(loginClass, ref login, timeout, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0005DBD0 File Offset: 0x0005BDD0
		[SecurityCritical]
		public static EventLogHandle EvtCreateBookmark(string bookmarkXml)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogHandle eventLogHandle = UnsafeNativeMethods.EvtCreateBookmark(bookmarkXml);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (eventLogHandle.IsInvalid)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return eventLogHandle;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0005DC08 File Offset: 0x0005BE08
		[SecurityCritical]
		public static void EvtUpdateBookmark(EventLogHandle bookmark, EventLogHandle eventHandle)
		{
			bool flag = UnsafeNativeMethods.EvtUpdateBookmark(bookmark, eventHandle);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0005DC2C File Offset: 0x0005BE2C
		[SecuritySafeCritical]
		public static object EvtGetEventInfo(EventLogHandle handle, UnsafeNativeMethods.EvtEventPropertyId enumType)
		{
			IntPtr intPtr = IntPtr.Zero;
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			object result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetEventInfo(handle, enumType, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 0 && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetEventInfo(handle, enumType, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0005DCDC File Offset: 0x0005BEDC
		[SecurityCritical]
		public static object EvtGetQueryInfo(EventLogHandle handle, UnsafeNativeMethods.EvtQueryPropertyId enumType)
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = 0;
			object result;
			try
			{
				bool flag = UnsafeNativeMethods.EvtGetQueryInfo(handle, enumType, 0, IntPtr.Zero, ref num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetQueryInfo(handle, enumType, num, intPtr, ref num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0005DD80 File Offset: 0x0005BF80
		[SecuritySafeCritical]
		public static object EvtGetPublisherMetadataProperty(EventLogHandle pmHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId)
		{
			IntPtr intPtr = IntPtr.Zero;
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			object result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetPublisherMetadataProperty(pmHandle, thePropertyId, 0, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetPublisherMetadataProperty(pmHandle, thePropertyId, 0, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0005DE30 File Offset: 0x0005C030
		[SecurityCritical]
		internal static EventLogHandle EvtGetPublisherMetadataPropertyHandle(EventLogHandle pmHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId)
		{
			IntPtr intPtr = IntPtr.Zero;
			EventLogHandle result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetPublisherMetadataProperty(pmHandle, thePropertyId, 0, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetPublisherMetadataProperty(pmHandle, thePropertyId, 0, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToSafeHandle(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0005DED4 File Offset: 0x0005C0D4
		[SecurityCritical]
		public static string EvtFormatMessage(EventLogHandle handle, uint msgId)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			StringBuilder stringBuilder = new StringBuilder(null);
			int num;
			bool flag = UnsafeNativeMethods.EvtFormatMessage(handle, EventLogHandle.Zero, msgId, 0, null, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageId, 0, stringBuilder, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 15029 && lastWin32Error != 15030 && lastWin32Error != 15031)
			{
				if (lastWin32Error <= 15028)
				{
					if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
					{
						goto IL_77;
					}
				}
				else if (lastWin32Error != 15033 && lastWin32Error != 15100)
				{
					goto IL_77;
				}
				return null;
				IL_77:
				if (lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
			stringBuilder.EnsureCapacity(num);
			flag = UnsafeNativeMethods.EvtFormatMessage(handle, EventLogHandle.Zero, msgId, 0, null, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageId, num, stringBuilder, out num);
			lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 15029 && lastWin32Error != 15030 && lastWin32Error != 15031)
			{
				if (lastWin32Error <= 15028)
				{
					if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
					{
						goto IL_ED;
					}
				}
				else if (lastWin32Error != 15033 && lastWin32Error != 15100)
				{
					goto IL_ED;
				}
				return null;
				IL_ED:
				if (lastWin32Error == 15029)
				{
					return null;
				}
				EventLogException.Throw(lastWin32Error);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0005DFE4 File Offset: 0x0005C1E4
		[SecurityCritical]
		public static object EvtGetObjectArrayProperty(EventLogHandle objArrayHandle, int index, int thePropertyId)
		{
			IntPtr intPtr = IntPtr.Zero;
			object result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetObjectArrayProperty(objArrayHandle, thePropertyId, index, 0, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetObjectArrayProperty(objArrayHandle, thePropertyId, index, 0, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0005E08C File Offset: 0x0005C28C
		[SecurityCritical]
		public static object EvtGetEventMetadataProperty(EventLogHandle handle, UnsafeNativeMethods.EvtEventMetadataPropertyId enumType)
		{
			IntPtr intPtr = IntPtr.Zero;
			object result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetEventMetadataProperty(handle, enumType, 0, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetEventMetadataProperty(handle, enumType, 0, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0005E130 File Offset: 0x0005C330
		[SecuritySafeCritical]
		public static object EvtGetChannelConfigProperty(EventLogHandle handle, UnsafeNativeMethods.EvtChannelConfigPropertyId enumType)
		{
			IntPtr intPtr = IntPtr.Zero;
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			object result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetChannelConfigProperty(handle, enumType, 0, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetChannelConfigProperty(handle, enumType, 0, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0005E1E0 File Offset: 0x0005C3E0
		[SecuritySafeCritical]
		public static void EvtSetChannelConfigProperty(EventLogHandle handle, UnsafeNativeMethods.EvtChannelConfigPropertyId enumType, object val)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			UnsafeNativeMethods.EvtVariant evtVariant = default(UnsafeNativeMethods.EvtVariant);
			CoTaskMemSafeHandle coTaskMemSafeHandle = new CoTaskMemSafeHandle();
			using (coTaskMemSafeHandle)
			{
				if (val != null)
				{
					switch (enumType)
					{
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigEnabled:
						evtVariant.Type = 13U;
						if ((bool)val)
						{
							evtVariant.Bool = 1U;
							goto IL_17E;
						}
						evtVariant.Bool = 0U;
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigAccess:
						evtVariant.Type = 1U;
						coTaskMemSafeHandle.SetMemory(Marshal.StringToCoTaskMemAuto((string)val));
						evtVariant.StringVal = coTaskMemSafeHandle.GetMemory();
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigRetention:
						evtVariant.Type = 13U;
						if ((bool)val)
						{
							evtVariant.Bool = 1U;
							goto IL_17E;
						}
						evtVariant.Bool = 0U;
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigAutoBackup:
						evtVariant.Type = 13U;
						if ((bool)val)
						{
							evtVariant.Bool = 1U;
							goto IL_17E;
						}
						evtVariant.Bool = 0U;
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigMaxSize:
						evtVariant.Type = 10U;
						evtVariant.ULong = (ulong)((long)val);
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigLogFilePath:
						evtVariant.Type = 1U;
						coTaskMemSafeHandle.SetMemory(Marshal.StringToCoTaskMemAuto((string)val));
						evtVariant.StringVal = coTaskMemSafeHandle.GetMemory();
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigLevel:
						evtVariant.Type = 8U;
						evtVariant.UInteger = (uint)((int)val);
						goto IL_17E;
					case UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigKeywords:
						evtVariant.Type = 10U;
						evtVariant.ULong = (ulong)((long)val);
						goto IL_17E;
					}
					throw new InvalidOperationException();
				}
				evtVariant.Type = 0U;
				IL_17E:
				bool flag = UnsafeNativeMethods.EvtSetChannelConfigProperty(handle, enumType, 0, ref evtVariant);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0005E3B0 File Offset: 0x0005C5B0
		[SecurityCritical]
		public static string EvtNextChannelPath(EventLogHandle handle, ref bool finish)
		{
			StringBuilder stringBuilder = new StringBuilder(null);
			int num;
			bool flag = UnsafeNativeMethods.EvtNextChannelPath(handle, 0, stringBuilder, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				if (lastWin32Error == 259)
				{
					finish = true;
					return null;
				}
				if (lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
			stringBuilder.EnsureCapacity(num);
			flag = UnsafeNativeMethods.EvtNextChannelPath(handle, num, stringBuilder, out num);
			lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0005E418 File Offset: 0x0005C618
		[SecurityCritical]
		public static string EvtNextPublisherId(EventLogHandle handle, ref bool finish)
		{
			StringBuilder stringBuilder = new StringBuilder(null);
			int num;
			bool flag = UnsafeNativeMethods.EvtNextPublisherId(handle, 0, stringBuilder, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				if (lastWin32Error == 259)
				{
					finish = true;
					return null;
				}
				if (lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
			stringBuilder.EnsureCapacity(num);
			flag = UnsafeNativeMethods.EvtNextPublisherId(handle, num, stringBuilder, out num);
			lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				EventLogException.Throw(lastWin32Error);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0005E480 File Offset: 0x0005C680
		[SecurityCritical]
		public static object EvtGetLogInfo(EventLogHandle handle, UnsafeNativeMethods.EvtLogPropertyId enumType)
		{
			IntPtr intPtr = IntPtr.Zero;
			object result;
			try
			{
				int num;
				bool flag = UnsafeNativeMethods.EvtGetLogInfo(handle, enumType, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtGetLogInfo(handle, enumType, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				UnsafeNativeMethods.EvtVariant val = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr, typeof(UnsafeNativeMethods.EvtVariant));
				result = NativeWrapper.ConvertToObject(val);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0005E524 File Offset: 0x0005C724
		[SecuritySafeCritical]
		public static void EvtRenderBufferWithContextSystem(EventLogHandle contextHandle, EventLogHandle eventHandle, UnsafeNativeMethods.EvtRenderFlags flag, NativeWrapper.SystemProperties systemProperties, int SYSTEM_PROPERTY_COUNT)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			try
			{
				int num;
				int num2;
				if (!UnsafeNativeMethods.EvtRender(contextHandle, eventHandle, flag, 0, IntPtr.Zero, out num, out num2))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 122)
					{
						EventLogException.Throw(lastWin32Error);
					}
				}
				intPtr = Marshal.AllocHGlobal(num);
				bool flag2 = UnsafeNativeMethods.EvtRender(contextHandle, eventHandle, flag, num, intPtr, out num, out num2);
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				if (!flag2)
				{
					EventLogException.Throw(lastWin32Error2);
				}
				if (num2 != SYSTEM_PROPERTY_COUNT)
				{
					throw new InvalidOperationException("We do not have " + SYSTEM_PROPERTY_COUNT.ToString() + " variants given for the  UnsafeNativeMethods.EvtRenderFlags.EvtRenderEventValues flag. (System Properties)");
				}
				intPtr2 = intPtr;
				for (int i = 0; i < num2; i++)
				{
					UnsafeNativeMethods.EvtVariant evtVariant = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr2, typeof(UnsafeNativeMethods.EvtVariant));
					switch (i)
					{
					case 0:
						systemProperties.ProviderName = (string)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeString);
						break;
					case 1:
						systemProperties.ProviderId = (Guid?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeGuid);
						break;
					case 2:
						systemProperties.Id = (ushort?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeUInt16);
						break;
					case 3:
						systemProperties.Qualifiers = (ushort?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeUInt16);
						break;
					case 4:
						systemProperties.Level = (byte?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeByte);
						break;
					case 5:
						systemProperties.Task = (ushort?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeUInt16);
						break;
					case 6:
						systemProperties.Opcode = (byte?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeByte);
						break;
					case 7:
						systemProperties.Keywords = (ulong?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeHexInt64);
						break;
					case 8:
						systemProperties.TimeCreated = (DateTime?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeFileTime);
						break;
					case 9:
						systemProperties.RecordId = (ulong?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeUInt64);
						break;
					case 10:
						systemProperties.ActivityId = (Guid?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeGuid);
						break;
					case 11:
						systemProperties.RelatedActivityId = (Guid?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeGuid);
						break;
					case 12:
						systemProperties.ProcessId = (uint?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeUInt32);
						break;
					case 13:
						systemProperties.ThreadId = (uint?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeUInt32);
						break;
					case 14:
						systemProperties.ChannelName = (string)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeString);
						break;
					case 15:
						systemProperties.ComputerName = (string)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeString);
						break;
					case 16:
						systemProperties.UserId = (SecurityIdentifier)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeSid);
						break;
					case 17:
						systemProperties.Version = (byte?)NativeWrapper.ConvertToObject(evtVariant, UnsafeNativeMethods.EvtVariantType.EvtVarTypeByte);
						break;
					}
					intPtr2 = new IntPtr((long)intPtr2 + (long)Marshal.SizeOf(evtVariant));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0005E82C File Offset: 0x0005CA2C
		[SecuritySafeCritical]
		public static IList<object> EvtRenderBufferWithContextUserOrValues(EventLogHandle contextHandle, EventLogHandle eventHandle)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			UnsafeNativeMethods.EvtRenderFlags flags = UnsafeNativeMethods.EvtRenderFlags.EvtRenderEventValues;
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			IList<object> result;
			try
			{
				int num;
				int num2;
				if (!UnsafeNativeMethods.EvtRender(contextHandle, eventHandle, flags, 0, IntPtr.Zero, out num, out num2))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 122)
					{
						EventLogException.Throw(lastWin32Error);
					}
				}
				intPtr = Marshal.AllocHGlobal(num);
				bool flag = UnsafeNativeMethods.EvtRender(contextHandle, eventHandle, flags, num, intPtr, out num, out num2);
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error2);
				}
				List<object> list = new List<object>(num2);
				if (num2 > 0)
				{
					intPtr2 = intPtr;
					for (int i = 0; i < num2; i++)
					{
						UnsafeNativeMethods.EvtVariant evtVariant = (UnsafeNativeMethods.EvtVariant)Marshal.PtrToStructure(intPtr2, typeof(UnsafeNativeMethods.EvtVariant));
						list.Add(NativeWrapper.ConvertToObject(evtVariant));
						intPtr2 = new IntPtr((long)intPtr2 + (long)Marshal.SizeOf(evtVariant));
					}
				}
				result = list;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0005E934 File Offset: 0x0005CB34
		[SecuritySafeCritical]
		public static string EvtFormatMessageRenderName(EventLogHandle pmHandle, EventLogHandle eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags flag)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			StringBuilder stringBuilder = new StringBuilder(null);
			int num;
			bool flag2 = UnsafeNativeMethods.EvtFormatMessage(pmHandle, eventHandle, 0U, 0, null, flag, 0, stringBuilder, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag2 && lastWin32Error != 15029)
			{
				if (lastWin32Error <= 15028)
				{
					if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
					{
						goto IL_60;
					}
				}
				else if (lastWin32Error != 15033 && lastWin32Error != 15100)
				{
					goto IL_60;
				}
				return null;
				IL_60:
				if (lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
			stringBuilder.EnsureCapacity(num);
			flag2 = UnsafeNativeMethods.EvtFormatMessage(pmHandle, eventHandle, 0U, 0, null, flag, num, stringBuilder, out num);
			lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag2 && lastWin32Error != 15029)
			{
				if (lastWin32Error <= 15028)
				{
					if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
					{
						goto IL_C2;
					}
				}
				else if (lastWin32Error != 15033 && lastWin32Error != 15100)
				{
					goto IL_C2;
				}
				return null;
				IL_C2:
				EventLogException.Throw(lastWin32Error);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0005EA10 File Offset: 0x0005CC10
		[SecuritySafeCritical]
		public static IEnumerable<string> EvtFormatMessageRenderKeywords(EventLogHandle pmHandle, EventLogHandle eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags flag)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			IntPtr intPtr = IntPtr.Zero;
			IEnumerable<string> result;
			try
			{
				List<string> list = new List<string>();
				int num;
				bool flag2 = UnsafeNativeMethods.EvtFormatMessageBuffer(pmHandle, eventHandle, 0U, 0, IntPtr.Zero, flag, 0, IntPtr.Zero, out num);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag2)
				{
					if (lastWin32Error <= 15028)
					{
						if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
						{
							goto IL_76;
						}
					}
					else if (lastWin32Error != 15033 && lastWin32Error != 15100)
					{
						goto IL_76;
					}
					return list.AsReadOnly();
					IL_76:
					if (lastWin32Error != 122)
					{
						EventLogException.Throw(lastWin32Error);
					}
				}
				intPtr = Marshal.AllocHGlobal(num * 2);
				flag2 = UnsafeNativeMethods.EvtFormatMessageBuffer(pmHandle, eventHandle, 0U, 0, IntPtr.Zero, flag, num, intPtr, out num);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag2)
				{
					if (lastWin32Error <= 15028)
					{
						if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
						{
							goto IL_E0;
						}
					}
					else if (lastWin32Error != 15033 && lastWin32Error != 15100)
					{
						goto IL_E0;
					}
					return list;
					IL_E0:
					EventLogException.Throw(lastWin32Error);
				}
				IntPtr intPtr2 = intPtr;
				for (;;)
				{
					string text = Marshal.PtrToStringAuto(intPtr2);
					if (string.IsNullOrEmpty(text))
					{
						break;
					}
					list.Add(text);
					intPtr2 = new IntPtr((long)intPtr2 + (long)(text.Length * 2) + 2L);
				}
				result = list.AsReadOnly();
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0005EB7C File Offset: 0x0005CD7C
		[SecurityCritical]
		public static string EvtRenderBookmark(EventLogHandle eventHandle)
		{
			IntPtr intPtr = IntPtr.Zero;
			UnsafeNativeMethods.EvtRenderFlags flags = UnsafeNativeMethods.EvtRenderFlags.EvtRenderBookmark;
			string result;
			try
			{
				int num;
				int num2;
				bool flag = UnsafeNativeMethods.EvtRender(EventLogHandle.Zero, eventHandle, flags, 0, IntPtr.Zero, out num, out num2);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag && lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
				intPtr = Marshal.AllocHGlobal(num);
				flag = UnsafeNativeMethods.EvtRender(EventLogHandle.Zero, eventHandle, flags, num, intPtr, out num, out num2);
				lastWin32Error = Marshal.GetLastWin32Error();
				if (!flag)
				{
					EventLogException.Throw(lastWin32Error);
				}
				result = Marshal.PtrToStringAuto(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0005EC20 File Offset: 0x0005CE20
		[SecuritySafeCritical]
		public static string EvtFormatMessageFormatDescription(EventLogHandle handle, EventLogHandle eventHandle, string[] values)
		{
			if (NativeWrapper.s_platformNotSupported)
			{
				throw new PlatformNotSupportedException();
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			UnsafeNativeMethods.EvtStringVariant[] array = new UnsafeNativeMethods.EvtStringVariant[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i].Type = 1U;
				array[i].StringVal = values[i];
			}
			StringBuilder stringBuilder = new StringBuilder(null);
			int num;
			bool flag = UnsafeNativeMethods.EvtFormatMessage(handle, eventHandle, uint.MaxValue, values.Length, array, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageEvent, 0, stringBuilder, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 15029)
			{
				if (lastWin32Error <= 15028)
				{
					if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
					{
						goto IL_B0;
					}
				}
				else if (lastWin32Error != 15033 && lastWin32Error != 15100)
				{
					goto IL_B0;
				}
				return null;
				IL_B0:
				if (lastWin32Error != 122)
				{
					EventLogException.Throw(lastWin32Error);
				}
			}
			stringBuilder.EnsureCapacity(num);
			flag = UnsafeNativeMethods.EvtFormatMessage(handle, eventHandle, uint.MaxValue, values.Length, array, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageEvent, num, stringBuilder, out num);
			lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 15029)
			{
				if (lastWin32Error <= 15028)
				{
					if (lastWin32Error != 1815 && lastWin32Error - 15027 > 1)
					{
						goto IL_11D;
					}
				}
				else if (lastWin32Error != 15033 && lastWin32Error != 15100)
				{
					goto IL_11D;
				}
				return null;
				IL_11D:
				EventLogException.Throw(lastWin32Error);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0005ED58 File Offset: 0x0005CF58
		[SecurityCritical]
		private static object ConvertToObject(UnsafeNativeMethods.EvtVariant val)
		{
			uint type = val.Type;
			switch (type)
			{
			case 0U:
				return null;
			case 1U:
				return NativeWrapper.ConvertToString(val);
			case 2U:
				return NativeWrapper.ConvertToAnsiString(val);
			case 3U:
				return val.SByte;
			case 4U:
				return val.UInt8;
			case 5U:
				return val.SByte;
			case 6U:
				return val.UShort;
			case 7U:
				return val.Integer;
			case 8U:
				return val.UInteger;
			case 9U:
				return val.Long;
			case 10U:
				return val.ULong;
			case 11U:
				return val.Single;
			case 12U:
				return val.Double;
			case 13U:
				if (val.Bool != 0U)
				{
					return true;
				}
				return false;
			case 14U:
				break;
			case 15U:
				if (!(val.GuidReference == IntPtr.Zero))
				{
					return Marshal.PtrToStructure(val.GuidReference, typeof(Guid));
				}
				return Guid.Empty;
			case 16U:
				return val.SizeT;
			case 17U:
				return DateTime.FromFileTime((long)val.FileTime);
			case 18U:
			{
				UnsafeNativeMethods.SystemTime systemTime = (UnsafeNativeMethods.SystemTime)Marshal.PtrToStructure(val.SystemTime, typeof(UnsafeNativeMethods.SystemTime));
				return new DateTime((int)systemTime.Year, (int)systemTime.Month, (int)systemTime.Day, (int)systemTime.Hour, (int)systemTime.Minute, (int)systemTime.Second, (int)systemTime.Milliseconds);
			}
			case 19U:
				if (!(val.SidVal == IntPtr.Zero))
				{
					return new SecurityIdentifier(val.SidVal);
				}
				return null;
			case 20U:
				return val.Integer;
			case 21U:
				return val.ULong;
			case 22U:
			case 23U:
			case 24U:
			case 25U:
			case 26U:
			case 27U:
			case 28U:
			case 29U:
			case 30U:
			case 31U:
				goto IL_45B;
			case 32U:
				return NativeWrapper.ConvertToSafeHandle(val);
			default:
				switch (type)
				{
				case 129U:
					return NativeWrapper.ConvertToStringArray(val, false);
				case 130U:
					return NativeWrapper.ConvertToStringArray(val, true);
				case 131U:
					return NativeWrapper.ConvertToArray(val, typeof(sbyte), 1);
				case 132U:
					break;
				case 133U:
				{
					if (val.Reference == IntPtr.Zero)
					{
						return new short[0];
					}
					short[] array = new short[val.Count];
					Marshal.Copy(val.Reference, array, 0, (int)val.Count);
					return array;
				}
				case 134U:
					return NativeWrapper.ConvertToArray(val, typeof(ushort), 2);
				case 135U:
				{
					if (val.Reference == IntPtr.Zero)
					{
						return new int[0];
					}
					int[] array2 = new int[val.Count];
					Marshal.Copy(val.Reference, array2, 0, (int)val.Count);
					return array2;
				}
				case 136U:
				case 148U:
					return NativeWrapper.ConvertToArray(val, typeof(uint), 4);
				case 137U:
				{
					if (val.Reference == IntPtr.Zero)
					{
						return new long[0];
					}
					long[] array3 = new long[val.Count];
					Marshal.Copy(val.Reference, array3, 0, (int)val.Count);
					return array3;
				}
				case 138U:
				case 149U:
					return NativeWrapper.ConvertToArray(val, typeof(ulong), 8);
				case 139U:
				{
					if (val.Reference == IntPtr.Zero)
					{
						return new float[0];
					}
					float[] array4 = new float[val.Count];
					Marshal.Copy(val.Reference, array4, 0, (int)val.Count);
					return array4;
				}
				case 140U:
				{
					if (val.Reference == IntPtr.Zero)
					{
						return new double[0];
					}
					double[] array5 = new double[val.Count];
					Marshal.Copy(val.Reference, array5, 0, (int)val.Count);
					return array5;
				}
				case 141U:
					return NativeWrapper.ConvertToBoolArray(val);
				case 142U:
				case 144U:
				case 147U:
					goto IL_45B;
				case 143U:
					return NativeWrapper.ConvertToArray(val, typeof(Guid), 16);
				case 145U:
					return NativeWrapper.ConvertToFileTimeArray(val);
				case 146U:
					return NativeWrapper.ConvertToSysTimeArray(val);
				default:
					goto IL_45B;
				}
				break;
			}
			if (val.Reference == IntPtr.Zero)
			{
				return new byte[0];
			}
			byte[] array6 = new byte[val.Count];
			Marshal.Copy(val.Reference, array6, 0, (int)val.Count);
			return array6;
			IL_45B:
			throw new EventLogInvalidDataException();
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0005F1C5 File Offset: 0x0005D3C5
		[SecurityCritical]
		public static object ConvertToObject(UnsafeNativeMethods.EvtVariant val, UnsafeNativeMethods.EvtVariantType desiredType)
		{
			if (val.Type == 0U)
			{
				return null;
			}
			if ((ulong)val.Type != (ulong)((long)desiredType))
			{
				throw new EventLogInvalidDataException();
			}
			return NativeWrapper.ConvertToObject(val);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0005F1E8 File Offset: 0x0005D3E8
		[SecurityCritical]
		public static string ConvertToString(UnsafeNativeMethods.EvtVariant val)
		{
			if (val.StringVal == IntPtr.Zero)
			{
				return string.Empty;
			}
			return Marshal.PtrToStringAuto(val.StringVal);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0005F20D File Offset: 0x0005D40D
		[SecurityCritical]
		public static string ConvertToAnsiString(UnsafeNativeMethods.EvtVariant val)
		{
			if (val.AnsiString == IntPtr.Zero)
			{
				return string.Empty;
			}
			return Marshal.PtrToStringAnsi(val.AnsiString);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0005F232 File Offset: 0x0005D432
		[SecurityCritical]
		public static EventLogHandle ConvertToSafeHandle(UnsafeNativeMethods.EvtVariant val)
		{
			if (val.Handle == IntPtr.Zero)
			{
				return EventLogHandle.Zero;
			}
			return new EventLogHandle(val.Handle, true);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0005F258 File Offset: 0x0005D458
		[SecurityCritical]
		public static Array ConvertToArray(UnsafeNativeMethods.EvtVariant val, Type objType, int size)
		{
			IntPtr reference = val.Reference;
			if (reference == IntPtr.Zero)
			{
				return Array.CreateInstance(objType, 0);
			}
			Array array = Array.CreateInstance(objType, new long[]
			{
				(long)((ulong)val.Count)
			});
			int num = 0;
			while ((long)num < (long)((ulong)val.Count))
			{
				array.SetValue(Marshal.PtrToStructure(reference, objType), num);
				reference = new IntPtr((long)reference + (long)size);
				num++;
			}
			return array;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0005F2CC File Offset: 0x0005D4CC
		[SecurityCritical]
		public static Array ConvertToBoolArray(UnsafeNativeMethods.EvtVariant val)
		{
			IntPtr reference = val.Reference;
			if (reference == IntPtr.Zero)
			{
				return new bool[0];
			}
			bool[] array = new bool[val.Count];
			int num = 0;
			while ((long)num < (long)((ulong)val.Count))
			{
				bool flag = Marshal.ReadInt32(reference) != 0;
				array[num] = flag;
				reference = new IntPtr((long)reference + 4L);
				num++;
			}
			return array;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0005F338 File Offset: 0x0005D538
		[SecurityCritical]
		public static Array ConvertToFileTimeArray(UnsafeNativeMethods.EvtVariant val)
		{
			IntPtr reference = val.Reference;
			if (reference == IntPtr.Zero)
			{
				return new DateTime[0];
			}
			DateTime[] array = new DateTime[val.Count];
			int num = 0;
			while ((long)num < (long)((ulong)val.Count))
			{
				array[num] = DateTime.FromFileTime(Marshal.ReadInt64(reference));
				reference = new IntPtr((long)reference + 8L);
				num++;
			}
			return array;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0005F3A4 File Offset: 0x0005D5A4
		[SecurityCritical]
		public static Array ConvertToSysTimeArray(UnsafeNativeMethods.EvtVariant val)
		{
			IntPtr reference = val.Reference;
			if (reference == IntPtr.Zero)
			{
				return new DateTime[0];
			}
			DateTime[] array = new DateTime[val.Count];
			int num = 0;
			while ((long)num < (long)((ulong)val.Count))
			{
				UnsafeNativeMethods.SystemTime systemTime = (UnsafeNativeMethods.SystemTime)Marshal.PtrToStructure(reference, typeof(UnsafeNativeMethods.SystemTime));
				array[num] = new DateTime((int)systemTime.Year, (int)systemTime.Month, (int)systemTime.Day, (int)systemTime.Hour, (int)systemTime.Minute, (int)systemTime.Second, (int)systemTime.Milliseconds);
				reference = new IntPtr((long)reference + 16L);
				num++;
			}
			return array;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0005F44C File Offset: 0x0005D64C
		[SecurityCritical]
		public static string[] ConvertToStringArray(UnsafeNativeMethods.EvtVariant val, bool ansi)
		{
			if (val.Reference == IntPtr.Zero)
			{
				return new string[0];
			}
			IntPtr reference = val.Reference;
			IntPtr[] array = new IntPtr[val.Count];
			Marshal.Copy(reference, array, 0, (int)val.Count);
			string[] array2 = new string[val.Count];
			int num = 0;
			while ((long)num < (long)((ulong)val.Count))
			{
				array2[num] = (ansi ? Marshal.PtrToStringAnsi(array[num]) : Marshal.PtrToStringAuto(array[num]));
				num++;
			}
			return array2;
		}

		// Token: 0x04000CA0 RID: 3232
		private static bool s_platformNotSupported = Environment.OSVersion.Version.Major < 6;

		// Token: 0x02000469 RID: 1129
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public class SystemProperties
		{
			// Token: 0x04001335 RID: 4917
			public bool filled;

			// Token: 0x04001336 RID: 4918
			public ushort? Id;

			// Token: 0x04001337 RID: 4919
			public byte? Version;

			// Token: 0x04001338 RID: 4920
			public ushort? Qualifiers;

			// Token: 0x04001339 RID: 4921
			public byte? Level;

			// Token: 0x0400133A RID: 4922
			public ushort? Task;

			// Token: 0x0400133B RID: 4923
			public byte? Opcode;

			// Token: 0x0400133C RID: 4924
			public ulong? Keywords;

			// Token: 0x0400133D RID: 4925
			public ulong? RecordId;

			// Token: 0x0400133E RID: 4926
			public string ProviderName;

			// Token: 0x0400133F RID: 4927
			public Guid? ProviderId;

			// Token: 0x04001340 RID: 4928
			public string ChannelName;

			// Token: 0x04001341 RID: 4929
			public uint? ProcessId;

			// Token: 0x04001342 RID: 4930
			public uint? ThreadId;

			// Token: 0x04001343 RID: 4931
			public string ComputerName;

			// Token: 0x04001344 RID: 4932
			public SecurityIdentifier UserId;

			// Token: 0x04001345 RID: 4933
			public DateTime? TimeCreated;

			// Token: 0x04001346 RID: 4934
			public Guid? ActivityId;

			// Token: 0x04001347 RID: 4935
			public Guid? RelatedActivityId;
		}
	}
}

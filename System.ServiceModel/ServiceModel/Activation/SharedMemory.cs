using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005CE RID: 1486
	internal class SharedMemory : IDisposable
	{
		// Token: 0x060039BC RID: 14780 RVA: 0x000DEC10 File Offset: 0x000DCE10
		private SharedMemory(SafeFileMappingHandle fileMapping)
		{
			this.fileMapping = fileMapping;
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000DEC20 File Offset: 0x000DCE20
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe static SharedMemory Create(string name, Guid content, List<SecurityIdentifier> allowedSids)
		{
			byte[] array = SecurityDescriptorHelper.FromSecurityIdentifiers(allowedSids, int.MinValue);
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			security_ATTRIBUTES.lpSecurityDescriptor = (IntPtr)((void*)value);
			SafeFileMappingHandle safeFileMappingHandle = UnsafeNativeMethods.CreateFileMapping((IntPtr)(-1), security_ATTRIBUTES, 4, 0, sizeof(SharedMemory.SharedMemoryContents), name);
			int lastWin32Error = Marshal.GetLastWin32Error();
			array2 = null;
			if (safeFileMappingHandle.IsInvalid)
			{
				safeFileMappingHandle.SetHandleAsInvalid();
				safeFileMappingHandle.Close();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
			SharedMemory sharedMemory = new SharedMemory(safeFileMappingHandle);
			SafeViewOfFileHandle safeViewOfFileHandle;
			SharedMemory.GetView(safeFileMappingHandle, true, out safeViewOfFileHandle);
			SharedMemory result;
			try
			{
				SharedMemory.SharedMemoryContents* ptr = (SharedMemory.SharedMemoryContents*)((void*)safeViewOfFileHandle.DangerousGetHandle());
				ptr->pipeGuid = content;
				Thread.MemoryBarrier();
				ptr->isInitialized = true;
				result = sharedMemory;
			}
			finally
			{
				safeViewOfFileHandle.Close();
			}
			return result;
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000DED04 File Offset: 0x000DCF04
		public void Dispose()
		{
			if (this.fileMapping != null)
			{
				this.fileMapping.Close();
				this.fileMapping = null;
			}
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000DED20 File Offset: 0x000DCF20
		private static bool GetView(SafeFileMappingHandle fileMapping, bool writable, out SafeViewOfFileHandle handle)
		{
			handle = UnsafeNativeMethods.MapViewOfFile(fileMapping, writable ? 2 : 4, 0, 0, (IntPtr)sizeof(SharedMemory.SharedMemoryContents));
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!handle.IsInvalid)
			{
				return true;
			}
			handle.SetHandleAsInvalid();
			fileMapping.Close();
			if (!writable && lastWin32Error == 2)
			{
				return false;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000DED80 File Offset: 0x000DCF80
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static string Read(string name)
		{
			string result;
			if (SharedMemory.Read(name, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(2));
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000DEDAC File Offset: 0x000DCFAC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe static bool Read(string name, out string content)
		{
			content = null;
			SafeFileMappingHandle safeFileMappingHandle = UnsafeNativeMethods.OpenFileMapping(4, false, "Global\\" + name);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!safeFileMappingHandle.IsInvalid)
			{
				bool result;
				try
				{
					SafeViewOfFileHandle safeViewOfFileHandle;
					if (!SharedMemory.GetView(safeFileMappingHandle, false, out safeViewOfFileHandle))
					{
						result = false;
					}
					else
					{
						try
						{
							SharedMemory.SharedMemoryContents* ptr = (SharedMemory.SharedMemoryContents*)((void*)safeViewOfFileHandle.DangerousGetHandle());
							content = (ptr->isInitialized ? ptr->pipeGuid.ToString() : null);
							result = true;
						}
						finally
						{
							safeViewOfFileHandle.Close();
						}
					}
				}
				finally
				{
					safeFileMappingHandle.Close();
				}
				return result;
			}
			safeFileMappingHandle.SetHandleAsInvalid();
			safeFileMappingHandle.Close();
			if (lastWin32Error == 2)
			{
				return false;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
		}

		// Token: 0x04002A2F RID: 10799
		private SafeFileMappingHandle fileMapping;

		// Token: 0x02000CBF RID: 3263
		private struct SharedMemoryContents
		{
			// Token: 0x04004588 RID: 17800
			public bool isInitialized;

			// Token: 0x04004589 RID: 17801
			public Guid pipeGuid;
		}
	}
}

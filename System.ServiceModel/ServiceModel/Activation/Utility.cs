using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.ComIntegration;
using System.ServiceModel.Diagnostics;
using System.Text;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005CF RID: 1487
	internal static class Utility
	{
		// Token: 0x060039C2 RID: 14786 RVA: 0x000DEE70 File Offset: 0x000DD070
		internal static Uri FormatListenerEndpoint(string serviceName, string listenerEndPoint)
		{
			return new UriBuilder(Uri.UriSchemeNetPipe, serviceName)
			{
				Path = string.Format(CultureInfo.InvariantCulture, "/{0}/", new object[]
				{
					listenerEndPoint
				})
			}.Uri;
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000DEEB0 File Offset: 0x000DD0B0
		private static SafeCloseHandle OpenCurrentProcessForWrite()
		{
			int id = Process.GetCurrentProcess().Id;
			SafeCloseHandle safeCloseHandle = ListenerUnsafeNativeMethods.OpenProcess(394240, false, id);
			if (safeCloseHandle.IsInvalid)
			{
				Exception exception = new Win32Exception();
				safeCloseHandle.SetHandleAsInvalid();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			return safeCloseHandle;
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000DEEF8 File Offset: 0x000DD0F8
		private static SafeCloseHandle OpenProcessForQuery(int pid)
		{
			SafeCloseHandle safeCloseHandle = ListenerUnsafeNativeMethods.OpenProcess(1024, false, pid);
			if (safeCloseHandle.IsInvalid)
			{
				Exception exception = new Win32Exception();
				safeCloseHandle.SetHandleAsInvalid();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			return safeCloseHandle;
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000DEF34 File Offset: 0x000DD134
		private static SafeCloseHandle GetProcessToken(SafeCloseHandle process, int requiredAccess)
		{
			SafeCloseHandle safeCloseHandle;
			bool flag = ListenerUnsafeNativeMethods.OpenProcessToken(process, requiredAccess, out safeCloseHandle);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
			return safeCloseHandle;
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000DEF6C File Offset: 0x000DD16C
		private static int GetTokenInformationLength(SafeCloseHandle token, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS tic)
		{
			int result;
			if (!ListenerUnsafeNativeMethods.GetTokenInformation(token, tic, null, 0, out result))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 122)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
				}
			}
			return result;
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000DEFA8 File Offset: 0x000DD1A8
		private static void GetTokenInformation(SafeCloseHandle token, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS tic, byte[] tokenInformation)
		{
			int num;
			if (!ListenerUnsafeNativeMethods.GetTokenInformation(token, tic, tokenInformation, tokenInformation.Length, out num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000DEFDC File Offset: 0x000DD1DC
		private static SafeServiceHandle OpenSCManager()
		{
			SafeServiceHandle safeServiceHandle = ListenerUnsafeNativeMethods.OpenSCManager(null, null, 1);
			if (safeServiceHandle.IsInvalid)
			{
				Exception exception = new Win32Exception();
				safeServiceHandle.SetHandleAsInvalid();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			return safeServiceHandle;
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000DF014 File Offset: 0x000DD214
		private static SafeServiceHandle OpenService(SafeServiceHandle scManager, string serviceName, int purpose)
		{
			SafeServiceHandle safeServiceHandle = ListenerUnsafeNativeMethods.OpenService(scManager, serviceName, purpose);
			if (safeServiceHandle.IsInvalid)
			{
				Exception exception = new Win32Exception();
				safeServiceHandle.SetHandleAsInvalid();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			return safeServiceHandle;
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000DF04C File Offset: 0x000DD24C
		internal static void AddRightGrantedToAccounts(List<SecurityIdentifier> accounts, int right, bool onProcess)
		{
			SafeCloseHandle safeCloseHandle = Utility.OpenCurrentProcessForWrite();
			try
			{
				if (onProcess)
				{
					Utility.EditKernelObjectSecurity(safeCloseHandle, accounts, null, right, true);
				}
				else
				{
					SafeCloseHandle processToken = Utility.GetProcessToken(safeCloseHandle, 393224);
					try
					{
						Utility.EditKernelObjectSecurity(processToken, accounts, null, right, true);
					}
					finally
					{
						processToken.Close();
					}
				}
			}
			finally
			{
				safeCloseHandle.Close();
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000DF0B0 File Offset: 0x000DD2B0
		internal static void AddRightGrantedToAccount(SecurityIdentifier account, int right)
		{
			SafeCloseHandle safeCloseHandle = Utility.OpenCurrentProcessForWrite();
			try
			{
				Utility.EditKernelObjectSecurity(safeCloseHandle, null, account, right, true);
			}
			finally
			{
				safeCloseHandle.Close();
			}
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000DF0E8 File Offset: 0x000DD2E8
		internal static void RemoveRightGrantedToAccount(SecurityIdentifier account, int right)
		{
			SafeCloseHandle safeCloseHandle = Utility.OpenCurrentProcessForWrite();
			try
			{
				Utility.EditKernelObjectSecurity(safeCloseHandle, null, account, right, false);
			}
			finally
			{
				safeCloseHandle.Close();
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000DF120 File Offset: 0x000DD320
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		internal unsafe static void KeepOnlyPrivilegeInProcess(string privilege)
		{
			SafeCloseHandle safeCloseHandle = Utility.OpenCurrentProcessForWrite();
			try
			{
				SafeCloseHandle processToken = Utility.GetProcessToken(safeCloseHandle, 131112);
				try
				{
					LUID luid;
					if (!ListenerUnsafeNativeMethods.LookupPrivilegeValue(IntPtr.Zero, privilege, &luid))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
					}
					int tokenInformationLength = Utility.GetTokenInformationLength(processToken, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenPrivileges);
					byte[] array = new byte[tokenInformationLength];
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						Utility.GetTokenInformation(processToken, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenPrivileges, array);
						ListenerUnsafeNativeMethods.TOKEN_PRIVILEGES* ptr2 = (ListenerUnsafeNativeMethods.TOKEN_PRIVILEGES*)ptr;
						LUID_AND_ATTRIBUTES* ptr3 = &ptr2->Privileges;
						int num = 0;
						for (int i = 0; i < ptr2->PrivilegeCount; i++)
						{
							if (!ptr3[i].Luid.Equals(luid))
							{
								ptr3[num].Attributes = PrivilegeAttribute.SE_PRIVILEGE_REMOVED;
								ptr3[num].Luid = ptr3[i].Luid;
								num++;
							}
						}
						ptr2->PrivilegeCount = num;
						bool flag = ListenerUnsafeNativeMethods.AdjustTokenPrivileges(processToken, false, ptr2, array.Length, IntPtr.Zero, IntPtr.Zero);
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						if (!flag || lastWin32Error2 != 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error2));
						}
					}
					finally
					{
						byte[] array2 = null;
					}
				}
				finally
				{
					processToken.Close();
				}
			}
			finally
			{
				safeCloseHandle.Close();
			}
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000DF2D0 File Offset: 0x000DD4D0
		private static void EditKernelObjectSecurity(SafeCloseHandle kernelObject, List<SecurityIdentifier> accounts, SecurityIdentifier account, int right, bool add)
		{
			int binaryLength;
			if (!ListenerUnsafeNativeMethods.GetKernelObjectSecurity(kernelObject, 4, null, 0, out binaryLength))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 122)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
				}
			}
			byte[] array = new byte[binaryLength];
			if (!ListenerUnsafeNativeMethods.GetKernelObjectSecurity(kernelObject, 4, array, array.Length, out binaryLength))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception());
			}
			CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(false, false, array, 0);
			DiscretionaryAcl discretionaryAcl = commonSecurityDescriptor.DiscretionaryAcl;
			if (account != null)
			{
				Utility.EditDacl(discretionaryAcl, account, right, add);
			}
			else if (accounts != null)
			{
				foreach (SecurityIdentifier account2 in accounts)
				{
					Utility.EditDacl(discretionaryAcl, account2, right, add);
				}
			}
			binaryLength = commonSecurityDescriptor.BinaryLength;
			array = new byte[binaryLength];
			commonSecurityDescriptor.GetBinaryForm(array, 0);
			if (!ListenerUnsafeNativeMethods.SetKernelObjectSecurity(kernelObject, 4, array))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception());
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000DF3D8 File Offset: 0x000DD5D8
		private static void EditDacl(DiscretionaryAcl dacl, SecurityIdentifier account, int right, bool add)
		{
			if (add)
			{
				dacl.AddAccess(AccessControlType.Allow, account, right, InheritanceFlags.None, PropagationFlags.None);
				return;
			}
			dacl.RemoveAccess(AccessControlType.Allow, account, right, InheritanceFlags.None, PropagationFlags.None);
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000DF3F8 File Offset: 0x000DD5F8
		internal static SecurityIdentifier GetWindowsServiceSid(string name)
		{
			string accountName = string.Format(CultureInfo.InvariantCulture, "NT Service\\{0}", new object[]
			{
				name
			});
			byte[] array = null;
			uint num = 0U;
			uint capacity = 0U;
			short num2;
			if (!ListenerUnsafeNativeMethods.LookupAccountName(null, accountName, array, ref num, null, ref capacity, out num2))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 122)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
				}
			}
			array = new byte[num];
			StringBuilder referencedDomainName = new StringBuilder((int)capacity);
			if (!ListenerUnsafeNativeMethods.LookupAccountName(null, accountName, array, ref num, referencedDomainName, ref capacity, out num2))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
			return new SecurityIdentifier(array, 0);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000DF499 File Offset: 0x000DD699
		internal static int GetPidForService(string serviceName)
		{
			return Utility.GetStatusForService(serviceName).dwProcessId;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000DF4A8 File Offset: 0x000DD6A8
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		internal unsafe static SecurityIdentifier GetLogonSidForPid(int pid)
		{
			SafeCloseHandle safeCloseHandle = Utility.OpenProcessForQuery(pid);
			SecurityIdentifier result;
			try
			{
				SafeCloseHandle processToken = Utility.GetProcessToken(safeCloseHandle, 8);
				try
				{
					int tokenInformationLength = Utility.GetTokenInformationLength(processToken, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenGroups);
					byte[] array = new byte[tokenInformationLength];
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						Utility.GetTokenInformation(processToken, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenGroups, array);
						ListenerUnsafeNativeMethods.TOKEN_GROUPS* ptr2 = (ListenerUnsafeNativeMethods.TOKEN_GROUPS*)ptr;
						ListenerUnsafeNativeMethods.SID_AND_ATTRIBUTES* ptr3 = (ListenerUnsafeNativeMethods.SID_AND_ATTRIBUTES*)(&ptr2->Groups);
						for (int i = 0; i < ptr2->GroupCount; i++)
						{
							if ((ptr3[i].Attributes & (ListenerUnsafeNativeMethods.SidAttribute)3221225472U) == (ListenerUnsafeNativeMethods.SidAttribute)3221225472U)
							{
								return new SecurityIdentifier(ptr3[i].Sid);
							}
						}
					}
					finally
					{
						byte[] array2 = null;
					}
					result = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
				}
				finally
				{
					processToken.Close();
				}
			}
			finally
			{
				safeCloseHandle.Close();
			}
			return result;
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000DF5A0 File Offset: 0x000DD7A0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		internal unsafe static SecurityIdentifier GetUserSidForPid(int pid)
		{
			SafeCloseHandle safeCloseHandle = Utility.OpenProcessForQuery(pid);
			SecurityIdentifier result;
			try
			{
				SafeCloseHandle processToken = Utility.GetProcessToken(safeCloseHandle, 8);
				try
				{
					int tokenInformationLength = Utility.GetTokenInformationLength(processToken, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenUser);
					byte[] array = new byte[tokenInformationLength];
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						Utility.GetTokenInformation(processToken, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenUser, array);
						ListenerUnsafeNativeMethods.TOKEN_USER* ptr2 = (ListenerUnsafeNativeMethods.TOKEN_USER*)ptr;
						ListenerUnsafeNativeMethods.SID_AND_ATTRIBUTES* ptr3 = (ListenerUnsafeNativeMethods.SID_AND_ATTRIBUTES*)(&ptr2->User);
						result = new SecurityIdentifier(ptr3->Sid);
					}
					finally
					{
						byte[] array2 = null;
					}
				}
				finally
				{
					processToken.Close();
				}
			}
			finally
			{
				safeCloseHandle.Close();
			}
			return result;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000DF64C File Offset: 0x000DD84C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private unsafe static ListenerUnsafeNativeMethods.SERVICE_STATUS_PROCESS GetStatusForService(string serviceName)
		{
			SafeServiceHandle safeServiceHandle = Utility.OpenSCManager();
			ListenerUnsafeNativeMethods.SERVICE_STATUS_PROCESS result;
			try
			{
				SafeServiceHandle safeServiceHandle2 = Utility.OpenService(safeServiceHandle, serviceName, 4);
				try
				{
					int num;
					if (!ListenerUnsafeNativeMethods.QueryServiceStatusEx(safeServiceHandle2, 0, null, 0, out num))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error != 122)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
						}
					}
					byte[] array = new byte[num];
					if (!ListenerUnsafeNativeMethods.QueryServiceStatusEx(safeServiceHandle2, 0, array, array.Length, out num))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception());
					}
					try
					{
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
						result = (ListenerUnsafeNativeMethods.SERVICE_STATUS_PROCESS)Marshal.PtrToStructure((IntPtr)((void*)value), typeof(ListenerUnsafeNativeMethods.SERVICE_STATUS_PROCESS));
					}
					finally
					{
						byte[] array2 = null;
					}
				}
				finally
				{
					safeServiceHandle2.Close();
				}
			}
			finally
			{
				safeServiceHandle.Close();
			}
			return result;
		}

		// Token: 0x04002A30 RID: 10800
		private const string WindowsServiceAccountFormat = "NT Service\\{0}";
	}
}

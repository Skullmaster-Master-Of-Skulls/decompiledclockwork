using System;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001B6 RID: 438
	[Guid("afef65ad-4577-447a-a148-83acadd3d4b9")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class SQLDebugging : ISQLDebug
	{
		// Token: 0x06001AC1 RID: 6849 RVA: 0x000BD1E4 File Offset: 0x000BC5E4
		private IntPtr CreateSD(ref IntPtr pDacl)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			bool flag = false;
			intPtr2 = Marshal.AllocHGlobal(6);
			if (!(intPtr2 == IntPtr.Zero))
			{
				Marshal.WriteInt32(intPtr2, 0, 0);
				Marshal.WriteByte(intPtr2, 4, 0);
				Marshal.WriteByte(intPtr2, 5, 5);
				flag = NativeMethods.AllocateAndInitializeSid(intPtr2, 1, 11, 0, 0, 0, 0, 0, 0, 0, ref zero);
				if (flag && !(zero == IntPtr.Zero))
				{
					flag = NativeMethods.AllocateAndInitializeSid(intPtr2, 2, 32, 544, 0, 0, 0, 0, 0, 0, ref zero2);
					if (flag && !(zero2 == IntPtr.Zero))
					{
						flag = false;
						intPtr = Marshal.AllocHGlobal(20);
						if (!(intPtr == IntPtr.Zero))
						{
							for (int i = 0; i < 20; i++)
							{
								Marshal.WriteByte(intPtr, i, 0);
							}
							int num = 44 + NativeMethods.GetLengthSid(zero) + NativeMethods.GetLengthSid(zero2);
							pDacl = Marshal.AllocHGlobal(num);
							if (!(pDacl == IntPtr.Zero) && NativeMethods.InitializeAcl(pDacl, num, 2) && NativeMethods.AddAccessDeniedAce(pDacl, 2, 262144, zero) && NativeMethods.AddAccessAllowedAce(pDacl, 2, 2147483648U, zero) && NativeMethods.AddAccessAllowedAce(pDacl, 2, 268435456U, zero2) && NativeMethods.InitializeSecurityDescriptor(intPtr, 1) && NativeMethods.SetSecurityDescriptorDacl(intPtr, true, pDacl, false))
							{
								flag = true;
							}
						}
					}
				}
			}
			if (intPtr2 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr2);
			}
			if (zero2 != IntPtr.Zero)
			{
				NativeMethods.FreeSid(zero2);
			}
			if (zero != IntPtr.Zero)
			{
				NativeMethods.FreeSid(zero);
			}
			if (flag)
			{
				return intPtr;
			}
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000BD3A4 File Offset: 0x000BC7A4
		bool ISQLDebug.SQLDebug(int dwpidDebugger, int dwpidDebuggee, [MarshalAs(UnmanagedType.LPStr)] string pszMachineName, [MarshalAs(UnmanagedType.LPStr)] string pszSDIDLLName, int dwOption, int cbData, byte[] rgbData)
		{
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			if (pszMachineName == null || pszSDIDLLName == null)
			{
				return false;
			}
			if (pszMachineName.Length > 32 || pszSDIDLLName.Length > 16)
			{
				return false;
			}
			Encoding encoding = Encoding.GetEncoding(1252);
			byte[] bytes = encoding.GetBytes(pszMachineName);
			byte[] bytes2 = encoding.GetBytes(pszSDIDLLName);
			if (rgbData != null && cbData > 255)
			{
				return false;
			}
			string text;
			if (ADP.IsPlatformNT5)
			{
				text = "Global\\SqlClientSSDebug";
			}
			else
			{
				text = "SqlClientSSDebug";
			}
			text += dwpidDebuggee.ToString(CultureInfo.InvariantCulture);
			intPtr3 = this.CreateSD(ref zero);
			intPtr4 = Marshal.AllocHGlobal(12);
			if (intPtr3 == IntPtr.Zero || intPtr4 == IntPtr.Zero)
			{
				return false;
			}
			Marshal.WriteInt32(intPtr4, 0, 12);
			Marshal.WriteIntPtr(intPtr4, 4, intPtr3);
			Marshal.WriteInt32(intPtr4, 8, 0);
			intPtr = NativeMethods.CreateFileMappingA(ADP.InvalidPtr, intPtr4, 4, 0, Marshal.SizeOf(typeof(MEMMAP)), text);
			if (!(IntPtr.Zero == intPtr))
			{
				intPtr2 = NativeMethods.MapViewOfFile(intPtr, 6, 0, 0, IntPtr.Zero);
				if (!(IntPtr.Zero == intPtr2))
				{
					int num = 0;
					Marshal.WriteInt32(intPtr2, num, dwpidDebugger);
					num += 4;
					Marshal.WriteInt32(intPtr2, num, dwOption);
					num += 4;
					Marshal.Copy(bytes, 0, ADP.IntPtrOffset(intPtr2, num), bytes.Length);
					num += 32;
					Marshal.Copy(bytes2, 0, ADP.IntPtrOffset(intPtr2, num), bytes2.Length);
					num += 16;
					Marshal.WriteInt32(intPtr2, num, cbData);
					num += 4;
					if (rgbData != null)
					{
						Marshal.Copy(rgbData, 0, ADP.IntPtrOffset(intPtr2, num), cbData);
					}
					NativeMethods.UnmapViewOfFile(intPtr2);
					flag = true;
				}
			}
			if (!flag && intPtr != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(intPtr);
			}
			if (intPtr4 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr4);
			}
			if (intPtr3 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr3);
			}
			if (zero != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(zero);
			}
			return flag;
		}

		// Token: 0x04000F67 RID: 3943
		private const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x04000F68 RID: 3944
		private const int DELETE = 65536;

		// Token: 0x04000F69 RID: 3945
		private const int READ_CONTROL = 131072;

		// Token: 0x04000F6A RID: 3946
		private const int WRITE_DAC = 262144;

		// Token: 0x04000F6B RID: 3947
		private const int WRITE_OWNER = 524288;

		// Token: 0x04000F6C RID: 3948
		private const int SYNCHRONIZE = 1048576;

		// Token: 0x04000F6D RID: 3949
		private const int FILE_ALL_ACCESS = 2032127;

		// Token: 0x04000F6E RID: 3950
		private const uint GENERIC_READ = 2147483648U;

		// Token: 0x04000F6F RID: 3951
		private const uint GENERIC_WRITE = 1073741824U;

		// Token: 0x04000F70 RID: 3952
		private const uint GENERIC_EXECUTE = 536870912U;

		// Token: 0x04000F71 RID: 3953
		private const uint GENERIC_ALL = 268435456U;

		// Token: 0x04000F72 RID: 3954
		private const int SECURITY_DESCRIPTOR_REVISION = 1;

		// Token: 0x04000F73 RID: 3955
		private const int ACL_REVISION = 2;

		// Token: 0x04000F74 RID: 3956
		private const int SECURITY_AUTHENTICATED_USER_RID = 11;

		// Token: 0x04000F75 RID: 3957
		private const int SECURITY_LOCAL_SYSTEM_RID = 18;

		// Token: 0x04000F76 RID: 3958
		private const int SECURITY_BUILTIN_DOMAIN_RID = 32;

		// Token: 0x04000F77 RID: 3959
		private const int SECURITY_WORLD_RID = 0;

		// Token: 0x04000F78 RID: 3960
		private const byte SECURITY_NT_AUTHORITY = 5;

		// Token: 0x04000F79 RID: 3961
		private const int DOMAIN_GROUP_RID_ADMINS = 512;

		// Token: 0x04000F7A RID: 3962
		private const int DOMAIN_ALIAS_RID_ADMINS = 544;

		// Token: 0x04000F7B RID: 3963
		private const int sizeofSECURITY_ATTRIBUTES = 12;

		// Token: 0x04000F7C RID: 3964
		private const int sizeofSECURITY_DESCRIPTOR = 20;

		// Token: 0x04000F7D RID: 3965
		private const int sizeofACCESS_ALLOWED_ACE = 12;

		// Token: 0x04000F7E RID: 3966
		private const int sizeofACCESS_DENIED_ACE = 12;

		// Token: 0x04000F7F RID: 3967
		private const int sizeofSID_IDENTIFIER_AUTHORITY = 6;

		// Token: 0x04000F80 RID: 3968
		private const int sizeofACL = 8;
	}
}

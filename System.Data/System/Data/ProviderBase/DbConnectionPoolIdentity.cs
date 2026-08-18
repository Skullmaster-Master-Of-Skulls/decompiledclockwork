using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Data.ProviderBase
{
	// Token: 0x02000278 RID: 632
	[Serializable]
	internal sealed class DbConnectionPoolIdentity
	{
		// Token: 0x06002159 RID: 8537 RVA: 0x002855B8 File Offset: 0x002849B8
		private DbConnectionPoolIdentity(string sidString, bool isRestricted, bool isNetwork)
		{
			this._sidString = sidString;
			this._isRestricted = isRestricted;
			this._isNetwork = isNetwork;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x002855E8 File Offset: 0x002849E8
		internal bool IsRestricted
		{
			get
			{
				return this._isRestricted;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x00285608 File Offset: 0x00284A08
		internal bool IsNetwork
		{
			get
			{
				return this._isNetwork;
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00285628 File Offset: 0x00284A28
		private static byte[] CreateWellKnownSid(WellKnownSidType sidType)
		{
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			byte[] array = new byte[maxBinaryLength];
			if (UnsafeNativeMethods.CreateWellKnownSid((int)sidType, null, array, ref maxBinaryLength) == 0)
			{
				DbConnectionPoolIdentity.IntegratedSecurityError(5);
			}
			return array;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00285658 File Offset: 0x00284A58
		public override bool Equals(object value)
		{
			bool flag = this == DbConnectionPoolIdentity.NoIdentity || this == value;
			if (!flag && value != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = (DbConnectionPoolIdentity)value;
				flag = (this._sidString == dbConnectionPoolIdentity._sidString && this._isRestricted == dbConnectionPoolIdentity._isRestricted && this._isNetwork == dbConnectionPoolIdentity._isNetwork);
			}
			return flag;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x002856B8 File Offset: 0x00284AB8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal static WindowsIdentity GetCurrentWindowsIdentity()
		{
			return WindowsIdentity.GetCurrent();
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x002856D8 File Offset: 0x00284AD8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static IntPtr GetWindowsIdentityToken(WindowsIdentity identity)
		{
			return identity.Token;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x002856F8 File Offset: 0x00284AF8
		internal static DbConnectionPoolIdentity GetCurrent()
		{
			if (!ADP.IsWindowsNT)
			{
				return DbConnectionPoolIdentity.NoIdentity;
			}
			WindowsIdentity currentWindowsIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity();
			IntPtr windowsIdentityToken = DbConnectionPoolIdentity.GetWindowsIdentityToken(currentWindowsIdentity);
			uint num = 2048U;
			uint num2 = 0U;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			UnsafeNativeMethods.SetLastError(0);
			bool isRestricted = UnsafeNativeMethods.IsTokenRestricted(windowsIdentityToken);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 0)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			DbConnectionPoolIdentity result = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				bool isNetwork;
				if (!UnsafeNativeMethods.CheckTokenMembership(windowsIdentityToken, DbConnectionPoolIdentity.NetworkSid, out isNetwork))
				{
					DbConnectionPoolIdentity.IntegratedSecurityError(1);
				}
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = SafeNativeMethods.LocalAlloc(0, (IntPtr)((long)((ulong)num)));
				}
				if (IntPtr.Zero == intPtr)
				{
					throw new OutOfMemoryException();
				}
				if (!UnsafeNativeMethods.GetTokenInformation(windowsIdentityToken, 1U, intPtr, num, ref num2))
				{
					if (num2 > num)
					{
						num = num2;
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							SafeNativeMethods.LocalFree(intPtr);
							intPtr = IntPtr.Zero;
							intPtr = SafeNativeMethods.LocalAlloc(0, (IntPtr)((long)((ulong)num)));
						}
						if (IntPtr.Zero == intPtr)
						{
							throw new OutOfMemoryException();
						}
						if (!UnsafeNativeMethods.GetTokenInformation(windowsIdentityToken, 1U, intPtr, num, ref num2))
						{
							DbConnectionPoolIdentity.IntegratedSecurityError(2);
						}
					}
					else
					{
						DbConnectionPoolIdentity.IntegratedSecurityError(3);
					}
				}
				currentWindowsIdentity.Dispose();
				IntPtr sid = Marshal.ReadIntPtr(intPtr, 0);
				if (!UnsafeNativeMethods.ConvertSidToStringSidW(sid, out zero))
				{
					DbConnectionPoolIdentity.IntegratedSecurityError(4);
				}
				if (IntPtr.Zero == zero)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.ConvertSidToStringSidWReturnedNull);
				}
				string sidString = Marshal.PtrToStringUni(zero);
				result = new DbConnectionPoolIdentity(sidString, isRestricted, isNetwork);
			}
			finally
			{
				if (IntPtr.Zero != intPtr)
				{
					SafeNativeMethods.LocalFree(intPtr);
					intPtr = IntPtr.Zero;
				}
				if (IntPtr.Zero != zero)
				{
					SafeNativeMethods.LocalFree(zero);
					zero = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x002858D8 File Offset: 0x00284CD8
		public override int GetHashCode()
		{
			if (this._sidString == null)
			{
				return 0;
			}
			return this._sidString.GetHashCode();
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00285908 File Offset: 0x00284D08
		private static void IntegratedSecurityError(int caller)
		{
			int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
			if (1 != caller || -2147023587 != hrforLastWin32Error)
			{
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
		}

		// Token: 0x040015B8 RID: 5560
		private const int E_NotImpersonationToken = -2147023587;

		// Token: 0x040015B9 RID: 5561
		private const int Win32_CheckTokenMembership = 1;

		// Token: 0x040015BA RID: 5562
		private const int Win32_GetTokenInformation_1 = 2;

		// Token: 0x040015BB RID: 5563
		private const int Win32_GetTokenInformation_2 = 3;

		// Token: 0x040015BC RID: 5564
		private const int Win32_ConvertSidToStringSidW = 4;

		// Token: 0x040015BD RID: 5565
		private const int Win32_CreateWellKnownSid = 5;

		// Token: 0x040015BE RID: 5566
		public static readonly DbConnectionPoolIdentity NoIdentity = new DbConnectionPoolIdentity(string.Empty, false, true);

		// Token: 0x040015BF RID: 5567
		private static readonly byte[] NetworkSid = ADP.IsWindowsNT ? DbConnectionPoolIdentity.CreateWellKnownSid(WellKnownSidType.NetworkSid) : null;

		// Token: 0x040015C0 RID: 5568
		private readonly string _sidString;

		// Token: 0x040015C1 RID: 5569
		private readonly bool _isRestricted;

		// Token: 0x040015C2 RID: 5570
		private readonly bool _isNetwork;
	}
}

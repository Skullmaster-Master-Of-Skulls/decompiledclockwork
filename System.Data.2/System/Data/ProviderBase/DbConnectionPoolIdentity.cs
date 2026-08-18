using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	internal sealed class DbConnectionPoolIdentity
	{
		// Token: 0x06002B0A RID: 11018 RVA: 0x0011AC84 File Offset: 0x0011A084
		private DbConnectionPoolIdentity(string sidString, bool isRestricted, bool isNetwork)
		{
			this._sidString = sidString;
			this._isRestricted = isRestricted;
			this._isNetwork = isNetwork;
			this._hashCode = ((sidString == null) ? 0 : sidString.GetHashCode());
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x0011ACC0 File Offset: 0x0011A0C0
		internal bool IsRestricted
		{
			get
			{
				return this._isRestricted;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0011ACD4 File Offset: 0x0011A0D4
		internal bool IsNetwork
		{
			get
			{
				return this._isNetwork;
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0011ACE8 File Offset: 0x0011A0E8
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

		// Token: 0x06002B0E RID: 11022 RVA: 0x0011AD18 File Offset: 0x0011A118
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

		// Token: 0x06002B0F RID: 11023 RVA: 0x0011AD78 File Offset: 0x0011A178
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal static WindowsIdentity GetCurrentWindowsIdentity()
		{
			return WindowsIdentity.GetCurrent();
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0011AD8C File Offset: 0x0011A18C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static IntPtr GetWindowsIdentityToken(WindowsIdentity identity)
		{
			return identity.Token;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0011ADA0 File Offset: 0x0011A1A0
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
			bool flag = Win32NativeMethods.IsTokenRestrictedWrapper(windowsIdentityToken);
			DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				bool flag2;
				if (!UnsafeNativeMethods.CheckTokenMembership(windowsIdentityToken, DbConnectionPoolIdentity.NetworkSid, out flag2))
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
				string text = Marshal.PtrToStringUni(zero);
				DbConnectionPoolIdentity lastIdentity = DbConnectionPoolIdentity._lastIdentity;
				if (lastIdentity != null && lastIdentity._sidString == text && lastIdentity._isRestricted == flag && lastIdentity._isNetwork == flag2)
				{
					dbConnectionPoolIdentity = lastIdentity;
				}
				else
				{
					dbConnectionPoolIdentity = new DbConnectionPoolIdentity(text, flag, flag2);
				}
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
			DbConnectionPoolIdentity._lastIdentity = dbConnectionPoolIdentity;
			return dbConnectionPoolIdentity;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x0011AFA0 File Offset: 0x0011A3A0
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0011AFB4 File Offset: 0x0011A3B4
		private static void IntegratedSecurityError(int caller)
		{
			int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
			if (1 != caller || -2147023587 != hrforLastWin32Error)
			{
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
		}

		// Token: 0x04001B84 RID: 7044
		private const int E_NotImpersonationToken = -2147023587;

		// Token: 0x04001B85 RID: 7045
		private const int Win32_CheckTokenMembership = 1;

		// Token: 0x04001B86 RID: 7046
		private const int Win32_GetTokenInformation_1 = 2;

		// Token: 0x04001B87 RID: 7047
		private const int Win32_GetTokenInformation_2 = 3;

		// Token: 0x04001B88 RID: 7048
		private const int Win32_ConvertSidToStringSidW = 4;

		// Token: 0x04001B89 RID: 7049
		private const int Win32_CreateWellKnownSid = 5;

		// Token: 0x04001B8A RID: 7050
		public static readonly DbConnectionPoolIdentity NoIdentity = new DbConnectionPoolIdentity(string.Empty, false, true);

		// Token: 0x04001B8B RID: 7051
		private static readonly byte[] NetworkSid = ADP.IsWindowsNT ? DbConnectionPoolIdentity.CreateWellKnownSid(WellKnownSidType.NetworkSid) : null;

		// Token: 0x04001B8C RID: 7052
		private static DbConnectionPoolIdentity _lastIdentity = null;

		// Token: 0x04001B8D RID: 7053
		private readonly string _sidString;

		// Token: 0x04001B8E RID: 7054
		private readonly bool _isRestricted;

		// Token: 0x04001B8F RID: 7055
		private readonly bool _isNetwork;

		// Token: 0x04001B90 RID: 7056
		private readonly int _hashCode;
	}
}

using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200001C RID: 28
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public static class ProtectedMemory
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004D7C File Offset: 0x00002F7C
		[SecuritySafeCritical]
		public static void Protect(byte[] userData, MemoryProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
			ProtectedMemory.VerifyScope(scope);
			if (userData.Length == 0 || (long)userData.Length % 16L != 0L)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_DpApi_InvalidMemoryLength"));
			}
			try
			{
				int num = CAPI.SystemFunction040(userData, (uint)userData.Length, (uint)scope);
				if (num < 0)
				{
					throw new CryptographicException(CAPI.CAPISafe.LsaNtStatusToWinError(num));
				}
			}
			catch (EntryPointNotFoundException)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004E18 File Offset: 0x00003018
		[SecuritySafeCritical]
		public static void Unprotect(byte[] encryptedData, MemoryProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
			ProtectedMemory.VerifyScope(scope);
			if (encryptedData.Length == 0 || (long)encryptedData.Length % 16L != 0L)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_DpApi_InvalidMemoryLength"));
			}
			try
			{
				int num = CAPI.SystemFunction041(encryptedData, (uint)encryptedData.Length, (uint)scope);
				if (num < 0)
				{
					throw new CryptographicException(CAPI.CAPISafe.LsaNtStatusToWinError(num));
				}
			}
			catch (EntryPointNotFoundException)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004EB4 File Offset: 0x000030B4
		private static void VerifyScope(MemoryProtectionScope scope)
		{
			if (scope != MemoryProtectionScope.SameProcess && scope != MemoryProtectionScope.CrossProcess && scope != MemoryProtectionScope.SameLogon)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)scope
				}));
			}
		}
	}
}

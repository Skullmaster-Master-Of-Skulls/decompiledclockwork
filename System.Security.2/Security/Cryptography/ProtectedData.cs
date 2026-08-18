using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200001B RID: 27
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public static class ProtectedData
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000049EC File Offset: 0x00002BEC
		[SecuritySafeCritical]
		public unsafe static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
			RuntimeHelpers.PrepareConstrainedRegions();
			byte[] result;
			try
			{
				gchandle = GCHandle.Alloc(userData, GCHandleType.Pinned);
				CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB2 = default(CAPI.CRYPTOAPI_BLOB);
				cryptoapi_BLOB2.cbData = (uint)userData.Length;
				cryptoapi_BLOB2.pbData = gchandle.AddrOfPinnedObject();
				CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB3 = default(CAPI.CRYPTOAPI_BLOB);
				if (optionalEntropy != null)
				{
					gchandle2 = GCHandle.Alloc(optionalEntropy, GCHandleType.Pinned);
					cryptoapi_BLOB3.cbData = (uint)optionalEntropy.Length;
					cryptoapi_BLOB3.pbData = gchandle2.AddrOfPinnedObject();
				}
				uint num = 1U;
				if (scope == DataProtectionScope.LocalMachine)
				{
					num |= 4U;
				}
				if (!CAPI.CryptProtectData(new IntPtr((void*)(&cryptoapi_BLOB2)), string.Empty, new IntPtr((void*)(&cryptoapi_BLOB3)), IntPtr.Zero, IntPtr.Zero, num, new IntPtr((void*)(&cryptoapi_BLOB))))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (CAPI.ErrorMayBeCausedByUnloadedProfile(lastWin32Error))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_DpApi_ProfileMayNotBeLoaded"));
					}
					throw new CryptographicException(lastWin32Error);
				}
				else
				{
					if (cryptoapi_BLOB.pbData == IntPtr.Zero)
					{
						throw new OutOfMemoryException();
					}
					byte[] array = new byte[cryptoapi_BLOB.cbData];
					Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, array.Length);
					result = array;
				}
			}
			catch (EntryPointNotFoundException)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (cryptoapi_BLOB.pbData != IntPtr.Zero)
				{
					CAPI.CAPISafe.ZeroMemory(cryptoapi_BLOB.pbData, cryptoapi_BLOB.cbData);
					CAPI.CAPISafe.LocalFree(cryptoapi_BLOB.pbData);
				}
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004BD0 File Offset: 0x00002DD0
		[SecuritySafeCritical]
		public unsafe static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
			RuntimeHelpers.PrepareConstrainedRegions();
			byte[] result;
			try
			{
				gchandle = GCHandle.Alloc(encryptedData, GCHandleType.Pinned);
				CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB2 = default(CAPI.CRYPTOAPI_BLOB);
				cryptoapi_BLOB2.cbData = (uint)encryptedData.Length;
				cryptoapi_BLOB2.pbData = gchandle.AddrOfPinnedObject();
				CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB3 = default(CAPI.CRYPTOAPI_BLOB);
				if (optionalEntropy != null)
				{
					gchandle2 = GCHandle.Alloc(optionalEntropy, GCHandleType.Pinned);
					cryptoapi_BLOB3.cbData = (uint)optionalEntropy.Length;
					cryptoapi_BLOB3.pbData = gchandle2.AddrOfPinnedObject();
				}
				uint num = 1U;
				if (scope == DataProtectionScope.LocalMachine)
				{
					num |= 4U;
				}
				if (!CAPI.CryptUnprotectData(new IntPtr((void*)(&cryptoapi_BLOB2)), IntPtr.Zero, new IntPtr((void*)(&cryptoapi_BLOB3)), IntPtr.Zero, IntPtr.Zero, num, new IntPtr((void*)(&cryptoapi_BLOB))))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				if (cryptoapi_BLOB.pbData == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				byte[] array = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, array.Length);
				result = array;
			}
			catch (EntryPointNotFoundException)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_PlatformRequiresNT"));
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (cryptoapi_BLOB.pbData != IntPtr.Zero)
				{
					CAPI.CAPISafe.ZeroMemory(cryptoapi_BLOB.pbData, cryptoapi_BLOB.cbData);
					CAPI.CAPISafe.LocalFree(cryptoapi_BLOB.pbData);
				}
			}
			return result;
		}
	}
}

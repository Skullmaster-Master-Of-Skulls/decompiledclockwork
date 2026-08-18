using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000E5 RID: 229
	internal sealed class BCryptHashAlgorithm : IDisposable
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00016E20 File Offset: 0x00015020
		[SecuritySafeCritical]
		public BCryptHashAlgorithm(CngAlgorithm algorithm, string implementation)
		{
			if (!BCryptNative.BCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			if (BCryptHashAlgorithm._algorithmCache == null)
			{
				BCryptHashAlgorithm._algorithmCache = new BCryptAlgorithmHandleCache();
			}
			this.m_algorithmHandle = BCryptHashAlgorithm._algorithmCache.GetCachedAlgorithmHandle(algorithm.Algorithm, implementation);
			this.Initialize();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00016E78 File Offset: 0x00015078
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.m_hashHandle != null)
			{
				this.m_hashHandle.Dispose();
			}
			if (this.m_algorithmHandle != null)
			{
				this.m_algorithmHandle = null;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00016E9C File Offset: 0x0001509C
		[SecuritySafeCritical]
		public void Initialize()
		{
			SafeBCryptHashHandle safeBCryptHashHandle = null;
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				int int32Property = BCryptNative.GetInt32Property<SafeBCryptAlgorithmHandle>(this.m_algorithmHandle, "ObjectLength");
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Marshal.AllocCoTaskMem(int32Property);
				}
				BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptCreateHash(this.m_algorithmHandle, out safeBCryptHashHandle, intPtr, int32Property, IntPtr.Zero, 0, 0);
				if (errorCode != BCryptNative.ErrorCode.Success)
				{
					throw new CryptographicException((int)errorCode);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					if (safeBCryptHashHandle != null)
					{
						safeBCryptHashHandle.HashObject = intPtr;
					}
					else
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
			if (this.m_hashHandle != null)
			{
				this.m_hashHandle.Dispose();
			}
			this.m_hashHandle = safeBCryptHashHandle;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00016F50 File Offset: 0x00015150
		[SecuritySafeCritical]
		public unsafe void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (ibStart < 0 || ibStart > array.Length - cbSize)
			{
				throw new ArgumentOutOfRangeException("ibStart");
			}
			if (cbSize < 0 || cbSize > array.Length)
			{
				throw new ArgumentOutOfRangeException("cbSize");
			}
			if (cbSize == 0)
			{
				return;
			}
			BCryptNative.ErrorCode errorCode;
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				errorCode = BCryptNative.UnsafeNativeMethods.BCryptHashData(this.m_hashHandle, ptr + ibStart, cbSize, 0);
			}
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00016FD0 File Offset: 0x000151D0
		[SecuritySafeCritical]
		public byte[] HashFinal()
		{
			int int32Property = BCryptNative.GetInt32Property<SafeBCryptHashHandle>(this.m_hashHandle, "HashDigestLength");
			byte[] array = new byte[int32Property];
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptFinishHash(this.m_hashHandle, array, array.Length, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00017014 File Offset: 0x00015214
		[SecuritySafeCritical]
		public void HashStream(Stream stream)
		{
			byte[] array = new byte[4096];
			int num;
			do
			{
				num = stream.Read(array, 0, array.Length);
				if (num > 0)
				{
					this.HashCore(array, 0, num);
				}
			}
			while (num > 0);
		}

		// Token: 0x040005F6 RID: 1526
		[ThreadStatic]
		[SecurityCritical]
		private static BCryptAlgorithmHandleCache _algorithmCache;

		// Token: 0x040005F7 RID: 1527
		[SecurityCritical]
		private SafeBCryptAlgorithmHandle m_algorithmHandle;

		// Token: 0x040005F8 RID: 1528
		[SecurityCritical]
		private SafeBCryptHashHandle m_hashHandle;
	}
}

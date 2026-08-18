using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000F0 RID: 240
	internal sealed class CapiHashAlgorithm : IDisposable
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x000188F6 File Offset: 0x00016AF6
		[SecuritySafeCritical]
		public CapiHashAlgorithm(string provider, CapiNative.ProviderType providerType, CapiNative.AlgorithmId algorithm)
		{
			this.m_algorithmId = algorithm;
			this.m_cspHandle = CapiNative.AcquireCsp(null, provider, providerType, CapiNative.CryptAcquireContextFlags.VerifyContext, true);
			this.Initialize();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001891F File Offset: 0x00016B1F
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.m_hashHandle != null)
			{
				this.m_hashHandle.Dispose();
			}
			if (this.m_cspHandle != null)
			{
				this.m_cspHandle.Dispose();
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00018948 File Offset: 0x00016B48
		[SecuritySafeCritical]
		public void Initialize()
		{
			SafeCapiHashHandle safeCapiHashHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!CapiNative.UnsafeNativeMethods.CryptCreateHash(this.m_cspHandle, this.m_algorithmId, SafeCapiKeyHandle.InvalidHandle, 0, out safeCapiHashHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == -2146893816)
					{
						throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
					}
					throw new CryptographicException(lastWin32Error);
				}
			}
			finally
			{
				if (safeCapiHashHandle != null && !safeCapiHashHandle.IsInvalid)
				{
					safeCapiHashHandle.SetParentCsp(this.m_cspHandle);
				}
			}
			if (this.m_hashHandle != null)
			{
				this.m_hashHandle.Dispose();
			}
			this.m_hashHandle = safeCapiHashHandle;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000189E0 File Offset: 0x00016BE0
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
				if (!CapiNative.UnsafeNativeMethods.CryptHashData(this.m_hashHandle, ptr + ibStart, cbSize, 0))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00018A62 File Offset: 0x00016C62
		[SecuritySafeCritical]
		public byte[] HashFinal()
		{
			return CapiNative.GetHashParameter(this.m_hashHandle, CapiNative.HashParameter.HashValue);
		}

		// Token: 0x0400062F RID: 1583
		private CapiNative.AlgorithmId m_algorithmId;

		// Token: 0x04000630 RID: 1584
		[SecurityCritical]
		private SafeCspHandle m_cspHandle;

		// Token: 0x04000631 RID: 1585
		[SecurityCritical]
		private SafeCapiHashHandle m_hashHandle;
	}
}

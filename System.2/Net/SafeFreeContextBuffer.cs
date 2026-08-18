using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EF RID: 495
	[SuppressUnmanagedCodeSecurity]
	internal abstract class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x000641F6 File Offset: 0x000623F6
		protected SafeFreeContextBuffer() : base(true)
		{
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000641FF File Offset: 0x000623FF
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00064208 File Offset: 0x00062408
		internal static int EnumeratePackages(SecurDll Dll, out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (Dll == SecurDll.SECURITY)
			{
				SafeFreeContextBuffer_SECURITY safeFreeContextBuffer_SECURITY = null;
				int num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.EnumerateSecurityPackagesW(out pkgnum, out safeFreeContextBuffer_SECURITY);
				pkgArray = safeFreeContextBuffer_SECURITY;
				if (num != 0 && pkgArray != null)
				{
					pkgArray.SetHandleAsInvalid();
				}
				return num;
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "Dll");
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0006425C File Offset: 0x0006245C
		internal static SafeFreeContextBuffer CreateEmptyHandle(SecurDll dll)
		{
			if (dll == SecurDll.SECURITY)
			{
				return new SafeFreeContextBuffer_SECURITY();
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "dll");
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00064289 File Offset: 0x00062489
		public unsafe static int QueryContextAttributes(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			if (dll == SecurDll.SECURITY)
			{
				return SafeFreeContextBuffer.QueryContextAttributes_SECURITY(phContext, contextAttribute, buffer, refHandle);
			}
			return -1;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006429C File Offset: 0x0006249C
		private unsafe static int QueryContextAttributes_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			int num = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
					phContext.DangerousRelease();
				}
				if (num == 0 && refHandle != null)
				{
					if (refHandle is SafeFreeContextBuffer)
					{
						((SafeFreeContextBuffer)refHandle).Set(*(IntPtr*)buffer);
					}
					else
					{
						((SafeFreeCertContext)refHandle).Set(*(IntPtr*)buffer);
					}
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00064344 File Offset: 0x00062544
		public static int SetContextAttributes(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, byte[] buffer)
		{
			if (dll == SecurDll.SECURITY)
			{
				return SafeFreeContextBuffer.SetContextAttributes_SECURITY(phContext, contextAttribute, buffer);
			}
			return -1;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00064354 File Offset: 0x00062554
		private static int SetContextAttributes_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte[] buffer)
		{
			int result = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					result = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.SetContextAttributesW(ref phContext._handle, contextAttribute, buffer, buffer.Length);
					phContext.DangerousRelease();
				}
			}
			return result;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000513 RID: 1299
	[SuppressUnmanagedCodeSecurity]
	internal abstract class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x000A592E File Offset: 0x000A492E
		protected SafeFreeContextBuffer() : base(true)
		{
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000A5937 File Offset: 0x000A4937
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000A5940 File Offset: 0x000A4940
		internal static int EnumeratePackages(SecurDll Dll, out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			int num;
			switch (Dll)
			{
			case SecurDll.SECURITY:
			{
				SafeFreeContextBuffer_SECURITY safeFreeContextBuffer_SECURITY = null;
				num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.EnumerateSecurityPackagesW(out pkgnum, out safeFreeContextBuffer_SECURITY);
				pkgArray = safeFreeContextBuffer_SECURITY;
				break;
			}
			case SecurDll.SECUR32:
			{
				SafeFreeContextBuffer_SECUR32 safeFreeContextBuffer_SECUR = null;
				num = UnsafeNclNativeMethods.SafeNetHandles_SECUR32.EnumerateSecurityPackagesA(out pkgnum, out safeFreeContextBuffer_SECUR);
				pkgArray = safeFreeContextBuffer_SECUR;
				break;
			}
			case SecurDll.SCHANNEL:
			{
				SafeFreeContextBuffer_SCHANNEL safeFreeContextBuffer_SCHANNEL = null;
				num = UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.EnumerateSecurityPackagesA(out pkgnum, out safeFreeContextBuffer_SCHANNEL);
				pkgArray = safeFreeContextBuffer_SCHANNEL;
				break;
			}
			default:
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"SecurDll"
				}), "Dll");
			}
			if (num != 0 && pkgArray != null)
			{
				pkgArray.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000A59D0 File Offset: 0x000A49D0
		internal static SafeFreeContextBuffer CreateEmptyHandle(SecurDll dll)
		{
			switch (dll)
			{
			case SecurDll.SECURITY:
				return new SafeFreeContextBuffer_SECURITY();
			case SecurDll.SECUR32:
				return new SafeFreeContextBuffer_SECUR32();
			case SecurDll.SCHANNEL:
				return new SafeFreeContextBuffer_SCHANNEL();
			default:
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"SecurDll"
				}), "dll");
			}
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000A5A2C File Offset: 0x000A4A2C
		public unsafe static int QueryContextAttributes(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			switch (dll)
			{
			case SecurDll.SECURITY:
				return SafeFreeContextBuffer.QueryContextAttributes_SECURITY(phContext, contextAttribute, buffer, refHandle);
			case SecurDll.SECUR32:
				return SafeFreeContextBuffer.QueryContextAttributes_SECUR32(phContext, contextAttribute, buffer, refHandle);
			case SecurDll.SCHANNEL:
				return SafeFreeContextBuffer.QueryContextAttributes_SCHANNEL(phContext, contextAttribute, buffer, refHandle);
			default:
				return -1;
			}
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000A5A74 File Offset: 0x000A4A74
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

		// Token: 0x0600282F RID: 10287 RVA: 0x000A5B28 File Offset: 0x000A4B28
		private unsafe static int QueryContextAttributes_SECUR32(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
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
					num = UnsafeNclNativeMethods.SafeNetHandles_SECUR32.QueryContextAttributesA(ref phContext._handle, contextAttribute, (void*)buffer);
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

		// Token: 0x06002830 RID: 10288 RVA: 0x000A5BDC File Offset: 0x000A4BDC
		private unsafe static int QueryContextAttributes_SCHANNEL(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
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
					num = UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.QueryContextAttributesA(ref phContext._handle, contextAttribute, (void*)buffer);
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
	}
}

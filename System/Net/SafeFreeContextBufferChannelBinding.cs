using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200052C RID: 1324
	[SuppressUnmanagedCodeSecurity]
	internal abstract class SafeFreeContextBufferChannelBinding : ChannelBinding
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x000A7D2E File Offset: 0x000A6D2E
		public override int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000A7D36 File Offset: 0x000A6D36
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000A7D40 File Offset: 0x000A6D40
		internal static SafeFreeContextBufferChannelBinding CreateEmptyHandle(SecurDll dll)
		{
			switch (dll)
			{
			case SecurDll.SECURITY:
				return new SafeFreeContextBufferChannelBinding_SECURITY();
			case SecurDll.SECUR32:
				return new SafeFreeContextBufferChannelBinding_SECUR32();
			case SecurDll.SCHANNEL:
				return new SafeFreeContextBufferChannelBinding_SCHANNEL();
			default:
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"SecurDll"
				}), "dll");
			}
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000A7D9C File Offset: 0x000A6D9C
		public unsafe static int QueryContextChannelBinding(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			switch (dll)
			{
			case SecurDll.SECURITY:
				return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding_SECURITY(phContext, contextAttribute, buffer, refHandle);
			case SecurDll.SECUR32:
				return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding_SECUR32(phContext, contextAttribute, buffer, refHandle);
			case SecurDll.SCHANNEL:
				return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding_SCHANNEL(phContext, contextAttribute, buffer, refHandle);
			default:
				return -1;
			}
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000A7DE4 File Offset: 0x000A6DE4
		private unsafe static int QueryContextChannelBinding_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
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
					refHandle.Set(buffer->pBindings);
					refHandle.size = buffer->BindingsLength;
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000A7E84 File Offset: 0x000A6E84
		private unsafe static int QueryContextChannelBinding_SECUR32(SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
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
					refHandle.Set(buffer->pBindings);
					refHandle.size = buffer->BindingsLength;
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000A7F24 File Offset: 0x000A6F24
		private unsafe static int QueryContextChannelBinding_SCHANNEL(SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
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
					refHandle.Set(buffer->pBindings);
					refHandle.size = buffer->BindingsLength;
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x04002798 RID: 10136
		private int size;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000203 RID: 515
	[SuppressUnmanagedCodeSecurity]
	internal abstract class SafeFreeContextBufferChannelBinding : ChannelBinding
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0006606A File Offset: 0x0006426A
		public override int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00066072 File Offset: 0x00064272
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006607B File Offset: 0x0006427B
		internal static SafeFreeContextBufferChannelBinding CreateEmptyHandle(SecurDll dll)
		{
			if (dll == SecurDll.SECURITY)
			{
				return new SafeFreeContextBufferChannelBinding_SECURITY();
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "dll");
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000660A8 File Offset: 0x000642A8
		public unsafe static int QueryContextChannelBinding(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			if (dll == SecurDll.SECURITY)
			{
				return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding_SECURITY(phContext, contextAttribute, buffer, refHandle);
			}
			return -1;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000660BC File Offset: 0x000642BC
		private unsafe static int QueryContextChannelBinding_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			int num = -2146893055;
			bool flag = false;
			if (contextAttribute != ContextAttribute.EndpointBindings && contextAttribute != ContextAttribute.UniqueBindings)
			{
				return num;
			}
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

		// Token: 0x04001560 RID: 5472
		private int size;
	}
}

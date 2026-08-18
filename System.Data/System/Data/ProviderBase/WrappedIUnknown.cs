using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;

namespace System.Data.ProviderBase
{
	// Token: 0x0200024F RID: 591
	internal class WrappedIUnknown : SafeHandle
	{
		// Token: 0x06002075 RID: 8309 RVA: 0x00280688 File Offset: 0x0027FA88
		internal WrappedIUnknown() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x002806A8 File Offset: 0x0027FAA8
		internal WrappedIUnknown(object unknown) : this()
		{
			if (unknown != null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = Marshal.GetIUnknownForObject(unknown);
				}
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x002806F8 File Offset: 0x0027FAF8
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00280718 File Offset: 0x0027FB18
		internal object ComWrapper()
		{
			object result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr punk = base.DangerousGetHandle();
				result = EnterpriseServicesHelper.WrapIUnknownWithComObject(punk);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00280778 File Offset: 0x0027FB78
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				Marshal.Release(handle);
			}
			return true;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;

namespace System.Data.ProviderBase
{
	// Token: 0x020002CE RID: 718
	internal class WrappedIUnknown : SafeHandle
	{
		// Token: 0x06002B51 RID: 11089 RVA: 0x0011D6FC File Offset: 0x0011CAFC
		internal WrappedIUnknown() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0011D718 File Offset: 0x0011CB18
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

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x0011D760 File Offset: 0x0011CB60
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0011D780 File Offset: 0x0011CB80
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

		// Token: 0x06002B55 RID: 11093 RVA: 0x0011D7D8 File Offset: 0x0011CBD8
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

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200015E RID: 350
	internal class SecurityQualityOfService : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x000A2E88 File Offset: 0x000A2288
		public SecurityQualityOfService(UnsafeNativeMethods.SecurityImpersonationLevel impersonationLevel, bool effectiveOnly, bool dynamicTrackingMode) : base(true)
		{
			this.Initialize(impersonationLevel, effectiveOnly, dynamicTrackingMode);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x000A2EA8 File Offset: 0x000A22A8
		protected override bool ReleaseHandle()
		{
			if (this.m_hQos.IsAllocated)
			{
				this.m_hQos.Free();
			}
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x000A2EDC File Offset: 0x000A22DC
		internal void Initialize(UnsafeNativeMethods.SecurityImpersonationLevel impersonationLevel, bool effectiveOnly, bool dynamicTrackingMode)
		{
			this.m_qos.length = (uint)Marshal.SizeOf(typeof(UnsafeNativeMethods.SECURITY_QUALITY_OF_SERVICE));
			this.m_qos.impersonationLevel = (int)impersonationLevel;
			this.m_qos.effectiveOnly = (effectiveOnly ? 1 : 0);
			this.m_qos.contextDynamicTrackingMode = (dynamicTrackingMode ? 1 : 0);
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.m_hQos = GCHandle.Alloc(this.m_qos, GCHandleType.Pinned);
				intPtr = this.m_hQos.AddrOfPinnedObject();
				if (intPtr != IntPtr.Zero)
				{
					base.SetHandle(intPtr);
				}
			}
		}

		// Token: 0x04000DE0 RID: 3552
		private UnsafeNativeMethods.SECURITY_QUALITY_OF_SERVICE m_qos;

		// Token: 0x04000DE1 RID: 3553
		private GCHandle m_hQos;
	}
}

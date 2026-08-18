using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000481 RID: 1153
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	public abstract class CriticalHandleZeroOrMinusOneIsInvalid : CriticalHandle
	{
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000990F0 File Offset: 0x000980F0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleZeroOrMinusOneIsInvalid() : base(IntPtr.Zero)
		{
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x000990FD File Offset: 0x000980FD
		public override bool IsInvalid
		{
			get
			{
				return this.handle.IsNull() || this.handle == new IntPtr(-1);
			}
		}
	}
}

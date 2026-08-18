using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200047F RID: 1151
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandleMinusOneIsInvalid : SafeHandle
	{
		// Token: 0x06002DB8 RID: 11704 RVA: 0x00099005 File Offset: 0x00098005
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleMinusOneIsInvalid(bool ownsHandle) : base(new IntPtr(-1), ownsHandle)
		{
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x00099014 File Offset: 0x00098014
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}

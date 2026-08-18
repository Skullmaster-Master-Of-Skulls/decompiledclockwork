using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000482 RID: 1154
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
	{
		// Token: 0x06002DC8 RID: 11720 RVA: 0x0009911F File Offset: 0x0009811F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleMinusOneIsInvalid() : base(new IntPtr(-1))
		{
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x0009912D File Offset: 0x0009812D
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}

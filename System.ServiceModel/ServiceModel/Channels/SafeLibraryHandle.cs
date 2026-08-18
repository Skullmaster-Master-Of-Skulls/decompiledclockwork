using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C2 RID: 2242
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060055AF RID: 21935 RVA: 0x00139907 File Offset: 0x00137B07
		internal SafeLibraryHandle() : base(true)
		{
			this.doNotfreeLibraryOnRelease = false;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x00139917 File Offset: 0x00137B17
		public void DoNotFreeLibraryOnRelease()
		{
			this.doNotfreeLibraryOnRelease = true;
		}

		// Token: 0x060055B1 RID: 21937
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060055B2 RID: 21938 RVA: 0x00139920 File Offset: 0x00137B20
		protected override bool ReleaseHandle()
		{
			if (this.doNotfreeLibraryOnRelease)
			{
				this.handle = IntPtr.Zero;
				return true;
			}
			return SafeLibraryHandle.FreeLibrary(this.handle);
		}

		// Token: 0x040034FC RID: 13564
		private bool doNotfreeLibraryOnRelease;
	}
}

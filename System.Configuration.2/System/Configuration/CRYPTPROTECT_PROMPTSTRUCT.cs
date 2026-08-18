using System;

namespace System.Configuration
{
	// Token: 0x0200004E RID: 78
	internal struct CRYPTPROTECT_PROMPTSTRUCT : IDisposable
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000129EA File Offset: 0x00010BEA
		void IDisposable.Dispose()
		{
			this.hwndApp = IntPtr.Zero;
		}

		// Token: 0x04000246 RID: 582
		public int cbSize;

		// Token: 0x04000247 RID: 583
		public int dwPromptFlags;

		// Token: 0x04000248 RID: 584
		public IntPtr hwndApp;

		// Token: 0x04000249 RID: 585
		public string szPrompt;
	}
}

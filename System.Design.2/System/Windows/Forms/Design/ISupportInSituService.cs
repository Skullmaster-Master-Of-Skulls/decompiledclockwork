using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000301 RID: 769
	[ComVisible(true)]
	internal interface ISupportInSituService
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001E8C RID: 7820
		bool IgnoreMessages { get; }

		// Token: 0x06001E8D RID: 7821
		void HandleKeyChar();

		// Token: 0x06001E8E RID: 7822
		IntPtr GetEditWindow();
	}
}

using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F8 RID: 760
	internal interface IMenuStatusHandler
	{
		// Token: 0x06001E4D RID: 7757
		bool OverrideInvoke(MenuCommand cmd);

		// Token: 0x06001E4E RID: 7758
		bool OverrideStatus(MenuCommand cmd);
	}
}

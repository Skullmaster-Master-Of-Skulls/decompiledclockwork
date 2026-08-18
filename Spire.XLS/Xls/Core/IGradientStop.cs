using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020005E4 RID: 1508
	public interface IGradientStop
	{
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06005995 RID: 22933
		// (set) Token: 0x06005996 RID: 22934
		Color Color { get; set; }

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06005997 RID: 22935
		// (set) Token: 0x06005998 RID: 22936
		int Position { get; set; }

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06005999 RID: 22937
		// (set) Token: 0x0600599A RID: 22938
		int Transparency { get; set; }
	}
}

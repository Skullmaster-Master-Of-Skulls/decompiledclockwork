using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001639 RID: 5689
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct KerningPair
	{
		// Token: 0x04003E77 RID: 15991
		public int wFirst;

		// Token: 0x04003E78 RID: 15992
		public int wSecond;

		// Token: 0x04003E79 RID: 15993
		public int iKernAmount;
	}
}

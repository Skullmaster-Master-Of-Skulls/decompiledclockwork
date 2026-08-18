using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Pdf.Gdi;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015EB RID: 5611
	public interface IFontDescriptor
	{
		// Token: 0x17004315 RID: 17173
		// (get) Token: 0x0600DA98 RID: 55960
		int Flags { get; }

		// Token: 0x17004316 RID: 17174
		// (get) Token: 0x0600DA99 RID: 55961
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		int[] FontBBox { get; }

		// Token: 0x17004317 RID: 17175
		// (get) Token: 0x0600DA9A RID: 55962
		int ItalicAngle { get; }

		// Token: 0x17004318 RID: 17176
		// (get) Token: 0x0600DA9B RID: 55963
		int StemV { get; }

		// Token: 0x17004319 RID: 17177
		// (get) Token: 0x0600DA9C RID: 55964
		bool HasKerningInfo { get; }

		// Token: 0x1700431A RID: 17178
		// (get) Token: 0x0600DA9D RID: 55965
		bool IsEmbeddable { get; }

		// Token: 0x1700431B RID: 17179
		// (get) Token: 0x0600DA9E RID: 55966
		bool IsSubsettable { get; }

		// Token: 0x1700431C RID: 17180
		// (get) Token: 0x0600DA9F RID: 55967
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		byte[] FontData { get; }

		// Token: 0x1700431D RID: 17181
		// (get) Token: 0x0600DAA0 RID: 55968
		GdiKerningPairs KerningInfo { get; }
	}
}

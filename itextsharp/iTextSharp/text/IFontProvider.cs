using System;

namespace iTextSharp.text
{
	// Token: 0x020004F3 RID: 1267
	public interface IFontProvider
	{
		// Token: 0x06002B4E RID: 11086
		bool IsRegistered(string fontname);

		// Token: 0x06002B4F RID: 11087
		Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color);
	}
}

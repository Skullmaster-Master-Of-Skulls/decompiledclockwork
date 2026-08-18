using System;

namespace Spire.Doc.Convertors.Sgml
{
	// Token: 0x02000015 RID: 21
	internal enum State
	{
		// Token: 0x04000098 RID: 152
		Initial,
		// Token: 0x04000099 RID: 153
		Markup,
		// Token: 0x0400009A RID: 154
		EndTag,
		// Token: 0x0400009B RID: 155
		Attr,
		// Token: 0x0400009C RID: 156
		AttrValue,
		// Token: 0x0400009D RID: 157
		Text,
		// Token: 0x0400009E RID: 158
		PartialTag,
		// Token: 0x0400009F RID: 159
		AutoClose,
		// Token: 0x040000A0 RID: 160
		CData,
		// Token: 0x040000A1 RID: 161
		PartialText,
		// Token: 0x040000A2 RID: 162
		PseudoStartTag,
		// Token: 0x040000A3 RID: 163
		Eof
	}
}

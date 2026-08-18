using System;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x02000090 RID: 144
	public interface IDocumentObject
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009D RID: 157
		Document Document { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009E RID: 158
		DocumentObject Owner { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009F RID: 159
		DocumentObjectType DocumentObjectType { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A0 RID: 160
		IDocumentObject NextSibling { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A1 RID: 161
		IDocumentObject PreviousSibling { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A2 RID: 162
		bool IsComposite { get; }

		// Token: 0x060000A3 RID: 163
		DocumentObject Clone();
	}
}

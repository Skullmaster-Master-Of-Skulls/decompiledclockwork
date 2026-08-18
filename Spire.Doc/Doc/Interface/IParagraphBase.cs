using System;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x020003AD RID: 941
	public interface IParagraphBase : IDocumentObject
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06003506 RID: 13574
		Paragraph OwnerParagraph { get; }
	}
}

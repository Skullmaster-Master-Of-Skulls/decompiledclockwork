using System;
using Spire.Doc.Documents.XML;

namespace Spire.Doc.Interface
{
	// Token: 0x020004FD RID: 1277
	public interface IDocumentSerializable
	{
		// Token: 0x0600420C RID: 16908
		void WriteXmlAttributes(IXDLSAttributeWriter writer);

		// Token: 0x0600420D RID: 16909
		void WriteXmlContent(IXDLSContentWriter writer);

		// Token: 0x0600420E RID: 16910
		void ReadXmlAttributes(IXDLSAttributeReader reader);

		// Token: 0x0600420F RID: 16911
		bool ReadXmlContent(IXDLSContentReader reader);

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06004210 RID: 16912
		XDLSHolder XDLSHolder { get; }

		// Token: 0x06004211 RID: 16913
		void RestoreReference(string name, int value);
	}
}

using System;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x02000501 RID: 1281
	public interface IParagraphCollection : IDocumentObjectCollection
	{
		// Token: 0x1700042B RID: 1067
		Paragraph this[int index]
		{
			get;
		}

		// Token: 0x0600423B RID: 16955
		int Add(IParagraph paragraph);

		// Token: 0x0600423C RID: 16956
		void Insert(int index, IParagraph paragraph);

		// Token: 0x0600423D RID: 16957
		int IndexOf(IParagraph paragraph);

		// Token: 0x0600423E RID: 16958
		void RemoveAt(int index);
	}
}

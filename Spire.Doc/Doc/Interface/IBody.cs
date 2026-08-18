using System;
using Spire.Doc.Collections;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x0200009B RID: 155
	public interface IBody : ICompositeObject
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000164 RID: 356
		TableCollection Tables { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000165 RID: 357
		ParagraphCollection Paragraphs { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000166 RID: 358
		FormFieldCollection FormFields { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000167 RID: 359
		IParagraph LastParagraph { get; }

		// Token: 0x06000168 RID: 360
		Paragraph AddParagraph();

		// Token: 0x06000169 RID: 361
		Table AddTable();

		// Token: 0x0600016A RID: 362
		void InsertXHTML(string html);

		// Token: 0x0600016B RID: 363
		void InsertXHTML(string html, int paragraphIndex);

		// Token: 0x0600016C RID: 364
		void InsertXHTML(string html, int paragraphIndex, int paragraphItemIndex);

		// Token: 0x0600016D RID: 365
		void EnsureMinimum();
	}
}

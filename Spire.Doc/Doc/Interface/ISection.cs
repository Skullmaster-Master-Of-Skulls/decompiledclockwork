using System;
using Spire.Doc.Collections;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x020000EB RID: 235
	public interface ISection : ICompositeObject
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003E2 RID: 994
		ParagraphCollection Paragraphs { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003E3 RID: 995
		TableCollection Tables { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003E4 RID: 996
		Body Body { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003E5 RID: 997
		PageSetup PageSetup { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003E6 RID: 998
		ColumnCollection Columns { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003E7 RID: 999
		// (set) Token: 0x060003E8 RID: 1000
		SectionBreakType BreakCode { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003E9 RID: 1001
		// (set) Token: 0x060003EA RID: 1002
		bool ProtectForm { get; set; }

		// Token: 0x060003EB RID: 1003
		Column AddColumn(float width, float spacing);

		// Token: 0x060003EC RID: 1004
		Paragraph AddParagraph();

		// Token: 0x060003ED RID: 1005
		Table AddTable();

		// Token: 0x060003EE RID: 1006
		Section Clone();

		// Token: 0x060003EF RID: 1007
		void MakeColumnsSameWidth();

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003F0 RID: 1008
		HeadersFooters HeadersFooters { get; }
	}
}

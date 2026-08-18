using System;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x02000095 RID: 149
	public abstract class DocumentContainer : DocumentBase, spr\u17C8
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000BC8C File Offset: 0x0000AC8C
		public int Count
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.WidgetCollection.Count;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000BCD4 File Offset: 0x0000ACD4
		spr\u1AB8 spr\u17C8.Item
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.WidgetCollection[index] as spr\u1AB8;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DA RID: 218
		protected abstract IDocumentObjectCollection WidgetCollection { get; }

		// Token: 0x060000DB RID: 219 RVA: 0x0000BD20 File Offset: 0x0000AD20
		public DocumentContainer(Document doc, DocumentObject owner) : base(doc, owner)
		{
		}
	}
}

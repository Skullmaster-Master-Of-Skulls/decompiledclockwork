using System;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000529 RID: 1321
	public class ParagraphCollection : DocumentSubsetCollection, IParagraphCollection
	{
		// Token: 0x1700052F RID: 1327
		public Paragraph this[int index]
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
				return (Paragraph)base.GetByIndex(index);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x0040705C File Offset: 0x0040605C
		internal IBody OwnerTextBody
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
				return base.Owner as IBody;
			}
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x004070A4 File Offset: 0x004060A4
		public ParagraphCollection(BodyRegionCollection bodyItems) : base(bodyItems, DocumentObjectType.Paragraph)
		{
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x004070BC File Offset: 0x004060BC
		public int Add(IParagraph paragraph)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.Document.ᜀ(paragraph);
			return base.ᜄ((DocumentObject)paragraph);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x00407110 File Offset: 0x00406110
		public bool Contains(IParagraph paragraph)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return base.ᜅ((DocumentObject)paragraph);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x00407158 File Offset: 0x00406158
		public void Insert(int index, IParagraph paragraph)
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
			base.Document.ᜀ(paragraph);
			base.ᜀ(index, (DocumentObject)paragraph);
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x004071B0 File Offset: 0x004061B0
		public int IndexOf(IParagraph paragraph)
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
			return base.ᜆ((DocumentObject)paragraph);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x004071F8 File Offset: 0x004061F8
		public void Remove(IParagraph paragraph)
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
			base.ᜂ((DocumentObject)paragraph);
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x00407240 File Offset: 0x00406240
		public void RemoveAt(int index)
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
			base.ᜁ(index);
		}
	}
}

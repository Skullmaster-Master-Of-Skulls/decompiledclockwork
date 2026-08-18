using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200052F RID: 1327
	public class ListStyleCollection : DocumentSerializableCollection
	{
		// Token: 0x17000538 RID: 1336
		public ListStyle this[int index]
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
				return (ListStyle)base.InnerList[index];
			}
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x004089C8 File Offset: 0x004079C8
		internal ListStyleCollection(Document A_0) : base(A_0, null)
		{
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x004089E0 File Offset: 0x004079E0
		public int Add(ListStyle style)
		{
			int a_ = 6;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (style == null)
			{
				throw new ArgumentNullException(ClipboardData.b("Ὣᩭ९ṱᅳ", a_));
			}
			IL_50:
			style.ᜀ(base.Document);
			return base.InnerList.Add(style);
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x00408A58 File Offset: 0x00407A58
		public ListStyle FindByName(string name)
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
			return StyleCollection.ᜀ(base.InnerList, name) as ListStyle;
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00408AA8 File Offset: 0x00407AA8
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
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
			return new ListStyle(base.Document);
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x00408AF0 File Offset: 0x00407AF0
		protected override string GetTagItemName()
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("੸ེѼ፾", a_);
		}
	}
}

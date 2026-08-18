using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200052B RID: 1323
	public class ListLevelCollection : DocumentSerializableCollection
	{
		// Token: 0x17000532 RID: 1330
		public ListLevel this[int index]
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
				return (ListLevel)base.InnerList[index];
			}
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x00407614 File Offset: 0x00406614
		internal ListLevelCollection(ListStyle A_0) : base(A_0.Document, A_0)
		{
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x00407630 File Offset: 0x00406630
		internal int ᜁ(ListLevel A_0)
		{
			int a_ = 4;
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				throw new ArgumentNullException(ClipboardData.b("٩५ᡭᕯṱ", a_));
			}
			A_0.ᜀ(base.OwnerBase);
			return base.InnerList.Add(A_0);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x004076A8 File Offset: 0x004066A8
		internal int ᜀ(ListLevel A_0)
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
			return base.InnerList.IndexOf(A_0);
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x004076F0 File Offset: 0x004066F0
		internal void ᜀ()
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
			base.InnerList.Clear();
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x00407738 File Offset: 0x00406738
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
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
			return new ListLevel(base.OwnerBase as ListStyle);
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00407784 File Offset: 0x00406784
		protected override string GetTagItemName()
		{
			int a_ = 11;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("ᵰᙲʹቶᕸ", a_);
		}
	}
}

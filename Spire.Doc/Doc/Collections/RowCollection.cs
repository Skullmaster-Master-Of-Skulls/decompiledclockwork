using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000542 RID: 1346
	public class RowCollection : DocumentObjectCollection
	{
		// Token: 0x1700055A RID: 1370
		public TableRow this[int index]
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
				return base.InnerList[index] as TableRow;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x0040E154 File Offset: 0x0040D154
		protected override Type[] TypesOfElement
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
				return RowCollection.ᜀ;
			}
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x0040E194 File Offset: 0x0040D194
		public RowCollection(Table owner) : base(owner.Document, owner)
		{
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x0040E1B0 File Offset: 0x0040D1B0
		public int Add(TableRow row)
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
			return base.Add(row);
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x0040E1F4 File Offset: 0x0040D1F4
		public void Insert(int index, TableRow row)
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
			base.Insert(index, row);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x0040E238 File Offset: 0x0040D238
		public int IndexOf(TableRow row)
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
			return base.IndexOf(row);
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x0040E27C File Offset: 0x0040D27C
		public void Remove(TableRow row)
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
			base.Remove(row);
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x0040E2C0 File Offset: 0x0040D2C0
		protected override string GetTagItemName()
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("੷ᕹ୻", a_);
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x0040E314 File Offset: 0x0040D314
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
			return new TableRow(base.Document);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x0040E35C File Offset: 0x0040D35C
		// Note: this type is marked as 'beforefieldinit'.
		static RowCollection()
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
			RowCollection.ᜀ = new Type[]
			{
				typeof(TableRow)
			};
		}

		// Token: 0x04003697 RID: 13975
		private string \u25D9\u00A7\u0086\u008F;

		// Token: 0x04003698 RID: 13976
		private string[] \u2460\u0085\u0085\u0083;

		// Token: 0x04003699 RID: 13977
		private float[] \u2609\u009F\u00A9\u00A8;

		// Token: 0x0400369A RID: 13978
		private new static readonly Type[] ᜀ;
	}
}

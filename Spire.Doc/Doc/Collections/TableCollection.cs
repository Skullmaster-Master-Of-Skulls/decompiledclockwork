using System;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000541 RID: 1345
	public class TableCollection : DocumentSubsetCollection, ITableCollection
	{
		// Token: 0x17000558 RID: 1368
		public ITable this[int index]
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
				return base.GetByIndex(index) as ITable;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x0040DEFC File Offset: 0x0040CEFC
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

		// Token: 0x06004636 RID: 17974 RVA: 0x0040DF44 File Offset: 0x0040CF44
		public TableCollection(BodyRegionCollection bodyItems) : base(bodyItems, DocumentObjectType.Table)
		{
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x0040DF5C File Offset: 0x0040CF5C
		public int Add(ITable table)
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
			return base.ᜄ((DocumentObject)table);
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x0040DFA4 File Offset: 0x0040CFA4
		public bool Contains(ITable table)
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
			return base.ᜅ((DocumentObject)table);
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x0040DFEC File Offset: 0x0040CFEC
		public int IndexOf(ITable table)
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
			return base.ᜆ((DocumentObject)table);
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x0040E034 File Offset: 0x0040D034
		public int Insert(int index, ITable table)
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
			return base.ᜀ(index, (DocumentObject)table);
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x0040E07C File Offset: 0x0040D07C
		public void Remove(ITable table)
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
			base.ᜂ((DocumentObject)table);
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x0040E0C4 File Offset: 0x0040D0C4
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

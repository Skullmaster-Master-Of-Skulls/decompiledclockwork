using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200053B RID: 1339
	public class ColumnCollection : DocumentSerializableCollection
	{
		// Token: 0x1700054D RID: 1357
		public Column this[int index]
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
				return (Column)base.InnerList[index];
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x0040D07C File Offset: 0x0040C07C
		internal Section OwnerSection
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
				return base.OwnerBase as Section;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x0040D0C4 File Offset: 0x0040C0C4
		internal ColumnCollection(Section A_0) : base(A_0.Document, A_0)
		{
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x0040D0E0 File Offset: 0x0040C0E0
		public int Add(Column column)
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
			column.ᜀ(base.OwnerBase);
			return base.InnerList.Add(column);
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x0040D134 File Offset: 0x0040C134
		public void Populate(int count, float spacing)
		{
			for (;;)
			{
				IL_18:
				float num = this.OwnerSection.PageSetup.ClientWidth / (float)count;
				num -= spacing;
				base.InnerList.Clear();
				int num2 = 0;
				for (;;)
				{
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num2 >= count)
							{
								num3 = 1;
								continue;
							}
							this.Add(new Column(base.Document)
							{
								Width = num,
								Space = spacing
							});
							num2++;
							if (true)
							{
							}
							num3 = 2;
							continue;
						case 1:
							goto IL_65;
						case 2:
							goto IL_47;
						case 3:
							goto IL_47;
						}
						goto IL_18;
						IL_47:
						num3 = 0;
					}
					IL_65:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_7B;
					}
				}
			}
			IL_7B:
			if (false)
			{
			}
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x0040D200 File Offset: 0x0040C200
		internal void ᜀ(ColumnCollection A_0)
		{
			for (;;)
			{
				IL_18:
				int num = 0;
				int count = base.InnerList.Count;
				for (;;)
				{
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_50;
						case 1:
							goto IL_32;
						case 2:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							Column column = base.InnerList[num] as Column;
							A_0.Add(column.ᜀ());
							num++;
							num2 = 1;
							continue;
						}
						case 3:
							goto IL_32;
						}
						goto IL_18;
						IL_32:
						num2 = 2;
					}
					IL_50:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_66;
					}
				}
			}
			IL_66:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x0040D2B4 File Offset: 0x0040C2B4
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
			return new Column(base.Document);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x0040D2FC File Offset: 0x0040C2FC
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
			return ClipboardData.b("᭷ᕹၻ୽", a_);
		}
	}
}

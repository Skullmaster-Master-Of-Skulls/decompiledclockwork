using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000543 RID: 1347
	public class TabCollection : DocumentSerializableCollection
	{
		// Token: 0x1700055C RID: 1372
		public Tab this[int index]
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
				return (Tab)base.InnerList[index];
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x0040E400 File Offset: 0x0040D400
		// (set) Token: 0x06004649 RID: 17993 RVA: 0x0040E444 File Offset: 0x0040D444
		internal bool CancelOnChangeEvent
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x0040E488 File Offset: 0x0040D488
		internal TabCollection(Document A_0) : base(A_0, null)
		{
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0040E4A0 File Offset: 0x0040D4A0
		internal TabCollection(Document A_0, FormatBase A_1) : this(A_0)
		{
			base.ᜀ(A_1);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x0040E4BC File Offset: 0x0040D4BC
		public Tab AddTab()
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
			return this.AddTab(0f, TabJustification.Left, TabLeader.NoLeader);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0040E504 File Offset: 0x0040D504
		public Tab AddTab(float position, TabJustification justification, TabLeader leader)
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
			Tab tab = new Tab(base.Document, position, justification, leader);
			base.InnerList.Add(tab);
			tab.ᜀ(this);
			this.ᜁ();
			return tab;
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x0040E56C File Offset: 0x0040D56C
		public Tab AddTab(float position)
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
			return this.AddTab(position, TabJustification.Left, TabLeader.NoLeader);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x0040E5B0 File Offset: 0x0040D5B0
		public void Clear()
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
			base.InnerList.Clear();
			this.ᜁ();
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x0040E5FC File Offset: 0x0040D5FC
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
			base.InnerList.RemoveAt(index);
			this.ᜁ();
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x0040E64C File Offset: 0x0040D64C
		internal void ᜀ(double A_0)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_A6;
					case 1:
						goto IL_A6;
					case 2:
						goto IL_A6;
					case 3:
						if ((double)this[num].Position == A_0)
						{
							num2 = 4;
							continue;
						}
						if (true)
						{
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_74;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 4:
						base.InnerList.Remove(this[num]);
						goto IL_74;
					case 5:
						goto IL_C5;
					case 6:
						if (num >= base.Count)
						{
							num2 = 5;
							continue;
						}
						num2 = 3;
						continue;
					}
					break;
					IL_74:
					num2 = 2;
					continue;
					IL_A6:
					num2 = 6;
				}
			}
			IL_C5:
			this.ᜁ();
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x0040E730 File Offset: 0x0040D730
		internal void ᜀ(Tab A_0)
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
			base.InnerList.Add(A_0);
			A_0.ᜀ(this);
			this.ᜁ();
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x0040E788 File Offset: 0x0040D788
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return new Tab(base.Document);
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x0040E7D0 File Offset: 0x0040D7D0
		protected override string GetTagItemName()
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("ⅴᙶ᭸", a_);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x0040E824 File Offset: 0x0040D824
		internal void ᜁ()
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					(base.OwnerBase as ParagraphFormat).ᜀ(this);
					num = 5;
					continue;
				case 1:
					if (!(base.OwnerBase is ParagraphFormat))
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					num = 1;
					continue;
				case 3:
					if (base.OwnerBase != null)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					goto IL_3C;
				case 5:
					return;
				}
				if (this.ᜀ)
				{
					num = 4;
					continue;
				}
				IL_81:
				num = 3;
			}
			IL_3C:
			if (true)
			{
			}
		}

		// Token: 0x0400369B RID: 13979
		private string \u2593\u0098\u0098\u0087;

		// Token: 0x0400369C RID: 13980
		private int[] \u25D8\u00AB\u0082\u0089;

		// Token: 0x0400369D RID: 13981
		private string[] \u2460\u00A7\u009A\u0087;

		// Token: 0x0400369E RID: 13982
		private byte \u2609\u009D\u00A2\u00A2;

		// Token: 0x0400369F RID: 13983
		private byte \u2593\u0095\u008E\u00AB;

		// Token: 0x040036A0 RID: 13984
		private new bool ᜀ;
	}
}

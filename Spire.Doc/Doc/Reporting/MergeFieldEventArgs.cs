using System;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Reporting
{
	// Token: 0x02000108 RID: 264
	public class MergeFieldEventArgs : EventArgs
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00055FC0 File Offset: 0x00054FC0
		public IDocument Document
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
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00056004 File Offset: 0x00055004
		public string FieldName
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
				return this.ᜁ.FieldName;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0005604C File Offset: 0x0005504C
		public object FieldValue
		{
			get
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
				return this.ᜂ;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00056090 File Offset: 0x00055090
		public string TableName
		{
			get
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
				return this.ᜄ;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000560D4 File Offset: 0x000550D4
		public int RowIndex
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
				return this.ᜃ;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00056118 File Offset: 0x00055118
		public CharacterFormat CharacterFormat
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
				return this.ᜁ.CharacterFormat;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00056160 File Offset: 0x00055160
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x000561B8 File Offset: 0x000551B8
		public string Text
		{
			get
			{
				if (this.FieldValue == null)
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
						return "";
					}
				}
				return this.FieldValue.ToString();
			}
			set
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
				this.ᜂ = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000561FC File Offset: 0x000551FC
		public IMergeField CurrentMergeField
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
				return this.ᜁ;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00056240 File Offset: 0x00055240
		public MergeFieldEventArgs(IDocument doc, string tableName, int rowIndex, IMergeField field, object value)
		{
			this.ᜀ = doc;
			this.ᜁ = field;
			this.ᜂ = value;
			this.ᜃ = rowIndex;
			this.ᜄ = tableName;
		}

		// Token: 0x04000E14 RID: 3604
		private IDocument ᜀ;

		// Token: 0x04000E15 RID: 3605
		private IMergeField ᜁ;

		// Token: 0x04000E16 RID: 3606
		private float \u2460\u008E\u008E\u009D;

		// Token: 0x04000E17 RID: 3607
		private object ᜂ;

		// Token: 0x04000E18 RID: 3608
		private int ᜃ;

		// Token: 0x04000E19 RID: 3609
		private float \u2460\u0082\u00AB\u0092;

		// Token: 0x04000E1A RID: 3610
		private long \u2609\u0085\u0086\u007F;

		// Token: 0x04000E1B RID: 3611
		private byte \u2593\u007F\u00A9\u009C;

		// Token: 0x04000E1C RID: 3612
		private string ᜄ;
	}
}

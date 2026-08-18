using System;

namespace iTextSharp.text
{
	// Token: 0x020004F1 RID: 1265
	public class ListItem : Paragraph
	{
		// Token: 0x06002B3B RID: 11067 RVA: 0x00105F08 File Offset: 0x00104F08
		public ListItem()
		{
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00105F10 File Offset: 0x00104F10
		public ListItem(float leading) : base(leading)
		{
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00105F19 File Offset: 0x00104F19
		public ListItem(Chunk chunk) : base(chunk)
		{
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00105F22 File Offset: 0x00104F22
		public ListItem(string str) : base(str)
		{
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00105F2B File Offset: 0x00104F2B
		public ListItem(string str, Font font) : base(str, font)
		{
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x00105F35 File Offset: 0x00104F35
		public ListItem(float leading, Chunk chunk) : base(leading, chunk)
		{
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00105F3F File Offset: 0x00104F3F
		public ListItem(float leading, string str) : base(leading, str)
		{
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x00105F49 File Offset: 0x00104F49
		public ListItem(float leading, string str, Font font) : base(leading, str, font)
		{
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x00105F54 File Offset: 0x00104F54
		public ListItem(Phrase phrase) : base(phrase)
		{
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x00105F5D File Offset: 0x00104F5D
		public override int Type
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x00105F61 File Offset: 0x00104F61
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x00105F69 File Offset: 0x00104F69
		public Chunk ListSymbol
		{
			get
			{
				return this.symbol;
			}
			set
			{
				if (this.symbol == null)
				{
					this.symbol = value;
					if (this.symbol.Font.IsStandardFont())
					{
						this.symbol.Font = this.font;
					}
				}
			}
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x00105F9D File Offset: 0x00104F9D
		public new static bool IsTag(string tag)
		{
			return "listitem".Equals(tag);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x00105FAA File Offset: 0x00104FAA
		public void SetIndentationLeft(float indentation, bool autoindent)
		{
			if (autoindent)
			{
				base.IndentationLeft = this.ListSymbol.GetWidthPoint();
				return;
			}
			base.IndentationLeft = indentation;
		}

		// Token: 0x04001DDD RID: 7645
		protected Chunk symbol;
	}
}

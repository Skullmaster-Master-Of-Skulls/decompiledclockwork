using System;

namespace iTextSharp.text
{
	// Token: 0x020000B7 RID: 183
	public class ZapfDingbatsNumberList : List
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001D4E8 File Offset: 0x0001C4E8
		public ZapfDingbatsNumberList(int type) : base(true)
		{
			this.type = type;
			float size = this.symbol.Font.Size;
			this.symbol.Font = FontFactory.GetFont("ZapfDingbats", size, 0);
			this.postSymbol = " ";
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001D538 File Offset: 0x0001C538
		public ZapfDingbatsNumberList(int type, int symbolIndent) : base(true, (float)symbolIndent)
		{
			this.type = type;
			float size = this.symbol.Font.Size;
			this.symbol.Font = FontFactory.GetFont("ZapfDingbats", size, 0);
			this.postSymbol = " ";
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001D588 File Offset: 0x0001C588
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0001D590 File Offset: 0x0001C590
		public int NumberType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001D59C File Offset: 0x0001C59C
		public override bool Add(IElement o)
		{
			if (o is ListItem)
			{
				ListItem listItem = (ListItem)o;
				Chunk chunk = new Chunk(this.preSymbol, this.symbol.Font);
				switch (this.type)
				{
				case 0:
					chunk.Append(((char)(this.first + this.list.Count + 171)).ToString());
					break;
				case 1:
					chunk.Append(((char)(this.first + this.list.Count + 181)).ToString());
					break;
				case 2:
					chunk.Append(((char)(this.first + this.list.Count + 191)).ToString());
					break;
				default:
					chunk.Append(((char)(this.first + this.list.Count + 201)).ToString());
					break;
				}
				chunk.Append(this.postSymbol);
				listItem.ListSymbol = chunk;
				listItem.SetIndentationLeft(this.symbolIndent, this.autoindent);
				listItem.IndentationRight = 0f;
				this.list.Add(listItem);
				return true;
			}
			if (o is List)
			{
				List list = (List)o;
				list.IndentationLeft += this.symbolIndent;
				this.first--;
				this.list.Add(list);
				return true;
			}
			return false;
		}

		// Token: 0x040002BE RID: 702
		protected int type;
	}
}

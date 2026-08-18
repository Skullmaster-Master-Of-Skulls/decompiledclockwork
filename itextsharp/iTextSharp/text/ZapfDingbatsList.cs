using System;

namespace iTextSharp.text
{
	// Token: 0x020000B8 RID: 184
	public class ZapfDingbatsList : List
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001D718 File Offset: 0x0001C718
		public ZapfDingbatsList(int zn) : base(true)
		{
			this.zn = zn;
			float size = this.symbol.Font.Size;
			this.symbol.Font = FontFactory.GetFont("ZapfDingbats", size, 0);
			this.postSymbol = " ";
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001D768 File Offset: 0x0001C768
		public ZapfDingbatsList(int zn, int symbolIndent) : base(true, (float)symbolIndent)
		{
			this.zn = zn;
			float size = this.symbol.Font.Size;
			this.symbol.Font = FontFactory.GetFont("ZapfDingbats", size, 0);
			this.postSymbol = " ";
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001D7C1 File Offset: 0x0001C7C1
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001D7B8 File Offset: 0x0001C7B8
		public int CharNumber
		{
			get
			{
				return this.zn;
			}
			set
			{
				this.zn = value;
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001D7CC File Offset: 0x0001C7CC
		public override bool Add(IElement o)
		{
			if (o is ListItem)
			{
				ListItem listItem = (ListItem)o;
				Chunk chunk = new Chunk(this.preSymbol, this.symbol.Font);
				chunk.Append(((char)this.zn).ToString());
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

		// Token: 0x040002BF RID: 703
		protected int zn;
	}
}

using System;
using iTextSharp.text.factories;

namespace iTextSharp.text
{
	// Token: 0x02000463 RID: 1123
	public class GreekList : List
	{
		// Token: 0x0600262F RID: 9775 RVA: 0x000E6711 File Offset: 0x000E5711
		public GreekList() : base(true)
		{
			this.SetGreekFont();
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000E6720 File Offset: 0x000E5720
		public GreekList(int symbolIndent) : base(true, (float)symbolIndent)
		{
			this.SetGreekFont();
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000E6731 File Offset: 0x000E5731
		public GreekList(bool greeklower, int symbolIndent) : base(true, (float)symbolIndent)
		{
			this.lowercase = greeklower;
			this.SetGreekFont();
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000E674C File Offset: 0x000E574C
		protected void SetGreekFont()
		{
			float size = this.symbol.Font.Size;
			this.symbol.Font = FontFactory.GetFont("Symbol", size, 0);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000E6784 File Offset: 0x000E5784
		public override bool Add(IElement o)
		{
			if (o is ListItem)
			{
				ListItem listItem = (ListItem)o;
				Chunk chunk = new Chunk(this.preSymbol, this.symbol.Font);
				chunk.Append(GreekAlphabetFactory.GetString(this.first + this.list.Count, this.lowercase));
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
	}
}

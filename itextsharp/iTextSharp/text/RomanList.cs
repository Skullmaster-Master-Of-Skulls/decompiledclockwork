using System;
using iTextSharp.text.factories;

namespace iTextSharp.text
{
	// Token: 0x02000635 RID: 1589
	public class RomanList : List
	{
		// Token: 0x060035C2 RID: 13762 RVA: 0x0014D758 File Offset: 0x0014C758
		public RomanList() : base(true)
		{
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x0014D761 File Offset: 0x0014C761
		public RomanList(int symbolIndent) : base(true, (float)symbolIndent)
		{
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0014D76C File Offset: 0x0014C76C
		public RomanList(bool romanlower, int symbolIndent) : base(true, (float)symbolIndent)
		{
			this.lowercase = romanlower;
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x0014D780 File Offset: 0x0014C780
		public override bool Add(IElement o)
		{
			if (o is ListItem)
			{
				ListItem listItem = (ListItem)o;
				Chunk chunk = new Chunk(this.preSymbol, this.symbol.Font);
				chunk.Append(RomanNumberFactory.GetString(this.first + this.list.Count, this.lowercase));
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

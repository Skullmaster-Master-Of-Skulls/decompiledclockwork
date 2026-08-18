using System;
using System.Collections.Generic;
using System.Globalization;
using System.util;
using iTextSharp.text.pdf;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x02000065 RID: 101
	public class IncCell : ITextElementArray, IElement
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00010CD8 File Offset: 0x0000FCD8
		public IncCell(string tag, ChainedProperties props)
		{
			this.cell = new PdfPCell();
			string text = props["colspan"];
			if (text != null)
			{
				this.cell.Colspan = int.Parse(text);
			}
			text = props["rowspan"];
			if (text != null)
			{
				this.cell.Rowspan = int.Parse(text);
			}
			text = props["align"];
			if (tag.Equals("th"))
			{
				this.cell.HorizontalAlignment = 1;
			}
			if (text != null)
			{
				if (Util.EqualsIgnoreCase(text, "center"))
				{
					this.cell.HorizontalAlignment = 1;
				}
				else if (Util.EqualsIgnoreCase(text, "right"))
				{
					this.cell.HorizontalAlignment = 2;
				}
				else if (Util.EqualsIgnoreCase(text, "left"))
				{
					this.cell.HorizontalAlignment = 0;
				}
				else if (Util.EqualsIgnoreCase(text, "justify"))
				{
					this.cell.HorizontalAlignment = 3;
				}
			}
			text = props["valign"];
			this.cell.VerticalAlignment = 5;
			if (text != null)
			{
				if (Util.EqualsIgnoreCase(text, "top"))
				{
					this.cell.VerticalAlignment = 4;
				}
				else if (Util.EqualsIgnoreCase(text, "bottom"))
				{
					this.cell.VerticalAlignment = 6;
				}
			}
			text = props["border"];
			float borderWidth = 0f;
			if (text != null)
			{
				borderWidth = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			this.cell.BorderWidth = borderWidth;
			text = props["cellpadding"];
			if (text != null)
			{
				this.cell.Padding = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			this.cell.UseDescender = true;
			text = props["bgcolor"];
			this.cell.BackgroundColor = Markup.DecodeColor(text);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010E9D File Offset: 0x0000FE9D
		public bool Add(IElement o)
		{
			this.cell.AddElement(o);
			return true;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00010EAC File Offset: 0x0000FEAC
		public List<Chunk> Chunks
		{
			get
			{
				return this.chunks;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010EB4 File Offset: 0x0000FEB4
		public bool Process(IElementListener listener)
		{
			return true;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00010EB7 File Offset: 0x0000FEB7
		public int Type
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00010EBB File Offset: 0x0000FEBB
		public PdfPCell Cell
		{
			get
			{
				return this.cell;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010EC3 File Offset: 0x0000FEC3
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010EC6 File Offset: 0x0000FEC6
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010EC9 File Offset: 0x0000FEC9
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040001BC RID: 444
		private List<Chunk> chunks = new List<Chunk>();

		// Token: 0x040001BD RID: 445
		private PdfPCell cell;
	}
}

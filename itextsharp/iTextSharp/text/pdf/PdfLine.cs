using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000419 RID: 1049
	public class PdfLine
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x000DA2DC File Offset: 0x000D92DC
		internal PdfLine(float left, float right, int alignment, float height)
		{
			this.left = left;
			this.width = right - left;
			this.originalWidth = this.width;
			this.alignment = alignment;
			this.height = height;
			this.line = new List<PdfChunk>();
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000DA31A File Offset: 0x000D931A
		internal PdfLine(float left, float originalWidth, float remainingWidth, int alignment, bool newlineSplit, List<PdfChunk> line, bool isRTL)
		{
			this.left = left;
			this.originalWidth = originalWidth;
			this.width = remainingWidth;
			this.alignment = alignment;
			this.line = line;
			this.newlineSplit = newlineSplit;
			this.isRTL = isRTL;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000DA358 File Offset: 0x000D9358
		internal PdfChunk Add(PdfChunk chunk)
		{
			if (chunk == null || chunk.ToString().Equals(""))
			{
				return null;
			}
			PdfChunk pdfChunk = chunk.Split(this.width);
			this.newlineSplit = (chunk.IsNewlineSplit() || pdfChunk == null);
			if (chunk.IsTab())
			{
				object[] array = (object[])chunk.GetAttribute("TAB");
				float num = (float)array[1];
				bool flag = (bool)array[2];
				if (flag && num < this.originalWidth - this.width)
				{
					return chunk;
				}
				this.width = this.originalWidth - num;
				chunk.AdjustLeft(this.left);
				this.AddToLine(chunk);
			}
			else if (chunk.Length > 0 || chunk.IsImage())
			{
				if (pdfChunk != null)
				{
					chunk.TrimLastSpace();
				}
				this.width -= chunk.Width;
				this.AddToLine(chunk);
			}
			else if (this.line.Count < 1)
			{
				chunk = pdfChunk;
				pdfChunk = chunk.Truncate(this.width);
				this.width -= chunk.Width;
				if (chunk.Length > 0)
				{
					this.AddToLine(chunk);
					return pdfChunk;
				}
				if (pdfChunk != null)
				{
					this.AddToLine(chunk);
				}
				return null;
			}
			else
			{
				this.width += this.line[this.line.Count - 1].TrimLastSpace();
			}
			return pdfChunk;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000DA4B8 File Offset: 0x000D94B8
		private void AddToLine(PdfChunk chunk)
		{
			if (chunk.ChangeLeading && chunk.IsImage())
			{
				float num = chunk.Image.ScaledHeight + chunk.ImageOffsetY + chunk.Image.BorderWidthTop;
				if (num > this.height)
				{
					this.height = num;
				}
			}
			this.line.Add(chunk);
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x000DA510 File Offset: 0x000D9510
		public int Size
		{
			get
			{
				return this.line.Count;
			}
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000DA51D File Offset: 0x000D951D
		public IEnumerator<PdfChunk> GetEnumerator()
		{
			return this.line.GetEnumerator();
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000DA52F File Offset: 0x000D952F
		internal float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000DA538 File Offset: 0x000D9538
		internal float IndentLeft
		{
			get
			{
				if (!this.isRTL)
				{
					if (this.GetSeparatorCount() <= 0)
					{
						switch (this.alignment)
						{
						case 1:
							return this.left + this.width / 2f;
						case 2:
							return this.left + this.width;
						}
					}
					return this.left;
				}
				switch (this.alignment)
				{
				case 0:
					return this.left + this.width;
				case 1:
					return this.left + this.width / 2f;
				default:
					return this.left;
				}
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000DA5D7 File Offset: 0x000D95D7
		public bool HasToBeJustified()
		{
			return (this.alignment == 3 || this.alignment == 8) && this.width != 0f;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000DA5FD File Offset: 0x000D95FD
		public void ResetAlignment()
		{
			if (this.alignment == 3)
			{
				this.alignment = 0;
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000DA60F File Offset: 0x000D960F
		internal void SetExtraIndent(float extra)
		{
			this.left += extra;
			this.width -= extra;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x000DA62D File Offset: 0x000D962D
		internal float WidthLeft
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000DA638 File Offset: 0x000D9638
		internal int NumberOfSpaces
		{
			get
			{
				string text = this.ToString();
				int length = text.Length;
				int num = 0;
				for (int i = 0; i < length; i++)
				{
					if (text[i] == ' ')
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x000DA671 File Offset: 0x000D9671
		public ListItem ListItem
		{
			set
			{
				this.listSymbol = value.ListSymbol;
				this.symbolIndent = value.IndentationLeft;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000DA68B File Offset: 0x000D968B
		public Chunk ListSymbol
		{
			get
			{
				return this.listSymbol;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000DA693 File Offset: 0x000D9693
		public float ListIndent
		{
			get
			{
				return this.symbolIndent;
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000DA69C File Offset: 0x000D969C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PdfChunk pdfChunk in this.line)
			{
				stringBuilder.Append(pdfChunk.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000DA704 File Offset: 0x000D9704
		public int GetLineLengthUtf32()
		{
			int num = 0;
			foreach (PdfChunk pdfChunk in this.line)
			{
				num += pdfChunk.LengthUtf32;
			}
			return num;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000DA75C File Offset: 0x000D975C
		public bool NewlineSplit
		{
			get
			{
				return this.newlineSplit && this.alignment != 8;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000DA774 File Offset: 0x000D9774
		public int LastStrokeChunk
		{
			get
			{
				int i;
				for (i = this.line.Count - 1; i >= 0; i--)
				{
					PdfChunk pdfChunk = this.line[i];
					if (pdfChunk.IsStroked())
					{
						break;
					}
				}
				return i;
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000DA7AF File Offset: 0x000D97AF
		public PdfChunk GetChunk(int idx)
		{
			if (idx < 0 || idx >= this.line.Count)
			{
				return null;
			}
			return this.line[idx];
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000DA7D1 File Offset: 0x000D97D1
		public float OriginalWidth
		{
			get
			{
				return this.originalWidth;
			}
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000DA7DC File Offset: 0x000D97DC
		internal float[] GetMaxSize()
		{
			float num = 0f;
			float num2 = -10000f;
			for (int i = 0; i < this.line.Count; i++)
			{
				PdfChunk pdfChunk = this.line[i];
				if (!pdfChunk.IsImage())
				{
					num = Math.Max(pdfChunk.Font.Size, num);
				}
				else
				{
					num2 = Math.Max(pdfChunk.Image.ScaledHeight + pdfChunk.ImageOffsetY, num2);
				}
			}
			return new float[]
			{
				num,
				num2
			};
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000DA861 File Offset: 0x000D9861
		internal bool RTL
		{
			get
			{
				return this.isRTL;
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000DA86C File Offset: 0x000D986C
		internal int GetSeparatorCount()
		{
			int num = 0;
			foreach (PdfChunk pdfChunk in this.line)
			{
				if (pdfChunk.IsTab())
				{
					return -1;
				}
				if (pdfChunk.IsHorizontalSeparator())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000DA8D8 File Offset: 0x000D98D8
		public float GetWidthCorrected(float charSpacing, float wordSpacing)
		{
			float num = 0f;
			for (int i = 0; i < this.line.Count; i++)
			{
				PdfChunk pdfChunk = this.line[i];
				num += pdfChunk.GetWidthCorrected(charSpacing, wordSpacing);
			}
			return num;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000DA91C File Offset: 0x000D991C
		public float Ascender
		{
			get
			{
				float num = 0f;
				foreach (PdfChunk pdfChunk in this.line)
				{
					if (pdfChunk.IsImage())
					{
						num = Math.Max(num, pdfChunk.Image.ScaledHeight + pdfChunk.ImageOffsetY);
					}
					else
					{
						PdfFont font = pdfChunk.Font;
						num = Math.Max(num, font.Font.GetFontDescriptor(1, font.Size));
					}
				}
				return num;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000DA9B4 File Offset: 0x000D99B4
		public float Descender
		{
			get
			{
				float num = 0f;
				foreach (PdfChunk pdfChunk in this.line)
				{
					if (pdfChunk.IsImage())
					{
						num = Math.Min(num, pdfChunk.ImageOffsetY);
					}
					else
					{
						PdfFont font = pdfChunk.Font;
						num = Math.Min(num, font.Font.GetFontDescriptor(3, font.Size));
					}
				}
				return num;
			}
		}

		// Token: 0x0400188D RID: 6285
		protected internal List<PdfChunk> line;

		// Token: 0x0400188E RID: 6286
		protected internal float left;

		// Token: 0x0400188F RID: 6287
		protected internal float width;

		// Token: 0x04001890 RID: 6288
		protected internal int alignment;

		// Token: 0x04001891 RID: 6289
		protected internal float height;

		// Token: 0x04001892 RID: 6290
		protected internal Chunk listSymbol;

		// Token: 0x04001893 RID: 6291
		protected internal float symbolIndent;

		// Token: 0x04001894 RID: 6292
		protected internal bool newlineSplit;

		// Token: 0x04001895 RID: 6293
		protected internal float originalWidth;

		// Token: 0x04001896 RID: 6294
		protected internal bool isRTL;
	}
}

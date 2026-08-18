using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001C2 RID: 450
	public class VerticalText
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x0005FB6C File Offset: 0x0005EB6C
		public VerticalText(PdfContentByte text)
		{
			this.text = text;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005FB90 File Offset: 0x0005EB90
		public void AddText(Phrase phrase)
		{
			foreach (Chunk chunk in phrase.Chunks)
			{
				this.chunks.Add(new PdfChunk(chunk, null));
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005FBF0 File Offset: 0x0005EBF0
		public void AddText(Chunk chunk)
		{
			this.chunks.Add(new PdfChunk(chunk, null));
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0005FC04 File Offset: 0x0005EC04
		public void SetVerticalLayout(float startX, float startY, float height, int maxLines, float leading)
		{
			this.startX = startX;
			this.startY = startY;
			this.height = height;
			this.maxLines = maxLines;
			this.Leading = leading;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0005FC2B File Offset: 0x0005EC2B
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0005FC33 File Offset: 0x0005EC33
		public float Leading
		{
			get
			{
				return this.leading;
			}
			set
			{
				this.leading = value;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0005FC3C File Offset: 0x0005EC3C
		protected PdfLine CreateLine(float width)
		{
			if (this.chunks.Count == 0)
			{
				return null;
			}
			this.splittedChunkText = null;
			this.currentStandbyChunk = null;
			PdfLine pdfLine = new PdfLine(0f, width, this.alignment, 0f);
			this.currentChunkMarker = 0;
			while (this.currentChunkMarker < this.chunks.Count)
			{
				PdfChunk pdfChunk = this.chunks[this.currentChunkMarker];
				string value = pdfChunk.ToString();
				this.currentStandbyChunk = pdfLine.Add(pdfChunk);
				if (this.currentStandbyChunk != null)
				{
					this.splittedChunkText = pdfChunk.ToString();
					pdfChunk.Value = value;
					return pdfLine;
				}
				this.currentChunkMarker++;
			}
			return pdfLine;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0005FCEC File Offset: 0x0005ECEC
		protected void ShortenChunkArray()
		{
			if (this.currentChunkMarker < 0)
			{
				return;
			}
			if (this.currentChunkMarker >= this.chunks.Count)
			{
				this.chunks.Clear();
				return;
			}
			PdfChunk pdfChunk = this.chunks[this.currentChunkMarker];
			pdfChunk.Value = this.splittedChunkText;
			this.chunks[this.currentChunkMarker] = this.currentStandbyChunk;
			for (int i = this.currentChunkMarker - 1; i >= 0; i--)
			{
				this.chunks.RemoveAt(i);
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0005FD76 File Offset: 0x0005ED76
		public int Go()
		{
			return this.Go(false);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0005FD80 File Offset: 0x0005ED80
		public int Go(bool simulate)
		{
			bool flag = false;
			PdfContentByte pdfContentByte = null;
			if (this.text != null)
			{
				pdfContentByte = this.text.Duplicate;
			}
			else if (!simulate)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("verticaltext.go.with.simulate.eq.eq.false.and.text.eq.eq.null"));
			}
			int num;
			while (this.maxLines > 0)
			{
				if (this.chunks.Count == 0)
				{
					num = VerticalText.NO_MORE_TEXT;
					IL_EB:
					if (flag)
					{
						this.text.EndText();
						this.text.Add(pdfContentByte);
					}
					return num;
				}
				PdfLine pdfLine = this.CreateLine(this.height);
				if (!simulate && !flag)
				{
					this.text.BeginText();
					flag = true;
				}
				this.ShortenChunkArray();
				if (!simulate)
				{
					this.text.SetTextMatrix(this.startX, this.startY - pdfLine.IndentLeft);
					this.WriteLine(pdfLine, this.text, pdfContentByte);
				}
				this.maxLines--;
				this.startX -= this.leading;
			}
			num = VerticalText.NO_MORE_COLUMN;
			if (this.chunks.Count == 0)
			{
				num |= VerticalText.NO_MORE_TEXT;
				goto IL_EB;
			}
			goto IL_EB;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0005FE94 File Offset: 0x0005EE94
		internal void WriteLine(PdfLine line, PdfContentByte text, PdfContentByte graphics)
		{
			PdfFont pdfFont = null;
			foreach (PdfChunk pdfChunk in line)
			{
				if (pdfChunk.Font.CompareTo(pdfFont) != 0)
				{
					pdfFont = pdfChunk.Font;
					text.SetFontAndSize(pdfFont.Font, pdfFont.Size);
				}
				BaseColor color = pdfChunk.Color;
				if (color != null)
				{
					text.SetColorFill(color);
				}
				text.ShowText(pdfChunk.ToString());
				if (color != null)
				{
					text.ResetRGBColorFill();
				}
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0005FF24 File Offset: 0x0005EF24
		public void SetOrigin(float startX, float startY)
		{
			this.startX = startX;
			this.startY = startY;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0005FF34 File Offset: 0x0005EF34
		public float OriginX
		{
			get
			{
				return this.startX;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0005FF3C File Offset: 0x0005EF3C
		public float OriginY
		{
			get
			{
				return this.startY;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0005FF44 File Offset: 0x0005EF44
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x0005FF4C File Offset: 0x0005EF4C
		public int MaxLines
		{
			get
			{
				return this.maxLines;
			}
			set
			{
				this.maxLines = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0005FF55 File Offset: 0x0005EF55
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0005FF5D File Offset: 0x0005EF5D
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0005FF66 File Offset: 0x0005EF66
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0005FF6E File Offset: 0x0005EF6E
		public int Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
			}
		}

		// Token: 0x04000C3D RID: 3133
		public static int NO_MORE_TEXT = 1;

		// Token: 0x04000C3E RID: 3134
		public static int NO_MORE_COLUMN = 2;

		// Token: 0x04000C3F RID: 3135
		protected List<PdfChunk> chunks = new List<PdfChunk>();

		// Token: 0x04000C40 RID: 3136
		protected PdfContentByte text;

		// Token: 0x04000C41 RID: 3137
		protected int alignment;

		// Token: 0x04000C42 RID: 3138
		protected int currentChunkMarker = -1;

		// Token: 0x04000C43 RID: 3139
		protected PdfChunk currentStandbyChunk;

		// Token: 0x04000C44 RID: 3140
		protected string splittedChunkText;

		// Token: 0x04000C45 RID: 3141
		protected float leading;

		// Token: 0x04000C46 RID: 3142
		protected float startX;

		// Token: 0x04000C47 RID: 3143
		protected float startY;

		// Token: 0x04000C48 RID: 3144
		protected int maxLines;

		// Token: 0x04000C49 RID: 3145
		protected float height;
	}
}

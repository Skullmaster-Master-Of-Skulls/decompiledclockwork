using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.draw
{
	// Token: 0x020003A2 RID: 930
	public class VerticalPositionMark : IDrawInterface, IElement
	{
		// Token: 0x06002032 RID: 8242 RVA: 0x000BFA40 File Offset: 0x000BEA40
		public VerticalPositionMark()
		{
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000BFA48 File Offset: 0x000BEA48
		public VerticalPositionMark(IDrawInterface drawInterface, float offset)
		{
			this.drawInterface = drawInterface;
			this.offset = offset;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000BFA5E File Offset: 0x000BEA5E
		public virtual void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y)
		{
			if (this.drawInterface != null)
			{
				this.drawInterface.Draw(canvas, llx, lly, urx, ury, y + this.offset);
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000BFA84 File Offset: 0x000BEA84
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000BFAB4 File Offset: 0x000BEAB4
		public int Type
		{
			get
			{
				return 55;
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000BFAB8 File Offset: 0x000BEAB8
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000BFABB File Offset: 0x000BEABB
		public bool IsNestable()
		{
			return false;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x000BFAC0 File Offset: 0x000BEAC0
		public List<Chunk> Chunks
		{
			get
			{
				return new List<Chunk>
				{
					new Chunk(this, true)
				};
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000BFAEA File Offset: 0x000BEAEA
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x000BFAE1 File Offset: 0x000BEAE1
		public virtual IDrawInterface DrawInterface
		{
			get
			{
				return this.drawInterface;
			}
			set
			{
				this.drawInterface = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000BFAFB File Offset: 0x000BEAFB
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x000BFAF2 File Offset: 0x000BEAF2
		public virtual float Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x04001627 RID: 5671
		protected IDrawInterface drawInterface;

		// Token: 0x04001628 RID: 5672
		protected float offset;
	}
}

using System;
using System.Collections.Generic;
using System.util;

namespace iTextSharp.text
{
	// Token: 0x02000172 RID: 370
	public class Paragraph : Phrase
	{
		// Token: 0x06000E3A RID: 3642 RVA: 0x00052D0B File Offset: 0x00051D0B
		public Paragraph()
		{
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00052D1A File Offset: 0x00051D1A
		public Paragraph(float leading) : base(leading)
		{
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00052D2A File Offset: 0x00051D2A
		public Paragraph(Chunk chunk) : base(chunk)
		{
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00052D3A File Offset: 0x00051D3A
		public Paragraph(float leading, Chunk chunk) : base(leading, chunk)
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00052D4B File Offset: 0x00051D4B
		public Paragraph(string str) : base(str)
		{
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00052D5B File Offset: 0x00051D5B
		public Paragraph(string str, Font font) : base(str, font)
		{
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00052D6C File Offset: 0x00051D6C
		public Paragraph(float leading, string str) : base(leading, str)
		{
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00052D7D File Offset: 0x00051D7D
		public Paragraph(float leading, string str, Font font) : base(leading, str, font)
		{
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00052D90 File Offset: 0x00051D90
		public Paragraph(Phrase phrase) : base(phrase)
		{
			if (phrase is Paragraph)
			{
				Paragraph paragraph = (Paragraph)phrase;
				this.Alignment = paragraph.Alignment;
				this.ExtraParagraphSpace = paragraph.ExtraParagraphSpace;
				this.FirstLineIndent = paragraph.FirstLineIndent;
				this.IndentationLeft = paragraph.IndentationLeft;
				this.IndentationRight = paragraph.IndentationRight;
				this.SpacingAfter = paragraph.SpacingAfter;
				this.SpacingBefore = paragraph.SpacingBefore;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00052E0E File Offset: 0x00051E0E
		public override int Type
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00052E14 File Offset: 0x00051E14
		public override bool Add(IElement o)
		{
			if (o is List)
			{
				List list = (List)o;
				list.IndentationLeft += this.indentationLeft;
				list.IndentationRight = this.indentationRight;
				base.Add(list);
				return true;
			}
			if (o is Image)
			{
				base.AddSpecial((Image)o);
				return true;
			}
			if (o is Paragraph)
			{
				base.Add(o);
				List<Chunk> chunks = this.Chunks;
				if (chunks.Count > 0)
				{
					Chunk chunk = chunks[chunks.Count - 1];
					base.Add(new Chunk("\n", chunk.Font));
				}
				else
				{
					base.Add(Chunk.NEWLINE);
				}
				return true;
			}
			base.Add(o);
			return true;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00052ED0 File Offset: 0x00051ED0
		public void SetAlignment(string alignment)
		{
			if (Util.EqualsIgnoreCase(alignment, "Center"))
			{
				this.alignment = 1;
				return;
			}
			if (Util.EqualsIgnoreCase(alignment, "Right"))
			{
				this.alignment = 2;
				return;
			}
			if (Util.EqualsIgnoreCase(alignment, "Justify"))
			{
				this.alignment = 3;
				return;
			}
			if (Util.EqualsIgnoreCase(alignment, "JustifyAll"))
			{
				this.alignment = 8;
				return;
			}
			this.alignment = 0;
		}

		// Token: 0x170002BB RID: 699
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00052F38 File Offset: 0x00051F38
		public override float Leading
		{
			set
			{
				this.leading = value;
				this.multipliedLeading = 0f;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00052F4C File Offset: 0x00051F4C
		public void SetLeading(float fixedLeading, float multipliedLeading)
		{
			this.leading = fixedLeading;
			this.multipliedLeading = multipliedLeading;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00052F5C File Offset: 0x00051F5C
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x00052F64 File Offset: 0x00051F64
		public float MultipliedLeading
		{
			get
			{
				return this.multipliedLeading;
			}
			set
			{
				this.leading = 0f;
				this.multipliedLeading = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00052F78 File Offset: 0x00051F78
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x00052F80 File Offset: 0x00051F80
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00052F89 File Offset: 0x00051F89
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x00052F91 File Offset: 0x00051F91
		public float IndentationLeft
		{
			get
			{
				return this.indentationLeft;
			}
			set
			{
				this.indentationLeft = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00052F9A File Offset: 0x00051F9A
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x00052FA2 File Offset: 0x00051FA2
		public float IndentationRight
		{
			get
			{
				return this.indentationRight;
			}
			set
			{
				this.indentationRight = value;
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00052FAB File Offset: 0x00051FAB
		public new static bool IsTag(string tag)
		{
			return "paragraph".Equals(tag);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00052FB8 File Offset: 0x00051FB8
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00052FC0 File Offset: 0x00051FC0
		public float SpacingBefore
		{
			get
			{
				return this.spacingBefore;
			}
			set
			{
				this.spacingBefore = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00052FC9 File Offset: 0x00051FC9
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00052FD1 File Offset: 0x00051FD1
		public float SpacingAfter
		{
			get
			{
				return this.spacingAfter;
			}
			set
			{
				this.spacingAfter = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00052FDA File Offset: 0x00051FDA
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00052FE2 File Offset: 0x00051FE2
		public bool KeepTogether
		{
			get
			{
				return this.keeptogether;
			}
			set
			{
				this.keeptogether = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00052FEC File Offset: 0x00051FEC
		public float TotalLeading
		{
			get
			{
				float num = (this.font == null) ? (12f * this.multipliedLeading) : this.font.GetCalculatedLeading(this.multipliedLeading);
				if (num > 0f && !base.HasLeading())
				{
					return num;
				}
				return this.Leading + num;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0005303B File Offset: 0x0005203B
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00053043 File Offset: 0x00052043
		public float FirstLineIndent
		{
			get
			{
				return this.firstLineIndent;
			}
			set
			{
				this.firstLineIndent = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0005304C File Offset: 0x0005204C
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x00053054 File Offset: 0x00052054
		public float ExtraParagraphSpace
		{
			get
			{
				return this.extraParagraphSpace;
			}
			set
			{
				this.extraParagraphSpace = value;
			}
		}

		// Token: 0x04000A6C RID: 2668
		protected int alignment = -1;

		// Token: 0x04000A6D RID: 2669
		protected float multipliedLeading;

		// Token: 0x04000A6E RID: 2670
		protected float indentationLeft;

		// Token: 0x04000A6F RID: 2671
		protected float indentationRight;

		// Token: 0x04000A70 RID: 2672
		private float firstLineIndent;

		// Token: 0x04000A71 RID: 2673
		protected float spacingBefore;

		// Token: 0x04000A72 RID: 2674
		protected float spacingAfter;

		// Token: 0x04000A73 RID: 2675
		private float extraParagraphSpace;

		// Token: 0x04000A74 RID: 2676
		protected bool keeptogether;
	}
}

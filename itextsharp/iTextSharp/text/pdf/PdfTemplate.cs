using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000274 RID: 628
	public class PdfTemplate : PdfContentByte
	{
		// Token: 0x060017A6 RID: 6054 RVA: 0x00087688 File Offset: 0x00086688
		protected PdfTemplate() : base(null)
		{
			this.type = 1;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000876B0 File Offset: 0x000866B0
		internal PdfTemplate(PdfWriter wr) : base(wr)
		{
			this.type = 1;
			this.pageResources = new PageResources();
			this.pageResources.AddDefaultColor(wr.DefaultColorspace);
			this.thisReference = this.writer.PdfIndirectReference;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0008770D File Offset: 0x0008670D
		public static PdfTemplate CreateTemplate(PdfWriter writer, float width, float height)
		{
			return PdfTemplate.CreateTemplate(writer, width, height, null);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00087718 File Offset: 0x00086718
		internal static PdfTemplate CreateTemplate(PdfWriter writer, float width, float height, PdfName forcedName)
		{
			PdfTemplate pdfTemplate = new PdfTemplate(writer);
			pdfTemplate.Width = width;
			pdfTemplate.Height = height;
			writer.AddDirectTemplateSimple(pdfTemplate, forcedName);
			return pdfTemplate;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00087744 File Offset: 0x00086744
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00087751 File Offset: 0x00086751
		public float Width
		{
			get
			{
				return this.bBox.Width;
			}
			set
			{
				this.bBox.Left = 0f;
				this.bBox.Right = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0008776F File Offset: 0x0008676F
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x0008777C File Offset: 0x0008677C
		public float Height
		{
			get
			{
				return this.bBox.Height;
			}
			set
			{
				this.bBox.Bottom = 0f;
				this.bBox.Top = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0008779A File Offset: 0x0008679A
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x000877A2 File Offset: 0x000867A2
		public Rectangle BoundingBox
		{
			get
			{
				return this.bBox;
			}
			set
			{
				this.bBox = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000877AB File Offset: 0x000867AB
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x000877B3 File Offset: 0x000867B3
		public IPdfOCG Layer
		{
			get
			{
				return this.layer;
			}
			set
			{
				this.layer = value;
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000877BC File Offset: 0x000867BC
		public void SetMatrix(float a, float b, float c, float d, float e, float f)
		{
			this.matrix = new PdfArray();
			this.matrix.Add(new PdfNumber(a));
			this.matrix.Add(new PdfNumber(b));
			this.matrix.Add(new PdfNumber(c));
			this.matrix.Add(new PdfNumber(d));
			this.matrix.Add(new PdfNumber(e));
			this.matrix.Add(new PdfNumber(f));
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00087843 File Offset: 0x00086843
		internal PdfArray Matrix
		{
			get
			{
				return this.matrix;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0008784B File Offset: 0x0008684B
		public PdfIndirectReference IndirectReference
		{
			get
			{
				if (this.thisReference == null)
				{
					this.thisReference = this.writer.PdfIndirectReference;
				}
				return this.thisReference;
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0008786C File Offset: 0x0008686C
		public void BeginVariableText()
		{
			this.content.Append("/Tx BMC ");
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0008787F File Offset: 0x0008687F
		public void EndVariableText()
		{
			this.content.Append("EMC ");
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00087892 File Offset: 0x00086892
		internal virtual PdfObject Resources
		{
			get
			{
				return this.PageResources.Resources;
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0008789F File Offset: 0x0008689F
		internal virtual PdfStream GetFormXObject(int compressionLevel)
		{
			return new PdfFormXObject(this, compressionLevel);
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000878A8 File Offset: 0x000868A8
		public override PdfContentByte Duplicate
		{
			get
			{
				PdfTemplate pdfTemplate = new PdfTemplate();
				pdfTemplate.writer = this.writer;
				pdfTemplate.pdf = this.pdf;
				pdfTemplate.thisReference = this.thisReference;
				pdfTemplate.pageResources = this.pageResources;
				pdfTemplate.bBox = new Rectangle(this.bBox);
				pdfTemplate.group = this.group;
				pdfTemplate.layer = this.layer;
				if (this.matrix != null)
				{
					pdfTemplate.matrix = new PdfArray(this.matrix);
				}
				pdfTemplate.separator = this.separator;
				return pdfTemplate;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0008793A File Offset: 0x0008693A
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00087942 File Offset: 0x00086942
		internal override PageResources PageResources
		{
			get
			{
				return this.pageResources;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0008794A File Offset: 0x0008694A
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x00087952 File Offset: 0x00086952
		public virtual PdfTransparencyGroup Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x04001015 RID: 4117
		public const int TYPE_TEMPLATE = 1;

		// Token: 0x04001016 RID: 4118
		public const int TYPE_IMPORTED = 2;

		// Token: 0x04001017 RID: 4119
		public const int TYPE_PATTERN = 3;

		// Token: 0x04001018 RID: 4120
		protected int type;

		// Token: 0x04001019 RID: 4121
		protected PdfIndirectReference thisReference;

		// Token: 0x0400101A RID: 4122
		protected PageResources pageResources;

		// Token: 0x0400101B RID: 4123
		protected Rectangle bBox = new Rectangle(0f, 0f);

		// Token: 0x0400101C RID: 4124
		protected PdfArray matrix;

		// Token: 0x0400101D RID: 4125
		protected PdfTransparencyGroup group;

		// Token: 0x0400101E RID: 4126
		protected IPdfOCG layer;
	}
}

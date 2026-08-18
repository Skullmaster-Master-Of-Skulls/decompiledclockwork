using System;
using System.ComponentModel;
using System.Drawing;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PDF
{
	// Token: 0x02000224 RID: 548
	public class PDFOptions : ICloneable
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x000AE4E8 File Offset: 0x000AD4E8
		public PDFOptions()
		{
			int a_ = 18;
			this.ᜅ = 1.0;
			this.ᜆ = 3.0;
			this.ᜇ = 1;
			this.ᜈ = Color.Black;
			base..ctor();
			this.ᜀ = new PdfFont();
			this.ᜀ.FontName = HyperlinksCollectionEditor.b("昭唯匱倳匵䨷簹医倽㐿", a_);
			this.ᜁ = new PdfFont();
			this.ᜁ.FontName = HyperlinksCollectionEditor.b("洭儯䈱䀳張圷吹稻儽⸿㙁", a_);
			this.ᜂ = new PdfFont();
			this.ᜂ.FontName = HyperlinksCollectionEditor.b("樭儯䘱唳瀵圷吹䠻", a_);
			this.ᜃ = new PdfFont();
			this.ᜃ.FontName = HyperlinksCollectionEditor.b("栭弯崱䀳匵䨷簹医倽㐿", a_);
			this.ᜄ = new PdfExportPageOptions();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000AE5D0 File Offset: 0x000AD5D0
		public object Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new PDFOptions
			{
				HeaderFont = (PdfFont)this.HeaderFont.Clone(),
				TitleFont = (PdfFont)this.TitleFont.Clone(),
				DataFont = (PdfFont)this.DataFont.Clone(),
				FooterFont = (PdfFont)this.FooterFont.Clone(),
				PageOptions = (PdfExportPageOptions)this.PageOptions.Clone(),
				RowSpacing = this.RowSpacing,
				ColSpacing = this.ColSpacing,
				GridLineWidth = this.GridLineWidth,
				GridLineColor = this.GridLineColor
			};
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000AE6B0 File Offset: 0x000AD6B0
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x000AE6F4 File Offset: 0x000AD6F4
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PdfExportPageOptions PageOptions
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜄ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (value != this.ᜄ)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_56;
						}
						break;
					case 3:
						this.ᜄ = value;
						num = 2;
						continue;
					}
					IL_24:
					if (value != null)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_56:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x000AE78C File Offset: 0x000AD78C
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x000AE7D0 File Offset: 0x000AD7D0
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets font of the result PDF headers.")]
		public PdfFont HeaderFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_56;
						}
						break;
					case 1:
						this.ᜀ = value;
						num = 0;
						continue;
					case 2:
						if (value != this.ᜀ)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						num = 2;
						continue;
					}
					IL_24:
					if (value != null)
					{
						num = 3;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_56:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x000AE868 File Offset: 0x000AD868
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x000AE8AC File Offset: 0x000AD8AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets or sets font of the result PDF tiltes.")]
		public PdfFont TitleFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_5E;
						}
						break;
					case 2:
						if (value != this.ᜁ)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						num = 2;
						continue;
					case 4:
						this.ᜁ = value;
						num = 0;
						continue;
					}
					IL_24:
					if (value != null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_5E:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x000AE944 File Offset: 0x000AD944
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x000AE988 File Offset: 0x000AD988
		[Description("Gets or sets font of data exported in the result PDF document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public PdfFont DataFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜂ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜂ)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_5E;
						}
						break;
					case 2:
						num = 0;
						continue;
					case 4:
						this.ᜂ = value;
						num = 1;
						continue;
					}
					IL_24:
					if (true)
					{
					}
					if (value != null)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_5E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000AEA20 File Offset: 0x000ADA20
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x000AEA64 File Offset: 0x000ADA64
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets or sets font of the result PDF footers.")]
		public PdfFont FooterFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜃ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_08;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_08;
						default:
							goto IL_5E;
						}
						break;
					case 2:
						num = 4;
						continue;
					case 3:
						this.ᜃ = value;
						num = 1;
						continue;
					case 4:
						if (value != this.ᜃ)
						{
							num = 3;
							continue;
						}
						return;
					}
					IL_2C:
					if (value != null)
					{
						num = 2;
						continue;
					}
					return;
					IL_08:
					if (true)
					{
					}
					goto IL_2C;
				}
				IL_5E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x000AEAFC File Offset: 0x000ADAFC
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x000AEB40 File Offset: 0x000ADB40
		[DefaultValue(1.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets internal row spacing in the result PDF document.")]
		public double RowSpacing
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜅ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.ᜅ = value;
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						if (false)
						{
						}
						if (value == this.ᜅ)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x000AEBBC File Offset: 0x000ADBBC
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x000AEC00 File Offset: 0x000ADC00
		[DefaultValue(3.0)]
		[Description("Gets or sets internal column spacing in the result PDF document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public double ColSpacing
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜆ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜆ = value;
						num = 0;
						continue;
					}
					IL_1C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (value == this.ᜆ)
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000AEC7C File Offset: 0x000ADC7C
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x000AECC0 File Offset: 0x000ADCC0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(1)]
		[Description("Gets or sets the width of the grid table lines in the result PDF document.")]
		public int GridLineWidth
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜇ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜇ = value;
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					IL_1C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						if (false)
						{
						}
						if (value == this.ᜇ)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x000AED3C File Offset: 0x000ADD3C
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x000AED80 File Offset: 0x000ADD80
		[DefaultValue(typeof(Color), "Black")]
		[Description("Gets or sets the color of the grid table lines in the result PDF document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color GridLineColor
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜈ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜈ = value;
						num = 2;
						continue;
					case 2:
						goto IL_69;
					}
					IL_1C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						if (false)
						{
						}
						if (!(value != this.ᜈ))
						{
							goto IL_6B;
						}
						num = 1;
						break;
					}
				}
				IL_69:
				IL_6B:
				if (true)
				{
				}
			}
		}

		// Token: 0x04000BA4 RID: 2980
		private float \u2593\u0098\u0083\u008A;

		// Token: 0x04000BA5 RID: 2981
		private PdfFont ᜀ;

		// Token: 0x04000BA6 RID: 2982
		private PdfFont ᜁ;

		// Token: 0x04000BA7 RID: 2983
		private PdfFont ᜂ;

		// Token: 0x04000BA8 RID: 2984
		private PdfFont ᜃ;

		// Token: 0x04000BA9 RID: 2985
		private PdfExportPageOptions ᜄ;

		// Token: 0x04000BAA RID: 2986
		private double ᜅ;

		// Token: 0x04000BAB RID: 2987
		private byte[] \u2460\u0087\u0096\u0094;

		// Token: 0x04000BAC RID: 2988
		private double ᜆ;

		// Token: 0x04000BAD RID: 2989
		private string \u25D8\u00A1\u0085\u0095;

		// Token: 0x04000BAE RID: 2990
		private int ᜇ;

		// Token: 0x04000BAF RID: 2991
		private Color ᜈ;
	}
}

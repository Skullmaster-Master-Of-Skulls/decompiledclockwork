using System;
using System.ComponentModel;
using System.Drawing;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.PDF
{
	// Token: 0x0200022A RID: 554
	public class PdfFont : ICloneable
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x000AF804 File Offset: 0x000AE804
		public PdfFont()
		{
			int a_ = 5;
			this.ᜀ = string.Empty;
			this.ᜂ = 10;
			this.ᜃ = PdfFontEncoding.WinAnsiEncoding;
			this.ᜄ = Color.Black;
			this.ᜇ = Color.Black;
			this.ᜈ = new int[256];
			base..ctor();
			this.ᜅ = new Font(HyperlinksCollectionEditor.b("怠儢䰤䘦䔨", a_), (float)this.ᜂ);
			this.CalcFontWidth();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000AF888 File Offset: 0x000AE888
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
			PdfFont pdfFont = new PdfFont();
			pdfFont.FontName = this.FontName;
			pdfFont.PdfFontName = this.PdfFontName;
			pdfFont.Encoding = this.Encoding;
			pdfFont.Size = this.Size;
			pdfFont.Color = this.Color;
			pdfFont.CustomFont = (Font)this.CustomFont.Clone();
			pdfFont.CustomFontColor = this.CustomFontColor;
			pdfFont.AllowCustomFont = this.AllowCustomFont;
			pdfFont.CalcFontWidth();
			return pdfFont;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000AF93C File Offset: 0x000AE93C
		public void CalcFontWidth()
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						Font font = (Font)this.ᜅ.Clone();
						Graphics graphics = Graphics.FromHwnd((IntPtr)0);
						int num = 0;
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							int num2 = 0;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_75;
								case 1:
									goto IL_75;
								case 2:
									if (num > this.ᜈ.GetUpperBound(0))
									{
										num2 = 3;
										continue;
									}
									this.ᜈ[num] = (int)(graphics.MeasureString(Convert.ToChar(num).ToString(), font).Width / 1.5f);
									num++;
									num2 = 1;
									continue;
								case 3:
									return;
								}
								break;
								IL_75:
								num2 = 2;
							}
							break;
						}
						}
					}
					break;
				}
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000AFA30 File Offset: 0x000AEA30
		public int GetWidth(int Numb)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 1:
					if (Numb >= 256)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_AB;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (Numb < 0)
				{
					break;
				}
				num = 2;
			}
			IL_5D:
			throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氞栠䴢䄤䈦儨搪堬嬮縰唲眴堶䰸唺夼䰾", a_)), Numb));
			IL_A9:
			goto IL_5D;
			IL_AB:
			return this.ᜈ[Numb];
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000AFAF0 File Offset: 0x000AEAF0
		public int ReturnFontLength()
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
			return this.ᜈ.Length;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000AFB34 File Offset: 0x000AEB34
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x000AFB78 File Offset: 0x000AEB78
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FontName
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
				return this.ᜀ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜀ = value;
						goto IL_69;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (!(value != this.ᜀ))
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					IL_69:
					num = 2;
				}
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x000AFBF8 File Offset: 0x000AEBF8
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x000AFC3C File Offset: 0x000AEC3C
		[Description("Defines the name of the font in the result PDF document.")]
		[DefaultValue(PdfFontName.Helvetica)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PdfFontName PdfFontName
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ = value;
						goto IL_64;
					case 1:
						return;
					}
					if (value == this.ᜁ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_64:
					num = 1;
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000AFCB8 File Offset: 0x000AECB8
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x000AFCFC File Offset: 0x000AECFC
		[DefaultValue(PdfFontEncoding.WinAnsiEncoding)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the character encoding of the result PDF document.")]
		public PdfFontEncoding Encoding
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
				return this.ᜃ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜃ = value;
						goto IL_5C;
					case 2:
						return;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_5C:
					if (true)
					{
					}
					num = 2;
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x000AFD78 File Offset: 0x000AED78
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x000AFDBC File Offset: 0x000AEDBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(10)]
		[Description("Gets or sets font size of the result PDF Document.")]
		public int Size
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
				return this.ᜂ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜂ = value;
						goto IL_64;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜂ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_64:
					num = 2;
				}
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x000AFE38 File Offset: 0x000AEE38
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x000AFE7C File Offset: 0x000AEE7C
		[Description("Gets or sets font color of the result PDF Document.")]
		[DefaultValue(typeof(Color), "Black")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color Color
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.ᜄ = value;
						goto IL_69;
					}
					if (true)
					{
					}
					if (!(value != this.ᜄ))
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					IL_69:
					num = 0;
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000AFEFC File Offset: 0x000AEEFC
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x000AFF40 File Offset: 0x000AEF40
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines user font.")]
		public Font CustomFont
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
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜅ != value)
							{
								num = 3;
								continue;
							}
							return;
						case 1:
							if (true)
							{
							}
							break;
						case 2:
							return;
						case 3:
							this.ᜅ.Dispose();
							this.ᜅ = value;
							this.CalcFontWidth();
							num = 2;
							continue;
						case 4:
							num = 0;
							continue;
						}
						if (value == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 4;
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000AFFEC File Offset: 0x000AEFEC
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x000B0030 File Offset: 0x000AF030
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("This property is used for allowing to use the user font.")]
		public bool AllowCustomFont
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
				return this.ᜆ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜆ = value;
						goto IL_64;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (this.ᜆ == value)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					IL_64:
					num = 2;
				}
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000B00AC File Offset: 0x000AF0AC
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x000B00F0 File Offset: 0x000AF0F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Black")]
		[Description("Defines the color of the user font.")]
		public Color CustomFontColor
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
				return this.ᜇ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						this.ᜇ = value;
						goto IL_69;
					case 2:
						return;
					}
					if (!(value != this.ᜇ))
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_69:
					num = 2;
				}
			}
		}

		// Token: 0x04000BE0 RID: 3040
		private string ᜀ;

		// Token: 0x04000BE1 RID: 3041
		private PdfFontName ᜁ;

		// Token: 0x04000BE2 RID: 3042
		private bool \u25D8\u008D\u009E\u009D;

		// Token: 0x04000BE3 RID: 3043
		private int ᜂ;

		// Token: 0x04000BE4 RID: 3044
		private PdfFontEncoding ᜃ;

		// Token: 0x04000BE5 RID: 3045
		private Color ᜄ;

		// Token: 0x04000BE6 RID: 3046
		private Font ᜅ;

		// Token: 0x04000BE7 RID: 3047
		private bool ᜆ;

		// Token: 0x04000BE8 RID: 3048
		private Color ᜇ;

		// Token: 0x04000BE9 RID: 3049
		private int[] ᜈ;
	}
}

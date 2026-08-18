using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001B9 RID: 441
	public class CellFont
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x00081450 File Offset: 0x00080450
		public CellFont()
		{
			int a_ = 13;
			this.ᜀ = 10f;
			this.ᜄ = 1;
			this.ᜅ = HyperlinksCollectionEditor.b("栨太䐬丮崰", a_);
			base..ctor();
			this.SetDefault();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00081498 File Offset: 0x00080498
		public void SetDefault()
		{
			int a_ = 7;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ = 10f;
			this.ᜁ = CellColor.Black;
			this.ᜂ = XlsFontScript.None;
			this.ᜃ = XlsFontUnderline.None;
			this.ᜄ = 1;
			this.ᜅ = HyperlinksCollectionEditor.b("戢圤並䠨䜪", a_);
			this.ᜆ = 0;
			this.ᜇ = false;
			this.ᜈ = false;
			this.ᜉ = false;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00081534 File Offset: 0x00080534
		public bool IsEqual(CellFont Font)
		{
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_119;
				case 1:
					num = 3;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					if (this.ᜉ == Font.Strikeout)
					{
						num = 15;
						continue;
					}
					return false;
				case 4:
					if (this.ᜃ == Font.Underline)
					{
						num = 7;
						continue;
					}
					return false;
				case 5:
					if (this.ᜈ == Font.Italic)
					{
						num = 1;
						continue;
					}
					return false;
				case 6:
					if (this.ᜁ == Font.Color)
					{
						num = 11;
						continue;
					}
					return false;
				case 7:
					num = 17;
					continue;
				case 8:
					return false;
				case 9:
					if (this.ᜇ != Font.Bold)
					{
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				case 10:
					if (this.ᜂ == Font.Script)
					{
						num = 2;
						continue;
					}
					return false;
				case 11:
					if (true)
					{
					}
					num = 10;
					continue;
				case 13:
					num = 9;
					continue;
				case 14:
					num = 5;
					continue;
				case 15:
					num = 6;
					continue;
				case 16:
					if (this.ᜀ == Font.Size)
					{
						num = 13;
						continue;
					}
					return false;
				case 17:
					if (this.ᜄ == Font.Charset)
					{
						num = 0;
						continue;
					}
					return false;
				}
				goto IL_58;
				IL_5B:
				num = 8;
				continue;
				IL_58:
				if (Font == null)
				{
					goto IL_5B;
				}
				num = 16;
			}
			return false;
			IL_119:
			return string.Compare(this.ᜅ, Font.Name, true) == 0;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00081734 File Offset: 0x00080734
		internal unsafe void ᜀ(spr\u20CC A_0)
		{
			switch (0)
			{
			default:
			{
				for (;;)
				{
					IL_77:
					A_0.ᜨ();
					A_0.ᜀ((ushort)(this.ᜀ * 20f));
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_232:
						goto IL_27C;
					default:
						if (false)
						{
						}
						num = 3;
						break;
					}
					XlsFontUnderline xlsFontUnderline;
					for (;;)
					{
						IL_10:
						switch (num)
						{
						case 0:
						{
							byte[] array;
							if ((array = A_0.ᜢ()) != null)
							{
								num = 6;
								continue;
							}
							goto IL_140;
						}
						case 1:
							switch (xlsFontUnderline)
							{
							case XlsFontUnderline.Single:
								A_0.ᜁ(1);
								num = 12;
								continue;
							case XlsFontUnderline.Double:
								A_0.ᜁ(2);
								num = 20;
								continue;
							case XlsFontUnderline.SingleAccounting:
								A_0.ᜁ(33);
								num = 17;
								continue;
							case XlsFontUnderline.DoubleAccounting:
								A_0.ᜁ(34);
								num = 15;
								continue;
							default:
								num = 16;
								continue;
							}
							break;
						case 2:
							if (true)
							{
							}
							goto IL_105;
						case 3:
							if (this.ᜈ)
							{
								num = 13;
								continue;
							}
							goto IL_154;
						case 4:
							goto IL_232;
						case 5:
							num = 0;
							continue;
						case 6:
							num = 23;
							continue;
						case 7:
							goto IL_1FF;
						case 8:
							if (this.ᜉ)
							{
								num = 10;
								continue;
							}
							goto IL_105;
						case 9:
							goto IL_1D8;
						case 10:
							A_0.ᜄ(A_0.ᜈ() + 8);
							num = 2;
							continue;
						case 11:
							goto IL_154;
						case 12:
							goto IL_2D8;
						case 13:
							A_0.ᜄ(A_0.ᜈ() + 2);
							num = 11;
							continue;
						case 14:
							if (this.ᜇ)
							{
								num = 22;
								continue;
							}
							A_0.ᜃ(400);
							num = 4;
							continue;
						case 15:
							goto IL_E8;
						case 16:
							num = 5;
							continue;
						case 17:
							goto IL_2F8;
						case 18:
							goto IL_1FF;
						case 19:
							goto IL_216;
						case 20:
							goto IL_100;
						case 21:
							goto IL_140;
						case 22:
							A_0.ᜃ(700);
							num = 9;
							continue;
						case 23:
						{
							byte[] array;
							if (array.Length == 0)
							{
								num = 21;
								continue;
							}
							fixed (byte* ptr = &array[0])
							{
								num = 7;
								continue;
								break;
							}
						}
						}
						goto IL_77;
						IL_105:
						A_0.ᜂ((ushort)spr\u2009.᠓[(int)((byte)this.ᜁ)]);
						num = 14;
						continue;
						IL_140:
						byte* ptr = null;
						num = 18;
						continue;
						IL_154:
						num = 8;
						continue;
						IL_1FF:
						((spr\u221A*)ptr)->ᜅ = 0;
						ptr = null;
						num = 19;
					}
					IL_1D8:
					IL_27C:
					A_0.ᜁ((ushort)((byte)this.ᜂ));
					xlsFontUnderline = this.ᜃ;
					num = 1;
					goto IL_10;
				}
				IL_E8:
				IL_100:
				IL_216:
				IL_2D8:
				IL_2F8:
				A_0.ᜅ(this.ᜄ);
				int length = this.ᜅ.Length;
				A_0.ᜀ((byte)length);
				A_0.ᜃ(1);
				A_0.ᜀ(this.ᜅ);
				return;
			}
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00081A70 File Offset: 0x00080A70
		public void Assign(object SourceFont, Color FontColor)
		{
			int a_ = 4;
			int num = 57;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4CA;
				case 1:
					this.ᜁ = CellColor.Turquoise;
					num = 45;
					continue;
				case 2:
					goto IL_418;
				case 3:
					return;
				case 4:
					this.ᜁ = CellColor.Violet;
					num = 48;
					continue;
				case 5:
					if (FontColor == System.Drawing.Color.Red)
					{
						num = 39;
						continue;
					}
					num = 52;
					continue;
				case 6:
					this.ᜁ = CellColor.Blue;
					num = 34;
					continue;
				case 7:
					if (FontColor == System.Drawing.Color.Blue)
					{
						num = 6;
						continue;
					}
					num = 16;
					continue;
				case 8:
					if (FontColor == System.Drawing.Color.Olive)
					{
						num = 61;
						continue;
					}
					num = 62;
					continue;
				case 9:
					if (SourceFont is Font)
					{
						num = 38;
						continue;
					}
					goto IL_787;
				case 10:
					if (FontColor == System.Drawing.Color.White)
					{
						num = 31;
						continue;
					}
					this.ᜁ = CellColor.Black;
					num = 26;
					continue;
				case 11:
					goto IL_782;
				case 12:
					goto IL_3D8;
				case 13:
					if (FontColor == System.Drawing.Color.Yellow)
					{
						num = 63;
						continue;
					}
					num = 7;
					continue;
				case 14:
					goto IL_76A;
				case 15:
					goto IL_3C0;
				case 16:
					if (FontColor == System.Drawing.Color.Fuchsia)
					{
						num = 28;
						continue;
					}
					num = 17;
					continue;
				case 17:
					if (FontColor == System.Drawing.Color.Aqua)
					{
						num = 1;
						continue;
					}
					num = 10;
					continue;
				case 18:
					this.ᜁ = CellColor.DarkBlue;
					num = 23;
					continue;
				case 19:
					this.ᜇ = true;
					num = 30;
					continue;
				case 20:
					goto IL_4B2;
				case 21:
					if (FontColor == System.Drawing.Color.Green)
					{
						num = 60;
						continue;
					}
					num = 8;
					continue;
				case 22:
					goto IL_4F0;
				case 23:
					goto IL_686;
				case 24:
					if (FontColor == System.Drawing.Color.Silver)
					{
						num = 43;
						continue;
					}
					num = 5;
					continue;
				case 25:
					if (FontColor == System.Drawing.Color.Teal)
					{
						num = 56;
						continue;
					}
					num = 53;
					continue;
				case 26:
					goto IL_66F;
				case 27:
					goto IL_1F4;
				case 28:
					this.ᜁ = CellColor.Pink;
					num = 14;
					continue;
				case 29:
					this.ᜃ = XlsFontUnderline.Single;
					num = 58;
					continue;
				case 30:
					goto IL_335;
				case 31:
					this.ᜁ = CellColor.White;
					num = 55;
					continue;
				case 32:
					goto IL_188;
				case 33:
					if (FontColor == System.Drawing.Color.Maroon)
					{
						num = 41;
						continue;
					}
					num = 21;
					continue;
				case 34:
					goto IL_46C;
				case 35:
					this.ᜁ = CellColor.Gray50Percent;
					num = 51;
					continue;
				case 36:
					if (((SourceFont as Font).Style & FontStyle.Italic) == FontStyle.Italic)
					{
						num = 47;
						continue;
					}
					goto IL_2EB;
				case 37:
					this.ᜁ = CellColor.BrightGreen;
					num = 11;
					continue;
				case 38:
					this.ᜀ = (SourceFont as Font).Size;
					this.ᜃ = XlsFontUnderline.None;
					num = 46;
					continue;
				case 39:
					this.ᜁ = CellColor.Red;
					num = 27;
					continue;
				case 40:
					goto IL_2EB;
				case 41:
					this.ᜁ = CellColor.DarkRed;
					num = 54;
					continue;
				case 42:
					if (SourceFont is CellFont)
					{
						num = 22;
						continue;
					}
					num = 9;
					continue;
				case 43:
					this.ᜁ = CellColor.Gray25Percent;
					num = 2;
					continue;
				case 44:
					if (((SourceFont as Font).Style & FontStyle.Underline) == FontStyle.Underline)
					{
						num = 29;
						continue;
					}
					goto IL_360;
				case 45:
					goto IL_3A8;
				case 46:
					if (((SourceFont as Font).Style & FontStyle.Bold) == FontStyle.Bold)
					{
						num = 19;
						continue;
					}
					goto IL_335;
				case 47:
					this.ᜈ = true;
					num = 40;
					continue;
				case 48:
					goto IL_580;
				case 49:
					goto IL_23C;
				case 50:
					if (FontColor == System.Drawing.Color.Purple)
					{
						num = 4;
						continue;
					}
					num = 25;
					continue;
				case 51:
					goto IL_237;
				case 52:
					if (FontColor == System.Drawing.Color.Lime)
					{
						num = 37;
						continue;
					}
					num = 13;
					continue;
				case 53:
					if (FontColor == System.Drawing.Color.Gray)
					{
						num = 35;
						continue;
					}
					num = 24;
					continue;
				case 54:
					goto IL_483;
				case 55:
					goto IL_598;
				case 56:
					this.ᜁ = CellColor.Teal;
					num = 0;
					continue;
				case 58:
					goto IL_360;
				case 59:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_188;
					default:
						if (false)
						{
						}
						if (((SourceFont as Font).Style & FontStyle.Strikeout) == FontStyle.Strikeout)
						{
							num = 32;
							continue;
						}
						goto IL_23C;
					}
					break;
				case 60:
					this.ᜁ = CellColor.Green;
					num = 20;
					continue;
				case 61:
					this.ᜁ = CellColor.DarkYellow;
					num = 12;
					continue;
				case 62:
					if (FontColor == System.Drawing.Color.Navy)
					{
						num = 18;
						continue;
					}
					num = 50;
					continue;
				case 63:
					this.ᜁ = CellColor.Yellow;
					num = 15;
					continue;
				}
				if (SourceFont == null)
				{
					num = 3;
					continue;
				}
				num = 42;
				continue;
				IL_188:
				this.ᜉ = true;
				num = 49;
				continue;
				IL_23C:
				num = 44;
				continue;
				IL_2EB:
				num = 59;
				continue;
				IL_335:
				num = 36;
				continue;
				IL_360:
				num = 33;
			}
			return;
			IL_1F4:
			IL_237:
			IL_295:
			this.ᜂ = XlsFontScript.None;
			this.ᜄ = (SourceFont as Font).GdiCharSet;
			this.ᜅ = (SourceFont as Font).Name;
			return;
			IL_3A8:
			IL_3C0:
			IL_3D8:
			IL_418:
			IL_46C:
			IL_483:
			IL_4B2:
			IL_4CA:
			goto IL_295;
			IL_4F0:
			this.ᜀ = (SourceFont as CellFont).Size;
			this.ᜇ = (SourceFont as CellFont).Bold;
			this.ᜈ = (SourceFont as CellFont).Italic;
			this.ᜉ = (SourceFont as CellFont).Strikeout;
			this.ᜁ = (SourceFont as CellFont).Color;
			this.ᜂ = (SourceFont as CellFont).Script;
			this.ᜃ = (SourceFont as CellFont).Underline;
			this.ᜄ = (SourceFont as CellFont).Charset;
			this.ᜅ = (SourceFont as CellFont).Name;
			return;
			IL_580:
			IL_598:
			IL_66F:
			goto IL_295;
			IL_686:
			if (true)
			{
			}
			IL_76A:
			IL_782:
			goto IL_295;
			IL_787:
			throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("椟䰡刣䜥䐧䌩䠫愭䀯圱䘳圵䰷匹医倽Ἷ́㝃㕅ⅇⵉ≋ᡍㅏ㹑⅓㍕", a_)), SourceFont.ToString(), this.ToString()));
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00082234 File Offset: 0x00081234
		public void AssignTo(ref Font DestFont, out Color FontColor)
		{
			byte[] bytes;
			for (;;)
			{
				FontStyle fontStyle = FontStyle.Regular;
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						fontStyle |= FontStyle.Italic;
						num = 6;
						continue;
					case 1:
						goto IL_6B;
					case 2:
						if (bytes.Length >= 3)
						{
							num = 8;
							continue;
						}
						goto IL_1E2;
					case 3:
						fontStyle |= FontStyle.Bold;
						num = 10;
						continue;
					case 4:
						if (this.ᜉ)
						{
							num = 11;
							continue;
						}
						goto IL_84;
					case 5:
						if (DestFont != null)
						{
							num = 12;
							continue;
						}
						goto IL_F3;
					case 6:
						goto IL_1BF;
					case 7:
						fontStyle |= FontStyle.Underline;
						num = 1;
						continue;
					case 8:
						goto IL_13F;
					case 9:
						goto IL_167;
					case 10:
						goto IL_B1;
					case 11:
						fontStyle |= FontStyle.Strikeout;
						num = 16;
						continue;
					case 12:
						DestFont.Dispose();
						num = 9;
						continue;
					case 13:
						if (this.Underline != XlsFontUnderline.None)
						{
							num = 7;
							continue;
						}
						goto IL_6B;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_167;
						default:
							if (false)
							{
							}
							if (this.ᜈ)
							{
								num = 0;
								continue;
							}
							goto IL_1BF;
						}
						break;
					case 15:
						if (this.ᜇ)
						{
							num = 3;
							continue;
						}
						goto IL_B1;
					case 16:
						goto IL_84;
					}
					break;
					IL_6B:
					num = 5;
					continue;
					IL_84:
					num = 13;
					continue;
					IL_B1:
					num = 14;
					continue;
					IL_F3:
					DestFont = new Font(this.Name, this.Size, fontStyle, GraphicsUnit.Point, this.Charset);
					bytes = BitConverter.GetBytes(spr\u2009.᠑[(int)this.ᜁ]);
					num = 2;
					continue;
					IL_167:
					if (true)
					{
					}
					goto IL_F3;
					IL_1BF:
					num = 4;
				}
			}
			IL_13F:
			FontColor = System.Drawing.Color.FromArgb((int)bytes[0], (int)bytes[1], (int)bytes[2]);
			return;
			IL_1E2:
			FontColor = System.Drawing.Color.Black;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00082430 File Offset: 0x00081430
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00082474 File Offset: 0x00081474
		[DefaultValue(10f)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public float Size
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
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								this.ᜀ = value;
								num = 2;
								continue;
							case 2:
								return;
							}
							if (value == this.ᜀ)
							{
								return;
							}
							num = 0;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000824F0 File Offset: 0x000814F0
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00082534 File Offset: 0x00081534
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(CellColorEditor), typeof(UITypeEditor))]
		[DefaultValue(CellColor.Black)]
		public CellColor Color
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
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							switch (num)
							{
							case 1:
								return;
							case 2:
								this.ᜁ = value;
								num = 1;
								continue;
							}
							if (value == this.ᜁ)
							{
								return;
							}
							num = 2;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x000825B0 File Offset: 0x000815B0
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x000825F4 File Offset: 0x000815F4
		[DefaultValue(XlsFontScript.None)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public XlsFontScript Script
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
				return this.ᜂ;
			}
			set
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								return;
							case 1:
								this.ᜂ = value;
								num = 0;
								continue;
							}
							if (value == this.ᜂ)
							{
								return;
							}
							num = 1;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00082670 File Offset: 0x00081670
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x000826B4 File Offset: 0x000816B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(XlsFontUnderline.None)]
		public XlsFontUnderline Underline
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜃ = value;
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (value == this.ᜃ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00082730 File Offset: 0x00081730
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00082774 File Offset: 0x00081774
		[DefaultValue(1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public byte Charset
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = value;
						num = 1;
						continue;
					case 1:
						return;
					}
					if (value == this.ᜄ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x000827F0 File Offset: 0x000817F0
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00082834 File Offset: 0x00081834
		[DefaultValue("Arial")]
		[Editor(typeof(sprᣓ), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Name
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
				return this.ᜅ;
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
						this.ᜅ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (!(value != this.ᜅ))
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000828B4 File Offset: 0x000818B4
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x000828F8 File Offset: 0x000818F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int FontIndex
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
						this.ᜆ = value;
						num = 1;
						continue;
					case 1:
						goto IL_64;
					}
					if (value == this.ᜆ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_66;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
				IL_64:
				IL_66:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00082974 File Offset: 0x00081974
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x000829B8 File Offset: 0x000819B8
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Bold
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜇ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.ᜇ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00082A34 File Offset: 0x00081A34
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00082A78 File Offset: 0x00081A78
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public bool Italic
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
				return this.ᜈ;
			}
			set
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜈ = value;
						num = 1;
						continue;
					case 1:
						return;
					}
					if (value == this.ᜈ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00082AF4 File Offset: 0x00081AF4
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00082B38 File Offset: 0x00081B38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public bool Strikeout
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
				return this.ᜉ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜉ = value;
						num = 2;
						continue;
					case 2:
						goto IL_64;
					}
					if (value == this.ᜉ)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_66;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
				IL_64:
				IL_66:
				if (true)
				{
				}
			}
		}

		// Token: 0x04000966 RID: 2406
		private float ᜀ;

		// Token: 0x04000967 RID: 2407
		private CellColor ᜁ;

		// Token: 0x04000968 RID: 2408
		private XlsFontScript ᜂ;

		// Token: 0x04000969 RID: 2409
		private XlsFontUnderline ᜃ;

		// Token: 0x0400096A RID: 2410
		private byte ᜄ;

		// Token: 0x0400096B RID: 2411
		private string ᜅ;

		// Token: 0x0400096C RID: 2412
		private int ᜆ;

		// Token: 0x0400096D RID: 2413
		private bool ᜇ;

		// Token: 0x0400096E RID: 2414
		private bool \u2593\u0090\u008C\u0081;

		// Token: 0x0400096F RID: 2415
		private bool ᜈ;

		// Token: 0x04000970 RID: 2416
		private bool ᜉ;
	}
}

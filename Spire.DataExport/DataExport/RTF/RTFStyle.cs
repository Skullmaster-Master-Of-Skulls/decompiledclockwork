using System;
using System.ComponentModel;
using System.Drawing;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.RTF
{
	// Token: 0x0200016F RID: 367
	public class RTFStyle : ICloneable
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x000619C4 File Offset: 0x000609C4
		public RTFStyle()
		{
			this.SetDefault();
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00061A08 File Offset: 0x00060A08
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
			return new RTFStyle
			{
				Font = this.Font,
				FontColor = this.FontColor,
				BackgroundColor = this.BackgroundColor,
				HighlightColor = this.HighlightColor,
				AllowHighlight = this.AllowHighlight,
				AllowBackground = this.AllowBackground,
				Alignment = this.Alignment
			};
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00061AA0 File Offset: 0x00060AA0
		public void SetDefault()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					}
					break;
				case 1:
					goto IL_6F;
				case 2:
					goto IL_5A;
				}
				if (this.ᜀ != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_5A:
				this.ᜀ.Dispose();
				num = 1;
			}
			IL_6F:
			this.ᜀ = spr\u2059.ᜀ();
			this.ᜁ = Color.Black;
			this.ᜂ = Color.White;
			this.ᜃ = Color.White;
			this.ᜄ = false;
			this.ᜅ = true;
			this.ᜆ = RtfTextAlignment.Left;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00061B60 File Offset: 0x00060B60
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧搩䴫䌭唯", a_), this.ᜀ.Name);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧礩䔫吭唯", a_), this.ᜀ.Size.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧椩䌫䈭弯䀱", a_), this.ᜁ.ToArgb().ToString(HyperlinksCollectionEditor.b("砟", a_)));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧栩䌫䈭启", a_), ((this.ᜀ.Style & FontStyle.Bold) == FontStyle.Bold).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧挩堫伭尯嬱圳", a_), ((this.ᜀ.Style & FontStyle.Italic) == FontStyle.Italic).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧缩䈫䨭唯䀱堳張嘷弹", a_), ((this.ᜀ.Style & FontStyle.Underline) == FontStyle.Underline).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("星䴡䨣別眧礩堫尭夯失儳礵䴷丹", a_), ((this.ᜀ.Style & FontStyle.Strikeout) == FontStyle.Strikeout).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("戟䌡䜣䴥伧堩䌫嬭帯嘱眳夵吷唹主", a_), this.ᜂ.ToArgb().ToString(HyperlinksCollectionEditor.b("砟", a_)));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("栟䬡䌣严䐧䌩䬫䘭䐯焱嬳娵圷䠹", a_), this.ᜃ.ToArgb().ToString(HyperlinksCollectionEditor.b("砟", a_)));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("感両䠣䤥弧戩䔫䤭堯帱崳儵倷丹", a_), this.ᜄ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("感両䠣䤥弧栩䴫䴭嬯唱䘳夵䴷吹堻", a_), this.ᜅ.ToString());
			string key = HyperlinksCollectionEditor.b("感両䴣䄥䘧䜩䤫䀭䐯", a_);
			int num = (int)this.ᜆ;
			File.WriteValue(Section, key, num.ToString());
			File.SaveToFile();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00061DB4 File Offset: 0x00060DB4
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				string familyName;
				int num;
				FontStyle fontStyle;
				for (;;)
				{
					this.SetDefault();
					familyName = File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦木䨪䀬䨮", a_), this.ᜀ.Name);
					num = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦稨䈪圬䨮", a_), this.ᜀ.Size.ToString()));
					this.ᜁ = Color.FromArgb(Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦樨䐪䄬䀮䌰", a_), this.ᜁ.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
					fontStyle = FontStyle.Regular;
					int num2 = 17;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if ((this.ᜀ.Style & FontStyle.Italic) == FontStyle.Italic)
							{
								num2 = 24;
								continue;
							}
							goto IL_2F0;
						case 1:
							goto IL_3A8;
						case 2:
							goto IL_2EB;
						case 3:
							if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦稨弪弬䘮娰嘲稴䈶䴸", a_), ((this.ᜀ.Style & FontStyle.Strikeout) == FontStyle.Strikeout).ToString())))
							{
								num2 = 7;
								continue;
							}
							num2 = 25;
							continue;
						case 4:
							goto IL_203;
						case 5:
							fontStyle ^= FontStyle.Bold;
							if (true)
							{
							}
							num2 = 4;
							continue;
						case 6:
							if (this.ᜀ != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_44B;
						case 7:
							fontStyle |= FontStyle.Strikeout;
							num2 = 8;
							continue;
						case 8:
							goto IL_2C8;
						case 9:
							if ((this.ᜀ.Style & FontStyle.Underline) == FontStyle.Underline)
							{
								num2 = 10;
								continue;
							}
							goto IL_181;
						case 10:
							fontStyle ^= FontStyle.Underline;
							num2 = 14;
							continue;
						case 11:
							if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦簨䔪䤬䨮䌰弲尴夶尸", a_), ((this.ᜀ.Style & FontStyle.Underline) == FontStyle.Underline).ToString())))
							{
								num2 = 20;
								continue;
							}
							num2 = 9;
							continue;
						case 12:
							fontStyle |= FontStyle.Bold;
							num2 = 18;
							continue;
						case 13:
							fontStyle ^= FontStyle.Strikeout;
							num2 = 21;
							continue;
						case 14:
							goto IL_181;
						case 15:
							goto IL_181;
						case 16:
							goto IL_2F0;
						case 17:
							if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦欨䐪䄬䬮", a_), ((this.ᜀ.Style & FontStyle.Bold) == FontStyle.Bold).ToString())))
							{
								num2 = 12;
								continue;
							}
							num2 = 22;
							continue;
						case 18:
							goto IL_203;
						case 19:
							if (!Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("夞丠䴢儤砦怨弪䰬䌮堰倲", a_), ((this.ᜀ.Style & FontStyle.Italic) == FontStyle.Italic).ToString())))
							{
								num2 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2EB;
							default:
								if (false)
								{
								}
								num2 = 26;
								continue;
							}
							break;
						case 20:
							fontStyle |= FontStyle.Underline;
							num2 = 15;
							continue;
						case 21:
							goto IL_2C8;
						case 22:
							if ((this.ᜀ.Style & FontStyle.Bold) == FontStyle.Bold)
							{
								num2 = 5;
								continue;
							}
							goto IL_203;
						case 23:
							goto IL_2F0;
						case 24:
							fontStyle ^= FontStyle.Italic;
							num2 = 16;
							continue;
						case 25:
							if ((this.ᜀ.Style & FontStyle.Strikeout) == FontStyle.Strikeout)
							{
								num2 = 13;
								continue;
							}
							goto IL_2C8;
						case 26:
							fontStyle |= FontStyle.Italic;
							num2 = 23;
							continue;
						}
						break;
						IL_181:
						num2 = 3;
						continue;
						IL_203:
						num2 = 19;
						continue;
						IL_2C8:
						num2 = 6;
						continue;
						IL_2EB:
						this.ᜀ.Dispose();
						num2 = 1;
						continue;
						IL_2F0:
						num2 = 11;
					}
				}
				IL_3A8:
				IL_44B:
				this.ᜀ = new Font(familyName, (float)num, fontStyle);
				this.ᜂ = Color.FromArgb(Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("崞䀠䀢两䀦嬨䐪堬䄮唰瀲娴嬶嘸䤺", a_), this.ᜂ.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				this.ᜃ = Color.FromArgb(Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("圞䠠䐢䴤䬦䀨䰪䔬嬮爰尲头堶䬸", a_), this.ᜃ.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				this.ᜄ = Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("帞䴠伢䨤倦愨䈪䨬䜮崰娲刴弶䴸", a_), this.ᜄ.ToString()));
				this.ᜅ = Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("帞䴠伢䨤倦欨䨪丬䐮嘰䄲娴䈶圸强", a_), this.ᜅ.ToString()));
				string key = HyperlinksCollectionEditor.b("帞䴠䨢䈤䤦䐨个䌬嬮", a_);
				int num3 = (int)this.ᜆ;
				this.ᜆ = (RtfTextAlignment)Convert.ToInt32(File.ReadValue(Section, key, num3.ToString()));
				return;
			}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00062334 File Offset: 0x00061334
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00062378 File Offset: 0x00061378
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines text font parameters for the current style.")]
		public Font Font
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
						this.ᜀ.Dispose();
						this.ᜀ = (value.Clone() as Font);
						goto IL_58;
					case 2:
						num = 4;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						}
						goto Block_2;
					case 4:
						if (value != this.ᜀ)
						{
							num = 1;
							continue;
						}
						return;
					}
					if (value != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					return;
					IL_58:
					num = 3;
				}
				Block_2:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00062430 File Offset: 0x00061430
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00062474 File Offset: 0x00061474
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Black")]
		[Description("Defines font color for the current style.")]
		public Color FontColor
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						return;
					case 2:
						goto IL_60;
					}
					if (value != this.ᜁ)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
					IL_60:
					this.ᜁ = value;
					num = 1;
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000624F4 File Offset: 0x000614F4
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00062538 File Offset: 0x00061538
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the background color in the current style.")]
		[DefaultValue(typeof(Color), "White")]
		public Color BackgroundColor
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4E;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value != this.ᜂ)
					{
						num = 0;
						continue;
					}
					break;
					IL_4E:
					this.ᜂ = value;
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000625B8 File Offset: 0x000615B8
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x000625FC File Offset: 0x000615FC
		[DefaultValue(typeof(Color), "White")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the color of the text highlighting in the current style.")]
		public Color HighlightColor
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
						goto IL_60;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					}
					if (value != this.ᜃ)
					{
						num = 1;
						continue;
					}
					break;
					IL_60:
					this.ᜃ = value;
					num = 0;
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0006267C File Offset: 0x0006167C
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x000626C0 File Offset: 0x000616C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Description("Enables highlighting text in the current style.")]
		public bool AllowHighlight
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 1:
						return;
					case 2:
						goto IL_5B;
					}
					if (value != this.ᜄ)
					{
						num = 2;
						continue;
					}
					break;
					IL_5B:
					this.ᜄ = value;
					num = 1;
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0006273C File Offset: 0x0006173C
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00062780 File Offset: 0x00061780
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Description("Enables using the background color in the current style.")]
		public bool AllowBackground
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
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_5B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value != this.ᜅ)
					{
						num = 1;
						continue;
					}
					break;
					IL_5B:
					this.ᜅ = value;
					num = 0;
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000627FC File Offset: 0x000617FC
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x00062840 File Offset: 0x00061840
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(RtfTextAlignment.Left)]
		[Description("Defines the text alignment for the current style.")]
		public RtfTextAlignment Alignment
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
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_5B;
					case 2:
						return;
					}
					if (value != this.ᜆ)
					{
						num = 1;
						continue;
					}
					break;
					IL_5B:
					this.ᜆ = value;
					num = 2;
				}
			}
		}

		// Token: 0x04000751 RID: 1873
		private Font ᜀ;

		// Token: 0x04000752 RID: 1874
		private Color ᜁ = Color.Black;

		// Token: 0x04000753 RID: 1875
		private Color ᜂ = Color.White;

		// Token: 0x04000754 RID: 1876
		private Color ᜃ = Color.White;

		// Token: 0x04000755 RID: 1877
		private bool ᜄ;

		// Token: 0x04000756 RID: 1878
		private bool[] \u2609\u0080\u00AB\u00A7;

		// Token: 0x04000757 RID: 1879
		private byte \u2460\u009B\u008F\u00AF;

		// Token: 0x04000758 RID: 1880
		private byte[] \u25D9\u008E\u00A7\u0082;

		// Token: 0x04000759 RID: 1881
		private string \u2593\u0096\u00AF\u00A4;

		// Token: 0x0400075A RID: 1882
		private bool ᜅ = true;

		// Token: 0x0400075B RID: 1883
		private RtfTextAlignment ᜆ;
	}
}

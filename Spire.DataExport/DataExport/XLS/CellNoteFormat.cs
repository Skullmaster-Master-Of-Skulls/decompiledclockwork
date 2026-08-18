using System;
using System.ComponentModel;
using System.Drawing;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001D9 RID: 473
	public class CellNoteFormat : CustomItem, ICloneable
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x0009E83C File Offset: 0x0009D83C
		public CellNoteFormat()
		{
			this.SetDefault();
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0009E8A0 File Offset: 0x0009D8A0
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
			return new CellNoteFormat
			{
				Alignment = this.Alignment,
				BackgroundColor = this.BackgroundColor,
				FillType = this.FillType,
				Font = this.Font,
				ForegroundColor = this.ForegroundColor,
				Gradient = this.Gradient,
				Orientation = this.Orientation,
				Transparency = this.Transparency
			};
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0009E944 File Offset: 0x0009D944
		internal override void InitCollectionItem()
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
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0009E980 File Offset: 0x0009D980
		public void SetDefault()
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.Horizontal = HorizontalAlignment.Left;
			this.ᜀ.Vertical = VerticalAlignment.Top;
			this.ᜁ = Color.FromArgb(255, 255, 225);
			this.ᜂ = Color.FromArgb(255, 255, 225);
			this.ᜃ = CellNoteFillType.Solid;
			this.ᜄ.Size = 8f;
			this.ᜄ.Bold = true;
			this.ᜄ.Italic = false;
			this.ᜄ.Strikeout = false;
			this.ᜄ.Name = HyperlinksCollectionEditor.b("紨䨪䔬䀮尰刲", a_);
			this.ᜅ = 0;
			this.ᜆ = CellOrientation.NoRotation;
			this.ᜇ = CellNoteGradient.Horizontal;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0009EA7C File Offset: 0x0009DA7C
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴礶堸嘺堼", a_), this.ᜄ.Name);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴搶倸䄺堼", a_), this.ᜄ.Size.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴琶嘸场刼䴾", a_), sprᮌ.ᜀ(this.ᜄ.Color));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴甶嘸场夼", a_), this.ᜄ.Bold.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴縶䴸娺儼嘾≀", a_), this.ᜄ.Italic.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴搶䴸䤺吼吾⑀ూい㍆", a_), this.ᜄ.Strikeout.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮弰䜲樴戶圸强堼䴾ⵀ⩂⭄≆", a_), ((int)this.ᜄ.Underline).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("攬䀮䌰爲头帶常唺值娾⽀㝂", a_), ((int)this.ᜀ.Horizontal).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("第䨮䌰䜲琴嬶倸尺匼刾⑀ⵂㅄ", a_), ((int)this.ᜀ.Vertical).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("漬丮到堲刴䔶嘸为匼嬾ɀⱂ⥄⡆㭈", a_), this.ᜁ.ToArgb().ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欬䀮䌰嘲刴䔶嘸为匼嬾ɀⱂ⥄⡆㭈", a_), this.ᜂ.ToArgb().ToString());
			string key = HyperlinksCollectionEditor.b("欬䘮崰弲愴丶䤸帺", a_);
			int num = (int)this.ᜃ;
			File.WriteValue(Section, key, num.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("礬崮倰崲䘴䜶堸䤺堼儾≀㩂", a_), this.ᜅ.ToString());
			string key2 = HyperlinksCollectionEditor.b("戬崮堰嘲嬴䌶堸伺吼倾⽀", a_);
			int num2 = (int)this.ᜆ;
			File.WriteValue(Section, key2, num2.ToString());
			string key3 = HyperlinksCollectionEditor.b("樬崮倰圲尴制圸伺", a_);
			int num3 = (int)this.ᜇ;
			File.WriteValue(Section, key3, num3.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0009ED08 File Offset: 0x0009DD08
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 12;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_76:
					this.ᜄ.Name = File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯簱唳嬵崷", a_), HyperlinksCollectionEditor.b("簧䬩䐫䄭崯匱", a_));
					this.ᜄ.Size = Convert.ToSingle(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯愱崳䰵崷", a_), 8.ToString()));
					this.ᜄ.Color = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯焱嬳娵圷䠹", a_), sprᮌ.ᜀ(CellColor.Black)));
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								this.ᜄ.Bold = true;
								num = 3;
								continue;
							case 1:
								if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯瀱嬳娵尷", a_), true.ToString())))
								{
									num = 0;
									continue;
								}
								this.ᜄ.Bold = false;
								num = 8;
								continue;
							case 2:
								this.ᜄ.Strikeout = true;
								num = 7;
								continue;
							case 3:
								if (true)
								{
								}
								goto IL_228;
							case 4:
								goto IL_268;
							case 5:
								if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯愱䀳䐵儷儹夻焽㔿㙁", a_), false.ToString())))
								{
									num = 2;
									continue;
								}
								this.ᜄ.Strikeout = false;
								num = 11;
								continue;
							case 6:
								goto IL_13F;
							case 7:
								goto IL_209;
							case 8:
								goto IL_228;
							case 9:
								goto IL_13F;
							case 10:
								if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯笱䀳圵吷匹弻", a_), false.ToString())))
								{
									num = 4;
									continue;
								}
								this.ᜄ.Italic = false;
								num = 9;
								continue;
							case 11:
								goto IL_1CA;
							}
							goto IL_76;
							IL_13F:
							num = 5;
							continue;
							IL_228:
							num = 10;
							continue;
						}
						IL_268:
						this.ᜄ.Italic = true;
						num = 6;
					}
				}
				IL_1CA:
				IL_209:
				this.ᜄ.Underline = (XlsFontUnderline)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩䈫娭漯朱娳刵崷䠹倻圽⸿❁", a_), 0.ToString()));
				this.ᜀ.Horizontal = (HorizontalAlignment)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("性䔩師漭尯嬱匳堵唷弹刻䨽", a_), 1.ToString()));
				this.ᜀ.Vertical = (VerticalAlignment)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("縧伩師娭焯帱崳儵嘷圹夻倽㐿", a_), 0.ToString()));
				this.ᜁ = Color.FromArgb(Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("樧䬩伫䔭圯䀱嬳䌵嘷帹缻儽ⰿⵁ㙃", a_), 14811135.ToString())));
				this.ᜂ = Color.FromArgb(Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䔩師䬭圯䀱嬳䌵嘷帹缻儽ⰿⵁ㙃", a_), 14811135.ToString())));
				this.ᜃ = (CellNoteFillType)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("渧䌩䀫䈭搯䬱䐳匵", a_), 0.ToString()));
				this.ᜅ = Convert.ToByte(File.ReadValue(Section, HyperlinksCollectionEditor.b("簧堩䴫䀭䌯䈱唳䐵崷吹弻䜽", a_), 0.ToString()));
				this.ᜆ = (CellOrientation)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("朧堩䔫䬭帯䘱唳䈵儷唹刻", a_), 0.ToString()));
				this.ᜇ = (CellNoteGradient)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("漧堩䴫䨭夯圱娳䈵", a_), 0.ToString()));
				return;
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0009F11C File Offset: 0x0009E11C
		public bool ShouldSerializeBackgroundColor()
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
			return this.ᜁ != Color.FromArgb(255, 255, 225);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0009F178 File Offset: 0x0009E178
		public void ResetBackgroundColor()
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
			this.ᜁ = Color.FromArgb(255, 255, 225);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0009F1D0 File Offset: 0x0009E1D0
		public bool ShouldSerializeForegroundColor()
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
			return this.ᜂ != Color.FromArgb(255, 255, 225);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0009F22C File Offset: 0x0009E22C
		public void ResetForegroundColor()
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
			this.ᜂ = Color.FromArgb(255, 255, 225);
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0009F284 File Offset: 0x0009E284
		[Browsable(false)]
		public override ItemType ItemType
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
				return ItemType.NoteFormat;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0009F2C0 File Offset: 0x0009E2C0
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0009F304 File Offset: 0x0009E304
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets the text alignment in the note cell.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public TextAlignment Alignment
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
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5F;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (value != this.ᜀ)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						goto IL_5F;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_5F:
					this.ᜀ = value;
					num = 1;
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0009F39C File Offset: 0x0009E39C
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0009F3E0 File Offset: 0x0009E3E0
		[Description("Gets or sets background color for the gradient fill.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
				return this.ᜁ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜁ = value;
						num = 2;
						continue;
					case 2:
						goto IL_55;
					}
					if (true)
					{
					}
					if (!(value != this.ᜁ))
					{
						return;
					}
					num = 1;
				}
				IL_55:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0009F460 File Offset: 0x0009E460
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0009F4A4 File Offset: 0x0009E4A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets foreground color for the notes.")]
		public Color ForegroundColor
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
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_55;
					case 2:
						this.ᜂ = value;
						num = 1;
						continue;
					}
					if (!(value != this.ᜂ))
					{
						return;
					}
					num = 2;
				}
				IL_55:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0009F524 File Offset: 0x0009E524
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x0009F568 File Offset: 0x0009E568
		[Description("Gets or sets the type of filling the note.")]
		[DefaultValue(CellNoteFillType.Solid)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public CellNoteFillType FillType
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_50;
					case 2:
						this.ᜃ = value;
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (value == this.ᜃ)
					{
						return;
					}
					num = 2;
				}
				IL_50:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0009F5E4 File Offset: 0x0009E5E4
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x0009F628 File Offset: 0x0009E628
		[Description("Gets or sets text font of the note.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFont Font
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
				return this.ᜄ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							this.ᜄ.Assign(value, Color.Black);
							num = 1;
							continue;
						}
						break;
					case 3:
						num = 4;
						continue;
					case 4:
						if (value != this.ᜄ)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0009F6D0 File Offset: 0x0009E6D0
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x0009F714 File Offset: 0x0009E714
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the percentage of the note transparency.")]
		public byte Transparency
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_48;
					case 1:
						this.ᜅ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜅ)
					{
						return;
					}
					num = 1;
				}
				IL_48:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0009F790 File Offset: 0x0009E790
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x0009F7D4 File Offset: 0x0009E7D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(CellOrientation.NoRotation)]
		[Description("Gets or sets the note orientation.")]
		public CellOrientation Orientation
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
						num = 2;
						continue;
					case 2:
						goto IL_48;
					}
					if (value == this.ᜆ)
					{
						return;
					}
					num = 1;
				}
				IL_48:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0009F850 File Offset: 0x0009E850
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x0009F894 File Offset: 0x0009E894
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(CellNoteGradient.Horizontal)]
		[Description("Gets or sets the type of the gradient fill.")]
		public CellNoteGradient Gradient
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_48;
					case 1:
						this.ᜇ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜇ)
					{
						return;
					}
					num = 1;
				}
				IL_48:
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
					break;
				}
			}
		}

		// Token: 0x04000AE0 RID: 2784
		private bool \u2593\u0090\u0081\u0088;

		// Token: 0x04000AE1 RID: 2785
		private byte \u2609ª\u00AB\u008A;

		// Token: 0x04000AE2 RID: 2786
		private TextAlignment ᜀ = new TextAlignment();

		// Token: 0x04000AE3 RID: 2787
		private Color ᜁ = Color.FromArgb(255, 255, 225);

		// Token: 0x04000AE4 RID: 2788
		private Color ᜂ = Color.FromArgb(255, 255, 225);

		// Token: 0x04000AE5 RID: 2789
		private CellNoteFillType ᜃ;

		// Token: 0x04000AE6 RID: 2790
		private CellFont ᜄ = new CellFont();

		// Token: 0x04000AE7 RID: 2791
		private byte ᜅ;

		// Token: 0x04000AE8 RID: 2792
		private CellOrientation ᜆ;

		// Token: 0x04000AE9 RID: 2793
		private int[] \u2609\u0099\u0082\u00AD;

		// Token: 0x04000AEA RID: 2794
		private CellNoteGradient ᜇ;
	}
}

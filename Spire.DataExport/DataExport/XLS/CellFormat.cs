using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001AE RID: 430
	public class CellFormat : CustomItem, ICloneable
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x0007DF3C File Offset: 0x0007CF3C
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
			return new CellFormat
			{
				Alignment = this.Alignment,
				Borders = this.Borders,
				FillStyle = this.FillStyle,
				Font = this.Font,
				WordWrap = this.WordWrap
			};
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0007DFBC File Offset: 0x0007CFBC
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

		// Token: 0x06000BFD RID: 3069 RVA: 0x0007DFF8 File Offset: 0x0007CFF8
		public bool IsDefault()
		{
			int a_ = 9;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					num = 33;
					continue;
				case 2:
					num = 11;
					continue;
				case 3:
					if (this.ᜂ.Background == CellColor.White)
					{
						num = 26;
						continue;
					}
					return false;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_327;
					default:
						if (false)
						{
						}
						num = 29;
						continue;
					}
					break;
				case 5:
					num = 35;
					continue;
				case 6:
					if (this.ᜃ.Horizontal == HorizontalAlignment.General)
					{
						num = 38;
						continue;
					}
					return false;
				case 8:
					num = 23;
					continue;
				case 9:
					if (true)
					{
					}
					if (this.ᜂ.Pattern == Pattern.None)
					{
						num = 24;
						continue;
					}
					return false;
				case 10:
					if (this.ᜁ.Bottom.Style == CellBorderStyle.None)
					{
						num = 19;
						continue;
					}
					return false;
				case 11:
					if (this.ᜀ.Charset == 1)
					{
						num = 25;
						continue;
					}
					return false;
				case 12:
					if (this.ᜁ.Left.Style == CellBorderStyle.None)
					{
						num = 4;
						continue;
					}
					return false;
				case 13:
					num = 36;
					continue;
				case 14:
					if (this.ᜃ.Vertical == VerticalAlignment.Bottom)
					{
						num = 30;
						continue;
					}
					return false;
				case 15:
					num = 12;
					continue;
				case 16:
					if (string.Compare(this.ᜀ.Name, HyperlinksCollectionEditor.b("搤唦䀨䨪䄬", a_), true) == 0)
					{
						num = 15;
						continue;
					}
					return false;
				case 17:
					if (this.ᜂ.Foreground == CellColor.Black)
					{
						num = 0;
						continue;
					}
					return false;
				case 18:
					num = 37;
					continue;
				case 19:
					num = 28;
					continue;
				case 20:
					num = 39;
					continue;
				case 21:
					num = 3;
					continue;
				case 22:
					num = 27;
					continue;
				case 23:
					if (this.ᜁ.DiagUp.Style == CellBorderStyle.None)
					{
						num = 21;
						continue;
					}
					return false;
				case 24:
					num = 17;
					continue;
				case 25:
					num = 16;
					continue;
				case 26:
					num = 9;
					continue;
				case 27:
					if (this.ᜁ.Top.Style == CellBorderStyle.None)
					{
						num = 32;
						continue;
					}
					return false;
				case 28:
					if (this.ᜁ.DiagDown.Style == CellBorderStyle.None)
					{
						num = 8;
						continue;
					}
					return false;
				case 29:
					if (this.ᜁ.Right.Style == CellBorderStyle.None)
					{
						num = 22;
						continue;
					}
					return false;
				case 30:
					goto IL_220;
				case 31:
					num = 34;
					continue;
				case 32:
					num = 10;
					continue;
				case 33:
					goto IL_327;
				case 34:
					if (!this.ᜀ.Strikeout)
					{
						num = 18;
						continue;
					}
					return false;
				case 35:
					if (!this.ᜀ.Italic)
					{
						num = 31;
						continue;
					}
					return false;
				case 36:
					if (this.ᜀ.Script == XlsFontScript.None)
					{
						num = 1;
						continue;
					}
					return false;
				case 37:
					if (this.ᜀ.Color == CellColor.Black)
					{
						num = 13;
						continue;
					}
					return false;
				case 38:
					num = 14;
					continue;
				case 39:
					if (!this.ᜀ.Bold)
					{
						num = 5;
						continue;
					}
					return false;
				}
				if (this.ᜀ.Size == 10f)
				{
					num = 20;
					continue;
				}
				return false;
				IL_327:
				if (this.ᜀ.Underline != XlsFontUnderline.None)
				{
					return false;
				}
				num = 2;
			}
			IL_220:
			return !this.ᜄ;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0007E480 File Offset: 0x0007D480
		internal void ᜀ(spr\u17ED A_0)
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
			this.ᜀ = A_0.ᜅ();
			this.ᜁ = A_0.ᜃ();
			this.ᜂ = A_0.ᜄ();
			this.ᜄ = A_0.ᜁ();
			this.ᜆ = A_0.ᜆ();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0007E4F8 File Offset: 0x0007D4F8
		public bool IsEqual(CellFormat Format)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 10;
					continue;
				case 2:
					num = 11;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (this.ᜀ.IsEqual(Format.Font))
						{
							num = 5;
							continue;
						}
						return false;
					}
					break;
				case 4:
					return false;
				case 5:
					num = 7;
					continue;
				case 6:
					goto IL_137;
				case 7:
					if (this.ᜁ.IsEqual(Format.Borders))
					{
						num = 1;
						continue;
					}
					return false;
				case 8:
					num = 9;
					continue;
				case 9:
					if (this.ᜄ == Format.WordWrap)
					{
						num = 6;
						continue;
					}
					return false;
				case 10:
					if (this.ᜂ.IsEqual(Format.FillStyle))
					{
						num = 2;
						continue;
					}
					return false;
				case 11:
					if (this.ᜃ.IsEqual(Format.Alignment))
					{
						num = 8;
						continue;
					}
					return false;
				}
				IL_40:
				if (Format == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				goto IL_40;
			}
			return false;
			IL_137:
			if (true)
			{
			}
			return this.ᜆ == Format.Rotation;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0007E670 File Offset: 0x0007D670
		public virtual void SetDefault()
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
			this.ᜀ.SetDefault();
			this.ᜁ.SetDefault();
			this.ᜂ.SetDefault();
			this.ᜃ.SetDefault();
			this.ᜄ = false;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0007E6E0 File Offset: 0x0007D6E0
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳砵夷圹夻", a_), this.Font.Name);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳攵儷䀹夻", a_), this.Font.Size.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳电圷嘹医䰽", a_), sprᮌ.ᜀ(this.Font.Color));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳琵圷嘹堻", a_), this.Font.Bold.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳缵䰷嬹倻圽⌿", a_), this.Font.Italic.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳攵䰷䠹唻唽┿ുㅃ㉅", a_), this.Font.Strikeout.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䄭帯䘱欳挵嘷帹夻䰽ⰿ⭁⩃⍅", a_), ((int)this.Font.Underline).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("搫䄭䈯猱堳張強吹儻嬽⸿㙁", a_), ((int)this.Alignment.Horizontal).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("稫䬭䈯䘱申娵儷崹刻匽┿ⱁぃ", a_), ((int)this.Alignment.Vertical).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷渹医丽", a_), ((int)this.Borders.Top.Style).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷瘹夻堽㐿", a_), ((int)this.Borders.Left.Style).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷根唻夽⠿㙁", a_), ((int)this.Borders.Right.Style).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷砹医䨽㐿ⵁ⥃", a_), ((int)this.Borders.Bottom.Style).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䜭尯帱搳圵䰷丹夻䰽⸿", a_), ((int)this.FillStyle.Pattern).ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷渹医丽̿ⵁ⡃⥅㩇", a_), sprᮌ.ᜀ(this.Borders.Top.Color));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷瘹夻堽㐿Ł⭃⩅❇㡉", a_), sprᮌ.ᜀ(this.Borders.Left.Color));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷根唻夽⠿㙁݃⥅⑇╉㹋", a_), sprᮌ.ᜀ(this.Borders.Right.Color));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("渫䄭䈯嘱儳䐵朷砹医䨽㐿ⵁ⥃Յ❇♉⍋㱍", a_), sprᮌ.ᜀ(this.Borders.Bottom.Color));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䜭尯帱瘳圵嬷儹嬻䰽⼿㝁⩃≅", a_), sprᮌ.ᜀ(this.FillStyle.Background));
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䜭尯帱爳夵䨷弹嬻䰽⼿㝁⩃≅", a_), sprᮌ.ᜀ(this.FillStyle.Foreground));
			File.SaveToFile();
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0007EA60 File Offset: 0x0007DA60
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 7;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ.Name = File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪挬丮尰嘲", a_), HyperlinksCollectionEditor.b("戢圤並䠨䜪", a_));
					this.ᜀ.Size = Convert.ToSingle(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪縬䘮䬰嘲", a_), 10.ToString()));
					this.ᜀ.Color = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪測䀮崰尲䜴", a_), sprᮌ.ᜀ(CellColor.Black)));
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪漬䀮崰圲", a_), false.ToString())))
							{
								num = 11;
								continue;
							}
							this.ᜀ.Bold = false;
							num = 4;
							continue;
						case 1:
							if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪縬嬮䌰娲帴制瘸为䤼", a_), false.ToString())))
							{
								num = 6;
								continue;
							}
							this.ᜀ.Strikeout = false;
							num = 7;
							continue;
						case 2:
							if (true)
							{
							}
							goto IL_22F;
						case 3:
							if (Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪搬嬮倰弲尴吶", a_), false.ToString())))
							{
								num = 9;
								continue;
							}
							this.ᜀ.Italic = false;
							num = 10;
							continue;
						case 4:
							goto IL_22F;
						case 5:
							goto IL_11A;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_210;
							default:
								if (false)
								{
								}
								this.ᜀ.Strikeout = true;
								num = 8;
								continue;
							}
							break;
						case 7:
							goto IL_1B5;
						case 8:
							goto IL_210;
						case 9:
							this.ᜀ.Italic = true;
							num = 5;
							continue;
						case 10:
							goto IL_11A;
						case 11:
							this.ᜀ.Bold = true;
							num = 2;
							continue;
						}
						break;
						IL_11A:
						num = 1;
						continue;
						IL_22F:
						num = 3;
					}
				}
				IL_1B5:
				IL_210:
				this.ᜀ.Underline = (XlsFontUnderline)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䨤䤦崨琪砬䄮唰嘲䜴嬶倸唺堼", a_), 0.ToString()));
				this.ᜃ.Horizontal = (HorizontalAlignment)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("欢䨤唦栨䜪䐬䠮弰帲倴夶䴸", a_), 0.ToString()));
				this.ᜃ.Vertical = (VerticalAlignment)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("產䀤唦崨横䄬䘮嘰崲場制圸伺", a_), 2.ToString()));
				this.ᜁ.Top.Style = (CellBorderStyle)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮攰尲䔴", a_), 0.ToString()));
				this.ᜁ.Left.Style = (CellBorderStyle)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮細嘲匴䌶", a_), 0.ToString()));
				this.ᜁ.Right.Style = (CellBorderStyle)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮挰娲刴弶䴸", a_), 0.ToString()));
				this.ᜁ.Bottom.Style = (CellBorderStyle)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮猰尲䄴䌶嘸嘺", a_), 0.ToString()));
				this.ᜂ.Pattern = (Pattern)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䰤䬦䔨笪䰬嬮䔰嘲䜴夶", a_), 0.ToString()));
				this.ᜁ.Top.Color = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮攰尲䔴琶嘸场刼䴾", a_), sprᮌ.ᜀ(CellColor.Black)));
				this.ᜁ.Left.Color = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮細嘲匴䌶稸吺儼倾㍀", a_), sprᮌ.ᜀ(CellColor.Black)));
				this.ᜁ.Right.Color = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮挰娲刴弶䴸砺刼匾⹀ㅂ", a_), sprᮌ.ᜀ(CellColor.Black)));
				this.ᜁ.Bottom.Color = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮猰尲䄴䌶嘸嘺縼倾ⵀⱂ㝄", a_), sprᮌ.ᜀ(CellColor.Black)));
				this.ᜂ.Background = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䰤䬦䔨椪䰬䰮娰吲䜴堶䰸唺夼", a_), sprᮌ.ᜀ(CellColor.White)));
				this.ᜂ.Foreground = sprᮌ.ᜄ(File.ReadValue(Section, HyperlinksCollectionEditor.b("攢䰤䬦䔨洪䈬崮吰吲䜴堶䰸唺夼", a_), sprᮌ.ᜀ(CellColor.Black)));
				return;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0007EF84 File Offset: 0x0007DF84
		[Browsable(false)]
		public override ItemType ItemType
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
				return ItemType.ItemFormat;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0007EFC0 File Offset: 0x0007DFC0
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x0007F004 File Offset: 0x0007E004
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
				return this.ᜀ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜀ)
						{
							num = 4;
							continue;
						}
						goto IL_79;
					case 1:
						goto IL_79;
					case 2:
						num = 0;
						continue;
					case 3:
						if (true)
						{
						}
						break;
					case 4:
						this.ᜀ.Assign(value, Color.Black);
						num = 1;
						continue;
					}
					IL_2C:
					if (value != null)
					{
						num = 2;
						continue;
					}
					IL_79:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_8F;
					}
				}
				IL_8F:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0007F0A8 File Offset: 0x0007E0A8
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0007F0EC File Offset: 0x0007E0EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public Borders Borders
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
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ = value;
						num = 3;
						continue;
					case 1:
						if (value != this.ᜁ)
						{
							num = 0;
							continue;
						}
						goto IL_6F;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_6F;
					}
					IL_24:
					if (value != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					IL_6F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_85;
					}
				}
				IL_85:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0007F184 File Offset: 0x0007E184
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0007F1C8 File Offset: 0x0007E1C8
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FillType FillStyle
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
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
							num = 4;
							continue;
						}
						break;
					case 3:
						this.ᜂ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					case 4:
						if (value != this.ᜂ)
						{
							num = 3;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0007F260 File Offset: 0x0007E260
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0007F2A4 File Offset: 0x0007E2A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
						return;
					case 2:
						this.ᜃ = value;
						num = 0;
						continue;
					case 3:
						if (true)
						{
						}
						if (value != this.ᜃ)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0007F33C File Offset: 0x0007E33C
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0007F380 File Offset: 0x0007E380
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public bool WordWrap
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
						goto IL_64;
					case 1:
						this.ᜄ = value;
						num = 0;
						continue;
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
						if (value == this.ᜄ)
						{
							goto IL_66;
						}
						num = 1;
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0007F3FC File Offset: 0x0007E3FC
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0007F440 File Offset: 0x0007E440
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public byte Rotation
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
						return;
					case 2:
						this.ᜆ = value;
						num = 1;
						continue;
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
						if (value == this.ᜆ)
						{
							return;
						}
						if (true)
						{
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0007F4BC File Offset: 0x0007E4BC
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0007F500 File Offset: 0x0007E500
		[Editor(typeof(ColumnNameEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string FieldName
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
				return this.ᜅ;
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
						return;
					case 2:
						this.ᜅ = value;
						this.SetName(value);
						num = 1;
						continue;
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
						if (!(value != this.ᜅ))
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x04000926 RID: 2342
		private byte \u25D9\u0093ª\u0084;

		// Token: 0x04000927 RID: 2343
		private string \u25D9\u0084\u00B0\u007F;

		// Token: 0x04000928 RID: 2344
		private CellFont ᜀ = new CellFont();

		// Token: 0x04000929 RID: 2345
		private Borders ᜁ = new Borders();

		// Token: 0x0400092A RID: 2346
		private bool \u25D9\u0099\u008B\u0092;

		// Token: 0x0400092B RID: 2347
		private byte \u2460\u009F\u008D\u00A8;

		// Token: 0x0400092C RID: 2348
		private FillType ᜂ = new FillType();

		// Token: 0x0400092D RID: 2349
		private int[] \u25D9\u0083\u0089\u00A8;

		// Token: 0x0400092E RID: 2350
		private TextAlignment ᜃ = new TextAlignment();

		// Token: 0x0400092F RID: 2351
		private bool ᜄ;

		// Token: 0x04000930 RID: 2352
		private string ᜅ = string.Empty;

		// Token: 0x04000931 RID: 2353
		private byte ᜆ;
	}
}

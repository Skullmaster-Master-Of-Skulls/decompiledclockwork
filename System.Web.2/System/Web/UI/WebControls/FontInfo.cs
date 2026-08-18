using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F8 RID: 1016
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public sealed class FontInfo
	{
		// Token: 0x060030EA RID: 12522 RVA: 0x0009F2DE File Offset: 0x0009D4DE
		internal FontInfo(Style owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x0009F2ED File Offset: 0x0009D4ED
		// (set) Token: 0x060030EC RID: 12524 RVA: 0x0009F31D File Offset: 0x0009D51D
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Bold")]
		[NotifyParentProperty(true)]
		public bool Bold
		{
			get
			{
				return this.owner.IsSet(2048) && (bool)this.owner.ViewState["Font_Bold"];
			}
			set
			{
				this.owner.ViewState["Font_Bold"] = value;
				this.owner.SetBit(2048);
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x0009F34A File Offset: 0x0009D54A
		// (set) Token: 0x060030EE RID: 12526 RVA: 0x0009F37A File Offset: 0x0009D57A
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Italic")]
		[NotifyParentProperty(true)]
		public bool Italic
		{
			get
			{
				return this.owner.IsSet(4096) && (bool)this.owner.ViewState["Font_Italic"];
			}
			set
			{
				this.owner.ViewState["Font_Italic"] = value;
				this.owner.SetBit(4096);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x0009F3A8 File Offset: 0x0009D5A8
		// (set) Token: 0x060030F0 RID: 12528 RVA: 0x0009F3C9 File Offset: 0x0009D5C9
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("FontInfo_Name")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				string[] names = this.Names;
				if (names.Length != 0)
				{
					return names[0];
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					this.Names = null;
					return;
				}
				this.Names = new string[]
				{
					value
				};
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x0009F3FC File Offset: 0x0009D5FC
		// (set) Token: 0x060030F2 RID: 12530 RVA: 0x0009F441 File Offset: 0x0009D641
		[TypeConverter(typeof(FontNamesConverter))]
		[WebCategory("Appearance")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("FontInfo_Names")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[NotifyParentProperty(true)]
		public string[] Names
		{
			get
			{
				if (this.owner.IsSet(512))
				{
					string[] array = (string[])this.owner.ViewState["Font_Names"];
					if (array != null)
					{
						return array;
					}
				}
				return new string[0];
			}
			set
			{
				this.owner.ViewState["Font_Names"] = value;
				this.owner.SetBit(512);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x0009F469 File Offset: 0x0009D669
		// (set) Token: 0x060030F4 RID: 12532 RVA: 0x0009F499 File Offset: 0x0009D699
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Overline")]
		[NotifyParentProperty(true)]
		public bool Overline
		{
			get
			{
				return this.owner.IsSet(16384) && (bool)this.owner.ViewState["Font_Overline"];
			}
			set
			{
				this.owner.ViewState["Font_Overline"] = value;
				this.owner.SetBit(16384);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x0009F4C6 File Offset: 0x0009D6C6
		internal Style Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x0009F4CE File Offset: 0x0009D6CE
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x0009F504 File Offset: 0x0009D704
		[WebCategory("Appearance")]
		[DefaultValue(typeof(FontUnit), "")]
		[WebSysDescription("FontInfo_Size")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public FontUnit Size
		{
			get
			{
				if (this.owner.IsSet(1024))
				{
					return (FontUnit)this.owner.ViewState["Font_Size"];
				}
				return FontUnit.Empty;
			}
			set
			{
				if (value.Type == FontSize.AsUnit && value.Unit.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.owner.ViewState["Font_Size"] = value;
				this.owner.SetBit(1024);
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x0009F56B File Offset: 0x0009D76B
		// (set) Token: 0x060030F9 RID: 12537 RVA: 0x0009F59B File Offset: 0x0009D79B
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Strikeout")]
		[NotifyParentProperty(true)]
		public bool Strikeout
		{
			get
			{
				return this.owner.IsSet(32768) && (bool)this.owner.ViewState["Font_Strikeout"];
			}
			set
			{
				this.owner.ViewState["Font_Strikeout"] = value;
				this.owner.SetBit(32768);
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x0009F5C8 File Offset: 0x0009D7C8
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x0009F5F8 File Offset: 0x0009D7F8
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Underline")]
		[NotifyParentProperty(true)]
		public bool Underline
		{
			get
			{
				return this.owner.IsSet(8192) && (bool)this.owner.ViewState["Font_Underline"];
			}
			set
			{
				this.owner.ViewState["Font_Underline"] = value;
				this.owner.SetBit(8192);
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0009F628 File Offset: 0x0009D828
		public void ClearDefaults()
		{
			if (this.Names.Length == 0)
			{
				this.owner.ViewState.Remove("Font_Names");
				this.owner.ClearBit(512);
			}
			if (this.Size == FontUnit.Empty)
			{
				this.owner.ViewState.Remove("Font_Size");
				this.owner.ClearBit(1024);
			}
			if (!this.Bold)
			{
				this.ResetBold();
			}
			if (!this.Italic)
			{
				this.ResetItalic();
			}
			if (!this.Underline)
			{
				this.ResetUnderline();
			}
			if (!this.Overline)
			{
				this.ResetOverline();
			}
			if (!this.Strikeout)
			{
				this.ResetStrikeout();
			}
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x0009F6E0 File Offset: 0x0009D8E0
		public void CopyFrom(FontInfo f)
		{
			if (f != null)
			{
				Style style = f.Owner;
				if (style.RegisteredCssClass.Length != 0)
				{
					if (style.IsSet(512))
					{
						this.ResetNames();
					}
					if (style.IsSet(1024) && f.Size != FontUnit.Empty)
					{
						this.ResetFontSize();
					}
					if (style.IsSet(2048))
					{
						this.ResetBold();
					}
					if (style.IsSet(4096))
					{
						this.ResetItalic();
					}
					if (style.IsSet(16384))
					{
						this.ResetOverline();
					}
					if (style.IsSet(32768))
					{
						this.ResetStrikeout();
					}
					if (style.IsSet(8192))
					{
						this.ResetUnderline();
						return;
					}
				}
				else
				{
					if (style.IsSet(512))
					{
						this.Names = f.Names;
					}
					if (style.IsSet(1024) && f.Size != FontUnit.Empty)
					{
						this.Size = f.Size;
					}
					if (style.IsSet(2048))
					{
						this.Bold = f.Bold;
					}
					if (style.IsSet(4096))
					{
						this.Italic = f.Italic;
					}
					if (style.IsSet(16384))
					{
						this.Overline = f.Overline;
					}
					if (style.IsSet(32768))
					{
						this.Strikeout = f.Strikeout;
					}
					if (style.IsSet(8192))
					{
						this.Underline = f.Underline;
					}
				}
			}
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0009F868 File Offset: 0x0009DA68
		public void MergeWith(FontInfo f)
		{
			if (f != null)
			{
				Style style = f.Owner;
				if (style.RegisteredCssClass.Length == 0)
				{
					if (style.IsSet(512) && !this.owner.IsSet(512))
					{
						this.Names = f.Names;
					}
					if (style.IsSet(1024) && (!this.owner.IsSet(1024) || this.Size == FontUnit.Empty))
					{
						this.Size = f.Size;
					}
					if (style.IsSet(2048) && !this.owner.IsSet(2048))
					{
						this.Bold = f.Bold;
					}
					if (style.IsSet(4096) && !this.owner.IsSet(4096))
					{
						this.Italic = f.Italic;
					}
					if (style.IsSet(16384) && !this.owner.IsSet(16384))
					{
						this.Overline = f.Overline;
					}
					if (style.IsSet(32768) && !this.owner.IsSet(32768))
					{
						this.Strikeout = f.Strikeout;
					}
					if (style.IsSet(8192) && !this.owner.IsSet(8192))
					{
						this.Underline = f.Underline;
					}
				}
			}
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
		internal void Reset()
		{
			if (this.owner.IsSet(512))
			{
				this.ResetNames();
			}
			if (this.owner.IsSet(1024))
			{
				this.ResetFontSize();
			}
			if (this.owner.IsSet(2048))
			{
				this.ResetBold();
			}
			if (this.owner.IsSet(4096))
			{
				this.ResetItalic();
			}
			if (this.owner.IsSet(8192))
			{
				this.ResetUnderline();
			}
			if (this.owner.IsSet(16384))
			{
				this.ResetOverline();
			}
			if (this.owner.IsSet(32768))
			{
				this.ResetStrikeout();
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x0009FA89 File Offset: 0x0009DC89
		private void ResetBold()
		{
			this.owner.ViewState.Remove("Font_Bold");
			this.owner.ClearBit(2048);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x0009FAB0 File Offset: 0x0009DCB0
		private void ResetNames()
		{
			this.owner.ViewState.Remove("Font_Names");
			this.owner.ClearBit(512);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0009FAD7 File Offset: 0x0009DCD7
		private void ResetFontSize()
		{
			this.owner.ViewState.Remove("Font_Size");
			this.owner.ClearBit(1024);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x0009FAFE File Offset: 0x0009DCFE
		private void ResetItalic()
		{
			this.owner.ViewState.Remove("Font_Italic");
			this.owner.ClearBit(4096);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x0009FB25 File Offset: 0x0009DD25
		private void ResetOverline()
		{
			this.owner.ViewState.Remove("Font_Overline");
			this.owner.ClearBit(16384);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x0009FB4C File Offset: 0x0009DD4C
		private void ResetStrikeout()
		{
			this.owner.ViewState.Remove("Font_Strikeout");
			this.owner.ClearBit(32768);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x0009FB73 File Offset: 0x0009DD73
		private void ResetUnderline()
		{
			this.owner.ViewState.Remove("Font_Underline");
			this.owner.ClearBit(8192);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x0009FB9A File Offset: 0x0009DD9A
		private bool ShouldSerializeBold()
		{
			return this.owner.IsSet(2048);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0009FBAC File Offset: 0x0009DDAC
		private bool ShouldSerializeItalic()
		{
			return this.owner.IsSet(4096);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x0009FBBE File Offset: 0x0009DDBE
		private bool ShouldSerializeOverline()
		{
			return this.owner.IsSet(16384);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x0009FBD0 File Offset: 0x0009DDD0
		private bool ShouldSerializeStrikeout()
		{
			return this.owner.IsSet(32768);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x0009FBE2 File Offset: 0x0009DDE2
		private bool ShouldSerializeUnderline()
		{
			return this.owner.IsSet(8192);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x0009FBF4 File Offset: 0x0009DDF4
		public bool ShouldSerializeNames()
		{
			string[] names = this.Names;
			return names.Length != 0;
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0009FC10 File Offset: 0x0009DE10
		public override string ToString()
		{
			string text = this.Size.ToString(CultureInfo.InvariantCulture);
			string text2 = this.Name;
			if (text.Length != 0)
			{
				if (text2.Length != 0)
				{
					text2 = text2 + ", " + text;
				}
				else
				{
					text2 = text;
				}
			}
			return text2;
		}

		// Token: 0x040020A3 RID: 8355
		private Style owner;
	}
}

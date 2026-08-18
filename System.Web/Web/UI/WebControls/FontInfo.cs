using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200057B RID: 1403
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FontInfo
	{
		// Token: 0x060044C0 RID: 17600 RVA: 0x0011A738 File Offset: 0x00119738
		internal FontInfo(Style owner)
		{
			this.owner = owner;
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x0011A747 File Offset: 0x00119747
		// (set) Token: 0x060044C2 RID: 17602 RVA: 0x0011A777 File Offset: 0x00119777
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Bold")]
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

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x0011A7A4 File Offset: 0x001197A4
		// (set) Token: 0x060044C4 RID: 17604 RVA: 0x0011A7D4 File Offset: 0x001197D4
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("FontInfo_Italic")]
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

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x0011A804 File Offset: 0x00119804
		// (set) Token: 0x060044C6 RID: 17606 RVA: 0x0011A828 File Offset: 0x00119828
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[WebCategory("Appearance")]
		[WebSysDescription("FontInfo_Name")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public string Name
		{
			get
			{
				string[] names = this.Names;
				if (names.Length > 0)
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

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x0011A868 File Offset: 0x00119868
		// (set) Token: 0x060044C8 RID: 17608 RVA: 0x0011A8AD File Offset: 0x001198AD
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(FontNamesConverter))]
		[WebCategory("Appearance")]
		[WebSysDescription("FontInfo_Names")]
		[RefreshProperties(RefreshProperties.Repaint)]
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

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x0011A8D5 File Offset: 0x001198D5
		// (set) Token: 0x060044CA RID: 17610 RVA: 0x0011A905 File Offset: 0x00119905
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[WebSysDescription("FontInfo_Overline")]
		[DefaultValue(false)]
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

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x0011A932 File Offset: 0x00119932
		internal Style Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x0011A93A File Offset: 0x0011993A
		// (set) Token: 0x060044CD RID: 17613 RVA: 0x0011A970 File Offset: 0x00119970
		[RefreshProperties(RefreshProperties.Repaint)]
		[WebCategory("Appearance")]
		[DefaultValue(typeof(FontUnit), "")]
		[WebSysDescription("FontInfo_Size")]
		[NotifyParentProperty(true)]
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

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x0011A9D7 File Offset: 0x001199D7
		// (set) Token: 0x060044CF RID: 17615 RVA: 0x0011AA07 File Offset: 0x00119A07
		[WebSysDescription("FontInfo_Strikeout")]
		[WebCategory("Appearance")]
		[DefaultValue(false)]
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

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x0011AA34 File Offset: 0x00119A34
		// (set) Token: 0x060044D1 RID: 17617 RVA: 0x0011AA64 File Offset: 0x00119A64
		[WebSysDescription("FontInfo_Underline")]
		[WebCategory("Appearance")]
		[DefaultValue(false)]
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

		// Token: 0x060044D2 RID: 17618 RVA: 0x0011AA94 File Offset: 0x00119A94
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

		// Token: 0x060044D3 RID: 17619 RVA: 0x0011AB50 File Offset: 0x00119B50
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

		// Token: 0x060044D4 RID: 17620 RVA: 0x0011ACD8 File Offset: 0x00119CD8
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

		// Token: 0x060044D5 RID: 17621 RVA: 0x0011AE44 File Offset: 0x00119E44
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

		// Token: 0x060044D6 RID: 17622 RVA: 0x0011AEF9 File Offset: 0x00119EF9
		private void ResetBold()
		{
			this.owner.ViewState.Remove("Font_Bold");
			this.owner.ClearBit(2048);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x0011AF20 File Offset: 0x00119F20
		private void ResetNames()
		{
			this.owner.ViewState.Remove("Font_Names");
			this.owner.ClearBit(512);
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x0011AF47 File Offset: 0x00119F47
		private void ResetFontSize()
		{
			this.owner.ViewState.Remove("Font_Size");
			this.owner.ClearBit(1024);
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x0011AF6E File Offset: 0x00119F6E
		private void ResetItalic()
		{
			this.owner.ViewState.Remove("Font_Italic");
			this.owner.ClearBit(4096);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x0011AF95 File Offset: 0x00119F95
		private void ResetOverline()
		{
			this.owner.ViewState.Remove("Font_Overline");
			this.owner.ClearBit(16384);
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x0011AFBC File Offset: 0x00119FBC
		private void ResetStrikeout()
		{
			this.owner.ViewState.Remove("Font_Strikeout");
			this.owner.ClearBit(32768);
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x0011AFE3 File Offset: 0x00119FE3
		private void ResetUnderline()
		{
			this.owner.ViewState.Remove("Font_Underline");
			this.owner.ClearBit(8192);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x0011B00A File Offset: 0x0011A00A
		private bool ShouldSerializeBold()
		{
			return this.owner.IsSet(2048);
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0011B01C File Offset: 0x0011A01C
		private bool ShouldSerializeItalic()
		{
			return this.owner.IsSet(4096);
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0011B02E File Offset: 0x0011A02E
		private bool ShouldSerializeOverline()
		{
			return this.owner.IsSet(16384);
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0011B040 File Offset: 0x0011A040
		private bool ShouldSerializeStrikeout()
		{
			return this.owner.IsSet(32768);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x0011B052 File Offset: 0x0011A052
		private bool ShouldSerializeUnderline()
		{
			return this.owner.IsSet(8192);
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0011B064 File Offset: 0x0011A064
		public bool ShouldSerializeNames()
		{
			string[] names = this.Names;
			return names.Length > 0;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0011B080 File Offset: 0x0011A080
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

		// Token: 0x040029C0 RID: 10688
		private Style owner;
	}
}

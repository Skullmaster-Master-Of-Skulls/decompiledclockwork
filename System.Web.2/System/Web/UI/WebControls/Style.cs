using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004DD RID: 1245
	[ToolboxItem(false)]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class Style : Component, IStateManager
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x000C7DAA File Offset: 0x000C5FAA
		public Style() : this(null)
		{
			this.ownStateBag = true;
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000C7DBA File Offset: 0x000C5FBA
		public Style(StateBag bag)
		{
			this.statebag = bag;
			this.marked = false;
			this.setBits = 0;
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x000C7DDD File Offset: 0x000C5FDD
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x000C7E03 File Offset: 0x000C6003
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("Style_BackColor")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(WebColorConverter))]
		public Color BackColor
		{
			get
			{
				if (this.IsSet(8))
				{
					return (Color)this.ViewState["BackColor"];
				}
				return Color.Empty;
			}
			set
			{
				this.ViewState["BackColor"] = value;
				this.SetBit(8);
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000C7E22 File Offset: 0x000C6022
		// (set) Token: 0x06003E0B RID: 15883 RVA: 0x000C7E49 File Offset: 0x000C6049
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("Style_BorderColor")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(WebColorConverter))]
		public Color BorderColor
		{
			get
			{
				if (this.IsSet(16))
				{
					return (Color)this.ViewState["BorderColor"];
				}
				return Color.Empty;
			}
			set
			{
				this.ViewState["BorderColor"] = value;
				this.SetBit(16);
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000C7E69 File Offset: 0x000C6069
		// (set) Token: 0x06003E0D RID: 15885 RVA: 0x000C7E90 File Offset: 0x000C6090
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("Style_BorderWidth")]
		[NotifyParentProperty(true)]
		public Unit BorderWidth
		{
			get
			{
				if (this.IsSet(32))
				{
					return (Unit)this.ViewState["BorderWidth"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Style_InvalidBorderWidth"));
				}
				this.ViewState["BorderWidth"] = value;
				this.SetBit(32);
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x000C7EEC File Offset: 0x000C60EC
		// (set) Token: 0x06003E0F RID: 15887 RVA: 0x000C7F0F File Offset: 0x000C610F
		[WebCategory("Appearance")]
		[DefaultValue(BorderStyle.NotSet)]
		[WebSysDescription("Style_BorderStyle")]
		[NotifyParentProperty(true)]
		public BorderStyle BorderStyle
		{
			get
			{
				if (this.IsSet(64))
				{
					return (BorderStyle)this.ViewState["BorderStyle"];
				}
				return BorderStyle.NotSet;
			}
			set
			{
				if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["BorderStyle"] = value;
				this.SetBit(64);
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x000C7F44 File Offset: 0x000C6144
		// (set) Token: 0x06003E11 RID: 15889 RVA: 0x000C7F80 File Offset: 0x000C6180
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Style_CSSClass")]
		[NotifyParentProperty(true)]
		[CssClassProperty]
		public string CssClass
		{
			get
			{
				if (!this.IsSet(2))
				{
					return string.Empty;
				}
				string text = (string)this.ViewState["CssClass"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CssClass"] = value;
				this.SetBit(2);
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000C7F9A File Offset: 0x000C619A
		[WebCategory("Appearance")]
		[WebSysDescription("Style_Font")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public FontInfo Font
		{
			get
			{
				if (this.fontInfo == null)
				{
					this.fontInfo = new FontInfo(this);
				}
				return this.fontInfo;
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003E13 RID: 15891 RVA: 0x000C7FB6 File Offset: 0x000C61B6
		// (set) Token: 0x06003E14 RID: 15892 RVA: 0x000C7FDC File Offset: 0x000C61DC
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("Style_ForeColor")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(WebColorConverter))]
		public Color ForeColor
		{
			get
			{
				if (this.IsSet(4))
				{
					return (Color)this.ViewState["ForeColor"];
				}
				return Color.Empty;
			}
			set
			{
				this.ViewState["ForeColor"] = value;
				this.SetBit(4);
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x000C7FFB File Offset: 0x000C61FB
		// (set) Token: 0x06003E16 RID: 15894 RVA: 0x000C8028 File Offset: 0x000C6228
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("Style_Height")]
		[NotifyParentProperty(true)]
		public Unit Height
		{
			get
			{
				if (this.IsSet(128))
				{
					return (Unit)this.ViewState["Height"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Style_InvalidHeight"));
				}
				this.ViewState["Height"] = value;
				this.SetBit(128);
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x000C807D File Offset: 0x000C627D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsEmpty
		{
			get
			{
				return this.setBits == 0 && this.RegisteredCssClass.Length == 0;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000C8097 File Offset: 0x000C6297
		protected bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000C809F File Offset: 0x000C629F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string RegisteredCssClass
		{
			get
			{
				if (this.registeredCssClass == null)
				{
					return string.Empty;
				}
				return this.registeredCssClass;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x000C80B5 File Offset: 0x000C62B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal StateBag ViewState
		{
			get
			{
				if (this.statebag == null)
				{
					this.statebag = new StateBag(false);
					if (this.IsTrackingViewState)
					{
						this.statebag.TrackViewState();
					}
				}
				return this.statebag;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x000C80E4 File Offset: 0x000C62E4
		// (set) Token: 0x06003E1C RID: 15900 RVA: 0x000C8110 File Offset: 0x000C6310
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("Style_Width")]
		[NotifyParentProperty(true)]
		public Unit Width
		{
			get
			{
				if (this.IsSet(256))
				{
					return (Unit)this.ViewState["Width"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Style_InvalidWidth"));
				}
				this.ViewState["Width"] = value;
				this.SetBit(256);
			}
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x000C8165 File Offset: 0x000C6365
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer, null);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x000C8170 File Offset: 0x000C6370
		public virtual void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			string text = string.Empty;
			bool flag = true;
			if (this.IsSet(2))
			{
				text = (string)this.ViewState["CssClass"];
				if (text == null)
				{
					text = string.Empty;
				}
			}
			if (!string.IsNullOrEmpty(this.registeredCssClass))
			{
				flag = false;
				if (text.Length != 0)
				{
					text = text + " " + this.registeredCssClass;
				}
				else
				{
					text = this.registeredCssClass;
				}
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			}
			if (flag)
			{
				CssStyleCollection styleAttributes = this.GetStyleAttributes(owner);
				styleAttributes.Render(writer);
			}
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x000C8203 File Offset: 0x000C6403
		internal void ClearBit(int bit)
		{
			this.setBits &= ~bit;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000C8214 File Offset: 0x000C6414
		public virtual void CopyFrom(Style s)
		{
			if (this.RegisteredCssClass.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("Style_RegisteredStylesAreReadOnly"));
			}
			if (s != null && !s.IsEmpty)
			{
				this.Font.CopyFrom(s.Font);
				if (s.IsSet(2))
				{
					this.CssClass = s.CssClass;
				}
				if (s.RegisteredCssClass.Length != 0)
				{
					if (this.IsSet(2))
					{
						this.CssClass = this.CssClass + " " + s.RegisteredCssClass;
					}
					else
					{
						this.CssClass = s.RegisteredCssClass;
					}
					if (s.IsSet(8) && s.BackColor != Color.Empty)
					{
						this.ViewState.Remove("BackColor");
						this.ClearBit(8);
					}
					if (s.IsSet(4) && s.ForeColor != Color.Empty)
					{
						this.ViewState.Remove("ForeColor");
						this.ClearBit(4);
					}
					if (s.IsSet(16) && s.BorderColor != Color.Empty)
					{
						this.ViewState.Remove("BorderColor");
						this.ClearBit(16);
					}
					if (s.IsSet(32) && s.BorderWidth != Unit.Empty)
					{
						this.ViewState.Remove("BorderWidth");
						this.ClearBit(32);
					}
					if (s.IsSet(64))
					{
						this.ViewState.Remove("BorderStyle");
						this.ClearBit(64);
					}
					if (s.IsSet(128) && s.Height != Unit.Empty)
					{
						this.ViewState.Remove("Height");
						this.ClearBit(128);
					}
					if (s.IsSet(256) && s.Width != Unit.Empty)
					{
						this.ViewState.Remove("Width");
						this.ClearBit(256);
						return;
					}
				}
				else
				{
					if (s.IsSet(8) && s.BackColor != Color.Empty)
					{
						this.BackColor = s.BackColor;
					}
					if (s.IsSet(4) && s.ForeColor != Color.Empty)
					{
						this.ForeColor = s.ForeColor;
					}
					if (s.IsSet(16) && s.BorderColor != Color.Empty)
					{
						this.BorderColor = s.BorderColor;
					}
					if (s.IsSet(32) && s.BorderWidth != Unit.Empty)
					{
						this.BorderWidth = s.BorderWidth;
					}
					if (s.IsSet(64))
					{
						this.BorderStyle = s.BorderStyle;
					}
					if (s.IsSet(128) && s.Height != Unit.Empty)
					{
						this.Height = s.Height;
					}
					if (s.IsSet(256) && s.Width != Unit.Empty)
					{
						this.Width = s.Width;
					}
				}
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000C852C File Offset: 0x000C672C
		protected virtual void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			StateBag viewState = this.ViewState;
			if (this.IsSet(4))
			{
				Color c = (Color)viewState["ForeColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(c));
				}
			}
			if (this.IsSet(8))
			{
				Color c = (Color)viewState["BackColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(c));
				}
			}
			if (this.IsSet(16))
			{
				Color c = (Color)viewState["BorderColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(c));
				}
			}
			BorderStyle borderStyle = this.BorderStyle;
			Unit borderWidth = this.BorderWidth;
			if (!borderWidth.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.BorderWidth, borderWidth.ToString(CultureInfo.InvariantCulture));
				if (borderStyle == BorderStyle.NotSet)
				{
					if (borderWidth.Value != 0.0)
					{
						attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
					}
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, Style.borderStyles[(int)borderStyle]);
				}
			}
			else if (borderStyle != BorderStyle.NotSet)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, Style.borderStyles[(int)borderStyle]);
			}
			FontInfo font = this.Font;
			string[] names = font.Names;
			if (names.Length != 0)
			{
				attributes.Add(HtmlTextWriterStyle.FontFamily, Style.FormatStringArray(names, ','));
			}
			FontUnit size = font.Size;
			if (!size.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.FontSize, size.ToString(CultureInfo.InvariantCulture));
			}
			if (this.IsSet(2048))
			{
				if (font.Bold)
				{
					attributes.Add(HtmlTextWriterStyle.FontWeight, "bold");
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.FontWeight, "normal");
				}
			}
			if (this.IsSet(4096))
			{
				if (font.Italic)
				{
					attributes.Add(HtmlTextWriterStyle.FontStyle, "italic");
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.FontStyle, "normal");
				}
			}
			string text = string.Empty;
			if (font.Underline)
			{
				text = "underline";
			}
			if (font.Overline)
			{
				text += " overline";
			}
			if (font.Strikeout)
			{
				text += " line-through";
			}
			if (text.Length > 0)
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, text);
			}
			else if (this.IsSet(8192) || this.IsSet(16384) || this.IsSet(32768))
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, "none");
			}
			if (this.IsSet(128))
			{
				Unit unit = (Unit)viewState["Height"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Height, unit.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (this.IsSet(256))
			{
				Unit unit = (Unit)viewState["Width"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Width, unit.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x000C87F8 File Offset: 0x000C69F8
		private static string FormatStringArray(string[] array, char delimiter)
		{
			int num = array.Length;
			if (num == 1)
			{
				return array[0];
			}
			if (num == 0)
			{
				return string.Empty;
			}
			return string.Join(delimiter.ToString(CultureInfo.InvariantCulture), array);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x000C882C File Offset: 0x000C6A2C
		public CssStyleCollection GetStyleAttributes(IUrlResolutionService urlResolver)
		{
			CssStyleCollection cssStyleCollection = new CssStyleCollection();
			this.FillStyleAttributes(cssStyleCollection, urlResolver);
			return cssStyleCollection;
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000C8848 File Offset: 0x000C6A48
		internal bool IsSet(int propKey)
		{
			return (this.setBits & propKey) != 0;
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000C8858 File Offset: 0x000C6A58
		protected internal void LoadViewState(object state)
		{
			if (state != null && this.ownStateBag)
			{
				this.ViewState.LoadViewState(state);
			}
			if (this.statebag != null)
			{
				object obj = this.ViewState["_!SB"];
				if (obj != null)
				{
					this.markedBits = (int)obj;
					this.setBits |= this.markedBits;
				}
			}
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000C88B7 File Offset: 0x000C6AB7
		protected internal virtual void TrackViewState()
		{
			if (this.ownStateBag)
			{
				this.ViewState.TrackViewState();
			}
			this.marked = true;
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000C88D4 File Offset: 0x000C6AD4
		public virtual void MergeWith(Style s)
		{
			if (this.RegisteredCssClass.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("Style_RegisteredStylesAreReadOnly"));
			}
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (this.IsEmpty)
			{
				this.CopyFrom(s);
				return;
			}
			this.Font.MergeWith(s.Font);
			if (s.IsSet(2) && !this.IsSet(2))
			{
				this.CssClass = s.CssClass;
			}
			if (s.RegisteredCssClass.Length == 0)
			{
				if (s.IsSet(8) && (!this.IsSet(8) || this.BackColor == Color.Empty))
				{
					this.BackColor = s.BackColor;
				}
				if (s.IsSet(4) && (!this.IsSet(4) || this.ForeColor == Color.Empty))
				{
					this.ForeColor = s.ForeColor;
				}
				if (s.IsSet(16) && (!this.IsSet(16) || this.BorderColor == Color.Empty))
				{
					this.BorderColor = s.BorderColor;
				}
				if (s.IsSet(32) && (!this.IsSet(32) || this.BorderWidth == Unit.Empty))
				{
					this.BorderWidth = s.BorderWidth;
				}
				if (s.IsSet(64) && !this.IsSet(64))
				{
					this.BorderStyle = s.BorderStyle;
				}
				if (s.IsSet(128) && (!this.IsSet(128) || this.Height == Unit.Empty))
				{
					this.Height = s.Height;
				}
				if (s.IsSet(256) && (!this.IsSet(256) || this.Width == Unit.Empty))
				{
					this.Width = s.Width;
					return;
				}
			}
			else
			{
				if (this.IsSet(2))
				{
					this.CssClass = this.CssClass + " " + s.RegisteredCssClass;
					return;
				}
				this.CssClass = s.RegisteredCssClass;
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000C8AE0 File Offset: 0x000C6CE0
		public virtual void Reset()
		{
			if (this.statebag != null)
			{
				if (this.IsSet(2))
				{
					this.ViewState.Remove("CssClass");
				}
				if (this.IsSet(8))
				{
					this.ViewState.Remove("BackColor");
				}
				if (this.IsSet(4))
				{
					this.ViewState.Remove("ForeColor");
				}
				if (this.IsSet(16))
				{
					this.ViewState.Remove("BorderColor");
				}
				if (this.IsSet(32))
				{
					this.ViewState.Remove("BorderWidth");
				}
				if (this.IsSet(64))
				{
					this.ViewState.Remove("BorderStyle");
				}
				if (this.IsSet(128))
				{
					this.ViewState.Remove("Height");
				}
				if (this.IsSet(256))
				{
					this.ViewState.Remove("Width");
				}
				this.Font.Reset();
				this.ViewState.Remove("_!SB");
				this.markedBits = 0;
			}
			this.setBits = 0;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000C8BF4 File Offset: 0x000C6DF4
		protected internal virtual object SaveViewState()
		{
			if (this.statebag != null)
			{
				if (this.markedBits != 0)
				{
					this.ViewState["_!SB"] = this.markedBits;
				}
				if (this.ownStateBag)
				{
					return this.ViewState.SaveViewState();
				}
			}
			return null;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000C8C41 File Offset: 0x000C6E41
		protected internal virtual void SetBit(int bit)
		{
			this.setBits |= bit;
			if (this.IsTrackingViewState)
			{
				this.markedBits |= bit;
			}
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000C8C67 File Offset: 0x000C6E67
		public void SetDirty()
		{
			this.ViewState.SetDirty(true);
			this.markedBits = this.setBits;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000C8C81 File Offset: 0x000C6E81
		internal void SetRegisteredCssClass(string cssClass)
		{
			this.registeredCssClass = cssClass;
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x000C8C8A File Offset: 0x000C6E8A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000C8C92 File Offset: 0x000C6E92
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000C8C9B File Offset: 0x000C6E9B
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x000C8CA3 File Offset: 0x000C6EA3
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x040023EE RID: 9198
		internal const int UNUSED = 1;

		// Token: 0x040023EF RID: 9199
		internal const int PROP_CSSCLASS = 2;

		// Token: 0x040023F0 RID: 9200
		internal const int PROP_FORECOLOR = 4;

		// Token: 0x040023F1 RID: 9201
		internal const int PROP_BACKCOLOR = 8;

		// Token: 0x040023F2 RID: 9202
		internal const int PROP_BORDERCOLOR = 16;

		// Token: 0x040023F3 RID: 9203
		internal const int PROP_BORDERWIDTH = 32;

		// Token: 0x040023F4 RID: 9204
		internal const int PROP_BORDERSTYLE = 64;

		// Token: 0x040023F5 RID: 9205
		internal const int PROP_HEIGHT = 128;

		// Token: 0x040023F6 RID: 9206
		internal const int PROP_WIDTH = 256;

		// Token: 0x040023F7 RID: 9207
		internal const int PROP_FONT_NAMES = 512;

		// Token: 0x040023F8 RID: 9208
		internal const int PROP_FONT_SIZE = 1024;

		// Token: 0x040023F9 RID: 9209
		internal const int PROP_FONT_BOLD = 2048;

		// Token: 0x040023FA RID: 9210
		internal const int PROP_FONT_ITALIC = 4096;

		// Token: 0x040023FB RID: 9211
		internal const int PROP_FONT_UNDERLINE = 8192;

		// Token: 0x040023FC RID: 9212
		internal const int PROP_FONT_OVERLINE = 16384;

		// Token: 0x040023FD RID: 9213
		internal const int PROP_FONT_STRIKEOUT = 32768;

		// Token: 0x040023FE RID: 9214
		internal const string SetBitsKey = "_!SB";

		// Token: 0x040023FF RID: 9215
		private StateBag statebag;

		// Token: 0x04002400 RID: 9216
		private FontInfo fontInfo;

		// Token: 0x04002401 RID: 9217
		private string registeredCssClass;

		// Token: 0x04002402 RID: 9218
		private bool ownStateBag;

		// Token: 0x04002403 RID: 9219
		private bool marked;

		// Token: 0x04002404 RID: 9220
		private int setBits;

		// Token: 0x04002405 RID: 9221
		private int markedBits;

		// Token: 0x04002406 RID: 9222
		internal static readonly string[] borderStyles = new string[]
		{
			"NotSet",
			"None",
			"Dotted",
			"Dashed",
			"Solid",
			"Double",
			"Groove",
			"Ridge",
			"Inset",
			"Outset"
		};
	}
}

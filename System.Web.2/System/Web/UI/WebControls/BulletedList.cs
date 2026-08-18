using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000382 RID: 898
	[DefaultProperty("BulletStyle")]
	[DefaultEvent("Click")]
	[Designer("System.Web.UI.Design.WebControls.BulletedListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class BulletedList : ListControl, IPostBackEventHandler
	{
		// Token: 0x060029CB RID: 10699 RVA: 0x000875AD File Offset: 0x000857AD
		public BulletedList()
		{
			this._firstItem = 0;
			this._itemCount = -1;
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000875C3 File Offset: 0x000857C3
		// (set) Token: 0x060029CD RID: 10701 RVA: 0x000875CB File Offset: 0x000857CB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoPostBack
		{
			get
			{
				return base.AutoPostBack;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("Property_Set_Not_Supported", new object[]
				{
					"AutoPostBack",
					base.GetType().ToString()
				}));
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000875F8 File Offset: 0x000857F8
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x00087621 File Offset: 0x00085821
		[WebCategory("Appearance")]
		[DefaultValue(BulletStyle.NotSet)]
		[WebSysDescription("BulletedList_BulletStyle")]
		public virtual BulletStyle BulletStyle
		{
			get
			{
				object obj = this.ViewState["BulletStyle"];
				if (obj != null)
				{
					return (BulletStyle)obj;
				}
				return BulletStyle.NotSet;
			}
			set
			{
				if (value < BulletStyle.NotSet || value > BulletStyle.CustomImage)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["BulletStyle"] = value;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x00087650 File Offset: 0x00085850
		// (set) Token: 0x060029D1 RID: 10705 RVA: 0x0008767D File Offset: 0x0008587D
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("BulletedList_BulletImageUrl")]
		public virtual string BulletImageUrl
		{
			get
			{
				object obj = this.ViewState["BulletImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["BulletImageUrl"] = value;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x00060B2F File Offset: 0x0005ED2F
		public override ControlCollection Controls
		{
			get
			{
				return new EmptyControlCollection(this);
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x00087690 File Offset: 0x00085890
		// (set) Token: 0x060029D4 RID: 10708 RVA: 0x000876B9 File Offset: 0x000858B9
		[WebCategory("Behavior")]
		[DefaultValue(BulletedListDisplayMode.Text)]
		[WebSysDescription("BulletedList_BulletedListDisplayMode")]
		public virtual BulletedListDisplayMode DisplayMode
		{
			get
			{
				object obj = this.ViewState["DisplayMode"];
				if (obj != null)
				{
					return (BulletedListDisplayMode)obj;
				}
				return BulletedListDisplayMode.Text;
			}
			set
			{
				if (value < BulletedListDisplayMode.Text || value > BulletedListDisplayMode.LinkButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DisplayMode"] = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000876E4 File Offset: 0x000858E4
		// (set) Token: 0x060029D6 RID: 10710 RVA: 0x0008770D File Offset: 0x0008590D
		[WebCategory("Appearance")]
		[DefaultValue(1)]
		[WebSysDescription("BulletedList_FirstBulletNumber")]
		public virtual int FirstBulletNumber
		{
			get
			{
				object obj = this.ViewState["FirstBulletNumber"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				this.ViewState["FirstBulletNumber"] = value;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x00087728 File Offset: 0x00085928
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x00087751 File Offset: 0x00085951
		[DefaultValue(false)]
		[Themeable(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("ListControl_RenderWhenDataEmpty")]
		public virtual bool RenderWhenDataEmpty
		{
			get
			{
				object obj = this.ViewState["RenderWhenDataEmpty"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RenderWhenDataEmpty"] = value;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x00087769 File Offset: 0x00085969
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x00087771 File Offset: 0x00085971
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int SelectedIndex
		{
			get
			{
				return base.SelectedIndex;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("BulletedList_SelectionNotSupported"));
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x00087782 File Offset: 0x00085982
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ListItem SelectedItem
		{
			get
			{
				return base.SelectedItem;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x0008778A File Offset: 0x0008598A
		// (set) Token: 0x060029DD RID: 10717 RVA: 0x00087771 File Offset: 0x00085971
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SelectedValue
		{
			get
			{
				return base.SelectedValue;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("BulletedList_SelectionNotSupported"));
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x00087792 File Offset: 0x00085992
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.TagKeyInternal;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x0008779C File Offset: 0x0008599C
		internal HtmlTextWriterTag TagKeyInternal
		{
			get
			{
				switch (this.BulletStyle)
				{
				case BulletStyle.NotSet:
					return HtmlTextWriterTag.Ul;
				case BulletStyle.Numbered:
				case BulletStyle.LowerAlpha:
				case BulletStyle.UpperAlpha:
				case BulletStyle.LowerRoman:
				case BulletStyle.UpperRoman:
					return HtmlTextWriterTag.Ol;
				case BulletStyle.Disc:
				case BulletStyle.Circle:
				case BulletStyle.Square:
					return HtmlTextWriterTag.Ul;
				case BulletStyle.CustomImage:
					return HtmlTextWriterTag.Ul;
				default:
					return HtmlTextWriterTag.Ol;
				}
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000877F0 File Offset: 0x000859F0
		// (set) Token: 0x060029E1 RID: 10721 RVA: 0x000835A9 File Offset: 0x000817A9
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("BulletedList_Target")]
		[TypeConverter(typeof(TargetConverter))]
		public virtual string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x0008781D File Offset: 0x00085A1D
		// (set) Token: 0x060029E3 RID: 10723 RVA: 0x00087825 File Offset: 0x00085A25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("BulletedList_TextNotSupported"));
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060029E4 RID: 10724 RVA: 0x00087836 File Offset: 0x00085A36
		// (remove) Token: 0x060029E5 RID: 10725 RVA: 0x00087849 File Offset: 0x00085A49
		[WebCategory("Action")]
		[WebSysDescription("BulletedList_OnClick")]
		public event BulletedListEventHandler Click
		{
			add
			{
				base.Events.AddHandler(BulletedList.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(BulletedList.EventClick, value);
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0008785C File Offset: 0x00085A5C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = false;
			switch (this.BulletStyle)
			{
			case BulletStyle.Numbered:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "decimal");
				flag = true;
				break;
			case BulletStyle.LowerAlpha:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "lower-alpha");
				flag = true;
				break;
			case BulletStyle.UpperAlpha:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "upper-alpha");
				flag = true;
				break;
			case BulletStyle.LowerRoman:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "lower-roman");
				flag = true;
				break;
			case BulletStyle.UpperRoman:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "upper-roman");
				flag = true;
				break;
			case BulletStyle.Disc:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "disc");
				break;
			case BulletStyle.Circle:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "circle");
				break;
			case BulletStyle.Square:
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "square");
				break;
			case BulletStyle.CustomImage:
			{
				string str = base.ResolveClientUrl(this.BulletImageUrl);
				writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleImage, "url(" + HttpUtility.UrlPathEncode(str) + ")");
				break;
			}
			}
			int firstBulletNumber = this.FirstBulletNumber;
			if (flag && firstBulletNumber != 1)
			{
				writer.AddAttribute("start", firstBulletNumber.ToString(CultureInfo.InvariantCulture));
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00087984 File Offset: 0x00085B84
		private string GetPostBackEventReference(string eventArgument)
		{
			if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
			{
				return "javascript:" + Util.GetClientValidatedPostback(this, this.ValidationGroup, eventArgument);
			}
			return this.Page.ClientScript.GetPostBackClientHyperlink(this, eventArgument, true);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000879E0 File Offset: 0x00085BE0
		protected virtual void OnClick(BulletedListEventArgs e)
		{
			BulletedListEventHandler bulletedListEventHandler = (BulletedListEventHandler)base.Events[BulletedList.EventClick];
			if (bulletedListEventHandler != null)
			{
				bulletedListEventHandler(this, e);
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00087A10 File Offset: 0x00085C10
		protected virtual void RenderBulletText(ListItem item, int index, HtmlTextWriter writer)
		{
			switch (this.DisplayMode)
			{
			case BulletedListDisplayMode.Text:
				if (!item.Enabled)
				{
					this.RenderDisabledAttributeHelper(writer, false);
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
				HttpUtility.HtmlEncode(item.Text, writer);
				if (!item.Enabled)
				{
					writer.RenderEndTag();
					return;
				}
				break;
			case BulletedListDisplayMode.HyperLink:
				if (this._cachedIsEnabled && item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, base.ResolveClientUrl(item.Value));
					string target = this.Target;
					if (!string.IsNullOrEmpty(target))
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
					}
				}
				else
				{
					this.RenderDisabledAttributeHelper(writer, item.Enabled);
				}
				this.RenderAccessKey(writer, this.AccessKey);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				HttpUtility.HtmlEncode(item.Text, writer);
				writer.RenderEndTag();
				return;
			case BulletedListDisplayMode.LinkButton:
				if (this._cachedIsEnabled && item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetPostBackEventReference(index.ToString(CultureInfo.InvariantCulture)));
				}
				else
				{
					this.RenderDisabledAttributeHelper(writer, item.Enabled);
				}
				this.RenderAccessKey(writer, this.AccessKey);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				HttpUtility.HtmlEncode(item.Text, writer);
				writer.RenderEndTag();
				break;
			default:
				return;
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00087B43 File Offset: 0x00085D43
		private void RenderDisabledAttributeHelper(HtmlTextWriter writer, bool isItemEnabled)
		{
			if (this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
				return;
			}
			if (!isItemEnabled && !string.IsNullOrEmpty(WebControl.DisabledCssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, WebControl.DisabledCssClass);
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00087B78 File Offset: 0x00085D78
		internal void RenderAccessKey(HtmlTextWriter writer, string AccessKey)
		{
			if (AccessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00087B98 File Offset: 0x00085D98
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0 && !this.RenderWhenDataEmpty)
			{
				return;
			}
			base.Render(writer);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00087BB8 File Offset: 0x00085DB8
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			this._cachedIsEnabled = base.IsEnabled;
			if (this._itemCount == -1)
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					this.Items[i].RenderAttributes(writer);
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					this.RenderBulletText(this.Items[i], i, writer);
					writer.RenderEndTag();
				}
				return;
			}
			for (int j = this._firstItem; j < this._firstItem + this._itemCount; j++)
			{
				this.Items[j].RenderAttributes(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				this.RenderBulletText(this.Items[j], j, writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00087C76 File Offset: 0x00085E76
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new BulletedListEventArgs(int.Parse(eventArgument, CultureInfo.InvariantCulture)));
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00087CB4 File Offset: 0x00085EB4
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x04001E80 RID: 7808
		private static readonly object EventClick = new object();

		// Token: 0x04001E81 RID: 7809
		private bool _cachedIsEnabled;

		// Token: 0x04001E82 RID: 7810
		private int _firstItem;

		// Token: 0x04001E83 RID: 7811
		private int _itemCount;
	}
}

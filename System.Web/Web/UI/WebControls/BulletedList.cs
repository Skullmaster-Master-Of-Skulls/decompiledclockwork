using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D0 RID: 1232
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.BulletedListDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("BulletStyle")]
	[DefaultEvent("Click")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BulletedList : ListControl, IPostBackEventHandler
	{
		// Token: 0x06003B39 RID: 15161 RVA: 0x000F9ACC File Offset: 0x000F8ACC
		public BulletedList()
		{
			this._firstItem = 0;
			this._itemCount = -1;
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x000F9AE2 File Offset: 0x000F8AE2
		// (set) Token: 0x06003B3B RID: 15163 RVA: 0x000F9AEC File Offset: 0x000F8AEC
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

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000F9B28 File Offset: 0x000F8B28
		// (set) Token: 0x06003B3D RID: 15165 RVA: 0x000F9B51 File Offset: 0x000F8B51
		[WebSysDescription("BulletedList_BulletStyle")]
		[WebCategory("Appearance")]
		[DefaultValue(BulletStyle.NotSet)]
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

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000F9B80 File Offset: 0x000F8B80
		// (set) Token: 0x06003B3F RID: 15167 RVA: 0x000F9BAD File Offset: 0x000F8BAD
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[UrlProperty]
		[WebSysDescription("BulletedList_BulletImageUrl")]
		[DefaultValue("")]
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

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000F9BC0 File Offset: 0x000F8BC0
		public override ControlCollection Controls
		{
			get
			{
				return new EmptyControlCollection(this);
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000F9BC8 File Offset: 0x000F8BC8
		// (set) Token: 0x06003B42 RID: 15170 RVA: 0x000F9BF1 File Offset: 0x000F8BF1
		[WebSysDescription("BulletedList_BulletedListDisplayMode")]
		[DefaultValue(BulletedListDisplayMode.Text)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000F9C1C File Offset: 0x000F8C1C
		// (set) Token: 0x06003B44 RID: 15172 RVA: 0x000F9C45 File Offset: 0x000F8C45
		[DefaultValue(1)]
		[WebSysDescription("BulletedList_FirstBulletNumber")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000F9C5D File Offset: 0x000F8C5D
		// (set) Token: 0x06003B46 RID: 15174 RVA: 0x000F9C65 File Offset: 0x000F8C65
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000F9C76 File Offset: 0x000F8C76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ListItem SelectedItem
		{
			get
			{
				return base.SelectedItem;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000F9C7E File Offset: 0x000F8C7E
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x000F9C86 File Offset: 0x000F8C86
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000F9C97 File Offset: 0x000F8C97
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.TagKeyInternal;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000F9CA0 File Offset: 0x000F8CA0
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

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000F9CF4 File Offset: 0x000F8CF4
		// (set) Token: 0x06003B4D RID: 15181 RVA: 0x000F9D21 File Offset: 0x000F8D21
		[TypeConverter(typeof(TargetConverter))]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("BulletedList_Target")]
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

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000F9D34 File Offset: 0x000F8D34
		// (set) Token: 0x06003B4F RID: 15183 RVA: 0x000F9D3C File Offset: 0x000F8D3C
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

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06003B50 RID: 15184 RVA: 0x000F9D4D File Offset: 0x000F8D4D
		// (remove) Token: 0x06003B51 RID: 15185 RVA: 0x000F9D60 File Offset: 0x000F8D60
		[WebSysDescription("BulletedList_OnClick")]
		[WebCategory("Action")]
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

		// Token: 0x06003B52 RID: 15186 RVA: 0x000F9D74 File Offset: 0x000F8D74
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

		// Token: 0x06003B53 RID: 15187 RVA: 0x000F9E9C File Offset: 0x000F8E9C
		private string GetPostBackEventReference(string eventArgument)
		{
			if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
			{
				return "javascript:" + Util.GetClientValidatedPostback(this, this.ValidationGroup, eventArgument);
			}
			return this.Page.ClientScript.GetPostBackClientHyperlink(this, eventArgument, true);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000F9EF8 File Offset: 0x000F8EF8
		protected virtual void OnClick(BulletedListEventArgs e)
		{
			BulletedListEventHandler bulletedListEventHandler = (BulletedListEventHandler)base.Events[BulletedList.EventClick];
			if (bulletedListEventHandler != null)
			{
				bulletedListEventHandler(this, e);
			}
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000F9F28 File Offset: 0x000F8F28
		protected virtual void RenderBulletText(ListItem item, int index, HtmlTextWriter writer)
		{
			switch (this.DisplayMode)
			{
			case BulletedListDisplayMode.Text:
				if (!item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
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
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
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
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
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

		// Token: 0x06003B56 RID: 15190 RVA: 0x000FA060 File Offset: 0x000F9060
		internal void RenderAccessKey(HtmlTextWriter writer, string AccessKey)
		{
			if (AccessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
			}
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000FA080 File Offset: 0x000F9080
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			base.Render(writer);
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000FA098 File Offset: 0x000F9098
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

		// Token: 0x06003B59 RID: 15193 RVA: 0x000FA156 File Offset: 0x000F9156
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new BulletedListEventArgs(int.Parse(eventArgument, CultureInfo.InvariantCulture)));
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000FA194 File Offset: 0x000F9194
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x040026B5 RID: 9909
		private static readonly object EventClick = new object();

		// Token: 0x040026B6 RID: 9910
		private bool _cachedIsEnabled;

		// Token: 0x040026B7 RID: 9911
		private int _firstItem;

		// Token: 0x040026B8 RID: 9912
		private int _itemCount;
	}
}

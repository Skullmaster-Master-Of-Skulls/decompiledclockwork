using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000351 RID: 849
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	public class HtmlInputImage : HtmlInputControl, IPostBackDataHandler, IPostBackEventHandler
	{
		// Token: 0x0600270A RID: 9994 RVA: 0x0007F71D File Offset: 0x0007D91D
		public HtmlInputImage() : base("image")
		{
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x0007F72C File Offset: 0x0007D92C
		// (set) Token: 0x0600270C RID: 9996 RVA: 0x0007EEAC File Offset: 0x0007D0AC
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Align
		{
			get
			{
				string text = base.Attributes["align"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["align"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x0007F754 File Offset: 0x0007D954
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x0007F77C File Offset: 0x0007D97C
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		public string Alt
		{
			get
			{
				string text = base.Attributes["alt"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["alt"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x0007F794 File Offset: 0x0007D994
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x0007EEF2 File Offset: 0x0007D0F2
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["border"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x0007F7C4 File Offset: 0x0007D9C4
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x0007DF48 File Offset: 0x0007C148
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string Src
		{
			get
			{
				string text = base.Attributes["src"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["src"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x0007F7EC File Offset: 0x0007D9EC
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x0007E239 File Offset: 0x0007C439
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = this.ViewState["CausesValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x0007F818 File Offset: 0x0007DA18
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x0007E369 File Offset: 0x0007C569
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("PostBackControl_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = (string)this.ViewState["ValidationGroup"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002717 RID: 10007 RVA: 0x0007F845 File Offset: 0x0007DA45
		// (remove) Token: 0x06002718 RID: 10008 RVA: 0x0007F858 File Offset: 0x0007DA58
		[WebCategory("Action")]
		[WebSysDescription("HtmlInputImage_OnServerClick")]
		public event ImageClickEventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputImage.EventServerClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputImage.EventServerClick, value);
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0007F86B File Offset: 0x0007DA6B
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				if (!base.Disabled)
				{
					this.Page.RegisterRequiresPostBack(this);
				}
				if (this.CausesValidation)
				{
					this.Page.RegisterPostBackScript();
				}
			}
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0007F8A4 File Offset: 0x0007DAA4
		protected virtual void OnServerClick(ImageClickEventArgs e)
		{
			ImageClickEventHandler imageClickEventHandler = (ImageClickEventHandler)base.Events[HtmlInputImage.EventServerClick];
			if (imageClickEventHandler != null)
			{
				imageClickEventHandler(this, e);
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0007F8D2 File Offset: 0x0007DAD2
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0007F8DB File Offset: 0x0007DADB
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(new ImageClickEventArgs(this._x, this._y));
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0007F90D File Offset: 0x0007DB0D
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0007F918 File Offset: 0x0007DB18
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.RenderedNameAttribute + ".x"];
			string text2 = postCollection[this.RenderedNameAttribute + ".y"];
			if (text != null && text2 != null && text.Length > 0 && text2.Length > 0)
			{
				base.ValidateEvent(this.UniqueID);
				this._x = int.Parse(text, CultureInfo.InvariantCulture);
				this._y = int.Parse(text2, CultureInfo.InvariantCulture);
				this.Page.RegisterRequiresRaiseEvent(this);
			}
			return false;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0007F9A6 File Offset: 0x0007DBA6
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0007F9B0 File Offset: 0x0007DBB0
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			if (this.Page != null)
			{
				Util.WriteOnClickAttribute(writer, this, true, false, this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0, this.ValidationGroup);
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x04001DCB RID: 7627
		private static readonly object EventServerClick = new object();

		// Token: 0x04001DCC RID: 7628
		private int _x;

		// Token: 0x04001DCD RID: 7629
		private int _y;
	}
}

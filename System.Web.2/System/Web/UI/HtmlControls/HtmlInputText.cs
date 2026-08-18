using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000356 RID: 854
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[ValidationProperty("Value")]
	public class HtmlInputText : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x06002744 RID: 10052 RVA: 0x0007E10C File Offset: 0x0007C30C
		public HtmlInputText() : base("text")
		{
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0007E119 File Offset: 0x0007C319
		public HtmlInputText(string type) : base(type)
		{
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x0007FD64 File Offset: 0x0007DF64
		// (set) Token: 0x06002747 RID: 10055 RVA: 0x0007F4EA File Offset: 0x0007D6EA
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MaxLength
		{
			get
			{
				string text = (string)this.ViewState["maxlength"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["maxlength"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x0007FD98 File Offset: 0x0007DF98
		// (set) Token: 0x06002749 RID: 10057 RVA: 0x0007F54E File Offset: 0x0007D74E
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["size"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x0007FDC8 File Offset: 0x0007DFC8
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x0007F390 File Offset: 0x0007D590
		public override string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["value"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x0600274C RID: 10060 RVA: 0x0007FDF0 File Offset: 0x0007DFF0
		// (remove) Token: 0x0600274D RID: 10061 RVA: 0x0007FE03 File Offset: 0x0007E003
		[WebCategory("Action")]
		[WebSysDescription("HtmlInputText_ServerChange")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputText.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputText.EventServerChange, value);
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0007FE18 File Offset: 0x0007E018
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputText.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0007FE48 File Offset: 0x0007E048
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			bool disabled = base.Disabled;
			if (!disabled && this.Page != null)
			{
				this.Page.RegisterEnabledControl(this);
			}
			if ((!disabled && base.Events[HtmlInputText.EventServerChange] == null) || base.Type.Equals("password", StringComparison.OrdinalIgnoreCase))
			{
				this.ViewState.SetItemDirty("value", false);
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0007F2EA File Offset: 0x0007D4EA
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.RenderAttributes(writer);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0007FEB3 File Offset: 0x0007E0B3
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0007FEC0 File Offset: 0x0007E0C0
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string value = this.Value;
			string value2 = postCollection.GetValues(postDataKey)[0];
			if (!value.Equals(value2))
			{
				base.ValidateEvent(postDataKey);
				this.Value = value2;
				return true;
			}
			return false;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0007FEF8 File Offset: 0x0007E0F8
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0007FF00 File Offset: 0x0007E100
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DD0 RID: 7632
		private static readonly object EventServerChange = new object();
	}
}

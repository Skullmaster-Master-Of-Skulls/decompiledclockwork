using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000342 RID: 834
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	public class HtmlAnchor : HtmlContainerControl, IPostBackEventHandler
	{
		// Token: 0x06002663 RID: 9827 RVA: 0x0007E201 File Offset: 0x0007C401
		public HtmlAnchor() : base("a")
		{
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x0007E210 File Offset: 0x0007C410
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x0007E239 File Offset: 0x0007C439
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

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x0007E254 File Offset: 0x0007C454
		// (set) Token: 0x06002667 RID: 9831 RVA: 0x0007DED8 File Offset: 0x0007C0D8
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string HRef
		{
			get
			{
				string text = base.Attributes["href"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["href"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x0007E27C File Offset: 0x0007C47C
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x0007E2A4 File Offset: 0x0007C4A4
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				string text = base.Attributes["name"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["name"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x0007E2BC File Offset: 0x0007C4BC
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x0007E2E4 File Offset: 0x0007C4E4
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Target
		{
			get
			{
				string text = base.Attributes["target"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["target"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x0007E2FC File Offset: 0x0007C4FC
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x0007E324 File Offset: 0x0007C524
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Title
		{
			get
			{
				string text = base.Attributes["title"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["title"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0007E33C File Offset: 0x0007C53C
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x0007E369 File Offset: 0x0007C569
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

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06002670 RID: 9840 RVA: 0x0007E37C File Offset: 0x0007C57C
		// (remove) Token: 0x06002671 RID: 9841 RVA: 0x0007E38F File Offset: 0x0007C58F
		[WebCategory("Action")]
		[WebSysDescription("HtmlControl_OnServerClick")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlAnchor.EventServerClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlAnchor.EventServerClick, value);
			}
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0007E3A4 File Offset: 0x0007C5A4
		private PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
			postBackOptions.RequiresJavaScriptProtocol = true;
			if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
			{
				postBackOptions.PerformValidation = true;
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0007E3FC File Offset: 0x0007C5FC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && base.Events[HtmlAnchor.EventServerClick] != null)
			{
				this.Page.RegisterPostBackScript();
				if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
				{
					this.Page.RegisterWebFormsScript();
				}
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0007E464 File Offset: 0x0007C664
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (base.Events[HtmlAnchor.EventServerClick] != null)
			{
				base.Attributes.Remove("href");
				base.RenderAttributes(writer);
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				writer.WriteAttribute("href", postBackEventReference, true);
				return;
			}
			base.PreProcessRelativeReferenceAttribute(writer, "href");
			base.RenderAttributes(writer);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0007E4D8 File Offset: 0x0007C6D8
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlAnchor.EventServerClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0007E506 File Offset: 0x0007C706
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0007E50F File Offset: 0x0007C70F
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		// Token: 0x04001DB9 RID: 7609
		private static readonly object EventServerClick = new object();
	}
}

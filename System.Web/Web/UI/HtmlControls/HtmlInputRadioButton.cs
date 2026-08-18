using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004A3 RID: 1187
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputRadioButton : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x06003775 RID: 14197 RVA: 0x000EDFCB File Offset: 0x000ECFCB
		public HtmlInputRadioButton() : base("radio")
		{
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x000EDFD8 File Offset: 0x000ECFD8
		// (set) Token: 0x06003777 RID: 14199 RVA: 0x000EE006 File Offset: 0x000ED006
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Default")]
		[DefaultValue("")]
		public bool Checked
		{
			get
			{
				string text = base.Attributes["checked"];
				return text != null && text.Equals("checked");
			}
			set
			{
				if (value)
				{
					base.Attributes["checked"] = "checked";
					return;
				}
				base.Attributes["checked"] = null;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000EE034 File Offset: 0x000ED034
		// (set) Token: 0x06003779 RID: 14201 RVA: 0x000EE05C File Offset: 0x000ED05C
		public override string Name
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

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x000EE074 File Offset: 0x000ED074
		// (set) Token: 0x0600377B RID: 14203 RVA: 0x000EE0A4 File Offset: 0x000ED0A4
		public override string Value
		{
			get
			{
				string text = base.Value;
				if (text.Length != 0)
				{
					return text;
				}
				text = this.ID;
				if (text != null)
				{
					return text;
				}
				return this.UniqueID;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600377C RID: 14204 RVA: 0x000EE0B0 File Offset: 0x000ED0B0
		internal override string RenderedNameAttribute
		{
			get
			{
				string text = base.RenderedNameAttribute;
				string uniqueID = this.UniqueID;
				int num = uniqueID.LastIndexOf(base.IdSeparator);
				if (num >= 0)
				{
					text = uniqueID.Substring(0, num + 1) + text;
				}
				return text;
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600377D RID: 14205 RVA: 0x000EE0EE File Offset: 0x000ED0EE
		// (remove) Token: 0x0600377E RID: 14206 RVA: 0x000EE101 File Offset: 0x000ED101
		[WebCategory("Action")]
		[WebSysDescription("Control_OnServerCheckChanged")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputRadioButton.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputRadioButton.EventServerChange, value);
			}
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000EE114 File Offset: 0x000ED114
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && !base.Disabled)
			{
				this.Page.RegisterRequiresPostBack(this);
				this.Page.RegisterEnabledControl(this);
			}
			if (base.Events[HtmlInputRadioButton.EventServerChange] == null && !base.Disabled)
			{
				this.ViewState.SetItemDirty("checked", false);
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000EE17C File Offset: 0x000ED17C
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputRadioButton.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000EE1AC File Offset: 0x000ED1AC
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.Value, this.RenderedNameAttribute);
			}
			writer.WriteAttribute("value", this.Value);
			base.Attributes.Remove("value");
			base.RenderAttributes(writer);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000EE205 File Offset: 0x000ED205
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000EE210 File Offset: 0x000ED210
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.RenderedNameAttribute];
			bool result = false;
			if (text != null && text.Equals(this.Value))
			{
				if (!this.Checked)
				{
					base.ValidateEvent(this.Value, this.RenderedNameAttribute);
					this.Checked = true;
					result = true;
				}
			}
			else if (this.Checked)
			{
				this.Checked = false;
			}
			return result;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000EE272 File Offset: 0x000ED272
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000EE27A File Offset: 0x000ED27A
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x040025CB RID: 9675
		private static readonly object EventServerChange = new object();
	}
}

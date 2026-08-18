using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200049D RID: 1181
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputCheckBox : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x06003720 RID: 14112 RVA: 0x000ED552 File Offset: 0x000EC552
		public HtmlInputCheckBox() : base("checkbox")
		{
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000ED560 File Offset: 0x000EC560
		// (set) Token: 0x06003722 RID: 14114 RVA: 0x000ED58E File Offset: 0x000EC58E
		[WebCategory("Default")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
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

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06003723 RID: 14115 RVA: 0x000ED5BA File Offset: 0x000EC5BA
		// (remove) Token: 0x06003724 RID: 14116 RVA: 0x000ED5CD File Offset: 0x000EC5CD
		[WebSysDescription("Control_OnServerCheckChanged")]
		[WebCategory("Action")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputCheckBox.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputCheckBox.EventServerChange, value);
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000ED5E0 File Offset: 0x000EC5E0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && !base.Disabled)
			{
				this.Page.RegisterRequiresPostBack(this);
				this.Page.RegisterEnabledControl(this);
			}
			if (base.Events[HtmlInputCheckBox.EventServerChange] == null && !base.Disabled)
			{
				this.ViewState.SetItemDirty("checked", false);
			}
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000ED648 File Offset: 0x000EC648
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputCheckBox.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000ED676 File Offset: 0x000EC676
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000ED680 File Offset: 0x000EC680
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string value = postCollection[postDataKey];
			bool flag = !string.IsNullOrEmpty(value);
			bool result = flag != this.Checked;
			this.Checked = flag;
			if (flag)
			{
				base.ValidateEvent(postDataKey);
			}
			return result;
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000ED6BE File Offset: 0x000EC6BE
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.RenderAttributes(writer);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000ED6E5 File Offset: 0x000EC6E5
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000ED6ED File Offset: 0x000EC6ED
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x040025C4 RID: 9668
		private static readonly object EventServerChange = new object();
	}
}

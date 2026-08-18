using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200049F RID: 1183
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputHidden : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x0600373C RID: 14140 RVA: 0x000ED8AB File Offset: 0x000EC8AB
		public HtmlInputHidden() : base("hidden")
		{
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600373D RID: 14141 RVA: 0x000ED8B8 File Offset: 0x000EC8B8
		// (remove) Token: 0x0600373E RID: 14142 RVA: 0x000ED8CB File Offset: 0x000EC8CB
		[WebCategory("Action")]
		[WebSysDescription("HtmlInputHidden_OnServerChange")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputHidden.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputHidden.EventServerChange, value);
			}
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000ED8E0 File Offset: 0x000EC8E0
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputHidden.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000ED910 File Offset: 0x000EC910
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.Disabled)
			{
				if (base.Events[HtmlInputHidden.EventServerChange] == null)
				{
					this.ViewState.SetItemDirty("value", false);
				}
				if (this.Page != null)
				{
					this.Page.RegisterEnabledControl(this);
				}
			}
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000ED963 File Offset: 0x000EC963
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000ED970 File Offset: 0x000EC970
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

		// Token: 0x06003743 RID: 14147 RVA: 0x000ED9A8 File Offset: 0x000EC9A8
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.RenderAttributes(writer);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000ED9CF File Offset: 0x000EC9CF
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000ED9D7 File Offset: 0x000EC9D7
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x040025C5 RID: 9669
		private static readonly object EventServerChange = new object();
	}
}

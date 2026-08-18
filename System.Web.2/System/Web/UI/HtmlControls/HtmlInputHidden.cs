using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000350 RID: 848
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	public class HtmlInputHidden : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x060026FF RID: 9983 RVA: 0x0007F5FF File Offset: 0x0007D7FF
		public HtmlInputHidden() : base("hidden")
		{
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002700 RID: 9984 RVA: 0x0007F60C File Offset: 0x0007D80C
		// (remove) Token: 0x06002701 RID: 9985 RVA: 0x0007F61F File Offset: 0x0007D81F
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

		// Token: 0x06002702 RID: 9986 RVA: 0x0007F634 File Offset: 0x0007D834
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputHidden.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0007F664 File Offset: 0x0007D864
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

		// Token: 0x06002704 RID: 9988 RVA: 0x0007F6B7 File Offset: 0x0007D8B7
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0007F6C4 File Offset: 0x0007D8C4
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

		// Token: 0x06002706 RID: 9990 RVA: 0x0007F2EA File Offset: 0x0007D4EA
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.RenderAttributes(writer);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0007F6FC File Offset: 0x0007D8FC
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0007F704 File Offset: 0x0007D904
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DCA RID: 7626
		private static readonly object EventServerChange = new object();
	}
}

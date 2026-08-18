using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000343 RID: 835
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	public class HtmlButton : HtmlContainerControl, IPostBackEventHandler
	{
		// Token: 0x06002679 RID: 9849 RVA: 0x0007E54E File Offset: 0x0007C74E
		public HtmlButton() : base("button")
		{
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x0007E55C File Offset: 0x0007C75C
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x0007E239 File Offset: 0x0007C439
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

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x0007E588 File Offset: 0x0007C788
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x0007E369 File Offset: 0x0007C569
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

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600267E RID: 9854 RVA: 0x0007E5B5 File Offset: 0x0007C7B5
		// (remove) Token: 0x0600267F RID: 9855 RVA: 0x0007E5C8 File Offset: 0x0007C7C8
		[WebCategory("Action")]
		[WebSysDescription("HtmlControl_OnServerClick")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlButton.EventServerClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlButton.EventServerClick, value);
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0007E5DB File Offset: 0x0007C7DB
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && base.Events[HtmlButton.EventServerClick] != null)
			{
				this.Page.RegisterPostBackScript();
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0007E60C File Offset: 0x0007C80C
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			bool flag = base.Events[HtmlButton.EventServerClick] != null;
			if (this.Page != null && flag)
			{
				Util.WriteOnClickAttribute(writer, this, false, true, this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0, this.ValidationGroup);
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0007E674 File Offset: 0x0007C874
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlButton.EventServerClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0007E6A2 File Offset: 0x0007C8A2
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0007E6AB File Offset: 0x0007C8AB
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		// Token: 0x04001DBA RID: 7610
		private static readonly object EventServerClick = new object();
	}
}

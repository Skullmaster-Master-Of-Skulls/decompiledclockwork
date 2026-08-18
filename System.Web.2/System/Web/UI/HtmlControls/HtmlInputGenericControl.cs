using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000341 RID: 833
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	public class HtmlInputGenericControl : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x06002658 RID: 9816 RVA: 0x0007E10C File Offset: 0x0007C30C
		public HtmlInputGenericControl() : base("text")
		{
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0007E119 File Offset: 0x0007C319
		public HtmlInputGenericControl(string type) : base(type)
		{
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600265A RID: 9818 RVA: 0x0007E122 File Offset: 0x0007C322
		// (remove) Token: 0x0600265B RID: 9819 RVA: 0x0007E135 File Offset: 0x0007C335
		[WebCategory("Action")]
		[WebSysDescription("HtmlInputText_ServerChange")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputGenericControl.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputGenericControl.EventServerChange, value);
			}
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0007E148 File Offset: 0x0007C348
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputGenericControl.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0007E176 File Offset: 0x0007C376
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.Disabled && this.Page != null)
			{
				this.Page.RegisterEnabledControl(this);
			}
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0007E19B File Offset: 0x0007C39B
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0007E1A8 File Offset: 0x0007C3A8
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

		// Token: 0x06002660 RID: 9824 RVA: 0x0007E1E0 File Offset: 0x0007C3E0
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x0007E1E8 File Offset: 0x0007C3E8
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DB8 RID: 7608
		private static readonly object EventServerChange = new object();
	}
}

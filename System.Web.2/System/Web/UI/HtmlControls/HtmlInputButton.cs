using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200034C RID: 844
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	public class HtmlInputButton : HtmlInputControl, IPostBackEventHandler
	{
		// Token: 0x060026CC RID: 9932 RVA: 0x0007EFC2 File Offset: 0x0007D1C2
		public HtmlInputButton() : base("button")
		{
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0007E119 File Offset: 0x0007C319
		public HtmlInputButton(string type) : base(type)
		{
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0007EFD0 File Offset: 0x0007D1D0
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x0007E239 File Offset: 0x0007C439
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

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0007EFFC File Offset: 0x0007D1FC
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0007E369 File Offset: 0x0007C569
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

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060026D2 RID: 9938 RVA: 0x0007F029 File Offset: 0x0007D229
		// (remove) Token: 0x060026D3 RID: 9939 RVA: 0x0007F03C File Offset: 0x0007D23C
		[WebCategory("Action")]
		[WebSysDescription("HtmlControl_OnServerClick")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputButton.EventServerClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputButton.EventServerClick, value);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0007F04F File Offset: 0x0007D24F
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && base.Events[HtmlInputButton.EventServerClick] != null)
			{
				this.Page.RegisterPostBackScript();
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0007F07D File Offset: 0x0007D27D
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			this.RenderAttributesInternal(writer);
			base.RenderAttributes(writer);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0007F090 File Offset: 0x0007D290
		internal virtual void RenderAttributesInternal(HtmlTextWriter writer)
		{
			bool flag = base.Events[HtmlInputButton.EventServerClick] != null;
			if (this.Page != null)
			{
				if (flag)
				{
					Util.WriteOnClickAttribute(writer, this, false, flag, this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0, this.ValidationGroup);
					return;
				}
				this.Page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0007F108 File Offset: 0x0007D308
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputButton.EventServerClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0007F136 File Offset: 0x0007D336
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0007F13F File Offset: 0x0007D33F
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		// Token: 0x04001DC7 RID: 7623
		private static readonly object EventServerClick = new object();
	}
}

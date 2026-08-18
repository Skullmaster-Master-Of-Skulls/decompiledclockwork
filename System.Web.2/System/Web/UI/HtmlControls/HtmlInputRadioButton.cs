using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000353 RID: 851
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	public class HtmlInputRadioButton : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x06002726 RID: 10022 RVA: 0x0007FA49 File Offset: 0x0007DC49
		public HtmlInputRadioButton() : base("radio")
		{
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x0007FA58 File Offset: 0x0007DC58
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x0007F1BA File Offset: 0x0007D3BA
		[WebCategory("Default")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x0007FA88 File Offset: 0x0007DC88
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x0007E2A4 File Offset: 0x0007C4A4
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

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x0007FAB0 File Offset: 0x0007DCB0
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x0007FAE0 File Offset: 0x0007DCE0
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

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x0007FAEC File Offset: 0x0007DCEC
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

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600272E RID: 10030 RVA: 0x0007FB2A File Offset: 0x0007DD2A
		// (remove) Token: 0x0600272F RID: 10031 RVA: 0x0007FB3D File Offset: 0x0007DD3D
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

		// Token: 0x06002730 RID: 10032 RVA: 0x0007FB50 File Offset: 0x0007DD50
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

		// Token: 0x06002731 RID: 10033 RVA: 0x0007FBB8 File Offset: 0x0007DDB8
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputRadioButton.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0007FBE8 File Offset: 0x0007DDE8
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

		// Token: 0x06002733 RID: 10035 RVA: 0x0007FC41 File Offset: 0x0007DE41
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0007FC4C File Offset: 0x0007DE4C
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

		// Token: 0x06002735 RID: 10037 RVA: 0x0007FCAE File Offset: 0x0007DEAE
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0007FCB6 File Offset: 0x0007DEB6
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DCF RID: 7631
		private static readonly object EventServerChange = new object();
	}
}

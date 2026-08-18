using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200042E RID: 1070
	[ControlValueProperty("Value")]
	[DefaultEvent("ValueChanged")]
	[DefaultProperty("Value")]
	[Designer("System.Web.UI.Design.WebControls.HiddenFieldDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[NonVisualControl]
	[SupportsEventValidation]
	public class HiddenField : Control, IPostBackDataHandler
	{
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x060033E1 RID: 13281 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000A9318 File Offset: 0x000A7518
		// (set) Token: 0x060033E3 RID: 13283 RVA: 0x000A9345 File Offset: 0x000A7545
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("HiddenField_Value")]
		public virtual string Value
		{
			get
			{
				string text = (string)this.ViewState["Value"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x060033E4 RID: 13284 RVA: 0x000A9358 File Offset: 0x000A7558
		// (remove) Token: 0x060033E5 RID: 13285 RVA: 0x000A936B File Offset: 0x000A756B
		[WebCategory("Action")]
		[WebSysDescription("HiddenField_OnValueChanged")]
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(HiddenField.EventValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(HiddenField.EventValueChanged, value);
			}
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000A9380 File Offset: 0x000A7580
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.ValidateEvent(this.UniqueID);
			string value = this.Value;
			string value2 = postCollection[postDataKey];
			if (!value.Equals(value2, StringComparison.Ordinal))
			{
				this.Value = value2;
				return true;
			}
			return false;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000A93BC File Offset: 0x000A75BC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.SaveValueViewState)
			{
				this.ViewState.SetItemDirty("Value", false);
			}
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000A93E0 File Offset: 0x000A75E0
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HiddenField.EventValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000A940E File Offset: 0x000A760E
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnValueChanged(EventArgs.Empty);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000A941C File Offset: 0x000A761C
		protected internal override void Render(HtmlTextWriter writer)
		{
			string uniqueID = this.UniqueID;
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
				this.Page.ClientScript.RegisterForEventValidation(uniqueID);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			string value = this.Value;
			if (value.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000A94AB File Offset: 0x000A76AB
		private bool SaveValueViewState
		{
			get
			{
				return base.Events[HiddenField.EventValueChanged] != null || !this.Visible || base.GetType() != typeof(HiddenField);
			}
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000A94E1 File Offset: 0x000A76E1
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000A94EB File Offset: 0x000A76EB
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x04002183 RID: 8579
		private static readonly object EventValueChanged = new object();
	}
}

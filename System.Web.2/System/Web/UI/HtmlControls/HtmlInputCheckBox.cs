using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200034D RID: 845
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	public class HtmlInputCheckBox : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x0007F17E File Offset: 0x0007D37E
		public HtmlInputCheckBox() : base("checkbox")
		{
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0007F18C File Offset: 0x0007D38C
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x0007F1BA File Offset: 0x0007D3BA
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

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060026DE RID: 9950 RVA: 0x0007F1E6 File Offset: 0x0007D3E6
		// (remove) Token: 0x060026DF RID: 9951 RVA: 0x0007F1F9 File Offset: 0x0007D3F9
		[WebCategory("Action")]
		[WebSysDescription("Control_OnServerCheckChanged")]
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

		// Token: 0x060026E0 RID: 9952 RVA: 0x0007F20C File Offset: 0x0007D40C
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

		// Token: 0x060026E1 RID: 9953 RVA: 0x0007F274 File Offset: 0x0007D474
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputCheckBox.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0007F2A2 File Offset: 0x0007D4A2
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0007F2AC File Offset: 0x0007D4AC
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

		// Token: 0x060026E4 RID: 9956 RVA: 0x0007F2EA File Offset: 0x0007D4EA
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.RenderAttributes(writer);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x0007F311 File Offset: 0x0007D511
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0007F319 File Offset: 0x0007D519
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DC8 RID: 7624
		private static readonly object EventServerChange = new object();
	}
}

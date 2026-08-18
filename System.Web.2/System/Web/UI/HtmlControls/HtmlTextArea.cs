using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000360 RID: 864
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[ValidationProperty("Value")]
	public class HtmlTextArea : HtmlContainerControl, IPostBackDataHandler
	{
		// Token: 0x060027FD RID: 10237 RVA: 0x00081678 File Offset: 0x0007F878
		public HtmlTextArea() : base("textarea")
		{
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00081688 File Offset: 0x0007F888
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x000816B6 File Offset: 0x0007F8B6
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Cols
		{
			get
			{
				string text = base.Attributes["cols"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["cols"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x0007F357 File Offset: 0x0007D557
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x00006164 File Offset: 0x00004364
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000816CE File Offset: 0x0007F8CE
		internal string RenderedNameAttribute
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x000816D8 File Offset: 0x0007F8D8
		// (set) Token: 0x06002804 RID: 10244 RVA: 0x00081706 File Offset: 0x0007F906
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Rows
		{
			get
			{
				string text = base.Attributes["rows"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["rows"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x0008171E File Offset: 0x0007F91E
		// (set) Token: 0x06002806 RID: 10246 RVA: 0x00081726 File Offset: 0x0007F926
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002807 RID: 10247 RVA: 0x0008172F File Offset: 0x0007F92F
		// (remove) Token: 0x06002808 RID: 10248 RVA: 0x00081742 File Offset: 0x0007F942
		[WebCategory("Action")]
		[WebSysDescription("HtmlTextArea_OnServerChange")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlTextArea.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlTextArea.EventServerChange, value);
			}
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x00081758 File Offset: 0x0007F958
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is LiteralControl || obj is DataBoundLiteralControl)
			{
				base.AddParsedSubObject(obj);
				return;
			}
			throw new HttpException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
			{
				"HtmlTextArea",
				obj.GetType().Name.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000817B4 File Offset: 0x0007F9B4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
			writer.WriteAttribute("name", this.RenderedNameAttribute);
			base.Attributes.Remove("name");
			base.RenderAttributes(writer);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00081808 File Offset: 0x0007FA08
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlTextArea.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x00081838 File Offset: 0x0007FA38
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.Disabled)
			{
				if (base.Events[HtmlTextArea.EventServerChange] == null)
				{
					this.ViewState.SetItemDirty("value", false);
				}
				if (this.Page != null)
				{
					this.Page.RegisterEnabledControl(this);
				}
			}
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0008188B File Offset: 0x0007FA8B
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x00081898 File Offset: 0x0007FA98
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string value = this.Value;
			string value2 = postCollection.GetValues(postDataKey)[0];
			if (value == null || !value.Equals(value2))
			{
				base.ValidateEvent(postDataKey);
				this.Value = value2;
				return true;
			}
			return false;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000818D3 File Offset: 0x0007FAD3
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000818DB File Offset: 0x0007FADB
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DE1 RID: 7649
		private static readonly object EventServerChange = new object();
	}
}

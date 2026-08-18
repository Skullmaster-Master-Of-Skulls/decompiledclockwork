using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004B1 RID: 1201
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTextArea : HtmlContainerControl, IPostBackDataHandler
	{
		// Token: 0x06003840 RID: 14400 RVA: 0x000EFFD5 File Offset: 0x000EEFD5
		public HtmlTextArea() : base("textarea")
		{
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x000EFFE4 File Offset: 0x000EEFE4
		// (set) Token: 0x06003842 RID: 14402 RVA: 0x000F0012 File Offset: 0x000EF012
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000F002A File Offset: 0x000EF02A
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x000F0032 File Offset: 0x000EF032
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

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000F0034 File Offset: 0x000EF034
		internal string RenderedNameAttribute
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000F003C File Offset: 0x000EF03C
		// (set) Token: 0x06003847 RID: 14407 RVA: 0x000F006A File Offset: 0x000EF06A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000F0082 File Offset: 0x000EF082
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x000F008A File Offset: 0x000EF08A
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600384A RID: 14410 RVA: 0x000F0093 File Offset: 0x000EF093
		// (remove) Token: 0x0600384B RID: 14411 RVA: 0x000F00A6 File Offset: 0x000EF0A6
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

		// Token: 0x0600384C RID: 14412 RVA: 0x000F00BC File Offset: 0x000EF0BC
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

		// Token: 0x0600384D RID: 14413 RVA: 0x000F0118 File Offset: 0x000EF118
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

		// Token: 0x0600384E RID: 14414 RVA: 0x000F016C File Offset: 0x000EF16C
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlTextArea.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000F019C File Offset: 0x000EF19C
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

		// Token: 0x06003850 RID: 14416 RVA: 0x000F01EF File Offset: 0x000EF1EF
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000F01FC File Offset: 0x000EF1FC
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

		// Token: 0x06003852 RID: 14418 RVA: 0x000F0237 File Offset: 0x000EF237
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000F023F File Offset: 0x000EF23F
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x040025DC RID: 9692
		private static readonly object EventServerChange = new object();
	}
}

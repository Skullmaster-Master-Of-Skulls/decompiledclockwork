using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ButtonBase
{
	// Token: 0x0200001B RID: 27
	[ClientScriptResource("Telerik.Web.UI.PostBackButtonBase", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(RadButtonBase))]
	public abstract class PostBackButtonBase : RadButtonBase, IButtonControl, IPostBackEventHandler
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00004851 File Offset: 0x00002A51
		public void RaisePostBackEvent(string eventArgument)
		{
			this.ValidatePage(eventArgument);
			this.OnClick(new ButtonClickEventArgs(false));
			this.OnCommand(new ButtonCommandEventArgs(this.CommandName, this.CommandArgument, false));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000487E File Offset: 0x00002A7E
		protected void ValidatePage(string eventArgument)
		{
			if (this.Page != null)
			{
				this.Page.ClientScript.ValidateEvent(this.UniqueID, eventArgument);
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000048B8 File Offset: 0x00002AB8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
			descriptor.AddProperty("_isClientSubmit", this.IsClientSubmit);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000048F8 File Offset: 0x00002AF8
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			base.Text = (clientState.ContainsKey("text") ? ((string)clientState["text"]) : string.Empty);
			this.CommandName = (clientState.ContainsKey("commandName") ? ((string)clientState["commandName"]) : string.Empty);
			this.CommandArgument = (clientState.ContainsKey("commandArgument") ? ((string)clientState["commandArgument"]) : string.Empty);
			this.AutoPostBack = (!clientState.ContainsKey("autoPostBack") || (bool)clientState["autoPostBack"]);
			this.ValidationGroup = (clientState.ContainsKey("validationGroup") ? ((string)clientState["validationGroup"]) : string.Empty);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000049DC File Offset: 0x00002BDC
		protected virtual void RegisterForEventValidation()
		{
			if (this.Page != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				ClientScriptManager clientScript = this.Page.ClientScript;
				clientScript.RegisterForEventValidation(postBackOptions);
				clientScript.RegisterForEventValidation(this.UniqueID, "true");
				clientScript.RegisterForEventValidation(this.UniqueID, "");
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004A30 File Offset: 0x00002C30
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = base.LoadPostData(postDataKey, postCollection);
			string text = postCollection[postDataKey] ?? postCollection[postDataKey + "_input"];
			if (this.UseSubmitBehavior && text != null && this.Page != null)
			{
				this.Page.ClientScript.ValidateEvent(postDataKey);
				this.Page.RegisterRequiresRaiseEvent(this);
			}
			return result;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004A94 File Offset: 0x00002C94
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), false);
			if (postBackEventReference == null)
			{
				return string.Empty;
			}
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004AD2 File Offset: 0x00002CD2
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00004ADA File Offset: 0x00002CDA
		internal virtual bool IsClientSubmit { get; private set; }

		// Token: 0x0600017C RID: 380 RVA: 0x00004AE4 File Offset: 0x00002CE4
		internal virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
			if (this.Page != null)
			{
				this.IsClientSubmit = (!this.UseSubmitBehavior || this.SingleClick);
				if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
				{
					postBackOptions.PerformValidation = true;
					if (RadAjaxManager.GetCurrent(this.Page) != null)
					{
						this.IsClientSubmit = true;
					}
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (!string.IsNullOrEmpty(this.PostBackUrl))
				{
					postBackOptions.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
				}
			}
			postBackOptions.ClientSubmit = this.IsClientSubmit;
			return postBackOptions;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00004B98 File Offset: 0x00002D98
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00004BA6 File Offset: 0x00002DA6
		[Description("Gets or sets a bool value indicating whether the Button control automatically posts back to the server when clicked.")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[ClientPropertyName("autoPostBack")]
		[Category("Behavior")]
		[Themeable(false)]
		public virtual bool AutoPostBack
		{
			get
			{
				return base.GetViewStateValue<bool>("AutoPostBack", true);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004BBE File Offset: 0x00002DBE
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00004BCC File Offset: 0x00002DCC
		[ClientPropertyName("_causesValidation")]
		[ClientControlProperty]
		[DefaultValue(true)]
		[Themeable(false)]
		[Description("Gets or sets a value indicating whether validation is performed when the Button control is clicked.")]
		[Category("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return base.GetViewStateValue<bool>("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004BE4 File Offset: 0x00002DE4
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00004BF6 File Offset: 0x00002DF6
		[Description("Gets or sets an optional parameter passed to the Command event along with the associated CommandName.")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Bindable(true)]
		[ClientControlProperty]
		[ClientPropertyName("commandArgument")]
		[Themeable(false)]
		public string CommandArgument
		{
			get
			{
				return base.GetViewStateValue<string>("CommandArgument", string.Empty);
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00004C09 File Offset: 0x00002E09
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00004C1B File Offset: 0x00002E1B
		[ClientPropertyName("commandName")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Themeable(false)]
		[Description("Gets or sets the command name associated with the Button control that is passed to the Command event.")]
		[DefaultValue("")]
		public string CommandName
		{
			get
			{
				return base.GetViewStateValue<string>("CommandName", string.Empty);
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00004C2E File Offset: 0x00002E2E
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00004C40 File Offset: 0x00002E40
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL of the page to post to from the current page when the Button control is clicked.")]
		[UrlProperty("*.aspx")]
		[DefaultValue("")]
		[Category("Behavior")]
		[Themeable(false)]
		public string PostBackUrl
		{
			get
			{
				return base.GetViewStateValue<string>("PostBackUrl", string.Empty);
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004C53 File Offset: 0x00002E53
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00004C61 File Offset: 0x00002E61
		[DefaultValue(false)]
		[Themeable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("singleClick")]
		[Description("Gets or sets a bool value indicating whether the Button control will be immediately disabled after the user has clicks it. (i.e. enables/disables 'Single Click' functionality)")]
		public virtual bool SingleClick
		{
			get
			{
				return base.GetViewStateValue<bool>("SingleClick", false);
			}
			set
			{
				this.ViewState["SingleClick"] = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004C79 File Offset: 0x00002E79
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00004C8B File Offset: 0x00002E8B
		[ClientPropertyName("singleClickText")]
		[ClientControlProperty]
		[Category("Appearance")]
		[Bindable(true)]
		[Description("Gets or sets the text displayed in the Button control after the button is being clicked and disabled.")]
		[Localizable(true)]
		[DefaultValue("")]
		public string SingleClickText
		{
			get
			{
				return base.GetViewStateValue<string>("SingleClickText", string.Empty);
			}
			set
			{
				this.ViewState["SingleClickText"] = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004C9E File Offset: 0x00002E9E
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00004CB0 File Offset: 0x00002EB0
		[Description("Gets or sets the group of controls for which the Button control causes validation when it posts back to the server.")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_validationGroup")]
		[Themeable(false)]
		public virtual string ValidationGroup
		{
			get
			{
				return base.GetViewStateValue<string>("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004CC3 File Offset: 0x00002EC3
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00004CD1 File Offset: 0x00002ED1
		[Category("StandardButton")]
		[DefaultValue(true)]
		[Themeable(false)]
		[Description("Gets or sets a value indicating whether the Button control uses the client browser's submit mechanism or the ASP.NET postback mechanism.")]
		public virtual bool UseSubmitBehavior
		{
			get
			{
				return base.GetViewStateValue<bool>("UseSubmitBehavior", true);
			}
			set
			{
				this.ViewState["UseSubmitBehavior"] = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600018F RID: 399 RVA: 0x00004CE9 File Offset: 0x00002EE9
		// (remove) Token: 0x06000190 RID: 400 RVA: 0x00004CFC File Offset: 0x00002EFC
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(PostBackButtonBase.eventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(PostBackButtonBase.eventClick, value);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000191 RID: 401 RVA: 0x00004D0F File Offset: 0x00002F0F
		// (remove) Token: 0x06000192 RID: 402 RVA: 0x00004D22 File Offset: 0x00002F22
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(PostBackButtonBase.eventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(PostBackButtonBase.eventCommand, value);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004D38 File Offset: 0x00002F38
		protected void OnClick(ButtonClickEventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PostBackButtonBase.eventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004D68 File Offset: 0x00002F68
		protected void OnCommand(ButtonCommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[PostBackButtonBase.eventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004DA0 File Offset: 0x00002FA0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, true);
			base.DescribeProperty<bool>(descriptor, "_causesValidation", this.CausesValidation, true);
			base.DescribeProperty<string>(descriptor, "commandArgument", this.CommandArgument, "");
			base.DescribeProperty<string>(descriptor, "commandName", this.CommandName, "");
			base.DescribeProperty<bool>(descriptor, "singleClick", this.SingleClick, false);
			base.DescribeProperty<string>(descriptor, "singleClickText", this.SingleClickText, "");
			base.DescribeProperty<string>(descriptor, "_validationGroup", this.ValidationGroup, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004E49 File Offset: 0x00003049
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04000019 RID: 25
		private static readonly object eventClick = new object();

		// Token: 0x0400001A RID: 26
		private static readonly object eventCommand = new object();
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.InputManager;

namespace Telerik.Web.UI
{
	// Token: 0x0200055A RID: 1370
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class InputSetting : IStateManager
	{
		// Token: 0x0600312D RID: 12589 RVA: 0x000A1AB1 File Offset: 0x0009FCB1
		public InputSetting()
		{
			this._internalTargetControls = new List<TargetInput>();
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000A1ACB File Offset: 0x0009FCCB
		protected StateBag ViewState
		{
			get
			{
				if (this.viewStateValue == null)
				{
					this.viewStateValue = new StateBag(false);
					if (this.isTrackingViewStateValue)
					{
						((IStateManager)this.viewStateValue).TrackViewState();
					}
				}
				return this.viewStateValue;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000A1AFA File Offset: 0x0009FCFA
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewStateValue;
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000A1B02 File Offset: 0x0009FD02
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000A1B0A File Offset: 0x0009FD0A
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000A1B14 File Offset: 0x0009FD14
		internal void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				((IStateManager)this.ViewState).LoadViewState(array[0]);
			}
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000A1B3C File Offset: 0x0009FD3C
		internal object SaveViewState()
		{
			return new object[]
			{
				(this.viewStateValue != null) ? ((IStateManager)this.viewStateValue).SaveViewState() : null
			};
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000A1B6A File Offset: 0x0009FD6A
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewStateValue = true;
			if (this.viewStateValue != null)
			{
				((IStateManager)this.viewStateValue).TrackViewState();
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000A1B86 File Offset: 0x0009FD86
		internal void SetDirty()
		{
			if (this.viewStateValue != null)
			{
				this.viewStateValue.SetDirty(true);
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000A1B9C File Offset: 0x0009FD9C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Description("The InputControls collection")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TargetControlCollection TargetControls
		{
			get
			{
				if (this.targetControls == null)
				{
					this.targetControls = new TargetControlCollection();
				}
				return this.targetControls;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000A1BB7 File Offset: 0x0009FDB7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client-side events")]
		public InputManagerClientEvents ClientEvents
		{
			get
			{
				if (this.clientEvents == null)
				{
					this.clientEvents = new InputManagerClientEvents(this.ViewState);
				}
				return this.clientEvents;
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x000A1BD8 File Offset: 0x0009FDD8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public InputSettingValidation Validation
		{
			get
			{
				if (this.validation == null)
				{
					this.validation = new InputSettingValidation(this.ViewState);
				}
				return this.validation;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000A1BF9 File Offset: 0x0009FDF9
		// (set) Token: 0x0600313A RID: 12602 RVA: 0x000A1C28 File Offset: 0x0009FE28
		[DefaultValue("")]
		[Description("The css style applied to control when is enabled.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual string EnabledCssClass
		{
			get
			{
				if (this.ViewState["EnabledCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["EnabledCssClass"];
			}
			set
			{
				this.ViewState["EnabledCssClass"] = value;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000A1C3B File Offset: 0x0009FE3B
		// (set) Token: 0x0600313C RID: 12604 RVA: 0x000A1C6A File Offset: 0x0009FE6A
		[Category("Behavior")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("")]
		public virtual string BehaviorID
		{
			get
			{
				if (this.ViewState["BehaviorID"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["BehaviorID"];
			}
			set
			{
				this.ViewState["BehaviorID"] = value;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x000A1C7D File Offset: 0x0009FE7D
		// (set) Token: 0x0600313E RID: 12606 RVA: 0x000A1CAC File Offset: 0x0009FEAC
		[Category("Appearance")]
		[Description("The css style applied to control when is hovered.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string HoveredCssClass
		{
			get
			{
				if (this.ViewState["HoveredCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["HoveredCssClass"];
			}
			set
			{
				this.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000A1CBF File Offset: 0x0009FEBF
		// (set) Token: 0x06003140 RID: 12608 RVA: 0x000A1CEE File Offset: 0x0009FEEE
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("The css style applied ti control when the text is invalid.")]
		[NotifyParentProperty(true)]
		public virtual string InvalidCssClass
		{
			get
			{
				if (this.ViewState["InvalidCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["InvalidCssClass"];
			}
			set
			{
				this.ViewState["InvalidCssClass"] = value;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000A1D01 File Offset: 0x0009FF01
		// (set) Token: 0x06003142 RID: 12610 RVA: 0x000A1D30 File Offset: 0x0009FF30
		[Category("Appearance")]
		[Description("The css style applied to control when is focused.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string FocusedCssClass
		{
			get
			{
				if (this.ViewState["FocusedCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["FocusedCssClass"];
			}
			set
			{
				this.ViewState["FocusedCssClass"] = value;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000A1D43 File Offset: 0x0009FF43
		// (set) Token: 0x06003144 RID: 12612 RVA: 0x000A1D72 File Offset: 0x0009FF72
		[NotifyParentProperty(true)]
		[Description("The css style applied to control when is read only.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string ReadOnlyCssClass
		{
			get
			{
				if (this.ViewState["ReadOnlyCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ReadOnlyCssClass"];
			}
			set
			{
				this.ViewState["ReadOnlyCssClass"] = value;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000A1D85 File Offset: 0x0009FF85
		// (set) Token: 0x06003146 RID: 12614 RVA: 0x000A1DB4 File Offset: 0x0009FFB4
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("The css style applied to control when text property is disabled.")]
		[Category("Appearance")]
		public virtual string DisabledCssClass
		{
			get
			{
				if (this.ViewState["DisabledCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["DisabledCssClass"];
			}
			set
			{
				this.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000A1DC7 File Offset: 0x0009FFC7
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x000A1DF6 File Offset: 0x0009FFF6
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The css style applied to control when text property is empty.")]
		[NotifyParentProperty(true)]
		public virtual string EmptyMessageCssClass
		{
			get
			{
				if (this.ViewState["EmptyMessageCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["EmptyMessageCssClass"];
			}
			set
			{
				this.ViewState["EmptyMessageCssClass"] = value;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000A1E09 File Offset: 0x000A0009
		// (set) Token: 0x0600314A RID: 12618 RVA: 0x000A1E38 File Offset: 0x000A0038
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Message shown when the text is empty.")]
		[NotifyParentProperty(true)]
		public virtual string EmptyMessage
		{
			get
			{
				if (this.ViewState["EmptyMessage"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["EmptyMessage"];
			}
			set
			{
				this.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000A1E4B File Offset: 0x000A004B
		// (set) Token: 0x0600314C RID: 12620 RVA: 0x000A1E7A File Offset: 0x000A007A
		[Category("Appearance")]
		[Description("Gets or sets the text for the error message displayed in the control when validation fails")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string ErrorMessage
		{
			get
			{
				if (this.ViewState["ErrorMessage"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ErrorMessage"];
			}
			set
			{
				this.ViewState["ErrorMessage"] = value;
				if (this.validation != null && this.validation.AssignedValidator != null)
				{
					this.validation.AssignedValidator.ErrorMessage = value;
				}
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000A1EB3 File Offset: 0x000A00B3
		// (set) Token: 0x0600314E RID: 12622 RVA: 0x000A1EDE File Offset: 0x000A00DE
		[DefaultValue(typeof(SelectionOnFocus), "CaretToEnd")]
		[NotifyParentProperty(true)]
		[Description("Whether the text in the control selected on focus and how.")]
		[Category("Behavior")]
		public virtual SelectionOnFocus SelectionOnFocus
		{
			get
			{
				if (this.ViewState["SelectionOnFocus"] == null)
				{
					return SelectionOnFocus.CaretToEnd;
				}
				return (SelectionOnFocus)this.ViewState["SelectionOnFocus"];
			}
			set
			{
				this.ViewState["SelectionOnFocus"] = value;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000A1EF6 File Offset: 0x000A00F6
		// (set) Token: 0x06003150 RID: 12624 RVA: 0x000A1F21 File Offset: 0x000A0121
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating the control should be initialized on client or not")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool InitializeOnClient
		{
			get
			{
				return this.ViewState["InitializeOnClient"] != null && (bool)this.ViewState["InitializeOnClient"];
			}
			set
			{
				this.ViewState["InitializeOnClient"] = value;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000A1F3C File Offset: 0x000A013C
		// (set) Token: 0x06003152 RID: 12626 RVA: 0x000A1F65 File Offset: 0x000A0165
		[Description("Gets or sets a value indicating whether the value entered into the textbox should be cleared on error.")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		public virtual bool ClearValueOnError
		{
			get
			{
				object obj = this.ViewState["ClearValueOnError"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ClearValueOnError"] = value;
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000A1F7D File Offset: 0x000A017D
		internal virtual void UpdateValue(TextBox input, bool shouldFormat)
		{
			if (this.IsEmptyMessage(input) && shouldFormat)
			{
				input.Text = this.EmptyMessage;
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000A1F98 File Offset: 0x000A0198
		internal virtual void UpdateCssClass(TextBox input)
		{
			if (!this.IsEmptyMessage(input) && !this.IsNegative(input))
			{
				input.CssClass = this.EnabledCssClass;
			}
			if (!this.IsEmptyMessage(input) && this.IsNegative(input) && this.ViewState["NegativeCssClass"] != null)
			{
				input.CssClass = (string)this.ViewState["NegativeCssClass"];
			}
			if (this.IsEmptyMessage(input))
			{
				input.CssClass = this.EmptyMessageCssClass;
			}
			if (input.ReadOnly)
			{
				input.CssClass = this.ReadOnlyCssClass;
			}
			if (!input.Enabled)
			{
				input.CssClass = this.DisabledCssClass;
			}
			if (this.invalidIds != null && this.invalidIds.Contains(input.ID) && (input.Page.IsPostBack || input.Page.IsCallback))
			{
				input.CssClass = this.InvalidCssClass;
			}
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000A2080 File Offset: 0x000A0280
		private bool IsEmptyMessage(TextBox input)
		{
			return this.IsEmpty(input) && !string.IsNullOrEmpty(this.EmptyMessage);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000A209B File Offset: 0x000A029B
		private bool IsEmpty(TextBox input)
		{
			return (!string.IsNullOrEmpty(input.Text) && input.Text == this.EmptyMessage) || string.IsNullOrEmpty(input.Text);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000A20CF File Offset: 0x000A02CF
		internal virtual bool IsNegative(TextBox input)
		{
			return false;
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000A20D2 File Offset: 0x000A02D2
		public virtual bool IsValid
		{
			get
			{
				if (this.invalidIds != null && this.invalidIds.Count > 0)
				{
					this._isValid = false;
				}
				return this._isValid;
			}
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000A20F7 File Offset: 0x000A02F7
		public virtual void Validate(TextBox input)
		{
			this.Validate(input, null);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000A2104 File Offset: 0x000A0304
		public virtual void Validate(TextBox input, object context)
		{
			if (this.invalidIds == null)
			{
				this.invalidIds = new List<string>();
			}
			if (this.Validation.IsRequired)
			{
				this._isValid = (!string.IsNullOrEmpty(input.Text) && !this.IsEmptyMessage(input));
				if (!this._isValid)
				{
					this.invalidIds.Add(input.ID);
					return;
				}
			}
			if (!string.IsNullOrEmpty(this.Validation.Location) && !string.IsNullOrEmpty(this.Validation.Method))
			{
				HttpWebRequest httpWebRequest = this.CreateWebRequest(input, context);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					string responseText = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
					this._isValid = this.RetriveReturnValueFromResponse(responseText);
					if (!this._isValid)
					{
						this.invalidIds.Add(input.ID);
					}
				}
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000A21E8 File Offset: 0x000A03E8
		private HttpWebRequest CreateWebRequest(TextBox input, object context)
		{
			string arg = string.Format("{0}/{1}", HttpContext.Current.Response.ApplyAppPathModifier(this.Validation.Location), this.Validation.Method);
			Uri url = HttpContext.Current.Request.Url;
			string uriString = string.Format("{0}{1}", url.GetLeftPart(UriPartial.Authority), arg);
			Uri requestUri = new Uri(uriString, UriKind.RelativeOrAbsolute);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			httpWebRequest.UseDefaultCredentials = true;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			string s = string.Format("{{\"id\":\"{0}\",\"value\":\"{1}\",\"context\":{2}}}", input.ID, input.Text, javaScriptSerializer.Serialize(context));
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			httpWebRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
			return httpWebRequest;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000A22F8 File Offset: 0x000A04F8
		private bool RetriveReturnValueFromResponse(string responseText)
		{
			if (string.IsNullOrEmpty(responseText))
			{
				return false;
			}
			bool result;
			if (!bool.TryParse(responseText, out result))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, string> dictionary = javaScriptSerializer.Deserialize<IDictionary<string, string>>(responseText);
				if (dictionary.ContainsKey("d"))
				{
					result = bool.Parse(dictionary["d"]);
				}
			}
			return result;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000A2348 File Offset: 0x000A0548
		internal virtual void Describe(IScriptDescriptor descriptor)
		{
			if (this.EmptyMessageCssClass != "RadInputMgr RadInputMgr_Default RadInput_Empty_Default")
			{
				descriptor.AddProperty("emptyMessageCss", this.EmptyMessageCssClass);
			}
			if (this.EnabledCssClass != "RadInputMgr RadInputMgr_Default RadInput_Enabled_Default")
			{
				descriptor.AddProperty("enabledCss", this.EnabledCssClass);
			}
			if (this.FocusedCssClass != "RadInputMgr RadInputMgr_Default RadInput_Focused_Default")
			{
				descriptor.AddProperty("focusedCss", this.FocusedCssClass);
			}
			if (this.HoveredCssClass != "RadInputMgr RadInputMgr_Default RadInput_Hover_Default")
			{
				descriptor.AddProperty("hoveredCss", this.HoveredCssClass);
			}
			if (this.InvalidCssClass != "RadInputMgr RadInputMgr_Default RadInput_Error_Default")
			{
				descriptor.AddProperty("invalidCss", this.InvalidCssClass);
			}
			if (this.ReadOnlyCssClass != "RadInputMgr RadInputMgr_Default RadInput_Read_Default")
			{
				descriptor.AddProperty("readOnlyCss", this.ReadOnlyCssClass);
			}
			if (this.DisabledCssClass != "RadInputMgr RadInputMgr_Default RadInput_Disabled_Default")
			{
				descriptor.AddProperty("disabledCss", this.DisabledCssClass);
			}
			if (this.SelectionOnFocus != SelectionOnFocus.CaretToEnd)
			{
				descriptor.AddProperty("selectionOnFocus", this.SelectionOnFocus);
			}
			if (!string.IsNullOrEmpty(this.EmptyMessage))
			{
				descriptor.AddProperty("emptyMessage", this.EmptyMessage);
			}
			if (!string.IsNullOrEmpty(this.ErrorMessage))
			{
				descriptor.AddProperty("errorMessage", this.ErrorMessage);
			}
			if (this.InitializeOnClient)
			{
				descriptor.AddProperty("initializeOnClient", this.InitializeOnClient);
			}
			if (!this.ClearValueOnError)
			{
				descriptor.AddProperty("clearValueOnError", this.ClearValueOnError);
			}
			descriptor.AddProperty("_invalidIds", new JavaScriptSerializer().Serialize(this.invalidIds));
			this.ClientEvents.DescribeEvents(descriptor);
			this.Validation.Describe(descriptor);
		}

		// Token: 0x04000D5A RID: 3418
		internal List<TargetInput> _internalTargetControls;

		// Token: 0x04000D5B RID: 3419
		private StateBag viewStateValue;

		// Token: 0x04000D5C RID: 3420
		private bool isTrackingViewStateValue;

		// Token: 0x04000D5D RID: 3421
		private TargetControlCollection targetControls;

		// Token: 0x04000D5E RID: 3422
		private InputManagerClientEvents clientEvents;

		// Token: 0x04000D5F RID: 3423
		private InputSettingValidation validation;

		// Token: 0x04000D60 RID: 3424
		internal bool _isValid = true;

		// Token: 0x04000D61 RID: 3425
		internal List<string> invalidIds;
	}
}

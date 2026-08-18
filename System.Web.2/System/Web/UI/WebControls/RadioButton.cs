using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A8 RID: 1192
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class RadioButton : CheckBox, IPostBackDataHandler
	{
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000C1C58 File Offset: 0x000BFE58
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000C1C85 File Offset: 0x000BFE85
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("RadioButton_GroupName")]
		[Themeable(false)]
		public virtual string GroupName
		{
			get
			{
				string text = (string)this.ViewState["GroupName"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["GroupName"] = value;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000C1C98 File Offset: 0x000BFE98
		internal string UniqueGroupName
		{
			get
			{
				if (this._uniqueGroupName == null)
				{
					string text = this.GroupName;
					string uniqueID = this.UniqueID;
					if (uniqueID != null)
					{
						int num = uniqueID.LastIndexOf(base.IdSeparator);
						if (num >= 0)
						{
							if (text.Length > 0)
							{
								text = uniqueID.Substring(0, num + 1) + text;
							}
							else if (this.NamingContainer is RadioButtonList)
							{
								text = uniqueID.Substring(0, num);
							}
						}
						if (text.Length == 0)
						{
							text = uniqueID;
						}
					}
					this._uniqueGroupName = text;
				}
				return this._uniqueGroupName;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000C1D18 File Offset: 0x000BFF18
		internal string ValueAttribute
		{
			get
			{
				string text = base.Attributes["value"];
				if (text == null)
				{
					base.EnsureID();
					if (this.ID != null)
					{
						text = this.ID;
					}
					else
					{
						text = this.UniqueID;
					}
				}
				return text;
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0008E146 File Offset: 0x0008C346
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x000C1D58 File Offset: 0x000BFF58
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.UniqueGroupName];
			bool result = false;
			if (text != null && text.Equals(this.ValueAttribute))
			{
				base.ValidateEvent(this.UniqueGroupName, text);
				if (!this.Checked)
				{
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

		// Token: 0x06003BAB RID: 15275 RVA: 0x0008E190 File Offset: 0x0008C390
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000C1DB8 File Offset: 0x000BFFB8
		protected override void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && !this.Page.IsPostBackEventControlRegistered)
			{
				this.Page.AutoPostBackControl = this;
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
			this.OnCheckedChanged(EventArgs.Empty);
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000C1E0A File Offset: 0x000C000A
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && !this.Checked && this.Enabled)
			{
				this.Page.RegisterRequiresPostBack(this);
			}
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000C1E38 File Offset: 0x000C0038
		internal override void RenderInputTag(HtmlTextWriter writer, string clientID, string onClick)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueGroupName);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.ValueAttribute);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.UniqueGroupName, this.ValueAttribute);
			}
			if (this.Checked)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!base.IsEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (this.AutoPostBack && !this.Checked && this.Page != null)
			{
				PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
				if (this.CausesValidation)
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (this.Page.Form != null)
				{
					postBackOptions.AutoPostBack = true;
				}
				onClick = Util.MergeScript(onClick, this.Page.ClientScript.GetPostBackEventReference(postBackOptions));
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}
			}
			else if (onClick != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
			}
			string accessKey = this.AccessKey;
			if (accessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
			}
			int tabIndex = (int)this.TabIndex;
			if (tabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this._inputAttributes != null && this._inputAttributes.Count != 0)
			{
				this._inputAttributes.AddAttributes(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x0400234A RID: 9034
		private string _uniqueGroupName;
	}
}

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000621 RID: 1569
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadioButton : CheckBox, IPostBackDataHandler
	{
		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x0013BA00 File Offset: 0x0013AA00
		// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x0013BA2D File Offset: 0x0013AA2D
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("RadioButton_GroupName")]
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

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x0013BA40 File Offset: 0x0013AA40
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

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x0013BAC0 File Offset: 0x0013AAC0
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

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0013BB00 File Offset: 0x0013AB00
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0013BB0C File Offset: 0x0013AB0C
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

		// Token: 0x06004DDA RID: 19930 RVA: 0x0013BB69 File Offset: 0x0013AB69
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0013BB74 File Offset: 0x0013AB74
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

		// Token: 0x06004DDC RID: 19932 RVA: 0x0013BBC6 File Offset: 0x0013ABC6
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && !this.Checked && this.Enabled)
			{
				this.Page.RegisterRequiresPostBack(this);
			}
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0013BBF4 File Offset: 0x0013ABF4
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
			if (!base.IsEnabled)
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

		// Token: 0x04002C71 RID: 11377
		private string _uniqueGroupName;
	}
}

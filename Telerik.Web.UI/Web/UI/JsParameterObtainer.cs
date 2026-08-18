using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200103E RID: 4158
	internal class JsParameterObtainer : Control
	{
		// Token: 0x170033A2 RID: 13218
		// (get) Token: 0x0600A3A0 RID: 41888 RVA: 0x0024682C File Offset: 0x00244A2C
		private string DialogParametersFieldValue
		{
			get
			{
				this.EnsureChildControls();
				return this.Page.Request.Form[this.dialogParametersHolder.ClientID];
			}
		}

		// Token: 0x170033A3 RID: 13219
		// (get) Token: 0x0600A3A1 RID: 41889 RVA: 0x00246854 File Offset: 0x00244A54
		private NameValueCollection QueryString
		{
			get
			{
				return this.Page.Request.QueryString;
			}
		}

		// Token: 0x170033A4 RID: 13220
		// (get) Token: 0x0600A3A2 RID: 41890 RVA: 0x00246866 File Offset: 0x00244A66
		private ParameterPassMode ParameterPassMode
		{
			get
			{
				if (!string.IsNullOrEmpty(this.QueryString["dp"]))
				{
					return ParameterPassMode.QueryString;
				}
				return ParameterPassMode.Javascript;
			}
		}

		// Token: 0x170033A5 RID: 13221
		// (get) Token: 0x0600A3A3 RID: 41891 RVA: 0x00246882 File Offset: 0x00244A82
		private string SerializedParameters
		{
			get
			{
				if (this.ParameterPassMode == ParameterPassMode.QueryString)
				{
					return this.QueryString["dp"];
				}
				return this.DialogParametersFieldValue;
			}
		}

		// Token: 0x170033A6 RID: 13222
		// (get) Token: 0x0600A3A4 RID: 41892 RVA: 0x002468A3 File Offset: 0x00244AA3
		public bool ParametersAvailable
		{
			get
			{
				return this.ParameterPassMode == ParameterPassMode.QueryString || !string.IsNullOrEmpty(this.DialogParametersFieldValue);
			}
		}

		// Token: 0x0600A3A6 RID: 41894 RVA: 0x002468D0 File Offset: 0x00244AD0
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.dialogParametersHolder.ID = "dialogParametersHolder";
			this.Controls.Add(this.dialogParametersHolder);
			if (string.IsNullOrEmpty(this.dialogParametersHolder.Value) && !string.IsNullOrEmpty(this.Page.Request.Form[this.dialogParametersHolder.ClientID]))
			{
				this.dialogParametersHolder.Value = this.Page.Request.Form[this.dialogParametersHolder.ClientID];
			}
			if (!this.ParametersAvailable)
			{
				ScriptManager.RegisterStartupScript(this, typeof(JsParameterObtainer), "DialogParametersSetter", this.GetPostbackScript(), true);
			}
		}

		// Token: 0x0600A3A7 RID: 41895 RVA: 0x0024698C File Offset: 0x00244B8C
		private string GetPostbackScript()
		{
			return string.Format("\r\nfunction GetRadWindow()\r\n{{\r\n\tif (window.radWindow)\r\n\t{{\r\n\t\treturn window.radWindow;\r\n\t}}\r\n\tif (window.frameElement && window.frameElement.radWindow)\r\n\t{{\r\n\t\treturn window.frameElement.radWindow;\r\n\t}}\r\n\t//If using classic windows (window.open)\r\n\t//Cache the reference, as a window can open a second window and the reference would be replaced.\r\n\tif (!window.__localRadEditorRadWindowReference && window.opener && window.opener.__getCurrentRadEditorRadWindowReference)\r\n\t{{\r\n\t\twindow.__localRadEditorRadWindowReference = window.opener.__getCurrentRadEditorRadWindowReference();\r\n\t}}\r\n\treturn window.__localRadEditorRadWindowReference;\r\n}}\r\n\r\nvar dialogParameters = GetRadWindow().DialogParameters;\r\ndocument.getElementById('{0}').value = dialogParameters;\r\n__doPostBack('{1}', '');\r\n", this.dialogParametersHolder.ClientID, this.Page.UniqueID);
		}

		// Token: 0x0600A3A8 RID: 41896 RVA: 0x002469AE File Offset: 0x00244BAE
		public DialogParameters GetDialogParameters()
		{
			if (this.ParametersAvailable)
			{
				return DialogParameters.Deserialize(this.SerializedParameters);
			}
			return null;
		}

		// Token: 0x04002D89 RID: 11657
		private HiddenField dialogParametersHolder = new HiddenField();
	}
}

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019EA RID: 6634
	[ClientScriptResource("Telerik.Web.UI.Widgets.XhtmlValidator", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class XhtmlValidator : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17004D7A RID: 19834
		// (get) Token: 0x060100B2 RID: 65714 RVA: 0x00399941 File Offset: 0x00397B41
		public override string DialogName
		{
			get
			{
				return "XhtmlValidator";
			}
		}

		// Token: 0x060100B3 RID: 65715 RVA: 0x00399948 File Offset: 0x00397B48
		private string DecodePostedString(string formValue)
		{
			if (!string.IsNullOrEmpty(formValue))
			{
				return ContentEncoder.Decode(formValue.Replace("~", "%"));
			}
			return string.Empty;
		}

		// Token: 0x060100B4 RID: 65716 RVA: 0x00399970 File Offset: 0x00397B70
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
			if (this.Page.IsPostBack)
			{
				NameValueCollection form = this.Page.Request.Form;
				string text = this.DecodePostedString(form["editorContent"]);
				string text2 = this.DecodePostedString(form["xhtmlSelect"]);
				string a = form["editorFullPage"];
				string content = string.Empty;
				if (a != "true")
				{
					string text3 = string.IsNullOrEmpty(text2) ? "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">" : text2;
					bool flag = text3.ToLower().IndexOf("dtd xhtml") != -1;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(text3);
					stringBuilder.Append("<html");
					if (flag)
					{
						stringBuilder.Append(" xmlns=\"http://www.w3.org/1999/xhtml\"");
					}
					stringBuilder.Append(">\n<head> <title>Validation Results</title><meta http-equiv=\"Content-Type\" content=\"text/");
					if (flag)
					{
						stringBuilder.Append("x");
					}
					stringBuilder.Append("html; charset=UTF-8\"");
					if (flag)
					{
						stringBuilder.Append("/");
					}
					stringBuilder.Append("></head>\n<body>");
					stringBuilder.Append(text);
					stringBuilder.Append("</body>\n</html>\n");
					content = stringBuilder.ToString();
				}
				else
				{
					content = text;
				}
				HTTPSend httpsend = new HTTPSend("http://validator.w3.org/check");
				httpsend.SetField("charset", "(detect automatically)");
				httpsend.SetField("doctype", "Inline");
				httpsend.SetField("group", "0");
				httpsend.SetField("verbose", "1");
				httpsend.SetField("ss", "1");
				httpsend.SendTextAsFile(content, "RadEditorContent.html");
				string text4 = httpsend.ResponseText.ToString();
				text4 = text4.Replace("<head>", "<head><base href='https://validator.w3.org/'/>");
				text4 = Regex.Replace(text4, "<input.*?>", "");
				text4 = Regex.Replace(text4, "<select[\\s\\S]+?<\\/select>", "", RegexOptions.Multiline);
				this.Page.Response.Clear();
				this.Page.Response.Write(text4);
				this.Page.Response.Flush();
				this.Page.Response.End();
			}
		}
	}
}

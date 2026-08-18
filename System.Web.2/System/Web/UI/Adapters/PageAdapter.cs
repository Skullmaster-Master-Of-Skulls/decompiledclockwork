using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Security.Cryptography;
using System.Web.UI.WebControls;

namespace System.Web.UI.Adapters
{
	// Token: 0x02000338 RID: 824
	public abstract class PageAdapter : ControlAdapter
	{
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual StringCollection CacheVaryByHeaders
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual StringCollection CacheVaryByParams
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x0007D9CF File Offset: 0x0007BBCF
		protected string ClientState
		{
			get
			{
				if (base.Page != null)
				{
					return base.Page.ClientState;
				}
				return null;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x0007D9E6 File Offset: 0x0007BBE6
		internal virtual char IdSeparator
		{
			get
			{
				return '$';
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x0007D9EC File Offset: 0x0007BBEC
		internal string QueryString
		{
			get
			{
				string text = base.Page.ClientQueryString;
				if (base.Page.Request.Browser.RequiresUniqueFilePathSuffix)
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += "&";
					}
					text += base.Page.UniqueFilePathSuffix;
				}
				return text;
			}
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x0007DA43 File Offset: 0x0007BC43
		public virtual NameValueCollection DeterminePostBackMode()
		{
			if (base.Control != null)
			{
				return base.Control.Page.DeterminePostBackMode();
			}
			return null;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0007DA5F File Offset: 0x0007BC5F
		public virtual NameValueCollection DeterminePostBackModeUnvalidated()
		{
			if (base.Control != null)
			{
				return base.Control.Page.DeterminePostBackModeUnvalidated();
			}
			return null;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0007DA7B File Offset: 0x0007BC7B
		public virtual ICollection GetRadioButtonsByGroup(string groupName)
		{
			if (this._radioButtonGroups == null)
			{
				return null;
			}
			return (ICollection)this._radioButtonGroups[groupName];
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0007DA98 File Offset: 0x0007BC98
		protected internal virtual string GetPostBackFormReference(string formId)
		{
			return "document.forms['" + formId + "']";
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0007DAAA File Offset: 0x0007BCAA
		public virtual PageStatePersister GetStatePersister()
		{
			return new HiddenFieldPageStatePersister(base.Page);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0007DAB8 File Offset: 0x0007BCB8
		public virtual void RegisterRadioButton(RadioButton radioButton)
		{
			string groupName = radioButton.GroupName;
			if (string.IsNullOrEmpty(groupName))
			{
				return;
			}
			if (this._radioButtonGroups == null)
			{
				this._radioButtonGroups = new ListDictionary();
			}
			ArrayList arrayList;
			if (this._radioButtonGroups.Contains(groupName))
			{
				arrayList = (ArrayList)this._radioButtonGroups[groupName];
			}
			else
			{
				arrayList = new ArrayList();
				this._radioButtonGroups[groupName] = arrayList;
			}
			arrayList.Add(radioButton);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0007DB27 File Offset: 0x0007BD27
		public virtual void RenderBeginHyperlink(HtmlTextWriter writer, string targetUrl, bool encodeUrl, string softkeyLabel)
		{
			this.RenderBeginHyperlink(writer, targetUrl, encodeUrl, softkeyLabel, null);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0007DB38 File Offset: 0x0007BD38
		public virtual void RenderBeginHyperlink(HtmlTextWriter writer, string targetUrl, bool encodeUrl, string softkeyLabel, string accessKey)
		{
			if (accessKey != null && accessKey.Length > 1)
			{
				throw new ArgumentOutOfRangeException("accessKey");
			}
			string value;
			if (encodeUrl)
			{
				value = HttpUtility.HtmlAttributeEncode(targetUrl);
			}
			else
			{
				value = targetUrl;
			}
			writer.AddAttribute("href", value);
			if (!string.IsNullOrEmpty(accessKey))
			{
				writer.AddAttribute("accessKey", accessKey);
			}
			writer.RenderBeginTag("a");
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0007DB99 File Offset: 0x0007BD99
		public virtual void RenderEndHyperlink(HtmlTextWriter writer)
		{
			writer.WriteEndTag("a");
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0007DBA6 File Offset: 0x0007BDA6
		public virtual void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text)
		{
			this.RenderPostBackEvent(writer, target, argument, softkeyLabel, text, null, null);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0007DBB8 File Offset: 0x0007BDB8
		public virtual void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text, string postUrl, string accessKey)
		{
			this.RenderPostBackEvent(writer, target, argument, softkeyLabel, text, postUrl, accessKey, false);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0007DBD8 File Offset: 0x0007BDD8
		protected void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text, string postUrl, string accessKey, bool encode)
		{
			string text2 = encode ? "&amp;" : "&";
			bool flag = !string.IsNullOrEmpty(postUrl);
			writer.WriteBeginTag("a");
			writer.Write(" href=\"");
			string url;
			if (!flag)
			{
				if (base.Browser["requiresAbsolutePostbackUrl"] == "true")
				{
					url = base.Page.Response.ApplyAppPathModifier(base.Page.Request.CurrentExecutionFilePath);
				}
				else
				{
					url = base.Page.RelativeFilePath;
				}
			}
			else
			{
				url = postUrl;
				base.Page.ContainsCrossPagePost = true;
			}
			writer.WriteEncodedUrl(url);
			writer.Write("?");
			string clientState = this.ClientState;
			if (clientState != null)
			{
				ICollection collection = base.Page.DecomposeViewStateIntoChunks();
				if (collection.Count > 1)
				{
					writer.Write("__VIEWSTATEFIELDCOUNT=" + collection.Count.ToString() + text2);
				}
				int num = 0;
				foreach (object obj in collection)
				{
					string str = (string)obj;
					writer.Write("__VIEWSTATE");
					if (num > 0)
					{
						writer.Write(num.ToString(CultureInfo.CurrentCulture));
					}
					writer.Write("=" + HttpUtility.UrlEncode(str));
					writer.Write(text2);
					num++;
				}
			}
			if (flag)
			{
				writer.Write("__PREVIOUSPAGE");
				writer.Write("=" + Page.EncryptString(base.Page.Request.CurrentExecutionFilePath, Purpose.WebForms_Page_PreviousPageID));
				writer.Write(text2);
			}
			writer.Write("__EVENTTARGET=" + HttpUtility.UrlEncode(target));
			writer.Write(text2);
			writer.Write("__EVENTARGUMENT=" + HttpUtility.UrlEncode(argument));
			string queryString = this.QueryString;
			if (!string.IsNullOrEmpty(queryString))
			{
				writer.Write(text2);
				writer.Write(queryString);
			}
			writer.Write("\"");
			if (!string.IsNullOrEmpty(accessKey))
			{
				writer.WriteAttribute("accessKey", accessKey);
			}
			writer.Write(">");
			writer.Write(text);
			writer.WriteEndTag("a");
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00036414 File Offset: 0x00034614
		public virtual string TransformText(string text)
		{
			return text;
		}

		// Token: 0x04001DB7 RID: 7607
		private IDictionary _radioButtonGroups;
	}
}

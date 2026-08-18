using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000333 RID: 819
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class WebResourceAttribute : Attribute
	{
		// Token: 0x060025F0 RID: 9712 RVA: 0x0007CCB8 File Offset: 0x0007AEB8
		public WebResourceAttribute(string webResource, string contentType)
		{
			if (string.IsNullOrEmpty(webResource))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("webResource");
			}
			if (string.IsNullOrEmpty(contentType))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("contentType");
			}
			this._contentType = contentType;
			this._webResource = webResource;
			this._performSubstitution = false;
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0007CD06 File Offset: 0x0007AF06
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x0007CD17 File Offset: 0x0007AF17
		public string CdnPath
		{
			get
			{
				return this._cdnPath ?? string.Empty;
			}
			set
			{
				this._cdnPath = value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x0007CD20 File Offset: 0x0007AF20
		// (set) Token: 0x060025F4 RID: 9716 RVA: 0x0007CD28 File Offset: 0x0007AF28
		public string LoadSuccessExpression { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x0007CD34 File Offset: 0x0007AF34
		internal string CdnPathSecureConnection
		{
			get
			{
				if (this._cdnPathSecureConnection == null)
				{
					string text = this.CdnPath;
					if (string.IsNullOrEmpty(text) || !this.CdnSupportsSecureConnection || !text.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
					{
						text = string.Empty;
					}
					else
					{
						text = "https" + text.Substring(4);
					}
					this._cdnPathSecureConnection = text;
				}
				return this._cdnPathSecureConnection;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x0007CD95 File Offset: 0x0007AF95
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x0007CD9D File Offset: 0x0007AF9D
		public bool CdnSupportsSecureConnection
		{
			get
			{
				return this._cdnSupportsSecureConnection;
			}
			set
			{
				this._cdnSupportsSecureConnection = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x0007CDA6 File Offset: 0x0007AFA6
		public string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0007CDAE File Offset: 0x0007AFAE
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x0007CDB6 File Offset: 0x0007AFB6
		public bool PerformSubstitution
		{
			get
			{
				return this._performSubstitution;
			}
			set
			{
				this._performSubstitution = value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x0007CDBF File Offset: 0x0007AFBF
		public string WebResource
		{
			get
			{
				return this._webResource;
			}
		}

		// Token: 0x04001DA5 RID: 7589
		private string _contentType;

		// Token: 0x04001DA6 RID: 7590
		private bool _performSubstitution;

		// Token: 0x04001DA7 RID: 7591
		private string _webResource;

		// Token: 0x04001DA8 RID: 7592
		private string _cdnPath;

		// Token: 0x04001DA9 RID: 7593
		private string _cdnPathSecureConnection;

		// Token: 0x04001DAA RID: 7594
		private bool _cdnSupportsSecureConnection;

		// Token: 0x04001DAB RID: 7595
		internal const string _microsoftCdnBasePath = "http://ajax.aspnetcdn.com/ajax/4.6/1/";
	}
}

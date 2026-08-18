using System;
using System.Configuration;
using System.Globalization;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006D0 RID: 1744
	public sealed class CustomErrorsSection : ConfigurationSection
	{
		// Token: 0x060053F8 RID: 21496 RVA: 0x00126C30 File Offset: 0x00124E30
		static CustomErrorsSection()
		{
			CustomErrorsSection._properties = new ConfigurationPropertyCollection();
			CustomErrorsSection._properties.Add(CustomErrorsSection._propAllowNestedErrors);
			CustomErrorsSection._properties.Add(CustomErrorsSection._propDefaultRedirect);
			CustomErrorsSection._properties.Add(CustomErrorsSection._propRedirectMode);
			CustomErrorsSection._properties.Add(CustomErrorsSection._propMode);
			CustomErrorsSection._properties.Add(CustomErrorsSection._propErrors);
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x00126D2A File Offset: 0x00124F2A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomErrorsSection._properties;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x00126D31 File Offset: 0x00124F31
		// (set) Token: 0x060053FC RID: 21500 RVA: 0x00126D43 File Offset: 0x00124F43
		[ConfigurationProperty("allowNestedErrors", DefaultValue = false)]
		public bool AllowNestedErrors
		{
			get
			{
				return (bool)base[CustomErrorsSection._propAllowNestedErrors];
			}
			set
			{
				base[CustomErrorsSection._propAllowNestedErrors] = value;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060053FD RID: 21501 RVA: 0x00126D56 File Offset: 0x00124F56
		// (set) Token: 0x060053FE RID: 21502 RVA: 0x00126D68 File Offset: 0x00124F68
		[ConfigurationProperty("defaultRedirect")]
		public string DefaultRedirect
		{
			get
			{
				return (string)base[CustomErrorsSection._propDefaultRedirect];
			}
			set
			{
				base[CustomErrorsSection._propDefaultRedirect] = value;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x00126D76 File Offset: 0x00124F76
		// (set) Token: 0x06005400 RID: 21504 RVA: 0x00126D88 File Offset: 0x00124F88
		[ConfigurationProperty("redirectMode", DefaultValue = CustomErrorsRedirectMode.ResponseRedirect)]
		public CustomErrorsRedirectMode RedirectMode
		{
			get
			{
				return (CustomErrorsRedirectMode)base[CustomErrorsSection._propRedirectMode];
			}
			set
			{
				base[CustomErrorsSection._propRedirectMode] = value;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06005401 RID: 21505 RVA: 0x00126D9B File Offset: 0x00124F9B
		// (set) Token: 0x06005402 RID: 21506 RVA: 0x00126DAD File Offset: 0x00124FAD
		[ConfigurationProperty("mode", DefaultValue = CustomErrorsMode.RemoteOnly)]
		public CustomErrorsMode Mode
		{
			get
			{
				return (CustomErrorsMode)base[CustomErrorsSection._propMode];
			}
			set
			{
				base[CustomErrorsSection._propMode] = value;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x00126DC0 File Offset: 0x00124FC0
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public CustomErrorCollection Errors
		{
			get
			{
				return (CustomErrorCollection)base[CustomErrorsSection._propErrors];
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x00126DD2 File Offset: 0x00124FD2
		internal string DefaultAbsolutePath
		{
			get
			{
				if (this._DefaultAbsolutePath == null)
				{
					this._DefaultAbsolutePath = CustomErrorsSection.GetAbsoluteRedirect(this.DefaultRedirect, this.basepath);
				}
				return this._DefaultAbsolutePath;
			}
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00126DFC File Offset: 0x00124FFC
		internal string GetRedirectString(int code)
		{
			string text = null;
			if (this.Errors != null)
			{
				CustomError customError = this.Errors[code.ToString(CultureInfo.InvariantCulture)];
				if (customError != null)
				{
					text = CustomErrorsSection.GetAbsoluteRedirect(customError.Redirect, this.basepath);
				}
			}
			if (text == null)
			{
				text = this.DefaultAbsolutePath;
			}
			return text;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00126E4C File Offset: 0x0012504C
		protected override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			CustomErrorsSection customErrorsSection = parentElement as CustomErrorsSection;
			if (customErrorsSection != null)
			{
				this.basepath = customErrorsSection.basepath;
			}
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00126E78 File Offset: 0x00125078
		protected override void DeserializeSection(XmlReader reader)
		{
			base.DeserializeSection(reader);
			WebContext webContext = base.EvaluationContext.HostingContext as WebContext;
			if (webContext != null)
			{
				this.basepath = UrlPath.AppendSlashToPathIfNeeded(webContext.Path);
			}
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00126EB1 File Offset: 0x001250B1
		internal static string GetAbsoluteRedirect(string path, string basePath)
		{
			if (path != null && UrlPath.IsRelativeUrl(path))
			{
				if (string.IsNullOrEmpty(basePath))
				{
					basePath = "/";
				}
				path = UrlPath.Combine(basePath, path);
			}
			return path;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00126ED7 File Offset: 0x001250D7
		internal static CustomErrorsSection GetSettings(HttpContext context)
		{
			return CustomErrorsSection.GetSettings(context, false);
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00126EE0 File Offset: 0x001250E0
		internal static CustomErrorsSection GetSettings(HttpContext context, bool canThrow)
		{
			CustomErrorsSection customErrorsSection = null;
			if (canThrow)
			{
				RuntimeConfig runtimeConfig = RuntimeConfig.GetConfig(context);
				if (runtimeConfig != null)
				{
					customErrorsSection = runtimeConfig.CustomErrors;
				}
			}
			else
			{
				RuntimeConfig runtimeConfig = RuntimeConfig.GetLKGConfig(context);
				if (runtimeConfig != null)
				{
					customErrorsSection = runtimeConfig.CustomErrors;
				}
				if (customErrorsSection == null)
				{
					if (CustomErrorsSection._default == null)
					{
						CustomErrorsSection._default = new CustomErrorsSection();
					}
					customErrorsSection = CustomErrorsSection._default;
				}
			}
			return customErrorsSection;
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00126F34 File Offset: 0x00125134
		internal bool CustomErrorsEnabled(HttpRequest request)
		{
			try
			{
				if (DeploymentSection.RetailInternal)
				{
					return true;
				}
			}
			catch
			{
			}
			switch (this.Mode)
			{
			case CustomErrorsMode.RemoteOnly:
				return !request.IsLocal;
			case CustomErrorsMode.On:
				return true;
			case CustomErrorsMode.Off:
				return false;
			default:
				return false;
			}
			bool result;
			return result;
		}

		// Token: 0x04002C2D RID: 11309
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C2E RID: 11310
		private static readonly ConfigurationProperty _propAllowNestedErrors = new ConfigurationProperty("allowNestedErrors", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C2F RID: 11311
		private static readonly ConfigurationProperty _propDefaultRedirect = new ConfigurationProperty("defaultRedirect", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002C30 RID: 11312
		private static readonly ConfigurationProperty _propRedirectMode = new ConfigurationProperty("redirectMode", typeof(CustomErrorsRedirectMode), CustomErrorsRedirectMode.ResponseRedirect, ConfigurationPropertyOptions.None);

		// Token: 0x04002C31 RID: 11313
		private static readonly ConfigurationProperty _propMode = new ConfigurationProperty("mode", typeof(CustomErrorsMode), CustomErrorsMode.RemoteOnly, ConfigurationPropertyOptions.None);

		// Token: 0x04002C32 RID: 11314
		private static readonly ConfigurationProperty _propErrors = new ConfigurationProperty(null, typeof(CustomErrorCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002C33 RID: 11315
		private string basepath;

		// Token: 0x04002C34 RID: 11316
		private string _DefaultAbsolutePath;

		// Token: 0x04002C35 RID: 11317
		private static CustomErrorsSection _default = null;
	}
}

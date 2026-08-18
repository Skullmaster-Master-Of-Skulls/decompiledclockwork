using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000728 RID: 1832
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthentication : ConfigurationElement
	{
		// Token: 0x0600584D RID: 22605 RVA: 0x00134EEC File Offset: 0x001330EC
		static PassportAuthentication()
		{
			PassportAuthentication._properties = new ConfigurationPropertyCollection();
			PassportAuthentication._properties.Add(PassportAuthentication._propRedirectUrl);
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x0600584F RID: 22607 RVA: 0x00134F56 File Offset: 0x00133156
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PassportAuthentication._properties;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06005850 RID: 22608 RVA: 0x00134F5D File Offset: 0x0013315D
		// (set) Token: 0x06005851 RID: 22609 RVA: 0x00134F6F File Offset: 0x0013316F
		[ConfigurationProperty("redirectUrl", DefaultValue = "internal")]
		[StringValidator]
		public string RedirectUrl
		{
			get
			{
				return (string)base[PassportAuthentication._propRedirectUrl];
			}
			set
			{
				base[PassportAuthentication._propRedirectUrl] = value;
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x00134F7D File Offset: 0x0013317D
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return PassportAuthentication.s_elemProperty;
			}
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x00134F84 File Offset: 0x00133184
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("passport");
			}
			PassportAuthentication passportAuthentication = (PassportAuthentication)value;
			if (StringUtil.StringStartsWith(passportAuthentication.RedirectUrl, "\\\\") || (passportAuthentication.RedirectUrl.Length > 1 && passportAuthentication.RedirectUrl[1] == ':'))
			{
				throw new ConfigurationErrorsException(SR.GetString("Auth_bad_url"));
			}
		}

		// Token: 0x04002EEB RID: 12011
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(PassportAuthentication), new ValidatorCallback(PassportAuthentication.Validate)));

		// Token: 0x04002EEC RID: 12012
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002EED RID: 12013
		private static readonly ConfigurationProperty _propRedirectUrl = new ConfigurationProperty("redirectUrl", typeof(string), "internal", ConfigurationPropertyOptions.None);
	}
}

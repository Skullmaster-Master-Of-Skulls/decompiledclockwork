using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006DE RID: 1758
	public sealed class FormsAuthenticationCredentials : ConfigurationElement
	{
		// Token: 0x060054A5 RID: 21669 RVA: 0x001286CC File Offset: 0x001268CC
		static FormsAuthenticationCredentials()
		{
			FormsAuthenticationCredentials._properties = new ConfigurationPropertyCollection();
			FormsAuthenticationCredentials._properties.Add(FormsAuthenticationCredentials._propUsers);
			FormsAuthenticationCredentials._properties.Add(FormsAuthenticationCredentials._propPasswordFormat);
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x00128738 File Offset: 0x00126938
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationCredentials._properties;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x0012873F File Offset: 0x0012693F
		[ConfigurationProperty("", IsDefaultCollection = true, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public FormsAuthenticationUserCollection Users
		{
			get
			{
				return (FormsAuthenticationUserCollection)base[FormsAuthenticationCredentials._propUsers];
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x00128751 File Offset: 0x00126951
		// (set) Token: 0x060054AA RID: 21674 RVA: 0x00128763 File Offset: 0x00126963
		[ConfigurationProperty("passwordFormat", DefaultValue = FormsAuthPasswordFormat.SHA1)]
		public FormsAuthPasswordFormat PasswordFormat
		{
			get
			{
				return (FormsAuthPasswordFormat)base[FormsAuthenticationCredentials._propPasswordFormat];
			}
			set
			{
				base[FormsAuthenticationCredentials._propPasswordFormat] = value;
			}
		}

		// Token: 0x04002C6A RID: 11370
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C6B RID: 11371
		private static readonly ConfigurationProperty _propUsers = new ConfigurationProperty(null, typeof(FormsAuthenticationUserCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002C6C RID: 11372
		private static readonly ConfigurationProperty _propPasswordFormat = new ConfigurationProperty("passwordFormat", typeof(FormsAuthPasswordFormat), FormsAuthPasswordFormat.SHA1, ConfigurationPropertyOptions.None);
	}
}

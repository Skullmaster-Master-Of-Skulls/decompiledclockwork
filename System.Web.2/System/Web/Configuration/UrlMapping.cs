using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000769 RID: 1897
	public sealed class UrlMapping : ConfigurationElement
	{
		// Token: 0x06005B71 RID: 23409 RVA: 0x0013D28C File Offset: 0x0013B48C
		static UrlMapping()
		{
			UrlMapping._properties = new ConfigurationPropertyCollection();
			UrlMapping._properties.Add(UrlMapping._propUrl);
			UrlMapping._properties.Add(UrlMapping._propMappedUrl);
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x00117E9E File Offset: 0x0011609E
		internal UrlMapping()
		{
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x0013D321 File Offset: 0x0013B521
		public UrlMapping(string url, string mappedUrl)
		{
			base[UrlMapping._propUrl] = url;
			base[UrlMapping._propMappedUrl] = mappedUrl;
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x0013D341 File Offset: 0x0013B541
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMapping._properties;
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06005B75 RID: 23413 RVA: 0x0013D348 File Offset: 0x0013B548
		[ConfigurationProperty("url", IsRequired = true, IsKey = true)]
		public string Url
		{
			get
			{
				return (string)base[UrlMapping._propUrl];
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06005B76 RID: 23414 RVA: 0x0013D35A File Offset: 0x0013B55A
		[ConfigurationProperty("mappedUrl", IsRequired = true)]
		public string MappedUrl
		{
			get
			{
				return (string)base[UrlMapping._propMappedUrl];
			}
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x0013D36C File Offset: 0x0013B56C
		private static void ValidateUrl(object value)
		{
			StdValidatorsAndConverters.NonEmptyStringValidator.Validate(value);
			string text = (string)value;
			if (!UrlPath.IsAppRelativePath(text))
			{
				throw new ConfigurationErrorsException(SR.GetString("UrlMappings_only_app_relative_url_allowed", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x04003039 RID: 12345
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400303A RID: 12346
		private static readonly ConfigurationProperty _propUrl = new ConfigurationProperty("url", typeof(string), null, StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, new CallbackValidator(typeof(string), new ValidatorCallback(UrlMapping.ValidateUrl)), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400303B RID: 12347
		private static readonly ConfigurationProperty _propMappedUrl = new ConfigurationProperty("mappedUrl", typeof(string), null, StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}

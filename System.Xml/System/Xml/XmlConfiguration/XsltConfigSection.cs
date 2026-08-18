using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x02000078 RID: 120
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class XsltConfigSection : ConfigurationSection
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00015C60 File Offset: 0x00014C60
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00015C72 File Offset: 0x00014C72
		[ConfigurationProperty("prohibitDefaultResolver", DefaultValue = "false")]
		internal string ProhibitDefaultResolverString
		{
			get
			{
				return (string)base["prohibitDefaultResolver"];
			}
			set
			{
				base["prohibitDefaultResolver"] = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00015C80 File Offset: 0x00014C80
		private bool _ProhibitDefaultResolver
		{
			get
			{
				string prohibitDefaultResolverString = this.ProhibitDefaultResolverString;
				bool result;
				XmlConvert.TryToBoolean(prohibitDefaultResolverString, out result);
				return result;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00015CA0 File Offset: 0x00014CA0
		private static bool s_ProhibitDefaultUrlResolver
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00015CC8 File Offset: 0x00014CC8
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XsltConfigSection.s_ProhibitDefaultUrlResolver)
			{
				return XmlNullResolver.Singleton;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00015CDC File Offset: 0x00014CDC
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00015CEE File Offset: 0x00014CEE
		[ConfigurationProperty("limitXPathComplexity", DefaultValue = "true")]
		internal string LimitXPathComplexityString
		{
			get
			{
				return (string)base["limitXPathComplexity"];
			}
			set
			{
				base["limitXPathComplexity"] = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00015CFC File Offset: 0x00014CFC
		private bool _LimitXPathComplexity
		{
			get
			{
				string limitXPathComplexityString = this.LimitXPathComplexityString;
				bool result = true;
				XmlConvert.TryToBoolean(limitXPathComplexityString, out result);
				return result;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00015D1C File Offset: 0x00014D1C
		public static bool LimitXPathComplexity
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection == null || xsltConfigSection._LimitXPathComplexity;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00015D44 File Offset: 0x00014D44
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00015D56 File Offset: 0x00014D56
		[ConfigurationProperty("enableMemberAccessForXslCompiledTransform", DefaultValue = "False")]
		internal string EnableMemberAccessForXslCompiledTransformString
		{
			get
			{
				return (string)base["enableMemberAccessForXslCompiledTransform"];
			}
			set
			{
				base["enableMemberAccessForXslCompiledTransform"] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00015D64 File Offset: 0x00014D64
		private bool _EnableMemberAccessForXslCompiledTransform
		{
			get
			{
				string enableMemberAccessForXslCompiledTransformString = this.EnableMemberAccessForXslCompiledTransformString;
				bool result = false;
				XmlConvert.TryToBoolean(enableMemberAccessForXslCompiledTransformString, out result);
				return result;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00015D84 File Offset: 0x00014D84
		internal static bool EnableMemberAccessForXslCompiledTransform
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._EnableMemberAccessForXslCompiledTransform;
			}
		}
	}
}

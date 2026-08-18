using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x020002F5 RID: 757
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XsltConfigSection : ConfigurationSection
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000EC898 File Offset: 0x000EAA98
		// (set) Token: 0x06002D6A RID: 11626 RVA: 0x000EC8AA File Offset: 0x000EAAAA
		[ConfigurationProperty("prohibitDefaultResolver", DefaultValue = "false")]
		public string ProhibitDefaultResolverString
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

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000EC8B8 File Offset: 0x000EAAB8
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

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000EC8D8 File Offset: 0x000EAAD8
		private static bool s_ProhibitDefaultUrlResolver
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000EC900 File Offset: 0x000EAB00
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XsltConfigSection.s_ProhibitDefaultUrlResolver)
			{
				return XmlNullResolver.Singleton;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000EC914 File Offset: 0x000EAB14
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x000EC926 File Offset: 0x000EAB26
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

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000EC934 File Offset: 0x000EAB34
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

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000EC954 File Offset: 0x000EAB54
		internal static bool LimitXPathComplexity
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection == null || xsltConfigSection._LimitXPathComplexity;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000EC97C File Offset: 0x000EAB7C
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x000EC98E File Offset: 0x000EAB8E
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

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000EC99C File Offset: 0x000EAB9C
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

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000EC9BC File Offset: 0x000EABBC
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

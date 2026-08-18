using System;
using System.Globalization;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x020002F3 RID: 755
	internal static class XmlConfigurationString
	{
		// Token: 0x04001390 RID: 5008
		internal const string XmlReaderSectionName = "xmlReader";

		// Token: 0x04001391 RID: 5009
		internal const string XsltSectionName = "xslt";

		// Token: 0x04001392 RID: 5010
		internal const string ProhibitDefaultResolverName = "prohibitDefaultResolver";

		// Token: 0x04001393 RID: 5011
		internal const string LimitXPathComplexityName = "limitXPathComplexity";

		// Token: 0x04001394 RID: 5012
		internal const string EnableMemberAccessForXslCompiledTransformName = "enableMemberAccessForXslCompiledTransform";

		// Token: 0x04001395 RID: 5013
		internal const string CollapseWhiteSpaceIntoEmptyStringName = "CollapseWhiteSpaceIntoEmptyString";

		// Token: 0x04001396 RID: 5014
		internal const string XmlConfigurationSectionName = "system.xml";

		// Token: 0x04001397 RID: 5015
		internal static string XmlReaderSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
		{
			"system.xml",
			"xmlReader"
		});

		// Token: 0x04001398 RID: 5016
		internal static string XsltSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
		{
			"system.xml",
			"xslt"
		});
	}
}

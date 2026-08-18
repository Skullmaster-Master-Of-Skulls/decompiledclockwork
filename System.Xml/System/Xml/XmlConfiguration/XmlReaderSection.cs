using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x02000076 RID: 118
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class XmlReaderSection : ConfigurationSection
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00015B6D File Offset: 0x00014B6D
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00015B7F File Offset: 0x00014B7F
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00015B90 File Offset: 0x00014B90
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00015BB0 File Offset: 0x00014BB0
		internal static bool ProhibitDefaultUrlResolver
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015BD8 File Offset: 0x00014BD8
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver)
			{
				return null;
			}
			return new XmlUrlResolver();
		}
	}
}

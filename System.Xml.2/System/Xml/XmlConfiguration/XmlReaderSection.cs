using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x020002F4 RID: 756
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlReaderSection : ConfigurationSection
	{
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000EC7AD File Offset: 0x000EA9AD
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x000EC7BF File Offset: 0x000EA9BF
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

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000EC7D0 File Offset: 0x000EA9D0
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

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000EC7F0 File Offset: 0x000EA9F0
		internal static bool ProhibitDefaultUrlResolver
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000EC818 File Offset: 0x000EAA18
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver)
			{
				return null;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000EC828 File Offset: 0x000EAA28
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x000EC83A File Offset: 0x000EAA3A
		[ConfigurationProperty("CollapseWhiteSpaceIntoEmptyString", DefaultValue = "false")]
		public string CollapseWhiteSpaceIntoEmptyStringString
		{
			get
			{
				return (string)base["CollapseWhiteSpaceIntoEmptyString"];
			}
			set
			{
				base["CollapseWhiteSpaceIntoEmptyString"] = value;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000EC848 File Offset: 0x000EAA48
		private bool _CollapseWhiteSpaceIntoEmptyString
		{
			get
			{
				string collapseWhiteSpaceIntoEmptyStringString = this.CollapseWhiteSpaceIntoEmptyStringString;
				bool result;
				XmlConvert.TryToBoolean(collapseWhiteSpaceIntoEmptyStringString, out result);
				return result;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000EC868 File Offset: 0x000EAA68
		internal static bool CollapseWhiteSpaceIntoEmptyString
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._CollapseWhiteSpaceIntoEmptyString;
			}
		}
	}
}

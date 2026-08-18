using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x02000077 RID: 119
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class XmlTextReaderSection : ConfigurationSection
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00015BF0 File Offset: 0x00014BF0
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00015C02 File Offset: 0x00014C02
		[ConfigurationProperty("limitCharactersFromEntities", DefaultValue = "true")]
		internal string LimitCharactersFromEntitiesString
		{
			get
			{
				return (string)base["limitCharactersFromEntities"];
			}
			set
			{
				base["limitCharactersFromEntities"] = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00015C10 File Offset: 0x00014C10
		private bool _LimitCharactersFromEntities
		{
			get
			{
				string limitCharactersFromEntitiesString = this.LimitCharactersFromEntitiesString;
				bool result = true;
				XmlConvert.TryToBoolean(limitCharactersFromEntitiesString, out result);
				return result;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00015C30 File Offset: 0x00014C30
		internal static bool LimitCharactersFromEntities
		{
			get
			{
				XmlTextReaderSection xmlTextReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlTextReaderSectionPath) as XmlTextReaderSection;
				return xmlTextReaderSection == null || xmlTextReaderSection._LimitCharactersFromEntities;
			}
		}
	}
}

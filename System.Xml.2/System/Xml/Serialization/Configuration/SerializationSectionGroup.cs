using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001D1 RID: 465
	public sealed class SerializationSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000AA834 File Offset: 0x000A8A34
		[ConfigurationProperty("schemaImporterExtensions")]
		public SchemaImporterExtensionsSection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionsSection)base.Sections["schemaImporterExtensions"];
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000AA84B File Offset: 0x000A8A4B
		[ConfigurationProperty("dateTimeSerialization")]
		public DateTimeSerializationSection DateTimeSerialization
		{
			get
			{
				return (DateTimeSerializationSection)base.Sections["dateTimeSerialization"];
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x000AA862 File Offset: 0x000A8A62
		public XmlSerializerSection XmlSerializer
		{
			get
			{
				return (XmlSerializerSection)base.Sections["xmlSerializer"];
			}
		}
	}
}

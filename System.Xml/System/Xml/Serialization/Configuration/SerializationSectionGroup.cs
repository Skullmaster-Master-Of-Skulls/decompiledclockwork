using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000355 RID: 853
	public sealed class SerializationSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000D3974 File Offset: 0x000D2974
		[ConfigurationProperty("schemaImporterExtensions")]
		public SchemaImporterExtensionsSection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionsSection)base.Sections["schemaImporterExtensions"];
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000D398B File Offset: 0x000D298B
		[ConfigurationProperty("dateTimeSerialization")]
		public DateTimeSerializationSection DateTimeSerialization
		{
			get
			{
				return (DateTimeSerializationSection)base.Sections["dateTimeSerialization"];
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000D39A2 File Offset: 0x000D29A2
		public XmlSerializerSection XmlSerializer
		{
			get
			{
				return (XmlSerializerSection)base.Sections["xmlSerializer"];
			}
		}
	}
}

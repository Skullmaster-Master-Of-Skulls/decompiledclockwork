using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006D5 RID: 1749
	public sealed class EventMappingSettings : ConfigurationElement
	{
		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x001270B7 File Offset: 0x001252B7
		// (set) Token: 0x0600541A RID: 21530 RVA: 0x001270BF File Offset: 0x001252BF
		internal Type RealType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x001270C8 File Offset: 0x001252C8
		static EventMappingSettings()
		{
			EventMappingSettings._properties = new ConfigurationPropertyCollection();
			EventMappingSettings._properties.Add(EventMappingSettings._propName);
			EventMappingSettings._properties.Add(EventMappingSettings._propType);
			EventMappingSettings._properties.Add(EventMappingSettings._propStartEventCode);
			EventMappingSettings._properties.Add(EventMappingSettings._propEndEventCode);
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00117E9E File Offset: 0x0011609E
		internal EventMappingSettings()
		{
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x001271AC File Offset: 0x001253AC
		public EventMappingSettings(string name, string type, int startEventCode, int endEventCode) : this()
		{
			this.Name = name;
			this.Type = type;
			this.StartEventCode = startEventCode;
			this.EndEventCode = endEventCode;
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x001271D1 File Offset: 0x001253D1
		public EventMappingSettings(string name, string type) : this()
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x001271E7 File Offset: 0x001253E7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EventMappingSettings._properties;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06005420 RID: 21536 RVA: 0x001271EE File Offset: 0x001253EE
		// (set) Token: 0x06005421 RID: 21537 RVA: 0x00127200 File Offset: 0x00125400
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[EventMappingSettings._propName];
			}
			set
			{
				base[EventMappingSettings._propName] = value;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06005422 RID: 21538 RVA: 0x0012720E File Offset: 0x0012540E
		// (set) Token: 0x06005423 RID: 21539 RVA: 0x00127220 File Offset: 0x00125420
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public string Type
		{
			get
			{
				return (string)base[EventMappingSettings._propType];
			}
			set
			{
				base[EventMappingSettings._propType] = value;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06005424 RID: 21540 RVA: 0x0012722E File Offset: 0x0012542E
		// (set) Token: 0x06005425 RID: 21541 RVA: 0x00127240 File Offset: 0x00125440
		[ConfigurationProperty("startEventCode", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int StartEventCode
		{
			get
			{
				return (int)base[EventMappingSettings._propStartEventCode];
			}
			set
			{
				base[EventMappingSettings._propStartEventCode] = value;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x00127253 File Offset: 0x00125453
		// (set) Token: 0x06005427 RID: 21543 RVA: 0x00127265 File Offset: 0x00125465
		[ConfigurationProperty("endEventCode", DefaultValue = 2147483647)]
		[IntegerValidator(MinValue = 0)]
		public int EndEventCode
		{
			get
			{
				return (int)base[EventMappingSettings._propEndEventCode];
			}
			set
			{
				base[EventMappingSettings._propEndEventCode] = value;
			}
		}

		// Token: 0x04002C3E RID: 11326
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C3F RID: 11327
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C40 RID: 11328
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002C41 RID: 11329
		private static readonly ConfigurationProperty _propStartEventCode = new ConfigurationProperty("startEventCode", typeof(int), 0, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002C42 RID: 11330
		private static readonly ConfigurationProperty _propEndEventCode = new ConfigurationProperty("endEventCode", typeof(int), int.MaxValue, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002C43 RID: 11331
		private Type _type;
	}
}

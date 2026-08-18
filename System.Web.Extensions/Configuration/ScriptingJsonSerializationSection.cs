using System;
using System.Configuration;
using System.Web.Script.Serialization;

namespace System.Web.Configuration
{
	// Token: 0x020000E2 RID: 226
	public sealed class ScriptingJsonSerializationSection : ConfigurationSection
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x0002ABCC File Offset: 0x00028DCC
		private static ConfigurationPropertyCollection BuildProperties()
		{
			return new ConfigurationPropertyCollection
			{
				ScriptingJsonSerializationSection._propConverters,
				ScriptingJsonSerializationSection._propRecursionLimitLimit,
				ScriptingJsonSerializationSection._propMaxJsonLength
			};
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002AC01 File Offset: 0x00028E01
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ScriptingJsonSerializationSection._properties;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0002AC08 File Offset: 0x00028E08
		[ConfigurationProperty("converters", IsKey = true, DefaultValue = "")]
		public ConvertersCollection Converters
		{
			get
			{
				return (ConvertersCollection)base[ScriptingJsonSerializationSection._propConverters];
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002AC1A File Offset: 0x00028E1A
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0002AC2C File Offset: 0x00028E2C
		[ConfigurationProperty("recursionLimit", DefaultValue = 100)]
		public int RecursionLimit
		{
			get
			{
				return (int)base[ScriptingJsonSerializationSection._propRecursionLimitLimit];
			}
			set
			{
				base[ScriptingJsonSerializationSection._propRecursionLimitLimit] = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002AC3F File Offset: 0x00028E3F
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0002AC51 File Offset: 0x00028E51
		[ConfigurationProperty("maxJsonLength", DefaultValue = 102400)]
		public int MaxJsonLength
		{
			get
			{
				return (int)base[ScriptingJsonSerializationSection._propMaxJsonLength];
			}
			set
			{
				base[ScriptingJsonSerializationSection._propMaxJsonLength] = value;
			}
		}

		// Token: 0x0400037D RID: 893
		private static readonly ConfigurationProperty _propConverters = new ConfigurationProperty("converters", typeof(ConvertersCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x0400037E RID: 894
		private static readonly ConfigurationProperty _propRecursionLimitLimit = new ConfigurationProperty("recursionLimit", typeof(int), 100, null, new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x0400037F RID: 895
		private static readonly ConfigurationProperty _propMaxJsonLength = new ConfigurationProperty("maxJsonLength", typeof(int), 102400, null, new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04000380 RID: 896
		private static ConfigurationPropertyCollection _properties = ScriptingJsonSerializationSection.BuildProperties();

		// Token: 0x02000178 RID: 376
		internal class ApplicationSettings
		{
			// Token: 0x06001076 RID: 4214 RVA: 0x0003896C File Offset: 0x00036B6C
			internal ApplicationSettings()
			{
				ScriptingJsonSerializationSection scriptingJsonSerializationSection = (ScriptingJsonSerializationSection)WebConfigurationManager.GetSection("system.web.extensions/scripting/webServices/jsonSerialization");
				if (scriptingJsonSerializationSection != null)
				{
					this._recusionLimit = scriptingJsonSerializationSection.RecursionLimit;
					this._maxJsonLimit = scriptingJsonSerializationSection.MaxJsonLength;
					this._converters = scriptingJsonSerializationSection.Converters.CreateConverters();
					return;
				}
				this._recusionLimit = (int)ScriptingJsonSerializationSection._propRecursionLimitLimit.DefaultValue;
				this._maxJsonLimit = (int)ScriptingJsonSerializationSection._propMaxJsonLength.DefaultValue;
				this._converters = new JavaScriptConverter[0];
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x06001077 RID: 4215 RVA: 0x000389F2 File Offset: 0x00036BF2
			internal int RecursionLimit
			{
				get
				{
					return this._recusionLimit;
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001078 RID: 4216 RVA: 0x000389FA File Offset: 0x00036BFA
			internal int MaxJsonLimit
			{
				get
				{
					return this._maxJsonLimit;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001079 RID: 4217 RVA: 0x00038A02 File Offset: 0x00036C02
			internal JavaScriptConverter[] Converters
			{
				get
				{
					return this._converters;
				}
			}

			// Token: 0x04000516 RID: 1302
			private int _recusionLimit;

			// Token: 0x04000517 RID: 1303
			private int _maxJsonLimit;

			// Token: 0x04000518 RID: 1304
			private JavaScriptConverter[] _converters;
		}
	}
}

using System;
using System.Configuration;
using System.Globalization;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006AE RID: 1710
	public sealed class BuildProvider : ConfigurationElement
	{
		// Token: 0x060052E9 RID: 21225 RVA: 0x00123F6C File Offset: 0x0012216C
		static BuildProvider()
		{
			BuildProvider._properties = new ConfigurationPropertyCollection();
			BuildProvider._properties.Add(BuildProvider._propExtension);
			BuildProvider._properties.Add(BuildProvider._propType);
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00123FE4 File Offset: 0x001221E4
		public BuildProvider(string extension, string type) : this()
		{
			this.Extension = extension;
			this.Type = type;
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x00123FFA File Offset: 0x001221FA
		internal BuildProvider()
		{
			this._info = new BuildProvider.ConfigurationBuildProviderInfo(this);
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x060052EC RID: 21228 RVA: 0x0012400E File Offset: 0x0012220E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BuildProvider._properties;
			}
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00124018 File Offset: 0x00122218
		public override bool Equals(object provider)
		{
			BuildProvider buildProvider = provider as BuildProvider;
			return buildProvider != null && StringUtil.EqualsIgnoreCase(this.Extension, buildProvider.Extension) && this.Type == buildProvider.Type;
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x00124055 File Offset: 0x00122255
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(StringUtil.GetNonRandomizedHashCode(this.Extension.ToLower(CultureInfo.InvariantCulture), false), this.Type.GetHashCode());
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x060052EF RID: 21231 RVA: 0x0012407D File Offset: 0x0012227D
		// (set) Token: 0x060052F0 RID: 21232 RVA: 0x0012408F File Offset: 0x0012228F
		[ConfigurationProperty("extension", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Extension
		{
			get
			{
				return (string)base[BuildProvider._propExtension];
			}
			set
			{
				base[BuildProvider._propExtension] = value;
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x0012409D File Offset: 0x0012229D
		// (set) Token: 0x060052F2 RID: 21234 RVA: 0x001240AF File Offset: 0x001222AF
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base[BuildProvider._propType];
			}
			set
			{
				base[BuildProvider._propType] = value;
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x060052F3 RID: 21235 RVA: 0x001240BD File Offset: 0x001222BD
		internal BuildProviderInfo BuildProviderInfo
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x04002B7A RID: 11130
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002B7B RID: 11131
		private static readonly ConfigurationProperty _propExtension = new ConfigurationProperty("extension", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002B7C RID: 11132
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002B7D RID: 11133
		private readonly BuildProviderInfo _info;

		// Token: 0x02000A3F RID: 2623
		private class ConfigurationBuildProviderInfo : BuildProviderInfo
		{
			// Token: 0x06006E85 RID: 28293 RVA: 0x00189DA4 File Offset: 0x00187FA4
			public ConfigurationBuildProviderInfo(BuildProvider buildProvider)
			{
				this._buildProvider = buildProvider;
			}

			// Token: 0x17001E3B RID: 7739
			// (get) Token: 0x06006E86 RID: 28294 RVA: 0x00189DC0 File Offset: 0x00187FC0
			internal override Type Type
			{
				get
				{
					if (this._type == null)
					{
						object @lock = this._lock;
						lock (@lock)
						{
							if (this._type == null)
							{
								this._type = CompilationUtil.LoadTypeWithChecks(this._buildProvider.Type, typeof(BuildProvider), null, this._buildProvider, "type");
							}
						}
					}
					return this._type;
				}
			}

			// Token: 0x04003AFE RID: 15102
			private readonly BuildProvider _buildProvider;

			// Token: 0x04003AFF RID: 15103
			private object _lock = new object();

			// Token: 0x04003B00 RID: 15104
			private Type _type;
		}
	}
}

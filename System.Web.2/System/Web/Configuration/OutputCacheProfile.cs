using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Configuration
{
	// Token: 0x0200071F RID: 1823
	public sealed class OutputCacheProfile : ConfigurationElement
	{
		// Token: 0x060057B3 RID: 22451 RVA: 0x00133468 File Offset: 0x00131668
		static OutputCacheProfile()
		{
			OutputCacheProfile._propVaryByContentEncoding = new ConfigurationProperty("varyByContentEncoding", typeof(string), null, ConfigurationPropertyOptions.None);
			OutputCacheProfile._propVaryByHeader = new ConfigurationProperty("varyByHeader", typeof(string), null, ConfigurationPropertyOptions.None);
			OutputCacheProfile._propVaryByParam = new ConfigurationProperty("varyByParam", typeof(string), null, ConfigurationPropertyOptions.None);
			OutputCacheProfile._propNoStore = new ConfigurationProperty("noStore", typeof(bool), false, ConfigurationPropertyOptions.None);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propName);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propEnabled);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propDuration);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propLocation);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propSqlDependency);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propVaryByCustom);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propVaryByControl);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propVaryByContentEncoding);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propVaryByHeader);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propVaryByParam);
			OutputCacheProfile._properties.Add(OutputCacheProfile._propNoStore);
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x00117E9E File Offset: 0x0011609E
		internal OutputCacheProfile()
		{
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x0013366B File Offset: 0x0013186B
		public OutputCacheProfile(string name)
		{
			this.Name = name;
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x060057B6 RID: 22454 RVA: 0x0013367A File Offset: 0x0013187A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheProfile._properties;
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x060057B7 RID: 22455 RVA: 0x00133681 File Offset: 0x00131881
		// (set) Token: 0x060057B8 RID: 22456 RVA: 0x00133693 File Offset: 0x00131893
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[OutputCacheProfile._propName];
			}
			set
			{
				base[OutputCacheProfile._propName] = value;
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x060057B9 RID: 22457 RVA: 0x001336A1 File Offset: 0x001318A1
		// (set) Token: 0x060057BA RID: 22458 RVA: 0x001336B3 File Offset: 0x001318B3
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[OutputCacheProfile._propEnabled];
			}
			set
			{
				base[OutputCacheProfile._propEnabled] = value;
			}
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x060057BB RID: 22459 RVA: 0x001336C6 File Offset: 0x001318C6
		// (set) Token: 0x060057BC RID: 22460 RVA: 0x001336D8 File Offset: 0x001318D8
		[ConfigurationProperty("duration", DefaultValue = -1)]
		public int Duration
		{
			get
			{
				return (int)base[OutputCacheProfile._propDuration];
			}
			set
			{
				base[OutputCacheProfile._propDuration] = value;
			}
		}

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x060057BD RID: 22461 RVA: 0x001336EB File Offset: 0x001318EB
		// (set) Token: 0x060057BE RID: 22462 RVA: 0x001336FD File Offset: 0x001318FD
		[ConfigurationProperty("location")]
		public OutputCacheLocation Location
		{
			get
			{
				return (OutputCacheLocation)base[OutputCacheProfile._propLocation];
			}
			set
			{
				base[OutputCacheProfile._propLocation] = value;
			}
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x060057BF RID: 22463 RVA: 0x00133710 File Offset: 0x00131910
		// (set) Token: 0x060057C0 RID: 22464 RVA: 0x00133722 File Offset: 0x00131922
		[ConfigurationProperty("sqlDependency")]
		public string SqlDependency
		{
			get
			{
				return (string)base[OutputCacheProfile._propSqlDependency];
			}
			set
			{
				base[OutputCacheProfile._propSqlDependency] = value;
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x00133730 File Offset: 0x00131930
		// (set) Token: 0x060057C2 RID: 22466 RVA: 0x00133742 File Offset: 0x00131942
		[ConfigurationProperty("varyByCustom")]
		public string VaryByCustom
		{
			get
			{
				return (string)base[OutputCacheProfile._propVaryByCustom];
			}
			set
			{
				base[OutputCacheProfile._propVaryByCustom] = value;
			}
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x00133750 File Offset: 0x00131950
		// (set) Token: 0x060057C4 RID: 22468 RVA: 0x00133762 File Offset: 0x00131962
		[ConfigurationProperty("varyByControl")]
		public string VaryByControl
		{
			get
			{
				return (string)base[OutputCacheProfile._propVaryByControl];
			}
			set
			{
				base[OutputCacheProfile._propVaryByControl] = value;
			}
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x00133770 File Offset: 0x00131970
		// (set) Token: 0x060057C6 RID: 22470 RVA: 0x00133782 File Offset: 0x00131982
		[ConfigurationProperty("varyByContentEncoding")]
		public string VaryByContentEncoding
		{
			get
			{
				return (string)base[OutputCacheProfile._propVaryByContentEncoding];
			}
			set
			{
				base[OutputCacheProfile._propVaryByContentEncoding] = value;
			}
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x00133790 File Offset: 0x00131990
		// (set) Token: 0x060057C8 RID: 22472 RVA: 0x001337A2 File Offset: 0x001319A2
		[ConfigurationProperty("varyByHeader")]
		public string VaryByHeader
		{
			get
			{
				return (string)base[OutputCacheProfile._propVaryByHeader];
			}
			set
			{
				base[OutputCacheProfile._propVaryByHeader] = value;
			}
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x001337B0 File Offset: 0x001319B0
		// (set) Token: 0x060057CA RID: 22474 RVA: 0x001337C2 File Offset: 0x001319C2
		[ConfigurationProperty("varyByParam")]
		public string VaryByParam
		{
			get
			{
				return (string)base[OutputCacheProfile._propVaryByParam];
			}
			set
			{
				base[OutputCacheProfile._propVaryByParam] = value;
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x001337D0 File Offset: 0x001319D0
		// (set) Token: 0x060057CC RID: 22476 RVA: 0x001337E2 File Offset: 0x001319E2
		[ConfigurationProperty("noStore", DefaultValue = false)]
		public bool NoStore
		{
			get
			{
				return (bool)base[OutputCacheProfile._propNoStore];
			}
			set
			{
				base[OutputCacheProfile._propNoStore] = value;
			}
		}

		// Token: 0x04002E99 RID: 11929
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002E9A RID: 11930
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002E9B RID: 11931
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E9C RID: 11932
		private static readonly ConfigurationProperty _propDuration = new ConfigurationProperty("duration", typeof(int), -1, ConfigurationPropertyOptions.None);

		// Token: 0x04002E9D RID: 11933
		private static readonly ConfigurationProperty _propLocation = new ConfigurationProperty("location", typeof(OutputCacheLocation), (OutputCacheLocation)(-1), ConfigurationPropertyOptions.None);

		// Token: 0x04002E9E RID: 11934
		private static readonly ConfigurationProperty _propSqlDependency = new ConfigurationProperty("sqlDependency", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E9F RID: 11935
		private static readonly ConfigurationProperty _propVaryByCustom = new ConfigurationProperty("varyByCustom", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002EA0 RID: 11936
		private static readonly ConfigurationProperty _propVaryByContentEncoding;

		// Token: 0x04002EA1 RID: 11937
		private static readonly ConfigurationProperty _propVaryByHeader;

		// Token: 0x04002EA2 RID: 11938
		private static readonly ConfigurationProperty _propVaryByParam;

		// Token: 0x04002EA3 RID: 11939
		private static readonly ConfigurationProperty _propNoStore;

		// Token: 0x04002EA4 RID: 11940
		private static readonly ConfigurationProperty _propVaryByControl = new ConfigurationProperty("varyByControl", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}

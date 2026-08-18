using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000755 RID: 1877
	public sealed class SqlCacheDependencyDatabase : ConfigurationElement
	{
		// Token: 0x06005A7C RID: 23164 RVA: 0x0013B4D8 File Offset: 0x001396D8
		static SqlCacheDependencyDatabase()
		{
			SqlCacheDependencyDatabase._properties.Add(SqlCacheDependencyDatabase._propName);
			SqlCacheDependencyDatabase._properties.Add(SqlCacheDependencyDatabase._propConnectionStringName);
			SqlCacheDependencyDatabase._properties.Add(SqlCacheDependencyDatabase._propPollTime);
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x0013B5A7 File Offset: 0x001397A7
		public SqlCacheDependencyDatabase(string name, string connectionStringName, int pollTime)
		{
			this.Name = name;
			this.ConnectionStringName = connectionStringName;
			this.PollTime = pollTime;
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x0013B5C4 File Offset: 0x001397C4
		public SqlCacheDependencyDatabase(string name, string connectionStringName)
		{
			this.Name = name;
			this.ConnectionStringName = connectionStringName;
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x00117E9E File Offset: 0x0011609E
		internal SqlCacheDependencyDatabase()
		{
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x06005A80 RID: 23168 RVA: 0x0013B5DA File Offset: 0x001397DA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencyDatabase._properties;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06005A81 RID: 23169 RVA: 0x0013B5E1 File Offset: 0x001397E1
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencyDatabase.s_elemProperty;
			}
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x0013B5E8 File Offset: 0x001397E8
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("sqlCacheDependencyDatabase");
			}
			SqlCacheDependencyDatabase sqlCacheDependencyDatabase = (SqlCacheDependencyDatabase)value;
			if (sqlCacheDependencyDatabase.PollTime != 0 && sqlCacheDependencyDatabase.PollTime < 500)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_sql_cache_dep_polltime"), sqlCacheDependencyDatabase.ElementInformation.Properties["pollTime"].Source, sqlCacheDependencyDatabase.ElementInformation.Properties["pollTime"].LineNumber);
			}
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x0013B663 File Offset: 0x00139863
		internal void CheckDefaultPollTime(int value)
		{
			if (base.ElementInformation.Properties["pollTime"].ValueOrigin == PropertyValueOrigin.Default)
			{
				this.defaultPollTime = value;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06005A84 RID: 23172 RVA: 0x0013B688 File Offset: 0x00139888
		// (set) Token: 0x06005A85 RID: 23173 RVA: 0x0013B69A File Offset: 0x0013989A
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[SqlCacheDependencyDatabase._propName];
			}
			set
			{
				base[SqlCacheDependencyDatabase._propName] = value;
			}
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06005A86 RID: 23174 RVA: 0x0013B6A8 File Offset: 0x001398A8
		// (set) Token: 0x06005A87 RID: 23175 RVA: 0x0013B6BA File Offset: 0x001398BA
		[ConfigurationProperty("connectionStringName", IsRequired = true)]
		[StringValidator(MinLength = 1)]
		public string ConnectionStringName
		{
			get
			{
				return (string)base[SqlCacheDependencyDatabase._propConnectionStringName];
			}
			set
			{
				base[SqlCacheDependencyDatabase._propConnectionStringName] = value;
			}
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06005A88 RID: 23176 RVA: 0x0013B6C8 File Offset: 0x001398C8
		// (set) Token: 0x06005A89 RID: 23177 RVA: 0x0013B6FD File Offset: 0x001398FD
		[ConfigurationProperty("pollTime", DefaultValue = 60000)]
		public int PollTime
		{
			get
			{
				if (base.ElementInformation.Properties["pollTime"].ValueOrigin == PropertyValueOrigin.Default)
				{
					return this.defaultPollTime;
				}
				return (int)base[SqlCacheDependencyDatabase._propPollTime];
			}
			set
			{
				base[SqlCacheDependencyDatabase._propPollTime] = value;
			}
		}

		// Token: 0x04002FF3 RID: 12275
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencyDatabase), new ValidatorCallback(SqlCacheDependencyDatabase.Validate)));

		// Token: 0x04002FF4 RID: 12276
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002FF5 RID: 12277
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002FF6 RID: 12278
		private static readonly ConfigurationProperty _propConnectionStringName = new ConfigurationProperty("connectionStringName", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002FF7 RID: 12279
		private static readonly ConfigurationProperty _propPollTime = new ConfigurationProperty("pollTime", typeof(int), 60000, ConfigurationPropertyOptions.None);

		// Token: 0x04002FF8 RID: 12280
		private int defaultPollTime;
	}
}

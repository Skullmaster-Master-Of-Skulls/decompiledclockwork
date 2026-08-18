using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200024B RID: 587
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SqlCacheDependencyDatabase : ConfigurationElement
	{
		// Token: 0x06001F0E RID: 7950 RVA: 0x0008A6C4 File Offset: 0x000896C4
		static SqlCacheDependencyDatabase()
		{
			SqlCacheDependencyDatabase._properties.Add(SqlCacheDependencyDatabase._propName);
			SqlCacheDependencyDatabase._properties.Add(SqlCacheDependencyDatabase._propConnectionStringName);
			SqlCacheDependencyDatabase._properties.Add(SqlCacheDependencyDatabase._propPollTime);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0008A793 File Offset: 0x00089793
		public SqlCacheDependencyDatabase(string name, string connectionStringName, int pollTime)
		{
			this.Name = name;
			this.ConnectionStringName = connectionStringName;
			this.PollTime = pollTime;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0008A7B0 File Offset: 0x000897B0
		public SqlCacheDependencyDatabase(string name, string connectionStringName)
		{
			this.Name = name;
			this.ConnectionStringName = connectionStringName;
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0008A7C6 File Offset: 0x000897C6
		internal SqlCacheDependencyDatabase()
		{
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0008A7CE File Offset: 0x000897CE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencyDatabase._properties;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0008A7D5 File Offset: 0x000897D5
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencyDatabase.s_elemProperty;
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0008A7DC File Offset: 0x000897DC
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

		// Token: 0x06001F15 RID: 7957 RVA: 0x0008A857 File Offset: 0x00089857
		internal void CheckDefaultPollTime(int value)
		{
			if (base.ElementInformation.Properties["pollTime"].ValueOrigin == PropertyValueOrigin.Default)
			{
				this.defaultPollTime = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0008A87C File Offset: 0x0008987C
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x0008A88E File Offset: 0x0008988E
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
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

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0008A89C File Offset: 0x0008989C
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x0008A8AE File Offset: 0x000898AE
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

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0008A8BC File Offset: 0x000898BC
		// (set) Token: 0x06001F1B RID: 7963 RVA: 0x0008A8F1 File Offset: 0x000898F1
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

		// Token: 0x04001A3C RID: 6716
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencyDatabase), new ValidatorCallback(SqlCacheDependencyDatabase.Validate)));

		// Token: 0x04001A3D RID: 6717
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A3E RID: 6718
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001A3F RID: 6719
		private static readonly ConfigurationProperty _propConnectionStringName = new ConfigurationProperty("connectionStringName", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04001A40 RID: 6720
		private static readonly ConfigurationProperty _propPollTime = new ConfigurationProperty("pollTime", typeof(int), 60000, ConfigurationPropertyOptions.None);

		// Token: 0x04001A41 RID: 6721
		private int defaultPollTime;
	}
}

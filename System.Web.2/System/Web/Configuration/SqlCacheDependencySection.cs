using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000757 RID: 1879
	public sealed class SqlCacheDependencySection : ConfigurationSection
	{
		// Token: 0x06005A9A RID: 23194 RVA: 0x0013B74C File Offset: 0x0013994C
		static SqlCacheDependencySection()
		{
			SqlCacheDependencySection._properties.Add(SqlCacheDependencySection._propEnabled);
			SqlCacheDependencySection._properties.Add(SqlCacheDependencySection._propPollTime);
			SqlCacheDependencySection._properties.Add(SqlCacheDependencySection._propDatabases);
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06005A9C RID: 23196 RVA: 0x0013B814 File Offset: 0x00139A14
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencySection._properties;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x0013B81B File Offset: 0x00139A1B
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencySection.s_elemProperty;
			}
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x0013B824 File Offset: 0x00139A24
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("sqlCacheDependency");
			}
			SqlCacheDependencySection sqlCacheDependencySection = (SqlCacheDependencySection)value;
			int pollTime = sqlCacheDependencySection.PollTime;
			if (pollTime != 0 && pollTime < 500)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_sql_cache_dep_polltime"), sqlCacheDependencySection.ElementInformation.Properties["pollTime"].Source, sqlCacheDependencySection.ElementInformation.Properties["pollTime"].LineNumber);
			}
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x0013B89C File Offset: 0x00139A9C
		protected override void PostDeserialize()
		{
			int pollTime = this.PollTime;
			foreach (object obj in this.Databases)
			{
				SqlCacheDependencyDatabase sqlCacheDependencyDatabase = (SqlCacheDependencyDatabase)obj;
				sqlCacheDependencyDatabase.CheckDefaultPollTime(pollTime);
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x0013B8FC File Offset: 0x00139AFC
		// (set) Token: 0x06005AA1 RID: 23201 RVA: 0x0013B90E File Offset: 0x00139B0E
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[SqlCacheDependencySection._propEnabled];
			}
			set
			{
				base[SqlCacheDependencySection._propEnabled] = value;
			}
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x0013B921 File Offset: 0x00139B21
		// (set) Token: 0x06005AA3 RID: 23203 RVA: 0x0013B933 File Offset: 0x00139B33
		[ConfigurationProperty("pollTime", DefaultValue = 60000)]
		public int PollTime
		{
			get
			{
				return (int)base[SqlCacheDependencySection._propPollTime];
			}
			set
			{
				base[SqlCacheDependencySection._propPollTime] = value;
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x0013B946 File Offset: 0x00139B46
		[ConfigurationProperty("databases")]
		public SqlCacheDependencyDatabaseCollection Databases
		{
			get
			{
				return (SqlCacheDependencyDatabaseCollection)base[SqlCacheDependencySection._propDatabases];
			}
		}

		// Token: 0x04002FFA RID: 12282
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencySection), new ValidatorCallback(SqlCacheDependencySection.Validate)));

		// Token: 0x04002FFB RID: 12283
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002FFC RID: 12284
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002FFD RID: 12285
		private static readonly ConfigurationProperty _propPollTime = new ConfigurationProperty("pollTime", typeof(int), 60000, ConfigurationPropertyOptions.None);

		// Token: 0x04002FFE RID: 12286
		private static readonly ConfigurationProperty _propDatabases = new ConfigurationProperty("databases", typeof(SqlCacheDependencyDatabaseCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

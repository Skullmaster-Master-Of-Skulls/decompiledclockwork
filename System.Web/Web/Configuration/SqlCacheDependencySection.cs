using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200024D RID: 589
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SqlCacheDependencySection : ConfigurationSection
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x0008A9C8 File Offset: 0x000899C8
		static SqlCacheDependencySection()
		{
			SqlCacheDependencySection._properties.Add(SqlCacheDependencySection._propEnabled);
			SqlCacheDependencySection._properties.Add(SqlCacheDependencySection._propPollTime);
			SqlCacheDependencySection._properties.Add(SqlCacheDependencySection._propDatabases);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0008AA98 File Offset: 0x00089A98
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencySection._properties;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0008AA9F File Offset: 0x00089A9F
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencySection.s_elemProperty;
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0008AAA8 File Offset: 0x00089AA8
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

		// Token: 0x06001F31 RID: 7985 RVA: 0x0008AB20 File Offset: 0x00089B20
		protected override void PostDeserialize()
		{
			int pollTime = this.PollTime;
			foreach (object obj in this.Databases)
			{
				SqlCacheDependencyDatabase sqlCacheDependencyDatabase = (SqlCacheDependencyDatabase)obj;
				sqlCacheDependencyDatabase.CheckDefaultPollTime(pollTime);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0008AB80 File Offset: 0x00089B80
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x0008AB92 File Offset: 0x00089B92
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

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0008ABA5 File Offset: 0x00089BA5
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x0008ABB7 File Offset: 0x00089BB7
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

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0008ABCA File Offset: 0x00089BCA
		[ConfigurationProperty("databases")]
		public SqlCacheDependencyDatabaseCollection Databases
		{
			get
			{
				return (SqlCacheDependencyDatabaseCollection)base[SqlCacheDependencySection._propDatabases];
			}
		}

		// Token: 0x04001A43 RID: 6723
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencySection), new ValidatorCallback(SqlCacheDependencySection.Validate)));

		// Token: 0x04001A44 RID: 6724
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A45 RID: 6725
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001A46 RID: 6726
		private static readonly ConfigurationProperty _propPollTime = new ConfigurationProperty("pollTime", typeof(int), 60000, ConfigurationPropertyOptions.None);

		// Token: 0x04001A47 RID: 6727
		private static readonly ConfigurationProperty _propDatabases = new ConfigurationProperty("databases", typeof(SqlCacheDependencyDatabaseCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006BC RID: 1724
	internal class ContextElement : ConfigurationElement
	{
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004499 RID: 17561 RVA: 0x00144531 File Offset: 0x00142731
		// (set) Token: 0x0600449A RID: 17562 RVA: 0x00144543 File Offset: 0x00142743
		[ConfigurationProperty("type", IsRequired = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public virtual string ContextTypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x00144551 File Offset: 0x00142751
		// (set) Token: 0x0600449C RID: 17564 RVA: 0x00144563 File Offset: 0x00142763
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[ConfigurationProperty("disableDatabaseInitialization", DefaultValue = false)]
		public virtual bool IsDatabaseInitializationDisabled
		{
			get
			{
				return (bool)base["disableDatabaseInitialization"];
			}
			set
			{
				base["disableDatabaseInitialization"] = value;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x00144576 File Offset: 0x00142776
		// (set) Token: 0x0600449E RID: 17566 RVA: 0x00144588 File Offset: 0x00142788
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[ConfigurationProperty("databaseInitializer")]
		public virtual DatabaseInitializerElement DatabaseInitializer
		{
			get
			{
				return (DatabaseInitializerElement)base["databaseInitializer"];
			}
			set
			{
				base["databaseInitializer"] = value;
			}
		}

		// Token: 0x04001943 RID: 6467
		private const string TypeKey = "type";

		// Token: 0x04001944 RID: 6468
		private const string DisableDatabaseInitializationKey = "disableDatabaseInitialization";

		// Token: 0x04001945 RID: 6469
		private const string DatabaseInitializerKey = "databaseInitializer";
	}
}

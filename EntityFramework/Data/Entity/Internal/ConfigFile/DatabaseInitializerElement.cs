using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006B8 RID: 1720
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class DatabaseInitializerElement : ConfigurationElement
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x00144344 File Offset: 0x00142544
		// (set) Token: 0x0600447F RID: 17535 RVA: 0x00144356 File Offset: 0x00142556
		[ConfigurationProperty("type", IsRequired = true)]
		public virtual string InitializerTypeName
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

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x00144364 File Offset: 0x00142564
		[ConfigurationProperty("parameters")]
		public virtual ParameterCollection Parameters
		{
			get
			{
				return (ParameterCollection)base["parameters"];
			}
		}

		// Token: 0x0400193A RID: 6458
		private const string TypeKey = "type";

		// Token: 0x0400193B RID: 6459
		private const string ParametersKey = "parameters";
	}
}

using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006BD RID: 1725
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class DefaultConnectionFactoryElement : ConfigurationElement
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x0014459E File Offset: 0x0014279E
		// (set) Token: 0x060044A1 RID: 17569 RVA: 0x001445B0 File Offset: 0x001427B0
		[ConfigurationProperty("type", IsRequired = true)]
		public string FactoryTypeName
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x001445BE File Offset: 0x001427BE
		[ConfigurationProperty("parameters")]
		public ParameterCollection Parameters
		{
			get
			{
				return (ParameterCollection)base["parameters"];
			}
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x001445D0 File Offset: 0x001427D0
		public Type GetFactoryType()
		{
			return Type.GetType(this.FactoryTypeName, true);
		}

		// Token: 0x04001946 RID: 6470
		private const string TypeKey = "type";

		// Token: 0x04001947 RID: 6471
		private const string ParametersKey = "parameters";
	}
}

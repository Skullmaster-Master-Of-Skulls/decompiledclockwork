using System;

namespace OracleInternal.Common
{
	// Token: 0x02000082 RID: 130
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	internal sealed class ConfigurationAttribute : Attribute
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0003A4B8 File Offset: 0x000386B8
		internal ConfigurationAttribute(string configEntry)
		{
			this.m_configEntry = configEntry;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0003A4D4 File Offset: 0x000386D4
		internal string ConfigEntry
		{
			get
			{
				return this.m_configEntry;
			}
		}

		// Token: 0x04000790 RID: 1936
		private string m_configEntry = "";
	}
}

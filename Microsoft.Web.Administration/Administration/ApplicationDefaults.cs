using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000007 RID: 7
	public sealed class ApplicationDefaults : ConfigurationElement
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000351A File Offset: 0x0000251A
		internal ApplicationDefaults() : this(null)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003523 File Offset: 0x00002523
		internal ApplicationDefaults(ApplicationDefaults parentDefaults)
		{
			this._parentDefaults = parentDefaults;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003532 File Offset: 0x00002532
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003544 File Offset: 0x00002544
		public string ApplicationPoolName
		{
			get
			{
				return (string)this.GetValue("applicationPool");
			}
			set
			{
				base["applicationPool"] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003552 File Offset: 0x00002552
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003564 File Offset: 0x00002564
		public string EnabledProtocols
		{
			get
			{
				return (string)this.GetValue("enabledProtocols");
			}
			set
			{
				base["enabledProtocols"] = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003574 File Offset: 0x00002574
		private object GetValue(string attributeName)
		{
			ConfigurationAttribute attribute = base.GetAttribute(attributeName);
			if (this._parentDefaults != null && attribute.IsInheritedFromDefaultValue)
			{
				return this._parentDefaults.GetValue(attributeName);
			}
			return attribute.Value;
		}

		// Token: 0x04000019 RID: 25
		private ApplicationDefaults _parentDefaults;
	}
}

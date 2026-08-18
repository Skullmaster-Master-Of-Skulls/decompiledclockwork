using System;

namespace System.Configuration
{
	// Token: 0x0200003F RID: 63
	internal class ConfigurationValue
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00012131 File Offset: 0x00010331
		internal ConfigurationValue(object value, ConfigurationValueFlags valueFlags, PropertySourceInfo sourceInfo)
		{
			this.Value = value;
			this.ValueFlags = valueFlags;
			this.SourceInfo = sourceInfo;
		}

		// Token: 0x0400021D RID: 541
		internal ConfigurationValueFlags ValueFlags;

		// Token: 0x0400021E RID: 542
		internal object Value;

		// Token: 0x0400021F RID: 543
		internal PropertySourceInfo SourceInfo;
	}
}

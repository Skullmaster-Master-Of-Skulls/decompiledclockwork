using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006EB RID: 1771
	internal struct StoredSetting
	{
		// Token: 0x060036CD RID: 14029 RVA: 0x000E9BDD File Offset: 0x000E8BDD
		internal StoredSetting(SettingsSerializeAs serializeAs, XmlNode value)
		{
			this.SerializeAs = serializeAs;
			this.Value = value;
		}

		// Token: 0x040031A9 RID: 12713
		internal SettingsSerializeAs SerializeAs;

		// Token: 0x040031AA RID: 12714
		internal XmlNode Value;
	}
}

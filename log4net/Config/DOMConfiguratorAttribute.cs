using System;

namespace log4net.Config
{
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.Assembly)]
	[Obsolete("Use XmlConfiguratorAttribute instead of DOMConfiguratorAttribute")]
	[Serializable]
	public sealed class DOMConfiguratorAttribute : XmlConfiguratorAttribute
	{
	}
}

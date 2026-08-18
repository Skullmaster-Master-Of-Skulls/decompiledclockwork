using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000002 RID: 2
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserializable", Justification = "It is spelled correctly")]
	public abstract class DeserializableConfigurationElement : ConfigurationElement
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public virtual void Deserialize(XmlReader reader)
		{
			this.DeserializeElement(reader, false);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DA File Offset: 0x000002DA
		public virtual void SerializeContent(XmlWriter writer)
		{
		}
	}
}

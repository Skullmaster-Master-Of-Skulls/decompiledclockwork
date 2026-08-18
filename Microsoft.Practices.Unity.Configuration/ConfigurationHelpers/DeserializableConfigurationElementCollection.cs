using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000005 RID: 5
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserializable", Justification = "It is spelled correctly.")]
	public abstract class DeserializableConfigurationElementCollection<TElement> : DeserializableConfigurationElementCollectionBase<TElement> where TElement : DeserializableConfigurationElement, new()
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002320 File Offset: 0x00000520
		protected override ConfigurationElement CreateNewElement()
		{
			return Activator.CreateInstance<TElement>();
		}
	}
}

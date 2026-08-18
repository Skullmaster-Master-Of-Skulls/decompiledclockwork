using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000039 RID: 57
	public abstract class SectionExtensionContext
	{
		// Token: 0x060001CB RID: 459
		public abstract void AddAlias(string newAlias, Type aliasedType);

		// Token: 0x060001CC RID: 460 RVA: 0x00006529 File Offset: 0x00004729
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "TAliased is required to do the mapping between the alias and the type")]
		public void AddAlias<TAliased>(string alias)
		{
			this.AddAlias(alias, typeof(TAliased));
		}

		// Token: 0x060001CD RID: 461
		public abstract void AddElement(string tag, Type elementType);

		// Token: 0x060001CE RID: 462 RVA: 0x0000653C File Offset: 0x0000473C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "TElement is required to map the tag to the element type")]
		public void AddElement<TElement>(string tag) where TElement : DeserializableConfigurationElement
		{
			this.AddElement(tag, typeof(TElement));
		}
	}
}

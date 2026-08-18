using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000627 RID: 1575
	public abstract class MexBindingBindingCollectionElement<TStandardBinding, TBindingConfiguration> : StandardBindingCollectionElement<TStandardBinding, TBindingConfiguration> where TStandardBinding : Binding where TBindingConfiguration : StandardBindingElement, new()
	{
		// Token: 0x06003C73 RID: 15475 RVA: 0x000E6D86 File Offset: 0x000E4F86
		protected internal override bool TryAdd(string name, Binding binding, Configuration config)
		{
			return false;
		}
	}
}

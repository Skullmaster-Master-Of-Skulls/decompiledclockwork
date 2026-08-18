using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200001D RID: 29
	public interface ILifetimePolicy : IBuilderPolicy
	{
		// Token: 0x06000064 RID: 100
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This operation could be slow, we want a method.")]
		object GetValue();

		// Token: 0x06000065 RID: 101
		void SetValue(object newValue);

		// Token: 0x06000066 RID: 102
		void RemoveValue();
	}
}

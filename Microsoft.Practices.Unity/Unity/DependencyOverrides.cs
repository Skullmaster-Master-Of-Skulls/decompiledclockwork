using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000013 RID: 19
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not really a collection, only implements IEnumerable to get convenient initialization syntax.")]
	public class DependencyOverrides : OverrideCollection<DependencyOverride, Type, object>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000029DF File Offset: 0x00000BDF
		protected override DependencyOverride MakeOverride(Type key, object value)
		{
			return new DependencyOverride(key, value);
		}
	}
}

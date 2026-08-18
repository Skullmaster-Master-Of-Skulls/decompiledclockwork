using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000032 RID: 50
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not really a collection, only implements IEnumerable to get convenient initialization syntax.")]
	public class ParameterOverrides : OverrideCollection<ParameterOverride, string, object>
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x000037E0 File Offset: 0x000019E0
		protected override ParameterOverride MakeOverride(string key, object value)
		{
			return new ParameterOverride(key, value);
		}
	}
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000034 RID: 52
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not really a collection, only implements IEnumerable to get convenient initialization syntax.")]
	public class PropertyOverrides : OverrideCollection<PropertyOverride, string, object>
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003854 File Offset: 0x00001A54
		protected override PropertyOverride MakeOverride(string key, object value)
		{
			return new PropertyOverride(key, value);
		}
	}
}

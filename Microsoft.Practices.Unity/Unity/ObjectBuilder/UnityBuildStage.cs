using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x020000A2 RID: 162
	public enum UnityBuildStage
	{
		// Token: 0x040000A4 RID: 164
		Setup,
		// Token: 0x040000A5 RID: 165
		TypeMapping,
		// Token: 0x040000A6 RID: 166
		Lifetime,
		// Token: 0x040000A7 RID: 167
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PreCreation", Justification = "Kept for backward compatibility with ObjectBuilder")]
		PreCreation,
		// Token: 0x040000A8 RID: 168
		Creation,
		// Token: 0x040000A9 RID: 169
		Initialization,
		// Token: 0x040000AA RID: 170
		PostInitialization
	}
}

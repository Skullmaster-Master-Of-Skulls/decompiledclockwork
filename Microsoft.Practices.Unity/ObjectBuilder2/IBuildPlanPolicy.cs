using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200002B RID: 43
	public interface IBuildPlanPolicy : IBuilderPolicy
	{
		// Token: 0x060000A4 RID: 164
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "Kept for backward compatibility with ObjectBuilder")]
		void BuildUp(IBuilderContext context);
	}
}

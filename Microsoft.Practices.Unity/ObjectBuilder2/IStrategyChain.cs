using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200006F RID: 111
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This class is only IEnumerable for testing support. Renaming it to StrategyCollection implies more than it really should.")]
	public interface IStrategyChain : IEnumerable<IBuilderStrategy>, IEnumerable
	{
		// Token: 0x060001CD RID: 461
		IStrategyChain Reverse();

		// Token: 0x060001CE RID: 462
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "Backwards compatibility with ObjectBuilder")]
		object ExecuteBuildUp(IBuilderContext context);

		// Token: 0x060001CF RID: 463
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TearDown", Justification = "Backwards compatibility with ObjectBuilder")]
		void ExecuteTearDown(IBuilderContext context);
	}
}

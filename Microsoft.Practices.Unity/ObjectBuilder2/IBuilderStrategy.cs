using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000024 RID: 36
	public interface IBuilderStrategy
	{
		// Token: 0x06000082 RID: 130
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", Justification = "Kept for backward compatibility with ObjectBuilder")]
		void PreBuildUp(IBuilderContext context);

		// Token: 0x06000083 RID: 131
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "Kept for backward compatibility with ObjectBuilder")]
		void PostBuildUp(IBuilderContext context);

		// Token: 0x06000084 RID: 132
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TearDown", Justification = "Kept for backward compatibility with ObjectBuilder")]
		void PreTearDown(IBuilderContext context);

		// Token: 0x06000085 RID: 133
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TearDown", Justification = "Kept for backward compatibility with ObjectBuilder")]
		void PostTearDown(IBuilderContext context);
	}
}

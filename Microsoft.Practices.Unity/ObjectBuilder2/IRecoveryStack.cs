using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200003C RID: 60
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "The name ends in stack becuase the semantics are a stack, and we want that to be obvious to users")]
	public interface IRecoveryStack
	{
		// Token: 0x060000F5 RID: 245
		void Add(IRequiresRecovery recovery);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F6 RID: 246
		int Count { get; }

		// Token: 0x060000F7 RID: 247
		void ExecuteRecovery();
	}
}

using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200009B RID: 155
	public interface INameReference
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000951 RID: 2385
		ActivationObject VariableScope { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000952 RID: 2386
		bool IsAssignment { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000953 RID: 2387
		AstNode AssignmentValue { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000954 RID: 2388
		JSVariableField VariableField { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000955 RID: 2389
		string Name { get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000956 RID: 2390
		long Index { get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000957 RID: 2391
		AstNode Parent { get; }
	}
}

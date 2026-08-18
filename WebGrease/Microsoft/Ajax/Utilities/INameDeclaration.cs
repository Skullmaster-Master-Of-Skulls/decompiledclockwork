using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000006 RID: 6
	public interface INameDeclaration
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54
		string Name { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000037 RID: 55
		Context Context { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56
		AstNode Parent { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000039 RID: 57
		AstNode Initializer { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003A RID: 58
		bool IsParameter { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003B RID: 59
		bool RenameNotAllowed { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60
		// (set) Token: 0x0600003D RID: 61
		JSVariableField VariableField { get; set; }
	}
}

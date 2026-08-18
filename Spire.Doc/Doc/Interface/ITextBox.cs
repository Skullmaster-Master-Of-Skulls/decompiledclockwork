using System;
using Spire.Doc.Formatting;

namespace Spire.Doc.Interface
{
	// Token: 0x02000508 RID: 1288
	public interface ITextBox : IParagraphBase, ICompositeObject
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06004257 RID: 16983
		Body Body { get; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06004258 RID: 16984
		// (set) Token: 0x06004259 RID: 16985
		TextBoxFormat Format { get; set; }
	}
}

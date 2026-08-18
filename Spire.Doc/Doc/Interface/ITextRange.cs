using System;
using Spire.Doc.Formatting;

namespace Spire.Doc.Interface
{
	// Token: 0x020004FE RID: 1278
	public interface ITextRange : IParagraphBase
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06004212 RID: 16914
		// (set) Token: 0x06004213 RID: 16915
		string Text { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06004214 RID: 16916
		CharacterFormat CharacterFormat { get; }

		// Token: 0x06004215 RID: 16917
		void ApplyCharacterFormat(CharacterFormat charFormat);
	}
}

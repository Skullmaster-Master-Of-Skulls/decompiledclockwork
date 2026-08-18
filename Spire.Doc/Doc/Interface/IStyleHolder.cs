using System;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x02000505 RID: 1285
	public interface IStyleHolder
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06004251 RID: 16977
		string StyleName { get; }

		// Token: 0x06004252 RID: 16978
		void ApplyStyle(string styleName);

		// Token: 0x06004253 RID: 16979
		void ApplyStyle(BuiltinStyle builtinStyle);
	}
}

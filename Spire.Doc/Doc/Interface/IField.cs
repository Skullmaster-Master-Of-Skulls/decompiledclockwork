using System;

namespace Spire.Doc.Interface
{
	// Token: 0x02000506 RID: 1286
	public interface IField : ITextRange
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06004254 RID: 16980
		// (set) Token: 0x06004255 RID: 16981
		FieldType Type { get; set; }
	}
}

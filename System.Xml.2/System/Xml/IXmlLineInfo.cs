using System;

namespace System.Xml
{
	// Token: 0x02000071 RID: 113
	[__DynamicallyInvokable]
	public interface IXmlLineInfo
	{
		// Token: 0x060003D1 RID: 977
		[__DynamicallyInvokable]
		bool HasLineInfo();

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003D2 RID: 978
		[__DynamicallyInvokable]
		int LineNumber { [__DynamicallyInvokable] get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003D3 RID: 979
		[__DynamicallyInvokable]
		int LinePosition { [__DynamicallyInvokable] get; }
	}
}

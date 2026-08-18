using System;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls.Core.Interfaces
{
	// Token: 0x0200000A RID: 10
	public interface IInternalFont : IFont
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000071 RID: 113
		int Index { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000072 RID: 114
		XlsFont Font { get; }
	}
}

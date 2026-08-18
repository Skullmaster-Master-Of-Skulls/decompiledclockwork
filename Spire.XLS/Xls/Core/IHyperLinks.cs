using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001ED RID: 493
	public interface IHyperLinks : IExcelApplication
	{
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001C2C RID: 7212
		int Count { get; }

		// Token: 0x17000A7B RID: 2683
		IHyperLink this[int index]
		{
			get;
		}

		// Token: 0x06001C2E RID: 7214
		IHyperLink Add(IXLSRange range);

		// Token: 0x06001C2F RID: 7215
		void RemoveAt(int index);
	}
}

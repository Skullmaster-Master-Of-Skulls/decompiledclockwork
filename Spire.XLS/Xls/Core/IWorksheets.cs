using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001E5 RID: 485
	public interface IWorksheets : IEnumerable
	{
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001BAA RID: 7082
		int Count { get; }

		// Token: 0x17000A59 RID: 2649
		IWorksheet this[int Index]
		{
			get;
		}

		// Token: 0x17000A5A RID: 2650
		IWorksheet this[string sheetName]
		{
			get;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001BAD RID: 7085
		object Parent { get; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001BAE RID: 7086
		// (set) Token: 0x06001BAF RID: 7087
		bool UseRangesCache { get; set; }

		// Token: 0x06001BB0 RID: 7088
		IWorksheet Create(string name);

		// Token: 0x06001BB1 RID: 7089
		IWorksheet Create();

		// Token: 0x06001BB2 RID: 7090
		void Remove(IWorksheet sheet);

		// Token: 0x06001BB3 RID: 7091
		void Remove(string sheetName);

		// Token: 0x06001BB4 RID: 7092
		void Remove(int index);

		// Token: 0x06001BB5 RID: 7093
		IWorksheet AddCopyBefore(IWorksheet toCopy);

		// Token: 0x06001BB6 RID: 7094
		IWorksheet AddCopyBefore(IWorksheet toCopy, IWorksheet sheetAfter);

		// Token: 0x06001BB7 RID: 7095
		IWorksheet AddCopyAfter(IWorksheet toCopy);

		// Token: 0x06001BB8 RID: 7096
		IWorksheet AddCopyAfter(IWorksheet toCopy, IWorksheet sheetBefore);
	}
}

using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200034F RID: 847
	public interface ISortedRule
	{
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003380 RID: 13184
		// (set) Token: 0x06003381 RID: 13185
		IXLSRange Range { get; set; }

		// Token: 0x06003382 RID: 13186
		void SortInt(int left, int right, int columnIndex);

		// Token: 0x06003383 RID: 13187
		void SortFloat(int left, int right, int columnIndex);

		// Token: 0x06003384 RID: 13188
		void SortDate(int left, int right, int columnIndex);

		// Token: 0x06003385 RID: 13189
		void SortString(int left, int right, int columnIndex);

		// Token: 0x06003386 RID: 13190
		void SortOnTypes(int left, int right, int columnIndex);

		// Token: 0x06003387 RID: 13191
		void SortIntDesc(int left, int right, int columnIndex);

		// Token: 0x06003388 RID: 13192
		void SortFloatDesc(int left, int right, int columnIndex);

		// Token: 0x06003389 RID: 13193
		void SortDateDesc(int left, int right, int columnIndex);

		// Token: 0x0600338A RID: 13194
		void SortStringDesc(int left, int right, int columnIndex);
	}
}

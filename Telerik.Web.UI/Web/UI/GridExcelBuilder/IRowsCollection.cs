using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B01 RID: 6913
	public interface IRowsCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010B96 RID: 68502
		int Add(RowElement value);

		// Token: 0x06010B97 RID: 68503
		bool Contains(RowElement value);

		// Token: 0x06010B98 RID: 68504
		void CopyTo(RowElement[] array, int index);

		// Token: 0x06010B99 RID: 68505
		int IndexOf(RowElement value);

		// Token: 0x06010B9A RID: 68506
		void Insert(int index, RowElement value);

		// Token: 0x06010B9B RID: 68507
		void Remove(RowElement value);

		// Token: 0x17005159 RID: 20825
		RowElement this[int index]
		{
			get;
			set;
		}
	}
}

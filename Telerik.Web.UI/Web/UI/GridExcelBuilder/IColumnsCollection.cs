using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001AFD RID: 6909
	public interface IColumnsCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010B5F RID: 68447
		int Add(ColumnElement value);

		// Token: 0x06010B60 RID: 68448
		bool Contains(ColumnElement value);

		// Token: 0x06010B61 RID: 68449
		void CopyTo(ColumnElement[] array, int index);

		// Token: 0x06010B62 RID: 68450
		int IndexOf(ColumnElement value);

		// Token: 0x06010B63 RID: 68451
		void Insert(int index, ColumnElement value);

		// Token: 0x06010B64 RID: 68452
		void Remove(ColumnElement value);

		// Token: 0x17005149 RID: 20809
		ColumnElement this[int index]
		{
			get;
			set;
		}
	}
}

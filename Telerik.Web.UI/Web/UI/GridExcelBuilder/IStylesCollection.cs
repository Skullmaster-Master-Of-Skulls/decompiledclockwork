using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B02 RID: 6914
	public interface IStylesCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010B9E RID: 68510
		int Add(StyleElement value);

		// Token: 0x06010B9F RID: 68511
		bool Contains(StyleElement value);

		// Token: 0x06010BA0 RID: 68512
		void CopyTo(StyleElement[] array, int index);

		// Token: 0x06010BA1 RID: 68513
		int IndexOf(StyleElement value);

		// Token: 0x06010BA2 RID: 68514
		void Insert(int index, StyleElement value);

		// Token: 0x06010BA3 RID: 68515
		void Remove(StyleElement value);

		// Token: 0x1700515A RID: 20826
		StyleElement this[int index]
		{
			get;
			set;
		}
	}
}

using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder.Abstract
{
	// Token: 0x02001B03 RID: 6915
	public interface IWorksheetCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010BA6 RID: 68518
		int Add(WorksheetElement value);

		// Token: 0x06010BA7 RID: 68519
		bool Contains(WorksheetElement value);

		// Token: 0x06010BA8 RID: 68520
		int IndexOf(WorksheetElement value);

		// Token: 0x06010BA9 RID: 68521
		void Insert(int index, WorksheetElement value);

		// Token: 0x06010BAA RID: 68522
		void Remove(WorksheetElement value);

		// Token: 0x1700515B RID: 20827
		WorksheetElement this[int index]
		{
			get;
			set;
		}

		// Token: 0x06010BAD RID: 68525
		void CopyTo(WorksheetElement[] array, int index);
	}
}

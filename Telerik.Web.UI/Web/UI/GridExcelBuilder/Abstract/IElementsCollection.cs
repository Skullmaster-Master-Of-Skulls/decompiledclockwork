using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder.Abstract
{
	// Token: 0x02001AFE RID: 6910
	public interface IElementsCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010B67 RID: 68455
		int Add(IElement value);

		// Token: 0x06010B68 RID: 68456
		bool Contains(IElement value);

		// Token: 0x06010B69 RID: 68457
		int IndexOf(IElement value);

		// Token: 0x06010B6A RID: 68458
		void Insert(int index, IElement value);

		// Token: 0x06010B6B RID: 68459
		void Remove(IElement value);

		// Token: 0x1700514A RID: 20810
		IElement this[int index]
		{
			get;
			set;
		}
	}
}

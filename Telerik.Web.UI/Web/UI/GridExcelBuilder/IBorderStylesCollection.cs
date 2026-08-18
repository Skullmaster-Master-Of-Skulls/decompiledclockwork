using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001AFC RID: 6908
	public interface IBorderStylesCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010B57 RID: 68439
		int Add(BorderStyles value);

		// Token: 0x06010B58 RID: 68440
		bool Contains(BorderStyles value);

		// Token: 0x06010B59 RID: 68441
		void CopyTo(BorderStyles[] array, int index);

		// Token: 0x06010B5A RID: 68442
		int IndexOf(BorderStyles value);

		// Token: 0x06010B5B RID: 68443
		void Insert(int index, BorderStyles value);

		// Token: 0x06010B5C RID: 68444
		void Remove(BorderStyles value);

		// Token: 0x17005148 RID: 20808
		BorderStyles this[int index]
		{
			get;
			set;
		}
	}
}

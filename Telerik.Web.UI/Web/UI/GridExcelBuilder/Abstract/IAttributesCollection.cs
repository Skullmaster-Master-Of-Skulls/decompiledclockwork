using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder.Abstract
{
	// Token: 0x02001AFB RID: 6907
	public interface IAttributesCollection : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06010B51 RID: 68433
		void Add(string key, string value);

		// Token: 0x06010B52 RID: 68434
		bool Contains(string key);

		// Token: 0x06010B53 RID: 68435
		void Remove(string key);

		// Token: 0x17005147 RID: 20807
		string this[string key]
		{
			get;
			set;
		}

		// Token: 0x06010B56 RID: 68438
		void CopyTo(string[] array, int index);
	}
}

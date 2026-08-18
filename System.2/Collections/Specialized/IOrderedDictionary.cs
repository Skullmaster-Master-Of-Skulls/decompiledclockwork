using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003AC RID: 940
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170008E7 RID: 2279
		object this[int index]
		{
			get;
			set;
		}

		// Token: 0x0600231E RID: 8990
		IDictionaryEnumerator GetEnumerator();

		// Token: 0x0600231F RID: 8991
		void Insert(int index, object key, object value);

		// Token: 0x06002320 RID: 8992
		void RemoveAt(int index);
	}
}

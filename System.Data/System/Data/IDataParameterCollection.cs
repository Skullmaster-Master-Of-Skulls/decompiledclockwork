using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000BB RID: 187
	public interface IDataParameterCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170001D1 RID: 465
		object this[string parameterName]
		{
			get;
			set;
		}

		// Token: 0x06000C6F RID: 3183
		bool Contains(string parameterName);

		// Token: 0x06000C70 RID: 3184
		int IndexOf(string parameterName);

		// Token: 0x06000C71 RID: 3185
		void RemoveAt(string parameterName);
	}
}

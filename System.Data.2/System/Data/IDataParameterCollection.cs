using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000102 RID: 258
	public interface IDataParameterCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700026D RID: 621
		object this[string parameterName]
		{
			get;
			set;
		}

		// Token: 0x0600107A RID: 4218
		bool Contains(string parameterName);

		// Token: 0x0600107B RID: 4219
		int IndexOf(string parameterName);

		// Token: 0x0600107C RID: 4220
		void RemoveAt(string parameterName);
	}
}

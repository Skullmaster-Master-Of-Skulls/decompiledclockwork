using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000045 RID: 69
	internal class SmiUniqueKeyProperty : SmiMetaDataProperty
	{
		// Token: 0x06000265 RID: 613 RVA: 0x001DEFE8 File Offset: 0x001DE3E8
		internal SmiUniqueKeyProperty(IList<bool> columnIsKey)
		{
			this._columns = new List<bool>(columnIsKey).AsReadOnly();
		}

		// Token: 0x17000044 RID: 68
		internal bool this[int ordinal]
		{
			get
			{
				return this._columns.Count > ordinal && this._columns[ordinal];
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x001DF048 File Offset: 0x001DE448
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x001DF058 File Offset: 0x001DE458
		internal override string TraceString()
		{
			string str = "UniqueKey(";
			bool flag = false;
			for (int i = 0; i < this._columns.Count; i++)
			{
				if (flag)
				{
					str += ",";
				}
				else
				{
					flag = true;
				}
				if (this._columns[i])
				{
					str += i.ToString(CultureInfo.InvariantCulture);
				}
			}
			return str + ")";
		}

		// Token: 0x040005F9 RID: 1529
		private IList<bool> _columns;
	}
}

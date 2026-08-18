using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200007A RID: 122
	internal class SmiUniqueKeyProperty : SmiMetaDataProperty
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0004810C File Offset: 0x0004750C
		internal SmiUniqueKeyProperty(IList<bool> columnIsKey)
		{
			this._columns = new List<bool>(columnIsKey).AsReadOnly();
		}

		// Token: 0x170000BA RID: 186
		internal bool this[int ordinal]
		{
			get
			{
				return this._columns.Count > ordinal && this._columns[ordinal];
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0004815C File Offset: 0x0004755C
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0004816C File Offset: 0x0004756C
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

		// Token: 0x0400025D RID: 605
		private IList<bool> _columns;
	}
}

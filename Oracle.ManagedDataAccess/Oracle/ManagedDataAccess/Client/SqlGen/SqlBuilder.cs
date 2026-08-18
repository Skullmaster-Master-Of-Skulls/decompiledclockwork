using System;
using System.Collections.Generic;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F1 RID: 241
	internal sealed class SqlBuilder : ISqlFragment
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0006E350 File Offset: 0x0006C550
		internal List<object> sqlFragments
		{
			get
			{
				if (this._sqlFragments == null)
				{
					this._sqlFragments = new List<object>();
				}
				return this._sqlFragments;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0006E36C File Offset: 0x0006C56C
		public void Append(object s)
		{
			this.sqlFragments.Add(s);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0006E37C File Offset: 0x0006C57C
		public void AppendLine()
		{
			this.sqlFragments.Add("\r\n");
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0006E390 File Offset: 0x0006C590
		public bool IsEmpty
		{
			get
			{
				return this._sqlFragments == null || 0 == this._sqlFragments.Count;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0006E3AC File Offset: 0x0006C5AC
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this._sqlFragments != null)
			{
				foreach (object obj in this._sqlFragments)
				{
					string text = obj as string;
					if (text != null)
					{
						writer.Write(text);
					}
					else
					{
						ISqlFragment sqlFragment = obj as ISqlFragment;
						if (sqlFragment == null)
						{
							throw new InvalidOperationException();
						}
						sqlFragment.WriteSql(writer, sqlGenerator);
					}
				}
			}
		}

		// Token: 0x04000C60 RID: 3168
		private List<object> _sqlFragments;
	}
}

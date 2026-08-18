using System;
using System.Collections.Generic;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x02000018 RID: 24
	internal sealed class SqlBuilder : ISqlFragment
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000F495 File Offset: 0x0000E495
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

		// Token: 0x060000C3 RID: 195 RVA: 0x0000F4B0 File Offset: 0x0000E4B0
		public void Append(object s)
		{
			this.sqlFragments.Add(s);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000F4BE File Offset: 0x0000E4BE
		public void AppendLine()
		{
			this.sqlFragments.Add("\r\n");
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000F4D0 File Offset: 0x0000E4D0
		public bool IsEmpty
		{
			get
			{
				return this._sqlFragments == null || 0 == this._sqlFragments.Count;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000F4EC File Offset: 0x0000E4EC
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

		// Token: 0x040000A5 RID: 165
		private List<object> _sqlFragments;
	}
}

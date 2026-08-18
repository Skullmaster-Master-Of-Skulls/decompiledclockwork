using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000031 RID: 49
	internal class SqlBuilder : ISqlFragment
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000128D8 File Offset: 0x00010AD8
		private List<object> sqlFragments
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

		// Token: 0x0600044F RID: 1103 RVA: 0x000128F3 File Offset: 0x00010AF3
		public void Append(object s)
		{
			this.sqlFragments.Add(s);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00012901 File Offset: 0x00010B01
		public void AppendLine()
		{
			this.sqlFragments.Add("\r\n");
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00012913 File Offset: 0x00010B13
		public virtual bool IsEmpty
		{
			get
			{
				return this._sqlFragments == null || this._sqlFragments.Count == 0;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00012930 File Offset: 0x00010B30
		public virtual void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
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

		// Token: 0x0400070C RID: 1804
		private List<object> _sqlFragments;
	}
}

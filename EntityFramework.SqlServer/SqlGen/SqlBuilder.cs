using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000036 RID: 54
	internal class SqlBuilder : ISqlFragment
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
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

		// Token: 0x0600030B RID: 779 RVA: 0x0000C90B File Offset: 0x0000AB0B
		public void Append(object s)
		{
			this.sqlFragments.Add(s);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C919 File Offset: 0x0000AB19
		public void AppendLine()
		{
			this.sqlFragments.Add("\r\n");
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000C92B File Offset: 0x0000AB2B
		public virtual bool IsEmpty
		{
			get
			{
				return this._sqlFragments == null || 0 == this._sqlFragments.Count;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C948 File Offset: 0x0000AB48
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

		// Token: 0x0400008F RID: 143
		private List<object> _sqlFragments;
	}
}

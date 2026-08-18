using System;
using System.Collections;
using System.Data;

namespace ImportExportClassLibrary
{
	// Token: 0x02000006 RID: 6
	internal class TemplateLoop
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003284 File Offset: 0x00002284
		public TemplateLoop(string name, int startIndex, DataSet ds)
		{
			int num = name.IndexOf('[');
			if (num > 0)
			{
				this.tableName = name.Substring(0, num);
				this.colname = name.Substring(num + 1, name.Length - num - 2);
			}
			else
			{
				this.tableName = name;
				this.colname = "";
			}
			this.currentIndex = 0;
			this.name = name;
			this.startIndex = startIndex;
			this.t = ds.Tables[this.tableName];
			this.count = 0;
			if (this.t != null && this.t.Rows.Count > 0 && this.t.Columns.Contains(this.colname))
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.t.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow[this.colname].ToString().Trim().ToLower();
					if (!arrayList.Contains(text))
					{
						arrayList.Add(text);
						this.count++;
					}
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000033E0 File Offset: 0x000023E0
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033E8 File Offset: 0x000023E8
		public bool IncrementCount()
		{
			string currentValue = this.GetCurrentValue();
			for (;;)
			{
				this.currentIndex++;
				if (this.currentIndex >= this.count)
				{
					break;
				}
				string currentValue2 = this.GetCurrentValue();
				if (currentValue2.CompareTo(currentValue) != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000342C File Offset: 0x0000242C
		public string GetCurrentValue()
		{
			if (this.t != null && this.currentIndex < this.t.Rows.Count)
			{
				return this.t.Rows[this.currentIndex][this.colname].ToString();
			}
			return "";
		}

		// Token: 0x04000010 RID: 16
		private string name;

		// Token: 0x04000011 RID: 17
		private string tableName;

		// Token: 0x04000012 RID: 18
		private string colname;

		// Token: 0x04000013 RID: 19
		private int currentIndex;

		// Token: 0x04000014 RID: 20
		private int count;

		// Token: 0x04000015 RID: 21
		private int startIndex;

		// Token: 0x04000016 RID: 22
		private DataTable t;
	}
}

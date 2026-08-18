using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000063 RID: 99
	public class MailMergeValue
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0002214C File Offset: 0x0002034C
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00022164 File Offset: 0x00020364
		public List<object> Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00022170 File Offset: 0x00020370
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00022210 File Offset: 0x00020410
		public string ValueToString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.values.Count; i++)
				{
					string arg = this.FormatValue(this.values[i]);
					stringBuilder.AppendFormat("{0}{1}", (i > 0) ? this.delimiter : "", arg);
				}
				string text = stringBuilder.ToString();
				bool flag = !string.IsNullOrEmpty(text);
				string result;
				if (flag)
				{
					result = string.Format("{0}{1}{2}", this.pre, text, this.post);
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				this.values.Clear();
				this.values.Add(value);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002222C File Offset: 0x0002042C
		private string FormatValue(object val)
		{
			bool flag = val == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = val.ToString();
			}
			return result;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00022254 File Offset: 0x00020454
		public MailMergeValue()
		{
			this.values = new List<object>();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002228C File Offset: 0x0002048C
		public MailMergeValue(object value)
		{
			this.values = new List<object>();
			this.values.Add(value);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00022210 File Offset: 0x00020410
		public void SetValue(object val)
		{
			this.values.Clear();
			this.values.Add(val);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000222DC File Offset: 0x000204DC
		public MailMergeValue(params object[] values)
		{
			this.values = new List<object>();
			foreach (object item in values)
			{
				this.values.Add(item);
			}
		}

		// Token: 0x04000292 RID: 658
		private List<object> values;

		// Token: 0x04000293 RID: 659
		private string delimiter = ", ";

		// Token: 0x04000294 RID: 660
		private string pre = "";

		// Token: 0x04000295 RID: 661
		private string post = "";
	}
}

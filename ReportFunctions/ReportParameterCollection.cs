using System;
using System.Collections;

namespace ReportFunctions
{
	// Token: 0x02000042 RID: 66
	public class ReportParameterCollection : CollectionBase
	{
		// Token: 0x170000BB RID: 187
		public ReportParameter this[int index]
		{
			get
			{
				return (ReportParameter)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000447D0 File Offset: 0x000437D0
		public int Add(ReportParameter reportParameter)
		{
			return base.List.Add(reportParameter);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000447EE File Offset: 0x000437EE
		public void Insert(int index, ReportParameter reportParameter)
		{
			base.List.Insert(index, reportParameter);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000447FF File Offset: 0x000437FF
		public void Remove(ReportParameter reportParameter)
		{
			base.List.Remove(reportParameter);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00044810 File Offset: 0x00043810
		public bool Contains(ReportParameter reportParameter)
		{
			return base.List.Contains(reportParameter);
		}

		// Token: 0x170000BC RID: 188
		public ReportParameter this[string ParamName]
		{
			get
			{
				return this.FindReportParameter(ParamName);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0004484C File Offset: 0x0004384C
		private ReportParameter FindReportParameter(string paramName)
		{
			string strB = paramName.ToLower().Trim();
			foreach (object obj in base.List)
			{
				ReportParameter reportParameter = (ReportParameter)obj;
				if (reportParameter.ParamName.ToLower().Trim().CompareTo(strB) == 0)
				{
					return reportParameter;
				}
			}
			return null;
		}
	}
}

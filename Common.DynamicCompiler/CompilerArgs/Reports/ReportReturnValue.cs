using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports
{
	// Token: 0x02000011 RID: 17
	public class ReportReturnValue : ICompilerReturnValue
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003AEC File Offset: 0x00001CEC
		public ReportReturnValue()
		{
			this.VariablesOut = new List<ReportVariable>();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003B04 File Offset: 0x00001D04
		public ReportReturnValue(ReportParameters reportParameters)
		{
			bool flag = reportParameters == null;
			if (flag)
			{
				this.VariablesOut = new List<ReportVariable>();
			}
			else
			{
				this.Table = reportParameters.Table;
				this.VariablesOut = (reportParameters.Variables ?? new List<ReportVariable>());
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003B54 File Offset: 0x00001D54
		public ReportReturnValue(DataTable table, ReportParameters reportParameters)
		{
			this.Table = table;
			bool flag = reportParameters == null || reportParameters.Variables == null;
			if (flag)
			{
				this.VariablesOut = new List<ReportVariable>();
			}
			else
			{
				this.VariablesOut = reportParameters.Variables.ToList<ReportVariable>();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public ReportReturnValue(DataTable table, IList<ReportVariable> variablesOut)
		{
			this.Table = table;
			this.VariablesOut = (variablesOut ?? new List<ReportVariable>());
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public DataTable Table
		{
			get
			{
				return this._table;
			}
			set
			{
				bool flag = this._table == null;
				if (flag)
				{
					this._table = new DataTable("table");
				}
				else
				{
					bool flag2 = string.IsNullOrEmpty(this._table.TableName);
					if (flag2)
					{
						this._table.TableName = "table";
					}
				}
				this._table = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003C3C File Offset: 0x00001E3C
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003C6C File Offset: 0x00001E6C
		public IList<ReportVariable> VariablesOut
		{
			get
			{
				bool flag = this._variablesOut == null;
				if (flag)
				{
					this._variablesOut = new List<ReportVariable>();
				}
				return this._variablesOut;
			}
			set
			{
				this._variablesOut = value;
			}
		}

		// Token: 0x04000039 RID: 57
		private DataTable _table;

		// Token: 0x0400003A RID: 58
		private IList<ReportVariable> _variablesOut;
	}
}

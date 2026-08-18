using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports
{
	// Token: 0x02000010 RID: 16
	public class ReportParameters : ICompilerParameters
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000392A File Offset: 0x00001B2A
		public ReportParameters()
		{
			this.Variables = new List<ReportVariable>();
			this.Context = new CompileContext();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000394C File Offset: 0x00001B4C
		public ReportParameters(CompileContext compileContext)
		{
			this.Variables = new List<ReportVariable>();
			this.Context = (compileContext ?? new CompileContext());
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003973 File Offset: 0x00001B73
		public ReportParameters(CompileContext compileContext, IList<ReportVariable> variables)
		{
			this.Variables = (variables ?? new List<ReportVariable>());
			this.Context = (compileContext ?? new CompileContext());
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000399F File Offset: 0x00001B9F
		public ReportParameters(CompileContext compileContext, IList<ReportVariable> variables, DataTable table)
		{
			this.Variables = (variables ?? new List<ReportVariable>());
			this.Context = (compileContext ?? new CompileContext());
			this.Table = table;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000039D4 File Offset: 0x00001BD4
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000039EC File Offset: 0x00001BEC
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003A48 File Offset: 0x00001C48
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003A70 File Offset: 0x00001C70
		public int WhoAmI
		{
			get
			{
				return (this.Context == null) ? 0 : this.Context.WhoAmI;
			}
			set
			{
				bool flag = this.Context == null;
				if (flag)
				{
					this.Context = new CompileContext();
				}
				this.Context.WhoAmI = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003AA4 File Offset: 0x00001CA4
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003AAC File Offset: 0x00001CAC
		public CompileContext Context { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003AB8 File Offset: 0x00001CB8
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003AE2 File Offset: 0x00001CE2
		public IList<ReportVariable> Variables
		{
			get
			{
				IList<ReportVariable> result;
				if ((result = this._variables) == null)
				{
					result = (this._variables = new List<ReportVariable>());
				}
				return result;
			}
			set
			{
				this._variables = value;
			}
		}

		// Token: 0x04000036 RID: 54
		private DataTable _table;

		// Token: 0x04000038 RID: 56
		private IList<ReportVariable> _variables;
	}
}

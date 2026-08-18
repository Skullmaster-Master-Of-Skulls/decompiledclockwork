using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class Variable
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0002A300 File Offset: 0x00028500
		public Variable(string varName, object varValue)
		{
			this.variableName = varName.Trim().ToLower();
			this.variableValue = varValue;
			this.searchFunctionsDataRow = null;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0002A329 File Offset: 0x00028529
		public Variable(string varName, object varValue, DataRow _SearchFunctionsDataRow)
		{
			this.variableName = varName.Trim().ToLower();
			this.variableValue = varValue;
			this.searchFunctionsDataRow = _SearchFunctionsDataRow;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0002A354 File Offset: 0x00028554
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0002A36C File Offset: 0x0002856C
		public string VariableName
		{
			get
			{
				return this.variableName;
			}
			set
			{
				this.variableName = value.Trim().ToLower();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0002A380 File Offset: 0x00028580
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0002A398 File Offset: 0x00028598
		public object VariableValue
		{
			get
			{
				return this.variableValue;
			}
			set
			{
				this.variableValue = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0002A3A4 File Offset: 0x000285A4
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0002A3BC File Offset: 0x000285BC
		public DataRow SearchFunctionsDataRow
		{
			get
			{
				return this.searchFunctionsDataRow;
			}
			set
			{
				this.searchFunctionsDataRow = value;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0002A3C8 File Offset: 0x000285C8
		public bool CommaSeparatedValueContains(string s)
		{
			string strB = s.ToLower().Trim();
			string[] array = this.variableValue.ToString().Trim().Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				bool flag = text.ToLower().Trim().CompareTo(strB) == 0;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0002A444 File Offset: 0x00028644
		public static DataTable GetSearchFunctionsTable(int searchFunctionId, int searchInfoId, int functionCode, string functionParameters, int ordernum, string custom, string customsqlinjection, string customsqlinjectionoperator, string functionDescription)
		{
			DataTable dataTable = new DataTable("t");
			Type typeFromHandle = typeof(int);
			dataTable.Columns.Add("searchfunctionid", typeFromHandle);
			dataTable.Columns.Add("searchinfoid", typeFromHandle);
			dataTable.Columns.Add("functioncode", typeFromHandle);
			dataTable.Columns.Add("functionparameters");
			dataTable.Columns.Add("ordernum", typeFromHandle);
			dataTable.Columns.Add("custom");
			dataTable.Columns.Add("customsqlinjection");
			dataTable.Columns.Add("customsqlinjectionoperator");
			dataTable.Columns.Add("functiondescription");
			DataRow dataRow = dataTable.NewRow();
			dataRow[0] = searchFunctionId;
			dataRow[1] = searchInfoId;
			dataRow[2] = functionCode;
			dataRow[3] = functionParameters;
			dataRow[4] = ordernum;
			dataRow[5] = custom;
			dataRow[6] = customsqlinjection;
			dataRow[7] = customsqlinjectionoperator;
			dataRow[8] = functionDescription;
			dataTable.Rows.Add(dataRow);
			return dataTable;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0002A588 File Offset: 0x00028788
		public static Variable FindVariable(ArrayList variables, string name)
		{
			string strB = name.Trim().ToLower();
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				bool flag = variable.VariableName.CompareTo(strB) == 0;
				if (flag)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0002A614 File Offset: 0x00028814
		public static Variable GetVariable(List<Variable> variables, string name)
		{
			return variables.Find((Variable e) => e.VariableName.ToLower().Equals(name.ToLower()));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0002A648 File Offset: 0x00028848
		public static object GetVariableValue(List<Variable> variables, string name)
		{
			Variable variable = Variable.GetVariable(variables, name);
			return (variable == null) ? null : variable.VariableValue;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0002A670 File Offset: 0x00028870
		public static string GetVariableStringValueThrowExceptionIfNotFound(List<Variable> variables, string name)
		{
			Variable variable = Variable.GetVariable(variables, name);
			bool flag = variable != null;
			if (flag)
			{
				return variable.VariableValue.ToString();
			}
			throw new Exception("Can't find variable [" + name + "]");
		}

		// Token: 0x04000107 RID: 263
		private string variableName;

		// Token: 0x04000108 RID: 264
		private object variableValue;

		// Token: 0x04000109 RID: 265
		private DataRow searchFunctionsDataRow;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace ReportFunctions
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class Variable
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00042C04 File Offset: 0x00041C04
		public Variable(string varName, object varValue)
		{
			this.variableName = varName.Trim().ToLower();
			this.variableValue = varValue;
			this.searchFunctionsDataRow = null;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00042C2E File Offset: 0x00041C2E
		public Variable(string varName, object varValue, DataRow _SearchFunctionsDataRow)
		{
			this.variableName = varName.Trim().ToLower();
			this.variableValue = varValue;
			this.searchFunctionsDataRow = _SearchFunctionsDataRow;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00042C58 File Offset: 0x00041C58
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00042C70 File Offset: 0x00041C70
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00042C84 File Offset: 0x00041C84
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00042C9C File Offset: 0x00041C9C
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00042CA8 File Offset: 0x00041CA8
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00042CC0 File Offset: 0x00041CC0
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

		// Token: 0x0600038D RID: 909 RVA: 0x00042CCC File Offset: 0x00041CCC
		public bool CommaSeparatedValueContains(string s)
		{
			string strB = s.ToLower().Trim();
			string[] array = this.variableValue.ToString().Trim().Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				if (text.ToLower().Trim().CompareTo(strB) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00042D58 File Offset: 0x00041D58
		public static DataTable GetSearchFunctionsTable(int searchFunctionId, int searchInfoId, int functionCode, string functionParameters, int ordernum, string custom, string customsqlinjection, string customsqlinjectionoperator, string functionDescription)
		{
			DataTable dataTable = new DataTable();
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

		// Token: 0x0600038F RID: 911 RVA: 0x00042E98 File Offset: 0x00041E98
		public static Variable FindVariable(ArrayList variables, string name)
		{
			string strB = name.Trim().ToLower();
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				if (variable.VariableName.CompareTo(strB) == 0)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00042F68 File Offset: 0x00041F68
		public static Variable GetVariable(List<Variable> variables, string name)
		{
			return variables.Find((Variable e) => e.VariableName.ToLower().Equals(name.ToLower()));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00042F9C File Offset: 0x00041F9C
		public static object GetVariableValue(List<Variable> variables, string name)
		{
			Variable variable = Variable.GetVariable(variables, name);
			return (variable == null) ? null : variable.VariableValue;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00042FC4 File Offset: 0x00041FC4
		public static string GetVariableStringValueThrowExceptionIfNotFound(List<Variable> variables, string name)
		{
			Variable variable = Variable.GetVariable(variables, name);
			if (variable != null)
			{
				return variable.VariableValue.ToString();
			}
			throw new Exception("Can't find variable [" + name + "]");
		}

		// Token: 0x040001C7 RID: 455
		private string variableName;

		// Token: 0x040001C8 RID: 456
		private object variableValue;

		// Token: 0x040001C9 RID: 457
		private DataRow searchFunctionsDataRow;
	}
}

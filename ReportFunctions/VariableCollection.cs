using System;
using System.Collections;

namespace ReportFunctions
{
	// Token: 0x0200003D RID: 61
	public class VariableCollection : CollectionBase
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00043008 File Offset: 0x00042008
		public int Add(Variable variable)
		{
			return base.List.Add(variable);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00043028 File Offset: 0x00042028
		public int Add(string vname, object vval)
		{
			return base.List.Add(new Variable(vname, vval));
		}

		// Token: 0x17000094 RID: 148
		public Variable this[int index]
		{
			get
			{
				return (Variable)base.List[index];
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00043070 File Offset: 0x00042070
		public Variable FindVariable(params string[] varNames)
		{
			foreach (string varname in varNames)
			{
				Variable variable = this[varname];
				if (variable != null)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x17000095 RID: 149
		public Variable this[string varname]
		{
			get
			{
				string strB = varname.ToLower();
				foreach (object obj in base.List)
				{
					Variable variable = (Variable)obj;
					if (variable.VariableName.ToLower().CompareTo(strB) == 0)
					{
						return variable;
					}
				}
				return null;
			}
		}
	}
}

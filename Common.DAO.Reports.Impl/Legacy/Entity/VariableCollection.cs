using System;
using System.Collections;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200002A RID: 42
	public class VariableCollection : CollectionBase
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0002A6B4 File Offset: 0x000288B4
		public int Add(Variable variable)
		{
			return base.List.Add(variable);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0002A6D4 File Offset: 0x000288D4
		public int Add(string vname, object vval)
		{
			return base.List.Add(new Variable(vname, vval));
		}

		// Token: 0x170000AC RID: 172
		public Variable this[int index]
		{
			get
			{
				return (Variable)base.List[index];
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0002A71C File Offset: 0x0002891C
		public Variable FindVariable(params string[] varNames)
		{
			foreach (string varname in varNames)
			{
				Variable variable = this[varname];
				bool flag = variable != null;
				if (flag)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x170000AD RID: 173
		public Variable this[string varname]
		{
			get
			{
				string strB = varname.ToLower();
				foreach (object obj in base.List)
				{
					Variable variable = (Variable)obj;
					bool flag = variable.VariableName.ToLower().CompareTo(strB) == 0;
					if (flag)
					{
						return variable;
					}
				}
				return null;
			}
		}
	}
}

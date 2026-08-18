using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200021D RID: 541
	internal class ScopeExpression : BlockExpression
	{
		// Token: 0x060013D3 RID: 5075 RVA: 0x00043BB0 File Offset: 0x00041DB0
		internal ScopeExpression(IList<ParameterExpression> variables)
		{
			this._variables = variables;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00043BBF File Offset: 0x00041DBF
		internal override int VariableCount
		{
			get
			{
				return this._variables.Count;
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00043BCC File Offset: 0x00041DCC
		internal override ParameterExpression GetVariable(int index)
		{
			return this._variables[index];
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00043BDA File Offset: 0x00041DDA
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return Expression.ReturnReadOnly<ParameterExpression>(ref this._variables);
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00043BE7 File Offset: 0x00041DE7
		protected IList<ParameterExpression> VariablesList
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00043BEF File Offset: 0x00041DEF
		internal IList<ParameterExpression> ReuseOrValidateVariables(ReadOnlyCollection<ParameterExpression> variables)
		{
			if (variables != null && variables != this.VariablesList)
			{
				Expression.ValidateVariables(variables, "variables");
				return variables;
			}
			return this.VariablesList;
		}

		// Token: 0x04000971 RID: 2417
		private IList<ParameterExpression> _variables;
	}
}

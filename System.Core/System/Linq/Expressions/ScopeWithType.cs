using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000220 RID: 544
	internal class ScopeWithType : ScopeN
	{
		// Token: 0x060013E3 RID: 5091 RVA: 0x00043C9F File Offset: 0x00041E9F
		internal ScopeWithType(IList<ParameterExpression> variables, IList<Expression> expressions, Type type) : base(variables, expressions)
		{
			this._type = type;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00043CB0 File Offset: 0x00041EB0
		public sealed override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00043CB8 File Offset: 0x00041EB8
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new ScopeWithType(base.ReuseOrValidateVariables(variables), args, this._type);
		}

		// Token: 0x04000974 RID: 2420
		private readonly Type _type;
	}
}

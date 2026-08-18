using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x0200027D RID: 637
	internal sealed class HoistedLocals
	{
		// Token: 0x060016AE RID: 5806 RVA: 0x0004B808 File Offset: 0x00049A08
		internal HoistedLocals(HoistedLocals parent, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (parent != null)
			{
				vars = new TrueReadOnlyCollection<ParameterExpression>(vars.AddFirst(parent.SelfVariable));
			}
			Dictionary<Expression, int> dictionary = new Dictionary<Expression, int>(vars.Count);
			for (int i = 0; i < vars.Count; i++)
			{
				dictionary.Add(vars[i], i);
			}
			this.SelfVariable = Expression.Variable(typeof(object[]), null);
			this.Parent = parent;
			this.Variables = vars;
			this.Indexes = new ReadOnlyDictionary<Expression, int>(dictionary);
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0004B88C File Offset: 0x00049A8C
		internal ParameterExpression ParentVariable
		{
			get
			{
				if (this.Parent == null)
				{
					return null;
				}
				return this.Parent.SelfVariable;
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0004B8A3 File Offset: 0x00049AA3
		internal static object[] GetParent(object[] locals)
		{
			return ((StrongBox<object[]>)locals[0]).Value;
		}

		// Token: 0x04000B47 RID: 2887
		internal readonly HoistedLocals Parent;

		// Token: 0x04000B48 RID: 2888
		internal readonly ReadOnlyDictionary<Expression, int> Indexes;

		// Token: 0x04000B49 RID: 2889
		internal readonly ReadOnlyCollection<ParameterExpression> Variables;

		// Token: 0x04000B4A RID: 2890
		internal readonly ParameterExpression SelfVariable;
	}
}

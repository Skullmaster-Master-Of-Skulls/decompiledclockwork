using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200013F RID: 319
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	[__DynamicallyInvokable]
	public static class RuntimeOps
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x00025924 File Offset: 0x00023B24
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static bool ExpandoTryGetValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			return expando.TryGetValue(indexClass, index, name, ignoreCase, out value);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00025933 File Offset: 0x00023B33
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static object ExpandoTrySetValue(ExpandoObject expando, object indexClass, int index, object value, string name, bool ignoreCase)
		{
			expando.TrySetValue(indexClass, index, value, name, ignoreCase, false);
			return value;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00025944 File Offset: 0x00023B44
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static bool ExpandoTryDeleteValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase)
		{
			return expando.TryDeleteValue(indexClass, index, name, ignoreCase, ExpandoObject.Uninitialized);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00025956 File Offset: 0x00023B56
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static bool ExpandoCheckVersion(ExpandoObject expando, object version)
		{
			return expando.Class == version;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00025961 File Offset: 0x00023B61
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static void ExpandoPromoteClass(ExpandoObject expando, object oldClass, object newClass)
		{
			expando.PromoteClass(oldClass, newClass);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002596C File Offset: 0x00023B6C
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static Expression Quote(Expression expression, object hoistedLocals, object[] locals)
		{
			RuntimeOps.ExpressionQuoter expressionQuoter = new RuntimeOps.ExpressionQuoter((HoistedLocals)hoistedLocals, locals);
			return expressionQuoter.Visit(expression);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002598D File Offset: 0x00023B8D
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static IRuntimeVariables MergeRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
		{
			return new RuntimeOps.MergedRuntimeVariables(first, second, indexes);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00025997 File Offset: 0x00023B97
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static IRuntimeVariables CreateRuntimeVariables(object[] data, long[] indexes)
		{
			return new RuntimeOps.RuntimeVariableList(data, indexes);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000259A0 File Offset: 0x00023BA0
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static IRuntimeVariables CreateRuntimeVariables()
		{
			return new RuntimeOps.EmptyRuntimeVariables();
		}

		// Token: 0x0200036A RID: 874
		private sealed class ExpressionQuoter : ExpressionVisitor
		{
			// Token: 0x06001B8A RID: 7050 RVA: 0x000634AC File Offset: 0x000616AC
			internal ExpressionQuoter(HoistedLocals scope, object[] locals)
			{
				this._scope = scope;
				this._locals = locals;
			}

			// Token: 0x06001B8B RID: 7051 RVA: 0x000634D0 File Offset: 0x000616D0
			protected internal override Expression VisitLambda<T>(Expression<T> node)
			{
				this._shadowedVars.Push(new Set<ParameterExpression>(node.Parameters));
				Expression expression = this.Visit(node.Body);
				this._shadowedVars.Pop();
				if (expression == node.Body)
				{
					return node;
				}
				return Expression.Lambda<T>(expression, node.Name, node.TailCall, node.Parameters);
			}

			// Token: 0x06001B8C RID: 7052 RVA: 0x00063530 File Offset: 0x00061730
			protected internal override Expression VisitBlock(BlockExpression node)
			{
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Push(new Set<ParameterExpression>(node.Variables));
				}
				ReadOnlyCollection<Expression> readOnlyCollection = base.Visit(node.Expressions);
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Pop();
				}
				if (readOnlyCollection == node.Expressions)
				{
					return node;
				}
				return Expression.Block(node.Variables, readOnlyCollection);
			}

			// Token: 0x06001B8D RID: 7053 RVA: 0x000635A0 File Offset: 0x000617A0
			protected override CatchBlock VisitCatchBlock(CatchBlock node)
			{
				if (node.Variable != null)
				{
					this._shadowedVars.Push(new Set<ParameterExpression>(new ParameterExpression[]
					{
						node.Variable
					}));
				}
				Expression expression = this.Visit(node.Body);
				Expression expression2 = this.Visit(node.Filter);
				if (node.Variable != null)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body && expression2 == node.Filter)
				{
					return node;
				}
				return Expression.MakeCatchBlock(node.Test, node.Variable, expression, expression2);
			}

			// Token: 0x06001B8E RID: 7054 RVA: 0x0006362C File Offset: 0x0006182C
			protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
			{
				int count = node.Variables.Count;
				List<IStrongBox> list = new List<IStrongBox>();
				List<ParameterExpression> list2 = new List<ParameterExpression>();
				int[] array = new int[count];
				for (int i = 0; i < count; i++)
				{
					IStrongBox box = this.GetBox(node.Variables[i]);
					if (box == null)
					{
						array[i] = list2.Count;
						list2.Add(node.Variables[i]);
					}
					else
					{
						array[i] = -1 - list.Count;
						list.Add(box);
					}
				}
				if (list.Count == 0)
				{
					return node;
				}
				ConstantExpression constantExpression = Expression.Constant(new RuntimeOps.RuntimeVariables(list.ToArray()), typeof(IRuntimeVariables));
				if (list2.Count == 0)
				{
					return constantExpression;
				}
				return Expression.Call(typeof(RuntimeOps).GetMethod("MergeRuntimeVariables"), Expression.RuntimeVariables(new TrueReadOnlyCollection<ParameterExpression>(list2.ToArray())), constantExpression, Expression.Constant(array));
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x00063718 File Offset: 0x00061918
			protected internal override Expression VisitParameter(ParameterExpression node)
			{
				IStrongBox box = this.GetBox(node);
				if (box == null)
				{
					return node;
				}
				return Expression.Field(Expression.Constant(box), "Value");
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x00063744 File Offset: 0x00061944
			private IStrongBox GetBox(ParameterExpression variable)
			{
				foreach (Set<ParameterExpression> set in this._shadowedVars)
				{
					if (set.Contains(variable))
					{
						return null;
					}
				}
				HoistedLocals hoistedLocals = this._scope;
				object[] array = this._locals;
				int num;
				while (!hoistedLocals.Indexes.TryGetValue(variable, out num))
				{
					hoistedLocals = hoistedLocals.Parent;
					if (hoistedLocals == null)
					{
						throw ContractUtils.Unreachable;
					}
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[num];
			}

			// Token: 0x04000F90 RID: 3984
			private readonly HoistedLocals _scope;

			// Token: 0x04000F91 RID: 3985
			private readonly object[] _locals;

			// Token: 0x04000F92 RID: 3986
			private readonly Stack<Set<ParameterExpression>> _shadowedVars = new Stack<Set<ParameterExpression>>();
		}

		// Token: 0x0200036B RID: 875
		private sealed class RuntimeVariables : IRuntimeVariables
		{
			// Token: 0x06001B91 RID: 7057 RVA: 0x000637E4 File Offset: 0x000619E4
			internal RuntimeVariables(IStrongBox[] boxes)
			{
				this._boxes = boxes;
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000637F3 File Offset: 0x000619F3
			int IRuntimeVariables.Count
			{
				get
				{
					return this._boxes.Length;
				}
			}

			// Token: 0x17000516 RID: 1302
			object IRuntimeVariables.this[int index]
			{
				get
				{
					return this._boxes[index].Value;
				}
				set
				{
					this._boxes[index].Value = value;
				}
			}

			// Token: 0x04000F93 RID: 3987
			private readonly IStrongBox[] _boxes;
		}

		// Token: 0x0200036C RID: 876
		private sealed class MergedRuntimeVariables : IRuntimeVariables
		{
			// Token: 0x06001B95 RID: 7061 RVA: 0x0006381C File Offset: 0x00061A1C
			internal MergedRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
			{
				this._first = first;
				this._second = second;
				this._indexes = indexes;
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00063839 File Offset: 0x00061A39
			public int Count
			{
				get
				{
					return this._indexes.Length;
				}
			}

			// Token: 0x17000518 RID: 1304
			public object this[int index]
			{
				get
				{
					index = this._indexes[index];
					if (index < 0)
					{
						return this._second[-1 - index];
					}
					return this._first[index];
				}
				set
				{
					index = this._indexes[index];
					if (index >= 0)
					{
						this._first[index] = value;
						return;
					}
					this._second[-1 - index] = value;
				}
			}

			// Token: 0x04000F94 RID: 3988
			private readonly IRuntimeVariables _first;

			// Token: 0x04000F95 RID: 3989
			private readonly IRuntimeVariables _second;

			// Token: 0x04000F96 RID: 3990
			private readonly int[] _indexes;
		}

		// Token: 0x0200036D RID: 877
		private sealed class EmptyRuntimeVariables : IRuntimeVariables
		{
			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0006389B File Offset: 0x00061A9B
			int IRuntimeVariables.Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1700051A RID: 1306
			object IRuntimeVariables.this[int index]
			{
				get
				{
					throw new ArgumentOutOfRangeException("index");
				}
				set
				{
					throw new ArgumentOutOfRangeException("index");
				}
			}
		}

		// Token: 0x0200036E RID: 878
		private sealed class RuntimeVariableList : IRuntimeVariables
		{
			// Token: 0x06001B9D RID: 7069 RVA: 0x000638BE File Offset: 0x00061ABE
			internal RuntimeVariableList(object[] data, long[] indexes)
			{
				this._data = data;
				this._indexes = indexes;
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000638D4 File Offset: 0x00061AD4
			public int Count
			{
				get
				{
					return this._indexes.Length;
				}
			}

			// Token: 0x1700051C RID: 1308
			public object this[int index]
			{
				get
				{
					return this.GetStrongBox(index).Value;
				}
				set
				{
					this.GetStrongBox(index).Value = value;
				}
			}

			// Token: 0x06001BA1 RID: 7073 RVA: 0x000638FC File Offset: 0x00061AFC
			private IStrongBox GetStrongBox(int index)
			{
				long num = this._indexes[index];
				object[] array = this._data;
				for (int i = (int)(num >> 32); i > 0; i--)
				{
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[(int)num];
			}

			// Token: 0x04000F97 RID: 3991
			private readonly object[] _data;

			// Token: 0x04000F98 RID: 3992
			private readonly long[] _indexes;
		}
	}
}

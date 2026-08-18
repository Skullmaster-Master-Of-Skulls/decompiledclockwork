using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	// Token: 0x020000BD RID: 189
	[DebuggerTypeProxy(typeof(BindingRestrictions.BindingRestrictionsProxy))]
	[DebuggerDisplay("{DebugView}")]
	[__DynamicallyInvokable]
	public abstract class BindingRestrictions
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x00011034 File Offset: 0x0000F234
		private BindingRestrictions()
		{
		}

		// Token: 0x0600056B RID: 1387
		internal abstract Expression GetExpression();

		// Token: 0x0600056C RID: 1388 RVA: 0x0001103C File Offset: 0x0000F23C
		[__DynamicallyInvokable]
		public BindingRestrictions Merge(BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			if (this == BindingRestrictions.Empty)
			{
				return restrictions;
			}
			if (restrictions == BindingRestrictions.Empty)
			{
				return this;
			}
			return new BindingRestrictions.MergedRestriction(this, restrictions);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00011064 File Offset: 0x0000F264
		[__DynamicallyInvokable]
		public static BindingRestrictions GetTypeRestriction(Expression expression, Type type)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			return new BindingRestrictions.TypeRestriction(expression, type);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00011083 File Offset: 0x0000F283
		internal static BindingRestrictions GetTypeRestriction(DynamicMetaObject obj)
		{
			if (obj.Value == null && obj.HasValue)
			{
				return BindingRestrictions.GetInstanceRestriction(obj.Expression, null);
			}
			return BindingRestrictions.GetTypeRestriction(obj.Expression, obj.LimitType);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000110B3 File Offset: 0x0000F2B3
		[__DynamicallyInvokable]
		public static BindingRestrictions GetInstanceRestriction(Expression expression, object instance)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			return new BindingRestrictions.InstanceRestriction(expression, instance);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000110C7 File Offset: 0x0000F2C7
		[__DynamicallyInvokable]
		public static BindingRestrictions GetExpressionRestriction(Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.Requires(expression.Type == typeof(bool), "expression");
			return new BindingRestrictions.CustomRestriction(expression);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000110FC File Offset: 0x0000F2FC
		[__DynamicallyInvokable]
		public static BindingRestrictions Combine(IList<DynamicMetaObject> contributingObjects)
		{
			BindingRestrictions bindingRestrictions = BindingRestrictions.Empty;
			if (contributingObjects != null)
			{
				foreach (DynamicMetaObject dynamicMetaObject in contributingObjects)
				{
					if (dynamicMetaObject != null)
					{
						bindingRestrictions = bindingRestrictions.Merge(dynamicMetaObject.Restrictions);
					}
				}
			}
			return bindingRestrictions;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00011158 File Offset: 0x0000F358
		[__DynamicallyInvokable]
		public Expression ToExpression()
		{
			if (this == BindingRestrictions.Empty)
			{
				return Expression.Constant(true);
			}
			BindingRestrictions.TestBuilder testBuilder = new BindingRestrictions.TestBuilder();
			Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
			stack.Push(this);
			do
			{
				BindingRestrictions bindingRestrictions = stack.Pop();
				BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
				if (mergedRestriction != null)
				{
					stack.Push(mergedRestriction.Right);
					stack.Push(mergedRestriction.Left);
				}
				else
				{
					testBuilder.Append(bindingRestrictions);
				}
			}
			while (stack.Count > 0);
			return testBuilder.ToExpression();
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000111CD File Offset: 0x0000F3CD
		private string DebugView
		{
			get
			{
				return this.ToExpression().ToString();
			}
		}

		// Token: 0x04000596 RID: 1430
		[__DynamicallyInvokable]
		public static readonly BindingRestrictions Empty = new BindingRestrictions.CustomRestriction(Expression.Constant(true));

		// Token: 0x04000597 RID: 1431
		private const int TypeRestrictionHash = 268435456;

		// Token: 0x04000598 RID: 1432
		private const int InstanceRestrictionHash = 536870912;

		// Token: 0x04000599 RID: 1433
		private const int CustomRestrictionHash = 1073741824;

		// Token: 0x0200030D RID: 781
		private sealed class TestBuilder
		{
			// Token: 0x06001A84 RID: 6788 RVA: 0x0006118F File Offset: 0x0005F38F
			internal void Append(BindingRestrictions restrictions)
			{
				if (this._unique.Contains(restrictions))
				{
					return;
				}
				this._unique.Add(restrictions);
				this.Push(restrictions.GetExpression(), 0);
			}

			// Token: 0x06001A85 RID: 6789 RVA: 0x000611BC File Offset: 0x0005F3BC
			internal Expression ToExpression()
			{
				Expression expression = this._tests.Pop().Node;
				while (this._tests.Count > 0)
				{
					expression = Expression.AndAlso(this._tests.Pop().Node, expression);
				}
				return expression;
			}

			// Token: 0x06001A86 RID: 6790 RVA: 0x00061204 File Offset: 0x0005F404
			private void Push(Expression node, int depth)
			{
				while (this._tests.Count > 0 && this._tests.Peek().Depth == depth)
				{
					node = Expression.AndAlso(this._tests.Pop().Node, node);
					depth++;
				}
				this._tests.Push(new BindingRestrictions.TestBuilder.AndNode
				{
					Node = node,
					Depth = depth
				});
			}

			// Token: 0x04000E31 RID: 3633
			private readonly Set<BindingRestrictions> _unique = new Set<BindingRestrictions>();

			// Token: 0x04000E32 RID: 3634
			private readonly Stack<BindingRestrictions.TestBuilder.AndNode> _tests = new Stack<BindingRestrictions.TestBuilder.AndNode>();

			// Token: 0x0200047B RID: 1147
			private struct AndNode
			{
				// Token: 0x040013A3 RID: 5027
				internal int Depth;

				// Token: 0x040013A4 RID: 5028
				internal Expression Node;
			}
		}

		// Token: 0x0200030E RID: 782
		private sealed class MergedRestriction : BindingRestrictions
		{
			// Token: 0x06001A88 RID: 6792 RVA: 0x00061293 File Offset: 0x0005F493
			internal MergedRestriction(BindingRestrictions left, BindingRestrictions right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x06001A89 RID: 6793 RVA: 0x000612A9 File Offset: 0x0005F4A9
			internal override Expression GetExpression()
			{
				throw ContractUtils.Unreachable;
			}

			// Token: 0x04000E33 RID: 3635
			internal readonly BindingRestrictions Left;

			// Token: 0x04000E34 RID: 3636
			internal readonly BindingRestrictions Right;
		}

		// Token: 0x0200030F RID: 783
		private sealed class CustomRestriction : BindingRestrictions
		{
			// Token: 0x06001A8A RID: 6794 RVA: 0x000612B0 File Offset: 0x0005F4B0
			internal CustomRestriction(Expression expression)
			{
				this._expression = expression;
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x000612C0 File Offset: 0x0005F4C0
			public override bool Equals(object obj)
			{
				BindingRestrictions.CustomRestriction customRestriction = obj as BindingRestrictions.CustomRestriction;
				return customRestriction != null && customRestriction._expression == this._expression;
			}

			// Token: 0x06001A8C RID: 6796 RVA: 0x000612E7 File Offset: 0x0005F4E7
			public override int GetHashCode()
			{
				return 1073741824 ^ this._expression.GetHashCode();
			}

			// Token: 0x06001A8D RID: 6797 RVA: 0x000612FA File Offset: 0x0005F4FA
			internal override Expression GetExpression()
			{
				return this._expression;
			}

			// Token: 0x04000E35 RID: 3637
			private readonly Expression _expression;
		}

		// Token: 0x02000310 RID: 784
		private sealed class TypeRestriction : BindingRestrictions
		{
			// Token: 0x06001A8E RID: 6798 RVA: 0x00061302 File Offset: 0x0005F502
			internal TypeRestriction(Expression parameter, Type type)
			{
				this._expression = parameter;
				this._type = type;
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x00061318 File Offset: 0x0005F518
			public override bool Equals(object obj)
			{
				BindingRestrictions.TypeRestriction typeRestriction = obj as BindingRestrictions.TypeRestriction;
				return typeRestriction != null && TypeUtils.AreEquivalent(typeRestriction._type, this._type) && typeRestriction._expression == this._expression;
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x00061352 File Offset: 0x0005F552
			public override int GetHashCode()
			{
				return 268435456 ^ this._expression.GetHashCode() ^ this._type.GetHashCode();
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x00061371 File Offset: 0x0005F571
			internal override Expression GetExpression()
			{
				return Expression.TypeEqual(this._expression, this._type);
			}

			// Token: 0x04000E36 RID: 3638
			private readonly Expression _expression;

			// Token: 0x04000E37 RID: 3639
			private readonly Type _type;
		}

		// Token: 0x02000311 RID: 785
		private sealed class InstanceRestriction : BindingRestrictions
		{
			// Token: 0x06001A92 RID: 6802 RVA: 0x00061384 File Offset: 0x0005F584
			internal InstanceRestriction(Expression parameter, object instance)
			{
				this._expression = parameter;
				this._instance = instance;
			}

			// Token: 0x06001A93 RID: 6803 RVA: 0x0006139C File Offset: 0x0005F59C
			public override bool Equals(object obj)
			{
				BindingRestrictions.InstanceRestriction instanceRestriction = obj as BindingRestrictions.InstanceRestriction;
				return instanceRestriction != null && instanceRestriction._instance == this._instance && instanceRestriction._expression == this._expression;
			}

			// Token: 0x06001A94 RID: 6804 RVA: 0x000613D1 File Offset: 0x0005F5D1
			public override int GetHashCode()
			{
				return 536870912 ^ RuntimeHelpers.GetHashCode(this._instance) ^ this._expression.GetHashCode();
			}

			// Token: 0x06001A95 RID: 6805 RVA: 0x000613F0 File Offset: 0x0005F5F0
			internal override Expression GetExpression()
			{
				if (this._instance == null)
				{
					return Expression.Equal(Expression.Convert(this._expression, typeof(object)), Expression.Constant(null));
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				return Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.Property(Expression.Constant(new WeakReference(this._instance)), typeof(WeakReference).GetProperty("Target"))),
					Expression.AndAlso(Expression.NotEqual(parameterExpression, Expression.Constant(null)), Expression.Equal(Expression.Convert(this._expression, typeof(object)), parameterExpression))
				});
			}

			// Token: 0x04000E38 RID: 3640
			private readonly Expression _expression;

			// Token: 0x04000E39 RID: 3641
			private readonly object _instance;
		}

		// Token: 0x02000312 RID: 786
		private sealed class BindingRestrictionsProxy
		{
			// Token: 0x06001A96 RID: 6806 RVA: 0x000614AD File Offset: 0x0005F6AD
			public BindingRestrictionsProxy(BindingRestrictions node)
			{
				this._node = node;
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000614BC File Offset: 0x0005F6BC
			public bool IsEmpty
			{
				get
				{
					return this._node == BindingRestrictions.Empty;
				}
			}

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000614CB File Offset: 0x0005F6CB
			public Expression Test
			{
				get
				{
					return this._node.ToExpression();
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000614D8 File Offset: 0x0005F6D8
			public BindingRestrictions[] Restrictions
			{
				get
				{
					List<BindingRestrictions> list = new List<BindingRestrictions>();
					Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
					stack.Push(this._node);
					do
					{
						BindingRestrictions bindingRestrictions = stack.Pop();
						BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
						if (mergedRestriction != null)
						{
							stack.Push(mergedRestriction.Right);
							stack.Push(mergedRestriction.Left);
						}
						else
						{
							list.Add(bindingRestrictions);
						}
					}
					while (stack.Count > 0);
					return list.ToArray();
				}
			}

			// Token: 0x06001A9A RID: 6810 RVA: 0x0006153E File Offset: 0x0005F73E
			public override string ToString()
			{
				return this._node.DebugView;
			}

			// Token: 0x04000E3A RID: 3642
			private readonly BindingRestrictions _node;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	// Token: 0x020000C5 RID: 197
	[__DynamicallyInvokable]
	[Serializable]
	public class DynamicObject : IDynamicMetaObjectProvider
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00011A80 File Offset: 0x0000FC80
		[__DynamicallyInvokable]
		protected DynamicObject()
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00011A88 File Offset: 0x0000FC88
		[__DynamicallyInvokable]
		public virtual bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00011A8E File Offset: 0x0000FC8E
		[__DynamicallyInvokable]
		public virtual bool TrySetMember(SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00011A91 File Offset: 0x0000FC91
		[__DynamicallyInvokable]
		public virtual bool TryDeleteMember(DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00011A94 File Offset: 0x0000FC94
		[__DynamicallyInvokable]
		public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00011A9A File Offset: 0x0000FC9A
		[__DynamicallyInvokable]
		public virtual bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		[__DynamicallyInvokable]
		public virtual bool TryCreateInstance(CreateInstanceBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00011AA6 File Offset: 0x0000FCA6
		[__DynamicallyInvokable]
		public virtual bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00011AAC File Offset: 0x0000FCAC
		[__DynamicallyInvokable]
		public virtual bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00011AB2 File Offset: 0x0000FCB2
		[__DynamicallyInvokable]
		public virtual bool TryUnaryOperation(UnaryOperationBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		[__DynamicallyInvokable]
		public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00011ABE File Offset: 0x0000FCBE
		[__DynamicallyInvokable]
		public virtual bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00011AC1 File Offset: 0x0000FCC1
		[__DynamicallyInvokable]
		public virtual bool TryDeleteIndex(DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		[__DynamicallyInvokable]
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return new string[0];
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00011ACC File Offset: 0x0000FCCC
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicObject.MetaDynamic(parameter, this);
		}

		// Token: 0x02000313 RID: 787
		private sealed class MetaDynamic : DynamicMetaObject
		{
			// Token: 0x06001A9B RID: 6811 RVA: 0x0006154B File Offset: 0x0005F74B
			internal MetaDynamic(Expression expression, DynamicObject value) : base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x06001A9C RID: 6812 RVA: 0x0006155A File Offset: 0x0005F75A
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				return this.Value.GetDynamicMemberNames();
			}

			// Token: 0x06001A9D RID: 6813 RVA: 0x00061568 File Offset: 0x0005F768
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				if (this.IsOverridden("TryGetMember"))
				{
					return this.CallMethodWithResult("TryGetMember", binder, DynamicObject.MetaDynamic.NoArgs, (DynamicMetaObject e) => binder.FallbackGetMember(this, e));
				}
				return base.BindGetMember(binder);
			}

			// Token: 0x06001A9E RID: 6814 RVA: 0x000615C8 File Offset: 0x0005F7C8
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				if (this.IsOverridden("TrySetMember"))
				{
					return this.CallMethodReturnLast("TrySetMember", binder, DynamicObject.MetaDynamic.NoArgs, value.Expression, (DynamicMetaObject e) => binder.FallbackSetMember(this, value, e));
				}
				return base.BindSetMember(binder, value);
			}

			// Token: 0x06001A9F RID: 6815 RVA: 0x00061640 File Offset: 0x0005F840
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				if (this.IsOverridden("TryDeleteMember"))
				{
					return this.CallMethodNoResult("TryDeleteMember", binder, DynamicObject.MetaDynamic.NoArgs, (DynamicMetaObject e) => binder.FallbackDeleteMember(this, e));
				}
				return base.BindDeleteMember(binder);
			}

			// Token: 0x06001AA0 RID: 6816 RVA: 0x000616A0 File Offset: 0x0005F8A0
			public override DynamicMetaObject BindConvert(ConvertBinder binder)
			{
				if (this.IsOverridden("TryConvert"))
				{
					return this.CallMethodWithResult("TryConvert", binder, DynamicObject.MetaDynamic.NoArgs, (DynamicMetaObject e) => binder.FallbackConvert(this, e));
				}
				return base.BindConvert(binder);
			}

			// Token: 0x06001AA1 RID: 6817 RVA: 0x00061700 File Offset: 0x0005F900
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				DynamicObject.MetaDynamic.Fallback fallback = (DynamicMetaObject e) => binder.FallbackInvokeMember(this, args, e);
				DynamicMetaObject errorSuggestion = this.BuildCallMethodWithResult("TryInvokeMember", binder, DynamicMetaObject.GetExpressions(args), this.BuildCallMethodWithResult("TryGetMember", new DynamicObject.MetaDynamic.GetBinderAdapter(binder), DynamicObject.MetaDynamic.NoArgs, fallback(null), (DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
				return fallback(errorSuggestion);
			}

			// Token: 0x06001AA2 RID: 6818 RVA: 0x00061788 File Offset: 0x0005F988
			public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
			{
				if (this.IsOverridden("TryCreateInstance"))
				{
					return this.CallMethodWithResult("TryCreateInstance", binder, DynamicMetaObject.GetExpressions(args), (DynamicMetaObject e) => binder.FallbackCreateInstance(this, args, e));
				}
				return base.BindCreateInstance(binder, args);
			}

			// Token: 0x06001AA3 RID: 6819 RVA: 0x000617F8 File Offset: 0x0005F9F8
			public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
			{
				if (this.IsOverridden("TryInvoke"))
				{
					return this.CallMethodWithResult("TryInvoke", binder, DynamicMetaObject.GetExpressions(args), (DynamicMetaObject e) => binder.FallbackInvoke(this, args, e));
				}
				return base.BindInvoke(binder, args);
			}

			// Token: 0x06001AA4 RID: 6820 RVA: 0x00061868 File Offset: 0x0005FA68
			public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
			{
				if (this.IsOverridden("TryBinaryOperation"))
				{
					return this.CallMethodWithResult("TryBinaryOperation", binder, DynamicMetaObject.GetExpressions(new DynamicMetaObject[]
					{
						arg
					}), (DynamicMetaObject e) => binder.FallbackBinaryOperation(this, arg, e));
				}
				return base.BindBinaryOperation(binder, arg);
			}

			// Token: 0x06001AA5 RID: 6821 RVA: 0x000618E4 File Offset: 0x0005FAE4
			public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
			{
				if (this.IsOverridden("TryUnaryOperation"))
				{
					return this.CallMethodWithResult("TryUnaryOperation", binder, DynamicObject.MetaDynamic.NoArgs, (DynamicMetaObject e) => binder.FallbackUnaryOperation(this, e));
				}
				return base.BindUnaryOperation(binder);
			}

			// Token: 0x06001AA6 RID: 6822 RVA: 0x00061944 File Offset: 0x0005FB44
			public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
			{
				if (this.IsOverridden("TryGetIndex"))
				{
					return this.CallMethodWithResult("TryGetIndex", binder, DynamicMetaObject.GetExpressions(indexes), (DynamicMetaObject e) => binder.FallbackGetIndex(this, indexes, e));
				}
				return base.BindGetIndex(binder, indexes);
			}

			// Token: 0x06001AA7 RID: 6823 RVA: 0x000619B4 File Offset: 0x0005FBB4
			public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
			{
				if (this.IsOverridden("TrySetIndex"))
				{
					return this.CallMethodReturnLast("TrySetIndex", binder, DynamicMetaObject.GetExpressions(indexes), value.Expression, (DynamicMetaObject e) => binder.FallbackSetIndex(this, indexes, value, e));
				}
				return base.BindSetIndex(binder, indexes, value);
			}

			// Token: 0x06001AA8 RID: 6824 RVA: 0x00061A3C File Offset: 0x0005FC3C
			public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
			{
				if (this.IsOverridden("TryDeleteIndex"))
				{
					return this.CallMethodNoResult("TryDeleteIndex", binder, DynamicMetaObject.GetExpressions(indexes), (DynamicMetaObject e) => binder.FallbackDeleteIndex(this, indexes, e));
				}
				return base.BindDeleteIndex(binder, indexes);
			}

			// Token: 0x06001AA9 RID: 6825 RVA: 0x00061AAC File Offset: 0x0005FCAC
			private static Expression[] GetConvertedArgs(params Expression[] args)
			{
				ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>(args.Length);
				for (int i = 0; i < args.Length; i++)
				{
					readOnlyCollectionBuilder.Add(Expression.Convert(args[i], typeof(object)));
				}
				return readOnlyCollectionBuilder.ToArray();
			}

			// Token: 0x06001AAA RID: 6826 RVA: 0x00061AF0 File Offset: 0x0005FCF0
			private static Expression ReferenceArgAssign(Expression callArgs, Expression[] args)
			{
				ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = null;
				for (int i = 0; i < args.Length; i++)
				{
					ContractUtils.Requires(args[i] is ParameterExpression);
					if (((ParameterExpression)args[i]).IsByRef)
					{
						if (readOnlyCollectionBuilder == null)
						{
							readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>();
						}
						readOnlyCollectionBuilder.Add(Expression.Assign(args[i], Expression.Convert(Expression.ArrayIndex(callArgs, Expression.Constant(i)), args[i].Type)));
					}
				}
				if (readOnlyCollectionBuilder != null)
				{
					return Expression.Block(readOnlyCollectionBuilder);
				}
				return Expression.Empty();
			}

			// Token: 0x06001AAB RID: 6827 RVA: 0x00061B70 File Offset: 0x0005FD70
			private static Expression[] BuildCallArgs(DynamicMetaObjectBinder binder, Expression[] parameters, Expression arg0, Expression arg1)
			{
				if (parameters != DynamicObject.MetaDynamic.NoArgs)
				{
					if (arg1 == null)
					{
						return new Expression[]
						{
							DynamicObject.MetaDynamic.Constant(binder),
							arg0
						};
					}
					return new Expression[]
					{
						DynamicObject.MetaDynamic.Constant(binder),
						arg0,
						arg1
					};
				}
				else
				{
					if (arg1 == null)
					{
						return new Expression[]
						{
							DynamicObject.MetaDynamic.Constant(binder)
						};
					}
					return new Expression[]
					{
						DynamicObject.MetaDynamic.Constant(binder),
						arg1
					};
				}
			}

			// Token: 0x06001AAC RID: 6828 RVA: 0x00061BDC File Offset: 0x0005FDDC
			private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
			{
				Type type = binder.GetType();
				while (!type.IsVisible)
				{
					type = type.BaseType;
				}
				return Expression.Constant(binder, type);
			}

			// Token: 0x06001AAD RID: 6829 RVA: 0x00061C08 File Offset: 0x0005FE08
			private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback fallback)
			{
				return this.CallMethodWithResult(methodName, binder, args, fallback, null);
			}

			// Token: 0x06001AAE RID: 6830 RVA: 0x00061C18 File Offset: 0x0005FE18
			private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback fallback, DynamicObject.MetaDynamic.Fallback fallbackInvoke)
			{
				DynamicMetaObject fallbackResult = fallback(null);
				DynamicMetaObject errorSuggestion = this.BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
				return fallback(errorSuggestion);
			}

			// Token: 0x06001AAF RID: 6831 RVA: 0x00061C44 File Offset: 0x0005FE44
			private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicMetaObject fallbackResult, DynamicObject.MetaDynamic.Fallback fallbackInvoke)
			{
				if (!this.IsOverridden(methodName))
				{
					return fallbackResult;
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				ParameterExpression parameterExpression2 = (methodName != "TryBinaryOperation") ? Expression.Parameter(typeof(object[]), null) : Expression.Parameter(typeof(object), null);
				Expression[] convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (binder.ReturnType != typeof(object))
				{
					UnaryExpression ifTrue = Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType);
					string value = Strings.DynamicObjectResultNotAssignable("{0}", this.Value.GetType(), binder.GetType(), binder.ReturnType);
					Expression test;
					if (binder.ReturnType.IsValueType && Nullable.GetUnderlyingType(binder.ReturnType) == null)
					{
						test = Expression.TypeIs(dynamicMetaObject.Expression, binder.ReturnType);
					}
					else
					{
						test = Expression.OrElse(Expression.Equal(dynamicMetaObject.Expression, Expression.Constant(null)), Expression.TypeIs(dynamicMetaObject.Expression, binder.ReturnType));
					}
					ConditionalExpression expression = Expression.Condition(test, ifTrue, Expression.Throw(Expression.New(typeof(InvalidCastException).GetConstructor(new Type[]
					{
						typeof(string)
					}), new Expression[]
					{
						Expression.Call(typeof(string).GetMethod("Format", new Type[]
						{
							typeof(string),
							typeof(object[])
						}), Expression.Constant(value), Expression.NewArrayInit(typeof(object), new Expression[]
						{
							Expression.Condition(Expression.Equal(dynamicMetaObject.Expression, Expression.Constant(null)), Expression.Constant("null"), Expression.Call(dynamicMetaObject.Expression, typeof(object).GetMethod("GetType")), typeof(object))
						}))
					}), binder.ReturnType), binder.ReturnType);
					dynamicMetaObject = new DynamicMetaObject(expression, dynamicMetaObject.Restrictions);
				}
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
				}
				return new DynamicMetaObject(Expression.Block(new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				}, new Expression[]
				{
					(methodName != "TryBinaryOperation") ? Expression.Assign(parameterExpression2, Expression.NewArrayInit(typeof(object), convertedArgs)) : Expression.Assign(parameterExpression2, convertedArgs[0]),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), typeof(DynamicObject).GetMethod(methodName), DynamicObject.MetaDynamic.BuildCallArgs(binder, args, parameterExpression2, parameterExpression)), Expression.Block((methodName != "TryBinaryOperation") ? DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression2, args) : Expression.Empty(), dynamicMetaObject.Expression), fallbackResult.Expression, binder.ReturnType)
				}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
			}

			// Token: 0x06001AB0 RID: 6832 RVA: 0x00061F38 File Offset: 0x00060138
			private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, Expression[] args, Expression value, DynamicObject.MetaDynamic.Fallback fallback)
			{
				DynamicMetaObject dynamicMetaObject = fallback(null);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), null);
				Expression[] convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.Block(new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				}, new Expression[]
				{
					Expression.Assign(parameterExpression2, Expression.NewArrayInit(typeof(object), convertedArgs)),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), typeof(DynamicObject).GetMethod(methodName), DynamicObject.MetaDynamic.BuildCallArgs(binder, args, parameterExpression2, Expression.Assign(parameterExpression, Expression.Convert(value, typeof(object))))), Expression.Block(DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression2, args), parameterExpression), dynamicMetaObject.Expression, typeof(object))
				}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
				return fallback(errorSuggestion);
			}

			// Token: 0x06001AB1 RID: 6833 RVA: 0x0006202C File Offset: 0x0006022C
			private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback fallback)
			{
				DynamicMetaObject dynamicMetaObject = fallback(null);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), null);
				Expression[] convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.NewArrayInit(typeof(object), convertedArgs)),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), typeof(DynamicObject).GetMethod(methodName), DynamicObject.MetaDynamic.BuildCallArgs(binder, args, parameterExpression, null)), Expression.Block(DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression, args), Expression.Empty()), dynamicMetaObject.Expression, typeof(void))
				}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
				return fallback(errorSuggestion);
			}

			// Token: 0x06001AB2 RID: 6834 RVA: 0x000620F4 File Offset: 0x000602F4
			private bool IsOverridden(string method)
			{
				MemberInfo[] member = this.Value.GetType().GetMember(method, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public);
				foreach (MethodInfo methodInfo in member)
				{
					if (methodInfo.DeclaringType != typeof(DynamicObject) && methodInfo.GetBaseDefinition().DeclaringType == typeof(DynamicObject))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001AB3 RID: 6835 RVA: 0x00062165 File Offset: 0x00060365
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x06001AB4 RID: 6836 RVA: 0x0006216D File Offset: 0x0006036D
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, typeof(DynamicObject)))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, typeof(DynamicObject));
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x000621A7 File Offset: 0x000603A7
			private new DynamicObject Value
			{
				get
				{
					return (DynamicObject)base.Value;
				}
			}

			// Token: 0x04000E3B RID: 3643
			private static readonly Expression[] NoArgs = new Expression[0];

			// Token: 0x0200047C RID: 1148
			// (Invoke) Token: 0x06002020 RID: 8224
			private delegate DynamicMetaObject Fallback(DynamicMetaObject errorSuggestion);

			// Token: 0x0200047D RID: 1149
			private sealed class GetBinderAdapter : GetMemberBinder
			{
				// Token: 0x06002023 RID: 8227 RVA: 0x0007027B File Offset: 0x0006E47B
				internal GetBinderAdapter(InvokeMemberBinder binder) : base(binder.Name, binder.IgnoreCase)
				{
				}

				// Token: 0x06002024 RID: 8228 RVA: 0x0007028F File Offset: 0x0006E48F
				public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
				{
					throw new NotSupportedException();
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;
using System.Reflection;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200013B RID: 315
	[__DynamicallyInvokable]
	public class CallSite<T> : CallSite where T : class
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00024AA2 File Offset: 0x00022CA2
		[__DynamicallyInvokable]
		public T Update
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._match)
				{
					return CallSite<T>._CachedNoMatch;
				}
				return CallSite<T>._CachedUpdate;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00024AB9 File Offset: 0x00022CB9
		private CallSite(CallSiteBinder binder) : base(binder)
		{
			this.Target = this.GetUpdateDelegate();
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00024ACE File Offset: 0x00022CCE
		private CallSite() : base(null)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00024AD7 File Offset: 0x00022CD7
		internal CallSite<T> CreateMatchMaker()
		{
			return new CallSite<T>();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00024ADE File Offset: 0x00022CDE
		[__DynamicallyInvokable]
		public static CallSite<T> Create(CallSiteBinder binder)
		{
			if (!typeof(T).IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			return new CallSite<T>(binder);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00024B07 File Offset: 0x00022D07
		private T GetUpdateDelegate()
		{
			return this.GetUpdateDelegate(ref CallSite<T>._CachedUpdate);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00024B14 File Offset: 0x00022D14
		private T GetUpdateDelegate(ref T addr)
		{
			if (addr == null)
			{
				addr = this.MakeUpdateDelegate();
			}
			return addr;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00024B38 File Offset: 0x00022D38
		private void ClearRuleCache()
		{
			base.Binder.GetRuleCache<T>();
			Dictionary<Type, object> cache = base.Binder.Cache;
			if (cache != null)
			{
				Dictionary<Type, object> obj = cache;
				lock (obj)
				{
					cache.Clear();
				}
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00024B90 File Offset: 0x00022D90
		internal void AddRule(T newRule)
		{
			T[] rules = this.Rules;
			if (rules == null)
			{
				this.Rules = new T[]
				{
					newRule
				};
				return;
			}
			T[] array;
			if (rules.Length < 9)
			{
				array = new T[rules.Length + 1];
				Array.Copy(rules, 0, array, 1, rules.Length);
			}
			else
			{
				array = new T[10];
				Array.Copy(rules, 0, array, 1, 9);
			}
			array[0] = newRule;
			this.Rules = array;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00024C00 File Offset: 0x00022E00
		internal void MoveRule(int i)
		{
			T[] rules = this.Rules;
			T t = rules[i];
			rules[i] = rules[i - 1];
			rules[i - 1] = rules[i - 2];
			rules[i - 2] = t;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00024C48 File Offset: 0x00022E48
		internal T MakeUpdateDelegate()
		{
			Type typeFromHandle = typeof(T);
			MethodInfo method = typeFromHandle.GetMethod("Invoke");
			Type[] array;
			if (typeFromHandle.IsGenericType && CallSite<T>.IsSimpleSignature(method, out array))
			{
				MethodInfo methodInfo = null;
				MethodInfo methodInfo2 = null;
				if (method.ReturnType == typeof(void))
				{
					if (typeFromHandle == DelegateHelpers.GetActionType(array.AddFirst(typeof(CallSite))))
					{
						methodInfo = typeof(UpdateDelegates).GetMethod("UpdateAndExecuteVoid" + array.Length.ToString(), BindingFlags.Static | BindingFlags.NonPublic);
						methodInfo2 = typeof(UpdateDelegates).GetMethod("NoMatchVoid" + array.Length.ToString(), BindingFlags.Static | BindingFlags.NonPublic);
					}
				}
				else if (typeFromHandle == DelegateHelpers.GetFuncType(array.AddFirst(typeof(CallSite))))
				{
					methodInfo = typeof(UpdateDelegates).GetMethod("UpdateAndExecute" + (array.Length - 1).ToString(), BindingFlags.Static | BindingFlags.NonPublic);
					methodInfo2 = typeof(UpdateDelegates).GetMethod("NoMatch" + (array.Length - 1).ToString(), BindingFlags.Static | BindingFlags.NonPublic);
				}
				if (methodInfo != null)
				{
					CallSite<T>._CachedNoMatch = (T)((object)CallSite<T>.CreateDelegateHelper(typeFromHandle, methodInfo2.MakeGenericMethod(array)));
					return (T)((object)CallSite<T>.CreateDelegateHelper(typeFromHandle, methodInfo.MakeGenericMethod(array)));
				}
			}
			CallSite<T>._CachedNoMatch = this.CreateCustomNoMatchDelegate(method);
			return this.CreateCustomUpdateDelegate(method);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00024DD0 File Offset: 0x00022FD0
		private static Delegate CreateDelegateHelper(Type delegateType, MethodInfo method)
		{
			return Delegate.CreateDelegate(delegateType, method);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00024DDC File Offset: 0x00022FDC
		private static bool IsSimpleSignature(MethodInfo invoke, out Type[] sig)
		{
			ParameterInfo[] parametersCached = invoke.GetParametersCached();
			ContractUtils.Requires(parametersCached.Length != 0 && parametersCached[0].ParameterType == typeof(CallSite), "T");
			Type[] array = new Type[(invoke.ReturnType != typeof(void)) ? parametersCached.Length : (parametersCached.Length - 1)];
			bool result = true;
			for (int i = 1; i < parametersCached.Length; i++)
			{
				ParameterInfo parameterInfo = parametersCached[i];
				if (parameterInfo.IsByRefParameter())
				{
					result = false;
				}
				array[i - 1] = parameterInfo.ParameterType;
			}
			if (invoke.ReturnType != typeof(void))
			{
				array[array.Length - 1] = invoke.ReturnType;
			}
			sig = array;
			return result;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00024E94 File Offset: 0x00023094
		private T CreateCustomNoMatchDelegate(MethodInfo invoke)
		{
			ParameterExpression[] array = invoke.GetParametersCached().Map((ParameterInfo p) => Expression.Parameter(p.ParameterType, p.Name));
			return Expression.Lambda<T>(Expression.Block(Expression.Call(typeof(CallSiteOps).GetMethod("SetNotMatched"), array.First<ParameterExpression>()), Expression.Default(invoke.GetReturnType())), array).Compile();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00024F08 File Offset: 0x00023108
		private T CreateCustomUpdateDelegate(MethodInfo invoke)
		{
			List<Expression> list = new List<Expression>();
			List<ParameterExpression> list2 = new List<ParameterExpression>();
			ParameterExpression[] array = invoke.GetParametersCached().Map((ParameterInfo p) => Expression.Parameter(p.ParameterType, p.Name));
			LabelTarget labelTarget = Expression.Label(invoke.GetReturnType());
			Type[] typeArguments = new Type[]
			{
				typeof(T)
			};
			ParameterExpression parameterExpression = array[0];
			ParameterExpression[] collection = array.RemoveFirst<ParameterExpression>();
			ParameterExpression parameterExpression2 = Expression.Variable(typeof(CallSite<T>), "this");
			list2.Add(parameterExpression2);
			list.Add(Expression.Assign(parameterExpression2, Expression.Convert(parameterExpression, parameterExpression2.Type)));
			ParameterExpression parameterExpression3 = Expression.Variable(typeof(T[]), "applicable");
			list2.Add(parameterExpression3);
			ParameterExpression parameterExpression4 = Expression.Variable(typeof(T), "rule");
			list2.Add(parameterExpression4);
			ParameterExpression parameterExpression5 = Expression.Variable(typeof(T), "originalRule");
			list2.Add(parameterExpression5);
			list.Add(Expression.Assign(parameterExpression5, Expression.Field(parameterExpression2, "Target")));
			ParameterExpression parameterExpression6 = null;
			if (labelTarget.Type != typeof(void))
			{
				list2.Add(parameterExpression6 = Expression.Variable(labelTarget.Type, "result"));
			}
			ParameterExpression parameterExpression7 = Expression.Variable(typeof(int), "count");
			list2.Add(parameterExpression7);
			ParameterExpression parameterExpression8 = Expression.Variable(typeof(int), "index");
			list2.Add(parameterExpression8);
			list.Add(Expression.Assign(parameterExpression, Expression.Call(typeof(CallSiteOps), "CreateMatchmaker", typeArguments, new Expression[]
			{
				parameterExpression2
			})));
			Expression test = Expression.Call(typeof(CallSiteOps).GetMethod("GetMatch"), parameterExpression);
			Expression expression = Expression.Call(typeof(CallSiteOps).GetMethod("ClearMatch"), parameterExpression);
			MethodCallExpression arg = Expression.Call(typeof(CallSiteOps), "UpdateRules", typeArguments, new Expression[]
			{
				parameterExpression2,
				parameterExpression8
			});
			Expression expression3;
			if (labelTarget.Type == typeof(void))
			{
				Expression expression2 = parameterExpression4;
				Expression[] list3 = array;
				expression3 = Expression.Block(Expression.Invoke(expression2, new TrueReadOnlyCollection<Expression>(list3)), Expression.IfThen(test, Expression.Block(arg, Expression.Return(labelTarget))));
			}
			else
			{
				Expression left = parameterExpression6;
				Expression expression4 = parameterExpression4;
				Expression[] list3 = array;
				expression3 = Expression.Block(Expression.Assign(left, Expression.Invoke(expression4, new TrueReadOnlyCollection<Expression>(list3))), Expression.IfThen(test, Expression.Block(arg, Expression.Return(labelTarget, parameterExpression6))));
			}
			Expression arg2 = Expression.Assign(parameterExpression4, Expression.ArrayAccess(parameterExpression3, new Expression[]
			{
				parameterExpression8
			}));
			LabelTarget labelTarget2 = Expression.Label();
			ConditionalExpression arg3 = Expression.IfThen(Expression.Equal(parameterExpression8, parameterExpression7), Expression.Break(labelTarget2));
			UnaryExpression unaryExpression = Expression.PreIncrementAssign(parameterExpression8);
			list.Add(Expression.IfThen(Expression.NotEqual(Expression.Assign(parameterExpression3, Expression.Call(typeof(CallSiteOps), "GetRules", typeArguments, new Expression[]
			{
				parameterExpression2
			})), Expression.Constant(null, parameterExpression3.Type)), Expression.Block(Expression.Assign(parameterExpression7, Expression.ArrayLength(parameterExpression3)), Expression.Assign(parameterExpression8, Expression.Constant(0)), Expression.Loop(Expression.Block(arg3, arg2, Expression.IfThen(Expression.NotEqual(Expression.Convert(parameterExpression4, typeof(object)), Expression.Convert(parameterExpression5, typeof(object))), Expression.Block(Expression.Assign(Expression.Field(parameterExpression2, "Target"), parameterExpression4), expression3, expression)), unaryExpression), labelTarget2, null))));
			ParameterExpression parameterExpression9 = Expression.Variable(typeof(RuleCache<T>), "cache");
			list2.Add(parameterExpression9);
			list.Add(Expression.Assign(parameterExpression9, Expression.Call(typeof(CallSiteOps), "GetRuleCache", typeArguments, new Expression[]
			{
				parameterExpression2
			})));
			list.Add(Expression.Assign(parameterExpression3, Expression.Call(typeof(CallSiteOps), "GetCachedRules", typeArguments, new Expression[]
			{
				parameterExpression9
			})));
			if (labelTarget.Type == typeof(void))
			{
				Expression expression5 = parameterExpression4;
				Expression[] list3 = array;
				expression3 = Expression.Block(Expression.Invoke(expression5, new TrueReadOnlyCollection<Expression>(list3)), Expression.IfThen(test, Expression.Return(labelTarget)));
			}
			else
			{
				Expression left2 = parameterExpression6;
				Expression expression6 = parameterExpression4;
				Expression[] list3 = array;
				expression3 = Expression.Block(Expression.Assign(left2, Expression.Invoke(expression6, new TrueReadOnlyCollection<Expression>(list3))), Expression.IfThen(test, Expression.Return(labelTarget, parameterExpression6)));
			}
			TryExpression arg4 = Expression.TryFinally(expression3, Expression.IfThen(test, Expression.Block(Expression.Call(typeof(CallSiteOps), "AddRule", typeArguments, new Expression[]
			{
				parameterExpression2,
				parameterExpression4
			}), Expression.Call(typeof(CallSiteOps), "MoveRule", typeArguments, new Expression[]
			{
				parameterExpression9,
				parameterExpression4,
				parameterExpression8
			}))));
			arg2 = Expression.Assign(Expression.Field(parameterExpression2, "Target"), Expression.Assign(parameterExpression4, Expression.ArrayAccess(parameterExpression3, new Expression[]
			{
				parameterExpression8
			})));
			list.Add(Expression.Assign(parameterExpression8, Expression.Constant(0)));
			list.Add(Expression.Assign(parameterExpression7, Expression.ArrayLength(parameterExpression3)));
			list.Add(Expression.Loop(Expression.Block(arg3, arg2, arg4, expression, unaryExpression), labelTarget2, null));
			list.Add(Expression.Assign(parameterExpression4, Expression.Constant(null, parameterExpression4.Type)));
			ParameterExpression parameterExpression10 = Expression.Variable(typeof(object[]), "args");
			list2.Add(parameterExpression10);
			list.Add(Expression.Assign(parameterExpression10, Expression.NewArrayInit(typeof(object), collection.Map((ParameterExpression p) => CallSite<T>.Convert(p, typeof(object))))));
			Expression arg5 = Expression.Assign(Expression.Field(parameterExpression2, "Target"), parameterExpression5);
			arg2 = Expression.Assign(Expression.Field(parameterExpression2, "Target"), Expression.Assign(parameterExpression4, Expression.Call(typeof(CallSiteOps), "Bind", typeArguments, new Expression[]
			{
				Expression.Property(parameterExpression2, "Binder"),
				parameterExpression2,
				parameterExpression10
			})));
			arg4 = Expression.TryFinally(expression3, Expression.IfThen(test, Expression.Call(typeof(CallSiteOps), "AddRule", typeArguments, new Expression[]
			{
				parameterExpression2,
				parameterExpression4
			})));
			list.Add(Expression.Loop(Expression.Block(arg5, arg2, arg4, expression), null, null));
			list.Add(Expression.Default(labelTarget.Type));
			Expression<T> expression7 = Expression.Lambda<T>(Expression.Label(labelTarget, Expression.Block(new ReadOnlyCollection<ParameterExpression>(list2), new ReadOnlyCollection<Expression>(list))), "CallSite.Target", true, new ReadOnlyCollection<ParameterExpression>(array));
			return expression7.Compile();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000255D7 File Offset: 0x000237D7
		private static Expression Convert(Expression arg, Type type)
		{
			if (TypeUtils.AreReferenceAssignable(type, arg.Type))
			{
				return arg;
			}
			return Expression.Convert(arg, type);
		}

		// Token: 0x04000765 RID: 1893
		[__DynamicallyInvokable]
		public T Target;

		// Token: 0x04000766 RID: 1894
		internal T[] Rules;

		// Token: 0x04000767 RID: 1895
		private static T _CachedUpdate;

		// Token: 0x04000768 RID: 1896
		private static volatile T _CachedNoMatch;

		// Token: 0x04000769 RID: 1897
		private const int MaxRules = 10;
	}
}

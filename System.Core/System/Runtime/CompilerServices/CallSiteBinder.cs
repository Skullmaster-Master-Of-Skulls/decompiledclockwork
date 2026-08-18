using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200013C RID: 316
	[__DynamicallyInvokable]
	public abstract class CallSiteBinder
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x000255F0 File Offset: 0x000237F0
		[__DynamicallyInvokable]
		protected CallSiteBinder()
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000255F8 File Offset: 0x000237F8
		[__DynamicallyInvokable]
		public static LabelTarget UpdateLabel
		{
			[__DynamicallyInvokable]
			get
			{
				return CallSiteBinder._updateLabel;
			}
		}

		// Token: 0x06000A41 RID: 2625
		[__DynamicallyInvokable]
		public abstract Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel);

		// Token: 0x06000A42 RID: 2626 RVA: 0x00025600 File Offset: 0x00023800
		[__DynamicallyInvokable]
		public virtual T BindDelegate<T>(CallSite<T> site, object[] args) where T : class
		{
			return default(T);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00025618 File Offset: 0x00023818
		internal T BindCore<T>(CallSite<T> site, object[] args) where T : class
		{
			T t = this.BindDelegate<T>(site, args);
			if (t != null)
			{
				return t;
			}
			CallSiteBinder.LambdaSignature<T> instance = CallSiteBinder.LambdaSignature<T>.Instance;
			Expression expression = this.Bind(args, instance.Parameters, instance.ReturnLabel);
			if (expression == null)
			{
				throw Error.NoOrInvalidRuleProduced();
			}
			if (!AppDomain.CurrentDomain.IsHomogenous)
			{
				throw Error.HomogenousAppDomainRequired();
			}
			Expression<T> expression2 = CallSiteBinder.Stitch<T>(expression, instance);
			T t2 = expression2.Compile();
			this.CacheTarget<T>(t2);
			return t2;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00025687 File Offset: 0x00023887
		[__DynamicallyInvokable]
		protected void CacheTarget<T>(T target) where T : class
		{
			this.GetRuleCache<T>().AddRule(target);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00025698 File Offset: 0x00023898
		private static Expression<T> Stitch<T>(Expression binding, CallSiteBinder.LambdaSignature<T> signature) where T : class
		{
			Type typeFromHandle = typeof(CallSite<T>);
			ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>(3);
			readOnlyCollectionBuilder.Add(binding);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(CallSite), "$site");
			ParameterExpression[] array = signature.Parameters.AddFirst(parameterExpression);
			Expression item = Expression.Label(CallSiteBinder.UpdateLabel);
			readOnlyCollectionBuilder.Add(item);
			ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder2 = readOnlyCollectionBuilder;
			LabelTarget returnLabel = signature.ReturnLabel;
			Expression test = Expression.Call(typeof(CallSiteOps).GetMethod("SetNotMatched"), array.First<ParameterExpression>());
			Expression ifTrue = Expression.Default(signature.ReturnLabel.Type);
			Expression expression = Expression.Property(Expression.Convert(parameterExpression, typeFromHandle), typeof(CallSite<T>).GetProperty("Update"));
			Expression[] list = array;
			readOnlyCollectionBuilder2.Add(Expression.Label(returnLabel, Expression.Condition(test, ifTrue, Expression.Invoke(expression, new TrueReadOnlyCollection<Expression>(list)))));
			return new Expression<T>(Expression.Block(readOnlyCollectionBuilder), "CallSite.Target", true, new TrueReadOnlyCollection<ParameterExpression>(array));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00025780 File Offset: 0x00023980
		internal RuleCache<T> GetRuleCache<T>() where T : class
		{
			if (this.Cache == null)
			{
				Interlocked.CompareExchange<Dictionary<Type, object>>(ref this.Cache, new Dictionary<Type, object>(), null);
			}
			Dictionary<Type, object> cache = this.Cache;
			Dictionary<Type, object> obj = cache;
			object obj2;
			lock (obj)
			{
				if (!cache.TryGetValue(typeof(T), out obj2))
				{
					obj2 = (cache[typeof(T)] = new RuleCache<T>());
				}
			}
			return obj2 as RuleCache<T>;
		}

		// Token: 0x0400076A RID: 1898
		private static readonly LabelTarget _updateLabel = Expression.Label("CallSiteBinder.UpdateLabel");

		// Token: 0x0400076B RID: 1899
		internal Dictionary<Type, object> Cache;

		// Token: 0x02000369 RID: 873
		private sealed class LambdaSignature<T> where T : class
		{
			// Token: 0x06001B88 RID: 7048 RVA: 0x000633D8 File Offset: 0x000615D8
			private LambdaSignature()
			{
				Type typeFromHandle = typeof(T);
				if (!typeFromHandle.IsSubclassOf(typeof(MulticastDelegate)))
				{
					throw Error.TypeParameterIsNotDelegate(typeFromHandle);
				}
				MethodInfo method = typeFromHandle.GetMethod("Invoke");
				ParameterInfo[] parametersCached = method.GetParametersCached();
				if (parametersCached[0].ParameterType != typeof(CallSite))
				{
					throw Error.FirstArgumentMustBeCallSite();
				}
				ParameterExpression[] array = new ParameterExpression[parametersCached.Length - 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Expression.Parameter(parametersCached[i + 1].ParameterType, "$arg" + i.ToString());
				}
				this.Parameters = new TrueReadOnlyCollection<ParameterExpression>(array);
				this.ReturnLabel = Expression.Label(method.GetReturnType());
			}

			// Token: 0x04000F8D RID: 3981
			internal static readonly CallSiteBinder.LambdaSignature<T> Instance = new CallSiteBinder.LambdaSignature<T>();

			// Token: 0x04000F8E RID: 3982
			internal readonly ReadOnlyCollection<ParameterExpression> Parameters;

			// Token: 0x04000F8F RID: 3983
			internal readonly LabelTarget ReturnLabel;
		}
	}
}

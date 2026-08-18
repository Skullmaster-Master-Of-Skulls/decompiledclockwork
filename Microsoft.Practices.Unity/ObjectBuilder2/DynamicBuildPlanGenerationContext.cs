using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000057 RID: 87
	public class DynamicBuildPlanGenerationContext
	{
		// Token: 0x06000172 RID: 370 RVA: 0x000054B1 File Offset: 0x000036B1
		public DynamicBuildPlanGenerationContext(Type typeToBuild)
		{
			this.typeToBuild = typeToBuild;
			this.contextParameter = Expression.Parameter(typeof(IBuilderContext), "context");
			this.buildPlanExpressions = new Queue<Expression>();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000054E5 File Offset: 0x000036E5
		public Type TypeToBuild
		{
			get
			{
				return this.typeToBuild;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000054ED File Offset: 0x000036ED
		public ParameterExpression ContextParameter
		{
			get
			{
				return this.contextParameter;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000054F5 File Offset: 0x000036F5
		public void AddToBuildPlan(Expression expression)
		{
			this.buildPlanExpressions.Enqueue(expression);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005504 File Offset: 0x00003704
		public Expression CreateParameterExpression(IDependencyResolverPolicy resolver, Type parameterType, Expression setOperationExpression)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
			ParameterExpression parameterExpression2 = Expression.Parameter(parameterType);
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}, new Expression[]
			{
				this.SaveCurrentOperationExpression(parameterExpression),
				setOperationExpression,
				Expression.Assign(parameterExpression2, this.GetResolveDependencyExpression(parameterType, resolver)),
				this.RestoreCurrentOperationExpression(parameterExpression),
				parameterExpression2
			});
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005572 File Offset: 0x00003772
		internal Expression GetExistingObjectExpression()
		{
			return Expression.MakeMemberAccess(this.ContextParameter, DynamicBuildPlanGenerationContext.GetBuildContextExistingObjectProperty);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005584 File Offset: 0x00003784
		internal Expression GetClearCurrentOperationExpression()
		{
			return Expression.Assign(Expression.Property(this.ContextParameter, typeof(IBuilderContext).GetTypeInfo().GetDeclaredProperty("CurrentOperation")), Expression.Constant(null));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000055B8 File Offset: 0x000037B8
		internal Expression GetResolveDependencyExpression(Type dependencyType, IDependencyResolverPolicy resolver)
		{
			return Expression.Convert(Expression.Call(Expression.Call(null, DynamicBuildPlanGenerationContext.GetResolverMethod, this.ContextParameter, Expression.Constant(dependencyType, typeof(Type)), Expression.Constant(resolver, typeof(IDependencyResolverPolicy))), DynamicBuildPlanGenerationContext.ResolveDependencyMethod, new Expression[]
			{
				this.ContextParameter
			}), dependencyType);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000565C File Offset: 0x0000385C
		internal DynamicBuildPlanMethod GetBuildMethod()
		{
			Func<IBuilderContext, object> planDelegate = (Func<IBuilderContext, object>)Expression.Lambda(Expression.Block(this.buildPlanExpressions.Concat(new Expression[]
			{
				this.GetExistingObjectExpression()
			})), new ParameterExpression[]
			{
				this.ContextParameter
			}).Compile();
			return delegate(IBuilderContext context)
			{
				try
				{
					context.Existing = planDelegate(context);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
			};
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000056C2 File Offset: 0x000038C2
		private Expression RestoreCurrentOperationExpression(ParameterExpression savedOperationExpression)
		{
			return Expression.Assign(Expression.MakeMemberAccess(this.ContextParameter, typeof(IBuilderContext).GetTypeInfo().GetDeclaredProperty("CurrentOperation")), savedOperationExpression);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000056EE File Offset: 0x000038EE
		private Expression SaveCurrentOperationExpression(ParameterExpression saveExpression)
		{
			return Expression.Assign(saveExpression, Expression.MakeMemberAccess(this.ContextParameter, typeof(IBuilderContext).GetTypeInfo().GetDeclaredProperty("CurrentOperation")));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000571A File Offset: 0x0000391A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dependencyType", Justification = "Obsolete method")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "resolverKey", Justification = "Obsolete method")]
		[Obsolete("Resolvers are no longer stored as policies.")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "context", Justification = "Obsolete method")]
		public static IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType, string resolverKey)
		{
			throw new NotSupportedException("This method is no longer used");
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005728 File Offset: 0x00003928
		public static IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType, IDependencyResolverPolicy resolver)
		{
			Guard.ArgumentNotNull(context, "context");
			IDependencyResolverPolicy overriddenResolver = context.GetOverriddenResolver(dependencyType);
			return overriddenResolver ?? resolver;
		}

		// Token: 0x04000052 RID: 82
		private readonly Type typeToBuild;

		// Token: 0x04000053 RID: 83
		private readonly ParameterExpression contextParameter;

		// Token: 0x04000054 RID: 84
		private readonly Queue<Expression> buildPlanExpressions;

		// Token: 0x04000055 RID: 85
		private static readonly MethodInfo ResolveDependencyMethod = StaticReflection.GetMethodInfo<IDependencyResolverPolicy>((IDependencyResolverPolicy r) => r.Resolve(null));

		// Token: 0x04000056 RID: 86
		private static readonly MethodInfo GetResolverMethod = StaticReflection.GetMethodInfo(() => DynamicBuildPlanGenerationContext.GetResolver(null, null, null));

		// Token: 0x04000057 RID: 87
		private static readonly MemberInfo GetBuildContextExistingObjectProperty = StaticReflection.GetMemberInfo<IBuilderContext, object>((IBuilderContext c) => c.Existing);
	}
}

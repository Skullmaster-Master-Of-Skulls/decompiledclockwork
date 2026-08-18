using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000006 RID: 6
	public class LazyDynamicMethodBuildPlanCreatorPolicy : IBuildPlanCreatorPolicy, IBuilderPolicy
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000211A File Offset: 0x0000031A
		public IBuildPlanPolicy CreatePlan(IBuilderContext context, NamedTypeBuildKey buildKey)
		{
			Guard.ArgumentNotNull(context, "context");
			Guard.ArgumentNotNull(buildKey, "buildKey");
			return new DynamicMethodBuildPlan(LazyDynamicMethodBuildPlanCreatorPolicy.CreateBuildPlanMethod(buildKey.Type));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		private static DynamicBuildPlanMethod CreateBuildPlanMethod(Type lazyType)
		{
			Type type = lazyType.GetTypeInfo().GenericTypeArguments[0];
			MethodInfo methodInfo;
			if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				methodInfo = LazyDynamicMethodBuildPlanCreatorPolicy.BuildResolveAllLazyMethod.MakeGenericMethod(new Type[]
				{
					type.GetTypeInfo().GenericTypeArguments[0]
				});
			}
			else
			{
				methodInfo = LazyDynamicMethodBuildPlanCreatorPolicy.BuildResolveLazyMethod.MakeGenericMethod(new Type[]
				{
					type
				});
			}
			return (DynamicBuildPlanMethod)methodInfo.CreateDelegate(typeof(DynamicBuildPlanMethod));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F0 File Offset: 0x000003F0
		private static void BuildResolveLazy<T>(IBuilderContext context)
		{
			if (context.Existing == null)
			{
				string name = context.BuildKey.Name;
				IUnityContainer container = context.NewBuildUp<IUnityContainer>();
				context.Existing = new Lazy<T>(() => container.Resolve(name, new ResolverOverride[0]));
			}
			DynamicMethodConstructorStrategy.SetPerBuildSingleton(context);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002260 File Offset: 0x00000460
		private static void BuildResolveAllLazy<T>(IBuilderContext context)
		{
			if (context.Existing == null)
			{
				IUnityContainer container = context.NewBuildUp<IUnityContainer>();
				context.Existing = new Lazy<IEnumerable<T>>(() => container.ResolveAll(new ResolverOverride[0]));
			}
			DynamicMethodConstructorStrategy.SetPerBuildSingleton(context);
		}

		// Token: 0x04000003 RID: 3
		private static readonly MethodInfo BuildResolveLazyMethod = StaticReflection.GetMethodInfo(() => LazyDynamicMethodBuildPlanCreatorPolicy.BuildResolveLazy<object>(null)).GetGenericMethodDefinition();

		// Token: 0x04000004 RID: 4
		private static readonly MethodInfo BuildResolveAllLazyMethod = StaticReflection.GetMethodInfo(() => LazyDynamicMethodBuildPlanCreatorPolicy.BuildResolveAllLazy<object>(null)).GetGenericMethodDefinition();
	}
}

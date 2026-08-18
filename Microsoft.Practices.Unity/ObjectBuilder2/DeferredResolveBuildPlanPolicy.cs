using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200002C RID: 44
	internal class DeferredResolveBuildPlanPolicy : IBuildPlanPolicy, IBuilderPolicy
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003480 File Offset: 0x00001680
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public void BuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			if (context.Existing == null)
			{
				IUnityContainer currentContainer = context.NewBuildUp<IUnityContainer>();
				Type typeToBuild = DeferredResolveBuildPlanPolicy.GetTypeToBuild(context.BuildKey.Type);
				string name = context.BuildKey.Name;
				Delegate existing;
				if (DeferredResolveBuildPlanPolicy.IsResolvingIEnumerable(typeToBuild))
				{
					existing = DeferredResolveBuildPlanPolicy.CreateResolveAllResolver(currentContainer, typeToBuild);
				}
				else
				{
					existing = DeferredResolveBuildPlanPolicy.CreateResolver(currentContainer, typeToBuild, name);
				}
				context.Existing = existing;
				DynamicMethodConstructorStrategy.SetPerBuildSingleton(context);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000034EC File Offset: 0x000016EC
		private static Type GetTypeToBuild(Type t)
		{
			return t.GetTypeInfo().GenericTypeArguments[0];
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000034FB File Offset: 0x000016FB
		private static bool IsResolvingIEnumerable(Type typeToBuild)
		{
			return typeToBuild.GetTypeInfo().IsGenericType && typeToBuild.GetGenericTypeDefinition() == typeof(IEnumerable<>);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003520 File Offset: 0x00001720
		private static Delegate CreateResolver(IUnityContainer currentContainer, Type typeToBuild, string nameToBuild)
		{
			Type type = typeof(DeferredResolveBuildPlanPolicy.ResolveTrampoline<>).MakeGenericType(new Type[]
			{
				typeToBuild
			});
			Type delegateType = typeof(Func<>).MakeGenericType(new Type[]
			{
				typeToBuild
			});
			MethodInfo declaredMethod = type.GetTypeInfo().GetDeclaredMethod("Resolve");
			object target = Activator.CreateInstance(type, new object[]
			{
				currentContainer,
				nameToBuild
			});
			return declaredMethod.CreateDelegate(delegateType, target);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035A0 File Offset: 0x000017A0
		private static Delegate CreateResolveAllResolver(IUnityContainer currentContainer, Type enumerableType)
		{
			Type typeToBuild = DeferredResolveBuildPlanPolicy.GetTypeToBuild(enumerableType);
			Type type = typeof(DeferredResolveBuildPlanPolicy.ResolveAllTrampoline<>).MakeGenericType(new Type[]
			{
				typeToBuild
			});
			Type delegateType = typeof(Func<>).MakeGenericType(new Type[]
			{
				enumerableType
			});
			MethodInfo declaredMethod = type.GetTypeInfo().GetDeclaredMethod("ResolveAll");
			object target = Activator.CreateInstance(type, new object[]
			{
				currentContainer
			});
			return declaredMethod.CreateDelegate(delegateType, target);
		}

		// Token: 0x0200002D RID: 45
		private class ResolveTrampoline<TItem>
		{
			// Token: 0x060000AB RID: 171 RVA: 0x0000362B File Offset: 0x0000182B
			public ResolveTrampoline(IUnityContainer container, string name)
			{
				this.container = container;
				this.name = name;
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00003641 File Offset: 0x00001841
			public TItem Resolve()
			{
				return this.container.Resolve(this.name, new ResolverOverride[0]);
			}

			// Token: 0x0400001F RID: 31
			private readonly IUnityContainer container;

			// Token: 0x04000020 RID: 32
			private readonly string name;
		}

		// Token: 0x0200002E RID: 46
		private class ResolveAllTrampoline<TItem>
		{
			// Token: 0x060000AD RID: 173 RVA: 0x0000365A File Offset: 0x0000185A
			public ResolveAllTrampoline(IUnityContainer container)
			{
				this.container = container;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00003669 File Offset: 0x00001869
			public IEnumerable<TItem> ResolveAll()
			{
				return this.container.ResolveAll(new ResolverOverride[0]);
			}

			// Token: 0x04000021 RID: 33
			private readonly IUnityContainer container;
		}
	}
}

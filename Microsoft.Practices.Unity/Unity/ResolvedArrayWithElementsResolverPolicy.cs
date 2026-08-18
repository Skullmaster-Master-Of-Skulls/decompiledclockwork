using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000096 RID: 150
	public class ResolvedArrayWithElementsResolverPolicy : IDependencyResolverPolicy, IBuilderPolicy
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00008944 File Offset: 0x00006B44
		public ResolvedArrayWithElementsResolverPolicy(Type elementType, params IDependencyResolverPolicy[] elementPolicies)
		{
			Guard.ArgumentNotNull(elementType, "elementType");
			MethodInfo methodInfo = typeof(ResolvedArrayWithElementsResolverPolicy).GetTypeInfo().GetDeclaredMethod("DoResolve").MakeGenericMethod(new Type[]
			{
				elementType
			});
			this.resolver = (ResolvedArrayWithElementsResolverPolicy.Resolver)methodInfo.CreateDelegate(typeof(ResolvedArrayWithElementsResolverPolicy.Resolver));
			this.elementPolicies = elementPolicies;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000089AF File Offset: 0x00006BAF
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class.")]
		public object Resolve(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			return this.resolver(context, this.elementPolicies);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000089D0 File Offset: 0x00006BD0
		private static object DoResolve<T>(IBuilderContext context, IDependencyResolverPolicy[] elementPolicies)
		{
			T[] array = new T[elementPolicies.Length];
			for (int i = 0; i < elementPolicies.Length; i++)
			{
				array[i] = (T)((object)elementPolicies[i].Resolve(context));
			}
			return array;
		}

		// Token: 0x04000096 RID: 150
		private readonly ResolvedArrayWithElementsResolverPolicy.Resolver resolver;

		// Token: 0x04000097 RID: 151
		private readonly IDependencyResolverPolicy[] elementPolicies;

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x060002B7 RID: 695
		private delegate object Resolver(IBuilderContext context, IDependencyResolverPolicy[] elementPolicies);
	}
}

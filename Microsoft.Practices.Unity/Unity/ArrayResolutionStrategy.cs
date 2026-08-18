using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200008F RID: 143
	public class ArrayResolutionStrategy : BuilderStrategy
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000863C File Offset: 0x0000683C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			Type type = context.BuildKey.Type;
			if (type.IsArray && type.GetArrayRank() == 1)
			{
				Type elementType = type.GetElementType();
				MethodInfo methodInfo = ArrayResolutionStrategy.GenericResolveArrayMethod.MakeGenericMethod(new Type[]
				{
					elementType
				});
				ArrayResolutionStrategy.ArrayResolver arrayResolver = (ArrayResolutionStrategy.ArrayResolver)methodInfo.CreateDelegate(typeof(ArrayResolutionStrategy.ArrayResolver));
				context.Existing = arrayResolver(context);
				context.BuildComplete = true;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000086D4 File Offset: 0x000068D4
		private static object ResolveArray<T>(IBuilderContext context)
		{
			IRegisteredNamesPolicy registeredNamesPolicy = context.Policies.Get(null);
			if (registeredNamesPolicy != null)
			{
				IEnumerable<string> enumerable = registeredNamesPolicy.GetRegisteredNames(typeof(T));
				if (typeof(T).GetTypeInfo().IsGenericType)
				{
					enumerable = enumerable.Concat(registeredNamesPolicy.GetRegisteredNames(typeof(T).GetGenericTypeDefinition()));
				}
				enumerable = enumerable.Distinct<string>();
				return (from n in enumerable
				select context.NewBuildUp(n)).ToArray<T>();
			}
			return new T[0];
		}

		// Token: 0x04000091 RID: 145
		private static readonly MethodInfo GenericResolveArrayMethod = typeof(ArrayResolutionStrategy).GetTypeInfo().DeclaredMethods.First((MethodInfo m) => m.Name == "ResolveArray" && !m.IsPublic && m.IsStatic);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x060002A4 RID: 676
		private delegate object ArrayResolver(IBuilderContext context);
	}
}

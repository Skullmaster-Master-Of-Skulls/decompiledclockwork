using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200009D RID: 157
	public class ResolvedParameter : TypedInjectionValue
	{
		// Token: 0x060002CA RID: 714 RVA: 0x000092D1 File Offset: 0x000074D1
		public ResolvedParameter(Type parameterType) : this(parameterType, null)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000092DB File Offset: 0x000074DB
		public ResolvedParameter(Type parameterType, string name) : base(parameterType)
		{
			this.name = name;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000092EC File Offset: 0x000074EC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done via Guard class")]
		public override IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild)
		{
			Guard.ArgumentNotNull(typeToBuild, "typeToBuild");
			ReflectionHelper reflectionHelper = new ReflectionHelper(this.ParameterType);
			if (reflectionHelper.IsGenericArray)
			{
				return this.CreateGenericArrayResolverPolicy(typeToBuild, reflectionHelper);
			}
			if (reflectionHelper.IsOpenGeneric || reflectionHelper.Type.IsGenericParameter)
			{
				return this.CreateGenericResolverPolicy(typeToBuild, reflectionHelper);
			}
			return this.CreateResolverPolicy(reflectionHelper.Type);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000934B File Offset: 0x0000754B
		private IDependencyResolverPolicy CreateResolverPolicy(Type typeToResolve)
		{
			return new NamedTypeDependencyResolverPolicy(typeToResolve, this.name);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00009359 File Offset: 0x00007559
		private IDependencyResolverPolicy CreateGenericResolverPolicy(Type typeToBuild, ReflectionHelper parameterReflector)
		{
			return new NamedTypeDependencyResolverPolicy(parameterReflector.GetClosedParameterType(typeToBuild.GenericTypeArguments), this.name);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009374 File Offset: 0x00007574
		private IDependencyResolverPolicy CreateGenericArrayResolverPolicy(Type typeToBuild, ReflectionHelper parameterReflector)
		{
			Type closedParameterType = parameterReflector.GetClosedParameterType(typeToBuild.GenericTypeArguments);
			return new NamedTypeDependencyResolverPolicy(closedParameterType, this.name);
		}

		// Token: 0x0400009F RID: 159
		private readonly string name;
	}
}

using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200001B RID: 27
	public class OptionalParameter : TypedInjectionValue
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002F99 File Offset: 0x00001199
		public OptionalParameter(Type type) : this(type, null)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FA3 File Offset: 0x000011A3
		public OptionalParameter(Type type, string name) : base(type)
		{
			this.name = name;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FB4 File Offset: 0x000011B4
		public override IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild)
		{
			Guard.ArgumentNotNull(typeToBuild, "typeToBuild");
			ReflectionHelper reflectionHelper = new ReflectionHelper(this.ParameterType);
			Type type = reflectionHelper.Type;
			if (reflectionHelper.IsOpenGeneric)
			{
				type = reflectionHelper.GetClosedParameterType(typeToBuild.GenericTypeArguments);
			}
			return new OptionalDependencyResolverPolicy(type, this.name);
		}

		// Token: 0x04000017 RID: 23
		private readonly string name;
	}
}

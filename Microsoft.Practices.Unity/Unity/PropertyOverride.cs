using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000033 RID: 51
	public class PropertyOverride : ResolverOverride
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000037F1 File Offset: 0x000019F1
		public PropertyOverride(string propertyName, object propertyValue)
		{
			this.propertyName = propertyName;
			this.propertyValue = InjectionParameterValue.ToParameter(propertyValue);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000380C File Offset: 0x00001A0C
		public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
		{
			Guard.ArgumentNotNull(context, "context");
			ResolvingPropertyValueOperation resolvingPropertyValueOperation = context.CurrentOperation as ResolvingPropertyValueOperation;
			if (resolvingPropertyValueOperation != null && resolvingPropertyValueOperation.PropertyName == this.propertyName)
			{
				return this.propertyValue.GetResolverPolicy(dependencyType);
			}
			return null;
		}

		// Token: 0x04000024 RID: 36
		private readonly string propertyName;

		// Token: 0x04000025 RID: 37
		private readonly InjectionParameterValue propertyValue;
	}
}

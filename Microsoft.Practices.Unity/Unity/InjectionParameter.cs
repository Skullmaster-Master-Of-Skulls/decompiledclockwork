using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000085 RID: 133
	public class InjectionParameter : TypedInjectionValue
	{
		// Token: 0x0600026C RID: 620 RVA: 0x000080B1 File Offset: 0x000062B1
		public InjectionParameter(object parameterValue) : this(InjectionParameter.GetParameterType(parameterValue), parameterValue)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000080C0 File Offset: 0x000062C0
		private static Type GetParameterType(object parameterValue)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue", Resources.ExceptionNullParameterValue);
			}
			return parameterValue.GetType();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000080DB File Offset: 0x000062DB
		public InjectionParameter(Type parameterType, object parameterValue) : base(parameterType)
		{
			this.parameterValue = parameterValue;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000080EB File Offset: 0x000062EB
		public override IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild)
		{
			return new LiteralValueDependencyResolverPolicy(this.parameterValue);
		}

		// Token: 0x04000087 RID: 135
		private readonly object parameterValue;
	}
}

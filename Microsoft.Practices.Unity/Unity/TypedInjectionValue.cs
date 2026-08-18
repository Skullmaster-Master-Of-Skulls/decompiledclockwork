using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200001A RID: 26
	public abstract class TypedInjectionValue : InjectionParameterValue
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002EF3 File Offset: 0x000010F3
		protected TypedInjectionValue(Type parameterType)
		{
			this.parameterReflector = new ReflectionHelper(parameterType);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F07 File Offset: 0x00001107
		public virtual Type ParameterType
		{
			get
			{
				return this.parameterReflector.Type;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F14 File Offset: 0x00001114
		public override string ParameterTypeName
		{
			get
			{
				return this.parameterReflector.Type.GetTypeInfo().Name;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F2C File Offset: 0x0000112C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public override bool MatchesType(Type t)
		{
			Guard.ArgumentNotNull(t, "t");
			ReflectionHelper reflectionHelper = new ReflectionHelper(t);
			if (reflectionHelper.IsOpenGeneric && this.parameterReflector.IsOpenGeneric)
			{
				return reflectionHelper.Type.GetGenericTypeDefinition() == this.parameterReflector.Type.GetGenericTypeDefinition();
			}
			return t.GetTypeInfo().IsAssignableFrom(this.parameterReflector.Type.GetTypeInfo());
		}

		// Token: 0x04000016 RID: 22
		private readonly ReflectionHelper parameterReflector;
	}
}

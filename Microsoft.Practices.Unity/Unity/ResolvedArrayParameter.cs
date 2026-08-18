using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000088 RID: 136
	public class ResolvedArrayParameter : TypedInjectionValue
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000839A File Offset: 0x0000659A
		public ResolvedArrayParameter(Type elementType, params object[] elementValues) : this(ResolvedArrayParameter.GetArrayType(elementType), elementType, elementValues)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000083AC File Offset: 0x000065AC
		protected ResolvedArrayParameter(Type arrayParameterType, Type elementType, params object[] elementValues) : base(arrayParameterType)
		{
			Guard.ArgumentNotNull(elementType, "elementType");
			Guard.ArgumentNotNull(elementValues, "elementValues");
			this.elementType = elementType;
			this.elementValues.AddRange(InjectionParameterValue.ToParameters(elementValues));
			foreach (InjectionParameterValue injectionParameterValue in this.elementValues)
			{
				if (!injectionParameterValue.MatchesType(elementType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.TypesAreNotAssignable, new object[]
					{
						elementType,
						injectionParameterValue.ParameterTypeName
					}));
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000846C File Offset: 0x0000666C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done via Guard class")]
		public override IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild)
		{
			Guard.ArgumentNotNull(typeToBuild, "typeToBuild");
			List<IDependencyResolverPolicy> list = new List<IDependencyResolverPolicy>();
			foreach (InjectionParameterValue injectionParameterValue in this.elementValues)
			{
				list.Add(injectionParameterValue.GetResolverPolicy(this.elementType));
			}
			return new ResolvedArrayWithElementsResolverPolicy(this.elementType, list.ToArray());
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000084EC File Offset: 0x000066EC
		private static Type GetArrayType(Type elementType)
		{
			Guard.ArgumentNotNull(elementType, "elementType");
			return elementType.MakeArrayType();
		}

		// Token: 0x0400008A RID: 138
		private readonly Type elementType;

		// Token: 0x0400008B RID: 139
		private readonly List<InjectionParameterValue> elementValues = new List<InjectionParameterValue>();
	}
}

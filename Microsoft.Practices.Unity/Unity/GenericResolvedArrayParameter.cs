using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000081 RID: 129
	public class GenericResolvedArrayParameter : InjectionParameterValue
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00007A9E File Offset: 0x00005C9E
		public GenericResolvedArrayParameter(string genericParameterName, params object[] elementValues)
		{
			Guard.ArgumentNotNull(genericParameterName, "genericParameterName");
			this.genericParameterName = genericParameterName;
			this.elementValues.AddRange(InjectionParameterValue.ToParameters(elementValues));
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public override string ParameterTypeName
		{
			get
			{
				return this.genericParameterName + "[]";
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007AE8 File Offset: 0x00005CE8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override bool MatchesType(Type t)
		{
			Guard.ArgumentNotNull(t, "t");
			if (!t.IsArray || t.GetArrayRank() != 1)
			{
				return false;
			}
			Type elementType = t.GetElementType();
			return elementType.GetTypeInfo().IsGenericParameter && elementType.GetTypeInfo().Name == this.genericParameterName;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007B40 File Offset: 0x00005D40
		public override IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild)
		{
			this.GuardTypeToBuildIsGeneric(typeToBuild);
			this.GuardTypeToBuildHasMatchingGenericParameter(typeToBuild);
			Type namedGenericParameter = new ReflectionHelper(typeToBuild).GetNamedGenericParameter(this.genericParameterName);
			List<IDependencyResolverPolicy> list = new List<IDependencyResolverPolicy>();
			foreach (InjectionParameterValue injectionParameterValue in this.elementValues)
			{
				list.Add(injectionParameterValue.GetResolverPolicy(typeToBuild));
			}
			return new ResolvedArrayWithElementsResolverPolicy(namedGenericParameter, list.ToArray());
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007BCC File Offset: 0x00005DCC
		private void GuardTypeToBuildIsGeneric(Type typeToBuild)
		{
			if (!typeToBuild.GetTypeInfo().IsGenericType)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NotAGenericType, new object[]
				{
					typeToBuild.GetTypeInfo().Name,
					this.genericParameterName
				}));
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007C1C File Offset: 0x00005E1C
		private void GuardTypeToBuildHasMatchingGenericParameter(Type typeToBuild)
		{
			foreach (Type type in typeToBuild.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters)
			{
				if (type.GetTypeInfo().Name == this.genericParameterName)
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoMatchingGenericArgument, new object[]
			{
				typeToBuild.GetTypeInfo().Name,
				this.genericParameterName
			}));
		}

		// Token: 0x0400007E RID: 126
		private readonly string genericParameterName;

		// Token: 0x0400007F RID: 127
		private readonly List<InjectionParameterValue> elementValues = new List<InjectionParameterValue>();
	}
}

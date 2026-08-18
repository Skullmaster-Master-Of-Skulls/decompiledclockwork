using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000016 RID: 22
	public abstract class GenericParameterBase : InjectionParameterValue
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002BF0 File Offset: 0x00000DF0
		protected GenericParameterBase(string genericParameterName) : this(genericParameterName, null)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BFC File Offset: 0x00000DFC
		protected GenericParameterBase(string genericParameterName, string resolutionKey)
		{
			Guard.ArgumentNotNull(genericParameterName, "genericParameterName");
			if (genericParameterName.EndsWith("[]", StringComparison.Ordinal) || genericParameterName.EndsWith("()", StringComparison.Ordinal))
			{
				this.genericParameterName = genericParameterName.Replace("[]", string.Empty).Replace("()", string.Empty);
				this.isArray = true;
			}
			else
			{
				this.genericParameterName = genericParameterName;
				this.isArray = false;
			}
			this.resolutionKey = resolutionKey;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C79 File Offset: 0x00000E79
		public override string ParameterTypeName
		{
			get
			{
				return this.genericParameterName;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C84 File Offset: 0x00000E84
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override bool MatchesType(Type t)
		{
			Guard.ArgumentNotNull(t, "t");
			if (!this.isArray)
			{
				return t.GetTypeInfo().IsGenericParameter && t.GetTypeInfo().Name == this.genericParameterName;
			}
			return t.IsArray && t.GetElementType().GetTypeInfo().IsGenericParameter && t.GetElementType().GetTypeInfo().Name == this.genericParameterName;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D04 File Offset: 0x00000F04
		public override IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild)
		{
			this.GuardTypeToBuildIsGeneric(typeToBuild);
			this.GuardTypeToBuildHasMatchingGenericParameter(typeToBuild);
			Type type = new ReflectionHelper(typeToBuild).GetNamedGenericParameter(this.genericParameterName);
			if (this.isArray)
			{
				type = type.MakeArrayType();
			}
			return this.DoGetResolverPolicy(type, this.resolutionKey);
		}

		// Token: 0x0600004F RID: 79
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "resolutionKey", Justification = "protected method parameter collides with private field - not an issue.")]
		protected abstract IDependencyResolverPolicy DoGetResolverPolicy(Type typeToResolve, string resolutionKey);

		// Token: 0x06000050 RID: 80 RVA: 0x00002D50 File Offset: 0x00000F50
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

		// Token: 0x06000051 RID: 81 RVA: 0x00002DA0 File Offset: 0x00000FA0
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

		// Token: 0x04000012 RID: 18
		private readonly string genericParameterName;

		// Token: 0x04000013 RID: 19
		private readonly bool isArray;

		// Token: 0x04000014 RID: 20
		private readonly string resolutionKey;
	}
}

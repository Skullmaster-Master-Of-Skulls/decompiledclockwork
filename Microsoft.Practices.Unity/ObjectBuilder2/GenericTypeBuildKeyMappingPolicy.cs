using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200004B RID: 75
	public class GenericTypeBuildKeyMappingPolicy : IBuildKeyMappingPolicy, IBuilderPolicy
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000043FC File Offset: 0x000025FC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class")]
		public GenericTypeBuildKeyMappingPolicy(NamedTypeBuildKey destinationKey)
		{
			Guard.ArgumentNotNull(destinationKey, "destinationKey");
			if (!destinationKey.Type.GetTypeInfo().IsGenericTypeDefinition)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.MustHaveOpenGenericType, new object[]
				{
					destinationKey.Type.GetTypeInfo().Name
				}));
			}
			this.destinationKey = destinationKey;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004464 File Offset: 0x00002664
		public NamedTypeBuildKey Map(NamedTypeBuildKey buildKey, IBuilderContext context)
		{
			Guard.ArgumentNotNull(buildKey, "buildKey");
			TypeInfo typeInfo = buildKey.Type.GetTypeInfo();
			if (typeInfo.IsGenericTypeDefinition)
			{
				return this.destinationKey;
			}
			this.GuardSameNumberOfGenericArguments(typeInfo);
			Type[] genericTypeArguments = typeInfo.GenericTypeArguments;
			Type type = this.destinationKey.Type.MakeGenericType(genericTypeArguments);
			return new NamedTypeBuildKey(type, this.destinationKey.Name);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000044C8 File Offset: 0x000026C8
		private void GuardSameNumberOfGenericArguments(TypeInfo sourceTypeInfo)
		{
			if (sourceTypeInfo.GenericTypeArguments.Length != this.DestinationType.GetTypeInfo().GenericTypeParameters.Length)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.MustHaveSameNumberOfGenericArguments, new object[]
				{
					sourceTypeInfo.Name,
					this.DestinationType.Name
				}), "sourceTypeInfo");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000452A File Offset: 0x0000272A
		private Type DestinationType
		{
			get
			{
				return this.destinationKey.Type;
			}
		}

		// Token: 0x04000042 RID: 66
		private readonly NamedTypeBuildKey destinationKey;
	}
}

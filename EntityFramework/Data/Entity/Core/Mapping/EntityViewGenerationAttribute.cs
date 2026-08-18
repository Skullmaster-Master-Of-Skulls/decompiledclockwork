using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003AD RID: 941
	[Obsolete("The mechanism to provide pre-generated views has changed. Implement a class that derives from System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCache and has a parameterless constructor, then associate it with a type that derives from DbContext or ObjectContext by using System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCacheTypeAttribute.", true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class EntityViewGenerationAttribute : Attribute
	{
		// Token: 0x06002258 RID: 8792 RVA: 0x000A0AAE File Offset: 0x0009ECAE
		public EntityViewGenerationAttribute(Type viewGenerationType)
		{
			Check.NotNull<Type>(viewGenerationType, "viewGenerationType");
			this.m_viewGenType = viewGenerationType;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x000A0AC9 File Offset: 0x0009ECC9
		public Type ViewGenerationType
		{
			get
			{
				return this.m_viewGenType;
			}
		}

		// Token: 0x04000C1B RID: 3099
		private readonly Type m_viewGenType;
	}
}

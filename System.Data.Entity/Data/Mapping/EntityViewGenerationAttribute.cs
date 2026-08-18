using System;

namespace System.Data.Mapping
{
	// Token: 0x02000232 RID: 562
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class EntityViewGenerationAttribute : Attribute
	{
		// Token: 0x060023FA RID: 9210 RVA: 0x000826FE File Offset: 0x000808FE
		public EntityViewGenerationAttribute(Type viewGenerationType)
		{
			EntityUtil.CheckArgumentNull<Type>(viewGenerationType, "viewGenType");
			this.m_viewGenType = viewGenerationType;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x00082719 File Offset: 0x00080919
		public Type ViewGenerationType
		{
			get
			{
				return this.m_viewGenType;
			}
		}

		// Token: 0x04000FF3 RID: 4083
		private Type m_viewGenType;
	}
}

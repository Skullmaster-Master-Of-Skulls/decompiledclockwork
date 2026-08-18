using System;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000EE RID: 238
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface)]
	public sealed class TypeConverterAttribute : Attribute
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x0001556C File Offset: 0x0001376C
		public TypeConverterAttribute()
		{
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00015574 File Offset: 0x00013774
		public TypeConverterAttribute(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00015583 File Offset: 0x00013783
		public TypeConverterAttribute(Type converterType)
		{
			this.m_typeName = SystemInfo.AssemblyQualifiedName(converterType);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00015597 File Offset: 0x00013797
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001559F File Offset: 0x0001379F
		public string ConverterTypeName
		{
			get
			{
				return this.m_typeName;
			}
			set
			{
				this.m_typeName = value;
			}
		}

		// Token: 0x04000299 RID: 665
		private string m_typeName;
	}
}

using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000262 RID: 610
	internal sealed class MetadataType : MetadataMember
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x00063085 File Offset: 0x00061285
		internal MetadataType(string name, TypeUsage typeUsage) : base(MetadataMemberClass.Type, name)
		{
			this.TypeUsage = typeUsage;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00063096 File Offset: 0x00061296
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataType.TypeClassName;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0006309D File Offset: 0x0006129D
		internal static string TypeClassName
		{
			get
			{
				return Strings.LocalizedType;
			}
		}

		// Token: 0x04000743 RID: 1859
		internal readonly TypeUsage TypeUsage;
	}
}

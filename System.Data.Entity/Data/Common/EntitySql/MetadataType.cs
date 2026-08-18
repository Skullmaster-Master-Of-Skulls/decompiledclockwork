using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000351 RID: 849
	internal sealed class MetadataType : MetadataMember
	{
		// Token: 0x06003193 RID: 12691 RVA: 0x000C2D30 File Offset: 0x000C0F30
		internal MetadataType(string name, TypeUsage typeUsage) : base(MetadataMemberClass.Type, name)
		{
			this.TypeUsage = typeUsage;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000C2D41 File Offset: 0x000C0F41
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataType.TypeClassName;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000C2D48 File Offset: 0x000C0F48
		internal static string TypeClassName
		{
			get
			{
				return Strings.LocalizedType;
			}
		}

		// Token: 0x04001590 RID: 5520
		internal readonly TypeUsage TypeUsage;
	}
}

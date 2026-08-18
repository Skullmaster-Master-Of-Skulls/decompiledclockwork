using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000352 RID: 850
	internal sealed class MetadataEnumMember : MetadataMember
	{
		// Token: 0x06003196 RID: 12694 RVA: 0x000C2D4F File Offset: 0x000C0F4F
		internal MetadataEnumMember(string name, TypeUsage enumType, EnumMember enumMember) : base(MetadataMemberClass.EnumMember, name)
		{
			this.EnumType = enumType;
			this.EnumMember = enumMember;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000C2D67 File Offset: 0x000C0F67
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataEnumMember.EnumMemberClassName;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000C2D6E File Offset: 0x000C0F6E
		internal static string EnumMemberClassName
		{
			get
			{
				return Strings.LocalizedEnumMember;
			}
		}

		// Token: 0x04001591 RID: 5521
		internal readonly TypeUsage EnumType;

		// Token: 0x04001592 RID: 5522
		internal readonly EnumMember EnumMember;
	}
}

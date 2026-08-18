using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200025E RID: 606
	internal sealed class MetadataEnumMember : MetadataMember
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x00063028 File Offset: 0x00061228
		internal MetadataEnumMember(string name, TypeUsage enumType, EnumMember enumMember) : base(MetadataMemberClass.EnumMember, name)
		{
			this.EnumType = enumType;
			this.EnumMember = enumMember;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00063040 File Offset: 0x00061240
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataEnumMember.EnumMemberClassName;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00063047 File Offset: 0x00061247
		internal static string EnumMemberClassName
		{
			get
			{
				return Strings.LocalizedEnumMember;
			}
		}

		// Token: 0x0400073A RID: 1850
		internal readonly TypeUsage EnumType;

		// Token: 0x0400073B RID: 1851
		internal readonly EnumMember EnumMember;
	}
}

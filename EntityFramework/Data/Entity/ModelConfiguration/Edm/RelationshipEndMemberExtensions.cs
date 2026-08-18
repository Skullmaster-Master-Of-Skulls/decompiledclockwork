using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000811 RID: 2065
	internal static class RelationshipEndMemberExtensions
	{
		// Token: 0x06005CE1 RID: 23777 RVA: 0x0019115D File Offset: 0x0018F35D
		public static bool IsMany(this RelationshipEndMember associationEnd)
		{
			return associationEnd.RelationshipMultiplicity.IsMany();
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0019116A File Offset: 0x0018F36A
		public static bool IsOptional(this RelationshipEndMember associationEnd)
		{
			return associationEnd.RelationshipMultiplicity.IsOptional();
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x00191177 File Offset: 0x0018F377
		public static bool IsRequired(this RelationshipEndMember associationEnd)
		{
			return associationEnd.RelationshipMultiplicity.IsRequired();
		}
	}
}

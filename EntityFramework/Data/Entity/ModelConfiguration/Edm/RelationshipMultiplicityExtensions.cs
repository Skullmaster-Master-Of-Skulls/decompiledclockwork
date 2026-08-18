using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000812 RID: 2066
	internal static class RelationshipMultiplicityExtensions
	{
		// Token: 0x06005CE4 RID: 23780 RVA: 0x00191184 File Offset: 0x0018F384
		public static bool IsMany(this RelationshipMultiplicity associationEndKind)
		{
			return associationEndKind == RelationshipMultiplicity.Many;
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x0019118A File Offset: 0x0018F38A
		public static bool IsOptional(this RelationshipMultiplicity associationEndKind)
		{
			return associationEndKind == RelationshipMultiplicity.ZeroOrOne;
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x00191190 File Offset: 0x0018F390
		public static bool IsRequired(this RelationshipMultiplicity associationEndKind)
		{
			return associationEndKind == RelationshipMultiplicity.One;
		}
	}
}

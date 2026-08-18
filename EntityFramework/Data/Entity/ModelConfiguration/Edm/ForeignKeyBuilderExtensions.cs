using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200080B RID: 2059
	internal static class ForeignKeyBuilderExtensions
	{
		// Token: 0x06005CA6 RID: 23718 RVA: 0x00190117 File Offset: 0x0018E317
		public static string GetPreferredName(this ForeignKeyBuilder fk)
		{
			return (string)fk.Annotations.GetAnnotation("PreferredName");
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x0019012E File Offset: 0x0018E32E
		public static void SetPreferredName(this ForeignKeyBuilder fk, string name)
		{
			fk.GetMetadataProperties().SetAnnotation("PreferredName", name);
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x00190144 File Offset: 0x0018E344
		public static bool GetIsTypeConstraint(this ForeignKeyBuilder fk)
		{
			object annotation = fk.Annotations.GetAnnotation("IsTypeConstraint");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x0019016D File Offset: 0x0018E36D
		public static void SetIsTypeConstraint(this ForeignKeyBuilder fk)
		{
			fk.GetMetadataProperties().SetAnnotation("IsTypeConstraint", true);
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x00190185 File Offset: 0x0018E385
		public static void SetIsSplitConstraint(this ForeignKeyBuilder fk)
		{
			fk.GetMetadataProperties().SetAnnotation("IsSplitConstraint", true);
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x0019019D File Offset: 0x0018E39D
		public static AssociationType GetAssociationType(this ForeignKeyBuilder fk)
		{
			return fk.Annotations.GetAnnotation("AssociationType") as AssociationType;
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x001901B4 File Offset: 0x0018E3B4
		public static void SetAssociationType(this ForeignKeyBuilder fk, AssociationType associationType)
		{
			fk.GetMetadataProperties().SetAnnotation("AssociationType", associationType);
		}

		// Token: 0x040024BC RID: 9404
		private const string IsTypeConstraint = "IsTypeConstraint";

		// Token: 0x040024BD RID: 9405
		private const string IsSplitConstraint = "IsSplitConstraint";

		// Token: 0x040024BE RID: 9406
		private const string AssociationType = "AssociationType";

		// Token: 0x040024BF RID: 9407
		private const string PreferredNameAnnotation = "PreferredName";
	}
}

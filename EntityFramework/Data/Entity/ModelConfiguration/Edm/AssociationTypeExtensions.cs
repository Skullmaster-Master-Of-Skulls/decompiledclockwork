using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000813 RID: 2067
	internal static class AssociationTypeExtensions
	{
		// Token: 0x06005CE7 RID: 23783 RVA: 0x00191196 File Offset: 0x0018F396
		public static void MarkIndependent(this AssociationType associationType)
		{
			associationType.GetMetadataProperties().SetAnnotation("IsIndependent", true);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x001911B0 File Offset: 0x0018F3B0
		public static bool IsIndependent(this AssociationType associationType)
		{
			object annotation = associationType.Annotations.GetAnnotation("IsIndependent");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x001911D9 File Offset: 0x0018F3D9
		public static void MarkPrincipalConfigured(this AssociationType associationType)
		{
			associationType.GetMetadataProperties().SetAnnotation("IsPrincipalConfigured", true);
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x001911F4 File Offset: 0x0018F3F4
		public static bool IsPrincipalConfigured(this AssociationType associationType)
		{
			object annotation = associationType.Annotations.GetAnnotation("IsPrincipalConfigured");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x0019121D File Offset: 0x0018F41D
		public static AssociationEndMember GetOtherEnd(this AssociationType associationType, AssociationEndMember associationEnd)
		{
			if (associationEnd != associationType.SourceEnd)
			{
				return associationType.SourceEnd;
			}
			return associationType.TargetEnd;
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x00191235 File Offset: 0x0018F435
		public static object GetConfiguration(this AssociationType associationType)
		{
			return associationType.Annotations.GetConfiguration();
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x00191242 File Offset: 0x0018F442
		public static void SetConfiguration(this AssociationType associationType, object configuration)
		{
			associationType.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00191250 File Offset: 0x0018F450
		public static bool IsRequiredToMany(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsRequired() && associationType.TargetEnd.IsMany();
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x0019126C File Offset: 0x0018F46C
		public static bool IsRequiredToRequired(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsRequired() && associationType.TargetEnd.IsRequired();
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00191288 File Offset: 0x0018F488
		public static bool IsManyToRequired(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsMany() && associationType.TargetEnd.IsRequired();
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x001912A4 File Offset: 0x0018F4A4
		public static bool IsManyToMany(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsMany() && associationType.TargetEnd.IsMany();
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x001912C0 File Offset: 0x0018F4C0
		public static bool IsOneToOne(this AssociationType associationType)
		{
			return !associationType.SourceEnd.IsMany() && !associationType.TargetEnd.IsMany();
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x001912E0 File Offset: 0x0018F4E0
		public static bool IsSelfReferencing(this AssociationType associationType)
		{
			AssociationEndMember sourceEnd = associationType.SourceEnd;
			AssociationEndMember targetEnd = associationType.TargetEnd;
			return sourceEnd.GetEntityType().GetRootType() == targetEnd.GetEntityType().GetRootType();
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00191313 File Offset: 0x0018F513
		public static bool IsRequiredToNonRequired(this AssociationType associationType)
		{
			return (associationType.SourceEnd.IsRequired() && !associationType.TargetEnd.IsRequired()) || (associationType.TargetEnd.IsRequired() && !associationType.SourceEnd.IsRequired());
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x00191350 File Offset: 0x0018F550
		public static bool TryGuessPrincipalAndDependentEnds(this AssociationType associationType, out AssociationEndMember principalEnd, out AssociationEndMember dependentEnd)
		{
			AssociationEndMember associationEndMember;
			dependentEnd = (associationEndMember = null);
			principalEnd = associationEndMember;
			AssociationEndMember sourceEnd = associationType.SourceEnd;
			AssociationEndMember targetEnd = associationType.TargetEnd;
			if (sourceEnd.RelationshipMultiplicity != targetEnd.RelationshipMultiplicity)
			{
				principalEnd = ((sourceEnd.IsRequired() || (sourceEnd.IsOptional() && targetEnd.IsMany())) ? sourceEnd : targetEnd);
				dependentEnd = ((principalEnd == sourceEnd) ? targetEnd : sourceEnd);
			}
			return principalEnd != null;
		}

		// Token: 0x040024D2 RID: 9426
		private const string IsIndependentAnnotation = "IsIndependent";

		// Token: 0x040024D3 RID: 9427
		private const string IsPrincipalConfiguredAnnotation = "IsPrincipalConfigured";
	}
}

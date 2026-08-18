using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x020007C2 RID: 1986
	internal abstract class ConstraintConfiguration
	{
		// Token: 0x06005A30 RID: 23088
		internal abstract ConstraintConfiguration Clone();

		// Token: 0x06005A31 RID: 23089
		internal abstract void Configure(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration);

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x00185297 File Offset: 0x00183497
		public virtual bool IsFullySpecified
		{
			get
			{
				return true;
			}
		}
	}
}

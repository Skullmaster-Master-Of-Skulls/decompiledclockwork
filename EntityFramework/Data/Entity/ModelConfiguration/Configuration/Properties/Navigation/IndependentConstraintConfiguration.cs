using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x020007C7 RID: 1991
	internal class IndependentConstraintConfiguration : ConstraintConfiguration
	{
		// Token: 0x06005A6B RID: 23147 RVA: 0x00185CDD File Offset: 0x00183EDD
		private IndependentConstraintConfiguration()
		{
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005A6C RID: 23148 RVA: 0x00185CE5 File Offset: 0x00183EE5
		public static ConstraintConfiguration Instance
		{
			get
			{
				return IndependentConstraintConfiguration._instance;
			}
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x00185CEC File Offset: 0x00183EEC
		internal override ConstraintConfiguration Clone()
		{
			return IndependentConstraintConfiguration._instance;
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x00185CF3 File Offset: 0x00183EF3
		internal override void Configure(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration)
		{
			associationType.MarkIndependent();
		}

		// Token: 0x04002413 RID: 9235
		private static readonly ConstraintConfiguration _instance = new IndependentConstraintConfiguration();
	}
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions.Sets
{
	// Token: 0x02000806 RID: 2054
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal static class V1ConventionSet
	{
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06005C90 RID: 23696 RVA: 0x0018FD52 File Offset: 0x0018DF52
		public static ConventionSet Conventions
		{
			get
			{
				return V1ConventionSet._conventions;
			}
		}

		// Token: 0x040024B8 RID: 9400
		private static readonly ConventionSet _conventions = new ConventionSet(new IConvention[]
		{
			new NotMappedTypeAttributeConvention(),
			new ComplexTypeAttributeConvention(),
			new TableAttributeConvention(),
			new NotMappedPropertyAttributeConvention(),
			new KeyAttributeConvention(),
			new RequiredPrimitivePropertyAttributeConvention(),
			new RequiredNavigationPropertyAttributeConvention(),
			new TimestampAttributeConvention(),
			new ConcurrencyCheckAttributeConvention(),
			new DatabaseGeneratedAttributeConvention(),
			new MaxLengthAttributeConvention(),
			new StringLengthAttributeConvention(),
			new ColumnAttributeConvention(),
			new IndexAttributeConvention(),
			new InversePropertyAttributeConvention(),
			new ForeignKeyPrimitivePropertyAttributeConvention()
		}.Reverse<IConvention>(), new IConvention[]
		{
			new IdKeyDiscoveryConvention(),
			new AssociationInverseDiscoveryConvention(),
			new ForeignKeyNavigationPropertyAttributeConvention(),
			new OneToOneConstraintIntroductionConvention(),
			new NavigationPropertyNameForeignKeyDiscoveryConvention(),
			new PrimaryKeyNameForeignKeyDiscoveryConvention(),
			new TypeNameForeignKeyDiscoveryConvention(),
			new ForeignKeyAssociationMultiplicityConvention(),
			new OneToManyCascadeDeleteConvention(),
			new ComplexTypeDiscoveryConvention(),
			new StoreGeneratedIdentityKeyConvention(),
			new PluralizingEntitySetNameConvention(),
			new DeclaredPropertyOrderingConvention(),
			new SqlCePropertyMaxLengthConvention(),
			new PropertyMaxLengthConvention(),
			new DecimalPropertyConvention()
		}, new IConvention[]
		{
			new ManyToManyCascadeDeleteConvention(),
			new MappingInheritedPropertiesSupportConvention()
		}, new IConvention[]
		{
			new PluralizingTableNameConvention(),
			new ColumnOrderingConvention(),
			new ForeignKeyIndexConvention()
		});
	}
}

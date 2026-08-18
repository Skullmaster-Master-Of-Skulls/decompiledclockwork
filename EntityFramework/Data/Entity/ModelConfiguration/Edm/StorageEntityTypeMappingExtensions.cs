using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200080F RID: 2063
	internal static class StorageEntityTypeMappingExtensions
	{
		// Token: 0x06005CCD RID: 23757 RVA: 0x00190DD0 File Offset: 0x0018EFD0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public static object GetConfiguration(this EntityTypeMapping entityTypeMapping)
		{
			return entityTypeMapping.Annotations.GetConfiguration();
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x00190DDD File Offset: 0x0018EFDD
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public static void SetConfiguration(this EntityTypeMapping entityTypeMapping, object configuration)
		{
			entityTypeMapping.Annotations.SetConfiguration(configuration);
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x00190E10 File Offset: 0x0018F010
		public static ColumnMappingBuilder GetPropertyMapping(this EntityTypeMapping entityTypeMapping, params EdmProperty[] propertyPath)
		{
			return entityTypeMapping.MappingFragments.SelectMany((MappingFragment f) => f.ColumnMappings).Single((ColumnMappingBuilder p) => p.PropertyPath.SequenceEqual(propertyPath));
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00190E63 File Offset: 0x0018F063
		public static EntityType GetPrimaryTable(this EntityTypeMapping entityTypeMapping)
		{
			return entityTypeMapping.MappingFragments.First<MappingFragment>().Table;
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x00190E90 File Offset: 0x0018F090
		public static bool UsesOtherTables(this EntityTypeMapping entityTypeMapping, EntityType table)
		{
			return entityTypeMapping.MappingFragments.Any((MappingFragment f) => f.Table != table);
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x00190EC1 File Offset: 0x0018F0C1
		public static Type GetClrType(this EntityTypeMapping entityTypeMappping)
		{
			return entityTypeMappping.Annotations.GetClrType();
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x00190ECE File Offset: 0x0018F0CE
		public static void SetClrType(this EntityTypeMapping entityTypeMapping, Type type)
		{
			entityTypeMapping.Annotations.SetClrType(type);
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x00190EDC File Offset: 0x0018F0DC
		public static EntityTypeMapping Clone(this EntityTypeMapping entityTypeMapping)
		{
			EntityTypeMapping entityTypeMapping2 = new EntityTypeMapping(null);
			entityTypeMapping2.AddType(entityTypeMapping.EntityType);
			entityTypeMapping.Annotations.Copy(entityTypeMapping2.Annotations);
			return entityTypeMapping2;
		}
	}
}

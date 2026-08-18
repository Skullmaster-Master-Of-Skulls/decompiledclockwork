using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000810 RID: 2064
	internal static class StorageMappingFragmentExtensions
	{
		// Token: 0x06005CD6 RID: 23766 RVA: 0x00190F0E File Offset: 0x0018F10E
		public static EdmProperty GetDefaultDiscriminator(this MappingFragment entityTypeMapppingFragment)
		{
			return (EdmProperty)entityTypeMapppingFragment.Annotations.GetAnnotation("DefaultDiscriminator");
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00190F25 File Offset: 0x0018F125
		public static void SetDefaultDiscriminator(this MappingFragment entityTypeMappingFragment, EdmProperty discriminator)
		{
			entityTypeMappingFragment.Annotations.SetAnnotation("DefaultDiscriminator", discriminator);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00190F38 File Offset: 0x0018F138
		public static void RemoveDefaultDiscriminatorAnnotation(this MappingFragment entityTypeMappingFragment)
		{
			entityTypeMappingFragment.Annotations.RemoveAnnotation("DefaultDiscriminator");
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00190F80 File Offset: 0x0018F180
		public static void RemoveDefaultDiscriminator(this MappingFragment entityTypeMappingFragment, EntitySetMapping entitySetMapping)
		{
			EdmProperty discriminatorColumn = entityTypeMappingFragment.RemoveDefaultDiscriminatorCondition();
			if (discriminatorColumn != null)
			{
				EntityType table = entityTypeMappingFragment.Table;
				(from c in table.Properties
				where c.Name.Equals(discriminatorColumn.Name, StringComparison.Ordinal)
				select c).ToList<EdmProperty>().Each(new Action<EdmProperty>(table.RemoveMember));
			}
			if (entitySetMapping != null && entityTypeMappingFragment.IsConditionOnlyFragment() && !entityTypeMappingFragment.ColumnConditions.Any<ConditionPropertyMapping>())
			{
				EntityTypeMapping entityTypeMapping = entitySetMapping.EntityTypeMappings.Single((EntityTypeMapping etm) => etm.MappingFragments.Contains(entityTypeMappingFragment));
				entityTypeMapping.RemoveFragment(entityTypeMappingFragment);
				if (entityTypeMapping.MappingFragments.Count == 0)
				{
					entitySetMapping.RemoveTypeMapping(entityTypeMapping);
				}
			}
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00191060 File Offset: 0x0018F260
		public static EdmProperty RemoveDefaultDiscriminatorCondition(this MappingFragment entityTypeMappingFragment)
		{
			EdmProperty defaultDiscriminator = entityTypeMappingFragment.GetDefaultDiscriminator();
			if (defaultDiscriminator != null && entityTypeMappingFragment.ColumnConditions.Any<ConditionPropertyMapping>())
			{
				entityTypeMappingFragment.ClearConditions();
			}
			entityTypeMappingFragment.RemoveDefaultDiscriminatorAnnotation();
			return defaultDiscriminator;
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x00191091 File Offset: 0x0018F291
		public static void AddDiscriminatorCondition(this MappingFragment entityTypeMapppingFragment, EdmProperty discriminatorColumn, object value)
		{
			entityTypeMapppingFragment.AddConditionProperty(new ValueConditionMapping(discriminatorColumn, value));
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x001910A0 File Offset: 0x0018F2A0
		public static void AddNullabilityCondition(this MappingFragment entityTypeMapppingFragment, EdmProperty column, bool isNull)
		{
			entityTypeMapppingFragment.AddConditionProperty(new IsNullConditionMapping(column, isNull));
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x001910B0 File Offset: 0x0018F2B0
		public static bool IsConditionOnlyFragment(this MappingFragment entityTypeMapppingFragment)
		{
			object annotation = entityTypeMapppingFragment.Annotations.GetAnnotation("ConditionOnlyFragment");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x001910D9 File Offset: 0x0018F2D9
		public static void SetIsConditionOnlyFragment(this MappingFragment entityTypeMapppingFragment, bool isConditionOnlyFragment)
		{
			if (isConditionOnlyFragment)
			{
				entityTypeMapppingFragment.Annotations.SetAnnotation("ConditionOnlyFragment", isConditionOnlyFragment);
				return;
			}
			entityTypeMapppingFragment.Annotations.RemoveAnnotation("ConditionOnlyFragment");
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x00191108 File Offset: 0x0018F308
		public static bool IsUnmappedPropertiesFragment(this MappingFragment entityTypeMapppingFragment)
		{
			object annotation = entityTypeMapppingFragment.Annotations.GetAnnotation("UnmappedPropertiesFragment");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x00191131 File Offset: 0x0018F331
		public static void SetIsUnmappedPropertiesFragment(this MappingFragment entityTypeMapppingFragment, bool isUnmappedPropertiesFragment)
		{
			if (isUnmappedPropertiesFragment)
			{
				entityTypeMapppingFragment.Annotations.SetAnnotation("UnmappedPropertiesFragment", isUnmappedPropertiesFragment);
				return;
			}
			entityTypeMapppingFragment.Annotations.RemoveAnnotation("UnmappedPropertiesFragment");
		}

		// Token: 0x040024CF RID: 9423
		private const string DefaultDiscriminatorAnnotation = "DefaultDiscriminator";

		// Token: 0x040024D0 RID: 9424
		private const string ConditionOnlyFragmentAnnotation = "ConditionOnlyFragment";

		// Token: 0x040024D1 RID: 9425
		private const string UnmappedPropertiesFragmentAnnotation = "UnmappedPropertiesFragment";
	}
}

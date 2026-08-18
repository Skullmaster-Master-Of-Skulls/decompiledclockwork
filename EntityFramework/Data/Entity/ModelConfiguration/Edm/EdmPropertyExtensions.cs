using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000819 RID: 2073
	internal static class EdmPropertyExtensions
	{
		// Token: 0x06005D3E RID: 23870 RVA: 0x001927D4 File Offset: 0x001909D4
		public static void CopyFrom(this EdmProperty column, EdmProperty other)
		{
			column.IsFixedLength = other.IsFixedLength;
			column.IsMaxLength = other.IsMaxLength;
			column.IsUnicode = other.IsUnicode;
			column.MaxLength = other.MaxLength;
			column.Precision = other.Precision;
			column.Scale = other.Scale;
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00192844 File Offset: 0x00190A44
		public static EdmProperty Clone(this EdmProperty tableColumn)
		{
			EdmProperty columnMetadata = new EdmProperty(tableColumn.Name, tableColumn.TypeUsage)
			{
				Nullable = tableColumn.Nullable,
				StoreGeneratedPattern = tableColumn.StoreGeneratedPattern,
				IsFixedLength = tableColumn.IsFixedLength,
				IsMaxLength = tableColumn.IsMaxLength,
				IsUnicode = tableColumn.IsUnicode,
				MaxLength = tableColumn.MaxLength,
				Precision = tableColumn.Precision,
				Scale = tableColumn.Scale
			};
			tableColumn.Annotations.Each(delegate(MetadataProperty a)
			{
				columnMetadata.GetMetadataProperties().Add(a);
			});
			return columnMetadata;
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x001928ED File Offset: 0x00190AED
		public static int? GetOrder(this EdmProperty tableColumn)
		{
			return (int?)tableColumn.Annotations.GetAnnotation("Order");
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x00192904 File Offset: 0x00190B04
		public static void SetOrder(this EdmProperty tableColumn, int order)
		{
			tableColumn.GetMetadataProperties().SetAnnotation("Order", order);
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x0019291C File Offset: 0x00190B1C
		public static string GetPreferredName(this EdmProperty tableColumn)
		{
			return (string)tableColumn.Annotations.GetAnnotation("PreferredName");
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x00192933 File Offset: 0x00190B33
		public static void SetPreferredName(this EdmProperty tableColumn, string name)
		{
			tableColumn.GetMetadataProperties().SetAnnotation("PreferredName", name);
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x00192946 File Offset: 0x00190B46
		public static string GetUnpreferredUniqueName(this EdmProperty tableColumn)
		{
			return (string)tableColumn.Annotations.GetAnnotation("UnpreferredUniqueName");
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x0019295D File Offset: 0x00190B5D
		public static void SetUnpreferredUniqueName(this EdmProperty tableColumn, string name)
		{
			tableColumn.GetMetadataProperties().SetAnnotation("UnpreferredUniqueName", name);
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x00192970 File Offset: 0x00190B70
		public static void RemoveStoreGeneratedIdentityPattern(this EdmProperty tableColumn)
		{
			if (tableColumn.StoreGeneratedPattern == StoreGeneratedPattern.Identity)
			{
				tableColumn.StoreGeneratedPattern = StoreGeneratedPattern.None;
			}
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x00192984 File Offset: 0x00190B84
		public static bool HasStoreGeneratedPattern(this EdmProperty property)
		{
			StoreGeneratedPattern? storeGeneratedPattern = property.GetStoreGeneratedPattern();
			return storeGeneratedPattern != null && storeGeneratedPattern != StoreGeneratedPattern.None;
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x001929BC File Offset: 0x00190BBC
		public static StoreGeneratedPattern? GetStoreGeneratedPattern(this EdmProperty property)
		{
			MetadataProperty metadataProperty;
			if (property.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty))
			{
				return (StoreGeneratedPattern?)Enum.Parse(typeof(StoreGeneratedPattern), (string)metadataProperty.Value);
			}
			return null;
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x00192A08 File Offset: 0x00190C08
		public static void SetStoreGeneratedPattern(this EdmProperty property, StoreGeneratedPattern storeGeneratedPattern)
		{
			MetadataProperty metadataProperty;
			if (!property.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty))
			{
				property.MetadataProperties.Source.Add(new MetadataProperty("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", TypeUsage.Create(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.String)), storeGeneratedPattern.ToString()));
				return;
			}
			metadataProperty.Value = storeGeneratedPattern.ToString();
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x00192A72 File Offset: 0x00190C72
		public static object GetConfiguration(this EdmProperty property)
		{
			return property.Annotations.GetConfiguration();
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x00192A7F File Offset: 0x00190C7F
		public static void SetConfiguration(this EdmProperty property, object configuration)
		{
			property.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x00192A8D File Offset: 0x00190C8D
		public static List<EdmPropertyPath> ToPropertyPathList(this EdmProperty property)
		{
			return property.ToPropertyPathList(new List<EdmProperty>());
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x00192A9C File Offset: 0x00190C9C
		public static List<EdmPropertyPath> ToPropertyPathList(this EdmProperty property, List<EdmProperty> currentPath)
		{
			List<EdmPropertyPath> list = new List<EdmPropertyPath>();
			EdmPropertyExtensions.IncludePropertyPath(list, currentPath, property);
			return list;
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x00192AB8 File Offset: 0x00190CB8
		private static void IncludePropertyPath(List<EdmPropertyPath> propertyPaths, List<EdmProperty> currentPath, EdmProperty property)
		{
			currentPath.Add(property);
			if (property.IsUnderlyingPrimitiveType)
			{
				propertyPaths.Add(new EdmPropertyPath(currentPath));
			}
			else if (property.IsComplexType)
			{
				foreach (EdmProperty property2 in property.ComplexType.Properties)
				{
					EdmPropertyExtensions.IncludePropertyPath(propertyPaths, currentPath, property2);
				}
			}
			currentPath.Remove(property);
		}

		// Token: 0x040024DF RID: 9439
		private const string OrderAnnotation = "Order";

		// Token: 0x040024E0 RID: 9440
		private const string PreferredNameAnnotation = "PreferredName";

		// Token: 0x040024E1 RID: 9441
		private const string UnpreferredUniqueNameAnnotation = "UnpreferredUniqueName";
	}
}

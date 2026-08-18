using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x020001D4 RID: 468
	internal static class MetadataPropertyExtensions
	{
		// Token: 0x06000F73 RID: 3955 RVA: 0x00041715 File Offset: 0x0003F915
		public static IList<Attribute> GetClrAttributes(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return (IList<Attribute>)metadataProperties.GetAnnotation("ClrAttributes");
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00041727 File Offset: 0x0003F927
		public static void SetClrAttributes(this ICollection<MetadataProperty> metadataProperties, IList<Attribute> attributes)
		{
			metadataProperties.SetAnnotation("ClrAttributes", attributes);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00041735 File Offset: 0x0003F935
		public static PropertyInfo GetClrPropertyInfo(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return (PropertyInfo)metadataProperties.GetAnnotation("ClrPropertyInfo");
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00041747 File Offset: 0x0003F947
		public static void SetClrPropertyInfo(this ICollection<MetadataProperty> metadataProperties, PropertyInfo propertyInfo)
		{
			metadataProperties.SetAnnotation("ClrPropertyInfo", propertyInfo);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00041755 File Offset: 0x0003F955
		public static Type GetClrType(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return (Type)metadataProperties.GetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType");
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00041767 File Offset: 0x0003F967
		public static void SetClrType(this ICollection<MetadataProperty> metadataProperties, Type type)
		{
			metadataProperties.SetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType", type);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00041775 File Offset: 0x0003F975
		public static object GetConfiguration(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return metadataProperties.GetAnnotation("Configuration");
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00041782 File Offset: 0x0003F982
		public static void SetConfiguration(this ICollection<MetadataProperty> metadataProperties, object configuration)
		{
			metadataProperties.SetAnnotation("Configuration", configuration);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00041790 File Offset: 0x0003F990
		public static object GetAnnotation(this IEnumerable<MetadataProperty> metadataProperties, string name)
		{
			foreach (MetadataProperty metadataProperty in metadataProperties)
			{
				if (metadataProperty.Name.Equals(name, StringComparison.Ordinal))
				{
					return metadataProperty.Value;
				}
			}
			return null;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00041808 File Offset: 0x0003FA08
		public static void SetAnnotation(this ICollection<MetadataProperty> metadataProperties, string name, object value)
		{
			MetadataProperty metadataProperty = metadataProperties.SingleOrDefault((MetadataProperty p) => p.Name.Equals(name, StringComparison.Ordinal));
			if (metadataProperty == null)
			{
				metadataProperty = MetadataProperty.CreateAnnotation(name, value);
				metadataProperties.Add(metadataProperty);
				return;
			}
			metadataProperty.Value = value;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00041870 File Offset: 0x0003FA70
		public static void RemoveAnnotation(this ICollection<MetadataProperty> metadataProperties, string name)
		{
			MetadataProperty metadataProperty = metadataProperties.SingleOrDefault((MetadataProperty p) => p.Name.Equals(name, StringComparison.Ordinal));
			if (metadataProperty != null)
			{
				metadataProperties.Remove(metadataProperty);
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000418A8 File Offset: 0x0003FAA8
		public static void Copy(this ICollection<MetadataProperty> sourceAnnotations, ICollection<MetadataProperty> targetAnnotations)
		{
			foreach (MetadataProperty metadataProperty in sourceAnnotations)
			{
				targetAnnotations.SetAnnotation(metadataProperty.Name, metadataProperty.Value);
			}
		}

		// Token: 0x0400042E RID: 1070
		private const string ClrPropertyInfoAnnotation = "ClrPropertyInfo";

		// Token: 0x0400042F RID: 1071
		private const string ClrAttributesAnnotation = "ClrAttributes";

		// Token: 0x04000430 RID: 1072
		private const string ConfiguationAnnotation = "Configuration";
	}
}

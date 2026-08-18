using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x02000142 RID: 322
	public class IndexAnnotationSerializer : IMetadataAnnotationSerializer
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x0003643C File Offset: 0x0003463C
		public virtual string Serialize(string name, object value)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<object>(value, "value");
			IndexAnnotation indexAnnotation = value as IndexAnnotation;
			if (indexAnnotation == null)
			{
				throw new ArgumentException(Strings.AnnotationSerializeWrongType(value.GetType().Name, typeof(IndexAnnotationSerializer).Name, typeof(IndexAnnotation).Name));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IndexAttribute indexAttribute in indexAnnotation.Indexes)
			{
				stringBuilder.Append(IndexAnnotationSerializer.SerializeIndexAttribute(indexAttribute));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000364F4 File Offset: 0x000346F4
		internal static string SerializeIndexAttribute(IndexAttribute indexAttribute)
		{
			StringBuilder stringBuilder = new StringBuilder("{ ");
			if (!string.IsNullOrWhiteSpace(indexAttribute.Name))
			{
				stringBuilder.Append("Name: ").Append(indexAttribute.Name.Replace(",", "\\,").Replace("{", "\\{"));
			}
			if (indexAttribute.Order != -1)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Order: ").Append(indexAttribute.Order);
			}
			if (indexAttribute.IsClusteredConfigured)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("IsClustered: ").Append(indexAttribute.IsClustered);
			}
			if (indexAttribute.IsUniqueConfigured)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("IsUnique: ").Append(indexAttribute.IsUnique);
			}
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00036624 File Offset: 0x00034824
		public virtual object Deserialize(string name, string value)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(value, "value");
			value = value.Trim();
			if (!value.StartsWith("{", StringComparison.Ordinal) || !value.EndsWith("}", StringComparison.Ordinal))
			{
				throw IndexAnnotationSerializer.BuildFormatException(value);
			}
			List<IndexAttribute> list = new List<IndexAttribute>();
			List<string> list2 = (from s in IndexAnnotationSerializer._indexesSplitter.Split(value)
			select s.Trim()).ToList<string>();
			list2[0] = list2[0].Substring(1);
			int index = list2.Count - 1;
			list2[index] = list2[index].Substring(0, list2[index].Length - 1);
			foreach (string text in list2)
			{
				IndexAttribute indexAttribute = new IndexAttribute();
				if (!string.IsNullOrWhiteSpace(text))
				{
					foreach (string text2 in from s in IndexAnnotationSerializer._indexPartsSplitter.Split(text)
					select s.Trim())
					{
						if (text2.StartsWith("Name:", StringComparison.Ordinal))
						{
							string text3 = text2.Substring(5).Trim();
							if (string.IsNullOrWhiteSpace(text3) || !string.IsNullOrWhiteSpace(indexAttribute.Name))
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.Name = text3.Replace("\\,", ",").Replace("\\{", "{");
						}
						else if (text2.StartsWith("Order:", StringComparison.Ordinal))
						{
							int order;
							if (!int.TryParse(text2.Substring(6).Trim(), out order) || indexAttribute.Order != -1)
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.Order = order;
						}
						else if (text2.StartsWith("IsClustered:", StringComparison.Ordinal))
						{
							bool isClustered;
							if (!bool.TryParse(text2.Substring(12).Trim(), out isClustered) || indexAttribute.IsClusteredConfigured)
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.IsClustered = isClustered;
						}
						else
						{
							if (!text2.StartsWith("IsUnique:", StringComparison.Ordinal))
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							bool isUnique;
							if (!bool.TryParse(text2.Substring(9).Trim(), out isUnique) || indexAttribute.IsUniqueConfigured)
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.IsUnique = isUnique;
						}
					}
				}
				list.Add(indexAttribute);
			}
			return new IndexAnnotation(list);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00036904 File Offset: 0x00034B04
		private static FormatException BuildFormatException(string value)
		{
			return new FormatException(Strings.AnnotationSerializeBadFormat(value, typeof(IndexAnnotationSerializer).Name, "{ Name: MyIndex, Order: 7, IsClustered: True, IsUnique: False } { } { Name: MyOtherIndex }"));
		}

		// Token: 0x040002D9 RID: 729
		internal const string FormatExample = "{ Name: MyIndex, Order: 7, IsClustered: True, IsUnique: False } { } { Name: MyOtherIndex }";

		// Token: 0x040002DA RID: 730
		private static readonly Regex _indexesSplitter = new Regex("(?<!\\\\)}\\s*{", RegexOptions.Compiled);

		// Token: 0x040002DB RID: 731
		private static readonly Regex _indexPartsSplitter = new Regex("(?<!\\\\),", RegexOptions.Compiled);
	}
}

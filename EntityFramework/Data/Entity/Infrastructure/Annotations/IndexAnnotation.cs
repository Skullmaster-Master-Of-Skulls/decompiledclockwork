using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x02000140 RID: 320
	public class IndexAnnotation : IMergeableAnnotation
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x000360DC File Offset: 0x000342DC
		public IndexAnnotation(IndexAttribute indexAttribute)
		{
			Check.NotNull<IndexAttribute>(indexAttribute, "indexAttribute");
			this._indexes.Add(indexAttribute);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00036107 File Offset: 0x00034307
		public IndexAnnotation(IEnumerable<IndexAttribute> indexAttributes)
		{
			Check.NotNull<IEnumerable<IndexAttribute>>(indexAttributes, "indexAttributes");
			IndexAnnotation.MergeLists(this._indexes, indexAttributes, null);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00036133 File Offset: 0x00034333
		internal IndexAnnotation(PropertyInfo propertyInfo, IEnumerable<IndexAttribute> indexAttributes)
		{
			Check.NotNull<IEnumerable<IndexAttribute>>(indexAttributes, "indexAttributes");
			IndexAnnotation.MergeLists(this._indexes, indexAttributes, propertyInfo);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00036180 File Offset: 0x00034380
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private static void MergeLists(ICollection<IndexAttribute> existingIndexes, IEnumerable<IndexAttribute> newIndexes, PropertyInfo propertyInfo)
		{
			using (IEnumerator<IndexAttribute> enumerator = newIndexes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IndexAttribute index = enumerator.Current;
					if (index == null)
					{
						throw new ArgumentNullException("indexAttribute");
					}
					IndexAttribute indexAttribute = existingIndexes.SingleOrDefault((IndexAttribute i) => i.Name == index.Name);
					if (indexAttribute == null)
					{
						existingIndexes.Add(index);
					}
					else
					{
						CompatibilityResult compatibilityResult = index.IsCompatibleWith(indexAttribute, false);
						if (!compatibilityResult)
						{
							string text = Environment.NewLine + "\t" + compatibilityResult.ErrorMessage;
							throw new InvalidOperationException((propertyInfo == null) ? Strings.ConflictingIndexAttribute(indexAttribute.Name, text) : Strings.ConflictingIndexAttributesOnProperty(propertyInfo.Name, propertyInfo.ReflectedType.Name, indexAttribute.Name, text));
						}
						existingIndexes.Remove(indexAttribute);
						existingIndexes.Add(index.MergeWith(indexAttribute, false));
					}
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000362A4 File Offset: 0x000344A4
		public virtual IEnumerable<IndexAttribute> Indexes
		{
			get
			{
				return this._indexes;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000362CC File Offset: 0x000344CC
		public virtual CompatibilityResult IsCompatibleWith(object other)
		{
			if (object.ReferenceEquals(this, other) || other == null)
			{
				return new CompatibilityResult(true, null);
			}
			IndexAnnotation indexAnnotation = other as IndexAnnotation;
			if (indexAnnotation == null)
			{
				return new CompatibilityResult(false, Strings.IncompatibleTypes(other.GetType().Name, typeof(IndexAnnotation).Name));
			}
			using (IEnumerator<IndexAttribute> enumerator = indexAnnotation._indexes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IndexAttribute newIndex = enumerator.Current;
					IndexAttribute indexAttribute = this._indexes.SingleOrDefault((IndexAttribute i) => i.Name == newIndex.Name);
					if (indexAttribute != null)
					{
						CompatibilityResult result = indexAttribute.IsCompatibleWith(newIndex, false);
						if (!result)
						{
							return result;
						}
					}
				}
			}
			return new CompatibilityResult(true, null);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000363B4 File Offset: 0x000345B4
		public virtual object MergeWith(object other)
		{
			if (object.ReferenceEquals(this, other) || other == null)
			{
				return this;
			}
			IndexAnnotation indexAnnotation = other as IndexAnnotation;
			if (indexAnnotation == null)
			{
				throw new ArgumentException(Strings.IncompatibleTypes(other.GetType().Name, typeof(IndexAnnotation).Name));
			}
			List<IndexAttribute> list = this._indexes.ToList<IndexAttribute>();
			IndexAnnotation.MergeLists(list, indexAnnotation._indexes, null);
			return new IndexAnnotation(list);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0003641D File Offset: 0x0003461D
		public override string ToString()
		{
			return "IndexAnnotation: " + new IndexAnnotationSerializer().Serialize("Index", this);
		}

		// Token: 0x040002D7 RID: 727
		public const string AnnotationName = "Index";

		// Token: 0x040002D8 RID: 728
		private readonly IList<IndexAttribute> _indexes = new List<IndexAttribute>();
	}
}

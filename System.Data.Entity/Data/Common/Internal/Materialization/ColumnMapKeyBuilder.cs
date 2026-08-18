using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Data.Query.InternalTrees;
using System.Globalization;
using System.Text;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003C6 RID: 966
	internal class ColumnMapKeyBuilder : ColumnMapVisitor<int>
	{
		// Token: 0x06003431 RID: 13361 RVA: 0x000C9C52 File Offset: 0x000C7E52
		private ColumnMapKeyBuilder(SpanIndex spanIndex)
		{
			this._spanIndex = spanIndex;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000C9C6C File Offset: 0x000C7E6C
		internal static string GetColumnMapKey(ColumnMap columnMap, SpanIndex spanIndex)
		{
			ColumnMapKeyBuilder columnMapKeyBuilder = new ColumnMapKeyBuilder(spanIndex);
			columnMap.Accept<int>(columnMapKeyBuilder, 0);
			return columnMapKeyBuilder._builder.ToString();
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000C9C93 File Offset: 0x000C7E93
		internal void Append(string value)
		{
			this._builder.Append(value);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000C9CA2 File Offset: 0x000C7EA2
		internal void Append(string prefix, Type type)
		{
			this.Append(prefix, type.AssemblyQualifiedName);
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000C9CB4 File Offset: 0x000C7EB4
		internal void Append(string prefix, TypeUsage type)
		{
			if (type != null)
			{
				InitializerMetadata initializerMetadata;
				if (InitializerMetadata.TryGetInitializerMetadata(type, out initializerMetadata))
				{
					initializerMetadata.AppendColumnMapKey(this);
				}
				this.Append(prefix, type.EdmType);
			}
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000C9CE4 File Offset: 0x000C7EE4
		internal void Append(string prefix, EdmType type)
		{
			if (type != null)
			{
				this.Append(prefix, type.NamespaceName);
				this.Append(".", type.Name);
				if (type.BuiltInTypeKind == BuiltInTypeKind.RowType && this._spanIndex != null)
				{
					this.Append("<<");
					Dictionary<int, AssociationEndMember> spanMap = this._spanIndex.GetSpanMap((RowType)type);
					if (spanMap != null)
					{
						string value = string.Empty;
						foreach (KeyValuePair<int, AssociationEndMember> keyValuePair in spanMap)
						{
							this.Append(value);
							this.AppendValue("C", keyValuePair.Key);
							this.Append(":", keyValuePair.Value.DeclaringType);
							this.Append(".", keyValuePair.Value.Name);
							value = ",";
						}
					}
					this.Append(">>");
				}
			}
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000C9DF0 File Offset: 0x000C7FF0
		private void Append(string prefix, string value)
		{
			this.Append(prefix);
			this.Append("'");
			this.Append(value);
			this.Append("'");
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000C9E16 File Offset: 0x000C8016
		private void Append(string prefix, ColumnMap columnMap)
		{
			this.Append(prefix);
			this.Append("[");
			if (columnMap != null)
			{
				columnMap.Accept<int>(this, 0);
			}
			this.Append("]");
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000C9E40 File Offset: 0x000C8040
		private void Append(string prefix, IEnumerable<ColumnMap> elements)
		{
			this.Append(prefix);
			this.Append("{");
			if (elements != null)
			{
				string prefix2 = string.Empty;
				foreach (ColumnMap columnMap in elements)
				{
					this.Append(prefix2, columnMap);
					prefix2 = ",";
				}
			}
			this.Append("}");
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000C9EB8 File Offset: 0x000C80B8
		private void Append(string prefix, EntityIdentity entityIdentity)
		{
			this.Append(prefix);
			this.Append("[");
			this.Append(",K", entityIdentity.Keys);
			SimpleEntityIdentity simpleEntityIdentity = entityIdentity as SimpleEntityIdentity;
			if (simpleEntityIdentity != null)
			{
				this.Append(",", simpleEntityIdentity.EntitySet);
			}
			else
			{
				DiscriminatedEntityIdentity discriminatedEntityIdentity = (DiscriminatedEntityIdentity)entityIdentity;
				this.Append("CM", discriminatedEntityIdentity.EntitySetColumnMap);
				foreach (EntitySet entitySet in discriminatedEntityIdentity.EntitySetMap)
				{
					this.Append(",E", entitySet);
				}
			}
			this.Append("]");
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000C9F4F File Offset: 0x000C814F
		private void Append(string prefix, EntitySet entitySet)
		{
			if (entitySet != null)
			{
				this.Append(prefix, entitySet.EntityContainer.Name);
				this.Append(".", entitySet.Name);
			}
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000C9F77 File Offset: 0x000C8177
		private void AppendValue(string prefix, object value)
		{
			this.Append(prefix, string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value
			}));
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000C9F99 File Offset: 0x000C8199
		internal override void Visit(ComplexTypeColumnMap columnMap, int dummy)
		{
			this.Append("C-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000C9FD0 File Offset: 0x000C81D0
		internal override void Visit(DiscriminatedCollectionColumnMap columnMap, int dummy)
		{
			this.Append("DC-D", columnMap.Discriminator);
			this.AppendValue(",DV", columnMap.DiscriminatorValue);
			this.Append(",FK", columnMap.ForeignKeys);
			this.Append(",K", columnMap.Keys);
			this.Append(",E", columnMap.Element);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000CA034 File Offset: 0x000C8234
		internal override void Visit(EntityColumnMap columnMap, int dummy)
		{
			this.Append("E-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
			this.Append(",I", columnMap.EntityIdentity);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000CA088 File Offset: 0x000C8288
		internal override void Visit(SimplePolymorphicColumnMap columnMap, int dummy)
		{
			this.Append("SP-", columnMap.Type);
			this.Append(",D", columnMap.TypeDiscriminator);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				this.AppendValue(",K", keyValuePair.Key);
				this.Append(":", keyValuePair.Value);
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000CA13C File Offset: 0x000C833C
		internal override void Visit(RecordColumnMap columnMap, int dummy)
		{
			this.Append("R-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000CA174 File Offset: 0x000C8374
		internal override void Visit(RefColumnMap columnMap, int dummy)
		{
			this.Append("Ref-", columnMap.EntityIdentity);
			EntityType type;
			bool flag = TypeHelpers.TryGetRefEntityType(columnMap.Type, out type);
			this.Append(",T", type);
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000CA1AC File Offset: 0x000C83AC
		internal override void Visit(ScalarColumnMap columnMap, int dummy)
		{
			string value = string.Format(CultureInfo.InvariantCulture, "S({0}-{1}:{2})", new object[]
			{
				columnMap.CommandId,
				columnMap.ColumnPos,
				columnMap.Type.Identity
			});
			this.Append(value);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000CA200 File Offset: 0x000C8400
		internal override void Visit(SimpleCollectionColumnMap columnMap, int dummy)
		{
			this.Append("DC-FK", columnMap.ForeignKeys);
			this.Append(",K", columnMap.Keys);
			this.Append(",E", columnMap.Element);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void Visit(VarRefColumnMap columnMap, int dummy)
		{
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000CA235 File Offset: 0x000C8435
		internal override void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, int dummy)
		{
			this.Append(string.Format(CultureInfo.InvariantCulture, "MD-{0}", new object[]
			{
				Guid.NewGuid()
			}));
		}

		// Token: 0x040016CA RID: 5834
		private readonly StringBuilder _builder = new StringBuilder();

		// Token: 0x040016CB RID: 5835
		private readonly SpanIndex _spanIndex;
	}
}

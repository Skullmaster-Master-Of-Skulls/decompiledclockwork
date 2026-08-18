using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002D9 RID: 729
	internal class ColumnMapKeyBuilder : ColumnMapVisitor<int>
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x0007F476 File Offset: 0x0007D676
		private ColumnMapKeyBuilder(SpanIndex spanIndex)
		{
			this._spanIndex = spanIndex;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0007F490 File Offset: 0x0007D690
		internal static string GetColumnMapKey(ColumnMap columnMap, SpanIndex spanIndex)
		{
			ColumnMapKeyBuilder columnMapKeyBuilder = new ColumnMapKeyBuilder(spanIndex);
			columnMap.Accept<int>(columnMapKeyBuilder, 0);
			return columnMapKeyBuilder._builder.ToString();
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0007F4B7 File Offset: 0x0007D6B7
		internal void Append(string value)
		{
			this._builder.Append(value);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0007F4C6 File Offset: 0x0007D6C6
		internal void Append(string prefix, Type type)
		{
			this.Append(prefix, type.AssemblyQualifiedName);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0007F4D8 File Offset: 0x0007D6D8
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

		// Token: 0x06001985 RID: 6533 RVA: 0x0007F508 File Offset: 0x0007D708
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

		// Token: 0x06001986 RID: 6534 RVA: 0x0007F614 File Offset: 0x0007D814
		private void Append(string prefix, string value)
		{
			this.Append(prefix);
			this.Append("'");
			this.Append(value);
			this.Append("'");
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0007F63A File Offset: 0x0007D83A
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

		// Token: 0x06001988 RID: 6536 RVA: 0x0007F664 File Offset: 0x0007D864
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

		// Token: 0x06001989 RID: 6537 RVA: 0x0007F6DC File Offset: 0x0007D8DC
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

		// Token: 0x0600198A RID: 6538 RVA: 0x0007F776 File Offset: 0x0007D976
		private void Append(string prefix, EntitySet entitySet)
		{
			if (entitySet != null)
			{
				this.Append(prefix, entitySet.EntityContainer.Name);
				this.Append(".", entitySet.Name);
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0007F7A0 File Offset: 0x0007D9A0
		private void AppendValue(string prefix, object value)
		{
			this.Append(prefix, string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value
			}));
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0007F7CF File Offset: 0x0007D9CF
		internal override void Visit(ComplexTypeColumnMap columnMap, int dummy)
		{
			this.Append("C-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0007F804 File Offset: 0x0007DA04
		internal override void Visit(DiscriminatedCollectionColumnMap columnMap, int dummy)
		{
			this.Append("DC-D", columnMap.Discriminator);
			this.AppendValue(",DV", columnMap.DiscriminatorValue);
			this.Append(",FK", columnMap.ForeignKeys);
			this.Append(",K", columnMap.Keys);
			this.Append(",E", columnMap.Element);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0007F868 File Offset: 0x0007DA68
		internal override void Visit(EntityColumnMap columnMap, int dummy)
		{
			this.Append("E-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
			this.Append(",I", columnMap.EntityIdentity);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0007F8BC File Offset: 0x0007DABC
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

		// Token: 0x06001990 RID: 6544 RVA: 0x0007F970 File Offset: 0x0007DB70
		internal override void Visit(RecordColumnMap columnMap, int dummy)
		{
			this.Append("R-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0007F9A8 File Offset: 0x0007DBA8
		internal override void Visit(RefColumnMap columnMap, int dummy)
		{
			this.Append("Ref-", columnMap.EntityIdentity);
			EntityType type;
			TypeHelpers.TryGetRefEntityType(columnMap.Type, out type);
			this.Append(",T", type);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0007F9E0 File Offset: 0x0007DBE0
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

		// Token: 0x06001993 RID: 6547 RVA: 0x0007FA36 File Offset: 0x0007DC36
		internal override void Visit(SimpleCollectionColumnMap columnMap, int dummy)
		{
			this.Append("DC-FK", columnMap.ForeignKeys);
			this.Append(",K", columnMap.Keys);
			this.Append(",E", columnMap.Element);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0007FA6B File Offset: 0x0007DC6B
		internal override void Visit(VarRefColumnMap columnMap, int dummy)
		{
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0007FA70 File Offset: 0x0007DC70
		internal override void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, int dummy)
		{
			this.Append(string.Format(CultureInfo.InvariantCulture, "MD-{0}", new object[]
			{
				Guid.NewGuid()
			}));
		}

		// Token: 0x040008CD RID: 2253
		private readonly StringBuilder _builder = new StringBuilder();

		// Token: 0x040008CE RID: 2254
		private readonly SpanIndex _spanIndex;
	}
}

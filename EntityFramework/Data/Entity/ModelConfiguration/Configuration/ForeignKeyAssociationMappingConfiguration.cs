using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007CE RID: 1998
	public sealed class ForeignKeyAssociationMappingConfiguration : AssociationMappingConfiguration
	{
		// Token: 0x06005AA8 RID: 23208 RVA: 0x001867F8 File Offset: 0x001849F8
		internal ForeignKeyAssociationMappingConfiguration()
		{
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00186818 File Offset: 0x00184A18
		private ForeignKeyAssociationMappingConfiguration(ForeignKeyAssociationMappingConfiguration source)
		{
			this._keyColumnNames.AddRange(source._keyColumnNames);
			this._tableName = source._tableName;
			foreach (KeyValuePair<Tuple<string, string>, object> item in source._annotations)
			{
				this._annotations.Add(item);
			}
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x001868A4 File Offset: 0x00184AA4
		internal override AssociationMappingConfiguration Clone()
		{
			return new ForeignKeyAssociationMappingConfiguration(this);
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x001868AC File Offset: 0x00184AAC
		public ForeignKeyAssociationMappingConfiguration MapKey(params string[] keyColumnNames)
		{
			Check.NotNull<string[]>(keyColumnNames, "keyColumnNames");
			this._keyColumnNames.Clear();
			this._keyColumnNames.AddRange(keyColumnNames);
			return this;
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x001868D2 File Offset: 0x00184AD2
		public ForeignKeyAssociationMappingConfiguration HasColumnAnnotation(string keyColumnName, string annotationName, object value)
		{
			Check.NotEmpty(keyColumnName, "keyColumnName");
			Check.NotEmpty(annotationName, "annotationName");
			this._annotations[Tuple.Create<string, string>(keyColumnName, annotationName)] = value;
			return this;
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x00186900 File Offset: 0x00184B00
		public ForeignKeyAssociationMappingConfiguration ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			return this.ToTable(tableName, null);
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x00186916 File Offset: 0x00184B16
		public ForeignKeyAssociationMappingConfiguration ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._tableName = new DatabaseName(tableName, schemaName);
			return this;
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x00186B40 File Offset: 0x00184D40
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal override void Configure(AssociationSetMapping associationSetMapping, EdmModel database, PropertyInfo navigationProperty)
		{
			ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass10 CS$<>8__locals1 = new ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass10();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.propertyMappings = associationSetMapping.SourceEndMapping.PropertyMappings.ToList<ScalarPropertyMapping>();
			if (this._tableName != null)
			{
				ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass13 CS$<>8__locals2 = new ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass13();
				CS$<>8__locals2.CS$<>8__locals11 = CS$<>8__locals1;
				ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass13 CS$<>8__locals3 = CS$<>8__locals2;
				EntityType targetTable;
				if ((targetTable = (from t in database.EntityTypes
				let n = t.GetTableName()
				where n != null && n.Equals(this._tableName)
				select t).SingleOrDefault<EntityType>()) == null)
				{
					targetTable = (from es in database.GetEntitySets()
					where string.Equals(es.Table, this._tableName.Name, StringComparison.Ordinal)
					select es.ElementType).SingleOrDefault<EntityType>();
				}
				CS$<>8__locals3.targetTable = targetTable;
				if (CS$<>8__locals2.targetTable == null)
				{
					throw Error.TableNotFound(this._tableName);
				}
				CS$<>8__locals2.sourceTable = associationSetMapping.Table;
				if (CS$<>8__locals2.sourceTable != CS$<>8__locals2.targetTable)
				{
					ForeignKeyBuilder foreignKeyBuilder = CS$<>8__locals2.sourceTable.ForeignKeyBuilders.Single((ForeignKeyBuilder fk) => fk.DependentColumns.SequenceEqual(from pm in CS$<>8__locals1.propertyMappings
					select pm.Column));
					CS$<>8__locals2.sourceTable.RemoveForeignKey(foreignKeyBuilder);
					CS$<>8__locals2.targetTable.AddForeignKey(foreignKeyBuilder);
					foreignKeyBuilder.DependentColumns.Each(delegate(EdmProperty c)
					{
						bool isPrimaryKeyColumn = c.IsPrimaryKeyColumn;
						CS$<>8__locals2.sourceTable.RemoveMember(c);
						CS$<>8__locals2.targetTable.AddMember(c);
						if (isPrimaryKeyColumn)
						{
							CS$<>8__locals2.targetTable.AddKeyMember(c);
						}
					});
					associationSetMapping.StoreEntitySet = database.GetEntitySet(CS$<>8__locals2.targetTable);
				}
			}
			if (this._keyColumnNames.Count > 0 && this._keyColumnNames.Count != CS$<>8__locals1.propertyMappings.Count<ScalarPropertyMapping>())
			{
				throw Error.IncorrectColumnCount(string.Join(", ", this._keyColumnNames));
			}
			this._keyColumnNames.Each(delegate(string n, int i)
			{
				CS$<>8__locals1.propertyMappings[i].Column.Name = n;
			});
			foreach (KeyValuePair<Tuple<string, string>, object> keyValuePair in this._annotations)
			{
				int num = this._keyColumnNames.IndexOf(keyValuePair.Key.Item1);
				if (num == -1)
				{
					throw new InvalidOperationException(Strings.BadKeyNameForAnnotation(keyValuePair.Key.Item1, keyValuePair.Key.Item2));
				}
				CS$<>8__locals1.propertyMappings[num].Column.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + keyValuePair.Key.Item2, keyValuePair.Value);
			}
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x00186DF8 File Offset: 0x00184FF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x00186E14 File Offset: 0x00185014
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(ForeignKeyAssociationMappingConfiguration other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (object.Equals(other._tableName, this._tableName) && other._keyColumnNames.SequenceEqual(this._keyColumnNames))
			{
				return (from a in other._annotations
				orderby a.Key
				select a).SequenceEqual(from a in this._annotations
				orderby a.Key
				select a);
			}
			return false;
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x00186EB4 File Offset: 0x001850B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(ForeignKeyAssociationMappingConfiguration)) && this.Equals((ForeignKeyAssociationMappingConfiguration)obj)));
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x00186F24 File Offset: 0x00185124
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			int seed = ((this._tableName != null) ? this._tableName.GetHashCode() : 0) * 397;
			seed = this._keyColumnNames.Aggregate(seed, (int h, string v) => h * 397 ^ v.GetHashCode());
			return (from a in this._annotations
			orderby a.Key
			select a).Aggregate(seed, (int h, KeyValuePair<Tuple<string, string>, object> v) => h * 397 ^ v.GetHashCode());
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x00186FC3 File Offset: 0x001851C3
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002420 RID: 9248
		private readonly List<string> _keyColumnNames = new List<string>();

		// Token: 0x04002421 RID: 9249
		private readonly IDictionary<Tuple<string, string>, object> _annotations = new Dictionary<Tuple<string, string>, object>();

		// Token: 0x04002422 RID: 9250
		private DatabaseName _tableName;
	}
}

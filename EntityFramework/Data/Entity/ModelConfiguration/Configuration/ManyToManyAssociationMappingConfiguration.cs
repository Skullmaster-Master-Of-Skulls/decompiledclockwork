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
	// Token: 0x020007CF RID: 1999
	public sealed class ManyToManyAssociationMappingConfiguration : AssociationMappingConfiguration
	{
		// Token: 0x06005ABF RID: 23231 RVA: 0x00186FCB File Offset: 0x001851CB
		internal ManyToManyAssociationMappingConfiguration()
		{
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x00186FF4 File Offset: 0x001851F4
		private ManyToManyAssociationMappingConfiguration(ManyToManyAssociationMappingConfiguration source)
		{
			this._leftKeyColumnNames.AddRange(source._leftKeyColumnNames);
			this._rightKeyColumnNames.AddRange(source._rightKeyColumnNames);
			this._tableName = source._tableName;
			foreach (KeyValuePair<string, object> item in source._annotations)
			{
				this._annotations.Add(item);
			}
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x0018709C File Offset: 0x0018529C
		internal override AssociationMappingConfiguration Clone()
		{
			return new ManyToManyAssociationMappingConfiguration(this);
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x001870A4 File Offset: 0x001852A4
		public ManyToManyAssociationMappingConfiguration ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			return this.ToTable(tableName, null);
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x001870BA File Offset: 0x001852BA
		public ManyToManyAssociationMappingConfiguration ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._tableName = new DatabaseName(tableName, schemaName);
			return this;
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x001870D6 File Offset: 0x001852D6
		public ManyToManyAssociationMappingConfiguration HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
			return this;
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x00187106 File Offset: 0x00185306
		public ManyToManyAssociationMappingConfiguration MapLeftKey(params string[] keyColumnNames)
		{
			Check.NotNull<string[]>(keyColumnNames, "keyColumnNames");
			this._leftKeyColumnNames.Clear();
			this._leftKeyColumnNames.AddRange(keyColumnNames);
			return this;
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x0018712C File Offset: 0x0018532C
		public ManyToManyAssociationMappingConfiguration MapRightKey(params string[] keyColumnNames)
		{
			Check.NotNull<string[]>(keyColumnNames, "keyColumnNames");
			this._rightKeyColumnNames.Clear();
			this._rightKeyColumnNames.AddRange(keyColumnNames);
			return this;
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x00187154 File Offset: 0x00185354
		internal override void Configure(AssociationSetMapping associationSetMapping, EdmModel database, PropertyInfo navigationProperty)
		{
			EntityType table = associationSetMapping.Table;
			if (this._tableName != null)
			{
				table.SetTableName(this._tableName);
				table.SetConfiguration(this);
			}
			bool flag = navigationProperty.IsSameAs(associationSetMapping.SourceEndMapping.AssociationEnd.GetClrPropertyInfo());
			ManyToManyAssociationMappingConfiguration.ConfigureColumnNames(flag ? this._leftKeyColumnNames : this._rightKeyColumnNames, associationSetMapping.SourceEndMapping.PropertyMappings.ToList<ScalarPropertyMapping>());
			ManyToManyAssociationMappingConfiguration.ConfigureColumnNames(flag ? this._rightKeyColumnNames : this._leftKeyColumnNames, associationSetMapping.TargetEndMapping.PropertyMappings.ToList<ScalarPropertyMapping>());
			foreach (KeyValuePair<string, object> keyValuePair in this._annotations)
			{
				table.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x00187264 File Offset: 0x00185464
		private static void ConfigureColumnNames(ICollection<string> keyColumnNames, IList<ScalarPropertyMapping> propertyMappings)
		{
			if (keyColumnNames.Count > 0 && keyColumnNames.Count != propertyMappings.Count)
			{
				throw Error.IncorrectColumnCount(string.Join(", ", keyColumnNames));
			}
			keyColumnNames.Each(delegate(string n, int i)
			{
				propertyMappings[i].Column.Name = n;
			});
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x001872BD File Offset: 0x001854BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x001872D8 File Offset: 0x001854D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(ManyToManyAssociationMappingConfiguration other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (!object.Equals(other._tableName, this._tableName))
			{
				return false;
			}
			if (object.Equals(other._tableName, this._tableName) && ((this._leftKeyColumnNames.SequenceEqual(other._leftKeyColumnNames) && this._rightKeyColumnNames.SequenceEqual(other._rightKeyColumnNames)) || (this._leftKeyColumnNames.SequenceEqual(other._rightKeyColumnNames) && this._rightKeyColumnNames.SequenceEqual(other._leftKeyColumnNames))))
			{
				return (from a in this._annotations
				orderby a.Key
				select a).SequenceEqual(from a in other._annotations
				orderby a.Key
				select a);
			}
			return false;
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x001873C9 File Offset: 0x001855C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(ManyToManyAssociationMappingConfiguration)) && this.Equals((ManyToManyAssociationMappingConfiguration)obj)));
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x00187448 File Offset: 0x00185648
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			int seed = ((this._tableName != null) ? this._tableName.GetHashCode() : 0) * 397;
			seed = this._leftKeyColumnNames.Aggregate(seed, (int h, string v) => h * 397 ^ v.GetHashCode());
			seed = this._rightKeyColumnNames.Aggregate(seed, (int h, string v) => h * 397 ^ v.GetHashCode());
			return (from a in this._annotations
			orderby a.Key
			select a).Aggregate(seed, (int h, KeyValuePair<string, object> v) => h * 397 ^ v.GetHashCode());
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x00187511 File Offset: 0x00185711
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400242B RID: 9259
		private readonly List<string> _leftKeyColumnNames = new List<string>();

		// Token: 0x0400242C RID: 9260
		private readonly List<string> _rightKeyColumnNames = new List<string>();

		// Token: 0x0400242D RID: 9261
		private DatabaseName _tableName;

		// Token: 0x0400242E RID: 9262
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();
	}
}

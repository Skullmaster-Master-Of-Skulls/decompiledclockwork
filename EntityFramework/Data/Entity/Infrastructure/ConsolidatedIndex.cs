using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200014E RID: 334
	internal class ConsolidatedIndex
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x000373E4 File Offset: 0x000355E4
		public ConsolidatedIndex(string table, IndexAttribute index)
		{
			this._table = table;
			this._index = index;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00037405 File Offset: 0x00035605
		public ConsolidatedIndex(string table, string column, IndexAttribute index) : this(table, index)
		{
			this._columns[index.Order] = column;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00037468 File Offset: 0x00035668
		public static IEnumerable<ConsolidatedIndex> BuildIndexes(string tableName, IEnumerable<Tuple<string, EdmProperty>> columns)
		{
			List<ConsolidatedIndex> list = new List<ConsolidatedIndex>();
			foreach (Tuple<string, EdmProperty> tuple in columns)
			{
				using (IEnumerator<IndexAttribute> enumerator2 = (from a in tuple.Item2.Annotations
				where a.Name == "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index"
				select a.Value).OfType<IndexAnnotation>().SelectMany((IndexAnnotation a) => a.Indexes).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						IndexAttribute index = enumerator2.Current;
						ConsolidatedIndex consolidatedIndex;
						if (index.Name != null)
						{
							consolidatedIndex = list.FirstOrDefault((ConsolidatedIndex i) => i.Index.Name == index.Name);
						}
						else
						{
							consolidatedIndex = null;
						}
						ConsolidatedIndex consolidatedIndex2 = consolidatedIndex;
						if (consolidatedIndex2 == null)
						{
							list.Add(new ConsolidatedIndex(tableName, tuple.Item1, index));
						}
						else
						{
							consolidatedIndex2.Add(tuple.Item1, index);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000375E8 File Offset: 0x000357E8
		public IndexAttribute Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00037604 File Offset: 0x00035804
		public IEnumerable<string> Columns
		{
			get
			{
				return from c in this._columns
				orderby c.Key
				select c.Value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0003765B File Offset: 0x0003585B
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00037664 File Offset: 0x00035864
		public void Add(string columnName, IndexAttribute index)
		{
			if (this._columns.ContainsKey(index.Order))
			{
				throw new InvalidOperationException(Strings.OrderConflictWhenConsolidating(index.Name, this._table, index.Order, this._columns[index.Order], columnName));
			}
			this._columns[index.Order] = columnName;
			CompatibilityResult compatibilityResult = this._index.IsCompatibleWith(index, true);
			if (!compatibilityResult)
			{
				throw new InvalidOperationException(Strings.ConflictWhenConsolidating(index.Name, this._table, compatibilityResult.ErrorMessage));
			}
			this._index = this._index.MergeWith(index, true);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00037710 File Offset: 0x00035910
		public CreateIndexOperation CreateCreateIndexOperation()
		{
			string[] array = this.Columns.ToArray<string>();
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(null)
			{
				Name = (this._index.Name ?? IndexOperation.BuildDefaultName(array)),
				Table = this._table
			};
			foreach (string item in array)
			{
				createIndexOperation.Columns.Add(item);
			}
			if (this._index.IsClusteredConfigured)
			{
				createIndexOperation.IsClustered = this._index.IsClustered;
			}
			if (this._index.IsUniqueConfigured)
			{
				createIndexOperation.IsUnique = this._index.IsUnique;
			}
			return createIndexOperation;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000377BE File Offset: 0x000359BE
		public DropIndexOperation CreateDropIndexOperation()
		{
			return (DropIndexOperation)this.CreateCreateIndexOperation().Inverse;
		}

		// Token: 0x040002F2 RID: 754
		private readonly string _table;

		// Token: 0x040002F3 RID: 755
		private IndexAttribute _index;

		// Token: 0x040002F4 RID: 756
		private readonly IDictionary<int, string> _columns = new Dictionary<int, string>();
	}
}

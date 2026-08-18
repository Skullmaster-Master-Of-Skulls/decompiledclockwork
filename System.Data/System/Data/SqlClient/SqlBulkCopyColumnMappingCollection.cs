using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020002B7 RID: 695
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x0600233A RID: 9018 RVA: 0x002903F8 File Offset: 0x0028F7F8
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x1700053E RID: 1342
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x00290438 File Offset: 0x0028F838
		// (set) Token: 0x0600233D RID: 9021 RVA: 0x00290458 File Offset: 0x0028F858
		internal bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00290478 File Offset: 0x0028F878
		public SqlBulkCopyColumnMapping Add(SqlBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			this.AssertWriteAccess();
			if ((ADP.IsEmpty(bulkCopyColumnMapping.SourceColumn) && bulkCopyColumnMapping.SourceOrdinal == -1) || (ADP.IsEmpty(bulkCopyColumnMapping.DestinationColumn) && bulkCopyColumnMapping.DestinationOrdinal == -1))
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			base.InnerList.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x002904D8 File Offset: 0x0028F8D8
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00290508 File Offset: 0x0028F908
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00290538 File Offset: 0x0028F938
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x00290568 File Offset: 0x0028F968
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00290598 File Offset: 0x0028F998
		private void AssertWriteAccess()
		{
			if (this.ReadOnly)
			{
				throw SQL.BulkLoadMappingInaccessible();
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x002905B8 File Offset: 0x0028F9B8
		public new void Clear()
		{
			this.AssertWriteAccess();
			base.Clear();
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x002905D8 File Offset: 0x0028F9D8
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return -1 != base.InnerList.IndexOf(value);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x002905F8 File Offset: 0x0028F9F8
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00290618 File Offset: 0x0028FA18
		internal void CreateDefaultMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new SqlBulkCopyColumnMapping(i, i));
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x00290648 File Offset: 0x0028FA48
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00290668 File Offset: 0x0028FA68
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Insert(index, value);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00290688 File Offset: 0x0028FA88
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Remove(value);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x002906A8 File Offset: 0x0028FAA8
		public new void RemoveAt(int index)
		{
			this.AssertWriteAccess();
			base.RemoveAt(index);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x002906C8 File Offset: 0x0028FAC8
		internal void ValidateCollection()
		{
			foreach (object obj in this)
			{
				SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
				SqlBulkCopyColumnMappingCollection.MappingSchema mappingSchema;
				if (sqlBulkCopyColumnMapping.SourceOrdinal != -1)
				{
					if (sqlBulkCopyColumnMapping.DestinationOrdinal != -1)
					{
						mappingSchema = SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsOrdinals;
					}
					else
					{
						mappingSchema = SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsNames;
					}
				}
				else if (sqlBulkCopyColumnMapping.DestinationOrdinal != -1)
				{
					mappingSchema = SqlBulkCopyColumnMappingCollection.MappingSchema.NemesOrdinals;
				}
				else
				{
					mappingSchema = SqlBulkCopyColumnMappingCollection.MappingSchema.NamesNames;
				}
				if (this._mappingSchema == SqlBulkCopyColumnMappingCollection.MappingSchema.Undefined)
				{
					this._mappingSchema = mappingSchema;
				}
				else if (this._mappingSchema != mappingSchema)
				{
					throw SQL.BulkLoadMappingsNamesOrOrdinalsOnly();
				}
			}
		}

		// Token: 0x040016FA RID: 5882
		private bool _readOnly;

		// Token: 0x040016FB RID: 5883
		private SqlBulkCopyColumnMappingCollection.MappingSchema _mappingSchema;

		// Token: 0x020002B8 RID: 696
		private enum MappingSchema
		{
			// Token: 0x040016FD RID: 5885
			Undefined,
			// Token: 0x040016FE RID: 5886
			NamesNames,
			// Token: 0x040016FF RID: 5887
			NemesOrdinals,
			// Token: 0x04001700 RID: 5888
			OrdinalsNames,
			// Token: 0x04001701 RID: 5889
			OrdinalsOrdinals
		}
	}
}

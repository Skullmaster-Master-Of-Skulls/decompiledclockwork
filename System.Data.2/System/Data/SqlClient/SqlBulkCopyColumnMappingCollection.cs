using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001AB RID: 427
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06001909 RID: 6409 RVA: 0x000B17D8 File Offset: 0x000B0BD8
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x17000386 RID: 902
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000B180C File Offset: 0x000B0C0C
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x000B1820 File Offset: 0x000B0C20
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

		// Token: 0x0600190D RID: 6413 RVA: 0x000B1834 File Offset: 0x000B0C34
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

		// Token: 0x0600190E RID: 6414 RVA: 0x000B1888 File Offset: 0x000B0C88
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000B18AC File Offset: 0x000B0CAC
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000B18D0 File Offset: 0x000B0CD0
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000B18F4 File Offset: 0x000B0CF4
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			SqlBulkCopyColumnMapping bulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000B1918 File Offset: 0x000B0D18
		private void AssertWriteAccess()
		{
			if (this.ReadOnly)
			{
				throw SQL.BulkLoadMappingInaccessible();
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000B1934 File Offset: 0x000B0D34
		public new void Clear()
		{
			this.AssertWriteAccess();
			base.Clear();
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000B1950 File Offset: 0x000B0D50
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return -1 != base.InnerList.IndexOf(value);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000B1970 File Offset: 0x000B0D70
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000B198C File Offset: 0x000B0D8C
		internal void CreateDefaultMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new SqlBulkCopyColumnMapping(i, i));
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000B19B8 File Offset: 0x000B0DB8
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000B19D4 File Offset: 0x000B0DD4
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Insert(index, value);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000B19F4 File Offset: 0x000B0DF4
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Remove(value);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000B1A14 File Offset: 0x000B0E14
		public new void RemoveAt(int index)
		{
			this.AssertWriteAccess();
			base.RemoveAt(index);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000B1A30 File Offset: 0x000B0E30
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

		// Token: 0x04000EEE RID: 3822
		private bool _readOnly;

		// Token: 0x04000EEF RID: 3823
		private SqlBulkCopyColumnMappingCollection.MappingSchema _mappingSchema;

		// Token: 0x0200038C RID: 908
		private enum MappingSchema
		{
			// Token: 0x04001FAE RID: 8110
			Undefined,
			// Token: 0x04001FAF RID: 8111
			NamesNames,
			// Token: 0x04001FB0 RID: 8112
			NemesOrdinals,
			// Token: 0x04001FB1 RID: 8113
			OrdinalsNames,
			// Token: 0x04001FB2 RID: 8114
			OrdinalsOrdinals
		}
	}
}

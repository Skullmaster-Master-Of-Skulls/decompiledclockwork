using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000075 RID: 117
	public sealed class OracleBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x0003AF18 File Offset: 0x00039F18
		static OracleBulkCopyColumnMappingCollection()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0003AF28 File Offset: 0x00039F28
		public OracleBulkCopyColumnMapping Add(OracleBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			if (bulkCopyColumnMapping.SourceOrdinal == -1 && OracleBulkCopyColumnMappingCollection.IsEmpty(bulkCopyColumnMapping.SourceColumn))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Column mapping"
				}));
			}
			if (bulkCopyColumnMapping.DestinationOrdinal == -1 && OracleBulkCopyColumnMappingCollection.IsEmpty(bulkCopyColumnMapping.DestinationColumn))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Column mapping"
				}));
			}
			base.InnerList.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0003AFD0 File Offset: 0x00039FD0
		public OracleBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			OracleBulkCopyColumnMapping bulkCopyColumnMapping = new OracleBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0003B00C File Offset: 0x0003A00C
		public OracleBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			OracleBulkCopyColumnMapping bulkCopyColumnMapping = new OracleBulkCopyColumnMapping(sourceColumnIndex, destinationColumn);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0003B048 File Offset: 0x0003A048
		public OracleBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			OracleBulkCopyColumnMapping bulkCopyColumnMapping = new OracleBulkCopyColumnMapping(sourceColumn, destinationColumnIndex);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0003B084 File Offset: 0x0003A084
		public OracleBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			OracleBulkCopyColumnMapping bulkCopyColumnMapping = new OracleBulkCopyColumnMapping(sourceColumn, destinationColumn);
			return this.Add(bulkCopyColumnMapping);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0003B0BE File Offset: 0x0003A0BE
		public new void Clear()
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			base.Clear();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0003B0E4 File Offset: 0x0003A0E4
		public bool Contains(OracleBulkCopyColumnMapping value)
		{
			return -1 != base.InnerList.IndexOf(value);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0003B0F8 File Offset: 0x0003A0F8
		public void CopyTo(OracleBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0003B107 File Offset: 0x0003A107
		public int IndexOf(OracleBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0003B115 File Offset: 0x0003A115
		public void Insert(int index, OracleBulkCopyColumnMapping value)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			base.InnerList.Insert(index, value);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0003B142 File Offset: 0x0003A142
		public void Remove(OracleBulkCopyColumnMapping value)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			base.InnerList.Remove(value);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0003B16E File Offset: 0x0003A16E
		public new void RemoveAt(int index)
		{
			if (this.BulkCopyInProgress)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_IN_PROGRESS, new string[0]));
			}
			base.RemoveAt(index);
		}

		// Token: 0x170000C6 RID: 198
		public OracleBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (OracleBulkCopyColumnMapping)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0003B1B7 File Offset: 0x0003A1B7
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0003B1BF File Offset: 0x0003A1BF
		internal bool BulkCopyInProgress
		{
			get
			{
				return this.m_bulkCopyInProgress;
			}
			set
			{
				this.m_bulkCopyInProgress = value;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0003B1C8 File Offset: 0x0003A1C8
		internal OracleBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0003B1D0 File Offset: 0x0003A1D0
		internal void CreateDefaultColumnMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new OracleBulkCopyColumnMapping(i, i));
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0003B1FC File Offset: 0x0003A1FC
		internal void Sort()
		{
			base.InnerList.Sort();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0003B20C File Offset: 0x0003A20C
		internal void ValidateCollection()
		{
			OracleBulkCopyColumnMappingCollection.MappingType mappingType = OracleBulkCopyColumnMappingCollection.MappingType.Undefined;
			foreach (object obj in base.InnerList)
			{
				OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping = (OracleBulkCopyColumnMapping)obj;
				OracleBulkCopyColumnMappingCollection.MappingType mappingType2;
				if (oracleBulkCopyColumnMapping.SourceOrdinal != -1)
				{
					if (oracleBulkCopyColumnMapping.DestinationOrdinal != -1)
					{
						mappingType2 = OracleBulkCopyColumnMappingCollection.MappingType.IndexIndex;
					}
					else
					{
						mappingType2 = OracleBulkCopyColumnMappingCollection.MappingType.IndexName;
					}
				}
				else if (oracleBulkCopyColumnMapping.DestinationOrdinal != -1)
				{
					mappingType2 = OracleBulkCopyColumnMappingCollection.MappingType.NameIndex;
				}
				else
				{
					mappingType2 = OracleBulkCopyColumnMappingCollection.MappingType.NameName;
				}
				if (mappingType == OracleBulkCopyColumnMappingCollection.MappingType.Undefined)
				{
					mappingType = mappingType2;
				}
				else if (mappingType != mappingType2)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_INV_COL_MAPPINGS, new string[0]));
				}
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0003B2B0 File Offset: 0x0003A2B0
		internal static bool IsEmpty(string str)
		{
			return str == null || 0 == str.Length;
		}

		// Token: 0x04000384 RID: 900
		private bool m_bulkCopyInProgress;

		// Token: 0x02000076 RID: 118
		private enum MappingType
		{
			// Token: 0x04000386 RID: 902
			Undefined,
			// Token: 0x04000387 RID: 903
			NameName,
			// Token: 0x04000388 RID: 904
			NameIndex,
			// Token: 0x04000389 RID: 905
			IndexName,
			// Token: 0x0400038A RID: 906
			IndexIndex
		}
	}
}

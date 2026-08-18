using System;
using System.Data.Common;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x0200023F RID: 575
	internal sealed class Bindings
	{
		// Token: 0x060023AE RID: 9134 RVA: 0x000F5BA8 File Offset: 0x000F4FA8
		private Bindings(int count)
		{
			this._count = count;
			this._dbbindings = new tagDBBINDING[count];
			for (int i = 0; i < this._dbbindings.Length; i++)
			{
				this._dbbindings[i] = new tagDBBINDING();
			}
			this._dbcolumns = new tagDBCOLUMNACCESS[count];
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000F5BFC File Offset: 0x000F4FFC
		internal Bindings(OleDbParameter[] parameters, int collectionChangeID) : this(parameters.Length)
		{
			this._bindInfo = new tagDBPARAMBINDINFO[parameters.Length];
			this._parameters = parameters;
			this._collectionChangeID = collectionChangeID;
			this._ifIRowsetElseIRow = true;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000F5C38 File Offset: 0x000F5038
		internal Bindings(OleDbDataReader dataReader, bool ifIRowsetElseIRow, int count) : this(count)
		{
			this._dataReader = dataReader;
			this._ifIRowsetElseIRow = ifIRowsetElseIRow;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000F5C5C File Offset: 0x000F505C
		internal tagDBPARAMBINDINFO[] BindInfo
		{
			get
			{
				return this._bindInfo;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000F5C70 File Offset: 0x000F5070
		internal tagDBCOLUMNACCESS[] DBColumnAccess
		{
			get
			{
				return this._dbcolumns;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x000F5C84 File Offset: 0x000F5084
		internal int CurrentIndex
		{
			set
			{
				this._index = value;
			}
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000F5C98 File Offset: 0x000F5098
		internal ColumnBinding[] ColumnBindings()
		{
			return this._columnBindings;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000F5CAC File Offset: 0x000F50AC
		internal OleDbParameter[] Parameters()
		{
			return this._parameters;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000F5CC0 File Offset: 0x000F50C0
		internal RowBinding RowBinding()
		{
			return this._rowBinding;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000F5CD4 File Offset: 0x000F50D4
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x000F5CE8 File Offset: 0x000F50E8
		internal bool ForceRebind
		{
			get
			{
				return this._forceRebind;
			}
			set
			{
				this._forceRebind = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x000F5CFC File Offset: 0x000F50FC
		internal IntPtr DataSourceType
		{
			set
			{
				this._bindInfo[this._index].pwszDataSourceType = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x000F5D20 File Offset: 0x000F5120
		internal IntPtr Name
		{
			set
			{
				this._bindInfo[this._index].pwszName = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000F5D44 File Offset: 0x000F5144
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x000F5D78 File Offset: 0x000F5178
		internal IntPtr ParamSize
		{
			get
			{
				if (this._bindInfo != null)
				{
					return this._bindInfo[this._index].ulParamSize;
				}
				return IntPtr.Zero;
			}
			set
			{
				this._bindInfo[this._index].ulParamSize = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x000F5D9C File Offset: 0x000F519C
		internal int Flags
		{
			set
			{
				this._bindInfo[this._index].dwFlags = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x000F5DC0 File Offset: 0x000F51C0
		internal IntPtr Ordinal
		{
			set
			{
				this._dbbindings[this._index].iOrdinal = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x000F5DE0 File Offset: 0x000F51E0
		internal int Part
		{
			set
			{
				this._dbbindings[this._index].dwPart = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x000F5E00 File Offset: 0x000F5200
		internal int ParamIO
		{
			set
			{
				this._dbbindings[this._index].eParamIO = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x000F5E20 File Offset: 0x000F5220
		internal int MaxLen
		{
			set
			{
				this._dbbindings[this._index].obStatus = (IntPtr)this._dataBufferSize;
				this._dbbindings[this._index].obLength = (IntPtr)(this._dataBufferSize + ADP.PtrSize);
				this._dbbindings[this._index].obValue = (IntPtr)(this._dataBufferSize + ADP.PtrSize + ADP.PtrSize);
				this._dataBufferSize += ADP.PtrSize + ADP.PtrSize;
				int dbType = this.DbType;
				if (dbType <= 136)
				{
					if (dbType != 8 && dbType != 12 && dbType != 136)
					{
						goto IL_D9;
					}
				}
				else if (dbType != 138 && dbType != 16512 && dbType != 16514)
				{
					goto IL_D9;
				}
				this._dataBufferSize += System.Data.OleDb.RowBinding.AlignDataSize(value * 2);
				this._needToReset = true;
				goto IL_EC;
				IL_D9:
				this._dataBufferSize += System.Data.OleDb.RowBinding.AlignDataSize(value);
				IL_EC:
				this._dbbindings[this._index].cbMaxLen = (IntPtr)value;
				this._dbcolumns[this._index].cbMaxLen = (IntPtr)value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000F5F50 File Offset: 0x000F5350
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x000F5F70 File Offset: 0x000F5370
		internal int DbType
		{
			get
			{
				return (int)this._dbbindings[this._index].wType;
			}
			set
			{
				this._dbbindings[this._index].wType = (short)value;
				this._dbcolumns[this._index].wType = (short)value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x000F5FAC File Offset: 0x000F53AC
		internal byte Precision
		{
			set
			{
				if (this._bindInfo != null)
				{
					this._bindInfo[this._index].bPrecision = value;
				}
				this._dbbindings[this._index].bPrecision = value;
				this._dbcolumns[this._index].bPrecision = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x000F6004 File Offset: 0x000F5404
		internal byte Scale
		{
			set
			{
				if (this._bindInfo != null)
				{
					this._bindInfo[this._index].bScale = value;
				}
				this._dbbindings[this._index].bScale = value;
				this._dbcolumns[this._index].bScale = value;
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000F605C File Offset: 0x000F545C
		internal int AllocateForAccessor(OleDbDataReader dataReader, int indexStart, int indexForAccessor)
		{
			RowBinding rowBinding = System.Data.OleDb.RowBinding.CreateBuffer(this._count, this._dataBufferSize, this._needToReset);
			this._rowBinding = rowBinding;
			ColumnBinding[] array = rowBinding.SetBindings(dataReader, this, indexStart, indexForAccessor, this._parameters, this._dbbindings, this._ifIRowsetElseIRow);
			this._columnBindings = array;
			if (!this._ifIRowsetElseIRow)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this._dbcolumns[i].pData = rowBinding.DangerousGetDataPtr(array[i].ValueOffset);
				}
			}
			return indexStart + array.Length;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000F60E8 File Offset: 0x000F54E8
		internal void ApplyInputParameters()
		{
			ColumnBinding[] array = this.ColumnBindings();
			OleDbParameter[] array2 = this.Parameters();
			this.RowBinding().StartDataBlock();
			for (int i = 0; i < array2.Length; i++)
			{
				if (ADP.IsDirection(array2[i], ParameterDirection.Input))
				{
					array[i].SetOffset(array2[i].Offset);
					array[i].Value(array2[i].GetCoercedValue());
				}
				else
				{
					array2[i].Value = null;
				}
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000F6154 File Offset: 0x000F5554
		internal void ApplyOutputParameters()
		{
			ColumnBinding[] array = this.ColumnBindings();
			OleDbParameter[] array2 = this.Parameters();
			for (int i = 0; i < array2.Length; i++)
			{
				if (ADP.IsDirection(array2[i], ParameterDirection.Output))
				{
					array2[i].Value = array[i].Value();
				}
			}
			this.CleanupBindings();
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000F61A0 File Offset: 0x000F55A0
		internal bool AreParameterBindingsInvalid(OleDbParameterCollection collection)
		{
			ColumnBinding[] array = this.ColumnBindings();
			if (!this.ForceRebind && collection.ChangeID == this._collectionChangeID && this._parameters.Length == collection.Count)
			{
				for (int i = 0; i < array.Length; i++)
				{
					ColumnBinding columnBinding = array[i];
					if (columnBinding.IsParameterBindingInvalid(collection[i]))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000F6200 File Offset: 0x000F5600
		internal void CleanupBindings()
		{
			RowBinding rowBinding = this.RowBinding();
			if (rowBinding != null)
			{
				rowBinding.ResetValues();
				foreach (ColumnBinding columnBinding in this.ColumnBindings())
				{
					if (columnBinding != null)
					{
						columnBinding.ResetValue();
					}
				}
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000F6240 File Offset: 0x000F5640
		internal void CloseFromConnection()
		{
			if (this._rowBinding != null)
			{
				this._rowBinding.CloseFromConnection();
			}
			this.Dispose();
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000F6268 File Offset: 0x000F5668
		internal OleDbHResult CreateAccessor(UnsafeNativeMethods.IAccessor iaccessor, int flags)
		{
			return this._rowBinding.CreateAccessor(iaccessor, flags, this._columnBindings);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000F6288 File Offset: 0x000F5688
		public void Dispose()
		{
			this._parameters = null;
			this._dataReader = null;
			this._columnBindings = null;
			RowBinding rowBinding = this._rowBinding;
			this._rowBinding = null;
			if (rowBinding != null)
			{
				rowBinding.Dispose();
			}
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000F62C4 File Offset: 0x000F56C4
		internal void GuidKindName(Guid guid, int eKind, IntPtr propid)
		{
			tagDBCOLUMNACCESS[] dbcolumnAccess = this.DBColumnAccess;
			dbcolumnAccess[this._index].columnid.uGuid = guid;
			dbcolumnAccess[this._index].columnid.eKind = eKind;
			dbcolumnAccess[this._index].columnid.ulPropid = propid;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000F6320 File Offset: 0x000F5720
		internal void ParameterStatus(StringBuilder builder)
		{
			ColumnBinding[] array = this.ColumnBindings();
			for (int i = 0; i < array.Length; i++)
			{
				ODB.CommandParameterStatus(builder, i, array[i].StatusValue());
			}
		}

		// Token: 0x04001575 RID: 5493
		private readonly tagDBPARAMBINDINFO[] _bindInfo;

		// Token: 0x04001576 RID: 5494
		private readonly tagDBBINDING[] _dbbindings;

		// Token: 0x04001577 RID: 5495
		private readonly tagDBCOLUMNACCESS[] _dbcolumns;

		// Token: 0x04001578 RID: 5496
		private OleDbParameter[] _parameters;

		// Token: 0x04001579 RID: 5497
		private int _collectionChangeID;

		// Token: 0x0400157A RID: 5498
		private OleDbDataReader _dataReader;

		// Token: 0x0400157B RID: 5499
		private ColumnBinding[] _columnBindings;

		// Token: 0x0400157C RID: 5500
		private RowBinding _rowBinding;

		// Token: 0x0400157D RID: 5501
		private int _index;

		// Token: 0x0400157E RID: 5502
		private int _count;

		// Token: 0x0400157F RID: 5503
		private int _dataBufferSize;

		// Token: 0x04001580 RID: 5504
		private bool _ifIRowsetElseIRow;

		// Token: 0x04001581 RID: 5505
		private bool _forceRebind;

		// Token: 0x04001582 RID: 5506
		private bool _needToReset;
	}
}

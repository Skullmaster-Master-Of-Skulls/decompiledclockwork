using System;
using System.Data.Common;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x0200020D RID: 525
	internal sealed class Bindings
	{
		// Token: 0x06001D4D RID: 7501 RVA: 0x0026D588 File Offset: 0x0026C988
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

		// Token: 0x06001D4E RID: 7502 RVA: 0x0026D5E8 File Offset: 0x0026C9E8
		internal Bindings(OleDbParameter[] parameters, int collectionChangeID) : this(parameters.Length)
		{
			this._bindInfo = new tagDBPARAMBINDINFO[parameters.Length];
			this._parameters = parameters;
			this._collectionChangeID = collectionChangeID;
			this._ifIRowsetElseIRow = true;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0026D628 File Offset: 0x0026CA28
		internal Bindings(OleDbDataReader dataReader, bool ifIRowsetElseIRow, int count) : this(count)
		{
			this._dataReader = dataReader;
			this._ifIRowsetElseIRow = ifIRowsetElseIRow;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0026D658 File Offset: 0x0026CA58
		internal tagDBPARAMBINDINFO[] BindInfo
		{
			get
			{
				return this._bindInfo;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0026D678 File Offset: 0x0026CA78
		internal tagDBCOLUMNACCESS[] DBColumnAccess
		{
			get
			{
				return this._dbcolumns;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0026D698 File Offset: 0x0026CA98
		internal int CurrentIndex
		{
			set
			{
				this._index = value;
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0026D6B8 File Offset: 0x0026CAB8
		internal ColumnBinding[] ColumnBindings()
		{
			return this._columnBindings;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0026D6D8 File Offset: 0x0026CAD8
		internal OleDbParameter[] Parameters()
		{
			return this._parameters;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0026D6F8 File Offset: 0x0026CAF8
		internal RowBinding RowBinding()
		{
			return this._rowBinding;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0026D718 File Offset: 0x0026CB18
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0026D738 File Offset: 0x0026CB38
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

		// Token: 0x170003F4 RID: 1012
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0026D758 File Offset: 0x0026CB58
		internal IntPtr DataSourceType
		{
			set
			{
				this._bindInfo[this._index].pwszDataSourceType = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0026D788 File Offset: 0x0026CB88
		internal IntPtr Name
		{
			set
			{
				this._bindInfo[this._index].pwszName = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0026D7B8 File Offset: 0x0026CBB8
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0026D7F8 File Offset: 0x0026CBF8
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

		// Token: 0x170003F7 RID: 1015
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0026D828 File Offset: 0x0026CC28
		internal int Flags
		{
			set
			{
				this._bindInfo[this._index].dwFlags = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0026D858 File Offset: 0x0026CC58
		internal IntPtr Ordinal
		{
			set
			{
				this._dbbindings[this._index].iOrdinal = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0026D878 File Offset: 0x0026CC78
		internal int Part
		{
			set
			{
				this._dbbindings[this._index].dwPart = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0026D898 File Offset: 0x0026CC98
		internal int ParamIO
		{
			set
			{
				this._dbbindings[this._index].eParamIO = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x0026D8B8 File Offset: 0x0026CCB8
		internal int MaxLen
		{
			set
			{
				this._dbbindings[this._index].obStatus = (IntPtr)this._dataBufferSize;
				this._dbbindings[this._index].obLength = (IntPtr)(this._dataBufferSize + ADP.PtrSize);
				this._dbbindings[this._index].obValue = (IntPtr)(this._dataBufferSize + ADP.PtrSize + ADP.PtrSize);
				this._dataBufferSize += ADP.PtrSize + ADP.PtrSize;
				int dbType = this.DbType;
				if (dbType <= 12)
				{
					if (dbType != 8 && dbType != 12)
					{
						goto IL_E8;
					}
				}
				else
				{
					switch (dbType)
					{
					case 136:
					case 138:
						break;
					case 137:
						goto IL_E8;
					default:
						switch (dbType)
						{
						case 16512:
						case 16514:
							break;
						case 16513:
							goto IL_E8;
						default:
							goto IL_E8;
						}
						break;
					}
				}
				this._dataBufferSize += System.Data.OleDb.RowBinding.AlignDataSize(value * 2);
				this._needToReset = true;
				goto IL_FB;
				IL_E8:
				this._dataBufferSize += System.Data.OleDb.RowBinding.AlignDataSize(value);
				IL_FB:
				this._dbbindings[this._index].cbMaxLen = (IntPtr)value;
				this._dbcolumns[this._index].cbMaxLen = (IntPtr)value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0026D9F8 File Offset: 0x0026CDF8
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0026DA18 File Offset: 0x0026CE18
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

		// Token: 0x170003FD RID: 1021
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0026DA58 File Offset: 0x0026CE58
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

		// Token: 0x170003FE RID: 1022
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0026DAB8 File Offset: 0x0026CEB8
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

		// Token: 0x06001D65 RID: 7525 RVA: 0x0026DB18 File Offset: 0x0026CF18
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

		// Token: 0x06001D66 RID: 7526 RVA: 0x0026DBA8 File Offset: 0x0026CFA8
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

		// Token: 0x06001D67 RID: 7527 RVA: 0x0026DC18 File Offset: 0x0026D018
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

		// Token: 0x06001D68 RID: 7528 RVA: 0x0026DC68 File Offset: 0x0026D068
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

		// Token: 0x06001D69 RID: 7529 RVA: 0x0026DCC8 File Offset: 0x0026D0C8
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

		// Token: 0x06001D6A RID: 7530 RVA: 0x0026DD08 File Offset: 0x0026D108
		internal void CloseFromConnection()
		{
			if (this._rowBinding != null)
			{
				this._rowBinding.CloseFromConnection();
			}
			this.Dispose();
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0026DD38 File Offset: 0x0026D138
		internal OleDbHResult CreateAccessor(UnsafeNativeMethods.IAccessor iaccessor, int flags)
		{
			return this._rowBinding.CreateAccessor(iaccessor, flags, this._columnBindings);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0026DD58 File Offset: 0x0026D158
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

		// Token: 0x06001D6D RID: 7533 RVA: 0x0026DD98 File Offset: 0x0026D198
		internal void GuidKindName(Guid guid, int eKind, IntPtr propid)
		{
			tagDBCOLUMNACCESS[] dbcolumnAccess = this.DBColumnAccess;
			dbcolumnAccess[this._index].columnid.uGuid = guid;
			dbcolumnAccess[this._index].columnid.eKind = eKind;
			dbcolumnAccess[this._index].columnid.ulPropid = propid;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0026DDF8 File Offset: 0x0026D1F8
		internal void ParameterStatus(StringBuilder builder)
		{
			ColumnBinding[] array = this.ColumnBindings();
			for (int i = 0; i < array.Length; i++)
			{
				ODB.CommandParameterStatus(builder, i, array[i].StatusValue());
			}
		}

		// Token: 0x040010B4 RID: 4276
		private readonly tagDBPARAMBINDINFO[] _bindInfo;

		// Token: 0x040010B5 RID: 4277
		private readonly tagDBBINDING[] _dbbindings;

		// Token: 0x040010B6 RID: 4278
		private readonly tagDBCOLUMNACCESS[] _dbcolumns;

		// Token: 0x040010B7 RID: 4279
		private OleDbParameter[] _parameters;

		// Token: 0x040010B8 RID: 4280
		private int _collectionChangeID;

		// Token: 0x040010B9 RID: 4281
		private OleDbDataReader _dataReader;

		// Token: 0x040010BA RID: 4282
		private ColumnBinding[] _columnBindings;

		// Token: 0x040010BB RID: 4283
		private RowBinding _rowBinding;

		// Token: 0x040010BC RID: 4284
		private int _index;

		// Token: 0x040010BD RID: 4285
		private int _count;

		// Token: 0x040010BE RID: 4286
		private int _dataBufferSize;

		// Token: 0x040010BF RID: 4287
		private bool _ifIRowsetElseIRow;

		// Token: 0x040010C0 RID: 4288
		private bool _forceRebind;

		// Token: 0x040010C1 RID: 4289
		private bool _needToReset;
	}
}

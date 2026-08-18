using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;
using OracleInternal.TTC.Accessors;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000070 RID: 112
	[TypeConverter("Oracle.ManagedDataAccess.Client.OracleParameterTypeConverter")]
	public sealed class OracleParameter : DbParameter, IDisposable, ICloneable
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00031708 File Offset: 0x0002F908
		public OracleParameter()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_enumType = PrmEnumType.NOTSET;
				this.m_direction = ParameterDirection.Input;
				this.m_oraDbType = OracleDbType.Varchar2;
				this.m_maxSize = -1;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000317C8 File Offset: 0x0002F9C8
		public OracleParameter(string parameterName, OracleDbType oraType)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_direction = ParameterDirection.Input;
				if (oraType < OracleDbType.BFile || oraType > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_oraDbType = oraType;
				this.m_paramName = parameterName;
				this.m_maxSize = -1;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000318A8 File Offset: 0x0002FAA8
		public OracleParameter(string parameterName, object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (obj != null && obj != DBNull.Value)
				{
					Type type = obj.GetType();
					if (type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
					{
						throw new ArgumentException();
					}
					object obj2 = OraDb_DbTypeTable.s_table[type];
					if (obj2 == null && type.IsArray)
					{
						obj2 = OraDb_DbTypeTable.s_table[type.GetElementType()];
					}
					if (obj2 == null)
					{
						throw new ArgumentException();
					}
					this.m_oraDbType = (OracleDbType)obj2;
					this.m_enumType = PrmEnumType.VALUE;
					this.m_value = obj;
				}
				else
				{
					this.m_oraDbType = OracleDbType.Varchar2;
					this.m_enumType = PrmEnumType.NOTSET;
				}
				this.m_direction = ParameterDirection.Input;
				this.m_paramName = parameterName;
				this.m_maxSize = -1;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00031A3C File Offset: 0x0002FC3C
		public OracleParameter(string parameterName, OracleDbType type, ParameterDirection direction)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_direction = direction;
				if (type < OracleDbType.BFile || type > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_oraDbType = type;
				this.m_paramName = parameterName;
				this.m_maxSize = -1;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00031B38 File Offset: 0x0002FD38
		public OracleParameter(string parameterName, OracleDbType type, int size)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (size < 0)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (type < OracleDbType.BFile || type > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_oraDbType = type;
				this.m_direction = ParameterDirection.Input;
				this.m_paramName = parameterName;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00031C34 File Offset: 0x0002FE34
		public OracleParameter(string parameterName, OracleDbType type, int size, string srcColumn)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (size < 0)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (type < OracleDbType.BFile || type > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_oraDbType = type;
				this.m_direction = ParameterDirection.Input;
				this.m_paramName = parameterName;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
				this.m_sourceColumn = srcColumn;
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00031D38 File Offset: 0x0002FF38
		public OracleParameter(string parameterName, OracleDbType type, object obj, ParameterDirection direction)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (type < OracleDbType.BFile || type > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_oraDbType = type;
				this.m_direction = direction;
				this.m_paramName = parameterName;
				this.m_value = obj;
				this.m_maxSize = -1;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00031E40 File Offset: 0x00030040
		internal OracleParameter(string parameterName, OracleDbType type, int size, string srcColumn, DataRowVersion version, object obj)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (type < OracleDbType.BFile || type > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (version != DataRowVersion.Original && version != DataRowVersion.Current && version != DataRowVersion.Proposed && version != DataRowVersion.Default)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_oraDbType = type;
				this.m_direction = ParameterDirection.Input;
				this.m_paramName = parameterName;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
				this.m_sourceColumn = srcColumn;
				this.m_sourceVersion = version;
				this.m_value = obj;
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00031F74 File Offset: 0x00030174
		public OracleParameter(string parameterName, OracleDbType type, int size, object obj, ParameterDirection direction)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (size < 0)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (type < OracleDbType.BFile || type > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_oraDbType = type;
				this.m_direction = direction;
				this.m_paramName = parameterName;
				this.m_value = obj;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00032098 File Offset: 0x00030298
		public OracleParameter(string parameterName, OracleDbType oraType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (size < 0)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (oraType < OracleDbType.BFile || oraType > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (srcVersion != DataRowVersion.Original && srcVersion != DataRowVersion.Current && srcVersion != DataRowVersion.Proposed && srcVersion != DataRowVersion.Default)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_oraDbType = oraType;
				this.m_direction = direction;
				this.m_paramName = parameterName;
				this.m_paramImpl.m_precision = precision;
				this.m_paramImpl.m_scale = scale;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
				this.m_sourceColumn = srcColumn;
				this.m_sourceVersion = srcVersion;
				this.m_value = obj;
				this.m_nullable = isNullable;
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00032234 File Offset: 0x00030434
		internal OracleParameter(DbType type, ParameterDirection direction, bool isNullable, int offSet, OracleDbType oraDbType, string paramName, byte precision, byte scale, int size, string srcColumn, DataRowVersion srcVersion, OracleParameterStatus paramStatus, object obj, bool bSetDbType, PrmEnumType enumType, bool modified)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (oraDbType < OracleDbType.BFile || oraDbType > OracleDbType.Boolean)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				if (srcVersion != DataRowVersion.Original && srcVersion != DataRowVersion.Current && srcVersion != DataRowVersion.Proposed && srcVersion != DataRowVersion.Default)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentOutOfRangeException();
				}
				this.m_enumType = enumType;
				this.m_modified = modified;
				this.m_oraDbType = oraDbType;
				this.m_direction = direction;
				this.m_paramName = paramName;
				this.m_paramImpl.m_precision = precision;
				this.m_paramImpl.m_scale = scale;
				this.m_sourceVersion = DataRowVersion.Current;
				this.m_paramImpl.m_curSize = -1;
				this.m_sourceColumn = srcColumn;
				this.m_sourceVersion = srcVersion;
				this.m_value = obj;
				this.m_nullable = isNullable;
				this.m_offset = offSet;
				this.m_paramImpl.m_status = paramStatus;
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
				if (bSetDbType)
				{
					this.m_dbType = type;
					this.m_bSetDbType = true;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x000323F0 File Offset: 0x000305F0
		internal bool DuplicateBind
		{
			set
			{
				this.m_bDuplicateBind = true;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000323FC File Offset: 0x000305FC
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00032450 File Offset: 0x00030650
		[DefaultValue(null)]
		[Browsable(false)]
		public int[] ArrayBindSize
		{
			get
			{
				if (this.m_paramImpl.m_curArrayBindSize != null && this.m_paramImpl.m_curArrayBindSize[0] != -1)
				{
					return this.m_paramImpl.m_curArrayBindSize;
				}
				if (this.m_maxArrayBindSize != null && this.m_maxArrayBindSize[0] != -1)
				{
					return this.m_maxArrayBindSize;
				}
				return null;
			}
			set
			{
				this.m_maxArrayBindSize = value;
				this.m_paramImpl.m_curArrayBindSize = null;
				this.m_modified = true;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0003246C File Offset: 0x0003066C
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0003247C File Offset: 0x0003067C
		[DefaultValue(null)]
		[Browsable(false)]
		public OracleParameterStatus[] ArrayBindStatus
		{
			get
			{
				return this.m_paramImpl.m_arrayBindStatus;
			}
			set
			{
				this.m_paramImpl.m_arrayBindStatus = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00032494 File Offset: 0x00030694
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0003249C File Offset: 0x0003069C
		[DefaultValue(OracleCollectionType.None)]
		[Browsable(false)]
		public OracleCollectionType CollectionType
		{
			get
			{
				return this.m_collType;
			}
			set
			{
				if (value != OracleCollectionType.None && value != OracleCollectionType.PLSQLAssociativeArray)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_collType = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000324B4 File Offset: 0x000306B4
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x000324E0 File Offset: 0x000306E0
		[Category("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("")]
		public override DbType DbType
		{
			get
			{
				if (!this.m_bSetDbType)
				{
					this.m_dbType = (DbType)OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[(int)this.m_oraDbType];
					this.m_bSetDbType = true;
				}
				return this.m_dbType;
			}
			set
			{
				if (value == DbType.Currency || value == DbType.SByte || value == DbType.UInt16 || value == DbType.UInt32 || value == DbType.UInt64 || value == DbType.VarNumeric || value == DbType.Guid)
				{
					throw new ArgumentException();
				}
				if (value < DbType.AnsiString || value > DbType.StringFixedLength)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_dbType = value;
				this.m_oraDbType = (OracleDbType)OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[(int)value];
				this.m_bSetDbType = true;
				this.m_modified = true;
				this.m_enumType = PrmEnumType.DBTYPE;
				this.m_bOracleDbTypeExSet = false;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00032558 File Offset: 0x00030758
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00032560 File Offset: 0x00030760
		[Category("Data")]
		[DefaultValue(ParameterDirection.Input)]
		[Description("")]
		public override ParameterDirection Direction
		{
			get
			{
				return this.m_direction;
			}
			set
			{
				if (value != ParameterDirection.Input && value != ParameterDirection.Output && value != ParameterDirection.InputOutput && value != ParameterDirection.ReturnValue)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_direction = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00032588 File Offset: 0x00030788
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00032590 File Offset: 0x00030790
		[Description("")]
		[DefaultValue(false)]
		[Category("Data")]
		public override bool IsNullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				this.m_nullable = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000325A0 File Offset: 0x000307A0
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x000325A8 File Offset: 0x000307A8
		[DefaultValue(0)]
		[Browsable(false)]
		public int Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_offset = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000325C4 File Offset: 0x000307C4
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x000325CC File Offset: 0x000307CC
		[Description("")]
		[Category("Data")]
		public OracleDbType OracleDbType
		{
			get
			{
				return this.m_oraDbType;
			}
			set
			{
				if (this.m_oraDbType != value)
				{
					if (value < OracleDbType.BFile || value > OracleDbType.Boolean)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.m_oraDbType = value;
				}
				this.m_bSetDbType = false;
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_bOracleDbTypeExSet = false;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00032614 File Offset: 0x00030814
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0003261C File Offset: 0x0003081C
		[Category("Data")]
		[Description("")]
		[DbProviderSpecificTypeProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OracleDbType OracleDbTypeEx
		{
			get
			{
				return this.m_oraDbType;
			}
			set
			{
				this.OracleDbType = value;
				this.m_bOracleDbTypeExSet = true;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0003262C File Offset: 0x0003082C
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00032644 File Offset: 0x00030844
		[DefaultValue("")]
		public override string ParameterName
		{
			get
			{
				if (this.m_paramName != null)
				{
					return this.m_paramName;
				}
				return string.Empty;
			}
			set
			{
				this.m_paramName = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00032650 File Offset: 0x00030850
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00032670 File Offset: 0x00030870
		[Category("Data")]
		[Description("")]
		[DefaultValue(0)]
		public new byte Precision
		{
			get
			{
				if (this.m_paramImpl.m_precision == 100)
				{
					return 0;
				}
				return this.m_paramImpl.m_precision;
			}
			set
			{
				this.m_paramImpl.m_precision = value;
				this.m_modified = true;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00032688 File Offset: 0x00030888
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x000326AC File Offset: 0x000308AC
		[Category("Data")]
		[DefaultValue(0)]
		[Description("")]
		public new byte Scale
		{
			get
			{
				if (this.m_paramImpl.m_scale == 129)
				{
					return 0;
				}
				return this.m_paramImpl.m_scale;
			}
			set
			{
				this.m_paramImpl.m_scale = value;
				this.m_modified = true;
			}
		}

		// Token: 0x1700018B RID: 395
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000326C4 File Offset: 0x000308C4
		private int MaxCharsToBeWrittenOrRead
		{
			set
			{
				if (this.m_oraDbType != OracleDbType.Clob && this.m_oraDbType != OracleDbType.NClob && value > 0)
				{
					this.m_maxCharsToBeWrittenOrRead = value;
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x000326E8 File Offset: 0x000308E8
		private int MaxBytesToBeWrittenOrRead
		{
			set
			{
				if (value > 0)
				{
					this.m_maxBytesToBeWrittenOrRead = value;
				}
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000326F8 File Offset: 0x000308F8
		private bool ShouldSerializeDbType()
		{
			return this.m_enumType == PrmEnumType.DBTYPE;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00032704 File Offset: 0x00030904
		private bool ShouldSerializeOracleDbType()
		{
			return this.m_enumType != PrmEnumType.DBTYPE;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00032714 File Offset: 0x00030914
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00032744 File Offset: 0x00030944
		[DefaultValue(0)]
		[Category("Data")]
		[Description("")]
		public override int Size
		{
			get
			{
				if (this.m_paramImpl.m_curSize != -1)
				{
					return this.m_paramImpl.m_curSize;
				}
				if (this.m_maxSize != -1)
				{
					return this.m_maxSize;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value != 0)
				{
					this.m_maxSize = value;
				}
				else
				{
					this.m_maxSize = -1;
				}
				this.m_paramImpl.m_curSize = -1;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00032770 File Offset: 0x00030970
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00032788 File Offset: 0x00030988
		[DefaultValue("")]
		[Category("Data")]
		[Description("")]
		public override string SourceColumn
		{
			get
			{
				if (this.m_sourceColumn != null)
				{
					return this.m_sourceColumn;
				}
				return string.Empty;
			}
			set
			{
				this.m_sourceColumn = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00032794 File Offset: 0x00030994
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0003279C File Offset: 0x0003099C
		[DefaultValue(false)]
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this.m_sourceColumnNullMapping;
			}
			set
			{
				this.m_sourceColumnNullMapping = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x000327A8 File Offset: 0x000309A8
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x000327B0 File Offset: 0x000309B0
		[Description("")]
		[DefaultValue(DataRowVersion.Current)]
		[Category("Data")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this.m_sourceVersion;
			}
			set
			{
				if (value != DataRowVersion.Original && value != DataRowVersion.Current && value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_sourceVersion = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000327E0 File Offset: 0x000309E0
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000327F0 File Offset: 0x000309F0
		[Browsable(false)]
		[DefaultValue(OracleParameterStatus.Success)]
		public OracleParameterStatus Status
		{
			get
			{
				return this.m_paramImpl.m_status;
			}
			set
			{
				if (value != OracleParameterStatus.Success && value != OracleParameterStatus.NullInsert && value != OracleParameterStatus.NullFetched && value != OracleParameterStatus.Truncation)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_paramImpl.m_status = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0003281C File Offset: 0x00030A1C
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00032824 File Offset: 0x00030A24
		[DefaultValue("")]
		[Category("Data")]
		[Description("")]
		public string UdtTypeName
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00032828 File Offset: 0x00030A28
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00032830 File Offset: 0x00030A30
		[Category("Data")]
		[DefaultValue(null)]
		[Description("")]
		public override object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				if (value != null && value != DBNull.Value && this.m_enumType == PrmEnumType.NOTSET)
				{
					Type type = value.GetType();
					if (type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
					{
						throw new ArgumentException();
					}
					object obj = OraDb_DbTypeTable.s_table[type];
					if (obj == null && type.IsArray)
					{
						obj = OraDb_DbTypeTable.s_table[type.GetElementType()];
					}
					if (obj == null)
					{
						throw new ArgumentException();
					}
					this.m_oraDbType = (OracleDbType)obj;
					this.m_bSetDbType = false;
					this.m_enumType = PrmEnumType.VALUE;
				}
				this.m_value = value;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00032904 File Offset: 0x00030B04
		public object Clone()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				OracleParameter oracleParameter;
				if (this.m_value != null && this.m_value.GetType().IsArray)
				{
					oracleParameter = new OracleParameter(this.m_dbType, this.m_direction, this.m_nullable, this.m_offset, this.m_oraDbType, this.m_paramName, this.m_paramImpl.m_precision, this.m_paramImpl.m_scale, this.m_maxSize, this.m_sourceColumn, this.m_sourceVersion, this.m_paramImpl.m_status, ((Array)this.m_value).Clone(), this.m_bSetDbType, this.m_enumType, this.m_modified);
				}
				else
				{
					oracleParameter = new OracleParameter(this.m_dbType, this.m_direction, this.m_nullable, this.m_offset, this.m_oraDbType, this.m_paramName, this.m_paramImpl.m_precision, this.m_paramImpl.m_scale, this.m_maxSize, this.m_sourceColumn, this.m_sourceVersion, this.m_paramImpl.m_status, this.m_value, this.m_bSetDbType, this.m_enumType, this.m_modified);
				}
				oracleParameter.m_collType = this.m_collType;
				oracleParameter.m_bOracleDbTypeExSet = this.m_bOracleDbTypeExSet;
				oracleParameter.m_bReturnDateTimeOffset = this.m_bReturnDateTimeOffset;
				result = oracleParameter;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00032AC8 File Offset: 0x00030CC8
		public void Dispose()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_disposed)
				{
					this.m_modified = true;
					this.m_disposed = true;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00032B30 File Offset: 0x00030D30
		public override void ResetDbType()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_enumType = PrmEnumType.NOTSET;
				this.DbType = DbType.String;
				this.OracleDbType = OracleDbType.Varchar2;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00032B98 File Offset: 0x00030D98
		public void ResetOracleDbType()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_enumType = PrmEnumType.NOTSET;
				this.DbType = DbType.String;
				this.OracleDbType = OracleDbType.Varchar2;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00032C00 File Offset: 0x00030E00
		public override string ToString()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (this.m_paramName != null)
				{
					result = this.m_paramName;
				}
				else
				{
					result = string.Empty;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00032C68 File Offset: 0x00030E68
		private int GetBindingSize_Char(int idx)
		{
			int bufferLength = 0;
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int bindingSize;
			try
			{
				if (this.m_bArrayBind)
				{
					string[] array;
					char[][] array2;
					OracleString[] array3;
					if ((array = (this.m_value as string[])) != null)
					{
						if (!this.m_nullIndicatorsForArrayBind[idx])
						{
							bufferLength = array[idx].Length;
						}
					}
					else if ((array2 = (this.m_value as char[][])) != null)
					{
						if (!this.m_nullIndicatorsForArrayBind[idx])
						{
							bufferLength = array2[idx].Length;
						}
					}
					else if (this.m_value is char[])
					{
						bufferLength = 1;
					}
					else if ((array3 = (this.m_value as OracleString[])) != null)
					{
						if (!this.m_nullIndicatorsForArrayBind[idx])
						{
							bufferLength = array3[idx].Length;
						}
					}
					else
					{
						flag = true;
					}
				}
				if (!this.m_bArrayBind || flag)
				{
					object value;
					if (!this.m_bArrayBind)
					{
						value = this.m_value;
					}
					else
					{
						value = ((Array)this.m_value).GetValue(idx);
					}
					string text;
					char[] array4;
					if ((text = (value as string)) != null)
					{
						bufferLength = text.Length;
					}
					else if ((array4 = (value as char[])) != null)
					{
						bufferLength = array4.Length;
					}
					else if (value is char)
					{
						bufferLength = 1;
					}
					else if (value is OracleString)
					{
						bufferLength = ((OracleString)value).Length;
					}
					else
					{
						bufferLength = Convert.ToString(value).Length;
					}
				}
				bindingSize = this.GetBindingSize(bufferLength, idx);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return bindingSize;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00032E20 File Offset: 0x00031020
		private int GetBindingSize_Raw(int idx)
		{
			int bufferLength = 0;
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int bindingSize;
			try
			{
				if (this.m_bArrayBind)
				{
					byte[][] array;
					OracleBinary[] array2;
					if ((array = (this.m_value as byte[][])) != null)
					{
						bufferLength = array[idx].Length;
					}
					else if ((array2 = (this.m_value as OracleBinary[])) != null)
					{
						bufferLength = array2[idx].Length;
					}
					else if (this.m_value is Guid[])
					{
						bufferLength = 16;
					}
					else
					{
						flag = true;
					}
				}
				if (!this.m_bArrayBind || flag)
				{
					object value;
					if (!this.m_bArrayBind)
					{
						value = this.m_value;
					}
					else
					{
						value = ((Array)this.m_value).GetValue(idx);
					}
					byte[] array3;
					if ((array3 = (value as byte[])) != null)
					{
						bufferLength = array3.Length;
					}
					else if (value is OracleBinary)
					{
						bufferLength = ((OracleBinary)value).Length;
					}
					else
					{
						if (!(value is Guid))
						{
							throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]));
						}
						bufferLength = 16;
					}
				}
				bindingSize = this.GetBindingSize(bufferLength, 0);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return bindingSize;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00032F78 File Offset: 0x00031178
		private int GetBindingSize(int bufferLength, int idx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num;
				if (!this.m_bArrayBind)
				{
					if (this.m_maxSize == -1)
					{
						num = bufferLength;
					}
					else
					{
						num = this.m_maxSize;
					}
				}
				else if (this.m_maxArrayBindSize[idx] == -1)
				{
					num = bufferLength;
				}
				else
				{
					num = this.m_maxArrayBindSize[idx];
				}
				if (this.m_offset > bufferLength)
				{
					throw new ArgumentException("Invalid offset", this.ParameterName);
				}
				if (this.m_offset + num > bufferLength)
				{
					num = bufferLength - this.m_offset;
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0003304C File Offset: 0x0003124C
		internal void SetSize(int size)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (size != 0)
				{
					this.m_maxSize = size;
				}
				else
				{
					this.m_maxSize = -1;
				}
				this.m_paramImpl.m_curSize = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000330DC File Offset: 0x000312DC
		internal static bool IsElemType(Type type, object value, int index)
		{
			Type left = null;
			bool result = false;
			if (value != null)
			{
				Array array = value as Array;
				if (array != null && array.Length > 0 && array.GetValue(index) != null)
				{
					left = array.GetValue(index).GetType();
				}
				result = (left == type);
			}
			return result;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00033124 File Offset: 0x00031324
		internal void PreBindFree()
		{
			try
			{
				if (this.m_oraDbType != OracleDbType.Blob && this.m_oraDbType != OracleDbType.Clob && this.m_oraDbType != OracleDbType.NClob)
				{
					this.m_paramImpl.m_saveValue = null;
				}
				else
				{
					int num = 1;
					if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
					{
						if (this.m_bArrayBind)
						{
							num = this.m_bindElemCnt;
						}
						if (this.m_paramImpl.m_saveValue != null)
						{
							for (int i = 0; i < num; i++)
							{
								if (!(this.m_value is OracleBlob) && !OracleParameter.IsElemType(typeof(OracleBlob), this.m_value, i))
								{
									object obj = this.m_paramImpl.m_saveValue[i];
									if (obj is OracleBlobImpl)
									{
										((OracleBlobImpl)obj).RelRef();
									}
									else if (obj is OracleClobImpl)
									{
										((OracleClobImpl)obj).RelRef();
									}
									this.m_paramImpl.m_saveValue[i] = null;
								}
							}
							this.m_paramImpl.m_saveValue = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0003323C File Offset: 0x0003143C
		internal void PreBind(OracleConnectionImpl connImpl, ColumnDescribeInfo cachedParamMetadata, ref bool bMetadataModified, int arrayBindCount, out ColumnDescribeInfo paramMetaData, out object paramValue, bool isEFSelectStatement, SqlStatementType stmtType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_maxBytesToBeWrittenOrRead = 1;
				this.m_maxCharsToBeWrittenOrRead = 0;
				this.m_bArrayBind = (arrayBindCount != 0 || this.m_collType == OracleCollectionType.PLSQLAssociativeArray);
				if (this.m_bArrayBind)
				{
					this.SetArrayContext(arrayBindCount);
					this.m_paramImpl.m_paramValForArrayBindInBytes = new byte[this.m_bindElemCnt][];
				}
				else
				{
					this.m_paramImpl.m_paramValInBytes = null;
				}
				this.SetNullIndicators();
				switch (this.m_oraDbType)
				{
				case OracleDbType.BFile:
					this.PreBind_BFile(connImpl);
					goto IL_28A;
				case OracleDbType.Blob:
					this.PreBind_Blob(connImpl, isEFSelectStatement);
					goto IL_28A;
				case OracleDbType.Byte:
				case OracleDbType.Int16:
				case OracleDbType.Int32:
					this.PreBind_Int32();
					goto IL_28A;
				case OracleDbType.Char:
				case OracleDbType.Long:
				case OracleDbType.Varchar2:
					this.m_characterSetId = (int)connImpl.m_serverCharacterSet;
					this.m_charSetForm = 1;
					this.PreBind_Char(connImpl, stmtType);
					goto IL_28A;
				case OracleDbType.Clob:
					this.m_charSetForm = 1;
					this.m_characterSetId = (int)connImpl.m_serverCharacterSet;
					this.PreBind_Clob(connImpl, isEFSelectStatement, stmtType);
					goto IL_28A;
				case OracleDbType.Date:
					this.PreBind_Date();
					goto IL_28A;
				case OracleDbType.Decimal:
					this.PreBind_Decimal();
					goto IL_28A;
				case OracleDbType.Double:
					this.PreBind_Double();
					goto IL_28A;
				case OracleDbType.LongRaw:
				case OracleDbType.Raw:
					this.PreBind_Raw();
					goto IL_28A;
				case OracleDbType.Int64:
					this.PreBind_Int64();
					goto IL_28A;
				case OracleDbType.IntervalDS:
					this.PreBind_IntervalDS();
					goto IL_28A;
				case OracleDbType.IntervalYM:
					this.PreBind_IntervalYM();
					goto IL_28A;
				case OracleDbType.NClob:
					this.m_charSetForm = 2;
					this.m_characterSetId = (int)connImpl.m_serverNCharSet;
					this.PreBind_Clob(connImpl, isEFSelectStatement, stmtType);
					goto IL_28A;
				case OracleDbType.NChar:
				case OracleDbType.NVarchar2:
					this.m_characterSetId = (int)connImpl.m_serverNCharSet;
					this.m_charSetForm = 2;
					this.PreBind_Char(connImpl, stmtType);
					goto IL_28A;
				case OracleDbType.RefCursor:
					this.PreBind_Cursor(connImpl);
					goto IL_28A;
				case OracleDbType.Single:
					this.PreBind_Single();
					goto IL_28A;
				case OracleDbType.TimeStamp:
					this.PreBind_TimeStamp();
					goto IL_28A;
				case OracleDbType.TimeStampLTZ:
					this.PreBind_TimeStampLTZ(connImpl);
					goto IL_28A;
				case OracleDbType.TimeStampTZ:
					this.PreBind_TimeStampTZ(connImpl);
					goto IL_28A;
				case OracleDbType.XmlType:
					this.PreBind_XmlType(connImpl);
					goto IL_28A;
				case OracleDbType.BinaryDouble:
					this.PreBind_BDouble();
					goto IL_28A;
				case OracleDbType.BinaryFloat:
					this.PreBind_BFloat();
					goto IL_28A;
				case OracleDbType.Boolean:
					this.PreBind_Boolean();
					goto IL_28A;
				}
				throw new OracleException(ResourceStringConstants.CMD_TYPE_NOT_SUPPORTED, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CMD_TYPE_NOT_SUPPORTED, new string[0]));
				IL_28A:
				paramMetaData = this.m_paramImpl.GetParameterMetaData(this, cachedParamMetadata, ref bMetadataModified);
				paramValue = (this.m_bArrayBind ? this.m_paramImpl.m_paramValForArrayBindInBytes : this.m_paramImpl.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00033568 File Offset: 0x00031768
		private bool IsProviderSpecificNullValue(object value)
		{
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				switch (this.m_oraDbType)
				{
				case OracleDbType.BFile:
				{
					OracleBFile oracleBFile;
					if ((oracleBFile = (value as OracleBFile)) != null && oracleBFile.IsNull)
					{
						flag = true;
					}
					break;
				}
				case OracleDbType.Blob:
				{
					OracleBlob oracleBlob;
					if (((oracleBlob = (value as OracleBlob)) != null && oracleBlob.IsNull) || (value is OracleBinary && ((OracleBinary)value).IsNull))
					{
						flag = true;
					}
					break;
				}
				case OracleDbType.Byte:
					if ((value is OracleDecimal && ((OracleDecimal)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
					{
						flag = true;
					}
					break;
				case OracleDbType.Char:
				case OracleDbType.Long:
				case OracleDbType.NChar:
				case OracleDbType.NVarchar2:
				case OracleDbType.Varchar2:
					if (value is OracleString && ((OracleString)value).IsNull)
					{
						flag = true;
					}
					break;
				case OracleDbType.Clob:
				case OracleDbType.NClob:
				{
					OracleClob oracleClob;
					if (((oracleClob = (value as OracleClob)) != null && oracleClob.IsNull) || (value is OracleString && ((OracleString)value).IsNull))
					{
						flag = true;
					}
					break;
				}
				case OracleDbType.Date:
				case OracleDbType.TimeStamp:
				case OracleDbType.TimeStampLTZ:
				case OracleDbType.TimeStampTZ:
					if ((value is OracleDate && ((OracleDate)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull) || (value is OracleTimeStamp && ((OracleTimeStamp)value).IsNull) || (value is OracleTimeStampTZ && ((OracleTimeStampTZ)value).IsNull) || (value is OracleTimeStampLTZ && ((OracleTimeStampLTZ)value).IsNull))
					{
						flag = true;
					}
					break;
				case OracleDbType.Decimal:
				case OracleDbType.Double:
				case OracleDbType.Int16:
				case OracleDbType.Int32:
				case OracleDbType.Int64:
				case OracleDbType.Single:
				case OracleDbType.BinaryDouble:
				case OracleDbType.BinaryFloat:
					if ((value is OracleDecimal && ((OracleDecimal)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
					{
						flag = true;
					}
					break;
				case OracleDbType.LongRaw:
				case OracleDbType.Raw:
					if (value is OracleBinary && ((OracleBinary)value).IsNull)
					{
						flag = true;
					}
					break;
				case OracleDbType.IntervalDS:
					if ((value is OracleIntervalDS && ((OracleIntervalDS)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
					{
						flag = true;
					}
					break;
				case OracleDbType.IntervalYM:
					if ((value is OracleIntervalYM && ((OracleIntervalYM)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
					{
						flag = true;
					}
					break;
				case OracleDbType.RefCursor:
				{
					OracleRefCursor oracleRefCursor;
					if ((oracleRefCursor = (value as OracleRefCursor)) != null && oracleRefCursor.IsNull)
					{
						flag = true;
					}
					break;
				}
				case OracleDbType.XmlType:
				{
					OracleXmlType oracleXmlType;
					OracleClob oracleClob2;
					if (((oracleXmlType = (value as OracleXmlType)) != null && oracleXmlType.IsNull) || ((oracleClob2 = (value as OracleClob)) != null && oracleClob2.IsNull) || (value is OracleString && ((OracleString)value).IsNull))
					{
						flag = true;
					}
					break;
				}
				case OracleDbType.Boolean:
					if (value is OracleBoolean && ((OracleBoolean)value).IsNull)
					{
						flag = true;
					}
					break;
				}
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00033978 File Offset: 0x00031B78
		internal void SetNullIndicators()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_value == null)
				{
					this.m_value = DBNull.Value;
				}
				if (this.m_bArrayBind)
				{
					this.m_nullIndicatorsForArrayBind = new bool[this.m_bindElemCnt];
					if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
					{
						if (this.m_value == DBNull.Value || this.IsProviderSpecificNullValue(this.m_value))
						{
							for (int i = 0; i < this.m_bindElemCnt; i++)
							{
								this.m_nullIndicatorsForArrayBind[i] = true;
							}
							this.m_IsValueNull = true;
						}
						else
						{
							int length = ((Array)this.m_value).Length;
							for (int i = 0; i < this.m_bindElemCnt; i++)
							{
								if (this.m_paramImpl.m_arrayBindStatus[i] == OracleParameterStatus.NullInsert)
								{
									this.m_nullIndicatorsForArrayBind[i] = true;
								}
								else if (length > i && (((Array)this.m_value).GetValue(i) == null || ((Array)this.m_value).GetValue(i) == DBNull.Value || (((Array)this.m_value).GetValue(i) is INullable && ((INullable)((Array)this.m_value).GetValue(i)).IsNull)))
								{
									this.m_nullIndicatorsForArrayBind[i] = true;
								}
								else
								{
									this.m_nullIndicatorsForArrayBind[i] = false;
								}
							}
						}
					}
					else
					{
						for (int i = 0; i < this.m_bindElemCnt; i++)
						{
							this.m_nullIndicatorsForArrayBind[i] = false;
						}
					}
				}
				else if (((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && (this.m_value == DBNull.Value || this.IsProviderSpecificNullValue(this.m_value) || this.m_paramImpl.m_status == OracleParameterStatus.NullInsert)) || this.m_direction == ParameterDirection.Output)
				{
					this.m_IsValueNull = true;
				}
				else
				{
					this.m_IsValueNull = false;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00033BB0 File Offset: 0x00031DB0
		internal void SetArrayContext(int arrayBindCount)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
					{
						if (this.m_value != null && this.m_value != DBNull.Value && this.m_value is Array)
						{
							if (this.m_maxSize > 0 && ((Array)this.m_value).Length > this.m_maxSize)
							{
								this.m_bindElemCnt = this.m_maxSize;
							}
							else
							{
								this.m_bindElemCnt = ((Array)this.m_value).Length;
							}
						}
						else
						{
							if (this.m_paramImpl.m_arrayBindStatus == null)
							{
								throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
								{
									"OracleParameter.Value"
								}));
							}
							this.m_bindElemCnt = 0;
							if (this.m_maxSize > 0 && this.m_paramImpl.m_arrayBindStatus.Length > this.m_maxSize)
							{
								this.m_bindElemCnt = this.m_maxSize;
							}
							else
							{
								this.m_bindElemCnt = this.m_paramImpl.m_arrayBindStatus.Length;
							}
						}
						if (this.m_bindElemCnt == 0)
						{
							throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
							{
								"OracleParameter.Value"
							}));
						}
						this.m_maxNoOfArrayElements = this.m_bindElemCnt;
						if (this.m_direction == ParameterDirection.InputOutput && this.m_maxNoOfArrayElements < this.m_maxSize)
						{
							this.m_maxNoOfArrayElements = this.m_maxSize;
						}
					}
					else
					{
						if (this.m_maxSize <= 0)
						{
							throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
							{
								"OracleParameter.Size"
							}));
						}
						this.m_bindElemCnt = this.m_maxSize;
						this.m_maxNoOfArrayElements = this.m_maxSize;
					}
				}
				else
				{
					this.m_bindElemCnt = arrayBindCount;
					this.m_maxNoOfArrayElements = arrayBindCount;
				}
				if (this.m_direction != ParameterDirection.Input && (this.OracleDbType == OracleDbType.Char || this.OracleDbType == OracleDbType.Varchar2 || this.OracleDbType == OracleDbType.Raw || this.OracleDbType == OracleDbType.Long || this.OracleDbType == OracleDbType.NChar || this.OracleDbType == OracleDbType.NVarchar2 || this.OracleDbType == OracleDbType.LongRaw) && (this.m_maxArrayBindSize == null || (this.m_maxArrayBindSize != null && this.m_maxArrayBindSize.Length < this.m_maxSize)))
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
					{
						"OracleParameter.ArrayBindSize"
					}));
				}
				if (this.m_paramImpl.m_curArrayBindSize == null || this.m_paramImpl.m_curArrayBindSize.Length != this.m_maxNoOfArrayElements)
				{
					this.m_paramImpl.m_curArrayBindSize = new int[this.m_maxNoOfArrayElements];
					for (int i = 0; i < this.m_maxNoOfArrayElements; i++)
					{
						this.m_paramImpl.m_curArrayBindSize[i] = -1;
					}
				}
				if (this.m_maxArrayBindSize == null)
				{
					this.m_maxArrayBindSize = new int[this.m_maxNoOfArrayElements];
					for (int i = 0; i < this.m_maxNoOfArrayElements; i++)
					{
						this.m_maxArrayBindSize[i] = -1;
					}
				}
				else if (this.m_maxArrayBindSize.Length < this.m_maxNoOfArrayElements)
				{
					int[] array = new int[this.m_maxNoOfArrayElements];
					int i;
					for (i = 0; i < this.m_maxArrayBindSize.Length; i++)
					{
						array[i] = this.m_maxArrayBindSize[i];
					}
					while (i < this.m_maxNoOfArrayElements)
					{
						array[i] = -1;
						i++;
					}
					this.m_maxArrayBindSize = array;
				}
				if (this.m_paramImpl.m_arrayBindStatus == null)
				{
					this.m_paramImpl.m_arrayBindStatus = new OracleParameterStatus[this.m_maxNoOfArrayElements];
					for (int i = 0; i < this.m_maxNoOfArrayElements; i++)
					{
						this.m_paramImpl.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
				}
				else if (this.m_paramImpl.m_arrayBindStatus.Length < this.m_maxNoOfArrayElements)
				{
					OracleParameterStatus[] array2 = new OracleParameterStatus[this.m_maxNoOfArrayElements];
					int i;
					for (i = 0; i < this.m_paramImpl.m_arrayBindStatus.Length; i++)
					{
						array2[i] = this.m_paramImpl.m_arrayBindStatus[i];
					}
					while (i < this.m_maxNoOfArrayElements)
					{
						array2[i] = OracleParameterStatus.Success;
						i++;
					}
					this.m_paramImpl.m_arrayBindStatus = array2;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00034010 File Offset: 0x00032210
		private void PreBind_Char(OracleConnectionImpl connImpl, SqlStatementType stmtType)
		{
			int[] array = null;
			int num = 0;
			int num2 = 0;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_CHARN;
				if (this.m_oraDbType == OracleDbType.Char || this.m_oraDbType == OracleDbType.NChar)
				{
					this.m_oraType = OraType.ORA_CHAR;
				}
				int maxBytesPerChar = connImpl.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar;
				if (this.m_charSetForm == 2)
				{
					maxBytesPerChar = connImpl.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar;
				}
				if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
				{
					if (!this.m_IsValueNull)
					{
						if (!this.m_bArrayBind)
						{
							num = (num2 = this.GetBindingSize_Char(0));
							if (this.m_direction == ParameterDirection.InputOutput && num < this.m_maxSize)
							{
								num2 = this.m_maxSize;
							}
						}
						else
						{
							array = new int[this.m_bindElemCnt];
							for (int i = 0; i < this.m_bindElemCnt; i++)
							{
								if (this.m_nullIndicatorsForArrayBind[i])
								{
									array[i] = 0;
								}
								else
								{
									array[i] = this.GetBindingSize_Char(i);
								}
								if (num2 < array[i])
								{
									num2 = array[i];
								}
								if (this.m_direction == ParameterDirection.InputOutput && this.m_maxArrayBindSize != null && num2 < this.m_maxArrayBindSize[i])
								{
									num2 = this.m_maxArrayBindSize[i];
								}
							}
						}
						if (!this.m_bArrayBind)
						{
							this.m_paramImpl.SetCharDataInBytes(connImpl, this.m_value, num, this.m_offset, this.m_charSetForm);
						}
						else
						{
							this.m_paramImpl.SetCharDataArrayInBytes(connImpl, this.m_value, array, this.m_offset, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind, this.m_charSetForm);
						}
						if (this.m_direction == ParameterDirection.InputOutput)
						{
							this.MaxBytesToBeWrittenOrRead = num2 * maxBytesPerChar;
							this.MaxCharsToBeWrittenOrRead = num2;
						}
						else
						{
							this.MaxBytesToBeWrittenOrRead = num2 * maxBytesPerChar;
							this.MaxCharsToBeWrittenOrRead = num2;
						}
					}
					else if (this.m_direction == ParameterDirection.InputOutput)
					{
						this.MaxBytesToBeWrittenOrRead = this.m_maxSize * maxBytesPerChar;
						this.MaxCharsToBeWrittenOrRead = this.m_maxSize;
					}
				}
				else if (this.m_bArrayBind)
				{
					if (this.m_maxArrayBindSize != null)
					{
						num2 = this.m_maxArrayBindSize[0];
						for (int j = 1; j < this.m_bindElemCnt; j++)
						{
							if (this.m_maxArrayBindSize[j] > num2)
							{
								num2 = this.m_maxArrayBindSize[j];
							}
						}
						if (num2 == -1)
						{
							num2 = 0;
						}
					}
					else
					{
						num2 = 0;
					}
					this.MaxBytesToBeWrittenOrRead = num2 * maxBytesPerChar;
					this.MaxCharsToBeWrittenOrRead = num2;
				}
				else
				{
					this.MaxBytesToBeWrittenOrRead = this.m_maxSize * maxBytesPerChar;
					this.MaxCharsToBeWrittenOrRead = this.m_maxSize;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000342DC File Offset: 0x000324DC
		private void PreBind_BDouble()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_IBDOUBLE;
				this.m_maxBytesToBeWrittenOrRead = 8;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetBinaryDoubleInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetBinaryDoubleArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000343AC File Offset: 0x000325AC
		private void PreBind_BFloat()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_IBFLOAT;
				this.m_maxBytesToBeWrittenOrRead = 4;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetBinaryFloatInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetBinaryFloatArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0003447C File Offset: 0x0003267C
		private void PreBind_Int32()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_NUMBER;
				this.m_maxBytesToBeWrittenOrRead = 22;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetInt32DataInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetInt32ArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0003454C File Offset: 0x0003274C
		private void PreBind_Int64()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_VARNUM;
				this.m_maxBytesToBeWrittenOrRead = 22;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetInt64DataInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetInt64ArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0003461C File Offset: 0x0003281C
		private void PreBind_Double()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_NUMBER;
				this.m_maxBytesToBeWrittenOrRead = 22;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetDoubleInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetDoubleArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000346EC File Offset: 0x000328EC
		private void PreBind_Single()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_VARNUM;
				this.m_maxBytesToBeWrittenOrRead = 22;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetSingleInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetSingleArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000347BC File Offset: 0x000329BC
		private void PreBind_Decimal()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_VARNUM;
				this.m_maxBytesToBeWrittenOrRead = 22;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetDecimalDataInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetDecimalArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0003488C File Offset: 0x00032A8C
		private void PreBind_Date()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_DATE;
				this.m_maxBytesToBeWrittenOrRead = 7;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetDateInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetDateArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0003495C File Offset: 0x00032B5C
		private void PreBind_TimeStamp()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_TIMESTAMP_DTY;
				this.m_maxBytesToBeWrittenOrRead = 11;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetTimeStampInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetTimeStampArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00034A30 File Offset: 0x00032C30
		private void PreBind_TimeStampLTZ(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_TIMESTAMP_LTZ_DTY;
				this.m_maxBytesToBeWrittenOrRead = 11;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetTimeStampLTZInBytes(connImpl, this.m_value);
					}
					else
					{
						this.m_paramImpl.SetTimeStampLTZArrayInBytes(connImpl, this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00034B08 File Offset: 0x00032D08
		private void PreBind_TimeStampTZ(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_TIMESTAMP_TZ_DTY;
				this.m_maxBytesToBeWrittenOrRead = 13;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetTimeStampTZInBytes(connImpl, this.m_value);
					}
					else
					{
						this.m_paramImpl.SetTimeStampTZArrayInBytes(connImpl, this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00034BE0 File Offset: 0x00032DE0
		private void PreBind_IntervalDS()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_INTERVAL_DS;
				this.m_maxBytesToBeWrittenOrRead = 11;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetIntervalDSInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetIntervalDSArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00034CB4 File Offset: 0x00032EB4
		private void PreBind_IntervalYM()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_INTERVAL_YM;
				this.m_maxBytesToBeWrittenOrRead = 5;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetIntervalYMInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetIntervalYMArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00034D88 File Offset: 0x00032F88
		private void PreBind_Raw()
		{
			this.m_oraType = OraType.ORA_RAW;
			int num = 0;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
				{
					if (!this.m_IsValueNull)
					{
						if (!this.m_bArrayBind)
						{
							num = this.GetBindingSize_Raw(0);
							this.m_paramImpl.SetRawDataInBytes(this.m_value, num, this.m_offset);
							this.MaxBytesToBeWrittenOrRead = num;
							if (this.m_direction == ParameterDirection.InputOutput && num < this.m_maxSize)
							{
								this.MaxBytesToBeWrittenOrRead = this.m_maxSize;
							}
						}
						else
						{
							int[] array = new int[this.m_bindElemCnt];
							for (int i = 0; i < this.m_bindElemCnt; i++)
							{
								if (this.m_nullIndicatorsForArrayBind[i])
								{
									array[i] = 0;
								}
								else
								{
									array[i] = this.GetBindingSize_Raw(i);
								}
								if (num < array[i])
								{
									num = array[i];
								}
								if (this.m_direction == ParameterDirection.InputOutput && this.m_maxArrayBindSize != null && num < this.m_maxArrayBindSize[i])
								{
									num = this.m_maxArrayBindSize[i];
								}
							}
							this.MaxBytesToBeWrittenOrRead = num;
							this.m_paramImpl.SetRawDataArrayInBytes(this.m_value, array, this.m_offset, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
						}
					}
					else if (this.m_direction == ParameterDirection.InputOutput)
					{
						this.MaxBytesToBeWrittenOrRead = this.m_maxSize;
					}
				}
				else if (this.m_bArrayBind)
				{
					if (this.m_maxArrayBindSize != null)
					{
						num = this.m_maxArrayBindSize[0];
						for (int j = 1; j < this.m_bindElemCnt; j++)
						{
							if (this.m_maxArrayBindSize[j] > num)
							{
								num = this.m_maxArrayBindSize[j];
							}
						}
						if (num == -1)
						{
							num = 0;
						}
					}
					this.MaxBytesToBeWrittenOrRead = num;
				}
				else
				{
					this.MaxBytesToBeWrittenOrRead = this.m_maxSize;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00034F98 File Offset: 0x00033198
		private void PreBind_Clob(OracleConnectionImpl connImpl, bool isEFSelectStatement, SqlStatementType stmtType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_maxBytesToBeWrittenOrRead = 4000;
				if (!this.m_bArrayBind && this.m_direction == ParameterDirection.Input && !(this.m_value is OracleClob))
				{
					int num = this.m_maxBytesToBeWrittenOrRead;
					int num2 = 0;
					if (!this.m_IsValueNull)
					{
						num2 = this.GetBindingSize_Char(0);
					}
					if (this.m_charSetForm == 2)
					{
						num /= 2;
					}
					if ((num2 < num && num2 > 0) || (num2 == 0 && isEFSelectStatement))
					{
						this.m_IsValueNull = (num2 <= 0);
						this.m_maxBytesToBeWrittenOrRead = 1;
						this.m_maxCharsToBeWrittenOrRead = 0;
						this.PreBind_Char(connImpl, stmtType);
						return;
					}
				}
				this.m_oraType = OraType.ORA_OCICLobLocator;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					bool bIsNClob = this.m_charSetForm != 1;
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetClobDataInBytes(connImpl, bIsNClob, this.m_value, this.m_offset, this.m_maxSize, this.m_maxArrayBindSize);
					}
					else
					{
						this.m_paramImpl.SetClobArrayDataInBytes(connImpl, bIsNClob, this.m_value, this.m_offset, this.m_maxSize, this.m_maxArrayBindSize, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00035134 File Offset: 0x00033334
		private void PreBind_BFile(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_OCIBFileLocator;
				this.m_maxBytesToBeWrittenOrRead = 4000;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetBFileDataInBytes(connImpl, this.m_value);
					}
					else
					{
						this.m_paramImpl.SetBFileArrayInBytes(connImpl, this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0003520C File Offset: 0x0003340C
		private void PreBind_Blob(OracleConnectionImpl connImpl, bool isEFSelectStatement)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_maxBytesToBeWrittenOrRead = 4000;
				if (!this.m_IsValueNull && !this.m_bArrayBind && this.m_direction == ParameterDirection.Input && !(this.m_value is OracleBlob))
				{
					int bindingSize_Raw = this.GetBindingSize_Raw(0);
					if ((bindingSize_Raw < this.m_maxBytesToBeWrittenOrRead && bindingSize_Raw > 0) || (bindingSize_Raw == 0 && isEFSelectStatement))
					{
						if (bindingSize_Raw == 0)
						{
							this.m_IsValueNull = true;
						}
						this.PreBind_Raw();
						this.m_IsValueNull = false;
						return;
					}
				}
				this.m_oraType = OraType.ORA_OCIBLobLocator;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetBlobDataInBytes(connImpl, this.m_value, this.m_offset, this.m_maxSize, this.m_maxArrayBindSize);
					}
					else
					{
						this.m_paramImpl.SetBlobArrayDataInBytes(connImpl, this.m_value, this.m_offset, this.m_maxSize, this.m_maxArrayBindSize, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00035364 File Offset: 0x00033564
		private void PreBind_XmlType(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_XMLTYPE;
				this.m_maxBytesToBeWrittenOrRead = 11;
				this.m_maxCharsToBeWrittenOrRead = 0;
				this.m_charSetForm = 0;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetXmlTypeInBytes(connImpl, this.m_value, this.m_offset, this.m_maxSize, this.m_maxArrayBindSize);
					}
					else
					{
						this.m_paramImpl.SetXmlTypeArrayInBytes(connImpl, this.m_value, this.m_offset, this.m_maxSize, this.m_maxArrayBindSize, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00035468 File Offset: 0x00033668
		private void PreBind_Cursor(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_REFCURSOR;
				this.m_maxBytesToBeWrittenOrRead = 1;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetRefCursorDataInBytes(connImpl, this.m_value);
					}
					else
					{
						this.m_paramImpl.SetRefCursorArrayInBytes(connImpl, this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0003553C File Offset: 0x0003373C
		private void PreBind_Boolean()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraType = OraType.ORA_BOOLEAN;
				this.m_maxBytesToBeWrittenOrRead = 2;
				if ((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && !this.m_IsValueNull)
				{
					if (!this.m_bArrayBind)
					{
						this.m_paramImpl.SetPlsqlBooleanDataInBytes(this.m_value);
					}
					else
					{
						this.m_paramImpl.SetPlsqlBooleanArrayInBytes(this.m_value, this.m_bindElemCnt, this.m_nullIndicatorsForArrayBind);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00035610 File Offset: 0x00033810
		internal void PostBind_Boolean(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetPlsqlBooleanFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraDbType, 0);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetPlsqlBooleanArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraDbType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000356E4 File Offset: 0x000338E4
		internal void PostBind_Char(OracleConnectionImpl connImpl, Accessor bindAccessor, char[] charArrayFromPooler)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetCharDataFromBytesInPLSQLArray(connImpl, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_maxArrayBindSize, this.m_charSetForm, charArrayFromPooler);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetCharDataFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_charSetForm, this.m_maxCharsToBeWrittenOrRead, charArrayFromPooler);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetCharArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_charSetForm, this.m_maxArrayBindSize, this.m_bindElemCnt, charArrayFromPooler);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00035800 File Offset: 0x00033A00
		internal void PostBind_RefCursor(OracleConnection connection, Accessor bindAccessor, long fetchSize, OracleIntervalDS sessionTimeZone, string commandText, string paramPosOrName, long longFetchSize, long clientInitialLOBFS, long internalInitialLOBFS, long[] scnFromExecution, bool bCallFromExecuteReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetRefCursorFromBytes(connection, bindAccessor, fetchSize, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, sessionTimeZone, commandText, paramPosOrName, longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, bCallFromExecuteReader);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetRefCursorArrayFromBytes(connection, bindAccessor, fetchSize, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, sessionTimeZone, commandText, paramPosOrName, longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, this.m_bindElemCnt, bCallFromExecuteReader);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000358E8 File Offset: 0x00033AE8
		internal void PostBind_Lob(OracleConnection connection, Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_paramImpl.GetLobDataFromBytes(connection, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraType, ref this.m_value, this.m_direction == ParameterDirection.InputOutput, this.m_charSetForm);
				}
				else
				{
					this.m_paramImpl.GetLobArrayFromBytes(connection, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraType, ref this.m_value, this.m_direction == ParameterDirection.InputOutput, this.m_charSetForm, this.m_bindElemCnt);
				}
				this.m_paramImpl.m_saveValue = null;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000359E4 File Offset: 0x00033BE4
		internal void PostBind_TimeStamp(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetTimeStampFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetTimeStampArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00035AA8 File Offset: 0x00033CA8
		internal void PostBind_TimeStampLTZ(OracleConnectionImpl connImpl, Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetTimeStampLTZFromBytes(connImpl, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetTimeStampLTZArrayFromBytes(connImpl, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00035B70 File Offset: 0x00033D70
		internal void PostBind_TimeStampTZ(OracleConnectionImpl connImpl, Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetTimeStampTZFromBytes(connImpl, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bReturnDateTimeOffset);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetTimeStampTZArrayFromBytes(connImpl, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt, this.m_bReturnDateTimeOffset);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00035C44 File Offset: 0x00033E44
		internal void PostBind_Raw(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetRawDataFromBytesInPlSqlArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetRawDataFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_maxBytesToBeWrittenOrRead);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetRawArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_maxArrayBindSize, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00035D44 File Offset: 0x00033F44
		internal void PostBind_Int32(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetInt32FromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetIntFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraDbType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetIntArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraDbType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00035E44 File Offset: 0x00034044
		internal void PostBind_Int64(object bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetLongFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetLongFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetLongArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00035F38 File Offset: 0x00034138
		internal void PostBind_Decimal(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetDecimalFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetDecimalFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetDecimalArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0003602C File Offset: 0x0003422C
		internal void PostBind_Single(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetSingleFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetSingleFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetSingleArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00036120 File Offset: 0x00034320
		internal void PostBind_BinaryFloat(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetBinaryFloatFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetBinaryFloatFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetBinaryFloatArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00036214 File Offset: 0x00034414
		internal void PostBind_BinaryDouble(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetBinaryDoubleFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetBinaryDoubleFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetBinaryDoubleArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00036308 File Offset: 0x00034508
		internal void PostBind_Double(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetDoubleFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetDoubleFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetDoubleArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000363FC File Offset: 0x000345FC
		internal void PostBind_Date(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					this.m_value = this.m_paramImpl.GetDateFromBytesInPLSQLArray(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetDateFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetDateArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000364F0 File Offset: 0x000346F0
		internal void PostBind_IntervalDS(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetIntervalDSFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetIntervalDSArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000365B4 File Offset: 0x000347B4
		internal void PostBind_IntervalYM(Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_value = this.m_paramImpl.GetIntervalYMFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType);
				}
				else
				{
					this.m_value = this.m_paramImpl.GetIntervalYMArrayFromBytes(bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00036678 File Offset: 0x00034878
		internal void PostBind_XmlType(OracleConnection connection, Accessor bindAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bArrayBind)
				{
					this.m_paramImpl.GetXmlTypeDataFromBytes(connection, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraType, ref this.m_value, this.m_direction == ParameterDirection.InputOutput);
				}
				else
				{
					this.m_paramImpl.GetXmlTypeArrayFromBytes(connection, bindAccessor, this.m_bOracleDbTypeExSet ? PrmEnumType.DBTYPE : this.m_enumType, this.m_oraType, ref this.m_value, this.m_direction == ParameterDirection.InputOutput, this.m_bindElemCnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0400066E RID: 1646
		private const int MaxOraDbType = 134;

		// Token: 0x0400066F RID: 1647
		private const int MinOraDbType = 101;

		// Token: 0x04000670 RID: 1648
		private const int DataThresholdSizeForCLOB = 32768;

		// Token: 0x04000671 RID: 1649
		private const int DataThresholdSizeForBLOB = 32768;

		// Token: 0x04000672 RID: 1650
		internal const byte MaxScale = 127;

		// Token: 0x04000673 RID: 1651
		internal const sbyte MinScale = -84;

		// Token: 0x04000674 RID: 1652
		internal const int InvalidSize = -1;

		// Token: 0x04000675 RID: 1653
		internal string m_paramName;

		// Token: 0x04000676 RID: 1654
		private string m_sourceColumn;

		// Token: 0x04000677 RID: 1655
		private DbType m_dbType;

		// Token: 0x04000678 RID: 1656
		private bool m_nullable;

		// Token: 0x04000679 RID: 1657
		private object m_value;

		// Token: 0x0400067A RID: 1658
		internal PrmEnumType m_enumType;

		// Token: 0x0400067B RID: 1659
		private int m_offset;

		// Token: 0x0400067C RID: 1660
		internal ParameterDirection m_direction;

		// Token: 0x0400067D RID: 1661
		internal int m_bindElemCnt;

		// Token: 0x0400067E RID: 1662
		internal int m_maxNoOfArrayElements;

		// Token: 0x0400067F RID: 1663
		internal OracleDbType m_oraDbType;

		// Token: 0x04000680 RID: 1664
		internal OraType m_oraType;

		// Token: 0x04000681 RID: 1665
		internal int m_maxSize;

		// Token: 0x04000682 RID: 1666
		internal bool m_modified;

		// Token: 0x04000683 RID: 1667
		private DataRowVersion m_sourceVersion;

		// Token: 0x04000684 RID: 1668
		private bool m_bSetDbType;

		// Token: 0x04000685 RID: 1669
		internal int m_characterSetId;

		// Token: 0x04000686 RID: 1670
		internal byte m_charSetForm;

		// Token: 0x04000687 RID: 1671
		internal OracleCollectionType m_collType;

		// Token: 0x04000688 RID: 1672
		internal bool m_disposed;

		// Token: 0x04000689 RID: 1673
		internal int m_maxBytesToBeWrittenOrRead;

		// Token: 0x0400068A RID: 1674
		internal int m_maxCharsToBeWrittenOrRead;

		// Token: 0x0400068B RID: 1675
		internal bool m_bArrayBind;

		// Token: 0x0400068C RID: 1676
		internal bool m_bDuplicateBind;

		// Token: 0x0400068D RID: 1677
		private int[] m_maxArrayBindSize;

		// Token: 0x0400068E RID: 1678
		private bool[] m_nullIndicatorsForArrayBind;

		// Token: 0x0400068F RID: 1679
		private bool m_IsValueNull;

		// Token: 0x04000690 RID: 1680
		internal OracleParameterCollection m_collRef;

		// Token: 0x04000691 RID: 1681
		internal OracleParameterImpl m_paramImpl = new OracleParameterImpl();

		// Token: 0x04000692 RID: 1682
		internal string m_paramPosOrName = string.Empty;

		// Token: 0x04000693 RID: 1683
		internal bool m_bOracleDbTypeExSet;

		// Token: 0x04000694 RID: 1684
		internal bool m_bReturnDateTimeOffset;

		// Token: 0x04000695 RID: 1685
		private bool m_sourceColumnNullMapping;
	}
}

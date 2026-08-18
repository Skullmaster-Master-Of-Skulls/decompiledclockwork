using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;

namespace System.Data.EntityClient
{
	// Token: 0x02000122 RID: 290
	public class EntityDataReader : DbDataReader, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0004173C File Offset: 0x0003F93C
		internal EntityDataReader(EntityCommand command, DbDataReader storeDataReader, CommandBehavior behavior)
		{
			this._command = command;
			this._storeDataReader = storeDataReader;
			this._storeExtendedDataRecord = (storeDataReader as IExtendedDataRecord);
			this._behavior = behavior;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00041765 File Offset: 0x0003F965
		public override int Depth
		{
			get
			{
				return this._storeDataReader.Depth;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00041772 File Offset: 0x0003F972
		public override int FieldCount
		{
			get
			{
				return this._storeDataReader.FieldCount;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0004177F File Offset: 0x0003F97F
		public override bool HasRows
		{
			get
			{
				return this._storeDataReader.HasRows;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0004178C File Offset: 0x0003F98C
		public override bool IsClosed
		{
			get
			{
				return this._storeDataReader.IsClosed;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00041799 File Offset: 0x0003F999
		public override int RecordsAffected
		{
			get
			{
				return this._storeDataReader.RecordsAffected;
			}
		}

		// Token: 0x170001FA RID: 506
		public override object this[int ordinal]
		{
			get
			{
				return this._storeDataReader[ordinal];
			}
		}

		// Token: 0x170001FB RID: 507
		public override object this[string name]
		{
			get
			{
				EntityUtil.CheckArgumentNull<string>(name, "name");
				return this._storeDataReader[name];
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x000417CE File Offset: 0x0003F9CE
		public override int VisibleFieldCount
		{
			get
			{
				return this._storeDataReader.VisibleFieldCount;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000417DB File Offset: 0x0003F9DB
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._storeExtendedDataRecord == null)
				{
					return null;
				}
				return this._storeExtendedDataRecord.DataRecordInfo;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000417F4 File Offset: 0x0003F9F4
		public override void Close()
		{
			if (this._command != null)
			{
				this._storeDataReader.Close();
				this._command.NotifyDataReaderClosing();
				if ((this._behavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
				{
					this._command.Connection.Close();
				}
				this._command = null;
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00041843 File Offset: 0x0003FA43
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this._storeDataReader.Dispose();
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0004185A File Offset: 0x0003FA5A
		public override bool GetBoolean(int ordinal)
		{
			return this._storeDataReader.GetBoolean(ordinal);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00041868 File Offset: 0x0003FA68
		public override byte GetByte(int ordinal)
		{
			return this._storeDataReader.GetByte(ordinal);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00041876 File Offset: 0x0003FA76
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._storeDataReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0004188A File Offset: 0x0003FA8A
		public override char GetChar(int ordinal)
		{
			return this._storeDataReader.GetChar(ordinal);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00041898 File Offset: 0x0003FA98
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._storeDataReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000418AC File Offset: 0x0003FAAC
		public override string GetDataTypeName(int ordinal)
		{
			return this._storeDataReader.GetDataTypeName(ordinal);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000418BA File Offset: 0x0003FABA
		public override DateTime GetDateTime(int ordinal)
		{
			return this._storeDataReader.GetDateTime(ordinal);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000418C8 File Offset: 0x0003FAC8
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return this._storeDataReader.GetData(ordinal);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000418D6 File Offset: 0x0003FAD6
		public override decimal GetDecimal(int ordinal)
		{
			return this._storeDataReader.GetDecimal(ordinal);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000418E4 File Offset: 0x0003FAE4
		public override double GetDouble(int ordinal)
		{
			return this._storeDataReader.GetDouble(ordinal);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000418F2 File Offset: 0x0003FAF2
		public override Type GetFieldType(int ordinal)
		{
			return this._storeDataReader.GetFieldType(ordinal);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00041900 File Offset: 0x0003FB00
		public override float GetFloat(int ordinal)
		{
			return this._storeDataReader.GetFloat(ordinal);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0004190E File Offset: 0x0003FB0E
		public override Guid GetGuid(int ordinal)
		{
			return this._storeDataReader.GetGuid(ordinal);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0004191C File Offset: 0x0003FB1C
		public override short GetInt16(int ordinal)
		{
			return this._storeDataReader.GetInt16(ordinal);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0004192A File Offset: 0x0003FB2A
		public override int GetInt32(int ordinal)
		{
			return this._storeDataReader.GetInt32(ordinal);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00041938 File Offset: 0x0003FB38
		public override long GetInt64(int ordinal)
		{
			return this._storeDataReader.GetInt64(ordinal);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00041946 File Offset: 0x0003FB46
		public override string GetName(int ordinal)
		{
			return this._storeDataReader.GetName(ordinal);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00041954 File Offset: 0x0003FB54
		public override int GetOrdinal(string name)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			return this._storeDataReader.GetOrdinal(name);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0004196E File Offset: 0x0003FB6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			return this._storeDataReader.GetProviderSpecificFieldType(ordinal);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004197C File Offset: 0x0003FB7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetProviderSpecificValue(int ordinal)
		{
			return this._storeDataReader.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0004198A File Offset: 0x0003FB8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetProviderSpecificValues(object[] values)
		{
			return this._storeDataReader.GetProviderSpecificValues(values);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00041998 File Offset: 0x0003FB98
		public override DataTable GetSchemaTable()
		{
			return this._storeDataReader.GetSchemaTable();
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000419A5 File Offset: 0x0003FBA5
		public override string GetString(int ordinal)
		{
			return this._storeDataReader.GetString(ordinal);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000419B3 File Offset: 0x0003FBB3
		public override object GetValue(int ordinal)
		{
			return this._storeDataReader.GetValue(ordinal);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000419C1 File Offset: 0x0003FBC1
		public override int GetValues(object[] values)
		{
			return this._storeDataReader.GetValues(values);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000419CF File Offset: 0x0003FBCF
		public override bool IsDBNull(int ordinal)
		{
			return this._storeDataReader.IsDBNull(ordinal);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000419E0 File Offset: 0x0003FBE0
		public override bool NextResult()
		{
			bool result;
			try
			{
				result = this._storeDataReader.NextResult();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_StoreReaderFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00041A24 File Offset: 0x0003FC24
		public override bool Read()
		{
			return this._storeDataReader.Read();
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00041A31 File Offset: 0x0003FC31
		public override IEnumerator GetEnumerator()
		{
			return this._storeDataReader.GetEnumerator();
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00041A3E File Offset: 0x0003FC3E
		public DbDataRecord GetDataRecord(int i)
		{
			if (this._storeExtendedDataRecord == null)
			{
				EntityUtil.ThrowArgumentOutOfRangeException("i");
			}
			return this._storeExtendedDataRecord.GetDataRecord(i);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000183A8 File Offset: 0x000165A8
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x04000A2B RID: 2603
		private EntityCommand _command;

		// Token: 0x04000A2C RID: 2604
		private CommandBehavior _behavior;

		// Token: 0x04000A2D RID: 2605
		private DbDataReader _storeDataReader;

		// Token: 0x04000A2E RID: 2606
		private IExtendedDataRecord _storeExtendedDataRecord;
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x0200033C RID: 828
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public class EntityDataReader : DbDataReader, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x06001D39 RID: 7481 RVA: 0x0008E1B2 File Offset: 0x0008C3B2
		internal EntityDataReader(EntityCommand command, DbDataReader storeDataReader, CommandBehavior behavior)
		{
			this._command = command;
			this._storeDataReader = storeDataReader;
			this._storeExtendedDataRecord = (storeDataReader as IExtendedDataRecord);
			this._behavior = behavior;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0008E1DB File Offset: 0x0008C3DB
		internal EntityDataReader()
		{
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0008E1E3 File Offset: 0x0008C3E3
		public override int Depth
		{
			get
			{
				return this._storeDataReader.Depth;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0008E1F0 File Offset: 0x0008C3F0
		public override int FieldCount
		{
			get
			{
				return this._storeDataReader.FieldCount;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0008E1FD File Offset: 0x0008C3FD
		public override bool HasRows
		{
			get
			{
				return this._storeDataReader.HasRows;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0008E20A File Offset: 0x0008C40A
		public override bool IsClosed
		{
			get
			{
				return this._storeDataReader.IsClosed;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0008E217 File Offset: 0x0008C417
		public override int RecordsAffected
		{
			get
			{
				return this._storeDataReader.RecordsAffected;
			}
		}

		// Token: 0x1700034A RID: 842
		public override object this[int ordinal]
		{
			get
			{
				return this._storeDataReader[ordinal];
			}
		}

		// Token: 0x1700034B RID: 843
		public override object this[string name]
		{
			get
			{
				Check.NotNull<string>(name, "name");
				return this._storeDataReader[name];
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0008E24C File Offset: 0x0008C44C
		public override int VisibleFieldCount
		{
			get
			{
				return this._storeDataReader.VisibleFieldCount;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0008E259 File Offset: 0x0008C459
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

		// Token: 0x06001D44 RID: 7492 RVA: 0x0008E270 File Offset: 0x0008C470
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

		// Token: 0x06001D45 RID: 7493 RVA: 0x0008E2BF File Offset: 0x0008C4BF
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				this._storeDataReader.Dispose();
			}
			this._disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0008E2E5 File Offset: 0x0008C4E5
		public override bool GetBoolean(int ordinal)
		{
			return this._storeDataReader.GetBoolean(ordinal);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0008E2F3 File Offset: 0x0008C4F3
		public override byte GetByte(int ordinal)
		{
			return this._storeDataReader.GetByte(ordinal);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0008E301 File Offset: 0x0008C501
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._storeDataReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0008E315 File Offset: 0x0008C515
		public override char GetChar(int ordinal)
		{
			return this._storeDataReader.GetChar(ordinal);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0008E323 File Offset: 0x0008C523
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._storeDataReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0008E337 File Offset: 0x0008C537
		public override string GetDataTypeName(int ordinal)
		{
			return this._storeDataReader.GetDataTypeName(ordinal);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0008E345 File Offset: 0x0008C545
		public override DateTime GetDateTime(int ordinal)
		{
			return this._storeDataReader.GetDateTime(ordinal);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0008E353 File Offset: 0x0008C553
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return this._storeDataReader.GetData(ordinal);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0008E361 File Offset: 0x0008C561
		public override decimal GetDecimal(int ordinal)
		{
			return this._storeDataReader.GetDecimal(ordinal);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0008E36F File Offset: 0x0008C56F
		public override double GetDouble(int ordinal)
		{
			return this._storeDataReader.GetDouble(ordinal);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0008E37D File Offset: 0x0008C57D
		public override Type GetFieldType(int ordinal)
		{
			return this._storeDataReader.GetFieldType(ordinal);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0008E38B File Offset: 0x0008C58B
		public override float GetFloat(int ordinal)
		{
			return this._storeDataReader.GetFloat(ordinal);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0008E399 File Offset: 0x0008C599
		public override Guid GetGuid(int ordinal)
		{
			return this._storeDataReader.GetGuid(ordinal);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0008E3A7 File Offset: 0x0008C5A7
		public override short GetInt16(int ordinal)
		{
			return this._storeDataReader.GetInt16(ordinal);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0008E3B5 File Offset: 0x0008C5B5
		public override int GetInt32(int ordinal)
		{
			return this._storeDataReader.GetInt32(ordinal);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
		public override long GetInt64(int ordinal)
		{
			return this._storeDataReader.GetInt64(ordinal);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0008E3D1 File Offset: 0x0008C5D1
		public override string GetName(int ordinal)
		{
			return this._storeDataReader.GetName(ordinal);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0008E3DF File Offset: 0x0008C5DF
		public override int GetOrdinal(string name)
		{
			Check.NotNull<string>(name, "name");
			return this._storeDataReader.GetOrdinal(name);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0008E3F9 File Offset: 0x0008C5F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			return this._storeDataReader.GetProviderSpecificFieldType(ordinal);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0008E407 File Offset: 0x0008C607
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetProviderSpecificValue(int ordinal)
		{
			return this._storeDataReader.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0008E415 File Offset: 0x0008C615
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetProviderSpecificValues(object[] values)
		{
			return this._storeDataReader.GetProviderSpecificValues(values);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0008E423 File Offset: 0x0008C623
		public override DataTable GetSchemaTable()
		{
			return this._storeDataReader.GetSchemaTable();
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0008E430 File Offset: 0x0008C630
		public override string GetString(int ordinal)
		{
			return this._storeDataReader.GetString(ordinal);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0008E43E File Offset: 0x0008C63E
		public override object GetValue(int ordinal)
		{
			return this._storeDataReader.GetValue(ordinal);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0008E44C File Offset: 0x0008C64C
		public override int GetValues(object[] values)
		{
			return this._storeDataReader.GetValues(values);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0008E45A File Offset: 0x0008C65A
		public override bool IsDBNull(int ordinal)
		{
			return this._storeDataReader.IsDBNull(ordinal);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0008E468 File Offset: 0x0008C668
		public override bool NextResult()
		{
			bool result;
			try
			{
				result = this._storeDataReader.NextResult();
			}
			catch (Exception innerException)
			{
				throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, innerException);
			}
			return result;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0008E5C0 File Offset: 0x0008C7C0
		public override async Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			bool result;
			try
			{
				result = await this._storeDataReader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			catch (Exception innerException)
			{
				throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, innerException);
			}
			return result;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0008E60E File Offset: 0x0008C80E
		public override bool Read()
		{
			return this._storeDataReader.Read();
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0008E61B File Offset: 0x0008C81B
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return this._storeDataReader.ReadAsync(cancellationToken);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0008E630 File Offset: 0x0008C830
		public override IEnumerator GetEnumerator()
		{
			return this._storeDataReader.GetEnumerator();
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0008E63D File Offset: 0x0008C83D
		public DbDataRecord GetDataRecord(int i)
		{
			if (this._storeExtendedDataRecord == null)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this._storeExtendedDataRecord.GetDataRecord(i);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0008E65E File Offset: 0x0008C85E
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x04000A06 RID: 2566
		private EntityCommand _command;

		// Token: 0x04000A07 RID: 2567
		private readonly CommandBehavior _behavior;

		// Token: 0x04000A08 RID: 2568
		private readonly DbDataReader _storeDataReader;

		// Token: 0x04000A09 RID: 2569
		private readonly IExtendedDataRecord _storeExtendedDataRecord;

		// Token: 0x04000A0A RID: 2570
		private bool _disposed;
	}
}

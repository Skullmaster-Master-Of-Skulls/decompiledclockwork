using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000201 RID: 513
	internal class ShapelessBufferedDataRecord : BufferedDataRecord
	{
		// Token: 0x06001272 RID: 4722 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		protected ShapelessBufferedDataRecord()
		{
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		internal static ShapelessBufferedDataRecord Initialize(string providerManifestToken, DbProviderServices providerSerivces, DbDataReader reader)
		{
			ShapelessBufferedDataRecord shapelessBufferedDataRecord = new ShapelessBufferedDataRecord();
			shapelessBufferedDataRecord.ReadMetadata(providerManifestToken, providerSerivces, reader);
			int fieldCount = shapelessBufferedDataRecord.FieldCount;
			List<object[]> list = new List<object[]>();
			if (shapelessBufferedDataRecord._spatialDataReader != null)
			{
				while (reader.Read())
				{
					object[] array = new object[fieldCount];
					for (int i = 0; i < fieldCount; i++)
					{
						if (reader.IsDBNull(i))
						{
							array[i] = DBNull.Value;
						}
						else if (shapelessBufferedDataRecord._geographyColumns[i])
						{
							array[i] = shapelessBufferedDataRecord._spatialDataReader.GetGeography(i);
						}
						else if (shapelessBufferedDataRecord._geometryColumns[i])
						{
							array[i] = shapelessBufferedDataRecord._spatialDataReader.GetGeometry(i);
						}
						else
						{
							array[i] = reader.GetValue(i);
						}
					}
					list.Add(array);
				}
			}
			else
			{
				while (reader.Read())
				{
					object[] array2 = new object[fieldCount];
					reader.GetValues(array2);
					list.Add(array2);
				}
			}
			shapelessBufferedDataRecord._rowCount = list.Count;
			shapelessBufferedDataRecord._resultSet = list;
			return shapelessBufferedDataRecord;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004DCB4 File Offset: 0x0004BEB4
		internal static async Task<ShapelessBufferedDataRecord> InitializeAsync(string providerManifestToken, DbProviderServices providerSerivces, DbDataReader reader, CancellationToken cancellationToken)
		{
			ShapelessBufferedDataRecord record = new ShapelessBufferedDataRecord();
			record.ReadMetadata(providerManifestToken, providerSerivces, reader);
			int fieldCount = record.FieldCount;
			List<object[]> resultSet = new List<object[]>();
			while (await reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>())
			{
				object[] row = new object[fieldCount];
				for (int i = 0; i < fieldCount; i++)
				{
					if (await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>())
					{
						row[i] = DBNull.Value;
					}
					else if (record._spatialDataReader != null && record._geographyColumns[i])
					{
						row[i] = await record._spatialDataReader.GetGeographyAsync(i, cancellationToken).WithCurrentCulture<DbGeography>();
					}
					else if (record._spatialDataReader != null && record._geometryColumns[i])
					{
						row[i] = await record._spatialDataReader.GetGeometryAsync(i, cancellationToken).WithCurrentCulture<DbGeometry>();
					}
					else
					{
						row[i] = await reader.GetFieldValueAsync<object>(i, cancellationToken).WithCurrentCulture<object>();
					}
				}
				resultSet.Add(row);
			}
			record._rowCount = resultSet.Count;
			record._resultSet = resultSet;
			return record;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004DD14 File Offset: 0x0004BF14
		protected override void ReadMetadata(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader)
		{
			base.ReadMetadata(providerManifestToken, providerServices, reader);
			int fieldCount = base.FieldCount;
			bool flag = false;
			DbSpatialDataReader dbSpatialDataReader = null;
			if (fieldCount > 0)
			{
				dbSpatialDataReader = providerServices.GetSpatialDataReader(reader, providerManifestToken);
			}
			if (dbSpatialDataReader != null)
			{
				this._geographyColumns = new bool[fieldCount];
				this._geometryColumns = new bool[fieldCount];
				for (int i = 0; i < fieldCount; i++)
				{
					this._geographyColumns[i] = dbSpatialDataReader.IsGeographyColumn(i);
					this._geometryColumns[i] = dbSpatialDataReader.IsGeometryColumn(i);
					flag = (flag || this._geographyColumns[i] || this._geometryColumns[i]);
				}
			}
			this._spatialDataReader = (flag ? dbSpatialDataReader : null);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004DDAD File Offset: 0x0004BFAD
		public override bool GetBoolean(int ordinal)
		{
			return this.GetFieldValue<bool>(ordinal);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004DDB6 File Offset: 0x0004BFB6
		public override byte GetByte(int ordinal)
		{
			return this.GetFieldValue<byte>(ordinal);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0004DDBF File Offset: 0x0004BFBF
		public override char GetChar(int ordinal)
		{
			return this.GetFieldValue<char>(ordinal);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0004DDC8 File Offset: 0x0004BFC8
		public override DateTime GetDateTime(int ordinal)
		{
			return this.GetFieldValue<DateTime>(ordinal);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004DDD1 File Offset: 0x0004BFD1
		public override decimal GetDecimal(int ordinal)
		{
			return this.GetFieldValue<decimal>(ordinal);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004DDDA File Offset: 0x0004BFDA
		public override double GetDouble(int ordinal)
		{
			return this.GetFieldValue<double>(ordinal);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0004DDE3 File Offset: 0x0004BFE3
		public override float GetFloat(int ordinal)
		{
			return this.GetFieldValue<float>(ordinal);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0004DDEC File Offset: 0x0004BFEC
		public override Guid GetGuid(int ordinal)
		{
			return this.GetFieldValue<Guid>(ordinal);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0004DDF5 File Offset: 0x0004BFF5
		public override short GetInt16(int ordinal)
		{
			return this.GetFieldValue<short>(ordinal);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004DDFE File Offset: 0x0004BFFE
		public override int GetInt32(int ordinal)
		{
			return this.GetFieldValue<int>(ordinal);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004DE07 File Offset: 0x0004C007
		public override long GetInt64(int ordinal)
		{
			return this.GetFieldValue<long>(ordinal);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004DE10 File Offset: 0x0004C010
		public override string GetString(int ordinal)
		{
			return this.GetFieldValue<string>(ordinal);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004DE19 File Offset: 0x0004C019
		public override T GetFieldValue<T>(int ordinal)
		{
			return (T)((object)this._currentRow[ordinal]);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004DE28 File Offset: 0x0004C028
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cancellationToken")]
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<T>((T)((object)this._currentRow[ordinal]));
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004DE3C File Offset: 0x0004C03C
		public override object GetValue(int ordinal)
		{
			return this.GetFieldValue<object>(ordinal);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004DE48 File Offset: 0x0004C048
		public override int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, base.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004DE7B File Offset: 0x0004C07B
		public override bool IsDBNull(int ordinal)
		{
			return this._currentRow.Length == 0 || DBNull.Value == this._currentRow[ordinal];
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004DE98 File Offset: 0x0004C098
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cancellationToken")]
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.IsDBNull(ordinal));
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004DEA8 File Offset: 0x0004C0A8
		public override bool Read()
		{
			if (++this._currentRowNumber < this._rowCount)
			{
				this._currentRow = this._resultSet[this._currentRowNumber];
				base.IsDataReady = true;
			}
			else
			{
				this._currentRow = null;
				base.IsDataReady = false;
			}
			return base.IsDataReady;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004DF02 File Offset: 0x0004C102
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cancellationToken")]
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.Read());
		}

		// Token: 0x04000563 RID: 1379
		private object[] _currentRow;

		// Token: 0x04000564 RID: 1380
		private List<object[]> _resultSet;

		// Token: 0x04000565 RID: 1381
		private DbSpatialDataReader _spatialDataReader;

		// Token: 0x04000566 RID: 1382
		private bool[] _geographyColumns;

		// Token: 0x04000567 RID: 1383
		private bool[] _geometryColumns;
	}
}

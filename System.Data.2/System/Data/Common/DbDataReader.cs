using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	// Token: 0x020002EF RID: 751
	public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002FC5 RID: 12229
		public abstract int Depth { get; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002FC6 RID: 12230
		public abstract int FieldCount { get; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002FC7 RID: 12231
		public abstract bool HasRows { get; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002FC8 RID: 12232
		public abstract bool IsClosed { get; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002FC9 RID: 12233
		public abstract int RecordsAffected { get; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x0012DBC0 File Offset: 0x0012CFC0
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		// Token: 0x170007CB RID: 1995
		public abstract object this[int ordinal]
		{
			get;
		}

		// Token: 0x170007CC RID: 1996
		public abstract object this[string name]
		{
			get;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x0012DBD4 File Offset: 0x0012CFD4
		public virtual void Close()
		{
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x0012DBE4 File Offset: 0x0012CFE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0012DBF8 File Offset: 0x0012CFF8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06002FD0 RID: 12240
		public abstract string GetDataTypeName(int ordinal);

		// Token: 0x06002FD1 RID: 12241
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		// Token: 0x06002FD2 RID: 12242
		public abstract Type GetFieldType(int ordinal);

		// Token: 0x06002FD3 RID: 12243
		public abstract string GetName(int ordinal);

		// Token: 0x06002FD4 RID: 12244
		public abstract int GetOrdinal(string name);

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0012DC10 File Offset: 0x0012D010
		public virtual DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002FD6 RID: 12246
		public abstract bool GetBoolean(int ordinal);

		// Token: 0x06002FD7 RID: 12247
		public abstract byte GetByte(int ordinal);

		// Token: 0x06002FD8 RID: 12248
		public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002FD9 RID: 12249
		public abstract char GetChar(int ordinal);

		// Token: 0x06002FDA RID: 12250
		public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002FDB RID: 12251 RVA: 0x0012DC24 File Offset: 0x0012D024
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DbDataReader GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x0012DC38 File Offset: 0x0012D038
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x0012DC4C File Offset: 0x0012D04C
		protected virtual DbDataReader GetDbDataReader(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002FDE RID: 12254
		public abstract DateTime GetDateTime(int ordinal);

		// Token: 0x06002FDF RID: 12255
		public abstract decimal GetDecimal(int ordinal);

		// Token: 0x06002FE0 RID: 12256
		public abstract double GetDouble(int ordinal);

		// Token: 0x06002FE1 RID: 12257
		public abstract float GetFloat(int ordinal);

		// Token: 0x06002FE2 RID: 12258
		public abstract Guid GetGuid(int ordinal);

		// Token: 0x06002FE3 RID: 12259
		public abstract short GetInt16(int ordinal);

		// Token: 0x06002FE4 RID: 12260
		public abstract int GetInt32(int ordinal);

		// Token: 0x06002FE5 RID: 12261
		public abstract long GetInt64(int ordinal);

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0012DC60 File Offset: 0x0012D060
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Type GetProviderSpecificFieldType(int ordinal)
		{
			return this.GetFieldType(ordinal);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0012DC74 File Offset: 0x0012D074
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object GetProviderSpecificValue(int ordinal)
		{
			return this.GetValue(ordinal);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0012DC88 File Offset: 0x0012D088
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		// Token: 0x06002FE9 RID: 12265
		public abstract string GetString(int ordinal);

		// Token: 0x06002FEA RID: 12266 RVA: 0x0012DC9C File Offset: 0x0012D09C
		public virtual Stream GetStream(int ordinal)
		{
			Stream result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				long num = 0L;
				byte[] array = new byte[4096];
				long bytes;
				do
				{
					bytes = this.GetBytes(ordinal, num, array, 0, array.Length);
					memoryStream.Write(array, 0, (int)bytes);
					num += bytes;
				}
				while (bytes > 0L);
				result = new MemoryStream(memoryStream.ToArray(), false);
			}
			return result;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0012DD1C File Offset: 0x0012D11C
		public virtual TextReader GetTextReader(int ordinal)
		{
			if (this.IsDBNull(ordinal))
			{
				return new StringReader(string.Empty);
			}
			return new StringReader(this.GetString(ordinal));
		}

		// Token: 0x06002FEC RID: 12268
		public abstract object GetValue(int ordinal);

		// Token: 0x06002FED RID: 12269 RVA: 0x0012DD4C File Offset: 0x0012D14C
		public virtual T GetFieldValue<T>(int ordinal)
		{
			return (T)((object)this.GetValue(ordinal));
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x0012DD68 File Offset: 0x0012D168
		public Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			return this.GetFieldValueAsync<T>(ordinal, CancellationToken.None);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0012DD84 File Offset: 0x0012D184
		public virtual Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<T>();
			}
			Task<T> result;
			try
			{
				result = Task.FromResult<T>(this.GetFieldValue<T>(ordinal));
			}
			catch (Exception ex)
			{
				result = ADP.CreatedTaskWithException<T>(ex);
			}
			return result;
		}

		// Token: 0x06002FF0 RID: 12272
		public abstract int GetValues(object[] values);

		// Token: 0x06002FF1 RID: 12273
		public abstract bool IsDBNull(int ordinal);

		// Token: 0x06002FF2 RID: 12274 RVA: 0x0012DDD8 File Offset: 0x0012D1D8
		public Task<bool> IsDBNullAsync(int ordinal)
		{
			return this.IsDBNullAsync(ordinal, CancellationToken.None);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0012DDF4 File Offset: 0x0012D1F4
		public virtual Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> result;
			try
			{
				result = (this.IsDBNull(ordinal) ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				result = ADP.CreatedTaskWithException<bool>(ex);
			}
			return result;
		}

		// Token: 0x06002FF4 RID: 12276
		public abstract bool NextResult();

		// Token: 0x06002FF5 RID: 12277
		public abstract bool Read();

		// Token: 0x06002FF6 RID: 12278 RVA: 0x0012DE50 File Offset: 0x0012D250
		public Task<bool> ReadAsync()
		{
			return this.ReadAsync(CancellationToken.None);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x0012DE68 File Offset: 0x0012D268
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> result;
			try
			{
				result = (this.Read() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				result = ADP.CreatedTaskWithException<bool>(ex);
			}
			return result;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x0012DEC4 File Offset: 0x0012D2C4
		public Task<bool> NextResultAsync()
		{
			return this.NextResultAsync(CancellationToken.None);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x0012DEDC File Offset: 0x0012D2DC
		public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> result;
			try
			{
				result = (this.NextResult() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				result = ADP.CreatedTaskWithException<bool>(ex);
			}
			return result;
		}
	}
}

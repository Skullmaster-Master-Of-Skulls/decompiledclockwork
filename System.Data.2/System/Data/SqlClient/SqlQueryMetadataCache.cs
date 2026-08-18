using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x0200019C RID: 412
	internal sealed class SqlQueryMetadataCache
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x000ABA40 File Offset: 0x000AAE40
		private SqlQueryMetadataCache()
		{
			this._cache = new MemoryCache("SqlQueryMetadataCache", null);
			this._inTrim = 0;
			this._cacheHits = 0L;
			this._cacheMisses = 0L;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000ABA7C File Offset: 0x000AAE7C
		internal static SqlQueryMetadataCache GetInstance()
		{
			return SqlQueryMetadataCache._singletonInstance;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000ABA90 File Offset: 0x000AAE90
		internal bool GetQueryMetadataIfExists(SqlCommand sqlCommand)
		{
			if (!SqlConnection.ColumnEncryptionQueryMetadataCacheEnabled)
			{
				return false;
			}
			string cacheLookupKeyFromSqlCommand = this.GetCacheLookupKeyFromSqlCommand(sqlCommand);
			if (cacheLookupKeyFromSqlCommand == null)
			{
				this.IncrementCacheMisses();
				return false;
			}
			Dictionary<string, SqlCipherMetadata> dictionary = this._cache.Get(cacheLookupKeyFromSqlCommand, null) as Dictionary<string, SqlCipherMetadata>;
			if (dictionary == null)
			{
				this.IncrementCacheMisses();
				return false;
			}
			foreach (object obj in sqlCommand.Parameters)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				SqlCipherMetadata cipherMetadata;
				if (!dictionary.TryGetValue(sqlParameter.ParameterNameFixed, out cipherMetadata))
				{
					foreach (object obj2 in sqlCommand.Parameters)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj2;
						sqlParameter2.CipherMetadata = null;
					}
					this.IncrementCacheMisses();
					return false;
				}
				sqlParameter.CipherMetadata = cipherMetadata;
			}
			foreach (object obj3 in sqlCommand.Parameters)
			{
				SqlParameter sqlParameter3 = (SqlParameter)obj3;
				SqlCipherMetadata sqlCipherMetadata = null;
				if (sqlParameter3.CipherMetadata != null)
				{
					sqlCipherMetadata = new SqlCipherMetadata(sqlParameter3.CipherMetadata.EncryptionInfo, 0, sqlParameter3.CipherMetadata.CipherAlgorithmId, sqlParameter3.CipherMetadata.CipherAlgorithmName, sqlParameter3.CipherMetadata.EncryptionType, sqlParameter3.CipherMetadata.NormalizationRuleVersion);
				}
				sqlParameter3.CipherMetadata = sqlCipherMetadata;
				if (sqlCipherMetadata != null)
				{
					try
					{
						SqlSecurityUtility.DecryptSymmetricKey(sqlCipherMetadata, sqlCommand.Connection.DataSource);
					}
					catch (Exception ex)
					{
						this.InvalidateCacheEntry(sqlCommand);
						if (ex is SqlException || ex is ArgumentException || ex is ArgumentNullException)
						{
							foreach (object obj4 in sqlCommand.Parameters)
							{
								SqlParameter sqlParameter4 = (SqlParameter)obj4;
								sqlParameter4.CipherMetadata = null;
							}
							this.IncrementCacheMisses();
							return false;
						}
						throw;
					}
				}
			}
			this.IncrementCacheHits();
			return true;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000ABD2C File Offset: 0x000AB12C
		internal void AddQueryMetadata(SqlCommand sqlCommand, bool ignoreQueriesWithReturnValueParams)
		{
			if (!SqlConnection.ColumnEncryptionQueryMetadataCacheEnabled)
			{
				return;
			}
			if (sqlCommand.CommandType == CommandType.StoredProcedure)
			{
				foreach (object obj in sqlCommand.Parameters)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (sqlParameter.Direction == ParameterDirection.ReturnValue && ignoreQueriesWithReturnValueParams)
					{
						sqlCommand.CachingQueryMetadataPostponed = true;
						return;
					}
				}
			}
			string cacheLookupKeyFromSqlCommand = this.GetCacheLookupKeyFromSqlCommand(sqlCommand);
			if (cacheLookupKeyFromSqlCommand == null)
			{
				return;
			}
			Dictionary<string, SqlCipherMetadata> dictionary = new Dictionary<string, SqlCipherMetadata>(sqlCommand.Parameters.Count);
			foreach (object obj2 in sqlCommand.Parameters)
			{
				SqlParameter sqlParameter2 = (SqlParameter)obj2;
				SqlCipherMetadata value = null;
				if (sqlParameter2.CipherMetadata != null)
				{
					value = new SqlCipherMetadata(sqlParameter2.CipherMetadata.EncryptionInfo, 0, sqlParameter2.CipherMetadata.CipherAlgorithmId, sqlParameter2.CipherMetadata.CipherAlgorithmName, sqlParameter2.CipherMetadata.EncryptionType, sqlParameter2.CipherMetadata.NormalizationRuleVersion);
				}
				dictionary.Add(sqlParameter2.ParameterNameFixed, value);
			}
			long count = this._cache.GetCount(null);
			if (count > 2300L && Interlocked.CompareExchange(ref this._inTrim, 1, 0) == 0)
			{
				try
				{
					this._cache.Trim((int)((double)(count - 2000L) / (double)count * 100.0));
				}
				finally
				{
					Interlocked.CompareExchange(ref this._inTrim, 0, 1);
				}
			}
			this._cache.Set(cacheLookupKeyFromSqlCommand, dictionary, DateTimeOffset.UtcNow.AddHours(10.0), null);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000ABF18 File Offset: 0x000AB318
		internal void InvalidateCacheEntry(SqlCommand sqlCommand)
		{
			string cacheLookupKeyFromSqlCommand = this.GetCacheLookupKeyFromSqlCommand(sqlCommand);
			if (cacheLookupKeyFromSqlCommand == null)
			{
				return;
			}
			this._cache.Remove(cacheLookupKeyFromSqlCommand, null);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000ABF40 File Offset: 0x000AB340
		private void IncrementCacheHits()
		{
			Interlocked.Increment(ref this._cacheHits);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000ABF5C File Offset: 0x000AB35C
		private void IncrementCacheMisses()
		{
			Interlocked.Increment(ref this._cacheMisses);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000ABF78 File Offset: 0x000AB378
		private void ResetCacheCounts()
		{
			this._cacheHits = 0L;
			this._cacheMisses = 0L;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000ABF98 File Offset: 0x000AB398
		private string GetCacheLookupKeyFromSqlCommand(SqlCommand sqlCommand)
		{
			SqlConnection connection = sqlCommand.Connection;
			if (connection == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(connection.DataSource, connection.DataSource.Length + 128 + sqlCommand.CommandText.Length + 6);
			stringBuilder.Append(":::");
			stringBuilder.Append(connection.Database.PadRight(128));
			stringBuilder.Append(":::");
			stringBuilder.Append(sqlCommand.CommandText);
			return stringBuilder.ToString();
		}

		// Token: 0x04000E98 RID: 3736
		private const int CacheSize = 2000;

		// Token: 0x04000E99 RID: 3737
		private const int CacheTrimThreshold = 300;

		// Token: 0x04000E9A RID: 3738
		private readonly MemoryCache _cache;

		// Token: 0x04000E9B RID: 3739
		private static readonly SqlQueryMetadataCache _singletonInstance = new SqlQueryMetadataCache();

		// Token: 0x04000E9C RID: 3740
		private int _inTrim;

		// Token: 0x04000E9D RID: 3741
		private long _cacheHits;

		// Token: 0x04000E9E RID: 3742
		private long _cacheMisses;
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Databases;
using TechnoPro.Common.DAO.Cache;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.DAO.Impl.Cache
{
	// Token: 0x0200011E RID: 286
	public class UserDatabaseCacheDAO : IUserDatabaseCacheDAO
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00052FA7 File Offset: 0x000511A7
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00052FAF File Offset: 0x000511AF
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x06000811 RID: 2065 RVA: 0x00052FB8 File Offset: 0x000511B8
		public UserDatabaseCacheDAO()
		{
			this.DatabaseManager = DatabaseLayerFactory.ClockWork;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00052FD0 File Offset: 0x000511D0
		public UserDatabaseCacheDAO(string tenantId)
		{
			bool flag = string.IsNullOrEmpty(tenantId);
			if (flag)
			{
				this.DatabaseManager = DatabaseLayerFactory.ClockWork;
			}
			else
			{
				this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, tenantId);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0005300C File Offset: 0x0005120C
		public void Remove(params string[] keys)
		{
			bool flag = keys == null || keys.Length < 1;
			if (!flag)
			{
				foreach (string value in keys)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@itemkey", DbType.String, value)
					};
					this.DatabaseManager.ExecuteNonQuery("DELETE from [CacheByUser] where ItemKey=@itemkey", parameters);
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		public object this[int userID, string key]
		{
			get
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@userid", DbType.Int32, userID),
					this.DatabaseManager.GetParameter("@itemkey", DbType.String, key)
				};
				byte[] array = (byte[])this.DatabaseManager.ExecuteScalar("select ItemValue from [CacheByUser] where UserID=@userid and ItemKey=@itemkey and (Expiry IS NULL OR Expiry>getdate())", parameters);
				return (array != null) ? UserDatabaseCacheDAO.Deserialize(array) : null;
			}
			set
			{
				bool flag = userID <= 0;
				if (!flag)
				{
					byte[] value2 = UserDatabaseCacheDAO.Serialize(value);
					DbParameter[] parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@userid", DbType.Int32, userID),
						this.DatabaseManager.GetParameter("@itemkey", DbType.String, key),
						this.DatabaseManager.GetParameter("@itemvalue", DbType.Binary, value2),
						this.DatabaseManager.GetParameter("@expiry", DbType.DateTime, DBNull.Value)
					};
					this.DatabaseManager.ExecuteNonQuery("if exists(select 1 from CacheByUser where UserID=@userid and ItemKey=@itemkey)\r\n                begin\r\n\t                update cacheByUser set ItemValue = @itemvalue,Expiry=@expiry where UserID=@userid and ItemKey=@itemkey\r\n                end\r\n                else\r\n                begin\r\n\t                insert into CacheByUser (UserID, ItemKey, ItemValue, Expiry) values(@userid, @itemkey, @itemvalue, @expiry)\r\n                end", parameters);
				}
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00053180 File Offset: 0x00051380
		public void Remove(int userID, string key)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@userid", DbType.Int32, userID),
				this.DatabaseManager.GetParameter("@itemkey", DbType.String, key)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE from [CacheByUser] where UserID = @userid and ItemKey=@itemkey", parameters);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000531D8 File Offset: 0x000513D8
		public void Insert(int userID, IDictionary<string, object> keyvalues)
		{
			this.Insert(userID, keyvalues, null);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000531F8 File Offset: 0x000513F8
		public void Insert(int userID, IDictionary<string, object> keyvalues, DateTime? expiryDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			foreach (KeyValuePair<string, object> keyValuePair in keyvalues)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@userid", DbType.Int32, userID),
					clockWork.GetParameter("@itemkey", DbType.String, keyValuePair.Key),
					clockWork.GetParameter("@itemvalue", DbType.Binary, UserDatabaseCacheDAO.Serialize(keyValuePair.Value)),
					clockWork.GetParameter("@expiry", DbType.DateTime, (expiryDate != null) ? expiryDate.Value : DBNull.Value)
				};
				clockWork.ExecuteNonQuery("if exists(select 1 from CacheByUser where UserID=@userid and ItemKey=@itemkey)\r\n                begin\r\n\t                update cacheByUser set ItemValue = @itemvalue,Expiry=@expiry where UserID=@userid and ItemKey=@itemkey\r\n                end\r\n                else\r\n                begin\r\n\t                insert into CacheByUser (UserID, ItemKey, ItemValue, Expiry) values(@userid, @itemkey, @itemvalue, @expiry)\r\n                end", parameters);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000532D4 File Offset: 0x000514D4
		public void Clear(int userID)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@userid", DbType.Int32, userID);
			this.DatabaseManager.ExecuteNonQuery("Delete from [CacheByUser] where UserID = @userid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00053318 File Offset: 0x00051518
		public IDictionary<string, object> GetValues(int userID, IList<string> keys)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@userid", DbType.String, userID),
				this.DatabaseManager.GetParameter("@itemkeys", DbType.String, keys.CommaSeparatedValues<string>())
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select ItemKey, ItemValue from CacheByUser \r\n                where UserID = @userid \r\n                and ItemKey in (select OrderID as itemkey from SplitStrings(@itemkeys))\r\n                and (Expiry IS NULL OR Expiry>getdate())", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						string key = (string)dataReader["ItemKey"];
						byte[] binary = (byte[])dataReader["ItemValue"];
						dictionary.Add(key, UserDatabaseCacheDAO.Deserialize(binary));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000533F0 File Offset: 0x000515F0
		public void Clear(string key)
		{
			this.DatabaseManager.ExecuteNonQuery("DELETE from [CacheByUser] where ItemKey=@itemkey", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@itemkey", DbType.String, key)
			});
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0005342C File Offset: 0x0005162C
		private static byte[] Serialize(object value)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, value);
			return memoryStream.ToArray();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0005345C File Offset: 0x0005165C
		private static object Deserialize(byte[] binary)
		{
			MemoryStream serializationStream = new MemoryStream(binary);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(serializationStream);
		}
	}
}

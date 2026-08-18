using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.Cache;
using TechnoPro.Common.DAO.Impl.Cache;
using TechnoPro.Common.ICore;

namespace TechnoPro.Common.Core
{
	// Token: 0x0200001C RID: 28
	public sealed class UserDatabaseCacheStorageManager : IUserDatabaseCacheStorageManager
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004C90 File Offset: 0x00002E90
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004C98 File Offset: 0x00002E98
		public IUserDatabaseCacheDAO UserDatabaseDAO { get; set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x00004CA1 File Offset: 0x00002EA1
		public UserDatabaseCacheStorageManager()
		{
			this.UserDatabaseDAO = new UserDatabaseCacheDAO();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004CB7 File Offset: 0x00002EB7
		public UserDatabaseCacheStorageManager(string tenantId)
		{
			this.UserDatabaseDAO = (string.IsNullOrEmpty(tenantId) ? new UserDatabaseCacheDAO() : new UserDatabaseCacheDAO(tenantId));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004CDD File Offset: 0x00002EDD
		public void Remove(params string[] keys)
		{
			this.UserDatabaseDAO.Remove(keys);
		}

		// Token: 0x17000039 RID: 57
		public object this[int userID, string key]
		{
			get
			{
				return this.UserDatabaseDAO[userID, key];
			}
			set
			{
				this.UserDatabaseDAO[userID, key] = value;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004D0F File Offset: 0x00002F0F
		public void Insert(int userID, string key, object value)
		{
			this.UserDatabaseDAO[userID, key] = value;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004D24 File Offset: 0x00002F24
		public void Insert(int userID, string key, object value, TimeSpan expiryTime)
		{
			bool flag = userID <= 0;
			if (!flag)
			{
				Dictionary<string, object> keyvalues = new Dictionary<string, object>
				{
					{
						key,
						value
					}
				};
				this.UserDatabaseDAO.Insert(userID, keyvalues, new DateTime?(DateTime.Now.Add(expiryTime)));
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004D70 File Offset: 0x00002F70
		public void Insert(int userID, IDictionary<string, object> keyvalues, TimeSpan expiryTime)
		{
			this.UserDatabaseDAO.Insert(userID, keyvalues, new DateTime?(DateTime.Now.Add(expiryTime)));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004D9F File Offset: 0x00002F9F
		public void Remove(int userID, string key)
		{
			this.UserDatabaseDAO.Remove(userID, key);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public void Insert(int userID, IDictionary<string, object> keyvalues)
		{
			this.UserDatabaseDAO.Insert(userID, keyvalues);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public void Clear(int userID)
		{
			this.UserDatabaseDAO.Clear(userID);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004DD1 File Offset: 0x00002FD1
		public void Clear(Enum key)
		{
			this.UserDatabaseDAO.Clear(this.GetStringKey(key));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public IDictionary<string, object> GetValues(int userID, IList<string> keys)
		{
			return this.UserDatabaseDAO.GetValues(userID, keys);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004E08 File Offset: 0x00003008
		private string GetStringKey(Enum key)
		{
			return Enum.GetName(key.GetType(), key);
		}

		// Token: 0x1700003A RID: 58
		public object this[int userID, Enum key]
		{
			get
			{
				return this[userID, this.GetStringKey(key)];
			}
			set
			{
				this[userID, this.GetStringKey(key)] = value;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004E5B File Offset: 0x0000305B
		public void Insert(int userID, Enum key, object value)
		{
			this.Insert(userID, this.GetStringKey(key), value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004E6E File Offset: 0x0000306E
		public void Remove(int userID, Enum key)
		{
			this.Remove(userID, this.GetStringKey(key));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004E80 File Offset: 0x00003080
		public void Remove(params Enum[] keys)
		{
			string[] array = new string[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this.GetStringKey(keys[i]);
			}
			this.Remove(array);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004EC0 File Offset: 0x000030C0
		public void Insert(int userID, IDictionary<Enum, object> keyvalues)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<Enum, object> keyValuePair in keyvalues)
			{
				dictionary.Add(this.GetStringKey(keyValuePair.Key), keyValuePair.Value);
			}
			this.Insert(userID, dictionary);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004F30 File Offset: 0x00003130
		public IDictionary<string, object> GetValues(int userID, IList<Enum> keys)
		{
			List<string> keys2 = keys.ToList<Enum>().ConvertAll<string>((Enum f) => this.GetStringKey(f));
			return this.GetValues(userID, keys2);
		}
	}
}

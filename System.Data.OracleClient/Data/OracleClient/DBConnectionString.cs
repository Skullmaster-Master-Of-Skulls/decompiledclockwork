using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace System.Data.OracleClient
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x000754C4 File Offset: 0x000748C4
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Hashtable synonyms, bool useOdbcRules) : this(new DbConnectionOptions(value, synonyms, useOdbcRules), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000754F4 File Offset: 0x000748F4
		internal DBConnectionString(DbConnectionOptions connectionOptions) : this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00075514 File Offset: 0x00074914
		private DBConnectionString(DbConnectionOptions connectionOptions, string restrictions, KeyRestrictionBehavior behavior, Hashtable synonyms, bool mustCloneDictionary)
		{
			switch (behavior)
			{
			case KeyRestrictionBehavior.AllowOnly:
			case KeyRestrictionBehavior.PreventUsage:
				this._behavior = behavior;
				this._encryptedUsersConnectionString = connectionOptions.UsersConnectionString(false);
				this._hasPassword = connectionOptions.HasPasswordKeyword;
				this._parsetable = connectionOptions.Parsetable;
				this._keychain = connectionOptions.KeyChain;
				if (this._hasPassword && !connectionOptions.HasPersistablePassword)
				{
					if (mustCloneDictionary)
					{
						this._parsetable = (Hashtable)this._parsetable.Clone();
					}
					if (this._parsetable.ContainsKey("password"))
					{
						this._parsetable["password"] = "*";
					}
					if (this._parsetable.ContainsKey("pwd"))
					{
						this._parsetable["pwd"] = "*";
					}
					this._keychain = connectionOptions.ReplacePasswordPwd(out this._encryptedUsersConnectionString, true);
				}
				if (!ADP.IsEmpty(restrictions))
				{
					this._restrictionValues = DBConnectionString.ParseRestrictions(restrictions, synonyms);
					this._restrictions = restrictions;
				}
				return;
			default:
				throw ADP.InvalidKeyRestrictionBehavior(behavior);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00075634 File Offset: 0x00074A34
		private DBConnectionString(DBConnectionString connectionString, string[] restrictionValues, KeyRestrictionBehavior behavior)
		{
			this._encryptedUsersConnectionString = connectionString._encryptedUsersConnectionString;
			this._parsetable = connectionString._parsetable;
			this._keychain = connectionString._keychain;
			this._hasPassword = connectionString._hasPassword;
			this._restrictionValues = restrictionValues;
			this._restrictions = null;
			this._behavior = behavior;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00075694 File Offset: 0x00074A94
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000756B4 File Offset: 0x00074AB4
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000756D4 File Offset: 0x00074AD4
		internal bool IsEmpty
		{
			get
			{
				return null == this._keychain;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x000756F4 File Offset: 0x00074AF4
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00075714 File Offset: 0x00074B14
		internal string Restrictions
		{
			get
			{
				string text = this._restrictions;
				if (text == null)
				{
					string[] restrictionValues = this._restrictionValues;
					if (restrictionValues != null && 0 < restrictionValues.Length)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < restrictionValues.Length; i++)
						{
							if (!ADP.IsEmpty(restrictionValues[i]))
							{
								stringBuilder.Append(restrictionValues[i]);
								stringBuilder.Append("=;");
							}
						}
						text = stringBuilder.ToString();
					}
				}
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		// Token: 0x17000156 RID: 342
		internal string this[string keyword]
		{
			get
			{
				return (string)this._parsetable[keyword];
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000757A4 File Offset: 0x00074BA4
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000757C4 File Offset: 0x00074BC4
		internal DBConnectionString Intersect(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			string[] restrictionValues = null;
			if (entry == null)
			{
				behavior = KeyRestrictionBehavior.AllowOnly;
			}
			else if (this._behavior != entry._behavior)
			{
				behavior = KeyRestrictionBehavior.AllowOnly;
				if (entry._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (!ADP.IsEmptyArray(this._restrictionValues))
					{
						if (!ADP.IsEmptyArray(entry._restrictionValues))
						{
							restrictionValues = DBConnectionString.NewRestrictionAllowOnly(entry._restrictionValues, this._restrictionValues);
						}
					}
					else
					{
						restrictionValues = entry._restrictionValues;
					}
				}
				else if (!ADP.IsEmptyArray(this._restrictionValues))
				{
					if (!ADP.IsEmptyArray(entry._restrictionValues))
					{
						restrictionValues = DBConnectionString.NewRestrictionAllowOnly(this._restrictionValues, entry._restrictionValues);
					}
					else
					{
						restrictionValues = this._restrictionValues;
					}
				}
			}
			else if (KeyRestrictionBehavior.PreventUsage == this._behavior)
			{
				if (ADP.IsEmptyArray(this._restrictionValues))
				{
					restrictionValues = entry._restrictionValues;
				}
				else if (ADP.IsEmptyArray(entry._restrictionValues))
				{
					restrictionValues = this._restrictionValues;
				}
				else
				{
					restrictionValues = DBConnectionString.NoDuplicateUnion(this._restrictionValues, entry._restrictionValues);
				}
			}
			else if (!ADP.IsEmptyArray(this._restrictionValues) && !ADP.IsEmptyArray(entry._restrictionValues))
			{
				if (this._restrictionValues.Length <= entry._restrictionValues.Length)
				{
					restrictionValues = DBConnectionString.NewRestrictionIntersect(this._restrictionValues, entry._restrictionValues);
				}
				else
				{
					restrictionValues = DBConnectionString.NewRestrictionIntersect(entry._restrictionValues, this._restrictionValues);
				}
			}
			return new DBConnectionString(this, restrictionValues, behavior);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00075934 File Offset: 0x00074D34
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00075964 File Offset: 0x00074D64
		internal bool IsSupersetOf(DBConnectionString entry)
		{
			switch (this._behavior)
			{
			case KeyRestrictionBehavior.AllowOnly:
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					if (!this.ContainsKey(nameValuePair.Name) && this.IsRestrictedKeyword(nameValuePair.Name))
					{
						return false;
					}
				}
				break;
			case KeyRestrictionBehavior.PreventUsage:
				if (this._restrictionValues != null)
				{
					foreach (string keyword in this._restrictionValues)
					{
						if (entry.ContainsKey(keyword))
						{
							return false;
						}
					}
				}
				break;
			default:
				throw ADP.InvalidKeyRestrictionBehavior(this._behavior);
			}
			return true;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00075A04 File Offset: 0x00074E04
		private static string[] NewRestrictionAllowOnly(string[] allowonly, string[] preventusage)
		{
			List<string> list = null;
			for (int i = 0; i < allowonly.Length; i++)
			{
				if (0 > Array.BinarySearch<string>(preventusage, allowonly[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(allowonly[i]);
				}
			}
			string[] result = null;
			if (list != null)
			{
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00075A54 File Offset: 0x00074E54
		private static string[] NewRestrictionIntersect(string[] a, string[] b)
		{
			List<string> list = null;
			for (int i = 0; i < a.Length; i++)
			{
				if (0 <= Array.BinarySearch<string>(b, a[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(a[i]);
				}
			}
			string[] result = null;
			if (list != null)
			{
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00075AA4 File Offset: 0x00074EA4
		private static string[] NoDuplicateUnion(string[] a, string[] b)
		{
			List<string> list = new List<string>(a.Length + b.Length);
			for (int i = 0; i < a.Length; i++)
			{
				list.Add(a[i]);
			}
			for (int j = 0; j < b.Length; j++)
			{
				if (0 > Array.BinarySearch<string>(a, b[j], StringComparer.Ordinal))
				{
					list.Add(b[j]);
				}
			}
			string[] array = list.ToArray();
			Array.Sort<string>(array, StringComparer.Ordinal);
			return array;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00075B14 File Offset: 0x00074F14
		private static string[] ParseRestrictions(string restrictions, Hashtable synonyms)
		{
			List<string> list = new List<string>();
			StringBuilder buffer = new StringBuilder(restrictions.Length);
			int i = 0;
			int length = restrictions.Length;
			while (i < length)
			{
				int currentPosition = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(restrictions, currentPosition, buffer, false, out text, out text2);
				if (!ADP.IsEmpty(text))
				{
					string text3 = (synonyms != null) ? ((string)synonyms[text]) : text;
					if (ADP.IsEmpty(text3))
					{
						throw ADP.KeywordNotSupported(text);
					}
					list.Add(text3);
				}
			}
			return DBConnectionString.RemoveDuplicates(list.ToArray());
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00075B94 File Offset: 0x00074F94
		internal static string[] RemoveDuplicates(string[] restrictions)
		{
			int num = restrictions.Length;
			if (0 < num)
			{
				Array.Sort<string>(restrictions, StringComparer.Ordinal);
				for (int i = 1; i < restrictions.Length; i++)
				{
					string text = restrictions[i - 1];
					if (text.Length == 0 || text == restrictions[i])
					{
						restrictions[i - 1] = null;
						num--;
					}
				}
				if (restrictions[restrictions.Length - 1].Length == 0)
				{
					restrictions[restrictions.Length - 1] = null;
					num--;
				}
				if (num != restrictions.Length)
				{
					string[] array = new string[num];
					num = 0;
					for (int j = 0; j < restrictions.Length; j++)
					{
						if (restrictions[j] != null)
						{
							array[num++] = restrictions[j];
						}
					}
					restrictions = array;
				}
			}
			return restrictions;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00075C44 File Offset: 0x00075044
		[Conditional("DEBUG")]
		private static void Verify(string[] restrictionValues)
		{
			if (restrictionValues != null)
			{
				for (int i = 1; i < restrictionValues.Length; i++)
				{
				}
			}
		}

		// Token: 0x040004FD RID: 1277
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x040004FE RID: 1278
		private readonly Hashtable _parsetable;

		// Token: 0x040004FF RID: 1279
		private readonly NameValuePair _keychain;

		// Token: 0x04000500 RID: 1280
		private readonly bool _hasPassword;

		// Token: 0x04000501 RID: 1281
		private readonly string[] _restrictionValues;

		// Token: 0x04000502 RID: 1282
		private readonly string _restrictions;

		// Token: 0x04000503 RID: 1283
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x04000504 RID: 1284
		private readonly string _encryptedActualConnectionString;
	}
}

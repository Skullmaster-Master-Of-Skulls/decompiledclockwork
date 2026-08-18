using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x020002E5 RID: 741
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x06002ED0 RID: 11984 RVA: 0x00128FD4 File Offset: 0x001283D4
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Hashtable synonyms, bool useOdbcRules) : this(new DbConnectionOptions(value, synonyms, useOdbcRules), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x00128FF8 File Offset: 0x001283F8
		internal DBConnectionString(DbConnectionOptions connectionOptions) : this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x00129010 File Offset: 0x00128410
		private DBConnectionString(DbConnectionOptions connectionOptions, string restrictions, KeyRestrictionBehavior behavior, Hashtable synonyms, bool mustCloneDictionary)
		{
			if (behavior <= KeyRestrictionBehavior.PreventUsage)
			{
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
			}
			throw ADP.InvalidKeyRestrictionBehavior(behavior);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00129118 File Offset: 0x00128518
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x00129170 File Offset: 0x00128570
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x00129184 File Offset: 0x00128584
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x00129198 File Offset: 0x00128598
		internal bool IsEmpty
		{
			get
			{
				return this._keychain == null;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x001291B0 File Offset: 0x001285B0
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x001291C4 File Offset: 0x001285C4
		internal string Restrictions
		{
			get
			{
				string text = this._restrictions;
				if (text == null)
				{
					string[] restrictionValues = this._restrictionValues;
					if (restrictionValues != null && restrictionValues.Length != 0)
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

		// Token: 0x1700079A RID: 1946
		internal string this[string keyword]
		{
			get
			{
				return (string)this._parsetable[keyword];
			}
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x00129250 File Offset: 0x00128650
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x0012926C File Offset: 0x0012866C
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

		// Token: 0x06002EDC RID: 11996 RVA: 0x001293D0 File Offset: 0x001287D0
		[Conditional("DEBUG")]
		private void ValidateCombinedSet(DBConnectionString componentSet, DBConnectionString combinedSet)
		{
			if (componentSet != null && combinedSet._restrictionValues != null && componentSet._restrictionValues != null)
			{
				if (componentSet._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
					{
						KeyRestrictionBehavior behavior = combinedSet._behavior;
						return;
					}
				}
				else if (componentSet._behavior == KeyRestrictionBehavior.PreventUsage && combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
				{
					KeyRestrictionBehavior behavior2 = combinedSet._behavior;
				}
			}
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x00129424 File Offset: 0x00128824
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x00129450 File Offset: 0x00128850
		internal bool IsSupersetOf(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			if (behavior != KeyRestrictionBehavior.AllowOnly)
			{
				if (behavior != KeyRestrictionBehavior.PreventUsage)
				{
					throw ADP.InvalidKeyRestrictionBehavior(this._behavior);
				}
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
			}
			else
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					if (!this.ContainsKey(nameValuePair.Name) && this.IsRestrictedKeyword(nameValuePair.Name))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x001294E0 File Offset: 0x001288E0
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

		// Token: 0x06002EE0 RID: 12000 RVA: 0x00129530 File Offset: 0x00128930
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

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00129580 File Offset: 0x00128980
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

		// Token: 0x06002EE2 RID: 12002 RVA: 0x001295EC File Offset: 0x001289EC
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

		// Token: 0x06002EE3 RID: 12003 RVA: 0x0012966C File Offset: 0x00128A6C
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

		// Token: 0x06002EE4 RID: 12004 RVA: 0x00129710 File Offset: 0x00128B10
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

		// Token: 0x04001CD0 RID: 7376
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x04001CD1 RID: 7377
		private readonly Hashtable _parsetable;

		// Token: 0x04001CD2 RID: 7378
		private readonly NameValuePair _keychain;

		// Token: 0x04001CD3 RID: 7379
		private readonly bool _hasPassword;

		// Token: 0x04001CD4 RID: 7380
		private readonly string[] _restrictionValues;

		// Token: 0x04001CD5 RID: 7381
		private readonly string _restrictions;

		// Token: 0x04001CD6 RID: 7382
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x04001CD7 RID: 7383
		private readonly string _encryptedActualConnectionString;
	}
}

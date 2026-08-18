using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x06001398 RID: 5016 RVA: 0x0023C068 File Offset: 0x0023B468
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Hashtable synonyms, bool useOdbcRules) : this(new DbConnectionOptions(value, synonyms, useOdbcRules), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0023C098 File Offset: 0x0023B498
		internal DBConnectionString(DbConnectionOptions connectionOptions) : this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0023C0B8 File Offset: 0x0023B4B8
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

		// Token: 0x0600139B RID: 5019 RVA: 0x0023C1D8 File Offset: 0x0023B5D8
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

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0023C238 File Offset: 0x0023B638
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0023C258 File Offset: 0x0023B658
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0023C278 File Offset: 0x0023B678
		internal bool IsEmpty
		{
			get
			{
				return null == this._keychain;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0023C298 File Offset: 0x0023B698
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0023C2B8 File Offset: 0x0023B6B8
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

		// Token: 0x170002A7 RID: 679
		internal string this[string keyword]
		{
			get
			{
				return (string)this._parsetable[keyword];
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0023C348 File Offset: 0x0023B748
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0023C368 File Offset: 0x0023B768
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

		// Token: 0x060013A4 RID: 5028 RVA: 0x0023C4D8 File Offset: 0x0023B8D8
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0023C508 File Offset: 0x0023B908
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

		// Token: 0x060013A6 RID: 5030 RVA: 0x0023C5A8 File Offset: 0x0023B9A8
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

		// Token: 0x060013A7 RID: 5031 RVA: 0x0023C5F8 File Offset: 0x0023B9F8
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

		// Token: 0x060013A8 RID: 5032 RVA: 0x0023C648 File Offset: 0x0023BA48
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

		// Token: 0x060013A9 RID: 5033 RVA: 0x0023C6B8 File Offset: 0x0023BAB8
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

		// Token: 0x060013AA RID: 5034 RVA: 0x0023C738 File Offset: 0x0023BB38
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

		// Token: 0x060013AB RID: 5035 RVA: 0x0023C7E8 File Offset: 0x0023BBE8
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

		// Token: 0x04000C25 RID: 3109
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x04000C26 RID: 3110
		private readonly Hashtable _parsetable;

		// Token: 0x04000C27 RID: 3111
		private readonly NameValuePair _keychain;

		// Token: 0x04000C28 RID: 3112
		private readonly bool _hasPassword;

		// Token: 0x04000C29 RID: 3113
		private readonly string[] _restrictionValues;

		// Token: 0x04000C2A RID: 3114
		private readonly string _restrictions;

		// Token: 0x04000C2B RID: 3115
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x04000C2C RID: 3116
		private readonly string _encryptedActualConnectionString;
	}
}

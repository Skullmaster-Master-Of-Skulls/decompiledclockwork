using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000309 RID: 777
	[Serializable]
	internal sealed class NameValuePermission : IComparable
	{
		// Token: 0x0600311B RID: 12571 RVA: 0x00131A54 File Offset: 0x00130E54
		internal NameValuePermission()
		{
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00131A68 File Offset: 0x00130E68
		private NameValuePermission(string keyword)
		{
			this._value = keyword;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00131A84 File Offset: 0x00130E84
		private NameValuePermission(string value, DBConnectionString entry)
		{
			this._value = value;
			this._entry = entry;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00131AA8 File Offset: 0x00130EA8
		private NameValuePermission(NameValuePermission permit)
		{
			this._value = permit._value;
			this._entry = permit._entry;
			this._tree = permit._tree;
			if (this._tree != null)
			{
				NameValuePermission[] array = this._tree.Clone() as NameValuePermission[];
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						array[i] = array[i].CopyNameValue();
					}
				}
				this._tree = array;
			}
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00131B20 File Offset: 0x00130F20
		int IComparable.CompareTo(object a)
		{
			return StringComparer.Ordinal.Compare(this._value, ((NameValuePermission)a)._value);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00131B48 File Offset: 0x00130F48
		internal static void AddEntry(NameValuePermission kvtree, ArrayList entries, DBConnectionString entry)
		{
			if (entry.KeyChain != null)
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					NameValuePermission nameValuePermission = kvtree.CheckKeyForValue(nameValuePair.Name);
					if (nameValuePermission == null)
					{
						nameValuePermission = new NameValuePermission(nameValuePair.Name);
						kvtree.Add(nameValuePermission);
					}
					kvtree = nameValuePermission;
					nameValuePermission = kvtree.CheckKeyForValue(nameValuePair.Value);
					if (nameValuePermission == null)
					{
						DBConnectionString dbconnectionString = (nameValuePair.Next != null) ? null : entry;
						nameValuePermission = new NameValuePermission(nameValuePair.Value, dbconnectionString);
						kvtree.Add(nameValuePermission);
						if (dbconnectionString != null)
						{
							entries.Add(dbconnectionString);
						}
					}
					else if (nameValuePair.Next == null)
					{
						if (nameValuePermission._entry != null)
						{
							entries.Remove(nameValuePermission._entry);
							nameValuePermission._entry = nameValuePermission._entry.Intersect(entry);
						}
						else
						{
							nameValuePermission._entry = entry;
						}
						entries.Add(nameValuePermission._entry);
					}
					kvtree = nameValuePermission;
				}
				return;
			}
			DBConnectionString entry2 = kvtree._entry;
			if (entry2 != null)
			{
				entries.Remove(entry2);
				kvtree._entry = entry2.Intersect(entry);
			}
			else
			{
				kvtree._entry = entry;
			}
			entries.Add(kvtree._entry);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x00131C5C File Offset: 0x0013105C
		internal void Intersect(ArrayList entries, NameValuePermission target)
		{
			if (target == null)
			{
				this._tree = null;
				this._entry = null;
				return;
			}
			if (this._entry != null)
			{
				entries.Remove(this._entry);
				this._entry = this._entry.Intersect(target._entry);
				entries.Add(this._entry);
			}
			else if (target._entry != null)
			{
				this._entry = target._entry.Intersect(null);
				entries.Add(this._entry);
			}
			if (this._tree != null)
			{
				int num = this._tree.Length;
				for (int i = 0; i < this._tree.Length; i++)
				{
					NameValuePermission nameValuePermission = target.CheckKeyForValue(this._tree[i]._value);
					if (nameValuePermission != null)
					{
						this._tree[i].Intersect(entries, nameValuePermission);
					}
					else
					{
						this._tree[i] = null;
						num--;
					}
				}
				if (num == 0)
				{
					this._tree = null;
					return;
				}
				if (num < this._tree.Length)
				{
					NameValuePermission[] array = new NameValuePermission[num];
					int j = 0;
					int num2 = 0;
					while (j < this._tree.Length)
					{
						if (this._tree[j] != null)
						{
							array[num2++] = this._tree[j];
						}
						j++;
					}
					this._tree = array;
				}
			}
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00131D90 File Offset: 0x00131190
		private void Add(NameValuePermission permit)
		{
			NameValuePermission[] tree = this._tree;
			int num = (tree != null) ? tree.Length : 0;
			NameValuePermission[] array = new NameValuePermission[1 + num];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = tree[i];
			}
			array[num] = permit;
			Array.Sort<NameValuePermission>(array);
			this._tree = array;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x00131DE0 File Offset: 0x001311E0
		internal bool CheckValueForKeyPermit(DBConnectionString parsetable)
		{
			if (parsetable == null)
			{
				return false;
			}
			bool flag = false;
			NameValuePermission[] tree = this._tree;
			if (tree != null)
			{
				flag = parsetable.IsEmpty;
				if (!flag)
				{
					foreach (NameValuePermission nameValuePermission in tree)
					{
						if (nameValuePermission != null)
						{
							string value = nameValuePermission._value;
							if (parsetable.ContainsKey(value))
							{
								string keyInQuestion = parsetable[value];
								NameValuePermission nameValuePermission2 = nameValuePermission.CheckKeyForValue(keyInQuestion);
								if (nameValuePermission2 == null)
								{
									return false;
								}
								if (!nameValuePermission2.CheckValueForKeyPermit(parsetable))
								{
									return false;
								}
								flag = true;
							}
						}
					}
				}
			}
			DBConnectionString entry = this._entry;
			if (entry != null)
			{
				flag = entry.IsSupersetOf(parsetable);
			}
			return flag;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00131E70 File Offset: 0x00131270
		private NameValuePermission CheckKeyForValue(string keyInQuestion)
		{
			NameValuePermission[] tree = this._tree;
			if (tree != null)
			{
				foreach (NameValuePermission nameValuePermission in tree)
				{
					if (string.Equals(keyInQuestion, nameValuePermission._value, StringComparison.OrdinalIgnoreCase))
					{
						return nameValuePermission;
					}
				}
			}
			return null;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x00131EAC File Offset: 0x001312AC
		internal NameValuePermission CopyNameValue()
		{
			return new NameValuePermission(this);
		}

		// Token: 0x04001D75 RID: 7541
		private string _value;

		// Token: 0x04001D76 RID: 7542
		private DBConnectionString _entry;

		// Token: 0x04001D77 RID: 7543
		private NameValuePermission[] _tree;

		// Token: 0x04001D78 RID: 7544
		internal static readonly NameValuePermission Default;
	}
}

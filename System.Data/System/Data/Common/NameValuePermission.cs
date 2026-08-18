using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	internal sealed class NameValuePermission : IComparable
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x00244EA8 File Offset: 0x002442A8
		internal NameValuePermission()
		{
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00244EC8 File Offset: 0x002442C8
		private NameValuePermission(string keyword)
		{
			this._value = keyword;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00244EE8 File Offset: 0x002442E8
		private NameValuePermission(string value, DBConnectionString entry)
		{
			this._value = value;
			this._entry = entry;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00244F18 File Offset: 0x00244318
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

		// Token: 0x06001587 RID: 5511 RVA: 0x00244F98 File Offset: 0x00244398
		int IComparable.CompareTo(object a)
		{
			return StringComparer.Ordinal.Compare(this._value, ((NameValuePermission)a)._value);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00244FC8 File Offset: 0x002443C8
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

		// Token: 0x06001589 RID: 5513 RVA: 0x002450E8 File Offset: 0x002444E8
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

		// Token: 0x0600158A RID: 5514 RVA: 0x00245228 File Offset: 0x00244628
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

		// Token: 0x0600158B RID: 5515 RVA: 0x00245278 File Offset: 0x00244678
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

		// Token: 0x0600158C RID: 5516 RVA: 0x00245308 File Offset: 0x00244708
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

		// Token: 0x0600158D RID: 5517 RVA: 0x00245348 File Offset: 0x00244748
		internal NameValuePermission CopyNameValue()
		{
			return new NameValuePermission(this);
		}

		// Token: 0x04000CB4 RID: 3252
		private string _value;

		// Token: 0x04000CB5 RID: 3253
		private DBConnectionString _entry;

		// Token: 0x04000CB6 RID: 3254
		private NameValuePermission[] _tree;

		// Token: 0x04000CB7 RID: 3255
		internal static readonly NameValuePermission Default;
	}
}

using System;
using System.Collections.Generic;

namespace System.Dynamic
{
	// Token: 0x020000C6 RID: 198
	internal class ExpandoClass
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x00011AD5 File Offset: 0x0000FCD5
		internal ExpandoClass()
		{
			this._hashCode = 6551;
			this._keys = new string[0];
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		internal ExpandoClass(string[] keys, int hashCode)
		{
			this._hashCode = hashCode;
			this._keys = keys;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00011B0C File Offset: 0x0000FD0C
		internal ExpandoClass FindNewClass(string newKey)
		{
			int hashCode = this._hashCode ^ newKey.GetHashCode();
			ExpandoClass result;
			lock (this)
			{
				List<WeakReference> transitionList = this.GetTransitionList(hashCode);
				for (int i = 0; i < transitionList.Count; i++)
				{
					ExpandoClass expandoClass = transitionList[i].Target as ExpandoClass;
					if (expandoClass == null)
					{
						transitionList.RemoveAt(i);
						i--;
					}
					else if (string.Equals(expandoClass._keys[expandoClass._keys.Length - 1], newKey, StringComparison.Ordinal))
					{
						return expandoClass;
					}
				}
				string[] array = new string[this._keys.Length + 1];
				Array.Copy(this._keys, array, this._keys.Length);
				array[this._keys.Length] = newKey;
				ExpandoClass expandoClass2 = new ExpandoClass(array, hashCode);
				transitionList.Add(new WeakReference(expandoClass2));
				result = expandoClass2;
			}
			return result;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00011C08 File Offset: 0x0000FE08
		private List<WeakReference> GetTransitionList(int hashCode)
		{
			if (this._transitions == null)
			{
				this._transitions = new Dictionary<int, List<WeakReference>>();
			}
			List<WeakReference> result;
			if (!this._transitions.TryGetValue(hashCode, out result))
			{
				result = (this._transitions[hashCode] = new List<WeakReference>());
			}
			return result;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00011C4C File Offset: 0x0000FE4C
		internal int GetValueIndex(string name, bool caseInsensitive, ExpandoObject obj)
		{
			if (caseInsensitive)
			{
				return this.GetValueIndexCaseInsensitive(name, obj);
			}
			return this.GetValueIndexCaseSensitive(name);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00011C64 File Offset: 0x0000FE64
		internal int GetValueIndexCaseSensitive(string name)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (string.Equals(this._keys[i], name, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00011C98 File Offset: 0x0000FE98
		private int GetValueIndexCaseInsensitive(string name, ExpandoObject obj)
		{
			int num = -1;
			object lockObject = obj.LockObject;
			lock (lockObject)
			{
				for (int i = this._keys.Length - 1; i >= 0; i--)
				{
					if (string.Equals(this._keys[i], name, StringComparison.OrdinalIgnoreCase) && !obj.IsDeletedMember(i))
					{
						if (num != -1)
						{
							return -2;
						}
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00011D18 File Offset: 0x0000FF18
		internal string[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x040005A8 RID: 1448
		private readonly string[] _keys;

		// Token: 0x040005A9 RID: 1449
		private readonly int _hashCode;

		// Token: 0x040005AA RID: 1450
		private Dictionary<int, List<WeakReference>> _transitions;

		// Token: 0x040005AB RID: 1451
		private const int EmptyHashCode = 6551;

		// Token: 0x040005AC RID: 1452
		internal static ExpandoClass Empty = new ExpandoClass();
	}
}

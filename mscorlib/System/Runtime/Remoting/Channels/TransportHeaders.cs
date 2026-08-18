using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006EF RID: 1775
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		// Token: 0x06003F60 RID: 16224 RVA: 0x000D85BF File Offset: 0x000D75BF
		public TransportHeaders()
		{
			this._headerList = new ArrayList(6);
		}

		// Token: 0x17000AAA RID: 2730
		public object this[object key]
		{
			get
			{
				string strB = (string)key;
				foreach (object obj in this._headerList)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (string.Compare((string)dictionaryEntry.Key, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					return;
				}
				string strB = (string)key;
				for (int i = this._headerList.Count - 1; i >= 0; i--)
				{
					string strA = (string)((DictionaryEntry)this._headerList[i]).Key;
					if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this._headerList.RemoveAt(i);
						break;
					}
				}
				if (value != null)
				{
					this._headerList.Add(new DictionaryEntry(key, value));
				}
			}
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000D86D6 File Offset: 0x000D76D6
		public IEnumerator GetEnumerator()
		{
			return this._headerList.GetEnumerator();
		}

		// Token: 0x0400201B RID: 8219
		private ArrayList _headerList;
	}
}

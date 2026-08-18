using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel
{
	// Token: 0x02000002 RID: 2
	internal sealed class AttributeList : IList<Attribute>, ICollection<Attribute>, IEnumerable<Attribute>, IEnumerable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AttributeList(AttributeCollection attributes)
		{
			this._attributes = attributes;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public int Count
		{
			get
			{
				return this._attributes.Count;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020EC File Offset: 0x000002EC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000003 RID: 3
		public Attribute this[int index]
		{
			get
			{
				return this._attributes[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002104 File Offset: 0x00000304
		public void Add(Attribute attribute)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210B File Offset: 0x0000030B
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002112 File Offset: 0x00000312
		public bool Contains(Attribute attribute)
		{
			return this._attributes.Contains(attribute);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002120 File Offset: 0x00000320
		public void CopyTo(Attribute[] target, int startIndex)
		{
			this._attributes.CopyTo(target, startIndex);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E0 File Offset: 0x000003E0
		public IEnumerator<Attribute> GetEnumerator()
		{
			for (int i = 0; i < this._attributes.Count; i++)
			{
				yield return this._attributes[i];
			}
			yield break;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._attributes).GetEnumerator();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000040C
		public int IndexOf(Attribute attribute)
		{
			for (int i = 0; i < this._attributes.Count; i++)
			{
				if (attribute == this._attributes[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002241 File Offset: 0x00000441
		public void Insert(int index, Attribute attribute)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002248 File Offset: 0x00000448
		bool ICollection<Attribute>.Remove(Attribute attribute)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000224F File Offset: 0x0000044F
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000001 RID: 1
		private readonly AttributeCollection _attributes;
	}
}

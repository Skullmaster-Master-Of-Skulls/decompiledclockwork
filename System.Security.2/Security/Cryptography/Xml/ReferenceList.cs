using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000037 RID: 55
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ReferenceList : IList, ICollection, IEnumerable
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00007971 File Offset: 0x00005B71
		public ReferenceList()
		{
			this.m_references = new ArrayList();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007984 File Offset: 0x00005B84
		public IEnumerator GetEnumerator()
		{
			return this.m_references.GetEnumerator();
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007991 File Offset: 0x00005B91
		public int Count
		{
			get
			{
				return this.m_references.Count;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000079A0 File Offset: 0x00005BA0
		public int Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_references.Add(value);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000079EC File Offset: 0x00005BEC
		public void Clear()
		{
			this.m_references.Clear();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000079F9 File Offset: 0x00005BF9
		public bool Contains(object value)
		{
			return this.m_references.Contains(value);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007A07 File Offset: 0x00005C07
		public int IndexOf(object value)
		{
			return this.m_references.IndexOf(value);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007A18 File Offset: 0x00005C18
		public void Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_references.Insert(index, value);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007A65 File Offset: 0x00005C65
		public void Remove(object value)
		{
			this.m_references.Remove(value);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007A73 File Offset: 0x00005C73
		public void RemoveAt(int index)
		{
			this.m_references.RemoveAt(index);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007A81 File Offset: 0x00005C81
		public EncryptedReference Item(int index)
		{
			return (EncryptedReference)this.m_references[index];
		}

		// Token: 0x1700003F RID: 63
		[IndexerName("ItemOf")]
		public EncryptedReference this[int index]
		{
			get
			{
				return this.Item(index);
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		object IList.this[int index]
		{
			get
			{
				return this.m_references[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(value is DataReference) && !(value is KeyReference))
				{
					throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
				}
				this.m_references[index] = value;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007B05 File Offset: 0x00005D05
		public void CopyTo(Array array, int index)
		{
			this.m_references.CopyTo(array, index);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007B14 File Offset: 0x00005D14
		bool IList.IsFixedSize
		{
			get
			{
				return this.m_references.IsFixedSize;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007B21 File Offset: 0x00005D21
		bool IList.IsReadOnly
		{
			get
			{
				return this.m_references.IsReadOnly;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007B2E File Offset: 0x00005D2E
		public object SyncRoot
		{
			get
			{
				return this.m_references.SyncRoot;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007B3B File Offset: 0x00005D3B
		public bool IsSynchronized
		{
			get
			{
				return this.m_references.IsSynchronized;
			}
		}

		// Token: 0x040003B1 RID: 945
		private ArrayList m_references;
	}
}

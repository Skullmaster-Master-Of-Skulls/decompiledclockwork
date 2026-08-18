using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C5 RID: 197
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ReferenceList : IList, ICollection, IEnumerable
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x00018001 File Offset: 0x00017001
		public ReferenceList()
		{
			this.m_references = new ArrayList();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00018014 File Offset: 0x00017014
		public IEnumerator GetEnumerator()
		{
			return this.m_references.GetEnumerator();
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00018021 File Offset: 0x00017021
		public int Count
		{
			get
			{
				return this.m_references.Count;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00018030 File Offset: 0x00017030
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

		// Token: 0x060004BC RID: 1212 RVA: 0x0001807C File Offset: 0x0001707C
		public void Clear()
		{
			this.m_references.Clear();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00018089 File Offset: 0x00017089
		public bool Contains(object value)
		{
			return this.m_references.Contains(value);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00018097 File Offset: 0x00017097
		public int IndexOf(object value)
		{
			return this.m_references.IndexOf(value);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000180A8 File Offset: 0x000170A8
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

		// Token: 0x060004C0 RID: 1216 RVA: 0x000180F5 File Offset: 0x000170F5
		public void Remove(object value)
		{
			this.m_references.Remove(value);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00018103 File Offset: 0x00017103
		public void RemoveAt(int index)
		{
			this.m_references.RemoveAt(index);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00018111 File Offset: 0x00017111
		public EncryptedReference Item(int index)
		{
			return (EncryptedReference)this.m_references[index];
		}

		// Token: 0x170000F8 RID: 248
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

		// Token: 0x170000F9 RID: 249
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

		// Token: 0x060004C7 RID: 1223 RVA: 0x00018195 File Offset: 0x00017195
		public void CopyTo(Array array, int index)
		{
			this.m_references.CopyTo(array, index);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000181A4 File Offset: 0x000171A4
		bool IList.IsFixedSize
		{
			get
			{
				return this.m_references.IsFixedSize;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000181B1 File Offset: 0x000171B1
		bool IList.IsReadOnly
		{
			get
			{
				return this.m_references.IsReadOnly;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000181BE File Offset: 0x000171BE
		public object SyncRoot
		{
			get
			{
				return this.m_references.SyncRoot;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000181CB File Offset: 0x000171CB
		public bool IsSynchronized
		{
			get
			{
				return this.m_references.IsSynchronized;
			}
		}

		// Token: 0x040005B8 RID: 1464
		private ArrayList m_references;
	}
}

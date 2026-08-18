using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003F RID: 63
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EncryptionPropertyCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00008411 File Offset: 0x00006611
		public EncryptionPropertyCollection()
		{
			this.m_props = new ArrayList();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008424 File Offset: 0x00006624
		public IEnumerator GetEnumerator()
		{
			return this.m_props.GetEnumerator();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008431 File Offset: 0x00006631
		public int Count
		{
			get
			{
				return this.m_props.Count;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000843E File Offset: 0x0000663E
		int IList.Add(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_props.Add(value);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008469 File Offset: 0x00006669
		public int Add(EncryptionProperty value)
		{
			return this.m_props.Add(value);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008477 File Offset: 0x00006677
		public void Clear()
		{
			this.m_props.Clear();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008484 File Offset: 0x00006684
		bool IList.Contains(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_props.Contains(value);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000084AF File Offset: 0x000066AF
		public bool Contains(EncryptionProperty value)
		{
			return this.m_props.Contains(value);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000084BD File Offset: 0x000066BD
		int IList.IndexOf(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_props.IndexOf(value);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000084E8 File Offset: 0x000066E8
		public int IndexOf(EncryptionProperty value)
		{
			return this.m_props.IndexOf(value);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000084F6 File Offset: 0x000066F6
		void IList.Insert(int index, object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_props.Insert(index, value);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008522 File Offset: 0x00006722
		public void Insert(int index, EncryptionProperty value)
		{
			this.m_props.Insert(index, value);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008531 File Offset: 0x00006731
		void IList.Remove(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_props.Remove(value);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000855C File Offset: 0x0000675C
		public void Remove(EncryptionProperty value)
		{
			this.m_props.Remove(value);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000856A File Offset: 0x0000676A
		public void RemoveAt(int index)
		{
			this.m_props.RemoveAt(index);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008578 File Offset: 0x00006778
		public bool IsFixedSize
		{
			get
			{
				return this.m_props.IsFixedSize;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008585 File Offset: 0x00006785
		public bool IsReadOnly
		{
			get
			{
				return this.m_props.IsReadOnly;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008592 File Offset: 0x00006792
		public EncryptionProperty Item(int index)
		{
			return (EncryptionProperty)this.m_props[index];
		}

		// Token: 0x1700005D RID: 93
		[IndexerName("ItemOf")]
		public EncryptionProperty this[int index]
		{
			get
			{
				return (EncryptionProperty)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x1700005E RID: 94
		object IList.this[int index]
		{
			get
			{
				return this.m_props[index];
			}
			set
			{
				if (!(value is EncryptionProperty))
				{
					throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
				}
				this.m_props[index] = value;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000085ED File Offset: 0x000067ED
		public void CopyTo(Array array, int index)
		{
			this.m_props.CopyTo(array, index);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000085ED File Offset: 0x000067ED
		public void CopyTo(EncryptionProperty[] array, int index)
		{
			this.m_props.CopyTo(array, index);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000085FC File Offset: 0x000067FC
		public object SyncRoot
		{
			get
			{
				return this.m_props.SyncRoot;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008609 File Offset: 0x00006809
		public bool IsSynchronized
		{
			get
			{
				return this.m_props.IsSynchronized;
			}
		}

		// Token: 0x040003C8 RID: 968
		private ArrayList m_props;
	}
}

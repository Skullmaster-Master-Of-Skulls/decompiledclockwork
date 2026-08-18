using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000BE RID: 190
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EncryptionPropertyCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x00017015 File Offset: 0x00016015
		public EncryptionPropertyCollection()
		{
			this.m_props = new ArrayList();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00017028 File Offset: 0x00016028
		public IEnumerator GetEnumerator()
		{
			return this.m_props.GetEnumerator();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00017035 File Offset: 0x00016035
		public int Count
		{
			get
			{
				return this.m_props.Count;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00017042 File Offset: 0x00016042
		int IList.Add(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_props.Add(value);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001706D File Offset: 0x0001606D
		public int Add(EncryptionProperty value)
		{
			return this.m_props.Add(value);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001707B File Offset: 0x0001607B
		public void Clear()
		{
			this.m_props.Clear();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00017088 File Offset: 0x00016088
		bool IList.Contains(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_props.Contains(value);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000170B3 File Offset: 0x000160B3
		public bool Contains(EncryptionProperty value)
		{
			return this.m_props.Contains(value);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000170C1 File Offset: 0x000160C1
		int IList.IndexOf(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			return this.m_props.IndexOf(value);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000170EC File Offset: 0x000160EC
		public int IndexOf(EncryptionProperty value)
		{
			return this.m_props.IndexOf(value);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000170FA File Offset: 0x000160FA
		void IList.Insert(int index, object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_props.Insert(index, value);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00017126 File Offset: 0x00016126
		public void Insert(int index, EncryptionProperty value)
		{
			this.m_props.Insert(index, value);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00017135 File Offset: 0x00016135
		void IList.Remove(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_props.Remove(value);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00017160 File Offset: 0x00016160
		public void Remove(EncryptionProperty value)
		{
			this.m_props.Remove(value);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001716E File Offset: 0x0001616E
		public void RemoveAt(int index)
		{
			this.m_props.RemoveAt(index);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001717C File Offset: 0x0001617C
		public bool IsFixedSize
		{
			get
			{
				return this.m_props.IsFixedSize;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00017189 File Offset: 0x00016189
		public bool IsReadOnly
		{
			get
			{
				return this.m_props.IsReadOnly;
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00017196 File Offset: 0x00016196
		public EncryptionProperty Item(int index)
		{
			return (EncryptionProperty)this.m_props[index];
		}

		// Token: 0x170000EB RID: 235
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

		// Token: 0x170000EC RID: 236
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

		// Token: 0x06000489 RID: 1161 RVA: 0x000171FB File Offset: 0x000161FB
		public void CopyTo(Array array, int index)
		{
			this.m_props.CopyTo(array, index);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001720A File Offset: 0x0001620A
		public void CopyTo(EncryptionProperty[] array, int index)
		{
			this.m_props.CopyTo(array, index);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00017219 File Offset: 0x00016219
		public object SyncRoot
		{
			get
			{
				return this.m_props.SyncRoot;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00017226 File Offset: 0x00016226
		public bool IsSynchronized
		{
			get
			{
				return this.m_props.IsSynchronized;
			}
		}

		// Token: 0x040005AF RID: 1455
		private ArrayList m_props;
	}
}

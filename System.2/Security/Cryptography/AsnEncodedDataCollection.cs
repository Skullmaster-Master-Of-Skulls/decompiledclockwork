using System;
using System.Collections;

namespace System.Security.Cryptography
{
	// Token: 0x0200044E RID: 1102
	public sealed class AsnEncodedDataCollection : ICollection, IEnumerable
	{
		// Token: 0x060028D3 RID: 10451 RVA: 0x000BAF63 File Offset: 0x000B9163
		public AsnEncodedDataCollection()
		{
			this.m_list = new ArrayList();
			this.m_oid = null;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000BAF7D File Offset: 0x000B917D
		public AsnEncodedDataCollection(AsnEncodedData asnEncodedData) : this()
		{
			this.m_list.Add(asnEncodedData);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000BAF94 File Offset: 0x000B9194
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (this.m_oid != null)
			{
				string value = this.m_oid.Value;
				string value2 = asnEncodedData.Oid.Value;
				if (value != null && value2 != null)
				{
					if (string.Compare(value, value2, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new CryptographicException(SR.GetString("Cryptography_Asn_MismatchedOidInCollection"));
					}
				}
				else if (value != null || value2 != null)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Asn_MismatchedOidInCollection"));
				}
			}
			return this.m_list.Add(asnEncodedData);
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000BB011 File Offset: 0x000B9211
		public void Remove(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.m_list.Remove(asnEncodedData);
		}

		// Token: 0x17000A09 RID: 2569
		public AsnEncodedData this[int index]
		{
			get
			{
				return (AsnEncodedData)this.m_list[index];
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000BB040 File Offset: 0x000B9240
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000BB04D File Offset: 0x000B924D
		public AsnEncodedDataEnumerator GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000BB055 File Offset: 0x000B9255
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000BB060 File Offset: 0x000B9260
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000BB0FA File Offset: 0x000B92FA
		public void CopyTo(AsnEncodedData[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x000BB104 File Offset: 0x000B9304
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000BB107 File Offset: 0x000B9307
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002284 RID: 8836
		private ArrayList m_list;

		// Token: 0x04002285 RID: 8837
		private Oid m_oid;
	}
}

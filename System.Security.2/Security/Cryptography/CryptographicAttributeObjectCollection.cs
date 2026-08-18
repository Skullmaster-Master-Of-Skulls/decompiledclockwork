using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000017 RID: 23
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CryptographicAttributeObjectCollection : ICollection, IEnumerable
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000045AF File Offset: 0x000027AF
		public CryptographicAttributeObjectCollection()
		{
			this.m_list = new ArrayList();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000045C2 File Offset: 0x000027C2
		[SecurityCritical]
		private CryptographicAttributeObjectCollection(IntPtr pCryptAttributes) : this((CAPI.CRYPT_ATTRIBUTES)Marshal.PtrToStructure(pCryptAttributes, typeof(CAPI.CRYPT_ATTRIBUTES)))
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000045DF File Offset: 0x000027DF
		[SecurityCritical]
		internal CryptographicAttributeObjectCollection(SafeLocalAllocHandle pCryptAttributes) : this(pCryptAttributes.DangerousGetHandle())
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045F0 File Offset: 0x000027F0
		[SecurityCritical]
		internal CryptographicAttributeObjectCollection(CAPI.CRYPT_ATTRIBUTES cryptAttributes)
		{
			this.m_list = new ArrayList((int)cryptAttributes.cAttr);
			for (uint num = 0U; num < cryptAttributes.cAttr; num += 1U)
			{
				IntPtr pAttribute = new IntPtr((long)cryptAttributes.rgAttr + (long)((ulong)num * (ulong)((long)Marshal.SizeOf(typeof(CAPI.CRYPT_ATTRIBUTE)))));
				this.m_list.Add(new CryptographicAttributeObject(pAttribute));
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000465D File Offset: 0x0000285D
		public CryptographicAttributeObjectCollection(CryptographicAttributeObject attribute)
		{
			this.m_list = new ArrayList();
			this.m_list.Add(attribute);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004680 File Offset: 0x00002880
		private CryptographicAttributeObjectCollection(CryptographicAttributeObjectCollection other)
		{
			this.m_list = new ArrayList(other.m_list.Count);
			foreach (object obj in other.m_list)
			{
				CryptographicAttributeObject cryptographicAttributeObject = (CryptographicAttributeObject)obj;
				CryptographicAttributeObject cryptographicAttributeObject2 = new CryptographicAttributeObject(cryptographicAttributeObject.Oid);
				foreach (AsnEncodedData asnEncodedData in cryptographicAttributeObject.Values)
				{
					cryptographicAttributeObject2.Values.Add(new AsnEncodedData(asnEncodedData.Oid, asnEncodedData.RawData));
				}
				this.m_list.Add(cryptographicAttributeObject2);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004748 File Offset: 0x00002948
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			return this.Add(new CryptographicAttributeObject(asnEncodedData));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004764 File Offset: 0x00002964
		public int Add(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			string text = null;
			if (attribute.Oid != null)
			{
				text = attribute.Oid.Value;
			}
			int i = 0;
			while (i < this.m_list.Count)
			{
				CryptographicAttributeObject cryptographicAttributeObject = (CryptographicAttributeObject)this.m_list[i];
				if (cryptographicAttributeObject.Values == attribute.Values)
				{
					throw new InvalidOperationException(SecurityResources.GetResourceString("InvalidOperation_DuplicateItemNotAllowed"));
				}
				string text2 = null;
				if (cryptographicAttributeObject.Oid != null)
				{
					text2 = cryptographicAttributeObject.Oid.Value;
				}
				if (text == null && text2 == null)
				{
					foreach (AsnEncodedData asnEncodedData in attribute.Values)
					{
						cryptographicAttributeObject.Values.Add(asnEncodedData);
					}
					return i;
				}
				if (text != null && text2 != null && string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (string.Compare(text, "1.2.840.113549.1.9.5", StringComparison.OrdinalIgnoreCase) == 0)
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Pkcs9_MultipleSigningTimeNotAllowed"));
					}
					foreach (AsnEncodedData asnEncodedData2 in attribute.Values)
					{
						cryptographicAttributeObject.Values.Add(asnEncodedData2);
					}
					return i;
				}
				else
				{
					i++;
				}
			}
			return this.m_list.Add(attribute);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004899 File Offset: 0x00002A99
		public void Remove(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			this.m_list.Remove(attribute);
		}

		// Token: 0x17000015 RID: 21
		public CryptographicAttributeObject this[int index]
		{
			get
			{
				return (CryptographicAttributeObject)this.m_list[index];
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000048C8 File Offset: 0x00002AC8
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000048D5 File Offset: 0x00002AD5
		public CryptographicAttributeObjectEnumerator GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000048D5 File Offset: 0x00002AD5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000048E0 File Offset: 0x00002AE0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000497A File Offset: 0x00002B7A
		public void CopyTo(CryptographicAttributeObject[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004984 File Offset: 0x00002B84
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004987 File Offset: 0x00002B87
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000498A File Offset: 0x00002B8A
		internal CryptographicAttributeObjectCollection DeepCopy()
		{
			return new CryptographicAttributeObjectCollection(this);
		}

		// Token: 0x0400037F RID: 895
		private ArrayList m_list;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000016 RID: 22
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CryptographicAttributeObject
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000044A9 File Offset: 0x000026A9
		private CryptographicAttributeObject()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000044B1 File Offset: 0x000026B1
		[SecurityCritical]
		internal CryptographicAttributeObject(IntPtr pAttribute) : this((CAPI.CRYPT_ATTRIBUTE)Marshal.PtrToStructure(pAttribute, typeof(CAPI.CRYPT_ATTRIBUTE)))
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000044CE File Offset: 0x000026CE
		[SecurityCritical]
		internal CryptographicAttributeObject(CAPI.CRYPT_ATTRIBUTE cryptAttribute) : this(new Oid(cryptAttribute.pszObjId), PkcsUtils.GetAsnEncodedDataCollection(cryptAttribute))
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000044E7 File Offset: 0x000026E7
		[SecurityCritical]
		internal CryptographicAttributeObject(CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE cryptAttribute) : this(new Oid(cryptAttribute.pszObjId), PkcsUtils.GetAsnEncodedDataCollection(cryptAttribute))
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004500 File Offset: 0x00002700
		internal CryptographicAttributeObject(AsnEncodedData asnEncodedData) : this(asnEncodedData.Oid, new AsnEncodedDataCollection(asnEncodedData))
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004514 File Offset: 0x00002714
		public CryptographicAttributeObject(Oid oid) : this(oid, new AsnEncodedDataCollection())
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004524 File Offset: 0x00002724
		public CryptographicAttributeObject(Oid oid, AsnEncodedDataCollection values)
		{
			this.m_oid = new Oid(oid);
			if (values == null)
			{
				this.m_values = new AsnEncodedDataCollection();
				return;
			}
			foreach (AsnEncodedData asnEncodedData in values)
			{
				if (string.Compare(asnEncodedData.Oid.Value, oid.Value, StringComparison.Ordinal) != 0)
				{
					throw new InvalidOperationException(SecurityResources.GetResourceString("InvalidOperation_DuplicateItemNotAllowed"));
				}
			}
			this.m_values = values;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000459A File Offset: 0x0000279A
		public Oid Oid
		{
			get
			{
				return new Oid(this.m_oid);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000045A7 File Offset: 0x000027A7
		public AsnEncodedDataCollection Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x0400037D RID: 893
		private Oid m_oid;

		// Token: 0x0400037E RID: 894
		private AsnEncodedDataCollection m_values;
	}
}

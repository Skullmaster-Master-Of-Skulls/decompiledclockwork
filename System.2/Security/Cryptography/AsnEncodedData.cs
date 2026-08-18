using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200044D RID: 1101
	public class AsnEncodedData
	{
		// Token: 0x060028C4 RID: 10436 RVA: 0x000BADF6 File Offset: 0x000B8FF6
		internal AsnEncodedData(Oid oid)
		{
			this.m_oid = oid;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000BAE05 File Offset: 0x000B9005
		internal AsnEncodedData(string oid, CAPIBase.CRYPTOAPI_BLOB encodedBlob) : this(oid, CAPI.BlobToByteArray(encodedBlob))
		{
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000BAE14 File Offset: 0x000B9014
		internal AsnEncodedData(Oid oid, CAPIBase.CRYPTOAPI_BLOB encodedBlob) : this(oid, CAPI.BlobToByteArray(encodedBlob))
		{
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000BAE23 File Offset: 0x000B9023
		protected AsnEncodedData()
		{
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000BAE2B File Offset: 0x000B902B
		public AsnEncodedData(byte[] rawData)
		{
			this.Reset(null, rawData);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000BAE3B File Offset: 0x000B903B
		public AsnEncodedData(string oid, byte[] rawData)
		{
			this.Reset(new Oid(oid), rawData);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000BAE50 File Offset: 0x000B9050
		public AsnEncodedData(Oid oid, byte[] rawData)
		{
			this.Reset(oid, rawData);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000BAE60 File Offset: 0x000B9060
		public AsnEncodedData(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.Reset(asnEncodedData.m_oid, asnEncodedData.m_rawData);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000BAE88 File Offset: 0x000B9088
		// (set) Token: 0x060028CD RID: 10445 RVA: 0x000BAE90 File Offset: 0x000B9090
		public Oid Oid
		{
			get
			{
				return this.m_oid;
			}
			set
			{
				if (value == null)
				{
					this.m_oid = null;
					return;
				}
				this.m_oid = new Oid(value);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x000BAEA9 File Offset: 0x000B90A9
		// (set) Token: 0x060028CF RID: 10447 RVA: 0x000BAEB1 File Offset: 0x000B90B1
		public byte[] RawData
		{
			get
			{
				return this.m_rawData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_rawData = (byte[])value.Clone();
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000BAED2 File Offset: 0x000B90D2
		public virtual void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.Reset(asnEncodedData.m_oid, asnEncodedData.m_rawData);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000BAEF4 File Offset: 0x000B90F4
		public virtual string Format(bool multiLine)
		{
			if (this.m_rawData == null || this.m_rawData.Length == 0)
			{
				return string.Empty;
			}
			string lpszStructType = string.Empty;
			if (this.m_oid != null && this.m_oid.Value != null)
			{
				lpszStructType = this.m_oid.Value;
			}
			return CAPI.CryptFormatObject(1U, multiLine ? 1U : 0U, lpszStructType, this.m_rawData);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000BAF53 File Offset: 0x000B9153
		private void Reset(Oid oid, byte[] rawData)
		{
			this.Oid = oid;
			this.RawData = rawData;
		}

		// Token: 0x04002282 RID: 8834
		internal Oid m_oid;

		// Token: 0x04002283 RID: 8835
		internal byte[] m_rawData;
	}
}

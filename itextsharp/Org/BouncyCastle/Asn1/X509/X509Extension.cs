using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000360 RID: 864
	public class X509Extension
	{
		// Token: 0x06001EEA RID: 7914 RVA: 0x000BA199 File Offset: 0x000B9199
		public X509Extension(DerBoolean critical, Asn1OctetString value)
		{
			if (critical == null)
			{
				throw new ArgumentNullException("critical");
			}
			this.critical = critical.IsTrue;
			this.value = value;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000BA1C2 File Offset: 0x000B91C2
		public X509Extension(bool critical, Asn1OctetString value)
		{
			this.critical = critical;
			this.value = value;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x000BA1D8 File Offset: 0x000B91D8
		public bool IsCritical
		{
			get
			{
				return this.critical;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x000BA1E0 File Offset: 0x000B91E0
		public Asn1OctetString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000BA1E8 File Offset: 0x000B91E8
		public override int GetHashCode()
		{
			int hashCode = this.Value.GetHashCode();
			if (!this.IsCritical)
			{
				return ~hashCode;
			}
			return hashCode;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000BA210 File Offset: 0x000B9210
		public override bool Equals(object obj)
		{
			X509Extension x509Extension = obj as X509Extension;
			return x509Extension != null && this.Value.Equals(x509Extension.Value) && this.IsCritical == x509Extension.IsCritical;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000BA24C File Offset: 0x000B924C
		public static Asn1Object ConvertValueToObject(X509Extension ext)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(ext.Value.GetOctets());
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("can't convert extension", innerException);
			}
			return result;
		}

		// Token: 0x04001560 RID: 5472
		internal bool critical;

		// Token: 0x04001561 RID: 5473
		internal Asn1OctetString value;
	}
}

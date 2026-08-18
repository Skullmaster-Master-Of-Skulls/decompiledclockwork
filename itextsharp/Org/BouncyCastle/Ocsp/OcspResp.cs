using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000236 RID: 566
	public class OcspResp
	{
		// Token: 0x0600161B RID: 5659 RVA: 0x00081A4C File Offset: 0x00080A4C
		public OcspResp(OcspResponse resp)
		{
			this.resp = resp;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00081A5B File Offset: 0x00080A5B
		public OcspResp(byte[] resp) : this(new Asn1InputStream(resp))
		{
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00081A69 File Offset: 0x00080A69
		public OcspResp(Stream inStr) : this(new Asn1InputStream(inStr))
		{
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00081A78 File Offset: 0x00080A78
		private OcspResp(Asn1InputStream aIn)
		{
			try
			{
				this.resp = OcspResponse.GetInstance(aIn.ReadObject());
			}
			catch (Exception ex)
			{
				throw new IOException("malformed response: " + ex.Message, ex);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00081AC8 File Offset: 0x00080AC8
		public int Status
		{
			get
			{
				return this.resp.ResponseStatus.Value.IntValue;
			}
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00081AE0 File Offset: 0x00080AE0
		public object GetResponseObject()
		{
			ResponseBytes responseBytes = this.resp.ResponseBytes;
			if (responseBytes == null)
			{
				return null;
			}
			if (responseBytes.ResponseType.Equals(OcspObjectIdentifiers.PkixOcspBasic))
			{
				try
				{
					return new BasicOcspResp(BasicOcspResponse.GetInstance(Asn1Object.FromByteArray(responseBytes.Response.GetOctets())));
				}
				catch (Exception ex)
				{
					throw new OcspException("problem decoding object: " + ex, ex);
				}
			}
			return responseBytes.Response;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00081B58 File Offset: 0x00080B58
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00081B68 File Offset: 0x00080B68
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			OcspResp ocspResp = obj as OcspResp;
			return ocspResp != null && this.resp.Equals(ocspResp.resp);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00081B98 File Offset: 0x00080B98
		public override int GetHashCode()
		{
			return this.resp.GetHashCode();
		}

		// Token: 0x04000F40 RID: 3904
		private OcspResponse resp;
	}
}

using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000343 RID: 835
	public class OCSPRespGenerator
	{
		// Token: 0x06001E39 RID: 7737 RVA: 0x000B5634 File Offset: 0x000B4634
		public OcspResp Generate(int status, object response)
		{
			if (response == null)
			{
				return new OcspResp(new OcspResponse(new OcspResponseStatus(status), null));
			}
			if (response is BasicOcspResp)
			{
				BasicOcspResp basicOcspResp = (BasicOcspResp)response;
				Asn1OctetString response2;
				try
				{
					response2 = new DerOctetString(basicOcspResp.GetEncoded());
				}
				catch (Exception e)
				{
					throw new OcspException("can't encode object.", e);
				}
				ResponseBytes responseBytes = new ResponseBytes(OcspObjectIdentifiers.PkixOcspBasic, response2);
				return new OcspResp(new OcspResponse(new OcspResponseStatus(status), responseBytes));
			}
			throw new OcspException("unknown response object");
		}

		// Token: 0x040014F7 RID: 5367
		public const int Successful = 0;

		// Token: 0x040014F8 RID: 5368
		public const int MalformedRequest = 1;

		// Token: 0x040014F9 RID: 5369
		public const int InternalError = 2;

		// Token: 0x040014FA RID: 5370
		public const int TryLater = 3;

		// Token: 0x040014FB RID: 5371
		public const int SigRequired = 5;

		// Token: 0x040014FC RID: 5372
		public const int Unauthorized = 6;
	}
}

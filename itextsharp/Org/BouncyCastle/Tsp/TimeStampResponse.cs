using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020004B3 RID: 1203
	public class TimeStampResponse
	{
		// Token: 0x060028A3 RID: 10403 RVA: 0x000F6613 File Offset: 0x000F5613
		public TimeStampResponse(TimeStampResp resp)
		{
			this.resp = resp;
			if (resp.TimeStampToken != null)
			{
				this.timeStampToken = new TimeStampToken(resp.TimeStampToken);
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000F663B File Offset: 0x000F563B
		public TimeStampResponse(byte[] resp) : this(TimeStampResponse.readTimeStampResp(new Asn1InputStream(resp)))
		{
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000F664E File Offset: 0x000F564E
		public TimeStampResponse(Stream input) : this(TimeStampResponse.readTimeStampResp(new Asn1InputStream(input)))
		{
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000F6664 File Offset: 0x000F5664
		private static TimeStampResp readTimeStampResp(Asn1InputStream input)
		{
			TimeStampResp instance;
			try
			{
				instance = TimeStampResp.GetInstance(input.ReadObject());
			}
			catch (ArgumentException ex)
			{
				throw new TspException("malformed timestamp response: " + ex, ex);
			}
			catch (InvalidCastException ex2)
			{
				throw new TspException("malformed timestamp response: " + ex2, ex2);
			}
			return instance;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000F66C4 File Offset: 0x000F56C4
		public int Status
		{
			get
			{
				return this.resp.Status.Status.IntValue;
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000F66DC File Offset: 0x000F56DC
		public string GetStatusString()
		{
			if (this.resp.Status.StatusString == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			PkiFreeText statusString = this.resp.Status.StatusString;
			for (int num = 0; num != statusString.Count; num++)
			{
				stringBuilder.Append(statusString[num].GetString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000F673E File Offset: 0x000F573E
		public PkiFailureInfo GetFailInfo()
		{
			if (this.resp.Status.FailInfo == null)
			{
				return null;
			}
			return new PkiFailureInfo(this.resp.Status.FailInfo);
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000F6769 File Offset: 0x000F5769
		public TimeStampToken TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000F6774 File Offset: 0x000F5774
		public void Validate(TimeStampRequest request)
		{
			TimeStampToken timeStampToken = this.TimeStampToken;
			if (timeStampToken != null)
			{
				TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
				if (request.Nonce != null && !request.Nonce.Equals(timeStampInfo.Nonce))
				{
					throw new TspValidationException("response contains wrong nonce value.");
				}
				if (this.Status != 0 && this.Status != 1)
				{
					throw new TspValidationException("time stamp token found in failed request.");
				}
				if (!Arrays.ConstantTimeAreEqual(request.GetMessageImprintDigest(), timeStampInfo.GetMessageImprintDigest()))
				{
					throw new TspValidationException("response for different message imprint digest.");
				}
				if (!timeStampInfo.MessageImprintAlgOid.Equals(request.MessageImprintAlgOid))
				{
					throw new TspValidationException("response for different message imprint algorithm.");
				}
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = timeStampToken.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificate];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = timeStampToken.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificateV2];
				if (attribute == null && attribute2 == null)
				{
					throw new TspValidationException("no signing certificate attribute present.");
				}
				if (attribute != null && attribute2 != null)
				{
					throw new TspValidationException("conflicting signing certificate attributes present.");
				}
				if (request.ReqPolicy != null && !request.ReqPolicy.Equals(timeStampInfo.Policy))
				{
					throw new TspValidationException("TSA policy wrong for request.");
				}
			}
			else if (this.Status == 0 || this.Status == 1)
			{
				throw new TspValidationException("no time stamp token found and one expected.");
			}
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000F6899 File Offset: 0x000F5899
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x04001CB2 RID: 7346
		private TimeStampResp resp;

		// Token: 0x04001CB3 RID: 7347
		private TimeStampToken timeStampToken;
	}
}

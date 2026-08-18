using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x0200006F RID: 111
	public class TimeStampResponseGenerator
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00013899 File Offset: 0x00012899
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms) : this(tokenGenerator, acceptedAlgorithms, null, null)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000138A5 File Offset: 0x000128A5
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms, IList acceptedPolicy) : this(tokenGenerator, acceptedAlgorithms, acceptedPolicy, null)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000138B1 File Offset: 0x000128B1
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms, IList acceptedPolicies, IList acceptedExtensions)
		{
			this.tokenGenerator = tokenGenerator;
			this.acceptedAlgorithms = acceptedAlgorithms;
			this.acceptedPolicies = acceptedPolicies;
			this.acceptedExtensions = acceptedExtensions;
			this.statusStrings = new Asn1EncodableVector(new Asn1Encodable[0]);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000138E8 File Offset: 0x000128E8
		private void addStatusString(string statusString)
		{
			this.statusStrings.Add(new Asn1Encodable[]
			{
				new DerUtf8String(statusString)
			});
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00013911 File Offset: 0x00012911
		private void setFailInfoField(int field)
		{
			this.failInfo |= field;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00013924 File Offset: 0x00012924
		private PkiStatusInfo getPkiStatusInfo()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger((int)this.status)
			});
			if (this.statusStrings.Count > 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new PkiFreeText(new DerSequence(this.statusStrings))
				});
			}
			if (this.failInfo != 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new TimeStampResponseGenerator.FailInfo(this.failInfo)
				});
			}
			return new PkiStatusInfo(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000139AB File Offset: 0x000129AB
		public TimeStampResponse Generate(TimeStampRequest request, BigInteger serialNumber, DateTime genTime)
		{
			return this.Generate(request, serialNumber, new DateTimeObject(genTime));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000139BC File Offset: 0x000129BC
		public TimeStampResponse Generate(TimeStampRequest request, BigInteger serialNumber, DateTimeObject genTime)
		{
			TimeStampResp resp;
			try
			{
				if (genTime == null)
				{
					throw new TspValidationException("The time source is not available.", 512);
				}
				request.Validate(this.acceptedAlgorithms, this.acceptedPolicies, this.acceptedExtensions);
				this.status = PkiStatus.Granted;
				this.addStatusString("Operation Okay");
				PkiStatusInfo pkiStatusInfo = this.getPkiStatusInfo();
				ContentInfo instance;
				try
				{
					TimeStampToken timeStampToken = this.tokenGenerator.Generate(request, serialNumber, genTime.Value);
					byte[] encoded = timeStampToken.ToCmsSignedData().GetEncoded();
					instance = ContentInfo.GetInstance(Asn1Object.FromByteArray(encoded));
				}
				catch (IOException e)
				{
					throw new TspException("Timestamp token received cannot be converted to ContentInfo", e);
				}
				resp = new TimeStampResp(pkiStatusInfo, instance);
			}
			catch (TspValidationException ex)
			{
				this.status = PkiStatus.Rejection;
				this.setFailInfoField(ex.FailureCode);
				this.addStatusString(ex.Message);
				PkiStatusInfo pkiStatusInfo2 = this.getPkiStatusInfo();
				resp = new TimeStampResp(pkiStatusInfo2, null);
			}
			TimeStampResponse result;
			try
			{
				result = new TimeStampResponse(resp);
			}
			catch (IOException)
			{
				throw new TspException("created badly formatted response!");
			}
			return result;
		}

		// Token: 0x040001F4 RID: 500
		private PkiStatus status;

		// Token: 0x040001F5 RID: 501
		private Asn1EncodableVector statusStrings;

		// Token: 0x040001F6 RID: 502
		private int failInfo;

		// Token: 0x040001F7 RID: 503
		private TimeStampTokenGenerator tokenGenerator;

		// Token: 0x040001F8 RID: 504
		private IList acceptedAlgorithms;

		// Token: 0x040001F9 RID: 505
		private IList acceptedPolicies;

		// Token: 0x040001FA RID: 506
		private IList acceptedExtensions;

		// Token: 0x02000073 RID: 115
		private class FailInfo : DerBitString
		{
			// Token: 0x060003BB RID: 955 RVA: 0x00013E39 File Offset: 0x00012E39
			internal FailInfo(int failInfoValue) : base(DerBitString.GetBytes(failInfoValue), DerBitString.GetPadBits(failInfoValue))
			{
			}
		}
	}
}

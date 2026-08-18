using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000482 RID: 1154
	public class V2TbsCertListGenerator
	{
		// Token: 0x06002722 RID: 10018 RVA: 0x000ECFC2 File Offset: 0x000EBFC2
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000ECFCB File Offset: 0x000EBFCB
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000ECFD4 File Offset: 0x000EBFD4
		public void SetThisUpdate(DerUtcTime thisUpdate)
		{
			this.thisUpdate = new Time(thisUpdate);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000ECFE2 File Offset: 0x000EBFE2
		public void SetNextUpdate(DerUtcTime nextUpdate)
		{
			this.nextUpdate = ((nextUpdate != null) ? new Time(nextUpdate) : null);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000ECFF6 File Offset: 0x000EBFF6
		public void SetThisUpdate(Time thisUpdate)
		{
			this.thisUpdate = thisUpdate;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000ECFFF File Offset: 0x000EBFFF
		public void SetNextUpdate(Time nextUpdate)
		{
			this.nextUpdate = nextUpdate;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000ED008 File Offset: 0x000EC008
		public void AddCrlEntry(Asn1Sequence crlEntry)
		{
			if (this.crlEntries == null)
			{
				this.crlEntries = new ArrayList();
			}
			this.crlEntries.Add(crlEntry);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000ED02A File Offset: 0x000EC02A
		public void AddCrlEntry(DerInteger userCertificate, DerUtcTime revocationDate, int reason)
		{
			this.AddCrlEntry(userCertificate, new Time(revocationDate), reason);
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000ED03A File Offset: 0x000EC03A
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, int reason)
		{
			this.AddCrlEntry(userCertificate, revocationDate, reason, null);
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000ED048 File Offset: 0x000EC048
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, int reason, DerGeneralizedTime invalidityDate)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			if (reason != 0)
			{
				CrlReason crlReason = new CrlReason(reason);
				try
				{
					arrayList.Add(X509Extensions.ReasonCode);
					arrayList2.Add(new X509Extension(false, new DerOctetString(crlReason.GetEncoded())));
				}
				catch (IOException arg)
				{
					throw new ArgumentException("error encoding reason: " + arg);
				}
			}
			if (invalidityDate != null)
			{
				try
				{
					arrayList.Add(X509Extensions.InvalidityDate);
					arrayList2.Add(new X509Extension(false, new DerOctetString(invalidityDate.GetEncoded())));
				}
				catch (IOException arg2)
				{
					throw new ArgumentException("error encoding invalidityDate: " + arg2);
				}
			}
			if (arrayList.Count != 0)
			{
				this.AddCrlEntry(userCertificate, revocationDate, new X509Extensions(arrayList, arrayList2));
				return;
			}
			this.AddCrlEntry(userCertificate, revocationDate, null);
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000ED120 File Offset: 0x000EC120
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, X509Extensions extensions)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				userCertificate,
				revocationDate
			});
			if (extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					extensions
				});
			}
			this.AddCrlEntry(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000ED164 File Offset: 0x000EC164
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000ED170 File Offset: 0x000EC170
		public TbsCertificateList GenerateTbsCertList()
		{
			if (this.signature == null || this.issuer == null || this.thisUpdate == null)
			{
				throw new InvalidOperationException("Not all mandatory fields set in V2 TbsCertList generator.");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.signature,
				this.issuer,
				this.thisUpdate
			});
			if (this.nextUpdate != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.nextUpdate
				});
			}
			if (this.crlEntries != null)
			{
				Asn1Sequence[] v = (Asn1Sequence[])this.crlEntries.ToArray(typeof(Asn1Sequence));
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(v)
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.extensions)
				});
			}
			return new TbsCertificateList(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04001AFC RID: 6908
		private DerInteger version = new DerInteger(1);

		// Token: 0x04001AFD RID: 6909
		private AlgorithmIdentifier signature;

		// Token: 0x04001AFE RID: 6910
		private X509Name issuer;

		// Token: 0x04001AFF RID: 6911
		private Time thisUpdate;

		// Token: 0x04001B00 RID: 6912
		private Time nextUpdate;

		// Token: 0x04001B01 RID: 6913
		private X509Extensions extensions;

		// Token: 0x04001B02 RID: 6914
		private ArrayList crlEntries;
	}
}

using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000484 RID: 1156
	public class TbsCertificateList : Asn1Encodable
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x000ED31C File Offset: 0x000EC31C
		public static TbsCertificateList GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsCertificateList.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000ED32C File Offset: 0x000EC32C
		public static TbsCertificateList GetInstance(object obj)
		{
			TbsCertificateList tbsCertificateList = obj as TbsCertificateList;
			if (obj == null || tbsCertificateList != null)
			{
				return tbsCertificateList;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsCertificateList((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000ED37C File Offset: 0x000EC37C
		internal TbsCertificateList(Asn1Sequence seq)
		{
			if (seq.Count < 3 || seq.Count > 7)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			int num = 0;
			this.seq = seq;
			if (seq[num] is DerInteger)
			{
				this.version = DerInteger.GetInstance(seq[num++]);
			}
			else
			{
				this.version = new DerInteger(0);
			}
			this.signature = AlgorithmIdentifier.GetInstance(seq[num++]);
			this.issuer = X509Name.GetInstance(seq[num++]);
			this.thisUpdate = Time.GetInstance(seq[num++]);
			if (num < seq.Count && (seq[num] is DerUtcTime || seq[num] is DerGeneralizedTime || seq[num] is Time))
			{
				this.nextUpdate = Time.GetInstance(seq[num++]);
			}
			if (num < seq.Count && !(seq[num] is DerTaggedObject))
			{
				this.revokedCertificates = Asn1Sequence.GetInstance(seq[num++]);
			}
			if (num < seq.Count && seq[num] is DerTaggedObject)
			{
				this.crlExtensions = X509Extensions.GetInstance(seq[num]);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000ED4D8 File Offset: 0x000EC4D8
		public int Version
		{
			get
			{
				return this.version.Value.IntValue + 1;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x000ED4EC File Offset: 0x000EC4EC
		public DerInteger VersionNumber
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000ED4F4 File Offset: 0x000EC4F4
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x000ED4FC File Offset: 0x000EC4FC
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000ED504 File Offset: 0x000EC504
		public Time ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x000ED50C File Offset: 0x000EC50C
		public Time NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000ED514 File Offset: 0x000EC514
		public CrlEntry[] GetRevokedCertificates()
		{
			if (this.revokedCertificates == null)
			{
				return new CrlEntry[0];
			}
			CrlEntry[] array = new CrlEntry[this.revokedCertificates.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new CrlEntry(Asn1Sequence.GetInstance(this.revokedCertificates[i]));
			}
			return array;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000ED569 File Offset: 0x000EC569
		public IEnumerable GetRevokedCertificateEnumeration()
		{
			if (this.revokedCertificates == null)
			{
				return EmptyEnumerable.Instance;
			}
			return new TbsCertificateList.RevokedCertificatesEnumeration(this.revokedCertificates);
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000ED584 File Offset: 0x000EC584
		public X509Extensions Extensions
		{
			get
			{
				return this.crlExtensions;
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000ED58C File Offset: 0x000EC58C
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04001B07 RID: 6919
		internal Asn1Sequence seq;

		// Token: 0x04001B08 RID: 6920
		internal DerInteger version;

		// Token: 0x04001B09 RID: 6921
		internal AlgorithmIdentifier signature;

		// Token: 0x04001B0A RID: 6922
		internal X509Name issuer;

		// Token: 0x04001B0B RID: 6923
		internal Time thisUpdate;

		// Token: 0x04001B0C RID: 6924
		internal Time nextUpdate;

		// Token: 0x04001B0D RID: 6925
		internal Asn1Sequence revokedCertificates;

		// Token: 0x04001B0E RID: 6926
		internal X509Extensions crlExtensions;

		// Token: 0x02000485 RID: 1157
		private class RevokedCertificatesEnumeration : IEnumerable
		{
			// Token: 0x06002741 RID: 10049 RVA: 0x000ED594 File Offset: 0x000EC594
			internal RevokedCertificatesEnumeration(IEnumerable en)
			{
				this.en = en;
			}

			// Token: 0x06002742 RID: 10050 RVA: 0x000ED5A3 File Offset: 0x000EC5A3
			public IEnumerator GetEnumerator()
			{
				return new TbsCertificateList.RevokedCertificatesEnumeration.RevokedCertificatesEnumerator(this.en.GetEnumerator());
			}

			// Token: 0x04001B0F RID: 6927
			private readonly IEnumerable en;

			// Token: 0x02000486 RID: 1158
			private class RevokedCertificatesEnumerator : IEnumerator
			{
				// Token: 0x06002743 RID: 10051 RVA: 0x000ED5B5 File Offset: 0x000EC5B5
				internal RevokedCertificatesEnumerator(IEnumerator e)
				{
					this.e = e;
				}

				// Token: 0x06002744 RID: 10052 RVA: 0x000ED5C4 File Offset: 0x000EC5C4
				public bool MoveNext()
				{
					return this.e.MoveNext();
				}

				// Token: 0x06002745 RID: 10053 RVA: 0x000ED5D1 File Offset: 0x000EC5D1
				public void Reset()
				{
					this.e.Reset();
				}

				// Token: 0x170006BE RID: 1726
				// (get) Token: 0x06002746 RID: 10054 RVA: 0x000ED5DE File Offset: 0x000EC5DE
				public object Current
				{
					get
					{
						return new CrlEntry(Asn1Sequence.GetInstance(this.e.Current));
					}
				}

				// Token: 0x04001B10 RID: 6928
				private readonly IEnumerator e;
			}
		}
	}
}

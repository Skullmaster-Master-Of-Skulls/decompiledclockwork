using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200038A RID: 906
	public class CompleteCertificateRefs : Asn1Encodable
	{
		// Token: 0x06001F8A RID: 8074 RVA: 0x000BC470 File Offset: 0x000BB470
		public static CompleteCertificateRefs GetInstance(object obj)
		{
			if (obj == null || obj is CompleteCertificateRefs)
			{
				return (CompleteCertificateRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompleteCertificateRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CompleteCertificateRefs' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000BC4C4 File Offset: 0x000BB4C4
		private CompleteCertificateRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				OtherCertID.GetInstance(asn1Encodable.ToAsn1Object());
			}
			this.otherCertIDs = seq;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000BC538 File Offset: 0x000BB538
		public CompleteCertificateRefs(params OtherCertID[] otherCertIDs)
		{
			if (otherCertIDs == null)
			{
				throw new ArgumentNullException("otherCertIDs");
			}
			this.otherCertIDs = new DerSequence(otherCertIDs);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000BC55C File Offset: 0x000BB55C
		public CompleteCertificateRefs(IEnumerable otherCertIDs)
		{
			if (otherCertIDs == null)
			{
				throw new ArgumentNullException("otherCertIDs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(otherCertIDs, typeof(OtherCertID)))
			{
				throw new ArgumentException("Must contain only 'OtherCertID' objects", "otherCertIDs");
			}
			this.otherCertIDs = new DerSequence(Asn1EncodableVector.FromEnumerable(otherCertIDs));
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000BC5B0 File Offset: 0x000BB5B0
		public OtherCertID[] GetOtherCertIDs()
		{
			OtherCertID[] array = new OtherCertID[this.otherCertIDs.Count];
			for (int i = 0; i < this.otherCertIDs.Count; i++)
			{
				array[i] = OtherCertID.GetInstance(this.otherCertIDs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000BC5FE File Offset: 0x000BB5FE
		public override Asn1Object ToAsn1Object()
		{
			return this.otherCertIDs;
		}

		// Token: 0x040015D9 RID: 5593
		private readonly Asn1Sequence otherCertIDs;
	}
}

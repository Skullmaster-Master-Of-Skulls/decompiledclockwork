using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200062C RID: 1580
	public class CompleteRevocationRefs : Asn1Encodable
	{
		// Token: 0x0600358B RID: 13707 RVA: 0x0014BD84 File Offset: 0x0014AD84
		public static CompleteRevocationRefs GetInstance(object obj)
		{
			if (obj == null || obj is CompleteRevocationRefs)
			{
				return (CompleteRevocationRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompleteRevocationRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CompleteRevocationRefs' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x0014BDD8 File Offset: 0x0014ADD8
		private CompleteRevocationRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				CrlOcspRef.GetInstance(asn1Encodable.ToAsn1Object());
			}
			this.crlOcspRefs = seq;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x0014BE4C File Offset: 0x0014AE4C
		public CompleteRevocationRefs(params CrlOcspRef[] crlOcspRefs)
		{
			if (crlOcspRefs == null)
			{
				throw new ArgumentNullException("crlOcspRefs");
			}
			this.crlOcspRefs = new DerSequence(crlOcspRefs);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x0014BE70 File Offset: 0x0014AE70
		public CompleteRevocationRefs(IEnumerable crlOcspRefs)
		{
			if (crlOcspRefs == null)
			{
				throw new ArgumentNullException("crlOcspRefs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(crlOcspRefs, typeof(CrlOcspRef)))
			{
				throw new ArgumentException("Must contain only 'CrlOcspRef' objects", "crlOcspRefs");
			}
			this.crlOcspRefs = new DerSequence(Asn1EncodableVector.FromEnumerable(crlOcspRefs));
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x0014BEC4 File Offset: 0x0014AEC4
		public CrlOcspRef[] GetCrlOcspRefs()
		{
			CrlOcspRef[] array = new CrlOcspRef[this.crlOcspRefs.Count];
			for (int i = 0; i < this.crlOcspRefs.Count; i++)
			{
				array[i] = CrlOcspRef.GetInstance(this.crlOcspRefs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x0014BF12 File Offset: 0x0014AF12
		public override Asn1Object ToAsn1Object()
		{
			return this.crlOcspRefs;
		}

		// Token: 0x040023D2 RID: 9170
		private readonly Asn1Sequence crlOcspRefs;
	}
}

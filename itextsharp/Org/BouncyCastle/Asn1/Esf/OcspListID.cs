using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x020001B9 RID: 441
	public class OcspListID : Asn1Encodable
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x0005EE40 File Offset: 0x0005DE40
		public static OcspListID GetInstance(object obj)
		{
			if (obj == null || obj is OcspListID)
			{
				return (OcspListID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspListID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspListID' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0005EE94 File Offset: 0x0005DE94
		private OcspListID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspResponses = (Asn1Sequence)seq[0].ToAsn1Object();
			foreach (object obj in this.ocspResponses)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				OcspResponsesID.GetInstance(asn1Encodable.ToAsn1Object());
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0005EF48 File Offset: 0x0005DF48
		public OcspListID(params OcspResponsesID[] ocspResponses)
		{
			if (ocspResponses == null)
			{
				throw new ArgumentNullException("ocspResponses");
			}
			this.ocspResponses = new DerSequence(ocspResponses);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0005EF6C File Offset: 0x0005DF6C
		public OcspListID(IEnumerable ocspResponses)
		{
			if (ocspResponses == null)
			{
				throw new ArgumentNullException("ocspResponses");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(ocspResponses, typeof(OcspResponsesID)))
			{
				throw new ArgumentException("Must contain only 'OcspResponsesID' objects", "ocspResponses");
			}
			this.ocspResponses = new DerSequence(Asn1EncodableVector.FromEnumerable(ocspResponses));
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0005EFC0 File Offset: 0x0005DFC0
		public OcspResponsesID[] GetOcspResponses()
		{
			OcspResponsesID[] array = new OcspResponsesID[this.ocspResponses.Count];
			for (int i = 0; i < this.ocspResponses.Count; i++)
			{
				array[i] = OcspResponsesID.GetInstance(this.ocspResponses[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0005F00E File Offset: 0x0005E00E
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.ocspResponses);
		}

		// Token: 0x04000C2F RID: 3119
		private readonly Asn1Sequence ocspResponses;
	}
}

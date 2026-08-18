using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200030B RID: 779
	public class AlgorithmIdentifier : Asn1Encodable
	{
		// Token: 0x06001C86 RID: 7302 RVA: 0x000AAEB0 File Offset: 0x000A9EB0
		public static AlgorithmIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AlgorithmIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000AAEC0 File Offset: 0x000A9EC0
		public static AlgorithmIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is AlgorithmIdentifier)
			{
				return (AlgorithmIdentifier)obj;
			}
			if (obj is DerObjectIdentifier)
			{
				return new AlgorithmIdentifier((DerObjectIdentifier)obj);
			}
			if (obj is string)
			{
				return new AlgorithmIdentifier((string)obj);
			}
			if (obj is Asn1Sequence)
			{
				return new AlgorithmIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000AAF3A File Offset: 0x000A9F3A
		public AlgorithmIdentifier(DerObjectIdentifier objectID)
		{
			this.objectID = objectID;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000AAF49 File Offset: 0x000A9F49
		public AlgorithmIdentifier(string objectID)
		{
			this.objectID = new DerObjectIdentifier(objectID);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000AAF5D File Offset: 0x000A9F5D
		public AlgorithmIdentifier(DerObjectIdentifier objectID, Asn1Encodable parameters)
		{
			this.objectID = objectID;
			this.parameters = parameters;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000AAF74 File Offset: 0x000A9F74
		internal AlgorithmIdentifier(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.objectID = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.parameters = seq[1];
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x000AAFDC File Offset: 0x000A9FDC
		public virtual DerObjectIdentifier ObjectID
		{
			get
			{
				return this.objectID;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x000AAFE4 File Offset: 0x000A9FE4
		public Asn1Encodable Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000AAFEC File Offset: 0x000A9FEC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.objectID
			});
			if (this.parameters != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.parameters
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040013A8 RID: 5032
		private readonly DerObjectIdentifier objectID;

		// Token: 0x040013A9 RID: 5033
		private readonly Asn1Encodable parameters;
	}
}

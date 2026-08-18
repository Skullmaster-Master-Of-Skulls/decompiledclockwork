using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000489 RID: 1161
	public class NameConstraints : Asn1Encodable
	{
		// Token: 0x06002752 RID: 10066 RVA: 0x000ED840 File Offset: 0x000EC840
		public static NameConstraints GetInstance(object obj)
		{
			if (obj == null || obj is NameConstraints)
			{
				return (NameConstraints)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NameConstraints((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000ED894 File Offset: 0x000EC894
		public NameConstraints(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.permitted = Asn1Sequence.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.excluded = Asn1Sequence.GetInstance(asn1TaggedObject, false);
					break;
				}
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000ED91C File Offset: 0x000EC91C
		public NameConstraints(ArrayList permitted, ArrayList excluded)
		{
			if (permitted != null)
			{
				this.permitted = this.createSequence(permitted);
			}
			if (excluded != null)
			{
				this.excluded = this.createSequence(excluded);
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000ED944 File Offset: 0x000EC944
		private DerSequence createSequence(ArrayList subtree)
		{
			GeneralSubtree[] v = (GeneralSubtree[])subtree.ToArray(typeof(GeneralSubtree));
			return new DerSequence(v);
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x000ED96D File Offset: 0x000EC96D
		public Asn1Sequence PermittedSubtrees
		{
			get
			{
				return this.permitted;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000ED975 File Offset: 0x000EC975
		public Asn1Sequence ExcludedSubtrees
		{
			get
			{
				return this.excluded;
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000ED980 File Offset: 0x000EC980
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.permitted != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.permitted)
				});
			}
			if (this.excluded != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.excluded)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001B1D RID: 6941
		private Asn1Sequence permitted;

		// Token: 0x04001B1E RID: 6942
		private Asn1Sequence excluded;
	}
}

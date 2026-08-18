using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200044F RID: 1103
	public class CrlListID : Asn1Encodable
	{
		// Token: 0x06002547 RID: 9543 RVA: 0x000E1EBC File Offset: 0x000E0EBC
		public static CrlListID GetInstance(object obj)
		{
			if (obj == null || obj is CrlListID)
			{
				return (CrlListID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlListID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlListID' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000E1F10 File Offset: 0x000E0F10
		private CrlListID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crls = (Asn1Sequence)seq[0].ToAsn1Object();
			foreach (object obj in this.crls)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				CrlValidatedID.GetInstance(asn1Encodable.ToAsn1Object());
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000E1FC4 File Offset: 0x000E0FC4
		public CrlListID(params CrlValidatedID[] crls)
		{
			if (crls == null)
			{
				throw new ArgumentNullException("crls");
			}
			this.crls = new DerSequence(crls);
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000E1FE8 File Offset: 0x000E0FE8
		public CrlListID(IEnumerable crls)
		{
			if (crls == null)
			{
				throw new ArgumentNullException("crls");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(crls, typeof(CrlValidatedID)))
			{
				throw new ArgumentException("Must contain only 'CrlValidatedID' objects", "crls");
			}
			this.crls = new DerSequence(Asn1EncodableVector.FromEnumerable(crls));
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000E203C File Offset: 0x000E103C
		public CrlValidatedID[] GetCrls()
		{
			CrlValidatedID[] array = new CrlValidatedID[this.crls.Count];
			for (int i = 0; i < this.crls.Count; i++)
			{
				array[i] = CrlValidatedID.GetInstance(this.crls[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000E208A File Offset: 0x000E108A
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.crls);
		}

		// Token: 0x04001A1F RID: 6687
		private readonly Asn1Sequence crls;
	}
}

using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x02000512 RID: 1298
	public class SemanticsInformation : Asn1Encodable
	{
		// Token: 0x06002C64 RID: 11364 RVA: 0x0010E8B4 File Offset: 0x0010D8B4
		public static SemanticsInformation GetInstance(object obj)
		{
			if (obj == null || obj is SemanticsInformation)
			{
				return (SemanticsInformation)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SemanticsInformation(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0010E908 File Offset: 0x0010D908
		public SemanticsInformation(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("no objects in SemanticsInformation");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is DerObjectIdentifier)
			{
				this.semanticsIdentifier = DerObjectIdentifier.GetInstance(obj);
				if (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
				else
				{
					obj = null;
				}
			}
			if (obj != null)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(obj);
				this.nameRegistrationAuthorities = new GeneralName[instance.Count];
				for (int i = 0; i < instance.Count; i++)
				{
					this.nameRegistrationAuthorities[i] = GeneralName.GetInstance(instance[i]);
				}
			}
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0010E9AB File Offset: 0x0010D9AB
		public SemanticsInformation(DerObjectIdentifier semanticsIdentifier, GeneralName[] generalNames)
		{
			this.semanticsIdentifier = semanticsIdentifier;
			this.nameRegistrationAuthorities = generalNames;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0010E9C1 File Offset: 0x0010D9C1
		public SemanticsInformation(DerObjectIdentifier semanticsIdentifier)
		{
			this.semanticsIdentifier = semanticsIdentifier;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0010E9D0 File Offset: 0x0010D9D0
		public SemanticsInformation(GeneralName[] generalNames)
		{
			this.nameRegistrationAuthorities = generalNames;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x0010E9DF File Offset: 0x0010D9DF
		public DerObjectIdentifier SemanticsIdentifier
		{
			get
			{
				return this.semanticsIdentifier;
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0010E9E7 File Offset: 0x0010D9E7
		public GeneralName[] GetNameRegistrationAuthorities()
		{
			return this.nameRegistrationAuthorities;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x0010E9F0 File Offset: 0x0010D9F0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.semanticsIdentifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.semanticsIdentifier
				});
			}
			if (this.nameRegistrationAuthorities != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(this.nameRegistrationAuthorities)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001E9B RID: 7835
		private readonly DerObjectIdentifier semanticsIdentifier;

		// Token: 0x04001E9C RID: 7836
		private readonly GeneralName[] nameRegistrationAuthorities;
	}
}

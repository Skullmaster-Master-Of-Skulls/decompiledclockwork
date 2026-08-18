using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x02000448 RID: 1096
	public class SmimeCapability : Asn1Encodable
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x000E11C9 File Offset: 0x000E01C9
		public SmimeCapability(Asn1Sequence seq)
		{
			this.capabilityID = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.parameters = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000E1203 File Offset: 0x000E0203
		public SmimeCapability(DerObjectIdentifier capabilityID, Asn1Encodable parameters)
		{
			if (capabilityID == null)
			{
				throw new ArgumentNullException("capabilityID");
			}
			this.capabilityID = capabilityID;
			if (parameters != null)
			{
				this.parameters = parameters.ToAsn1Object();
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000E122F File Offset: 0x000E022F
		public static SmimeCapability GetInstance(object obj)
		{
			if (obj == null || obj is SmimeCapability)
			{
				return (SmimeCapability)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SmimeCapability((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid SmimeCapability");
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000E1261 File Offset: 0x000E0261
		public DerObjectIdentifier CapabilityID
		{
			get
			{
				return this.capabilityID;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000E1269 File Offset: 0x000E0269
		public Asn1Object Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000E1274 File Offset: 0x000E0274
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.capabilityID
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

		// Token: 0x040019E1 RID: 6625
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.PreferSignedData;

		// Token: 0x040019E2 RID: 6626
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.CannotDecryptAny;

		// Token: 0x040019E3 RID: 6627
		public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = PkcsObjectIdentifiers.SmimeCapabilitiesVersions;

		// Token: 0x040019E4 RID: 6628
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x040019E5 RID: 6629
		public static readonly DerObjectIdentifier DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc;

		// Token: 0x040019E6 RID: 6630
		public static readonly DerObjectIdentifier RC2Cbc = PkcsObjectIdentifiers.RC2Cbc;

		// Token: 0x040019E7 RID: 6631
		private DerObjectIdentifier capabilityID;

		// Token: 0x040019E8 RID: 6632
		private Asn1Object parameters;
	}
}

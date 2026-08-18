using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000142 RID: 322
	public class AuthorityKeyIdentifier : Asn1Encodable
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x00040F8B File Offset: 0x0003FF8B
		public static AuthorityKeyIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AuthorityKeyIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00040F9C File Offset: 0x0003FF9C
		public static AuthorityKeyIdentifier GetInstance(object obj)
		{
			if (obj is AuthorityKeyIdentifier)
			{
				return (AuthorityKeyIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthorityKeyIdentifier((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return AuthorityKeyIdentifier.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00041004 File Offset: 0x00040004
		protected internal AuthorityKeyIdentifier(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.keyidentifier = Asn1OctetString.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.certissuer = GeneralNames.GetInstance(asn1TaggedObject, false);
					break;
				case 2:
					this.certserno = DerInteger.GetInstance(asn1TaggedObject, false);
					break;
				default:
					throw new ArgumentException("illegal tag");
				}
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000410AC File Offset: 0x000400AC
		public AuthorityKeyIdentifier(SubjectPublicKeyInfo spki)
		{
			IDigest digest = new Sha1Digest();
			byte[] array = new byte[digest.GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			digest.BlockUpdate(bytes, 0, bytes.Length);
			digest.DoFinal(array, 0);
			this.keyidentifier = new DerOctetString(array);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00041100 File Offset: 0x00040100
		public AuthorityKeyIdentifier(SubjectPublicKeyInfo spki, GeneralNames name, BigInteger serialNumber)
		{
			IDigest digest = new Sha1Digest();
			byte[] array = new byte[digest.GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			digest.BlockUpdate(bytes, 0, bytes.Length);
			digest.DoFinal(array, 0);
			this.keyidentifier = new DerOctetString(array);
			this.certissuer = name;
			this.certserno = new DerInteger(serialNumber);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00041164 File Offset: 0x00040164
		public AuthorityKeyIdentifier(GeneralNames name, BigInteger serialNumber)
		{
			this.keyidentifier = null;
			this.certissuer = GeneralNames.GetInstance(name.ToAsn1Object());
			this.certserno = new DerInteger(serialNumber);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00041190 File Offset: 0x00040190
		public AuthorityKeyIdentifier(byte[] keyIdentifier)
		{
			this.keyidentifier = new DerOctetString(keyIdentifier);
			this.certissuer = null;
			this.certserno = null;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000411B2 File Offset: 0x000401B2
		public AuthorityKeyIdentifier(byte[] keyIdentifier, GeneralNames name, BigInteger serialNumber)
		{
			this.keyidentifier = new DerOctetString(keyIdentifier);
			this.certissuer = GeneralNames.GetInstance(name.ToAsn1Object());
			this.certserno = new DerInteger(serialNumber);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000411E3 File Offset: 0x000401E3
		public byte[] GetKeyIdentifier()
		{
			if (this.keyidentifier != null)
			{
				return this.keyidentifier.GetOctets();
			}
			return null;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000411FA File Offset: 0x000401FA
		public GeneralNames AuthorityCertIssuer
		{
			get
			{
				return this.certissuer;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00041202 File Offset: 0x00040202
		public BigInteger AuthorityCertSerialNumber
		{
			get
			{
				if (this.certserno != null)
				{
					return this.certserno.Value;
				}
				return null;
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0004121C File Offset: 0x0004021C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.keyidentifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.keyidentifier)
				});
			}
			if (this.certissuer != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.certissuer)
				});
			}
			if (this.certserno != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.certserno)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000412AD File Offset: 0x000402AD
		public override string ToString()
		{
			return "AuthorityKeyIdentifier: KeyID(" + this.keyidentifier.GetOctets() + ")";
		}

		// Token: 0x04000925 RID: 2341
		internal readonly Asn1OctetString keyidentifier;

		// Token: 0x04000926 RID: 2342
		internal readonly GeneralNames certissuer;

		// Token: 0x04000927 RID: 2343
		internal readonly DerInteger certserno;
	}
}

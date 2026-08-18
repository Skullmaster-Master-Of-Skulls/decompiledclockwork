using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020005A3 RID: 1443
	public class PgpPublicKey
	{
		// Token: 0x060031B5 RID: 12725 RVA: 0x00135BA0 File Offset: 0x00134BA0
		private void Init()
		{
			IBcpgKey key = this.publicPk.Key;
			if (this.publicPk.Version <= 3)
			{
				RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)key;
				this.keyId = rsaPublicBcpgKey.Modulus.LongValue;
				try
				{
					IDigest digest = DigestUtilities.GetDigest("MD5");
					byte[] array = rsaPublicBcpgKey.Modulus.ToByteArrayUnsigned();
					digest.BlockUpdate(array, 0, array.Length);
					array = rsaPublicBcpgKey.PublicExponent.ToByteArrayUnsigned();
					digest.BlockUpdate(array, 0, array.Length);
					this.fingerprint = DigestUtilities.DoFinal(digest);
				}
				catch (Exception)
				{
					throw new IOException("can't find MD5");
				}
				this.keyStrength = rsaPublicBcpgKey.Modulus.BitLength;
				return;
			}
			byte[] encodedContents = this.publicPk.GetEncodedContents();
			try
			{
				IDigest digest2 = DigestUtilities.GetDigest("SHA1");
				digest2.Update(153);
				digest2.Update((byte)(encodedContents.Length >> 8));
				digest2.Update((byte)encodedContents.Length);
				digest2.BlockUpdate(encodedContents, 0, encodedContents.Length);
				this.fingerprint = DigestUtilities.DoFinal(digest2);
			}
			catch (Exception)
			{
				throw new IOException("can't find SHA1");
			}
			this.keyId = (long)((ulong)this.fingerprint[this.fingerprint.Length - 8] << 56 | (ulong)this.fingerprint[this.fingerprint.Length - 7] << 48 | (ulong)this.fingerprint[this.fingerprint.Length - 6] << 40 | (ulong)this.fingerprint[this.fingerprint.Length - 5] << 32 | (ulong)this.fingerprint[this.fingerprint.Length - 4] << 24 | (ulong)this.fingerprint[this.fingerprint.Length - 3] << 16 | (ulong)this.fingerprint[this.fingerprint.Length - 2] << 8 | (ulong)this.fingerprint[this.fingerprint.Length - 1]);
			if (key is RsaPublicBcpgKey)
			{
				this.keyStrength = ((RsaPublicBcpgKey)key).Modulus.BitLength;
				return;
			}
			if (key is DsaPublicBcpgKey)
			{
				this.keyStrength = ((DsaPublicBcpgKey)key).P.BitLength;
				return;
			}
			if (key is ElGamalPublicBcpgKey)
			{
				this.keyStrength = ((ElGamalPublicBcpgKey)key).P.BitLength;
			}
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x00135DD8 File Offset: 0x00134DD8
		public PgpPublicKey(PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, DateTime time)
		{
			if (pubKey.IsPrivate)
			{
				throw new ArgumentException("Expected a public key", "pubKey");
			}
			IBcpgKey key;
			if (pubKey is RsaKeyParameters)
			{
				RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)pubKey;
				key = new RsaPublicBcpgKey(rsaKeyParameters.Modulus, rsaKeyParameters.Exponent);
			}
			else if (pubKey is DsaPublicKeyParameters)
			{
				DsaPublicKeyParameters dsaPublicKeyParameters = (DsaPublicKeyParameters)pubKey;
				DsaParameters parameters = dsaPublicKeyParameters.Parameters;
				key = new DsaPublicBcpgKey(parameters.P, parameters.Q, parameters.G, dsaPublicKeyParameters.Y);
			}
			else
			{
				if (!(pubKey is ElGamalPublicKeyParameters))
				{
					throw new PgpException("unknown key class");
				}
				ElGamalPublicKeyParameters elGamalPublicKeyParameters = (ElGamalPublicKeyParameters)pubKey;
				ElGamalParameters parameters2 = elGamalPublicKeyParameters.Parameters;
				key = new ElGamalPublicBcpgKey(parameters2.P, parameters2.G, elGamalPublicKeyParameters.Y);
			}
			this.publicPk = new PublicKeyPacket(algorithm, time, key);
			this.ids = new ArrayList();
			this.idSigs = new ArrayList();
			try
			{
				this.Init();
			}
			catch (IOException exception)
			{
				throw new PgpException("exception calculating keyId", exception);
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x00135F14 File Offset: 0x00134F14
		internal PgpPublicKey(PublicKeyPacket publicPk, TrustPacket trustPk, ArrayList sigs)
		{
			this.publicPk = publicPk;
			this.trustPk = trustPk;
			this.subSigs = sigs;
			this.Init();
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x00135F70 File Offset: 0x00134F70
		internal PgpPublicKey(PgpPublicKey key, TrustPacket trust, ArrayList subSigs)
		{
			this.publicPk = key.publicPk;
			this.trustPk = trust;
			this.subSigs = subSigs;
			this.fingerprint = key.fingerprint;
			this.keyId = key.keyId;
			this.keyStrength = key.keyStrength;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00135FF0 File Offset: 0x00134FF0
		internal PgpPublicKey(PgpPublicKey pubKey)
		{
			this.publicPk = pubKey.publicPk;
			this.keySigs = new ArrayList(pubKey.keySigs);
			this.ids = new ArrayList(pubKey.ids);
			this.idTrusts = new ArrayList(pubKey.idTrusts);
			this.idSigs = new ArrayList(pubKey.idSigs.Count);
			for (int num = 0; num != pubKey.idSigs.Count; num++)
			{
				this.idSigs.Add(new ArrayList((ArrayList)pubKey.idSigs[num]));
			}
			if (pubKey.subSigs != null)
			{
				this.subSigs = new ArrayList(pubKey.subSigs.Count);
				for (int num2 = 0; num2 != pubKey.subSigs.Count; num2++)
				{
					this.subSigs.Add(pubKey.subSigs[num2]);
				}
			}
			this.fingerprint = pubKey.fingerprint;
			this.keyId = pubKey.keyId;
			this.keyStrength = pubKey.keyStrength;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0013612C File Offset: 0x0013512C
		internal PgpPublicKey(PublicKeyPacket publicPk, TrustPacket trustPk, ArrayList keySigs, ArrayList ids, ArrayList idTrusts, ArrayList idSigs)
		{
			this.publicPk = publicPk;
			this.trustPk = trustPk;
			this.keySigs = keySigs;
			this.ids = ids;
			this.idTrusts = idTrusts;
			this.idSigs = idSigs;
			this.Init();
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x001361A0 File Offset: 0x001351A0
		internal PgpPublicKey(PublicKeyPacket publicPk, ArrayList ids, ArrayList idSigs)
		{
			this.publicPk = publicPk;
			this.ids = ids;
			this.idSigs = idSigs;
			this.Init();
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x001361FA File Offset: 0x001351FA
		public int Version
		{
			get
			{
				return this.publicPk.Version;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x00136207 File Offset: 0x00135207
		public DateTime CreationTime
		{
			get
			{
				return this.publicPk.GetTime();
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x00136214 File Offset: 0x00135214
		public int ValidDays
		{
			get
			{
				if (this.publicPk.Version > 3)
				{
					return (int)(this.GetValidSeconds() / 86400L);
				}
				return this.publicPk.ValidDays;
			}
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x0013623E File Offset: 0x0013523E
		public byte[] GetTrustData()
		{
			if (this.trustPk == null)
			{
				return null;
			}
			return this.trustPk.GetLevelAndTrustAmount();
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00136258 File Offset: 0x00135258
		public long GetValidSeconds()
		{
			if (this.publicPk.Version > 3)
			{
				if (this.IsMasterKey)
				{
					for (int num = 0; num != PgpPublicKey.MasterKeyCertificationTypes.Length; num++)
					{
						long expirationTimeFromSig = this.GetExpirationTimeFromSig(true, PgpPublicKey.MasterKeyCertificationTypes[num]);
						if (expirationTimeFromSig >= 0L)
						{
							return expirationTimeFromSig;
						}
					}
				}
				else
				{
					long expirationTimeFromSig2 = this.GetExpirationTimeFromSig(false, 24);
					if (expirationTimeFromSig2 >= 0L)
					{
						return expirationTimeFromSig2;
					}
				}
				return 0L;
			}
			return (long)this.publicPk.ValidDays * 24L * 60L * 60L;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x001362D4 File Offset: 0x001352D4
		private long GetExpirationTimeFromSig(bool selfSigned, int signatureType)
		{
			foreach (object obj in this.GetSignaturesOfType(signatureType))
			{
				PgpSignature pgpSignature = (PgpSignature)obj;
				if (!selfSigned || pgpSignature.KeyId == this.KeyId)
				{
					PgpSignatureSubpacketVector hashedSubPackets = pgpSignature.GetHashedSubPackets();
					if (hashedSubPackets != null)
					{
						return hashedSubPackets.GetKeyExpirationTime();
					}
					return 0L;
				}
			}
			return -1L;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x00136358 File Offset: 0x00135358
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00136360 File Offset: 0x00135360
		public byte[] GetFingerprint()
		{
			return (byte[])this.fingerprint.Clone();
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x00136374 File Offset: 0x00135374
		public bool IsEncryptionKey
		{
			get
			{
				PublicKeyAlgorithmTag algorithm = this.publicPk.Algorithm;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					break;
				default:
					if (algorithm != PublicKeyAlgorithmTag.ElGamalEncrypt && algorithm != PublicKeyAlgorithmTag.ElGamalGeneral)
					{
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x001363AA File Offset: 0x001353AA
		public bool IsMasterKey
		{
			get
			{
				return this.subSigs == null;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x001363B5 File Offset: 0x001353B5
		public PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return this.publicPk.Algorithm;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x001363C2 File Offset: 0x001353C2
		public int BitStrength
		{
			get
			{
				return this.keyStrength;
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x001363CC File Offset: 0x001353CC
		public AsymmetricKeyParameter GetKey()
		{
			AsymmetricKeyParameter result;
			try
			{
				PublicKeyAlgorithmTag algorithm = this.publicPk.Algorithm;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
				case PublicKeyAlgorithmTag.RsaSign:
				{
					RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)this.publicPk.Key;
					result = new RsaKeyParameters(false, rsaPublicBcpgKey.Modulus, rsaPublicBcpgKey.PublicExponent);
					break;
				}
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
					{
						ElGamalPublicBcpgKey elGamalPublicBcpgKey = (ElGamalPublicBcpgKey)this.publicPk.Key;
						return new ElGamalPublicKeyParameters(elGamalPublicBcpgKey.Y, new ElGamalParameters(elGamalPublicBcpgKey.P, elGamalPublicBcpgKey.G));
					}
					case PublicKeyAlgorithmTag.Dsa:
					{
						DsaPublicBcpgKey dsaPublicBcpgKey = (DsaPublicBcpgKey)this.publicPk.Key;
						return new DsaPublicKeyParameters(dsaPublicBcpgKey.Y, new DsaParameters(dsaPublicBcpgKey.P, dsaPublicBcpgKey.Q, dsaPublicBcpgKey.G));
					}
					}
					throw new PgpException("unknown public key algorithm encountered");
				}
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception constructing public key", exception);
			}
			return result;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x001364EC File Offset: 0x001354EC
		public IEnumerable GetUserIds()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.ids)
			{
				if (obj is string)
				{
					arrayList.Add(obj);
				}
			}
			return new EnumerableProxy(arrayList);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00136558 File Offset: 0x00135558
		public IEnumerable GetUserAttributes()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.ids)
			{
				if (obj is PgpUserAttributeSubpacketVector)
				{
					arrayList.Add(obj);
				}
			}
			return new EnumerableProxy(arrayList);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x001365C4 File Offset: 0x001355C4
		public IEnumerable GetSignaturesForId(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			for (int num = 0; num != this.ids.Count; num++)
			{
				if (id.Equals(this.ids[num]))
				{
					return new EnumerableProxy((ArrayList)this.idSigs[num]);
				}
			}
			return null;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00136624 File Offset: 0x00135624
		public IEnumerable GetSignaturesForUserAttribute(PgpUserAttributeSubpacketVector userAttributes)
		{
			for (int num = 0; num != this.ids.Count; num++)
			{
				if (userAttributes.Equals(this.ids[num]))
				{
					return new EnumerableProxy((ArrayList)this.idSigs[num]);
				}
			}
			return null;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00136674 File Offset: 0x00135674
		public IEnumerable GetSignaturesOfType(int signatureType)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.GetSignatures())
			{
				PgpSignature pgpSignature = (PgpSignature)obj;
				if (pgpSignature.SignatureType == signatureType)
				{
					arrayList.Add(pgpSignature);
				}
			}
			return new EnumerableProxy(arrayList);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x001366E4 File Offset: 0x001356E4
		public IEnumerable GetSignatures()
		{
			ArrayList arrayList;
			if (this.subSigs != null)
			{
				arrayList = this.subSigs;
			}
			else
			{
				arrayList = new ArrayList(this.keySigs);
				foreach (object obj in this.idSigs)
				{
					ICollection c = (ICollection)obj;
					arrayList.AddRange(c);
				}
			}
			return new EnumerableProxy(arrayList);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00136760 File Offset: 0x00135760
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x00136780 File Offset: 0x00135780
		public void Encode(Stream outStr)
		{
			BcpgOutputStream bcpgOutputStream = BcpgOutputStream.Wrap(outStr);
			bcpgOutputStream.WritePacket(this.publicPk);
			if (this.trustPk != null)
			{
				bcpgOutputStream.WritePacket(this.trustPk);
			}
			if (this.subSigs == null)
			{
				foreach (object obj in this.keySigs)
				{
					PgpSignature pgpSignature = (PgpSignature)obj;
					pgpSignature.Encode(bcpgOutputStream);
				}
				for (int num = 0; num != this.ids.Count; num++)
				{
					if (this.ids[num] is string)
					{
						string id = (string)this.ids[num];
						bcpgOutputStream.WritePacket(new UserIdPacket(id));
					}
					else
					{
						PgpUserAttributeSubpacketVector pgpUserAttributeSubpacketVector = (PgpUserAttributeSubpacketVector)this.ids[num];
						bcpgOutputStream.WritePacket(new UserAttributePacket(pgpUserAttributeSubpacketVector.ToSubpacketArray()));
					}
					if (this.idTrusts[num] != null)
					{
						bcpgOutputStream.WritePacket((ContainedPacket)this.idTrusts[num]);
					}
					foreach (object obj2 in ((ArrayList)this.idSigs[num]))
					{
						PgpSignature pgpSignature2 = (PgpSignature)obj2;
						pgpSignature2.Encode(bcpgOutputStream);
					}
				}
				return;
			}
			foreach (object obj3 in this.subSigs)
			{
				PgpSignature pgpSignature3 = (PgpSignature)obj3;
				pgpSignature3.Encode(bcpgOutputStream);
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0013695C File Offset: 0x0013595C
		public bool IsRevoked()
		{
			int num = 0;
			bool flag = false;
			if (this.IsMasterKey)
			{
				while (!flag)
				{
					if (num >= this.keySigs.Count)
					{
						break;
					}
					if (((PgpSignature)this.keySigs[num++]).SignatureType == 32)
					{
						flag = true;
					}
				}
			}
			else
			{
				while (!flag && num < this.subSigs.Count)
				{
					if (((PgpSignature)this.subSigs[num++]).SignatureType == 40)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x001369DC File Offset: 0x001359DC
		public static PgpPublicKey AddCertification(PgpPublicKey key, string id, PgpSignature certification)
		{
			return PgpPublicKey.AddCert(key, id, certification);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x001369E6 File Offset: 0x001359E6
		public static PgpPublicKey AddCertification(PgpPublicKey key, PgpUserAttributeSubpacketVector userAttributes, PgpSignature certification)
		{
			return PgpPublicKey.AddCert(key, userAttributes, certification);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x001369F0 File Offset: 0x001359F0
		private static PgpPublicKey AddCert(PgpPublicKey key, object id, PgpSignature certification)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			IList list = null;
			for (int num = 0; num != pgpPublicKey.ids.Count; num++)
			{
				if (id.Equals(pgpPublicKey.ids[num]))
				{
					list = (IList)pgpPublicKey.idSigs[num];
				}
			}
			if (list != null)
			{
				list.Add(certification);
			}
			else
			{
				list = new ArrayList();
				list.Add(certification);
				pgpPublicKey.ids.Add(id);
				pgpPublicKey.idTrusts.Add(null);
				pgpPublicKey.idSigs.Add(list);
			}
			return pgpPublicKey;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00136A85 File Offset: 0x00135A85
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, PgpUserAttributeSubpacketVector userAttributes)
		{
			return PgpPublicKey.RemoveCert(key, userAttributes);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x00136A8E File Offset: 0x00135A8E
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, string id)
		{
			return PgpPublicKey.RemoveCert(key, id);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00136A98 File Offset: 0x00135A98
		private static PgpPublicKey RemoveCert(PgpPublicKey key, object id)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			bool flag = false;
			for (int i = 0; i < pgpPublicKey.ids.Count; i++)
			{
				if (id.Equals(pgpPublicKey.ids[i]))
				{
					flag = true;
					pgpPublicKey.ids.RemoveAt(i);
					pgpPublicKey.idTrusts.RemoveAt(i);
					pgpPublicKey.idSigs.RemoveAt(i);
				}
			}
			if (!flag)
			{
				return null;
			}
			return pgpPublicKey;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x00136B04 File Offset: 0x00135B04
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, string id, PgpSignature certification)
		{
			return PgpPublicKey.RemoveCert(key, id, certification);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00136B0E File Offset: 0x00135B0E
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, PgpUserAttributeSubpacketVector userAttributes, PgpSignature certification)
		{
			return PgpPublicKey.RemoveCert(key, userAttributes, certification);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00136B18 File Offset: 0x00135B18
		private static PgpPublicKey RemoveCert(PgpPublicKey key, object id, PgpSignature certification)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			bool flag = false;
			for (int i = 0; i < pgpPublicKey.ids.Count; i++)
			{
				if (id.Equals(pgpPublicKey.ids[i]))
				{
					ArrayList arrayList = (ArrayList)pgpPublicKey.idSigs[i];
					flag = arrayList.Contains(certification);
					if (flag)
					{
						arrayList.Remove(certification);
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return pgpPublicKey;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x00136B84 File Offset: 0x00135B84
		public static PgpPublicKey AddCertification(PgpPublicKey key, PgpSignature certification)
		{
			if (key.IsMasterKey)
			{
				if (certification.SignatureType == 40)
				{
					throw new ArgumentException("signature type incorrect for master key revocation.");
				}
			}
			else if (certification.SignatureType == 32)
			{
				throw new ArgumentException("signature type incorrect for sub-key revocation.");
			}
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			if (pgpPublicKey.subSigs != null)
			{
				pgpPublicKey.subSigs.Add(certification);
			}
			else
			{
				pgpPublicKey.keySigs.Add(certification);
			}
			return pgpPublicKey;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00136BF0 File Offset: 0x00135BF0
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, PgpSignature certification)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			ArrayList arrayList = (pgpPublicKey.subSigs != null) ? pgpPublicKey.subSigs : pgpPublicKey.keySigs;
			int num = arrayList.IndexOf(certification);
			bool flag = num >= 0;
			if (flag)
			{
				arrayList.RemoveAt(num);
			}
			else
			{
				foreach (object obj in key.GetUserIds())
				{
					string id = (string)obj;
					foreach (object obj2 in key.GetSignaturesForId(id))
					{
						if (certification == obj2)
						{
							flag = true;
							pgpPublicKey = PgpPublicKey.RemoveCertification(pgpPublicKey, id, certification);
						}
					}
				}
				if (!flag)
				{
					foreach (object obj3 in key.GetUserAttributes())
					{
						PgpUserAttributeSubpacketVector userAttributes = (PgpUserAttributeSubpacketVector)obj3;
						foreach (object obj4 in key.GetSignaturesForUserAttribute(userAttributes))
						{
							if (certification == obj4)
							{
								flag = true;
								pgpPublicKey = PgpPublicKey.RemoveCertification(pgpPublicKey, userAttributes, certification);
							}
						}
					}
				}
			}
			return pgpPublicKey;
		}

		// Token: 0x04002234 RID: 8756
		private static readonly int[] MasterKeyCertificationTypes = new int[]
		{
			19,
			18,
			17,
			16
		};

		// Token: 0x04002235 RID: 8757
		private long keyId;

		// Token: 0x04002236 RID: 8758
		private byte[] fingerprint;

		// Token: 0x04002237 RID: 8759
		private int keyStrength;

		// Token: 0x04002238 RID: 8760
		internal PublicKeyPacket publicPk;

		// Token: 0x04002239 RID: 8761
		internal TrustPacket trustPk;

		// Token: 0x0400223A RID: 8762
		internal ArrayList keySigs = new ArrayList();

		// Token: 0x0400223B RID: 8763
		internal ArrayList ids = new ArrayList();

		// Token: 0x0400223C RID: 8764
		internal ArrayList idTrusts = new ArrayList();

		// Token: 0x0400223D RID: 8765
		internal ArrayList idSigs = new ArrayList();

		// Token: 0x0400223E RID: 8766
		internal ArrayList subSigs;
	}
}

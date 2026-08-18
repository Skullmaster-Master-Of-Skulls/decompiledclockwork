using System;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020004C4 RID: 1220
	public class ECKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06002997 RID: 10647 RVA: 0x000FCFEA File Offset: 0x000FBFEA
		public ECKeyPairGenerator() : this("EC")
		{
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000FCFF7 File Offset: 0x000FBFF7
		public ECKeyPairGenerator(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			this.algorithm = this.VerifyAlgorithmName(algorithm);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000FD01C File Offset: 0x000FC01C
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is ECKeyGenerationParameters)
			{
				ECKeyGenerationParameters eckeyGenerationParameters = (ECKeyGenerationParameters)parameters;
				this.publicKeyParamSet = eckeyGenerationParameters.PublicKeyParamSet;
				this.parameters = eckeyGenerationParameters.DomainParameters;
			}
			else
			{
				int strength = parameters.Strength;
				DerObjectIdentifier oid;
				if (strength <= 239)
				{
					if (strength == 192)
					{
						oid = X9ObjectIdentifiers.Prime192v1;
						goto IL_AA;
					}
					if (strength == 224)
					{
						oid = SecObjectIdentifiers.SecP224r1;
						goto IL_AA;
					}
					if (strength == 239)
					{
						oid = X9ObjectIdentifiers.Prime239v1;
						goto IL_AA;
					}
				}
				else
				{
					if (strength == 256)
					{
						oid = X9ObjectIdentifiers.Prime256v1;
						goto IL_AA;
					}
					if (strength == 384)
					{
						oid = SecObjectIdentifiers.SecP384r1;
						goto IL_AA;
					}
					if (strength == 521)
					{
						oid = SecObjectIdentifiers.SecP521r1;
						goto IL_AA;
					}
				}
				throw new InvalidParameterException("unknown key size.");
				IL_AA:
				X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(oid);
				this.parameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
			}
			this.random = parameters.Random;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000FD110 File Offset: 0x000FC110
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			BigInteger n = this.parameters.N;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(n.BitLength, this.random);
			}
			while (bigInteger.SignValue == 0 || bigInteger.CompareTo(n) >= 0);
			ECPoint q = this.parameters.G.Multiply(bigInteger);
			if (this.publicKeyParamSet != null)
			{
				return new AsymmetricCipherKeyPair(new ECPublicKeyParameters(this.algorithm, q, this.publicKeyParamSet), new ECPrivateKeyParameters(this.algorithm, bigInteger, this.publicKeyParamSet));
			}
			return new AsymmetricCipherKeyPair(new ECPublicKeyParameters(this.algorithm, q, this.parameters), new ECPrivateKeyParameters(this.algorithm, bigInteger, this.parameters));
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000FD1BC File Offset: 0x000FC1BC
		private string VerifyAlgorithmName(string algorithm)
		{
			string text = algorithm.ToUpper(CultureInfo.InvariantCulture);
			string key;
			switch (key = text)
			{
			case "EC":
			case "ECDSA":
			case "ECDH":
			case "ECDHC":
			case "ECGOST3410":
			case "ECMQV":
				return text;
			}
			throw new ArgumentException("unrecognised algorithm: " + algorithm, "algorithm");
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000FD284 File Offset: 0x000FC284
		internal static X9ECParameters FindECCurveByOid(DerObjectIdentifier oid)
		{
			X9ECParameters byOid = X962NamedCurves.GetByOid(oid);
			if (byOid == null)
			{
				byOid = SecNamedCurves.GetByOid(oid);
				if (byOid == null)
				{
					byOid = NistNamedCurves.GetByOid(oid);
					if (byOid == null)
					{
						byOid = TeleTrusTNamedCurves.GetByOid(oid);
					}
				}
			}
			return byOid;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000FD2B8 File Offset: 0x000FC2B8
		internal static ECPublicKeyParameters GetCorrespondingPublicKey(ECPrivateKeyParameters privKey)
		{
			ECDomainParameters ecdomainParameters = privKey.Parameters;
			ECPoint q = ecdomainParameters.G.Multiply(privKey.D);
			if (privKey.PublicKeyParamSet != null)
			{
				return new ECPublicKeyParameters(privKey.AlgorithmName, q, privKey.PublicKeyParamSet);
			}
			return new ECPublicKeyParameters(privKey.AlgorithmName, q, ecdomainParameters);
		}

		// Token: 0x04001D03 RID: 7427
		private readonly string algorithm;

		// Token: 0x04001D04 RID: 7428
		private ECDomainParameters parameters;

		// Token: 0x04001D05 RID: 7429
		private DerObjectIdentifier publicKeyParamSet;

		// Token: 0x04001D06 RID: 7430
		private SecureRandom random;
	}
}

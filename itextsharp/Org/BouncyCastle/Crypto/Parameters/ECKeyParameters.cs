using System;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002F2 RID: 754
	public abstract class ECKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06001BB9 RID: 7097 RVA: 0x000A5F9B File Offset: 0x000A4F9B
		protected ECKeyParameters(string algorithm, bool isPrivate, ECDomainParameters parameters) : base(isPrivate)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.algorithm = this.VerifyAlgorithmName(algorithm);
			this.parameters = parameters;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000A5FD4 File Offset: 0x000A4FD4
		protected ECKeyParameters(string algorithm, bool isPrivate, DerObjectIdentifier publicKeyParamSet) : base(isPrivate)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			this.algorithm = this.VerifyAlgorithmName(algorithm);
			this.parameters = ECKeyParameters.LookupParameters(publicKeyParamSet);
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x000A6024 File Offset: 0x000A5024
		public string AlgorithmName
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x000A602C File Offset: 0x000A502C
		public ECDomainParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x000A6034 File Offset: 0x000A5034
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000A603C File Offset: 0x000A503C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECDomainParameters ecdomainParameters = obj as ECDomainParameters;
			return ecdomainParameters != null && this.Equals(ecdomainParameters);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000A6062 File Offset: 0x000A5062
		protected bool Equals(ECKeyParameters other)
		{
			return this.parameters.Equals(other.parameters) && base.Equals(other);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000A6080 File Offset: 0x000A5080
		public override int GetHashCode()
		{
			return this.parameters.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000A6094 File Offset: 0x000A5094
		internal ECKeyGenerationParameters CreateKeyGenerationParameters(SecureRandom random)
		{
			if (this.publicKeyParamSet != null)
			{
				return new ECKeyGenerationParameters(this.publicKeyParamSet, random);
			}
			return new ECKeyGenerationParameters(this.parameters, random);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000A60B8 File Offset: 0x000A50B8
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

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000A6180 File Offset: 0x000A5180
		internal static ECDomainParameters LookupParameters(DerObjectIdentifier publicKeyParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			ECDomainParameters ecdomainParameters = ECGost3410NamedCurves.GetByOid(publicKeyParamSet);
			if (ecdomainParameters == null)
			{
				X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(publicKeyParamSet);
				if (x9ECParameters == null)
				{
					throw new ArgumentException("OID is not a valid public key parameter set", "publicKeyParamSet");
				}
				ecdomainParameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
			}
			return ecdomainParameters;
		}

		// Token: 0x04001304 RID: 4868
		private readonly string algorithm;

		// Token: 0x04001305 RID: 4869
		private readonly ECDomainParameters parameters;

		// Token: 0x04001306 RID: 4870
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}

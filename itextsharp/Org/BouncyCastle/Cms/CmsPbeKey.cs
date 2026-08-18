using System;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000300 RID: 768
	public abstract class CmsPbeKey : ICipherParameters
	{
		// Token: 0x06001C21 RID: 7201 RVA: 0x000A88EC File Offset: 0x000A78EC
		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000A88FC File Offset: 0x000A78FC
		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000A890B File Offset: 0x000A790B
		public CmsPbeKey(char[] password, byte[] salt, int iterationCount)
		{
			this.password = (char[])password.Clone();
			this.salt = Arrays.Clone(salt);
			this.iterationCount = iterationCount;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000A8938 File Offset: 0x000A7938
		public CmsPbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm)
		{
			if (!keyDerivationAlgorithm.ObjectID.Equals(PkcsObjectIdentifiers.IdPbkdf2))
			{
				throw new ArgumentException("Unsupported key derivation algorithm: " + keyDerivationAlgorithm.ObjectID);
			}
			Pbkdf2Params instance = Pbkdf2Params.GetInstance(keyDerivationAlgorithm.Parameters.ToAsn1Object());
			this.password = (char[])password.Clone();
			this.salt = instance.GetSalt();
			this.iterationCount = instance.IterationCount.IntValue;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000A89B4 File Offset: 0x000A79B4
		~CmsPbeKey()
		{
			Array.Clear(this.password, 0, this.password.Length);
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000A89F0 File Offset: 0x000A79F0
		[Obsolete("Will be removed")]
		public string Password
		{
			get
			{
				return new string(this.password);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000A89FD File Offset: 0x000A79FD
		public byte[] Salt
		{
			get
			{
				return Arrays.Clone(this.salt);
			}
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000A8A0A File Offset: 0x000A7A0A
		[Obsolete("Use 'Salt' property instead")]
		public byte[] GetSalt()
		{
			return this.Salt;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x000A8A12 File Offset: 0x000A7A12
		public int IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000A8A1A File Offset: 0x000A7A1A
		public string Algorithm
		{
			get
			{
				return "PKCS5S2";
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000A8A21 File Offset: 0x000A7A21
		public string Format
		{
			get
			{
				return "RAW";
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000A8A28 File Offset: 0x000A7A28
		public byte[] GetEncoded()
		{
			return null;
		}

		// Token: 0x06001C2D RID: 7213
		internal abstract KeyParameter GetEncoded(string algorithmOid);

		// Token: 0x04001354 RID: 4948
		internal readonly char[] password;

		// Token: 0x04001355 RID: 4949
		internal readonly byte[] salt;

		// Token: 0x04001356 RID: 4950
		internal readonly int iterationCount;
	}
}

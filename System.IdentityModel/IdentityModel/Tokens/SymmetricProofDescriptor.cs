using System;
using System.IdentityModel.Protocols.WSTrust;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000184 RID: 388
	public class SymmetricProofDescriptor : ProofDescriptor
	{
		// Token: 0x06000CB1 RID: 3249 RVA: 0x0003B694 File Offset: 0x00039894
		public SymmetricProofDescriptor(byte[] key, EncryptingCredentials targetWrappingCredentials)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			this._keySizeInBits = key.Length;
			this._key = key;
			this._targetWrappingCredentials = targetWrappingCredentials;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0003B6C6 File Offset: 0x000398C6
		public SymmetricProofDescriptor(EncryptingCredentials targetWrappingCredentials) : this(256, targetWrappingCredentials)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003B6D4 File Offset: 0x000398D4
		public SymmetricProofDescriptor(int keySizeInBits, EncryptingCredentials targetWrappingCredentials) : this(keySizeInBits, targetWrappingCredentials, null)
		{
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003B6DF File Offset: 0x000398DF
		public SymmetricProofDescriptor(int keySizeInBits, EncryptingCredentials targetWrappingCredentials, EncryptingCredentials requestorWrappingCredentials) : this(keySizeInBits, targetWrappingCredentials, requestorWrappingCredentials, null)
		{
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003B6EC File Offset: 0x000398EC
		public SymmetricProofDescriptor(int keySizeInBits, EncryptingCredentials targetWrappingCredentials, EncryptingCredentials requestorWrappingCredentials, string encryptWith)
		{
			this._keySizeInBits = keySizeInBits;
			if (encryptWith == "http://www.w3.org/2001/04/xmlenc#des-cbc" || encryptWith == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc" || encryptWith == "http://www.w3.org/2001/04/xmlenc#kw-tripledes")
			{
				this._key = CryptoHelper.KeyGenerator.GenerateDESKey(this._keySizeInBits);
			}
			else
			{
				this._key = CryptoHelper.KeyGenerator.GenerateSymmetricKey(this._keySizeInBits);
			}
			this._requestorWrappingCredentials = requestorWrappingCredentials;
			this._targetWrappingCredentials = targetWrappingCredentials;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003B762 File Offset: 0x00039962
		public SymmetricProofDescriptor(int keySizeInBits, EncryptingCredentials targetWrappingCredentials, EncryptingCredentials requestorWrappingCredentials, byte[] sourceEntropy) : this(keySizeInBits, targetWrappingCredentials, requestorWrappingCredentials, sourceEntropy, null)
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0003B770 File Offset: 0x00039970
		public SymmetricProofDescriptor(int keySizeInBits, EncryptingCredentials targetWrappingCredentials, EncryptingCredentials requestorWrappingCredentials, byte[] sourceEntropy, string encryptWith)
		{
			if (sourceEntropy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sourceEntropy");
			}
			if (sourceEntropy.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("sourceEntropy", SR.GetString("ID2058"));
			}
			this._keySizeInBits = keySizeInBits;
			this._sourceEntropy = sourceEntropy;
			if (encryptWith == "http://www.w3.org/2001/04/xmlenc#des-cbc" || encryptWith == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc" || encryptWith == "http://www.w3.org/2001/04/xmlenc#kw-tripledes")
			{
				this._key = CryptoHelper.KeyGenerator.GenerateDESKey(this._keySizeInBits, this._sourceEntropy, out this._targetEntropy);
			}
			else
			{
				this._key = CryptoHelper.KeyGenerator.GenerateSymmetricKey(this._keySizeInBits, this._sourceEntropy, out this._targetEntropy);
			}
			this._requestorWrappingCredentials = requestorWrappingCredentials;
			this._targetWrappingCredentials = targetWrappingCredentials;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003B839 File Offset: 0x00039A39
		public byte[] GetKeyBytes()
		{
			return this._key;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003B841 File Offset: 0x00039A41
		protected EncryptingCredentials RequestorEncryptingCredentials
		{
			get
			{
				return this._requestorWrappingCredentials;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0003B849 File Offset: 0x00039A49
		protected byte[] GetSourceEntropy()
		{
			return this._sourceEntropy;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0003B851 File Offset: 0x00039A51
		protected byte[] GetTargetEntropy()
		{
			return this._targetEntropy;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0003B859 File Offset: 0x00039A59
		protected EncryptingCredentials TargetEncryptingCredentials
		{
			get
			{
				return this._targetWrappingCredentials;
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0003B864 File Offset: 0x00039A64
		public override void ApplyTo(RequestSecurityTokenResponse response)
		{
			if (response == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("response");
			}
			if (this._targetEntropy != null)
			{
				response.RequestedProofToken = new RequestedProofToken("http://schemas.microsoft.com/idfx/computedkeyalgorithm/psha1");
				response.KeySizeInBits = new int?(this._keySizeInBits);
				response.Entropy = new Entropy(this._targetEntropy, this._requestorWrappingCredentials);
				return;
			}
			response.RequestedProofToken = new RequestedProofToken(this._key, this._requestorWrappingCredentials);
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0003B8DC File Offset: 0x00039ADC
		public override SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				if (this._ski == null)
				{
					this._ski = CryptoHelper.KeyGenerator.GetSecurityKeyIdentifier(this._key, this._targetWrappingCredentials);
				}
				return this._ski;
			}
		}

		// Token: 0x04000C91 RID: 3217
		private byte[] _key;

		// Token: 0x04000C92 RID: 3218
		private int _keySizeInBits;

		// Token: 0x04000C93 RID: 3219
		private byte[] _sourceEntropy;

		// Token: 0x04000C94 RID: 3220
		private byte[] _targetEntropy;

		// Token: 0x04000C95 RID: 3221
		private SecurityKeyIdentifier _ski;

		// Token: 0x04000C96 RID: 3222
		private EncryptingCredentials _requestorWrappingCredentials;

		// Token: 0x04000C97 RID: 3223
		private EncryptingCredentials _targetWrappingCredentials;
	}
}

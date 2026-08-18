using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000210 RID: 528
	public abstract class WSTrustMessage : OpenObject
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000487DB File Offset: 0x000469DB
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x000487E3 File Offset: 0x000469E3
		public bool AllowPostdating
		{
			get
			{
				return this.allowPostdating;
			}
			set
			{
				this.allowPostdating = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000487EC File Offset: 0x000469EC
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x000487F4 File Offset: 0x000469F4
		public EndpointReference AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
			set
			{
				this.appliesTo = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000487FD File Offset: 0x000469FD
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x00048805 File Offset: 0x00046A05
		public BinaryExchange BinaryExchange
		{
			get
			{
				return this.binaryExchange;
			}
			set
			{
				this.binaryExchange = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004880E File Offset: 0x00046A0E
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x00048816 File Offset: 0x00046A16
		public string ReplyTo
		{
			get
			{
				return this.replyTo;
			}
			set
			{
				this.replyTo = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004881F File Offset: 0x00046A1F
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00048827 File Offset: 0x00046A27
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
			set
			{
				this.authenticationType = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00048830 File Offset: 0x00046A30
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00048838 File Offset: 0x00046A38
		public string CanonicalizationAlgorithm
		{
			get
			{
				return this.canonicalizationAlgorithm;
			}
			set
			{
				this.canonicalizationAlgorithm = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00048841 File Offset: 0x00046A41
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00048849 File Offset: 0x00046A49
		public string Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00048852 File Offset: 0x00046A52
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0004885A File Offset: 0x00046A5A
		public string EncryptionAlgorithm
		{
			get
			{
				return this.encryptionAlgorithm;
			}
			set
			{
				this.encryptionAlgorithm = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00048863 File Offset: 0x00046A63
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0004886B File Offset: 0x00046A6B
		public Entropy Entropy
		{
			get
			{
				return this.entropy;
			}
			set
			{
				this.entropy = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00048874 File Offset: 0x00046A74
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0004887C File Offset: 0x00046A7C
		public string EncryptWith
		{
			get
			{
				return this.issuedTokenEncryptionAlgorithm;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("IssuedTokenEncryptionAlgorithm");
				}
				this.issuedTokenEncryptionAlgorithm = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004889D File Offset: 0x00046A9D
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x000488A5 File Offset: 0x00046AA5
		public string SignWith
		{
			get
			{
				return this.issuedTokenSignatureAlgorithm;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuedTokenSignatureAlgorithm = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000488C6 File Offset: 0x00046AC6
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x000488CE File Offset: 0x00046ACE
		public int? KeySizeInBits
		{
			get
			{
				return this.keySizeInBits;
			}
			set
			{
				if (value != null && value.Value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.keySizeInBits = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000488FF File Offset: 0x00046AFF
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x00048907 File Offset: 0x00046B07
		public string KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				this.keyType = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x00048910 File Offset: 0x00046B10
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x00048918 File Offset: 0x00046B18
		public string KeyWrapAlgorithm
		{
			get
			{
				return this.keyWrapAlgorithm;
			}
			set
			{
				this.keyWrapAlgorithm = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00048921 File Offset: 0x00046B21
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x00048929 File Offset: 0x00046B29
		public Lifetime Lifetime
		{
			get
			{
				return this.lifetime;
			}
			set
			{
				this.lifetime = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00048932 File Offset: 0x00046B32
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x0004893A File Offset: 0x00046B3A
		public string RequestType
		{
			get
			{
				return this.requestType;
			}
			set
			{
				this.requestType = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00048943 File Offset: 0x00046B43
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x0004894B File Offset: 0x00046B4B
		public string SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
			set
			{
				this.signatureAlgorithm = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00048954 File Offset: 0x00046B54
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0004895C File Offset: 0x00046B5C
		public string TokenType
		{
			get
			{
				return this.tokenType;
			}
			set
			{
				this.tokenType = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00048965 File Offset: 0x00046B65
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0004896D File Offset: 0x00046B6D
		public UseKey UseKey
		{
			get
			{
				return this.useKey;
			}
			set
			{
				this.useKey = value;
			}
		}

		// Token: 0x04000EC3 RID: 3779
		private bool allowPostdating;

		// Token: 0x04000EC4 RID: 3780
		private EndpointReference appliesTo;

		// Token: 0x04000EC5 RID: 3781
		private string replyTo;

		// Token: 0x04000EC6 RID: 3782
		private string authenticationType;

		// Token: 0x04000EC7 RID: 3783
		private string canonicalizationAlgorithm;

		// Token: 0x04000EC8 RID: 3784
		private string context;

		// Token: 0x04000EC9 RID: 3785
		private string encryptionAlgorithm;

		// Token: 0x04000ECA RID: 3786
		private Entropy entropy;

		// Token: 0x04000ECB RID: 3787
		private string issuedTokenEncryptionAlgorithm;

		// Token: 0x04000ECC RID: 3788
		private string keyWrapAlgorithm;

		// Token: 0x04000ECD RID: 3789
		private string issuedTokenSignatureAlgorithm;

		// Token: 0x04000ECE RID: 3790
		private int? keySizeInBits;

		// Token: 0x04000ECF RID: 3791
		private string keyType;

		// Token: 0x04000ED0 RID: 3792
		private Lifetime lifetime;

		// Token: 0x04000ED1 RID: 3793
		private string requestType;

		// Token: 0x04000ED2 RID: 3794
		private string signatureAlgorithm;

		// Token: 0x04000ED3 RID: 3795
		private string tokenType;

		// Token: 0x04000ED4 RID: 3796
		private UseKey useKey;

		// Token: 0x04000ED5 RID: 3797
		private BinaryExchange binaryExchange;
	}
}

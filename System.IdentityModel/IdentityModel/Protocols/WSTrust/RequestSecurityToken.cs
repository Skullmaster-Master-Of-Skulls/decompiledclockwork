using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001FF RID: 511
	public class RequestSecurityToken : WSTrustMessage
	{
		// Token: 0x060010D9 RID: 4313 RVA: 0x0004761B File Offset: 0x0004581B
		public RequestSecurityToken() : this(null, null)
		{
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00047625 File Offset: 0x00045825
		public RequestSecurityToken(string requestType) : this(requestType, null)
		{
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00047630 File Offset: 0x00045830
		public RequestSecurityToken(string requestType, string keyType)
		{
			base.RequestType = requestType;
			if (keyType == "http://schemas.microsoft.com/idfx/keytype/symmetric")
			{
				base.Entropy = new Entropy(256);
				base.KeySizeInBits = new int?(256);
			}
			else if (keyType == "http://schemas.microsoft.com/idfx/keytype/bearer")
			{
				base.KeySizeInBits = new int?(0);
			}
			else if (keyType == "http://schemas.microsoft.com/idfx/keytype/asymmetric")
			{
				base.KeySizeInBits = new int?(1024);
			}
			base.KeyType = keyType;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x000476B8 File Offset: 0x000458B8
		public RequestClaimCollection Claims
		{
			get
			{
				if (this._claims == null)
				{
					this._claims = new RequestClaimCollection();
				}
				return this._claims;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000476D3 File Offset: 0x000458D3
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x000476DB File Offset: 0x000458DB
		public SecurityTokenElement Encryption
		{
			get
			{
				return this._encryption;
			}
			set
			{
				this._encryption = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x000476E4 File Offset: 0x000458E4
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x000476EC File Offset: 0x000458EC
		public string ComputedKeyAlgorithm
		{
			get
			{
				return this._computedKeyAlgorithm;
			}
			set
			{
				this._computedKeyAlgorithm = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x000476F5 File Offset: 0x000458F5
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x000476FD File Offset: 0x000458FD
		public bool? Delegatable
		{
			get
			{
				return this._delegatable;
			}
			set
			{
				this._delegatable = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00047706 File Offset: 0x00045906
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0004770E File Offset: 0x0004590E
		public SecurityTokenElement DelegateTo
		{
			get
			{
				return this._delegateTo;
			}
			set
			{
				this._delegateTo = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00047717 File Offset: 0x00045917
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0004771F File Offset: 0x0004591F
		public bool? Forwardable
		{
			get
			{
				return this._forwardable;
			}
			set
			{
				this._forwardable = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00047728 File Offset: 0x00045928
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00047730 File Offset: 0x00045930
		public SecurityTokenElement OnBehalfOf
		{
			get
			{
				return this._onBehalfOf;
			}
			set
			{
				this._onBehalfOf = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00047739 File Offset: 0x00045939
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x00047741 File Offset: 0x00045941
		public Participants Participants
		{
			get
			{
				return this._participants;
			}
			set
			{
				this._participants = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0004774A File Offset: 0x0004594A
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00047752 File Offset: 0x00045952
		public EndpointReference Issuer
		{
			get
			{
				return this._onBehalfOfIssuer;
			}
			set
			{
				this._onBehalfOfIssuer = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0004775B File Offset: 0x0004595B
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00047763 File Offset: 0x00045963
		public AdditionalContext AdditionalContext
		{
			get
			{
				return this._additionalContext;
			}
			set
			{
				this._additionalContext = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0004776C File Offset: 0x0004596C
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00047774 File Offset: 0x00045974
		public SecurityTokenElement ActAs
		{
			get
			{
				return this._actAs;
			}
			set
			{
				this._actAs = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004777D File Offset: 0x0004597D
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00047785 File Offset: 0x00045985
		public SecurityTokenElement CancelTarget
		{
			get
			{
				return this._cancelTarget;
			}
			set
			{
				this._cancelTarget = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004778E File Offset: 0x0004598E
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00047796 File Offset: 0x00045996
		public SecurityTokenElement ProofEncryption
		{
			get
			{
				return this._proofEncryption;
			}
			set
			{
				this._proofEncryption = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004779F File Offset: 0x0004599F
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x000477A7 File Offset: 0x000459A7
		public Renewing Renewing
		{
			get
			{
				return this._renewing;
			}
			set
			{
				this._renewing = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000477B0 File Offset: 0x000459B0
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x000477B8 File Offset: 0x000459B8
		public SecurityTokenElement RenewTarget
		{
			get
			{
				return this._renewTarget;
			}
			set
			{
				this._renewTarget = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000477C1 File Offset: 0x000459C1
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x000477C9 File Offset: 0x000459C9
		public RequestSecurityToken SecondaryParameters
		{
			get
			{
				return this._secondaryParameters;
			}
			set
			{
				this._secondaryParameters = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000477D2 File Offset: 0x000459D2
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x000477DA File Offset: 0x000459DA
		public SecurityTokenElement ValidateTarget
		{
			get
			{
				return this._validateTarget;
			}
			set
			{
				this._validateTarget = value;
			}
		}

		// Token: 0x04000E82 RID: 3714
		private AdditionalContext _additionalContext;

		// Token: 0x04000E83 RID: 3715
		private RequestClaimCollection _claims;

		// Token: 0x04000E84 RID: 3716
		private string _computedKeyAlgorithm;

		// Token: 0x04000E85 RID: 3717
		private Renewing _renewing;

		// Token: 0x04000E86 RID: 3718
		private SecurityTokenElement _renewTarget;

		// Token: 0x04000E87 RID: 3719
		private SecurityTokenElement _proofEncryption;

		// Token: 0x04000E88 RID: 3720
		private RequestSecurityToken _secondaryParameters;

		// Token: 0x04000E89 RID: 3721
		private SecurityTokenElement _onBehalfOf;

		// Token: 0x04000E8A RID: 3722
		private EndpointReference _onBehalfOfIssuer;

		// Token: 0x04000E8B RID: 3723
		private SecurityTokenElement _actAs;

		// Token: 0x04000E8C RID: 3724
		private SecurityTokenElement _delegateTo;

		// Token: 0x04000E8D RID: 3725
		private bool? _forwardable;

		// Token: 0x04000E8E RID: 3726
		private bool? _delegatable;

		// Token: 0x04000E8F RID: 3727
		private SecurityTokenElement _cancelTarget;

		// Token: 0x04000E90 RID: 3728
		private SecurityTokenElement _validateTarget;

		// Token: 0x04000E91 RID: 3729
		private Participants _participants;

		// Token: 0x04000E92 RID: 3730
		private SecurityTokenElement _encryption;
	}
}

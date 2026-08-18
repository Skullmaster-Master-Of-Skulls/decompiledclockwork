using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000200 RID: 512
	public class RequestSecurityTokenResponse : WSTrustMessage
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x000477E3 File Offset: 0x000459E3
		public RequestSecurityTokenResponse()
		{
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000477F4 File Offset: 0x000459F4
		public RequestSecurityTokenResponse(WSTrustMessage message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			base.RequestType = message.RequestType;
			base.Context = message.Context;
			base.KeyType = message.KeyType;
			int? keySizeInBits = message.KeySizeInBits;
			int num = 0;
			if ((keySizeInBits.GetValueOrDefault() > num & keySizeInBits != null) && StringComparer.Ordinal.Equals(message.KeyType, "http://schemas.microsoft.com/idfx/keytype/symmetric"))
			{
				base.KeySizeInBits = message.KeySizeInBits;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00047885 File Offset: 0x00045A85
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0004788D File Offset: 0x00045A8D
		public bool IsFinal
		{
			get
			{
				return this._isFinal;
			}
			set
			{
				this._isFinal = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00047896 File Offset: 0x00045A96
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x0004789E File Offset: 0x00045A9E
		public SecurityKeyIdentifierClause RequestedAttachedReference
		{
			get
			{
				return this._requestedAttachedReference;
			}
			set
			{
				this._requestedAttachedReference = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000478A7 File Offset: 0x00045AA7
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x000478AF File Offset: 0x00045AAF
		public RequestedSecurityToken RequestedSecurityToken
		{
			get
			{
				return this._requestedSecurityToken;
			}
			set
			{
				this._requestedSecurityToken = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000478B8 File Offset: 0x00045AB8
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x000478C0 File Offset: 0x00045AC0
		public RequestedProofToken RequestedProofToken
		{
			get
			{
				return this._requestedProofToken;
			}
			set
			{
				this._requestedProofToken = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x000478C9 File Offset: 0x00045AC9
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x000478D1 File Offset: 0x00045AD1
		public SecurityKeyIdentifierClause RequestedUnattachedReference
		{
			get
			{
				return this._requestedUnattachedReference;
			}
			set
			{
				this._requestedUnattachedReference = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x000478DA File Offset: 0x00045ADA
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x000478E2 File Offset: 0x00045AE2
		public bool RequestedTokenCancelled
		{
			get
			{
				return this._requestedTokenCancelled;
			}
			set
			{
				this._requestedTokenCancelled = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x000478EB File Offset: 0x00045AEB
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x000478F3 File Offset: 0x00045AF3
		public Status Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		// Token: 0x04000E93 RID: 3731
		private SecurityKeyIdentifierClause _requestedAttachedReference;

		// Token: 0x04000E94 RID: 3732
		private RequestedProofToken _requestedProofToken;

		// Token: 0x04000E95 RID: 3733
		private RequestedSecurityToken _requestedSecurityToken;

		// Token: 0x04000E96 RID: 3734
		private SecurityKeyIdentifierClause _requestedUnattachedReference;

		// Token: 0x04000E97 RID: 3735
		private bool _requestedTokenCancelled;

		// Token: 0x04000E98 RID: 3736
		private Status _status;

		// Token: 0x04000E99 RID: 3737
		private bool _isFinal = true;
	}
}

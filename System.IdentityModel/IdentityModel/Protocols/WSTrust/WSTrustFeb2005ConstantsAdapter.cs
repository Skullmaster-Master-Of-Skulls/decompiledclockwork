using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x0200020D RID: 525
	internal class WSTrustFeb2005ConstantsAdapter : WSTrustConstantsAdapter
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x000483BC File Offset: 0x000465BC
		protected WSTrustFeb2005ConstantsAdapter()
		{
			base.NamespaceURI = "http://schemas.xmlsoap.org/ws/2005/02/trust";
			base.Prefix = "t";
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000483DA File Offset: 0x000465DA
		internal static WSTrustFeb2005ConstantsAdapter Instance
		{
			get
			{
				if (WSTrustFeb2005ConstantsAdapter.instance == null)
				{
					WSTrustFeb2005ConstantsAdapter.instance = new WSTrustFeb2005ConstantsAdapter();
				}
				return WSTrustFeb2005ConstantsAdapter.instance;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x000483F2 File Offset: 0x000465F2
		internal override WSTrustConstantsAdapter.WSTrustActions Actions
		{
			get
			{
				if (WSTrustFeb2005ConstantsAdapter.trustFeb2005Actions == null)
				{
					WSTrustFeb2005ConstantsAdapter.trustFeb2005Actions = new WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005Actions();
				}
				return WSTrustFeb2005ConstantsAdapter.trustFeb2005Actions;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0004840A File Offset: 0x0004660A
		internal override WSTrustConstantsAdapter.WSTrustComputedKeyAlgorithm ComputedKeyAlgorithm
		{
			get
			{
				if (WSTrustFeb2005ConstantsAdapter.trustFeb2005ComputedKeyAlgorithm == null)
				{
					WSTrustFeb2005ConstantsAdapter.trustFeb2005ComputedKeyAlgorithm = new WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005ComputedKeyAlgorithm();
				}
				return WSTrustFeb2005ConstantsAdapter.trustFeb2005ComputedKeyAlgorithm;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00048422 File Offset: 0x00046622
		internal override WSTrustConstantsAdapter.WSTrustKeyTypes KeyTypes
		{
			get
			{
				if (WSTrustFeb2005ConstantsAdapter.trustFeb2005KeyTypes == null)
				{
					WSTrustFeb2005ConstantsAdapter.trustFeb2005KeyTypes = new WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005KeyTypes();
				}
				return WSTrustFeb2005ConstantsAdapter.trustFeb2005KeyTypes;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0004843A File Offset: 0x0004663A
		internal override WSTrustConstantsAdapter.WSTrustRequestTypes RequestTypes
		{
			get
			{
				if (WSTrustFeb2005ConstantsAdapter.trustFeb2005RequestTypes == null)
				{
					WSTrustFeb2005ConstantsAdapter.trustFeb2005RequestTypes = new WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005RequestTypes();
				}
				return WSTrustFeb2005ConstantsAdapter.trustFeb2005RequestTypes;
			}
		}

		// Token: 0x04000EBE RID: 3774
		private static WSTrustFeb2005ConstantsAdapter instance;

		// Token: 0x04000EBF RID: 3775
		private static WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005Actions trustFeb2005Actions;

		// Token: 0x04000EC0 RID: 3776
		private static WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005ComputedKeyAlgorithm trustFeb2005ComputedKeyAlgorithm;

		// Token: 0x04000EC1 RID: 3777
		private static WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005KeyTypes trustFeb2005KeyTypes;

		// Token: 0x04000EC2 RID: 3778
		private static WSTrustFeb2005ConstantsAdapter.WSTrustFeb2005RequestTypes trustFeb2005RequestTypes;

		// Token: 0x020002CA RID: 714
		internal class WSTrustFeb2005Actions : WSTrustConstantsAdapter.WSTrustActions
		{
			// Token: 0x06001441 RID: 5185 RVA: 0x00054818 File Offset: 0x00052A18
			internal WSTrustFeb2005Actions()
			{
				base.Cancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel";
				base.CancelResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel";
				base.Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";
				base.IssueResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";
				base.Renew = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew";
				base.RenewResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew";
				base.RequestSecurityContextToken = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT";
				base.RequestSecurityContextTokenCancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT-Cancel";
				base.RequestSecurityContextTokenResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT";
				base.RequestSecurityContextTokenResponseCancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT-Cancel";
				base.Validate = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate";
				base.ValidateResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate";
			}
		}

		// Token: 0x020002CB RID: 715
		internal class WSTrustFeb2005ComputedKeyAlgorithm : WSTrustConstantsAdapter.WSTrustComputedKeyAlgorithm
		{
			// Token: 0x06001442 RID: 5186 RVA: 0x000548AF File Offset: 0x00052AAF
			internal WSTrustFeb2005ComputedKeyAlgorithm()
			{
				base.Psha1 = "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";
			}
		}

		// Token: 0x020002CC RID: 716
		internal class WSTrustFeb2005KeyTypes : WSTrustConstantsAdapter.WSTrustKeyTypes
		{
			// Token: 0x06001443 RID: 5187 RVA: 0x000548C2 File Offset: 0x00052AC2
			internal WSTrustFeb2005KeyTypes()
			{
				base.Asymmetric = "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";
				base.Bearer = "http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey";
				base.Symmetric = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";
			}
		}

		// Token: 0x020002CD RID: 717
		internal class WSTrustFeb2005RequestTypes : WSTrustConstantsAdapter.WSTrustRequestTypes
		{
			// Token: 0x06001444 RID: 5188 RVA: 0x000548EB File Offset: 0x00052AEB
			internal WSTrustFeb2005RequestTypes()
			{
				base.Cancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel";
				base.Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";
				base.Renew = "http://schemas.xmlsoap.org/ws/2005/02/trust/Renew";
				base.Validate = "http://schemas.xmlsoap.org/ws/2005/02/trust/Validate";
			}
		}
	}
}

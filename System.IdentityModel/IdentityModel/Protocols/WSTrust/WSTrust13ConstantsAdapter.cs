using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000207 RID: 519
	internal class WSTrust13ConstantsAdapter : WSTrustConstantsAdapter
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x0004799E File Offset: 0x00045B9E
		protected WSTrust13ConstantsAdapter()
		{
			base.NamespaceURI = "http://docs.oasis-open.org/ws-sx/ws-trust/200512";
			base.Prefix = "trust";
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x000479BC File Offset: 0x00045BBC
		internal static WSTrust13ConstantsAdapter Instance
		{
			get
			{
				if (WSTrust13ConstantsAdapter.instance == null)
				{
					WSTrust13ConstantsAdapter.instance = new WSTrust13ConstantsAdapter();
				}
				return WSTrust13ConstantsAdapter.instance;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x000479D4 File Offset: 0x00045BD4
		internal override WSTrustConstantsAdapter.WSTrustActions Actions
		{
			get
			{
				if (WSTrust13ConstantsAdapter.trust13ActionNames == null)
				{
					WSTrust13ConstantsAdapter.trust13ActionNames = new WSTrust13ConstantsAdapter.WSTrust13Actions();
				}
				return WSTrust13ConstantsAdapter.trust13ActionNames;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000479EC File Offset: 0x00045BEC
		internal override WSTrustConstantsAdapter.WSTrustComputedKeyAlgorithm ComputedKeyAlgorithm
		{
			get
			{
				if (WSTrust13ConstantsAdapter.trust13ComputedKeyAlgorithm == null)
				{
					WSTrust13ConstantsAdapter.trust13ComputedKeyAlgorithm = new WSTrust13ConstantsAdapter.WSTrust13ComputedKeyAlgorithm();
				}
				return WSTrust13ConstantsAdapter.trust13ComputedKeyAlgorithm;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00047A04 File Offset: 0x00045C04
		internal override WSTrustConstantsAdapter.WSTrustElementNames Elements
		{
			get
			{
				if (WSTrust13ConstantsAdapter.trust13ElementNames == null)
				{
					WSTrust13ConstantsAdapter.trust13ElementNames = new WSTrust13ConstantsAdapter.WSTrust13ElementNames();
				}
				return WSTrust13ConstantsAdapter.trust13ElementNames;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00047A1C File Offset: 0x00045C1C
		internal override WSTrustConstantsAdapter.WSTrustKeyTypes KeyTypes
		{
			get
			{
				if (WSTrust13ConstantsAdapter.trust13KeyTypes == null)
				{
					WSTrust13ConstantsAdapter.trust13KeyTypes = new WSTrust13ConstantsAdapter.WSTrust13KeyTypes();
				}
				return WSTrust13ConstantsAdapter.trust13KeyTypes;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00047A34 File Offset: 0x00045C34
		internal override WSTrustConstantsAdapter.WSTrustRequestTypes RequestTypes
		{
			get
			{
				if (WSTrust13ConstantsAdapter.trust13RequestTypes == null)
				{
					WSTrust13ConstantsAdapter.trust13RequestTypes = new WSTrust13ConstantsAdapter.WSTrust13RequestTypes();
				}
				return WSTrust13ConstantsAdapter.trust13RequestTypes;
			}
		}

		// Token: 0x04000EAD RID: 3757
		private static WSTrust13ConstantsAdapter instance;

		// Token: 0x04000EAE RID: 3758
		private static WSTrust13ConstantsAdapter.WSTrust13ElementNames trust13ElementNames;

		// Token: 0x04000EAF RID: 3759
		private static WSTrust13ConstantsAdapter.WSTrust13Actions trust13ActionNames;

		// Token: 0x04000EB0 RID: 3760
		private static WSTrust13ConstantsAdapter.WSTrust13ComputedKeyAlgorithm trust13ComputedKeyAlgorithm;

		// Token: 0x04000EB1 RID: 3761
		private static WSTrust13ConstantsAdapter.WSTrust13KeyTypes trust13KeyTypes;

		// Token: 0x04000EB2 RID: 3762
		private static WSTrust13ConstantsAdapter.WSTrust13RequestTypes trust13RequestTypes;

		// Token: 0x020002B6 RID: 694
		internal class WSTrust13ElementNames : WSTrustConstantsAdapter.WSTrustElementNames
		{
			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x060013CB RID: 5067 RVA: 0x00054126 File Offset: 0x00052326
			internal string KeyWrapAlgorithm
			{
				get
				{
					return this.keyWrapAlgorithm;
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x060013CC RID: 5068 RVA: 0x0005412E File Offset: 0x0005232E
			internal string SecondaryParamters
			{
				get
				{
					return this.secondaryParameters;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x060013CD RID: 5069 RVA: 0x00054136 File Offset: 0x00052336
			internal string RequestSecurityTokenResponseCollection
			{
				get
				{
					return this.requestSecurityTokenResponseCollection;
				}
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x060013CE RID: 5070 RVA: 0x0005413E File Offset: 0x0005233E
			internal string ValidateTarget
			{
				get
				{
					return this.validateTarget;
				}
			}

			// Token: 0x040011CF RID: 4559
			private string keyWrapAlgorithm = "KeyWrapAlgorithm";

			// Token: 0x040011D0 RID: 4560
			private string secondaryParameters = "SecondaryParameters";

			// Token: 0x040011D1 RID: 4561
			private string requestSecurityTokenResponseCollection = "RequestSecurityTokenResponseCollection";

			// Token: 0x040011D2 RID: 4562
			private string validateTarget = "ValidateTarget";
		}

		// Token: 0x020002B7 RID: 695
		internal class WSTrust13Actions : WSTrustConstantsAdapter.WSTrustActions
		{
			// Token: 0x060013D0 RID: 5072 RVA: 0x0005417C File Offset: 0x0005237C
			internal WSTrust13Actions()
			{
				base.Cancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel";
				base.CancelResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel";
				base.Issue = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";
				base.IssueResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue";
				base.Renew = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew";
				base.RenewResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew";
				base.RequestSecurityContextToken = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT";
				base.RequestSecurityContextTokenCancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT-Cancel";
				base.RequestSecurityContextTokenResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT";
				base.RequestSecurityContextTokenResponseCancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT-Cancel";
				base.Validate = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate";
				base.ValidateResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate";
			}
		}

		// Token: 0x020002B8 RID: 696
		internal class WSTrust13ComputedKeyAlgorithm : WSTrustConstantsAdapter.WSTrustComputedKeyAlgorithm
		{
			// Token: 0x060013D1 RID: 5073 RVA: 0x00054213 File Offset: 0x00052413
			internal WSTrust13ComputedKeyAlgorithm()
			{
				base.Psha1 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1";
			}
		}

		// Token: 0x020002B9 RID: 697
		internal class WSTrust13KeyTypes : WSTrustConstantsAdapter.WSTrustKeyTypes
		{
			// Token: 0x060013D2 RID: 5074 RVA: 0x00054226 File Offset: 0x00052426
			internal WSTrust13KeyTypes()
			{
				base.Asymmetric = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey";
				base.Bearer = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";
				base.Symmetric = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey";
			}
		}

		// Token: 0x020002BA RID: 698
		internal class WSTrust13RequestTypes : WSTrustConstantsAdapter.WSTrustRequestTypes
		{
			// Token: 0x060013D3 RID: 5075 RVA: 0x0005424F File Offset: 0x0005244F
			internal WSTrust13RequestTypes()
			{
				base.Cancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel";
				base.Issue = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";
				base.Renew = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew";
				base.Validate = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Validate";
			}
		}
	}
}

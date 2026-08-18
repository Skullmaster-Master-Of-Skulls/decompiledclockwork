using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000120 RID: 288
	internal class EndpointAuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00021289 File Offset: 0x0001F489
		public EndpointAuthorizationPolicy(string endpointId)
		{
			if (endpointId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointId");
			}
			this._endpointId = endpointId;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x000212B6 File Offset: 0x0001F4B6
		public string EndpointId
		{
			get
			{
				return this._endpointId;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00002434 File Offset: 0x00000634
		bool IAuthorizationPolicy.Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			return true;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00003459 File Offset: 0x00001659
		ClaimSet IAuthorizationPolicy.Issuer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x000212BE File Offset: 0x0001F4BE
		string IAuthorizationComponent.Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x04000AE3 RID: 2787
		private string _endpointId;

		// Token: 0x04000AE4 RID: 2788
		private string _id = UniqueId.CreateUniqueId();
	}
}

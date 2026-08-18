using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000155 RID: 341
	[DataContract]
	public class SamlAuthorizationDecisionClaimResource
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0002F136 File Offset: 0x0002D336
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (string.IsNullOrEmpty(this.resource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("resource");
			}
			if (string.IsNullOrEmpty(this.actionName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("actionName");
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002F174 File Offset: 0x0002D374
		public SamlAuthorizationDecisionClaimResource(string resource, SamlAccessDecision accessDecision, string actionNamespace, string actionName)
		{
			if (string.IsNullOrEmpty(resource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("resource");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("actionName");
			}
			this.resource = resource;
			this.accessDecision = accessDecision;
			this.actionNamespace = actionNamespace;
			this.actionName = actionName;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002F1D5 File Offset: 0x0002D3D5
		public string Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002F1DD File Offset: 0x0002D3DD
		public SamlAccessDecision AccessDecision
		{
			get
			{
				return this.accessDecision;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002F1E5 File Offset: 0x0002D3E5
		public string ActionNamespace
		{
			get
			{
				return this.actionNamespace;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002F1ED File Offset: 0x0002D3ED
		public string ActionName
		{
			get
			{
				return this.actionName;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002F1F8 File Offset: 0x0002D3F8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			SamlAuthorizationDecisionClaimResource samlAuthorizationDecisionClaimResource = obj as SamlAuthorizationDecisionClaimResource;
			return samlAuthorizationDecisionClaimResource != null && (this.ActionName == samlAuthorizationDecisionClaimResource.ActionName && this.ActionNamespace == samlAuthorizationDecisionClaimResource.ActionNamespace && this.Resource == samlAuthorizationDecisionClaimResource.Resource) && this.AccessDecision == samlAuthorizationDecisionClaimResource.AccessDecision;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002F265 File Offset: 0x0002D465
		public override int GetHashCode()
		{
			return this.resource.GetHashCode() ^ this.accessDecision.GetHashCode();
		}

		// Token: 0x04000BB8 RID: 3000
		[DataMember]
		private string resource;

		// Token: 0x04000BB9 RID: 3001
		[DataMember]
		private SamlAccessDecision accessDecision;

		// Token: 0x04000BBA RID: 3002
		[DataMember]
		private string actionNamespace;

		// Token: 0x04000BBB RID: 3003
		[DataMember]
		private string actionName;
	}
}

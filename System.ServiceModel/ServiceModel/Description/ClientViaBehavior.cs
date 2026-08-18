using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003FA RID: 1018
	public class ClientViaBehavior : IEndpointBehavior
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x0008AC0A File Offset: 0x00088E0A
		public ClientViaBehavior(Uri uri)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			this.uri = uri;
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x0008AC32 File Offset: 0x00088E32
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x0008AC3A File Offset: 0x00088E3A
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.uri = value;
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0008AC5C File Offset: 0x00088E5C
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0008AC5E File Offset: 0x00088E5E
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0008AC60 File Offset: 0x00088E60
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFXEndpointBehaviorUsedOnWrongSide", new object[]
			{
				typeof(ClientViaBehavior).Name
			})));
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0008AC93 File Offset: 0x00088E93
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			if (behavior == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("behavior");
			}
			behavior.Via = this.Uri;
		}

		// Token: 0x040021A6 RID: 8614
		private Uri uri;
	}
}

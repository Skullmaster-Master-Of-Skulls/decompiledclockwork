using System;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C2 RID: 450
	[ServiceContract(Name = "IPeerResolverContract", Namespace = "http://schemas.microsoft.com/net/2006/05/peer/resolver", SessionMode = SessionMode.Allowed)]
	public interface IPeerResolverContract
	{
		// Token: 0x06000EBC RID: 3772
		[OperationContract(IsOneWay = false, Name = "Register", Action = "http://schemas.microsoft.com/net/2006/05/peer/resolver/Register", ReplyAction = "http://schemas.microsoft.com/net/2006/05/peer/resolver/RegisterResponse")]
		RegisterResponseInfo Register(RegisterInfo registerInfo);

		// Token: 0x06000EBD RID: 3773
		[OperationContract(IsOneWay = false, Name = "Update", Action = "http://schemas.microsoft.com/net/2006/05/peer/resolver/Update", ReplyAction = "http://schemas.microsoft.com/net/2006/05/peer/resolver/UpdateResponse")]
		RegisterResponseInfo Update(UpdateInfo updateInfo);

		// Token: 0x06000EBE RID: 3774
		[OperationContract(IsOneWay = false, Name = "Resolve", Action = "http://schemas.microsoft.com/net/2006/05/peer/resolver/Resolve", ReplyAction = "http://schemas.microsoft.com/net/2006/05/peer/resolver/ResolveResponse")]
		ResolveResponseInfo Resolve(ResolveInfo resolveInfo);

		// Token: 0x06000EBF RID: 3775
		[OperationContract(IsOneWay = false, Name = "Unregister", Action = "http://schemas.microsoft.com/net/2006/05/peer/resolver/Unregister")]
		void Unregister(UnregisterInfo unregisterInfo);

		// Token: 0x06000EC0 RID: 3776
		[OperationContract(IsOneWay = false, Name = "Refresh", Action = "http://schemas.microsoft.com/net/2006/05/peer/resolver/Refresh", ReplyAction = "http://schemas.microsoft.com/net/2006/05/peer/resolver/RefreshResponse")]
		RefreshResponseInfo Refresh(RefreshInfo refreshInfo);

		// Token: 0x06000EC1 RID: 3777
		[OperationContract(IsOneWay = false, Name = "GetServiceInfo", Action = "http://schemas.microsoft.com/net/2006/05/peer/resolver/GetServiceSettings", ReplyAction = "http://schemas.microsoft.com/net/2006/05/peer/resolver/GetServiceSettingsResponse")]
		ServiceSettingsResponseInfo GetServiceSettings();
	}
}

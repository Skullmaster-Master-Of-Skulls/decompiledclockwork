using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Security
{
	// Token: 0x0200027D RID: 637
	internal class GetTokenUIAsyncResult : AsyncResult
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x00043321 File Offset: 0x00041521
		internal GetTokenUIAsyncResult(Binding binding, IClientChannel channel, ClientCredentials credentials, AsyncCallback callback, object state) : base(callback, state)
		{
			this.credentials = credentials;
			this.proxy = channel;
			this.binding = binding;
			this.CallBegin(true);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004334C File Offset: 0x0004154C
		private void CallBegin(bool completedSynchronously)
		{
			IAsyncResult asyncResult = null;
			Exception ex = null;
			try
			{
				SecurityTokenManager securityTokenManager = this.credentials.CreateSecurityTokenManager();
				CardSpacePolicyElement[] chain;
				this.requiresInfoCard = InfoCardHelper.IsInfocardRequired(this.binding, this.credentials, securityTokenManager, this.proxy.RemoteAddress, out chain, out this.relyingPartyIssuer);
				MessageSecurityVersion bindingSecurityVersionOrDefault = InfoCardHelper.GetBindingSecurityVersionOrDefault(this.binding);
				WSSecurityTokenSerializer defaultInstance = WSSecurityTokenSerializer.DefaultInstance;
				asyncResult = this.credentials.GetInfoCardTokenCallback.BeginInvoke(this.requiresInfoCard, chain, securityTokenManager.CreateSecurityTokenSerializer(bindingSecurityVersionOrDefault.SecurityTokenVersion), GetTokenUIAsyncResult.callback, this);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex == null)
			{
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this.CallEnd(asyncResult, out ex);
			}
			if (ex != null)
			{
				return;
			}
			this.CallComplete(completedSynchronously, null);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00043418 File Offset: 0x00041618
		private static void Callback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			GetTokenUIAsyncResult getTokenUIAsyncResult = (GetTokenUIAsyncResult)result.AsyncState;
			Exception exception = null;
			getTokenUIAsyncResult.CallEnd(result, out exception);
			getTokenUIAsyncResult.CallComplete(false, exception);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00043450 File Offset: 0x00041650
		private void CallEnd(IAsyncResult result, out Exception exception)
		{
			try
			{
				SecurityToken token = this.credentials.GetInfoCardTokenCallback.EndInvoke(result);
				ChannelParameterCollection property = this.proxy.GetProperty<ChannelParameterCollection>();
				if (property != null)
				{
					property.Add(new InfoCardChannelParameter(token, this.relyingPartyIssuer, this.requiresInfoCard));
				}
				exception = null;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000434BC File Offset: 0x000416BC
		private void CallComplete(bool completedSynchronously, Exception exception)
		{
			base.Complete(completedSynchronously, exception);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000434C6 File Offset: 0x000416C6
		internal static void End(IAsyncResult result)
		{
			AsyncResult.End<GetTokenUIAsyncResult>(result);
		}

		// Token: 0x040019D5 RID: 6613
		private IClientChannel proxy;

		// Token: 0x040019D6 RID: 6614
		private ClientCredentials credentials;

		// Token: 0x040019D7 RID: 6615
		private Uri relyingPartyIssuer;

		// Token: 0x040019D8 RID: 6616
		private bool requiresInfoCard;

		// Token: 0x040019D9 RID: 6617
		private Binding binding;

		// Token: 0x040019DA RID: 6618
		private static AsyncCallback callback = Fx.ThunkCallback(new AsyncCallback(GetTokenUIAsyncResult.Callback));
	}
}

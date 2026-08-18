using System;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200029F RID: 671
	internal sealed class ImpersonatingMessage : Message
	{
		// Token: 0x06001455 RID: 5205 RVA: 0x0004C515 File Offset: 0x0004A715
		public ImpersonatingMessage(Message innerMessage)
		{
			if (innerMessage == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerMessage");
			}
			this.innerMessage = innerMessage;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0004C537 File Offset: 0x0004A737
		public override bool IsEmpty
		{
			get
			{
				return this.innerMessage.IsEmpty;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0004C544 File Offset: 0x0004A744
		public override bool IsFault
		{
			get
			{
				return this.innerMessage.IsFault;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0004C551 File Offset: 0x0004A751
		public override MessageHeaders Headers
		{
			get
			{
				return this.innerMessage.Headers;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0004C55E File Offset: 0x0004A75E
		public override MessageProperties Properties
		{
			get
			{
				return this.innerMessage.Properties;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0004C56B File Offset: 0x0004A76B
		public override MessageVersion Version
		{
			get
			{
				return this.innerMessage.Version;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0004C578 File Offset: 0x0004A778
		internal override RecycledMessageState RecycledMessageState
		{
			get
			{
				return this.innerMessage.RecycledMessageState;
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004C585 File Offset: 0x0004A785
		protected override void OnClose()
		{
			base.OnClose();
			this.innerMessage.Close();
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0004C598 File Offset: 0x0004A798
		protected override IAsyncResult OnBeginWriteMessage(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			ImpersonateOnSerializingReplyMessageProperty impersonateOnSerializingReplyMessageProperty = null;
			IDisposable impersonationContext = null;
			IPrincipal originalPrincipal = null;
			bool isThreadPrincipalSet = false;
			if (!ImpersonateOnSerializingReplyMessageProperty.TryGet(this.innerMessage, out impersonateOnSerializingReplyMessageProperty))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToImpersonateWhileSerializingReponse")));
			}
			IAsyncResult result;
			try
			{
				impersonateOnSerializingReplyMessageProperty.StartImpersonation(out impersonationContext, out originalPrincipal, out isThreadPrincipalSet);
				result = this.innerMessage.BeginWriteMessage(writer, callback, state);
			}
			finally
			{
				try
				{
					impersonateOnSerializingReplyMessageProperty.StopImpersonation(impersonationContext, originalPrincipal, isThreadPrincipalSet);
				}
				catch
				{
					string message = null;
					try
					{
						message = SR.GetString("SFxRevertImpersonationFailed0");
					}
					finally
					{
						DiagnosticUtility.FailFast(message);
					}
				}
			}
			return result;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0004C64C File Offset: 0x0004A84C
		protected override void OnWriteMessage(XmlDictionaryWriter writer)
		{
			this.ImpersonateCall(delegate
			{
				this.innerMessage.WriteMessage(writer);
			});
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004C67F File Offset: 0x0004A87F
		protected override void OnEndWriteMessage(IAsyncResult result)
		{
			this.innerMessage.EndWriteMessage(result);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004C68D File Offset: 0x0004A88D
		protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteStartEnvelope(writer);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004C69B File Offset: 0x0004A89B
		protected override void OnWriteStartHeaders(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteStartHeaders(writer);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0004C6A9 File Offset: 0x0004A8A9
		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteStartBody(writer);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004C6B7 File Offset: 0x0004A8B7
		protected override string OnGetBodyAttribute(string localName, string ns)
		{
			return this.innerMessage.GetBodyAttribute(localName, ns);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004C6C6 File Offset: 0x0004A8C6
		protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
		{
			return this.innerMessage.CreateBufferedCopy(maxBufferSize);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004C6D4 File Offset: 0x0004A8D4
		protected override IAsyncResult OnBeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			ImpersonateOnSerializingReplyMessageProperty impersonateOnSerializingReplyMessageProperty = null;
			IDisposable impersonationContext = null;
			IPrincipal originalPrincipal = null;
			bool isThreadPrincipalSet = false;
			if (!ImpersonateOnSerializingReplyMessageProperty.TryGet(this.innerMessage, out impersonateOnSerializingReplyMessageProperty))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToImpersonateWhileSerializingReponse")));
			}
			IAsyncResult result;
			try
			{
				impersonateOnSerializingReplyMessageProperty.StartImpersonation(out impersonationContext, out originalPrincipal, out isThreadPrincipalSet);
				result = this.innerMessage.BeginWriteBodyContents(writer, callback, state);
			}
			finally
			{
				try
				{
					impersonateOnSerializingReplyMessageProperty.StopImpersonation(impersonationContext, originalPrincipal, isThreadPrincipalSet);
				}
				catch
				{
					string message = null;
					try
					{
						message = SR.GetString("SFxRevertImpersonationFailed0");
					}
					finally
					{
						DiagnosticUtility.FailFast(message);
					}
				}
			}
			return result;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004C788 File Offset: 0x0004A988
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.ImpersonateCall(delegate
			{
				this.innerMessage.WriteBodyContents(writer);
			});
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004C7BB File Offset: 0x0004A9BB
		protected override void OnEndWriteBodyContents(IAsyncResult result)
		{
			this.innerMessage.EndWriteBodyContents(result);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			this.ImpersonateCall(delegate
			{
				this.innerMessage.BodyToString(writer);
			});
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0004C800 File Offset: 0x0004AA00
		private void ImpersonateCall(Action callToImpersonate)
		{
			ImpersonateOnSerializingReplyMessageProperty impersonateOnSerializingReplyMessageProperty = null;
			IDisposable impersonationContext = null;
			IPrincipal originalPrincipal = null;
			bool isThreadPrincipalSet = false;
			if (!ImpersonateOnSerializingReplyMessageProperty.TryGet(this.innerMessage, out impersonateOnSerializingReplyMessageProperty))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToImpersonateWhileSerializingReponse")));
			}
			try
			{
				impersonateOnSerializingReplyMessageProperty.StartImpersonation(out impersonationContext, out originalPrincipal, out isThreadPrincipalSet);
				callToImpersonate();
			}
			finally
			{
				try
				{
					impersonateOnSerializingReplyMessageProperty.StopImpersonation(impersonationContext, originalPrincipal, isThreadPrincipalSet);
				}
				catch
				{
					string message = null;
					try
					{
						message = SR.GetString("SFxRevertImpersonationFailed0");
					}
					finally
					{
						DiagnosticUtility.FailFast(message);
					}
				}
			}
		}

		// Token: 0x04001AB5 RID: 6837
		private Message innerMessage;
	}
}

using System;
using System.Collections;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200076D RID: 1901
	internal class RequestReplyCorrelator : IRequestReplyCorrelator
	{
		// Token: 0x06004899 RID: 18585 RVA: 0x0010C324 File Offset: 0x0010A524
		internal RequestReplyCorrelator()
		{
			this.states = new Hashtable();
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0010C338 File Offset: 0x0010A538
		void IRequestReplyCorrelator.Add<T>(Message request, T state)
		{
			UniqueId messageId = request.Headers.MessageId;
			Type typeFromHandle = typeof(T);
			RequestReplyCorrelator.Key key = new RequestReplyCorrelator.Key(messageId, typeFromHandle);
			ICorrelatorKey correlatorKey = state as ICorrelatorKey;
			if (correlatorKey != null)
			{
				correlatorKey.RequestCorrelatorKey = key;
			}
			Hashtable obj = this.states;
			lock (obj)
			{
				this.states.Add(key, state);
			}
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x0010C3C0 File Offset: 0x0010A5C0
		T IRequestReplyCorrelator.Find<T>(Message reply, bool remove)
		{
			UniqueId relatesTo = this.GetRelatesTo(reply);
			Type typeFromHandle = typeof(T);
			RequestReplyCorrelator.Key key = new RequestReplyCorrelator.Key(relatesTo, typeFromHandle);
			Hashtable obj = this.states;
			T result;
			lock (obj)
			{
				result = (T)((object)this.states[key]);
				if (remove)
				{
					this.states.Remove(key);
				}
			}
			return result;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0010C43C File Offset: 0x0010A63C
		internal void RemoveRequest(ICorrelatorKey request)
		{
			if (request.RequestCorrelatorKey != null)
			{
				Hashtable obj = this.states;
				lock (obj)
				{
					this.states.Remove(request.RequestCorrelatorKey);
				}
			}
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0010C490 File Offset: 0x0010A690
		private UniqueId GetRelatesTo(Message reply)
		{
			UniqueId relatesTo = reply.Headers.RelatesTo;
			if (relatesTo == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentException(SR.GetString("SuppliedMessageIsNotAReplyItHasNoRelatesTo0")), reply);
			}
			return relatesTo;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0010C4CC File Offset: 0x0010A6CC
		internal static bool AddressReply(Message reply, Message request)
		{
			RequestReplyCorrelator.ReplyToInfo info = RequestReplyCorrelator.ExtractReplyToInfo(request);
			return RequestReplyCorrelator.AddressReply(reply, info);
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0010C4E8 File Offset: 0x0010A6E8
		internal static bool AddressReply(Message reply, RequestReplyCorrelator.ReplyToInfo info)
		{
			EndpointAddress endpointAddress = null;
			if (info.HasFaultTo && reply.IsFault)
			{
				endpointAddress = info.FaultTo;
			}
			else if (info.HasReplyTo)
			{
				endpointAddress = info.ReplyTo;
			}
			else if (reply.Version.Addressing == AddressingVersion.WSAddressingAugust2004)
			{
				if (info.HasFrom)
				{
					endpointAddress = info.From;
				}
				else
				{
					endpointAddress = EndpointAddress.AnonymousAddress;
				}
			}
			if (endpointAddress != null)
			{
				endpointAddress.ApplyTo(reply);
				return !endpointAddress.IsNone;
			}
			return true;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0010C56B File Offset: 0x0010A76B
		internal static RequestReplyCorrelator.ReplyToInfo ExtractReplyToInfo(Message message)
		{
			return new RequestReplyCorrelator.ReplyToInfo(message);
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x0010C574 File Offset: 0x0010A774
		internal static void PrepareRequest(Message request)
		{
			MessageHeaders headers = request.Headers;
			if (headers.MessageId == null)
			{
				headers.MessageId = new UniqueId();
			}
			request.Properties.AllowOutputBatching = false;
			if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
			{
				TraceUtility.AddAmbientActivityToMessage(request);
			}
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x0010C5C4 File Offset: 0x0010A7C4
		internal static void PrepareReply(Message reply, UniqueId messageId)
		{
			if (messageId == null)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingMessageID")), reply);
			}
			MessageHeaders headers = reply.Headers;
			if (headers.RelatesTo == null)
			{
				headers.RelatesTo = messageId;
			}
			if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
			{
				TraceUtility.AddAmbientActivityToMessage(reply);
			}
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0010C614 File Offset: 0x0010A814
		internal static void PrepareReply(Message reply, Message request)
		{
			UniqueId messageId = request.Headers.MessageId;
			if (messageId != null)
			{
				MessageHeaders headers = reply.Headers;
				if (headers.RelatesTo == null)
				{
					headers.RelatesTo = messageId;
				}
			}
			if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
			{
				TraceUtility.AddAmbientActivityToMessage(reply);
			}
		}

		// Token: 0x04002DF5 RID: 11765
		private Hashtable states;

		// Token: 0x02000CE4 RID: 3300
		internal struct ReplyToInfo
		{
			// Token: 0x06007A27 RID: 31271 RVA: 0x001C7638 File Offset: 0x001C5838
			internal ReplyToInfo(Message message)
			{
				this.faultTo = message.Headers.FaultTo;
				this.replyTo = message.Headers.ReplyTo;
				if (message.Version.Addressing == AddressingVersion.WSAddressingAugust2004)
				{
					this.from = message.Headers.From;
					return;
				}
				this.from = null;
			}

			// Token: 0x17001BA1 RID: 7073
			// (get) Token: 0x06007A28 RID: 31272 RVA: 0x001C7692 File Offset: 0x001C5892
			internal EndpointAddress FaultTo
			{
				get
				{
					return this.faultTo;
				}
			}

			// Token: 0x17001BA2 RID: 7074
			// (get) Token: 0x06007A29 RID: 31273 RVA: 0x001C769A File Offset: 0x001C589A
			internal EndpointAddress From
			{
				get
				{
					return this.from;
				}
			}

			// Token: 0x17001BA3 RID: 7075
			// (get) Token: 0x06007A2A RID: 31274 RVA: 0x001C76A2 File Offset: 0x001C58A2
			internal bool HasFaultTo
			{
				get
				{
					return !this.IsTrivial(this.FaultTo);
				}
			}

			// Token: 0x17001BA4 RID: 7076
			// (get) Token: 0x06007A2B RID: 31275 RVA: 0x001C76B3 File Offset: 0x001C58B3
			internal bool HasFrom
			{
				get
				{
					return !this.IsTrivial(this.From);
				}
			}

			// Token: 0x17001BA5 RID: 7077
			// (get) Token: 0x06007A2C RID: 31276 RVA: 0x001C76C4 File Offset: 0x001C58C4
			internal bool HasReplyTo
			{
				get
				{
					return !this.IsTrivial(this.ReplyTo);
				}
			}

			// Token: 0x17001BA6 RID: 7078
			// (get) Token: 0x06007A2D RID: 31277 RVA: 0x001C76D5 File Offset: 0x001C58D5
			internal EndpointAddress ReplyTo
			{
				get
				{
					return this.replyTo;
				}
			}

			// Token: 0x06007A2E RID: 31278 RVA: 0x001C76DD File Offset: 0x001C58DD
			private bool IsTrivial(EndpointAddress address)
			{
				return address == null || address == EndpointAddress.AnonymousAddress;
			}

			// Token: 0x040045E1 RID: 17889
			private readonly EndpointAddress faultTo;

			// Token: 0x040045E2 RID: 17890
			private readonly EndpointAddress from;

			// Token: 0x040045E3 RID: 17891
			private readonly EndpointAddress replyTo;
		}

		// Token: 0x02000CE5 RID: 3301
		internal class Key
		{
			// Token: 0x06007A2F RID: 31279 RVA: 0x001C76F5 File Offset: 0x001C58F5
			internal Key(UniqueId messageId, Type stateType)
			{
				this.MessageId = messageId;
				this.StateType = stateType;
			}

			// Token: 0x06007A30 RID: 31280 RVA: 0x001C770C File Offset: 0x001C590C
			public override bool Equals(object obj)
			{
				RequestReplyCorrelator.Key key = obj as RequestReplyCorrelator.Key;
				return key != null && key.MessageId == this.MessageId && key.StateType == this.StateType;
			}

			// Token: 0x06007A31 RID: 31281 RVA: 0x001C774B File Offset: 0x001C594B
			public override int GetHashCode()
			{
				return this.MessageId.GetHashCode() ^ this.StateType.GetHashCode();
			}

			// Token: 0x06007A32 RID: 31282 RVA: 0x001C7764 File Offset: 0x001C5964
			public override string ToString()
			{
				string[] array = new string[6];
				array[0] = typeof(RequestReplyCorrelator.Key).ToString();
				array[1] = ": {";
				int num = 2;
				UniqueId messageId = this.MessageId;
				array[num] = ((messageId != null) ? messageId.ToString() : null);
				array[3] = ", ";
				array[4] = this.StateType.ToString();
				array[5] = "}";
				return string.Concat(array);
			}

			// Token: 0x040045E4 RID: 17892
			internal UniqueId MessageId;

			// Token: 0x040045E5 RID: 17893
			internal Type StateType;
		}
	}
}

using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A4 RID: 1956
	[Serializable]
	public class CallbackContextMessageProperty : IMessageProperty
	{
		// Token: 0x060049F5 RID: 18933 RVA: 0x0010F8B1 File Offset: 0x0010DAB1
		public CallbackContextMessageProperty(IDictionary<string, string> context) : this(null, context)
		{
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0010F8BB File Offset: 0x0010DABB
		public CallbackContextMessageProperty(string listenAddress, IDictionary<string, string> context) : this(new Uri(listenAddress), context)
		{
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0010F8CA File Offset: 0x0010DACA
		public CallbackContextMessageProperty(Uri listenAddress, IDictionary<string, string> context) : this(new EndpointAddress(listenAddress, new AddressHeader[0]), context)
		{
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0010F8E0 File Offset: 0x0010DAE0
		public CallbackContextMessageProperty(EndpointAddress listenAddress, IDictionary<string, string> context)
		{
			if (listenAddress != null && listenAddress.Headers.FindHeader("Context", "http://schemas.microsoft.com/ws/2006/05/context") != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ListenAddressAlreadyContainsContext"));
			}
			this.listenAddress = listenAddress;
			this.context = context;
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0010F936 File Offset: 0x0010DB36
		public CallbackContextMessageProperty(EndpointAddress callbackAddress)
		{
			if (callbackAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackAddress");
			}
			this.callbackAddress = callbackAddress;
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x060049FA RID: 18938 RVA: 0x0010F95E File Offset: 0x0010DB5E
		public static string Name
		{
			get
			{
				return "CallbackContextMessageProperty";
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0010F965 File Offset: 0x0010DB65
		public EndpointAddress CallbackAddress
		{
			get
			{
				if (this.callbackAddress == null && this.listenAddress != null)
				{
					this.callbackAddress = CallbackContextMessageProperty.CreateCallbackAddress(this.listenAddress, this.context);
				}
				return this.callbackAddress;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x060049FC RID: 18940 RVA: 0x0010F9A0 File Offset: 0x0010DBA0
		public IDictionary<string, string> Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x0010F9A8 File Offset: 0x0010DBA8
		public EndpointAddress CreateCallbackAddress(Uri listenAddress)
		{
			if (listenAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("listenAddress");
			}
			return CallbackContextMessageProperty.CreateCallbackAddress(new EndpointAddress(listenAddress, new AddressHeader[0]), this.context);
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0010F9DC File Offset: 0x0010DBDC
		private static EndpointAddress CreateCallbackAddress(EndpointAddress listenAddress, IDictionary<string, string> context)
		{
			if (listenAddress == null)
			{
				return null;
			}
			EndpointAddressBuilder endpointAddressBuilder = new EndpointAddressBuilder(listenAddress);
			if (context != null)
			{
				endpointAddressBuilder.Headers.Add(new ContextAddressHeader(context));
			}
			return endpointAddressBuilder.ToEndpointAddress();
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x0010FA15 File Offset: 0x0010DC15
		public static bool TryGet(Message message, out CallbackContextMessageProperty contextMessageProperty)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return CallbackContextMessageProperty.TryGet(message.Properties, out contextMessageProperty);
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x0010FA38 File Offset: 0x0010DC38
		public static bool TryGet(MessageProperties properties, out CallbackContextMessageProperty contextMessageProperty)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			object obj = null;
			if (properties.TryGetValue("CallbackContextMessageProperty", out obj))
			{
				contextMessageProperty = (obj as CallbackContextMessageProperty);
			}
			else
			{
				contextMessageProperty = null;
			}
			return contextMessageProperty != null;
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x0010FA7B File Offset: 0x0010DC7B
		public void AddOrReplaceInMessage(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			this.AddOrReplaceInMessageProperties(message.Properties);
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x0010FA9C File Offset: 0x0010DC9C
		public void AddOrReplaceInMessageProperties(MessageProperties properties)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			properties["CallbackContextMessageProperty"] = this;
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x0010FABD File Offset: 0x0010DCBD
		public IMessageProperty CreateCopy()
		{
			if (this.callbackAddress != null)
			{
				return new CallbackContextMessageProperty(this.callbackAddress);
			}
			return new CallbackContextMessageProperty(this.listenAddress, this.context);
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x0010FAEC File Offset: 0x0010DCEC
		public void GetListenAddressAndContext(out EndpointAddress listenAddress, out IDictionary<string, string> context)
		{
			if (this.CallbackAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackaddress");
			}
			EndpointAddressBuilder endpointAddressBuilder = new EndpointAddressBuilder(this.CallbackAddress);
			AddressHeader addressHeader = null;
			int index = -1;
			for (int i = 0; i < endpointAddressBuilder.Headers.Count; i++)
			{
				if (endpointAddressBuilder.Headers[i].Name == "Context" && endpointAddressBuilder.Headers[i].Namespace == "http://schemas.microsoft.com/ws/2006/05/context")
				{
					if (addressHeader != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MultipleContextHeadersFoundInCallbackAddress")));
					}
					addressHeader = endpointAddressBuilder.Headers[i];
					index = i;
				}
			}
			if (addressHeader != null)
			{
				endpointAddressBuilder.Headers.RemoveAt(index);
			}
			context = ((addressHeader != null) ? ContextMessageHeader.ParseContextHeader(addressHeader.GetAddressHeaderReader()).Context : null);
			listenAddress = endpointAddressBuilder.ToEndpointAddress();
		}

		// Token: 0x04002EE8 RID: 12008
		private const string PropertyName = "CallbackContextMessageProperty";

		// Token: 0x04002EE9 RID: 12009
		[NonSerialized]
		private readonly EndpointAddress listenAddress;

		// Token: 0x04002EEA RID: 12010
		private readonly IDictionary<string, string> context;

		// Token: 0x04002EEB RID: 12011
		[NonSerialized]
		private EndpointAddress callbackAddress;
	}
}

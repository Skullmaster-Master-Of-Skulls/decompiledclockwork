using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtras.Soap
{
	// Token: 0x02000009 RID: 9
	internal class SoapHeadersClientHook : IClientMessageInspector, IChannelInitializer, IExtension<IContextChannel>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002C43 File Offset: 0x00000E43
		private SoapHeadersClientHook(Dictionary<string, SoapHeadersClientHook.OperationHeaders> headersFromAction, Dictionary<Type, SoapHeaderHelper> soapHelpers)
		{
			this.headersFromAction = headersFromAction;
			this.shHelpers = soapHelpers;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C68 File Offset: 0x00000E68
		void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
		{
			SoapHeadersClientHook.OperationHeaders operationHeaders;
			if (this.headersFromAction.TryGetValue((string)correlationState, out operationHeaders))
			{
				if (operationHeaders.Out.Count > 0)
				{
					foreach (SoapHeaderAttribute soapHeaderAttribute in operationHeaders.Out)
					{
						string name = soapHeaderAttribute.Name;
						SoapHeaderHelper soapHeaderHelper = this.shHelpers[soapHeaderAttribute.Type];
						this.Headers[name] = soapHeaderHelper.GetHeader(name, reply.Headers);
					}
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D30 File Offset: 0x00000F30
		object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			SoapHeadersClientHook.OperationHeaders operationHeaders;
			if (this.headersFromAction.TryGetValue(request.Headers.Action, out operationHeaders))
			{
				if (operationHeaders.In.Count > 0)
				{
					foreach (SoapHeaderAttribute soapHeaderAttribute in operationHeaders.In)
					{
						string name = soapHeaderAttribute.Name;
						object obj = this.Headers[name];
						if (obj != null)
						{
							SoapHeaderHelper soapHeaderHelper = this.shHelpers[soapHeaderAttribute.Type];
							soapHeaderHelper.AddHeader(name, obj, request.Headers);
						}
					}
				}
			}
			return request.Headers.Action;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E20 File Offset: 0x00001020
		internal static void Hook(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			Dictionary<string, SoapHeadersClientHook.OperationHeaders> dictionary = new Dictionary<string, SoapHeadersClientHook.OperationHeaders>();
			Dictionary<Type, SoapHeaderHelper> dictionary2 = new Dictionary<Type, SoapHeaderHelper>();
			foreach (OperationDescription operationDescription in contractDescription.Operations)
			{
				SoapHeaderAttribute[] array = (SoapHeaderAttribute[])operationDescription.SyncMethod.GetCustomAttributes(typeof(SoapHeaderAttribute), false);
				if (array.Length > 0)
				{
					SoapHeadersClientHook.OperationHeaders operationHeaders = new SoapHeadersClientHook.OperationHeaders();
					string key = string.Concat(new string[]
					{
						contractDescription.Namespace,
						"/",
						contractDescription.Name,
						"/",
						operationDescription.Name
					});
					foreach (SoapHeaderAttribute soapHeaderAttribute in array)
					{
						if ((soapHeaderAttribute.Direction & SoapHeaderDirection.In) == SoapHeaderDirection.In)
						{
							operationHeaders.In.Add(soapHeaderAttribute);
						}
						if ((soapHeaderAttribute.Direction & SoapHeaderDirection.Out) == SoapHeaderDirection.Out)
						{
							operationHeaders.Out.Add(soapHeaderAttribute);
						}
						if (!dictionary2.ContainsKey(soapHeaderAttribute.Type))
						{
							dictionary2.Add(soapHeaderAttribute.Type, new SoapHeaderHelper(soapHeaderAttribute.Type));
						}
					}
					dictionary.Add(key, operationHeaders);
				}
			}
			SoapHeadersClientHook item = new SoapHeadersClientHook(dictionary, dictionary2);
			clientRuntime.MessageInspectors.Add(item);
			clientRuntime.ChannelInitializers.Add(item);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FE4 File Offset: 0x000011E4
		void IChannelInitializer.Initialize(IClientChannel channel)
		{
			channel.Extensions.Add(this);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002FF4 File Offset: 0x000011F4
		void IExtension<IContextChannel>.Attach(IContextChannel owner)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FF7 File Offset: 0x000011F7
		void IExtension<IContextChannel>.Detach(IContextChannel owner)
		{
		}

		// Token: 0x04000009 RID: 9
		private Dictionary<Type, SoapHeaderHelper> shHelpers;

		// Token: 0x0400000A RID: 10
		private Dictionary<string, SoapHeadersClientHook.OperationHeaders> headersFromAction;

		// Token: 0x0400000B RID: 11
		internal Hashtable Headers = new Hashtable();

		// Token: 0x0200000A RID: 10
		private class OperationHeaders
		{
			// Token: 0x0400000C RID: 12
			public List<SoapHeaderAttribute> In = new List<SoapHeaderAttribute>();

			// Token: 0x0400000D RID: 13
			public List<SoapHeaderAttribute> Out = new List<SoapHeaderAttribute>();
		}
	}
}

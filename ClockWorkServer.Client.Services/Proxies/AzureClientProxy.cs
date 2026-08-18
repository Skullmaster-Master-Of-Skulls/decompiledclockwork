using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using ClockWorkLogger;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000054 RID: 84
	public static class AzureClientProxy<TInterface> where TInterface : class
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		private static Binding Binding
		{
			get
			{
				return new BasicHttpBinding();
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public static TInterface GetInstance(Uri cloudServiceUri)
		{
			return AzureClientProxy<TInterface>.GetInstance(AzureClientProxy<TInterface>.Binding, AzureClientProxy<TInterface>.GetEndpointAddress(cloudServiceUri));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public static TInterface GetInstance(Binding binding, EndpointAddress endpointAddress)
		{
			return AzureClientProxy<TInterface>.GetReusableInstance(new object[]
			{
				binding,
				endpointAddress
			});
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000BB34 File Offset: 0x00009D34
		private static EndpointAddress GetEndpointAddress(Uri cloudServiceUri)
		{
			string arg = typeof(TInterface).Name.Substring(1);
			return new EndpointAddress(cloudServiceUri + arg + ".svc");
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000BB70 File Offset: 0x00009D70
		private static TInterface GetReusableInstance(object[] args)
		{
			string text = typeof(TInterface).Name.Substring(1);
			TInterface result;
			try
			{
				Type type = Type.GetType(string.Format("TechnoPro.ClockWorkServer.Client.Services.Proxies.{0}ReusableClientProxy", text));
				bool flag = type != null;
				if (flag)
				{
					TInterface tinterface = (TInterface)((object)Activator.CreateInstance(type, args));
					IClientBase clientBase = tinterface as IClientBase;
					bool flag2 = clientBase != null;
					if (flag2)
					{
						foreach (OperationDescription operationDescription in clientBase.Endpoint.Contract.Operations)
						{
							DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
							bool flag3 = dataContractSerializerOperationBehavior != null;
							if (flag3)
							{
								dataContractSerializerOperationBehavior.MaxItemsInObjectGraph = int.MaxValue;
							}
						}
					}
					result = tinterface;
				}
				else
				{
					CWLogger.Logger.Error("WCFClientProxy::GetReusableInstance: Failed to return an instance of {0}", text);
					result = default(TInterface);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error(string.Format("WCFClientProxy::GetReusableInstance: Failed to return an instance of {0}: {1}", text, ex.ToString()));
				result = default(TInterface);
			}
			return result;
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Client.Services.Adapters
{
	// Token: 0x02000176 RID: 374
	public static class BindingAdapter<TInterface> where TInterface : class
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x0002596C File Offset: 0x00023B6C
		public static Binding GetBinding(eBindingType bindingType)
		{
			string key = string.Format("{0}.{1}Binding", typeof(TInterface).Name, bindingType.ToString());
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[key];
			bool flag = obj != null && obj is Binding;
			Binding result;
			if (flag)
			{
				result = (Binding)obj;
			}
			else
			{
				switch (bindingType)
				{
				case eBindingType.NetTcpBinding:
					obj = BindingAdapter<TInterface>.GetNetTcpBinding();
					break;
				case eBindingType.HttpBinding:
					obj = BindingAdapter<TInterface>.GetHttpBinding();
					break;
				case eBindingType.MsmqBinding:
					obj = BindingAdapter<TInterface>.GetMsmqBinding();
					break;
				case eBindingType.NetPipeBinding:
					obj = BindingAdapter<TInterface>.GetNetPipeBinding();
					break;
				default:
					obj = BindingAdapter<TInterface>.GetHttpBinding();
					break;
				}
				cacheStorageManager.Insert(key, obj);
				result = (Binding)obj;
			}
			return result;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00025A2C File Offset: 0x00023C2C
		private static NetTcpBinding GetNetTcpBinding()
		{
			Type typeFromHandle = typeof(TInterface);
			return typeFromHandle.GetNetTcpBinding(SecurityMode.Message);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00025A50 File Offset: 0x00023C50
		private static Binding GetHttpBinding()
		{
			Type typeFromHandle = typeof(TInterface);
			return typeFromHandle.GetHttpBinding();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00025A74 File Offset: 0x00023C74
		private static NetMsmqBinding GetMsmqBinding()
		{
			Type typeFromHandle = typeof(TInterface);
			return typeFromHandle.GetNetMsmqBinding();
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00025A98 File Offset: 0x00023C98
		private static NetNamedPipeBinding GetNetPipeBinding()
		{
			Type typeFromHandle = typeof(TInterface);
			return typeFromHandle.GetNetNamedPipeBinding();
		}
	}
}

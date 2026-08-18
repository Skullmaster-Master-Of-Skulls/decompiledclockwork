using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000DC RID: 220
	public class DefaultHttpControllerActivator : IHttpControllerActivator
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00011320 File Offset: 0x0000F520
		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			if (controllerType == null)
			{
				throw Error.ArgumentNull("controllerType");
			}
			IHttpController result;
			try
			{
				Func<IHttpController> func;
				object obj;
				if (this._fastCache == null)
				{
					IHttpController instanceOrActivator = DefaultHttpControllerActivator.GetInstanceOrActivator(request, controllerType, out func);
					if (instanceOrActivator != null)
					{
						return instanceOrActivator;
					}
					Tuple<HttpControllerDescriptor, Func<IHttpController>> value = Tuple.Create<HttpControllerDescriptor, Func<IHttpController>>(controllerDescriptor, func);
					Interlocked.CompareExchange<Tuple<HttpControllerDescriptor, Func<IHttpController>>>(ref this._fastCache, value, null);
				}
				else if (this._fastCache.Item1 == controllerDescriptor)
				{
					func = this._fastCache.Item2;
				}
				else if (controllerDescriptor.Properties.TryGetValue(this._cacheKey, out obj))
				{
					func = (Func<IHttpController>)obj;
				}
				else
				{
					IHttpController instanceOrActivator2 = DefaultHttpControllerActivator.GetInstanceOrActivator(request, controllerType, out func);
					if (instanceOrActivator2 != null)
					{
						return instanceOrActivator2;
					}
					controllerDescriptor.Properties.TryAdd(this._cacheKey, func);
				}
				result = func();
			}
			catch (Exception innerException)
			{
				throw Error.InvalidOperation(innerException, SRResources.DefaultControllerFactory_ErrorCreatingController, new object[]
				{
					controllerType.Name
				});
			}
			return result;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00011434 File Offset: 0x0000F634
		private static IHttpController GetInstanceOrActivator(HttpRequestMessage request, Type controllerType, out Func<IHttpController> activator)
		{
			IHttpController httpController = (IHttpController)request.GetDependencyScope().GetService(controllerType);
			if (httpController != null)
			{
				activator = null;
				return httpController;
			}
			activator = TypeActivator.Create<IHttpController>(controllerType);
			return null;
		}

		// Token: 0x04000186 RID: 390
		private Tuple<HttpControllerDescriptor, Func<IHttpController>> _fastCache;

		// Token: 0x04000187 RID: 391
		private object _cacheKey = new object();
	}
}

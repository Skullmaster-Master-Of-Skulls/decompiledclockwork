using System;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	// Token: 0x020006EA RID: 1770
	internal class HandlerFactoryCache
	{
		// Token: 0x06005519 RID: 21785 RVA: 0x0012986C File Offset: 0x00127A6C
		internal HandlerFactoryCache(string type)
		{
			object obj = this.Create(type);
			if (obj is IHttpHandler)
			{
				this._factory = new HandlerFactoryWrapper((IHttpHandler)obj, this.GetHandlerType(type));
			}
			else
			{
				if (!(obj is IHttpHandlerFactory))
				{
					throw new HttpException(SR.GetString("Type_not_factory_or_handler", new object[]
					{
						obj.GetType().FullName
					}));
				}
				this._factory = (IHttpHandlerFactory)obj;
			}
			TelemetryLogger.LogHttpHandler(obj.GetType());
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x001298F0 File Offset: 0x00127AF0
		internal HandlerFactoryCache(HttpHandlerAction mapping)
		{
			object obj = mapping.Create();
			if (obj is IHttpHandler)
			{
				this._factory = new HandlerFactoryWrapper((IHttpHandler)obj, this.GetHandlerType(mapping));
			}
			else
			{
				if (!(obj is IHttpHandlerFactory))
				{
					throw new HttpException(SR.GetString("Type_not_factory_or_handler", new object[]
					{
						obj.GetType().FullName
					}));
				}
				this._factory = (IHttpHandlerFactory)obj;
			}
			TelemetryLogger.LogHttpHandler(obj.GetType());
		}

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x00129971 File Offset: 0x00127B71
		internal IHttpHandlerFactory Factory
		{
			get
			{
				return this._factory;
			}
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x00129979 File Offset: 0x00127B79
		[FileIOPermission(SecurityAction.Assert, AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery))]
		private Type GetTypeWithAssert(string type)
		{
			return BuildManager.GetType(type, true, false);
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00129984 File Offset: 0x00127B84
		internal Type GetHandlerType(HttpHandlerAction handlerAction)
		{
			Type typeWithAssert = this.GetTypeWithAssert(handlerAction.Type);
			if (!ConfigUtil.IsTypeHandlerOrFactory(typeWithAssert))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_not_factory_or_handler", new object[]
				{
					handlerAction.Type
				}), handlerAction.ElementInformation.Source, handlerAction.ElementInformation.LineNumber);
			}
			return typeWithAssert;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x001299DC File Offset: 0x00127BDC
		internal Type GetHandlerType(string type)
		{
			Type typeWithAssert = this.GetTypeWithAssert(type);
			HttpRuntime.FailIfNoAPTCABit(typeWithAssert, null, null);
			if (!ConfigUtil.IsTypeHandlerOrFactory(typeWithAssert))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_not_factory_or_handler", new object[]
				{
					type
				}));
			}
			return typeWithAssert;
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x00129A1C File Offset: 0x00127C1C
		internal object Create(string type)
		{
			return HttpRuntime.CreateNonPublicInstanceByWebObjectActivator(this.GetHandlerType(type));
		}

		// Token: 0x04002C95 RID: 11413
		private IHttpHandlerFactory _factory;
	}
}

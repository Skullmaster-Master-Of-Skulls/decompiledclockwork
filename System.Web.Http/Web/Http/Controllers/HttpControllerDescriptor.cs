using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using System.Web.Http.Internal;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000DD RID: 221
	public class HttpControllerDescriptor
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00011478 File Offset: 0x0000F678
		public HttpControllerDescriptor(HttpConfiguration configuration, string controllerName, Type controllerType)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (controllerName == null)
			{
				throw Error.ArgumentNull("controllerName");
			}
			if (controllerType == null)
			{
				throw Error.ArgumentNull("controllerType");
			}
			this._configuration = configuration;
			this._controllerName = controllerName;
			this._controllerType = controllerType;
			this.Initialize();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000114E1 File Offset: 0x0000F6E1
		public HttpControllerDescriptor()
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000114F4 File Offset: 0x0000F6F4
		internal HttpControllerDescriptor(HttpConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001150E File Offset: 0x0000F70E
		public virtual ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00011516 File Offset: 0x0000F716
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0001151E File Offset: 0x0000F71E
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._configuration = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00011530 File Offset: 0x0000F730
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00011538 File Offset: 0x0000F738
		public string ControllerName
		{
			get
			{
				return this._controllerName;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerName = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001154A File Offset: 0x0000F74A
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00011552 File Offset: 0x0000F752
		public Type ControllerType
		{
			get
			{
				return this._controllerType;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerType = value;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001156C File Offset: 0x0000F76C
		public virtual IHttpController CreateController(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IHttpControllerActivator httpControllerActivator = this.Configuration.Services.GetHttpControllerActivator();
			return httpControllerActivator.Create(request, this, this.ControllerType);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000115A8 File Offset: 0x0000F7A8
		public virtual Collection<IFilter> GetFilters()
		{
			return this.GetCustomAttributes<IFilter>();
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public virtual Collection<T> GetCustomAttributes<T>() where T : class
		{
			return this.GetCustomAttributes<T>(true);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000115BC File Offset: 0x0000F7BC
		public virtual Collection<T> GetCustomAttributes<T>(bool inherit) where T : class
		{
			object[] objects;
			if (inherit)
			{
				if (this._attributeCache == null)
				{
					this._attributeCache = this.ControllerType.GetCustomAttributes(true);
				}
				objects = this._attributeCache;
			}
			else
			{
				if (this._declaredOnlyAttributeCache == null)
				{
					this._declaredOnlyAttributeCache = this.ControllerType.GetCustomAttributes(false);
				}
				objects = this._declaredOnlyAttributeCache;
			}
			return new Collection<T>(TypeHelper.OfType<T>(objects));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001161B File Offset: 0x0000F81B
		internal void Initialize(HttpConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00011624 File Offset: 0x0000F824
		private void Initialize()
		{
			HttpControllerDescriptor.InvokeAttributesOnControllerType(this, this.ControllerType);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00011634 File Offset: 0x0000F834
		private static void InvokeAttributesOnControllerType(HttpControllerDescriptor controllerDescriptor, Type type)
		{
			if (type == null)
			{
				return;
			}
			HttpControllerDescriptor.InvokeAttributesOnControllerType(controllerDescriptor, type.BaseType);
			object[] customAttributes = type.GetCustomAttributes(false);
			foreach (object obj in customAttributes)
			{
				IControllerConfiguration controllerConfiguration = obj as IControllerConfiguration;
				if (controllerConfiguration != null)
				{
					HttpConfiguration configuration = controllerDescriptor.Configuration;
					HttpControllerSettings httpControllerSettings = new HttpControllerSettings(configuration);
					controllerConfiguration.Initialize(httpControllerSettings, controllerDescriptor);
					controllerDescriptor.Configuration = HttpConfiguration.ApplyControllerSettings(httpControllerSettings, configuration);
				}
			}
		}

		// Token: 0x04000188 RID: 392
		private readonly ConcurrentDictionary<object, object> _properties = new ConcurrentDictionary<object, object>();

		// Token: 0x04000189 RID: 393
		private HttpConfiguration _configuration;

		// Token: 0x0400018A RID: 394
		private string _controllerName;

		// Token: 0x0400018B RID: 395
		private Type _controllerType;

		// Token: 0x0400018C RID: 396
		private object[] _attributeCache;

		// Token: 0x0400018D RID: 397
		private object[] _declaredOnlyAttributeCache;
	}
}

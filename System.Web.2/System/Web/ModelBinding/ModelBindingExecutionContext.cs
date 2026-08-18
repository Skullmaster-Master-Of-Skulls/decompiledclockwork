using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000689 RID: 1673
	public class ModelBindingExecutionContext
	{
		// Token: 0x06005106 RID: 20742 RVA: 0x00117465 File Offset: 0x00115665
		public ModelBindingExecutionContext(HttpContextBase httpContext, ModelStateDictionary modelState)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (modelState == null)
			{
				throw new ArgumentNullException("modelState");
			}
			this._httpContext = httpContext;
			this._modelState = modelState;
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x001174A2 File Offset: 0x001156A2
		public virtual HttpContextBase HttpContext
		{
			get
			{
				return this._httpContext;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06005108 RID: 20744 RVA: 0x001174AA File Offset: 0x001156AA
		public virtual ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x001174B2 File Offset: 0x001156B2
		public virtual void PublishService<TService>(TService service)
		{
			this._services[typeof(TService)] = service;
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x001174CF File Offset: 0x001156CF
		public virtual TService GetService<TService>()
		{
			return (TService)((object)this._services[typeof(TService)]);
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x001174EC File Offset: 0x001156EC
		public virtual TService TryGetService<TService>()
		{
			if (this._services.ContainsKey(typeof(TService)))
			{
				return (TService)((object)this._services[typeof(TService)]);
			}
			return default(TService);
		}

		// Token: 0x04002ADF RID: 10975
		private Dictionary<Type, object> _services = new Dictionary<Type, object>();

		// Token: 0x04002AE0 RID: 10976
		private HttpContextBase _httpContext;

		// Token: 0x04002AE1 RID: 10977
		private ModelStateDictionary _modelState;
	}
}

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Async;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages.Scope;

namespace System.Web.Mvc
{
	// Token: 0x020000FE RID: 254
	public abstract class ControllerBase : IController
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00012369 File Offset: 0x00010569
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x00012371 File Offset: 0x00010571
		public ControllerContext ControllerContext { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001237C File Offset: 0x0001057C
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x000123C8 File Offset: 0x000105C8
		public TempDataDictionary TempData
		{
			get
			{
				if (this.ControllerContext != null && this.ControllerContext.IsChildAction)
				{
					return this.ControllerContext.ParentActionViewContext.TempData;
				}
				if (this._tempDataDictionary == null)
				{
					this._tempDataDictionary = new TempDataDictionary();
				}
				return this._tempDataDictionary;
			}
			set
			{
				this._tempDataDictionary = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000123D1 File Offset: 0x000105D1
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x000123D9 File Offset: 0x000105D9
		public bool ValidateRequest
		{
			get
			{
				return this._validateRequest;
			}
			set
			{
				this._validateRequest = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000123E2 File Offset: 0x000105E2
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00012408 File Offset: 0x00010608
		public IValueProvider ValueProvider
		{
			get
			{
				if (this._valueProvider == null)
				{
					this._valueProvider = ValueProviderFactories.Factories.GetValueProvider(this.ControllerContext);
				}
				return this._valueProvider;
			}
			set
			{
				this._valueProvider = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001241C File Offset: 0x0001061C
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewDataDictionary == null)
				{
					this._dynamicViewDataDictionary = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewDataDictionary;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00012455 File Offset: 0x00010655
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00012470 File Offset: 0x00010670
		public ViewDataDictionary ViewData
		{
			get
			{
				if (this._viewDataDictionary == null)
				{
					this._viewDataDictionary = new ViewDataDictionary();
				}
				return this._viewDataDictionary;
			}
			set
			{
				this._viewDataDictionary = value;
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001247C File Offset: 0x0001067C
		protected virtual void Execute(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (requestContext.HttpContext == null)
			{
				throw new ArgumentException(MvcResources.ControllerBase_CannotExecuteWithNullHttpContext, "requestContext");
			}
			this.VerifyExecuteCalledOnce();
			this.Initialize(requestContext);
			using (ScopeStorage.CreateTransientScope())
			{
				this.ExecuteCore();
			}
		}

		// Token: 0x06000686 RID: 1670
		protected abstract void ExecuteCore();

		// Token: 0x06000687 RID: 1671 RVA: 0x000124E4 File Offset: 0x000106E4
		protected virtual void Initialize(RequestContext requestContext)
		{
			this.ControllerContext = new ControllerContext(requestContext, this);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000124F4 File Offset: 0x000106F4
		internal void VerifyExecuteCalledOnce()
		{
			if (!this._executeWasCalledGate.TryEnter())
			{
				string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ControllerBase_CannotHandleMultipleRequests, new object[]
				{
					base.GetType()
				});
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00012536 File Offset: 0x00010736
		void IController.Execute(RequestContext requestContext)
		{
			this.Execute(requestContext);
		}

		// Token: 0x040001E0 RID: 480
		private readonly SingleEntryGate _executeWasCalledGate = new SingleEntryGate();

		// Token: 0x040001E1 RID: 481
		private DynamicViewDataDictionary _dynamicViewDataDictionary;

		// Token: 0x040001E2 RID: 482
		private TempDataDictionary _tempDataDictionary;

		// Token: 0x040001E3 RID: 483
		private bool _validateRequest = true;

		// Token: 0x040001E4 RID: 484
		private IValueProvider _valueProvider;

		// Token: 0x040001E5 RID: 485
		private ViewDataDictionary _viewDataDictionary;
	}
}

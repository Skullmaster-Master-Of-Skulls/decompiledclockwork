using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001EE RID: 494
	public class ViewEngineCollection : Collection<IViewEngine>
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00027A4D File Offset: 0x00025C4D
		public ViewEngineCollection()
		{
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00027A55 File Offset: 0x00025C55
		public ViewEngineCollection(IList<IViewEngine> list) : base(list)
		{
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00027A5E File Offset: 0x00025C5E
		internal ViewEngineCollection(IList<IViewEngine> list, IDependencyResolver dependencyResolver) : base(list)
		{
			this._dependencyResolver = dependencyResolver;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00027A70 File Offset: 0x00025C70
		internal IViewEngine[] CombinedItems
		{
			get
			{
				IViewEngine[] array = this._combinedItems;
				if (array == null)
				{
					array = MultiServiceResolver.GetCombined<IViewEngine>(base.Items, this._dependencyResolver);
					this._combinedItems = array;
				}
				return array;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00027AA1 File Offset: 0x00025CA1
		protected override void ClearItems()
		{
			this._combinedItems = null;
			base.ClearItems();
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00027AB0 File Offset: 0x00025CB0
		protected override void InsertItem(int index, IViewEngine item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.InsertItem(index, item);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00027ACF File Offset: 0x00025CCF
		protected override void RemoveItem(int index)
		{
			this._combinedItems = null;
			base.RemoveItem(index);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00027ADF File Offset: 0x00025CDF
		protected override void SetItem(int index, IViewEngine item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.SetItem(index, item);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00027AFE File Offset: 0x00025CFE
		private ViewEngineResult Find(Func<IViewEngine, ViewEngineResult> cacheLocator, Func<IViewEngine, ViewEngineResult> locator)
		{
			return this.Find(cacheLocator, false) ?? this.Find(locator, true);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00027B14 File Offset: 0x00025D14
		private ViewEngineResult Find(Func<IViewEngine, ViewEngineResult> lookup, bool trackSearchedPaths)
		{
			List<string> list = null;
			if (trackSearchedPaths)
			{
				list = new List<string>();
			}
			foreach (IViewEngine viewEngine in this.CombinedItems)
			{
				if (viewEngine != null)
				{
					ViewEngineResult viewEngineResult = lookup(viewEngine);
					if (viewEngineResult.View != null)
					{
						return viewEngineResult;
					}
					if (trackSearchedPaths)
					{
						list.AddRange(viewEngineResult.SearchedLocations);
					}
				}
			}
			if (trackSearchedPaths)
			{
				return new ViewEngineResult(list.Distinct<string>().ToList<string>());
			}
			return null;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00027BBC File Offset: 0x00025DBC
		public virtual ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(partialViewName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "partialViewName");
			}
			return this.Find((IViewEngine e) => e.FindPartialView(controllerContext, partialViewName, true), (IViewEngine e) => e.FindPartialView(controllerContext, partialViewName, false));
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00027C6C File Offset: 0x00025E6C
		public virtual ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(viewName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "viewName");
			}
			return this.Find((IViewEngine e) => e.FindView(controllerContext, viewName, masterName, true), (IViewEngine e) => e.FindView(controllerContext, viewName, masterName, false));
		}

		// Token: 0x040003ED RID: 1005
		private IViewEngine[] _combinedItems;

		// Token: 0x040003EE RID: 1006
		private IDependencyResolver _dependencyResolver;
	}
}

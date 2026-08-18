using System;
using System.Web.ModelBinding;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200047C RID: 1148
	public class ModelMethodContext
	{
		// Token: 0x0600390B RID: 14603 RVA: 0x000B9D72 File Offset: 0x000B7F72
		public ModelMethodContext(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			this._page = page;
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000B9D8F File Offset: 0x000B7F8F
		public ModelStateDictionary ModelState
		{
			get
			{
				return this._page.ModelState;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000B9D9C File Offset: 0x000B7F9C
		public static ModelMethodContext Current
		{
			get
			{
				Page page = HttpContext.Current.Handler as Page;
				if (page != null)
				{
					return new ModelMethodContext(page);
				}
				return null;
			}
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000B9DC4 File Offset: 0x000B7FC4
		public virtual void UpdateModel<TModel>(TModel model) where TModel : class
		{
			this._page.UpdateModel<TModel>(model);
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000B9DD2 File Offset: 0x000B7FD2
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			this._page.UpdateModel<TModel>(model, valueProvider);
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000B9DE1 File Offset: 0x000B7FE1
		public virtual bool TryUpdateModel<TModel>(TModel model) where TModel : class
		{
			return this._page.TryUpdateModel<TModel>(model);
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000B9DEF File Offset: 0x000B7FEF
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			return this._page.TryUpdateModel<TModel>(model, valueProvider);
		}

		// Token: 0x040022A4 RID: 8868
		private Page _page;
	}
}

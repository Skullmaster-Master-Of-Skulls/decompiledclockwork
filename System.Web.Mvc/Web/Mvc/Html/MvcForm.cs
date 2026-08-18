using System;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001AB RID: 427
	public class MvcForm : IDisposable
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		[Obsolete("This constructor is obsolete, because its functionality has been moved to MvcForm(ViewContext) now.", true)]
		public MvcForm(HttpResponseBase httpResponse)
		{
			throw new InvalidOperationException(MvcResources.MvcForm_ConstructorObsolete);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001F1B6 File Offset: 0x0001D3B6
		public MvcForm(ViewContext viewContext)
		{
			if (viewContext == null)
			{
				throw new ArgumentNullException("viewContext");
			}
			this._viewContext = viewContext;
			this._viewContext.FormContext = new FormContext();
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001F1E3 File Offset: 0x0001D3E3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001F1F2 File Offset: 0x0001D3F2
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				FormExtensions.EndForm(this._viewContext);
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001F20E File Offset: 0x0001D40E
		public void EndForm()
		{
			this.Dispose(true);
		}

		// Token: 0x0400032F RID: 815
		private readonly ViewContext _viewContext;

		// Token: 0x04000330 RID: 816
		private bool _disposed;
	}
}

using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x0200004B RID: 75
	public abstract class HttpTaskAsyncHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00007728 File Offset: 0x00005928
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ProcessRequest(HttpContext context)
		{
			string @string = SR.GetString("HttpTaskAsyncHandler_CannotExecuteSynchronously", new object[]
			{
				base.GetType()
			});
			throw new NotSupportedException(@string);
		}

		// Token: 0x0600058B RID: 1419
		public abstract Task ProcessRequestAsync(HttpContext context);

		// Token: 0x0600058C RID: 1420 RVA: 0x00007758 File Offset: 0x00005958
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			return TaskAsyncHelper.BeginTask(() => this.ProcessRequestAsync(context), cb, extraData);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000778C File Offset: 0x0000598C
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			TaskAsyncHelper.EndTask(result);
		}
	}
}

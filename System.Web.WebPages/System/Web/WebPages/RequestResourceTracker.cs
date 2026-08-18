using System;
using System.Collections.Generic;

namespace System.Web.WebPages
{
	// Token: 0x0200006C RID: 108
	internal static class RequestResourceTracker
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000A55C File Offset: 0x0000875C
		private static List<RequestResourceTracker.SecureWeakReference> GetResources(HttpContextBase context)
		{
			List<RequestResourceTracker.SecureWeakReference> list = (List<RequestResourceTracker.SecureWeakReference>)context.Items[RequestResourceTracker._resourcesKey];
			if (list == null)
			{
				list = new List<RequestResourceTracker.SecureWeakReference>();
				context.Items[RequestResourceTracker._resourcesKey] = list;
			}
			return list;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A5A4 File Offset: 0x000087A4
		internal static void DisposeResources(HttpContextBase context)
		{
			List<RequestResourceTracker.SecureWeakReference> resources = RequestResourceTracker.GetResources(context);
			if (resources != null)
			{
				resources.ForEach(delegate(RequestResourceTracker.SecureWeakReference resource)
				{
					resource.Dispose();
				});
				resources.Clear();
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A5E4 File Offset: 0x000087E4
		internal static void RegisterForDispose(HttpContextBase context, IDisposable resource)
		{
			List<RequestResourceTracker.SecureWeakReference> resources = RequestResourceTracker.GetResources(context);
			if (resources != null)
			{
				resources.Add(new RequestResourceTracker.SecureWeakReference(resource));
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A608 File Offset: 0x00008808
		internal static void RegisterForDispose(IDisposable resource)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				RequestResourceTracker.RegisterForDispose(new HttpContextWrapper(httpContext), resource);
			}
		}

		// Token: 0x040000E0 RID: 224
		private static readonly object _resourcesKey = new object();

		// Token: 0x0200006D RID: 109
		private sealed class SecureWeakReference
		{
			// Token: 0x060002CD RID: 717 RVA: 0x0000A636 File Offset: 0x00008836
			public SecureWeakReference(IDisposable reference)
			{
				this._reference = new WeakReference(reference);
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000A64C File Offset: 0x0000884C
			internal void Dispose()
			{
				IDisposable disposable = (IDisposable)this._reference.Target;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x040000E2 RID: 226
			private readonly WeakReference _reference;
		}
	}
}

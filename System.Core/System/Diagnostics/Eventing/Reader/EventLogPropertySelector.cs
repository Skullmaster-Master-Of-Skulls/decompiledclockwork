using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B2 RID: 690
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogPropertySelector : IDisposable
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x0005B390 File Offset: 0x00059590
		[SecurityCritical]
		public EventLogPropertySelector(IEnumerable<string> propertyQueries)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (propertyQueries == null)
			{
				throw new ArgumentNullException("propertyQueries");
			}
			ICollection<string> collection = propertyQueries as ICollection<string>;
			string[] array;
			if (collection != null)
			{
				array = new string[collection.Count];
				collection.CopyTo(array, 0);
			}
			else
			{
				List<string> list = new List<string>(propertyQueries);
				array = list.ToArray();
			}
			this.renderContextHandleValues = NativeWrapper.EvtCreateRenderContext(array.Length, array, UnsafeNativeMethods.EvtRenderContextFlags.EvtRenderContextValues);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0005B3F9 File Offset: 0x000595F9
		internal EventLogHandle Handle
		{
			get
			{
				return this.renderContextHandleValues;
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0005B401 File Offset: 0x00059601
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0005B410 File Offset: 0x00059610
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
			}
			if (this.renderContextHandleValues != null && !this.renderContextHandleValues.IsInvalid)
			{
				this.renderContextHandleValues.Dispose();
			}
		}

		// Token: 0x04000C35 RID: 3125
		private EventLogHandle renderContextHandleValues;
	}
}

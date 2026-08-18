using System;
using System.Configuration.Internal;

namespace System.Web.Configuration
{
	// Token: 0x02000269 RID: 617
	internal sealed class WebConfigurationHostFileChange
	{
		// Token: 0x0600207D RID: 8317 RVA: 0x0008DD69 File Offset: 0x0008CD69
		internal WebConfigurationHostFileChange(StreamChangeCallback callback)
		{
			this._callback = callback;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0008DD78 File Offset: 0x0008CD78
		internal void OnFileChanged(object sender, FileChangeEvent e)
		{
			this._callback(e.FileName);
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0008DD8B File Offset: 0x0008CD8B
		internal StreamChangeCallback Callback
		{
			get
			{
				return this._callback;
			}
		}

		// Token: 0x04001AA6 RID: 6822
		private StreamChangeCallback _callback;
	}
}

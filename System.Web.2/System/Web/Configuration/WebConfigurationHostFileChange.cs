using System;
using System.Configuration.Internal;

namespace System.Web.Configuration
{
	// Token: 0x02000775 RID: 1909
	internal sealed class WebConfigurationHostFileChange
	{
		// Token: 0x06005BFB RID: 23547 RVA: 0x0013EC63 File Offset: 0x0013CE63
		internal WebConfigurationHostFileChange(StreamChangeCallback callback)
		{
			this._callback = callback;
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x0013EC72 File Offset: 0x0013CE72
		internal void OnFileChanged(object sender, FileChangeEvent e)
		{
			this._callback(e.FileName);
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x0013EC85 File Offset: 0x0013CE85
		internal StreamChangeCallback Callback
		{
			get
			{
				return this._callback;
			}
		}

		// Token: 0x04003069 RID: 12393
		private StreamChangeCallback _callback;
	}
}

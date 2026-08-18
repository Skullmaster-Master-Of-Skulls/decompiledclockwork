using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005AE RID: 1454
	public abstract class WebPartTransformer
	{
		// Token: 0x0600499A RID: 18842 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual Control CreateConfigurationControl()
		{
			return null;
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void LoadConfigurationState(object savedState)
		{
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x0000298D File Offset: 0x00000B8D
		protected internal virtual object SaveConfigurationState()
		{
			return null;
		}

		// Token: 0x0600499D RID: 18845
		public abstract object Transform(object providerData);
	}
}

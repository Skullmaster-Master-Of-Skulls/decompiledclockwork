using System;

namespace System.Web.Util
{
	// Token: 0x020001FC RID: 508
	public interface IWebPropertyAccessor
	{
		// Token: 0x0600190A RID: 6410
		object GetProperty(object target);

		// Token: 0x0600190B RID: 6411
		void SetProperty(object target, object value);
	}
}

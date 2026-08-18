using System;

namespace System.Web.UI
{
	// Token: 0x02000297 RID: 663
	public interface IAttributeAccessor
	{
		// Token: 0x06001F76 RID: 8054
		string GetAttribute(string key);

		// Token: 0x06001F77 RID: 8055
		void SetAttribute(string key, string value);
	}
}

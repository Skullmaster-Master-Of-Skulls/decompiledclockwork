using System;
using System.Collections;

namespace System.Web.UI.Design
{
	// Token: 0x02000088 RID: 136
	public abstract class WebFormsReferenceManager
	{
		// Token: 0x0600040A RID: 1034
		public abstract Type GetType(string tagPrefix, string tagName);

		// Token: 0x0600040B RID: 1035
		public abstract string GetTagPrefix(Type objectType);

		// Token: 0x0600040C RID: 1036
		public abstract string RegisterTagPrefix(Type objectType);

		// Token: 0x0600040D RID: 1037
		public abstract ICollection GetRegisterDirectives();

		// Token: 0x0600040E RID: 1038
		public abstract string GetUserControlPath(string tagPrefix, string tagName);
	}
}

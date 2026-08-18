using System;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.Caching
{
	// Token: 0x02000894 RID: 2196
	internal class SRef
	{
		// Token: 0x0600671A RID: 26394 RVA: 0x0016C10C File Offset: 0x0016A30C
		internal SRef(object target)
		{
			this._sizedRef = HttpRuntime.CreateNonPublicInstance(SRef.s_type, new object[]
			{
				target
			});
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x0600671B RID: 26395 RVA: 0x0016C130 File Offset: 0x0016A330
		internal long ApproximateSize
		{
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				object obj = SRef.s_type.InvokeMember("ApproximateSize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, this._sizedRef, null, CultureInfo.InvariantCulture);
				return this._lastReportedSize = (long)obj;
			}
		}

		// Token: 0x0600671C RID: 26396 RVA: 0x0016C16E File Offset: 0x0016A36E
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void Dispose()
		{
			SRef.s_type.InvokeMember("Dispose", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, this._sizedRef, null, CultureInfo.InvariantCulture);
		}

		// Token: 0x04003540 RID: 13632
		private static Type s_type = Type.GetType("System.SizedReference", true, false);

		// Token: 0x04003541 RID: 13633
		private object _sizedRef;

		// Token: 0x04003542 RID: 13634
		private long _lastReportedSize;
	}
}

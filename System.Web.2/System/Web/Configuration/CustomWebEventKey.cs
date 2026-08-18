using System;

namespace System.Web.Configuration
{
	// Token: 0x020006D1 RID: 1745
	internal class CustomWebEventKey
	{
		// Token: 0x0600540C RID: 21516 RVA: 0x00126F90 File Offset: 0x00125190
		internal CustomWebEventKey(Type eventType, int eventCode)
		{
			this._type = eventType;
			this._eventCode = eventCode;
		}

		// Token: 0x04002C36 RID: 11318
		internal Type _type;

		// Token: 0x04002C37 RID: 11319
		internal int _eventCode;
	}
}

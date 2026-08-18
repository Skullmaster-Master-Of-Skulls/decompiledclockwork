using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200072C RID: 1836
	internal class SmuggledObjRef
	{
		// Token: 0x060041D8 RID: 16856 RVA: 0x000E01BB File Offset: 0x000DF1BB
		public SmuggledObjRef(ObjRef objRef)
		{
			this._objRef = objRef;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x000E01CA File Offset: 0x000DF1CA
		public ObjRef ObjRef
		{
			get
			{
				return this._objRef;
			}
		}

		// Token: 0x04002106 RID: 8454
		private ObjRef _objRef;
	}
}

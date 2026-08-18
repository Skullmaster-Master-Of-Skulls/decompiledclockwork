using System;

namespace System.Web.Http.Description
{
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ResponseTypeAttribute : Attribute
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00006FF8 File Offset: 0x000051F8
		public ResponseTypeAttribute(Type responseType)
		{
			this.ResponseType = responseType;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007007 File Offset: 0x00005207
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000700F File Offset: 0x0000520F
		public Type ResponseType { get; private set; }
	}
}

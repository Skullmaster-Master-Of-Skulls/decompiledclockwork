using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000533 RID: 1331
	[ComVisible(true)]
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		// Token: 0x0600331E RID: 13086 RVA: 0x000AD390 File Offset: 0x000AC390
		public SafeArrayTypeMismatchException() : base(Environment.GetResourceString("Arg_SafeArrayTypeMismatchException"))
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000AD3AD File Offset: 0x000AC3AD
		public SafeArrayTypeMismatchException(string message) : base(message)
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000AD3C1 File Offset: 0x000AC3C1
		public SafeArrayTypeMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000AD3D6 File Offset: 0x000AC3D6
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

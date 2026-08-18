using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x020005C1 RID: 1473
	[ComVisible(true)]
	[Serializable]
	public class PathTooLongException : IOException
	{
		// Token: 0x060036AD RID: 13997 RVA: 0x000B8E42 File Offset: 0x000B7E42
		public PathTooLongException() : base(Environment.GetResourceString("IO.PathTooLong"))
		{
			base.SetErrorCode(-2147024690);
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000B8E5F File Offset: 0x000B7E5F
		public PathTooLongException(string message) : base(message)
		{
			base.SetErrorCode(-2147024690);
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000B8E73 File Offset: 0x000B7E73
		public PathTooLongException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024690);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000B8E88 File Offset: 0x000B7E88
		protected PathTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

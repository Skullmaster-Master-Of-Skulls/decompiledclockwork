using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	public abstract class DbException : ExternalException
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x00241078 File Offset: 0x00240478
		protected DbException()
		{
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00241098 File Offset: 0x00240498
		protected DbException(string message) : base(message)
		{
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x002410B8 File Offset: 0x002404B8
		protected DbException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x002410D8 File Offset: 0x002404D8
		protected DbException(string message, int errorCode) : base(message, errorCode)
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x002410F8 File Offset: 0x002404F8
		protected DbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

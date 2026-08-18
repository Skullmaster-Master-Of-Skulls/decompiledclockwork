using System;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public sealed class InvalidUdtException : SystemException
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x000438CC File Offset: 0x00042CCC
		internal InvalidUdtException()
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000438EC File Offset: 0x00042CEC
		internal InvalidUdtException(string message) : base(message)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0004390C File Offset: 0x00042D0C
		internal InvalidUdtException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0004392C File Offset: 0x00042D2C
		private InvalidUdtException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00043944 File Offset: 0x00042D44
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0004395C File Offset: 0x00042D5C
		internal static InvalidUdtException Create(Type udtType, string resourceReason)
		{
			string @string = Res.GetString(resourceReason);
			string string2 = Res.GetString("SqlUdt_InvalidUdtMessage", new object[]
			{
				udtType.FullName,
				@string
			});
			InvalidUdtException ex = new InvalidUdtException(string2);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}
	}
}

using System;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	public sealed class InvalidUdtException : SystemException
	{
		// Token: 0x0600218A RID: 8586 RVA: 0x002873D8 File Offset: 0x002867D8
		internal InvalidUdtException()
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x002873F8 File Offset: 0x002867F8
		internal InvalidUdtException(string message) : base(message)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00287418 File Offset: 0x00286818
		internal InvalidUdtException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00287438 File Offset: 0x00286838
		private InvalidUdtException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00287458 File Offset: 0x00286858
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00287478 File Offset: 0x00286878
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

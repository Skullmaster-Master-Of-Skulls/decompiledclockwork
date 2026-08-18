using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000573 RID: 1395
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class InvalidEnumArgumentException : ArgumentException
	{
		// Token: 0x060033DF RID: 13279 RVA: 0x000E438A File Offset: 0x000E258A
		public InvalidEnumArgumentException() : this(null)
		{
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000E4393 File Offset: 0x000E2593
		public InvalidEnumArgumentException(string message) : base(message)
		{
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000E439C File Offset: 0x000E259C
		public InvalidEnumArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000E43A6 File Offset: 0x000E25A6
		public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass) : base(SR.GetString("InvalidEnumArgument", new object[]
		{
			argumentName,
			invalidValue.ToString(CultureInfo.CurrentCulture),
			enumClass.Name
		}), argumentName)
		{
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000E43DB File Offset: 0x000E25DB
		protected InvalidEnumArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

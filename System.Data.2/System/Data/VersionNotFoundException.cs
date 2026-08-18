using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x0005C790 File Offset: 0x0005BB90
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0005C7A8 File Offset: 0x0005BBA8
		public VersionNotFoundException() : base(Res.GetString("DataSet_DefaultVersionNotFoundException"))
		{
			base.HResult = -2146232023;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0005C7D0 File Offset: 0x0005BBD0
		public VersionNotFoundException(string s) : base(s)
		{
			base.HResult = -2146232023;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0005C7F0 File Offset: 0x0005BBF0
		public VersionNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232023;
		}
	}
}

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x020005AE RID: 1454
	[ComVisible(true)]
	[Serializable]
	public class IOException : SystemException
	{
		// Token: 0x06003594 RID: 13716 RVA: 0x000B2A52 File Offset: 0x000B1A52
		public IOException() : base(Environment.GetResourceString("Arg_IOException"))
		{
			base.SetErrorCode(-2146232800);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000B2A6F File Offset: 0x000B1A6F
		public IOException(string message) : base(message)
		{
			base.SetErrorCode(-2146232800);
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000B2A83 File Offset: 0x000B1A83
		public IOException(string message, int hresult) : base(message)
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000B2A93 File Offset: 0x000B1A93
		internal IOException(string message, int hresult, string maybeFullPath) : base(message)
		{
			base.SetErrorCode(hresult);
			this._maybeFullPath = maybeFullPath;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000B2AAA File Offset: 0x000B1AAA
		public IOException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146232800);
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000B2ABF File Offset: 0x000B1ABF
		protected IOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001C24 RID: 7204
		[NonSerialized]
		private string _maybeFullPath;
	}
}

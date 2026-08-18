using System;
using System.Runtime.Serialization;

namespace System.Web.Util
{
	// Token: 0x020001C3 RID: 451
	internal sealed class AppVerifierException : Exception
	{
		// Token: 0x0600172B RID: 5931 RVA: 0x00048E42 File Offset: 0x00047042
		public AppVerifierException(AppVerifierErrorCode errorCode, string message) : base(message)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00048E52 File Offset: 0x00047052
		private AppVerifierException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00048E5C File Offset: 0x0004705C
		public AppVerifierErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x040016E8 RID: 5864
		private readonly AppVerifierErrorCode _errorCode;
	}
}

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200050C RID: 1292
	[ComVisible(true)]
	[Serializable]
	public class COMException : ExternalException
	{
		// Token: 0x060031B2 RID: 12722 RVA: 0x000A9A10 File Offset: 0x000A8A10
		public COMException() : base(Environment.GetResourceString("Arg_COMException"))
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000A9A2D File Offset: 0x000A8A2D
		public COMException(string message) : base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000A9A41 File Offset: 0x000A8A41
		public COMException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000A9A56 File Offset: 0x000A8A56
		public COMException(string message, int errorCode) : base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000A9A66 File Offset: 0x000A8A66
		protected COMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000A9A70 File Offset: 0x000A8A70
		public override string ToString()
		{
			string message = this.Message;
			string str = base.GetType().ToString();
			string text = str + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (message != null && message.Length > 0)
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}

using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200058B RID: 1419
	internal sealed class MessageOperationFormatter : IClientMessageFormatter, IDispatchMessageFormatter
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000D2866 File Offset: 0x000D0A66
		internal static MessageOperationFormatter Instance
		{
			get
			{
				if (MessageOperationFormatter.instance == null)
				{
					MessageOperationFormatter.instance = new MessageOperationFormatter();
				}
				return MessageOperationFormatter.instance;
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000D287E File Offset: 0x000D0A7E
		public object DeserializeReply(Message message, object[] parameters)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			if (parameters != null && parameters.Length != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxParametersMustBeEmpty")));
			}
			return message;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000D28BC File Offset: 0x000D0ABC
		public void DeserializeRequest(Message message, object[] parameters)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			if (parameters == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("parameters"), message);
			}
			if (parameters.Length != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxParameterMustBeArrayOfOneElement")));
			}
			parameters[0] = message;
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000D2919 File Offset: 0x000D0B19
		public bool IsFault(string operation, Exception error)
		{
			return false;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000D291C File Offset: 0x000D0B1C
		public MessageFault SerializeFault(Exception error)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMessageOperationFormatterCannotSerializeFault")));
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000D2938 File Offset: 0x000D0B38
		public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
		{
			if (!(result is Message))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxResultMustBeMessage")));
			}
			if (parameters != null && parameters.Length != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxParametersMustBeEmpty")));
			}
			return (Message)result;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000D2990 File Offset: 0x000D0B90
		public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parameters"));
			}
			if (parameters.Length != 1 || !(parameters[0] is Message))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxParameterMustBeMessage")));
			}
			return (Message)parameters[0];
		}

		// Token: 0x040028C0 RID: 10432
		private static MessageOperationFormatter instance;
	}
}

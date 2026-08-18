using System;
using System.Globalization;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B6 RID: 2486
	[__DynamicallyInvokable]
	public abstract class FaultConverter
	{
		// Token: 0x0600617F RID: 24959 RVA: 0x0016B55B File Offset: 0x0016975B
		[__DynamicallyInvokable]
		public static FaultConverter GetDefaultFaultConverter(MessageVersion version)
		{
			return new FaultConverter.DefaultFaultConverter(version);
		}

		// Token: 0x06006180 RID: 24960
		[__DynamicallyInvokable]
		protected abstract bool OnTryCreateException(Message message, MessageFault fault, out Exception exception);

		// Token: 0x06006181 RID: 24961
		[__DynamicallyInvokable]
		protected abstract bool OnTryCreateFaultMessage(Exception exception, out Message message);

		// Token: 0x06006182 RID: 24962 RVA: 0x0016B564 File Offset: 0x00169764
		[__DynamicallyInvokable]
		public bool TryCreateException(Message message, MessageFault fault, out Exception exception)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (fault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("fault");
			}
			bool flag = this.OnTryCreateException(message, fault, out exception);
			if (flag)
			{
				if (exception == null)
				{
					string @string = SR.GetString("FaultConverterDidNotCreateException", new object[]
					{
						base.GetType().Name
					});
					Exception exception2 = new InvalidOperationException(@string);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception2);
				}
			}
			else if (exception != null)
			{
				string string2 = SR.GetString("FaultConverterCreatedException", new object[]
				{
					base.GetType().Name
				});
				Exception exception3 = new InvalidOperationException(string2, exception);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception3);
			}
			return flag;
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x0016B618 File Offset: 0x00169818
		public bool TryCreateFaultMessage(Exception exception, out Message message)
		{
			bool flag = this.OnTryCreateFaultMessage(exception, out message);
			if (flag)
			{
				if (message == null)
				{
					string @string = SR.GetString("FaultConverterDidNotCreateFaultMessage", new object[]
					{
						base.GetType().Name
					});
					Exception exception2 = new InvalidOperationException(@string);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception2);
				}
			}
			else if (message != null)
			{
				string string2 = SR.GetString("FaultConverterCreatedFaultMessage", new object[]
				{
					base.GetType().Name
				});
				Exception exception3 = new InvalidOperationException(string2);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception3);
			}
			return flag;
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x0016B6A0 File Offset: 0x001698A0
		[__DynamicallyInvokable]
		protected FaultConverter()
		{
		}

		// Token: 0x02000E42 RID: 3650
		private class DefaultFaultConverter : FaultConverter
		{
			// Token: 0x060082C0 RID: 33472 RVA: 0x001E34C6 File Offset: 0x001E16C6
			internal DefaultFaultConverter(MessageVersion version)
			{
				this.version = version;
			}

			// Token: 0x060082C1 RID: 33473 RVA: 0x001E34D8 File Offset: 0x001E16D8
			protected override bool OnTryCreateException(Message message, MessageFault fault, out Exception exception)
			{
				exception = null;
				if (string.Compare(fault.Code.Namespace, this.version.Envelope.Namespace, StringComparison.Ordinal) == 0 && string.Compare(fault.Code.Name, "MustUnderstand", StringComparison.Ordinal) == 0)
				{
					exception = new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
					return true;
				}
				bool flag;
				bool flag2;
				FaultCode faultCode;
				if (this.version.Envelope == EnvelopeVersion.Soap11)
				{
					flag = true;
					flag2 = true;
					faultCode = fault.Code;
				}
				else
				{
					flag = fault.Code.IsSenderFault;
					flag2 = fault.Code.IsReceiverFault;
					faultCode = fault.Code.SubCode;
				}
				if (faultCode == null)
				{
					return false;
				}
				if (faultCode.Namespace == null)
				{
					return false;
				}
				if (flag && string.Compare(faultCode.Namespace, this.version.Addressing.Namespace, StringComparison.Ordinal) == 0)
				{
					if (string.Compare(faultCode.Name, "ActionNotSupported", StringComparison.Ordinal) == 0)
					{
						exception = new ActionNotSupportedException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
						return true;
					}
					if (string.Compare(faultCode.Name, "DestinationUnreachable", StringComparison.Ordinal) == 0)
					{
						exception = new EndpointNotFoundException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
						return true;
					}
					if (string.Compare(faultCode.Name, "InvalidAddressingHeader", StringComparison.Ordinal) == 0)
					{
						if (faultCode.SubCode != null && string.Compare(faultCode.SubCode.Namespace, this.version.Addressing.Namespace, StringComparison.Ordinal) == 0 && string.Compare(faultCode.SubCode.Name, "InvalidCardinality", StringComparison.Ordinal) == 0)
						{
							exception = new MessageHeaderException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text, true);
							return true;
						}
					}
					else if (this.version.Addressing == AddressingVersion.WSAddressing10)
					{
						if (string.Compare(faultCode.Name, "MessageAddressingHeaderRequired", StringComparison.Ordinal) == 0)
						{
							exception = new MessageHeaderException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
							return true;
						}
						if (string.Compare(faultCode.Name, "InvalidAddressingHeader", StringComparison.Ordinal) == 0)
						{
							exception = new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
							return true;
						}
					}
					else
					{
						if (string.Compare(faultCode.Name, "MessageInformationHeaderRequired", StringComparison.Ordinal) == 0)
						{
							exception = new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
							return true;
						}
						if (string.Compare(faultCode.Name, "InvalidMessageInformationHeader", StringComparison.Ordinal) == 0)
						{
							exception = new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
							return true;
						}
					}
				}
				if (flag2 && string.Compare(faultCode.Namespace, this.version.Addressing.Namespace, StringComparison.Ordinal) == 0 && string.Compare(faultCode.Name, "EndpointUnavailable", StringComparison.Ordinal) == 0)
				{
					exception = new ServerTooBusyException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
					return true;
				}
				return false;
			}

			// Token: 0x060082C2 RID: 33474 RVA: 0x001E37C8 File Offset: 0x001E19C8
			protected override bool OnTryCreateFaultMessage(Exception exception, out Message message)
			{
				if (this.version.Addressing == AddressingVersion.WSAddressing10)
				{
					if (exception is MessageHeaderException)
					{
						MessageHeaderException ex = exception as MessageHeaderException;
						if (ex.HeaderNamespace == AddressingVersion.WSAddressing10.Namespace)
						{
							message = ex.ProvideFault(this.version);
							return true;
						}
					}
					else if (exception is ActionMismatchAddressingException)
					{
						ActionMismatchAddressingException ex2 = exception as ActionMismatchAddressingException;
						message = ex2.ProvideFault(this.version);
						return true;
					}
				}
				if (this.version.Addressing != AddressingVersion.None && exception is ActionNotSupportedException)
				{
					ActionNotSupportedException ex3 = exception as ActionNotSupportedException;
					message = ex3.ProvideFault(this.version);
					return true;
				}
				if (exception is MustUnderstandSoapException)
				{
					MustUnderstandSoapException ex4 = exception as MustUnderstandSoapException;
					message = ex4.ProvideFault(this.version);
					return true;
				}
				message = null;
				return false;
			}

			// Token: 0x04004A3B RID: 19003
			private MessageVersion version;
		}
	}
}

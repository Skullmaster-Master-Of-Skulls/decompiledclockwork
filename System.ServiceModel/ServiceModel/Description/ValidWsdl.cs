using System;
using System.Web.Services.Description;

namespace System.ServiceModel.Description
{
	// Token: 0x02000410 RID: 1040
	internal static class ValidWsdl
	{
		// Token: 0x060027E2 RID: 10210 RVA: 0x0009673C File Offset: 0x0009493C
		internal static bool Check(SoapHeaderBinding soapHeaderBinding, MessageBinding messageBinding, WsdlWarningHandler warningHandler)
		{
			if (soapHeaderBinding.Message == null || soapHeaderBinding.Message.IsEmpty)
			{
				string @string = SR.GetString("XsdMissingRequiredAttribute1", new object[]
				{
					"message"
				});
				string string2 = SR.GetString("IgnoreSoapHeaderBinding3", new object[]
				{
					messageBinding.OperationBinding.Name,
					messageBinding.OperationBinding.Binding.ServiceDescription.TargetNamespace,
					@string
				});
				warningHandler(string2);
				return false;
			}
			if (string.IsNullOrEmpty(soapHeaderBinding.Part))
			{
				string string3 = SR.GetString("XsdMissingRequiredAttribute1", new object[]
				{
					"part"
				});
				string string4 = SR.GetString("IgnoreSoapHeaderBinding3", new object[]
				{
					messageBinding.OperationBinding.Name,
					messageBinding.OperationBinding.Binding.ServiceDescription.TargetNamespace,
					string3
				});
				warningHandler(string4);
				return false;
			}
			return true;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0009682C File Offset: 0x00094A2C
		internal static bool Check(SoapFaultBinding soapFaultBinding, FaultBinding faultBinding, WsdlWarningHandler warningHandler)
		{
			if (string.IsNullOrEmpty(soapFaultBinding.Name))
			{
				string @string = SR.GetString("XsdMissingRequiredAttribute1", new object[]
				{
					"name"
				});
				string string2 = SR.GetString("IgnoreSoapFaultBinding3", new object[]
				{
					faultBinding.OperationBinding.Name,
					faultBinding.OperationBinding.Binding.ServiceDescription.TargetNamespace,
					@string
				});
				warningHandler(string2);
				return false;
			}
			return true;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000968A4 File Offset: 0x00094AA4
		internal static bool Check(MessagePart part, Message message, WsdlWarningHandler warningHandler)
		{
			if (string.IsNullOrEmpty(part.Name))
			{
				string @string = SR.GetString("XsdMissingRequiredAttribute1", new object[]
				{
					"name"
				});
				string string2 = SR.GetString("IgnoreMessagePart3", new object[]
				{
					message.Name,
					message.ServiceDescription.TargetNamespace,
					@string
				});
				warningHandler(string2);
				return false;
			}
			return true;
		}
	}
}

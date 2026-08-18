using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x0200043D RID: 1085
	public abstract class TypedMessageConverter
	{
		// Token: 0x06002A69 RID: 10857 RVA: 0x000A3FDE File Offset: 0x000A21DE
		public static TypedMessageConverter Create(Type messageContract, string action)
		{
			return TypedMessageConverter.Create(messageContract, action, null, TypeLoader.DefaultDataContractFormatAttribute);
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000A3FED File Offset: 0x000A21ED
		public static TypedMessageConverter Create(Type messageContract, string action, string defaultNamespace)
		{
			return TypedMessageConverter.Create(messageContract, action, defaultNamespace, TypeLoader.DefaultDataContractFormatAttribute);
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000A3FFC File Offset: 0x000A21FC
		public static TypedMessageConverter Create(Type messageContract, string action, XmlSerializerFormatAttribute formatterAttribute)
		{
			return TypedMessageConverter.Create(messageContract, action, null, formatterAttribute);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000A4007 File Offset: 0x000A2207
		public static TypedMessageConverter Create(Type messageContract, string action, DataContractFormatAttribute formatterAttribute)
		{
			return TypedMessageConverter.Create(messageContract, action, null, formatterAttribute);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000A4012 File Offset: 0x000A2212
		public static TypedMessageConverter Create(Type messageContract, string action, string defaultNamespace, XmlSerializerFormatAttribute formatterAttribute)
		{
			if (messageContract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageContract"));
			}
			if (defaultNamespace == null)
			{
				defaultNamespace = "http://tempuri.org/";
			}
			return new XmlMessageConverter(TypedMessageConverter.GetOperationFormatter(messageContract, formatterAttribute, defaultNamespace, action));
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000A404C File Offset: 0x000A224C
		public static TypedMessageConverter Create(Type messageContract, string action, string defaultNamespace, DataContractFormatAttribute formatterAttribute)
		{
			if (messageContract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageContract"));
			}
			if (!messageContract.IsDefined(typeof(MessageContractAttribute), false))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxMessageContractAttributeRequired", new object[]
				{
					messageContract
				}), "messageContract"));
			}
			if (defaultNamespace == null)
			{
				defaultNamespace = "http://tempuri.org/";
			}
			return new XmlMessageConverter(TypedMessageConverter.GetOperationFormatter(messageContract, formatterAttribute, defaultNamespace, action));
		}

		// Token: 0x06002A6F RID: 10863
		public abstract Message ToMessage(object typedMessage);

		// Token: 0x06002A70 RID: 10864
		public abstract Message ToMessage(object typedMessage, MessageVersion version);

		// Token: 0x06002A71 RID: 10865
		public abstract object FromMessage(Message message);

		// Token: 0x06002A72 RID: 10866 RVA: 0x000A40CC File Offset: 0x000A22CC
		private static OperationFormatter GetOperationFormatter(Type t, Attribute formatAttribute, string defaultNS, string action)
		{
			bool flag = formatAttribute is XmlSerializerFormatAttribute;
			TypeLoader typeLoader = new TypeLoader();
			MessageDescription item = typeLoader.CreateTypedMessageDescription(t, null, null, defaultNS, action, MessageDirection.Output);
			ContractDescription declaringContract = new ContractDescription("dummy_contract", defaultNS);
			OperationDescription operationDescription = new OperationDescription(NamingHelper.XmlName(t.Name), declaringContract, false);
			operationDescription.Messages.Add(item);
			if (flag)
			{
				return XmlSerializerOperationBehavior.CreateOperationFormatter(operationDescription, (XmlSerializerFormatAttribute)formatAttribute);
			}
			return new DataContractSerializerOperationFormatter(operationDescription, (DataContractFormatAttribute)formatAttribute, null);
		}
	}
}

using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A5F RID: 2655
	public sealed class TransactionFlowBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x060068E4 RID: 26852 RVA: 0x00187ECC File Offset: 0x001860CC
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			bool flag = true;
			bool flag2 = true;
			TransactionFlowOption transactionFlowOption = TransactionFlowOption.NotAllowed;
			TransactionProtocol transactionProtocol = TransactionFlowDefaults.TransactionProtocol;
			bool flag3 = false;
			bool flag4 = false;
			XmlElement xmlElement = null;
			XmlElement xmlElement2 = null;
			foreach (OperationDescription operation in context.Contract.Operations)
			{
				ICollection<XmlElement> operationBindingAssertions = context.GetOperationBindingAssertions(operation);
				foreach (XmlElement xmlElement3 in operationBindingAssertions)
				{
					if (xmlElement3.NamespaceURI == "http://schemas.microsoft.com/ws/2006/02/tx/oletx" && xmlElement3.LocalName == "OleTxAssertion")
					{
						xmlElement = xmlElement3;
						TransactionFlowOption option = this.GetOption(xmlElement3, true);
						this.UpdateTransactionFlowAtribute(operation, option);
						TransactionFlowBindingElementImporter.TrackAgreement(ref flag, option, ref transactionFlowOption, ref flag3);
						TransactionFlowBindingElementImporter.TrackAgreementTransactionProtocol(ref flag2, TransactionProtocol.OleTransactions, ref transactionProtocol, ref flag4);
					}
					else if (xmlElement3.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/10/wsat" && xmlElement3.LocalName == "ATAssertion")
					{
						xmlElement2 = xmlElement3;
						TransactionFlowOption option2 = this.GetOption(xmlElement3, true);
						this.UpdateTransactionFlowAtribute(operation, option2);
						TransactionFlowBindingElementImporter.TrackAgreement(ref flag, option2, ref transactionFlowOption, ref flag3);
						TransactionFlowBindingElementImporter.TrackAgreementTransactionProtocol(ref flag2, TransactionProtocol.WSAtomicTransactionOctober2004, ref transactionProtocol, ref flag4);
					}
					else if (xmlElement3.NamespaceURI == "http://docs.oasis-open.org/ws-tx/wsat/2006/06" && xmlElement3.LocalName == "ATAssertion")
					{
						xmlElement2 = xmlElement3;
						TransactionFlowOption option3 = this.GetOption(xmlElement3, false);
						this.UpdateTransactionFlowAtribute(operation, option3);
						TransactionFlowBindingElementImporter.TrackAgreement(ref flag, option3, ref transactionFlowOption, ref flag3);
						TransactionFlowBindingElementImporter.TrackAgreementTransactionProtocol(ref flag2, TransactionProtocol.WSAtomicTransaction11, ref transactionProtocol, ref flag4);
					}
				}
				if (xmlElement != null)
				{
					operationBindingAssertions.Remove(xmlElement);
				}
				if (xmlElement2 != null)
				{
					operationBindingAssertions.Remove(xmlElement2);
				}
			}
			if (flag3)
			{
				TransactionFlowBindingElement transactionFlowBindingElement = this.EnsureBindingElement(context);
				transactionFlowBindingElement.Transactions = true;
				if (flag4 && flag2)
				{
					transactionFlowBindingElement.TransactionProtocol = transactionProtocol;
					return;
				}
				if (flag4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxCannotHaveDifferentTransactionProtocolsInOneBinding")));
				}
			}
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x00188128 File Offset: 0x00186328
		private void UpdateTransactionFlowAtribute(OperationDescription operation, TransactionFlowOption txFlow)
		{
			operation.Behaviors.Remove<TransactionFlowAttribute>();
			operation.Behaviors.Add(new TransactionFlowAttribute(txFlow));
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x00188147 File Offset: 0x00186347
		private static void TrackAgreement(ref bool everyoneAgrees, TransactionFlowOption option, ref TransactionFlowOption agreedOption, ref bool anOperationCares)
		{
			if (!anOperationCares)
			{
				agreedOption = option;
				anOperationCares = true;
				return;
			}
			if (option != agreedOption)
			{
				everyoneAgrees = false;
			}
		}

		// Token: 0x060068E7 RID: 26855 RVA: 0x0018815C File Offset: 0x0018635C
		private static void TrackAgreementTransactionProtocol(ref bool everyoneAgrees, TransactionProtocol option, ref TransactionProtocol agreedOption, ref bool anOperationCares)
		{
			if (!anOperationCares)
			{
				agreedOption = option;
				anOperationCares = true;
				return;
			}
			if (option != agreedOption)
			{
				everyoneAgrees = false;
			}
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x00188174 File Offset: 0x00186374
		private TransactionFlowOption GetOption(XmlElement elem, bool useLegacyNs)
		{
			TransactionFlowOption result;
			try
			{
				if (TransactionFlowBindingElementImporter.IsRealOptionalTrue(elem) || (useLegacyNs && TransactionFlowBindingElementImporter.IsLegacyOptionalTrue(elem)))
				{
					result = TransactionFlowOption.Allowed;
				}
				else
				{
					result = TransactionFlowOption.Mandatory;
				}
			}
			catch (FormatException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedBooleanAttribute", new object[]
				{
					"Optional",
					ex.Message
				})));
			}
			return result;
		}

		// Token: 0x060068E9 RID: 26857 RVA: 0x001881E0 File Offset: 0x001863E0
		private static bool IsRealOptionalTrue(XmlElement elem)
		{
			string attribute = elem.GetAttribute("Optional", "http://schemas.xmlsoap.org/ws/2004/09/policy");
			string attribute2 = elem.GetAttribute("Optional", "http://www.w3.org/ns/ws-policy");
			return XmlUtil.IsTrue(attribute) || XmlUtil.IsTrue(attribute2);
		}

		// Token: 0x060068EA RID: 26858 RVA: 0x00188220 File Offset: 0x00186420
		private static bool IsLegacyOptionalTrue(XmlElement elem)
		{
			string attribute = elem.GetAttribute("Optional", "http://schemas.xmlsoap.org/ws/2002/12/policy");
			return XmlUtil.IsTrue(attribute);
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x00188244 File Offset: 0x00186444
		private TransactionFlowBindingElement EnsureBindingElement(PolicyConversionContext context)
		{
			TransactionFlowBindingElement transactionFlowBindingElement = context.BindingElements.Find<TransactionFlowBindingElement>();
			if (transactionFlowBindingElement == null)
			{
				transactionFlowBindingElement = new TransactionFlowBindingElement(false);
				context.BindingElements.Add(transactionFlowBindingElement);
			}
			return transactionFlowBindingElement;
		}
	}
}

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x0200040E RID: 1038
	internal class MessageContractImporter
	{
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x0009371C File Offset: 0x0009191C
		private Dictionary<System.Web.Services.Description.Message, IList<string>> BodyPartsTable
		{
			get
			{
				if (this.bodyPartsTable == null)
				{
					this.bodyPartsTable = new Dictionary<System.Web.Services.Description.Message, IList<string>>();
				}
				return this.bodyPartsTable;
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00093738 File Offset: 0x00091938
		internal static void ImportMessageBinding(WsdlImporter importer, WsdlEndpointConversionContext endpointContext, Type schemaImporterType)
		{
			bool flag = MessageContractImporter.IsReferencedContract(importer, endpointContext);
			MessageContractImporter.MarkSoapExtensionsAsHandled(endpointContext.WsdlBinding);
			foreach (object obj in endpointContext.WsdlBinding.Operations)
			{
				OperationBinding operationBinding = (OperationBinding)obj;
				OperationDescription operationDescription = endpointContext.GetOperationDescription(operationBinding);
				if (flag || MessageContractImporter.OperationHasBeenHandled(operationDescription))
				{
					MessageContractImporter.MarkSoapExtensionsAsHandled(operationBinding);
					if (operationBinding.Input != null)
					{
						MessageContractImporter.MarkSoapExtensionsAsHandled(operationBinding.Input);
					}
					if (operationBinding.Output != null)
					{
						MessageContractImporter.MarkSoapExtensionsAsHandled(operationBinding.Output);
					}
					foreach (object obj2 in operationBinding.Faults)
					{
						MessageBinding item = (MessageBinding)obj2;
						MessageContractImporter.MarkSoapExtensionsAsHandled(item);
					}
				}
			}
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x0009383C File Offset: 0x00091A3C
		private static bool OperationHasBeenHandled(OperationDescription operation)
		{
			return operation.Behaviors.Find<IOperationContractGenerationExtension>() != null;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0009384C File Offset: 0x00091A4C
		private static bool IsReferencedContract(WsdlImporter importer, WsdlEndpointConversionContext endpointContext)
		{
			return importer.KnownContracts.ContainsValue(endpointContext.Endpoint.Contract);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00093864 File Offset: 0x00091A64
		private static void MarkSoapExtensionsAsHandled(NamedItem item)
		{
			foreach (object obj in item.Extensions)
			{
				ServiceDescriptionFormatExtension serviceDescriptionFormatExtension = obj as ServiceDescriptionFormatExtension;
				if (serviceDescriptionFormatExtension != null && MessageContractImporter.IsSoapBindingExtension(serviceDescriptionFormatExtension))
				{
					serviceDescriptionFormatExtension.Handled = true;
				}
				else if (SoapHelper.IsSoapFaultBinding(obj as XmlElement))
				{
					serviceDescriptionFormatExtension.Handled = true;
				}
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000938E0 File Offset: 0x00091AE0
		private static bool IsSoapBindingExtension(ServiceDescriptionFormatExtension ext)
		{
			return ext is SoapBinding || ext is SoapBodyBinding || ext is SoapHeaderBinding || ext is SoapOperationBinding || ext is SoapFaultBinding || ext is SoapHeaderFaultBinding;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00093915 File Offset: 0x00091B15
		internal static void ImportMessageContract(WsdlImporter importer, WsdlContractConversionContext contractContext, MessageContractImporter.SchemaImporter schemaImporter)
		{
			new MessageContractImporter(importer, contractContext, schemaImporter).ImportMessageContract();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00093924 File Offset: 0x00091B24
		private MessageContractImporter(WsdlImporter importer, WsdlContractConversionContext contractContext, MessageContractImporter.SchemaImporter schemaImporter)
		{
			this.contractContext = contractContext;
			this.importer = importer;
			this.allSchemas = MessageContractImporter.GatherSchemas(importer);
			this.schemaImporter = schemaImporter;
			object obj;
			if (this.importer.State.TryGetValue(typeof(FaultImportOptions), out obj))
			{
				this.faultImportOptions = (FaultImportOptions)obj;
				return;
			}
			this.faultImportOptions = new FaultImportOptions();
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x0009398E File Offset: 0x00091B8E
		private XmlSchemaSet AllSchemas
		{
			get
			{
				return this.allSchemas;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x00093996 File Offset: 0x00091B96
		private MessageContractImporter.SchemaImporter CurrentSchemaImporter
		{
			get
			{
				return this.schemaImporter;
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0009399E File Offset: 0x00091B9E
		internal void AddWarning(string message)
		{
			this.AddError(message, true);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000939A8 File Offset: 0x00091BA8
		private void AddError(string message)
		{
			this.AddError(message, false);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000939B4 File Offset: 0x00091BB4
		private void AddError(string message, bool isWarning)
		{
			MetadataConversionError item = new MetadataConversionError(message, isWarning);
			if (!this.importer.Errors.Contains(item))
			{
				this.importer.Errors.Add(item);
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000939F0 File Offset: 0x00091BF0
		private void TraceImportInformation(OperationDescription operation)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(2)
				{
					{
						"Operation",
						operation.Name
					},
					{
						"Format",
						this.CurrentSchemaImporter.GetFormatName()
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Information, 524354, SR.GetString("TraceCodeCannotBeImportedInCurrentFormat"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00093A50 File Offset: 0x00091C50
		private void ImportMessageContract()
		{
			if (this.contractContext.Contract.Operations.Count <= 0)
			{
				return;
			}
			this.CurrentSchemaImporter.PreprocessSchema();
			bool flag = true;
			MessageContractImporter.OperationInfo[] array = new MessageContractImporter.OperationInfo[this.contractContext.Contract.Operations.Count];
			int num = 0;
			foreach (OperationDescription operation in this.contractContext.Contract.Operations)
			{
				MessageContractImporter.OperationInfo operationInfo;
				if (!this.CanImportOperation(operation, out operationInfo))
				{
					this.TraceImportInformation(operation);
					flag = false;
					break;
				}
				array[num++] = operationInfo;
			}
			if (flag)
			{
				num = 0;
				foreach (OperationDescription operation2 in this.contractContext.Contract.Operations)
				{
					this.ImportOperationContract(operation2, array[num++]);
				}
			}
			this.CurrentSchemaImporter.PostprocessSchema(flag);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00093B6C File Offset: 0x00091D6C
		private bool CanImportOperation(OperationDescription operation, out MessageContractImporter.OperationInfo operationInfo)
		{
			operationInfo = null;
			if (MessageContractImporter.OperationHasBeenHandled(operation))
			{
				return false;
			}
			Operation operation2 = this.contractContext.GetOperation(operation);
			Collection<OperationBinding> operationBindings = this.contractContext.GetOperationBindings(operation2);
			return this.CanImportOperation(operation, operation2, operationBindings, out operationInfo) && this.CanImportFaults(operation2, operation);
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00093BB8 File Offset: 0x00091DB8
		private bool CanImportFaults(Operation operation, OperationDescription description)
		{
			if (!this.faultImportOptions.UseMessageFormat)
			{
				return true;
			}
			foreach (object obj in operation.Faults)
			{
				OperationFault fault = (OperationFault)obj;
				if (!this.CanImportFault(fault, description))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x00093C2C File Offset: 0x00091E2C
		private bool CanImportFault(OperationFault fault, OperationDescription description)
		{
			XmlSchemaElement detailElement;
			XmlQualifiedName detailElementTypeName;
			XmlQualifiedName xmlQualifiedName;
			return this.ValidateFault(fault, description, out detailElement, out detailElementTypeName, out xmlQualifiedName) && this.CurrentSchemaImporter.CanImportFault(detailElement, detailElementTypeName);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00093C58 File Offset: 0x00091E58
		private void ImportOperationContract(OperationDescription operation, MessageContractImporter.OperationInfo operationInfo)
		{
			Operation operation2 = this.contractContext.GetOperation(operation);
			Collection<OperationBinding> operationBindings = this.contractContext.GetOperationBindings(operation2);
			bool isReply = false;
			foreach (object obj in operation2.Messages)
			{
				OperationMessage wsdlOperationMessage = (OperationMessage)obj;
				this.ImportMessage(wsdlOperationMessage, isReply, operationInfo.IsEncoded, operationInfo.AreAllMessagesWrapped);
				isReply = true;
			}
			if (operationInfo.Style == OperationFormatStyle.Rpc)
			{
				MessageContractImporter.SetWrapperName(operation);
			}
			this.CurrentSchemaImporter.SetOperationStyle(operation, operationInfo.Style);
			this.CurrentSchemaImporter.SetOperationIsEncoded(operation, operationInfo.IsEncoded);
			this.CurrentSchemaImporter.SetOperationSupportFaults(operation, this.faultImportOptions.UseMessageFormat);
			this.ImportFaults(operation2, operation, operationInfo.IsEncoded);
			foreach (OperationBinding operationBinding in operationBindings)
			{
				foreach (MessageDescription messageDescription in operation.Messages)
				{
					OperationMessage operationMessage = this.contractContext.GetOperationMessage(messageDescription);
					ServiceDescriptionCollection serviceDescriptions = operationMessage.Operation.PortType.ServiceDescription.ServiceDescriptions;
					System.Web.Services.Description.Message message = serviceDescriptions.GetMessage(operationMessage.Message);
					MessageBinding messageBinding = (messageDescription.Direction == MessageDirection.Input) ? operationBinding.Input : operationBinding.Output;
					if (messageBinding != null)
					{
						this.ImportMessageBinding(messageBinding, message, messageDescription, operationInfo.Style, operationInfo.IsEncoded);
					}
				}
			}
			operation.Behaviors.Add(this.CurrentSchemaImporter.GetOperationGenerator());
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00093E34 File Offset: 0x00092034
		private bool CanImportOperation(OperationDescription operation, Operation wsdlOperation, Collection<OperationBinding> operationBindings, out MessageContractImporter.OperationInfo operationInfo)
		{
			operationInfo = null;
			OperationFormatStyle operationFormatStyle = OperationFormatStyle.Document;
			bool isEncoded = false;
			bool areAllMessagesWrapped = true;
			MessageContractImporter.StyleAndUse? styleAndUse = null;
			ServiceDescriptionCollection serviceDescriptions = wsdlOperation.PortType.ServiceDescription.ServiceDescriptions;
			OperationBinding operationBinding = null;
			foreach (OperationBinding operationBinding2 in operationBindings)
			{
				OperationFormatStyle style = MessageContractImporter.GetStyle(operationBinding2);
				bool? flag = null;
				foreach (MessageDescription messageDescription in operation.Messages)
				{
					OperationMessage operationMessage = this.contractContext.GetOperationMessage(messageDescription);
					if (operationMessage.Message.IsEmpty)
					{
						if (operationMessage is OperationInput)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlOperationInputNeedsMessageAttribute2", new object[]
							{
								wsdlOperation.Name,
								wsdlOperation.PortType.Name
							})));
						}
						if (operationMessage is OperationOutput)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlOperationOutputNeedsMessageAttribute2", new object[]
							{
								wsdlOperation.Name,
								wsdlOperation.PortType.Name
							})));
						}
					}
					System.Web.Services.Description.Message message = serviceDescriptions.GetMessage(operationMessage.Message);
					if (message != null)
					{
						MessageBinding messageBinding = (messageDescription.Direction == MessageDirection.Input) ? operationBinding2.Input : operationBinding2.Output;
						if (messageBinding != null)
						{
							bool flag2;
							if (!this.CanImportMessageBinding(messageBinding, message, style, out flag2))
							{
								return false;
							}
							if (flag == null)
							{
								flag = new bool?(flag2);
							}
							else
							{
								bool? flag3 = flag;
								bool flag4 = flag2;
								if (!(flag3.GetValueOrDefault() == flag4 & flag3 != null))
								{
									this.AddError(SR.GetString("SFxInconsistentWsdlOperationUseInBindingMessages", new object[]
									{
										messageBinding.OperationBinding.Name,
										messageBinding.OperationBinding.Binding.Name
									}));
								}
							}
						}
					}
				}
				foreach (object obj in operationBinding2.Faults)
				{
					FaultBinding faultBinding = (FaultBinding)obj;
					bool flag5;
					if (!this.CanImportFaultBinding(faultBinding, style, out flag5))
					{
						return false;
					}
					if (flag == null)
					{
						flag = new bool?(flag5);
					}
					else
					{
						bool? flag3 = flag;
						bool flag4 = flag5;
						if (!(flag3.GetValueOrDefault() == flag4 & flag3 != null))
						{
							this.AddError(SR.GetString("SFxInconsistentWsdlOperationUseInBindingFaults", new object[]
							{
								faultBinding.OperationBinding.Name,
								faultBinding.OperationBinding.Binding.Name
							}));
						}
					}
				}
				flag = new bool?(flag.GetValueOrDefault());
				if (styleAndUse == null)
				{
					styleAndUse = new MessageContractImporter.StyleAndUse?(MessageContractImporter.GetStyleAndUse(style, flag.Value));
					operationFormatStyle = style;
					isEncoded = flag.Value;
					operationBinding = operationBinding2;
				}
				else
				{
					MessageContractImporter.StyleAndUse styleAndUse2 = MessageContractImporter.GetStyleAndUse(style, flag.Value);
					MessageContractImporter.StyleAndUse styleAndUse3 = styleAndUse2;
					MessageContractImporter.StyleAndUse? styleAndUse4 = styleAndUse;
					if (!(styleAndUse3 == styleAndUse4.GetValueOrDefault() & styleAndUse4 != null))
					{
						this.AddError(SR.GetString("SFxInconsistentWsdlOperationUseAndStyleInBinding", new object[]
						{
							operation.Name,
							operationBinding2.Binding.Name,
							MessageContractImporter.GetUse(styleAndUse2),
							MessageContractImporter.GetStyle(styleAndUse2),
							operationBinding.Binding.Name,
							MessageContractImporter.GetUse(styleAndUse.Value),
							MessageContractImporter.GetStyle(styleAndUse.Value)
						}));
					}
					MessageContractImporter.StyleAndUse styleAndUse5 = styleAndUse2;
					styleAndUse4 = styleAndUse;
					if (styleAndUse5 < styleAndUse4.GetValueOrDefault() & styleAndUse4 != null)
					{
						styleAndUse = new MessageContractImporter.StyleAndUse?(styleAndUse2);
						operationFormatStyle = style;
						isEncoded = flag.Value;
						operationBinding = operationBinding2;
					}
				}
			}
			OperationFormatStyle? operationFormatStyle2 = null;
			foreach (object obj2 in wsdlOperation.Messages)
			{
				OperationMessage operationMessage2 = (OperationMessage)obj2;
				if (operationMessage2.Message.IsEmpty)
				{
					if (operationMessage2 is OperationInput)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlOperationInputNeedsMessageAttribute2", new object[]
						{
							wsdlOperation.Name,
							wsdlOperation.PortType.Name
						})));
					}
					if (operationMessage2 is OperationOutput)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlOperationOutputNeedsMessageAttribute2", new object[]
						{
							wsdlOperation.Name,
							wsdlOperation.PortType.Name
						})));
					}
				}
				System.Web.Services.Description.Message message2 = serviceDescriptions.GetMessage(operationMessage2.Message);
				OperationFormatStyle? operationFormatStyle3;
				if (!this.CanImportMessage(message2, operationMessage2.Name, out operationFormatStyle3, ref areAllMessagesWrapped))
				{
					return false;
				}
				if (message2.Parts.Count > 0)
				{
					if (operationFormatStyle2 != null)
					{
						if (operationFormatStyle3 == null)
						{
							continue;
						}
						OperationFormatStyle? operationFormatStyle4 = operationFormatStyle3;
						OperationFormatStyle? operationFormatStyle5 = operationFormatStyle2;
						if ((operationFormatStyle4.GetValueOrDefault() == operationFormatStyle5.GetValueOrDefault() & operationFormatStyle4 != null == (operationFormatStyle5 != null)) || operationFormatStyle3.Value != OperationFormatStyle.Document)
						{
							continue;
						}
					}
					operationFormatStyle2 = operationFormatStyle3;
				}
			}
			if (styleAndUse == null)
			{
				operationFormatStyle = operationFormatStyle2.GetValueOrDefault();
			}
			else if (operationFormatStyle2 != null && operationFormatStyle2.Value != operationFormatStyle && operationFormatStyle2.Value == OperationFormatStyle.Document)
			{
				this.AddError(SR.GetString("SFxInconsistentWsdlOperationStyleInOperationMessages", new object[]
				{
					operation.Name,
					operationFormatStyle2,
					operationFormatStyle
				}));
			}
			operationInfo = new MessageContractImporter.OperationInfo(operationFormatStyle, isEncoded, areAllMessagesWrapped);
			return true;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x00094438 File Offset: 0x00092638
		private bool CanImportMessage(System.Web.Services.Description.Message wsdlMessage, string operationName, out OperationFormatStyle? inferredStyle, ref bool areAllMessagesWrapped)
		{
			MessagePartCollection parts = wsdlMessage.Parts;
			if (parts.Count == 1)
			{
				if (this.CanImportAnyMessage(parts[0]))
				{
					areAllMessagesWrapped = false;
					inferredStyle = new OperationFormatStyle?(OperationFormatStyle.Document);
					return true;
				}
				if (this.CanImportStream(parts[0], out inferredStyle, ref areAllMessagesWrapped))
				{
					return true;
				}
				if (areAllMessagesWrapped && this.CanImportWrappedMessage(parts[0]))
				{
					inferredStyle = new OperationFormatStyle?(OperationFormatStyle.Document);
					return true;
				}
				areAllMessagesWrapped = false;
			}
			inferredStyle = null;
			IList<string> list;
			this.BodyPartsTable.TryGetValue(wsdlMessage, out list);
			foreach (object obj in parts)
			{
				MessagePart messagePart = (MessagePart)obj;
				if (list == null || list.Contains(messagePart.Name))
				{
					OperationFormatStyle operationFormatStyle;
					if (!this.CurrentSchemaImporter.CanImportMessagePart(messagePart, out operationFormatStyle))
					{
						return false;
					}
					if (inferredStyle == null)
					{
						inferredStyle = new OperationFormatStyle?(operationFormatStyle);
					}
					else if (operationFormatStyle != inferredStyle.Value)
					{
						this.AddError(SR.GetString("SFxInconsistentWsdlOperationStyleInMessageParts", new object[]
						{
							operationName
						}));
					}
				}
			}
			return true;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x00094570 File Offset: 0x00092770
		private void ImportMessage(OperationMessage wsdlOperationMessage, bool isReply, bool isEncoded, bool areAllMessagesWrapped)
		{
			MessageDescription messageDescription = this.contractContext.GetMessageDescription(wsdlOperationMessage);
			OperationDescription operationDescription = this.contractContext.GetOperationDescription(wsdlOperationMessage.Operation);
			ServiceDescriptionCollection serviceDescriptions = wsdlOperationMessage.Operation.PortType.ServiceDescription.ServiceDescriptions;
			System.Web.Services.Description.Message message = serviceDescriptions.GetMessage(wsdlOperationMessage.Message);
			if (message.Parts.Count == 1)
			{
				if (this.TryImportAnyMessage(message.Parts[0], messageDescription, isReply))
				{
					return;
				}
				if (this.TryImportStream(message.Parts[0], messageDescription, isReply, areAllMessagesWrapped))
				{
					return;
				}
				if (areAllMessagesWrapped && this.TryImportWrappedMessage(messageDescription, operationDescription.Messages[0], message, isReply))
				{
					return;
				}
			}
			MessagePartCollection parts = message.Parts;
			IList<string> list;
			this.BodyPartsTable.TryGetValue(message, out list);
			string[] parameterOrder = wsdlOperationMessage.Operation.ParameterOrder;
			foreach (object obj in parts)
			{
				MessagePart messagePart = (MessagePart)obj;
				if (ValidWsdl.Check(messagePart, message, new WsdlWarningHandler(this.AddWarning)) && (list == null || list.Contains(messagePart.Name)))
				{
					bool flag = false;
					if (parameterOrder != null && isReply)
					{
						flag = (Array.IndexOf<string>(parameterOrder, messagePart.Name) == -1);
					}
					MessagePartDescription messagePartDescription = this.CurrentSchemaImporter.ImportMessagePart(messagePart, false, isEncoded);
					if (flag && messageDescription.Body.ReturnValue == null)
					{
						messageDescription.Body.ReturnValue = messagePartDescription;
					}
					else
					{
						messageDescription.Body.Parts.Add(messagePartDescription);
					}
				}
			}
			if (isReply && messageDescription.Body.ReturnValue == null && messageDescription.Body.Parts.Count > 0 && !this.CheckIsRef(operationDescription.Messages[0], messageDescription.Body.Parts[0]))
			{
				messageDescription.Body.ReturnValue = messageDescription.Body.Parts[0];
				messageDescription.Body.Parts.RemoveAt(0);
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00094790 File Offset: 0x00092990
		private static MessageContractImporter.StyleAndUse GetStyleAndUse(OperationFormatStyle style, bool isEncoded)
		{
			if (style == OperationFormatStyle.Document)
			{
				if (!isEncoded)
				{
					return MessageContractImporter.StyleAndUse.DocumentLiteral;
				}
				return MessageContractImporter.StyleAndUse.DocumentEncoded;
			}
			else
			{
				if (!isEncoded)
				{
					return MessageContractImporter.StyleAndUse.RpcLiteral;
				}
				return MessageContractImporter.StyleAndUse.RpcEncoded;
			}
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000947A2 File Offset: 0x000929A2
		private static string GetStyle(MessageContractImporter.StyleAndUse styleAndUse)
		{
			if (styleAndUse != MessageContractImporter.StyleAndUse.RpcLiteral && styleAndUse != MessageContractImporter.StyleAndUse.RpcEncoded)
			{
				return "document";
			}
			return "rpc";
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000947B7 File Offset: 0x000929B7
		private static string GetUse(MessageContractImporter.StyleAndUse styleAndUse)
		{
			if (styleAndUse != MessageContractImporter.StyleAndUse.RpcEncoded && styleAndUse != MessageContractImporter.StyleAndUse.DocumentEncoded)
			{
				return "literal";
			}
			return "encoded";
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000947CC File Offset: 0x000929CC
		private static void SetWrapperName(OperationDescription operation)
		{
			MessageDescriptionCollection messages = operation.Messages;
			if (messages != null && messages.Count > 0)
			{
				MessageDescription messageDescription = messages[0];
				if (messageDescription != null)
				{
					messageDescription.Body.WrapperName = operation.Name;
					messageDescription.Body.WrapperNamespace = operation.DeclaringContract.Namespace;
				}
				if (messages.Count > 1)
				{
					MessageDescription messageDescription2 = messages[1];
					if (messageDescription2 != null)
					{
						messageDescription2.Body.WrapperName = TypeLoader.GetBodyWrapperResponseName(operation.Name).EncodedName;
						messageDescription2.Body.WrapperNamespace = operation.DeclaringContract.Namespace;
					}
				}
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00094868 File Offset: 0x00092A68
		private void ImportFaults(Operation operation, OperationDescription description, bool isEncoded)
		{
			foreach (object obj in operation.Faults)
			{
				OperationFault fault = (OperationFault)obj;
				this.ImportFault(fault, description, isEncoded);
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000948C4 File Offset: 0x00092AC4
		private void ImportFault(OperationFault fault, OperationDescription description, bool isEncoded)
		{
			XmlSchemaElement element;
			XmlQualifiedName xmlQualifiedName;
			XmlQualifiedName xmlQualifiedName2;
			if (!this.ValidateFault(fault, description, out element, out xmlQualifiedName, out xmlQualifiedName2))
			{
				return;
			}
			MessageContractImporter.SchemaImporter schemaImporter;
			if (this.faultImportOptions.UseMessageFormat)
			{
				schemaImporter = this.CurrentSchemaImporter;
			}
			else
			{
				schemaImporter = MessageContractImporter.DataContractSerializerSchemaImporter.Get(this.importer);
			}
			CodeTypeReference detailTypeReference;
			if (MessageContractImporter.IsNullOrEmpty(xmlQualifiedName))
			{
				detailTypeReference = schemaImporter.ImportFaultElement(xmlQualifiedName2, element, isEncoded);
			}
			else
			{
				detailTypeReference = schemaImporter.ImportFaultType(xmlQualifiedName2, xmlQualifiedName, isEncoded);
			}
			FaultDescription faultDescription = this.contractContext.GetFaultDescription(fault);
			faultDescription.DetailTypeReference = detailTypeReference;
			faultDescription.ElementName = new XmlName(xmlQualifiedName2.Name, true);
			faultDescription.Namespace = xmlQualifiedName2.Namespace;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x0009495C File Offset: 0x00092B5C
		private bool ValidateFault(OperationFault fault, OperationDescription description, out XmlSchemaElement detailElement, out XmlQualifiedName detailElementTypeName, out XmlQualifiedName detailElementQname)
		{
			detailElement = null;
			detailElementTypeName = null;
			detailElementQname = null;
			ServiceDescriptionCollection serviceDescriptions = fault.Operation.PortType.ServiceDescription.ServiceDescriptions;
			if (fault.Message.IsEmpty)
			{
				this.TraceFaultCannotBeImported(fault.Name, description.Name, SR.GetString("SFxWsdlOperationFaultNeedsMessageAttribute2", new object[]
				{
					fault.Name,
					fault.Operation.PortType.Name
				}));
				description.Faults.Remove(this.contractContext.GetFaultDescription(fault));
				return false;
			}
			System.Web.Services.Description.Message message = serviceDescriptions.GetMessage(fault.Message);
			if (message.Parts.Count != 1)
			{
				this.TraceFaultCannotBeImported(fault.Name, description.Name, SR.GetString("UnsupportedWSDLOnlyOneMessage"));
				description.Faults.Remove(this.contractContext.GetFaultDescription(fault));
				return false;
			}
			MessagePart messagePart = message.Parts[0];
			detailElementQname = messagePart.Element;
			if (MessageContractImporter.IsNullOrEmpty(detailElementQname) || !MessageContractImporter.IsNullOrEmpty(messagePart.Type))
			{
				this.TraceFaultCannotBeImported(fault.Name, description.Name, SR.GetString("UnsupportedWSDLTheFault"));
				description.Faults.Remove(this.contractContext.GetFaultDescription(fault));
				return false;
			}
			detailElement = MessageContractImporter.FindSchemaElement(this.AllSchemas, detailElementQname);
			detailElementTypeName = MessageContractImporter.GetTypeName(detailElement);
			return true;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00094ABE File Offset: 0x00092CBE
		private bool CanImportAnyMessage(MessagePart part)
		{
			return MessageContractImporter.CheckPart(part.Type, DataContractSerializerMessageContractImporter.GenericMessageTypeName);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00094AD0 File Offset: 0x00092CD0
		private bool TryImportAnyMessage(MessagePart part, MessageDescription description, bool isReply)
		{
			return MessageContractImporter.CheckAndAddPart(part.Type, DataContractSerializerMessageContractImporter.GenericMessageTypeName, part.Name, string.Empty, typeof(System.ServiceModel.Channels.Message), description, isReply);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00094AFC File Offset: 0x00092CFC
		private bool CanImportStream(MessagePart part, out OperationFormatStyle? style, ref bool areAllMessagesWrapped)
		{
			style = new OperationFormatStyle?(OperationFormatStyle.Document);
			if (areAllMessagesWrapped && this.IsWrapperPart(part))
			{
				string text;
				XmlSchemaForm xmlSchemaForm;
				XmlSchemaComplexType elementComplexType = MessageContractImporter.GetElementComplexType(part.Element, this.allSchemas, out text, out xmlSchemaForm);
				if (elementComplexType != null)
				{
					XmlSchemaSequence rootSequence = MessageContractImporter.GetRootSequence(elementComplexType);
					if (rootSequence != null && rootSequence.Items.Count == 1 && rootSequence.Items[0] is XmlSchemaElement)
					{
						return MessageContractImporter.CheckPart(((XmlSchemaElement)rootSequence.Items[0]).SchemaTypeName, DataContractSerializerMessageContractImporter.StreamBodyTypeName);
					}
				}
				return false;
			}
			areAllMessagesWrapped = false;
			XmlQualifiedName xmlQualifiedName = part.Type;
			style = new OperationFormatStyle?(OperationFormatStyle.Rpc);
			if (MessageContractImporter.IsNullOrEmpty(xmlQualifiedName))
			{
				if (MessageContractImporter.IsNullOrEmpty(part.Element))
				{
					return false;
				}
				style = new OperationFormatStyle?(OperationFormatStyle.Document);
				xmlQualifiedName = MessageContractImporter.GetTypeName(MessageContractImporter.FindSchemaElement(this.allSchemas, part.Element));
			}
			return MessageContractImporter.CheckPart(xmlQualifiedName, DataContractSerializerMessageContractImporter.StreamBodyTypeName);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00094BEC File Offset: 0x00092DEC
		private bool TryImportStream(MessagePart part, MessageDescription description, bool isReply, bool areAllMessagesWrapped)
		{
			string ns = string.Empty;
			if (!areAllMessagesWrapped || !this.IsWrapperPart(part))
			{
				XmlQualifiedName xmlQualifiedName = part.Type;
				if (MessageContractImporter.IsNullOrEmpty(xmlQualifiedName))
				{
					if (MessageContractImporter.IsNullOrEmpty(part.Element))
					{
						return false;
					}
					ns = part.Element.Namespace;
					xmlQualifiedName = MessageContractImporter.GetTypeName(MessageContractImporter.FindSchemaElement(this.allSchemas, part.Element));
				}
				return MessageContractImporter.CheckAndAddPart(xmlQualifiedName, DataContractSerializerMessageContractImporter.StreamBodyTypeName, part.Name, ns, typeof(Stream), description, isReply);
			}
			XmlSchemaForm elementFormDefault;
			XmlSchemaSequence rootSequence = MessageContractImporter.GetRootSequence(MessageContractImporter.GetElementComplexType(part.Element, this.allSchemas, out ns, out elementFormDefault));
			if (rootSequence == null || rootSequence.Items.Count != 1 || !(rootSequence.Items[0] is XmlSchemaElement))
			{
				return false;
			}
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)rootSequence.Items[0];
			description.Body.WrapperName = new XmlName(part.Element.Name, true).EncodedName;
			description.Body.WrapperNamespace = part.Element.Namespace;
			if (xmlSchemaElement.SchemaTypeName.IsEmpty && xmlSchemaElement.RefName != null)
			{
				return MessageContractImporter.CheckAndAddPart(xmlSchemaElement.ElementSchemaType.QualifiedName, DataContractSerializerMessageContractImporter.StreamBodyTypeName, xmlSchemaElement.RefName.Name, MessageContractImporter.GetLocalElementNamespace(xmlSchemaElement.RefName.Namespace, xmlSchemaElement, elementFormDefault), typeof(Stream), description, isReply);
			}
			return MessageContractImporter.CheckAndAddPart(xmlSchemaElement.SchemaTypeName, DataContractSerializerMessageContractImporter.StreamBodyTypeName, xmlSchemaElement.Name, MessageContractImporter.GetLocalElementNamespace(ns, xmlSchemaElement, elementFormDefault), typeof(Stream), description, isReply);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00094D93 File Offset: 0x00092F93
		private bool CanImportWrappedMessage(MessagePart wsdlPart)
		{
			return this.IsWrapperPart(wsdlPart) && this.CurrentSchemaImporter.CanImportWrapperElement(wsdlPart.Element);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00094DB4 File Offset: 0x00092FB4
		private bool TryImportWrappedMessage(MessageDescription messageDescription, MessageDescription requestMessage, System.Web.Services.Description.Message wsdlMessage, bool isReply)
		{
			MessagePart messagePart = wsdlMessage.Parts[0];
			if (!this.CanImportWrappedMessage(messagePart))
			{
				return false;
			}
			XmlQualifiedName element = messagePart.Element;
			MessagePartDescription[] array = this.CurrentSchemaImporter.ImportWrapperElement(element);
			if (array == null)
			{
				return false;
			}
			messageDescription.Body.WrapperName = new XmlName(element.Name, true).EncodedName;
			messageDescription.Body.WrapperNamespace = element.Namespace;
			if (array.Length != 0)
			{
				int i = 0;
				if (isReply && messageDescription.Body.ReturnValue == null && !this.CheckIsRef(requestMessage, array[0]))
				{
					messageDescription.Body.ReturnValue = array[0];
					i = 1;
				}
				while (i < array.Length)
				{
					MessagePartDescription item = array[i];
					messageDescription.Body.Parts.Add(item);
					i++;
				}
			}
			return true;
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00094E7C File Offset: 0x0009307C
		private bool IsWrapperPart(MessagePart wsdlPart)
		{
			bool flag = false;
			object obj = null;
			if (this.importer.State.TryGetValue(typeof(WrappedOptions), out obj))
			{
				flag = ((WrappedOptions)obj).WrappedFlag;
			}
			return wsdlPart.Name == "parameters" && !MessageContractImporter.IsNullOrEmpty(wsdlPart.Element) && !flag;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00094EDC File Offset: 0x000930DC
		private bool CheckIsRef(MessageDescription requestMessage, MessagePartDescription part)
		{
			foreach (MessagePartDescription x in requestMessage.Body.Parts)
			{
				if (this.CompareMessageParts(x, part))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00094F38 File Offset: 0x00093138
		private bool CompareMessageParts(MessagePartDescription x, MessagePartDescription y)
		{
			return x.Name == y.Name && x.Namespace == y.Namespace;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00094F60 File Offset: 0x00093160
		private static MessagePart FindPartByName(System.Web.Services.Description.Message message, string name)
		{
			foreach (object obj in message.Parts)
			{
				MessagePart messagePart = (MessagePart)obj;
				if (messagePart.Name == name)
				{
					return messagePart;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlMessageDoesNotContainPart3", new object[]
			{
				name,
				message.Name,
				message.ServiceDescription.TargetNamespace
			})));
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00095000 File Offset: 0x00093200
		private static XmlSchemaElement FindSchemaElement(XmlSchemaSet schemaSet, XmlQualifiedName elementName)
		{
			XmlSchema xmlSchema;
			return MessageContractImporter.FindSchemaElement(schemaSet, elementName, out xmlSchema);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x00095018 File Offset: 0x00093218
		private static XmlSchemaElement FindSchemaElement(XmlSchemaSet schemaSet, XmlQualifiedName elementName, out XmlSchema containingSchema)
		{
			XmlSchemaElement xmlSchemaElement = null;
			containingSchema = null;
			foreach (object obj in MessageContractImporter.GetSchema(schemaSet, elementName.Namespace))
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				xmlSchemaElement = (XmlSchemaElement)xmlSchema.Elements[elementName];
				if (xmlSchemaElement != null)
				{
					containingSchema = xmlSchema;
					break;
				}
			}
			if (xmlSchemaElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxSchemaDoesNotContainElement", new object[]
				{
					elementName.Name,
					elementName.Namespace
				})));
			}
			return xmlSchemaElement;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000950C8 File Offset: 0x000932C8
		private static XmlSchemaType FindSchemaType(XmlSchemaSet schemaSet, XmlQualifiedName typeName)
		{
			if (typeName.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return null;
			}
			XmlSchema xmlSchema;
			return MessageContractImporter.FindSchemaType(schemaSet, typeName, out xmlSchema);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000950F4 File Offset: 0x000932F4
		private static XmlSchemaType FindSchemaType(XmlSchemaSet schemaSet, XmlQualifiedName typeName, out XmlSchema containingSchema)
		{
			containingSchema = null;
			if (StockSchemas.IsKnownSchema(typeName.Namespace))
			{
				return null;
			}
			XmlSchemaType xmlSchemaType = null;
			foreach (object obj in MessageContractImporter.GetSchema(schemaSet, typeName.Namespace))
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				xmlSchemaType = (XmlSchemaType)xmlSchema.SchemaTypes[typeName];
				if (xmlSchemaType != null)
				{
					containingSchema = xmlSchema;
					break;
				}
			}
			if (xmlSchemaType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxSchemaDoesNotContainType", new object[]
				{
					typeName.Name,
					typeName.Namespace
				})));
			}
			return xmlSchemaType;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000951B0 File Offset: 0x000933B0
		private static XmlSchemaSet GatherSchemas(WsdlImporter importer)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.XmlResolver = null;
			foreach (object obj in importer.WsdlDocuments)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				XmlQualifiedName[] array = serviceDescription.Namespaces.ToArray();
				if (serviceDescription.Types != null && serviceDescription.Types.Schemas != null)
				{
					foreach (object obj2 in serviceDescription.Types.Schemas)
					{
						XmlSchema xmlSchema = (XmlSchema)obj2;
						XmlSerializerNamespaces namespaces = xmlSchema.Namespaces;
						XmlQualifiedName[] array2 = namespaces.ToArray();
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						foreach (XmlQualifiedName xmlQualifiedName in array2)
						{
							dictionary.Add(xmlQualifiedName.Name, null);
						}
						foreach (XmlQualifiedName xmlQualifiedName2 in array)
						{
							if (!dictionary.ContainsKey(xmlQualifiedName2.Name))
							{
								namespaces.Add(xmlQualifiedName2.Name, xmlQualifiedName2.Namespace);
							}
						}
						if (xmlSchema.Items.Count > 0)
						{
							xmlSchemaSet.Add(xmlSchema);
						}
						else
						{
							foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
							{
								XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
								if (xmlSchemaExternal.Schema != null)
								{
									xmlSchemaSet.Add(xmlSchemaExternal.Schema);
								}
							}
						}
					}
				}
			}
			xmlSchemaSet.Add(importer.XmlSchemas);
			return xmlSchemaSet;
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000953C8 File Offset: 0x000935C8
		private static void CollectEncodedAndLiteralSchemas(ServiceDescriptionCollection serviceDescriptions, XmlSchemas encodedSchemas, XmlSchemas literalSchemas, XmlSchemaSet allSchemas)
		{
			XmlSchema xmlSchema = StockSchemas.CreateWsdl();
			XmlSchema xmlSchema2 = StockSchemas.CreateSoap();
			XmlSchema xmlSchema3 = StockSchemas.CreateSoapEncoding();
			Hashtable hashtable = new Hashtable();
			if (!allSchemas.Contains(xmlSchema.TargetNamespace))
			{
				hashtable[xmlSchema2] = xmlSchema;
			}
			if (!allSchemas.Contains(xmlSchema2.TargetNamespace))
			{
				hashtable[xmlSchema2] = xmlSchema2;
			}
			if (!allSchemas.Contains(xmlSchema3.TargetNamespace))
			{
				hashtable[xmlSchema3] = xmlSchema3;
			}
			foreach (object obj in serviceDescriptions)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.Messages)
				{
					System.Web.Services.Description.Message message = (System.Web.Services.Description.Message)obj2;
					foreach (object obj3 in message.Parts)
					{
						MessagePart messagePart = (MessagePart)obj3;
						bool isEncoded;
						bool isLiteral;
						MessageContractImporter.FindUse(messagePart, out isEncoded, out isLiteral);
						if (messagePart.Element != null && !messagePart.Element.IsEmpty)
						{
							XmlSchemaElement xmlSchemaElement = MessageContractImporter.FindSchemaElement(allSchemas, messagePart.Element);
							if (xmlSchemaElement != null)
							{
								MessageContractImporter.AddSchema(xmlSchemaElement.Parent as XmlSchema, isEncoded, isLiteral, encodedSchemas, literalSchemas, hashtable);
								if (xmlSchemaElement.SchemaTypeName != null && !xmlSchemaElement.SchemaTypeName.IsEmpty)
								{
									XmlSchemaType xmlSchemaType = MessageContractImporter.FindSchemaType(allSchemas, xmlSchemaElement.SchemaTypeName);
									if (xmlSchemaType != null)
									{
										MessageContractImporter.AddSchema(xmlSchemaType.Parent as XmlSchema, isEncoded, isLiteral, encodedSchemas, literalSchemas, hashtable);
									}
								}
							}
						}
						if (messagePart.Type != null && !messagePart.Type.IsEmpty)
						{
							XmlSchemaType xmlSchemaType2 = MessageContractImporter.FindSchemaType(allSchemas, messagePart.Type);
							if (xmlSchemaType2 != null)
							{
								MessageContractImporter.AddSchema(xmlSchemaType2.Parent as XmlSchema, isEncoded, isLiteral, encodedSchemas, literalSchemas, hashtable);
							}
						}
					}
				}
			}
			Hashtable hashtable2;
			foreach (XmlSchemas xmlSchemas in new XmlSchemas[]
			{
				encodedSchemas,
				literalSchemas
			})
			{
				hashtable2 = new Hashtable();
				foreach (object obj4 in xmlSchemas)
				{
					XmlSchema schema = (XmlSchema)obj4;
					MessageContractImporter.AddImport(schema, hashtable2, allSchemas);
				}
				foreach (object obj5 in hashtable2.Keys)
				{
					XmlSchema xmlSchema4 = (XmlSchema)obj5;
					if (hashtable[xmlSchema4] == null && !xmlSchemas.Contains(xmlSchema4))
					{
						xmlSchemas.Add(xmlSchema4);
					}
				}
			}
			hashtable2 = new Hashtable();
			foreach (object obj6 in allSchemas.Schemas())
			{
				XmlSchema schema2 = (XmlSchema)obj6;
				if (!encodedSchemas.Contains(schema2) && !literalSchemas.Contains(schema2))
				{
					MessageContractImporter.AddImport(schema2, hashtable2, allSchemas);
				}
			}
			foreach (object obj7 in hashtable2.Keys)
			{
				XmlSchema xmlSchema5 = (XmlSchema)obj7;
				if (hashtable[xmlSchema5] == null)
				{
					if (!encodedSchemas.Contains(xmlSchema5))
					{
						encodedSchemas.Add(xmlSchema5);
					}
					if (!literalSchemas.Contains(xmlSchema5))
					{
						literalSchemas.Add(xmlSchema5);
					}
				}
			}
			if (encodedSchemas.Count > 0)
			{
				foreach (object obj8 in hashtable.Values)
				{
					XmlSchema schema3 = (XmlSchema)obj8;
					encodedSchemas.AddReference(schema3);
				}
			}
			if (literalSchemas.Count > 0)
			{
				foreach (object obj9 in hashtable.Values)
				{
					XmlSchema schema4 = (XmlSchema)obj9;
					literalSchemas.AddReference(schema4);
				}
			}
			MessageContractImporter.AddSoapEncodingSchemaIfNeeded(literalSchemas);
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x00095900 File Offset: 0x00093B00
		private static void AddSoapEncodingSchemaIfNeeded(XmlSchemas schemas)
		{
			XmlSchema xmlSchema = StockSchemas.CreateFakeXsdSchema();
			foreach (object obj in schemas)
			{
				XmlSchema xmlSchema2 = (XmlSchema)obj;
				foreach (object obj2 in xmlSchema2.Includes)
				{
					XmlSchemaImport xmlSchemaImport = obj2 as XmlSchemaImport;
					if (xmlSchemaImport != null && xmlSchemaImport.Namespace == xmlSchema.TargetNamespace)
					{
						schemas.Add(xmlSchema);
						return;
					}
				}
			}
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000959C4 File Offset: 0x00093BC4
		private static void AddImport(XmlSchema schema, Hashtable imports, XmlSchemaSet allSchemas)
		{
			if (schema == null || imports[schema] != null)
			{
				return;
			}
			imports.Add(schema, schema);
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal is XmlSchemaImport)
				{
					XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)xmlSchemaExternal;
					foreach (object obj in allSchemas.Schemas(xmlSchemaImport.Namespace))
					{
						XmlSchema schema2 = (XmlSchema)obj;
						MessageContractImporter.AddImport(schema2, imports, allSchemas);
					}
				}
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x00095A94 File Offset: 0x00093C94
		private static void AddSchema(XmlSchema schema, bool isEncoded, bool isLiteral, XmlSchemas encodedSchemas, XmlSchemas literalSchemas, Hashtable references)
		{
			if (schema != null)
			{
				if (isEncoded && !encodedSchemas.Contains(schema))
				{
					if (references.Contains(schema))
					{
						encodedSchemas.AddReference(schema);
					}
					else
					{
						encodedSchemas.Add(schema);
					}
				}
				if (isLiteral && !literalSchemas.Contains(schema))
				{
					if (references.Contains(schema))
					{
						literalSchemas.AddReference(schema);
						return;
					}
					literalSchemas.Add(schema);
				}
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00095AF4 File Offset: 0x00093CF4
		private static void FindUse(MessagePart part, out bool isEncoded, out bool isLiteral)
		{
			isEncoded = false;
			isLiteral = false;
			string name = part.Message.Name;
			Operation operation = null;
			ServiceDescription serviceDescription = part.Message.ServiceDescription;
			foreach (object obj in serviceDescription.PortTypes)
			{
				PortType portType = (PortType)obj;
				foreach (object obj2 in portType.Operations)
				{
					Operation operation2 = (Operation)obj2;
					foreach (object obj3 in operation2.Messages)
					{
						OperationMessage operationMessage = (OperationMessage)obj3;
						if (operationMessage.Message.Equals(new XmlQualifiedName(part.Message.Name, serviceDescription.TargetNamespace)))
						{
							operation = operation2;
							MessageContractImporter.FindUse(operation, serviceDescription, name, ref isEncoded, ref isLiteral);
						}
					}
				}
			}
			if (operation == null)
			{
				MessageContractImporter.FindUse(null, serviceDescription, name, ref isEncoded, ref isLiteral);
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00095C48 File Offset: 0x00093E48
		private static void FindUse(Operation operation, ServiceDescription description, string messageName, ref bool isEncoded, ref bool isLiteral)
		{
			string targetNamespace = description.TargetNamespace;
			foreach (object obj in description.Bindings)
			{
				System.Web.Services.Description.Binding binding = (System.Web.Services.Description.Binding)obj;
				if (operation == null || new XmlQualifiedName(operation.PortType.Name, targetNamespace).Equals(binding.Type))
				{
					foreach (object obj2 in binding.Operations)
					{
						OperationBinding operationBinding = (OperationBinding)obj2;
						if (operationBinding.Input != null)
						{
							foreach (object obj3 in operationBinding.Input.Extensions)
							{
								if (operation != null)
								{
									SoapBodyBinding soapBodyBinding = obj3 as SoapBodyBinding;
									if (soapBodyBinding != null && operation.IsBoundBy(operationBinding))
									{
										if (soapBodyBinding.Use == SoapBindingUse.Encoded)
										{
											isEncoded = true;
										}
										else if (soapBodyBinding.Use == SoapBindingUse.Literal)
										{
											isLiteral = true;
										}
									}
								}
								else
								{
									SoapHeaderBinding soapHeaderBinding = obj3 as SoapHeaderBinding;
									if (soapHeaderBinding != null && soapHeaderBinding.Message.Name == messageName)
									{
										if (soapHeaderBinding.Use == SoapBindingUse.Encoded)
										{
											isEncoded = true;
										}
										else if (soapHeaderBinding.Use == SoapBindingUse.Literal)
										{
											isLiteral = true;
										}
									}
								}
							}
						}
						if (operationBinding.Output != null)
						{
							foreach (object obj4 in operationBinding.Output.Extensions)
							{
								if (operation != null)
								{
									if (operation.IsBoundBy(operationBinding))
									{
										SoapBodyBinding soapBodyBinding2 = obj4 as SoapBodyBinding;
										if (soapBodyBinding2 != null)
										{
											if (soapBodyBinding2.Use == SoapBindingUse.Encoded)
											{
												isEncoded = true;
											}
											else if (soapBodyBinding2.Use == SoapBindingUse.Literal)
											{
												isLiteral = true;
											}
										}
										else if (obj4 is MimeXmlBinding)
										{
											isLiteral = true;
										}
									}
								}
								else
								{
									SoapHeaderBinding soapHeaderBinding2 = obj4 as SoapHeaderBinding;
									if (soapHeaderBinding2 != null && soapHeaderBinding2.Message.Name == messageName)
									{
										if (soapHeaderBinding2.Use == SoapBindingUse.Encoded)
										{
											isEncoded = true;
										}
										else if (soapHeaderBinding2.Use == SoapBindingUse.Literal)
										{
											isLiteral = true;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x00095F00 File Offset: 0x00094100
		private static string GetLocalElementNamespace(string ns, XmlSchemaElement element, XmlSchemaForm elementFormDefault)
		{
			XmlSchemaForm xmlSchemaForm = (element.Form != XmlSchemaForm.None) ? element.Form : elementFormDefault;
			if (xmlSchemaForm != XmlSchemaForm.Qualified)
			{
				return string.Empty;
			}
			return ns;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x00095F2C File Offset: 0x0009412C
		private static IEnumerable GetSchema(XmlSchemaSet schemaSet, string ns)
		{
			ICollection collection = schemaSet.Schemas(ns);
			if (collection == null || collection.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxSchemaNotFound", new object[]
				{
					ns
				})));
			}
			return collection;
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00095F74 File Offset: 0x00094174
		private static SoapBindingStyle GetStyle(System.Web.Services.Description.Binding binding)
		{
			SoapBindingStyle result = SoapBindingStyle.Default;
			if (binding != null)
			{
				SoapBinding soapBinding = binding.Extensions.Find(typeof(SoapBinding)) as SoapBinding;
				if (soapBinding != null)
				{
					result = soapBinding.Style;
				}
			}
			return result;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00095FAC File Offset: 0x000941AC
		private static OperationFormatStyle GetStyle(OperationBinding operationBinding)
		{
			SoapBindingStyle style = MessageContractImporter.GetStyle(operationBinding.Binding);
			if (operationBinding != null)
			{
				SoapOperationBinding soapOperationBinding = operationBinding.Extensions.Find(typeof(SoapOperationBinding)) as SoapOperationBinding;
				if (soapOperationBinding != null && soapOperationBinding.Style != SoapBindingStyle.Default)
				{
					style = soapOperationBinding.Style;
				}
			}
			if (style != SoapBindingStyle.Rpc)
			{
				return OperationFormatStyle.Document;
			}
			return OperationFormatStyle.Rpc;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00095FFC File Offset: 0x000941FC
		private static XmlQualifiedName GetTypeName(XmlSchemaElement element)
		{
			if (element.SchemaType != null)
			{
				return XmlQualifiedName.Empty;
			}
			if (MessageContractImporter.IsNullOrEmpty(element.SchemaTypeName))
			{
				return MessageContractImporter.AnyType;
			}
			return element.SchemaTypeName;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x00096025 File Offset: 0x00094225
		private static bool IsNullOrEmpty(XmlQualifiedName qname)
		{
			return qname == null || qname.IsEmpty;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x00096038 File Offset: 0x00094238
		private void TraceFaultCannotBeImported(string faultName, string operationName, string message)
		{
			this.AddWarning(SR.GetString("SFxFaultCannotBeImported", new object[]
			{
				faultName,
				operationName,
				message
			}));
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0009605C File Offset: 0x0009425C
		private static bool CheckAndAddPart(XmlQualifiedName typeNameFound, XmlQualifiedName typeNameRequired, string name, string ns, Type type, MessageDescription description, bool isReply)
		{
			if (MessageContractImporter.IsNullOrEmpty(typeNameFound) || typeNameFound != typeNameRequired)
			{
				return false;
			}
			MessagePartDescription messagePartDescription = new MessagePartDescription(name, ns);
			messagePartDescription.Type = type;
			if (isReply && description.Body.ReturnValue == null)
			{
				description.Body.ReturnValue = messagePartDescription;
			}
			else
			{
				description.Body.Parts.Add(messagePartDescription);
			}
			return true;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000960C0 File Offset: 0x000942C0
		private static bool CheckPart(XmlQualifiedName typeNameFound, XmlQualifiedName typeNameRequired)
		{
			return !MessageContractImporter.IsNullOrEmpty(typeNameFound) && typeNameFound == typeNameRequired;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000960D4 File Offset: 0x000942D4
		private static XmlSchemaComplexType GetElementComplexType(XmlQualifiedName elementName, XmlSchemaSet schemaSet, out string ns, out XmlSchemaForm elementFormDefault)
		{
			XmlSchema xmlSchema;
			XmlSchemaElement xmlSchemaElement = MessageContractImporter.FindSchemaElement(schemaSet, elementName, out xmlSchema);
			ns = elementName.Namespace;
			elementFormDefault = xmlSchema.ElementFormDefault;
			XmlSchemaType xmlSchemaType;
			if (xmlSchemaElement.SchemaType != null)
			{
				xmlSchemaType = xmlSchemaElement.SchemaType;
			}
			else
			{
				XmlQualifiedName typeName = MessageContractImporter.GetTypeName(xmlSchemaElement);
				if (typeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return null;
				}
				xmlSchemaType = MessageContractImporter.FindSchemaType(schemaSet, typeName, out xmlSchema);
				ns = typeName.Namespace;
				elementFormDefault = xmlSchema.ElementFormDefault;
			}
			if (xmlSchemaType == null)
			{
				return null;
			}
			return xmlSchemaType as XmlSchemaComplexType;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0009614E File Offset: 0x0009434E
		private static XmlSchemaSequence GetRootSequence(XmlSchemaComplexType complexType)
		{
			if (complexType == null)
			{
				return null;
			}
			if (complexType.Particle == null)
			{
				return null;
			}
			return complexType.Particle as XmlSchemaSequence;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0009616C File Offset: 0x0009436C
		private bool CanImportMessageBinding(MessageBinding messageBinding, System.Web.Services.Description.Message wsdlMessage, OperationFormatStyle style, out bool isEncoded)
		{
			isEncoded = false;
			bool? flag = null;
			foreach (object obj in messageBinding.Extensions)
			{
				SoapHeaderBinding soapHeaderBinding = obj as SoapHeaderBinding;
				if (soapHeaderBinding != null)
				{
					if (!ValidWsdl.Check(soapHeaderBinding, messageBinding, new WsdlWarningHandler(this.AddWarning)))
					{
						return false;
					}
					bool flag2;
					if (!this.CanImportMessageHeaderBinding(soapHeaderBinding, wsdlMessage, style, out flag2))
					{
						return false;
					}
					if (flag == null)
					{
						flag = new bool?(flag2);
					}
					else if (flag.Value != flag2)
					{
						this.AddError(SR.GetString("SFxInconsistentWsdlOperationUseInBindingExtensions", new object[]
						{
							messageBinding.OperationBinding.Name,
							messageBinding.OperationBinding.Binding.Name
						}));
					}
				}
				else
				{
					SoapBodyBinding soapBodyBinding = obj as SoapBodyBinding;
					if (soapBodyBinding != null)
					{
						bool flag2;
						if (!this.CanImportMessageBodyBinding(soapBodyBinding, style, out flag2))
						{
							return false;
						}
						if (flag == null)
						{
							flag = new bool?(flag2);
						}
						else if (flag.Value != flag2)
						{
							this.AddError(SR.GetString("SFxInconsistentWsdlOperationUseInBindingExtensions", new object[]
							{
								messageBinding.OperationBinding.Name,
								messageBinding.OperationBinding.Binding.Name
							}));
						}
						string[] array = soapBodyBinding.Parts;
						if (array == null)
						{
							array = new string[wsdlMessage.Parts.Count];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = wsdlMessage.Parts[i].Name;
							}
						}
						bool flag3 = false;
						IList<string> list;
						if (!this.BodyPartsTable.TryGetValue(wsdlMessage, out list))
						{
							list = new List<string>();
							this.BodyPartsTable.Add(wsdlMessage, list);
							flag3 = true;
						}
						foreach (string text in array)
						{
							if (!string.IsNullOrEmpty(text))
							{
								if (flag3)
								{
									list.Add(text);
								}
								else if (!list.Contains(text))
								{
									this.AddError(SR.GetString("SFxInconsistentBindingBodyParts", new object[]
									{
										messageBinding.OperationBinding.Name,
										messageBinding.OperationBinding.Binding.Name,
										text
									}));
									list.Add(text);
								}
							}
						}
					}
				}
			}
			if (flag != null)
			{
				isEncoded = flag.Value;
			}
			return true;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00096404 File Offset: 0x00094604
		private bool CanImportFaultBinding(FaultBinding faultBinding, OperationFormatStyle style, out bool isFaultEncoded)
		{
			bool? flag = null;
			foreach (object obj in faultBinding.Extensions)
			{
				XmlElement element = obj as XmlElement;
				bool flag2;
				if (SoapHelper.IsSoapFaultBinding(element))
				{
					flag2 = SoapHelper.IsEncoded(element);
				}
				else
				{
					SoapFaultBinding soapFaultBinding = obj as SoapFaultBinding;
					if (soapFaultBinding == null || !ValidWsdl.Check(soapFaultBinding, faultBinding, new WsdlWarningHandler(this.AddWarning)))
					{
						continue;
					}
					flag2 = (soapFaultBinding.Use == SoapBindingUse.Encoded);
				}
				if (flag == null)
				{
					flag = new bool?(flag2);
				}
				else if (flag.Value != flag2)
				{
					this.AddError(SR.GetString("SFxInconsistentWsdlOperationUseInBindingExtensions", new object[]
					{
						faultBinding.OperationBinding.Name,
						faultBinding.OperationBinding.Binding.Name
					}));
				}
			}
			isFaultEncoded = flag.GetValueOrDefault();
			return this.CurrentSchemaImporter.CanImportStyleAndUse(style, isFaultEncoded);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x00096514 File Offset: 0x00094714
		private bool CanImportMessageBodyBinding(SoapBodyBinding bodyBinding, OperationFormatStyle style, out bool isEncoded)
		{
			isEncoded = (bodyBinding.Use == SoapBindingUse.Encoded);
			return this.CurrentSchemaImporter.CanImportStyleAndUse(style, isEncoded);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x00096530 File Offset: 0x00094730
		private bool CanImportMessageHeaderBinding(SoapHeaderBinding headerBinding, System.Web.Services.Description.Message wsdlMessage, OperationFormatStyle style, out bool isEncoded)
		{
			isEncoded = (headerBinding.Use == SoapBindingUse.Encoded);
			System.Web.Services.Description.Message message = wsdlMessage.ServiceDescription.ServiceDescriptions.GetMessage(headerBinding.Message);
			MessagePart messagePart = MessageContractImporter.FindPartByName(message, headerBinding.Part);
			OperationFormatStyle operationFormatStyle;
			if (!this.CurrentSchemaImporter.CanImportMessagePart(messagePart, out operationFormatStyle))
			{
				return false;
			}
			if (operationFormatStyle != style)
			{
				this.AddError(SR.GetString("SFxInconsistentWsdlOperationStyleInHeader", new object[]
				{
					messagePart.Name,
					operationFormatStyle,
					style
				}));
			}
			return this.CurrentSchemaImporter.CanImportStyleAndUse(style, isEncoded);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000965C4 File Offset: 0x000947C4
		private void ImportMessageBinding(MessageBinding messageBinding, System.Web.Services.Description.Message wsdlMessage, MessageDescription description, OperationFormatStyle style, bool isEncoded)
		{
			OperationMessage operationMessage = this.contractContext.GetOperationMessage(description);
			foreach (object obj in messageBinding.Extensions)
			{
				SoapHeaderBinding soapHeaderBinding = obj as SoapHeaderBinding;
				if (soapHeaderBinding != null)
				{
					this.ImportMessageHeaderBinding(soapHeaderBinding, wsdlMessage, description, style, isEncoded, messageBinding.OperationBinding.Name);
				}
				else
				{
					SoapBodyBinding soapBodyBinding = obj as SoapBodyBinding;
					if (soapBodyBinding != null)
					{
						this.ImportMessageBodyBinding(soapBodyBinding, wsdlMessage, description, style, isEncoded, messageBinding.OperationBinding.Name);
					}
				}
			}
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0009666C File Offset: 0x0009486C
		private void ImportMessageBodyBinding(SoapBodyBinding bodyBinding, System.Web.Services.Description.Message wsdlMessage, MessageDescription description, OperationFormatStyle style, bool isEncoded, string operationName)
		{
			if (style == OperationFormatStyle.Rpc && bodyBinding.Namespace != null)
			{
				description.Body.WrapperNamespace = bodyBinding.Namespace;
			}
			this.CurrentSchemaImporter.ValidateStyleAndUse(style, isEncoded, operationName);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000966A0 File Offset: 0x000948A0
		private void ImportMessageHeaderBinding(SoapHeaderBinding headerBinding, System.Web.Services.Description.Message wsdlMessage, MessageDescription description, OperationFormatStyle style, bool isEncoded, string operationName)
		{
			System.Web.Services.Description.Message message = wsdlMessage.ServiceDescription.ServiceDescriptions.GetMessage(headerBinding.Message);
			MessagePart part = MessageContractImporter.FindPartByName(message, headerBinding.Part);
			if (!description.Headers.Contains(this.CurrentSchemaImporter.GetPartName(part)))
			{
				description.Headers.Add((MessageHeaderDescription)this.schemaImporter.ImportMessagePart(part, true, isEncoded));
				this.CurrentSchemaImporter.ValidateStyleAndUse(style, isEncoded, operationName);
			}
		}

		// Token: 0x040021F7 RID: 8695
		private static readonly XmlQualifiedName AnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040021F8 RID: 8696
		private readonly XmlSchemaSet allSchemas;

		// Token: 0x040021F9 RID: 8697
		private readonly WsdlContractConversionContext contractContext;

		// Token: 0x040021FA RID: 8698
		private readonly WsdlImporter importer;

		// Token: 0x040021FB RID: 8699
		private MessageContractImporter.SchemaImporter schemaImporter;

		// Token: 0x040021FC RID: 8700
		private readonly FaultImportOptions faultImportOptions;

		// Token: 0x040021FD RID: 8701
		private static object schemaImporterLock = new object();

		// Token: 0x040021FE RID: 8702
		private Dictionary<System.Web.Services.Description.Message, IList<string>> bodyPartsTable;

		// Token: 0x02000BC7 RID: 3015
		private enum StyleAndUse
		{
			// Token: 0x04004206 RID: 16902
			DocumentLiteral,
			// Token: 0x04004207 RID: 16903
			RpcLiteral,
			// Token: 0x04004208 RID: 16904
			RpcEncoded,
			// Token: 0x04004209 RID: 16905
			DocumentEncoded
		}

		// Token: 0x02000BC8 RID: 3016
		internal abstract class SchemaImporter
		{
			// Token: 0x0600748D RID: 29837 RVA: 0x001B3271 File Offset: 0x001B1471
			internal SchemaImporter(WsdlImporter importer)
			{
				this.importer = importer;
				this.schemaSet = MessageContractImporter.GatherSchemas(importer);
			}

			// Token: 0x0600748E RID: 29838 RVA: 0x001B328C File Offset: 0x001B148C
			internal XmlQualifiedName GetPartName(MessagePart part)
			{
				if (!MessageContractImporter.IsNullOrEmpty(part.Element))
				{
					return part.Element;
				}
				if (!MessageContractImporter.IsNullOrEmpty(part.Type))
				{
					return new XmlQualifiedName(part.Name, string.Empty);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlPartMustHaveElementOrType", new object[]
				{
					part.Name,
					part.Message.Name,
					part.Message.Namespaces
				})));
			}

			// Token: 0x0600748F RID: 29839 RVA: 0x001B3310 File Offset: 0x001B1510
			internal bool CanImportMessagePart(MessagePart part, out OperationFormatStyle style)
			{
				style = OperationFormatStyle.Document;
				if (!MessageContractImporter.IsNullOrEmpty(part.Element))
				{
					return this.CanImportElement(MessageContractImporter.FindSchemaElement(this.schemaSet, part.Element));
				}
				if (!MessageContractImporter.IsNullOrEmpty(part.Type))
				{
					style = OperationFormatStyle.Rpc;
					return this.CanImportType(part.Type);
				}
				return false;
			}

			// Token: 0x06007490 RID: 29840 RVA: 0x001B3364 File Offset: 0x001B1564
			internal MessagePartDescription ImportMessagePart(MessagePart part, bool isHeader, bool isEncoded)
			{
				if (!MessageContractImporter.IsNullOrEmpty(part.Element))
				{
					return this.ImportParameterElement(part.Element, isHeader, false);
				}
				if (!MessageContractImporter.IsNullOrEmpty(part.Type))
				{
					MessagePartDescription messagePartDescription = isHeader ? new MessageHeaderDescription(part.Name, string.Empty) : new MessagePartDescription(part.Name, string.Empty);
					messagePartDescription.BaseType = this.ImportType(messagePartDescription, part.Type, isEncoded);
					return messagePartDescription;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWsdlPartMustHaveElementOrType", new object[]
				{
					part.Name,
					part.Message.Name,
					part.Message.Namespaces
				})));
			}

			// Token: 0x06007491 RID: 29841 RVA: 0x001B341C File Offset: 0x001B161C
			internal MessagePartDescription ImportParameterElement(XmlQualifiedName elementName, bool isHeader, bool isMultiple)
			{
				return this.ImportParameterElement(MessageContractImporter.FindSchemaElement(this.schemaSet, elementName), elementName.Namespace, isHeader, isMultiple);
			}

			// Token: 0x06007492 RID: 29842 RVA: 0x001B3438 File Offset: 0x001B1638
			internal MessagePartDescription ImportParameterElement(XmlSchemaElement element, string ns, bool isHeader, bool isMultiple)
			{
				if (element.MaxOccurs > 1m)
				{
					isMultiple = true;
				}
				if (!MessageContractImporter.IsNullOrEmpty(element.RefName))
				{
					return this.ImportParameterElement(element.RefName, isHeader, isMultiple);
				}
				MessagePartDescription messagePartDescription = isHeader ? new MessageHeaderDescription(element.Name, ns) : new MessagePartDescription(element.Name, ns);
				messagePartDescription.Multiple = isMultiple;
				messagePartDescription.BaseType = this.ImportElement(messagePartDescription, element, false);
				return messagePartDescription;
			}

			// Token: 0x06007493 RID: 29843 RVA: 0x001B34AD File Offset: 0x001B16AD
			internal virtual bool CanImportFault(XmlSchemaElement detailElement, XmlQualifiedName detailElementTypeName)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06007494 RID: 29844 RVA: 0x001B34BE File Offset: 0x001B16BE
			internal virtual CodeTypeReference ImportFaultElement(XmlQualifiedName elementName, XmlSchemaElement element, bool isEncoded)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06007495 RID: 29845 RVA: 0x001B34CF File Offset: 0x001B16CF
			internal virtual CodeTypeReference ImportFaultType(XmlQualifiedName elementName, XmlQualifiedName typeName, bool isEncoded)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06007496 RID: 29846 RVA: 0x001B34E0 File Offset: 0x001B16E0
			internal virtual void SetOperationSupportFaults(OperationDescription operation, bool supportFaults)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06007497 RID: 29847
			internal abstract void PreprocessSchema();

			// Token: 0x06007498 RID: 29848
			internal abstract void PostprocessSchema(bool used);

			// Token: 0x06007499 RID: 29849
			internal abstract bool CanImportStyleAndUse(OperationFormatStyle style, bool isEncoded);

			// Token: 0x0600749A RID: 29850
			internal abstract void ValidateStyleAndUse(OperationFormatStyle style, bool isEncoded, string operationName);

			// Token: 0x0600749B RID: 29851
			internal abstract IOperationBehavior GetOperationGenerator();

			// Token: 0x0600749C RID: 29852
			internal abstract bool CanImportType(XmlQualifiedName typeName);

			// Token: 0x0600749D RID: 29853
			internal abstract string ImportType(MessagePartDescription part, XmlQualifiedName typeName, bool isEncoded);

			// Token: 0x0600749E RID: 29854
			internal abstract bool CanImportElement(XmlSchemaElement element);

			// Token: 0x0600749F RID: 29855
			internal abstract string ImportElement(MessagePartDescription part, XmlSchemaElement element, bool isEncoded);

			// Token: 0x060074A0 RID: 29856
			internal abstract bool CanImportWrapperElement(XmlQualifiedName elementName);

			// Token: 0x060074A1 RID: 29857
			internal abstract MessagePartDescription[] ImportWrapperElement(XmlQualifiedName elementName);

			// Token: 0x060074A2 RID: 29858
			internal abstract void SetOperationStyle(OperationDescription operation, OperationFormatStyle style);

			// Token: 0x060074A3 RID: 29859
			internal abstract bool GetOperationIsEncoded(OperationDescription operation);

			// Token: 0x060074A4 RID: 29860
			internal abstract void SetOperationIsEncoded(OperationDescription operation, bool isEncoded);

			// Token: 0x060074A5 RID: 29861
			internal abstract string GetFormatName();

			// Token: 0x0400420A RID: 16906
			protected readonly XmlSchemaSet schemaSet;

			// Token: 0x0400420B RID: 16907
			protected readonly WsdlImporter importer;
		}

		// Token: 0x02000BC9 RID: 3017
		internal class DataContractSerializerSchemaImporter : MessageContractImporter.SchemaImporter
		{
			// Token: 0x060074A6 RID: 29862 RVA: 0x001B34F1 File Offset: 0x001B16F1
			public DataContractSerializerSchemaImporter(WsdlImporter importer) : base(importer)
			{
				this.DataContractSerializerOperationGenerator = new DataContractSerializerOperationGenerator(this.DataContractImporter.CodeCompileUnit);
			}

			// Token: 0x17001AEA RID: 6890
			// (get) Token: 0x060074A7 RID: 29863 RVA: 0x001B3510 File Offset: 0x001B1710
			private XsdDataContractImporter DataContractImporter
			{
				get
				{
					object obj;
					if (!this.importer.State.TryGetValue(typeof(XsdDataContractImporter), out obj))
					{
						object obj2;
						if (!this.importer.State.TryGetValue(typeof(CodeCompileUnit), out obj2))
						{
							obj2 = new CodeCompileUnit();
							this.importer.State.Add(typeof(CodeCompileUnit), obj2);
						}
						obj = new XsdDataContractImporter((CodeCompileUnit)obj2);
						this.importer.State.Add(typeof(XsdDataContractImporter), obj);
					}
					return (XsdDataContractImporter)obj;
				}
			}

			// Token: 0x060074A8 RID: 29864 RVA: 0x001B35A7 File Offset: 0x001B17A7
			internal override bool CanImportElement(XmlSchemaElement element)
			{
				return (element.IsNillable || SchemaHelper.IsElementValueType(element)) && this.DataContractImporter.CanImport(this.schemaSet, element);
			}

			// Token: 0x060074A9 RID: 29865 RVA: 0x001B35CD File Offset: 0x001B17CD
			internal override bool CanImportType(XmlQualifiedName typeName)
			{
				return this.DataContractImporter.CanImport(this.schemaSet, typeName);
			}

			// Token: 0x060074AA RID: 29866 RVA: 0x001B35E4 File Offset: 0x001B17E4
			internal override bool CanImportWrapperElement(XmlQualifiedName elementName)
			{
				string text;
				XmlSchemaForm xmlSchemaForm;
				XmlSchemaComplexType elementComplexType = MessageContractImporter.GetElementComplexType(elementName, this.schemaSet, out text, out xmlSchemaForm);
				if (elementComplexType == null)
				{
					return false;
				}
				if (elementComplexType.Particle == null)
				{
					return true;
				}
				XmlSchemaSequence xmlSchemaSequence = elementComplexType.Particle as XmlSchemaSequence;
				if (xmlSchemaSequence == null)
				{
					return false;
				}
				for (int i = 0; i < xmlSchemaSequence.Items.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement = xmlSchemaSequence.Items[i] as XmlSchemaElement;
					if (xmlSchemaElement == null)
					{
						return false;
					}
					if (!MessageContractImporter.IsNullOrEmpty(xmlSchemaElement.RefName))
					{
						xmlSchemaElement = MessageContractImporter.FindSchemaElement(this.schemaSet, xmlSchemaElement.RefName);
					}
					if (xmlSchemaElement.MaxOccurs > 1m)
					{
						return false;
					}
					if (!this.DataContractImporter.CanImport(this.schemaSet, xmlSchemaElement))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060074AB RID: 29867 RVA: 0x001B36A8 File Offset: 0x001B18A8
			internal override bool CanImportFault(XmlSchemaElement detailElement, XmlQualifiedName detailElementTypeName)
			{
				MessageContractImporter.DataContractSerializerSchemaImporter dataContractSerializerSchemaImporter = MessageContractImporter.DataContractSerializerSchemaImporter.Get(this.importer);
				if (MessageContractImporter.IsNullOrEmpty(detailElementTypeName))
				{
					return dataContractSerializerSchemaImporter.CanImportFaultElement(detailElement);
				}
				return dataContractSerializerSchemaImporter.CanImportFaultType(detailElementTypeName);
			}

			// Token: 0x060074AC RID: 29868 RVA: 0x001B36D8 File Offset: 0x001B18D8
			internal static MessageContractImporter.DataContractSerializerSchemaImporter Get(WsdlImporter importer)
			{
				Type typeFromHandle = typeof(MessageContractImporter.DataContractSerializerSchemaImporter);
				object obj;
				if (importer.State.ContainsKey(typeFromHandle))
				{
					obj = importer.State[typeFromHandle];
				}
				else
				{
					obj = new MessageContractImporter.DataContractSerializerSchemaImporter(importer);
					importer.State.Add(typeFromHandle, obj);
				}
				return (MessageContractImporter.DataContractSerializerSchemaImporter)obj;
			}

			// Token: 0x060074AD RID: 29869 RVA: 0x001B3728 File Offset: 0x001B1928
			internal override MessagePartDescription[] ImportWrapperElement(XmlQualifiedName elementName)
			{
				string ns;
				XmlSchemaForm elementFormDefault;
				XmlSchemaComplexType elementComplexType = MessageContractImporter.GetElementComplexType(elementName, this.schemaSet, out ns, out elementFormDefault);
				if (elementComplexType == null)
				{
					return null;
				}
				if (elementComplexType.Particle == null)
				{
					return new MessagePartDescription[0];
				}
				XmlSchemaSequence xmlSchemaSequence = elementComplexType.Particle as XmlSchemaSequence;
				if (xmlSchemaSequence == null)
				{
					return null;
				}
				MessagePartDescription[] array = new MessagePartDescription[xmlSchemaSequence.Items.Count];
				for (int i = 0; i < xmlSchemaSequence.Items.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement = xmlSchemaSequence.Items[i] as XmlSchemaElement;
					if (xmlSchemaElement == null)
					{
						return null;
					}
					array[i] = base.ImportParameterElement(xmlSchemaElement, MessageContractImporter.GetLocalElementNamespace(ns, xmlSchemaElement, elementFormDefault), false, false);
					if (array[i] == null)
					{
						return null;
					}
				}
				return array;
			}

			// Token: 0x060074AE RID: 29870 RVA: 0x001B37D4 File Offset: 0x001B19D4
			internal override string ImportType(MessagePartDescription part, XmlQualifiedName typeName, bool isEncoded)
			{
				if (isEncoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDataContractSerializerDoesNotSupportEncoded", new object[]
					{
						part.Name
					})));
				}
				this.DataContractImporter.Import(this.schemaSet, typeName);
				CodeTypeReference codeTypeReference = this.DataContractImporter.GetCodeTypeReference(typeName);
				ICollection<CodeTypeReference> knownTypeReferences = this.DataContractImporter.GetKnownTypeReferences(typeName);
				this.DataContractSerializerOperationGenerator.Add(part, codeTypeReference, knownTypeReferences, false);
				if (codeTypeReference.ArrayRank == 0)
				{
					return codeTypeReference.BaseType;
				}
				return codeTypeReference.BaseType + "[]";
			}

			// Token: 0x060074AF RID: 29871 RVA: 0x001B3868 File Offset: 0x001B1A68
			internal static bool TryGetFailedReferenceType(Exception ex, out Type failedReferenceType)
			{
				if (ex == null)
				{
					throw new ArgumentNullException("ex");
				}
				if (ex.Data.Contains("System.Runtime.Serialization.FailedReferenceType"))
				{
					failedReferenceType = (ex.Data["System.Runtime.Serialization.FailedReferenceType"] as Type);
					if (null != failedReferenceType)
					{
						return true;
					}
				}
				failedReferenceType = null;
				return false;
			}

			// Token: 0x060074B0 RID: 29872 RVA: 0x001B38BC File Offset: 0x001B1ABC
			internal override string ImportElement(MessagePartDescription part, XmlSchemaElement element, bool isEncoded)
			{
				if (part.Multiple)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDataContractSerializerDoesNotSupportBareArray", new object[]
					{
						part.Name
					})));
				}
				XmlQualifiedName xmlQualifiedName = null;
				while (null == xmlQualifiedName)
				{
					try
					{
						xmlQualifiedName = this.DataContractImporter.Import(this.schemaSet, element);
						break;
					}
					catch (InvalidDataContractException ex)
					{
						Type item;
						if (!MessageContractImporter.DataContractSerializerSchemaImporter.TryGetFailedReferenceType(ex, out item))
						{
							throw;
						}
						this.DataContractImporter.Options.ReferencedTypes.Remove(item);
					}
					catch (InvalidOperationException ex2)
					{
						Type item;
						if (!MessageContractImporter.DataContractSerializerSchemaImporter.TryGetFailedReferenceType(ex2, out item))
						{
							throw;
						}
						this.DataContractImporter.Options.ReferencedTypes.Remove(item);
					}
				}
				CodeTypeReference codeTypeReference = this.DataContractImporter.GetCodeTypeReference(xmlQualifiedName, element);
				ICollection<CodeTypeReference> knownTypeReferences = this.DataContractImporter.GetKnownTypeReferences(xmlQualifiedName);
				this.DataContractSerializerOperationGenerator.Add(part, codeTypeReference, knownTypeReferences, !element.IsNillable && !this.IsValueType(xmlQualifiedName));
				if (codeTypeReference.ArrayRank == 0)
				{
					return codeTypeReference.BaseType;
				}
				return codeTypeReference.BaseType + "[]";
			}

			// Token: 0x060074B1 RID: 29873 RVA: 0x001B39E8 File Offset: 0x001B1BE8
			private bool IsValueType(XmlQualifiedName typeName)
			{
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.IsNillable = true;
				CodeTypeReference codeTypeReference = this.DataContractImporter.GetCodeTypeReference(typeName, xmlSchemaElement);
				return codeTypeReference.BaseType == typeof(Nullable<>).FullName;
			}

			// Token: 0x060074B2 RID: 29874 RVA: 0x001B3A2C File Offset: 0x001B1C2C
			private int SetImportXmlType(bool value)
			{
				if (this.DataContractImporter.Options == null)
				{
					this.DataContractImporter.Options = new ImportOptions();
					this.DataContractImporter.Options.ImportXmlType = value;
					return -1;
				}
				if (this.DataContractImporter.Options.ImportXmlType != value)
				{
					this.DataContractImporter.Options.ImportXmlType = value;
					return 0;
				}
				return 1;
			}

			// Token: 0x060074B3 RID: 29875 RVA: 0x001B3A90 File Offset: 0x001B1C90
			private void RestoreImportXmlType(int oldValue)
			{
				if (oldValue == 1)
				{
					return;
				}
				if (oldValue == 0)
				{
					this.DataContractImporter.Options.ImportXmlType = !this.DataContractImporter.Options.ImportXmlType;
					return;
				}
				this.DataContractImporter.Options = null;
			}

			// Token: 0x060074B4 RID: 29876 RVA: 0x001B3ACC File Offset: 0x001B1CCC
			internal override CodeTypeReference ImportFaultElement(XmlQualifiedName elementName, XmlSchemaElement element, bool isEncoded)
			{
				int oldValue = this.SetImportXmlType(true);
				CodeTypeReference codeTypeReference;
				try
				{
					XmlQualifiedName typeName = this.DataContractImporter.Import(this.schemaSet, element);
					codeTypeReference = this.DataContractImporter.GetCodeTypeReference(typeName, element);
				}
				finally
				{
					this.RestoreImportXmlType(oldValue);
				}
				return codeTypeReference;
			}

			// Token: 0x060074B5 RID: 29877 RVA: 0x001B3B20 File Offset: 0x001B1D20
			internal bool CanImportFaultElement(XmlSchemaElement element)
			{
				int oldValue = this.SetImportXmlType(false);
				bool result;
				try
				{
					result = this.DataContractImporter.CanImport(this.schemaSet, element);
				}
				finally
				{
					this.RestoreImportXmlType(oldValue);
				}
				return result;
			}

			// Token: 0x060074B6 RID: 29878 RVA: 0x001B3B64 File Offset: 0x001B1D64
			internal override CodeTypeReference ImportFaultType(XmlQualifiedName elementName, XmlQualifiedName typeName, bool isEncoded)
			{
				int oldValue = this.SetImportXmlType(true);
				CodeTypeReference codeTypeReference;
				try
				{
					this.DataContractImporter.Import(this.schemaSet, typeName);
					codeTypeReference = this.DataContractImporter.GetCodeTypeReference(typeName);
				}
				finally
				{
					this.RestoreImportXmlType(oldValue);
				}
				return codeTypeReference;
			}

			// Token: 0x060074B7 RID: 29879 RVA: 0x001B3BB4 File Offset: 0x001B1DB4
			internal bool CanImportFaultType(XmlQualifiedName typeName)
			{
				int oldValue = this.SetImportXmlType(false);
				bool result;
				try
				{
					result = this.DataContractImporter.CanImport(this.schemaSet, typeName);
				}
				finally
				{
					this.RestoreImportXmlType(oldValue);
				}
				return result;
			}

			// Token: 0x060074B8 RID: 29880 RVA: 0x001B3BF8 File Offset: 0x001B1DF8
			internal override void PreprocessSchema()
			{
				this.errors = new Collection<MetadataConversionError>();
				this.compileValidationEventHandler = delegate(object sender, ValidationEventArgs args)
				{
					SchemaHelper.HandleSchemaValidationError(sender, args, this.errors);
				};
				this.schemaSet.ValidationEventHandler += this.compileValidationEventHandler;
			}

			// Token: 0x060074B9 RID: 29881 RVA: 0x001B3C28 File Offset: 0x001B1E28
			internal override void PostprocessSchema(bool used)
			{
				if (used && this.errors != null)
				{
					foreach (MetadataConversionError item in this.errors)
					{
						this.importer.Errors.Add(item);
					}
					this.errors.Clear();
				}
				this.schemaSet.ValidationEventHandler -= this.compileValidationEventHandler;
			}

			// Token: 0x060074BA RID: 29882 RVA: 0x001B3CA8 File Offset: 0x001B1EA8
			internal override IOperationBehavior GetOperationGenerator()
			{
				return this.DataContractSerializerOperationGenerator;
			}

			// Token: 0x060074BB RID: 29883 RVA: 0x001B3CB0 File Offset: 0x001B1EB0
			internal override bool CanImportStyleAndUse(OperationFormatStyle style, bool isEncoded)
			{
				return !isEncoded;
			}

			// Token: 0x060074BC RID: 29884 RVA: 0x001B3CB6 File Offset: 0x001B1EB6
			internal override void ValidateStyleAndUse(OperationFormatStyle style, bool isEncoded, string operationName)
			{
				if (isEncoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDataContractSerializerDoesNotSupportEncoded", new object[]
					{
						operationName
					})));
				}
			}

			// Token: 0x060074BD RID: 29885 RVA: 0x001B3CE0 File Offset: 0x001B1EE0
			internal override void SetOperationStyle(OperationDescription operation, OperationFormatStyle style)
			{
				DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
				if (dataContractSerializerOperationBehavior == null)
				{
					dataContractSerializerOperationBehavior = new DataContractSerializerOperationBehavior(operation, new DataContractFormatAttribute());
					operation.Behaviors.Add(dataContractSerializerOperationBehavior);
				}
				dataContractSerializerOperationBehavior.DataContractFormatAttribute.Style = style;
			}

			// Token: 0x060074BE RID: 29886 RVA: 0x001B3D20 File Offset: 0x001B1F20
			internal override bool GetOperationIsEncoded(OperationDescription operation)
			{
				return false;
			}

			// Token: 0x060074BF RID: 29887 RVA: 0x001B3D23 File Offset: 0x001B1F23
			internal override void SetOperationIsEncoded(OperationDescription operation, bool isEncoded)
			{
				if (isEncoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDataContractSerializerDoesNotSupportEncoded", new object[]
					{
						operation.Name
					})));
				}
			}

			// Token: 0x060074C0 RID: 29888 RVA: 0x001B3D51 File Offset: 0x001B1F51
			internal override void SetOperationSupportFaults(OperationDescription operation, bool supportFaults)
			{
			}

			// Token: 0x060074C1 RID: 29889 RVA: 0x001B3D53 File Offset: 0x001B1F53
			internal override string GetFormatName()
			{
				return "DataContract";
			}

			// Token: 0x0400420C RID: 16908
			internal const string FailedReferenceTypeExceptionKey = "System.Runtime.Serialization.FailedReferenceType";

			// Token: 0x0400420D RID: 16909
			private DataContractSerializerOperationGenerator DataContractSerializerOperationGenerator;

			// Token: 0x0400420E RID: 16910
			private ValidationEventHandler compileValidationEventHandler;

			// Token: 0x0400420F RID: 16911
			private Collection<MetadataConversionError> errors;
		}

		// Token: 0x02000BCA RID: 3018
		internal class XmlSerializerSchemaImporter : MessageContractImporter.SchemaImporter
		{
			// Token: 0x060074C3 RID: 29891 RVA: 0x001B3D6C File Offset: 0x001B1F6C
			public XmlSerializerSchemaImporter(WsdlImporter importer) : base(importer)
			{
				XmlSerializerImportOptions xmlSerializerImportOptions;
				if (importer.State.ContainsKey(typeof(XmlSerializerImportOptions)))
				{
					xmlSerializerImportOptions = (XmlSerializerImportOptions)importer.State[typeof(XmlSerializerImportOptions)];
				}
				else
				{
					object obj;
					if (!importer.State.TryGetValue(typeof(CodeCompileUnit), out obj))
					{
						obj = new CodeCompileUnit();
						importer.State.Add(typeof(CodeCompileUnit), obj);
					}
					xmlSerializerImportOptions = new XmlSerializerImportOptions((CodeCompileUnit)obj);
					importer.State.Add(typeof(XmlSerializerImportOptions), xmlSerializerImportOptions);
				}
				WebReferenceOptions webReferenceOptions = xmlSerializerImportOptions.WebReferenceOptions;
				this.codeProvider = xmlSerializerImportOptions.CodeProvider;
				this.encodedSchemas = new XmlSchemas();
				this.literalSchemas = new XmlSchemas();
				MessageContractImporter.CollectEncodedAndLiteralSchemas(importer.WsdlDocuments, this.encodedSchemas, this.literalSchemas, this.schemaSet);
				CodeIdentifiers identifiers = new CodeIdentifiers();
				object schemaImporterLock = MessageContractImporter.schemaImporterLock;
				lock (schemaImporterLock)
				{
					this.xmlImporter = new XmlSchemaImporter(this.literalSchemas, webReferenceOptions.CodeGenerationOptions, xmlSerializerImportOptions.CodeProvider, new ImportContext(identifiers, false));
				}
				if (webReferenceOptions != null)
				{
					foreach (string text in webReferenceOptions.SchemaImporterExtensions)
					{
						this.xmlImporter.Extensions.Add(text, Type.GetType(text, true));
					}
				}
				object schemaImporterLock2 = MessageContractImporter.schemaImporterLock;
				lock (schemaImporterLock2)
				{
					this.soapImporter = new SoapSchemaImporter(this.encodedSchemas, webReferenceOptions.CodeGenerationOptions, xmlSerializerImportOptions.CodeProvider, new ImportContext(identifiers, false));
				}
				this.xmlSerializerOperationGenerator = new XmlSerializerOperationGenerator(xmlSerializerImportOptions);
			}

			// Token: 0x060074C4 RID: 29892 RVA: 0x001B3F6C File Offset: 0x001B216C
			internal override bool CanImportElement(XmlSchemaElement element)
			{
				return true;
			}

			// Token: 0x060074C5 RID: 29893 RVA: 0x001B3F6F File Offset: 0x001B216F
			internal override bool CanImportType(XmlQualifiedName typeName)
			{
				return true;
			}

			// Token: 0x060074C6 RID: 29894 RVA: 0x001B3F74 File Offset: 0x001B2174
			internal override bool CanImportWrapperElement(XmlQualifiedName elementName)
			{
				string text;
				XmlSchemaForm xmlSchemaForm;
				return MessageContractImporter.GetElementComplexType(elementName, this.schemaSet, out text, out xmlSchemaForm) != null;
			}

			// Token: 0x060074C7 RID: 29895 RVA: 0x001B3F98 File Offset: 0x001B2198
			internal override bool CanImportFault(XmlSchemaElement detailElement, XmlQualifiedName detailElementTypeName)
			{
				return true;
			}

			// Token: 0x060074C8 RID: 29896 RVA: 0x001B3F9C File Offset: 0x001B219C
			internal static MessageContractImporter.XmlSerializerSchemaImporter Get(WsdlImporter importer)
			{
				Type typeFromHandle = typeof(MessageContractImporter.XmlSerializerSchemaImporter);
				object obj;
				if (importer.State.ContainsKey(typeFromHandle))
				{
					obj = importer.State[typeFromHandle];
				}
				else
				{
					obj = new MessageContractImporter.XmlSerializerSchemaImporter(importer);
					importer.State.Add(typeFromHandle, obj);
				}
				return (MessageContractImporter.XmlSerializerSchemaImporter)obj;
			}

			// Token: 0x060074C9 RID: 29897 RVA: 0x001B3FEC File Offset: 0x001B21EC
			internal override MessagePartDescription[] ImportWrapperElement(XmlQualifiedName elementName)
			{
				XmlMembersMapping xmlMembersMapping = this.xmlImporter.ImportMembersMapping(elementName);
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < xmlMembersMapping.Count; i++)
				{
					XmlMemberMapping xmlMemberMapping = xmlMembersMapping[i];
					string name = NamingHelper.XmlName(xmlMemberMapping.MemberName);
					MessagePartDescription messagePartDescription = new MessagePartDescription(name, (xmlMemberMapping.Namespace == null) ? string.Empty : xmlMemberMapping.Namespace);
					this.xmlSerializerOperationGenerator.Add(messagePartDescription, xmlMemberMapping, xmlMembersMapping, false);
					messagePartDescription.BaseType = xmlMemberMapping.GenerateTypeName(this.codeProvider);
					arrayList.Add(messagePartDescription);
				}
				return (MessagePartDescription[])arrayList.ToArray(typeof(MessagePartDescription));
			}

			// Token: 0x060074CA RID: 29898 RVA: 0x001B4094 File Offset: 0x001B2294
			internal override CodeTypeReference ImportFaultElement(XmlQualifiedName elementName, XmlSchemaElement element, bool isEncoded)
			{
				if (isEncoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDocEncodedFaultNotSupported")));
				}
				XmlMembersMapping xmlMembersMapping = this.xmlImporter.ImportMembersMapping(new XmlQualifiedName[]
				{
					elementName
				});
				this.xmlSerializerOperationGenerator.XmlExporter.ExportMembersMapping(xmlMembersMapping);
				return new CodeTypeReference(this.xmlSerializerOperationGenerator.GetTypeName(xmlMembersMapping[0]));
			}

			// Token: 0x060074CB RID: 29899 RVA: 0x001B40FC File Offset: 0x001B22FC
			internal override CodeTypeReference ImportFaultType(XmlQualifiedName elementName, XmlQualifiedName typeName, bool isEncoded)
			{
				XmlName xmlName = new XmlName(elementName.Name, true);
				string @namespace = elementName.Namespace;
				SoapSchemaMember soapSchemaMember = new SoapSchemaMember();
				soapSchemaMember.MemberName = xmlName.EncodedName;
				soapSchemaMember.MemberType = typeName;
				XmlMembersMapping xmlMembersMapping;
				if (isEncoded)
				{
					xmlMembersMapping = this.soapImporter.ImportMembersMapping(xmlName.DecodedName, @namespace, new SoapSchemaMember[]
					{
						soapSchemaMember
					});
					this.xmlSerializerOperationGenerator.SoapExporter.ExportMembersMapping(xmlMembersMapping);
				}
				else
				{
					xmlMembersMapping = this.xmlImporter.ImportMembersMapping(xmlName.DecodedName, @namespace, new SoapSchemaMember[]
					{
						soapSchemaMember
					});
					this.xmlSerializerOperationGenerator.XmlExporter.ExportMembersMapping(xmlMembersMapping);
				}
				return new CodeTypeReference(this.xmlSerializerOperationGenerator.GetTypeName(xmlMembersMapping[0]));
			}

			// Token: 0x060074CC RID: 29900 RVA: 0x001B41B0 File Offset: 0x001B23B0
			internal override string ImportType(MessagePartDescription part, XmlQualifiedName typeName, bool isEncoded)
			{
				XmlName xmlName = new XmlName(part.Name, true);
				string @namespace = part.Namespace;
				SoapSchemaMember soapSchemaMember = new SoapSchemaMember();
				soapSchemaMember.MemberName = xmlName.EncodedName;
				soapSchemaMember.MemberType = typeName;
				XmlMembersMapping membersMapping;
				if (isEncoded)
				{
					membersMapping = this.soapImporter.ImportMembersMapping(xmlName.DecodedName, @namespace, new SoapSchemaMember[]
					{
						soapSchemaMember
					});
				}
				else
				{
					membersMapping = this.xmlImporter.ImportMembersMapping(xmlName.DecodedName, @namespace, new SoapSchemaMember[]
					{
						soapSchemaMember
					});
				}
				return this.AddPartType(part, membersMapping, isEncoded);
			}

			// Token: 0x060074CD RID: 29901 RVA: 0x001B4234 File Offset: 0x001B2434
			internal override string ImportElement(MessagePartDescription part, XmlSchemaElement element, bool isEncoded)
			{
				if (isEncoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDocEncodedNotSupported", new object[]
					{
						part.Name
					})));
				}
				XmlMembersMapping membersMapping = this.xmlImporter.ImportMembersMapping(new XmlQualifiedName[]
				{
					element.QualifiedName
				});
				return this.AddPartType(part, membersMapping, isEncoded);
			}

			// Token: 0x060074CE RID: 29902 RVA: 0x001B4291 File Offset: 0x001B2491
			private string AddPartType(MessagePartDescription part, XmlMembersMapping membersMapping, bool isEncoded)
			{
				this.xmlSerializerOperationGenerator.Add(part, membersMapping[0], membersMapping, isEncoded);
				return membersMapping[0].GenerateTypeName(this.codeProvider);
			}

			// Token: 0x060074CF RID: 29903 RVA: 0x001B42BC File Offset: 0x001B24BC
			internal override void PreprocessSchema()
			{
				XmlSchema schema = StockSchemas.CreateWsdl();
				XmlSchema schema2 = StockSchemas.CreateSoap();
				XmlSchema schema3 = StockSchemas.CreateSoapEncoding();
				XmlSchema schema4 = StockSchemas.CreateFakeXsdSchema();
				XmlSchema schema5 = StockSchemas.CreateFakeXmlSchema();
				this.schemaSet.Add(schema);
				this.schemaSet.Add(schema2);
				this.schemaSet.Add(schema3);
				this.schemaSet.Add(schema4);
				this.schemaSet.Add(schema5);
				SchemaHelper.Compile(this.schemaSet, this.importer.Errors);
				this.schemaSet.Remove(schema);
				this.schemaSet.Remove(schema2);
				this.schemaSet.Remove(schema3);
				this.schemaSet.Remove(schema4);
				this.schemaSet.Remove(schema5);
			}

			// Token: 0x060074D0 RID: 29904 RVA: 0x001B4382 File Offset: 0x001B2582
			internal override void PostprocessSchema(bool used)
			{
			}

			// Token: 0x060074D1 RID: 29905 RVA: 0x001B4384 File Offset: 0x001B2584
			internal override IOperationBehavior GetOperationGenerator()
			{
				return this.xmlSerializerOperationGenerator;
			}

			// Token: 0x060074D2 RID: 29906 RVA: 0x001B438C File Offset: 0x001B258C
			internal override bool CanImportStyleAndUse(OperationFormatStyle style, bool isEncoded)
			{
				return true;
			}

			// Token: 0x060074D3 RID: 29907 RVA: 0x001B438F File Offset: 0x001B258F
			internal override void ValidateStyleAndUse(OperationFormatStyle style, bool isEncoded, string operationName)
			{
				if (isEncoded && style != OperationFormatStyle.Rpc)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDocEncodedNotSupported", new object[]
					{
						operationName
					})));
				}
			}

			// Token: 0x060074D4 RID: 29908 RVA: 0x001B43BC File Offset: 0x001B25BC
			internal static XmlSerializerFormatAttribute GetFormatAttribute(OperationDescription operation, bool createNew)
			{
				XmlSerializerOperationBehavior xmlSerializerOperationBehavior = operation.Behaviors.Find<XmlSerializerOperationBehavior>();
				if (xmlSerializerOperationBehavior != null)
				{
					return xmlSerializerOperationBehavior.XmlSerializerFormatAttribute;
				}
				if (!createNew)
				{
					return null;
				}
				xmlSerializerOperationBehavior = new XmlSerializerOperationBehavior(operation);
				operation.Behaviors.Add(xmlSerializerOperationBehavior);
				return xmlSerializerOperationBehavior.XmlSerializerFormatAttribute;
			}

			// Token: 0x060074D5 RID: 29909 RVA: 0x001B4400 File Offset: 0x001B2600
			internal override void SetOperationStyle(OperationDescription operation, OperationFormatStyle style)
			{
				XmlSerializerFormatAttribute formatAttribute = MessageContractImporter.XmlSerializerSchemaImporter.GetFormatAttribute(operation, true);
				formatAttribute.Style = style;
			}

			// Token: 0x060074D6 RID: 29910 RVA: 0x001B441C File Offset: 0x001B261C
			internal override bool GetOperationIsEncoded(OperationDescription operation)
			{
				XmlSerializerFormatAttribute formatAttribute = MessageContractImporter.XmlSerializerSchemaImporter.GetFormatAttribute(operation, false);
				if (formatAttribute == null)
				{
					return TypeLoader.DefaultXmlSerializerFormatAttribute.IsEncoded;
				}
				return formatAttribute.IsEncoded;
			}

			// Token: 0x060074D7 RID: 29911 RVA: 0x001B4448 File Offset: 0x001B2648
			internal override void SetOperationIsEncoded(OperationDescription operation, bool isEncoded)
			{
				XmlSerializerFormatAttribute formatAttribute = MessageContractImporter.XmlSerializerSchemaImporter.GetFormatAttribute(operation, true);
				formatAttribute.IsEncoded = isEncoded;
			}

			// Token: 0x060074D8 RID: 29912 RVA: 0x001B4464 File Offset: 0x001B2664
			internal override void SetOperationSupportFaults(OperationDescription operation, bool supportFaults)
			{
				XmlSerializerFormatAttribute formatAttribute = MessageContractImporter.XmlSerializerSchemaImporter.GetFormatAttribute(operation, true);
				formatAttribute.SupportFaults = supportFaults;
			}

			// Token: 0x060074D9 RID: 29913 RVA: 0x001B4480 File Offset: 0x001B2680
			internal override string GetFormatName()
			{
				return "XmlSerializer";
			}

			// Token: 0x04004210 RID: 16912
			private XmlSerializerOperationGenerator xmlSerializerOperationGenerator;

			// Token: 0x04004211 RID: 16913
			private XmlSchemaImporter xmlImporter;

			// Token: 0x04004212 RID: 16914
			private SoapSchemaImporter soapImporter;

			// Token: 0x04004213 RID: 16915
			private CodeDomProvider codeProvider;

			// Token: 0x04004214 RID: 16916
			private XmlSchemas literalSchemas;

			// Token: 0x04004215 RID: 16917
			private XmlSchemas encodedSchemas;
		}

		// Token: 0x02000BCB RID: 3019
		private class OperationInfo
		{
			// Token: 0x060074DA RID: 29914 RVA: 0x001B4487 File Offset: 0x001B2687
			internal OperationInfo(OperationFormatStyle style, bool isEncoded, bool areAllMessagesWrapped)
			{
				this.style = style;
				this.isEncoded = isEncoded;
				this.areAllMessagesWrapped = areAllMessagesWrapped;
			}

			// Token: 0x17001AEB RID: 6891
			// (get) Token: 0x060074DB RID: 29915 RVA: 0x001B44A4 File Offset: 0x001B26A4
			internal OperationFormatStyle Style
			{
				get
				{
					return this.style;
				}
			}

			// Token: 0x17001AEC RID: 6892
			// (get) Token: 0x060074DC RID: 29916 RVA: 0x001B44AC File Offset: 0x001B26AC
			internal bool IsEncoded
			{
				get
				{
					return this.isEncoded;
				}
			}

			// Token: 0x17001AED RID: 6893
			// (get) Token: 0x060074DD RID: 29917 RVA: 0x001B44B4 File Offset: 0x001B26B4
			internal bool AreAllMessagesWrapped
			{
				get
				{
					return this.areAllMessagesWrapped;
				}
			}

			// Token: 0x04004216 RID: 16918
			private OperationFormatStyle style;

			// Token: 0x04004217 RID: 16919
			private bool isEncoded;

			// Token: 0x04004218 RID: 16920
			private bool areAllMessagesWrapped;
		}
	}
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Security;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x02000416 RID: 1046
	internal class OperationGenerator
	{
		// Token: 0x0600280B RID: 10251 RVA: 0x00096D86 File Offset: 0x00094F86
		internal OperationGenerator()
		{
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x00096D8E File Offset: 0x00094F8E
		internal Dictionary<MessagePartDescription, CodeAttributeDeclarationCollection> ParameterAttributes
		{
			get
			{
				if (this.parameterAttributes == null)
				{
					this.parameterAttributes = new Dictionary<MessagePartDescription, CodeAttributeDeclarationCollection>();
				}
				return this.parameterAttributes;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x00096DA9 File Offset: 0x00094FA9
		internal Dictionary<MessagePartDescription, CodeTypeReference> ParameterTypes
		{
			get
			{
				if (this.parameterTypes == null)
				{
					this.parameterTypes = new Dictionary<MessagePartDescription, CodeTypeReference>();
				}
				return this.parameterTypes;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x00096DC4 File Offset: 0x00094FC4
		internal Dictionary<MessagePartDescription, string> SpecialPartName
		{
			get
			{
				if (this.specialPartName == null)
				{
					this.specialPartName = new Dictionary<MessagePartDescription, string>();
				}
				return this.specialPartName;
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x00096DE0 File Offset: 0x00094FE0
		internal void GenerateOperation(OperationContractGenerationContext context, ref OperationFormatStyle style, bool isEncoded, IWrappedBodyTypeGenerator wrappedBodyTypeGenerator, Dictionary<MessagePartDescription, ICollection<CodeTypeReference>> knownTypes)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			if (context.Operation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OperationPropertyIsRequiredForAttributeGeneration")));
			}
			OperationGenerator.MethodSignatureGenerator methodSignatureGenerator = new OperationGenerator.MethodSignatureGenerator(this, context, style, isEncoded, wrappedBodyTypeGenerator, knownTypes);
			methodSignatureGenerator.GenerateSyncSignature(ref style);
			if (context.IsTask)
			{
				methodSignatureGenerator.GenerateTaskSignature(ref style);
			}
			if (context.IsAsync)
			{
				methodSignatureGenerator.GenerateAsyncSignature(ref style);
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x00096E5B File Offset: 0x0009505B
		internal static CodeAttributeDeclaration GenerateAttributeDeclaration(ServiceContractGenerator generator, Attribute attribute)
		{
			return OperationGenerator.CustomAttributeHelper.GenerateAttributeDeclaration(generator, attribute);
		}

		// Token: 0x04002211 RID: 8721
		private Dictionary<MessagePartDescription, CodeTypeReference> parameterTypes;

		// Token: 0x04002212 RID: 8722
		private Dictionary<MessagePartDescription, CodeAttributeDeclarationCollection> parameterAttributes;

		// Token: 0x04002213 RID: 8723
		private Dictionary<MessagePartDescription, string> specialPartName;

		// Token: 0x02000BCD RID: 3021
		private class MethodSignatureGenerator
		{
			// Token: 0x060074E5 RID: 29925 RVA: 0x001B4664 File Offset: 0x001B2864
			internal MethodSignatureGenerator(OperationGenerator parent, OperationContractGenerationContext context, OperationFormatStyle style, bool isEncoded, IWrappedBodyTypeGenerator wrappedBodyTypeGenerator, Dictionary<MessagePartDescription, ICollection<CodeTypeReference>> knownTypes)
			{
				this.Parent = parent;
				this.Context = context;
				this.Style = style;
				this.IsEncoded = isEncoded;
				this.WrappedBodyTypeGenerator = wrappedBodyTypeGenerator;
				this.KnownTypes = knownTypes;
				this.MessageContractType = (context.ServiceContractGenerator.OptionsInternal.IsSet(ServiceContractGenerationOptions.TypedMessages) ? MessageContractType.WrappedMessageContract : MessageContractType.None);
				this.ContractName = context.Contract.Contract.CodeName;
				this.ContractNS = context.Operation.DeclaringContract.Namespace;
				this.DefaultNS = ((style == OperationFormatStyle.Rpc) ? string.Empty : this.ContractNS);
				this.Oneway = context.Operation.IsOneWay;
				this.Request = context.Operation.Messages[0];
				this.Response = (this.Oneway ? null : context.Operation.Messages[1]);
				this.IsNewRequest = true;
				this.IsNewResponse = true;
				this.BeginPartCodeGenerator = null;
				this.EndPartCodeGenerator = null;
				this.IsTaskWithOutputParameters = (context.IsTask && context.Operation.HasOutputParameters);
			}

			// Token: 0x060074E6 RID: 29926 RVA: 0x001B478A File Offset: 0x001B298A
			internal void GenerateSyncSignature(ref OperationFormatStyle style)
			{
				this.Method = this.Context.SyncMethod;
				this.EndMethod = this.Context.SyncMethod;
				this.DefaultName = this.Method.Name;
				this.GenerateOperationSignatures(ref style);
			}

			// Token: 0x060074E7 RID: 29927 RVA: 0x001B47C8 File Offset: 0x001B29C8
			internal void GenerateAsyncSignature(ref OperationFormatStyle style)
			{
				this.Method = this.Context.BeginMethod;
				this.EndMethod = this.Context.EndMethod;
				this.DefaultName = this.Method.Name.Substring(5);
				this.GenerateOperationSignatures(ref style);
			}

			// Token: 0x060074E8 RID: 29928 RVA: 0x001B4815 File Offset: 0x001B2A15
			private void GenerateOperationSignatures(ref OperationFormatStyle style)
			{
				if (this.MessageContractType != MessageContractType.None || this.GenerateTypedMessageForTaskWithOutputParameters())
				{
					this.CheckAndSetMessageContractTypeToBare();
					this.GenerateTypedMessageOperation(false, ref style);
					return;
				}
				if (!this.TryGenerateParameterizedOperation())
				{
					this.GenerateTypedMessageOperation(true, ref style);
				}
			}

			// Token: 0x060074E9 RID: 29929 RVA: 0x001B4848 File Offset: 0x001B2A48
			private bool GenerateTypedMessageForTaskWithOutputParameters()
			{
				if (this.IsTaskWithOutputParameters)
				{
					if (this.Method == this.Context.TaskMethod)
					{
						this.Method.Comments.Add(new CodeCommentStatement(SR.GetString("SFxCodeGenWarning", new object[]
						{
							SR.GetString("SFxCannotImportAsParameters_OutputParameterAndTask")
						})));
					}
					return true;
				}
				return false;
			}

			// Token: 0x060074EA RID: 29930 RVA: 0x001B48A8 File Offset: 0x001B2AA8
			private void CheckAndSetMessageContractTypeToBare()
			{
				if (this.MessageContractType == MessageContractType.BareMessageContract)
				{
					return;
				}
				try
				{
					this.WrappedBodyTypeGenerator.ValidateForParameterMode(this.Context.Operation);
				}
				catch (ParameterModeException ex)
				{
					this.MessageContractType = ex.MessageContractType;
				}
			}

			// Token: 0x060074EB RID: 29931 RVA: 0x001B48F8 File Offset: 0x001B2AF8
			private bool TryGenerateParameterizedOperation()
			{
				CodeParameterDeclarationExpressionCollection value = null;
				CodeParameterDeclarationExpressionCollection value2 = new CodeParameterDeclarationExpressionCollection(this.Method.Parameters);
				if (this.EndMethod != null)
				{
					value = new CodeParameterDeclarationExpressionCollection(this.EndMethod.Parameters);
				}
				try
				{
					this.GenerateParameterizedOperation();
				}
				catch (ParameterModeException ex)
				{
					this.MessageContractType = ex.MessageContractType;
					CodeMemberMethod method = this.Method;
					method.Comments.Add(new CodeCommentStatement(SR.GetString("SFxCodeGenWarning", new object[]
					{
						ex.Message
					})));
					method.Parameters.Clear();
					method.Parameters.AddRange(value2);
					if (this.Context.IsAsync)
					{
						CodeMemberMethod endMethod = this.EndMethod;
						endMethod.Parameters.Clear();
						endMethod.Parameters.AddRange(value);
					}
					return false;
				}
				return true;
			}

			// Token: 0x060074EC RID: 29932 RVA: 0x001B49DC File Offset: 0x001B2BDC
			private void GenerateParameterizedOperation()
			{
				OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.ValidateProtectionLevel(this);
				this.CreateOrOverrideActionProperties();
				if (!this.HasUntypedMessages)
				{
					OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.ValidateWrapperSettings(this);
					OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.ValidateNoHeaders(this);
					this.WrappedBodyTypeGenerator.ValidateForParameterMode(this.Context.Operation);
					OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator parameterizedMethodGenerator = new OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator(this.Method, this.EndMethod);
					this.BeginPartCodeGenerator = parameterizedMethodGenerator.InputGenerator;
					this.EndPartCodeGenerator = parameterizedMethodGenerator.OutputGenerator;
					if (!this.Oneway && this.Response.Body.ReturnValue != null)
					{
						this.EndMethod.ReturnType = this.GetParameterType(this.Response.Body.ReturnValue);
						OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.GenerateMessageParameterAttribute(this.Response.Body.ReturnValue, this.EndMethod.ReturnTypeCustomAttributes, TypeLoader.GetReturnValueName(this.DefaultName), this.DefaultNS);
						this.AddAdditionalAttributes(this.Response.Body.ReturnValue, this.EndMethod.ReturnTypeCustomAttributes, this.IsEncoded);
					}
					this.GenerateMessageBodyParts(false);
					return;
				}
				if (!this.IsCompletelyUntyped)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_Message", new object[]
					{
						this.Context.Operation.CodeName
					})));
				}
				this.CreateUntypedMessages();
			}

			// Token: 0x060074ED RID: 29933 RVA: 0x001B4B28 File Offset: 0x001B2D28
			private void GenerateTypedMessageOperation(bool hideFromEditor, ref OperationFormatStyle style)
			{
				this.CreateOrOverrideActionProperties();
				if (this.HasUntypedMessages)
				{
					this.CreateUntypedMessages();
					if (this.IsCompletelyUntyped)
					{
						return;
					}
				}
				CodeNamespace ns = this.Context.ServiceContractGenerator.NamespaceManager.EnsureNamespace(this.ContractNS);
				if (!this.Request.IsUntypedMessage)
				{
					CodeTypeReference type = this.GenerateTypedMessageHeaderAndReturnValueParts(ns, this.DefaultName + "Request", this.Request, false, hideFromEditor, ref this.IsNewRequest, out this.BeginPartCodeGenerator);
					this.Method.Parameters.Insert(0, new CodeParameterDeclarationExpression(type, "request"));
				}
				if (!this.Oneway && !this.Response.IsUntypedMessage)
				{
					CodeTypeReference returnType = this.GenerateTypedMessageHeaderAndReturnValueParts(ns, this.DefaultName + "Response", this.Response, true, hideFromEditor, ref this.IsNewResponse, out this.EndPartCodeGenerator);
					this.EndMethod.ReturnType = returnType;
				}
				this.GenerateMessageBodyParts(true);
				if (!this.IsEncoded)
				{
					style = OperationFormatStyle.Document;
				}
			}

			// Token: 0x060074EE RID: 29934 RVA: 0x001B4C24 File Offset: 0x001B2E24
			private CodeTypeReference GenerateTypedMessageHeaderAndReturnValueParts(CodeNamespace ns, string defaultName, MessageDescription message, bool isReply, bool hideFromEditor, ref bool isNewMessage, out OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator partCodeGenerator)
			{
				CodeTypeReference codeTypeReference;
				if (OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.FindGeneratedTypedMessage(this.Context.Contract, message, out codeTypeReference))
				{
					partCodeGenerator = null;
					isNewMessage = false;
				}
				else
				{
					UniqueCodeNamespaceScope uniqueCodeNamespaceScope = new UniqueCodeNamespaceScope(ns);
					CodeTypeDeclaration codeTypeDeclaration = this.Context.Contract.TypeFactory.CreateClassType();
					string name = XmlName.IsNullOrEmpty(message.MessageName) ? null : message.MessageName.DecodedName;
					codeTypeReference = uniqueCodeNamespaceScope.AddUnique(codeTypeDeclaration, name, defaultName);
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.AddGeneratedTypedMessage(this.Context.Contract, message, codeTypeReference);
					if (this.MessageContractType == MessageContractType.BareMessageContract && message.Body.WrapperName != null)
					{
						this.WrapTypedMessage(ns, codeTypeDeclaration.Name, message, isReply, this.Context.IsInherited, hideFromEditor);
					}
					partCodeGenerator = new OperationGenerator.MethodSignatureGenerator.TypedMessagePartCodeGenerator(codeTypeDeclaration);
					if (hideFromEditor)
					{
						OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.AddEditorBrowsableAttribute(codeTypeDeclaration.CustomAttributes);
					}
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateWrapperAttribute(message, partCodeGenerator);
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateProtectionLevelAttribute(message, partCodeGenerator);
					foreach (MessageHeaderDescription setting in message.Headers)
					{
						this.GenerateHeaderPart(setting, partCodeGenerator);
					}
					if (isReply && message.Body.ReturnValue != null)
					{
						this.GenerateBodyPart(0, message.Body.ReturnValue, partCodeGenerator, true, this.IsEncoded, this.DefaultNS);
					}
				}
				return codeTypeReference;
			}

			// Token: 0x17001AF0 RID: 6896
			// (get) Token: 0x060074EF RID: 29935 RVA: 0x001B4D84 File Offset: 0x001B2F84
			private bool IsCompletelyUntyped
			{
				get
				{
					bool flag = this.Request != null && this.Request.IsUntypedMessage;
					bool flag2 = this.Response != null && this.Response.IsUntypedMessage;
					return (flag && flag2) || ((flag2 && this.Request == null) || this.IsEmpty(this.Request)) || ((flag && this.Response == null) || this.IsEmpty(this.Response));
				}
			}

			// Token: 0x060074F0 RID: 29936 RVA: 0x001B4DFD File Offset: 0x001B2FFD
			private bool IsEmpty(MessageDescription message)
			{
				return message.Body.Parts.Count == 0 && message.Headers.Count == 0;
			}

			// Token: 0x17001AF1 RID: 6897
			// (get) Token: 0x060074F1 RID: 29937 RVA: 0x001B4E24 File Offset: 0x001B3024
			private bool HasUntypedMessages
			{
				get
				{
					bool flag = this.Request != null && this.Request.IsUntypedMessage;
					bool flag2 = this.Response != null && this.Response.IsUntypedMessage;
					return flag || flag2;
				}
			}

			// Token: 0x060074F2 RID: 29938 RVA: 0x001B4E64 File Offset: 0x001B3064
			private void CreateUntypedMessages()
			{
				bool flag = this.Request != null && this.Request.IsUntypedMessage;
				bool flag2 = this.Response != null && this.Response.IsUntypedMessage;
				if (flag)
				{
					this.Method.Parameters.Insert(0, new CodeParameterDeclarationExpression(this.Context.ServiceContractGenerator.GetCodeTypeReference(typeof(Message)), "request"));
				}
				if (flag2)
				{
					this.EndMethod.ReturnType = this.Context.ServiceContractGenerator.GetCodeTypeReference(typeof(Message));
				}
			}

			// Token: 0x060074F3 RID: 29939 RVA: 0x001B4F00 File Offset: 0x001B3100
			private void CreateOrOverrideActionProperties()
			{
				if (this.Request != null)
				{
					OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<string>(OperationGenerator.CustomAttributeHelper.FindOrCreateAttributeDeclaration<OperationContractAttribute>(this.Method.CustomAttributes), "Action", this.Request.Action);
				}
				if (this.Response != null)
				{
					OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<string>(OperationGenerator.CustomAttributeHelper.FindOrCreateAttributeDeclaration<OperationContractAttribute>(this.Method.CustomAttributes), "ReplyAction", this.Response.Action);
				}
			}

			// Token: 0x060074F4 RID: 29940 RVA: 0x001B4F68 File Offset: 0x001B3168
			private void WrapTypedMessage(CodeNamespace ns, string typeName, MessageDescription messageDescription, bool isReply, bool isInherited, bool hideFromEditor)
			{
				UniqueCodeNamespaceScope uniqueCodeNamespaceScope = new UniqueCodeNamespaceScope(ns);
				CodeTypeDeclaration codeTypeDeclaration = this.Context.Contract.TypeFactory.CreateClassType();
				CodeTypeReference value = uniqueCodeNamespaceScope.AddUnique(codeTypeDeclaration, typeName + "Body", "Body");
				if (hideFromEditor)
				{
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.AddEditorBrowsableAttribute(codeTypeDeclaration.CustomAttributes);
				}
				string wrapperNamespace = this.GetWrapperNamespace(messageDescription);
				string messageName = XmlName.IsNullOrEmpty(messageDescription.MessageName) ? null : messageDescription.MessageName.DecodedName;
				this.WrappedBodyTypeGenerator.AddTypeAttributes(messageName, wrapperNamespace, codeTypeDeclaration.CustomAttributes, this.IsEncoded);
				OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator partGenerator = new OperationGenerator.MethodSignatureGenerator.TypedMessagePartCodeGenerator(codeTypeDeclaration);
				ProtectionLevel protectionLevel = ProtectionLevel.None;
				bool flag = false;
				if (messageDescription.Body.ReturnValue != null)
				{
					this.AddWrapperPart(messageDescription.MessageName, this.WrappedBodyTypeGenerator, partGenerator, messageDescription.Body.ReturnValue, codeTypeDeclaration.CustomAttributes);
					protectionLevel = ProtectionLevelHelper.Max(protectionLevel, messageDescription.Body.ReturnValue.ProtectionLevel);
					if (messageDescription.Body.ReturnValue.HasProtectionLevel)
					{
						flag = true;
					}
				}
				List<CodeTypeReference> list = new List<CodeTypeReference>();
				foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
				{
					this.AddWrapperPart(messageDescription.MessageName, this.WrappedBodyTypeGenerator, partGenerator, messagePartDescription, codeTypeDeclaration.CustomAttributes);
					protectionLevel = ProtectionLevelHelper.Max(protectionLevel, messagePartDescription.ProtectionLevel);
					if (messagePartDescription.HasProtectionLevel)
					{
						flag = true;
					}
					ICollection<CodeTypeReference> collection = null;
					if (this.KnownTypes != null && this.KnownTypes.TryGetValue(messagePartDescription, out collection))
					{
						foreach (CodeTypeReference item in collection)
						{
							list.Add(item);
						}
					}
				}
				messageDescription.Body.Parts.Clear();
				MessagePartDescription messagePartDescription2 = new MessagePartDescription(messageDescription.Body.WrapperName, messageDescription.Body.WrapperNamespace);
				if (this.KnownTypes != null)
				{
					this.KnownTypes.Add(messagePartDescription2, list);
				}
				if (flag)
				{
					messagePartDescription2.ProtectionLevel = protectionLevel;
				}
				messageDescription.Body.WrapperName = null;
				messageDescription.Body.WrapperNamespace = null;
				if (isReply)
				{
					messageDescription.Body.ReturnValue = messagePartDescription2;
				}
				else
				{
					messageDescription.Body.Parts.Add(messagePartDescription2);
				}
				OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateConstructors(codeTypeDeclaration);
				this.Parent.ParameterTypes.Add(messagePartDescription2, value);
				this.Parent.SpecialPartName.Add(messagePartDescription2, "Body");
			}

			// Token: 0x060074F5 RID: 29941 RVA: 0x001B520C File Offset: 0x001B340C
			private string GetWrapperNamespace(MessageDescription messageDescription)
			{
				string result = this.DefaultNS;
				if (messageDescription.Body.ReturnValue != null)
				{
					result = messageDescription.Body.ReturnValue.Namespace;
				}
				else if (messageDescription.Body.Parts.Count > 0)
				{
					result = messageDescription.Body.Parts[0].Namespace;
				}
				return result;
			}

			// Token: 0x060074F6 RID: 29942 RVA: 0x001B526C File Offset: 0x001B346C
			private void GenerateMessageBodyParts(bool generateTypedMessages)
			{
				int num = 0;
				if (this.IsNewRequest)
				{
					foreach (MessagePartDescription messagePart in this.Request.Body.Parts)
					{
						this.GenerateBodyPart(num++, messagePart, this.BeginPartCodeGenerator, generateTypedMessages, this.IsEncoded, this.DefaultNS);
					}
				}
				if (!this.Oneway && this.IsNewResponse)
				{
					num = ((this.Response.Body.ReturnValue != null) ? 1 : 0);
					foreach (MessagePartDescription messagePart2 in this.Response.Body.Parts)
					{
						this.GenerateBodyPart(num++, messagePart2, this.EndPartCodeGenerator, generateTypedMessages, this.IsEncoded, this.DefaultNS);
					}
				}
				if (this.IsNewRequest && this.BeginPartCodeGenerator != null)
				{
					this.BeginPartCodeGenerator.EndCodeGeneration();
				}
				if (this.IsNewResponse && this.EndPartCodeGenerator != null)
				{
					this.EndPartCodeGenerator.EndCodeGeneration();
				}
			}

			// Token: 0x060074F7 RID: 29943 RVA: 0x001B53A4 File Offset: 0x001B35A4
			private void AddWrapperPart(XmlName messageName, IWrappedBodyTypeGenerator wrappedBodyTypeGenerator, OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator partGenerator, MessagePartDescription part, CodeAttributeDeclarationCollection typeAttributes)
			{
				string codeName = part.CodeName;
				CodeTypeReference type;
				if (part.Type == typeof(Stream))
				{
					type = this.Context.ServiceContractGenerator.GetCodeTypeReference(typeof(byte[]));
				}
				else
				{
					type = this.GetParameterType(part);
				}
				CodeAttributeDeclarationCollection fieldAttributes = partGenerator.AddPart(type, ref codeName);
				CodeAttributeDeclarationCollection attributesImported = null;
				bool flag = this.Parent.ParameterAttributes.TryGetValue(part, out attributesImported);
				wrappedBodyTypeGenerator.AddMemberAttributes(messageName, part, attributesImported, typeAttributes, fieldAttributes);
				this.Parent.ParameterTypes.Remove(part);
				if (flag)
				{
					this.Parent.ParameterAttributes.Remove(part);
				}
			}

			// Token: 0x060074F8 RID: 29944 RVA: 0x001B5450 File Offset: 0x001B3650
			private void GenerateBodyPart(int order, MessagePartDescription messagePart, OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator partCodeGenerator, bool generateTypedMessage, bool isEncoded, string defaultNS)
			{
				if (!generateTypedMessage)
				{
					order = -1;
				}
				string codeName;
				if (!this.Parent.SpecialPartName.TryGetValue(messagePart, out codeName))
				{
					codeName = messagePart.CodeName;
				}
				CodeTypeReference parameterType = this.GetParameterType(messagePart);
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = partCodeGenerator.AddPart(parameterType, ref codeName);
				if (codeAttributeDeclarationCollection == null)
				{
					return;
				}
				XmlName defaultName = new XmlName(codeName);
				if (generateTypedMessage)
				{
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateMessageBodyMemberAttribute(order, messagePart, codeAttributeDeclarationCollection, defaultName);
				}
				else
				{
					OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.GenerateMessageParameterAttribute(messagePart, codeAttributeDeclarationCollection, defaultName, defaultNS);
				}
				this.AddAdditionalAttributes(messagePart, codeAttributeDeclarationCollection, generateTypedMessage || isEncoded);
			}

			// Token: 0x060074F9 RID: 29945 RVA: 0x001B54C4 File Offset: 0x001B36C4
			private void GenerateHeaderPart(MessageHeaderDescription setting, OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator parts)
			{
				string codeName;
				if (!this.Parent.SpecialPartName.TryGetValue(setting, out codeName))
				{
					codeName = setting.CodeName;
				}
				CodeTypeReference parameterType = this.GetParameterType(setting);
				CodeAttributeDeclarationCollection attributes = parts.AddPart(parameterType, ref codeName);
				OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateMessageHeaderAttribute(setting, attributes, new XmlName(codeName));
				this.AddAdditionalAttributes(setting, attributes, true);
			}

			// Token: 0x060074FA RID: 29946 RVA: 0x001B5518 File Offset: 0x001B3718
			private CodeTypeReference GetParameterType(MessagePartDescription setting)
			{
				if (setting.Type != null)
				{
					return this.Context.ServiceContractGenerator.GetCodeTypeReference(setting.Type);
				}
				if (this.Parent.parameterTypes.ContainsKey(setting))
				{
					return this.Parent.parameterTypes[setting];
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxNoTypeSpecifiedForParameter", new object[]
				{
					setting.Name
				})));
			}

			// Token: 0x060074FB RID: 29947 RVA: 0x001B5598 File Offset: 0x001B3798
			private void AddAdditionalAttributes(MessagePartDescription setting, CodeAttributeDeclarationCollection attributes, bool isAdditionalAttributesAllowed)
			{
				if (this.Parent.parameterAttributes != null && this.Parent.parameterAttributes.ContainsKey(setting))
				{
					CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = this.Parent.parameterAttributes[setting];
					if (codeAttributeDeclarationCollection != null && codeAttributeDeclarationCollection.Count > 0)
					{
						if (isAdditionalAttributesAllowed)
						{
							attributes.AddRange(codeAttributeDeclarationCollection);
							return;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SfxUseTypedMessageForCustomAttributes", new object[]
						{
							setting.Name,
							codeAttributeDeclarationCollection[0].AttributeType.BaseType
						})));
					}
				}
			}

			// Token: 0x060074FC RID: 29948 RVA: 0x001B562C File Offset: 0x001B382C
			internal void GenerateTaskSignature(ref OperationFormatStyle style)
			{
				this.Method = this.Context.TaskMethod;
				this.EndMethod = this.Context.TaskMethod;
				this.DefaultName = this.Context.SyncMethod.Name;
				this.GenerateOperationSignatures(ref style);
				CodeTypeReference returnType = this.Method.ReturnType;
				CodeTypeReference returnType2;
				if (returnType.BaseType == ServiceReflector.VoidType.FullName)
				{
					returnType2 = new CodeTypeReference(ServiceReflector.taskType);
				}
				else
				{
					returnType2 = new CodeTypeReference(this.Context.ServiceContractGenerator.GetCodeTypeReference(ServiceReflector.taskTResultType).BaseType, new CodeTypeReference[]
					{
						returnType
					});
				}
				this.Method.ReturnType = returnType2;
			}

			// Token: 0x0400421F RID: 16927
			private readonly OperationGenerator Parent;

			// Token: 0x04004220 RID: 16928
			private readonly OperationContractGenerationContext Context;

			// Token: 0x04004221 RID: 16929
			private readonly OperationFormatStyle Style;

			// Token: 0x04004222 RID: 16930
			private readonly bool IsEncoded;

			// Token: 0x04004223 RID: 16931
			private readonly IWrappedBodyTypeGenerator WrappedBodyTypeGenerator;

			// Token: 0x04004224 RID: 16932
			private readonly Dictionary<MessagePartDescription, ICollection<CodeTypeReference>> KnownTypes;

			// Token: 0x04004225 RID: 16933
			private CodeMemberMethod Method;

			// Token: 0x04004226 RID: 16934
			private CodeMemberMethod EndMethod;

			// Token: 0x04004227 RID: 16935
			private readonly string ContractName;

			// Token: 0x04004228 RID: 16936
			private string DefaultName;

			// Token: 0x04004229 RID: 16937
			private readonly string ContractNS;

			// Token: 0x0400422A RID: 16938
			private readonly string DefaultNS;

			// Token: 0x0400422B RID: 16939
			private readonly bool Oneway;

			// Token: 0x0400422C RID: 16940
			private readonly MessageDescription Request;

			// Token: 0x0400422D RID: 16941
			private readonly MessageDescription Response;

			// Token: 0x0400422E RID: 16942
			private bool IsNewRequest;

			// Token: 0x0400422F RID: 16943
			private bool IsNewResponse;

			// Token: 0x04004230 RID: 16944
			private bool IsTaskWithOutputParameters;

			// Token: 0x04004231 RID: 16945
			private MessageContractType MessageContractType;

			// Token: 0x04004232 RID: 16946
			private OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator BeginPartCodeGenerator;

			// Token: 0x04004233 RID: 16947
			private OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator EndPartCodeGenerator;

			// Token: 0x02000F19 RID: 3865
			private interface IPartCodeGenerator
			{
				// Token: 0x06008617 RID: 34327
				CodeAttributeDeclarationCollection AddPart(CodeTypeReference type, ref string name);

				// Token: 0x17001D73 RID: 7539
				// (get) Token: 0x06008618 RID: 34328
				CodeAttributeDeclarationCollection MessageLevelAttributes { get; }

				// Token: 0x06008619 RID: 34329
				void EndCodeGeneration();
			}

			// Token: 0x02000F1A RID: 3866
			private class ParameterizedMethodGenerator
			{
				// Token: 0x0600861A RID: 34330 RVA: 0x001F1078 File Offset: 0x001EF278
				internal ParameterizedMethodGenerator(CodeMemberMethod beginMethod, CodeMemberMethod endMethod)
				{
					this.ins = new OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator(this, beginMethod.Name, beginMethod.Parameters, beginMethod.CustomAttributes, FieldDirection.In);
					this.outs = new OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator(this, beginMethod.Name, endMethod.Parameters, beginMethod.CustomAttributes, FieldDirection.Out);
					this.isSync = (beginMethod == endMethod);
				}

				// Token: 0x0600861B RID: 34331 RVA: 0x001F10D4 File Offset: 0x001EF2D4
				internal CodeParameterDeclarationExpression GetOrCreateParameter(CodeTypeReference type, string name, FieldDirection direction, ref int index, out bool createdNew)
				{
					OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator parametersPartCodeGenerator = (direction != FieldDirection.In) ? this.ins : this.outs;
					int num = index;
					CodeParameterDeclarationExpression parameter = parametersPartCodeGenerator.GetParameter(name, ref num);
					bool flag = parameter != null && parameter.Type.BaseType == type.BaseType;
					if (flag)
					{
						parameter.Direction = FieldDirection.Ref;
						if (this.isSync)
						{
							index = num + 1;
							createdNew = false;
							return parameter;
						}
					}
					CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
					codeParameterDeclarationExpression.Name = name;
					codeParameterDeclarationExpression.Type = type;
					codeParameterDeclarationExpression.Direction = direction;
					if (flag)
					{
						codeParameterDeclarationExpression.Direction = FieldDirection.Ref;
					}
					createdNew = true;
					return codeParameterDeclarationExpression;
				}

				// Token: 0x17001D74 RID: 7540
				// (get) Token: 0x0600861C RID: 34332 RVA: 0x001F116C File Offset: 0x001EF36C
				internal OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator InputGenerator
				{
					get
					{
						return this.ins;
					}
				}

				// Token: 0x17001D75 RID: 7541
				// (get) Token: 0x0600861D RID: 34333 RVA: 0x001F1174 File Offset: 0x001EF374
				internal OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator OutputGenerator
				{
					get
					{
						return this.outs;
					}
				}

				// Token: 0x04004DD4 RID: 19924
				private OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator ins;

				// Token: 0x04004DD5 RID: 19925
				private OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator outs;

				// Token: 0x04004DD6 RID: 19926
				private bool isSync;

				// Token: 0x02000FC3 RID: 4035
				private class ParametersPartCodeGenerator : OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator
				{
					// Token: 0x060088CB RID: 35019 RVA: 0x001FD9E8 File Offset: 0x001FBBE8
					internal ParametersPartCodeGenerator(OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator parent, string methodName, CodeParameterDeclarationExpressionCollection parameters, CodeAttributeDeclarationCollection messageAttrs, FieldDirection direction)
					{
						this.parent = parent;
						this.methodName = methodName;
						this.parameters = parameters;
						this.messageAttrs = messageAttrs;
						this.direction = direction;
						this.index = 0;
					}

					// Token: 0x060088CC RID: 35020 RVA: 0x001FDA1C File Offset: 0x001FBC1C
					public bool NameExists(string name)
					{
						if (string.Compare(name, this.methodName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						int num = 0;
						return this.GetParameter(name, ref num) != null;
					}

					// Token: 0x060088CD RID: 35021 RVA: 0x001FDA48 File Offset: 0x001FBC48
					CodeAttributeDeclarationCollection OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator.AddPart(CodeTypeReference type, ref string name)
					{
						name = UniqueCodeIdentifierScope.MakeValid(name, "param");
						bool flag;
						CodeParameterDeclarationExpression orCreateParameter = this.parent.GetOrCreateParameter(type, name, this.direction, ref this.index, out flag);
						if (flag)
						{
							orCreateParameter.Name = OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator.GetUniqueParameterName(orCreateParameter.Name, this);
							CodeParameterDeclarationExpressionCollection codeParameterDeclarationExpressionCollection = this.parameters;
							int num = this.index;
							this.index = num + 1;
							codeParameterDeclarationExpressionCollection.Insert(num, orCreateParameter);
						}
						name = orCreateParameter.Name;
						if (!flag)
						{
							return null;
						}
						return orCreateParameter.CustomAttributes;
					}

					// Token: 0x060088CE RID: 35022 RVA: 0x001FDAC8 File Offset: 0x001FBCC8
					internal CodeParameterDeclarationExpression GetParameter(string name, ref int index)
					{
						for (int i = index; i < this.parameters.Count; i++)
						{
							CodeParameterDeclarationExpression codeParameterDeclarationExpression = this.parameters[i];
							if (string.Compare(codeParameterDeclarationExpression.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
							{
								index = i;
								return codeParameterDeclarationExpression;
							}
						}
						return null;
					}

					// Token: 0x17001DB1 RID: 7601
					// (get) Token: 0x060088CF RID: 35023 RVA: 0x001FDB0E File Offset: 0x001FBD0E
					CodeAttributeDeclarationCollection OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator.MessageLevelAttributes
					{
						get
						{
							return this.messageAttrs;
						}
					}

					// Token: 0x060088D0 RID: 35024 RVA: 0x001FDB16 File Offset: 0x001FBD16
					void OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator.EndCodeGeneration()
					{
					}

					// Token: 0x060088D1 RID: 35025 RVA: 0x001FDB18 File Offset: 0x001FBD18
					private static string GetUniqueParameterName(string name, OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator parameters)
					{
						return NamingHelper.GetUniqueName(name, new NamingHelper.DoesNameExist(OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator.DoesParameterNameExist), parameters);
					}

					// Token: 0x060088D2 RID: 35026 RVA: 0x001FDB2D File Offset: 0x001FBD2D
					private static bool DoesParameterNameExist(string name, object parametersObject)
					{
						return ((OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator.ParametersPartCodeGenerator)parametersObject).NameExists(name);
					}

					// Token: 0x0400506D RID: 20589
					private OperationGenerator.MethodSignatureGenerator.ParameterizedMethodGenerator parent;

					// Token: 0x0400506E RID: 20590
					private FieldDirection direction;

					// Token: 0x0400506F RID: 20591
					private CodeParameterDeclarationExpressionCollection parameters;

					// Token: 0x04005070 RID: 20592
					private CodeAttributeDeclarationCollection messageAttrs;

					// Token: 0x04005071 RID: 20593
					private string methodName;

					// Token: 0x04005072 RID: 20594
					private int index;
				}
			}

			// Token: 0x02000F1B RID: 3867
			private class TypedMessagePartCodeGenerator : OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator
			{
				// Token: 0x0600861E RID: 34334 RVA: 0x001F117C File Offset: 0x001EF37C
				internal TypedMessagePartCodeGenerator(CodeTypeDeclaration typeDecl)
				{
					this.typeDecl = typeDecl;
					this.memberScope = new UniqueCodeIdentifierScope();
					this.memberScope.AddReserved(typeDecl.Name);
				}

				// Token: 0x0600861F RID: 34335 RVA: 0x001F11A8 File Offset: 0x001EF3A8
				CodeAttributeDeclarationCollection OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator.AddPart(CodeTypeReference type, ref string name)
				{
					CodeMemberField codeMemberField = new CodeMemberField();
					CodeTypeMember codeTypeMember = codeMemberField;
					string name2;
					name = (name2 = this.memberScope.AddUnique(name, "member"));
					codeTypeMember.Name = name2;
					codeMemberField.Type = type;
					codeMemberField.Attributes = MemberAttributes.Public;
					this.typeDecl.Members.Add(codeMemberField);
					return codeMemberField.CustomAttributes;
				}

				// Token: 0x17001D76 RID: 7542
				// (get) Token: 0x06008620 RID: 34336 RVA: 0x001F1202 File Offset: 0x001EF402
				CodeAttributeDeclarationCollection OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator.MessageLevelAttributes
				{
					get
					{
						return this.typeDecl.CustomAttributes;
					}
				}

				// Token: 0x06008621 RID: 34337 RVA: 0x001F120F File Offset: 0x001EF40F
				void OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator.EndCodeGeneration()
				{
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateConstructors(this.typeDecl);
				}

				// Token: 0x04004DD7 RID: 19927
				private CodeTypeDeclaration typeDecl;

				// Token: 0x04004DD8 RID: 19928
				private UniqueCodeIdentifierScope memberScope;
			}

			// Token: 0x02000F1C RID: 3868
			private static class TypedMessageHelper
			{
				// Token: 0x06008622 RID: 34338 RVA: 0x001F121C File Offset: 0x001EF41C
				internal static void GenerateProtectionLevelAttribute(MessageDescription message, OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator partCodeGenerator)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = OperationGenerator.CustomAttributeHelper.FindOrCreateAttributeDeclaration<MessageContractAttribute>(partCodeGenerator.MessageLevelAttributes);
					if (message.HasProtectionLevel)
					{
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ProtectionLevel", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ProtectionLevel)), message.ProtectionLevel.ToString())));
					}
				}

				// Token: 0x06008623 RID: 34339 RVA: 0x001F127C File Offset: 0x001EF47C
				internal static void GenerateWrapperAttribute(MessageDescription message, OperationGenerator.MethodSignatureGenerator.IPartCodeGenerator partCodeGenerator)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = OperationGenerator.CustomAttributeHelper.FindOrCreateAttributeDeclaration<MessageContractAttribute>(partCodeGenerator.MessageLevelAttributes);
					if (message.Body.WrapperName != null)
					{
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("WrapperName", new CodePrimitiveExpression(NamingHelper.CodeName(message.Body.WrapperName))));
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("WrapperNamespace", new CodePrimitiveExpression(message.Body.WrapperNamespace)));
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsWrapped", new CodePrimitiveExpression(true)));
						return;
					}
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsWrapped", new CodePrimitiveExpression(false)));
				}

				// Token: 0x06008624 RID: 34340 RVA: 0x001F1336 File Offset: 0x001EF536
				internal static void AddEditorBrowsableAttribute(CodeAttributeDeclarationCollection attributes)
				{
					attributes.Add(ClientClassGenerator.CreateEditorBrowsableAttribute(EditorBrowsableState.Advanced));
				}

				// Token: 0x06008625 RID: 34341 RVA: 0x001F1345 File Offset: 0x001EF545
				internal static void AddGeneratedTypedMessage(ServiceContractGenerationContext contract, MessageDescription message, CodeTypeReference codeTypeReference)
				{
					if (message.XsdTypeName != null && !message.XsdTypeName.IsEmpty)
					{
						contract.ServiceContractGenerator.GeneratedTypedMessages.Add(message, codeTypeReference);
					}
				}

				// Token: 0x06008626 RID: 34342 RVA: 0x001F1374 File Offset: 0x001EF574
				internal static bool FindGeneratedTypedMessage(ServiceContractGenerationContext contract, MessageDescription message, out CodeTypeReference codeTypeReference)
				{
					if (message.XsdTypeName == null || message.XsdTypeName.IsEmpty)
					{
						codeTypeReference = null;
						return false;
					}
					return contract.ServiceContractGenerator.GeneratedTypedMessages.TryGetValue(message, out codeTypeReference);
				}

				// Token: 0x06008627 RID: 34343 RVA: 0x001F13A8 File Offset: 0x001EF5A8
				internal static void GenerateConstructors(CodeTypeDeclaration typeDecl)
				{
					CodeConstructor codeConstructor = new CodeConstructor();
					codeConstructor.Attributes = MemberAttributes.Public;
					typeDecl.Members.Add(codeConstructor);
					CodeConstructor codeConstructor2 = new CodeConstructor();
					codeConstructor2.Attributes = MemberAttributes.Public;
					foreach (object obj in typeDecl.Members)
					{
						CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
						CodeMemberField codeMemberField = codeTypeMember as CodeMemberField;
						if (codeMemberField != null)
						{
							CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(codeMemberField.Type, codeMemberField.Name);
							codeConstructor2.Parameters.Add(codeParameterDeclarationExpression);
							codeConstructor2.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), codeMemberField.Name), new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name)));
						}
					}
					if (codeConstructor2.Parameters.Count > 0)
					{
						typeDecl.Members.Add(codeConstructor2);
					}
				}

				// Token: 0x06008628 RID: 34344 RVA: 0x001F14A4 File Offset: 0x001EF6A4
				internal static void GenerateMessageBodyMemberAttribute(int order, MessagePartDescription setting, CodeAttributeDeclarationCollection attributes, XmlName defaultName)
				{
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateMessageContractMemberAttribute<MessageBodyMemberAttribute>(order, setting, attributes, defaultName);
				}

				// Token: 0x06008629 RID: 34345 RVA: 0x001F14AF File Offset: 0x001EF6AF
				internal static void GenerateMessageHeaderAttribute(MessageHeaderDescription setting, CodeAttributeDeclarationCollection attributes, XmlName defaultName)
				{
					if (setting.Multiple)
					{
						OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateMessageContractMemberAttribute<MessageHeaderArrayAttribute>(-1, setting, attributes, defaultName);
						return;
					}
					OperationGenerator.MethodSignatureGenerator.TypedMessageHelper.GenerateMessageContractMemberAttribute<MessageHeaderAttribute>(-1, setting, attributes, defaultName);
				}

				// Token: 0x0600862A RID: 34346 RVA: 0x001F14CC File Offset: 0x001EF6CC
				private static void GenerateMessageContractMemberAttribute<T>(int order, MessagePartDescription setting, CodeAttributeDeclarationCollection attrs, XmlName defaultName) where T : Attribute
				{
					CodeAttributeDeclaration attribute = OperationGenerator.CustomAttributeHelper.FindOrCreateAttributeDeclaration<T>(attrs);
					if (setting.Name != defaultName.EncodedName)
					{
						OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<string>(attribute, "Name", setting.Name);
					}
					OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<string>(attribute, "Namespace", setting.Namespace);
					if (setting.HasProtectionLevel)
					{
						OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<ProtectionLevel>(attribute, "ProtectionLevel", setting.ProtectionLevel);
					}
					if (order >= 0)
					{
						OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<int>(attribute, "Order", order);
					}
				}
			}

			// Token: 0x02000F1D RID: 3869
			private static class ParameterizedMessageHelper
			{
				// Token: 0x0600862B RID: 34347 RVA: 0x001F1540 File Offset: 0x001EF740
				internal static void GenerateMessageParameterAttribute(MessagePartDescription setting, CodeAttributeDeclarationCollection attributes, XmlName defaultName, string defaultNS)
				{
					if (setting.Name != defaultName.EncodedName)
					{
						OperationGenerator.CustomAttributeHelper.CreateOrOverridePropertyDeclaration<string>(OperationGenerator.CustomAttributeHelper.FindOrCreateAttributeDeclaration<MessageParameterAttribute>(attributes), "Name", setting.Name);
					}
					if (setting.Namespace != defaultNS)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_NamespaceMismatch", new object[]
						{
							setting.Namespace,
							defaultNS
						})));
					}
				}

				// Token: 0x0600862C RID: 34348 RVA: 0x001F15B4 File Offset: 0x001EF7B4
				internal static void ValidateProtectionLevel(OperationGenerator.MethodSignatureGenerator parent)
				{
					if (parent.Request != null && parent.Request.HasProtectionLevel)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_MessageHasProtectionLevel", new object[]
						{
							(parent.Request.Action == null) ? "" : parent.Request.Action
						})));
					}
					if (parent.Response != null && parent.Response.HasProtectionLevel)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_MessageHasProtectionLevel", new object[]
						{
							(parent.Response.Action == null) ? "" : parent.Response.Action
						})));
					}
				}

				// Token: 0x0600862D RID: 34349 RVA: 0x001F1670 File Offset: 0x001EF870
				internal static void ValidateWrapperSettings(OperationGenerator.MethodSignatureGenerator parent)
				{
					if (parent.Request.Body.WrapperName == null || (parent.Response != null && parent.Response.Body.WrapperName == null))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_Bare", new object[]
						{
							parent.Context.Operation.CodeName
						})));
					}
					if (!OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.StringEqualOrNull(parent.Request.Body.WrapperNamespace, parent.ContractNS))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_DifferentWrapperNs", new object[]
						{
							parent.Request.MessageName,
							parent.Request.Body.WrapperNamespace,
							parent.ContractNS
						})));
					}
					XmlName xmlName = new XmlName(parent.DefaultName);
					if (!string.Equals(parent.Request.Body.WrapperName, xmlName.EncodedName, StringComparison.Ordinal))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_DifferentWrapperName", new object[]
						{
							parent.Request.MessageName,
							parent.Request.Body.WrapperName,
							xmlName.EncodedName
						})));
					}
					if (parent.Response != null)
					{
						if (!OperationGenerator.MethodSignatureGenerator.ParameterizedMessageHelper.StringEqualOrNull(parent.Response.Body.WrapperNamespace, parent.ContractNS))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_DifferentWrapperNs", new object[]
							{
								parent.Response.MessageName,
								parent.Response.Body.WrapperNamespace,
								parent.ContractNS
							})));
						}
						if (!string.Equals(parent.Response.Body.WrapperName, TypeLoader.GetBodyWrapperResponseName(xmlName).EncodedName, StringComparison.Ordinal))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_DifferentWrapperName", new object[]
							{
								parent.Response.MessageName,
								parent.Response.Body.WrapperName,
								xmlName.EncodedName
							})));
						}
					}
				}

				// Token: 0x0600862E RID: 34350 RVA: 0x001F1898 File Offset: 0x001EFA98
				internal static void ValidateNoHeaders(OperationGenerator.MethodSignatureGenerator parent)
				{
					if (parent.Request.Headers.Count > 0)
					{
						if (!parent.IsEncoded)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_HeadersAreUnsupported", new object[]
							{
								parent.Request.MessageName
							})));
						}
						parent.Context.Contract.ServiceContractGenerator.Errors.Add(new MetadataConversionError(SR.GetString("SFxCannotImportAsParameters_HeadersAreIgnoredInEncoded", new object[]
						{
							parent.Request.MessageName
						}), true));
					}
					if (parent.Oneway || parent.Response.Headers.Count <= 0)
					{
						return;
					}
					if (parent.IsEncoded)
					{
						parent.Context.Contract.ServiceContractGenerator.Errors.Add(new MetadataConversionError(SR.GetString("SFxCannotImportAsParameters_HeadersAreIgnoredInEncoded", new object[]
						{
							parent.Response.MessageName
						}), true));
						return;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_HeadersAreUnsupported", new object[]
					{
						parent.Response.MessageName
					})));
				}

				// Token: 0x0600862F RID: 34351 RVA: 0x001F19C1 File Offset: 0x001EFBC1
				private static bool StringEqualOrNull(string overrideValue, string defaultValue)
				{
					return overrideValue == null || string.Equals(overrideValue, defaultValue, StringComparison.Ordinal);
				}
			}
		}

		// Token: 0x02000BCE RID: 3022
		private static class CustomAttributeHelper
		{
			// Token: 0x060074FD RID: 29949 RVA: 0x001B56DE File Offset: 0x001B38DE
			internal static void CreateOrOverridePropertyDeclaration<V>(CodeAttributeDeclaration attribute, string propertyName, V value)
			{
				SecurityAttributeGenerationHelper.CreateOrOverridePropertyDeclaration<V>(attribute, propertyName, value);
			}

			// Token: 0x060074FE RID: 29950 RVA: 0x001B56E8 File Offset: 0x001B38E8
			internal static CodeAttributeDeclaration FindOrCreateAttributeDeclaration<T>(CodeAttributeDeclarationCollection attributes) where T : Attribute
			{
				return SecurityAttributeGenerationHelper.FindOrCreateAttributeDeclaration<T>(attributes);
			}

			// Token: 0x060074FF RID: 29951 RVA: 0x001B56F0 File Offset: 0x001B38F0
			internal static CodeAttributeDeclaration GenerateAttributeDeclaration(ServiceContractGenerator generator, Attribute attribute)
			{
				Type type = attribute.GetType();
				Attribute obj = (Attribute)Activator.CreateInstance(type);
				MemberInfo[] members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public);
				Array.Sort<MemberInfo>(members, (MemberInfo a, MemberInfo b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(generator.GetCodeTypeReference(type));
				foreach (MemberInfo memberInfo in members)
				{
					if (!(memberInfo.DeclaringType == typeof(Attribute)))
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							object value = fieldInfo.GetValue(attribute);
							object value2 = fieldInfo.GetValue(obj);
							if (!object.Equals(value, value2))
							{
								codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(fieldInfo.Name, OperationGenerator.CustomAttributeHelper.GetArgValue(value)));
							}
						}
						else
						{
							PropertyInfo propertyInfo = memberInfo as PropertyInfo;
							if (propertyInfo != null)
							{
								object value3 = propertyInfo.GetValue(attribute, null);
								object value4 = propertyInfo.GetValue(obj, null);
								if (!object.Equals(value3, value4))
								{
									codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(propertyInfo.Name, OperationGenerator.CustomAttributeHelper.GetArgValue(value3)));
								}
							}
						}
					}
				}
				return codeAttributeDeclaration;
			}

			// Token: 0x06007500 RID: 29952 RVA: 0x001B5830 File Offset: 0x001B3A30
			private static CodeExpression GetArgValue(object val)
			{
				Type type = val.GetType();
				if (type.IsPrimitive || type == typeof(string))
				{
					return new CodePrimitiveExpression(val);
				}
				if (type.IsEnum)
				{
					return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(type), Enum.Format(type, val, "G"));
				}
				return null;
			}
		}
	}
}

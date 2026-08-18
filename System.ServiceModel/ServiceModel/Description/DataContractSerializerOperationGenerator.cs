using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003FF RID: 1023
	internal class DataContractSerializerOperationGenerator : IOperationBehavior, IOperationContractGenerationExtension
	{
		// Token: 0x060026FF RID: 9983 RVA: 0x0008EFB4 File Offset: 0x0008D1B4
		public DataContractSerializerOperationGenerator() : this(new CodeCompileUnit())
		{
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x0008EFC1 File Offset: 0x0008D1C1
		public DataContractSerializerOperationGenerator(CodeCompileUnit codeCompileUnit)
		{
			this.codeCompileUnit = codeCompileUnit;
			this.operationGenerator = new OperationGenerator();
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0008EFE8 File Offset: 0x0008D1E8
		internal void Add(MessagePartDescription part, CodeTypeReference typeReference, ICollection<CodeTypeReference> knownTypeReferences, bool isNonNillableReferenceType)
		{
			this.OperationGenerator.ParameterTypes.Add(part, typeReference);
			if (knownTypeReferences != null)
			{
				this.KnownTypes.Add(part, knownTypeReferences);
			}
			if (isNonNillableReferenceType)
			{
				if (this.isNonNillableReferenceTypes == null)
				{
					this.isNonNillableReferenceTypes = new Dictionary<MessagePartDescription, bool>();
				}
				this.isNonNillableReferenceTypes.Add(part, isNonNillableReferenceType);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0008F03C File Offset: 0x0008D23C
		internal OperationGenerator OperationGenerator
		{
			get
			{
				return this.operationGenerator;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x0008F044 File Offset: 0x0008D244
		internal Dictionary<OperationDescription, DataContractFormatAttribute> OperationAttributes
		{
			get
			{
				return this.operationAttributes;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0008F04C File Offset: 0x0008D24C
		internal Dictionary<MessagePartDescription, ICollection<CodeTypeReference>> KnownTypes
		{
			get
			{
				if (this.knownTypes == null)
				{
					this.knownTypes = new Dictionary<MessagePartDescription, ICollection<CodeTypeReference>>();
				}
				return this.knownTypes;
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0008F067 File Offset: 0x0008D267
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0008F069 File Offset: 0x0008D269
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0008F06B File Offset: 0x0008D26B
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0008F06D File Offset: 0x0008D26D
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0008F070 File Offset: 0x0008D270
		void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
		{
			DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = context.Operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
			DataContractFormatAttribute dataContractFormatAttribute = (dataContractSerializerOperationBehavior == null) ? new DataContractFormatAttribute() : dataContractSerializerOperationBehavior.DataContractFormatAttribute;
			OperationFormatStyle style = dataContractFormatAttribute.Style;
			this.operationGenerator.GenerateOperation(context, ref style, false, new DataContractSerializerOperationGenerator.WrappedBodyTypeGenerator(this, context), this.knownTypes);
			dataContractFormatAttribute.Style = style;
			if (dataContractFormatAttribute.Style != TypeLoader.DefaultDataContractFormatAttribute.Style)
			{
				context.SyncMethod.CustomAttributes.Add(OperationGenerator.GenerateAttributeDeclaration(context.Contract.ServiceContractGenerator, dataContractFormatAttribute));
			}
			if (this.knownTypes != null)
			{
				Dictionary<CodeTypeReference, object> operationKnownTypes = new Dictionary<CodeTypeReference, object>(new DataContractSerializerOperationGenerator.CodeTypeReferenceComparer());
				foreach (MessageDescription messageDescription in context.Operation.Messages)
				{
					foreach (MessagePartDescription part in messageDescription.Body.Parts)
					{
						this.AddKnownTypesForPart(context, part, operationKnownTypes);
					}
					foreach (MessageHeaderDescription part2 in messageDescription.Headers)
					{
						this.AddKnownTypesForPart(context, part2, operationKnownTypes);
					}
					if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
					{
						this.AddKnownTypesForPart(context, messageDescription.Body.ReturnValue, operationKnownTypes);
					}
				}
			}
			DataContractSerializerOperationGenerator.UpdateTargetCompileUnit(context, this.codeCompileUnit);
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0008F224 File Offset: 0x0008D424
		private void AddKnownTypesForPart(OperationContractGenerationContext context, MessagePartDescription part, Dictionary<CodeTypeReference, object> operationKnownTypes)
		{
			ICollection<CodeTypeReference> collection;
			if (this.knownTypes.TryGetValue(part, out collection))
			{
				foreach (CodeTypeReference codeTypeReference in collection)
				{
					object obj;
					if (!operationKnownTypes.TryGetValue(codeTypeReference, out obj))
					{
						operationKnownTypes.Add(codeTypeReference, null);
						CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(ServiceKnownTypeAttribute).FullName);
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(codeTypeReference)));
						context.SyncMethod.CustomAttributes.Add(codeAttributeDeclaration);
					}
				}
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0008F2CC File Offset: 0x0008D4CC
		internal static void UpdateTargetCompileUnit(OperationContractGenerationContext context, CodeCompileUnit codeCompileUnit)
		{
			CodeCompileUnit targetCompileUnit = context.ServiceContractGenerator.TargetCompileUnit;
			if (targetCompileUnit != codeCompileUnit)
			{
				foreach (object obj in codeCompileUnit.Namespaces)
				{
					CodeNamespace value = (CodeNamespace)obj;
					if (!targetCompileUnit.Namespaces.Contains(value))
					{
						targetCompileUnit.Namespaces.Add(value);
					}
				}
				foreach (string value2 in codeCompileUnit.ReferencedAssemblies)
				{
					if (!targetCompileUnit.ReferencedAssemblies.Contains(value2))
					{
						targetCompileUnit.ReferencedAssemblies.Add(value2);
					}
				}
				foreach (object obj2 in codeCompileUnit.AssemblyCustomAttributes)
				{
					CodeAttributeDeclaration value3 = (CodeAttributeDeclaration)obj2;
					if (!targetCompileUnit.AssemblyCustomAttributes.Contains(value3))
					{
						targetCompileUnit.AssemblyCustomAttributes.Add(value3);
					}
				}
				foreach (object obj3 in codeCompileUnit.StartDirectives)
				{
					CodeDirective value4 = (CodeDirective)obj3;
					if (!targetCompileUnit.StartDirectives.Contains(value4))
					{
						targetCompileUnit.StartDirectives.Add(value4);
					}
				}
				foreach (object obj4 in codeCompileUnit.EndDirectives)
				{
					CodeDirective value5 = (CodeDirective)obj4;
					if (!targetCompileUnit.EndDirectives.Contains(value5))
					{
						targetCompileUnit.EndDirectives.Add(value5);
					}
				}
				foreach (object obj5 in codeCompileUnit.UserData)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj5;
					targetCompileUnit.UserData[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
		}

		// Token: 0x040021D6 RID: 8662
		private Dictionary<OperationDescription, DataContractFormatAttribute> operationAttributes = new Dictionary<OperationDescription, DataContractFormatAttribute>();

		// Token: 0x040021D7 RID: 8663
		private OperationGenerator operationGenerator;

		// Token: 0x040021D8 RID: 8664
		private Dictionary<MessagePartDescription, ICollection<CodeTypeReference>> knownTypes;

		// Token: 0x040021D9 RID: 8665
		private Dictionary<MessagePartDescription, bool> isNonNillableReferenceTypes;

		// Token: 0x040021DA RID: 8666
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x02000BB0 RID: 2992
		internal class WrappedBodyTypeGenerator : IWrappedBodyTypeGenerator
		{
			// Token: 0x06007425 RID: 29733 RVA: 0x001B1ACC File Offset: 0x001AFCCC
			public void ValidateForParameterMode(OperationDescription operation)
			{
				if (this.dataContractSerializerOperationGenerator.isNonNillableReferenceTypes == null)
				{
					return;
				}
				foreach (MessageDescription messageDescription in operation.Messages)
				{
					if (messageDescription.Body != null)
					{
						if (messageDescription.Body.ReturnValue != null)
						{
							this.ValidateForParameterMode(messageDescription.Body.ReturnValue);
						}
						foreach (MessagePartDescription part in messageDescription.Body.Parts)
						{
							this.ValidateForParameterMode(part);
						}
					}
				}
			}

			// Token: 0x06007426 RID: 29734 RVA: 0x001B1B88 File Offset: 0x001AFD88
			private void ValidateForParameterMode(MessagePartDescription part)
			{
				if (this.dataContractSerializerOperationGenerator.isNonNillableReferenceTypes.ContainsKey(part))
				{
					ParameterModeException ex = new ParameterModeException(SR.GetString("SFxCannotImportAsParameters_ElementIsNotNillable", new object[]
					{
						part.Name,
						part.Namespace
					}));
					ex.MessageContractType = MessageContractType.BareMessageContract;
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
			}

			// Token: 0x06007427 RID: 29735 RVA: 0x001B1BE3 File Offset: 0x001AFDE3
			public WrappedBodyTypeGenerator(DataContractSerializerOperationGenerator dataContractSerializerOperationGenerator, OperationContractGenerationContext context)
			{
				this.context = context;
				this.dataContractSerializerOperationGenerator = dataContractSerializerOperationGenerator;
			}

			// Token: 0x06007428 RID: 29736 RVA: 0x001B1BFC File Offset: 0x001AFDFC
			public void AddMemberAttributes(XmlName messageName, MessagePartDescription part, CodeAttributeDeclarationCollection attributesImported, CodeAttributeDeclarationCollection typeAttributes, CodeAttributeDeclarationCollection fieldAttributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = null;
				foreach (object obj in typeAttributes)
				{
					CodeAttributeDeclaration codeAttributeDeclaration2 = (CodeAttributeDeclaration)obj;
					if (codeAttributeDeclaration2.AttributeType.BaseType == DataContractSerializerOperationGenerator.WrappedBodyTypeGenerator.dataContractAttributeTypeRef.BaseType)
					{
						codeAttributeDeclaration = codeAttributeDeclaration2;
						break;
					}
				}
				if (codeAttributeDeclaration == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(string.Format(CultureInfo.InvariantCulture, "Cannot find DataContract attribute for  {0}", new object[]
					{
						messageName
					})));
				}
				bool flag = false;
				foreach (object obj2 in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj2;
					if (codeAttributeArgument.Name == "Namespace")
					{
						flag = true;
						string a = ((CodePrimitiveExpression)codeAttributeArgument.Value).Value.ToString();
						if (a != part.Namespace)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWrapperTypeHasMultipleNamespaces", new object[]
							{
								messageName
							})));
						}
					}
				}
				if (!flag)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(part.Namespace)));
				}
				DataMemberAttribute dataMemberAttribute = new DataMemberAttribute();
				DataMemberAttribute dataMemberAttribute2 = dataMemberAttribute;
				int num = this.memberCount;
				this.memberCount = num + 1;
				dataMemberAttribute2.Order = num;
				dataMemberAttribute.EmitDefaultValue = !this.IsNonNillableReferenceType(part);
				fieldAttributes.Add(OperationGenerator.GenerateAttributeDeclaration(this.context.Contract.ServiceContractGenerator, dataMemberAttribute));
			}

			// Token: 0x06007429 RID: 29737 RVA: 0x001B1DBC File Offset: 0x001AFFBC
			private bool IsNonNillableReferenceType(MessagePartDescription part)
			{
				return this.dataContractSerializerOperationGenerator.isNonNillableReferenceTypes != null && this.dataContractSerializerOperationGenerator.isNonNillableReferenceTypes.ContainsKey(part);
			}

			// Token: 0x0600742A RID: 29738 RVA: 0x001B1DDE File Offset: 0x001AFFDE
			public void AddTypeAttributes(string messageName, string typeNS, CodeAttributeDeclarationCollection typeAttributes, bool isEncoded)
			{
				typeAttributes.Add(OperationGenerator.GenerateAttributeDeclaration(this.context.Contract.ServiceContractGenerator, new DataContractAttribute()));
				this.memberCount = 0;
			}

			// Token: 0x040041CA RID: 16842
			private static CodeTypeReference dataContractAttributeTypeRef = new CodeTypeReference(typeof(DataContractAttribute));

			// Token: 0x040041CB RID: 16843
			private int memberCount;

			// Token: 0x040041CC RID: 16844
			private OperationContractGenerationContext context;

			// Token: 0x040041CD RID: 16845
			private DataContractSerializerOperationGenerator dataContractSerializerOperationGenerator;
		}

		// Token: 0x02000BB1 RID: 2993
		private class CodeTypeReferenceComparer : IEqualityComparer<CodeTypeReference>
		{
			// Token: 0x0600742C RID: 29740 RVA: 0x001B1E20 File Offset: 0x001B0020
			public bool Equals(CodeTypeReference x, CodeTypeReference y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null || y == null || x.ArrayRank != y.ArrayRank || x.BaseType != y.BaseType)
				{
					return false;
				}
				CodeTypeReferenceCollection typeArguments = x.TypeArguments;
				CodeTypeReferenceCollection typeArguments2 = y.TypeArguments;
				if (typeArguments2.Count == typeArguments.Count)
				{
					foreach (object obj in typeArguments)
					{
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						foreach (object obj2 in typeArguments2)
						{
							CodeTypeReference codeTypeReference2 = (CodeTypeReference)obj2;
							if (!this.Equals(codeTypeReference, codeTypeReference))
							{
								return false;
							}
						}
					}
					return true;
				}
				return true;
			}

			// Token: 0x0600742D RID: 29741 RVA: 0x001B1F14 File Offset: 0x001B0114
			public int GetHashCode(CodeTypeReference obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}

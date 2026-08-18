using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.Description
{
	// Token: 0x020003FB RID: 1019
	internal class ClientClassGenerator : IServiceContractGenerationExtension
	{
		// Token: 0x06002698 RID: 9880 RVA: 0x0008ACB4 File Offset: 0x00088EB4
		internal ClientClassGenerator(bool tryAddHelperMethod) : this(tryAddHelperMethod, false)
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0008ACBE File Offset: 0x00088EBE
		internal ClientClassGenerator(bool tryAddHelperMethod, bool generateEventAsyncMethods)
		{
			this.tryAddHelperMethod = tryAddHelperMethod;
			this.generateEventAsyncMethods = generateEventAsyncMethods;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0008ACD4 File Offset: 0x00088ED4
		void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
		{
			CodeTypeDeclaration codeTypeDeclaration = context.TypeFactory.CreateClassType();
			codeTypeDeclaration.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetClientClassName(context.ContractType.Name), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			CodeTypeReference contractTypeReference = context.ContractTypeReference;
			if (context.DuplexCallbackType == null)
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(context.ServiceContractGenerator.GetCodeTypeReference(typeof(ClientBase<>)).BaseType, new CodeTypeReference[]
				{
					context.ContractTypeReference
				}));
			}
			else
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(context.ServiceContractGenerator.GetCodeTypeReference(typeof(DuplexClientBase<>)).BaseType, new CodeTypeReference[]
				{
					context.ContractTypeReference
				}));
			}
			codeTypeDeclaration.BaseTypes.Add(context.ContractTypeReference);
			if (ClientClassGenerator.ClientCtorParamNames.Length != ClientClassGenerator.ClientCtorParamTypes.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid client generation constructor table initialization", new object[0])));
			}
			for (int i = 0; i < ClientClassGenerator.ClientCtorParamNames.Length; i++)
			{
				if (ClientClassGenerator.ClientCtorParamNames[i].Length != ClientClassGenerator.ClientCtorParamTypes[i].Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid client generation constructor table initialization", new object[0])));
				}
				CodeConstructor codeConstructor = new CodeConstructor();
				codeConstructor.Attributes = MemberAttributes.Public;
				if (context.DuplexCallbackType != null)
				{
					codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(InstanceContext), ClientClassGenerator.inputInstanceName));
					codeConstructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(ClientClassGenerator.inputInstanceName));
				}
				for (int j = 0; j < ClientClassGenerator.ClientCtorParamNames[i].Length; j++)
				{
					codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(ClientClassGenerator.ClientCtorParamTypes[i][j], ClientClassGenerator.ClientCtorParamNames[i][j]));
					codeConstructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(ClientClassGenerator.ClientCtorParamNames[i][j]));
				}
				codeTypeDeclaration.Members.Add(codeConstructor);
			}
			foreach (OperationContractGenerationContext operationContractGenerationContext in context.Operations)
			{
				if (!operationContractGenerationContext.Operation.IsServerInitiated())
				{
					CodeTypeReference declaringTypeReference = operationContractGenerationContext.DeclaringTypeReference;
					ClientClassGenerator.GenerateClientClassMethod(codeTypeDeclaration, contractTypeReference, operationContractGenerationContext.SyncMethod, this.tryAddHelperMethod, declaringTypeReference);
					if (operationContractGenerationContext.IsAsync)
					{
						CodeMemberMethod beginMethod = ClientClassGenerator.GenerateClientClassMethod(codeTypeDeclaration, contractTypeReference, operationContractGenerationContext.BeginMethod, this.tryAddHelperMethod, declaringTypeReference);
						CodeMemberMethod endMethod = ClientClassGenerator.GenerateClientClassMethod(codeTypeDeclaration, contractTypeReference, operationContractGenerationContext.EndMethod, this.tryAddHelperMethod, declaringTypeReference);
						if (this.generateEventAsyncMethods)
						{
							ClientClassGenerator.GenerateEventAsyncMethods(context, codeTypeDeclaration, operationContractGenerationContext.SyncMethod.Name, beginMethod, endMethod);
						}
					}
					if (operationContractGenerationContext.IsTask)
					{
						ClientClassGenerator.GenerateClientClassMethod(codeTypeDeclaration, contractTypeReference, operationContractGenerationContext.TaskMethod, !operationContractGenerationContext.Operation.HasOutputParameters && this.tryAddHelperMethod, declaringTypeReference);
					}
				}
			}
			context.Namespace.Types.Add(codeTypeDeclaration);
			context.ClientType = codeTypeDeclaration;
			context.ClientTypeReference = ServiceContractGenerator.NamespaceHelper.GetCodeTypeReference(context.Namespace, codeTypeDeclaration);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0008B018 File Offset: 0x00089218
		private static CodeMemberMethod GenerateClientClassMethod(CodeTypeDeclaration clientType, CodeTypeReference contractTypeRef, CodeMemberMethod method, bool addHelperMethod, CodeTypeReference declaringContractTypeRef)
		{
			CodeMemberMethod implementationOfMethod = ClientClassGenerator.GetImplementationOfMethod(contractTypeRef, method);
			ClientClassGenerator.AddMethodImpl(implementationOfMethod);
			int index = clientType.Members.Add(implementationOfMethod);
			CodeMemberMethod codeMemberMethod = null;
			if (addHelperMethod)
			{
				codeMemberMethod = ClientClassGenerator.GenerateHelperMethod(declaringContractTypeRef, implementationOfMethod);
				if (codeMemberMethod != null)
				{
					clientType.Members[index].CustomAttributes.Add(ClientClassGenerator.CreateEditorBrowsableAttribute(EditorBrowsableState.Advanced));
					clientType.Members.Add(codeMemberMethod);
				}
			}
			if (codeMemberMethod == null)
			{
				return implementationOfMethod;
			}
			return codeMemberMethod;
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0008B084 File Offset: 0x00089284
		internal static CodeAttributeDeclaration CreateEditorBrowsableAttribute(EditorBrowsableState editorBrowsableState)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(EditorBrowsableAttribute)));
			CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(typeof(EditorBrowsableState));
			CodeAttributeArgument value = new CodeAttributeArgument(new CodeFieldReferenceExpression(targetObject, editorBrowsableState.ToString()));
			codeAttributeDeclaration.Arguments.Add(value);
			return codeAttributeDeclaration;
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0008B0E0 File Offset: 0x000892E0
		private static CodeMemberMethod GenerateHelperMethod(CodeTypeReference ifaceType, CodeMemberMethod method)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = method.Name;
			codeMemberMethod.Attributes = (MemberAttributes)24578;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeCastExpression(ifaceType, new CodeThisReferenceExpression()), method.Name), new CodeExpression[0]);
			bool flag = false;
			foreach (object obj in method.Parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				CodeTypeDeclaration codeType = ServiceContractGenerator.NamespaceHelper.GetCodeType(codeParameterDeclarationExpression.Type);
				if (codeType != null)
				{
					flag = true;
					CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("inValue");
					codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(codeParameterDeclarationExpression.Type, codeVariableReferenceExpression.VariableName, new CodeObjectCreateExpression(codeParameterDeclarationExpression.Type, new CodeExpression[0])));
					codeMethodInvokeExpression.Parameters.Add(codeVariableReferenceExpression);
					ClientClassGenerator.GenerateParameters(codeMemberMethod, codeType, codeVariableReferenceExpression, FieldDirection.In);
				}
				else
				{
					codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(codeParameterDeclarationExpression.Type, codeParameterDeclarationExpression.Name));
					codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name));
				}
			}
			if (method.ReturnType.BaseType == ClientClassGenerator.voidTypeRef.BaseType)
			{
				codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			}
			else
			{
				CodeTypeDeclaration codeType2 = ServiceContractGenerator.NamespaceHelper.GetCodeType(method.ReturnType);
				if (codeType2 != null)
				{
					flag = true;
					CodeVariableReferenceExpression codeVariableReferenceExpression2 = new CodeVariableReferenceExpression("retVal");
					codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(method.ReturnType, codeVariableReferenceExpression2.VariableName, codeMethodInvokeExpression));
					CodeMethodReturnStatement codeMethodReturnStatement = ClientClassGenerator.GenerateParameters(codeMemberMethod, codeType2, codeVariableReferenceExpression2, FieldDirection.Out);
					if (codeMethodReturnStatement != null)
					{
						codeMemberMethod.Statements.Add(codeMethodReturnStatement);
					}
				}
				else
				{
					codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
					codeMemberMethod.ReturnType = method.ReturnType;
				}
			}
			if (flag)
			{
				method.PrivateImplementationType = ifaceType;
			}
			if (!flag)
			{
				return null;
			}
			return codeMemberMethod;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0008B2DC File Offset: 0x000894DC
		private static CodeMethodReturnStatement GenerateParameters(CodeMemberMethod helperMethod, CodeTypeDeclaration codeTypeDeclaration, CodeExpression target, FieldDirection dir)
		{
			CodeMethodReturnStatement result = null;
			foreach (object obj in codeTypeDeclaration.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				CodeMemberField codeMemberField = codeTypeMember as CodeMemberField;
				if (codeMemberField != null)
				{
					CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(target, codeMemberField.Name);
					CodeTypeDeclaration codeType = ServiceContractGenerator.NamespaceHelper.GetCodeType(codeMemberField.Type);
					if (codeType != null)
					{
						if (dir == FieldDirection.In)
						{
							helperMethod.Statements.Add(new CodeAssignStatement(codeFieldReferenceExpression, new CodeObjectCreateExpression(codeMemberField.Type, new CodeExpression[0])));
						}
						result = ClientClassGenerator.GenerateParameters(helperMethod, codeType, codeFieldReferenceExpression, dir);
					}
					else
					{
						CodeParameterDeclarationExpression codeParameterDeclarationExpression = ClientClassGenerator.GetRefParameter(helperMethod.Parameters, dir, codeMemberField);
						if (codeParameterDeclarationExpression == null && dir == FieldDirection.Out && helperMethod.ReturnType.BaseType == ClientClassGenerator.voidTypeRef.BaseType)
						{
							helperMethod.ReturnType = codeMemberField.Type;
							result = new CodeMethodReturnStatement(codeFieldReferenceExpression);
						}
						else
						{
							if (codeParameterDeclarationExpression == null)
							{
								codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(codeMemberField.Type, NamingHelper.GetUniqueName(codeMemberField.Name, new NamingHelper.DoesNameExist(ClientClassGenerator.DoesParameterNameExist), helperMethod));
								codeParameterDeclarationExpression.Direction = dir;
								helperMethod.Parameters.Add(codeParameterDeclarationExpression);
							}
							if (dir == FieldDirection.Out)
							{
								helperMethod.Statements.Add(new CodeAssignStatement(new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name), codeFieldReferenceExpression));
							}
							else
							{
								helperMethod.Statements.Add(new CodeAssignStatement(codeFieldReferenceExpression, new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name)));
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x0008B47C File Offset: 0x0008967C
		private static CodeParameterDeclarationExpression GetRefParameter(CodeParameterDeclarationExpressionCollection parameters, FieldDirection dir, CodeMemberField field)
		{
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				if (codeParameterDeclarationExpression.Name == field.Name)
				{
					if (codeParameterDeclarationExpression.Direction != dir && codeParameterDeclarationExpression.Type.BaseType == field.Type.BaseType)
					{
						codeParameterDeclarationExpression.Direction = FieldDirection.Ref;
						return codeParameterDeclarationExpression;
					}
					return null;
				}
			}
			return null;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0008B514 File Offset: 0x00089714
		internal static bool DoesMemberNameExist(string name, object typeDeclarationObject)
		{
			CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)typeDeclarationObject;
			if (string.Compare(codeTypeDeclaration.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			foreach (object obj in codeTypeDeclaration.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (string.Compare(codeTypeMember.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0008B598 File Offset: 0x00089798
		internal static bool DoesTypeNameExists(string name, object codeTypeDeclarationCollectionObject)
		{
			CodeTypeDeclarationCollection codeTypeDeclarationCollection = (CodeTypeDeclarationCollection)codeTypeDeclarationCollectionObject;
			foreach (object obj in codeTypeDeclarationCollection)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				if (string.Compare(codeTypeDeclaration.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0008B608 File Offset: 0x00089808
		internal static bool DoesTypeAndMemberNameExist(string name, object nameCollection)
		{
			object[] array = (object[])nameCollection;
			return ClientClassGenerator.DoesTypeNameExists(name, array[0]) || ClientClassGenerator.DoesMemberNameExist(name, array[1]);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0008B638 File Offset: 0x00089838
		internal static bool DoesMethodNameExist(string name, object operationsObject)
		{
			Collection<OperationContractGenerationContext> collection = (Collection<OperationContractGenerationContext>)operationsObject;
			foreach (OperationContractGenerationContext operationContractGenerationContext in collection)
			{
				if (string.Compare(operationContractGenerationContext.SyncMethod.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
				if (operationContractGenerationContext.IsAsync)
				{
					if (string.Compare(operationContractGenerationContext.BeginMethod.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
					if (string.Compare(operationContractGenerationContext.EndMethod.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				if (operationContractGenerationContext.IsTask && string.Compare(operationContractGenerationContext.TaskMethod.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0008B6F4 File Offset: 0x000898F4
		internal static bool DoesParameterNameExist(string name, object methodObject)
		{
			CodeMemberMethod codeMemberMethod = (CodeMemberMethod)methodObject;
			if (string.Compare(codeMemberMethod.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			CodeParameterDeclarationExpressionCollection parameters = codeMemberMethod.Parameters;
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				if (string.Compare(codeParameterDeclarationExpression.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0008B77C File Offset: 0x0008997C
		private static void AddMethodImpl(CodeMemberMethod method)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(ClientClassGenerator.GetChannelReference(), method.Name, new CodeExpression[0]);
			foreach (object obj in method.Parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				codeMethodInvokeExpression.Parameters.Add(new CodeDirectionExpression(codeParameterDeclarationExpression.Direction, new CodeVariableReferenceExpression(codeParameterDeclarationExpression.Name)));
			}
			if (ClientClassGenerator.IsVoid(method))
			{
				method.Statements.Add(codeMethodInvokeExpression);
				return;
			}
			method.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0008B830 File Offset: 0x00089A30
		private static CodeMemberMethod GetImplementationOfMethod(CodeTypeReference ifaceType, CodeMemberMethod method)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = method.Name;
			codeMemberMethod.ImplementationTypes.Add(ifaceType);
			codeMemberMethod.Attributes = (MemberAttributes)24578;
			foreach (object obj in method.Parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = new CodeParameterDeclarationExpression(codeParameterDeclarationExpression.Type, codeParameterDeclarationExpression.Name);
				codeParameterDeclarationExpression2.Direction = codeParameterDeclarationExpression.Direction;
				codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression2);
			}
			codeMemberMethod.ReturnType = method.ReturnType;
			return codeMemberMethod;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0008B8E8 File Offset: 0x00089AE8
		private static void GenerateEventAsyncMethods(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName, CodeMemberMethod beginMethod, CodeMemberMethod endMethod)
		{
			CodeTypeDeclaration operationCompletedEventArgsType = ClientClassGenerator.CreateOperationCompletedEventArgsType(context, syncMethodName, endMethod);
			CodeMemberEvent operationCompletedEvent = ClientClassGenerator.CreateOperationCompletedEvent(context, clientType, syncMethodName, operationCompletedEventArgsType);
			CodeMemberField beginOperationDelegate = ClientClassGenerator.CreateBeginOperationDelegate(context, clientType, syncMethodName);
			CodeMemberMethod beginOperationMethod = ClientClassGenerator.CreateBeginOperationMethod(context, clientType, syncMethodName, beginMethod);
			CodeMemberField endOperationDelegate = ClientClassGenerator.CreateEndOperationDelegate(context, clientType, syncMethodName);
			CodeMemberMethod endOperationMethod = ClientClassGenerator.CreateEndOperationMethod(context, clientType, syncMethodName, endMethod);
			CodeMemberField operationCompletedDelegate = ClientClassGenerator.CreateOperationCompletedDelegate(context, clientType, syncMethodName);
			CodeMemberMethod operationCompletedMethod = ClientClassGenerator.CreateOperationCompletedMethod(context, clientType, syncMethodName, operationCompletedEventArgsType, operationCompletedEvent);
			CodeMemberMethod eventAsyncMethod = ClientClassGenerator.CreateEventAsyncMethod(context, clientType, syncMethodName, beginMethod, beginOperationDelegate, beginOperationMethod, endOperationDelegate, endOperationMethod, operationCompletedDelegate, operationCompletedMethod);
			ClientClassGenerator.CreateEventAsyncMethodOverload(clientType, eventAsyncMethod);
			beginMethod.CustomAttributes.Add(ClientClassGenerator.CreateEditorBrowsableAttribute(EditorBrowsableState.Advanced));
			endMethod.CustomAttributes.Add(ClientClassGenerator.CreateEditorBrowsableAttribute(EditorBrowsableState.Advanced));
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0008B98C File Offset: 0x00089B8C
		private static CodeTypeDeclaration CreateOperationCompletedEventArgsType(ServiceContractGenerationContext context, string syncMethodName, CodeMemberMethod endMethod)
		{
			if (endMethod.Parameters.Count == 1 && endMethod.ReturnType.BaseType == ClientClassGenerator.voidTypeRef.BaseType)
			{
				return null;
			}
			CodeTypeDeclaration codeTypeDeclaration = context.TypeFactory.CreateClassType();
			codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(ClientClassGenerator.asyncCompletedEventArgsType));
			CodeMemberField codeMemberField = new CodeMemberField();
			codeMemberField.Type = new CodeTypeReference(ClientClassGenerator.objectArrayType);
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression();
			codeFieldReferenceExpression.TargetObject = new CodeThisReferenceExpression();
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			for (int i = 0; i < ClientClassGenerator.EventArgsCtorParamTypes.Length; i++)
			{
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(ClientClassGenerator.EventArgsCtorParamTypes[i], ClientClassGenerator.EventArgsCtorParamNames[i]));
				if (i > 0)
				{
					codeConstructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(ClientClassGenerator.EventArgsCtorParamNames[i]));
				}
			}
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor.Statements.Add(new CodeAssignStatement(codeFieldReferenceExpression, new CodeVariableReferenceExpression(ClientClassGenerator.EventArgsCtorParamNames[0])));
			int asyncResultParamIndex = ClientClassGenerator.GetAsyncResultParamIndex(endMethod);
			int num = 0;
			for (int j = 0; j < endMethod.Parameters.Count; j++)
			{
				if (j != asyncResultParamIndex)
				{
					ClientClassGenerator.CreateEventAsyncCompletedArgsTypeProperty(codeTypeDeclaration, endMethod.Parameters[j].Type, endMethod.Parameters[j].Name, new CodeArrayIndexerExpression(codeFieldReferenceExpression, new CodeExpression[]
					{
						new CodePrimitiveExpression(num++)
					}));
				}
			}
			if (endMethod.ReturnType.BaseType != ClientClassGenerator.voidTypeRef.BaseType)
			{
				ClientClassGenerator.CreateEventAsyncCompletedArgsTypeProperty(codeTypeDeclaration, endMethod.ReturnType, NamingHelper.GetUniqueName("Result", new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMemberNameExist), codeTypeDeclaration), new CodeArrayIndexerExpression(codeFieldReferenceExpression, new CodeExpression[]
				{
					new CodePrimitiveExpression(num)
				}));
			}
			codeMemberField.Name = NamingHelper.GetUniqueName("results", new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMemberNameExist), codeTypeDeclaration);
			codeFieldReferenceExpression.FieldName = codeMemberField.Name;
			codeTypeDeclaration.Members.Add(codeMemberField);
			codeTypeDeclaration.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetOperationCompletedEventArgsTypeName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesTypeAndMemberNameExist), new object[]
			{
				context.Namespace.Types,
				codeTypeDeclaration
			});
			context.Namespace.Types.Add(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x0008BBF0 File Offset: 0x00089DF0
		private static int GetAsyncResultParamIndex(CodeMemberMethod endMethod)
		{
			int num = endMethod.Parameters.Count - 1;
			if (endMethod.Parameters[num].Type.BaseType != ClientClassGenerator.asyncResultTypeRef.BaseType)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0008BC38 File Offset: 0x00089E38
		private static CodeMemberProperty CreateEventAsyncCompletedArgsTypeProperty(CodeTypeDeclaration ownerTypeDecl, CodeTypeReference propertyType, string propertyName, CodeExpression propertyValueExpr)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes = (MemberAttributes)24578;
			codeMemberProperty.Type = propertyType;
			codeMemberProperty.Name = propertyName;
			codeMemberProperty.HasSet = false;
			codeMemberProperty.HasGet = true;
			CodeCastExpression expression = new CodeCastExpression(propertyType, propertyValueExpr);
			CodeMethodReturnStatement value = new CodeMethodReturnStatement(expression);
			codeMemberProperty.GetStatements.Add(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), ClientClassGenerator.raiseExceptionIfNecessaryMethodName, new CodeExpression[0]));
			codeMemberProperty.GetStatements.Add(value);
			ownerTypeDecl.Members.Add(codeMemberProperty);
			return codeMemberProperty;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0008BCC0 File Offset: 0x00089EC0
		private static CodeMemberEvent CreateOperationCompletedEvent(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName, CodeTypeDeclaration operationCompletedEventArgsType)
		{
			CodeMemberEvent codeMemberEvent = new CodeMemberEvent();
			codeMemberEvent.Attributes = MemberAttributes.Public;
			codeMemberEvent.Type = new CodeTypeReference(ClientClassGenerator.eventHandlerType);
			if (operationCompletedEventArgsType == null)
			{
				codeMemberEvent.Type.TypeArguments.Add(ClientClassGenerator.asyncCompletedEventArgsType);
			}
			else
			{
				codeMemberEvent.Type.TypeArguments.Add(operationCompletedEventArgsType.Name);
			}
			codeMemberEvent.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetOperationCompletedEventName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			clientType.Members.Add(codeMemberEvent);
			return codeMemberEvent;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0008BD50 File Offset: 0x00089F50
		private static CodeMemberField CreateBeginOperationDelegate(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName)
		{
			CodeMemberField codeMemberField = new CodeMemberField();
			codeMemberField.Attributes = MemberAttributes.Private;
			codeMemberField.Type = new CodeTypeReference(ClientClassGenerator.beginOperationDelegateTypeName);
			codeMemberField.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetBeginOperationDelegateName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			clientType.Members.Add(codeMemberField);
			return codeMemberField;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0008BDB0 File Offset: 0x00089FB0
		private static CodeMemberMethod CreateBeginOperationMethod(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName, CodeMemberMethod beginMethod)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = MemberAttributes.Private;
			codeMemberMethod.ReturnType = new CodeTypeReference(ClientClassGenerator.asyncResultType);
			codeMemberMethod.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetBeginOperationMethodName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(ClientClassGenerator.objectArrayType);
			codeParameterDeclarationExpression.Name = NamingHelper.GetUniqueName("inValues", new NamingHelper.DoesNameExist(ClientClassGenerator.DoesParameterNameExist), beginMethod);
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), beginMethod.Name, new CodeExpression[0]);
			CodeExpression targetObject = new CodeVariableReferenceExpression(codeParameterDeclarationExpression.Name);
			for (int i = 0; i < beginMethod.Parameters.Count - 2; i++)
			{
				CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
				codeVariableDeclarationStatement.Type = beginMethod.Parameters[i].Type;
				codeVariableDeclarationStatement.Name = beginMethod.Parameters[i].Name;
				codeVariableDeclarationStatement.InitExpression = new CodeCastExpression(codeVariableDeclarationStatement.Type, new CodeArrayIndexerExpression(targetObject, new CodeExpression[]
				{
					new CodePrimitiveExpression(i)
				}));
				codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
				codeMethodInvokeExpression.Parameters.Add(new CodeDirectionExpression(beginMethod.Parameters[i].Direction, new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name)));
			}
			for (int j = beginMethod.Parameters.Count - 2; j < beginMethod.Parameters.Count; j++)
			{
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(beginMethod.Parameters[j].Type, beginMethod.Parameters[j].Name));
				codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression(beginMethod.Parameters[j].Name));
			}
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
			clientType.Members.Add(codeMemberMethod);
			return codeMemberMethod;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0008BFC8 File Offset: 0x0008A1C8
		private static CodeMemberField CreateEndOperationDelegate(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName)
		{
			CodeMemberField codeMemberField = new CodeMemberField();
			codeMemberField.Attributes = MemberAttributes.Private;
			codeMemberField.Type = new CodeTypeReference(ClientClassGenerator.endOperationDelegateTypeName);
			codeMemberField.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetEndOperationDelegateName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			clientType.Members.Add(codeMemberField);
			return codeMemberField;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0008C028 File Offset: 0x0008A228
		private static CodeMemberMethod CreateEndOperationMethod(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName, CodeMemberMethod endMethod)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = MemberAttributes.Private;
			codeMemberMethod.ReturnType = new CodeTypeReference(ClientClassGenerator.objectArrayType);
			codeMemberMethod.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetEndOperationMethodName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			int asyncResultParamIndex = ClientClassGenerator.GetAsyncResultParamIndex(endMethod);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), endMethod.Name, new CodeExpression[0]);
			CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
			codeArrayCreateExpression.CreateType = new CodeTypeReference(ClientClassGenerator.objectArrayType);
			for (int i = 0; i < endMethod.Parameters.Count; i++)
			{
				if (i == asyncResultParamIndex)
				{
					codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(endMethod.Parameters[i].Type, endMethod.Parameters[i].Name));
					codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression(endMethod.Parameters[i].Name));
				}
				else
				{
					CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(endMethod.Parameters[i].Type, endMethod.Parameters[i].Name);
					CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), ClientClassGenerator.getDefaultValueForInitializationMethodName, new CodeTypeReference[]
					{
						endMethod.Parameters[i].Type
					});
					codeVariableDeclarationStatement.InitExpression = new CodeMethodInvokeExpression(method, new CodeExpression[0]);
					codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
					codeMethodInvokeExpression.Parameters.Add(new CodeDirectionExpression(endMethod.Parameters[i].Direction, new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name)));
					codeArrayCreateExpression.Initializers.Add(new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name));
				}
			}
			if (endMethod.ReturnType.BaseType != ClientClassGenerator.voidTypeRef.BaseType)
			{
				CodeVariableDeclarationStatement codeVariableDeclarationStatement2 = new CodeVariableDeclarationStatement();
				codeVariableDeclarationStatement2.Type = endMethod.ReturnType;
				codeVariableDeclarationStatement2.Name = NamingHelper.GetUniqueName("retVal", new NamingHelper.DoesNameExist(ClientClassGenerator.DoesParameterNameExist), endMethod);
				codeVariableDeclarationStatement2.InitExpression = codeMethodInvokeExpression;
				codeArrayCreateExpression.Initializers.Add(new CodeVariableReferenceExpression(codeVariableDeclarationStatement2.Name));
				codeMemberMethod.Statements.Add(codeVariableDeclarationStatement2);
			}
			else
			{
				codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			}
			if (codeArrayCreateExpression.Initializers.Count > 0)
			{
				codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeArrayCreateExpression));
			}
			else
			{
				codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(null)));
			}
			clientType.Members.Add(codeMemberMethod);
			return codeMemberMethod;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0008C2C4 File Offset: 0x0008A4C4
		private static CodeMemberField CreateOperationCompletedDelegate(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName)
		{
			CodeMemberField codeMemberField = new CodeMemberField();
			codeMemberField.Attributes = MemberAttributes.Private;
			codeMemberField.Type = new CodeTypeReference(ClientClassGenerator.sendOrPostCallbackType);
			codeMemberField.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetOperationCompletedDelegateName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			clientType.Members.Add(codeMemberField);
			return codeMemberField;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0008C324 File Offset: 0x0008A524
		private static CodeMemberMethod CreateOperationCompletedMethod(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName, CodeTypeDeclaration operationCompletedEventArgsType, CodeMemberEvent operationCompletedEvent)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = MemberAttributes.Private;
			codeMemberMethod.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetOperationCompletedMethodName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(ClientClassGenerator.objectType), "state"));
			codeMemberMethod.ReturnType = new CodeTypeReference(ClientClassGenerator.voidType);
			CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(ClientClassGenerator.invokeAsyncCompletedEventArgsTypeName, "e");
			codeVariableDeclarationStatement.InitExpression = new CodeCastExpression(ClientClassGenerator.invokeAsyncCompletedEventArgsTypeName, new CodeArgumentReferenceExpression(codeMemberMethod.Parameters[0].Name));
			CodeVariableReferenceExpression targetObject = new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name);
			CodeObjectCreateExpression codeObjectCreateExpression;
			if (operationCompletedEventArgsType != null)
			{
				codeObjectCreateExpression = new CodeObjectCreateExpression(operationCompletedEventArgsType.Name, new CodeExpression[]
				{
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[0]),
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[1]),
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[2]),
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[3])
				});
			}
			else
			{
				codeObjectCreateExpression = new CodeObjectCreateExpression(ClientClassGenerator.asyncCompletedEventArgsType, new CodeExpression[]
				{
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[1]),
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[2]),
					new CodePropertyReferenceExpression(targetObject, ClientClassGenerator.EventArgsPropertyNames[3])
				});
			}
			CodeEventReferenceExpression codeEventReferenceExpression = new CodeEventReferenceExpression(new CodeThisReferenceExpression(), operationCompletedEvent.Name);
			CodeDelegateInvokeExpression expression = new CodeDelegateInvokeExpression(codeEventReferenceExpression, new CodeExpression[]
			{
				new CodeThisReferenceExpression(),
				codeObjectCreateExpression
			});
			CodeConditionStatement value = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeEventReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeStatement[]
			{
				codeVariableDeclarationStatement,
				new CodeExpressionStatement(expression)
			});
			codeMemberMethod.Statements.Add(value);
			clientType.Members.Add(codeMemberMethod);
			return codeMemberMethod;
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0008C4E4 File Offset: 0x0008A6E4
		private static CodeMemberMethod CreateEventAsyncMethod(ServiceContractGenerationContext context, CodeTypeDeclaration clientType, string syncMethodName, CodeMemberMethod beginMethod, CodeMemberField beginOperationDelegate, CodeMemberMethod beginOperationMethod, CodeMemberField endOperationDelegate, CodeMemberMethod endOperationMethod, CodeMemberField operationCompletedDelegate, CodeMemberMethod operationCompletedMethod)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = NamingHelper.GetUniqueName(ClientClassGenerator.GetEventAsyncMethodName(syncMethodName), new NamingHelper.DoesNameExist(ClientClassGenerator.DoesMethodNameExist), context.Operations);
			codeMemberMethod.Attributes = (MemberAttributes)24578;
			codeMemberMethod.ReturnType = new CodeTypeReference(ClientClassGenerator.voidType);
			CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression(new CodeTypeReference(ClientClassGenerator.objectArrayType), new CodeExpression[0]);
			for (int i = 0; i < beginMethod.Parameters.Count - 2; i++)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = beginMethod.Parameters[i];
				CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = new CodeParameterDeclarationExpression(codeParameterDeclarationExpression.Type, codeParameterDeclarationExpression.Name);
				codeParameterDeclarationExpression2.Direction = FieldDirection.In;
				codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression2);
				codeArrayCreateExpression.Initializers.Add(new CodeVariableReferenceExpression(codeParameterDeclarationExpression2.Name));
			}
			string uniqueName = NamingHelper.GetUniqueName("userState", new NamingHelper.DoesNameExist(ClientClassGenerator.DoesParameterNameExist), codeMemberMethod);
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(ClientClassGenerator.objectType), uniqueName));
			codeMemberMethod.Statements.Add(ClientClassGenerator.CreateDelegateIfNotNull(beginOperationDelegate, beginOperationMethod));
			codeMemberMethod.Statements.Add(ClientClassGenerator.CreateDelegateIfNotNull(endOperationDelegate, endOperationMethod));
			codeMemberMethod.Statements.Add(ClientClassGenerator.CreateDelegateIfNotNull(operationCompletedDelegate, operationCompletedMethod));
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), ClientClassGenerator.invokeAsyncMethodName, new CodeExpression[0]);
			codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), beginOperationDelegate.Name));
			if (codeArrayCreateExpression.Initializers.Count > 0)
			{
				codeMethodInvokeExpression.Parameters.Add(codeArrayCreateExpression);
			}
			else
			{
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(null));
			}
			codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), endOperationDelegate.Name));
			codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), operationCompletedDelegate.Name));
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression(uniqueName));
			codeMemberMethod.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
			clientType.Members.Add(codeMemberMethod);
			return codeMemberMethod;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0008C6FC File Offset: 0x0008A8FC
		private static CodeMemberMethod CreateEventAsyncMethodOverload(CodeTypeDeclaration clientType, CodeMemberMethod eventAsyncMethod)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = eventAsyncMethod.Attributes;
			codeMemberMethod.Name = eventAsyncMethod.Name;
			codeMemberMethod.ReturnType = eventAsyncMethod.ReturnType;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), eventAsyncMethod.Name, new CodeExpression[0]);
			for (int i = 0; i < eventAsyncMethod.Parameters.Count - 1; i++)
			{
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(eventAsyncMethod.Parameters[i].Type, eventAsyncMethod.Parameters[i].Name));
				codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression(eventAsyncMethod.Parameters[i].Name));
			}
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(null));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			int index = clientType.Members.IndexOf(eventAsyncMethod);
			clientType.Members.Insert(index, codeMemberMethod);
			return codeMemberMethod;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x0008C7F4 File Offset: 0x0008A9F4
		private static CodeStatement CreateDelegateIfNotNull(CodeMemberField delegateField, CodeMemberMethod delegateMethod)
		{
			return new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), delegateField.Name), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)), new CodeStatement[]
			{
				new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), delegateField.Name), new CodeDelegateCreateExpression(delegateField.Type, new CodeThisReferenceExpression(), delegateMethod.Name))
			});
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0008C856 File Offset: 0x0008AA56
		private static string GetClassName(string interfaceName)
		{
			if (interfaceName.Length >= 2 && string.Compare(interfaceName, 0, "I", 0, "I".Length, StringComparison.Ordinal) == 0 && char.IsUpper(interfaceName, 1))
			{
				return interfaceName.Substring(1);
			}
			return interfaceName;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0008C88D File Offset: 0x0008AA8D
		private static string GetEventAsyncMethodName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}Async", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0008C8A8 File Offset: 0x0008AAA8
		private static string GetBeginOperationDelegateName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "onBegin{0}Delegate", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0008C8C3 File Offset: 0x0008AAC3
		private static string GetBeginOperationMethodName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "OnBegin{0}", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0008C8DE File Offset: 0x0008AADE
		private static string GetEndOperationDelegateName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "onEnd{0}Delegate", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0008C8F9 File Offset: 0x0008AAF9
		private static string GetEndOperationMethodName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "OnEnd{0}", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0008C914 File Offset: 0x0008AB14
		private static string GetOperationCompletedDelegateName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "on{0}CompletedDelegate", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0008C92F File Offset: 0x0008AB2F
		private static string GetOperationCompletedMethodName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "On{0}Completed", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0008C94A File Offset: 0x0008AB4A
		private static string GetOperationCompletedEventName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}Completed", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x0008C965 File Offset: 0x0008AB65
		private static string GetOperationCompletedEventArgsTypeName(string syncMethodName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}CompletedEventArgs", new object[]
			{
				syncMethodName
			});
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x0008C980 File Offset: 0x0008AB80
		internal static string GetClientClassName(string interfaceName)
		{
			return ClientClassGenerator.GetClassName(interfaceName) + "Client";
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x0008C992 File Offset: 0x0008AB92
		private static bool IsVoid(CodeMemberMethod method)
		{
			return method.ReturnType == null || string.Compare(method.ReturnType.BaseType, typeof(void).FullName, StringComparison.Ordinal) == 0;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x0008C9C1 File Offset: 0x0008ABC1
		private static CodeExpression GetChannelReference()
		{
			return new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), "Channel");
		}

		// Token: 0x040021A7 RID: 8615
		private bool tryAddHelperMethod;

		// Token: 0x040021A8 RID: 8616
		private bool generateEventAsyncMethods;

		// Token: 0x040021A9 RID: 8617
		private static Type clientBaseType = typeof(ClientBase<>);

		// Token: 0x040021AA RID: 8618
		private static Type duplexClientBaseType = typeof(DuplexClientBase<>);

		// Token: 0x040021AB RID: 8619
		private static Type instanceContextType = typeof(InstanceContext);

		// Token: 0x040021AC RID: 8620
		private static Type objectType = typeof(object);

		// Token: 0x040021AD RID: 8621
		private static Type objectArrayType = typeof(object[]);

		// Token: 0x040021AE RID: 8622
		private static Type exceptionType = typeof(Exception);

		// Token: 0x040021AF RID: 8623
		private static Type boolType = typeof(bool);

		// Token: 0x040021B0 RID: 8624
		private static Type stringType = typeof(string);

		// Token: 0x040021B1 RID: 8625
		private static Type endpointAddressType = typeof(EndpointAddress);

		// Token: 0x040021B2 RID: 8626
		private static Type uriType = typeof(Uri);

		// Token: 0x040021B3 RID: 8627
		private static Type bindingType = typeof(Binding);

		// Token: 0x040021B4 RID: 8628
		private static Type sendOrPostCallbackType = typeof(SendOrPostCallback);

		// Token: 0x040021B5 RID: 8629
		private static Type asyncCompletedEventArgsType = typeof(AsyncCompletedEventArgs);

		// Token: 0x040021B6 RID: 8630
		private static Type eventHandlerType = typeof(EventHandler<>);

		// Token: 0x040021B7 RID: 8631
		private static Type voidType = typeof(void);

		// Token: 0x040021B8 RID: 8632
		private static Type asyncResultType = typeof(IAsyncResult);

		// Token: 0x040021B9 RID: 8633
		private static Type asyncCallbackType = typeof(AsyncCallback);

		// Token: 0x040021BA RID: 8634
		private static CodeTypeReference voidTypeRef = new CodeTypeReference(typeof(void));

		// Token: 0x040021BB RID: 8635
		private static CodeTypeReference asyncResultTypeRef = new CodeTypeReference(typeof(IAsyncResult));

		// Token: 0x040021BC RID: 8636
		private static string inputInstanceName = "callbackInstance";

		// Token: 0x040021BD RID: 8637
		private static string invokeAsyncCompletedEventArgsTypeName = "InvokeAsyncCompletedEventArgs";

		// Token: 0x040021BE RID: 8638
		private static string invokeAsyncMethodName = "InvokeAsync";

		// Token: 0x040021BF RID: 8639
		private static string raiseExceptionIfNecessaryMethodName = "RaiseExceptionIfNecessary";

		// Token: 0x040021C0 RID: 8640
		private static string beginOperationDelegateTypeName = "BeginOperationDelegate";

		// Token: 0x040021C1 RID: 8641
		private static string endOperationDelegateTypeName = "EndOperationDelegate";

		// Token: 0x040021C2 RID: 8642
		private static string getDefaultValueForInitializationMethodName = "GetDefaultValueForInitialization";

		// Token: 0x040021C3 RID: 8643
		private static Type[][] ClientCtorParamTypes = new Type[][]
		{
			new Type[0],
			new Type[]
			{
				ClientClassGenerator.stringType
			},
			new Type[]
			{
				ClientClassGenerator.stringType,
				ClientClassGenerator.stringType
			},
			new Type[]
			{
				ClientClassGenerator.stringType,
				ClientClassGenerator.endpointAddressType
			},
			new Type[]
			{
				ClientClassGenerator.bindingType,
				ClientClassGenerator.endpointAddressType
			}
		};

		// Token: 0x040021C4 RID: 8644
		private static string[][] ClientCtorParamNames = new string[][]
		{
			new string[0],
			new string[]
			{
				"endpointConfigurationName"
			},
			new string[]
			{
				"endpointConfigurationName",
				"remoteAddress"
			},
			new string[]
			{
				"endpointConfigurationName",
				"remoteAddress"
			},
			new string[]
			{
				"binding",
				"remoteAddress"
			}
		};

		// Token: 0x040021C5 RID: 8645
		private static Type[] EventArgsCtorParamTypes = new Type[]
		{
			ClientClassGenerator.objectArrayType,
			ClientClassGenerator.exceptionType,
			ClientClassGenerator.boolType,
			ClientClassGenerator.objectType
		};

		// Token: 0x040021C6 RID: 8646
		private static string[] EventArgsCtorParamNames = new string[]
		{
			"results",
			"exception",
			"cancelled",
			"userState"
		};

		// Token: 0x040021C7 RID: 8647
		private static string[] EventArgsPropertyNames = new string[]
		{
			"Results",
			"Error",
			"Cancelled",
			"UserState"
		};

		// Token: 0x02000BAE RID: 2990
		private static class Strings
		{
			// Token: 0x040041C5 RID: 16837
			public const string ClientBaseChannelProperty = "Channel";

			// Token: 0x040041C6 RID: 16838
			public const string ClientTypeSuffix = "Client";

			// Token: 0x040041C7 RID: 16839
			public const string InterfaceTypePrefix = "I";
		}
	}
}

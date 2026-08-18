using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200000B RID: 11
	public static class BindingTransform
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00002F21 File Offset: 0x00001121
		public static AstNode FromBinding(AstNode node)
		{
			return BindingTransform.ConvertFromBinding(node);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002F29 File Offset: 0x00001129
		public static AstNode ToBinding(AstNode node)
		{
			return BindingTransform.ConvertToBinding(node);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002F34 File Offset: 0x00001134
		public static AstNodeList ToParameters(AstNode node)
		{
			AstNodeList astNodeList = null;
			if (node != null)
			{
				astNodeList = new AstNodeList(node.Context);
				GroupingOperator groupingOperator = node as GroupingOperator;
				BindingTransform.RecurseParameters(astNodeList, (groupingOperator != null) ? groupingOperator.Operand : node);
			}
			return astNodeList;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002F6C File Offset: 0x0000116C
		private static AstNode ConvertFromBinding(AstNode node)
		{
			BindingIdentifier bindingIdentifier = node as BindingIdentifier;
			if (bindingIdentifier != null)
			{
				return BindingTransform.ConvertFromBindingIdentifier(bindingIdentifier);
			}
			ArrayLiteral bindingLiteral;
			if ((bindingLiteral = (node as ArrayLiteral)) != null)
			{
				return BindingTransform.ConvertFromBindingArrayLiteral(bindingLiteral);
			}
			ObjectLiteral bindingLiteral2;
			if ((bindingLiteral2 = (node as ObjectLiteral)) != null)
			{
				return BindingTransform.ConvertFromBindingObjectLiteral(bindingLiteral2);
			}
			ObjectLiteralProperty bindingLiteral3;
			if ((bindingLiteral3 = (node as ObjectLiteralProperty)) != null)
			{
				return BindingTransform.ConvertFromBindingObjectProperty(bindingLiteral3);
			}
			node.Context.HandleError(JSError.UnableToConvertFromBinding, true);
			return null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002FEC File Offset: 0x000011EC
		private static Lookup ConvertFromBindingIdentifier(BindingIdentifier bindingIdentifier)
		{
			Lookup lookup = null;
			if (bindingIdentifier != null)
			{
				lookup = new Lookup(bindingIdentifier.Context)
				{
					Name = bindingIdentifier.Name,
					VariableField = bindingIdentifier.VariableField
				};
				bindingIdentifier.VariableField.IfNotNull(delegate(JSVariableField v)
				{
					v.References.Add(lookup);
				});
			}
			return lookup;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003058 File Offset: 0x00001258
		private static ArrayLiteral ConvertFromBindingArrayLiteral(ArrayLiteral bindingLiteral)
		{
			ArrayLiteral arrayLiteral = null;
			if (bindingLiteral != null)
			{
				arrayLiteral = new ArrayLiteral(bindingLiteral.Context)
				{
					TerminatingContext = bindingLiteral.TerminatingContext
				};
				if (bindingLiteral.Elements != null)
				{
					arrayLiteral.Elements = new AstNodeList(bindingLiteral.Elements.Context);
					foreach (AstNode node in bindingLiteral.Elements)
					{
						arrayLiteral.Elements.Append(BindingTransform.ConvertFromBinding(node));
					}
				}
			}
			return arrayLiteral;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000030F0 File Offset: 0x000012F0
		private static ObjectLiteral ConvertFromBindingObjectLiteral(ObjectLiteral bindingLiteral)
		{
			ObjectLiteral objectLiteral = null;
			if (bindingLiteral != null)
			{
				objectLiteral = new ObjectLiteral(bindingLiteral.Context)
				{
					TerminatingContext = bindingLiteral.TerminatingContext
				};
				if (bindingLiteral.Properties != null)
				{
					objectLiteral.Properties = new AstNodeList(bindingLiteral.Properties.Context);
					foreach (AstNode node in bindingLiteral.Properties)
					{
						objectLiteral.Properties.Append(BindingTransform.ConvertFromBinding(node));
					}
				}
			}
			return objectLiteral;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003188 File Offset: 0x00001388
		private static ObjectLiteralProperty ConvertFromBindingObjectProperty(ObjectLiteralProperty bindingLiteral)
		{
			ObjectLiteralProperty result = null;
			if (bindingLiteral != null)
			{
				result = new ObjectLiteralProperty(bindingLiteral.Context)
				{
					Name = BindingTransform.ConvertFromBindingObjectName(bindingLiteral.Name),
					Value = BindingTransform.ConvertFromBinding(bindingLiteral.Value),
					TerminatingContext = bindingLiteral.TerminatingContext
				};
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000031D8 File Offset: 0x000013D8
		private static ObjectLiteralField ConvertFromBindingObjectName(ObjectLiteralField bindingLiteral)
		{
			ObjectLiteralField result = null;
			if (bindingLiteral != null)
			{
				result = new ObjectLiteralField(bindingLiteral.Name, bindingLiteral.PrimitiveType, bindingLiteral.Context)
				{
					ColonContext = bindingLiteral.ColonContext,
					IsIdentifier = bindingLiteral.IsIdentifier,
					MayHaveIssues = bindingLiteral.MayHaveIssues,
					TerminatingContext = bindingLiteral.TerminatingContext
				};
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003238 File Offset: 0x00001438
		private static AstNode ConvertToBinding(AstNode node)
		{
			Lookup lookup = node as Lookup;
			if (lookup != null)
			{
				return BindingTransform.ConvertToBindingIdentifier(lookup);
			}
			ArrayLiteral arrayLiteral;
			if ((arrayLiteral = (node as ArrayLiteral)) != null)
			{
				return BindingTransform.ConvertToBindingArrayLiteral(arrayLiteral);
			}
			ObjectLiteral objectLiteral;
			if ((objectLiteral = (node as ObjectLiteral)) != null)
			{
				return BindingTransform.ConvertToBindingObjectLiteral(objectLiteral);
			}
			ObjectLiteralProperty objectProperty;
			if ((objectProperty = (node as ObjectLiteralProperty)) != null)
			{
				return BindingTransform.ConvertToBindingObjectProperty(objectProperty);
			}
			ConstantWrapper constantWrapper;
			if ((constantWrapper = (node as ConstantWrapper)) != null && constantWrapper.Value == Missing.Value)
			{
				return constantWrapper;
			}
			ImportExportSpecifier specifier;
			if ((specifier = (node as ImportExportSpecifier)) != null)
			{
				return BindingTransform.ConvertToBindingSpecifier(specifier);
			}
			node.Context.HandleError(JSError.UnableToConvertToBinding, true);
			return null;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000032F8 File Offset: 0x000014F8
		private static BindingIdentifier ConvertToBindingIdentifier(Lookup lookup)
		{
			BindingIdentifier bindingIdentifier = null;
			if (lookup != null)
			{
				bindingIdentifier = new BindingIdentifier(lookup.Context)
				{
					Name = lookup.Name,
					VariableField = lookup.VariableField
				};
				lookup.VariableField.IfNotNull(delegate(JSVariableField v)
				{
					v.Declarations.Add(bindingIdentifier);
					v.References.Remove(lookup);
				});
			}
			return bindingIdentifier;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003384 File Offset: 0x00001584
		private static ArrayLiteral ConvertToBindingArrayLiteral(ArrayLiteral arrayLiteral)
		{
			ArrayLiteral arrayLiteral2 = null;
			if (arrayLiteral != null)
			{
				arrayLiteral2 = new ArrayLiteral(arrayLiteral.Context)
				{
					TerminatingContext = arrayLiteral.TerminatingContext
				};
				if (arrayLiteral.Elements != null)
				{
					arrayLiteral2.Elements = new AstNodeList(arrayLiteral.Elements.Context);
					foreach (AstNode node in arrayLiteral.Elements)
					{
						arrayLiteral2.Elements.Append(BindingTransform.ConvertToBinding(node));
					}
				}
			}
			return arrayLiteral2;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000341C File Offset: 0x0000161C
		private static ObjectLiteral ConvertToBindingObjectLiteral(ObjectLiteral objectLiteral)
		{
			ObjectLiteral objectLiteral2 = null;
			if (objectLiteral != null)
			{
				objectLiteral2 = new ObjectLiteral(objectLiteral.Context)
				{
					TerminatingContext = objectLiteral.TerminatingContext
				};
				if (objectLiteral.Properties != null)
				{
					objectLiteral2.Properties = new AstNodeList(objectLiteral.Properties.Context);
					foreach (AstNode node in objectLiteral.Properties)
					{
						objectLiteral2.Properties.Append(BindingTransform.ConvertToBinding(node));
					}
				}
			}
			return objectLiteral2;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000034B4 File Offset: 0x000016B4
		private static ObjectLiteralProperty ConvertToBindingObjectProperty(ObjectLiteralProperty objectProperty)
		{
			ObjectLiteralProperty result = null;
			if (objectProperty != null)
			{
				result = new ObjectLiteralProperty(objectProperty.Context)
				{
					Name = BindingTransform.ConvertToBindingObjectName(objectProperty.Name),
					Value = BindingTransform.ConvertToBinding(objectProperty.Value),
					TerminatingContext = objectProperty.TerminatingContext
				};
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003504 File Offset: 0x00001704
		private static ObjectLiteralField ConvertToBindingObjectName(ObjectLiteralField objectName)
		{
			ObjectLiteralField result = null;
			if (objectName != null)
			{
				result = new ObjectLiteralField(objectName.Name, objectName.PrimitiveType, objectName.Context)
				{
					IsIdentifier = objectName.IsIdentifier,
					ColonContext = objectName.ColonContext,
					MayHaveIssues = objectName.MayHaveIssues,
					TerminatingContext = objectName.TerminatingContext
				};
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003561 File Offset: 0x00001761
		private static ImportExportSpecifier ConvertToBindingSpecifier(ImportExportSpecifier specifier)
		{
			if (specifier != null && specifier.LocalIdentifier != null)
			{
				specifier.LocalIdentifier = BindingTransform.ConvertToBinding(specifier.LocalIdentifier);
			}
			return specifier;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003580 File Offset: 0x00001780
		private static void RecurseParameters(AstNodeList parameterList, AstNode node)
		{
			if (node != null)
			{
				BinaryOperator binaryOperator = node as BinaryOperator;
				if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Comma)
				{
					BindingTransform.RecurseParameters(parameterList, binaryOperator.Operand1);
					AstNodeList astNodeList = binaryOperator.Operand2 as AstNodeList;
					if (astNodeList != null)
					{
						using (IEnumerator<AstNode> enumerator = astNodeList.Children.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								AstNode node2 = enumerator.Current;
								parameterList.Append(BindingTransform.ConvertToParameter(node2, parameterList.Count));
							}
							return;
						}
					}
					parameterList.Append(BindingTransform.ConvertToParameter(binaryOperator.Operand2, parameterList.Count));
					return;
				}
				parameterList.Append(BindingTransform.ConvertToParameter(node, 0));
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003638 File Offset: 0x00001838
		private static ParameterDeclaration ConvertToParameter(AstNode node, int position)
		{
			ParameterDeclaration parameterDeclaration = new ParameterDeclaration(node.Context)
			{
				Position = position
			};
			UnaryOperator unaryOperator = node as UnaryOperator;
			if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.RestSpread)
			{
				parameterDeclaration.HasRest = true;
				parameterDeclaration.RestContext = unaryOperator.OperatorContext;
				parameterDeclaration.Binding = BindingTransform.ConvertToBinding(unaryOperator.Operand);
			}
			else
			{
				BinaryOperator binaryOperator = node as BinaryOperator;
				if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Assign)
				{
					parameterDeclaration.AssignContext = binaryOperator.OperatorContext;
					parameterDeclaration.Initializer = binaryOperator.Operand2;
					parameterDeclaration.Binding = BindingTransform.ConvertToBinding(binaryOperator.Operand1);
				}
				else
				{
					parameterDeclaration.Binding = BindingTransform.ConvertToBinding(node);
				}
			}
			if (parameterDeclaration.Binding == null)
			{
				return null;
			}
			return parameterDeclaration;
		}
	}
}

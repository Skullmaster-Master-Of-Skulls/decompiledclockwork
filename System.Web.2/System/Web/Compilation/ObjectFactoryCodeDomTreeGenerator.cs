using System;
using System.CodeDom;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200084F RID: 2127
	internal class ObjectFactoryCodeDomTreeGenerator
	{
		// Token: 0x060064E9 RID: 25833 RVA: 0x00161790 File Offset: 0x0015F990
		internal ObjectFactoryCodeDomTreeGenerator(string outputAssemblyName)
		{
			this._codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace("__ASP");
			this._codeCompileUnit.Namespaces.Add(codeNamespace);
			string name = "FastObjectFactory_" + Util.MakeValidTypeNameFromString(outputAssemblyName).ToLower(CultureInfo.InvariantCulture);
			this._factoryClass = new CodeTypeDeclaration(name);
			this._factoryClass.TypeAttributes &= ~TypeAttributes.Public;
			CodeSnippetTypeMember codeSnippetTypeMember = new CodeSnippetTypeMember(string.Empty);
			codeSnippetTypeMember.LinePragma = new CodeLinePragma("c:\\\\dummy.txt", 1);
			this._factoryClass.Members.Add(codeSnippetTypeMember);
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes |= MemberAttributes.Private;
			this._factoryClass.Members.Add(codeConstructor);
			codeNamespace.Types.Add(this._factoryClass);
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x00161870 File Offset: 0x0015FA70
		internal void AddFactoryMethod(string typeToCreate, CodeCompileUnit ccu = null)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = ObjectFactoryCodeDomTreeGenerator.GetCreateMethodNameForType(typeToCreate);
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(object));
			codeMemberMethod.Attributes = MemberAttributes.Static;
			ObjectFactoryCodeDomTreeGenerator.AddCreateTypeInstanceStatement(typeToCreate, ccu, codeMemberMethod.Statements);
			this._factoryClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x001618CC File Offset: 0x0015FACC
		private static void AddCreateTypeInstanceStatement(string typeToCreate, CodeCompileUnit ccu, CodeStatementCollection statements)
		{
			if (BinaryCompatibility.Current.TargetsAtLeastFramework472 && ccu != null)
			{
				CodePropertyReferenceExpression right = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("System.Web.HttpRuntime"), "WebObjectActivator");
				statements.Add(new CodeVariableDeclarationStatement(typeof(IServiceProvider), "__activator"));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("__activator");
				statements.Add(new CodeAssignStatement(codeVariableReferenceExpression, right));
				CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(codeVariableReferenceExpression, "GetService", new CodeExpression[]
				{
					new CodeTypeOfExpression(typeToCreate)
				});
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement
				{
					Condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null))
				};
				codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement(expression));
				if (ObjectFactoryCodeDomTreeGenerator.DoesGeneratedCodeHaveDefaultCtor(typeToCreate, ccu))
				{
					CodeObjectCreateExpression expression2 = new CodeObjectCreateExpression(typeToCreate, new CodeExpression[0]);
					codeConditionStatement.FalseStatements.Add(new CodeMethodReturnStatement(expression2));
				}
				else
				{
					CodeThrowExceptionStatement value = new CodeThrowExceptionStatement(new CodeObjectCreateExpression(new CodeTypeReference(typeof(InvalidOperationException)), new CodeExpression[]
					{
						new CodePrimitiveExpression(SR.GetString("Could_not_create_type_instance", new object[]
						{
							typeToCreate
						}))
					}));
					codeConditionStatement.FalseStatements.Add(value);
				}
				statements.Add(codeConditionStatement);
				return;
			}
			CodeObjectCreateExpression expression3 = new CodeObjectCreateExpression(typeToCreate, new CodeExpression[0]);
			statements.Add(new CodeMethodReturnStatement(expression3));
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x00161A1C File Offset: 0x0015FC1C
		private static bool DoesGeneratedCodeHaveDefaultCtor(string typeToCreate, CodeCompileUnit ccu)
		{
			for (int i = 0; i < ccu.Namespaces.Count; i++)
			{
				CodeNamespace codeNamespace = ccu.Namespaces[i];
				for (int j = 0; j < codeNamespace.Types.Count; j++)
				{
					CodeTypeDeclaration codeTypeDeclaration = codeNamespace.Types[j];
					if (StringUtil.Equals(typeToCreate, codeNamespace.Name + "." + codeTypeDeclaration.Name))
					{
						foreach (object obj in codeTypeDeclaration.Members)
						{
							CodeConstructor codeConstructor = obj as CodeConstructor;
							if (codeConstructor != null && (codeConstructor.Attributes & MemberAttributes.Public) == MemberAttributes.Public && ObjectFactoryCodeDomTreeGenerator.DoesAllConstructorParametersHaveDefaultValue(codeConstructor))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x00161B14 File Offset: 0x0015FD14
		private static bool DoesAllConstructorParametersHaveDefaultValue(CodeConstructor ctor)
		{
			foreach (object obj in ctor.Parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				bool flag = false;
				foreach (object obj2 in codeParameterDeclarationExpression.CustomAttributes)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj2;
					CodeTypeReference attributeType = codeAttributeDeclaration.AttributeType;
					if (((attributeType != null) ? attributeType.BaseType : null) == ObjectFactoryCodeDomTreeGenerator.optionalAttributeTypeName)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x00161BE0 File Offset: 0x0015FDE0
		private static string GetCreateMethodNameForType(string typeToCreate)
		{
			return "Create_" + Util.MakeValidTypeNameFromString(typeToCreate);
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x00161BF2 File Offset: 0x0015FDF2
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this._codeCompileUnit;
			}
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x00161BFC File Offset: 0x0015FDFC
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static InstantiateObject GetFastObjectCreationDelegate(Type t)
		{
			Assembly assembly = t.Assembly;
			string text = Util.GetAssemblyShortName(t.Assembly);
			text = text.ToLower(CultureInfo.InvariantCulture);
			Type type = assembly.GetType("__ASP.FastObjectFactory_" + Util.MakeValidTypeNameFromString(text));
			if (type == null)
			{
				return null;
			}
			string createMethodNameForType = ObjectFactoryCodeDomTreeGenerator.GetCreateMethodNameForType(t.FullName);
			InstantiateObject result;
			try
			{
				result = (InstantiateObject)Delegate.CreateDelegate(typeof(InstantiateObject), type, createMethodNameForType);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400340B RID: 13323
		private CodeCompileUnit _codeCompileUnit;

		// Token: 0x0400340C RID: 13324
		private CodeTypeDeclaration _factoryClass;

		// Token: 0x0400340D RID: 13325
		private const string factoryClassNameBase = "FastObjectFactory_";

		// Token: 0x0400340E RID: 13326
		private const string factoryFullClassNameBase = "__ASP.FastObjectFactory_";

		// Token: 0x0400340F RID: 13327
		private static readonly string optionalAttributeTypeName = typeof(OptionalAttribute).FullName;
	}
}

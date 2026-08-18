using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Resources.Tools
{
	// Token: 0x0200000B RID: 11
	public static class StronglyTypedResourceBuilder
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000222B File Offset: 0x0000042B
		public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			return StronglyTypedResourceBuilder.Create(resourceList, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000223C File Offset: 0x0000043C
		public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			if (resourceList == null)
			{
				throw new ArgumentNullException("resourceList");
			}
			Dictionary<string, StronglyTypedResourceBuilder.ResourceData> dictionary = new Dictionary<string, StronglyTypedResourceBuilder.ResourceData>(StringComparer.InvariantCultureIgnoreCase);
			foreach (object obj in resourceList)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				ResXDataNode resXDataNode = dictionaryEntry.Value as ResXDataNode;
				StronglyTypedResourceBuilder.ResourceData value;
				if (resXDataNode != null)
				{
					string text = (string)dictionaryEntry.Key;
					if (text != resXDataNode.Name)
					{
						throw new ArgumentException(SR.GetString("MismatchedResourceName", new object[]
						{
							text,
							resXDataNode.Name
						}));
					}
					string valueTypeName = resXDataNode.GetValueTypeName(null);
					Type type = Type.GetType(valueTypeName);
					string valueAsString = resXDataNode.GetValue(null).ToString();
					value = new StronglyTypedResourceBuilder.ResourceData(type, valueAsString);
				}
				else
				{
					Type type2 = (dictionaryEntry.Value == null) ? typeof(object) : dictionaryEntry.Value.GetType();
					value = new StronglyTypedResourceBuilder.ResourceData(type2, (dictionaryEntry.Value == null) ? null : dictionaryEntry.Value.ToString());
				}
				dictionary.Add((string)dictionaryEntry.Key, value);
			}
			return StronglyTypedResourceBuilder.InternalCreate(dictionary, baseName, generatedCodeNamespace, resourcesNamespace, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002390 File Offset: 0x00000590
		private static CodeCompileUnit InternalCreate(Dictionary<string, StronglyTypedResourceBuilder.ResourceData> resourceList, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (codeProvider == null)
			{
				throw new ArgumentNullException("codeProvider");
			}
			ArrayList arrayList = new ArrayList(0);
			Hashtable hashtable;
			SortedList sortedList = StronglyTypedResourceBuilder.VerifyResourceNames(resourceList, codeProvider, arrayList, out hashtable);
			string text = baseName;
			if (!codeProvider.IsValidIdentifier(text))
			{
				string text2 = StronglyTypedResourceBuilder.VerifyResourceName(text, codeProvider);
				if (text2 != null)
				{
					text = text2;
				}
			}
			if (!codeProvider.IsValidIdentifier(text))
			{
				throw new ArgumentException(SR.GetString("InvalidIdentifier", new object[]
				{
					text
				}));
			}
			if (!string.IsNullOrEmpty(generatedCodeNamespace) && !codeProvider.IsValidIdentifier(generatedCodeNamespace))
			{
				string text3 = StronglyTypedResourceBuilder.VerifyResourceName(generatedCodeNamespace, codeProvider, true);
				if (text3 != null)
				{
					generatedCodeNamespace = text3;
				}
			}
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.ReferencedAssemblies.Add("System.dll");
			codeCompileUnit.UserData.Add("AllowLateBound", false);
			codeCompileUnit.UserData.Add("RequireVariableDeclaration", true);
			CodeNamespace codeNamespace = new CodeNamespace(generatedCodeNamespace);
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeCompileUnit.Namespaces.Add(codeNamespace);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(text);
			codeNamespace.Types.Add(codeTypeDeclaration);
			StronglyTypedResourceBuilder.AddGeneratedCodeAttributeforMember(codeTypeDeclaration);
			TypeAttributes typeAttributes = internalClass ? TypeAttributes.NotPublic : TypeAttributes.Public;
			codeTypeDeclaration.TypeAttributes = typeAttributes;
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(SR.GetString("ClassDocComment"), true));
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));
			CodeTypeReference codeTypeReference = new CodeTypeReference(typeof(DebuggerNonUserCodeAttribute));
			codeTypeReference.Options = CodeTypeReferenceOptions.GlobalReference;
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(codeTypeReference));
			CodeTypeReference codeTypeReference2 = new CodeTypeReference(typeof(CompilerGeneratedAttribute));
			codeTypeReference2.Options = CodeTypeReferenceOptions.GlobalReference;
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(codeTypeReference2));
			bool useStatic = internalClass || codeProvider.Supports(GeneratorSupport.PublicStaticMembers);
			bool supportsTryCatch = codeProvider.Supports(GeneratorSupport.TryCatchStatements);
			ITargetAwareCodeDomProvider targetAwareCodeDomProvider = codeProvider as ITargetAwareCodeDomProvider;
			bool flag = targetAwareCodeDomProvider != null && !targetAwareCodeDomProvider.SupportsProperty(typeof(Type), "Assembly", false);
			if (flag)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
			}
			StronglyTypedResourceBuilder.EmitBasicClassMembers(codeTypeDeclaration, generatedCodeNamespace, baseName, resourcesNamespace, internalClass, useStatic, supportsTryCatch, flag);
			foreach (object obj in sortedList)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text4 = (string)dictionaryEntry.Key;
				string text5 = (string)hashtable[text4];
				if (text5 == null)
				{
					text5 = text4;
				}
				if (!StronglyTypedResourceBuilder.DefineResourceFetchingProperty(text4, text5, (StronglyTypedResourceBuilder.ResourceData)dictionaryEntry.Value, codeTypeDeclaration, internalClass, useStatic))
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			unmatchable = (string[])arrayList.ToArray(typeof(string));
			CodeGenerator.ValidateIdentifiers(codeCompileUnit);
			return codeCompileUnit;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026B4 File Offset: 0x000008B4
		public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			return StronglyTypedResourceBuilder.Create(resxFile, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026C4 File Offset: 0x000008C4
		public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			if (resxFile == null)
			{
				throw new ArgumentNullException("resxFile");
			}
			Dictionary<string, StronglyTypedResourceBuilder.ResourceData> dictionary = new Dictionary<string, StronglyTypedResourceBuilder.ResourceData>(StringComparer.InvariantCultureIgnoreCase);
			using (ResXResourceReader resXResourceReader = new ResXResourceReader(resxFile))
			{
				resXResourceReader.UseResXDataNodes = true;
				foreach (object obj in resXResourceReader)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					ResXDataNode resXDataNode = (ResXDataNode)dictionaryEntry.Value;
					string valueTypeName = resXDataNode.GetValueTypeName(null);
					Type type = Type.GetType(valueTypeName);
					string valueAsString = resXDataNode.GetValue(null).ToString();
					StronglyTypedResourceBuilder.ResourceData value = new StronglyTypedResourceBuilder.ResourceData(type, valueAsString);
					dictionary.Add((string)dictionaryEntry.Key, value);
				}
			}
			return StronglyTypedResourceBuilder.InternalCreate(dictionary, baseName, generatedCodeNamespace, resourcesNamespace, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027B0 File Offset: 0x000009B0
		private static void AddGeneratedCodeAttributeforMember(CodeTypeMember typeMember)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(GeneratedCodeAttribute)));
			codeAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
			CodeAttributeArgument value = new CodeAttributeArgument(new CodePrimitiveExpression(typeof(StronglyTypedResourceBuilder).FullName));
			CodeAttributeArgument value2 = new CodeAttributeArgument(new CodePrimitiveExpression("4.0.0.0"));
			codeAttributeDeclaration.Arguments.Add(value);
			codeAttributeDeclaration.Arguments.Add(value2);
			typeMember.CustomAttributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002830 File Offset: 0x00000A30
		private static void EmitBasicClassMembers(CodeTypeDeclaration srClass, string nameSpace, string baseName, string resourcesNamespace, bool internalClass, bool useStatic, bool supportsTryCatch, bool useTypeInfo)
		{
			string value;
			if (resourcesNamespace != null)
			{
				if (resourcesNamespace.Length > 0)
				{
					value = resourcesNamespace + "." + baseName;
				}
				else
				{
					value = baseName;
				}
			}
			else if (nameSpace != null && nameSpace.Length > 0)
			{
				value = nameSpace + "." + baseName;
			}
			else
			{
				value = baseName;
			}
			CodeCommentStatement value2 = new CodeCommentStatement(SR.GetString("ClassComments1"));
			srClass.Comments.Add(value2);
			value2 = new CodeCommentStatement(SR.GetString("ClassComments2"));
			srClass.Comments.Add(value2);
			value2 = new CodeCommentStatement(SR.GetString("ClassComments3"));
			srClass.Comments.Add(value2);
			value2 = new CodeCommentStatement(SR.GetString("ClassComments4"));
			srClass.Comments.Add(value2);
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(SuppressMessageAttribute)));
			codeAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Performance")));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("CA1811:AvoidUncalledPrivateCode")));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.CustomAttributes.Add(codeAttributeDeclaration);
			if (useStatic || internalClass)
			{
				codeConstructor.Attributes = MemberAttributes.FamilyAndAssembly;
			}
			else
			{
				codeConstructor.Attributes = MemberAttributes.Public;
			}
			srClass.Members.Add(codeConstructor);
			CodeTypeReference codeTypeReference = new CodeTypeReference(typeof(ResourceManager), CodeTypeReferenceOptions.GlobalReference);
			CodeMemberField codeMemberField = new CodeMemberField(codeTypeReference, "resourceMan");
			codeMemberField.Attributes = MemberAttributes.Private;
			if (useStatic)
			{
				codeMemberField.Attributes |= MemberAttributes.Static;
			}
			srClass.Members.Add(codeMemberField);
			CodeTypeReference type = new CodeTypeReference(typeof(CultureInfo), CodeTypeReferenceOptions.GlobalReference);
			codeMemberField = new CodeMemberField(type, "resourceCulture");
			codeMemberField.Attributes = MemberAttributes.Private;
			if (useStatic)
			{
				codeMemberField.Attributes |= MemberAttributes.Static;
			}
			srClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			srClass.Members.Add(codeMemberProperty);
			codeMemberProperty.Name = "ResourceManager";
			codeMemberProperty.HasGet = true;
			codeMemberProperty.HasSet = false;
			codeMemberProperty.Type = codeTypeReference;
			if (internalClass)
			{
				codeMemberProperty.Attributes = MemberAttributes.Assembly;
			}
			else
			{
				codeMemberProperty.Attributes = MemberAttributes.Public;
			}
			if (useStatic)
			{
				codeMemberProperty.Attributes |= MemberAttributes.Static;
			}
			CodeAttributeArgument codeAttributeArgument = new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(new CodeTypeReference(typeof(EditorBrowsableState))
			{
				Options = CodeTypeReferenceOptions.GlobalReference
			}), "Advanced"));
			CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration("System.ComponentModel.EditorBrowsableAttribute", new CodeAttributeArgument[]
			{
				codeAttributeArgument
			});
			codeAttributeDeclaration2.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
			codeMemberProperty.CustomAttributes.Add(codeAttributeDeclaration2);
			CodeMemberProperty codeMemberProperty2 = new CodeMemberProperty();
			srClass.Members.Add(codeMemberProperty2);
			codeMemberProperty2.Name = "Culture";
			codeMemberProperty2.HasGet = true;
			codeMemberProperty2.HasSet = true;
			codeMemberProperty2.Type = type;
			if (internalClass)
			{
				codeMemberProperty2.Attributes = MemberAttributes.Assembly;
			}
			else
			{
				codeMemberProperty2.Attributes = MemberAttributes.Public;
			}
			if (useStatic)
			{
				codeMemberProperty2.Attributes |= MemberAttributes.Static;
			}
			codeMemberProperty2.CustomAttributes.Add(codeAttributeDeclaration2);
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(null, "resourceMan");
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(object)), "ReferenceEquals");
			CodeMethodInvokeExpression condition = new CodeMethodInvokeExpression(method, new CodeExpression[]
			{
				codeFieldReferenceExpression,
				new CodePrimitiveExpression(null)
			});
			CodePropertyReferenceExpression codePropertyReferenceExpression;
			if (useTypeInfo)
			{
				CodeMethodInvokeExpression targetObject = new CodeMethodInvokeExpression(new CodeTypeOfExpression(new CodeTypeReference(srClass.Name)), "GetTypeInfo", new CodeExpression[0]);
				codePropertyReferenceExpression = new CodePropertyReferenceExpression(targetObject, "Assembly");
			}
			else
			{
				codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeTypeOfExpression(new CodeTypeReference(srClass.Name)), "Assembly");
			}
			CodeObjectCreateExpression initExpression = new CodeObjectCreateExpression(codeTypeReference, new CodeExpression[]
			{
				new CodePrimitiveExpression(value),
				codePropertyReferenceExpression
			});
			CodeStatement[] trueStatements = new CodeStatement[]
			{
				new CodeVariableDeclarationStatement(codeTypeReference, "temp", initExpression),
				new CodeAssignStatement(codeFieldReferenceExpression, new CodeVariableReferenceExpression("temp"))
			};
			codeMemberProperty.GetStatements.Add(new CodeConditionStatement(condition, trueStatements));
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeFieldReferenceExpression));
			codeMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
			codeMemberProperty.Comments.Add(new CodeCommentStatement(SR.GetString("ResMgrPropertyComment"), true));
			codeMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
			CodeFieldReferenceExpression codeFieldReferenceExpression2 = new CodeFieldReferenceExpression(null, "resourceCulture");
			codeMemberProperty2.GetStatements.Add(new CodeMethodReturnStatement(codeFieldReferenceExpression2));
			CodePropertySetValueReferenceExpression right = new CodePropertySetValueReferenceExpression();
			codeMemberProperty2.SetStatements.Add(new CodeAssignStatement(codeFieldReferenceExpression2, right));
			codeMemberProperty2.Comments.Add(new CodeCommentStatement("<summary>", true));
			codeMemberProperty2.Comments.Add(new CodeCommentStatement(SR.GetString("CulturePropertyComment1"), true));
			codeMemberProperty2.Comments.Add(new CodeCommentStatement(SR.GetString("CulturePropertyComment2"), true));
			codeMemberProperty2.Comments.Add(new CodeCommentStatement("</summary>", true));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D68 File Offset: 0x00000F68
		private static string TruncateAndFormatCommentStringForOutput(string commentString)
		{
			if (commentString != null)
			{
				if (commentString.Length > 512)
				{
					commentString = SR.GetString("StringPropertyTruncatedComment", new object[]
					{
						commentString.Substring(0, 512)
					});
				}
				commentString = SecurityElement.Escape(commentString);
			}
			return commentString;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private static bool DefineResourceFetchingProperty(string propertyName, string resourceName, StronglyTypedResourceBuilder.ResourceData data, CodeTypeDeclaration srClass, bool internalClass, bool useStatic)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = propertyName;
			codeMemberProperty.HasGet = true;
			codeMemberProperty.HasSet = false;
			Type type = data.Type;
			if (type == null)
			{
				return false;
			}
			if (type == typeof(MemoryStream))
			{
				type = typeof(UnmanagedMemoryStream);
			}
			while (!type.IsPublic)
			{
				type = type.BaseType;
			}
			CodeTypeReference codeTypeReference = new CodeTypeReference(type);
			codeMemberProperty.Type = codeTypeReference;
			if (internalClass)
			{
				codeMemberProperty.Attributes = MemberAttributes.Assembly;
			}
			else
			{
				codeMemberProperty.Attributes = MemberAttributes.Public;
			}
			if (useStatic)
			{
				codeMemberProperty.Attributes |= MemberAttributes.Static;
			}
			CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(null, "ResourceManager");
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(useStatic ? null : new CodeThisReferenceExpression(), "resourceCulture");
			bool flag = type == typeof(string);
			bool flag2 = type == typeof(UnmanagedMemoryStream) || type == typeof(MemoryStream);
			string methodName = string.Empty;
			string text = string.Empty;
			string text2 = StronglyTypedResourceBuilder.TruncateAndFormatCommentStringForOutput(data.ValueAsString);
			string text3 = string.Empty;
			if (!flag)
			{
				text3 = StronglyTypedResourceBuilder.TruncateAndFormatCommentStringForOutput(type.ToString());
			}
			if (flag)
			{
				methodName = "GetString";
			}
			else if (flag2)
			{
				methodName = "GetStream";
			}
			else
			{
				methodName = "GetObject";
			}
			if (flag)
			{
				text = SR.GetString("StringPropertyComment", new object[]
				{
					text2
				});
			}
			else if (text2 == null || string.Equals(text3, text2))
			{
				text = SR.GetString("NonStringPropertyComment", new object[]
				{
					text3
				});
			}
			else
			{
				text = SR.GetString("NonStringPropertyDetailedComment", new object[]
				{
					text3,
					text2
				});
			}
			codeMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
			codeMemberProperty.Comments.Add(new CodeCommentStatement(text, true));
			codeMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
			CodeExpression codeExpression = new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[]
			{
				new CodePrimitiveExpression(resourceName),
				codeFieldReferenceExpression
			});
			CodeMethodReturnStatement value;
			if (flag || flag2)
			{
				value = new CodeMethodReturnStatement(codeExpression);
			}
			else
			{
				CodeVariableDeclarationStatement value2 = new CodeVariableDeclarationStatement(typeof(object), "obj", codeExpression);
				codeMemberProperty.GetStatements.Add(value2);
				value = new CodeMethodReturnStatement(new CodeCastExpression(codeTypeReference, new CodeVariableReferenceExpression("obj")));
			}
			codeMemberProperty.GetStatements.Add(value);
			srClass.Members.Add(codeMemberProperty);
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003025 File Offset: 0x00001225
		public static string VerifyResourceName(string key, CodeDomProvider provider)
		{
			return StronglyTypedResourceBuilder.VerifyResourceName(key, provider, false);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003030 File Offset: 0x00001230
		private static string VerifyResourceName(string key, CodeDomProvider provider, bool isNameSpace)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			foreach (char c in StronglyTypedResourceBuilder.CharsToReplace)
			{
				if (!isNameSpace || (c != '.' && c != ':'))
				{
					key = key.Replace(c, '_');
				}
			}
			if (provider.IsValidIdentifier(key))
			{
				return key;
			}
			key = provider.CreateValidIdentifier(key);
			if (provider.IsValidIdentifier(key))
			{
				return key;
			}
			key = "_" + key;
			if (provider.IsValidIdentifier(key))
			{
				return key;
			}
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000030C4 File Offset: 0x000012C4
		private static SortedList VerifyResourceNames(Dictionary<string, StronglyTypedResourceBuilder.ResourceData> resourceList, CodeDomProvider codeProvider, ArrayList errors, out Hashtable reverseFixupTable)
		{
			reverseFixupTable = new Hashtable(0, StringComparer.InvariantCultureIgnoreCase);
			SortedList sortedList = new SortedList(StringComparer.InvariantCultureIgnoreCase, resourceList.Count);
			foreach (KeyValuePair<string, StronglyTypedResourceBuilder.ResourceData> keyValuePair in resourceList)
			{
				string text = keyValuePair.Key;
				if (string.Equals(text, "ResourceManager") || string.Equals(text, "Culture") || typeof(void) == keyValuePair.Value.Type)
				{
					errors.Add(text);
				}
				else if ((text.Length <= 0 || text[0] != '$') && (text.Length <= 1 || text[0] != '>' || text[1] != '>'))
				{
					if (!codeProvider.IsValidIdentifier(text))
					{
						string text2 = StronglyTypedResourceBuilder.VerifyResourceName(text, codeProvider, false);
						if (text2 == null)
						{
							errors.Add(text);
							continue;
						}
						string text3 = (string)reverseFixupTable[text2];
						if (text3 != null)
						{
							if (!errors.Contains(text3))
							{
								errors.Add(text3);
							}
							if (sortedList.Contains(text2))
							{
								sortedList.Remove(text2);
							}
							errors.Add(text);
							continue;
						}
						reverseFixupTable[text2] = text;
						text = text2;
					}
					StronglyTypedResourceBuilder.ResourceData value = keyValuePair.Value;
					if (!sortedList.Contains(text))
					{
						sortedList.Add(text, value);
					}
					else
					{
						string text4 = (string)reverseFixupTable[text];
						if (text4 != null)
						{
							if (!errors.Contains(text4))
							{
								errors.Add(text4);
							}
							reverseFixupTable.Remove(text);
						}
						errors.Add(keyValuePair.Key);
						sortedList.Remove(text);
					}
				}
			}
			return sortedList;
		}

		// Token: 0x040000AD RID: 173
		private const string ResMgrFieldName = "resourceMan";

		// Token: 0x040000AE RID: 174
		private const string ResMgrPropertyName = "ResourceManager";

		// Token: 0x040000AF RID: 175
		private const string CultureInfoFieldName = "resourceCulture";

		// Token: 0x040000B0 RID: 176
		private const string CultureInfoPropertyName = "Culture";

		// Token: 0x040000B1 RID: 177
		private static readonly char[] CharsToReplace = new char[]
		{
			' ',
			'\u00a0',
			'.',
			',',
			';',
			'|',
			'~',
			'@',
			'#',
			'%',
			'^',
			'&',
			'*',
			'+',
			'-',
			'/',
			'\\',
			'<',
			'>',
			'?',
			'[',
			']',
			'(',
			')',
			'{',
			'}',
			'"',
			'\'',
			':',
			'!'
		};

		// Token: 0x040000B2 RID: 178
		private const char ReplacementChar = '_';

		// Token: 0x040000B3 RID: 179
		private const string DocCommentSummaryStart = "<summary>";

		// Token: 0x040000B4 RID: 180
		private const string DocCommentSummaryEnd = "</summary>";

		// Token: 0x040000B5 RID: 181
		private const int DocCommentLengthThreshold = 512;

		// Token: 0x0200039D RID: 925
		internal sealed class ResourceData
		{
			// Token: 0x0600258F RID: 9615 RVA: 0x000EB5D4 File Offset: 0x000E97D4
			internal ResourceData(Type type, string valueAsString)
			{
				this._type = type;
				this._valueAsString = valueAsString;
			}

			// Token: 0x170007E5 RID: 2021
			// (get) Token: 0x06002590 RID: 9616 RVA: 0x000EB5EA File Offset: 0x000E97EA
			internal Type Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x170007E6 RID: 2022
			// (get) Token: 0x06002591 RID: 9617 RVA: 0x000EB5F2 File Offset: 0x000E97F2
			internal string ValueAsString
			{
				get
				{
					return this._valueAsString;
				}
			}

			// Token: 0x04001B72 RID: 7026
			private Type _type;

			// Token: 0x04001B73 RID: 7027
			private string _valueAsString;
		}
	}
}

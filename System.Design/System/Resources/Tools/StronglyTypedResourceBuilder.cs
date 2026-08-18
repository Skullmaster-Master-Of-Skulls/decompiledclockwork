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
	// Token: 0x0200054D RID: 1357
	public static class StronglyTypedResourceBuilder
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x0010EC07 File Offset: 0x0010DC07
		public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			return StronglyTypedResourceBuilder.Create(resourceList, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x0010EC18 File Offset: 0x0010DC18
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
					string valueIfItWasAString = null;
					if (type == typeof(string))
					{
						valueIfItWasAString = (string)resXDataNode.GetValue(null);
					}
					value = new StronglyTypedResourceBuilder.ResourceData(type, valueIfItWasAString);
				}
				else
				{
					Type type2 = (dictionaryEntry.Value == null) ? typeof(object) : dictionaryEntry.Value.GetType();
					value = new StronglyTypedResourceBuilder.ResourceData(type2, dictionaryEntry.Value as string);
				}
				dictionary.Add((string)dictionaryEntry.Key, value);
			}
			return StronglyTypedResourceBuilder.InternalCreate(dictionary, baseName, generatedCodeNamespace, resourcesNamespace, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0010ED88 File Offset: 0x0010DD88
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
			StronglyTypedResourceBuilder.EmitBasicClassMembers(codeTypeDeclaration, generatedCodeNamespace, baseName, resourcesNamespace, internalClass, useStatic, supportsTryCatch);
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

		// Token: 0x06002F93 RID: 12179 RVA: 0x0010F06C File Offset: 0x0010E06C
		public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			return StronglyTypedResourceBuilder.Create(resxFile, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0010F07C File Offset: 0x0010E07C
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
					string valueIfItWasAString = null;
					if (type == typeof(string))
					{
						valueIfItWasAString = (string)resXDataNode.GetValue(null);
					}
					StronglyTypedResourceBuilder.ResourceData value = new StronglyTypedResourceBuilder.ResourceData(type, valueIfItWasAString);
					dictionary.Add((string)dictionaryEntry.Key, value);
				}
			}
			return StronglyTypedResourceBuilder.InternalCreate(dictionary, baseName, generatedCodeNamespace, resourcesNamespace, codeProvider, internalClass, out unmatchable);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0010F17C File Offset: 0x0010E17C
		private static void AddGeneratedCodeAttributeforMember(CodeTypeMember typeMember)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(GeneratedCodeAttribute)));
			codeAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
			CodeAttributeArgument value = new CodeAttributeArgument(new CodePrimitiveExpression(typeof(StronglyTypedResourceBuilder).FullName));
			CodeAttributeArgument value2 = new CodeAttributeArgument(new CodePrimitiveExpression("2.0.0.0"));
			codeAttributeDeclaration.Arguments.Add(value);
			codeAttributeDeclaration.Arguments.Add(value2);
			typeMember.CustomAttributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x0010F1FC File Offset: 0x0010E1FC
		private static void EmitBasicClassMembers(CodeTypeDeclaration srClass, string nameSpace, string baseName, string resourcesNamespace, bool internalClass, bool useStatic, bool supportsTryCatch)
		{
			string value;
			if (resourcesNamespace != null)
			{
				if (resourcesNamespace.Length > 0)
				{
					value = resourcesNamespace + '.' + baseName;
				}
				else
				{
					value = baseName;
				}
			}
			else if (nameSpace != null && nameSpace.Length > 0)
			{
				value = nameSpace + '.' + baseName;
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
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeTypeOfExpression(new CodeTypeReference(srClass.Name)), "Assembly");
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

		// Token: 0x06002F97 RID: 12183 RVA: 0x0010F714 File Offset: 0x0010E714
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
			string methodName = "GetObject";
			if (flag)
			{
				methodName = "GetString";
				string text = data.ValueIfString;
				if (text == null)
				{
					text = string.Empty;
				}
				if (text.Length > 512)
				{
					text = SR.GetString("StringPropertyTruncatedComment", new object[]
					{
						text.Substring(0, 512)
					});
				}
				text = SecurityElement.Escape(text);
				string text2 = string.Format(CultureInfo.CurrentCulture, SR.GetString("StringPropertyComment"), new object[]
				{
					text
				});
				codeMemberProperty.Comments.Add(new CodeCommentStatement("<summary>", true));
				codeMemberProperty.Comments.Add(new CodeCommentStatement(text2, true));
				codeMemberProperty.Comments.Add(new CodeCommentStatement("</summary>", true));
			}
			else if (flag2)
			{
				methodName = "GetStream";
			}
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

		// Token: 0x06002F98 RID: 12184 RVA: 0x0010F96A File Offset: 0x0010E96A
		public static string VerifyResourceName(string key, CodeDomProvider provider)
		{
			return StronglyTypedResourceBuilder.VerifyResourceName(key, provider, false);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x0010F974 File Offset: 0x0010E974
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

		// Token: 0x06002F9A RID: 12186 RVA: 0x0010FA08 File Offset: 0x0010EA08
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

		// Token: 0x0400204C RID: 8268
		private const string ResMgrFieldName = "resourceMan";

		// Token: 0x0400204D RID: 8269
		private const string ResMgrPropertyName = "ResourceManager";

		// Token: 0x0400204E RID: 8270
		private const string CultureInfoFieldName = "resourceCulture";

		// Token: 0x0400204F RID: 8271
		private const string CultureInfoPropertyName = "Culture";

		// Token: 0x04002050 RID: 8272
		private const char ReplacementChar = '_';

		// Token: 0x04002051 RID: 8273
		private const string DocCommentSummaryStart = "<summary>";

		// Token: 0x04002052 RID: 8274
		private const string DocCommentSummaryEnd = "</summary>";

		// Token: 0x04002053 RID: 8275
		private const int DocCommentLengthThreshold = 512;

		// Token: 0x04002054 RID: 8276
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

		// Token: 0x0200054E RID: 1358
		internal sealed class ResourceData
		{
			// Token: 0x06002F9C RID: 12188 RVA: 0x0010FC25 File Offset: 0x0010EC25
			internal ResourceData(Type type, string valueIfItWasAString)
			{
				this._type = type;
				this._valueIfString = valueIfItWasAString;
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x06002F9D RID: 12189 RVA: 0x0010FC3B File Offset: 0x0010EC3B
			internal Type Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x06002F9E RID: 12190 RVA: 0x0010FC43 File Offset: 0x0010EC43
			internal string ValueIfString
			{
				get
				{
					return this._valueIfString;
				}
			}

			// Token: 0x04002055 RID: 8277
			private Type _type;

			// Token: 0x04002056 RID: 8278
			private string _valueIfString;
		}
	}
}

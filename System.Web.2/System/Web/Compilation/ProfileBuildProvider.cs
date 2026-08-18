using System;
using System.CodeDom;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Profile;

namespace System.Web.Compilation
{
	// Token: 0x02000857 RID: 2135
	internal class ProfileBuildProvider : BuildProvider
	{
		// Token: 0x06006527 RID: 25895 RVA: 0x001522E0 File Offset: 0x001504E0
		private ProfileBuildProvider()
		{
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x00163D2C File Offset: 0x00161F2C
		internal static ProfileBuildProvider Create()
		{
			ProfileBuildProvider profileBuildProvider = new ProfileBuildProvider();
			profileBuildProvider.SetVirtualPath(HttpRuntime.AppDomainAppVirtualPathObject.SimpleCombine("Profile"));
			return profileBuildProvider;
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06006529 RID: 25897 RVA: 0x00163D55 File Offset: 0x00161F55
		internal static bool HasCompilableProfile
		{
			get
			{
				return ProfileManager.Enabled && (ProfileBase.GetPropertiesForCompilation().Count != 0 || ProfileBase.InheritsFromCustomType || ProfileManager.DynamicProfileProperties.Count != 0);
			}
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x00163D84 File Offset: 0x00161F84
		internal static Type GetProfileTypeFromAssembly(Assembly assembly, bool isPrecompiledApp)
		{
			if (!ProfileBuildProvider.HasCompilableProfile)
			{
				return null;
			}
			Type type = assembly.GetType("ProfileCommon");
			if (type == null && isPrecompiledApp)
			{
				throw new HttpException(SR.GetString("Profile_not_precomped"));
			}
			return type;
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x00163DC4 File Offset: 0x00161FC4
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			Hashtable propertiesForCompilation = ProfileBase.GetPropertiesForCompilation();
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			Hashtable hashtable = new Hashtable();
			Type type = Type.GetType(ProfileBase.InheritsFromTypeString, false);
			CodeNamespace codeNamespace = new CodeNamespace();
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.Profile"));
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = "ProfileCommon";
			if (type != null)
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(type));
				assemblyBuilder.AddAssemblyReference(type.Assembly, codeCompileUnit);
			}
			else
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(ProfileBase.InheritsFromTypeString));
				ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
				if (profileAppConfig != null)
				{
					PropertyInformation propertyInformation = profileAppConfig.ElementInformation.Properties["inherits"];
					if (propertyInformation != null && propertyInformation.Source != null && propertyInformation.LineNumber > 0)
					{
						codeTypeDeclaration.LinePragma = new CodeLinePragma(HttpRuntime.GetSafePath(propertyInformation.Source), propertyInformation.LineNumber);
					}
				}
			}
			assemblyBuilder.GenerateTypeFactory("ProfileCommon");
			foreach (object obj in propertiesForCompilation)
			{
				ProfileNameTypeStruct profileNameTypeStruct = (ProfileNameTypeStruct)((DictionaryEntry)obj).Value;
				if (profileNameTypeStruct.PropertyType != null)
				{
					assemblyBuilder.AddAssemblyReference(profileNameTypeStruct.PropertyType.Assembly, codeCompileUnit);
				}
				int num = profileNameTypeStruct.Name.IndexOf('.');
				if (num < 0)
				{
					this.CreateCodeForProperty(assemblyBuilder, codeTypeDeclaration, profileNameTypeStruct);
				}
				else
				{
					string text = profileNameTypeStruct.Name.Substring(0, num);
					if (!assemblyBuilder.CodeDomProvider.IsValidIdentifier(text))
					{
						throw new ConfigurationErrorsException(SR.GetString("Profile_bad_group", new object[]
						{
							text
						}), profileNameTypeStruct.FileName, profileNameTypeStruct.LineNumber);
					}
					if (hashtable[text] == null)
					{
						hashtable.Add(text, profileNameTypeStruct.Name);
					}
					else
					{
						hashtable[text] = (string)hashtable[text] + ";" + profileNameTypeStruct.Name;
					}
				}
			}
			foreach (object obj2 in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				this.AddPropertyGroup(assemblyBuilder, (string)dictionaryEntry.Key, (string)dictionaryEntry.Value, propertiesForCompilation, codeTypeDeclaration, codeNamespace);
			}
			this.AddCodeForGetProfileForUser(codeTypeDeclaration);
			codeNamespace.Types.Add(codeTypeDeclaration);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			assemblyBuilder.AddCodeCompileUnit(this, codeCompileUnit);
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x001640CC File Offset: 0x001622CC
		private void CreateCodeForProperty(AssemblyBuilder assemblyBuilder, CodeTypeDeclaration type, ProfileNameTypeStruct property)
		{
			string text = property.Name;
			int num = text.IndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			if (!assemblyBuilder.CodeDomProvider.IsValidIdentifier(text))
			{
				throw new ConfigurationErrorsException(SR.GetString("Profile_bad_name"), property.FileName, property.LineNumber);
			}
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = text;
			codeMemberProperty.Attributes = MemberAttributes.Public;
			codeMemberProperty.HasGet = true;
			codeMemberProperty.Type = property.PropertyCodeRefType;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "GetPropertyValue";
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(text));
			CodeMethodReturnStatement value = new CodeMethodReturnStatement(new CodeCastExpression(codeMemberProperty.Type, codeMethodInvokeExpression));
			codeMemberProperty.GetStatements.Add(value);
			if (!property.IsReadOnly)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
				codeMethodInvokeExpression2.Method.TargetObject = new CodeThisReferenceExpression();
				codeMethodInvokeExpression2.Method.MethodName = "SetPropertyValue";
				codeMethodInvokeExpression2.Parameters.Add(new CodePrimitiveExpression(text));
				codeMethodInvokeExpression2.Parameters.Add(new CodePropertySetValueReferenceExpression());
				codeMemberProperty.HasSet = true;
				codeMemberProperty.SetStatements.Add(codeMethodInvokeExpression2);
			}
			type.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x00164220 File Offset: 0x00162420
		private void AddPropertyGroup(AssemblyBuilder assemblyBuilder, string groupName, string propertyNames, Hashtable properties, CodeTypeDeclaration type, CodeNamespace ns)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = groupName;
			codeMemberProperty.Attributes = MemberAttributes.Public;
			codeMemberProperty.HasGet = true;
			codeMemberProperty.Type = new CodeTypeReference("ProfileGroup" + groupName);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "GetProfileGroup";
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(codeMemberProperty.Name));
			CodeMethodReturnStatement value = new CodeMethodReturnStatement(new CodeCastExpression(codeMemberProperty.Type, codeMethodInvokeExpression));
			codeMemberProperty.GetStatements.Add(value);
			type.Members.Add(codeMemberProperty);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = "ProfileGroup" + groupName;
			codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(ProfileGroupBase)));
			string[] array = propertyNames.Split(new char[]
			{
				';'
			});
			foreach (string key in array)
			{
				this.CreateCodeForProperty(assemblyBuilder, codeTypeDeclaration, (ProfileNameTypeStruct)properties[key]);
			}
			ns.Types.Add(codeTypeDeclaration);
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x00164354 File Offset: 0x00162554
		private void AddCodeForGetProfileForUser(CodeTypeDeclaration type)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "GetProfile";
			codeMemberMethod.Attributes = MemberAttributes.Public;
			codeMemberMethod.ReturnType = new CodeTypeReference("ProfileCommon");
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "username"));
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeTypeReferenceExpression("ProfileBase");
			codeMethodInvokeExpression.Method.MethodName = "Create";
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("username"));
			CodeMethodReturnStatement value = new CodeMethodReturnStatement(new CodeCastExpression(codeMemberMethod.ReturnType, codeMethodInvokeExpression));
			ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
			codeMemberMethod.Statements.Add(value);
			type.Members.Add(codeMemberMethod);
		}

		// Token: 0x04003431 RID: 13361
		private const string ProfileTypeName = "ProfileCommon";
	}
}

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x02000800 RID: 2048
	internal class ApplicationBrowserCapabilitiesCodeGenerator : BrowserCapabilitiesCodeGenerator
	{
		// Token: 0x060061C6 RID: 25030 RVA: 0x001557F8 File Offset: 0x001539F8
		internal ApplicationBrowserCapabilitiesCodeGenerator(BuildProvider buildProvider)
		{
			this._browserOverrides = new OrderedDictionary();
			this._defaultBrowserOverrides = new OrderedDictionary();
			this._buildProvider = buildProvider;
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x060061C7 RID: 25031 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool GenerateOverrides
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x0015581D File Offset: 0x00153A1D
		internal override string TypeName
		{
			get
			{
				return "ApplicationBrowserCapabilitiesFactory";
			}
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void Create()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x00155824 File Offset: 0x00153A24
		private static void AddStringToHashtable(OrderedDictionary table, object key, string content, bool before)
		{
			ArrayList arrayList = (ArrayList)table[key];
			if (arrayList == null)
			{
				arrayList = new ArrayList(1);
				table[key] = arrayList;
			}
			if (before)
			{
				arrayList.Insert(0, content);
				return;
			}
			arrayList.Add(content);
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x00155864 File Offset: 0x00153A64
		private static string GetFirstItemFromKey(OrderedDictionary table, object key)
		{
			ArrayList arrayList = (ArrayList)table[key];
			if (arrayList != null && arrayList.Count > 0)
			{
				return arrayList[0] as string;
			}
			return null;
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x00155898 File Offset: 0x00153A98
		internal override void HandleUnRecognizedParentElement(BrowserDefinition bd, bool isDefault)
		{
			string parentName = bd.ParentName;
			int num = bd.GetType().GetHashCode() ^ parentName.GetHashCode();
			if (isDefault)
			{
				ApplicationBrowserCapabilitiesCodeGenerator.AddStringToHashtable(this._defaultBrowserOverrides, num, bd.Name, bd.IsRefID);
				return;
			}
			ApplicationBrowserCapabilitiesCodeGenerator.AddStringToHashtable(this._browserOverrides, num, bd.Name, bd.IsRefID);
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x00155900 File Offset: 0x00153B00
		internal void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			base.ProcessBrowserFiles(true, BrowserCapabilitiesCompiler.AppBrowsersVirtualDir.VirtualPathString);
			base.ProcessCustomBrowserFiles(true, BrowserCapabilitiesCompiler.AppBrowsersVirtualDir.VirtualPathString);
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < base.CustomTreeNames.Count; i++)
			{
				arrayList.Add((BrowserDefinition)((BrowserTree)base.CustomTreeList[i])[base.CustomTreeNames[i]]);
			}
			CodeNamespace codeNamespace = new CodeNamespace("ASP");
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.Configuration"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
			codeCompileUnit.Namespaces.Add(codeNamespace);
			Type browserCapabilitiesFactoryBaseType = BrowserCapabilitiesCompiler.GetBrowserCapabilitiesFactoryBaseType();
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Attributes = MemberAttributes.Private;
			codeTypeDeclaration.IsClass = true;
			codeTypeDeclaration.Name = this.TypeName;
			codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(browserCapabilitiesFactoryBaseType));
			codeNamespace.Types.Add(codeTypeDeclaration);
			BindingFlags bindingAttr = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic;
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = (MemberAttributes)24580;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			codeMemberMethod.Name = "ConfigureCustomCapabilities";
			CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(typeof(NameValueCollection), "headers");
			codeMemberMethod.Parameters.Add(value);
			value = new CodeParameterDeclarationExpression(typeof(HttpBrowserCapabilities), "browserCaps");
			codeMemberMethod.Parameters.Add(value);
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			for (int j = 0; j < arrayList.Count; j++)
			{
				base.GenerateSingleProcessCall((BrowserDefinition)arrayList[j], codeMemberMethod);
			}
			foreach (object obj in this._browserOverrides)
			{
				object key = ((DictionaryEntry)obj).Key;
				BrowserDefinition browserDefinition = (BrowserDefinition)base.BrowserTree[ApplicationBrowserCapabilitiesCodeGenerator.GetFirstItemFromKey(this._browserOverrides, key)];
				string parentName = browserDefinition.ParentName;
				if (!TargetFrameworkUtil.HasMethod(browserCapabilitiesFactoryBaseType, parentName + "ProcessBrowsers", bindingAttr) || !TargetFrameworkUtil.HasMethod(browserCapabilitiesFactoryBaseType, parentName + "ProcessGateways", bindingAttr))
				{
					string parentID = browserDefinition.ParentID;
					if (browserDefinition != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Browser_parentID_Not_Found", new object[]
						{
							parentID
						}), browserDefinition.XmlNode);
					}
					throw new ConfigurationErrorsException(SR.GetString("Browser_parentID_Not_Found", new object[]
					{
						parentID
					}));
				}
				else
				{
					bool flag = true;
					if (browserDefinition is GatewayDefinition)
					{
						flag = false;
					}
					string name = parentName + (flag ? "ProcessBrowsers" : "ProcessGateways");
					CodeMemberMethod codeMemberMethod2 = new CodeMemberMethod();
					codeMemberMethod2.Name = name;
					codeMemberMethod2.ReturnType = new CodeTypeReference(typeof(void));
					codeMemberMethod2.Attributes = (MemberAttributes)12292;
					if (flag)
					{
						value = new CodeParameterDeclarationExpression(typeof(bool), "ignoreApplicationBrowsers");
						codeMemberMethod2.Parameters.Add(value);
					}
					value = new CodeParameterDeclarationExpression(typeof(NameValueCollection), "headers");
					codeMemberMethod2.Parameters.Add(value);
					value = new CodeParameterDeclarationExpression(typeof(HttpBrowserCapabilities), "browserCaps");
					codeMemberMethod2.Parameters.Add(value);
					codeTypeDeclaration.Members.Add(codeMemberMethod2);
					ArrayList arrayList2 = (ArrayList)this._browserOverrides[key];
					CodeStatementCollection stmts = codeMemberMethod2.Statements;
					bool flag2 = false;
					foreach (object obj2 in arrayList2)
					{
						string key2 = (string)obj2;
						BrowserDefinition browserDefinition2 = (BrowserDefinition)base.BrowserTree[key2];
						if (browserDefinition2 is GatewayDefinition || browserDefinition2.IsRefID)
						{
							base.GenerateSingleProcessCall(browserDefinition2, codeMemberMethod2);
						}
						else
						{
							if (!flag2)
							{
								CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
								codeConditionStatement.Condition = new CodeVariableReferenceExpression("ignoreApplicationBrowsers");
								codeMemberMethod2.Statements.Add(codeConditionStatement);
								stmts = codeConditionStatement.FalseStatements;
								flag2 = true;
							}
							stmts = base.GenerateTrackedSingleProcessCall(stmts, browserDefinition2, codeMemberMethod2);
							if (this._baseInstance == null)
							{
								if (MultiTargetingUtil.IsTargetFramework40OrAbove || browserCapabilitiesFactoryBaseType.Assembly == BrowserCapabilitiesCompiler.AspBrowserCapsFactoryAssembly)
								{
									this._baseInstance = (BrowserCapabilitiesFactoryBase)Activator.CreateInstance(browserCapabilitiesFactoryBaseType);
								}
								else
								{
									this._baseInstance = new BrowserCapabilitiesFactory35();
								}
							}
							int num = (int)((Triplet)this._baseInstance.InternalGetBrowserElements()[parentName]).Third;
							base.AddBrowserToCollectionRecursive(browserDefinition2, num + 1);
						}
					}
				}
			}
			foreach (object obj3 in this._defaultBrowserOverrides)
			{
				object key3 = ((DictionaryEntry)obj3).Key;
				BrowserDefinition browserDefinition3 = (BrowserDefinition)base.DefaultTree[ApplicationBrowserCapabilitiesCodeGenerator.GetFirstItemFromKey(this._defaultBrowserOverrides, key3)];
				string parentName2 = browserDefinition3.ParentName;
				if (browserCapabilitiesFactoryBaseType.GetMethod("Default" + parentName2 + "ProcessBrowsers", bindingAttr) == null)
				{
					string parentID2 = browserDefinition3.ParentID;
					if (browserDefinition3 != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("DefaultBrowser_parentID_Not_Found", new object[]
						{
							parentID2
						}), browserDefinition3.XmlNode);
					}
				}
				string name2 = "Default" + parentName2 + "ProcessBrowsers";
				CodeMemberMethod codeMemberMethod3 = new CodeMemberMethod();
				codeMemberMethod3.Name = name2;
				codeMemberMethod3.ReturnType = new CodeTypeReference(typeof(void));
				codeMemberMethod3.Attributes = (MemberAttributes)12292;
				value = new CodeParameterDeclarationExpression(typeof(bool), "ignoreApplicationBrowsers");
				codeMemberMethod3.Parameters.Add(value);
				value = new CodeParameterDeclarationExpression(typeof(NameValueCollection), "headers");
				codeMemberMethod3.Parameters.Add(value);
				value = new CodeParameterDeclarationExpression(typeof(HttpBrowserCapabilities), "browserCaps");
				codeMemberMethod3.Parameters.Add(value);
				codeTypeDeclaration.Members.Add(codeMemberMethod3);
				ArrayList arrayList3 = (ArrayList)this._defaultBrowserOverrides[key3];
				CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
				codeConditionStatement2.Condition = new CodeVariableReferenceExpression("ignoreApplicationBrowsers");
				codeMemberMethod3.Statements.Add(codeConditionStatement2);
				CodeStatementCollection stmts2 = codeConditionStatement2.FalseStatements;
				foreach (object obj4 in arrayList3)
				{
					string key4 = (string)obj4;
					BrowserDefinition browserDefinition2 = (BrowserDefinition)base.DefaultTree[key4];
					if (browserDefinition2.IsRefID)
					{
						base.GenerateSingleProcessCall(browserDefinition2, codeMemberMethod3, "Default");
					}
					else
					{
						stmts2 = base.GenerateTrackedSingleProcessCall(stmts2, browserDefinition2, codeMemberMethod3, "Default");
					}
				}
			}
			foreach (object obj5 in base.BrowserTree)
			{
				BrowserDefinition browserDefinition2 = ((DictionaryEntry)obj5).Value as BrowserDefinition;
				base.GenerateProcessMethod(browserDefinition2, codeTypeDeclaration);
			}
			for (int k = 0; k < arrayList.Count; k++)
			{
				foreach (object obj6 in ((BrowserTree)base.CustomTreeList[k]))
				{
					BrowserDefinition browserDefinition2 = ((DictionaryEntry)obj6).Value as BrowserDefinition;
					base.GenerateProcessMethod(browserDefinition2, codeTypeDeclaration);
				}
			}
			foreach (object obj7 in base.DefaultTree)
			{
				BrowserDefinition browserDefinition2 = ((DictionaryEntry)obj7).Value as BrowserDefinition;
				base.GenerateProcessMethod(browserDefinition2, codeTypeDeclaration, "Default");
			}
			base.GenerateOverrideMatchedHeaders(codeTypeDeclaration);
			base.GenerateOverrideBrowserElements(codeTypeDeclaration);
			Assembly assembly = BrowserCapabilitiesCompiler.GetBrowserCapabilitiesFactoryBaseType().Assembly;
			assemblyBuilder.AddAssemblyReference(assembly, codeCompileUnit);
			assemblyBuilder.AddCodeCompileUnit(this._buildProvider, codeCompileUnit);
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x00156254 File Offset: 0x00154454
		internal override void ProcessBrowserNode(XmlNode node, BrowserTree browserTree)
		{
			if (node.Name == "defaultBrowser")
			{
				throw new ConfigurationErrorsException(SR.GetString("Browser_Not_Allowed_InAppLevel", new object[]
				{
					node.Name
				}), node);
			}
			base.ProcessBrowserNode(node, browserTree);
		}

		// Token: 0x040032D4 RID: 13012
		internal const string FactoryTypeName = "ApplicationBrowserCapabilitiesFactory";

		// Token: 0x040032D5 RID: 13013
		private OrderedDictionary _browserOverrides;

		// Token: 0x040032D6 RID: 13014
		private OrderedDictionary _defaultBrowserOverrides;

		// Token: 0x040032D7 RID: 13015
		private BrowserCapabilitiesFactoryBase _baseInstance;

		// Token: 0x040032D8 RID: 13016
		private BuildProvider _buildProvider;
	}
}

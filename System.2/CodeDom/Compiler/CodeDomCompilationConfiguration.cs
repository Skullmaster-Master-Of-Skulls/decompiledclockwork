using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066D RID: 1645
	internal class CodeDomCompilationConfiguration
	{
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000F68DB File Offset: 0x000F4ADB
		internal static CodeDomCompilationConfiguration Default
		{
			get
			{
				return CodeDomCompilationConfiguration.defaultInstance;
			}
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000F68E4 File Offset: 0x000F4AE4
		internal CodeDomCompilationConfiguration()
		{
			this._compilerLanguages = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this._compilerExtensions = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this._allCompilerInfo = new ArrayList();
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			string codeDomProviderTypeName = "Microsoft.CSharp.CSharpCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			CompilerInfo compilerInfo = new CompilerInfo(compilerParameters, codeDomProviderTypeName);
			compilerInfo._compilerLanguages = new string[]
			{
				"c#",
				"cs",
				"csharp"
			};
			compilerInfo._compilerExtensions = new string[]
			{
				".cs",
				"cs"
			};
			compilerInfo._providerOptions = new Dictionary<string, string>();
			compilerInfo._providerOptions["CompilerVersion"] = "v4.0";
			this.AddCompilerInfo(compilerInfo);
			compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			codeDomProviderTypeName = "Microsoft.VisualBasic.VBCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			compilerInfo = new CompilerInfo(compilerParameters, codeDomProviderTypeName);
			compilerInfo._compilerLanguages = new string[]
			{
				"vb",
				"vbs",
				"visualbasic",
				"vbscript"
			};
			compilerInfo._compilerExtensions = new string[]
			{
				".vb",
				"vb"
			};
			compilerInfo._providerOptions = new Dictionary<string, string>();
			compilerInfo._providerOptions["CompilerVersion"] = "v4.0";
			this.AddCompilerInfo(compilerInfo);
			compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			codeDomProviderTypeName = "Microsoft.JScript.JScriptCodeProvider, Microsoft.JScript, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			this.AddCompilerInfo(new CompilerInfo(compilerParameters, codeDomProviderTypeName)
			{
				_compilerLanguages = new string[]
				{
					"js",
					"jscript",
					"javascript"
				},
				_compilerExtensions = new string[]
				{
					".js",
					"js"
				},
				_providerOptions = new Dictionary<string, string>()
			});
			compilerParameters = new CompilerParameters();
			compilerParameters.WarningLevel = 4;
			codeDomProviderTypeName = "Microsoft.VisualC.CppCodeProvider, CppCodeProvider, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			this.AddCompilerInfo(new CompilerInfo(compilerParameters, codeDomProviderTypeName)
			{
				_compilerLanguages = new string[]
				{
					"c++",
					"mc",
					"cpp"
				},
				_compilerExtensions = new string[]
				{
					".h",
					"h"
				},
				_providerOptions = new Dictionary<string, string>()
			});
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000F6B08 File Offset: 0x000F4D08
		private CodeDomCompilationConfiguration(CodeDomCompilationConfiguration original)
		{
			if (original._compilerLanguages != null)
			{
				this._compilerLanguages = (Hashtable)original._compilerLanguages.Clone();
			}
			if (original._compilerExtensions != null)
			{
				this._compilerExtensions = (Hashtable)original._compilerExtensions.Clone();
			}
			if (original._allCompilerInfo != null)
			{
				this._allCompilerInfo = (ArrayList)original._allCompilerInfo.Clone();
			}
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000F6B78 File Offset: 0x000F4D78
		private void AddCompilerInfo(CompilerInfo compilerInfo)
		{
			foreach (string key in compilerInfo._compilerLanguages)
			{
				this._compilerLanguages[key] = compilerInfo;
			}
			foreach (string key2 in compilerInfo._compilerExtensions)
			{
				this._compilerExtensions[key2] = compilerInfo;
			}
			this._allCompilerInfo.Add(compilerInfo);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000F6BE8 File Offset: 0x000F4DE8
		private void RemoveUnmapped()
		{
			for (int i = 0; i < this._allCompilerInfo.Count; i++)
			{
				((CompilerInfo)this._allCompilerInfo[i])._mapped = false;
			}
			foreach (object obj in this._compilerLanguages.Values)
			{
				CompilerInfo compilerInfo = (CompilerInfo)obj;
				compilerInfo._mapped = true;
			}
			foreach (object obj2 in this._compilerExtensions.Values)
			{
				CompilerInfo compilerInfo2 = (CompilerInfo)obj2;
				compilerInfo2._mapped = true;
			}
			for (int j = this._allCompilerInfo.Count - 1; j >= 0; j--)
			{
				if (!((CompilerInfo)this._allCompilerInfo[j])._mapped)
				{
					this._allCompilerInfo.RemoveAt(j);
				}
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000F6D08 File Offset: 0x000F4F08
		private CompilerInfo FindExistingCompilerInfo(string[] languageList, string[] extensionList)
		{
			CompilerInfo result = null;
			foreach (object obj in this._allCompilerInfo)
			{
				CompilerInfo compilerInfo = (CompilerInfo)obj;
				if (compilerInfo._compilerExtensions.Length == extensionList.Length && compilerInfo._compilerLanguages.Length == languageList.Length)
				{
					bool flag = false;
					for (int i = 0; i < compilerInfo._compilerExtensions.Length; i++)
					{
						if (compilerInfo._compilerExtensions[i] != extensionList[i])
						{
							flag = true;
							break;
						}
					}
					for (int j = 0; j < compilerInfo._compilerLanguages.Length; j++)
					{
						if (compilerInfo._compilerLanguages[j] != languageList[j])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						result = compilerInfo;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x04002C69 RID: 11369
		internal const string sectionName = "system.codedom";

		// Token: 0x04002C6A RID: 11370
		private static readonly char[] s_fieldSeparators = new char[]
		{
			';'
		};

		// Token: 0x04002C6B RID: 11371
		internal Hashtable _compilerLanguages;

		// Token: 0x04002C6C RID: 11372
		internal Hashtable _compilerExtensions;

		// Token: 0x04002C6D RID: 11373
		internal ArrayList _allCompilerInfo;

		// Token: 0x04002C6E RID: 11374
		private static CodeDomCompilationConfiguration defaultInstance = new CodeDomCompilationConfiguration();

		// Token: 0x020008B3 RID: 2227
		internal class SectionHandler
		{
			// Token: 0x0600462E RID: 17966 RVA: 0x00124F38 File Offset: 0x00123138
			private SectionHandler()
			{
			}

			// Token: 0x0600462F RID: 17967 RVA: 0x00124F40 File Offset: 0x00123140
			internal static object CreateStatic(object inheritedObject, XmlNode node)
			{
				CodeDomCompilationConfiguration codeDomCompilationConfiguration = (CodeDomCompilationConfiguration)inheritedObject;
				CodeDomCompilationConfiguration result;
				if (codeDomCompilationConfiguration == null)
				{
					result = new CodeDomCompilationConfiguration();
				}
				else
				{
					result = new CodeDomCompilationConfiguration(codeDomCompilationConfiguration);
				}
				HandlerBase.CheckForUnrecognizedAttributes(node);
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						if (xmlNode.Name == "compilers")
						{
							CodeDomCompilationConfiguration.SectionHandler.ProcessCompilersElement(result, xmlNode);
						}
						else
						{
							HandlerBase.ThrowUnrecognizedElement(xmlNode);
						}
					}
				}
				return result;
			}

			// Token: 0x06004630 RID: 17968 RVA: 0x00124FE0 File Offset: 0x001231E0
			private static IDictionary<string, string> GetProviderOptions(XmlNode compilerNode)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (object obj in compilerNode)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name != "providerOption")
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
					string key = null;
					string value = null;
					HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref key);
					HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "value", ref value);
					HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
					HandlerBase.CheckForChildNodes(xmlNode);
					dictionary[key] = value;
				}
				return dictionary;
			}

			// Token: 0x06004631 RID: 17969 RVA: 0x00125088 File Offset: 0x00123288
			private static void ProcessCompilersElement(CodeDomCompilationConfiguration result, XmlNode node)
			{
				HandlerBase.CheckForUnrecognizedAttributes(node);
				string filename = ConfigurationErrorsException.GetFilename(node);
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					int lineNumber = ConfigurationErrorsException.GetLineNumber(xmlNode);
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						if (xmlNode.Name != "compiler")
						{
							HandlerBase.ThrowUnrecognizedElement(xmlNode);
						}
						string empty = string.Empty;
						XmlNode andRemoveRequiredNonEmptyStringAttribute = HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "language", ref empty);
						string empty2 = string.Empty;
						HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "extension", ref empty2);
						string text = null;
						HandlerBase.GetAndRemoveStringAttribute(xmlNode, "type", ref text);
						CompilerParameters compilerParameters = new CompilerParameters();
						int num = 0;
						if (HandlerBase.GetAndRemoveNonNegativeIntegerAttribute(xmlNode, "warningLevel", ref num) != null)
						{
							compilerParameters.WarningLevel = num;
							compilerParameters.TreatWarningsAsErrors = (num > 0);
						}
						string compilerOptions = null;
						if (HandlerBase.GetAndRemoveStringAttribute(xmlNode, "compilerOptions", ref compilerOptions) != null)
						{
							compilerParameters.CompilerOptions = compilerOptions;
						}
						IDictionary<string, string> providerOptions = CodeDomCompilationConfiguration.SectionHandler.GetProviderOptions(xmlNode);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						string[] array = empty.Split(CodeDomCompilationConfiguration.s_fieldSeparators);
						string[] array2 = empty2.Split(CodeDomCompilationConfiguration.s_fieldSeparators);
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = array[i].Trim();
						}
						for (int j = 0; j < array2.Length; j++)
						{
							array2[j] = array2[j].Trim();
						}
						foreach (string text2 in array)
						{
							if (text2.Length == 0)
							{
								throw new ConfigurationErrorsException(SR.GetString("Language_Names_Cannot_Be_Empty"));
							}
						}
						foreach (string text3 in array2)
						{
							if (text3.Length == 0 || text3[0] != '.')
							{
								throw new ConfigurationErrorsException(SR.GetString("Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based"));
							}
						}
						CompilerInfo compilerInfo;
						if (text != null)
						{
							compilerInfo = new CompilerInfo(compilerParameters, text);
						}
						else
						{
							compilerInfo = result.FindExistingCompilerInfo(array, array2);
							if (compilerInfo == null)
							{
								throw new ConfigurationErrorsException();
							}
						}
						compilerInfo.configFileName = filename;
						compilerInfo.configFileLineNumber = lineNumber;
						if (text != null)
						{
							compilerInfo._compilerLanguages = array;
							compilerInfo._compilerExtensions = array2;
							compilerInfo._providerOptions = providerOptions;
							result.AddCompilerInfo(compilerInfo);
						}
						else
						{
							foreach (KeyValuePair<string, string> keyValuePair in providerOptions)
							{
								compilerInfo._providerOptions[keyValuePair.Key] = keyValuePair.Value;
							}
						}
					}
				}
				result.RemoveUnmapped();
			}
		}
	}
}

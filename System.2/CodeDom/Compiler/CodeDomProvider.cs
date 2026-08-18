using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000670 RID: 1648
	[ToolboxItem(false)]
	[ComVisible(true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeDomProvider : Component
	{
		// Token: 0x06003BB7 RID: 15287 RVA: 0x000F7078 File Offset: 0x000F5278
		[ComVisible(false)]
		public static CodeDomProvider CreateProvider(string language, IDictionary<string, string> providerOptions)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			return compilerInfo.CreateProvider(providerOptions);
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000F7094 File Offset: 0x000F5294
		[ComVisible(false)]
		public static CodeDomProvider CreateProvider(string language)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			return compilerInfo.CreateProvider();
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000F70B0 File Offset: 0x000F52B0
		[ComVisible(false)]
		public static string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtensionNoThrow = CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension);
			if (compilerInfoForExtensionNoThrow == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("CodeDomProvider_NotDefined"));
			}
			return compilerInfoForExtensionNoThrow._compilerLanguages[0];
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x000F70DF File Offset: 0x000F52DF
		[ComVisible(false)]
		public static bool IsDefinedLanguage(string language)
		{
			return CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language) != null;
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x000F70EA File Offset: 0x000F52EA
		[ComVisible(false)]
		public static bool IsDefinedExtension(string extension)
		{
			return CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension) != null;
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x000F70F8 File Offset: 0x000F52F8
		[ComVisible(false)]
		public static CompilerInfo GetCompilerInfo(string language)
		{
			CompilerInfo compilerInfoForLanguageNoThrow = CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language);
			if (compilerInfoForLanguageNoThrow == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("CodeDomProvider_NotDefined"));
			}
			return compilerInfoForLanguageNoThrow;
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000F7120 File Offset: 0x000F5320
		private static CompilerInfo GetCompilerInfoForLanguageNoThrow(string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			return (CompilerInfo)CodeDomProvider.Config._compilerLanguages[language.Trim()];
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000F7158 File Offset: 0x000F5358
		private static CompilerInfo GetCompilerInfoForExtensionNoThrow(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return (CompilerInfo)CodeDomProvider.Config._compilerExtensions[extension.Trim()];
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000F7190 File Offset: 0x000F5390
		[ComVisible(false)]
		public static CompilerInfo[] GetAllCompilerInfo()
		{
			ArrayList allCompilerInfo = CodeDomProvider.Config._allCompilerInfo;
			return (CompilerInfo[])allCompilerInfo.ToArray(typeof(CompilerInfo));
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000F71C0 File Offset: 0x000F53C0
		private static CodeDomCompilationConfiguration Config
		{
			get
			{
				CodeDomCompilationConfiguration codeDomCompilationConfiguration = (CodeDomCompilationConfiguration)PrivilegedConfigurationManager.GetSection("system.codedom");
				if (codeDomCompilationConfiguration == null)
				{
					return CodeDomCompilationConfiguration.Default;
				}
				return codeDomCompilationConfiguration;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000F71E7 File Offset: 0x000F53E7
		public virtual string FileExtension
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000F71EE File Offset: 0x000F53EE
		public virtual LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.None;
			}
		}

		// Token: 0x06003BC3 RID: 15299
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeGenerator CreateGenerator();

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000F71F1 File Offset: 0x000F53F1
		public virtual ICodeGenerator CreateGenerator(TextWriter output)
		{
			return this.CreateGenerator();
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000F71F9 File Offset: 0x000F53F9
		public virtual ICodeGenerator CreateGenerator(string fileName)
		{
			return this.CreateGenerator();
		}

		// Token: 0x06003BC6 RID: 15302
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeCompiler CreateCompiler();

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000F7201 File Offset: 0x000F5401
		[Obsolete("Callers should not use the ICodeParser interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public virtual ICodeParser CreateParser()
		{
			return null;
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000F7204 File Offset: 0x000F5404
		public virtual TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000F720C File Offset: 0x000F540C
		public virtual CompilerResults CompileAssemblyFromDom(CompilerParameters options, params CodeCompileUnit[] compilationUnits)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromDomBatch(options, compilationUnits);
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000F721B File Offset: 0x000F541B
		public virtual CompilerResults CompileAssemblyFromFile(CompilerParameters options, params string[] fileNames)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromFileBatch(options, fileNames);
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x000F722A File Offset: 0x000F542A
		public virtual CompilerResults CompileAssemblyFromSource(CompilerParameters options, params string[] sources)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromSourceBatch(options, sources);
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x000F7239 File Offset: 0x000F5439
		public virtual bool IsValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().IsValidIdentifier(value);
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x000F7247 File Offset: 0x000F5447
		public virtual string CreateEscapedIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateEscapedIdentifier(value);
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x000F7255 File Offset: 0x000F5455
		public virtual string CreateValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateValidIdentifier(value);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000F7263 File Offset: 0x000F5463
		public virtual string GetTypeOutput(CodeTypeReference type)
		{
			return this.CreateGeneratorHelper().GetTypeOutput(type);
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000F7271 File Offset: 0x000F5471
		public virtual bool Supports(GeneratorSupport generatorSupport)
		{
			return this.CreateGeneratorHelper().Supports(generatorSupport);
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000F727F File Offset: 0x000F547F
		public virtual void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromExpression(expression, writer, options);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000F728F File Offset: 0x000F548F
		public virtual void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromStatement(statement, writer, options);
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000F729F File Offset: 0x000F549F
		public virtual void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromNamespace(codeNamespace, writer, options);
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000F72AF File Offset: 0x000F54AF
		public virtual void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromCompileUnit(compileUnit, writer, options);
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000F72BF File Offset: 0x000F54BF
		public virtual void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromType(codeType, writer, options);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000F72CF File Offset: 0x000F54CF
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000F72E0 File Offset: 0x000F54E0
		public virtual CodeCompileUnit Parse(TextReader codeStream)
		{
			return this.CreateParserHelper().Parse(codeStream);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000F72F0 File Offset: 0x000F54F0
		private ICodeCompiler CreateCompilerHelper()
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
			}
			return codeCompiler;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000F7318 File Offset: 0x000F5518
		private ICodeGenerator CreateGeneratorHelper()
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
			}
			return codeGenerator;
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000F7340 File Offset: 0x000F5540
		private ICodeParser CreateParserHelper()
		{
			ICodeParser codeParser = this.CreateParser();
			if (codeParser == null)
			{
				throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
			}
			return codeParser;
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000F7368 File Offset: 0x000F5568
		internal static bool TryGetProbableCoreAssemblyFilePath(CompilerParameters parameters, out string coreAssemblyFilePath)
		{
			string text = null;
			char[] separator = new char[]
			{
				Path.DirectorySeparatorChar
			};
			string value = Path.Combine("Reference Assemblies", "Microsoft", "Framework");
			foreach (string text2 in parameters.ReferencedAssemblies)
			{
				if (Path.GetFileName(text2).Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase))
				{
					coreAssemblyFilePath = string.Empty;
					return false;
				}
				if (text2.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array.Length - 5; i++)
					{
						if (string.Equals(array[i], "Reference Assemblies", StringComparison.OrdinalIgnoreCase) && array[i + 4].StartsWith("v", StringComparison.OrdinalIgnoreCase))
						{
							if (text != null)
							{
								if (!string.Equals(text, Path.GetDirectoryName(text2), StringComparison.OrdinalIgnoreCase))
								{
									coreAssemblyFilePath = string.Empty;
									return false;
								}
							}
							else
							{
								text = Path.GetDirectoryName(text2);
							}
						}
					}
				}
			}
			if (text != null)
			{
				coreAssemblyFilePath = Path.Combine(text, "mscorlib.dll");
				return true;
			}
			coreAssemblyFilePath = string.Empty;
			return false;
		}
	}
}

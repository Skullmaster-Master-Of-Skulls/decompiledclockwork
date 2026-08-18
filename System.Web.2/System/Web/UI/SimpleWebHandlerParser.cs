using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.RegularExpressions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002FA RID: 762
	public abstract class SimpleWebHandlerParser : IAssemblyDependencyParser
	{
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x0007294F File Offset: 0x00070B4F
		internal string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x00072957 File Offset: 0x00070B57
		// (set) Token: 0x06002333 RID: 9011 RVA: 0x0007295F File Offset: 0x00070B5F
		internal bool IgnoreParseErrors
		{
			get
			{
				return this._ignoreParseErrors;
			}
			set
			{
				this._ignoreParseErrors = value;
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00072968 File Offset: 0x00070B68
		internal void SetBuildProvider(SimpleHandlerBuildProvider buildProvider)
		{
			this._buildProvider = buildProvider;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00072971 File Offset: 0x00070B71
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
		{
			this._virtualPath = VirtualPath.Create(virtualPath);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00072988 File Offset: 0x00070B88
		protected Type GetCompiledTypeFromCache()
		{
			BuildResultCompiledType buildResultCompiledType = (BuildResultCompiledType)BuildManager.GetVPathBuildResult(this._virtualPath);
			return buildResultCompiledType.ResultType;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000729AC File Offset: 0x00070BAC
		internal void Parse(ICollection referencedAssemblies)
		{
			this._referencedAssemblies = referencedAssemblies;
			this.AddSourceDependency(this._virtualPath);
			using (this._reader = this._buildProvider.OpenReaderInternal())
			{
				this.ParseReader();
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x00072A04 File Offset: 0x00070C04
		internal CompilerType CompilerType
		{
			get
			{
				return this._compilerType;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x00072A0C File Offset: 0x00070C0C
		internal ICollection AssemblyDependencies
		{
			get
			{
				return this._linkedAssemblies;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x00072A14 File Offset: 0x00070C14
		internal ICollection SourceDependencies
		{
			get
			{
				return this._sourceDependencies;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x00072A1C File Offset: 0x00070C1C
		internal CodeCompileUnit GetCodeModel()
		{
			if (this._sourceString == null)
			{
				return null;
			}
			return new CodeSnippetCompileUnit(this._sourceString)
			{
				LinePragma = BaseCodeDomTreeGenerator.CreateCodeLinePragmaHelper(this._virtualPath.VirtualPathString, this._lineNumber)
			};
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x00072A5C File Offset: 0x00070C5C
		internal IDictionary GetLinePragmasTable()
		{
			LinePragmaCodeInfo linePragmaCodeInfo = new LinePragmaCodeInfo();
			linePragmaCodeInfo._startLine = this._lineNumber;
			linePragmaCodeInfo._startColumn = this._startColumn;
			linePragmaCodeInfo._startGeneratedColumn = 1;
			linePragmaCodeInfo._codeLength = -1;
			linePragmaCodeInfo._isCodeNugget = false;
			IDictionary dictionary = new Hashtable();
			dictionary[this._lineNumber] = linePragmaCodeInfo;
			return dictionary;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x00072AB5 File Offset: 0x00070CB5
		internal bool HasInlineCode
		{
			get
			{
				return this._sourceString != null;
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00072AC0 File Offset: 0x00070CC0
		internal Type GetTypeToCache(Assembly builtAssembly)
		{
			Type type = null;
			if (builtAssembly != null)
			{
				type = builtAssembly.GetType(this._typeName);
			}
			if (type == null)
			{
				type = this.GetType(this._typeName);
			}
			try
			{
				this.ValidateBaseType(type);
			}
			catch (Exception ex)
			{
				throw new HttpParseException(ex.Message, ex, this._virtualPath, this._sourceString, this._lineNumber);
			}
			return type;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void ValidateBaseType(Type t)
		{
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00072B38 File Offset: 0x00070D38
		private void ParseReader()
		{
			string text = this._reader.ReadToEnd();
			try
			{
				this.ParseString(text);
			}
			catch (Exception ex)
			{
				throw new HttpParseException(ex.Message, ex, this._virtualPath, text, this._lineNumber);
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00072B88 File Offset: 0x00070D88
		private void ParseString(string text)
		{
			int num = 0;
			this._lineNumber = 1;
			for (;;)
			{
				Match match = SimpleWebHandlerParser.directiveRegex.Match(text, num);
				if (!match.Success)
				{
					break;
				}
				this._lineNumber += Util.LineCount(text, num, match.Index);
				num = match.Index;
				IDictionary dictionary = CollectionsUtil.CreateCaseInsensitiveSortedList();
				string directiveName = this.ProcessAttributes(match, dictionary);
				this.ProcessDirective(directiveName, dictionary);
				this._lineNumber += Util.LineCount(text, num, match.Index + match.Length);
				num = match.Index + match.Length;
				int num2 = text.LastIndexOfAny(SimpleWebHandlerParser.s_newlineChars, num - 1);
				this._startColumn = num - num2;
			}
			if (!this._fFoundMainDirective && !this.IgnoreParseErrors)
			{
				throw new HttpException(SR.GetString("Missing_directive", new object[]
				{
					this.DefaultDirectiveName
				}));
			}
			string text2 = text.Substring(num);
			if (!Util.IsWhiteSpaceString(text2))
			{
				this._sourceString = text2;
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x00072C84 File Offset: 0x00070E84
		private string ProcessAttributes(Match match, IDictionary attribs)
		{
			string result = string.Empty;
			CaptureCollection captures = match.Groups["attrname"].Captures;
			CaptureCollection captures2 = match.Groups["attrval"].Captures;
			CaptureCollection captureCollection = null;
			captureCollection = match.Groups["equal"].Captures;
			for (int i = 0; i < captures.Count; i++)
			{
				string text = captures[i].ToString();
				string value = captures2[i].ToString();
				bool flag = captureCollection[i].ToString().Length > 0;
				if (text != null)
				{
					if (!flag && i == 0)
					{
						result = text;
					}
					else
					{
						try
						{
							if (attribs != null)
							{
								attribs.Add(text, value);
							}
						}
						catch (ArgumentException)
						{
							if (!this.IgnoreParseErrors)
							{
								throw new HttpException(SR.GetString("Duplicate_attr_in_tag", new object[]
								{
									text
								}));
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002343 RID: 9027
		protected abstract string DefaultDirectiveName { get; }

		// Token: 0x06002344 RID: 9028 RVA: 0x00072D84 File Offset: 0x00070F84
		private static void ProcessCompilationParams(IDictionary directive, CompilerParameters compilParams)
		{
			bool includeDebugInformation = false;
			if (Util.GetAndRemoveBooleanAttribute(directive, "debug", ref includeDebugInformation))
			{
				compilParams.IncludeDebugInformation = includeDebugInformation;
			}
			if (compilParams.IncludeDebugInformation && !HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
			{
				throw new HttpException(SR.GetString("Insufficient_trust_for_attribute", new object[]
				{
					"debug"
				}));
			}
			int num = 0;
			if (Util.GetAndRemoveNonNegativeIntegerAttribute(directive, "warninglevel", ref num))
			{
				compilParams.WarningLevel = num;
				if (num > 0)
				{
					compilParams.TreatWarningsAsErrors = true;
				}
			}
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directive, "compileroptions");
			if (andRemoveNonEmptyAttribute != null)
			{
				CompilationUtil.CheckCompilerOptionsAllowed(andRemoveNonEmptyAttribute, false, null, 0);
				compilParams.CompilerOptions = andRemoveNonEmptyAttribute;
			}
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00072E20 File Offset: 0x00071020
		internal virtual void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (directiveName.Length == 0)
			{
				directiveName = this.DefaultDirectiveName;
			}
			if (this.IsMainDirective(directiveName))
			{
				if (this._fFoundMainDirective && !this.IgnoreParseErrors)
				{
					throw new HttpException(SR.GetString("Only_one_directive_allowed", new object[]
					{
						this.DefaultDirectiveName
					}));
				}
				this._fFoundMainDirective = true;
				directive.Remove("description");
				directive.Remove("codebehind");
				string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directive, "language");
				if (andRemoveNonEmptyAttribute != null)
				{
					this._compilerType = this._buildProvider.GetDefaultCompilerTypeForLanguageInternal(andRemoveNonEmptyAttribute);
				}
				else
				{
					this._compilerType = this._buildProvider.GetDefaultCompilerTypeInternal();
				}
				this._typeName = Util.GetAndRemoveRequiredAttribute(directive, "class");
				if (this._compilerType.CompilerParameters != null)
				{
					SimpleWebHandlerParser.ProcessCompilationParams(directive, this._compilerType.CompilerParameters);
				}
			}
			else if (StringUtil.EqualsIgnoreCase(directiveName, "assembly"))
			{
				string andRemoveNonEmptyAttribute2 = Util.GetAndRemoveNonEmptyAttribute(directive, "name");
				VirtualPath andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "src");
				if (andRemoveNonEmptyAttribute2 != null && andRemoveVirtualPathAttribute != null && !this.IgnoreParseErrors)
				{
					throw new HttpException(SR.GetString("Attributes_mutually_exclusive", new object[]
					{
						"Name",
						"Src"
					}));
				}
				if (andRemoveNonEmptyAttribute2 != null)
				{
					this.AddAssemblyDependency(andRemoveNonEmptyAttribute2);
				}
				else if (andRemoveVirtualPathAttribute != null)
				{
					this.ImportSourceFile(andRemoveVirtualPathAttribute);
				}
				else if (!this.IgnoreParseErrors)
				{
					throw new HttpException(SR.GetString("Missing_attr", new object[]
					{
						"name"
					}));
				}
			}
			else if (!this.IgnoreParseErrors)
			{
				throw new HttpException(SR.GetString("Unknown_directive", new object[]
				{
					directiveName
				}));
			}
			Util.CheckUnknownDirectiveAttributes(directiveName, directive);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00072FCF File Offset: 0x000711CF
		internal virtual bool IsMainDirective(string directiveName)
		{
			return string.Compare(directiveName, this.DefaultDirectiveName, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00072FE4 File Offset: 0x000711E4
		private void ImportSourceFile(VirtualPath virtualPath)
		{
			VirtualPath parent = this._virtualPath.Parent;
			VirtualPath virtualPath2 = parent.Combine(virtualPath);
			this.AddSourceDependency(virtualPath2);
			CompilationUtil.GetCompilerInfoFromVirtualPath(virtualPath2);
			BuildResultCompiledAssembly buildResultCompiledAssembly = (BuildResultCompiledAssembly)BuildManager.GetVPathBuildResult(virtualPath2);
			Assembly resultAssembly = buildResultCompiledAssembly.ResultAssembly;
			this.AddAssemblyDependency(resultAssembly);
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0007302D File Offset: 0x0007122D
		internal void AddSourceDependency(VirtualPath fileName)
		{
			if (this._sourceDependencies == null)
			{
				this._sourceDependencies = new CaseInsensitiveStringSet();
			}
			this._sourceDependencies.Add(fileName.VirtualPathString);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00073054 File Offset: 0x00071254
		private void AddAssemblyDependency(string assemblyName)
		{
			Assembly assembly = Assembly.Load(assemblyName);
			this.AddAssemblyDependency(assembly);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0007306F File Offset: 0x0007126F
		private void AddAssemblyDependency(Assembly assembly)
		{
			if (this._linkedAssemblies == null)
			{
				this._linkedAssemblies = new AssemblySet();
			}
			this._linkedAssemblies.Add(assembly);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00073090 File Offset: 0x00071290
		private Type GetType(string typeName)
		{
			Type type;
			if (Util.TypeNameContainsAssembly(typeName))
			{
				try
				{
					type = Type.GetType(typeName, true);
				}
				catch (Exception innerException)
				{
					throw new HttpParseException(null, innerException, this._virtualPath, this._sourceString, this._lineNumber);
				}
				return type;
			}
			type = Util.GetTypeFromAssemblies(this._referencedAssemblies, typeName, false);
			if (type != null)
			{
				return type;
			}
			type = Util.GetTypeFromAssemblies(this._linkedAssemblies, typeName, false);
			if (type != null)
			{
				return type;
			}
			throw new HttpParseException(SR.GetString("Could_not_create_type", new object[]
			{
				typeName
			}), null, this._virtualPath, this._sourceString, this._lineNumber);
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x0007313C File Offset: 0x0007133C
		ICollection IAssemblyDependencyParser.AssemblyDependencies
		{
			get
			{
				return this.AssemblyDependencies;
			}
		}

		// Token: 0x04001CA5 RID: 7333
		private static readonly Regex directiveRegex = new SimpleDirectiveRegex();

		// Token: 0x04001CA6 RID: 7334
		private SimpleHandlerBuildProvider _buildProvider;

		// Token: 0x04001CA7 RID: 7335
		private TextReader _reader;

		// Token: 0x04001CA8 RID: 7336
		private VirtualPath _virtualPath;

		// Token: 0x04001CA9 RID: 7337
		private int _lineNumber;

		// Token: 0x04001CAA RID: 7338
		private int _startColumn;

		// Token: 0x04001CAB RID: 7339
		private bool _fFoundMainDirective;

		// Token: 0x04001CAC RID: 7340
		private string _typeName;

		// Token: 0x04001CAD RID: 7341
		private CompilerType _compilerType;

		// Token: 0x04001CAE RID: 7342
		private string _sourceString;

		// Token: 0x04001CAF RID: 7343
		private AssemblySet _linkedAssemblies;

		// Token: 0x04001CB0 RID: 7344
		private ICollection _referencedAssemblies;

		// Token: 0x04001CB1 RID: 7345
		private static char[] s_newlineChars = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x04001CB2 RID: 7346
		private bool _ignoreParseErrors;

		// Token: 0x04001CB3 RID: 7347
		private StringSet _sourceDependencies;
	}
}

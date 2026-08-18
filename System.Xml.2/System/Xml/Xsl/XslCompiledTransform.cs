using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl
{
	// Token: 0x020002D5 RID: 725
	public sealed class XslCompiledTransform
	{
		// Token: 0x06002B85 RID: 11141 RVA: 0x000E74C2 File Offset: 0x000E56C2
		static XslCompiledTransform()
		{
			XslCompiledTransform.MemberAccessPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
			XslCompiledTransform.ReaderSettings = new XmlReaderSettings();
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000E74EA File Offset: 0x000E56EA
		public XslCompiledTransform()
		{
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000E74F2 File Offset: 0x000E56F2
		public XslCompiledTransform(bool enableDebug)
		{
			this.enableDebug = enableDebug;
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000E7501 File Offset: 0x000E5701
		private void Reset()
		{
			this.compilerResults = null;
			this.outputSettings = null;
			this.qil = null;
			this.command = null;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000E751F File Offset: 0x000E571F
		internal CompilerErrorCollection Errors
		{
			get
			{
				if (this.compilerResults == null)
				{
					return null;
				}
				return this.compilerResults.Errors;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000E7536 File Offset: 0x000E5736
		public XmlWriterSettings OutputSettings
		{
			get
			{
				return this.outputSettings;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000E753E File Offset: 0x000E573E
		public TempFileCollection TemporaryFiles
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				if (this.compilerResults == null)
				{
					return null;
				}
				return this.compilerResults.TempFiles;
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000E7555 File Offset: 0x000E5755
		public void Load(XmlReader stylesheet)
		{
			this.Reset();
			this.LoadInternal(stylesheet, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000E756F File Offset: 0x000E576F
		public void Load(XmlReader stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.LoadInternal(stylesheet, settings, stylesheetResolver);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000E7581 File Offset: 0x000E5781
		public void Load(IXPathNavigable stylesheet)
		{
			this.Reset();
			this.LoadInternal(stylesheet, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000E759B File Offset: 0x000E579B
		public void Load(IXPathNavigable stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.LoadInternal(stylesheet, settings, stylesheetResolver);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000E75AD File Offset: 0x000E57AD
		public void Load(string stylesheetUri)
		{
			this.Reset();
			if (stylesheetUri == null)
			{
				throw new ArgumentNullException("stylesheetUri");
			}
			this.LoadInternal(stylesheetUri, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000E75D5 File Offset: 0x000E57D5
		public void Load(string stylesheetUri, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			if (stylesheetUri == null)
			{
				throw new ArgumentNullException("stylesheetUri");
			}
			this.LoadInternal(stylesheetUri, settings, stylesheetResolver);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000E75F8 File Offset: 0x000E57F8
		private CompilerResults LoadInternal(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (settings == null)
			{
				settings = XsltSettings.Default;
			}
			this.CompileXsltToQil(stylesheet, settings, stylesheetResolver);
			CompilerError firstError = this.GetFirstError();
			if (firstError != null)
			{
				throw new XslLoadException(firstError);
			}
			if (!settings.CheckOnly)
			{
				this.CompileQilToMsil(settings);
			}
			return this.compilerResults;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000E764C File Offset: 0x000E584C
		private void CompileXsltToQil(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.compilerResults = new Compiler(settings, this.enableDebug, null).Compile(stylesheet, stylesheetResolver, out this.qil);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000E7670 File Offset: 0x000E5870
		private CompilerError GetFirstError()
		{
			foreach (object obj in this.compilerResults.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning)
				{
					return compilerError;
				}
			}
			return null;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000E76D8 File Offset: 0x000E58D8
		private void CompileQilToMsil(XsltSettings settings)
		{
			this.command = new XmlILGenerator().Generate(this.qil, null);
			this.outputSettings = this.command.StaticData.DefaultWriterSettings;
			this.qil = null;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000E7710 File Offset: 0x000E5910
		public static CompilerErrorCollection CompileToType(XmlReader stylesheet, XsltSettings settings, XmlResolver stylesheetResolver, bool debug, TypeBuilder typeBuilder, string scriptAssemblyPath)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (typeBuilder == null)
			{
				throw new ArgumentNullException("typeBuilder");
			}
			if (settings == null)
			{
				settings = XsltSettings.Default;
			}
			if (settings.EnableScript && scriptAssemblyPath == null)
			{
				throw new ArgumentNullException("scriptAssemblyPath");
			}
			if (scriptAssemblyPath != null)
			{
				scriptAssemblyPath = Path.GetFullPath(scriptAssemblyPath);
			}
			QilExpression query;
			CompilerErrorCollection errors = new Compiler(settings, debug, scriptAssemblyPath).Compile(stylesheet, stylesheetResolver, out query).Errors;
			if (!errors.HasErrors)
			{
				if (XslCompiledTransform.GeneratedCodeCtor == null)
				{
					XslCompiledTransform.GeneratedCodeCtor = typeof(GeneratedCodeAttribute).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string)
					});
				}
				typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(XslCompiledTransform.GeneratedCodeCtor, new object[]
				{
					typeof(XslCompiledTransform).FullName,
					"4.0.0.0"
				}));
				new XmlILGenerator().Generate(query, typeBuilder);
			}
			return errors;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000E7818 File Offset: 0x000E5A18
		public void Load(Type compiledStylesheet)
		{
			this.Reset();
			if (compiledStylesheet == null)
			{
				throw new ArgumentNullException("compiledStylesheet");
			}
			object[] customAttributes = compiledStylesheet.GetCustomAttributes(typeof(GeneratedCodeAttribute), false);
			GeneratedCodeAttribute generatedCodeAttribute = (customAttributes.Length != 0) ? ((GeneratedCodeAttribute)customAttributes[0]) : null;
			if (generatedCodeAttribute != null && generatedCodeAttribute.Tool == typeof(XslCompiledTransform).FullName)
			{
				if (new Version("4.0.0.0").CompareTo(new Version(generatedCodeAttribute.Version)) < 0)
				{
					throw new ArgumentException(Res.GetString("Xslt_IncompatibleCompiledStylesheetVersion", new object[]
					{
						generatedCodeAttribute.Version,
						"4.0.0.0"
					}), "compiledStylesheet");
				}
				FieldInfo field = compiledStylesheet.GetField("staticData", BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo field2 = compiledStylesheet.GetField("ebTypes", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null && field2 != null)
				{
					if (XsltConfigSection.EnableMemberAccessForXslCompiledTransform)
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
					}
					byte[] array = field.GetValue(null) as byte[];
					if (array != null)
					{
						MethodInfo method = compiledStylesheet.GetMethod("Execute", BindingFlags.Static | BindingFlags.NonPublic);
						Type[] earlyBoundTypes = (Type[])field2.GetValue(null);
						this.Load(method, array, earlyBoundTypes);
						return;
					}
				}
			}
			if (this.command == null)
			{
				throw new ArgumentException(Res.GetString("Xslt_NotCompiledStylesheet", new object[]
				{
					compiledStylesheet.FullName
				}), "compiledStylesheet");
			}
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000E7978 File Offset: 0x000E5B78
		public void Load(MethodInfo executeMethod, byte[] queryData, Type[] earlyBoundTypes)
		{
			this.Reset();
			if (executeMethod == null)
			{
				throw new ArgumentNullException("executeMethod");
			}
			if (queryData == null)
			{
				throw new ArgumentNullException("queryData");
			}
			if (!XsltConfigSection.EnableMemberAccessForXslCompiledTransform && executeMethod.DeclaringType != null && !executeMethod.DeclaringType.IsVisible)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			DynamicMethod dynamicMethod = executeMethod as DynamicMethod;
			Delegate @delegate = (dynamicMethod != null) ? dynamicMethod.CreateDelegate(typeof(ExecuteDelegate)) : Delegate.CreateDelegate(typeof(ExecuteDelegate), executeMethod);
			this.command = new XmlILCommand((ExecuteDelegate)@delegate, new XmlQueryStaticData(queryData, earlyBoundTypes));
			this.outputSettings = this.command.StaticData.DefaultWriterSettings;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000E7A3B File Offset: 0x000E5C3B
		public void Transform(IXPathNavigable input, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, null, results, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000E7A52 File Offset: 0x000E5C52
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, arguments, results, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000E7A6C File Offset: 0x000E5C6C
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000E7AC0 File Offset: 0x000E5CC0
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000E7B14 File Offset: 0x000E5D14
		public void Transform(XmlReader input, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, null, results, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000E7B2B File Offset: 0x000E5D2B
		public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, arguments, results, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000E7B44 File Offset: 0x000E5D44
		public void Transform(XmlReader input, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000E7B98 File Offset: 0x000E5D98
		public void Transform(XmlReader input, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000E7BEC File Offset: 0x000E5DEC
		public void Transform(string inputUri, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				this.Transform(xmlReader, null, results, XsltConfigSection.CreateDefaultResolver());
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000E7C38 File Offset: 0x000E5E38
		public void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				this.Transform(xmlReader, arguments, results, XsltConfigSection.CreateDefaultResolver());
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000E7C84 File Offset: 0x000E5E84
		public void Transform(string inputUri, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
				{
					this.Transform(xmlReader, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000E7CF8 File Offset: 0x000E5EF8
		public void Transform(string inputUri, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
				{
					this.Transform(xmlReader, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000E7D6C File Offset: 0x000E5F6C
		public void Transform(string inputUri, string resultsFile)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (resultsFile == null)
			{
				throw new ArgumentNullException("resultsFile");
			}
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(resultsFile, this.OutputSettings))
				{
					this.Transform(xmlReader, null, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000E7DF4 File Offset: 0x000E5FF4
		public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.CheckCommand();
			this.command.Execute(input, documentResolver, arguments, results);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000E7E13 File Offset: 0x000E6013
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.CheckCommand();
			this.command.Execute(input.CreateNavigator(), documentResolver, arguments, results);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000E7E37 File Offset: 0x000E6037
		private static void CheckArguments(object input, object results)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000E7E55 File Offset: 0x000E6055
		private static void CheckArguments(string inputUri, object results)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000E7E73 File Offset: 0x000E6073
		private void CheckCommand()
		{
			if (this.command == null)
			{
				throw new InvalidOperationException(Res.GetString("Xslt_NoStylesheetLoaded"));
			}
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000E7E8D File Offset: 0x000E608D
		private QilExpression TestCompile(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.CompileXsltToQil(stylesheet, settings, stylesheetResolver);
			return this.qil;
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000E7EA4 File Offset: 0x000E60A4
		private void TestGenerate(XsltSettings settings)
		{
			this.CompileQilToMsil(settings);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000E7EAD File Offset: 0x000E60AD
		private void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			this.command.Execute(inputUri, documentResolver, arguments, results);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000E7EC0 File Offset: 0x000E60C0
		internal static void PrintQil(object qil, XmlWriter xw, bool printComments, bool printTypes, bool printLineInfo)
		{
			QilExpression node = (QilExpression)qil;
			QilXmlWriter.Options options = QilXmlWriter.Options.None;
			if (printComments)
			{
				options |= QilXmlWriter.Options.Annotations;
			}
			if (printTypes)
			{
				options |= QilXmlWriter.Options.TypeInfo;
			}
			if (printLineInfo)
			{
				options |= QilXmlWriter.Options.LineInfo;
			}
			QilXmlWriter qilXmlWriter = new QilXmlWriter(xw, options);
			qilXmlWriter.ToXml(node);
			xw.Flush();
		}

		// Token: 0x04001323 RID: 4899
		private static readonly XmlReaderSettings ReaderSettings;

		// Token: 0x04001324 RID: 4900
		private static readonly PermissionSet MemberAccessPermissionSet = new PermissionSet(PermissionState.None);

		// Token: 0x04001325 RID: 4901
		private const string Version = "4.0.0.0";

		// Token: 0x04001326 RID: 4902
		private bool enableDebug;

		// Token: 0x04001327 RID: 4903
		private CompilerResults compilerResults;

		// Token: 0x04001328 RID: 4904
		private XmlWriterSettings outputSettings;

		// Token: 0x04001329 RID: 4905
		private QilExpression qil;

		// Token: 0x0400132A RID: 4906
		private XmlILCommand command;

		// Token: 0x0400132B RID: 4907
		private static volatile ConstructorInfo GeneratedCodeCtor;
	}
}

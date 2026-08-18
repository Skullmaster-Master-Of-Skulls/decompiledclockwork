using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld;
using System.Xml.Xsl.XsltOld.Debugger;

namespace System.Xml.Xsl
{
	// Token: 0x020002DE RID: 734
	[Obsolete("This class has been deprecated. Please use System.Xml.Xsl.XslCompiledTransform instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XslTransform
	{
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x000E8423 File Offset: 0x000E6623
		private XmlResolver _DocumentResolver
		{
			get
			{
				if (this.isDocumentResolverSet)
				{
					return this._documentResolver;
				}
				return XsltConfigSection.CreateDefaultResolver();
			}
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000E8439 File Offset: 0x000E6639
		public XslTransform()
		{
		}

		// Token: 0x17000998 RID: 2456
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x000E8441 File Offset: 0x000E6641
		public XmlResolver XmlResolver
		{
			set
			{
				this._documentResolver = value;
				this.isDocumentResolverSet = true;
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000E8451 File Offset: 0x000E6651
		public void Load(XmlReader stylesheet)
		{
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000E845F File Offset: 0x000E665F
		public void Load(XmlReader stylesheet, XmlResolver resolver)
		{
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve), resolver);
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000E846F File Offset: 0x000E666F
		public void Load(IXPathNavigable stylesheet)
		{
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000E847D File Offset: 0x000E667D
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet.CreateNavigator(), resolver);
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000E849A File Offset: 0x000E669A
		public void Load(XPathNavigator stylesheet)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000E84B6 File Offset: 0x000E66B6
		public void Load(XPathNavigator stylesheet, XmlResolver resolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Compile(stylesheet, resolver, null);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000E84D0 File Offset: 0x000E66D0
		public void Load(string url)
		{
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(url);
			Evidence evidence = XmlSecureResolver.CreateEvidenceForUrl(xmlTextReaderImpl.BaseURI);
			this.Compile(Compiler.LoadDocument(xmlTextReaderImpl).CreateNavigator(), XsltConfigSection.CreateDefaultResolver(), evidence);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000E8508 File Offset: 0x000E6708
		public void Load(string url, XmlResolver resolver)
		{
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(url);
			xmlTextReaderImpl.XmlResolver = resolver;
			Evidence evidence = XmlSecureResolver.CreateEvidenceForUrl(xmlTextReaderImpl.BaseURI);
			this.Compile(Compiler.LoadDocument(xmlTextReaderImpl).CreateNavigator(), resolver, evidence);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000E8542 File Offset: 0x000E6742
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet.CreateNavigator(), resolver, evidence);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000E8560 File Offset: 0x000E6760
		public void Load(XmlReader stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve), resolver, evidence);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000E857F File Offset: 0x000E677F
		public void Load(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (evidence == null)
			{
				evidence = new Evidence();
			}
			else
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.Compile(stylesheet, resolver, evidence);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000E85B0 File Offset: 0x000E67B0
		private void CheckCommand()
		{
			if (this._CompiledStylesheet == null)
			{
				throw new InvalidOperationException(Res.GetString("Xslt_NoStylesheetLoaded"));
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000E85CC File Offset: 0x000E67CC
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args, XmlResolver resolver)
		{
			this.CheckCommand();
			Processor processor = new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger);
			return processor.StartReader();
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000E8606 File Offset: 0x000E6806
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args)
		{
			return this.Transform(input, args, this._DocumentResolver);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000E8618 File Offset: 0x000E6818
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			this.CheckCommand();
			Processor processor = new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger);
			processor.Execute(output);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000E8654 File Offset: 0x000E6854
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(input, args, output, this._DocumentResolver);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000E8668 File Offset: 0x000E6868
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			this.CheckCommand();
			Processor processor = new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger);
			processor.Execute(output);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000E86A4 File Offset: 0x000E68A4
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output)
		{
			this.Transform(input, args, output, this._DocumentResolver);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000E86B8 File Offset: 0x000E68B8
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			this.CheckCommand();
			Processor processor = new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger);
			processor.Execute(output);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000E86F4 File Offset: 0x000E68F4
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output)
		{
			this.CheckCommand();
			Processor processor = new Processor(input, args, this._DocumentResolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger);
			processor.Execute(output);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000E8734 File Offset: 0x000E6934
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Transform(input.CreateNavigator(), args, resolver);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000E8752 File Offset: 0x000E6952
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Transform(input.CreateNavigator(), args, this._DocumentResolver);
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000E8775 File Offset: 0x000E6975
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000E8795 File Offset: 0x000E6995
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000E87B9 File Offset: 0x000E69B9
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000E87D9 File Offset: 0x000E69D9
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000E87FD File Offset: 0x000E69FD
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000E881D File Offset: 0x000E6A1D
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000E8844 File Offset: 0x000E6A44
		public void Transform(string inputfile, string outputfile, XmlResolver resolver)
		{
			FileStream fileStream = null;
			try
			{
				XPathDocument input = new XPathDocument(inputfile);
				fileStream = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite);
				this.Transform(input, null, fileStream, resolver);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000E888C File Offset: 0x000E6A8C
		public void Transform(string inputfile, string outputfile)
		{
			this.Transform(inputfile, outputfile, this._DocumentResolver);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000E889C File Offset: 0x000E6A9C
		private void Compile(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			Compiler compiler = (this.Debugger == null) ? new Compiler() : new DbgCompiler(this.Debugger);
			NavigatorInput input = new NavigatorInput(stylesheet);
			compiler.Compile(input, resolver ?? XmlNullResolver.Singleton, evidence);
			this._CompiledStylesheet = compiler.CompiledStylesheet;
			this._QueryStore = compiler.QueryStore;
			this._RootAction = compiler.RootAction;
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000E8901 File Offset: 0x000E6B01
		internal IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000E8909 File Offset: 0x000E6B09
		internal XslTransform(object debugger)
		{
			if (debugger != null)
			{
				this.debugger = new XslTransform.DebuggerAddapter(debugger);
			}
		}

		// Token: 0x04001335 RID: 4917
		private XmlResolver _documentResolver;

		// Token: 0x04001336 RID: 4918
		private bool isDocumentResolverSet;

		// Token: 0x04001337 RID: 4919
		private Stylesheet _CompiledStylesheet;

		// Token: 0x04001338 RID: 4920
		private List<TheQuery> _QueryStore;

		// Token: 0x04001339 RID: 4921
		private RootAction _RootAction;

		// Token: 0x0400133A RID: 4922
		private IXsltDebugger debugger;

		// Token: 0x020004BA RID: 1210
		private class DebuggerAddapter : IXsltDebugger
		{
			// Token: 0x060031A1 RID: 12705 RVA: 0x00120C68 File Offset: 0x0011EE68
			public DebuggerAddapter(object unknownDebugger)
			{
				this.unknownDebugger = unknownDebugger;
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
				Type type = unknownDebugger.GetType();
				this.getBltIn = type.GetMethod("GetBuiltInTemplatesUri", bindingAttr);
				this.onCompile = type.GetMethod("OnInstructionCompile", bindingAttr);
				this.onExecute = type.GetMethod("OnInstructionExecute", bindingAttr);
			}

			// Token: 0x060031A2 RID: 12706 RVA: 0x00120CC2 File Offset: 0x0011EEC2
			public string GetBuiltInTemplatesUri()
			{
				if (this.getBltIn == null)
				{
					return null;
				}
				return (string)this.getBltIn.Invoke(this.unknownDebugger, new object[0]);
			}

			// Token: 0x060031A3 RID: 12707 RVA: 0x00120CF0 File Offset: 0x0011EEF0
			public void OnInstructionCompile(XPathNavigator styleSheetNavigator)
			{
				if (this.onCompile != null)
				{
					this.onCompile.Invoke(this.unknownDebugger, new object[]
					{
						styleSheetNavigator
					});
				}
			}

			// Token: 0x060031A4 RID: 12708 RVA: 0x00120D1C File Offset: 0x0011EF1C
			public void OnInstructionExecute(IXsltProcessor xsltProcessor)
			{
				if (this.onExecute != null)
				{
					this.onExecute.Invoke(this.unknownDebugger, new object[]
					{
						xsltProcessor
					});
				}
			}

			// Token: 0x04001F83 RID: 8067
			private object unknownDebugger;

			// Token: 0x04001F84 RID: 8068
			private MethodInfo getBltIn;

			// Token: 0x04001F85 RID: 8069
			private MethodInfo onCompile;

			// Token: 0x04001F86 RID: 8070
			private MethodInfo onExecute;
		}
	}
}

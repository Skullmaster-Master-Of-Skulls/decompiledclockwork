using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B2 RID: 1202
	internal abstract class QueryMatcher
	{
		// Token: 0x06002DE0 RID: 11744 RVA: 0x000B2FE0 File Offset: 0x000B11E0
		static QueryMatcher()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml("<a/>");
			QueryMatcher.fxCompiler = xmlDocument.CreateNavigator();
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000B301C File Offset: 0x000B121C
		internal QueryMatcher()
		{
			this.maxNodes = int.MaxValue;
			this.query = null;
			this.processorPool = new WeakReference(null);
			this.subExprVars = 0;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000B3049 File Offset: 0x000B1249
		internal bool IsCompiled
		{
			get
			{
				return this.query != null;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000B3054 File Offset: 0x000B1254
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x000B305C File Offset: 0x000B125C
		internal int NodeQuota
		{
			get
			{
				return this.maxNodes;
			}
			set
			{
				this.maxNodes = value;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000B3065 File Offset: 0x000B1265
		internal Opcode RootOpcode
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000B306D File Offset: 0x000B126D
		internal int SubExprVarCount
		{
			get
			{
				return this.subExprVars;
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000B3078 File Offset: 0x000B1278
		internal static OpcodeBlock CompileForExternalEngine(string expression, XmlNamespaceManager namespaces, object item, bool match)
		{
			XPathExpression xpathExpression = QueryMatcher.fxCompiler.Compile(expression);
			if (namespaces != null)
			{
				if (namespaces is XsltContext)
				{
					XPathLexer xpathLexer = new XPathLexer(expression, false);
					while (xpathLexer.MoveNext())
					{
						string prefix = xpathLexer.Token.Prefix;
						if (prefix.Length > 0 && namespaces.LookupNamespace(prefix) == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XsltException(SR.GetString("FilterUndefinedPrefix", new object[]
							{
								prefix
							})));
						}
					}
				}
				xpathExpression.SetContext(namespaces);
			}
			if (XPathResultType.Error == xpathExpression.ReturnType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XPathException(SR.GetString("FilterCouldNotCompile", new object[]
				{
					expression
				})));
			}
			OpcodeBlock result = default(OpcodeBlock);
			SingleFxEngineResultOpcode singleFxEngineResultOpcode;
			if (!match)
			{
				singleFxEngineResultOpcode = new QuerySingleFxEngineResultOpcode();
			}
			else
			{
				singleFxEngineResultOpcode = new MatchSingleFxEngineResultOpcode();
			}
			singleFxEngineResultOpcode.XPath = xpathExpression;
			singleFxEngineResultOpcode.Item = item;
			result.Append(singleFxEngineResultOpcode);
			return result;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000B3158 File Offset: 0x000B1358
		internal static OpcodeBlock CompileForInternalEngine(XPathMessageFilter filter, QueryCompilerFlags flags, IFunctionLibrary[] functionLibs, out ValueDataType returnType)
		{
			return QueryMatcher.CompileForInternalEngine(filter.XPath.Trim(), filter.namespaces, flags, functionLibs, out returnType);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000B3174 File Offset: 0x000B1374
		internal static OpcodeBlock CompileForInternalEngine(string xpath, XmlNamespaceManager nsManager, QueryCompilerFlags flags, IFunctionLibrary[] functionLibs, out ValueDataType returnType)
		{
			returnType = ValueDataType.None;
			OpcodeBlock result;
			if (xpath.Length == 0)
			{
				result = default(OpcodeBlock);
				result.Append(new PushBooleanOpcode(true));
			}
			else
			{
				XPathParser xpathParser = new XPathParser(xpath, nsManager, functionLibs);
				XPathExpr xpathExpr = xpathParser.Parse();
				if (xpathExpr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.CouldNotParseExpression));
				}
				returnType = xpathExpr.ReturnType;
				XPathCompiler xpathCompiler = new XPathCompiler(flags);
				result = xpathCompiler.Compile(xpathExpr);
			}
			return result;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000B31E1 File Offset: 0x000B13E1
		internal static OpcodeBlock CompileForInternalEngine(string xpath, XmlNamespaceManager ns, QueryCompilerFlags flags, out ValueDataType returnType)
		{
			return QueryMatcher.CompileForInternalEngine(xpath, ns, flags, QueryMatcher.defaultFunctionLibs, out returnType);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000B31F4 File Offset: 0x000B13F4
		internal SeekableXPathNavigator CreateMessageNavigator(Message message, bool matchBody)
		{
			SeekableXPathNavigator navigator = message.GetNavigator(matchBody, this.maxNodes);
			navigator.MoveToRoot();
			return navigator;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000B3216 File Offset: 0x000B1416
		internal SeekableXPathNavigator CreateSeekableNavigator(XPathNavigator navigator)
		{
			return new GenericSeekableNavigator(navigator);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000B3220 File Offset: 0x000B1420
		internal SeekableXPathNavigator CreateSafeNavigator(SeekableXPathNavigator navigator)
		{
			INodeCounter nodeCounter = navigator as INodeCounter;
			if (nodeCounter != null)
			{
				nodeCounter.CounterMarker = this.maxNodes;
				nodeCounter.MaxCounter = this.maxNodes;
			}
			else
			{
				navigator = new SafeSeekableNavigator(navigator, this.maxNodes);
			}
			return navigator;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000B3260 File Offset: 0x000B1460
		internal QueryProcessor CreateProcessor()
		{
			QueryProcessor queryProcessor = null;
			WeakReference obj = this.processorPool;
			lock (obj)
			{
				QueryMatcher.QueryProcessorPool queryProcessorPool = this.processorPool.Target as QueryMatcher.QueryProcessorPool;
				if (queryProcessorPool != null)
				{
					queryProcessor = queryProcessorPool.Pop();
				}
			}
			if (queryProcessor != null)
			{
				queryProcessor.ClearProcessor();
			}
			else
			{
				queryProcessor = new QueryProcessor(this);
			}
			queryProcessor.AddRef();
			return queryProcessor;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000B32D0 File Offset: 0x000B14D0
		internal FilterResult Match(MessageBuffer messageBuffer, ICollection<MessageFilter> matches)
		{
			Message message = messageBuffer.CreateMessage();
			FilterResult result;
			try
			{
				result = this.Match(message, true, matches);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000B3308 File Offset: 0x000B1508
		internal FilterResult Match(Message message, bool matchBody, ICollection<MessageFilter> matches)
		{
			QueryProcessor queryProcessor = this.CreateProcessor();
			queryProcessor.MatchSet = matches;
			queryProcessor.EnsureFilterCollection();
			try
			{
				queryProcessor.Eval(this.query, message, matchBody);
			}
			catch (XPathNavigatorException ex)
			{
				throw TraceUtility.ThrowHelperError(ex.Process(this.query), message);
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw TraceUtility.ThrowHelperError(ex2.Process(this.query), message);
			}
			return new FilterResult(queryProcessor);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000B3384 File Offset: 0x000B1584
		internal QueryResult<TResult> Evaluate<TResult>(MessageBuffer messageBuffer)
		{
			Message message = messageBuffer.CreateMessage();
			return this.Evaluate<TResult>(message, true);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B33A0 File Offset: 0x000B15A0
		internal QueryResult<TResult> Evaluate<TResult>(Message message, bool matchBody)
		{
			return new QueryResult<TResult>(this, message, matchBody);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000B33AC File Offset: 0x000B15AC
		internal FilterResult Match(SeekableXPathNavigator navigator, ICollection<MessageFilter> matches)
		{
			if (this.maxNodes < 2147483647)
			{
				navigator = this.CreateSafeNavigator(navigator);
			}
			QueryProcessor queryProcessor = this.CreateProcessor();
			queryProcessor.MatchSet = matches;
			queryProcessor.EnsureFilterCollection();
			try
			{
				queryProcessor.Eval(this.query, navigator);
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(this.query));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(this.query));
			}
			return new FilterResult(queryProcessor);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000B3444 File Offset: 0x000B1644
		internal FilterResult Match(XPathNavigator navigator, ICollection<MessageFilter> matches)
		{
			SeekableXPathNavigator navigator2 = this.CreateSeekableNavigator(navigator);
			return this.Match(navigator2, matches);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000B3464 File Offset: 0x000B1664
		internal void ReleaseProcessor(QueryProcessor processor)
		{
			if (!processor.ReleaseRef())
			{
				return;
			}
			WeakReference obj = this.processorPool;
			lock (obj)
			{
				QueryMatcher.QueryProcessorPool queryProcessorPool = this.processorPool.Target as QueryMatcher.QueryProcessorPool;
				if (queryProcessorPool == null)
				{
					queryProcessorPool = new QueryMatcher.QueryProcessorPool();
					this.processorPool.Target = queryProcessorPool;
				}
				queryProcessorPool.Push(processor);
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000B34D4 File Offset: 0x000B16D4
		internal void ReleaseResult(FilterResult result)
		{
			if (result.Processor != null)
			{
				result.Processor.MatchSet = null;
				this.ReleaseProcessor(result.Processor);
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000B34F9 File Offset: 0x000B16F9
		internal virtual void Trim()
		{
			if (this.query != null)
			{
				this.query.Trim();
			}
		}

		// Token: 0x040024EF RID: 9455
		private static IFunctionLibrary[] defaultFunctionLibs = new IFunctionLibrary[]
		{
			new XPathFunctionLibrary()
		};

		// Token: 0x040024F0 RID: 9456
		private static XPathNavigator fxCompiler;

		// Token: 0x040024F1 RID: 9457
		protected int maxNodes;

		// Token: 0x040024F2 RID: 9458
		protected Opcode query;

		// Token: 0x040024F3 RID: 9459
		protected int subExprVars;

		// Token: 0x040024F4 RID: 9460
		protected WeakReference processorPool;

		// Token: 0x02000C48 RID: 3144
		internal class QueryProcessorPool
		{
			// Token: 0x06007770 RID: 30576 RVA: 0x001BDF60 File Offset: 0x001BC160
			internal QueryProcessorPool()
			{
			}

			// Token: 0x06007771 RID: 30577 RVA: 0x001BDF68 File Offset: 0x001BC168
			internal QueryProcessor Pop()
			{
				QueryProcessor queryProcessor = this.processor;
				if (queryProcessor != null)
				{
					this.processor = (QueryProcessor)queryProcessor.next;
					queryProcessor.next = null;
					return queryProcessor;
				}
				return null;
			}

			// Token: 0x06007772 RID: 30578 RVA: 0x001BDF9A File Offset: 0x001BC19A
			internal void Push(QueryProcessor p)
			{
				p.next = this.processor;
				this.processor = p;
			}

			// Token: 0x04004450 RID: 17488
			private QueryProcessor processor;
		}
	}
}

using System;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B4 RID: 1204
	internal class XPathQueryMatcher : QueryMatcher
	{
		// Token: 0x06002DF8 RID: 11768 RVA: 0x000B3510 File Offset: 0x000B1710
		static XPathQueryMatcher()
		{
			ValueDataType valueDataType;
			XPathQueryMatcher.rootFilter = QueryMatcher.CompileForInternalEngine("/", null, QueryCompilerFlags.None, out valueDataType);
			XPathQueryMatcher.rootFilter.Append(new MatchResultOpcode());
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000B354A File Offset: 0x000B174A
		internal XPathQueryMatcher(bool match)
		{
			this.flags = XPathFilterFlags.None;
			this.match = match;
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000B3560 File Offset: 0x000B1760
		internal bool IsAlwaysMatch
		{
			get
			{
				return (this.flags & XPathFilterFlags.AlwaysMatch) > XPathFilterFlags.None;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x000B356D File Offset: 0x000B176D
		internal bool IsFxFilter
		{
			get
			{
				return (this.flags & XPathFilterFlags.IsFxFilter) > XPathFilterFlags.None;
			}
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000B357C File Offset: 0x000B177C
		internal void Compile(string expression, XmlNamespaceManager namespaces)
		{
			if (this.query == null)
			{
				try
				{
					this.CompileForInternal(expression, namespaces);
				}
				catch (QueryCompileException)
				{
				}
				if (this.query == null)
				{
					this.CompileForExternal(expression, namespaces);
				}
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000B35C0 File Offset: 0x000B17C0
		internal void CompileForExternal(string xpath, XmlNamespaceManager names)
		{
			Opcode first = QueryMatcher.CompileForExternalEngine(xpath, names, null, this.match).First;
			this.query = first;
			this.flags |= XPathFilterFlags.IsFxFilter;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000B35FC File Offset: 0x000B17FC
		internal void CompileForInternal(string xpath, XmlNamespaceManager names)
		{
			this.query = null;
			xpath = xpath.Trim();
			if (xpath.Length == 0)
			{
				this.query = XPathQueryMatcher.matchAlwaysFilter;
				this.flags |= XPathFilterFlags.AlwaysMatch;
			}
			else if (1 == xpath.Length && '/' == xpath[0])
			{
				this.query = XPathQueryMatcher.rootFilter.First;
				this.flags |= XPathFilterFlags.AlwaysMatch;
			}
			else
			{
				ValueDataType valueDataType;
				OpcodeBlock opcodeBlock = QueryMatcher.CompileForInternalEngine(xpath, names, QueryCompilerFlags.None, out valueDataType);
				if (this.match)
				{
					opcodeBlock.Append(new MatchResultOpcode());
				}
				else
				{
					opcodeBlock.Append(new QueryResultOpcode());
				}
				this.query = opcodeBlock.First;
			}
			this.flags &= (XPathFilterFlags)(-3);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000B36B8 File Offset: 0x000B18B8
		internal FilterResult Match(MessageBuffer messageBuffer)
		{
			Message message = messageBuffer.CreateMessage();
			FilterResult result;
			try
			{
				result = this.Match(message, true);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000B36F0 File Offset: 0x000B18F0
		internal FilterResult Match(Message message, bool matchBody)
		{
			if (this.IsAlwaysMatch)
			{
				return new FilterResult(true);
			}
			return base.Match(message, matchBody, null);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000B370A File Offset: 0x000B190A
		internal FilterResult Match(SeekableXPathNavigator navigator)
		{
			if (this.IsAlwaysMatch)
			{
				return new FilterResult(true);
			}
			if (this.IsFxFilter)
			{
				return new FilterResult(this.MatchFx(navigator));
			}
			return base.Match(navigator, null);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000B3738 File Offset: 0x000B1938
		internal FilterResult Match(XPathNavigator navigator)
		{
			if (this.IsAlwaysMatch)
			{
				return new FilterResult(true);
			}
			if (this.IsFxFilter)
			{
				return new FilterResult(this.MatchFx(navigator));
			}
			return base.Match(navigator, null);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000B3768 File Offset: 0x000B1968
		internal bool MatchFx(XPathNavigator navigator)
		{
			INodeCounter nodeCounter = navigator as INodeCounter;
			if (nodeCounter == null)
			{
				navigator = new SafeSeekableNavigator(new GenericSeekableNavigator(navigator), base.NodeQuota);
			}
			else
			{
				nodeCounter.CounterMarker = base.NodeQuota;
				nodeCounter.MaxCounter = base.NodeQuota;
			}
			bool result;
			try
			{
				result = ((MatchSingleFxEngineResultOpcode)this.query).Match(navigator);
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(this.query));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(this.query));
			}
			return result;
		}

		// Token: 0x040024F9 RID: 9465
		private XPathFilterFlags flags;

		// Token: 0x040024FA RID: 9466
		private bool match;

		// Token: 0x040024FB RID: 9467
		private static PushBooleanOpcode matchAlwaysFilter = new PushBooleanOpcode(true);

		// Token: 0x040024FC RID: 9468
		private static OpcodeBlock rootFilter;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004DF RID: 1247
	internal class QueryProcessor : ProcessingContext
	{
		// Token: 0x06002F5F RID: 12127 RVA: 0x000B67B0 File Offset: 0x000B49B0
		internal QueryProcessor(QueryMatcher matcher)
		{
			base.Processor = this;
			this.matcher = matcher;
			this.flags = QueryProcessingFlags.Match;
			this.messageAction = null;
			this.messageId = null;
			this.messageSoapUri = null;
			this.messageTo = null;
			if (matcher.SubExprVarCount > 0)
			{
				this.subExprVars = new QueryProcessor.SubExprVariable[matcher.SubExprVarCount];
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000B680E File Offset: 0x000B4A0E
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x000B6816 File Offset: 0x000B4A16
		internal string Action
		{
			get
			{
				return this.messageAction;
			}
			set
			{
				this.messageAction = value;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x000B6820 File Offset: 0x000B4A20
		// (set) Token: 0x06002F63 RID: 12131 RVA: 0x000B6897 File Offset: 0x000B4A97
		internal SeekableXPathNavigator ContextNode
		{
			get
			{
				if (this.contextNode == null)
				{
					if (this.message == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected));
					}
					this.contextNode = this.matcher.CreateMessageNavigator(this.message, this.matchMessageBody);
					this.counter = (this.contextNode as INodeCounter);
					if (this.counter == null)
					{
						this.counter = DummyNodeCounter.Dummy;
					}
				}
				return this.contextNode;
			}
			set
			{
				this.contextNode = value;
				this.counter = (value as INodeCounter);
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000B68AC File Offset: 0x000B4AAC
		// (set) Token: 0x06002F65 RID: 12133 RVA: 0x000B68B4 File Offset: 0x000B4AB4
		internal Message ContextMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
				if (value != null)
				{
					this.flags |= QueryProcessingFlags.Message;
					return;
				}
				this.flags &= (QueryProcessingFlags)253;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000B68E1 File Offset: 0x000B4AE1
		// (set) Token: 0x06002F67 RID: 12135 RVA: 0x000B691A File Offset: 0x000B4B1A
		internal int CounterMarker
		{
			get
			{
				if (this.counter == null)
				{
					this.counter = (this.ContextNode as INodeCounter);
					if (this.counter == null)
					{
						this.counter = DummyNodeCounter.Dummy;
					}
				}
				return this.counter.CounterMarker;
			}
			set
			{
				this.counter.CounterMarker = value;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (set) Token: 0x06002F68 RID: 12136 RVA: 0x000B6928 File Offset: 0x000B4B28
		internal bool MatchBody
		{
			set
			{
				this.matchMessageBody = value;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x000B6931 File Offset: 0x000B4B31
		internal QueryMatcher Matcher
		{
			get
			{
				return this.matcher;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x000B6939 File Offset: 0x000B4B39
		// (set) Token: 0x06002F6B RID: 12139 RVA: 0x000B6941 File Offset: 0x000B4B41
		internal ICollection<KeyValuePair<MessageQuery, XPathResult>> ResultSet
		{
			get
			{
				return this.resultSet;
			}
			set
			{
				this.resultSet = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000B694A File Offset: 0x000B4B4A
		// (set) Token: 0x06002F6D RID: 12141 RVA: 0x000B6952 File Offset: 0x000B4B52
		internal string MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000B695B File Offset: 0x000B4B5B
		// (set) Token: 0x06002F6F RID: 12143 RVA: 0x000B6963 File Offset: 0x000B4B63
		internal bool Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000B696C File Offset: 0x000B4B6C
		// (set) Token: 0x06002F71 RID: 12145 RVA: 0x000B6974 File Offset: 0x000B4B74
		internal XPathResult QueryResult
		{
			get
			{
				return this.queryResult;
			}
			set
			{
				this.queryResult = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000B697D File Offset: 0x000B4B7D
		internal Collection<MessageFilter> MatchList
		{
			get
			{
				return this.matchList;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000B6985 File Offset: 0x000B4B85
		// (set) Token: 0x06002F74 RID: 12148 RVA: 0x000B698D File Offset: 0x000B4B8D
		internal ICollection<MessageFilter> MatchSet
		{
			get
			{
				return this.matchSet;
			}
			set
			{
				this.matchSet = value;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x000B6996 File Offset: 0x000B4B96
		// (set) Token: 0x06002F76 RID: 12150 RVA: 0x000B699E File Offset: 0x000B4B9E
		internal string SoapUri
		{
			get
			{
				return this.messageSoapUri;
			}
			set
			{
				this.messageSoapUri = value;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000B69A7 File Offset: 0x000B4BA7
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000B69AF File Offset: 0x000B4BAF
		internal string ToHeader
		{
			get
			{
				return this.messageTo;
			}
			set
			{
				this.messageTo = value;
			}
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000B69B8 File Offset: 0x000B4BB8
		internal void AddRef()
		{
			Interlocked.Increment(ref this.refCount);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000B69C8 File Offset: 0x000B4BC8
		internal void ClearProcessor()
		{
			base.ClearContext();
			this.flags = QueryProcessingFlags.Match;
			this.messageAction = null;
			this.messageId = null;
			this.messageSoapUri = null;
			this.messageTo = null;
			int subExprVarCount = this.matcher.SubExprVarCount;
			if (subExprVarCount == 0)
			{
				this.subExprVars = null;
				return;
			}
			QueryProcessor.SubExprVariable[] array = this.subExprVars;
			if (array == null)
			{
				this.subExprVars = new QueryProcessor.SubExprVariable[subExprVarCount];
				return;
			}
			int num = array.Length;
			if (num != subExprVarCount)
			{
				this.subExprVars = new QueryProcessor.SubExprVariable[subExprVarCount];
				return;
			}
			if (num == 1)
			{
				NodeSequence seq = array[0].seq;
				if (seq != null)
				{
					this.ReleaseSequenceToPool(seq);
				}
				return;
			}
			for (int i = 0; i < num; i++)
			{
				NodeSequence seq2 = array[i].seq;
				if (seq2 != null && seq2.refCount > 0)
				{
					this.ReleaseSequenceToPool(seq2);
				}
			}
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000B6A9C File Offset: 0x000B4C9C
		internal ProcessingContext CloneContext(ProcessingContext srcContext)
		{
			ProcessingContext processingContext = this.PopContext();
			if (processingContext == null)
			{
				processingContext = new ProcessingContext();
			}
			processingContext.CopyFrom(srcContext);
			return processingContext;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000B6AC4 File Offset: 0x000B4CC4
		internal QueryBranchResultSet CreateResultSet()
		{
			QueryBranchResultSet queryBranchResultSet = this.PopResultSet();
			if (queryBranchResultSet == null)
			{
				queryBranchResultSet = new QueryBranchResultSet();
			}
			else
			{
				queryBranchResultSet.Clear();
			}
			return queryBranchResultSet;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000B6AEA File Offset: 0x000B4CEA
		internal int ElapsedCount(int marker)
		{
			return this.counter.ElapsedCount(marker);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000B6AF8 File Offset: 0x000B4CF8
		internal void EnsureFilterCollection()
		{
			this.resultSet = null;
			if (this.matchSet == null)
			{
				if (this.matchList == null)
				{
					this.matchList = new Collection<MessageFilter>();
				}
				else
				{
					this.matchList.Clear();
				}
				this.matchSet = this.matchList;
			}
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000B6B38 File Offset: 0x000B4D38
		internal void Eval(Opcode block)
		{
			Opcode opcode = block;
			try
			{
				while (opcode != null)
				{
					opcode = opcode.Eval(this);
				}
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(opcode));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(opcode));
			}
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000B6B98 File Offset: 0x000B4D98
		internal void Eval(Opcode block, ProcessingContext context)
		{
			Opcode opcode = block;
			try
			{
				while (opcode != null)
				{
					opcode = opcode.Eval(context);
				}
			}
			catch (XPathNavigatorException ex)
			{
				throw TraceUtility.ThrowHelperError(ex.Process(opcode), this.message);
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw TraceUtility.ThrowHelperError(ex2.Process(opcode), this.message);
			}
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000B6BFC File Offset: 0x000B4DFC
		internal void Eval(Opcode block, Message message, bool matchBody)
		{
			this.result = false;
			this.ContextNode = null;
			this.ContextMessage = message;
			this.MatchBody = matchBody;
			this.Eval(block);
			this.message = null;
			this.contextNode = null;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000B6C2F File Offset: 0x000B4E2F
		internal void Eval(Opcode block, SeekableXPathNavigator navigator)
		{
			this.result = false;
			this.ContextNode = navigator;
			this.ContextMessage = null;
			this.Eval(block);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000B6C50 File Offset: 0x000B4E50
		internal bool LoadVariable(ProcessingContext context, int var)
		{
			if (this.subExprVars[var].seq == null)
			{
				return false;
			}
			int iterationCount = context.IterationCount;
			this.counter.IncreaseBy(iterationCount * this.subExprVars[var].count);
			NodeSequence seq = this.subExprVars[var].seq;
			context.PushSequenceFrame();
			for (int i = 0; i < iterationCount; i++)
			{
				seq.refCount++;
				context.PushSequence(seq);
			}
			return true;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000B6CD4 File Offset: 0x000B4ED4
		internal ProcessingContext PopContext()
		{
			ProcessingContext processingContext = this.contextPool;
			if (processingContext != null)
			{
				this.contextPool = processingContext.Next;
				processingContext.Next = null;
			}
			return processingContext;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000B6D00 File Offset: 0x000B4F00
		internal NodeSequence PopSequence()
		{
			NodeSequence nodeSequence = this.sequencePool;
			if (nodeSequence != null)
			{
				this.sequencePool = nodeSequence.Next;
				nodeSequence.Next = null;
			}
			return nodeSequence;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000B6D2C File Offset: 0x000B4F2C
		internal QueryBranchResultSet PopResultSet()
		{
			QueryBranchResultSet queryBranchResultSet = this.resultPool;
			if (queryBranchResultSet != null)
			{
				this.resultPool = queryBranchResultSet.Next;
				queryBranchResultSet.Next = null;
			}
			return queryBranchResultSet;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000B6D57 File Offset: 0x000B4F57
		internal void PushContext(ProcessingContext context)
		{
			context.Next = this.contextPool;
			this.contextPool = context;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000B6D6C File Offset: 0x000B4F6C
		internal void PushResultSet(QueryBranchResultSet resultSet)
		{
			resultSet.Next = this.resultPool;
			this.resultPool = resultSet;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000B6D81 File Offset: 0x000B4F81
		internal bool ReleaseRef()
		{
			return Interlocked.Decrement(ref this.refCount) == 0;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000B6D91 File Offset: 0x000B4F91
		internal void ReleaseContext(ProcessingContext context)
		{
			this.PushContext(context);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000B6D9A File Offset: 0x000B4F9A
		internal void ReleaseResults(QueryBranchResultSet resultSet)
		{
			this.PushResultSet(resultSet);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000B6DA3 File Offset: 0x000B4FA3
		internal void ReleaseSequenceToPool(NodeSequence sequence)
		{
			if (NodeSequence.Empty != sequence)
			{
				sequence.Reset(this.sequencePool);
				this.sequencePool = sequence;
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000B6DC0 File Offset: 0x000B4FC0
		internal void SaveVariable(ProcessingContext context, int var, int count)
		{
			NodeSequence nodeSequence = context.Sequences[context.TopSequenceArg.basePtr].Sequence;
			if (nodeSequence == null)
			{
				nodeSequence = base.CreateSequence();
			}
			nodeSequence.OwnerContext = null;
			this.subExprVars[var].seq = nodeSequence;
			this.subExprVars[var].count = count;
		}

		// Token: 0x040025C7 RID: 9671
		private SeekableXPathNavigator contextNode;

		// Token: 0x040025C8 RID: 9672
		private ProcessingContext contextPool;

		// Token: 0x040025C9 RID: 9673
		private INodeCounter counter;

		// Token: 0x040025CA RID: 9674
		private QueryProcessingFlags flags;

		// Token: 0x040025CB RID: 9675
		private QueryMatcher matcher;

		// Token: 0x040025CC RID: 9676
		private Message message;

		// Token: 0x040025CD RID: 9677
		private bool matchMessageBody;

		// Token: 0x040025CE RID: 9678
		private int refCount;

		// Token: 0x040025CF RID: 9679
		private bool result;

		// Token: 0x040025D0 RID: 9680
		private XPathResult queryResult;

		// Token: 0x040025D1 RID: 9681
		private QueryBranchResultSet resultPool;

		// Token: 0x040025D2 RID: 9682
		private Collection<MessageFilter> matchList;

		// Token: 0x040025D3 RID: 9683
		private ICollection<MessageFilter> matchSet;

		// Token: 0x040025D4 RID: 9684
		private ICollection<KeyValuePair<MessageQuery, XPathResult>> resultSet;

		// Token: 0x040025D5 RID: 9685
		private NodeSequence sequencePool;

		// Token: 0x040025D6 RID: 9686
		private QueryProcessor.SubExprVariable[] subExprVars;

		// Token: 0x040025D7 RID: 9687
		private string messageAction;

		// Token: 0x040025D8 RID: 9688
		private string messageId;

		// Token: 0x040025D9 RID: 9689
		private string messageSoapUri;

		// Token: 0x040025DA RID: 9690
		private string messageTo;

		// Token: 0x02000C49 RID: 3145
		private struct SubExprVariable
		{
			// Token: 0x04004451 RID: 17489
			internal NodeSequence seq;

			// Token: 0x04004452 RID: 17490
			internal int count;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Antlr.Runtime.Debug;

namespace Antlr.Runtime
{
	// Token: 0x0200000E RID: 14
	public abstract class BaseRecognizer
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000025CB File Offset: 0x000007CB
		public BaseRecognizer() : this(new RecognizerSharedState())
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000025D8 File Offset: 0x000007D8
		public BaseRecognizer(RecognizerSharedState state)
		{
			if (state == null)
			{
				state = new RecognizerSharedState();
			}
			this.state = state;
			this.InitDFAs();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000025F7 File Offset: 0x000007F7
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000025FF File Offset: 0x000007FF
		public TextWriter TraceDestination { get; set; }

		// Token: 0x0600004B RID: 75 RVA: 0x00002608 File Offset: 0x00000808
		public virtual void SetState(RecognizerSharedState value)
		{
			this.state = value;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002611 File Offset: 0x00000811
		protected virtual void InitDFAs()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002614 File Offset: 0x00000814
		public virtual void Reset()
		{
			if (this.state == null)
			{
				return;
			}
			this.state._fsp = -1;
			this.state.errorRecovery = false;
			this.state.lastErrorIndex = -1;
			this.state.failed = false;
			this.state.syntaxErrors = 0;
			this.state.backtracking = 0;
			int num = 0;
			while (this.state.ruleMemo != null && num < this.state.ruleMemo.Length)
			{
				this.state.ruleMemo[num] = null;
				num++;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026A8 File Offset: 0x000008A8
		public virtual object Match(IIntStream input, int ttype, BitSet follow)
		{
			object currentInputSymbol = this.GetCurrentInputSymbol(input);
			if (input.LA(1) == ttype)
			{
				input.Consume();
				this.state.errorRecovery = false;
				this.state.failed = false;
				return currentInputSymbol;
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return currentInputSymbol;
			}
			return this.RecoverFromMismatchedToken(input, ttype, follow);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000270E File Offset: 0x0000090E
		public virtual void MatchAny(IIntStream input)
		{
			this.state.errorRecovery = false;
			this.state.failed = false;
			input.Consume();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000272E File Offset: 0x0000092E
		public virtual bool MismatchIsUnwantedToken(IIntStream input, int ttype)
		{
			return input.LA(2) == ttype;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000273C File Offset: 0x0000093C
		public virtual bool MismatchIsMissingToken(IIntStream input, BitSet follow)
		{
			if (follow == null)
			{
				return false;
			}
			if (follow.Member(1))
			{
				BitSet a = this.ComputeContextSensitiveRuleFOLLOW();
				follow = follow.Or(a);
				if (this.state._fsp >= 0)
				{
					follow.Remove(1);
				}
			}
			return follow.Member(input.LA(1)) || follow.Member(1);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002797 File Offset: 0x00000997
		public virtual void ReportError(RecognitionException e)
		{
			if (this.state.errorRecovery)
			{
				return;
			}
			this.state.syntaxErrors++;
			this.state.errorRecovery = true;
			this.DisplayRecognitionError(this.TokenNames, e);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000027D4 File Offset: 0x000009D4
		public virtual void DisplayRecognitionError(string[] tokenNames, RecognitionException e)
		{
			string errorHeader = this.GetErrorHeader(e);
			string errorMessage = this.GetErrorMessage(e, tokenNames);
			this.EmitErrorMessage(errorHeader + " " + errorMessage);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002804 File Offset: 0x00000A04
		public virtual string GetErrorMessage(RecognitionException e, string[] tokenNames)
		{
			string result = e.Message;
			if (e is UnwantedTokenException)
			{
				UnwantedTokenException ex = (UnwantedTokenException)e;
				string str;
				if (ex.Expecting == -1)
				{
					str = "EndOfFile";
				}
				else
				{
					str = tokenNames[ex.Expecting];
				}
				result = "extraneous input " + this.GetTokenErrorDisplay(ex.UnexpectedToken) + " expecting " + str;
			}
			else if (e is MissingTokenException)
			{
				MissingTokenException ex2 = (MissingTokenException)e;
				string str2;
				if (ex2.Expecting == -1)
				{
					str2 = "EndOfFile";
				}
				else
				{
					str2 = tokenNames[ex2.Expecting];
				}
				result = "missing " + str2 + " at " + this.GetTokenErrorDisplay(e.Token);
			}
			else if (e is MismatchedTokenException)
			{
				MismatchedTokenException ex3 = (MismatchedTokenException)e;
				string str3;
				if (ex3.Expecting == -1)
				{
					str3 = "EndOfFile";
				}
				else
				{
					str3 = tokenNames[ex3.Expecting];
				}
				result = "mismatched input " + this.GetTokenErrorDisplay(e.Token) + " expecting " + str3;
			}
			else if (e is MismatchedTreeNodeException)
			{
				MismatchedTreeNodeException ex4 = (MismatchedTreeNodeException)e;
				string str4;
				if (ex4.Expecting == -1)
				{
					str4 = "EndOfFile";
				}
				else
				{
					str4 = tokenNames[ex4.Expecting];
				}
				string str5 = (ex4.Node != null) ? (ex4.Node.ToString() ?? string.Empty) : string.Empty;
				result = "mismatched tree node: " + str5 + " expecting " + str4;
			}
			else if (e is NoViableAltException)
			{
				result = "no viable alternative at input " + this.GetTokenErrorDisplay(e.Token);
			}
			else if (e is EarlyExitException)
			{
				result = "required (...)+ loop did not match anything at input " + this.GetTokenErrorDisplay(e.Token);
			}
			else if (e is MismatchedSetException)
			{
				MismatchedSetException ex5 = (MismatchedSetException)e;
				result = string.Concat(new object[]
				{
					"mismatched input ",
					this.GetTokenErrorDisplay(e.Token),
					" expecting set ",
					ex5.Expecting
				});
			}
			else if (e is MismatchedNotSetException)
			{
				MismatchedNotSetException ex6 = (MismatchedNotSetException)e;
				result = string.Concat(new object[]
				{
					"mismatched input ",
					this.GetTokenErrorDisplay(e.Token),
					" expecting set ",
					ex6.Expecting
				});
			}
			else if (e is FailedPredicateException)
			{
				FailedPredicateException ex7 = (FailedPredicateException)e;
				result = string.Concat(new string[]
				{
					"rule ",
					ex7.RuleName,
					" failed predicate: {",
					ex7.PredicateText,
					"}?"
				});
			}
			return result;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public virtual int NumberOfSyntaxErrors
		{
			get
			{
				return this.state.syntaxErrors;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public virtual string GetErrorHeader(RecognitionException e)
		{
			string text = this.SourceName ?? string.Empty;
			if (text.Length > 0)
			{
				text += ' ';
			}
			return string.Format("{0}line {1}:{2}", text, e.Line, e.CharPositionInLine + 1);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B2C File Offset: 0x00000D2C
		public virtual string GetTokenErrorDisplay(IToken t)
		{
			string text = t.Text;
			if (text == null)
			{
				if (t.Type == -1)
				{
					text = "<EOF>";
				}
				else
				{
					text = "<" + t.Type + ">";
				}
			}
			text = Regex.Replace(text, "\n", "\\\\n");
			text = Regex.Replace(text, "\r", "\\\\r");
			text = Regex.Replace(text, "\t", "\\\\t");
			return "'" + text + "'";
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BB2 File Offset: 0x00000DB2
		public virtual void EmitErrorMessage(string msg)
		{
			if (this.TraceDestination != null)
			{
				this.TraceDestination.WriteLine(msg);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public virtual void Recover(IIntStream input, RecognitionException re)
		{
			if (this.state.lastErrorIndex == input.Index)
			{
				input.Consume();
			}
			this.state.lastErrorIndex = input.Index;
			BitSet set = this.ComputeErrorRecoverySet();
			this.BeginResync();
			this.ConsumeUntil(input, set);
			this.EndResync();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C1A File Offset: 0x00000E1A
		public virtual void BeginResync()
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C1C File Offset: 0x00000E1C
		public virtual void EndResync()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C1E File Offset: 0x00000E1E
		protected virtual BitSet ComputeErrorRecoverySet()
		{
			return this.CombineFollows(false);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C27 File Offset: 0x00000E27
		protected virtual BitSet ComputeContextSensitiveRuleFOLLOW()
		{
			return this.CombineFollows(true);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C30 File Offset: 0x00000E30
		protected virtual BitSet CombineFollows(bool exact)
		{
			int fsp = this.state._fsp;
			BitSet bitSet = new BitSet();
			for (int i = fsp; i >= 0; i--)
			{
				BitSet bitSet2 = this.state.following[i];
				bitSet.OrInPlace(bitSet2);
				if (exact)
				{
					if (!bitSet2.Member(1))
					{
						break;
					}
					if (i > 0)
					{
						bitSet.Remove(1);
					}
				}
			}
			return bitSet;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C88 File Offset: 0x00000E88
		protected virtual object RecoverFromMismatchedToken(IIntStream input, int ttype, BitSet follow)
		{
			RecognitionException ex = null;
			if (this.MismatchIsUnwantedToken(input, ttype))
			{
				ex = new UnwantedTokenException(ttype, input, this.TokenNames);
				this.BeginResync();
				input.Consume();
				this.EndResync();
				this.ReportError(ex);
				object currentInputSymbol = this.GetCurrentInputSymbol(input);
				input.Consume();
				return currentInputSymbol;
			}
			if (this.MismatchIsMissingToken(input, follow))
			{
				object missingSymbol = this.GetMissingSymbol(input, ex, ttype, follow);
				ex = new MissingTokenException(ttype, input, missingSymbol);
				this.ReportError(ex);
				return missingSymbol;
			}
			ex = new MismatchedTokenException(ttype, input, this.TokenNames);
			throw ex;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D0E File Offset: 0x00000F0E
		public virtual object RecoverFromMismatchedSet(IIntStream input, RecognitionException e, BitSet follow)
		{
			if (this.MismatchIsMissingToken(input, follow))
			{
				this.ReportError(e);
				return this.GetMissingSymbol(input, e, 0, follow);
			}
			throw e;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D2D File Offset: 0x00000F2D
		protected virtual object GetCurrentInputSymbol(IIntStream input)
		{
			return null;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D30 File Offset: 0x00000F30
		protected virtual object GetMissingSymbol(IIntStream input, RecognitionException e, int expectedTokenType, BitSet follow)
		{
			return null;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D34 File Offset: 0x00000F34
		public virtual void ConsumeUntil(IIntStream input, int tokenType)
		{
			int num = input.LA(1);
			while (num != -1 && num != tokenType)
			{
				input.Consume();
				num = input.LA(1);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D64 File Offset: 0x00000F64
		public virtual void ConsumeUntil(IIntStream input, BitSet set)
		{
			int num = input.LA(1);
			while (num != -1 && !set.Member(num))
			{
				input.Consume();
				num = input.LA(1);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D98 File Offset: 0x00000F98
		protected void PushFollow(BitSet fset)
		{
			if (this.state._fsp + 1 >= this.state.following.Length)
			{
				Array.Resize<BitSet>(ref this.state.following, this.state.following.Length * 2);
			}
			this.state.following[++this.state._fsp] = fset;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E03 File Offset: 0x00001003
		protected void PopFollow()
		{
			this.state._fsp--;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E18 File Offset: 0x00001018
		public virtual IList<string> GetRuleInvocationStack()
		{
			return BaseRecognizer.GetRuleInvocationStack(new StackTrace(true));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E28 File Offset: 0x00001028
		public static IList<string> GetRuleInvocationStack(StackTrace trace)
		{
			if (trace == null)
			{
				throw new ArgumentNullException("trace");
			}
			List<string> list = new List<string>();
			StackFrame[] array = trace.GetFrames() ?? new StackFrame[0];
			for (int i = array.Length - 1; i >= 0; i--)
			{
				StackFrame stackFrame = array[i];
				MethodBase method = stackFrame.GetMethod();
				GrammarRuleAttribute[] array2 = (GrammarRuleAttribute[])method.GetCustomAttributes(typeof(GrammarRuleAttribute), true);
				if (array2 != null && array2.Length > 0)
				{
					list.Add(array2[0].Name);
				}
			}
			return list;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002EAA File Offset: 0x000010AA
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002EB7 File Offset: 0x000010B7
		public virtual int BacktrackingLevel
		{
			get
			{
				return this.state.backtracking;
			}
			set
			{
				this.state.backtracking = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002EC5 File Offset: 0x000010C5
		public virtual bool Failed
		{
			get
			{
				return this.state.failed;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002ED2 File Offset: 0x000010D2
		public virtual string[] TokenNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002ED5 File Offset: 0x000010D5
		public virtual string GrammarFileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110
		public abstract string SourceName { get; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002ED8 File Offset: 0x000010D8
		public virtual List<string> ToStrings(ICollection<IToken> tokens)
		{
			if (tokens == null)
			{
				return null;
			}
			List<string> list = new List<string>(tokens.Count);
			foreach (IToken token in tokens)
			{
				list.Add(token.Text);
			}
			return list;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F38 File Offset: 0x00001138
		public virtual int GetRuleMemoization(int ruleIndex, int ruleStartIndex)
		{
			if (this.state.ruleMemo[ruleIndex] == null)
			{
				this.state.ruleMemo[ruleIndex] = new Dictionary<int, int>();
			}
			int result;
			if (!this.state.ruleMemo[ruleIndex].TryGetValue(ruleStartIndex, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F80 File Offset: 0x00001180
		public virtual bool AlreadyParsedRule(IIntStream input, int ruleIndex)
		{
			int ruleMemoization = this.GetRuleMemoization(ruleIndex, input.Index);
			if (ruleMemoization == -1)
			{
				return false;
			}
			if (ruleMemoization == -2)
			{
				this.state.failed = true;
			}
			else
			{
				input.Seek(ruleMemoization + 1);
			}
			return true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002FC0 File Offset: 0x000011C0
		public virtual void Memoize(IIntStream input, int ruleIndex, int ruleStartIndex)
		{
			int value = this.state.failed ? -2 : (input.Index - 1);
			if (this.state.ruleMemo == null && this.TraceDestination != null)
			{
				this.TraceDestination.WriteLine("!!!!!!!!! memo array is null for " + this.GrammarFileName);
			}
			if (ruleIndex >= this.state.ruleMemo.Length && this.TraceDestination != null)
			{
				this.TraceDestination.WriteLine(string.Concat(new object[]
				{
					"!!!!!!!!! memo size is ",
					this.state.ruleMemo.Length,
					", but rule index is ",
					ruleIndex
				}));
			}
			if (this.state.ruleMemo[ruleIndex] != null)
			{
				this.state.ruleMemo[ruleIndex][ruleStartIndex] = value;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003098 File Offset: 0x00001298
		public virtual int GetRuleMemoizationCacheSize()
		{
			int num = 0;
			int num2 = 0;
			while (this.state.ruleMemo != null && num2 < this.state.ruleMemo.Length)
			{
				IDictionary<int, int> dictionary = this.state.ruleMemo[num2];
				if (dictionary != null)
				{
					num += dictionary.Count;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030E8 File Offset: 0x000012E8
		public virtual void TraceIn(string ruleName, int ruleIndex, object inputSymbol)
		{
			if (this.TraceDestination == null)
			{
				return;
			}
			this.TraceDestination.Write(string.Concat(new object[]
			{
				"enter ",
				ruleName,
				" ",
				inputSymbol
			}));
			if (this.state.backtracking > 0)
			{
				this.TraceDestination.Write(" backtracking=" + this.state.backtracking);
			}
			this.TraceDestination.WriteLine();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000316C File Offset: 0x0000136C
		public virtual void TraceOut(string ruleName, int ruleIndex, object inputSymbol)
		{
			if (this.TraceDestination == null)
			{
				return;
			}
			this.TraceDestination.Write(string.Concat(new object[]
			{
				"exit ",
				ruleName,
				" ",
				inputSymbol
			}));
			if (this.state.backtracking > 0)
			{
				this.TraceDestination.Write(" backtracking=" + this.state.backtracking);
				if (this.state.failed)
				{
					this.TraceDestination.Write(" failed");
				}
				else
				{
					this.TraceDestination.Write(" succeeded");
				}
			}
			this.TraceDestination.WriteLine();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000321F File Offset: 0x0000141F
		public virtual IDebugEventListener DebugListener
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003224 File Offset: 0x00001424
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugEnterRule(string grammarFileName, string ruleName)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.EnterRule(grammarFileName, ruleName);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003244 File Offset: 0x00001444
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugExitRule(string grammarFileName, string ruleName)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.ExitRule(grammarFileName, ruleName);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003264 File Offset: 0x00001464
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugEnterSubRule(int decisionNumber)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.EnterSubRule(decisionNumber);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003284 File Offset: 0x00001484
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugExitSubRule(int decisionNumber)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.ExitSubRule(decisionNumber);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000032A4 File Offset: 0x000014A4
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugEnterAlt(int alt)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.EnterAlt(alt);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000032C4 File Offset: 0x000014C4
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugEnterDecision(int decisionNumber, bool couldBacktrack)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.EnterDecision(decisionNumber, couldBacktrack);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032E4 File Offset: 0x000014E4
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugExitDecision(int decisionNumber)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.ExitDecision(decisionNumber);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003304 File Offset: 0x00001504
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugLocation(int line, int charPositionInLine)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.Location(line, charPositionInLine);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003324 File Offset: 0x00001524
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugSemanticPredicate(bool result, string predicate)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.SemanticPredicate(result, predicate);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003344 File Offset: 0x00001544
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugBeginBacktrack(int level)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.BeginBacktrack(level);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003364 File Offset: 0x00001564
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugEndBacktrack(int level, bool successful)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.EndBacktrack(level, successful);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003384 File Offset: 0x00001584
		[Conditional("ANTLR_DEBUG")]
		protected virtual void DebugRecognitionException(RecognitionException ex)
		{
			IDebugEventListener debugListener = this.DebugListener;
			if (debugListener != null)
			{
				debugListener.RecognitionException(ex);
			}
		}

		// Token: 0x04000010 RID: 16
		public const int MemoRuleFailed = -2;

		// Token: 0x04000011 RID: 17
		public const int MemoRuleUnknown = -1;

		// Token: 0x04000012 RID: 18
		public const int InitialFollowStackSize = 100;

		// Token: 0x04000013 RID: 19
		public const int DefaultTokenChannel = 0;

		// Token: 0x04000014 RID: 20
		public const int Hidden = 99;

		// Token: 0x04000015 RID: 21
		public const string NextTokenRuleName = "nextToken";

		// Token: 0x04000016 RID: 22
		protected internal RecognizerSharedState state;
	}
}

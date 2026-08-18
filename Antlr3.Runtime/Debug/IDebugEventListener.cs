using System;

namespace Antlr.Runtime.Debug
{
	// Token: 0x02000019 RID: 25
	public interface IDebugEventListener
	{
		// Token: 0x0600011B RID: 283
		void Initialize();

		// Token: 0x0600011C RID: 284
		void EnterRule(string grammarFileName, string ruleName);

		// Token: 0x0600011D RID: 285
		void EnterAlt(int alt);

		// Token: 0x0600011E RID: 286
		void ExitRule(string grammarFileName, string ruleName);

		// Token: 0x0600011F RID: 287
		void EnterSubRule(int decisionNumber);

		// Token: 0x06000120 RID: 288
		void ExitSubRule(int decisionNumber);

		// Token: 0x06000121 RID: 289
		void EnterDecision(int decisionNumber, bool couldBacktrack);

		// Token: 0x06000122 RID: 290
		void ExitDecision(int decisionNumber);

		// Token: 0x06000123 RID: 291
		void ConsumeToken(IToken t);

		// Token: 0x06000124 RID: 292
		void ConsumeHiddenToken(IToken t);

		// Token: 0x06000125 RID: 293
		void LT(int i, IToken t);

		// Token: 0x06000126 RID: 294
		void Mark(int marker);

		// Token: 0x06000127 RID: 295
		void Rewind(int marker);

		// Token: 0x06000128 RID: 296
		void Rewind();

		// Token: 0x06000129 RID: 297
		void BeginBacktrack(int level);

		// Token: 0x0600012A RID: 298
		void EndBacktrack(int level, bool successful);

		// Token: 0x0600012B RID: 299
		void Location(int line, int pos);

		// Token: 0x0600012C RID: 300
		void RecognitionException(RecognitionException e);

		// Token: 0x0600012D RID: 301
		void BeginResync();

		// Token: 0x0600012E RID: 302
		void EndResync();

		// Token: 0x0600012F RID: 303
		void SemanticPredicate(bool result, string predicate);

		// Token: 0x06000130 RID: 304
		void Commence();

		// Token: 0x06000131 RID: 305
		void Terminate();

		// Token: 0x06000132 RID: 306
		void ConsumeNode(object t);

		// Token: 0x06000133 RID: 307
		void LT(int i, object t);

		// Token: 0x06000134 RID: 308
		void NilNode(object t);

		// Token: 0x06000135 RID: 309
		void ErrorNode(object t);

		// Token: 0x06000136 RID: 310
		void CreateNode(object t);

		// Token: 0x06000137 RID: 311
		void CreateNode(object node, IToken token);

		// Token: 0x06000138 RID: 312
		void BecomeRoot(object newRoot, object oldRoot);

		// Token: 0x06000139 RID: 313
		void AddChild(object root, object child);

		// Token: 0x0600013A RID: 314
		void SetTokenBoundaries(object t, int tokenStartIndex, int tokenStopIndex);
	}
}

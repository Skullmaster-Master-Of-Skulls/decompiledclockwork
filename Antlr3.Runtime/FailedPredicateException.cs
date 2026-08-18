using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class FailedPredicateException : RecognitionException
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00004F15 File Offset: 0x00003115
		public FailedPredicateException()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004F1D File Offset: 0x0000311D
		public FailedPredicateException(string message) : base(message)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004F26 File Offset: 0x00003126
		public FailedPredicateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004F30 File Offset: 0x00003130
		public FailedPredicateException(IIntStream input, string ruleName, string predicateText) : base(input)
		{
			this._ruleName = ruleName;
			this._predicateText = predicateText;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004F47 File Offset: 0x00003147
		public FailedPredicateException(string message, IIntStream input, string ruleName, string predicateText) : base(message, input)
		{
			this._ruleName = ruleName;
			this._predicateText = predicateText;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004F60 File Offset: 0x00003160
		public FailedPredicateException(string message, IIntStream input, string ruleName, string predicateText, Exception innerException) : base(message, input, innerException)
		{
			this._ruleName = ruleName;
			this._predicateText = predicateText;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004F7B File Offset: 0x0000317B
		protected FailedPredicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._ruleName = info.GetString("RuleName");
			this._predicateText = info.GetString("PredicateText");
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00004FB5 File Offset: 0x000031B5
		public string RuleName
		{
			get
			{
				return this._ruleName;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00004FBD File Offset: 0x000031BD
		public string PredicateText
		{
			get
			{
				return this._predicateText;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004FC5 File Offset: 0x000031C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("RuleName", this._ruleName);
			info.AddValue("PredicateText", this._predicateText);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005000 File Offset: 0x00003200
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"FailedPredicateException(",
				this.RuleName,
				",{",
				this.PredicateText,
				"}?)"
			});
		}

		// Token: 0x0400004A RID: 74
		private readonly string _ruleName;

		// Token: 0x0400004B RID: 75
		private readonly string _predicateText;
	}
}

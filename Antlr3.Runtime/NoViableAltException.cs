using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class NoViableAltException : RecognitionException
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00006A0A File Offset: 0x00004C0A
		public NoViableAltException()
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006A12 File Offset: 0x00004C12
		public NoViableAltException(string grammarDecisionDescription)
		{
			this._grammarDecisionDescription = grammarDecisionDescription;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006A21 File Offset: 0x00004C21
		public NoViableAltException(string message, string grammarDecisionDescription) : base(message)
		{
			this._grammarDecisionDescription = grammarDecisionDescription;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006A31 File Offset: 0x00004C31
		public NoViableAltException(string message, string grammarDecisionDescription, Exception innerException) : base(message, innerException)
		{
			this._grammarDecisionDescription = grammarDecisionDescription;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006A42 File Offset: 0x00004C42
		public NoViableAltException(string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input) : this(grammarDecisionDescription, decisionNumber, stateNumber, input, 1)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006A50 File Offset: 0x00004C50
		public NoViableAltException(string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input, int k) : base(input, k)
		{
			this._grammarDecisionDescription = grammarDecisionDescription;
			this._decisionNumber = decisionNumber;
			this._stateNumber = stateNumber;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006A71 File Offset: 0x00004C71
		public NoViableAltException(string message, string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input) : this(message, grammarDecisionDescription, decisionNumber, stateNumber, input, 1)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006A81 File Offset: 0x00004C81
		public NoViableAltException(string message, string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input, int k) : base(message, input, k)
		{
			this._grammarDecisionDescription = grammarDecisionDescription;
			this._decisionNumber = decisionNumber;
			this._stateNumber = stateNumber;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public NoViableAltException(string message, string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input, Exception innerException) : this(message, grammarDecisionDescription, decisionNumber, stateNumber, input, 1, innerException)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006AB6 File Offset: 0x00004CB6
		public NoViableAltException(string message, string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input, int k, Exception innerException) : base(message, input, k, innerException)
		{
			this._grammarDecisionDescription = grammarDecisionDescription;
			this._decisionNumber = decisionNumber;
			this._stateNumber = stateNumber;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006ADC File Offset: 0x00004CDC
		protected NoViableAltException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._grammarDecisionDescription = info.GetString("GrammarDecisionDescription");
			this._decisionNumber = info.GetInt32("DecisionNumber");
			this._stateNumber = info.GetInt32("StateNumber");
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006B32 File Offset: 0x00004D32
		public int DecisionNumber
		{
			get
			{
				return this._decisionNumber;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006B3A File Offset: 0x00004D3A
		public string GrammarDecisionDescription
		{
			get
			{
				return this._grammarDecisionDescription;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00006B42 File Offset: 0x00004D42
		public int StateNumber
		{
			get
			{
				return this._stateNumber;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006B4C File Offset: 0x00004D4C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("GrammarDecisionDescription", this._grammarDecisionDescription);
			info.AddValue("DecisionNumber", this._decisionNumber);
			info.AddValue("StateNumber", this._stateNumber);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override string ToString()
		{
			if (base.Input is ICharStream)
			{
				return string.Concat(new object[]
				{
					"NoViableAltException('",
					(char)this.UnexpectedType,
					"'@[",
					this.GrammarDecisionDescription,
					"])"
				});
			}
			return string.Concat(new object[]
			{
				"NoViableAltException(",
				this.UnexpectedType,
				"@[",
				this.GrammarDecisionDescription,
				"])"
			});
		}

		// Token: 0x04000066 RID: 102
		private readonly string _grammarDecisionDescription;

		// Token: 0x04000067 RID: 103
		private readonly int _decisionNumber;

		// Token: 0x04000068 RID: 104
		private readonly int _stateNumber;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class MismatchedTokenException : RecognitionException
	{
		// Token: 0x06000218 RID: 536 RVA: 0x000065F4 File Offset: 0x000047F4
		public MismatchedTokenException()
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000065FC File Offset: 0x000047FC
		public MismatchedTokenException(string message) : base(message)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006605 File Offset: 0x00004805
		public MismatchedTokenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000660F File Offset: 0x0000480F
		public MismatchedTokenException(int expecting, IIntStream input) : this(expecting, input, null)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000661A File Offset: 0x0000481A
		public MismatchedTokenException(int expecting, IIntStream input, IList<string> tokenNames) : base(input)
		{
			this._expecting = expecting;
			if (tokenNames != null)
			{
				this._tokenNames = new List<string>(tokenNames).AsReadOnly();
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000663E File Offset: 0x0000483E
		public MismatchedTokenException(string message, int expecting, IIntStream input, IList<string> tokenNames) : base(message, input)
		{
			this._expecting = expecting;
			if (tokenNames != null)
			{
				this._tokenNames = new List<string>(tokenNames).AsReadOnly();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006665 File Offset: 0x00004865
		public MismatchedTokenException(string message, int expecting, IIntStream input, IList<string> tokenNames, Exception innerException) : base(message, input, innerException)
		{
			this._expecting = expecting;
			if (tokenNames != null)
			{
				this._tokenNames = new List<string>(tokenNames).AsReadOnly();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006690 File Offset: 0x00004890
		protected MismatchedTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._expecting = info.GetInt32("Expecting");
			this._tokenNames = new ReadOnlyCollection<string>((string[])info.GetValue("TokenNames", typeof(string[])));
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000066E9 File Offset: 0x000048E9
		public int Expecting
		{
			get
			{
				return this._expecting;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000066F1 File Offset: 0x000048F1
		public ReadOnlyCollection<string> TokenNames
		{
			get
			{
				return this._tokenNames;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000066FC File Offset: 0x000048FC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Expecting", this._expecting);
			info.AddValue("TokenNames", (this._tokenNames != null) ? new List<string>(this._tokenNames).ToArray() : null);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006758 File Offset: 0x00004958
		public override string ToString()
		{
			int unexpectedType = this.UnexpectedType;
			string text = (this.TokenNames != null && unexpectedType >= 0 && unexpectedType < this.TokenNames.Count) ? this.TokenNames[unexpectedType] : unexpectedType.ToString();
			string text2 = (this.TokenNames != null && this.Expecting >= 0 && this.Expecting < this.TokenNames.Count) ? this.TokenNames[this.Expecting] : this.Expecting.ToString();
			return string.Concat(new string[]
			{
				"MismatchedTokenException(",
				text,
				"!=",
				text2,
				")"
			});
		}

		// Token: 0x04000062 RID: 98
		private readonly int _expecting;

		// Token: 0x04000063 RID: 99
		private readonly ReadOnlyCollection<string> _tokenNames;
	}
}

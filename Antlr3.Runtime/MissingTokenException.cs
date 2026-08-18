using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class MissingTokenException : MismatchedTokenException
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000690E File Offset: 0x00004B0E
		public MissingTokenException()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006916 File Offset: 0x00004B16
		public MissingTokenException(string message) : base(message)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000691F File Offset: 0x00004B1F
		public MissingTokenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006929 File Offset: 0x00004B29
		public MissingTokenException(int expecting, IIntStream input, object inserted) : this(expecting, input, inserted, null)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006935 File Offset: 0x00004B35
		public MissingTokenException(int expecting, IIntStream input, object inserted, IList<string> tokenNames) : base(expecting, input, tokenNames)
		{
			this._inserted = inserted;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006948 File Offset: 0x00004B48
		public MissingTokenException(string message, int expecting, IIntStream input, object inserted, IList<string> tokenNames) : base(message, expecting, input, tokenNames)
		{
			this._inserted = inserted;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000695D File Offset: 0x00004B5D
		public MissingTokenException(string message, int expecting, IIntStream input, object inserted, IList<string> tokenNames, Exception innerException) : base(message, expecting, input, tokenNames, innerException)
		{
			this._inserted = inserted;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006974 File Offset: 0x00004B74
		protected MissingTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000697E File Offset: 0x00004B7E
		public virtual int MissingType
		{
			get
			{
				return base.Expecting;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006988 File Offset: 0x00004B88
		public override string ToString()
		{
			if (this._inserted != null && base.Token != null)
			{
				return string.Concat(new object[]
				{
					"MissingTokenException(inserted ",
					this._inserted,
					" at ",
					base.Token.Text,
					")"
				});
			}
			if (base.Token != null)
			{
				return "MissingTokenException(at " + base.Token.Text + ")";
			}
			return "MissingTokenException";
		}

		// Token: 0x04000065 RID: 101
		private readonly object _inserted;
	}
}

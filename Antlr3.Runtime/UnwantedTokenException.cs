using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class UnwantedTokenException : MismatchedTokenException
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public UnwantedTokenException()
		{
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000BD00 File Offset: 0x00009F00
		public UnwantedTokenException(string message) : base(message)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000BD09 File Offset: 0x00009F09
		public UnwantedTokenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000BD13 File Offset: 0x00009F13
		public UnwantedTokenException(int expecting, IIntStream input) : base(expecting, input)
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000BD1D File Offset: 0x00009F1D
		public UnwantedTokenException(int expecting, IIntStream input, IList<string> tokenNames) : base(expecting, input, tokenNames)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000BD28 File Offset: 0x00009F28
		public UnwantedTokenException(string message, int expecting, IIntStream input, IList<string> tokenNames) : base(message, expecting, input, tokenNames)
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000BD35 File Offset: 0x00009F35
		public UnwantedTokenException(string message, int expecting, IIntStream input, IList<string> tokenNames, Exception innerException) : base(message, expecting, input, tokenNames, innerException)
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000BD44 File Offset: 0x00009F44
		protected UnwantedTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000BD4E File Offset: 0x00009F4E
		public virtual IToken UnexpectedToken
		{
			get
			{
				return base.Token;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000BD58 File Offset: 0x00009F58
		public override string ToString()
		{
			string str = (base.TokenNames != null && base.Expecting >= 0 && base.Expecting < base.TokenNames.Count) ? base.TokenNames[base.Expecting] : base.Expecting.ToString();
			string text = ", expected " + str;
			if (base.Expecting == 0)
			{
				text = "";
			}
			if (base.Token == null)
			{
				return "UnwantedTokenException(found=" + text + ")";
			}
			return "UnwantedTokenException(found=" + base.Token.Text + text + ")";
		}
	}
}

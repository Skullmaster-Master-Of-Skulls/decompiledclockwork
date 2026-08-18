using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class MismatchedNotSetException : MismatchedSetException
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00006415 File Offset: 0x00004615
		public MismatchedNotSetException()
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000641D File Offset: 0x0000461D
		public MismatchedNotSetException(string message) : base(message)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006426 File Offset: 0x00004626
		public MismatchedNotSetException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006430 File Offset: 0x00004630
		public MismatchedNotSetException(BitSet expecting, IIntStream input) : base(expecting, input)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000643A File Offset: 0x0000463A
		public MismatchedNotSetException(string message, BitSet expecting, IIntStream input) : base(message, expecting, input)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006445 File Offset: 0x00004645
		public MismatchedNotSetException(string message, BitSet expecting, IIntStream input, Exception innerException) : base(message, expecting, input, innerException)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006452 File Offset: 0x00004652
		protected MismatchedNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000645C File Offset: 0x0000465C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"MismatchedNotSetException(",
				this.UnexpectedType,
				"!=",
				base.Expecting,
				")"
			});
		}
	}
}

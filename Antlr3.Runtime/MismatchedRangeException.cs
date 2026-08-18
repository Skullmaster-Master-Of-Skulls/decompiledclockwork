using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class MismatchedRangeException : RecognitionException
	{
		// Token: 0x0600020D RID: 525 RVA: 0x000064A5 File Offset: 0x000046A5
		public MismatchedRangeException()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000064AD File Offset: 0x000046AD
		public MismatchedRangeException(string message) : base(message)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000064B6 File Offset: 0x000046B6
		public MismatchedRangeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000064C0 File Offset: 0x000046C0
		public MismatchedRangeException(int a, int b, IIntStream input) : base(input)
		{
			this._a = a;
			this._b = b;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000064D7 File Offset: 0x000046D7
		public MismatchedRangeException(string message, int a, int b, IIntStream input) : base(message, input)
		{
			this._a = a;
			this._b = b;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000064F0 File Offset: 0x000046F0
		public MismatchedRangeException(string message, int a, int b, IIntStream input, Exception innerException) : base(message, input, innerException)
		{
			this._a = a;
			this._b = b;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000650B File Offset: 0x0000470B
		protected MismatchedRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._a = info.GetInt32("A");
			this._b = info.GetInt32("B");
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006545 File Offset: 0x00004745
		public int A
		{
			get
			{
				return this._a;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000654D File Offset: 0x0000474D
		public int B
		{
			get
			{
				return this._b;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006555 File Offset: 0x00004755
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("A", this._a);
			info.AddValue("B", this._b);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006590 File Offset: 0x00004790
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"MismatchedRangeException(",
				this.UnexpectedType,
				" not in [",
				this.A,
				",",
				this.B,
				"])"
			});
		}

		// Token: 0x04000060 RID: 96
		private readonly int _a;

		// Token: 0x04000061 RID: 97
		private readonly int _b;
	}
}

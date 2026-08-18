using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class MismatchedSetException : RecognitionException
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00006313 File Offset: 0x00004513
		public MismatchedSetException()
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000631B File Offset: 0x0000451B
		public MismatchedSetException(string message) : base(message)
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006324 File Offset: 0x00004524
		public MismatchedSetException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000632E File Offset: 0x0000452E
		public MismatchedSetException(BitSet expecting, IIntStream input) : base(input)
		{
			this._expecting = expecting;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000633E File Offset: 0x0000453E
		public MismatchedSetException(string message, BitSet expecting, IIntStream input) : base(message, input)
		{
			this._expecting = expecting;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000634F File Offset: 0x0000454F
		public MismatchedSetException(string message, BitSet expecting, IIntStream input, Exception innerException) : base(message, input, innerException)
		{
			this._expecting = expecting;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006362 File Offset: 0x00004562
		protected MismatchedSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._expecting = (BitSet)info.GetValue("Expecting", typeof(BitSet));
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000639A File Offset: 0x0000459A
		public BitSet Expecting
		{
			get
			{
				return this._expecting;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000063A2 File Offset: 0x000045A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Expecting", this._expecting);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000063CC File Offset: 0x000045CC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"MismatchedSetException(",
				this.UnexpectedType,
				"!=",
				this.Expecting,
				")"
			});
		}

		// Token: 0x0400005F RID: 95
		private readonly BitSet _expecting;
	}
}

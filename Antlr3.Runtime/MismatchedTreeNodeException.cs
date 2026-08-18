using System;
using System.Runtime.Serialization;
using Antlr.Runtime.Tree;

namespace Antlr.Runtime
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class MismatchedTreeNodeException : RecognitionException
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00006816 File Offset: 0x00004A16
		public MismatchedTreeNodeException()
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000681E File Offset: 0x00004A1E
		public MismatchedTreeNodeException(string message) : base(message)
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006827 File Offset: 0x00004A27
		public MismatchedTreeNodeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006831 File Offset: 0x00004A31
		public MismatchedTreeNodeException(int expecting, ITreeNodeStream input) : base(input)
		{
			this._expecting = expecting;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006841 File Offset: 0x00004A41
		public MismatchedTreeNodeException(string message, int expecting, ITreeNodeStream input) : base(message, input)
		{
			this._expecting = expecting;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006852 File Offset: 0x00004A52
		public MismatchedTreeNodeException(string message, int expecting, ITreeNodeStream input, Exception innerException) : base(message, input, innerException)
		{
			this._expecting = expecting;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006865 File Offset: 0x00004A65
		protected MismatchedTreeNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._expecting = info.GetInt32("Expecting");
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000688E File Offset: 0x00004A8E
		public int Expecting
		{
			get
			{
				return this._expecting;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006896 File Offset: 0x00004A96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Expecting", this._expecting);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000068C0 File Offset: 0x00004AC0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"MismatchedTreeNodeException(",
				this.UnexpectedType,
				"!=",
				this.Expecting,
				")"
			});
		}

		// Token: 0x04000064 RID: 100
		private readonly int _expecting;
	}
}

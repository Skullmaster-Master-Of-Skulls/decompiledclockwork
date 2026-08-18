using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class EarlyExitException : RecognitionException
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00004E6C File Offset: 0x0000306C
		public EarlyExitException()
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004E74 File Offset: 0x00003074
		public EarlyExitException(string message) : base(message)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004E7D File Offset: 0x0000307D
		public EarlyExitException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004E87 File Offset: 0x00003087
		public EarlyExitException(int decisionNumber, IIntStream input) : base(input)
		{
			this._decisionNumber = decisionNumber;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004E97 File Offset: 0x00003097
		public EarlyExitException(string message, int decisionNumber, IIntStream input) : base(message, input)
		{
			this._decisionNumber = decisionNumber;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004EA8 File Offset: 0x000030A8
		public EarlyExitException(string message, int decisionNumber, IIntStream input, Exception innerException) : base(message, input, innerException)
		{
			this._decisionNumber = decisionNumber;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004EBB File Offset: 0x000030BB
		protected EarlyExitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._decisionNumber = info.GetInt32("DecisionNumber");
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004EE4 File Offset: 0x000030E4
		public int DecisionNumber
		{
			get
			{
				return this._decisionNumber;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004EEC File Offset: 0x000030EC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("DecisionNumber", this.DecisionNumber);
		}

		// Token: 0x04000049 RID: 73
		private readonly int _decisionNumber;
	}
}

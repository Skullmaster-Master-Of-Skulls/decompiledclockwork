using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F7 RID: 2039
	internal abstract class ClientFramingDecoder : FramingDecoder
	{
		// Token: 0x06004CF6 RID: 19702 RVA: 0x001192EA File Offset: 0x001174EA
		protected ClientFramingDecoder(long streamPosition) : base(streamPosition)
		{
			this.currentState = ClientFramingDecoderState.ReadingUpgradeRecord;
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x001192FA File Offset: 0x001174FA
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x00119302 File Offset: 0x00117502
		public ClientFramingDecoderState CurrentState
		{
			get
			{
				return this.currentState;
			}
			protected set
			{
				this.currentState = value;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x0011930B File Offset: 0x0011750B
		protected override string CurrentStateAsString
		{
			get
			{
				return this.currentState.ToString();
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004CFA RID: 19706
		public abstract string Fault { get; }

		// Token: 0x06004CFB RID: 19707
		public abstract int Decode(byte[] bytes, int offset, int size);

		// Token: 0x04002FF5 RID: 12277
		private ClientFramingDecoderState currentState;
	}
}

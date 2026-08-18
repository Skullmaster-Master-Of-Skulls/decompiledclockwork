using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F1 RID: 2033
	internal class ServerModeDecoder : FramingDecoder
	{
		// Token: 0x06004CD0 RID: 19664 RVA: 0x00118514 File Offset: 0x00116714
		public ServerModeDecoder()
		{
			this.currentState = ServerModeDecoder.State.ReadingVersionRecord;
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00118524 File Offset: 0x00116724
		public int Decode(byte[] bytes, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			int result;
			try
			{
				int num;
				switch (this.currentState)
				{
				case ServerModeDecoder.State.ReadingVersionRecord:
					base.ValidateRecordType(FramingRecordType.Version, (FramingRecordType)bytes[offset]);
					this.currentState = ServerModeDecoder.State.ReadingMajorVersion;
					num = 1;
					break;
				case ServerModeDecoder.State.ReadingMajorVersion:
					this.majorVersion = (int)bytes[offset];
					base.ValidateMajorVersion(this.majorVersion);
					this.currentState = ServerModeDecoder.State.ReadingMinorVersion;
					num = 1;
					break;
				case ServerModeDecoder.State.ReadingMinorVersion:
					this.minorVersion = (int)bytes[offset];
					this.currentState = ServerModeDecoder.State.ReadingModeRecord;
					num = 1;
					break;
				case ServerModeDecoder.State.ReadingModeRecord:
					base.ValidateRecordType(FramingRecordType.Mode, (FramingRecordType)bytes[offset]);
					this.currentState = ServerModeDecoder.State.ReadingModeValue;
					num = 1;
					break;
				case ServerModeDecoder.State.ReadingModeValue:
					this.mode = (FramingMode)bytes[offset];
					base.ValidateFramingMode(this.mode);
					this.currentState = ServerModeDecoder.State.Done;
					num = 1;
					break;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("InvalidDecoderStateMachine"))));
				}
				base.StreamPosition += (long)num;
				result = num;
			}
			catch (InvalidDataException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(innerException));
			}
			return result;
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x00118638 File Offset: 0x00116838
		public void Reset()
		{
			base.StreamPosition = 0L;
			this.currentState = ServerModeDecoder.State.ReadingVersionRecord;
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x00118649 File Offset: 0x00116849
		public ServerModeDecoder.State CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x00118651 File Offset: 0x00116851
		protected override string CurrentStateAsString
		{
			get
			{
				return this.currentState.ToString();
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x00118664 File Offset: 0x00116864
		public FramingMode Mode
		{
			get
			{
				if (this.currentState != ServerModeDecoder.State.Done)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.mode;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x0011868F File Offset: 0x0011688F
		public int MajorVersion
		{
			get
			{
				if (this.currentState != ServerModeDecoder.State.Done)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.majorVersion;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x001186BA File Offset: 0x001168BA
		public int MinorVersion
		{
			get
			{
				if (this.currentState != ServerModeDecoder.State.Done)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.minorVersion;
			}
		}

		// Token: 0x04002FCC RID: 12236
		private ServerModeDecoder.State currentState;

		// Token: 0x04002FCD RID: 12237
		private int majorVersion;

		// Token: 0x04002FCE RID: 12238
		private int minorVersion;

		// Token: 0x04002FCF RID: 12239
		private FramingMode mode;

		// Token: 0x02000D0F RID: 3343
		public enum State
		{
			// Token: 0x04004679 RID: 18041
			ReadingVersionRecord,
			// Token: 0x0400467A RID: 18042
			ReadingMajorVersion,
			// Token: 0x0400467B RID: 18043
			ReadingMinorVersion,
			// Token: 0x0400467C RID: 18044
			ReadingModeRecord,
			// Token: 0x0400467D RID: 18045
			ReadingModeValue,
			// Token: 0x0400467E RID: 18046
			Done
		}
	}
}

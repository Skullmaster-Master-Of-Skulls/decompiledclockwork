using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public class MsmqException : ExternalException
	{
		// Token: 0x060002EF RID: 751 RVA: 0x000114EC File Offset: 0x0000F6EC
		public MsmqException()
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000114F4 File Offset: 0x0000F6F4
		public MsmqException(string message) : base(message)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000114FD File Offset: 0x0000F6FD
		public MsmqException(string message, int error) : base(message, error)
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00011507 File Offset: 0x0000F707
		public MsmqException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011511 File Offset: 0x0000F711
		protected MsmqException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001151B File Offset: 0x0000F71B
		internal bool FaultSender
		{
			get
			{
				this.TuneBehavior();
				return this.faultSender.Value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0001152E File Offset: 0x0000F72E
		internal bool FaultReceiver
		{
			get
			{
				this.TuneBehavior();
				return this.faultReceiver.Value;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011544 File Offset: 0x0000F744
		private void TuneBehavior()
		{
			if (this.faultSender != null && this.faultReceiver != null)
			{
				return;
			}
			int errorCode = this.ErrorCode;
			if (errorCode <= -1072824267)
			{
				if (errorCode <= -1072824300)
				{
					if (errorCode <= -1072824311)
					{
						if (errorCode == -1072824317)
						{
							this.faultSender = new bool?(true);
							this.faultReceiver = new bool?(true);
							this.outerExceptionType = typeof(EndpointNotFoundException);
							return;
						}
						if (errorCode == -1072824311)
						{
							this.faultSender = new bool?(true);
							this.faultReceiver = new bool?(true);
							this.outerExceptionType = typeof(AddressAccessDeniedException);
							return;
						}
					}
					else
					{
						if (errorCode == -1072824309)
						{
							this.faultSender = new bool?(false);
							this.faultReceiver = new bool?(true);
							this.outerExceptionType = typeof(EndpointNotFoundException);
							return;
						}
						if (errorCode == -1072824300)
						{
							this.faultSender = new bool?(false);
							this.faultReceiver = new bool?(false);
							this.outerExceptionType = typeof(ArgumentException);
							return;
						}
					}
				}
				else if (errorCode <= -1072824290)
				{
					if (errorCode == -1072824293)
					{
						this.faultSender = new bool?(false);
						this.faultReceiver = new bool?(false);
						this.outerExceptionType = typeof(TimeoutException);
						return;
					}
					if (errorCode == -1072824290)
					{
						this.faultSender = new bool?(false);
						this.faultReceiver = new bool?(false);
						this.outerExceptionType = typeof(ArgumentException);
						return;
					}
				}
				else
				{
					if (errorCode == -1072824288)
					{
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(ArgumentException);
						return;
					}
					switch (errorCode)
					{
					case -1072824283:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(AddressAccessDeniedException);
						return;
					case -1072824282:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824281:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824280:
					case -1072824279:
					case -1072824277:
					case -1072824275:
					case -1072824274:
					case -1072824272:
						break;
					case -1072824278:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824276:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824273:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824271:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					default:
						if (errorCode == -1072824267)
						{
							this.faultSender = new bool?(true);
							this.faultReceiver = new bool?(true);
							this.outerExceptionType = typeof(CommunicationException);
							return;
						}
						break;
					}
				}
			}
			else if (errorCode <= -1072824240)
			{
				if (errorCode <= -1072824257)
				{
					if (errorCode == -1072824266)
					{
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					}
					if (errorCode == -1072824257)
					{
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					}
				}
				else
				{
					if (errorCode == -1072824255)
					{
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					}
					switch (errorCode)
					{
					case -1072824245:
						this.faultSender = new bool?(false);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(EndpointNotFoundException);
						return;
					case -1072824244:
						this.faultSender = new bool?(false);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824242:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(CommunicationException);
						return;
					case -1072824240:
						this.faultSender = new bool?(true);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(InvalidOperationException);
						return;
					}
				}
			}
			else if (errorCode <= -1072824215)
			{
				switch (errorCode)
				{
				case -1072824234:
					this.faultSender = new bool?(false);
					this.faultReceiver = new bool?(false);
					this.outerExceptionType = typeof(InvalidOperationException);
					return;
				case -1072824233:
				case -1072824231:
					break;
				case -1072824232:
					this.faultSender = new bool?(false);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(CommunicationException);
					return;
				case -1072824230:
					this.faultSender = new bool?(true);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(EndpointNotFoundException);
					return;
				default:
					if (errorCode == -1072824215)
					{
						this.faultSender = new bool?(false);
						this.faultReceiver = new bool?(true);
						this.outerExceptionType = typeof(EndpointNotFoundException);
						return;
					}
					break;
				}
			}
			else
			{
				if (errorCode == -1072824211)
				{
					this.faultSender = new bool?(true);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(CommunicationException);
					return;
				}
				if (errorCode == -1072824209)
				{
					this.faultSender = new bool?(true);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(CommunicationException);
					return;
				}
				switch (errorCode)
				{
				case -1072824193:
					this.faultSender = new bool?(true);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(CommunicationException);
					return;
				case -1072824192:
					this.faultSender = new bool?(true);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(CommunicationException);
					return;
				case -1072824190:
					this.faultSender = new bool?(true);
					this.faultReceiver = new bool?(true);
					this.outerExceptionType = typeof(CommunicationException);
					return;
				}
			}
			this.faultSender = new bool?(true);
			this.faultReceiver = new bool?(true);
			this.outerExceptionType = null;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00011C26 File Offset: 0x0000FE26
		internal Exception Normalized
		{
			get
			{
				this.TuneBehavior();
				if (null != this.outerExceptionType)
				{
					return Activator.CreateInstance(this.outerExceptionType, new object[]
					{
						this.Message,
						this
					}) as Exception;
				}
				return this;
			}
		}

		// Token: 0x0400094F RID: 2383
		[NonSerialized]
		private bool? faultSender;

		// Token: 0x04000950 RID: 2384
		[NonSerialized]
		private bool? faultReceiver;

		// Token: 0x04000951 RID: 2385
		[NonSerialized]
		private Type outerExceptionType;
	}
}

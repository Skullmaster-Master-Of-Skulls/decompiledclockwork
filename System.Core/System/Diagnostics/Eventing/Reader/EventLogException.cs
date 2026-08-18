using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C3 RID: 707
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogException : Exception, ISerializable
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x0005D3B4 File Offset: 0x0005B5B4
		internal static void Throw(int errorCode)
		{
			if (errorCode <= 1818)
			{
				if (errorCode <= 5)
				{
					if (errorCode - 2 > 1)
					{
						if (errorCode != 5)
						{
							goto IL_97;
						}
						throw new UnauthorizedAccessException();
					}
				}
				else
				{
					if (errorCode == 13)
					{
						goto IL_76;
					}
					if (errorCode != 1223 && errorCode != 1818)
					{
						goto IL_97;
					}
					throw new OperationCanceledException();
				}
			}
			else if (errorCode <= 15007)
			{
				if (errorCode != 15002)
				{
					if (errorCode == 15005)
					{
						goto IL_76;
					}
					if (errorCode != 15007)
					{
						goto IL_97;
					}
				}
			}
			else
			{
				if (errorCode - 15011 <= 1)
				{
					throw new EventLogReadingException(errorCode);
				}
				if (errorCode - 15027 > 1)
				{
					if (errorCode != 15037)
					{
						goto IL_97;
					}
					throw new EventLogProviderDisabledException(errorCode);
				}
			}
			throw new EventLogNotFoundException(errorCode);
			IL_76:
			throw new EventLogInvalidDataException(errorCode);
			IL_97:
			throw new EventLogException(errorCode);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005D45E File Offset: 0x0005B65E
		public EventLogException()
		{
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005D466 File Offset: 0x0005B666
		public EventLogException(string message) : base(message)
		{
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005D46F File Offset: 0x0005B66F
		public EventLogException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0005D479 File Offset: 0x0005B679
		protected EventLogException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005D483 File Offset: 0x0005B683
		protected EventLogException(int errorCode)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0005D492 File Offset: 0x0005B692
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("errorCode", this.errorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0005D4BC File Offset: 0x0005B6BC
		public override string Message
		{
			[SecurityCritical]
			get
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
				Win32Exception ex = new Win32Exception(this.errorCode);
				return ex.Message;
			}
		}

		// Token: 0x04000C97 RID: 3223
		private int errorCode;
	}
}

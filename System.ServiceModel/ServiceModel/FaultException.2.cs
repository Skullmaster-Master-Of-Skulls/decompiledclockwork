using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200010A RID: 266
	[__DynamicallyInvokable]
	[Serializable]
	public class FaultException<TDetail> : FaultException
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0001B296 File Offset: 0x00019496
		public FaultException(TDetail detail)
		{
			this.detail = detail;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001B2A5 File Offset: 0x000194A5
		public FaultException(TDetail detail, string reason) : base(reason)
		{
			this.detail = detail;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001B2B5 File Offset: 0x000194B5
		public FaultException(TDetail detail, FaultReason reason) : base(reason)
		{
			this.detail = detail;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B2C5 File Offset: 0x000194C5
		public FaultException(TDetail detail, string reason, FaultCode code) : base(reason, code)
		{
			this.detail = detail;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B2D6 File Offset: 0x000194D6
		public FaultException(TDetail detail, FaultReason reason, FaultCode code) : base(reason, code)
		{
			this.detail = detail;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B2E7 File Offset: 0x000194E7
		public FaultException(TDetail detail, string reason, FaultCode code, string action) : base(reason, code, action)
		{
			this.detail = detail;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B2FA File Offset: 0x000194FA
		[__DynamicallyInvokable]
		public FaultException(TDetail detail, FaultReason reason, FaultCode code, string action) : base(reason, code, action)
		{
			this.detail = detail;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B30D File Offset: 0x0001950D
		protected FaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.detail = (TDetail)((object)info.GetValue("detail", typeof(TDetail)));
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001B337 File Offset: 0x00019537
		[__DynamicallyInvokable]
		public TDetail Detail
		{
			[__DynamicallyInvokable]
			get
			{
				return this.detail;
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001B33F File Offset: 0x0001953F
		[__DynamicallyInvokable]
		public override MessageFault CreateMessageFault()
		{
			return MessageFault.CreateFault(base.Code, base.Reason, this.detail);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001B35D File Offset: 0x0001955D
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("detail", this.detail);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001B380 File Offset: 0x00019580
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return SR.GetString("SFxFaultExceptionToString3", new object[]
			{
				base.GetType(),
				this.Message,
				(this.detail != null) ? this.detail.ToString() : string.Empty
			});
		}

		// Token: 0x04000A63 RID: 2659
		private TDetail detail;
	}
}

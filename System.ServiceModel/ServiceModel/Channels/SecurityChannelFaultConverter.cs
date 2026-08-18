using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000997 RID: 2455
	internal class SecurityChannelFaultConverter : FaultConverter
	{
		// Token: 0x06005FDC RID: 24540 RVA: 0x00165770 File Offset: 0x00163970
		internal SecurityChannelFaultConverter(IChannel innerChannel)
		{
			this.innerChannel = innerChannel;
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x00165780 File Offset: 0x00163980
		protected override bool OnTryCreateException(Message message, MessageFault fault, out Exception exception)
		{
			if (this.innerChannel == null)
			{
				exception = null;
				return false;
			}
			FaultConverter property = this.innerChannel.GetProperty<FaultConverter>();
			if (property != null)
			{
				return property.TryCreateException(message, fault, out exception);
			}
			exception = null;
			return false;
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x001657B8 File Offset: 0x001639B8
		protected override bool OnTryCreateFaultMessage(Exception exception, out Message message)
		{
			if (this.innerChannel == null)
			{
				message = null;
				return false;
			}
			FaultConverter property = this.innerChannel.GetProperty<FaultConverter>();
			if (property != null)
			{
				return property.TryCreateFaultMessage(exception, out message);
			}
			message = null;
			return false;
		}

		// Token: 0x0400385B RID: 14427
		private IChannel innerChannel;
	}
}

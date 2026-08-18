using System;
using System.ServiceModel;
using TechnoPro.Common.WCF.Attributes;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class BindingSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public int MaxReceivedMessageSize { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public int MaxBufferSize { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public int MaxStringContentLength { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public int MaxArrayLength { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		public TimeSpan OpenTimeout { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		public TimeSpan CloseTimeout { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B6 File Offset: 0x000002B6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020BE File Offset: 0x000002BE
		public TimeSpan SendTimeout { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020CF File Offset: 0x000002CF
		public TimeSpan ReceiveTimeout { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020E0 File Offset: 0x000002E0
		public TransferMode TransferMode { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000020EC File Offset: 0x000002EC
		public BindingSettings()
		{
			this.TransferMode = TransferMode.Buffered;
			this.MaxReceivedMessageSize = 524288000;
			this.MaxBufferSize = 524288000;
			this.MaxStringContentLength = 524288000;
			this.MaxArrayLength = 524288000;
			this.OpenTimeout = new TimeSpan(0, 10, 0);
			this.CloseTimeout = new TimeSpan(0, 10, 0);
			this.SendTimeout = new TimeSpan(0, 10, 0);
			this.ReceiveTimeout = new TimeSpan(8, 0, 0);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002178 File Offset: 0x00000378
		public void ApplyBindingSettingsAttributes(params BindingServiceAttribute[] settings)
		{
			foreach (BindingServiceAttribute att in settings)
			{
				this.ApplyBindingSettingsAttribute(att);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021A8 File Offset: 0x000003A8
		protected virtual void ApplyBindingSettingsAttribute(BindingServiceAttribute att)
		{
			bool flag = att is XtraSizeServiceAttribute;
			if (flag)
			{
				XtraSizeServiceAttribute xtraSizeServiceAttribute = att as XtraSizeServiceAttribute;
				this.MaxReceivedMessageSize = xtraSizeServiceAttribute.SizeInBytes;
				this.MaxBufferSize = xtraSizeServiceAttribute.SizeInBytes;
				this.MaxStringContentLength = xtraSizeServiceAttribute.SizeInBytes;
				this.MaxArrayLength = xtraSizeServiceAttribute.SizeInBytes;
			}
			else
			{
				bool flag2 = att is XtraTimeServiceAttribute;
				if (flag2)
				{
					XtraTimeServiceAttribute xtraTimeServiceAttribute = att as XtraTimeServiceAttribute;
					this.OpenTimeout = new TimeSpan(0, xtraTimeServiceAttribute.TimeoutInMinutes, 0);
					this.CloseTimeout = new TimeSpan(0, xtraTimeServiceAttribute.TimeoutInMinutes, 0);
					this.SendTimeout = new TimeSpan(0, xtraTimeServiceAttribute.TimeoutInMinutes, 0);
					this.ReceiveTimeout = new TimeSpan(8, 0, 0);
				}
				else
				{
					bool flag3 = att is StreamingServiceAttribute;
					if (flag3)
					{
						StreamingServiceAttribute streamingServiceAttribute = att as StreamingServiceAttribute;
						this.MaxReceivedMessageSize = streamingServiceAttribute.SizeInBytes;
						this.MaxBufferSize = streamingServiceAttribute.SizeInBytes;
						this.MaxStringContentLength = streamingServiceAttribute.SizeInBytes;
						this.MaxArrayLength = streamingServiceAttribute.SizeInBytes;
						this.OpenTimeout = new TimeSpan(0, streamingServiceAttribute.TimeoutInMinutes, 0);
						this.CloseTimeout = new TimeSpan(0, streamingServiceAttribute.TimeoutInMinutes, 0);
						this.SendTimeout = new TimeSpan(0, streamingServiceAttribute.TimeoutInMinutes, 0);
						this.ReceiveTimeout = new TimeSpan(8, 0, 0);
						this.TransferMode = TransferMode.Streamed;
					}
				}
			}
		}
	}
}

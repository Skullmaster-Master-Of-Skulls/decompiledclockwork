using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F7 RID: 2295
	internal abstract class MsmqReceiveParameters
	{
		// Token: 0x0600578A RID: 22410 RVA: 0x0014142A File Offset: 0x0013F62A
		internal MsmqReceiveParameters(MsmqBindingElementBase bindingElement) : this(bindingElement, bindingElement.AddressTranslator)
		{
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x0014143C File Offset: 0x0013F63C
		internal MsmqReceiveParameters(MsmqBindingElementBase bindingElement, MsmqUri.IAddressTranslator addressTranslator)
		{
			this.addressTranslator = addressTranslator;
			this.durable = bindingElement.Durable;
			this.exactlyOnce = bindingElement.ExactlyOnce;
			this.maxRetryCycles = bindingElement.MaxRetryCycles;
			this.receiveErrorHandling = bindingElement.ReceiveErrorHandling;
			this.receiveRetryCount = bindingElement.ReceiveRetryCount;
			this.retryCycleDelay = bindingElement.RetryCycleDelay;
			this.transportSecurity = new MsmqTransportSecurity(bindingElement.MsmqTransportSecurity);
			this.useMsmqTracing = bindingElement.UseMsmqTracing;
			this.useSourceJournal = bindingElement.UseSourceJournal;
			this.receiveContextSettings = new MsmqReceiveContextSettings(bindingElement.ReceiveContextSettings);
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x0600578C RID: 22412 RVA: 0x001414D8 File Offset: 0x0013F6D8
		internal MsmqReceiveContextSettings ReceiveContextSettings
		{
			get
			{
				return this.receiveContextSettings;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x0600578D RID: 22413 RVA: 0x001414E0 File Offset: 0x0013F6E0
		internal MsmqUri.IAddressTranslator AddressTranslator
		{
			get
			{
				return this.addressTranslator;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x0600578E RID: 22414 RVA: 0x001414E8 File Offset: 0x0013F6E8
		internal bool Durable
		{
			get
			{
				return this.durable;
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x0600578F RID: 22415 RVA: 0x001414F0 File Offset: 0x0013F6F0
		internal bool ExactlyOnce
		{
			get
			{
				return this.exactlyOnce;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06005790 RID: 22416 RVA: 0x001414F8 File Offset: 0x0013F6F8
		internal int ReceiveRetryCount
		{
			get
			{
				return this.receiveRetryCount;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06005791 RID: 22417 RVA: 0x00141500 File Offset: 0x0013F700
		internal int MaxRetryCycles
		{
			get
			{
				return this.maxRetryCycles;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x00141508 File Offset: 0x0013F708
		internal ReceiveErrorHandling ReceiveErrorHandling
		{
			get
			{
				return this.receiveErrorHandling;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x00141510 File Offset: 0x0013F710
		internal TimeSpan RetryCycleDelay
		{
			get
			{
				return this.retryCycleDelay;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x00141518 File Offset: 0x0013F718
		internal MsmqTransportSecurity TransportSecurity
		{
			get
			{
				return this.transportSecurity;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x00141520 File Offset: 0x0013F720
		internal bool UseMsmqTracing
		{
			get
			{
				return this.useMsmqTracing;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x00141528 File Offset: 0x0013F728
		internal bool UseSourceJournal
		{
			get
			{
				return this.useSourceJournal;
			}
		}

		// Token: 0x040035D4 RID: 13780
		private MsmqUri.IAddressTranslator addressTranslator;

		// Token: 0x040035D5 RID: 13781
		private bool durable;

		// Token: 0x040035D6 RID: 13782
		private bool exactlyOnce;

		// Token: 0x040035D7 RID: 13783
		private int maxRetryCycles;

		// Token: 0x040035D8 RID: 13784
		private ReceiveErrorHandling receiveErrorHandling;

		// Token: 0x040035D9 RID: 13785
		private int receiveRetryCount;

		// Token: 0x040035DA RID: 13786
		private TimeSpan retryCycleDelay;

		// Token: 0x040035DB RID: 13787
		private MsmqTransportSecurity transportSecurity;

		// Token: 0x040035DC RID: 13788
		private MsmqReceiveContextSettings receiveContextSettings;

		// Token: 0x040035DD RID: 13789
		private bool useMsmqTracing;

		// Token: 0x040035DE RID: 13790
		private bool useSourceJournal;
	}
}

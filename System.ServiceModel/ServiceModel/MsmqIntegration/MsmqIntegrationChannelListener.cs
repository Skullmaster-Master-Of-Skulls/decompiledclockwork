using System;
using System.ServiceModel.Channels;
using System.Xml.Serialization;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B0 RID: 944
	internal sealed class MsmqIntegrationChannelListener : MsmqInputChannelListenerBase
	{
		// Token: 0x0600235A RID: 9050 RVA: 0x00081734 File Offset: 0x0007F934
		internal MsmqIntegrationChannelListener(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters) : base(bindingElement, context, receiveParameters, null)
		{
			base.SetSecurityTokenAuthenticator(MsmqUri.FormatNameAddressTranslator.Scheme, context);
			MsmqIntegrationReceiveParameters msmqIntegrationReceiveParameters = receiveParameters as MsmqIntegrationReceiveParameters;
			this.xmlSerializerList = XmlSerializer.FromTypes(msmqIntegrationReceiveParameters.TargetSerializationTypes);
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x00081774 File Offset: 0x0007F974
		public override string Scheme
		{
			get
			{
				return "msmq.formatname";
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x0008177B File Offset: 0x0007F97B
		internal XmlSerializer[] XmlSerializerList
		{
			get
			{
				return this.xmlSerializerList;
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00081783 File Offset: 0x0007F983
		protected override IInputChannel CreateInputChannel(MsmqInputChannelListenerBase listener)
		{
			return new MsmqIntegrationInputChannel(this);
		}

		// Token: 0x04001FE9 RID: 8169
		private XmlSerializer[] xmlSerializerList;
	}
}

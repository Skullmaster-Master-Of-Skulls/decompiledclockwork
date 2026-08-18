using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.ServiceModel.Activation;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A3 RID: 2211
	public class NamedPipeTransportBindingElement : ConnectionOrientedTransportBindingElement
	{
		// Token: 0x0600544E RID: 21582 RVA: 0x00136782 File Offset: 0x00134982
		public NamedPipeTransportBindingElement()
		{
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x001367AC File Offset: 0x001349AC
		protected NamedPipeTransportBindingElement(NamedPipeTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			if (elementToBeCloned.allowedUsers != null)
			{
				foreach (SecurityIdentifier item in elementToBeCloned.allowedUsers)
				{
					this.allowedUsers.Add(item);
				}
			}
			this.connectionPoolSettings = elementToBeCloned.connectionPoolSettings.Clone();
			this.settings = elementToBeCloned.settings.Clone();
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x00136858 File Offset: 0x00134A58
		// (set) Token: 0x06005451 RID: 21585 RVA: 0x00136860 File Offset: 0x00134A60
		internal List<SecurityIdentifier> AllowedUsers
		{
			get
			{
				return this.allowedUsers;
			}
			set
			{
				this.allowedUsers = value;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06005452 RID: 21586 RVA: 0x00136869 File Offset: 0x00134A69
		public Collection<SecurityIdentifier> AllowedSecurityIdentifiers
		{
			get
			{
				if (this.allowedUsersCollection == null)
				{
					this.allowedUsersCollection = new Collection<SecurityIdentifier>(this.allowedUsers);
				}
				return this.allowedUsersCollection;
			}
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x0013688A File Offset: 0x00134A8A
		public NamedPipeConnectionPoolSettings ConnectionPoolSettings
		{
			get
			{
				return this.connectionPoolSettings;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06005454 RID: 21588 RVA: 0x00136892 File Offset: 0x00134A92
		public NamedPipeSettings PipeSettings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06005455 RID: 21589 RVA: 0x0013689A File Offset: 0x00134A9A
		public override string Scheme
		{
			get
			{
				return "net.pipe";
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06005456 RID: 21590 RVA: 0x001368A1 File Offset: 0x00134AA1
		internal override string WsdlTransportUri
		{
			get
			{
				return "http://schemas.microsoft.com/soap/named-pipe";
			}
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x001368A8 File Offset: 0x00134AA8
		public override BindingElement Clone()
		{
			return new NamedPipeTransportBindingElement(this);
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x001368B0 File Offset: 0x00134AB0
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			return (IChannelFactory<TChannel>)new NamedPipeChannelFactory<TChannel>(this, context);
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x00136914 File Offset: 0x00134B14
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			NamedPipeChannelListener namedPipeChannelListener;
			if (typeof(TChannel) == typeof(IReplyChannel))
			{
				namedPipeChannelListener = new NamedPipeReplyChannelListener(this, context);
			}
			else
			{
				if (!(typeof(TChannel) == typeof(IDuplexSessionChannel)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
					{
						typeof(TChannel)
					}));
				}
				namedPipeChannelListener = new NamedPipeDuplexChannelListener(this, context);
			}
			AspNetEnvironment.Current.ApplyHostedContext(namedPipeChannelListener, context);
			return (IChannelListener<TChannel>)namedPipeChannelListener;
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x001369F4 File Offset: 0x00134BF4
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(IBindingDeliveryCapabilities))
			{
				return (T)((object)new NamedPipeTransportBindingElement.BindingDeliveryCapabilitiesHelper());
			}
			if (typeof(T) == typeof(NamedPipeSettings))
			{
				return (T)((object)this.PipeSettings);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x00136A68 File Offset: 0x00134C68
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			NamedPipeTransportBindingElement namedPipeTransportBindingElement = b as NamedPipeTransportBindingElement;
			return namedPipeTransportBindingElement != null && this.ConnectionPoolSettings.IsMatch(namedPipeTransportBindingElement.ConnectionPoolSettings) && this.PipeSettings.IsMatch(namedPipeTransportBindingElement.PipeSettings);
		}

		// Token: 0x0400330A RID: 13066
		private List<SecurityIdentifier> allowedUsers = new List<SecurityIdentifier>();

		// Token: 0x0400330B RID: 13067
		private Collection<SecurityIdentifier> allowedUsersCollection;

		// Token: 0x0400330C RID: 13068
		private NamedPipeConnectionPoolSettings connectionPoolSettings = new NamedPipeConnectionPoolSettings();

		// Token: 0x0400330D RID: 13069
		private NamedPipeSettings settings = new NamedPipeSettings();

		// Token: 0x02000D79 RID: 3449
		private class BindingDeliveryCapabilitiesHelper : IBindingDeliveryCapabilities
		{
			// Token: 0x06007E66 RID: 32358 RVA: 0x001D7999 File Offset: 0x001D5B99
			internal BindingDeliveryCapabilitiesHelper()
			{
			}

			// Token: 0x17001C25 RID: 7205
			// (get) Token: 0x06007E67 RID: 32359 RVA: 0x001D79A1 File Offset: 0x001D5BA1
			bool IBindingDeliveryCapabilities.AssuresOrderedDelivery
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001C26 RID: 7206
			// (get) Token: 0x06007E68 RID: 32360 RVA: 0x001D79A4 File Offset: 0x001D5BA4
			bool IBindingDeliveryCapabilities.QueuedDelivery
			{
				get
				{
					return false;
				}
			}
		}
	}
}

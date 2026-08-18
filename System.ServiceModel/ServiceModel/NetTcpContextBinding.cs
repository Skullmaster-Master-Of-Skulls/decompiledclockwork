using System;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x0200003B RID: 59
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class NetTcpContextBinding : NetTcpBinding
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00009288 File Offset: 0x00007488
		public NetTcpContextBinding()
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000929E File Offset: 0x0000749E
		public NetTcpContextBinding(SecurityMode securityMode) : base(securityMode)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000092B5 File Offset: 0x000074B5
		public NetTcpContextBinding(string configName)
		{
			if (configName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configName");
			}
			this.ApplyConfiguration(configName);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000092E5 File Offset: 0x000074E5
		public NetTcpContextBinding(SecurityMode securityMode, bool reliableSessionEnabled) : base(securityMode, reliableSessionEnabled)
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009300 File Offset: 0x00007500
		private NetTcpContextBinding(NetTcpBinding netTcpBinding)
		{
			NetTcpContextBinding.NetTcpContextBindingPropertyTransferHelper netTcpContextBindingPropertyTransferHelper = new NetTcpContextBinding.NetTcpContextBindingPropertyTransferHelper();
			netTcpContextBindingPropertyTransferHelper.InitializeFrom(netTcpBinding);
			netTcpContextBindingPropertyTransferHelper.SetBindingElementType(typeof(NetTcpContextBinding));
			netTcpContextBindingPropertyTransferHelper.ApplyConfiguration(this);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009345 File Offset: 0x00007545
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000934D File Offset: 0x0000754D
		[DefaultValue(null)]
		public Uri ClientCallbackAddress { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009356 File Offset: 0x00007556
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000935E File Offset: 0x0000755E
		[DefaultValue(true)]
		public bool ContextManagementEnabled
		{
			get
			{
				return this.contextManagementEnabled;
			}
			set
			{
				this.contextManagementEnabled = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009367 File Offset: 0x00007567
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000936F File Offset: 0x0000756F
		[DefaultValue(ProtectionLevel.Sign)]
		public ProtectionLevel ContextProtectionLevel
		{
			get
			{
				return this.contextProtectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.contextProtectionLevel = value;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009398 File Offset: 0x00007598
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = base.CreateBindingElements();
			bindingElementCollection.Insert(0, new ContextBindingElement(this.ContextProtectionLevel, ContextExchangeMechanism.ContextSoapHeader, this.ClientCallbackAddress, this.ContextManagementEnabled));
			return bindingElementCollection;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000093CC File Offset: 0x000075CC
		internal new static bool TryCreate(BindingElementCollection bindingElements, out Binding binding)
		{
			if (bindingElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			binding = null;
			ContextBindingElement contextBindingElement = bindingElements.Find<ContextBindingElement>();
			if (contextBindingElement != null && contextBindingElement.ContextExchangeMechanism != ContextExchangeMechanism.HttpCookie)
			{
				BindingElementCollection bindingElementCollection = new BindingElementCollection(bindingElements);
				bindingElementCollection.Remove<ContextBindingElement>();
				Binding binding2;
				if (NetTcpBinding.TryCreate(bindingElementCollection, out binding2))
				{
					binding = new NetTcpContextBinding((NetTcpBinding)binding2)
					{
						ContextProtectionLevel = contextBindingElement.ProtectionLevel,
						ContextManagementEnabled = contextBindingElement.ContextManagementEnabled
					};
				}
			}
			return binding != null;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009448 File Offset: 0x00007648
		private void ApplyConfiguration(string configurationName)
		{
			NetTcpContextBindingCollectionElement bindingCollectionElement = NetTcpContextBindingCollectionElement.GetBindingCollectionElement();
			NetTcpContextBindingElement netTcpContextBindingElement = bindingCollectionElement.Bindings[configurationName];
			netTcpContextBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x040001B5 RID: 437
		private bool contextManagementEnabled = true;

		// Token: 0x040001B6 RID: 438
		private ProtectionLevel contextProtectionLevel = ProtectionLevel.Sign;

		// Token: 0x02000ACC RID: 2764
		private class NetTcpContextBindingPropertyTransferHelper : NetTcpBindingElement
		{
			// Token: 0x170019B7 RID: 6583
			// (get) Token: 0x06006E42 RID: 28226 RVA: 0x0019BA2D File Offset: 0x00199C2D
			protected override Type BindingElementType
			{
				get
				{
					return this.bindingElementType;
				}
			}

			// Token: 0x06006E43 RID: 28227 RVA: 0x0019BA35 File Offset: 0x00199C35
			public void SetBindingElementType(Type bindingElementType)
			{
				this.bindingElementType = bindingElementType;
			}

			// Token: 0x04003F08 RID: 16136
			private Type bindingElementType = typeof(NetTcpBinding);
		}
	}
}

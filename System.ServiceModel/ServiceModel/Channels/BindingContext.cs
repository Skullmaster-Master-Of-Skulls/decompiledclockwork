using System;
using System.Globalization;
using System.ServiceModel.Description;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A0 RID: 2464
	[__DynamicallyInvokable]
	public class BindingContext
	{
		// Token: 0x060060B3 RID: 24755 RVA: 0x001699D9 File Offset: 0x00167BD9
		[__DynamicallyInvokable]
		public BindingContext(CustomBinding binding, BindingParameterCollection parameters) : this(binding, parameters, null, string.Empty, ListenUriMode.Explicit)
		{
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x001699EC File Offset: 0x00167BEC
		public BindingContext(CustomBinding binding, BindingParameterCollection parameters, Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (listenUriRelativeAddress == null)
			{
				listenUriRelativeAddress = string.Empty;
			}
			if (!ListenUriModeHelper.IsDefined(listenUriMode))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("listenUriMode"));
			}
			this.Initialize(binding, binding.Elements, parameters, listenUriBaseAddress, listenUriRelativeAddress, listenUriMode);
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x00169A4E File Offset: 0x00167C4E
		private BindingContext(CustomBinding binding, BindingElementCollection remainingBindingElements, BindingParameterCollection parameters, Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode)
		{
			this.Initialize(binding, remainingBindingElements, parameters, listenUriBaseAddress, listenUriRelativeAddress, listenUriMode);
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x00169A65 File Offset: 0x00167C65
		private void Initialize(CustomBinding binding, BindingElementCollection remainingBindingElements, BindingParameterCollection parameters, Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode)
		{
			this.binding = binding;
			this.remainingBindingElements = new BindingElementCollection(remainingBindingElements);
			this.bindingParameters = new BindingParameterCollection(parameters);
			this.listenUriBaseAddress = listenUriBaseAddress;
			this.listenUriRelativeAddress = listenUriRelativeAddress;
			this.listenUriMode = listenUriMode;
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x060060B7 RID: 24759 RVA: 0x00169A9E File Offset: 0x00167C9E
		[__DynamicallyInvokable]
		public CustomBinding Binding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.binding;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x060060B8 RID: 24760 RVA: 0x00169AA6 File Offset: 0x00167CA6
		[__DynamicallyInvokable]
		public BindingParameterCollection BindingParameters
		{
			[__DynamicallyInvokable]
			get
			{
				return this.bindingParameters;
			}
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x060060B9 RID: 24761 RVA: 0x00169AAE File Offset: 0x00167CAE
		// (set) Token: 0x060060BA RID: 24762 RVA: 0x00169AB6 File Offset: 0x00167CB6
		public Uri ListenUriBaseAddress
		{
			get
			{
				return this.listenUriBaseAddress;
			}
			set
			{
				this.listenUriBaseAddress = value;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x060060BB RID: 24763 RVA: 0x00169ABF File Offset: 0x00167CBF
		// (set) Token: 0x060060BC RID: 24764 RVA: 0x00169AC7 File Offset: 0x00167CC7
		public ListenUriMode ListenUriMode
		{
			get
			{
				return this.listenUriMode;
			}
			set
			{
				this.listenUriMode = value;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x060060BD RID: 24765 RVA: 0x00169AD0 File Offset: 0x00167CD0
		// (set) Token: 0x060060BE RID: 24766 RVA: 0x00169AD8 File Offset: 0x00167CD8
		public string ListenUriRelativeAddress
		{
			get
			{
				return this.listenUriRelativeAddress;
			}
			set
			{
				this.listenUriRelativeAddress = value;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x00169AE1 File Offset: 0x00167CE1
		[__DynamicallyInvokable]
		public BindingElementCollection RemainingBindingElements
		{
			[__DynamicallyInvokable]
			get
			{
				return this.remainingBindingElements;
			}
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x00169AE9 File Offset: 0x00167CE9
		[__DynamicallyInvokable]
		public IChannelFactory<TChannel> BuildInnerChannelFactory<TChannel>()
		{
			return this.RemoveNextElement().BuildChannelFactory<TChannel>(this);
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x00169AF7 File Offset: 0x00167CF7
		public IChannelListener<TChannel> BuildInnerChannelListener<TChannel>() where TChannel : class, IChannel
		{
			return this.RemoveNextElement().BuildChannelListener<TChannel>(this);
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x00169B08 File Offset: 0x00167D08
		[__DynamicallyInvokable]
		public bool CanBuildInnerChannelFactory<TChannel>()
		{
			BindingContext bindingContext = this.Clone();
			return bindingContext.RemoveNextElement().CanBuildChannelFactory<TChannel>(bindingContext);
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x00169B28 File Offset: 0x00167D28
		public bool CanBuildInnerChannelListener<TChannel>() where TChannel : class, IChannel
		{
			BindingContext bindingContext = this.Clone();
			return bindingContext.RemoveNextElement().CanBuildChannelListener<TChannel>(bindingContext);
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x00169B48 File Offset: 0x00167D48
		[__DynamicallyInvokable]
		public T GetInnerProperty<T>() where T : class
		{
			if (this.remainingBindingElements.Count == 0)
			{
				return default(T);
			}
			BindingContext bindingContext = this.Clone();
			return bindingContext.RemoveNextElement().GetProperty<T>(bindingContext);
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x00169B7F File Offset: 0x00167D7F
		[__DynamicallyInvokable]
		public BindingContext Clone()
		{
			return new BindingContext(this.binding, this.remainingBindingElements, this.bindingParameters, this.listenUriBaseAddress, this.listenUriRelativeAddress, this.listenUriMode);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x00169BAC File Offset: 0x00167DAC
		private BindingElement RemoveNextElement()
		{
			BindingElement bindingElement = this.remainingBindingElements.Remove<BindingElement>();
			if (bindingElement != null)
			{
				return bindingElement;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoChannelBuilderAvailable", new object[]
			{
				this.binding.Name,
				this.binding.Namespace
			})));
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x00169C08 File Offset: 0x00167E08
		internal void ValidateBindingElementsConsumed()
		{
			if (this.RemainingBindingElements.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (BindingElement bindingElement in this.RemainingBindingElements)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
						stringBuilder.Append(" ");
					}
					string text = bindingElement.GetType().ToString();
					stringBuilder.Append(text.Substring(text.LastIndexOf('.') + 1));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NotAllBindingElementsBuilt", new object[]
				{
					stringBuilder.ToString()
				})));
			}
		}

		// Token: 0x0400389B RID: 14491
		private CustomBinding binding;

		// Token: 0x0400389C RID: 14492
		private BindingParameterCollection bindingParameters;

		// Token: 0x0400389D RID: 14493
		private Uri listenUriBaseAddress;

		// Token: 0x0400389E RID: 14494
		private ListenUriMode listenUriMode;

		// Token: 0x0400389F RID: 14495
		private string listenUriRelativeAddress;

		// Token: 0x040038A0 RID: 14496
		private BindingElementCollection remainingBindingElements;
	}
}

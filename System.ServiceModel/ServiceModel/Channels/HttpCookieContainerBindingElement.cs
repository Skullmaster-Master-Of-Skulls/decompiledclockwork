using System;
using System.ComponentModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C2 RID: 1986
	[Obsolete("This type is obsolete. To enable the Http CookieContainer, use the AllowCookies property on the http binding or on the HttpTransportBindingElement.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class HttpCookieContainerBindingElement : BindingElement
	{
		// Token: 0x06004AE7 RID: 19175 RVA: 0x001128EA File Offset: 0x00110AEA
		[Obsolete("This type is obsolete. To enable the Http CookieContainer, use the AllowCookies property on the http binding or on the HttpTransportBindingElement.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HttpCookieContainerBindingElement()
		{
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x001128F2 File Offset: 0x00110AF2
		[Obsolete("This type is obsolete. To enable the Http CookieContainer, use the AllowCookies property on the http binding or on the HttpTransportBindingElement.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected HttpCookieContainerBindingElement(HttpCookieContainerBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x001128FB File Offset: 0x00110AFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override BindingElement Clone()
		{
			return new HttpCookieContainerBindingElement(this);
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x00112904 File Offset: 0x00110B04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			if (!context.Binding.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) && !context.Binding.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CookieContainerBindingElementNeedsHttp", new object[]
				{
					typeof(HttpCookieContainerBindingElement)
				})));
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x00112998 File Offset: 0x00110B98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.GetInnerProperty<T>();
		}
	}
}

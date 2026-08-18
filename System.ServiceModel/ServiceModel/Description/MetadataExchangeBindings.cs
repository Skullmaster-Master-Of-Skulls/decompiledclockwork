using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Description
{
	// Token: 0x020003EF RID: 1007
	public static class MetadataExchangeBindings
	{
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x0008933A File Offset: 0x0008753A
		internal static Binding Http
		{
			get
			{
				if (MetadataExchangeBindings.httpBinding == null)
				{
					MetadataExchangeBindings.httpBinding = MetadataExchangeBindings.CreateHttpBinding();
				}
				return MetadataExchangeBindings.httpBinding;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00089352 File Offset: 0x00087552
		internal static Binding HttpGet
		{
			get
			{
				if (MetadataExchangeBindings.httpGetBinding == null)
				{
					MetadataExchangeBindings.httpGetBinding = MetadataExchangeBindings.CreateHttpGetBinding();
				}
				return MetadataExchangeBindings.httpGetBinding;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x0008936A File Offset: 0x0008756A
		internal static Binding Https
		{
			get
			{
				if (MetadataExchangeBindings.httpsBinding == null)
				{
					MetadataExchangeBindings.httpsBinding = MetadataExchangeBindings.CreateHttpsBinding();
				}
				return MetadataExchangeBindings.httpsBinding;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00089382 File Offset: 0x00087582
		internal static Binding HttpsGet
		{
			get
			{
				if (MetadataExchangeBindings.httpsGetBinding == null)
				{
					MetadataExchangeBindings.httpsGetBinding = MetadataExchangeBindings.CreateHttpsGetBinding();
				}
				return MetadataExchangeBindings.httpsGetBinding;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x0008939A File Offset: 0x0008759A
		internal static Binding Tcp
		{
			get
			{
				if (MetadataExchangeBindings.tcpBinding == null)
				{
					MetadataExchangeBindings.tcpBinding = MetadataExchangeBindings.CreateTcpBinding();
				}
				return MetadataExchangeBindings.tcpBinding;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000893B2 File Offset: 0x000875B2
		internal static Binding NamedPipe
		{
			get
			{
				if (MetadataExchangeBindings.pipeBinding == null)
				{
					MetadataExchangeBindings.pipeBinding = MetadataExchangeBindings.CreateNamedPipeBinding();
				}
				return MetadataExchangeBindings.pipeBinding;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000893CA File Offset: 0x000875CA
		public static Binding CreateMexHttpBinding()
		{
			return MetadataExchangeBindings.CreateHttpBinding();
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000893D1 File Offset: 0x000875D1
		public static Binding CreateMexHttpsBinding()
		{
			return MetadataExchangeBindings.CreateHttpsBinding();
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000893D8 File Offset: 0x000875D8
		public static Binding CreateMexTcpBinding()
		{
			return MetadataExchangeBindings.CreateTcpBinding();
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000893DF File Offset: 0x000875DF
		public static Binding CreateMexNamedPipeBinding()
		{
			return MetadataExchangeBindings.CreateNamedPipeBinding();
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000893E8 File Offset: 0x000875E8
		internal static Binding GetBindingForScheme(string scheme)
		{
			Binding result = null;
			MetadataExchangeBindings.TryGetBindingForScheme(scheme, out result);
			return result;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00089404 File Offset: 0x00087604
		internal static bool TryGetBindingForScheme(string scheme, out Binding binding)
		{
			if (string.Compare(scheme, "http", StringComparison.OrdinalIgnoreCase) == 0)
			{
				binding = MetadataExchangeBindings.Http;
			}
			else if (string.Compare(scheme, "https", StringComparison.OrdinalIgnoreCase) == 0)
			{
				binding = MetadataExchangeBindings.Https;
			}
			else if (string.Compare(scheme, "net.tcp", StringComparison.OrdinalIgnoreCase) == 0)
			{
				binding = MetadataExchangeBindings.Tcp;
			}
			else if (string.Compare(scheme, "net.pipe", StringComparison.OrdinalIgnoreCase) == 0)
			{
				binding = MetadataExchangeBindings.NamedPipe;
			}
			else
			{
				binding = null;
			}
			return binding != null;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00089478 File Offset: 0x00087678
		private static WSHttpBinding CreateHttpBinding()
		{
			return new WSHttpBinding(SecurityMode.None, false)
			{
				Name = "MetadataExchangeHttpBinding",
				Namespace = "http://schemas.microsoft.com/ws/2005/02/mex/bindings"
			};
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000894A4 File Offset: 0x000876A4
		private static WSHttpBinding CreateHttpsBinding()
		{
			return new WSHttpBinding(new WSHttpSecurity(SecurityMode.Transport, new HttpTransportSecurity(), new NonDualMessageSecurityOverHttp()), false)
			{
				Name = "MetadataExchangeHttpsBinding",
				Namespace = "http://schemas.microsoft.com/ws/2005/02/mex/bindings"
			};
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000894DF File Offset: 0x000876DF
		private static CustomBinding CreateHttpGetBinding()
		{
			return MetadataExchangeBindings.CreateGetBinding(new HttpTransportBindingElement());
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000894EB File Offset: 0x000876EB
		private static CustomBinding CreateHttpsGetBinding()
		{
			return MetadataExchangeBindings.CreateGetBinding(new HttpsTransportBindingElement());
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000894F8 File Offset: 0x000876F8
		private static CustomBinding CreateGetBinding(HttpTransportBindingElement httpTransport)
		{
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
			textMessageEncodingBindingElement.MessageVersion = MessageVersion.None;
			httpTransport.Method = "GET";
			httpTransport.InheritBaseAddressSettings = true;
			return new CustomBinding(new BindingElement[]
			{
				textMessageEncodingBindingElement,
				httpTransport
			});
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0008953C File Offset: 0x0008773C
		private static CustomBinding CreateTcpBinding()
		{
			CustomBinding customBinding = new CustomBinding("MetadataExchangeTcpBinding", "http://schemas.microsoft.com/ws/2005/02/mex/bindings", new BindingElement[0]);
			TcpTransportBindingElement item = new TcpTransportBindingElement();
			customBinding.Elements.Add(item);
			return customBinding;
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00089574 File Offset: 0x00087774
		private static CustomBinding CreateNamedPipeBinding()
		{
			CustomBinding customBinding = new CustomBinding("MetadataExchangeNamedPipeBinding", "http://schemas.microsoft.com/ws/2005/02/mex/bindings", new BindingElement[0]);
			NamedPipeTransportBindingElement item = new NamedPipeTransportBindingElement();
			customBinding.Elements.Add(item);
			return customBinding;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000895AC File Offset: 0x000877AC
		internal static bool IsSchemeSupported(string scheme)
		{
			Binding binding;
			return MetadataExchangeBindings.TryGetBindingForScheme(scheme, out binding);
		}

		// Token: 0x0400216A RID: 8554
		private static Binding httpBinding;

		// Token: 0x0400216B RID: 8555
		private static Binding httpGetBinding;

		// Token: 0x0400216C RID: 8556
		private static Binding httpsBinding;

		// Token: 0x0400216D RID: 8557
		private static Binding httpsGetBinding;

		// Token: 0x0400216E RID: 8558
		private static Binding tcpBinding;

		// Token: 0x0400216F RID: 8559
		private static Binding pipeBinding;
	}
}

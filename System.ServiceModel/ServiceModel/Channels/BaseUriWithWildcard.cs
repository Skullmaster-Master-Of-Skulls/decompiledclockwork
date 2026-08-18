using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000820 RID: 2080
	[DataContract]
	internal sealed class BaseUriWithWildcard
	{
		// Token: 0x06004DBA RID: 19898 RVA: 0x0011BEA0 File Offset: 0x0011A0A0
		public BaseUriWithWildcard(Uri baseAddress, HostNameComparisonMode hostNameComparisonMode)
		{
			this.baseAddress = baseAddress;
			this.hostNameComparisonMode = hostNameComparisonMode;
			this.SetComparisonAddressAndHashCode();
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x0011BEBC File Offset: 0x0011A0BC
		private BaseUriWithWildcard(string protocol, int defaultPort, string binding, int segmentCount, string path, string sampleBinding)
		{
			string[] array = BaseUriWithWildcard.SplitBinding(binding);
			if (array.Length != segmentCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new UriFormatException(SR.GetString("Hosting_MisformattedBinding", new object[]
				{
					binding,
					protocol,
					sampleBinding
				})));
			}
			int num = segmentCount - 1;
			string host = this.ParseHostAndHostNameComparisonMode(array[num]);
			int num2 = -1;
			if (--num >= 0)
			{
				string text = array[num].Trim();
				if (!string.IsNullOrEmpty(text) && !int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new UriFormatException(SR.GetString("Hosting_MisformattedPort", new object[]
					{
						protocol,
						binding,
						text
					})));
				}
				if (num2 == defaultPort)
				{
					num2 = -1;
				}
			}
			try
			{
				this.baseAddress = new UriBuilder(protocol, host, num2, path).Uri;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new UriFormatException(SR.GetString("Hosting_MisformattedBindingData", new object[]
				{
					binding,
					protocol
				})));
			}
			this.SetComparisonAddressAndHashCode();
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x0011BFE8 File Offset: 0x0011A1E8
		internal Uri BaseAddress
		{
			get
			{
				return this.baseAddress;
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x0011BFF0 File Offset: 0x0011A1F0
		internal HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x0011BFF8 File Offset: 0x0011A1F8
		private static string[] SplitBinding(string binding)
		{
			bool flag = false;
			List<int> list = null;
			for (int i = 0; i < binding.Length; i++)
			{
				if (flag && binding[i] == ']')
				{
					flag = false;
				}
				else if (binding[i] == '[')
				{
					flag = true;
				}
				else if (!flag && binding[i] == ':')
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(i);
				}
			}
			string[] array;
			if (list == null)
			{
				array = new string[]
				{
					binding
				};
			}
			else
			{
				array = new string[list.Count + 1];
				int num = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (j < list.Count)
					{
						int num2 = list[j];
						array[j] = binding.Substring(num, num2 - num);
						num = num2 + 1;
					}
					else if (num < binding.Length)
					{
						array[j] = binding.Substring(num, binding.Length - num);
					}
					else
					{
						array[j] = string.Empty;
					}
				}
			}
			return array;
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x0011C0E8 File Offset: 0x0011A2E8
		internal static BaseUriWithWildcard CreateHostedUri(string protocol, string binding, string path)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (path == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("path");
			}
			if (protocol.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
			{
				return new BaseUriWithWildcard(Uri.UriSchemeHttp, 80, binding, 3, path, ":80:");
			}
			if (protocol.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
			{
				return new BaseUriWithWildcard(Uri.UriSchemeHttps, 443, binding, 3, path, ":443:");
			}
			if (protocol.Equals(Uri.UriSchemeNetTcp, StringComparison.OrdinalIgnoreCase))
			{
				return new BaseUriWithWildcard(Uri.UriSchemeNetTcp, 808, binding, 2, path, "808:*");
			}
			if (protocol.Equals(Uri.UriSchemeNetPipe, StringComparison.OrdinalIgnoreCase))
			{
				return BaseUriWithWildcard.CreateHostedPipeUri(binding, path);
			}
			if (protocol.Equals(MsmqUri.NetMsmqAddressTranslator.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				return new BaseUriWithWildcard(MsmqUri.NetMsmqAddressTranslator.Scheme, -1, binding, 1, path, "*");
			}
			if (protocol.Equals(MsmqUri.FormatNameAddressTranslator.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				return new BaseUriWithWildcard(MsmqUri.FormatNameAddressTranslator.Scheme, -1, binding, 1, path, "*");
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new UriFormatException(SR.GetString("Hosting_NotSupportedProtocol", new object[]
			{
				binding
			})));
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x0011C21B File Offset: 0x0011A41B
		internal static BaseUriWithWildcard CreateHostedPipeUri(string binding, string path)
		{
			return new BaseUriWithWildcard(Uri.UriSchemeNetPipe, -1, binding, 1, path, "*");
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x0011C230 File Offset: 0x0011A430
		public override bool Equals(object o)
		{
			BaseUriWithWildcard baseUriWithWildcard = o as BaseUriWithWildcard;
			return baseUriWithWildcard != null && baseUriWithWildcard.hashCode == this.hashCode && baseUriWithWildcard.hostNameComparisonMode == this.hostNameComparisonMode && baseUriWithWildcard.comparand.Port == this.comparand.Port && baseUriWithWildcard.comparand.Scheme == this.comparand.Scheme && this.comparand.Address.Equals(baseUriWithWildcard.comparand.Address);
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x0011C2B2 File Offset: 0x0011A4B2
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0011C2BC File Offset: 0x0011A4BC
		internal bool IsBaseOf(Uri fullAddress)
		{
			if (this.baseAddress.Scheme != fullAddress.Scheme)
			{
				return false;
			}
			if (this.baseAddress.Port != fullAddress.Port)
			{
				return false;
			}
			if (this.HostNameComparisonMode == HostNameComparisonMode.Exact && string.Compare(this.baseAddress.Host, fullAddress.Host, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			string components = this.baseAddress.GetComponents(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
			string components2 = fullAddress.GetComponents(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
			return components.Length <= components2.Length && (components.Length >= components2.Length || components[components.Length - 1] == '/' || components2[components.Length] == '/') && string.Compare(components2, 0, components, 0, components.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x0011C38B File Offset: 0x0011A58B
		[OnDeserialized]
		internal void OnDeserialized(StreamingContext context)
		{
			UriSchemeKeyedCollection.ValidateBaseAddress(this.baseAddress, "context");
			if (!HostNameComparisonModeHelper.IsDefined(this.HostNameComparisonMode))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("context", SR.GetString("Hosting_BaseUriDeserializedNotValid"));
			}
			this.SetComparisonAddressAndHashCode();
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0011C3CC File Offset: 0x0011A5CC
		private string ParseHostAndHostNameComparisonMode(string host)
		{
			if (string.IsNullOrEmpty(host) || host.Equals("*") || host.StartsWith("*."))
			{
				this.hostNameComparisonMode = HostNameComparisonMode.WeakWildcard;
				host = DnsCache.MachineName;
			}
			else if (host.Equals("+"))
			{
				this.hostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
				host = DnsCache.MachineName;
			}
			else
			{
				this.hostNameComparisonMode = HostNameComparisonMode.Exact;
			}
			return host;
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x0011C430 File Offset: 0x0011A630
		private void SetComparisonAddressAndHashCode()
		{
			if (this.HostNameComparisonMode == HostNameComparisonMode.Exact)
			{
				this.comparand.Address = this.baseAddress.ToString();
			}
			else
			{
				this.comparand.Address = this.baseAddress.GetComponents(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
			}
			this.comparand.Port = this.baseAddress.Port;
			this.comparand.Scheme = this.baseAddress.Scheme;
			if (this.comparand.Port == -1 && this.comparand.Scheme == Uri.UriSchemeNetTcp)
			{
				this.comparand.Port = 808;
			}
			this.hashCode = (this.comparand.Address.GetHashCode() ^ this.comparand.Port ^ (int)this.HostNameComparisonMode);
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x0011C4FF File Offset: 0x0011A6FF
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				this.HostNameComparisonMode,
				this.BaseAddress
			});
		}

		// Token: 0x0400309D RID: 12445
		[DataMember]
		private Uri baseAddress;

		// Token: 0x0400309E RID: 12446
		private const char segmentDelimiter = '/';

		// Token: 0x0400309F RID: 12447
		[DataMember]
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x040030A0 RID: 12448
		private const string plus = "+";

		// Token: 0x040030A1 RID: 12449
		private const string star = "*";

		// Token: 0x040030A2 RID: 12450
		private const string starDot = "*.";

		// Token: 0x040030A3 RID: 12451
		private const int HttpUriDefaultPort = 80;

		// Token: 0x040030A4 RID: 12452
		private const int HttpsUriDefaultPort = 443;

		// Token: 0x040030A5 RID: 12453
		private BaseUriWithWildcard.Comparand comparand;

		// Token: 0x040030A6 RID: 12454
		private int hashCode;

		// Token: 0x02000D23 RID: 3363
		private struct Comparand
		{
			// Token: 0x040046FC RID: 18172
			public string Address;

			// Token: 0x040046FD RID: 18173
			public int Port;

			// Token: 0x040046FE RID: 18174
			public string Scheme;
		}
	}
}

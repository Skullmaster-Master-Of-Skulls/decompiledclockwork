using System;
using System.Globalization;
using System.Net;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008FD RID: 2301
	internal static class MsmqUri
	{
		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x060057D8 RID: 22488 RVA: 0x00142AE4 File Offset: 0x00140CE4
		public static MsmqUri.IAddressTranslator NetMsmqAddressTranslator
		{
			get
			{
				if (MsmqUri.netMsmqAddressTranslator == null)
				{
					MsmqUri.netMsmqAddressTranslator = new MsmqUri.NetMsmq();
				}
				return MsmqUri.netMsmqAddressTranslator;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x060057D9 RID: 22489 RVA: 0x00142AFC File Offset: 0x00140CFC
		public static MsmqUri.IAddressTranslator ActiveDirectoryAddressTranslator
		{
			get
			{
				if (MsmqUri.activeDirectoryAddressTranslator == null)
				{
					MsmqUri.activeDirectoryAddressTranslator = new MsmqUri.ActiveDirectory();
				}
				return MsmqUri.activeDirectoryAddressTranslator;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x060057DA RID: 22490 RVA: 0x00142B14 File Offset: 0x00140D14
		public static MsmqUri.IAddressTranslator DeadLetterQueueAddressTranslator
		{
			get
			{
				if (MsmqUri.deadLetterQueueAddressTranslator == null)
				{
					MsmqUri.deadLetterQueueAddressTranslator = new MsmqUri.Dlq();
				}
				return MsmqUri.deadLetterQueueAddressTranslator;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x060057DB RID: 22491 RVA: 0x00142B2C File Offset: 0x00140D2C
		public static MsmqUri.IAddressTranslator SrmpAddressTranslator
		{
			get
			{
				if (MsmqUri.srmpAddressTranslator == null)
				{
					MsmqUri.srmpAddressTranslator = new MsmqUri.Srmp();
				}
				return MsmqUri.srmpAddressTranslator;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x00142B44 File Offset: 0x00140D44
		public static MsmqUri.IAddressTranslator SrmpsAddressTranslator
		{
			get
			{
				if (MsmqUri.srmpsAddressTranslator == null)
				{
					MsmqUri.srmpsAddressTranslator = new MsmqUri.SrmpSecure();
				}
				return MsmqUri.srmpsAddressTranslator;
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x00142B5C File Offset: 0x00140D5C
		public static MsmqUri.IAddressTranslator FormatNameAddressTranslator
		{
			get
			{
				if (MsmqUri.formatnameAddressTranslator == null)
				{
					MsmqUri.formatnameAddressTranslator = new MsmqUri.FormatName();
				}
				return MsmqUri.formatnameAddressTranslator;
			}
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x00142B74 File Offset: 0x00140D74
		public static string UriToFormatNameByScheme(Uri uri)
		{
			if (uri.Scheme == MsmqUri.NetMsmqAddressTranslator.Scheme)
			{
				return MsmqUri.NetMsmqAddressTranslator.UriToFormatName(uri);
			}
			if (uri.Scheme == MsmqUri.FormatNameAddressTranslator.Scheme)
			{
				return MsmqUri.FormatNameAddressTranslator.UriToFormatName(uri);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("uri");
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x00142BD8 File Offset: 0x00140DD8
		private static void AppendQueueName(StringBuilder builder, string relativePath, string slash)
		{
			if (relativePath.StartsWith("/private$", StringComparison.OrdinalIgnoreCase))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqWrongPrivateQueueSyntax")));
			}
			if (relativePath.StartsWith("/private", StringComparison.OrdinalIgnoreCase))
			{
				if ("/private".Length == relativePath.Length)
				{
					builder.Append("private$");
					builder.Append(slash);
					relativePath = "/";
				}
				else if ('/' == relativePath["/private".Length])
				{
					builder.Append("private$");
					builder.Append(slash);
					relativePath = relativePath.Substring("/private".Length);
				}
			}
			builder.Append(relativePath.Substring(1));
		}

		// Token: 0x040035FB RID: 13819
		private static MsmqUri.IAddressTranslator netMsmqAddressTranslator;

		// Token: 0x040035FC RID: 13820
		private static MsmqUri.IAddressTranslator activeDirectoryAddressTranslator;

		// Token: 0x040035FD RID: 13821
		private static MsmqUri.IAddressTranslator deadLetterQueueAddressTranslator;

		// Token: 0x040035FE RID: 13822
		private static MsmqUri.IAddressTranslator srmpAddressTranslator;

		// Token: 0x040035FF RID: 13823
		private static MsmqUri.IAddressTranslator srmpsAddressTranslator;

		// Token: 0x04003600 RID: 13824
		private static MsmqUri.IAddressTranslator formatnameAddressTranslator;

		// Token: 0x02000D9E RID: 3486
		internal interface IAddressTranslator
		{
			// Token: 0x17001C3E RID: 7230
			// (get) Token: 0x06007ED5 RID: 32469
			string Scheme { get; }

			// Token: 0x06007ED6 RID: 32470
			string UriToFormatName(Uri uri);

			// Token: 0x06007ED7 RID: 32471
			Uri CreateUri(string host, string name, bool isPrivate);
		}

		// Token: 0x02000D9F RID: 3487
		private class NetMsmq : MsmqUri.IAddressTranslator
		{
			// Token: 0x17001C3F RID: 7231
			// (get) Token: 0x06007ED8 RID: 32472 RVA: 0x001D879C File Offset: 0x001D699C
			public string Scheme
			{
				get
				{
					return "net.msmq";
				}
			}

			// Token: 0x06007ED9 RID: 32473 RVA: 0x001D87A4 File Offset: 0x001D69A4
			public string UriToFormatName(Uri uri)
			{
				if (null == uri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("uri"));
				}
				if (uri.Scheme != this.Scheme)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidScheme"), "uri"));
				}
				if (string.IsNullOrEmpty(uri.Host))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("MsmqWrongUri"));
				}
				if (-1 != uri.Port)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("MsmqUnexpectedPort"));
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("DIRECT=");
				if (string.Compare(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
				{
					stringBuilder.Append("OS:.");
				}
				else
				{
					IPAddress ipaddress = null;
					if (IPAddress.TryParse(uri.Host, out ipaddress))
					{
						stringBuilder.Append("TCP:");
					}
					else
					{
						stringBuilder.Append("OS:");
					}
					stringBuilder.Append(uri.Host);
				}
				stringBuilder.Append("\\");
				MsmqUri.AppendQueueName(stringBuilder, Uri.UnescapeDataString(uri.PathAndQuery), "\\");
				return stringBuilder.ToString();
			}

			// Token: 0x06007EDA RID: 32474 RVA: 0x001D88D4 File Offset: 0x001D6AD4
			public Uri CreateUri(string host, string name, bool isPrivate)
			{
				string text = "/" + name;
				if (isPrivate)
				{
					text = "/private" + text;
				}
				return new UriBuilder(this.Scheme, host, -1, text).Uri;
			}
		}

		// Token: 0x02000DA0 RID: 3488
		private class PathName : MsmqUri.IAddressTranslator
		{
			// Token: 0x17001C40 RID: 7232
			// (get) Token: 0x06007EDC RID: 32476 RVA: 0x001D8917 File Offset: 0x001D6B17
			public string Scheme
			{
				get
				{
					return "net.msmq";
				}
			}

			// Token: 0x06007EDD RID: 32477 RVA: 0x001D8920 File Offset: 0x001D6B20
			public virtual string UriToFormatName(Uri uri)
			{
				if (null == uri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("uri"));
				}
				if (uri.Scheme != this.Scheme)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidScheme"), "uri"));
				}
				if (string.IsNullOrEmpty(uri.Host))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("MsmqWrongUri"));
				}
				if (-1 != uri.Port)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("MsmqUnexpectedPort"));
				}
				uri = this.PostVerify(uri);
				StringBuilder stringBuilder = new StringBuilder();
				if (string.Compare(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
				{
					stringBuilder.Append(".");
				}
				else
				{
					stringBuilder.Append(uri.Host);
				}
				stringBuilder.Append("\\");
				MsmqUri.AppendQueueName(stringBuilder, Uri.UnescapeDataString(uri.PathAndQuery), "\\");
				return stringBuilder.ToString();
			}

			// Token: 0x06007EDE RID: 32478 RVA: 0x001D8A24 File Offset: 0x001D6C24
			public Uri CreateUri(string host, string name, bool isPrivate)
			{
				string text = "/" + name;
				if (isPrivate)
				{
					text = "/private" + text;
				}
				return new UriBuilder(this.Scheme, host, -1, text).Uri;
			}

			// Token: 0x06007EDF RID: 32479 RVA: 0x001D8A5F File Offset: 0x001D6C5F
			protected virtual Uri PostVerify(Uri uri)
			{
				return uri;
			}
		}

		// Token: 0x02000DA1 RID: 3489
		private class ActiveDirectory : MsmqUri.PathName
		{
			// Token: 0x06007EE1 RID: 32481 RVA: 0x001D8A6A File Offset: 0x001D6C6A
			public override string UriToFormatName(Uri uri)
			{
				return MsmqFormatName.FromQueuePath(base.UriToFormatName(uri));
			}
		}

		// Token: 0x02000DA2 RID: 3490
		private class Dlq : MsmqUri.PathName
		{
			// Token: 0x06007EE3 RID: 32483 RVA: 0x001D8A80 File Offset: 0x001D6C80
			protected override Uri PostVerify(Uri uri)
			{
				if (string.Compare(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return uri;
				}
				try
				{
					if (string.Compare(DnsCache.MachineName, DnsCache.Resolve(uri).HostName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new UriBuilder(base.Scheme, "localhost", -1, uri.PathAndQuery).Uri;
					}
				}
				catch (EndpointNotFoundException ex)
				{
					MsmqDiagnostics.ExpectedException(ex);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqDLQNotLocal"), "uri"));
			}
		}

		// Token: 0x02000DA3 RID: 3491
		private abstract class SrmpBase : MsmqUri.IAddressTranslator
		{
			// Token: 0x17001C41 RID: 7233
			// (get) Token: 0x06007EE5 RID: 32485 RVA: 0x001D8B20 File Offset: 0x001D6D20
			public string Scheme
			{
				get
				{
					return "net.msmq";
				}
			}

			// Token: 0x06007EE6 RID: 32486 RVA: 0x001D8B28 File Offset: 0x001D6D28
			public string UriToFormatName(Uri uri)
			{
				if (null == uri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("uri"));
				}
				if (uri.Scheme != this.Scheme)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidScheme"), "uri"));
				}
				if (string.IsNullOrEmpty(uri.Host))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("MsmqWrongUri"));
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("DIRECT=");
				stringBuilder.Append(this.DirectScheme);
				stringBuilder.Append(uri.Host);
				if (-1 != uri.Port)
				{
					stringBuilder.Append(":");
					stringBuilder.Append(uri.Port.ToString(CultureInfo.InvariantCulture));
				}
				string relativePath = Uri.UnescapeDataString(uri.PathAndQuery);
				stringBuilder.Append("/msmq/");
				MsmqUri.AppendQueueName(stringBuilder, relativePath, "/");
				return stringBuilder.ToString();
			}

			// Token: 0x17001C42 RID: 7234
			// (get) Token: 0x06007EE7 RID: 32487
			protected abstract string DirectScheme { get; }

			// Token: 0x06007EE8 RID: 32488 RVA: 0x001D8C2C File Offset: 0x001D6E2C
			public Uri CreateUri(string host, string name, bool isPrivate)
			{
				string text = "/" + name;
				if (isPrivate)
				{
					text = "/private" + text;
				}
				return new UriBuilder(this.Scheme, host, -1, text).Uri;
			}

			// Token: 0x040048DB RID: 18651
			private const string msmqPart = "/msmq/";
		}

		// Token: 0x02000DA4 RID: 3492
		private class Srmp : MsmqUri.SrmpBase
		{
			// Token: 0x17001C43 RID: 7235
			// (get) Token: 0x06007EEA RID: 32490 RVA: 0x001D8C6F File Offset: 0x001D6E6F
			protected override string DirectScheme
			{
				get
				{
					return "http://";
				}
			}
		}

		// Token: 0x02000DA5 RID: 3493
		private class SrmpSecure : MsmqUri.SrmpBase
		{
			// Token: 0x17001C44 RID: 7236
			// (get) Token: 0x06007EEC RID: 32492 RVA: 0x001D8C7E File Offset: 0x001D6E7E
			protected override string DirectScheme
			{
				get
				{
					return "https://";
				}
			}
		}

		// Token: 0x02000DA6 RID: 3494
		private class FormatName : MsmqUri.IAddressTranslator
		{
			// Token: 0x17001C45 RID: 7237
			// (get) Token: 0x06007EEE RID: 32494 RVA: 0x001D8C8D File Offset: 0x001D6E8D
			public string Scheme
			{
				get
				{
					return "msmq.formatname";
				}
			}

			// Token: 0x06007EEF RID: 32495 RVA: 0x001D8C94 File Offset: 0x001D6E94
			public string UriToFormatName(Uri uri)
			{
				if (null == uri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("uri"));
				}
				if (uri.Scheme != this.Scheme)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidScheme"), "uri"));
				}
				return Uri.UnescapeDataString(uri.AbsoluteUri.Substring(this.Scheme.Length + 1));
			}

			// Token: 0x06007EF0 RID: 32496 RVA: 0x001D8D10 File Offset: 0x001D6F10
			public Uri CreateUri(string host, string name, bool isPrivate)
			{
				string text;
				if (isPrivate)
				{
					text = "PRIVATE$\\" + name;
				}
				else
				{
					text = name;
				}
				text = "DIRECT=OS:" + host + "\\" + text;
				return new Uri(this.Scheme + ":" + text);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000209 RID: 521
	internal class ServiceNameStore
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x00066525 File Offset: 0x00064725
		public ServiceNameCollection ServiceNames
		{
			get
			{
				if (this.serviceNameCollection == null)
				{
					this.serviceNameCollection = new ServiceNameCollection(this.serviceNames);
				}
				return this.serviceNameCollection;
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00066546 File Offset: 0x00064746
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00066560 File Offset: 0x00064760
		private bool AddSingleServiceName(string spn)
		{
			spn = ServiceNameCollection.NormalizeServiceName(spn);
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00066584 File Offset: 0x00064784
		public bool Add(string uriPrefix)
		{
			string[] array = this.BuildServiceNames(uriPrefix);
			bool flag = false;
			foreach (string text in array)
			{
				if (this.AddSingleServiceName(text))
				{
					flag = true;
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Add() " + SR.GetString("net_log_listener_spn_add", new object[]
						{
							text,
							uriPrefix
						}));
					}
				}
			}
			if (flag)
			{
				this.serviceNameCollection = null;
			}
			else if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Add() " + SR.GetString("net_log_listener_spn_not_add", new object[]
				{
					uriPrefix
				}));
			}
			return flag;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00066644 File Offset: 0x00064844
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
			text = ServiceNameCollection.NormalizeServiceName(text);
			bool flag = this.Contains(text);
			if (flag)
			{
				this.serviceNames.Remove(text);
				this.serviceNameCollection = null;
			}
			if (Logging.On)
			{
				if (flag)
				{
					Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Remove() " + SR.GetString("net_log_listener_spn_remove", new object[]
					{
						text,
						uriPrefix
					}));
				}
				else
				{
					Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Remove() " + SR.GetString("net_log_listener_spn_not_remove", new object[]
					{
						uriPrefix
					}));
				}
			}
			return flag;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000666F6 File Offset: 0x000648F6
		private bool Contains(string newServiceName)
		{
			return newServiceName != null && ServiceNameCollection.Contains(newServiceName, this.serviceNames);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00066709 File Offset: 0x00064909
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00066720 File Offset: 0x00064920
		private string ExtractHostname(string uriPrefix, bool allowInvalidUriStrings)
		{
			if (Uri.IsWellFormedUriString(uriPrefix, UriKind.Absolute))
			{
				Uri uri = new Uri(uriPrefix);
				return uri.Host;
			}
			if (allowInvalidUriStrings)
			{
				int num = uriPrefix.IndexOf("://") + 3;
				int num2 = num;
				bool flag = false;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				return uriPrefix.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000667B8 File Offset: 0x000649B8
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000667E0 File Offset: 0x000649E0
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) == 0 || IPAddress.TryParse(text, out ipaddress))
			{
				try
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					return new string[]
					{
						"HTTP/" + hostName
					};
				}
				catch (SocketException)
				{
					return new string[0];
				}
				catch (SecurityException)
				{
					return new string[0];
				}
			}
			if (!text.Contains("."))
			{
				try
				{
					string hostName2 = Dns.GetHostEntry(text).HostName;
					return new string[]
					{
						"HTTP/" + text,
						"HTTP/" + hostName2
					};
				}
				catch (SocketException)
				{
					return new string[]
					{
						"HTTP/" + text
					};
				}
				catch (SecurityException)
				{
					return new string[]
					{
						"HTTP/" + text
					};
				}
			}
			return new string[]
			{
				"HTTP/" + text
			};
		}

		// Token: 0x04001568 RID: 5480
		private List<string> serviceNames;

		// Token: 0x04001569 RID: 5481
		private ServiceNameCollection serviceNameCollection;
	}
}

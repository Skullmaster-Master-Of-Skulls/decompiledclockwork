using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000538 RID: 1336
	internal class ServiceNameStore
	{
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000A9A19 File Offset: 0x000A8A19
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

		// Token: 0x060028DF RID: 10463 RVA: 0x000A9A3A File Offset: 0x000A8A3A
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000A9A54 File Offset: 0x000A8A54
		private bool AddSingleServiceName(string spn)
		{
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000A9A70 File Offset: 0x000A8A70
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
						Logging.PrintInfo(Logging.HttpListener, string.Concat(new string[]
						{
							"ServiceNameStore#",
							ValidationHelper.HashString(this),
							"::Add() adding default SPNs '",
							text,
							"' from prefix '",
							uriPrefix,
							"'"
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
				Logging.PrintInfo(Logging.HttpListener, string.Concat(new string[]
				{
					"ServiceNameStore#",
					ValidationHelper.HashString(this),
					"::Add() no default SPN added for prefix '",
					uriPrefix,
					"'"
				}));
			}
			return flag;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000A9B5C File Offset: 0x000A8B5C
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
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
					Logging.PrintInfo(Logging.HttpListener, string.Concat(new string[]
					{
						"ServiceNameStore#",
						ValidationHelper.HashString(this),
						"::Remove() removing default SPN '",
						text,
						"' from prefix '",
						uriPrefix,
						"'"
					}));
				}
				else
				{
					Logging.PrintInfo(Logging.HttpListener, string.Concat(new string[]
					{
						"ServiceNameStore#",
						ValidationHelper.HashString(this),
						"::Remove() no default SPN removed for prefix '",
						uriPrefix,
						"'"
					}));
				}
			}
			return flag;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000A9C24 File Offset: 0x000A8C24
		private bool Contains(string newServiceName)
		{
			if (newServiceName == null)
			{
				return false;
			}
			bool result = false;
			foreach (string strA in this.serviceNames)
			{
				if (string.Compare(strA, newServiceName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000A9C88 File Offset: 0x000A8C88
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000A9C9C File Offset: 0x000A8C9C
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

		// Token: 0x060028E6 RID: 10470 RVA: 0x000A9D30 File Offset: 0x000A8D30
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000A9D58 File Offset: 0x000A8D58
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) != 0 && string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				if (!IPAddress.TryParse(text, out ipaddress))
				{
					goto IL_7D;
				}
			}
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
			IL_7D:
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

		// Token: 0x040027C9 RID: 10185
		private List<string> serviceNames;

		// Token: 0x040027CA RID: 10186
		private ServiceNameCollection serviceNameCollection;
	}
}

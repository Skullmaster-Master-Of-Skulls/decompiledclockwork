using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x02000446 RID: 1094
	[Serializable]
	public class ServiceNameCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600289D RID: 10397 RVA: 0x000BA7A0 File Offset: 0x000B89A0
		public ServiceNameCollection(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				string serviceName = (string)obj;
				ServiceNameCollection.AddIfNew(base.InnerList, serviceName);
			}
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000BA810 File Offset: 0x000B8A10
		public ServiceNameCollection Merge(string serviceName)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(base.InnerList);
			ServiceNameCollection.AddIfNew(arrayList, serviceName);
			return new ServiceNameCollection(arrayList);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000BA840 File Offset: 0x000B8A40
		public ServiceNameCollection Merge(IEnumerable serviceNames)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(base.InnerList);
			foreach (object obj in serviceNames)
			{
				ServiceNameCollection.AddIfNew(arrayList, obj as string);
			}
			return new ServiceNameCollection(arrayList);
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000BA8B4 File Offset: 0x000B8AB4
		private static void AddIfNew(ArrayList newServiceNames, string serviceName)
		{
			if (string.IsNullOrEmpty(serviceName))
			{
				throw new ArgumentException(SR.GetString("security_ServiceNameCollection_EmptyServiceName"));
			}
			serviceName = ServiceNameCollection.NormalizeServiceName(serviceName);
			if (!ServiceNameCollection.Contains(serviceName, newServiceNames))
			{
				newServiceNames.Add(serviceName);
			}
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000BA8E8 File Offset: 0x000B8AE8
		internal static bool Contains(string searchServiceName, ICollection serviceNames)
		{
			foreach (object obj in serviceNames)
			{
				string serviceName = (string)obj;
				if (ServiceNameCollection.Match(serviceName, searchServiceName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000BA948 File Offset: 0x000B8B48
		public bool Contains(string searchServiceName)
		{
			string searchServiceName2 = ServiceNameCollection.NormalizeServiceName(searchServiceName);
			return ServiceNameCollection.Contains(searchServiceName2, base.InnerList);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000BA968 File Offset: 0x000B8B68
		internal static string NormalizeServiceName(string inputServiceName)
		{
			if (string.IsNullOrWhiteSpace(inputServiceName))
			{
				return inputServiceName;
			}
			int num = inputServiceName.IndexOf('/');
			if (num < 0)
			{
				return inputServiceName;
			}
			string text = inputServiceName.Substring(0, num + 1);
			string text2 = inputServiceName.Substring(num + 1);
			if (string.IsNullOrWhiteSpace(text2))
			{
				return inputServiceName;
			}
			string text3 = text2;
			string text4 = string.Empty;
			string text5 = string.Empty;
			UriHostNameType uriHostNameType = Uri.CheckHostName(text2);
			if (uriHostNameType == UriHostNameType.Unknown)
			{
				string text6 = text2;
				int num2 = text2.IndexOf('/');
				if (num2 >= 0)
				{
					text6 = text2.Substring(0, num2);
					text5 = text2.Substring(num2);
					text3 = text6;
				}
				int num3 = text6.LastIndexOf(':');
				if (num3 >= 0)
				{
					text3 = text6.Substring(0, num3);
					text4 = text6.Substring(num3 + 1);
					ushort num4;
					if (!ushort.TryParse(text4, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4))
					{
						return inputServiceName;
					}
					text4 = text6.Substring(num3);
				}
				uriHostNameType = Uri.CheckHostName(text3);
			}
			if (uriHostNameType != UriHostNameType.Dns)
			{
				return inputServiceName;
			}
			Uri uri;
			if (!Uri.TryCreate(Uri.UriSchemeHttp + Uri.SchemeDelimiter + text3, UriKind.Absolute, out uri))
			{
				return inputServiceName;
			}
			string components = uri.GetComponents(UriComponents.NormalizedHost, UriFormat.SafeUnescaped);
			string text7 = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[]
			{
				text,
				components,
				text4,
				text5
			});
			if (ServiceNameCollection.Match(inputServiceName, text7))
			{
				return inputServiceName;
			}
			return text7;
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000BAAAB File Offset: 0x000B8CAB
		internal static bool Match(string serviceName1, string serviceName2)
		{
			return string.Compare(serviceName1, serviceName2, StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
}

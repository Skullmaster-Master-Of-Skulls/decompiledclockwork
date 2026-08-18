using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A00 RID: 2560
	internal static class PeerMessageHelpers
	{
		// Token: 0x060065AB RID: 26027 RVA: 0x0017AEF0 File Offset: 0x001790F0
		public static string GetHeaderString(MessageHeaders headers, string name, string ns)
		{
			string result = null;
			int num = headers.FindHeader(name, ns);
			if (num >= 0)
			{
				using (XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(num))
				{
					result = readerAtHeader.ReadElementString();
				}
				headers.UnderstoodHeaders.Add(headers[num]);
			}
			return result;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x0017AF4C File Offset: 0x0017914C
		public static Uri GetHeaderUri(MessageHeaders headers, string name, string ns)
		{
			Uri result = null;
			string headerString = PeerMessageHelpers.GetHeaderString(headers, name, ns);
			if (headerString != null)
			{
				result = new Uri(headerString);
			}
			return result;
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x0017AF70 File Offset: 0x00179170
		public static ulong GetHeaderULong(MessageHeaders headers, int index)
		{
			ulong result = ulong.MaxValue;
			if (index >= 0)
			{
				using (XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(index))
				{
					result = XmlConvert.ToUInt64(readerAtHeader.ReadElementString());
				}
				headers.UnderstoodHeaders.Add(headers[index]);
			}
			return result;
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x0017AFC8 File Offset: 0x001791C8
		public static UniqueId GetHeaderUniqueId(MessageHeaders headers, string name, string ns)
		{
			UniqueId result = null;
			int num = headers.FindHeader(name, ns);
			if (num >= 0)
			{
				using (XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(num))
				{
					result = readerAtHeader.ReadElementContentAsUniqueId();
				}
				headers.UnderstoodHeaders.Add(headers[num]);
			}
			return result;
		}

		// Token: 0x02000E5C RID: 3676
		// (Invoke) Token: 0x06008353 RID: 33619
		public delegate void CleanupCallback(IPeerNeighbor neighbor, PeerCloseReason reason, Exception exception);
	}
}

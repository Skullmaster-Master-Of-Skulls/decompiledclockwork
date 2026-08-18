using System;
using System.Linq;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005EC RID: 1516
	public static class eBindingTypeAdapter
	{
		// Token: 0x060030CA RID: 12490 RVA: 0x00042844 File Offset: 0x00040A44
		public static eBindingType GetBindingType(this Uri uri)
		{
			string text = uri.Scheme.Split(new char[]
			{
				':'
			}).FirstOrDefault<string>();
			bool flag = string.IsNullOrEmpty(text);
			eBindingType result;
			if (flag)
			{
				result = eBindingType.Unspecified;
			}
			else
			{
				text = text.Trim().ToLower();
				bool flag2 = text.Equals("http") || text.Equals("https");
				if (flag2)
				{
					result = eBindingType.HttpBinding;
				}
				else
				{
					bool flag3 = text.Equals("net.tcp");
					if (flag3)
					{
						result = eBindingType.NetTcpBinding;
					}
					else
					{
						bool flag4 = text.Equals("net.pipe");
						if (flag4)
						{
							result = eBindingType.NetPipeBinding;
						}
						else
						{
							bool flag5 = text.Equals("net.msmq");
							if (flag5)
							{
								result = eBindingType.MsmqBinding;
							}
							else
							{
								result = eBindingType.Unspecified;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000428F0 File Offset: 0x00040AF0
		public static string GetUriScheme(this eBindingType binding)
		{
			string result;
			switch (binding)
			{
			case eBindingType.NetTcpBinding:
				result = "net.tcp";
				break;
			case eBindingType.HttpBinding:
				result = "http";
				break;
			case eBindingType.MsmqBinding:
				result = "net.msmq";
				break;
			case eBindingType.NetPipeBinding:
				result = "net.pipe";
				break;
			default:
				result = string.Empty;
				break;
			}
			return result;
		}
	}
}

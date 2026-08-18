using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public class ClientParameters : Dictionary<string, string>
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0000E4FB File Offset: 0x0000C6FB
		public ClientParameters()
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000E505 File Offset: 0x0000C705
		protected ClientParameters(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000E511 File Offset: 0x0000C711
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000E518 File Offset: 0x0000C718
		public static ClientParameters DefaultInstance { get; set; }

		// Token: 0x0600055F RID: 1375 RVA: 0x0000E520 File Offset: 0x0000C720
		static ClientParameters()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			ClientParameters.DefaultInstance = new ClientParameters
			{
				{
					"IP",
					TechnoPro.Common.Win32.Environment.GetIPAddress()
				},
				{
					"APPNAME",
					(entryAssembly != null) ? entryAssembly.GetName().Name : "ClockWorkServer"
				},
				{
					"ADDR_SIZE",
					(IntPtr.Size == 4) ? "32" : "64"
				},
				{
					"NET_VERSIONS",
					TechnoPro.Common.Win32.Environment.GetDotNetVersionsInstalled().CommaSeparatedValuesWithoutSpace<DotNetVersion>()
				}
			};
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = base.ContainsKey("IP");
			if (flag)
			{
				stringBuilder.AppendFormat("{0}:", base["IP"]);
			}
			bool flag2 = base.ContainsKey("APPNAME");
			if (flag2)
			{
				stringBuilder.AppendFormat("{0}:", base["APPNAME"]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000E61C File Offset: 0x0000C81C
		public override int GetHashCode()
		{
			string text = this.ToString();
			return string.IsNullOrEmpty(text) ? base.GetHashCode() : text.GetHashCode();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000E64C File Offset: 0x0000C84C
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && this.MatchingHashCodes(obj);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000E680 File Offset: 0x0000C880
		private bool MatchingHashCodes(object obj)
		{
			return this.GetHashCode().Equals(obj.GetHashCode());
		}
	}
}

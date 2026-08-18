using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using \u0008;

namespace \u0005
{
	// Token: 0x0200036A RID: 874
	internal class \u0001
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x001265FC File Offset: 0x001247FC
		public static string \u0001(int \u0002)
		{
			\u0002 -= global::\u0005.\u0001.\u0006;
			if (global::\u0005.\u0001.\u0005)
			{
				string text;
				global::\u0005.\u0001.\u0004.TryGetValue(\u0002, out text);
				if (text != null)
				{
					return text;
				}
			}
			int index = \u0002;
			int num = (int)global::\u0005.\u0001.\u0003[index++];
			int num2;
			if ((num & 128) == 0)
			{
				num2 = num;
				if (num2 == 0)
				{
					return string.Empty;
				}
			}
			else if ((num & 64) == 0)
			{
				num2 = ((num & 63) << 8) + (int)global::\u0005.\u0001.\u0003[index++];
			}
			else
			{
				num2 = ((num & 31) << 24) + ((int)global::\u0005.\u0001.\u0003[index++] << 16) + ((int)global::\u0005.\u0001.\u0003[index++] << 8) + (int)global::\u0005.\u0001.\u0003[index++];
			}
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(Encoding.UTF8.GetString(global::\u0005.\u0001.\u0003, index, num2));
				string text2 = string.Intern(Encoding.UTF8.GetString(array, 0, array.Length));
				if (global::\u0005.\u0001.\u0005)
				{
					try
					{
						global::\u0005.\u0001.\u0004.Add(\u0002, text2);
					}
					catch
					{
					}
				}
				result = text2;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00126710 File Offset: 0x00124910
		static \u0001()
		{
			if (global::\u0005.\u0001.\u0001 == "1")
			{
				global::\u0005.\u0001.\u0005 = true;
				global::\u0005.\u0001.\u0004 = new Dictionary<int, string>();
			}
			global::\u0005.\u0001.\u0006 = Convert.ToInt32(global::\u0005.\u0001.\u0002);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("{74ed86c6-a7a9-42ec-a453-ce00850c4697}"))
			{
				int num = Convert.ToInt32(manifestResourceStream.Length);
				byte[] array = new byte[num];
				manifestResourceStream.Read(array, 0, num);
				global::\u0005.\u0001.\u0003 = \u0008.\u0004.\u0001(array);
				manifestResourceStream.Close();
			}
		}

		// Token: 0x0400209F RID: 8351
		private static readonly string \u0001 = "0";

		// Token: 0x040020A0 RID: 8352
		private static readonly string \u0002 = "172";

		// Token: 0x040020A1 RID: 8353
		private static readonly byte[] \u0003 = null;

		// Token: 0x040020A2 RID: 8354
		private static readonly Dictionary<int, string> \u0004;

		// Token: 0x040020A3 RID: 8355
		private static readonly bool \u0005 = false;

		// Token: 0x040020A4 RID: 8356
		private static readonly int \u0006 = 0;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200033E RID: 830
	internal class dr : Dictionary<RtfVisualSpecialCharKind, string>
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x000819C0 File Offset: 0x000809C0
		public dr()
		{
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000819C8 File Offset: 0x000809C8
		public dr(string A_0)
		{
			this.a(A_0);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000819D8 File Offset: 0x000809D8
		public void a(string A_0)
		{
			base.Clear();
			if (string.IsNullOrEmpty(A_0))
			{
				return;
			}
			string[] array = A_0.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length == 2)
				{
					RtfVisualSpecialCharKind key = (RtfVisualSpecialCharKind)Enum.Parse(typeof(RtfVisualSpecialCharKind), array2[0]);
					base.Add(key, array2[1]);
				}
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00081A50 File Offset: 0x00080A50
		public string a()
		{
			if (base.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RtfVisualSpecialCharKind rtfVisualSpecialCharKind in base.Keys)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(Enum.GetName(typeof(RtfVisualSpecialCharKind), rtfVisualSpecialCharKind));
				stringBuilder.Append('=');
				stringBuilder.Append(base[rtfVisualSpecialCharKind]);
			}
			return stringBuilder.ToString();
		}
	}
}

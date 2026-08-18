using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200026B RID: 619
	public class MailAddressCollection : Collection<MailAddress>
	{
		// Token: 0x0600174E RID: 5966 RVA: 0x00076EAC File Offset: 0x000750AC
		public void Add(string addresses)
		{
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"addresses"
				}), "addresses");
			}
			this.ParseValue(addresses);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00076EFE File Offset: 0x000750FE
		protected override void SetItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00076F16 File Offset: 0x00075116
		protected override void InsertItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00076F30 File Offset: 0x00075130
		internal void ParseValue(string addresses)
		{
			IList<MailAddress> list = MailAddressParser.ParseMultipleAddresses(addresses);
			for (int i = 0; i < list.Count; i++)
			{
				base.Add(list[i]);
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00076F64 File Offset: 0x00075164
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailAddress mailAddress in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mailAddress.ToString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00076FD4 File Offset: 0x000751D4
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			foreach (MailAddress mailAddress in this)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = mailAddress.Encode(charsConsumed, allowUnicode);
				}
				else
				{
					text = text + ", " + mailAddress.Encode(1, allowUnicode);
				}
			}
			return text;
		}
	}
}

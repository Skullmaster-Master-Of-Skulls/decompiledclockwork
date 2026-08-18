using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200069A RID: 1690
	public class MailAddressCollection : Collection<MailAddress>
	{
		// Token: 0x0600342E RID: 13358 RVA: 0x000DC0B0 File Offset: 0x000DB0B0
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

		// Token: 0x0600342F RID: 13359 RVA: 0x000DC104 File Offset: 0x000DB104
		protected override void SetItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000DC11C File Offset: 0x000DB11C
		protected override void InsertItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000DC134 File Offset: 0x000DB134
		internal void ParseValue(string addresses)
		{
			for (int i = 0; i < addresses.Length; i++)
			{
				MailAddress mailAddress = MailBnfHelper.ReadMailAddress(addresses, ref i);
				if (mailAddress == null)
				{
					return;
				}
				base.Add(mailAddress);
				if (!MailBnfHelper.SkipCFWS(addresses, ref i))
				{
					break;
				}
				if (addresses[i] != ',')
				{
					return;
				}
			}
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000DC17C File Offset: 0x000DB17C
		internal string ToEncodedString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailAddress mailAddress in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mailAddress.ToEncodedString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000DC1EC File Offset: 0x000DB1EC
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
	}
}

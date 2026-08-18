using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x0200026E RID: 622
	internal static class MailHeaderInfo
	{
		// Token: 0x0600175C RID: 5980 RVA: 0x0007740C File Offset: 0x0007560C
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.m_HeaderInfo.Length; i++)
			{
				MailHeaderInfo.m_HeaderDictionary.Add(MailHeaderInfo.m_HeaderInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00077748 File Offset: 0x00075948
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.m_HeaderInfo[(int)id].NormalizedName;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00077768 File Offset: 0x00075968
		internal static MailHeaderID GetID(string name)
		{
			int result;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out result))
			{
				return (MailHeaderID)result;
			}
			return MailHeaderID.Unknown;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00077788 File Offset: 0x00075988
		internal static bool IsWellKnown(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000777A4 File Offset: 0x000759A4
		internal static bool IsUserSettable(string name)
		{
			int num;
			return !MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) || MailHeaderInfo.m_HeaderInfo[num].IsUserSettable;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000777D4 File Offset: 0x000759D4
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && MailHeaderInfo.m_HeaderInfo[num].IsSingleton;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00077804 File Offset: 0x00075A04
		internal static string NormalizeCase(string name)
		{
			int num;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num))
			{
				return MailHeaderInfo.m_HeaderInfo[num].NormalizedName;
			}
			return name;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00077834 File Offset: 0x00075A34
		internal static bool IsMatch(string name, MailHeaderID header)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && num == (int)header;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00077858 File Offset: 0x00075A58
		internal static bool AllowsUnicode(string name)
		{
			int num;
			return !MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) || MailHeaderInfo.m_HeaderInfo[num].AllowsUnicode;
		}

		// Token: 0x040017D8 RID: 6104
		private static readonly MailHeaderInfo.HeaderInfo[] m_HeaderInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true, true, true)
		};

		// Token: 0x040017D9 RID: 6105
		private static readonly Dictionary<string, int> m_HeaderDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0200079D RID: 1949
		private struct HeaderInfo
		{
			// Token: 0x060042F8 RID: 17144 RVA: 0x00118598 File Offset: 0x00116798
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable, bool allowsUnicode)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
				this.IsUserSettable = isUserSettable;
				this.AllowsUnicode = allowsUnicode;
			}

			// Token: 0x040033AD RID: 13229
			public readonly string NormalizedName;

			// Token: 0x040033AE RID: 13230
			public readonly bool IsSingleton;

			// Token: 0x040033AF RID: 13231
			public readonly MailHeaderID ID;

			// Token: 0x040033B0 RID: 13232
			public readonly bool IsUserSettable;

			// Token: 0x040033B1 RID: 13233
			public readonly bool AllowsUnicode;
		}
	}
}

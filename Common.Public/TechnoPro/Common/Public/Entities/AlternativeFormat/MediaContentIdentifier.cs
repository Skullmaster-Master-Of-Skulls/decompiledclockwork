using System;
using System.Text;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200058D RID: 1421
	public class MediaContentIdentifier : IEquatable<MediaContentIdentifier>
	{
		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x00032A4F File Offset: 0x00030C4F
		// (set) Token: 0x06002E0E RID: 11790 RVA: 0x00032A57 File Offset: 0x00030C57
		public Guid? MediaContentUniqueId { get; set; }

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x00032A60 File Offset: 0x00030C60
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x00032A68 File Offset: 0x00030C68
		public int MediaContentId { get; set; }

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x00032A71 File Offset: 0x00030C71
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x00032A79 File Offset: 0x00030C79
		public string ISBN { get; set; }

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x00032A82 File Offset: 0x00030C82
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x00032A8A File Offset: 0x00030C8A
		public string ExternalId { get; set; }

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x00032A93 File Offset: 0x00030C93
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x00032A9B File Offset: 0x00030C9B
		public string ExternalSourceProvider { get; set; }

		// Token: 0x06002E17 RID: 11799 RVA: 0x00032AA4 File Offset: 0x00030CA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = this.MediaContentUniqueId != null;
			if (flag)
			{
				stringBuilder.Append(this.MediaContentUniqueId.Value);
			}
			bool flag2 = this.MediaContentId > 0;
			if (flag2)
			{
				bool flag3 = stringBuilder.Length > 0;
				if (flag3)
				{
					stringBuilder.AppendFormat("-{0}", this.MediaContentId);
				}
				else
				{
					stringBuilder.Append(this.MediaContentId);
				}
			}
			bool flag4 = !string.IsNullOrEmpty(this.ISBN);
			if (flag4)
			{
				bool flag5 = stringBuilder.Length > 0;
				if (flag5)
				{
					stringBuilder.AppendFormat("-{0}", this.ISBN);
				}
				else
				{
					stringBuilder.Append(this.ISBN);
				}
			}
			bool flag6 = !string.IsNullOrEmpty(this.ExternalId);
			if (flag6)
			{
				bool flag7 = stringBuilder.Length > 0;
				if (flag7)
				{
					stringBuilder.AppendFormat("-{0}", this.ExternalId);
				}
				else
				{
					stringBuilder.Append(this.ExternalId);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x00032BBC File Offset: 0x00030DBC
		public override int GetHashCode()
		{
			return string.Format("{0}:{1}:{2}:{3}", new object[]
			{
				(this.MediaContentUniqueId != null) ? this.MediaContentUniqueId.Value.ToString() : "NULL",
				this.MediaContentId,
				this.ISBN ?? "NULL",
				this.ExternalId ?? "NULL"
			}).GetHashCode();
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00032C4C File Offset: 0x00030E4C
		public bool Equals(MediaContentIdentifier other)
		{
			return other != null && (this.MatchingIds(other) || this.MatchingHashCodes(other));
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x00032C78 File Offset: 0x00030E78
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && (this.MatchingIds((MediaContentIdentifier)obj) || this.MatchingHashCodes(obj));
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x00032CBC File Offset: 0x00030EBC
		protected virtual bool MatchingIds(MediaContentIdentifier obj)
		{
			bool flag = this.MediaContentUniqueId != null && obj.MediaContentUniqueId != null && this.MediaContentUniqueId.Value != Guid.Empty && obj.MediaContentUniqueId.Value != Guid.Empty && this.MediaContentUniqueId.Value.Equals(obj.MediaContentUniqueId.Value);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.MediaContentId > 0 && obj.MediaContentId > 0 && this.MediaContentId == obj.MediaContentId;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(this.ISBN) && !string.IsNullOrEmpty(obj.ISBN) && this.ISBN.Equals(obj.ISBN);
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(this.ExternalId) && !string.IsNullOrEmpty(obj.ExternalId) && !string.IsNullOrEmpty(this.ExternalSourceProvider) && !string.IsNullOrEmpty(obj.ExternalSourceProvider) && this.ExternalSourceProvider.Equals(obj.ExternalSourceProvider) && this.ExternalId.Equals(obj.ExternalId);
						result = flag4;
					}
				}
			}
			return result;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x00032E24 File Offset: 0x00031024
		protected virtual bool MatchingHashCodes(object obj)
		{
			return this.GetHashCode().Equals(obj.GetHashCode());
		}
	}
}

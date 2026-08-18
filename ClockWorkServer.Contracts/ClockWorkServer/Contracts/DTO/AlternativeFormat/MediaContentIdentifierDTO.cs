using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B4A RID: 2890
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaContentIdentifierDTO : IEquatable<MediaContentIdentifierDTO>
	{
		// Token: 0x06003D55 RID: 15701 RVA: 0x000036BD File Offset: 0x000018BD
		public MediaContentIdentifierDTO()
		{
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public MediaContentIdentifierDTO(string identifier)
		{
			string[] array = identifier.Split(new char[]
			{
				'_'
			}, StringSplitOptions.None);
			this.MediaContentUniqueId = (string.IsNullOrEmpty(array[0]) ? null : new Guid?(new Guid(array[0])));
			int mediaContentId;
			bool flag = !string.IsNullOrEmpty(array[1]) && int.TryParse(array[1], out mediaContentId);
			if (flag)
			{
				this.MediaContentId = mediaContentId;
			}
			this.ISBN = array[2];
			this.ExternalId = array[3];
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		// (set) Token: 0x06003D58 RID: 15704 RVA: 0x0001DFCC File Offset: 0x0001C1CC
		[DataMember]
		public Guid? MediaContentUniqueId { get; set; }

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x0001DFD5 File Offset: 0x0001C1D5
		// (set) Token: 0x06003D5A RID: 15706 RVA: 0x0001DFDD File Offset: 0x0001C1DD
		[DataMember]
		public string ExternalId { get; set; }

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x0001DFE6 File Offset: 0x0001C1E6
		// (set) Token: 0x06003D5C RID: 15708 RVA: 0x0001DFEE File Offset: 0x0001C1EE
		[DataMember]
		public int MediaContentId { get; set; }

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x0001DFF7 File Offset: 0x0001C1F7
		// (set) Token: 0x06003D5E RID: 15710 RVA: 0x0001DFFF File Offset: 0x0001C1FF
		[DataMember]
		public string ISBN { get; set; }

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06003D5F RID: 15711 RVA: 0x0001E008 File Offset: 0x0001C208
		// (set) Token: 0x06003D60 RID: 15712 RVA: 0x0001E010 File Offset: 0x0001C210
		[DataMember]
		public string ExternalSourceProvider { get; set; }

		// Token: 0x06003D61 RID: 15713 RVA: 0x0001E01C File Offset: 0x0001C21C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				(this.MediaContentUniqueId != null) ? this.MediaContentUniqueId.ToString() : string.Empty,
				'_',
				this.MediaContentId,
				'_',
				this.ISBN ?? string.Empty,
				'_',
				this.ExternalId ?? string.Empty
			});
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		public bool Equals(MediaContentIdentifierDTO other)
		{
			return other != null && (this.MatchingIds(other) || this.MatchingHashCodes(other));
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0001E104 File Offset: 0x0001C304
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && (this.MatchingIds((MediaContentIdentifierDTO)obj) || this.MatchingHashCodes(obj));
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0001E148 File Offset: 0x0001C348
		protected virtual bool MatchingIds(MediaContentIdentifierDTO obj)
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

		// Token: 0x06003D66 RID: 15718 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		protected virtual bool MatchingHashCodes(object obj)
		{
			return this.GetHashCode().Equals(obj.GetHashCode());
		}

		// Token: 0x0400176C RID: 5996
		private const char Identifier_Delimiter = '_';
	}
}

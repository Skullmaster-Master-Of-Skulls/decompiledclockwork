using System;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters
{
	// Token: 0x0200059C RID: 1436
	public static class MediaContentFormatAdapter
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x000335C4 File Offset: 0x000317C4
		public static string ToDisplayString(this MediaContentFormat mediaContentFormat)
		{
			MediaContentFormatInfoAttribute attribute = mediaContentFormat.GetAttribute<MediaContentFormatInfoAttribute>();
			return ((attribute != null) ? attribute.Title : null) ?? mediaContentFormat.ToString().Replace("_", " ");
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00033610 File Offset: 0x00031810
		public static string GetDefinition(this MediaContentFormat mediaContentFormat)
		{
			MediaContentFormatInfoAttribute attribute = mediaContentFormat.GetAttribute<MediaContentFormatInfoAttribute>();
			return (attribute != null) ? (attribute.Definition ?? string.Empty) : string.Empty;
		}
	}
}

using System;

namespace Telerik.Web.UI.Notification
{
	// Token: 0x0200061E RID: 1566
	public class AudioFormat
	{
		// Token: 0x060038F1 RID: 14577 RVA: 0x000BB479 File Offset: 0x000B9679
		public AudioFormat(AudioFormats format)
		{
			this.Format = format;
			this.ResolveFormatData();
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000BB48E File Offset: 0x000B968E
		// (set) Token: 0x060038F3 RID: 14579 RVA: 0x000BB496 File Offset: 0x000B9696
		public AudioFormats Format { get; private set; }

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000BB49F File Offset: 0x000B969F
		// (set) Token: 0x060038F5 RID: 14581 RVA: 0x000BB4A7 File Offset: 0x000B96A7
		public string FileExtension { get; set; }

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000BB4B0 File Offset: 0x000B96B0
		// (set) Token: 0x060038F7 RID: 14583 RVA: 0x000BB4B8 File Offset: 0x000B96B8
		public string MimeType { get; set; }

		// Token: 0x060038F8 RID: 14584 RVA: 0x000BB4C4 File Offset: 0x000B96C4
		private void ResolveFormatData()
		{
			switch (this.Format)
			{
			case AudioFormats.Mp3:
				this.FileExtension = "mp3";
				this.MimeType = "audio/mpeg";
				return;
			case AudioFormats.Wave:
				this.FileExtension = "wav";
				this.MimeType = "audio/wav";
				return;
			default:
				throw new NotSupportedException("Audio format is not supported");
			}
		}
	}
}

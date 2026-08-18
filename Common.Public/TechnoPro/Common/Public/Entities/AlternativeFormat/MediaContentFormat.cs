using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000579 RID: 1401
	[Serializable]
	public enum MediaContentFormat
	{
		// Token: 0x04001FB3 RID: 8115
		[MediaContentFormatInfo("Unspecified", "")]
		UNSPECIFIED,
		// Token: 0x04001FB4 RID: 8116
		[MediaContentFormatInfo("PDF format", "Portable Document Format. Produced by Adobe Systems.")]
		DIGITAL_TEXTBOOK_PDF,
		// Token: 0x04001FB5 RID: 8117
		[MediaContentFormatInfo("General e-text format", "General e-text files")]
		DIGITAL_TEXTBOOK_ETEXT,
		// Token: 0x04001FB6 RID: 8118
		[MediaContentFormatInfo("ePub format", "Electronic Publication. Free and open e-book standard designed for reflowable content.")]
		DIGITAL_TEXTBOOK_ePub,
		// Token: 0x04001FB7 RID: 8119
		[MediaContentFormatInfo("Wynn format", "Wynn is produced by Literacy Software Solution. Use a bi-modal approach - simultaneous highlighting of the text as it is spoken.")]
		DIGITAL_TEXTBOOK_Wynn,
		// Token: 0x04001FB8 RID: 8120
		[MediaContentFormatInfo("Kurzweil 3000 format", "Kurzweil 3000 file format")]
		DIGITAL_TEXTBOOK_KESI3000,
		// Token: 0x04001FB9 RID: 8121
		[MediaContentFormatInfo("Kurzweil 1000 format", "Kurzweil 1000 file format")]
		DIGITAL_TEXTBOOK_KESI1000,
		// Token: 0x04001FBA RID: 8122
		[MediaContentFormatInfo("Word or RTF format", "RTF or Microsoft Word file format can be used by screen readers like NVDA or JAWS")]
		DIGITAL_TEXTBOOK_Word,
		// Token: 0x04001FBB RID: 8123
		[MediaContentFormatInfo("Open eBook format", "Open eBook Publication Structure. Legacy e-book format superseded by epub format.")]
		DIGITAL_TEXTBOOK_OpenBook,
		// Token: 0x04001FBC RID: 8124
		[MediaContentFormatInfo("DAISY format", "Digital Accessible Information Systems. Include structural elements to allow for improved navigation.")]
		DIGITAL_TEXTBOOK_DAISY,
		// Token: 0x04001FBD RID: 8125
		[MediaContentFormatInfo("Audio (digital) transcription", "Digital audio transcription. Created with either human voice or synthetic speech.")]
		AUDIO_TRANSCRIPTION,
		// Token: 0x04001FBE RID: 8126
		[MediaContentFormatInfo("Video captioning format", "Video captioning format")]
		VIDEO_CAPTIONING,
		// Token: 0x04001FBF RID: 8127
		[MediaContentFormatInfo("Large print format", "Print enlargement on paper. Can be up to two feet wide in color")]
		LARGE_PRINT,
		// Token: 0x04001FC0 RID: 8128
		[MediaContentFormatInfo("Braille format", "A tactile writing system of cells and dots.")]
		BRAILLE,
		// Token: 0x04001FC1 RID: 8129
		[MediaContentFormatInfo("Scanned files format (eg. TIF, JPEG, PNG)", "Scanned files.")]
		DIGITAL_TEXTBOOK_SCANNED,
		// Token: 0x04001FC2 RID: 8130
		[MediaContentFormatInfo("Publisher provided format", "General files by publisher.")]
		DIGITAL_TEXTBOOK_PUBLISHER
	}
}

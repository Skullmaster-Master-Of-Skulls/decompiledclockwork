using System;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;

namespace System.Data.OleDb
{
	// Token: 0x02000287 RID: 647
	internal sealed class ChapterHandle : WrappedIUnknown
	{
		// Token: 0x06002707 RID: 9991 RVA: 0x001086A8 File Offset: 0x00107AA8
		internal static ChapterHandle CreateChapterHandle(object chapteredRowset, RowBinding binding, int valueOffset)
		{
			if (chapteredRowset == null || IntPtr.Zero == binding.ReadIntPtr(valueOffset))
			{
				return ChapterHandle.DB_NULL_HCHAPTER;
			}
			return new ChapterHandle(chapteredRowset, binding, valueOffset);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x001086DC File Offset: 0x00107ADC
		internal static ChapterHandle CreateChapterHandle(IntPtr chapter)
		{
			if (IntPtr.Zero == chapter)
			{
				return ChapterHandle.DB_NULL_HCHAPTER;
			}
			return new ChapterHandle(chapter);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x00108704 File Offset: 0x00107B04
		private ChapterHandle(IntPtr chapter) : base(null)
		{
			this._chapterHandle = chapter;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00108720 File Offset: 0x00107B20
		private ChapterHandle(object chapteredRowset, RowBinding binding, int valueOffset) : base(chapteredRowset)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._chapterHandle = binding.InterlockedExchangePointer(valueOffset);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x00108768 File Offset: 0x00107B68
		internal IntPtr HChapter
		{
			get
			{
				return this._chapterHandle;
			}
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0010877C File Offset: 0x00107B7C
		protected override bool ReleaseHandle()
		{
			IntPtr chapterHandle = this._chapterHandle;
			this._chapterHandle = IntPtr.Zero;
			if (IntPtr.Zero != this.handle && IntPtr.Zero != chapterHandle)
			{
				Bid.Trace("<oledb.IChapteredRowset.ReleaseChapter|API|OLEDB> Chapter=%Id\n", chapterHandle);
				OleDbHResult a = NativeOledbWrapper.IChapteredRowsetReleaseChapter(this.handle, chapterHandle);
				Bid.Trace("<oledb.IChapteredRowset.ReleaseChapter|API|OLEDB|RET> %08X{HRESULT}\n", a);
			}
			return base.ReleaseHandle();
		}

		// Token: 0x040019EF RID: 6639
		internal static readonly ChapterHandle DB_NULL_HCHAPTER = new ChapterHandle(IntPtr.Zero);

		// Token: 0x040019F0 RID: 6640
		private IntPtr _chapterHandle;
	}
}

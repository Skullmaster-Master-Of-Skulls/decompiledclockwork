using System;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;

namespace System.Data.OleDb
{
	// Token: 0x02000262 RID: 610
	internal sealed class ChapterHandle : WrappedIUnknown
	{
		// Token: 0x060020C8 RID: 8392 RVA: 0x00282568 File Offset: 0x00281968
		internal static ChapterHandle CreateChapterHandle(object chapteredRowset, RowBinding binding, int valueOffset)
		{
			if (chapteredRowset == null || IntPtr.Zero == binding.ReadIntPtr(valueOffset))
			{
				return ChapterHandle.DB_NULL_HCHAPTER;
			}
			return new ChapterHandle(chapteredRowset, binding, valueOffset);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x002825A8 File Offset: 0x002819A8
		internal static ChapterHandle CreateChapterHandle(IntPtr chapter)
		{
			if (IntPtr.Zero == chapter)
			{
				return ChapterHandle.DB_NULL_HCHAPTER;
			}
			return new ChapterHandle(chapter);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x002825D8 File Offset: 0x002819D8
		private ChapterHandle(IntPtr chapter) : base(null)
		{
			this._chapterHandle = chapter;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x002825F8 File Offset: 0x002819F8
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00282648 File Offset: 0x00281A48
		internal IntPtr HChapter
		{
			get
			{
				return this._chapterHandle;
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00282668 File Offset: 0x00281A68
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

		// Token: 0x04001550 RID: 5456
		internal static readonly ChapterHandle DB_NULL_HCHAPTER = new ChapterHandle(IntPtr.Zero);

		// Token: 0x04001551 RID: 5457
		private IntPtr _chapterHandle;
	}
}

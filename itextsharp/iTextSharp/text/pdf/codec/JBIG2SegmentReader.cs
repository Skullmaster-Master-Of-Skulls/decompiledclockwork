using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020002D8 RID: 728
	public class JBIG2SegmentReader
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x000A3158 File Offset: 0x000A2158
		public JBIG2SegmentReader(RandomAccessFileOrArray ra)
		{
			this.ra = ra;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000A3190 File Offset: 0x000A2190
		public static byte[] CopyByteArray(byte[] b)
		{
			byte[] array = new byte[b.Length];
			Array.Copy(b, 0, array, 0, b.Length);
			return array;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000A31B4 File Offset: 0x000A21B4
		public void Read()
		{
			if (this.read)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("already.attempted.a.read.on.this.jbig2.file"));
			}
			this.read = true;
			this.ReadFileHeader();
			if (this.sequential)
			{
				do
				{
					JBIG2SegmentReader.JBIG2Segment jbig2Segment = this.ReadHeader();
					this.ReadSegment(jbig2Segment);
					this.segments[jbig2Segment.segmentNumber] = jbig2Segment;
				}
				while (this.ra.FilePointer < this.ra.Length);
				return;
			}
			JBIG2SegmentReader.JBIG2Segment jbig2Segment2;
			do
			{
				jbig2Segment2 = this.ReadHeader();
				this.segments[jbig2Segment2.segmentNumber] = jbig2Segment2;
			}
			while (jbig2Segment2.type != 51);
			foreach (int key in this.segments.Keys)
			{
				this.ReadSegment(this.segments[key]);
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000A32A0 File Offset: 0x000A22A0
		private void ReadSegment(JBIG2SegmentReader.JBIG2Segment s)
		{
			int filePointer = this.ra.FilePointer;
			if (s.dataLength == (long)((ulong)-1))
			{
				return;
			}
			byte[] array = new byte[(int)s.dataLength];
			this.ra.Read(array);
			s.data = array;
			if (s.type == 48)
			{
				int filePointer2 = this.ra.FilePointer;
				this.ra.Seek(filePointer);
				int pageBitmapWidth = this.ra.ReadInt();
				int pageBitmapHeight = this.ra.ReadInt();
				this.ra.Seek(filePointer2);
				JBIG2SegmentReader.JBIG2Page jbig2Page = this.pages[s.page];
				if (jbig2Page == null)
				{
					throw new InvalidOperationException(MessageLocalization.GetComposedMessage("referring.to.widht.height.of.page.we.havent.seen.yet.1", s.page));
				}
				jbig2Page.pageBitmapWidth = pageBitmapWidth;
				jbig2Page.pageBitmapHeight = pageBitmapHeight;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000A3374 File Offset: 0x000A2374
		private JBIG2SegmentReader.JBIG2Segment ReadHeader()
		{
			int filePointer = this.ra.FilePointer;
			int num = this.ra.ReadInt();
			JBIG2SegmentReader.JBIG2Segment jbig2Segment = new JBIG2SegmentReader.JBIG2Segment(num);
			int num2 = this.ra.Read();
			bool deferredNonRetain = (num2 & 128) == 128;
			jbig2Segment.deferredNonRetain = deferredNonRetain;
			bool flag = (num2 & 64) == 64;
			int type = num2 & 63;
			jbig2Segment.type = type;
			int num3 = this.ra.Read();
			int num4 = (num3 & 224) >> 5;
			bool[] array = null;
			if (num4 == 7)
			{
				this.ra.Seek(this.ra.FilePointer - 1);
				num4 = (this.ra.ReadInt() & 536870911);
				array = new bool[num4 + 1];
				int num5 = 0;
				int num6 = 0;
				do
				{
					int num7 = num5 % 8;
					if (num7 == 0)
					{
						num6 = this.ra.Read();
					}
					array[num5] = ((1 << num7 & num6) >> num7 == 1);
					num5++;
				}
				while (num5 <= num4);
			}
			else if (num4 <= 4)
			{
				array = new bool[num4 + 1];
				num3 &= 31;
				for (int i = 0; i <= num4; i++)
				{
					array[i] = ((1 << i & num3) >> i == 1);
				}
			}
			else if (num4 == 5 || num4 == 6)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("count.of.referred.to.segments.had.bad.value.in.header.for.segment.1.starting.at.2", num, filePointer));
			}
			jbig2Segment.segmentRetentionFlags = array;
			jbig2Segment.countOfReferredToSegments = num4;
			int[] array2 = new int[num4 + 1];
			for (int j = 1; j <= num4; j++)
			{
				if (num <= 256)
				{
					array2[j] = this.ra.Read();
				}
				else if (num <= 65536)
				{
					array2[j] = this.ra.ReadUnsignedShort();
				}
				else
				{
					array2[j] = (int)this.ra.ReadUnsignedInt();
				}
			}
			jbig2Segment.referredToSegmentNumbers = array2;
			int page_association_offset = this.ra.FilePointer - filePointer;
			int num8;
			if (flag)
			{
				num8 = this.ra.ReadInt();
			}
			else
			{
				num8 = this.ra.Read();
			}
			if (num8 < 0)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("page.1.invalid.for.segment.2.starting.at.3", num8, num, filePointer));
			}
			jbig2Segment.page = num8;
			jbig2Segment.page_association_size = flag;
			jbig2Segment.page_association_offset = page_association_offset;
			if (num8 > 0 && !this.pages.ContainsKey(num8))
			{
				this.pages[num8] = new JBIG2SegmentReader.JBIG2Page(num8, this);
			}
			if (num8 > 0)
			{
				this.pages[num8].AddSegment(jbig2Segment);
			}
			else
			{
				this.globals[jbig2Segment] = null;
			}
			long dataLength = this.ra.ReadUnsignedInt();
			jbig2Segment.dataLength = dataLength;
			int filePointer2 = this.ra.FilePointer;
			this.ra.Seek(filePointer);
			byte[] array3 = new byte[filePointer2 - filePointer];
			this.ra.Read(array3);
			jbig2Segment.headerData = array3;
			return jbig2Segment;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000A3678 File Offset: 0x000A2678
		private void ReadFileHeader()
		{
			this.ra.Seek(0);
			byte[] array = new byte[8];
			this.ra.Read(array);
			byte[] array2 = new byte[]
			{
				151,
				74,
				66,
				50,
				13,
				10,
				26,
				10
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					throw new InvalidOperationException(MessageLocalization.GetComposedMessage("file.header.idstring.not.good.at.byte.1", i));
				}
			}
			int num = this.ra.Read();
			this.sequential = ((num & 1) == 1);
			this.number_of_pages_known = ((num & 2) == 0);
			if ((num & 252) != 0)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("file.header.flags.bits.2.7.not.0"));
			}
			if (this.number_of_pages_known)
			{
				this.number_of_pages = this.ra.ReadInt();
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000A3739 File Offset: 0x000A2739
		public int NumberOfPages()
		{
			return this.pages.Count;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000A3746 File Offset: 0x000A2746
		public int GetPageHeight(int i)
		{
			return this.pages[i].pageBitmapHeight;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000A3759 File Offset: 0x000A2759
		public int GetPageWidth(int i)
		{
			return this.pages[i].pageBitmapWidth;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000A376C File Offset: 0x000A276C
		public JBIG2SegmentReader.JBIG2Page GetPage(int page)
		{
			return this.pages[page];
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000A377C File Offset: 0x000A277C
		public byte[] GetGlobal(bool for_embedding)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				foreach (JBIG2SegmentReader.JBIG2Segment jbig2Segment in this.globals.Keys)
				{
					if (!for_embedding || (jbig2Segment.type != 51 && jbig2Segment.type != 49))
					{
						memoryStream.Write(jbig2Segment.headerData, 0, jbig2Segment.headerData.Length);
						memoryStream.Write(jbig2Segment.data, 0, jbig2Segment.data.Length);
					}
				}
				memoryStream.Close();
			}
			catch
			{
			}
			if (memoryStream.Length <= 0L)
			{
				return null;
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000A3840 File Offset: 0x000A2840
		public override string ToString()
		{
			if (this.read)
			{
				return "Jbig2SegmentReader: number of pages: " + this.NumberOfPages();
			}
			return "Jbig2SegmentReader in indeterminate state.";
		}

		// Token: 0x0400127F RID: 4735
		public const int SYMBOL_DICTIONARY = 0;

		// Token: 0x04001280 RID: 4736
		public const int INTERMEDIATE_TEXT_REGION = 4;

		// Token: 0x04001281 RID: 4737
		public const int IMMEDIATE_TEXT_REGION = 6;

		// Token: 0x04001282 RID: 4738
		public const int IMMEDIATE_LOSSLESS_TEXT_REGION = 7;

		// Token: 0x04001283 RID: 4739
		public const int PATTERN_DICTIONARY = 16;

		// Token: 0x04001284 RID: 4740
		public const int INTERMEDIATE_HALFTONE_REGION = 20;

		// Token: 0x04001285 RID: 4741
		public const int IMMEDIATE_HALFTONE_REGION = 22;

		// Token: 0x04001286 RID: 4742
		public const int IMMEDIATE_LOSSLESS_HALFTONE_REGION = 23;

		// Token: 0x04001287 RID: 4743
		public const int INTERMEDIATE_GENERIC_REGION = 36;

		// Token: 0x04001288 RID: 4744
		public const int IMMEDIATE_GENERIC_REGION = 38;

		// Token: 0x04001289 RID: 4745
		public const int IMMEDIATE_LOSSLESS_GENERIC_REGION = 39;

		// Token: 0x0400128A RID: 4746
		public const int INTERMEDIATE_GENERIC_REFINEMENT_REGION = 40;

		// Token: 0x0400128B RID: 4747
		public const int IMMEDIATE_GENERIC_REFINEMENT_REGION = 42;

		// Token: 0x0400128C RID: 4748
		public const int IMMEDIATE_LOSSLESS_GENERIC_REFINEMENT_REGION = 43;

		// Token: 0x0400128D RID: 4749
		public const int PAGE_INFORMATION = 48;

		// Token: 0x0400128E RID: 4750
		public const int END_OF_PAGE = 49;

		// Token: 0x0400128F RID: 4751
		public const int END_OF_STRIPE = 50;

		// Token: 0x04001290 RID: 4752
		public const int END_OF_FILE = 51;

		// Token: 0x04001291 RID: 4753
		public const int PROFILES = 52;

		// Token: 0x04001292 RID: 4754
		public const int TABLES = 53;

		// Token: 0x04001293 RID: 4755
		public const int EXTENSION = 62;

		// Token: 0x04001294 RID: 4756
		private SortedDictionary<int, JBIG2SegmentReader.JBIG2Segment> segments = new SortedDictionary<int, JBIG2SegmentReader.JBIG2Segment>();

		// Token: 0x04001295 RID: 4757
		private SortedDictionary<int, JBIG2SegmentReader.JBIG2Page> pages = new SortedDictionary<int, JBIG2SegmentReader.JBIG2Page>();

		// Token: 0x04001296 RID: 4758
		private SortedDictionary<JBIG2SegmentReader.JBIG2Segment, object> globals = new SortedDictionary<JBIG2SegmentReader.JBIG2Segment, object>();

		// Token: 0x04001297 RID: 4759
		private RandomAccessFileOrArray ra;

		// Token: 0x04001298 RID: 4760
		private bool sequential;

		// Token: 0x04001299 RID: 4761
		private bool number_of_pages_known;

		// Token: 0x0400129A RID: 4762
		private int number_of_pages = -1;

		// Token: 0x0400129B RID: 4763
		private bool read;

		// Token: 0x020002D9 RID: 729
		public class JBIG2Segment : IComparable<JBIG2SegmentReader.JBIG2Segment>
		{
			// Token: 0x06001B3C RID: 6972 RVA: 0x000A3865 File Offset: 0x000A2865
			public JBIG2Segment(int segment_number)
			{
				this.segmentNumber = segment_number;
			}

			// Token: 0x06001B3D RID: 6973 RVA: 0x000A3898 File Offset: 0x000A2898
			public int CompareTo(JBIG2SegmentReader.JBIG2Segment s)
			{
				return this.segmentNumber - s.segmentNumber;
			}

			// Token: 0x0400129C RID: 4764
			public int segmentNumber;

			// Token: 0x0400129D RID: 4765
			public long dataLength = -1L;

			// Token: 0x0400129E RID: 4766
			public int page = -1;

			// Token: 0x0400129F RID: 4767
			public int[] referredToSegmentNumbers;

			// Token: 0x040012A0 RID: 4768
			public bool[] segmentRetentionFlags;

			// Token: 0x040012A1 RID: 4769
			public int type = -1;

			// Token: 0x040012A2 RID: 4770
			public bool deferredNonRetain;

			// Token: 0x040012A3 RID: 4771
			public int countOfReferredToSegments = -1;

			// Token: 0x040012A4 RID: 4772
			public byte[] data;

			// Token: 0x040012A5 RID: 4773
			public byte[] headerData;

			// Token: 0x040012A6 RID: 4774
			public bool page_association_size;

			// Token: 0x040012A7 RID: 4775
			public int page_association_offset = -1;
		}

		// Token: 0x020002DA RID: 730
		public class JBIG2Page
		{
			// Token: 0x06001B3E RID: 6974 RVA: 0x000A38A7 File Offset: 0x000A28A7
			public JBIG2Page(int page, JBIG2SegmentReader sr)
			{
				this.page = page;
				this.sr = sr;
			}

			// Token: 0x06001B3F RID: 6975 RVA: 0x000A38D8 File Offset: 0x000A28D8
			public byte[] GetData(bool for_embedding)
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (int key in this.segs.Keys)
				{
					JBIG2SegmentReader.JBIG2Segment jbig2Segment = this.segs[key];
					if (!for_embedding || (jbig2Segment.type != 51 && jbig2Segment.type != 49))
					{
						if (for_embedding)
						{
							byte[] array = JBIG2SegmentReader.CopyByteArray(jbig2Segment.headerData);
							if (jbig2Segment.page_association_size)
							{
								array[jbig2Segment.page_association_offset] = 0;
								array[jbig2Segment.page_association_offset + 1] = 0;
								array[jbig2Segment.page_association_offset + 2] = 0;
								array[jbig2Segment.page_association_offset + 3] = 1;
							}
							else
							{
								array[jbig2Segment.page_association_offset] = 1;
							}
							memoryStream.Write(array, 0, array.Length);
						}
						else
						{
							memoryStream.Write(jbig2Segment.headerData, 0, jbig2Segment.headerData.Length);
						}
						memoryStream.Write(jbig2Segment.data, 0, jbig2Segment.data.Length);
					}
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}

			// Token: 0x06001B40 RID: 6976 RVA: 0x000A39F0 File Offset: 0x000A29F0
			public void AddSegment(JBIG2SegmentReader.JBIG2Segment s)
			{
				this.segs[s.segmentNumber] = s;
			}

			// Token: 0x040012A8 RID: 4776
			public int page;

			// Token: 0x040012A9 RID: 4777
			private JBIG2SegmentReader sr;

			// Token: 0x040012AA RID: 4778
			private SortedDictionary<int, JBIG2SegmentReader.JBIG2Segment> segs = new SortedDictionary<int, JBIG2SegmentReader.JBIG2Segment>();

			// Token: 0x040012AB RID: 4779
			public int pageBitmapWidth = -1;

			// Token: 0x040012AC RID: 4780
			public int pageBitmapHeight = -1;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x0200005E RID: 94
	public class GifImage
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000D248 File Offset: 0x0000C248
		public GifImage(Uri url)
		{
			this.fromUrl = url;
			Stream stream = null;
			try
			{
				stream = WebRequest.Create(url).GetResponse().GetResponseStream();
				this.Process(stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D2B4 File Offset: 0x0000C2B4
		public GifImage(string file) : this(Utilities.ToURL(file))
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D2C4 File Offset: 0x0000C2C4
		public GifImage(byte[] data)
		{
			this.fromData = data;
			Stream stream = null;
			try
			{
				stream = new MemoryStream(data);
				this.Process(stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D328 File Offset: 0x0000C328
		public GifImage(Stream isp)
		{
			this.Process(isp);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D352 File Offset: 0x0000C352
		public int GetFrameCount()
		{
			return this.frames.Count;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D360 File Offset: 0x0000C360
		public Image GetImage(int frame)
		{
			GifImage.GifFrame gifFrame = this.frames[frame - 1];
			return gifFrame.image;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D384 File Offset: 0x0000C384
		public int[] GetFramePosition(int frame)
		{
			GifImage.GifFrame gifFrame = this.frames[frame - 1];
			return new int[]
			{
				gifFrame.ix,
				gifFrame.iy
			};
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D3BC File Offset: 0x0000C3BC
		public int[] GetLogicalScreen()
		{
			return new int[]
			{
				this.width,
				this.height
			};
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D3E3 File Offset: 0x0000C3E3
		internal void Process(Stream isp)
		{
			this.inp = new BufferedStream(isp);
			this.ReadHeader();
			this.ReadContents();
			if (this.frames.Count == 0)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("the.file.does.not.contain.any.valid.image"));
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D41C File Offset: 0x0000C41C
		protected void ReadHeader()
		{
			string text = "";
			for (int i = 0; i < 6; i++)
			{
				text += (char)this.inp.ReadByte();
			}
			if (!text.StartsWith("GIF8"))
			{
				throw new IOException(MessageLocalization.GetComposedMessage("gif.signature.nor.found"));
			}
			this.ReadLSD();
			if (this.gctFlag)
			{
				this.m_global_table = this.ReadColorTable(this.m_gbpc);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D490 File Offset: 0x0000C490
		protected void ReadLSD()
		{
			this.width = this.ReadShort();
			this.height = this.ReadShort();
			int num = this.inp.ReadByte();
			this.gctFlag = ((num & 128) != 0);
			this.m_gbpc = (num & 7) + 1;
			this.bgIndex = this.inp.ReadByte();
			this.pixelAspect = this.inp.ReadByte();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D501 File Offset: 0x0000C501
		protected int ReadShort()
		{
			return this.inp.ReadByte() | this.inp.ReadByte() << 8;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D51C File Offset: 0x0000C51C
		protected int ReadBlock()
		{
			this.blockSize = this.inp.ReadByte();
			if (this.blockSize <= 0)
			{
				return this.blockSize = 0;
			}
			for (int i = 0; i < this.blockSize; i++)
			{
				int num = this.inp.ReadByte();
				if (num < 0)
				{
					return this.blockSize = i;
				}
				this.block[i] = (byte)num;
			}
			return this.blockSize;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D58C File Offset: 0x0000C58C
		protected byte[] ReadColorTable(int bpc)
		{
			int num = 1 << bpc;
			int count = 3 * num;
			bpc = GifImage.NewBpc(bpc);
			byte[] array = new byte[(1 << bpc) * 3];
			this.ReadFully(array, 0, count);
			return array;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D5C4 File Offset: 0x0000C5C4
		protected static int NewBpc(int bpc)
		{
			switch (bpc)
			{
			case 1:
			case 2:
			case 4:
				return bpc;
			case 3:
				return 4;
			default:
				return 8;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D5F4 File Offset: 0x0000C5F4
		protected void ReadContents()
		{
			bool flag = false;
			while (!flag)
			{
				int num = this.inp.ReadByte();
				int num2 = num;
				if (num2 != 33)
				{
					if (num2 == 44)
					{
						this.ReadImage();
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					num = this.inp.ReadByte();
					int num3 = num;
					if (num3 != 249)
					{
						if (num3 != 255)
						{
							this.Skip();
						}
						else
						{
							this.ReadBlock();
							this.Skip();
						}
					}
					else
					{
						this.ReadGraphicControlExt();
					}
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D66C File Offset: 0x0000C66C
		protected void ReadImage()
		{
			this.ix = this.ReadShort();
			this.iy = this.ReadShort();
			this.iw = this.ReadShort();
			this.ih = this.ReadShort();
			int num = this.inp.ReadByte();
			this.lctFlag = ((num & 128) != 0);
			this.interlace = ((num & 64) != 0);
			this.lctSize = 2 << (num & 7);
			this.m_bpc = GifImage.NewBpc(this.m_gbpc);
			if (this.lctFlag)
			{
				this.m_curr_table = this.ReadColorTable((num & 7) + 1);
				this.m_bpc = GifImage.NewBpc((num & 7) + 1);
			}
			else
			{
				this.m_curr_table = this.m_global_table;
			}
			if (this.transparency && this.transIndex >= this.m_curr_table.Length / 3)
			{
				this.transparency = false;
			}
			if (this.transparency && this.m_bpc == 1)
			{
				byte[] array = new byte[12];
				Array.Copy(this.m_curr_table, 0, array, 0, 6);
				this.m_curr_table = array;
				this.m_bpc = 2;
			}
			if (!this.DecodeImageData())
			{
				this.Skip();
			}
			Image image = new ImgRaw(this.iw, this.ih, 1, this.m_bpc, this.m_out);
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(PdfName.INDEXED);
			pdfArray.Add(PdfName.DEVICERGB);
			int num2 = this.m_curr_table.Length;
			pdfArray.Add(new PdfNumber(num2 / 3 - 1));
			pdfArray.Add(new PdfString(this.m_curr_table));
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.COLORSPACE, pdfArray);
			image.Additional = pdfDictionary;
			if (this.transparency)
			{
				image.Transparency = new int[]
				{
					this.transIndex,
					this.transIndex
				};
			}
			image.OriginalType = 3;
			image.OriginalData = this.fromData;
			image.Url = this.fromUrl;
			GifImage.GifFrame gifFrame = new GifImage.GifFrame();
			gifFrame.image = image;
			gifFrame.ix = this.ix;
			gifFrame.iy = this.iy;
			this.frames.Add(gifFrame);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D8A8 File Offset: 0x0000C8A8
		protected bool DecodeImageData()
		{
			int num = -1;
			int num2 = this.iw * this.ih;
			bool result = false;
			if (this.prefix == null)
			{
				this.prefix = new short[4096];
			}
			if (this.suffix == null)
			{
				this.suffix = new byte[4096];
			}
			if (this.pixelStack == null)
			{
				this.pixelStack = new byte[4097];
			}
			this.m_line_stride = (this.iw * this.m_bpc + 7) / 8;
			this.m_out = new byte[this.m_line_stride * this.ih];
			int num3 = 1;
			int num4 = this.interlace ? 8 : 1;
			int num5 = 0;
			int num6 = 0;
			int num7 = this.inp.ReadByte();
			int num8 = 1 << num7;
			int num9 = num8 + 1;
			int num10 = num8 + 2;
			int num11 = num;
			int num12 = num7 + 1;
			int num13 = (1 << num12) - 1;
			for (int i = 0; i < num8; i++)
			{
				this.prefix[i] = 0;
				this.suffix[i] = (byte)i;
			}
			int num19;
			int num18;
			int num17;
			int num16;
			int num15;
			int num14 = num15 = (num16 = (num17 = (num18 = (num19 = 0))));
			int j = 0;
			while (j < num2)
			{
				if (num18 == 0)
				{
					if (num14 < num12)
					{
						if (num16 == 0)
						{
							num16 = this.ReadBlock();
							if (num16 <= 0)
							{
								result = true;
								break;
							}
							num19 = 0;
						}
						num15 += (int)(this.block[num19] & byte.MaxValue) << num14;
						num14 += 8;
						num19++;
						num16--;
						continue;
					}
					int i = num15 & num13;
					num15 >>= num12;
					num14 -= num12;
					if (i > num10 || i == num9)
					{
						break;
					}
					if (i == num8)
					{
						num12 = num7 + 1;
						num13 = (1 << num12) - 1;
						num10 = num8 + 2;
						num11 = num;
						continue;
					}
					if (num11 == num)
					{
						this.pixelStack[num18++] = this.suffix[i];
						num11 = i;
						num17 = i;
						continue;
					}
					int num20 = i;
					if (i == num10)
					{
						this.pixelStack[num18++] = (byte)num17;
						i = num11;
					}
					while (i > num8)
					{
						this.pixelStack[num18++] = this.suffix[i];
						i = (int)this.prefix[i];
					}
					num17 = (int)(this.suffix[i] & byte.MaxValue);
					if (num10 >= 4096)
					{
						break;
					}
					this.pixelStack[num18++] = (byte)num17;
					this.prefix[num10] = (short)num11;
					this.suffix[num10] = (byte)num17;
					num10++;
					if ((num10 & num13) == 0 && num10 < 4096)
					{
						num12++;
						num13 += num10;
					}
					num11 = num20;
				}
				num18--;
				j++;
				this.SetPixel(num6, num5, (int)this.pixelStack[num18]);
				num6++;
				if (num6 >= this.iw)
				{
					num6 = 0;
					num5 += num4;
					if (num5 >= this.ih)
					{
						if (this.interlace)
						{
							do
							{
								num3++;
								switch (num3)
								{
								case 2:
									num5 = 4;
									break;
								case 3:
									num5 = 2;
									num4 = 4;
									break;
								case 4:
									num5 = 1;
									num4 = 2;
									break;
								default:
									num5 = this.ih - 1;
									num4 = 0;
									break;
								}
							}
							while (num5 >= this.ih);
						}
						else
						{
							num5 = this.ih - 1;
							num4 = 0;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000DBF4 File Offset: 0x0000CBF4
		protected void SetPixel(int x, int y, int v)
		{
			if (this.m_bpc == 8)
			{
				int num = x + this.iw * y;
				this.m_out[num] = (byte)v;
				return;
			}
			int num2 = this.m_line_stride * y + x / (8 / this.m_bpc);
			int num3 = v << 8 - this.m_bpc * (x % (8 / this.m_bpc)) - this.m_bpc;
			byte[] @out = this.m_out;
			int num4 = num2;
			@out[num4] |= (byte)num3;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000DC6F File Offset: 0x0000CC6F
		protected void ResetFrame()
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000DC74 File Offset: 0x0000CC74
		protected void ReadGraphicControlExt()
		{
			this.inp.ReadByte();
			int num = this.inp.ReadByte();
			this.dispose = (num & 28) >> 2;
			if (this.dispose == 0)
			{
				this.dispose = 1;
			}
			this.transparency = ((num & 1) != 0);
			this.delay = this.ReadShort() * 10;
			this.transIndex = this.inp.ReadByte();
			this.inp.ReadByte();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000DCEF File Offset: 0x0000CCEF
		protected void Skip()
		{
			do
			{
				this.ReadBlock();
			}
			while (this.blockSize > 0);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000DD04 File Offset: 0x0000CD04
		private void ReadFully(byte[] b, int offset, int count)
		{
			while (count > 0)
			{
				int num = this.inp.Read(b, offset, count);
				if (num <= 0)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("insufficient.data"));
				}
				count -= num;
				offset += num;
			}
		}

		// Token: 0x0400014E RID: 334
		protected const int MaxStackSize = 4096;

		// Token: 0x0400014F RID: 335
		protected Stream inp;

		// Token: 0x04000150 RID: 336
		protected int width;

		// Token: 0x04000151 RID: 337
		protected int height;

		// Token: 0x04000152 RID: 338
		protected bool gctFlag;

		// Token: 0x04000153 RID: 339
		protected int bgIndex;

		// Token: 0x04000154 RID: 340
		protected int bgColor;

		// Token: 0x04000155 RID: 341
		protected int pixelAspect;

		// Token: 0x04000156 RID: 342
		protected bool lctFlag;

		// Token: 0x04000157 RID: 343
		protected bool interlace;

		// Token: 0x04000158 RID: 344
		protected int lctSize;

		// Token: 0x04000159 RID: 345
		protected int ix;

		// Token: 0x0400015A RID: 346
		protected int iy;

		// Token: 0x0400015B RID: 347
		protected int iw;

		// Token: 0x0400015C RID: 348
		protected int ih;

		// Token: 0x0400015D RID: 349
		protected byte[] block = new byte[256];

		// Token: 0x0400015E RID: 350
		protected int blockSize;

		// Token: 0x0400015F RID: 351
		protected int dispose;

		// Token: 0x04000160 RID: 352
		protected bool transparency;

		// Token: 0x04000161 RID: 353
		protected int delay;

		// Token: 0x04000162 RID: 354
		protected int transIndex;

		// Token: 0x04000163 RID: 355
		protected short[] prefix;

		// Token: 0x04000164 RID: 356
		protected byte[] suffix;

		// Token: 0x04000165 RID: 357
		protected byte[] pixelStack;

		// Token: 0x04000166 RID: 358
		protected byte[] pixels;

		// Token: 0x04000167 RID: 359
		protected byte[] m_out;

		// Token: 0x04000168 RID: 360
		protected int m_bpc;

		// Token: 0x04000169 RID: 361
		protected int m_gbpc;

		// Token: 0x0400016A RID: 362
		protected byte[] m_global_table;

		// Token: 0x0400016B RID: 363
		protected byte[] m_local_table;

		// Token: 0x0400016C RID: 364
		protected byte[] m_curr_table;

		// Token: 0x0400016D RID: 365
		protected int m_line_stride;

		// Token: 0x0400016E RID: 366
		protected byte[] fromData;

		// Token: 0x0400016F RID: 367
		protected Uri fromUrl;

		// Token: 0x04000170 RID: 368
		internal List<GifImage.GifFrame> frames = new List<GifImage.GifFrame>();

		// Token: 0x0200005F RID: 95
		internal class GifFrame
		{
			// Token: 0x04000171 RID: 369
			internal Image image;

			// Token: 0x04000172 RID: 370
			internal int ix;

			// Token: 0x04000173 RID: 371
			internal int iy;
		}
	}
}

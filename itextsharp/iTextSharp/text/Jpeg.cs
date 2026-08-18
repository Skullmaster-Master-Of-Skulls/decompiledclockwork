using System;
using System.IO;
using System.Net;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x02000331 RID: 817
	public class Jpeg : Image
	{
		// Token: 0x06001D8A RID: 7562 RVA: 0x000B1348 File Offset: 0x000B0348
		public Jpeg(Image image) : base(image)
		{
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000B1351 File Offset: 0x000B0351
		public Jpeg(Uri Uri) : base(Uri)
		{
			this.ProcessParameters();
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000B1360 File Offset: 0x000B0360
		public Jpeg(byte[] img) : base(null)
		{
			this.rawData = img;
			this.originalData = img;
			this.ProcessParameters();
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000B137D File Offset: 0x000B037D
		public Jpeg(byte[] img, float width, float height) : this(img)
		{
			this.scaledWidth = width;
			this.scaledHeight = height;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000B1394 File Offset: 0x000B0394
		private static int GetShort(Stream istr)
		{
			return (istr.ReadByte() << 8) + istr.ReadByte();
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000B13A5 File Offset: 0x000B03A5
		private static int GetShortInverted(Stream istr)
		{
			return istr.ReadByte() + istr.ReadByte() << 8;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000B13B8 File Offset: 0x000B03B8
		private static int MarkerType(int marker)
		{
			for (int i = 0; i < Jpeg.VALID_MARKERS.Length; i++)
			{
				if (marker == Jpeg.VALID_MARKERS[i])
				{
					return 0;
				}
			}
			for (int j = 0; j < Jpeg.NOPARAM_MARKERS.Length; j++)
			{
				if (marker == Jpeg.NOPARAM_MARKERS[j])
				{
					return 2;
				}
			}
			for (int k = 0; k < Jpeg.UNSUPPORTED_MARKERS.Length; k++)
			{
				if (marker == Jpeg.UNSUPPORTED_MARKERS[k])
				{
					return 1;
				}
			}
			return -1;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000B1420 File Offset: 0x000B0420
		private void ProcessParameters()
		{
			this.type = 32;
			this.originalType = 1;
			Stream stream = null;
			try
			{
				string p;
				if (this.rawData == null)
				{
					WebRequest webRequest = WebRequest.Create(this.url);
					stream = webRequest.GetResponse().GetResponseStream();
					p = this.url.ToString();
				}
				else
				{
					stream = new MemoryStream(this.rawData);
					p = "Byte array";
				}
				if (stream.ReadByte() != 255 || stream.ReadByte() != 216)
				{
					throw new BadElementException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.jpeg.file", p));
				}
				bool flag = true;
				int num2;
				for (;;)
				{
					int num = stream.ReadByte();
					if (num < 0)
					{
						break;
					}
					if (num == 255)
					{
						num2 = stream.ReadByte();
						if (flag && num2 == 224)
						{
							flag = false;
							int num3 = Jpeg.GetShort(stream);
							if (num3 < 16)
							{
								Utilities.Skip(stream, num3 - 2);
							}
							else
							{
								byte[] array = new byte[Jpeg.JFIF_ID.Length];
								int num4 = stream.Read(array, 0, array.Length);
								if (num4 != array.Length)
								{
									goto Block_14;
								}
								bool flag2 = true;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] != Jpeg.JFIF_ID[i])
									{
										flag2 = false;
										break;
									}
								}
								if (!flag2)
								{
									Utilities.Skip(stream, num3 - 2 - array.Length);
								}
								else
								{
									Utilities.Skip(stream, 2);
									int num5 = stream.ReadByte();
									int @short = Jpeg.GetShort(stream);
									int short2 = Jpeg.GetShort(stream);
									if (num5 == 1)
									{
										this.dpiX = @short;
										this.dpiY = short2;
									}
									else if (num5 == 2)
									{
										this.dpiX = (int)((float)@short * 2.54f + 0.5f);
										this.dpiY = (int)((float)short2 * 2.54f + 0.5f);
									}
									Utilities.Skip(stream, num3 - 2 - array.Length - 7);
								}
							}
						}
						else if (num2 == 238)
						{
							int num3 = Jpeg.GetShort(stream) - 2;
							byte[] array2 = new byte[num3];
							for (int j = 0; j < num3; j++)
							{
								array2[j] = (byte)stream.ReadByte();
							}
							if (array2.Length >= 12)
							{
								string @string = Encoding.ASCII.GetString(array2, 0, 5);
								if (Util.EqualsIgnoreCase(@string, "adobe"))
								{
									this.invert = true;
								}
							}
						}
						else if (num2 == 226)
						{
							int num3 = Jpeg.GetShort(stream) - 2;
							byte[] array3 = new byte[num3];
							for (int k = 0; k < num3; k++)
							{
								array3[k] = (byte)stream.ReadByte();
							}
							if (array3.Length >= 14)
							{
								string string2 = Encoding.ASCII.GetString(array3, 0, 11);
								if (string2.Equals("ICC_PROFILE"))
								{
									int num6 = (int)(array3[12] & byte.MaxValue);
									int num7 = (int)(array3[13] & byte.MaxValue);
									if (num6 < 1)
									{
										num6 = 1;
									}
									if (num7 < 1)
									{
										num7 = 1;
									}
									if (this.icc == null)
									{
										this.icc = new byte[num7][];
									}
									this.icc[num6 - 1] = array3;
								}
							}
						}
						else
						{
							flag = false;
							int num8 = Jpeg.MarkerType(num2);
							if (num8 == 0)
							{
								goto Block_30;
							}
							if (num8 == 1)
							{
								goto Block_32;
							}
							if (num8 != 2)
							{
								Utilities.Skip(stream, Jpeg.GetShort(stream) - 2);
							}
						}
					}
				}
				throw new IOException(MessageLocalization.GetComposedMessage("premature.eof.while.reading.jpg"));
				Block_14:
				throw new BadElementException(MessageLocalization.GetComposedMessage("1.corrupted.jfif.marker", p));
				Block_30:
				Utilities.Skip(stream, 2);
				if (stream.ReadByte() != 8)
				{
					throw new BadElementException(MessageLocalization.GetComposedMessage("1.must.have.8.bits.per.component", p));
				}
				this.scaledHeight = (float)Jpeg.GetShort(stream);
				this.Top = this.scaledHeight;
				this.scaledWidth = (float)Jpeg.GetShort(stream);
				this.Right = this.scaledWidth;
				this.colorspace = stream.ReadByte();
				this.bpc = 8;
				goto IL_3B5;
				Block_32:
				throw new BadElementException(MessageLocalization.GetComposedMessage("1.unsupported.jpeg.marker.2", p, num2));
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			IL_3B5:
			this.plainWidth = this.Width;
			this.plainHeight = base.Height;
			if (this.icc != null)
			{
				int num9 = 0;
				for (int l = 0; l < this.icc.Length; l++)
				{
					if (this.icc[l] == null)
					{
						this.icc = null;
						return;
					}
					num9 += this.icc[l].Length - 14;
				}
				byte[] array4 = new byte[num9];
				num9 = 0;
				for (int m = 0; m < this.icc.Length; m++)
				{
					Array.Copy(this.icc[m], 14, array4, num9, this.icc[m].Length - 14);
					num9 += this.icc[m].Length - 14;
				}
				try
				{
					ICC_Profile instance = ICC_Profile.GetInstance(array4);
					base.TagICC = instance;
				}
				catch
				{
				}
				this.icc = null;
			}
		}

		// Token: 0x04001443 RID: 5187
		public const int NOT_A_MARKER = -1;

		// Token: 0x04001444 RID: 5188
		public const int VALID_MARKER = 0;

		// Token: 0x04001445 RID: 5189
		public const int UNSUPPORTED_MARKER = 1;

		// Token: 0x04001446 RID: 5190
		public const int NOPARAM_MARKER = 2;

		// Token: 0x04001447 RID: 5191
		public const int M_APP0 = 224;

		// Token: 0x04001448 RID: 5192
		public const int M_APP2 = 226;

		// Token: 0x04001449 RID: 5193
		public const int M_APPE = 238;

		// Token: 0x0400144A RID: 5194
		public static int[] VALID_MARKERS = new int[]
		{
			192,
			193,
			194
		};

		// Token: 0x0400144B RID: 5195
		public static int[] UNSUPPORTED_MARKERS = new int[]
		{
			195,
			197,
			198,
			199,
			200,
			201,
			202,
			203,
			205,
			206,
			207
		};

		// Token: 0x0400144C RID: 5196
		public static int[] NOPARAM_MARKERS = new int[]
		{
			208,
			209,
			210,
			211,
			212,
			213,
			214,
			215,
			216,
			1
		};

		// Token: 0x0400144D RID: 5197
		public static byte[] JFIF_ID = new byte[]
		{
			74,
			70,
			73,
			70,
			0
		};

		// Token: 0x0400144E RID: 5198
		private byte[][] icc;
	}
}

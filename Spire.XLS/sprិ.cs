using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using Spire.Compression;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004E2 RID: 1250
[CLSCompliant(false)]
internal class spr\u17B7 : spr\u1D3B, IDisposable, sprẫ
{
	// Token: 0x06004CAF RID: 19631 RVA: 0x002ED51C File Offset: 0x002EC51C
	public spr\u17B7(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x06004CB0 RID: 19632 RVA: 0x002ED548 File Offset: 0x002EC548
	public spr\u17B7(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004CB1 RID: 19633 RVA: 0x002ED578 File Offset: 0x002EC578
	public spr\u17B7(spr\u1D3B A_0, Stream A_1) : base(A_0, A_1, null)
	{
	}

	// Token: 0x06004CB2 RID: 19634 RVA: 0x002ED5A8 File Offset: 0x002EC5A8
	public Image ᜁ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.\u170D;
	}

	// Token: 0x06004CB3 RID: 19635 RVA: 0x002ED5EC File Offset: 0x002EC5EC
	public void ᜁ(Image A_0)
	{
		int a_ = 14;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ᑃ⽅⭇㹉㥋㱍㕏", a_));
		}
		this.\u170D = A_0;
		MemoryStream memoryStream = spr\u17B7.ᜀ(this.\u170D);
		this.ᜅ = this.ᜀ(memoryStream, 0);
		memoryStream.Close();
	}

	// Token: 0x06004CB4 RID: 19636 RVA: 0x002ED670 File Offset: 0x002EC670
	public new byte[] ᜀ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return this.ᜆ;
	}

	// Token: 0x06004CB5 RID: 19637 RVA: 0x002ED6B4 File Offset: 0x002EC6B4
	public new void ᜀ(byte[] A_0)
	{
		int a_ = 9;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_34;
			case 2:
				if (A_0.Length != this.ᜆ.Length)
				{
					num = 3;
					continue;
				}
				goto IL_87;
			case 3:
				goto IL_71;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤾⁀⽂い≆", a_));
		IL_71:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤾⁀⽂い≆杈݊⡌ⅎ㙐❒㵔", a_));
		IL_87:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_71;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜆ = A_0;
			return;
		}
	}

	// Token: 0x06004CB6 RID: 19638 RVA: 0x002ED774 File Offset: 0x002EC774
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		for (;;)
		{
			this.ᜈ.Y = 0;
			this.ᜈ.X = 0;
			this.ᜈ.Width = this.\u170D.Width;
			this.ᜈ.Height = this.\u170D.Height;
			this.ᜉ.X = (int)spr\u17FF.ᜀ((double)this.\u170D.Width, MeasureUnits.EMU);
			this.ᜉ.Y = (int)spr\u17FF.ᜀ((double)this.\u170D.Height, MeasureUnits.EMU);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.\u1717() == (MsoRecords)0)
					{
						num = 1;
						continue;
					}
					goto IL_105;
				case 1:
					base.ᜀ((MsoRecords)61466);
					base.ᜈ(980);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_103;
				}
				break;
			}
		}
		IL_103:
		IL_105:
		this.ᜋ = MsoBlipCompression.msoCompressionDeflate;
		int num2 = 0;
		A_0.Write(this.ᜆ, 0, this.ᜆ.Length);
		num2 += this.ᜆ.Length;
		spr\u1D3B.ᜀ(A_0, this.ᜇ);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜈ.Left);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜈ.Top);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜈ.Right);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜈ.Bottom);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜉ.X);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜉ.Y);
		num2 += 4;
		spr\u1D3B.ᜀ(A_0, this.ᜊ);
		num2 += 4;
		A_0.WriteByte((byte)this.ᜋ);
		num2++;
		A_0.WriteByte((byte)this.ᜌ);
		num2++;
		A_0.Write(this.ᜅ, 0, this.ᜅ.Length);
		num2 += this.ᜅ.Length;
		this.m_iLength = num2;
	}

	// Token: 0x06004CB7 RID: 19639 RVA: 0x002ED998 File Offset: 0x002EC998
	public override void ᜀ(Stream A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_14C:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_69;
			}
			break;
		}
		MemoryStream memoryStream;
		long position;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (this.ᜋ == MsoBlipCompression.msoCompressionDeflate)
				{
					num = 9;
					continue;
				}
				this.ᜄ.Write(this.ᜅ, 0, this.ᜅ.Length);
				num = 7;
				continue;
			case 1:
				num = 6;
				continue;
			case 2:
				if (this.ᜄ != null)
				{
					num = 3;
					continue;
				}
				goto IL_17B;
			case 3:
				if (true)
				{
				}
				this.ᜄ.Close();
				num = 4;
				continue;
			case 4:
				goto IL_17B;
			case 5:
			{
				sprᾲ sprᾲ;
				byte[] array;
				int count;
				if ((count = sprᾲ.ᜀ(array, 0, array.Length)) <= 0)
				{
					goto IL_14C;
				}
				this.ᜄ.Write(array, 0, count);
				num = 10;
				continue;
			}
			case 6:
				goto IL_266;
			case 7:
				goto IL_23C;
			case 8:
				goto IL_12C;
			case 9:
			{
				sprᾲ sprᾲ = new sprᾲ(memoryStream);
				byte[] array = new byte[32768];
				num = 8;
				continue;
			}
			case 10:
				goto IL_12C;
			}
			goto IL_69;
			IL_12C:
			num = 5;
			continue;
			IL_17B:
			this.ᜄ = new MemoryStream();
			long num2 = A_0.Position - position;
			int num3 = (int)((long)this.m_iLength - num2);
			memoryStream = new MemoryStream(num3);
			memoryStream.SetLength((long)num3);
			A_0.Read(memoryStream.GetBuffer(), 0, num3);
			this.ᜅ = new byte[memoryStream.Length];
			memoryStream.Read(this.ᜅ, 0, (int)memoryStream.Length);
			memoryStream.Position = 0L;
			num = 0;
		}
		IL_23C:
		IL_266:
		this.ᜄ.Position = 0L;
		this.\u170D = spr\u17FF.ᜀ(this.ᜄ);
		memoryStream.Close();
		return;
		IL_69:
		position = A_0.Position;
		A_0.Read(this.ᜆ, 0, 16);
		this.ᜁ(A_0);
		this.ᜇ = spr\u1D3B.ᜄ(A_0);
		int num4 = spr\u1D3B.ᜄ(A_0);
		int num5 = spr\u1D3B.ᜄ(A_0);
		int right = spr\u1D3B.ᜄ(A_0);
		int bottom = spr\u1D3B.ᜄ(A_0);
		this.ᜈ = Rectangle.FromLTRB(num4, num5, right, bottom);
		num4 = spr\u1D3B.ᜄ(A_0);
		num5 = spr\u1D3B.ᜄ(A_0);
		this.ᜉ = new Point(num4, num5);
		this.ᜊ = spr\u1D3B.ᜄ(A_0);
		this.ᜋ = (MsoBlipCompression)A_0.ReadByte();
		this.ᜌ = (MsoBlipFilter)A_0.ReadByte();
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06004CB8 RID: 19640 RVA: 0x002EDC58 File Offset: 0x002ECC58
	private void ᜁ(Stream A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06004CB9 RID: 19641 RVA: 0x002EDC94 File Offset: 0x002ECC94
	public new static MemoryStream ᜀ(Image A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			MemoryStream memoryStream;
			for (;;)
			{
				Bitmap bitmap;
				switch (num)
				{
				case 0:
					try
					{
						Graphics graphics = Graphics.FromImage(bitmap);
						try
						{
							IntPtr hdc = graphics.GetHdc();
							GraphicsUnit a_ = GraphicsUnit.Pixel;
							RectangleF bounds = A_0.GetBounds(ref a_);
							MetafileFrameUnit frameUnit = spr\u17B7.ᜀ(a_);
							Metafile metafile = new Metafile(memoryStream, hdc, bounds, frameUnit);
							try
							{
								graphics.ReleaseHdc(hdc);
								Graphics graphics2 = Graphics.FromImage(metafile);
								try
								{
									RectangleF rect = new RectangleF(bounds.X, bounds.Y, bounds.Width - 1f, bounds.Height - 1f);
									graphics2.DrawImage(A_0, rect);
								}
								finally
								{
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_10F;
										case 2:
											((IDisposable)graphics2).Dispose();
											num = 0;
											continue;
										}
										if (graphics2 == null)
										{
											break;
										}
										num = 2;
									}
									IL_10F:;
								}
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_16D;
									case 2:
										for (;;)
										{
											((IDisposable)metafile).Dispose();
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												break;
											default:
												goto IL_15E;
											}
										}
										IL_15E:
										if (false)
										{
										}
										num = 0;
										continue;
									}
									if (metafile == null)
									{
										break;
									}
									num = 2;
								}
								IL_16D:;
							}
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_1AF;
								case 2:
									((IDisposable)graphics).Dispose();
									num = 0;
									continue;
								}
								if (graphics == null)
								{
									break;
								}
								num = 2;
							}
							IL_1AF:;
						}
						goto IL_22D;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 1:
								goto IL_1F7;
							case 2:
								((IDisposable)bitmap).Dispose();
								num = 1;
								continue;
							}
							if (bitmap == null)
							{
								break;
							}
							num = 2;
						}
						IL_1F7:;
					}
					goto IL_1FA;
				case 2:
					goto IL_45;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_1FA:
				memoryStream = new MemoryStream();
				int height = A_0.Height;
				int width = A_0.Width;
				bitmap = new Bitmap(width + 1, height + 1);
				num = 0;
			}
			IL_45:
			return null;
			IL_22D:
			memoryStream.Position = 0L;
			return memoryStream;
		}
		}
	}

	// Token: 0x06004CBA RID: 19642 RVA: 0x002EDF3C File Offset: 0x002ECF3C
	private new static MetafileFrameUnit ᜀ(GraphicsUnit A_0)
	{
		int a_ = 10;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case GraphicsUnit.World:
						goto IL_63;
					case GraphicsUnit.Display:
						return MetafileFrameUnit.GdiCompatible;
					case GraphicsUnit.Pixel:
						return MetafileFrameUnit.Pixel;
					case GraphicsUnit.Point:
						goto IL_8B;
					case GraphicsUnit.Inch:
						return MetafileFrameUnit.Inch;
					case GraphicsUnit.Document:
						return MetafileFrameUnit.Document;
					case GraphicsUnit.Millimeter:
						return MetafileFrameUnit.Millimeter;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_89;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		return MetafileFrameUnit.Pixel;
		IL_63:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return MetafileFrameUnit.Millimeter;
		default:
			if (false)
			{
			}
			return MetafileFrameUnit.Pixel;
		}
		IL_89:
		throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
		IL_8B:
		if (true)
		{
		}
		return MetafileFrameUnit.Point;
	}

	// Token: 0x06004CBB RID: 19643 RVA: 0x002EDFF8 File Offset: 0x002ECFF8
	private new byte[] ᜀ(Stream A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			MemoryStream memoryStream;
			for (;;)
			{
				memoryStream = new MemoryStream();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						byte[] array;
						int a_;
						if ((a_ = A_0.Read(array, 0, 32768)) <= 0)
						{
							num = 4;
							continue;
						}
						spr᥏ spr᥏;
						long length;
						spr᥏.ᜂ(array, 0, a_, A_0.Position + 1L >= length);
						num = 2;
						continue;
					}
					case 1:
					{
						try
						{
							new MD5CryptoServiceProvider().ComputeHash(A_0).CopyTo(this.ᜆ, 0);
							goto IL_78;
						}
						catch (InvalidOperationException)
						{
							new MACTripleDES().ComputeHash(A_0).CopyTo(this.ᜆ, 0);
							goto IL_78;
						}
						goto IL_10C;
						IL_78:
						this.ᜇ = (int)A_0.Length;
						spr᥏ spr᥏ = new spr᥏(memoryStream, CompressionLevel.Best, false);
						byte[] array = new byte[32768];
						A_0.Position = 0L;
						long length = A_0.Length;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_73;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 2:
						goto IL_73;
					case 3:
						goto IL_10C;
					case 4:
						goto IL_139;
					}
					break;
					IL_10C:
					num = 0;
					continue;
					IL_73:
					goto IL_10C;
				}
			}
			IL_139:
			memoryStream.Position = 0L;
			this.m_iLength = A_1;
			this.ᜊ = (int)memoryStream.Length;
			byte[] array2 = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array2, 0, (int)memoryStream.Length);
			return array2;
		}
		}
	}

	// Token: 0x06004CBC RID: 19644 RVA: 0x002EE198 File Offset: 0x002ED198
	protected override object ᜅ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_E6:
			goto IL_A0;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		int num;
		spr\u17B7 spr_u17B;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				return spr_u17B;
			case 1:
				spr_u17B.ᜄ = UtilityMethods.ᜀ(this.ᜄ);
				spr_u17B.ᜄ.Position = 0L;
				num = 4;
				continue;
			case 2:
				if (this.ᜄ != null)
				{
					num = 1;
					continue;
				}
				goto IL_A0;
			case 3:
				spr_u17B.\u170D = spr\u17FF.ᜀ(spr_u17B.ᜄ);
				num = 0;
				continue;
			case 4:
				goto IL_E6;
			case 5:
				if (this.\u170D != null)
				{
					num = 3;
					continue;
				}
				return spr_u17B;
			}
			goto IL_46;
		}
		return spr_u17B;
		IL_46:
		spr_u17B = (spr\u17B7)base.ᜅ();
		spr_u17B.ᜅ = spr\u1CD3.ᜀ(this.ᜅ);
		if (true)
		{
		}
		num = 2;
		goto IL_28;
		IL_A0:
		num = 5;
		goto IL_28;
	}

	// Token: 0x06004CBD RID: 19645 RVA: 0x002EE290 File Offset: 0x002ED290
	protected override void \u171F()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					switch (num)
					{
					case 1:
						this.ᜄ.Close();
						this.ᜄ = null;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (this.ᜄ == null)
					{
						return;
					}
					num = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06004CBE RID: 19646 RVA: 0x002EE318 File Offset: 0x002ED318
	protected override void ᜣ()
	{
		try
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6E;
				case 1:
					this.ᜱ();
					num = 2;
					continue;
				case 2:
					goto IL_66;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					if (this.ᜄ != null)
					{
						num = 1;
						continue;
					}
					break;
				}
				IL_66:
				num = 0;
			}
			IL_6E:;
		}
		finally
		{
			base.ᜣ();
		}
	}

	// Token: 0x040022E8 RID: 8936
	private new const int ᜀ = 32768;

	// Token: 0x040022E9 RID: 8937
	private new const int ᜁ = 0;

	// Token: 0x040022EA RID: 8938
	private new const int ᜂ = 16;

	// Token: 0x040022EB RID: 8939
	private new const int ᜃ = 40;

	// Token: 0x040022EC RID: 8940
	private new MemoryStream ᜄ;

	// Token: 0x040022ED RID: 8941
	private new byte[] ᜅ;

	// Token: 0x040022EE RID: 8942
	private new byte[] ᜆ = new byte[16];

	// Token: 0x040022EF RID: 8943
	private int ᜇ;

	// Token: 0x040022F0 RID: 8944
	private Rectangle ᜈ;

	// Token: 0x040022F1 RID: 8945
	private Point ᜉ;

	// Token: 0x040022F2 RID: 8946
	private new int ᜊ;

	// Token: 0x040022F3 RID: 8947
	private new MsoBlipCompression ᜋ;

	// Token: 0x040022F4 RID: 8948
	private new MsoBlipFilter ᜌ = MsoBlipFilter.msofilterNone;

	// Token: 0x040022F5 RID: 8949
	private new Image \u170D;
}

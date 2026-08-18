using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;
using Spire.Pdf.General.Paper.Base;
using Spire.Pdf.General.Paper.Drawing.Images;

// Token: 0x02000276 RID: 630
internal class spr\u1D53
{
	// Token: 0x060021C0 RID: 8640 RVA: 0x0023191C File Offset: 0x0023091C
	internal static bool ᜀ(SizeF A_0)
	{
		if (A_0.Width != 0f)
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
				return A_0.Height == 0f;
			}
		}
		return true;
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x00231978 File Offset: 0x00230978
	internal static bool ᜀ(spr\u1937 A_0)
	{
		for (;;)
		{
			Spire.Doc.Fields.Shape.ShapeType shapeType = A_0.\u1774();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (shapeType != Spire.Doc.Fields.Shape.ShapeType.OleObject)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return true;
				case 1:
					num = 5;
					continue;
				case 2:
					if (shapeType != Spire.Doc.Fields.Shape.ShapeType.Image)
					{
						goto IL_98;
					}
					return true;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_98;
					default:
						goto IL_80;
					}
					break;
				case 4:
					num = 2;
					continue;
				case 5:
					if (shapeType == Spire.Doc.Fields.Shape.ShapeType.OleControl)
					{
						num = 3;
						continue;
					}
					return false;
				}
				break;
				IL_98:
				num = 1;
			}
		}
		return true;
		IL_80:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x00231A2C File Offset: 0x00230A2C
	internal static LineJoin ᜀ(StrokeJoinStyle A_0)
	{
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
					case StrokeJoinStyle.Bevel:
						return LineJoin.Bevel;
					case StrokeJoinStyle.Miter:
						return LineJoin.Miter;
					case StrokeJoinStyle.Round:
						return LineJoin.Round;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_74;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return LineJoin.Miter;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		return LineJoin.Miter;
		IL_74:
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x00231AB8 File Offset: 0x00230AB8
	internal static LineCap ᜁ(StrokeEndCap A_0)
	{
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
					case StrokeEndCap.Flat:
						return LineCap.Flat;
					case StrokeEndCap.Round:
						return LineCap.Round;
					case StrokeEndCap.Square:
						return LineCap.Square;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return LineCap.Round;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_6C;
				}
				break;
			}
		}
		return LineCap.Round;
		IL_6C:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x00231B44 File Offset: 0x00230B44
	internal static DashCap ᜀ(StrokeEndCap A_0)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (A_0)
					{
					case StrokeEndCap.Flat:
					case StrokeEndCap.Square:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_52;
						}
						break;
					case StrokeEndCap.Round:
						return DashCap.Round;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_74;
				}
				break;
			}
		}
		return DashCap.Round;
		IL_52:
		if (true)
		{
		}
		if (false)
		{
		}
		return DashCap.Flat;
		IL_74:
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x00231BCC File Offset: 0x00230BCC
	internal static byte[] ᜁ(sprỏ A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				array = A_0.ᜂ();
				spr\u2010 spr_u = spr\u1D53.ᜀ(A_0);
				int num = 12;
				for (;;)
				{
					ImageSize imageSize;
					FileFormat a_;
					spr\u2091 spr_u2;
					switch (num)
					{
					case 0:
						if (spr\u2075.ᜀ(imageSize.WidthPixels, imageSize.HeightPixels))
						{
							num = 10;
							continue;
						}
						a_ = A_0.ᜅ();
						num = 8;
						continue;
					case 1:
						if (A_0.ᜅ() == FileFormat.Unknown)
						{
							num = 2;
							continue;
						}
						imageSize = A_0.\u1714();
						num = 0;
						continue;
					case 2:
						goto IL_10E;
					case 3:
						try
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								spr_u2.ᜀ(spr_u);
								MemoryStream memoryStream = new MemoryStream();
								try
								{
									spr_u2.ᜀ(memoryStream, a_);
									array = spr\u1CC6.ᜀ(memoryStream);
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_191;
										case 1:
											((IDisposable)memoryStream).Dispose();
											num = 0;
											continue;
										}
										if (memoryStream == null)
										{
											break;
										}
										num = 1;
									}
									IL_191:;
								}
								break;
							}
							}
							return array;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_1D6;
								case 1:
									((IDisposable)spr_u2).Dispose();
									num = 0;
									continue;
								}
								if (spr_u2 == null)
								{
									break;
								}
								num = 1;
							}
							IL_1D6:;
						}
						goto IL_1D9;
					case 4:
						if (A_0.ᜐ() == ImageType.Metafile)
						{
							num = 5;
							continue;
						}
						goto IL_261;
					case 5:
						goto IL_89;
					case 6:
						if (A_0.ᜅ() != FileFormat.Pict)
						{
							num = 7;
							continue;
						}
						return array;
					case 7:
						num = 1;
						continue;
					case 8:
						if (true)
						{
						}
						if (A_0.ᜐ() != ImageType.Emf)
						{
							num = 9;
							continue;
						}
						goto IL_89;
					case 9:
						num = 4;
						continue;
					case 10:
						return array;
					case 11:
						goto IL_261;
					case 12:
						if (spr_u.ᜀ())
						{
							num = 13;
							continue;
						}
						goto IL_1D9;
					case 13:
						return array;
					}
					break;
					IL_89:
					array = spr\u205F.ᜀ(array, new SizeF((float)imageSize.HorizontalResolution, (float)imageSize.VerticalResolution));
					a_ = FileFormat.Png;
					num = 11;
					continue;
					IL_1D9:
					num = 6;
					continue;
					IL_261:
					spr_u2 = new spr\u2091(array);
					num = 3;
				}
			}
			return array;
			IL_10E:
			return array;
		}
		}
	}

	// Token: 0x060021C6 RID: 8646 RVA: 0x00231E74 File Offset: 0x00230E74
	private static spr\u2010 ᜀ(sprỏ A_0)
	{
		ImageColorModeCore a_;
		for (;;)
		{
			a_ = ImageColorModeCore.None;
			int num = 3;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_4E;
				case 1:
					a_ = ImageColorModeCore.Grayscale;
					num = 0;
					continue;
				case 2:
					a_ = ImageColorModeCore.BlackAndWhite;
					num = 4;
					continue;
				case 3:
					if (A_0.ᜏ())
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					num = 5;
					continue;
				case 4:
					goto IL_A0;
				case 5:
					while (A_0.ᜌ())
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 1;
							goto IL_02;
						}
					}
					goto IL_A2;
				}
				break;
			}
		}
		IL_4E:
		IL_A0:
		IL_A2:
		return new spr\u2010(a_, (float)A_0.ᜀ(), (float)A_0.ᜆ());
	}
}

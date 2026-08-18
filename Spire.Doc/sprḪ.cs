using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;

// Token: 0x02000277 RID: 631
internal class sprḪ
{
	// Token: 0x060021C8 RID: 8648 RVA: 0x00231F4C File Offset: 0x00230F4C
	internal static void ᜀ(spr᪑ A_0, SizeF A_1, Stream A_2, ImageType A_3, spr\u1808 A_4)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_3)
					{
					case ImageType.Emf:
					case ImageType.Metafile:
						goto IL_279;
					case ImageType.Pict:
					case ImageType.Xaml:
					case ImageType.DrawingGroup:
						goto IL_22F;
					case ImageType.Jpeg:
					{
						spr\u2091 spr_u = sprḪ.ᜀ(A_0, A_1, A_4);
						num = 7;
						continue;
					}
					case ImageType.Png:
					{
						spr\u2091 spr_u2 = sprḪ.ᜀ(A_0, A_1, A_4);
						num = 1;
						continue;
					}
					case ImageType.Bitmap:
					{
						spr\u2091 spr_u3 = sprḪ.ᜀ(A_0, A_1, A_4);
						num = 9;
						continue;
					}
					case ImageType.Tiff:
					{
						spr\u2091 spr_u4 = sprḪ.ᜀ(A_0, A_1, A_4);
						num = 5;
						continue;
					}
					default:
						num = 6;
						continue;
					}
					break;
				case 1:
					goto IL_CF;
				case 2:
					if (A_2 == null)
					{
						num = 3;
						continue;
					}
					goto IL_119;
				case 3:
					goto IL_2A2;
				case 4:
					goto IL_1D9;
				case 5:
					goto IL_263;
				case 6:
					num = 4;
					continue;
				case 7:
					goto IL_181;
				case 9:
					try
					{
						spr\u2091 spr_u3;
						spr_u3.ᜂ(A_2);
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A1;
								default:
									goto IL_C6;
								}
								break;
							case 1:
								goto IL_A1;
							}
							spr\u2091 spr_u3;
							if (spr_u3 != null)
							{
								num = 1;
								continue;
							}
							goto IL_CE;
							IL_A1:
							((IDisposable)spr_u3).Dispose();
							num = 0;
						}
						IL_C6:
						if (false)
						{
						}
						IL_CE:;
					}
					goto Block_3;
				case 10:
					goto IL_64;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 2;
				continue;
				IL_119:
				if (true)
				{
				}
				num = 0;
				continue;
				Block_3:
				try
				{
					IL_CF:
					spr\u2091 spr_u2;
					spr_u2.ᜁ(A_2);
					return;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						spr\u2091 spr_u2;
						switch (num)
						{
						case 1:
							((IDisposable)spr_u2).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_116;
						}
						if (spr_u2 == null)
						{
							break;
						}
						num = 1;
					}
					IL_116:;
				}
				goto IL_119;
			}
			IL_64:
			throw new ArgumentNullException(ClipboardData.b("๮Űr", a_));
			IL_181:
			try
			{
				spr\u2091 spr_u;
				spr_u.ᜀ(A_2, A_4.ᜋ());
				return;
			}
			finally
			{
				num = 1;
				for (;;)
				{
					spr\u2091 spr_u;
					switch (num)
					{
					case 0:
						goto IL_22C;
					case 2:
						((IDisposable)spr_u).Dispose();
						num = 0;
						continue;
					}
					if (spr_u == null)
					{
						break;
					}
					num = 2;
				}
				IL_22C:;
			}
			IL_1D9:
			IL_22F:
			throw new InvalidOperationException(ClipboardData.b("㩮ὰᙲ൴ݶᱸ᡺ॼ᩾ꎂ꾎릘ﶚ철슢톤螦\udba8캪\udcac\udaae풰삲솴튶\uddb8閺", a_));
			IL_263:
			try
			{
				spr\u2091 spr_u4;
				spr_u4.ᜀ(A_2, A_4.ᜇ());
			}
			finally
			{
				num = 1;
				for (;;)
				{
					spr\u2091 spr_u4;
					switch (num)
					{
					case 0:
						((IDisposable)spr_u4).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_2F2;
					}
					if (spr_u4 == null)
					{
						break;
					}
					num = 0;
				}
				IL_2F2:;
			}
			return;
			IL_279:
			sprḪ.ᜀ(A_0, A_1, A_4, A_2);
			return;
			IL_2A2:
			throw new ArgumentNullException(ClipboardData.b("ᱮհŲၴᙶᑸ", a_));
		}
		}
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x00232284 File Offset: 0x00231284
	internal static spr\u2091 ᜀ(spr᪑ A_0, SizeF A_1, spr\u1808 A_2)
	{
		Size a_ = spr\u23C4.ᜀ(A_1, A_2.ᜂ(), (double)A_2.ᜅ());
		spr\u2091 spr_u = new spr\u2091(a_.Width, a_.Height, A_2.ᜅ(), A_2.ᜅ(), A_2.ᜃ());
		spr\u2616 spr_u2 = new spr\u2616(spr_u);
		try
		{
			if (true)
			{
			}
			sprḪ.ᜀ(A_0, spr_u2, a_, A_2);
		}
		finally
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7F;
					default:
						goto IL_A3;
					}
					break;
				case 2:
					goto IL_7F;
				}
				if (spr_u2 != null)
				{
					num = 2;
					continue;
				}
				goto IL_AB;
				IL_7F:
				((IDisposable)spr_u2).Dispose();
				num = 1;
			}
			IL_A3:
			if (false)
			{
			}
			IL_AB:;
		}
		spr_u.ᜀ(new spr\u2010(A_2.ᜉ(), A_2.ᜁ(), A_2.ᜆ()));
		return spr_u;
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x00232378 File Offset: 0x00231378
	private static void ᜀ(spr᪑ A_0, SizeF A_1, spr\u1808 A_2, Stream A_3)
	{
		int a_ = 16;
		switch (0)
		{
		default:
			for (;;)
			{
				Bitmap bitmap = null;
				Graphics graphics = null;
				IntPtr intPtr = IntPtr.Zero;
				Metafile metafile = null;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						try
						{
							bitmap = new Bitmap(1, 1);
							bitmap.SetResolution(96f, 96f);
							graphics = spr\u205F.ᜀ(bitmap);
							intPtr = graphics.GetHdc();
							Size a_2 = spr\u23C4.ᜀ(A_1, A_2.ᜂ(), 96.0);
							metafile = new Metafile(A_3, intPtr, new RectangleF(0f, 0f, (float)a_2.Width, (float)a_2.Height), MetafileFrameUnit.Pixel, A_2.ᜈ());
							spr\u2616 spr_u = new spr\u2616(metafile);
							try
							{
								sprḪ.ᜀ(A_0, spr_u, a_2, A_2);
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_119;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_119;
										default:
											goto IL_13F;
										}
										break;
									}
									if (spr_u != null)
									{
										num = 0;
										continue;
									}
									goto IL_147;
									IL_119:
									((IDisposable)spr_u).Dispose();
									num = 2;
								}
								IL_13F:
								if (false)
								{
								}
								IL_147:;
							}
							goto IL_43;
						}
						finally
						{
							num = 13;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (true)
									{
									}
									goto IL_1FA;
								case 1:
									graphics.Dispose();
									num = 0;
									continue;
								case 2:
									num = 12;
									continue;
								case 3:
									metafile.Dispose();
									num = 7;
									continue;
								case 4:
									goto IL_1B7;
								case 5:
									if (bitmap != null)
									{
										num = 11;
										continue;
									}
									goto IL_282;
								case 6:
									if (intPtr != IntPtr.Zero)
									{
										num = 2;
										continue;
									}
									goto IL_1BC;
								case 7:
									goto IL_1D3;
								case 8:
									goto IL_1BC;
								case 9:
									graphics.ReleaseHdc(intPtr);
									num = 8;
									continue;
								case 10:
									if (graphics != null)
									{
										num = 1;
										continue;
									}
									goto IL_1FA;
								case 11:
									bitmap.Dispose();
									num = 4;
									continue;
								case 12:
									if (graphics != null)
									{
										num = 9;
										continue;
									}
									goto IL_1BC;
								}
								if (metafile != null)
								{
									num = 3;
									continue;
								}
								goto IL_1D3;
								IL_1BC:
								num = 10;
								continue;
								IL_1D3:
								num = 6;
								continue;
								IL_1FA:
								num = 5;
							}
							IL_1B7:
							IL_282:;
						}
						goto IL_283;
						IL_43:
						num = 2;
						continue;
					case 1:
						goto IL_65;
					case 2:
						if (A_3 == null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
			IL_65:
			IL_283:
			throw new ArgumentNullException(ClipboardData.b("յ౷ࡹ᥻ώ", a_));
		}
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x00232650 File Offset: 0x00231650
	private static void ᜀ(spr᪑ A_0, spr\u2616 A_1, Size A_2, spr\u1808 A_3)
	{
		int a_ = 15;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_3.ᜀ())
				{
					goto IL_82;
				}
				goto IL_EA;
			case 1:
				goto IL_A5;
			case 2:
				A_1.ᜀ();
				num = 3;
				continue;
			case 3:
				goto IL_EA;
			case 5:
				if (A_3.ᜊ())
				{
					num = 7;
					continue;
				}
				goto IL_128;
			case 6:
				goto IL_D4;
			case 7:
				A_1.ᜃ();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_82;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 8:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				A_1.ᜅ();
				num = 0;
				continue;
			case 9:
				goto IL_4C;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 8;
			continue;
			IL_82:
			num = 2;
			continue;
			IL_EA:
			num = 5;
		}
		IL_4C:
		throw new ArgumentNullException(ClipboardData.b("ᑴݶ੸", a_));
		IL_A5:
		throw new ArgumentNullException(ClipboardData.b("ቴᅶŸ", a_));
		IL_D4:
		IL_128:
		A_1.ᜀ(A_3.ᜄ(), 0f, 0f, (float)A_2.Width, (float)A_2.Height);
		A_1.ᜀ(A_3.ᜂ());
		spr\u23A8 spr_u23A = new spr\u23A8();
		spr_u23A.ᜀ(A_0, A_1.ᜆ());
	}
}

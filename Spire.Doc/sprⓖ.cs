using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;

// Token: 0x0200027C RID: 636
internal class spr\u24D6
{
	// Token: 0x060021F8 RID: 8696 RVA: 0x00233DF4 File Offset: 0x00232DF4
	public spr\u24D6(int A_0, int A_1, Stream A_2, Spire.Doc.Documents.ImageFormat A_3)
	{
		if (A_3 == Spire.Doc.Documents.ImageFormat.Emf && A_2 == null)
		{
			A_2 = new MemoryStream();
		}
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x00233E1C File Offset: 0x00232E1C
	public spr\u24D6()
	{
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x00233E30 File Offset: 0x00232E30
	public spr\u24D6(int A_0, int A_1, string A_2, Spire.Doc.Documents.ImageFormat A_3)
	{
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x00233E44 File Offset: 0x00232E44
	internal void ᜀ(Document A_0, ImageType A_1, bool A_2)
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
		spr\u1A69 spr_u1A = new spr\u1A69();
		spr_u1A.ᜢ().IsHidden = A_2;
		spr_u1A.ᜀ(A_0);
		spr_u1A.ᜀ(A_1);
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x00233EA0 File Offset: 0x00232EA0
	public Image[] ᜀ(Document A_0, ImageType A_1)
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
		return this.ᜀ(A_0, A_1, null);
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x00233EE4 File Offset: 0x00232EE4
	public Image[] ᜀ(Document A_0, ImageType A_1, MemoryStream A_2)
	{
		Image[] result;
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
			spr\u1A69 spr_u1A = new spr\u1A69();
			try
			{
				spr_u1A.ᜁ(A_0);
				result = spr_u1A.ᜀ(0, -1, A_1, A_2, true);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)spr_u1A).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (spr_u1A == null)
					{
						goto IL_85;
					}
					num = 0;
				}
				IL_7B:
				if (true)
				{
				}
				IL_85:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x00233F88 File Offset: 0x00232F88
	public Stream ᜀ(int A_0, Document A_1, System.Drawing.Imaging.ImageFormat A_2)
	{
		Stream result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				MemoryStream memoryStream = new MemoryStream();
				spr\u1A69 spr_u1A = new spr\u1A69();
				try
				{
					for (;;)
					{
						spr_u1A.ᜁ(A_1);
						int num = 10;
						for (;;)
						{
							switch (num)
							{
							case 0:
								result = spr_u1A.ᜀ(A_0, 1, ImageType.Metafile, null);
								num = 8;
								continue;
							case 1:
								num = 5;
								continue;
							case 2:
								if (true)
								{
								}
								num = 4;
								continue;
							case 3:
								goto IL_10D;
							case 4:
								goto IL_167;
							case 5:
							{
								Image[] array;
								if (array[A_0] != null)
								{
									num = 6;
									continue;
								}
								goto IL_FF;
							}
							case 6:
							{
								Image[] array;
								array[A_0].Save(memoryStream, A_2);
								num = 2;
								continue;
							}
							case 7:
							{
								Image[] array;
								if (array != null)
								{
									num = 11;
									continue;
								}
								goto IL_FF;
							}
							case 8:
								goto IL_DB;
							case 9:
							{
								Image[] array;
								if (array.Length > A_0)
								{
									num = 1;
									continue;
								}
								goto IL_FF;
							}
							case 10:
							{
								if (A_2 == System.Drawing.Imaging.ImageFormat.Emf)
								{
									num = 0;
									continue;
								}
								Image[] array = spr_u1A.ᜀ(A_0, 1, ImageType.Metafile, null, true);
								num = 7;
								continue;
							}
							case 11:
								num = 9;
								continue;
							}
							break;
							IL_FF:
							result = null;
							num = 3;
						}
					}
					IL_DB:
					IL_10D:
					break;
					IL_167:
					return memoryStream;
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)spr_u1A).Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_1A7;
						}
						if (spr_u1A == null)
						{
							break;
						}
						num = 0;
					}
					IL_1A7:;
				}
				break;
			}
			}
			break;
		}
		return result;
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x0023415C File Offset: 0x0023315C
	public Image[] ᜀ(int A_0, int A_1, Document A_2, ImageType A_3)
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
		spr\u1A69 spr_u1A = new spr\u1A69();
		spr_u1A.ᜁ(A_2);
		return spr_u1A.ᜀ(A_0, A_1, A_3, null, true);
	}
}

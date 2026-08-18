using System;
using System.IO;

// Token: 0x02000303 RID: 771
internal static class spr\u17C7
{
	// Token: 0x060029EA RID: 10730 RVA: 0x0029C628 File Offset: 0x0029B628
	internal static Stream ᜀ(Stream A_0)
	{
		Stream stream = new MemoryStream();
		StreamReader streamReader = new StreamReader(A_0);
		try
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				StreamWriter streamWriter = new StreamWriter(stream);
				try
				{
					streamWriter.Write(streamReader.ReadToEnd());
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)streamWriter).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_8B;
						}
						if (streamWriter == null)
						{
							break;
						}
						num = 0;
					}
					IL_8B:;
				}
				break;
			}
			}
		}
		finally
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					((IDisposable)streamReader).Dispose();
					num = 1;
					continue;
				case 1:
					goto IL_C7;
				}
				if (streamReader == null)
				{
					break;
				}
				num = 0;
			}
			IL_C7:;
		}
		A_0.Position = 0L;
		stream.Position = 0L;
		return stream;
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x0029C72C File Offset: 0x0029B72C
	public static int ᜀ(double A_0, double A_1)
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
		return spr\u17C7.ᜀ(A_0, A_1, 0.5);
	}

	// Token: 0x060029EC RID: 10732 RVA: 0x0029C778 File Offset: 0x0029B778
	public static int ᜀ(double A_0, double A_1, double A_2)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 - A_1 < A_2)
				{
					num = 6;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				default:
					goto IL_B5;
				}
				break;
			case 1:
				goto IL_3C;
			case 2:
				if (A_0 < A_1)
				{
					num = 5;
					continue;
				}
				return 0;
			case 3:
				return 0;
			case 5:
				num = 7;
				continue;
			case 6:
				return 0;
			case 7:
				if (A_1 - A_0 < A_2)
				{
					num = 3;
					continue;
				}
				return -1;
			}
			if (A_0 > A_1)
			{
				num = 1;
				continue;
			}
			num = 2;
			continue;
			IL_3C:
			num = 0;
		}
		return 0;
		IL_B5:
		if (false)
		{
		}
		return 1;
	}

	// Token: 0x040024BE RID: 9406
	public const double ᜀ = 0.5;
}

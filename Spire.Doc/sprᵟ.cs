using System;
using System.Drawing;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;
using Spire.Pdf.General.Paper.Base;

// Token: 0x020002CD RID: 717
internal class spr\u1D5F
{
	// Token: 0x060026B4 RID: 9908 RVA: 0x002633B0 File Offset: 0x002623B0
	internal spr\u1D5F(Document A_0, spr\u227F A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x002633D4 File Offset: 0x002623D4
	internal void ᜀ(Image A_0)
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			MemoryStream memoryStream;
			switch (num)
			{
			case 1:
				goto IL_42;
			case 2:
				try
				{
					spr\u2091.ᜀ(A_0, memoryStream);
					this.ᜁ(memoryStream);
					return;
				}
				finally
				{
					for (;;)
					{
						IL_54:
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A7;
							case 2:
								((IDisposable)memoryStream).Dispose();
								num = 0;
								continue;
							}
							if (memoryStream == null)
							{
								goto IL_A9;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_54;
							default:
								if (false)
								{
								}
								num = 2;
								break;
							}
						}
					}
					IL_A7:
					IL_A9:;
				}
				goto IL_AA;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_AA:
			memoryStream = new MemoryStream();
			num = 2;
		}
		IL_42:
		throw new ArgumentNullException(ClipboardData.b("ᱴ᩶ᡸᱺ᡼", a_));
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x002634C4 File Offset: 0x002624C4
	internal void ᜁ(Stream A_0)
	{
		int a_ = 17;
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
				throw new ArgumentNullException(ClipboardData.b("Ѷ൸ॺ᡼Ṿ", a_));
			}
		}
		this.ᜃ(spr\u1CC6.ᜀ(A_0));
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x00263530 File Offset: 0x00262530
	internal void ᜂ(string A_0)
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		spr\u1CC6.ᜁ(A_0, ClipboardData.b("ཨɪŬ੮㽰ቲᡴቶ", a_));
		this.ᜃ(this.ᜅ(A_0));
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x00263598 File Offset: 0x00262598
	internal Image ᜅ()
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
		return spr\u2091.ᜀ(this.ᜊ());
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x002635E0 File Offset: 0x002625E0
	internal Stream ᜊ()
	{
		byte[] array = this.ᜋ();
		if (sprὊ.ᜀ(array))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				Stream result;
				return result;
			}
			default:
				if (false)
				{
				}
				break;
			}
		}
		else
		{
			try
			{
				return new MemoryStream(this.ᜅ(this.ᜁ.ᜀ()));
			}
			catch (Exception)
			{
				return spr\u1CC6.ᜂ();
			}
		}
		if (true)
		{
		}
		return new MemoryStream(array);
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x00263668 File Offset: 0x00262668
	internal byte[] ᜆ()
	{
		byte[] array = this.ᜋ();
		if (!sprὊ.ᜀ(array))
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
				return this.ᜀ();
			}
		}
		return array;
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x002636BC File Offset: 0x002626BC
	internal void ᜀ(Stream A_0)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			Stream stream;
			switch (num)
			{
			case 0:
				goto IL_3A;
			case 2:
				try
				{
					spr\u1CC6.ᜀ(stream, A_0);
					return;
				}
				finally
				{
					for (;;)
					{
						IL_4B:
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A6;
							case 1:
								((IDisposable)stream).Dispose();
								num = 0;
								continue;
							}
							if (true)
							{
							}
							if (stream == null)
							{
								goto IL_A8;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4B;
							default:
								if (false)
								{
								}
								num = 1;
								break;
							}
						}
					}
					IL_A6:
					IL_A8:;
				}
				goto IL_A9;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_A9:
			stream = this.ᜊ();
			num = 2;
		}
		IL_3A:
		throw new ArgumentNullException(ClipboardData.b("ᥩᡫᱭᕯ፱ᥳ", a_));
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x002637AC File Offset: 0x002627AC
	internal void ᜃ(string A_0)
	{
		int a_ = 17;
		spr\u1CC6.ᜁ(A_0, ClipboardData.b("ᅶၸ᝺᡼ㅾ", a_));
		Stream stream = File.Create(A_0);
		try
		{
			this.ᜀ(stream);
		}
		finally
		{
			for (;;)
			{
				IL_2F:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)stream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_82;
					}
					if (stream == null)
					{
						goto IL_84;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2F;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
			}
			IL_82:
			IL_84:;
		}
		if (true)
		{
		}
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x00263860 File Offset: 0x00262860
	internal void ᜄ(byte[] A_0)
	{
		try
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_55:
				this.ᜂ(A_0);
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_53;
				case 2:
					goto IL_66;
				case 3:
					goto IL_75;
				}
				if (spr\u1D5F.ᜃ != null)
				{
					num = 0;
					continue;
				}
				IL_66:
				this.ᜃ(A_0);
				num = 3;
			}
			IL_53:
			goto IL_55;
			IL_75:;
		}
		catch
		{
		}
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x00263904 File Offset: 0x00262904
	private void ᜂ(byte[] A_0)
	{
		int a_ = 6;
		try
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					Stream stream;
					switch (num)
					{
					case 0:
						goto IL_D2;
					case 1:
						goto IL_18D;
					case 3:
						Directory.CreateDirectory(spr\u1D5F.ᜃ);
						num = 0;
						continue;
					case 4:
						try
						{
							stream.Write(A_0, 0, A_0.Length);
							stream.Flush();
							goto IL_181;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_B5;
								case 1:
									IL_A2:
									((IDisposable)stream).Dispose();
									num = 0;
									continue;
								}
								if (stream != null)
								{
									num = 1;
									continue;
								}
								IL_B5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A2;
								default:
									goto IL_CB;
								}
							}
							IL_CB:
							if (false)
							{
							}
						}
						goto IL_D2;
						IL_181:
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (!Directory.Exists(spr\u1D5F.ᜃ))
					{
						num = 3;
						continue;
					}
					IL_D2:
					string path = string.Concat(new object[]
					{
						spr\u1D5F.ᜃ,
						Path.DirectorySeparatorChar,
						Path.GetFileName(this.ᜀ.FileName),
						ClipboardData.b("䉫", a_),
						++spr\u1D5F.ᜄ,
						ClipboardData.b("䉫", a_),
						spr\u17D1.ᜁ(spr\u2075.\u171B(A_0))
					});
					stream = File.Create(path);
					num = 4;
				}
				IL_18D:
				break;
			}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x00263AEC File Offset: 0x00262AEC
	internal byte[] ᜁ()
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
		return this.ᜅ(this.ᜁ.ᜀ());
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x00263B38 File Offset: 0x00262B38
	internal byte[] ᜅ(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜁ(A_0);
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x00263B80 File Offset: 0x00262B80
	private byte[] ᜁ(string A_0)
	{
		byte[] result = null;
		try
		{
			Stream stream = spr\u1DA1.ᜁ(A_0);
			try
			{
				result = spr\u1CC6.ᜀ(stream);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4F;
					case 2:
						IL_3D:
						((IDisposable)stream).Dispose();
						num = 0;
						continue;
					}
					if (stream != null)
					{
						num = 2;
						continue;
					}
					IL_4F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3D;
					default:
						goto IL_65;
					}
				}
				IL_65:
				if (false)
				{
				}
			}
		}
		catch
		{
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x00263C30 File Offset: 0x00262C30
	private byte[] ᜀ()
	{
		byte[] array = this.ᜁ();
		if (array == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return spr\u1CC6.ᜁ();
			}
		}
		return array;
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x00263C80 File Offset: 0x00262C80
	internal string ᜉ()
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
		return this.ᜁ.ᜀ();
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x00263CC8 File Offset: 0x00262CC8
	internal void ᜄ(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ.ᜀ(A_0);
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x00263D10 File Offset: 0x00262D10
	internal byte[] ᜋ()
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
		return this.ᜁ.ᜁ();
	}

	// Token: 0x060026C6 RID: 9926 RVA: 0x00263D58 File Offset: 0x00262D58
	internal void ᜃ(byte[] A_0)
	{
		this.ᜂ = null;
		if (!sprὊ.ᜀ(A_0))
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
				if (true)
				{
				}
				this.ᜁ.ᜀ(null);
				return;
			}
		}
		this.ᜁ.ᜀ(spr\u1D5F.ᜁ(A_0));
	}

	// Token: 0x060026C7 RID: 9927 RVA: 0x00263DC4 File Offset: 0x00262DC4
	internal static byte[] ᜁ(byte[] A_0)
	{
		int num = 5;
		for (;;)
		{
			spr\u2091 spr_u;
			switch (num)
			{
			case 0:
				A_0 = spr\u2075.ᜀ(A_0, spr\u2075.\u1712(A_0));
				num = 1;
				continue;
			case 1:
				return A_0;
			case 2:
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					try
					{
						spr_u.ᜀ(memoryStream, FileFormat.Png);
						A_0 = spr\u1CC6.ᜀ(memoryStream);
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_9B;
							case 2:
								IL_89:
								((IDisposable)memoryStream).Dispose();
								num = 1;
								continue;
							}
							if (memoryStream != null)
							{
								num = 2;
								continue;
							}
							IL_9B:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_89;
							default:
								goto IL_B1;
							}
						}
						IL_B1:
						if (false)
						{
						}
					}
					return A_0;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)spr_u).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_F4;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 0;
					}
					IL_F4:;
				}
				goto IL_F7;
			case 3:
				if (spr\u2075.ᜆ(A_0))
				{
					num = 0;
					continue;
				}
				return A_0;
			case 4:
				num = 3;
				continue;
			case 5:
				if (true)
				{
				}
				break;
			}
			if (spr\u1D5F.ᜀ(A_0))
			{
				num = 4;
				continue;
			}
			IL_F7:
			spr_u = new spr\u2091(A_0);
			num = 2;
		}
		return A_0;
	}

	// Token: 0x060026C8 RID: 9928 RVA: 0x00263F44 File Offset: 0x00262F44
	internal bool ᜂ()
	{
		if (!this.ᜄ())
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
				if (true)
				{
				}
				return this.ᜇ();
			}
		}
		return true;
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x00263F94 File Offset: 0x00262F94
	internal ImageSize \u170D()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_89;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8B;
				default:
					if (false)
					{
					}
					this.ᜂ = (this.ᜄ() ? new ImageSize(spr\u2075.\u171A(this.ᜋ())) : new ImageSize(0, 0));
					num = 1;
					continue;
				}
				break;
			case 3:
				num = 2;
				continue;
			}
			if (this.ᜂ != null)
			{
				break;
			}
			num = 3;
		}
		IL_89:
		IL_8B:
		if (true)
		{
		}
		return this.ᜂ;
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x00264044 File Offset: 0x00263044
	internal ImageType ᜌ()
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
		return spr\u1D5F.ᜁ(this.ᜃ());
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x0026408C File Offset: 0x0026308C
	internal bool ᜇ()
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
		return spr\u1CC6.ᜋ(this.ᜁ.ᜀ());
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x002640D8 File Offset: 0x002630D8
	internal bool ᜈ()
	{
		if (this.ᜇ())
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
				if (true)
				{
				}
				return !this.ᜄ();
			}
		}
		return false;
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x00264128 File Offset: 0x00263128
	internal bool ᜄ()
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
		return sprὊ.ᜀ(this.ᜋ());
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x00264170 File Offset: 0x00263170
	internal FileFormat ᜃ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9B;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_9B;
			case 3:
				if (this.ᜇ())
				{
					num = 2;
					continue;
				}
				return FileFormat.Unknown;
			case 4:
				if (this.ᜁ.ᜀ().Length > 0)
				{
					num = 5;
					continue;
				}
				return FileFormat.Unknown;
			case 5:
				goto IL_81;
			}
			if (this.ᜄ())
			{
				num = 0;
				continue;
			}
			num = 3;
			continue;
			IL_9B:
			num = 4;
		}
		IL_5C:
		return spr\u2075.\u171B(this.ᜋ());
		IL_81:
		return spr\u2075.ᜁ(this.ᜁ.ᜀ());
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x00264244 File Offset: 0x00263244
	internal static ImageType ᜁ(FileFormat A_0)
	{
		ImageType result;
		for (;;)
		{
			result = ImageType.NoImage;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						switch (A_0)
						{
						case FileFormat.Emf:
						case FileFormat.Wmf:
							result = ImageType.Metafile;
							num = 5;
							continue;
						case FileFormat.Pict:
							result = ImageType.Pict;
							num = 4;
							continue;
						case FileFormat.Jpeg:
							result = ImageType.Jpeg;
							num = 7;
							continue;
						case FileFormat.Png:
							result = ImageType.Pict;
							num = 2;
							continue;
						case FileFormat.Bmp:
							result = ImageType.Bitmap;
							num = 1;
							continue;
						default:
							num = 8;
							continue;
						}
						break;
					}
					break;
				case 1:
					return result;
				case 2:
					return result;
				case 3:
					num = 6;
					continue;
				case 4:
					return result;
				case 5:
					return result;
				case 6:
					return result;
				case 7:
					return result;
				case 8:
					num = 9;
					continue;
				case 9:
					switch (A_0)
					{
					case FileFormat.XamlFixed:
					case FileFormat.XamlFlow:
					case FileFormat.XamlFlowPack:
						result = ImageType.Xaml;
						num = 10;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				case 10:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x00264384 File Offset: 0x00263384
	private static bool ᜀ(byte[] A_0)
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
		return spr\u1D5F.ᜀ(spr\u2075.\u171B(A_0));
	}

	// Token: 0x060026D1 RID: 9937 RVA: 0x002643CC File Offset: 0x002633CC
	private static bool ᜀ(FileFormat A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					if (A_0 != FileFormat.Svg)
					{
						num = 4;
						continue;
					}
					goto IL_90;
				case 2:
					if (true)
					{
					}
					switch (A_0)
					{
					case FileFormat.Emf:
					case FileFormat.Wmf:
					case FileFormat.Pict:
					case FileFormat.Jpeg:
					case FileFormat.Png:
						return true;
					case FileFormat.Bmp:
					case FileFormat.Tiff:
					case FileFormat.Gif:
						goto IL_90;
					default:
						num = 3;
						continue;
					}
					break;
				case 3:
					num = 1;
					continue;
				case 4:
					num = 0;
					continue;
				}
				break;
				IL_90:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_A6;
				}
			}
		}
		return true;
		IL_A6:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x00264488 File Offset: 0x00263488
	internal static void ᜀ(string A_0)
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
		spr\u1D5F.ᜃ = A_0;
	}

	// Token: 0x04002288 RID: 8840
	private readonly Document ᜀ;

	// Token: 0x04002289 RID: 8841
	private readonly spr\u227F ᜁ;

	// Token: 0x0400228A RID: 8842
	private ImageSize ᜂ;

	// Token: 0x0400228B RID: 8843
	private static string ᜃ;

	// Token: 0x0400228C RID: 8844
	private static int ᜄ;
}

using System;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x020000E9 RID: 233
internal class sprᬰ : IDisposable
{
	// Token: 0x060004E1 RID: 1249 RVA: 0x00030650 File Offset: 0x0002F650
	public bool ᜁ()
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
		return this.ᜀ.CanRead;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00030698 File Offset: 0x0002F698
	public bool ᜆ()
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
		return this.ᜀ.CanWrite;
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x000306E0 File Offset: 0x0002F6E0
	public bool ᜄ()
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
		return this.ᜀ.CanSeek;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00030728 File Offset: 0x0002F728
	public long ᜊ()
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
		return this.ᜀ.Length;
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00030770 File Offset: 0x0002F770
	public string ᜃ()
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
		return this.ᜀ.Name;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x000307B8 File Offset: 0x0002F7B8
	public long ᜈ()
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
		return this.ᜀ.Position;
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00030800 File Offset: 0x0002F800
	public void ᜀ(long A_0)
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
		this.ᜀ.Position = A_0;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00030848 File Offset: 0x0002F848
	public sprᬰ(string A_0, FileMode A_1, bool A_2)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
		this.ᜃ = A_2;
		if (this.ᜃ)
		{
			this.ᜀ = new FileStream(this.ᜁ, this.ᜂ);
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00030894 File Offset: 0x0002F894
	private void ᜀ()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					if (this.ᜀ == null)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜀ = new FileStream(this.ᜁ, this.ᜂ);
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				if (this.ᜃ)
				{
					return;
				}
				num = 1;
			}
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00030948 File Offset: 0x0002F948
	public void ᜂ()
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
		this.ᜀ.Close();
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00030990 File Offset: 0x0002F990
	public void ᜇ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜀ.Close();
				this.ᜀ = null;
				if (true)
				{
				}
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (this.ᜀ == null)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00030A18 File Offset: 0x0002FA18
	public void ᜉ()
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
		this.ᜀ.Flush();
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00030A60 File Offset: 0x0002FA60
	public void ᜁ(long A_0, long A_1)
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
		this.ᜀ.Lock(A_0, A_1);
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00030AA8 File Offset: 0x0002FAA8
	public int ᜀ([In] [Out] byte[] A_0, int A_1, int A_2)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					}
					if (false)
					{
					}
					this.ᜀ = new FileStream(this.ᜁ, this.ᜂ);
					num = 4;
					continue;
				case 2:
					num = 3;
					continue;
				case 3:
					if (this.ᜀ == null)
					{
						num = 0;
						continue;
					}
					goto IL_A5;
				case 4:
					goto IL_83;
				}
				if (this.ᜃ)
				{
					goto IL_A5;
				}
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_83:
		IL_A5:
		return this.ᜀ.Read(A_0, A_1, A_2);
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00030B68 File Offset: 0x0002FB68
	public int ᜅ()
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
		this.ᜀ();
		return this.ᜀ.ReadByte();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00030BB4 File Offset: 0x0002FBB4
	public long ᜀ(long A_0, SeekOrigin A_1)
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
		this.ᜀ();
		return this.ᜀ.Seek(A_0, A_1);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00030C04 File Offset: 0x0002FC04
	public void ᜁ(long A_0)
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
		this.ᜀ();
		this.ᜀ.SetLength(A_0);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00030C54 File Offset: 0x0002FC54
	public void ᜀ(long A_0, long A_1)
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
		this.ᜀ();
		this.ᜀ.Unlock(A_0, A_1);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00030CA4 File Offset: 0x0002FCA4
	public void ᜁ(byte[] A_0, int A_1, int A_2)
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
		this.ᜀ();
		this.ᜀ.Write(A_0, A_1, A_2);
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00030CF4 File Offset: 0x0002FCF4
	public void ᜀ(byte A_0)
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
		this.ᜀ();
		this.ᜀ.WriteByte(A_0);
	}

	// Token: 0x04000542 RID: 1346
	private FileStream ᜀ;

	// Token: 0x04000543 RID: 1347
	private string ᜁ;

	// Token: 0x04000544 RID: 1348
	private FileMode ᜂ;

	// Token: 0x04000545 RID: 1349
	private bool ᜃ;
}

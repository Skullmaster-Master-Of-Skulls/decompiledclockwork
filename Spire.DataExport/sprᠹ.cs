using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

// Token: 0x02000130 RID: 304
[DefaultMember("Item")]
internal class sprᠹ : IEnumerable
{
	// Token: 0x0600076C RID: 1900 RVA: 0x0004B4A4 File Offset: 0x0004A4A4
	public sprᠹ(sprᦛ A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0004B4CC File Offset: 0x0004A4CC
	public IEnumerator ᜁ()
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
		return this.ᜀ.GetEnumerator();
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0004B514 File Offset: 0x0004A514
	public int ᜀ(sprᦠ A_0)
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
		return this.ᜀ.Add(A_0);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0004B55C File Offset: 0x0004A55C
	public void ᜀ(int A_0)
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
		this.ᜀ.RemoveAt(A_0);
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0004B5A4 File Offset: 0x0004A5A4
	public void ᜁ(int A_0, sprᦠ A_1)
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
		this.ᜀ.Insert(A_0, A_1);
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0004B5EC File Offset: 0x0004A5EC
	public bool ᜀ(string A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				int num = 0;
				int num2 = this.ᜃ() - 1;
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						A_1 = this.ᜁ(A_1).ᜁ();
						num3 = 3;
						continue;
					case 1:
						goto IL_5F;
					case 2:
						A_1 = num;
						num3 = 8;
						continue;
					case 3:
						return flag;
					case 4:
						goto IL_5F;
					case 5:
					{
						if (num > num2)
						{
							num3 = 2;
							continue;
						}
						int num4 = num + num2 >> 1;
						int num5 = string.Compare(this.ᜁ(num4).ᜂ(), A_0);
						num3 = 6;
						continue;
					}
					case 6:
					{
						int num5;
						if (num5 < 0)
						{
							num3 = 11;
							continue;
						}
						int num4;
						num2 = num4 - 1;
						num3 = 10;
						continue;
					}
					case 7:
						goto IL_5F;
					case 8:
						if (flag)
						{
							num3 = 0;
							continue;
						}
						return flag;
					case 9:
					{
						flag = true;
						int num4;
						num = num4;
						num3 = 4;
						continue;
					}
					case 10:
					{
						int num5;
						if (num5 == 0)
						{
							num3 = 9;
							continue;
						}
						goto IL_5F;
					}
					case 11:
					{
						if (true)
						{
						}
						int num4;
						num = num4 + 1;
						num3 = 1;
						continue;
					}
					}
					break;
					IL_5F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return flag;
					default:
						if (false)
						{
						}
						num3 = 5;
						break;
					}
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0004B770 File Offset: 0x0004A770
	public void ᜀ()
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
		this.ᜀ.Sort(new sprᠹ.ᜀ());
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0004B7BC File Offset: 0x0004A7BC
	private void ᜀ(Stream A_0)
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
		spr\u1DCF a_;
		a_.ᜀ = 60;
		a_.ᜁ = (ushort)this.ᜁ.ᜂ()[this.ᜁ.ᜁ()];
		sprᦛ sprᦛ = this.ᜁ;
		sprᦛ.ᜀ(sprᦛ.ᜁ() + 1);
		byte[] array = spr\u1DCF.ᜀ(a_);
		A_0.Write(array, 0, array.Length);
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0004B850 File Offset: 0x0004A850
	public void ᜀ(int A_0, Stream A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			string text;
			byte value;
			UnicodeEncoding unicodeEncoding;
			ushort value2;
			byte[] bytes;
			int value3;
			for (;;)
			{
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_101:
					if (num <= 0)
					{
						return;
					}
					num2 = 10;
					break;
				default:
				{
					if (false)
					{
					}
					text = this.ᜁ(A_0).ᜂ();
					int length = text.Length;
					num = length * 2 + 3;
					value = 1;
					unicodeEncoding = new UnicodeEncoding();
					num2 = 1;
					break;
				}
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.ᜀ(A_1);
						this.ᜁ.ᜁ(0);
						num2 = 5;
						continue;
					case 1:
					{
						if (this.ᜁ.ᜀ() + num > 8224)
						{
							num2 = 0;
							continue;
						}
						value2 = (ushort)text.Length;
						bytes = BitConverter.GetBytes(value2);
						A_1.Write(bytes, 0, bytes.Length);
						bytes = BitConverter.GetBytes((short)value);
						A_1.Write(bytes, 0, 1);
						bytes = unicodeEncoding.GetBytes(text);
						A_1.Write(bytes, 0, num - 3);
						sprᦛ sprᦛ = this.ᜁ;
						sprᦛ.ᜁ(sprᦛ.ᜀ() + num);
						num2 = 4;
						continue;
					}
					case 2:
						value2 = (ushort)text.Length;
						bytes = BitConverter.GetBytes(value2);
						A_1.Write(bytes, 0, bytes.Length);
						bytes = BitConverter.GetBytes((short)value);
						A_1.Write(bytes, 0, 1);
						bytes = unicodeEncoding.GetBytes(text);
						A_1.Write(bytes, 0, 8221);
						num -= 8221;
						value3 = 0;
						num2 = 9;
						continue;
					case 3:
						goto IL_101;
					case 4:
						goto IL_1E1;
					case 5:
						if (num > 8224)
						{
							num2 = 2;
							continue;
						}
						goto IL_229;
					case 6:
						num2 = 3;
						continue;
					case 7:
						if (num <= 8224)
						{
							num2 = 6;
							continue;
						}
						this.ᜀ(A_1);
						bytes = BitConverter.GetBytes(value3);
						A_1.Write(bytes, 0, 1);
						bytes = unicodeEncoding.GetBytes(text);
						A_1.Write(bytes, 0, 8223);
						num -= 8223;
						num2 = 8;
						continue;
					case 8:
						goto IL_284;
					case 9:
						goto IL_284;
					case 10:
						goto IL_114;
					}
					break;
					IL_284:
					num2 = 7;
				}
			}
			IL_114:
			this.ᜀ(A_1);
			bytes = BitConverter.GetBytes(value3);
			A_1.Write(bytes, 0, 1);
			bytes = unicodeEncoding.GetBytes(text);
			A_1.Write(bytes, 0, num);
			return;
			IL_1E1:
			if (true)
			{
			}
			return;
			IL_229:
			value2 = (ushort)text.Length;
			bytes = BitConverter.GetBytes(value2);
			A_1.Write(bytes, 0, bytes.Length);
			bytes = BitConverter.GetBytes((short)value);
			A_1.Write(bytes, 0, 1);
			bytes = unicodeEncoding.GetBytes(text);
			A_1.Write(bytes, 0, num - 3);
			sprᦛ sprᦛ2 = this.ᜁ;
			sprᦛ2.ᜁ(sprᦛ2.ᜀ() + num);
			return;
		}
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0004BB50 File Offset: 0x0004AB50
	public int ᜃ()
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
		return this.ᜀ.Count;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0004BB98 File Offset: 0x0004AB98
	public sprᦠ ᜁ(int A_0)
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
		return this.ᜀ[A_0] as sprᦠ;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0004BBE4 File Offset: 0x0004ABE4
	public void ᜀ(int A_0, sprᦠ A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0004BC2C File Offset: 0x0004AC2C
	public sprᦛ ᜂ()
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
		return this.ᜁ;
	}

	// Token: 0x040005F0 RID: 1520
	private ArrayList ᜀ = new ArrayList();

	// Token: 0x040005F1 RID: 1521
	private sprᦛ ᜁ;

	// Token: 0x02000131 RID: 305
	private class ᜀ : IComparer
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0004BC70 File Offset: 0x0004AC70
		int IComparer.ᜀ(object A_0, object A_1)
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
			return string.Compare((A_0 as sprᦠ).ᜂ(), (A_1 as sprᦠ).ᜂ(), true);
		}
	}
}

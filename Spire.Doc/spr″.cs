using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

// Token: 0x0200020E RID: 526
[DefaultMember("Item")]
[CLSCompliant(false)]
internal class spr\u2033
{
	// Token: 0x060018BA RID: 6330 RVA: 0x00179510 File Offset: 0x00178510
	internal int ᜁ()
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

	// Token: 0x060018BB RID: 6331 RVA: 0x00179554 File Offset: 0x00178554
	internal void ᜁ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x00179598 File Offset: 0x00178598
	internal string ᜀ(int A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x001795E0 File Offset: 0x001785E0
	internal int ᜀ()
	{
		if (this.ᜀ == null)
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
				return 0;
			}
		}
		return this.ᜀ.Count;
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x00179634 File Offset: 0x00178634
	internal spr\u2033(Stream A_0, int A_1)
	{
		int num = 6;
		long position = A_0.Position;
		byte[] array = new byte[num];
		A_0.Read(array, 0, num);
		this.ᜁ = BitConverter.ToInt32(array, 2);
		this.ᜀ = new List<string>(this.ᜁ);
		for (int i = 0; i < this.ᜁ; i++)
		{
			A_0.Read(array, 0, 2);
			short num2 = BitConverter.ToInt16(array, 0);
			array = new byte[(int)(num2 * 2)];
			A_0.Read(array, 0, array.Length);
			string @string = Encoding.Unicode.GetString(array);
			this.ᜀ.Add(@string);
		}
		long position2 = A_0.Position;
		if (position2 - position > (long)A_1)
		{
			throw new spr\u246D("");
		}
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x001796F8 File Offset: 0x001786F8
	internal spr\u2033()
	{
		this.ᜁ = 0;
		this.ᜀ = new List<string>(this.ᜁ);
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x00179724 File Offset: 0x00178724
	internal void ᜀ(Stream A_0, sprᾱ A_1)
	{
		switch (0)
		{
		default:
		{
			int num2;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_72:
				int num = 6;
				A_1.ᝨ((int)A_0.Position);
				byte[] array = new byte[num];
				byte[] bytes = BitConverter.GetBytes(this.ᜀ.Count);
				bytes.CopyTo(array, 2);
				array[0] = (array[1] = byte.MaxValue);
				A_0.Write(array, 0, array.Length);
				this.ᜁ = this.ᜀ.Count;
				num2 = 0;
				num3 = 6;
				break;
			}
			default:
				if (false)
				{
				}
				num3 = 5;
				break;
			}
			for (;;)
			{
				switch (num3)
				{
				case 0:
				{
					if (num2 >= this.ᜁ)
					{
						num3 = 4;
						continue;
					}
					byte[] bytes2 = Encoding.Unicode.GetBytes(this.ᜀ[num2]);
					byte[] bytes = BitConverter.GetBytes((short)(bytes2.Length / 2));
					A_0.Write(bytes, 0, bytes.Length);
					A_0.Write(bytes2, 0, bytes2.Length);
					num2++;
					num3 = 2;
					continue;
				}
				case 1:
					goto IL_72;
				case 2:
					if (true)
					{
					}
					goto IL_77;
				case 3:
					return;
				case 4:
					A_1.ᝀ((int)(A_0.Position - (long)A_1.ខ()));
					num3 = 3;
					continue;
				case 6:
					goto IL_77;
				}
				if (this.ᜀ.Count > 0)
				{
					num3 = 1;
					continue;
				}
				break;
				IL_77:
				num3 = 0;
			}
			return;
		}
		}
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x001798C0 File Offset: 0x001788C0
	internal void ᜁ(string A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x00179908 File Offset: 0x00178908
	internal int ᜀ(string A_0)
	{
		int result;
		for (;;)
		{
			result = -1;
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= this.ᜀ.Count)
					{
						num2 = 5;
						continue;
					}
					num2 = 3;
					continue;
				case 1:
					return result;
				case 2:
					goto IL_9E;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					if (this.ᜀ(num) == A_0)
					{
						num2 = 6;
						continue;
					}
					num++;
					if (true)
					{
					}
					num2 = 2;
					continue;
				case 4:
					goto IL_9E;
				case 5:
					return result;
				case 6:
					result = num;
					num2 = 1;
					continue;
				}
				break;
				IL_9E:
				num2 = 0;
			}
		}
		return result;
	}

	// Token: 0x04001CC9 RID: 7369
	private List<string> ᜀ;

	// Token: 0x04001CCA RID: 7370
	private int ᜁ;
}

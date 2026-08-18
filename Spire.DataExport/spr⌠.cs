using System;
using System.IO;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.XLS;

// Token: 0x0200000B RID: 11
internal class spr\u2320 : DisposabledObject
{
	// Token: 0x06000048 RID: 72 RVA: 0x00004EF4 File Offset: 0x00003EF4
	public spr\u2320(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
		this.ᜃ = A_2;
		this.ᜄ = A_3;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00004F24 File Offset: 0x00003F24
	protected virtual void ᜀ(bool A_0)
	{
		if (!this.ᜀ)
		{
			if (true)
			{
			}
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜄ != null)
						{
							num = 5;
							continue;
						}
						goto IL_B9;
					case 1:
						goto IL_B9;
					case 2:
						if (this.ᜅ != null)
						{
							num = 8;
							continue;
						}
						goto IL_D9;
					case 4:
						goto IL_D9;
					case 5:
						this.ᜄ = null;
						num = 1;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D9;
						default:
							goto IL_101;
						}
						break;
					case 7:
						this.ᜀ(this, new EventArgs());
						num = 0;
						continue;
					case 8:
						this.ᜅ.Dispose();
						this.ᜅ = null;
						num = 4;
						continue;
					}
					if (A_0)
					{
						num = 7;
						continue;
					}
					goto IL_D9;
					IL_B9:
					num = 2;
					continue;
					IL_D9:
					this.ᜀ = true;
					num = 6;
				}
				IL_101:
				if (false)
				{
				}
			}
			finally
			{
				base.Dispose(A_0);
			}
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005054 File Offset: 0x00004054
	private void ᜀ(object A_0, EventArgs A_1)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_66;
			case 2:
				return;
			case 3:
				if (this.ᜆ != null)
				{
					num = 4;
					continue;
				}
				return;
			case 4:
				this.ᜆ(A_0, A_1);
				num = 2;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (A_1 == null)
				{
					num = 1;
				}
				else
				{
					num = 3;
				}
				break;
			}
		}
		IL_66:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("⨦⌨椪䐬䤮地愲倴吶嘸䤺夼Ծ筀ᅂ⑄⹆㩈⹊ौ⩎≐❒❔㡖⁘睚⭜㹞፠奢d", a_));
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005110 File Offset: 0x00004110
	protected virtual int ᜧ()
	{
		int num;
		for (;;)
		{
			num = sizeof(spr\u1DCF) + (int)this.ᜃ;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						goto IL_72;
					}
					break;
				case 1:
					num += this.ᜡ().ᜥ();
					num2 = 0;
					continue;
				case 2:
					goto IL_34;
				}
				break;
				IL_34:
				if (this.ᜅ == null)
				{
					return num;
				}
				num2 = 1;
			}
		}
		IL_72:
		if (true)
		{
		}
		if (false)
		{
		}
		return num;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000051A0 File Offset: 0x000041A0
	public void ᜀ(spr\u20E7 A_0)
	{
		int a_ = 6;
		if (this.ᜅ != null)
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
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("次䨣倥䤧䘩䔫䨭缯䈱儳䐵夷丹唻儽⸿ᵁŃ㹅⭇⽉⁋ݍ㹏⅑⁓⑕㭗⹙㕛ㅝ๟", a_)));
			}
		}
		this.ᜅ = A_0;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00005214 File Offset: 0x00004214
	public virtual void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				ushort num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_205;
				case 1:
					goto IL_12B;
				case 2:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F4;
					default:
						if (false)
						{
						}
						num2 = 8220;
						num = 6;
						continue;
					}
					break;
				case 5:
					if (this.ᜅ != null)
					{
						num = 14;
						continue;
					}
					return;
				case 6:
					goto IL_24B;
				case 7:
					goto IL_205;
				case 8:
					goto IL_24B;
				case 9:
					if (num3 <= 0)
					{
						num = 1;
						continue;
					}
					num = 12;
					continue;
				case 10:
					A_0.Write(this.ᜄ, 0, (int)num2);
					num3 -= (int)num2;
					num4 += (int)num2;
					num = 0;
					continue;
				case 11:
					if (num2 > 0)
					{
						num = 10;
						continue;
					}
					goto IL_12B;
				case 12:
					if (num3 > 8220)
					{
						num = 13;
						continue;
					}
					num2 = (ushort)num3;
					num = 17;
					continue;
				case 13:
					num2 = 8220;
					num = 18;
					continue;
				case 14:
					this.ᜅ.ᜀ(A_0);
					num = 16;
					continue;
				case 15:
					if (num3 > 8220)
					{
						num = 4;
						continue;
					}
					goto IL_1F4;
				case 16:
					return;
				case 17:
					goto IL_80;
				case 18:
					goto IL_80;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				byte[] bytes = BitConverter.GetBytes(this.ᜂ);
				A_0.Write(bytes, 0, bytes.Length);
				num3 = this.ᜌ();
				num2 = 0;
				num4 = 0;
				num = 15;
				continue;
				IL_80:
				ushort value = 60;
				bytes = BitConverter.GetBytes(value);
				A_0.Write(bytes, 0, bytes.Length);
				bytes = BitConverter.GetBytes(num2);
				A_0.Write(bytes, 0, bytes.Length);
				A_0.Write(this.ᜄ, num4, (int)num2);
				num3 -= (int)num2;
				num4 += (int)num2;
				num = 7;
				continue;
				IL_12B:
				num = 5;
				continue;
				IL_1F4:
				num2 = (ushort)num3;
				num = 8;
				continue;
				IL_205:
				num = 9;
				continue;
				IL_24B:
				bytes = BitConverter.GetBytes(num2);
				A_0.Write(bytes, 0, bytes.Length);
				num = 11;
			}
			return;
		}
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000054A4 File Offset: 0x000044A4
	public virtual void ᜨ()
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
		Array.Clear(this.ᜄ, 0, (int)this.ᜃ);
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000054F4 File Offset: 0x000044F4
	public sprᲤ ᜦ()
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

	// Token: 0x06000050 RID: 80 RVA: 0x00005538 File Offset: 0x00004538
	public spr\u24D7 ᜠ()
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
		return this.ᜦ().\u1712().ᜂ().ᜅ();
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00005588 File Offset: 0x00004588
	public sprᬚ ᜤ()
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
		return this.ᜦ().\u1712().ᜂ().ᜉ();
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000055D8 File Offset: 0x000045D8
	public spr\u2504 ᜩ()
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
		return this.ᜦ().\u1712().ᜂ().ᜇ();
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00005628 File Offset: 0x00004628
	public ushort \u170D()
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
		return this.ᜂ;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000566C File Offset: 0x0000466C
	public virtual int ᜌ()
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
		return (int)this.ᜃ;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000056B0 File Offset: 0x000046B0
	public virtual void ᜂ(int A_0)
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
		this.ᜃ = (ushort)A_0;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000056F4 File Offset: 0x000046F4
	public byte[] ᜢ()
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
		return this.ᜄ;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005738 File Offset: 0x00004738
	public int ᜥ()
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
		return this.ᜧ();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000577C File Offset: 0x0000477C
	public spr\u20E7 ᜡ()
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
		return this.ᜅ;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000057C0 File Offset: 0x000047C0
	public EventHandler ᜣ()
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

	// Token: 0x0600005A RID: 90 RVA: 0x00005804 File Offset: 0x00004804
	public void ᜀ(EventHandler A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x04000013 RID: 19
	private bool ᜀ;

	// Token: 0x04000014 RID: 20
	private sprᲤ ᜁ;

	// Token: 0x04000015 RID: 21
	private ushort ᜂ;

	// Token: 0x04000016 RID: 22
	private ushort ᜃ;

	// Token: 0x04000017 RID: 23
	private byte[] ᜄ;

	// Token: 0x04000018 RID: 24
	private spr\u20E7 ᜅ;

	// Token: 0x04000019 RID: 25
	private EventHandler ᜆ;
}

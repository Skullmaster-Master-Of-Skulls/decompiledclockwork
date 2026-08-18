using System;

// Token: 0x02000268 RID: 616
internal class spr\u24A1
{
	// Token: 0x0600205C RID: 8284 RVA: 0x00223214 File Offset: 0x00222214
	internal int ᜂ()
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
		return this.ᜀ;
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x00223258 File Offset: 0x00222258
	internal void ᜀ(int A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x0022329C File Offset: 0x0022229C
	internal int ᜃ()
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
		return this.ᜂ;
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x002232E0 File Offset: 0x002222E0
	internal void ᜁ(int A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x00223324 File Offset: 0x00222324
	internal short ᜀ()
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

	// Token: 0x06002061 RID: 8289 RVA: 0x00223368 File Offset: 0x00222368
	internal void ᜀ(short A_0)
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

	// Token: 0x06002062 RID: 8290 RVA: 0x002233AC File Offset: 0x002223AC
	internal byte[] ᜄ()
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
		byte[] array = new byte[4];
		byte[] bytes = BitConverter.GetBytes(this.ᜀ);
		bytes.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x00223404 File Offset: 0x00222404
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
		byte[] array = new byte[4];
		byte[] bytes = BitConverter.GetBytes(this.ᜁ);
		bytes.CopyTo(array, 0);
		bytes = BitConverter.GetBytes((ushort)this.ᜂ);
		bytes.CopyTo(array, 2);
		return array;
	}

	// Token: 0x0400206A RID: 8298
	private int ᜀ;

	// Token: 0x0400206B RID: 8299
	private short ᜁ;

	// Token: 0x0400206C RID: 8300
	private int ᜂ;
}

using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000007 RID: 7
[DefaultMember("Item")]
internal class spr\u2574 : IEnumerable
{
	// Token: 0x06000028 RID: 40 RVA: 0x00003E4C File Offset: 0x00002E4C
	public spr\u2574(spr\u219E A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003E74 File Offset: 0x00002E74
	public IEnumerator ᜇ()
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

	// Token: 0x0600002A RID: 42 RVA: 0x00003EBC File Offset: 0x00002EBC
	public int ᜁ(object A_0)
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

	// Token: 0x0600002B RID: 43 RVA: 0x00003F04 File Offset: 0x00002F04
	public void ᜂ(int A_0)
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

	// Token: 0x0600002C RID: 44 RVA: 0x00003F4C File Offset: 0x00002F4C
	public object ᜋ()
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
		return this.ᜀ(0);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003F90 File Offset: 0x00002F90
	public int ᜂ(object A_0)
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
		return this.ᜀ.IndexOf(A_0);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003FD8 File Offset: 0x00002FD8
	public void ᜁ(int A_0, object A_1)
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
		this.ᜀ.Insert(A_0, A_1);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00004020 File Offset: 0x00003020
	public object ᜉ()
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
		return this.ᜀ(this.ᜀ.Count - 1);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00004070 File Offset: 0x00003070
	public int ᜀ(object A_0)
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
		int result = this.ᜂ(A_0);
		this.ᜀ.Remove(A_0);
		return result;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000040C0 File Offset: 0x000030C0
	public void ᜊ()
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
		this.ᜀ.Clear();
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00004108 File Offset: 0x00003108
	public void ᜀ()
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
		this.ᜀ.Sort();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004150 File Offset: 0x00003150
	public void ᜀ(IComparer A_0)
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
		this.ᜀ.Sort(A_0);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004198 File Offset: 0x00003198
	public int ᜌ()
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

	// Token: 0x06000035 RID: 53 RVA: 0x000041E0 File Offset: 0x000031E0
	public object ᜀ(int A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004228 File Offset: 0x00003228
	public void ᜀ(int A_0, object A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00004270 File Offset: 0x00003270
	public spr\u219E ᜈ()
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

	// Token: 0x0400000A RID: 10
	private ArrayList ᜀ = new ArrayList();

	// Token: 0x0400000B RID: 11
	private spr\u219E ᜁ;
}

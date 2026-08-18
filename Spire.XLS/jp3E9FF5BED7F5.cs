using System;
using System.Runtime.InteropServices;

// Token: 0x02000002 RID: 2
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class jp3E9FF5BED7F5 : Attribute
{
	// Token: 0x06000001 RID: 1 RVA: 0x000034D0 File Offset: 0x000024D0
	public jp3E9FF5BED7F5(string a, int c, bool b)
	{
		this.a = a;
		this.c = c;
		this.b = b;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x000034F8 File Offset: 0x000024F8
	public string A
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000003 RID: 3 RVA: 0x0000350C File Offset: 0x0000250C
	public bool B
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00003520 File Offset: 0x00002520
	public int C
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x04000001 RID: 1
	private string a;

	// Token: 0x04000002 RID: 2
	private bool b;

	// Token: 0x04000003 RID: 3
	private int c;
}

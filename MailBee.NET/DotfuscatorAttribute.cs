using System;
using System.Runtime.InteropServices;

// Token: 0x02000002 RID: 2
[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(false)]
public sealed class DotfuscatorAttribute : Attribute
{
	// Token: 0x06000001 RID: 1 RVA: 0x00003174 File Offset: 0x00002174
	public DotfuscatorAttribute(string a, int c)
	{
		this.a = a;
		this.c = c;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00003198 File Offset: 0x00002198
	public string A
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000003 RID: 3 RVA: 0x000031AC File Offset: 0x000021AC
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
	private int c;
}

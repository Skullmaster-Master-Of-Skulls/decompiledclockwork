using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200050B RID: 1291
[CompilerGenerated]
internal sealed class a<a, b>
{
	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000CBC05 File Offset: 0x000CAC05
	public a len
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000CBC0D File Offset: 0x000CAC0D
	public b possibleMatch
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x000CBC15 File Offset: 0x000CAC15
	[DebuggerHidden]
	public a(a A_0, b A_1)
	{
		this.a = A_0;
		this.b = A_1;
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x000CBC2C File Offset: 0x000CAC2C
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		global::a<a, b> a = value as global::a<a, b>;
		return a != null && EqualityComparer<a>.Default.Equals(this.a, a.a) && EqualityComparer<b>.Default.Equals(this.b, a.b);
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x000CBC73 File Offset: 0x000CAC73
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (2002791129 * -1521134295 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b);
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x000CBCA8 File Offset: 0x000CACA8
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ len = {0}, possibleMatch = {1} }}";
		object[] array = new object[2];
		int num = 0;
		a a = this.a;
		ref a ptr = ref a;
		a a2 = default(a);
		object obj;
		if (a2 == null)
		{
			a2 = a;
			ptr = ref a2;
			if (a2 == null)
			{
				obj = null;
				goto IL_46;
			}
		}
		obj = ptr.ToString();
		IL_46:
		array[num] = obj;
		int num2 = 1;
		b b = this.b;
		ref b ptr2 = ref b;
		b b2 = default(b);
		object obj2;
		if (b2 == null)
		{
			b2 = b;
			ptr2 = ref b2;
			if (b2 == null)
			{
				obj2 = null;
				goto IL_81;
			}
		}
		obj2 = ptr2.ToString();
		IL_81:
		array[num2] = obj2;
		return string.Format(provider, format, array);
	}

	// Token: 0x04001D90 RID: 7568
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly a a;

	// Token: 0x04001D91 RID: 7569
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly b b;
}

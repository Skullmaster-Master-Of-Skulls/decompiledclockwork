using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Spire.CompoundFile.Doc;

// Token: 0x020001F3 RID: 499
[Serializable]
internal class spr\u1FA8 : Exception
{
	// Token: 0x060015C8 RID: 5576 RVA: 0x0016051C File Offset: 0x0015F51C
	public spr\u1FA8()
	{
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x00160530 File Offset: 0x0015F530
	public spr\u1FA8(string A_0) : base(A_0)
	{
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x00160544 File Offset: 0x0015F544
	public spr\u1FA8(string A_0, spr\u251B A_1) : base(A_0)
	{
		if (A_1 != null)
		{
			this.ᜀ = A_1.ᜇ();
		}
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x0016056C File Offset: 0x0015F56C
	public spr\u1FA8(string A_0, Exception A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x00160584 File Offset: 0x0015F584
	protected spr\u1FA8(SerializationInfo A_0, StreamingContext A_1)
	{
		int a_ = 3;
		base..ctor(A_0, A_1);
		if (A_0 != null)
		{
			this.ᜀ = A_0.GetString(ClipboardData.b("౨ժᥬٮհੲ㙴ᡶ᝸ེ᡼ݾ", a_));
		}
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x001605C4 File Offset: 0x0015F5C4
	public string ᜀ()
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

	// Token: 0x060015CE RID: 5582 RVA: 0x00160608 File Offset: 0x0015F608
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		int a_ = 6;
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
			if (info != null)
			{
				info.AddValue(ClipboardData.b("५mѯ᭱sཱུ㭷ᕹቻ੽嬨", a_), this.ᜀ);
				base.GetObjectData(info, context);
				return;
			}
			break;
		}
		throw new ArgumentNullException(ClipboardData.b("իmᙯᵱ", a_));
	}

	// Token: 0x040019E5 RID: 6629
	private string ᜀ;
}

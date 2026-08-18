using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interface;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000259 RID: 601
[DefaultMember("Item")]
internal class spr\u1AA2 : CollectionExtended<DocumentProperty>, ICustomDocumentProperties
{
	// Token: 0x060023F4 RID: 9204 RVA: 0x0014F210 File Offset: 0x0014E210
	internal spr\u1AA2(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x0014F230 File Offset: 0x0014E230
	public new Spire.Xls.Core.Interface.IDocumentProperty ᜂ(string A_0)
	{
		for (;;)
		{
			Spire.Xls.Core.Interface.IDocumentProperty documentProperty = this.ᜁ(A_0);
			if (documentProperty != null)
			{
				return documentProperty;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_21;
			}
		}
		IL_21:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜃ(A_0);
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x0014F284 File Offset: 0x0014E284
	Spire.Xls.Core.Interface.IDocumentProperty ICustomDocumentProperties.ᜀ(int A_0)
	{
		int a_ = 5;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8F:
			if (A_0 <= base.Count - 1)
			{
				return base.InnerList[A_0];
			}
			num = 3;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				num = 2;
				continue;
			case 2:
				goto IL_8F;
			case 3:
				goto IL_A2;
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 1;
		}
		IL_65:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("刺匼嬾⑀㭂", a_), RecordTableEnumerator.b("洺尼匾㑀♂敄⑆⡈╊⍌⁎═獒㝔㉖祘㝚㡜ⱞበ䍢ᅤསࡨժ䵬彮兰ቲ᭴፶奸ᱺོ᩾ꦈﾊﾐ뎒놞", a_));
		IL_A2:
		goto IL_65;
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x0014F344 File Offset: 0x0014E344
	internal new Spire.Xls.Core.Interface.IDocumentProperty ᜁ(string A_0)
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
		DocumentProperty result;
		this.ᜂ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x0014F390 File Offset: 0x0014E390
	public void ᜄ(string A_0)
	{
		int num = 2;
		for (;;)
		{
			DocumentProperty item;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜂ.Remove(A_0);
					base.Remove(item);
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 1:
				return;
			}
			IL_26:
			if (this.ᜂ.TryGetValue(A_0, out item))
			{
				num = 0;
				continue;
			}
			break;
			goto IL_26;
		}
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x0014F420 File Offset: 0x0014E420
	public Spire.Xls.Core.Interface.IDocumentProperty ᜃ(string A_0)
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
		DocumentProperty documentProperty = new DocumentProperty(A_0, null);
		this.ᜂ.Add(A_0, documentProperty);
		base.Add(documentProperty);
		return documentProperty;
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x0014F478 File Offset: 0x0014E478
	public new bool ᜀ(string A_0)
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
		return this.ᜂ.ContainsKey(A_0);
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x0014F4C0 File Offset: 0x0014E4C0
	[CLSCompliant(false)]
	internal new void ᜀ(spr\u22A9 A_0)
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
		XlsBuiltInDocumentProperties.ᜀ(A_0, this.ᜂ.Values);
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x0014F50C File Offset: 0x0014E50C
	[CLSCompliant(false)]
	internal new void ᜀ(sprᮓ A_0)
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
		XlsBuiltInDocumentProperties.ᜀ(A_0, spr\u1AA2.ᜁ, this.ᜂ.Values);
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x0014F560 File Offset: 0x0014E560
	internal new void ᜁ(sprᮓ A_0)
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
		XlsBuiltInDocumentProperties.ᜀ(A_0, spr\u1AA2.ᜁ, this.ᜂ, base.List, true, false);
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x0014F5B4 File Offset: 0x0014E5B4
	[CLSCompliant(false)]
	internal new void ᜀ(sprណ A_0)
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
			switch (0)
			{
			}
			break;
		}
		for (;;)
		{
			List<spr\u22A9> list = A_0.ᜀ();
			int num = 0;
			int count = list.Count;
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					spr\u22A9 spr_u22A;
					XlsBuiltInDocumentProperties.ᜀ(spr_u22A, this.ᜂ, base.InnerList, true, false);
					num2 = 1;
					continue;
				}
				case 1:
					goto IL_74;
				case 2:
					goto IL_D3;
				case 3:
				{
					spr\u22A9 spr_u22A;
					if (spr_u22A.ᜃ() == spr\u1AA2.ᜁ)
					{
						num2 = 0;
						continue;
					}
					goto IL_74;
				}
				case 4:
				{
					if (num >= count)
					{
						num2 = 5;
						continue;
					}
					spr\u22A9 spr_u22A = list[num];
					num2 = 3;
					continue;
				}
				case 5:
					return;
				case 6:
					goto IL_D3;
				}
				break;
				IL_74:
				num++;
				num2 = 2;
				continue;
				IL_D3:
				if (true)
				{
				}
				num2 = 4;
			}
		}
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x0014F6BC File Offset: 0x0014E6BC
	protected virtual void ᜀ()
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
		this.ᜂ.Clear();
	}

	// Token: 0x06002400 RID: 9216 RVA: 0x0014F704 File Offset: 0x0014E704
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1AA2()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1AA2.ᜁ = new Guid(RecordTableEnumerator.b("礼ਾɀ݂ń牆祈繊恌絎ᑐ橒ᙔ穖桘歚汜ᵞ䱠婢噤幦幨䙪嵬坮䅰䍲䝴㕶䭸㡺㭼䙾삀욂", a_));
	}

	// Token: 0x04001265 RID: 4709
	internal new const string ᜀ = "D5CDD505-2E9C-101B-9397-08002B2CF9AE";

	// Token: 0x04001266 RID: 4710
	internal new static readonly Guid ᜁ;

	// Token: 0x04001267 RID: 4711
	private new Dictionary<string, DocumentProperty> ᜂ = new Dictionary<string, DocumentProperty>();
}

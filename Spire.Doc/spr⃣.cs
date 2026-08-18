using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents.XML;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x020002D5 RID: 725
[DefaultMember("Item")]
internal class spr\u20E3 : DocumentSerializableCollection
{
	// Token: 0x0600277A RID: 10106 RVA: 0x0027AC0C File Offset: 0x00279C0C
	public OverrideLevelFormat ᜀ(int A_0)
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
		return (OverrideLevelFormat)base.InnerList[this.ᜂ()[A_0]];
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x0027AC64 File Offset: 0x00279C64
	private spr\u177D ᜀ()
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
		return base.OwnerBase as spr\u177D;
	}

	// Token: 0x0600277C RID: 10108 RVA: 0x0027ACAC File Offset: 0x00279CAC
	internal Dictionary<int, int> ᜂ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				IL_2C:
				this.ᜀ = new Dictionary<int, int>();
				num = 2;
				continue;
			case 2:
				goto IL_43;
			}
			if (this.ᜀ == null)
			{
				num = 0;
				continue;
			}
			IL_43:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2C;
			default:
				goto IL_63;
			}
		}
		IL_63:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ;
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x0027AD30 File Offset: 0x00279D30
	internal void ᜀ(Dictionary<int, int> A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x0027AD74 File Offset: 0x00279D74
	internal spr\u20E3(Document A_0) : base(A_0, A_0)
	{
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x0027AD8C File Offset: 0x00279D8C
	internal int ᜀ(int A_0, OverrideLevelFormat A_1)
	{
		int num;
		for (;;)
		{
			A_1.ᜀ(this.ᜀ());
			num = base.InnerList.Add(A_1);
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						goto IL_86;
					}
					break;
				case 1:
					this.ᜂ()[A_0] = num;
					num2 = 2;
					continue;
				case 2:
					return num;
				case 3:
					if (this.ᜂ().ContainsKey(A_0))
					{
						goto IL_47;
					}
					this.ᜂ().Add(A_0, num);
					num2 = 0;
					continue;
				}
				break;
				IL_47:
				num2 = 1;
			}
		}
		IL_86:
		if (true)
		{
		}
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x0027AE4C File Offset: 0x00279E4C
	internal int ᜀ(OverrideLevelFormat A_0)
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (true)
		{
		}
		if (false)
		{
		}
		int num = base.InnerList.IndexOf(A_0);
		int result = num;
		using (Dictionary<int, int>.Enumerator enumerator = this.ᜂ().GetEnumerator())
		{
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_EB;
				case 1:
					goto IL_DF;
				case 3:
				{
					KeyValuePair<int, int> keyValuePair;
					if (keyValuePair.Value == num)
					{
						num2 = 4;
						continue;
					}
					break;
				}
				case 4:
				{
					KeyValuePair<int, int> keyValuePair;
					result = keyValuePair.Key;
					num2 = 1;
					continue;
				}
				case 5:
					goto IL_DF;
				case 6:
				{
					if (!enumerator.MoveNext())
					{
						num2 = 5;
						continue;
					}
					KeyValuePair<int, int> keyValuePair = enumerator.Current;
					num2 = 3;
					continue;
				}
				}
				IL_AF:
				num2 = 6;
				continue;
				goto IL_AF;
				IL_DF:
				num2 = 0;
			}
			IL_EB:;
		}
		return result;
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x0027AF68 File Offset: 0x00279F68
	internal bool ᜁ(int A_0)
	{
		while (this.ᜂ().Count > 0)
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
				return this.ᜂ().ContainsKey(A_0);
			}
		}
		return false;
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x0027AFC4 File Offset: 0x00279FC4
	internal virtual void ᜀ(CollectionEx A_0)
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
			base.CloneToImpl(A_0);
			using (Dictionary<int, int>.Enumerator enumerator = this.ᜂ().GetEnumerator())
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B2;
					case 1:
						num = 0;
						continue;
					case 3:
						goto IL_8F;
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						KeyValuePair<int, int> keyValuePair = enumerator.Current;
						(A_0 as spr\u20E3).ᜂ().Add(keyValuePair.Key, keyValuePair.Value);
						num = 3;
						continue;
					}
					}
					goto IL_55;
					IL_8F:
					num = 4;
					continue;
					IL_55:
					if (true)
					{
					}
					goto IL_8F;
				}
				IL_B2:;
			}
			break;
		}
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x0027B0B0 File Offset: 0x0027A0B0
	protected virtual OwnerHolder ᜀ(IXDLSContentReader A_0)
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
		return new OverrideLevelFormat(base.Document);
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x0027B0F8 File Offset: 0x0027A0F8
	protected virtual string ᜁ()
	{
		int a_ = 3;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return ClipboardData.b("٨ᵪ࡬ᵮͰᩲᅴቶ呸᝺᡼ॾ", a_);
	}

	// Token: 0x040022D9 RID: 8921
	private new Dictionary<int, int> ᜀ;
}

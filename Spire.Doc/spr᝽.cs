using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x02000259 RID: 601
internal class spr\u177D : Style
{
	// Token: 0x06001E11 RID: 7697 RVA: 0x001DB040 File Offset: 0x001DA040
	public virtual StyleType ᜀ()
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
		return StyleType.OtherStyle;
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x001DB07C File Offset: 0x001DA07C
	internal spr\u20E3 ᜃ()
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

	// Token: 0x06001E13 RID: 7699 RVA: 0x001DB0C0 File Offset: 0x001DA0C0
	internal spr\u177D(Document A_0) : base(A_0)
	{
		this.ᜀ = new spr\u20E3(A_0);
		this.ᜀ.ᜀ(this);
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x001DB0EC File Offset: 0x001DA0EC
	public virtual IStyle ᜂ()
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
		return (IStyle)this.CloneImpl();
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x001DB134 File Offset: 0x001DA134
	protected virtual object ᜄ()
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
		spr\u177D spr_u177D = (spr\u177D)base.CloneImpl();
		spr_u177D.ᜀ = new spr\u20E3(base.Document);
		this.ᜀ.CloneToImpl(spr_u177D.ᜀ);
		return spr_u177D;
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x001DB1A0 File Offset: 0x001DA1A0
	internal void ᜅ()
	{
		int num = 3;
		for (;;)
		{
			Dictionary<int, int>.Enumerator enumerator;
			switch (num)
			{
			case 0:
				goto IL_145;
			case 1:
				try
				{
					num = 2;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							KeyValuePair<int, int> keyValuePair = enumerator.Current;
							OverrideLevelFormat overrideLevelFormat = this.ᜀ.ᜀ(keyValuePair.Key);
							overrideLevelFormat.ᜁ();
							num = 0;
							continue;
						}
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_DB;
						}
						IL_6C:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						goto IL_6C;
					}
					IL_DB:
					return;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_EB;
			case 2:
				if (this.ᜀ.Count == 0)
				{
					num = 0;
					continue;
				}
				goto IL_EB;
			case 4:
				if (true)
				{
				}
				num = 2;
				continue;
			}
			if (this.ᜀ != null)
			{
				num = 4;
				continue;
			}
			break;
			IL_EB:
			int count = this.ᜀ.Count;
			enumerator = this.ᜀ.ᜂ().GetEnumerator();
			num = 1;
		}
		IL_145:
		this.ᜀ = null;
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x001DB30C File Offset: 0x001DA30C
	internal ListLevel ᜀ(int A_0)
	{
		int a_ = 17;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8A:
			if (A_0 <= this.ᜀ.Count - 1)
			{
				goto IL_C6;
			}
			num = 3;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8A;
			case 1:
				goto IL_80;
			case 3:
				A_0 = this.ᜀ.Count - 1;
				num = 1;
				continue;
			case 4:
				goto IL_67;
			}
			if (true)
			{
			}
			if (A_0 < 0)
			{
				num = 4;
			}
			else
			{
				num = 0;
			}
		}
		IL_67:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᥶౸ᙺὼ᩾", a_), ClipboardData.b("ⅶᡸ᝺ࡼ᩾ꆀꦈﮎ놐랖ﺚ膠힢춤욦잨讪鶬", a_));
		IL_80:
		IL_C6:
		return this.ᜀ.ᜀ(A_0).OverrideListLevel;
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x001DB3F0 File Offset: 0x001DA3F0
	protected virtual void ᜁ()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		base.InitXDLSHolder();
		base.XDLSHolder.AddElement(ClipboardData.b("ᩴŶᱸॺོᙾꢄﶊ", a_), this.ᜀ);
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x001DB45C File Offset: 0x001DA45C
	protected virtual void ᜀ(IXDLSAttributeWriter A_0)
	{
		int a_ = 3;
		for (;;)
		{
			base.WriteXmlAttributes(A_0);
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.WriteValue(ClipboardData.b("㭨๪Ṭ幮", a_), this.ᜁ);
					num = 5;
					continue;
				case 1:
					if (true)
					{
					}
					A_0.WriteValue(ClipboardData.b("㱨ժᡬᱮᑰᝲ䝴", a_), this.ᜄ);
					num = 9;
					continue;
				case 2:
					if (this.ᜄ != 0)
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					if (this.ᜃ != 0)
					{
						num = 6;
						continue;
					}
					goto IL_14E;
				case 4:
					goto IL_96;
				case 5:
					goto IL_B9;
				case 6:
					goto IL_6F;
				case 7:
					goto IL_14E;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						A_0.WriteValue(ClipboardData.b("㭨๪Ṭ嵮", a_), this.ᜂ);
						num = 4;
						continue;
					}
					break;
				case 9:
					return;
				case 10:
					if (this.ᜁ != 0)
					{
						num = 0;
						continue;
					}
					goto IL_B9;
				case 11:
					if (this.ᜂ != 0)
					{
						num = 8;
						continue;
					}
					goto IL_96;
				}
				break;
				IL_6F:
				A_0.WriteValue(ClipboardData.b("㱨ժᡬᱮᑰᝲ䑴", a_), this.ᜃ);
				num = 7;
				continue;
				IL_96:
				num = 3;
				continue;
				IL_B9:
				num = 11;
				continue;
				IL_14E:
				num = 2;
			}
		}
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x001DB604 File Offset: 0x001DA604
	protected virtual void ᜀ(IXDLSAttributeReader A_0)
	{
		int a_ = 17;
		for (;;)
		{
			base.ReadXmlAttributes(A_0);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.HasAttribute(ClipboardData.b("╶ᱸࡺ䱼", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_E0;
				case 1:
					goto IL_AF;
				case 2:
					this.ᜁ = A_0.ReadInt(ClipboardData.b("╶ᱸࡺ䱼", a_));
					num = 6;
					continue;
				case 3:
					if (A_0.HasAttribute(ClipboardData.b("≶᝸๺๼᩾늂", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_181;
				case 4:
					goto IL_7D;
				case 5:
					this.ᜄ = A_0.ReadInt(ClipboardData.b("≶᝸๺๼᩾놂", a_));
					num = 11;
					continue;
				case 6:
					goto IL_E0;
				case 7:
					if (A_0.HasAttribute(ClipboardData.b("≶᝸๺๼᩾놂", a_)))
					{
						num = 5;
						continue;
					}
					return;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D;
					default:
						if (false)
						{
						}
						this.ᜂ = A_0.ReadInt(ClipboardData.b("╶ᱸࡺ佼", a_));
						num = 1;
						continue;
					}
					break;
				case 9:
					if (A_0.HasAttribute(ClipboardData.b("╶ᱸࡺ佼", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_AF;
				case 10:
					goto IL_181;
				case 11:
					return;
				}
				break;
				IL_7D:
				this.ᜃ = A_0.ReadInt(ClipboardData.b("≶᝸๺๼᩾늂", a_));
				if (true)
				{
				}
				num = 10;
				continue;
				IL_AF:
				num = 3;
				continue;
				IL_E0:
				num = 9;
				continue;
				IL_181:
				num = 7;
			}
		}
	}

	// Token: 0x04001F9B RID: 8091
	private new spr\u20E3 ᜀ;

	// Token: 0x04001F9C RID: 8092
	internal new int ᜁ;

	// Token: 0x04001F9D RID: 8093
	internal new int ᜂ;

	// Token: 0x04001F9E RID: 8094
	internal int ᜃ;

	// Token: 0x04001F9F RID: 8095
	internal int ᜄ;
}

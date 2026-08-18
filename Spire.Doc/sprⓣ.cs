using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Core;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Core.Escher;

// Token: 0x02000266 RID: 614
internal class spr\u24E3
{
	// Token: 0x06002020 RID: 8224 RVA: 0x0021FF84 File Offset: 0x0021EF84
	internal Document ᜋ()
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
		return this.ᜊ;
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x0021FFC8 File Offset: 0x0021EFC8
	internal Dictionary<int, spr\u2542> ᜈ()
	{
		int num = 0;
		for (;;)
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
				switch (num)
				{
				case 1:
					goto IL_6F;
				case 2:
					this.ᜈ = new Dictionary<int, spr\u2542>();
					num = 1;
					continue;
				}
				break;
			}
			if (this.ᜈ != null)
			{
				break;
			}
			num = 2;
		}
		IL_6F:
		return this.ᜈ;
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x0022004C File Offset: 0x0021F04C
	internal spr\u2459 ᜇ()
	{
		int num = 2;
		for (;;)
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
				switch (num)
				{
				case 0:
					this.ᜉ = this.ᜆ();
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_70;
				}
				break;
			}
			if (this.ᜉ != null)
			{
				break;
			}
			num = 0;
		}
		IL_70:
		return this.ᜉ;
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x002200D4 File Offset: 0x0021F0D4
	internal spr\u24E3(Document A_0)
	{
		this.ᜊ = A_0;
		this.ᜇ = new sprᵲ(this.ᜊ);
		this.ᜂ();
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x00220108 File Offset: 0x0021F108
	internal spr\u24E3(Stream A_0, Stream A_1, int A_2, int A_3, Document A_4) : this(A_4)
	{
		A_0.Position = (long)A_2;
		if (A_3 != 0)
		{
			this.ᜀ(A_0, A_3, A_1);
		}
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x00220138 File Offset: 0x0021F138
	internal void ᜀ(Stream A_0, int A_1, Stream A_2)
	{
		int a_ = 17;
		for (;;)
		{
			long num = A_0.Position + (long)A_1;
			num = Math.Min(num, A_0.Length);
			this.ᜆ = (spr\u1D2F.ᜀ(A_0, this.ᜊ) as spr\u2403);
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_141;
					default:
					{
						if (false)
						{
						}
						if (A_0.Position >= num)
						{
							num2 = 1;
							continue;
						}
						int a_2 = A_0.ReadByte();
						spr\u1DB9 spr_u1DB = spr\u1D2F.ᜀ(A_0, this.ᜊ) as spr\u1DB9;
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					}
					break;
				case 1:
					goto IL_102;
				case 2:
				{
					spr\u1DB9 spr_u1DB;
					if (spr_u1DB == null)
					{
						num2 = 5;
						continue;
					}
					int a_2;
					spr_u1DB.ᜀ((ShapeDocType)a_2);
					this.ᜇ.Add(spr_u1DB);
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_C7;
				case 4:
					if (this.ᜆ == null)
					{
						num2 = 6;
						continue;
					}
					goto IL_C7;
				case 5:
					goto IL_141;
				case 6:
					goto IL_73;
				}
				break;
				IL_C7:
				num2 = 0;
			}
		}
		IL_73:
		throw new ArgumentException(ClipboardData.b("ㅶၸॺ๼୾ꆀ욂ﾌ꾎ﾚ붜튠莢쮤좦\udda8讪좮횰\udab4\ud9b6춸\udaba풼톾꓀뇂", a_));
		IL_102:
		this.ᜀ();
		this.ᜂ(A_2);
		return;
		IL_141:
		throw new ArgumentException(ClipboardData.b("㉶Ÿ୺᡼᱾Ꞇ춈캌ﾐﺖﺚ뾞펠욢욤좦\udba8쾪\udeac辮\udeb0\uddb2\ud9b4캶鞸", a_));
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x00220298 File Offset: 0x0021F298
	internal void ᜂ(Stream A_0)
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
			using (List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator())
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						spr\u1DB9 a_ = (spr\u1DB9)enumerator.Current;
						this.ᜀ(a_, A_0);
						num = 3;
						continue;
					}
					case 1:
						num = 4;
						continue;
					case 3:
						goto IL_81;
					case 4:
						goto IL_A4;
					}
					goto IL_58;
					IL_81:
					num = 0;
					continue;
					IL_58:
					if (true)
					{
					}
					goto IL_81;
				}
				IL_A4:;
			}
			break;
		}
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x0022036C File Offset: 0x0021F36C
	internal void ᜃ(Stream A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 10;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					goto IL_BC;
				case 2:
				{
					int count;
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					spr\u2192 spr_u = this.ᜆ.ᜀ().\u1714()[num2] as spr\u2192;
					num = 3;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
					{
						if (false)
						{
						}
						spr\u2192 spr_u;
						if (spr_u is sprΏ)
						{
							num = 5;
							continue;
						}
						spr_u.ᜆ(A_0);
						num = 11;
						continue;
					}
					}
					break;
				case 4:
					if (this.ᜆ.ᜀ() != null)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
				{
					if (true)
					{
					}
					spr\u2192 spr_u;
					(spr_u as sprΏ).ᜃ(A_0);
					num = 9;
					continue;
				}
				case 6:
				{
					spr\u2192 spr_u = null;
					num2 = 0;
					int count = this.ᜆ.ᜀ().\u1714().Count;
					num = 0;
					continue;
				}
				case 7:
					return;
				case 8:
					num = 4;
					continue;
				case 9:
					goto IL_100;
				case 11:
					goto IL_100;
				}
				if (this.ᜆ != null)
				{
					num = 8;
					continue;
				}
				return;
				IL_BC:
				num = 2;
				continue;
				IL_100:
				num2++;
				num = 1;
			}
		}
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x002204E8 File Offset: 0x0021F4E8
	internal uint ᜄ(Stream A_0)
	{
		if (this.ᜆ == null)
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
				break;
			}
			return 0U;
		}
		long position = A_0.Position;
		this.ᜃ();
		this.ᜁ(A_0);
		this.ᜀ(A_0);
		return (uint)(A_0.Position - position);
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x00220554 File Offset: 0x0021F554
	internal void ᜀ(WordSubdocument A_0, spr\u2192 A_1)
	{
		int num = 2;
		for (;;)
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
				switch (num)
				{
				case 0:
					goto IL_6A;
				case 1:
					this.ᜂ();
					num = 0;
					continue;
				}
				break;
			}
			if (this.ᜆ != null)
			{
				break;
			}
			num = 1;
		}
		IL_6A:
		this.ᜄ();
		ShapeDocType a_ = spr\u24E3.ᜀ(A_0);
		spr\u1DB9 spr_u1DB = this.ᜀ(a_);
		spr_u1DB.ᜁ().\u1714().Add(A_1);
		this.ᜁ(A_1 as spr\u2542);
		this.ᜂ(A_1 as spr\u2542);
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0022060C File Offset: 0x0021F60C
	internal spr\u1DB9 ᜀ(ShapeDocType A_0)
	{
		for (;;)
		{
			List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator();
			spr\u1DB9 result;
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B1;
					case 1:
					{
						spr\u1DB9 spr_u1DB;
						if (spr_u1DB.ᜄ() == A_0)
						{
							num = 6;
							continue;
						}
						break;
					}
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 5;
							continue;
						}
						spr\u1DB9 spr_u1DB = (spr\u1DB9)enumerator.Current;
						num = 1;
						continue;
					}
					case 3:
						goto IL_BB;
					case 5:
						num = 3;
						continue;
					case 6:
					{
						spr\u1DB9 spr_u1DB;
						result = spr_u1DB;
						num = 0;
						continue;
					}
					}
					IL_64:
					num = 2;
					continue;
					goto IL_64;
				}
				IL_B1:
				return result;
				IL_BB:
				goto IL_0E;
			}
			finally
			{
				if (true)
				{
				}
				((IDisposable)enumerator).Dispose();
			}
			return result;
			IL_0E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2E;
			}
		}
		IL_2E:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x00220700 File Offset: 0x0021F700
	internal void ᜉ()
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
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x0022073C File Offset: 0x0021F73C
	internal void ᜊ()
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			spr\u1DB9 spr_u1DB;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
					if (false)
					{
					}
					spr_u1DB = (this.ᜇ[1] as spr\u1DB9);
					num = 3;
					continue;
				}
				break;
			case 1:
				goto IL_BA;
			case 3:
				goto IL_A6;
			}
			if (this.ᜇ.Count > 1)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			return;
			IL_A6:
			if (spr_u1DB.ᜄ() != ShapeDocType.HeaderFooter)
			{
				break;
			}
			num = 1;
		}
		throw new ArgumentException(ClipboardData.b("㙲൴ݶᱸ᡺ॼ᩾ꎂﶎ놐趠莢잤튦\udda8讪쪬삮얰鎲운\ud8b6풸\udeba즼ힾꣀ귂ꋄ곈ꟊ뻌꫎￐", a_));
		IL_BA:
		this.ᜇ.RemoveAt(1);
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x00220810 File Offset: 0x0021F810
	internal spr\u2542 ᜃ(spr\u2542 A_0)
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
			{
				if (true)
				{
				}
				if (false)
				{
				}
				spr\u2542 result;
				using (List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator())
				{
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							goto IL_D0;
						case 2:
							goto IL_DE;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							spr\u2542 spr_u = (spr\u2542)enumerator.Current;
							spr\u2542 spr_u2 = spr_u.ᜀ(A_0);
							num = 4;
							continue;
						}
						case 4:
						{
							spr\u2542 spr_u2;
							if (spr_u2 != null)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 6:
						{
							spr\u2542 spr_u2;
							result = spr_u2;
							num = 1;
							continue;
						}
						}
						IL_7C:
						num = 3;
						continue;
						goto IL_7C;
					}
					IL_D0:
					return result;
					IL_DE:
					break;
				}
				return result;
			}
			}
			return null;
		}
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x00220920 File Offset: 0x0021F920
	internal int ᜃ(int A_0)
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
		return this.ᜁ(A_0).ᜐ();
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x00220968 File Offset: 0x0021F968
	internal void ᜁ(int A_0, int A_1)
	{
		if (true)
		{
		}
		for (;;)
		{
			IL_1C:
			spr\u2459 spr_u = this.ᜁ(A_0);
			spr_u.ᜂ(A_1);
			sprᥥ sprᥥ = spr_u.ᜀ(MSOFBT.msofbtClientTextbox) as sprᥥ;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6D:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					sprᥥ.ᜀ(A_1);
					num = 2;
					continue;
				case 1:
					goto IL_6A;
				case 2:
					return;
				}
				goto IL_1C;
			}
			IL_6A:
			if (sprᥥ != null)
			{
				goto IL_6D;
			}
			break;
		}
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x00220A00 File Offset: 0x0021FA00
	internal spr\u2542 ᜀ(int A_0)
	{
		spr\u2459 spr_u;
		for (;;)
		{
			spr_u = this.ᜁ(A_0);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (spr_u.ᜅ().ᜋ())
					{
						goto IL_52;
					}
					goto IL_92;
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_5A;
				case 3:
					if (spr_u == null)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 4:
					goto IL_3B;
				case 5:
					if (spr_u.ᜅ().ᜊ() == EscherShapeType.msosptMin)
					{
						num = 1;
						continue;
					}
					goto IL_92;
				}
				break;
				IL_52:
				num = 2;
				continue;
				IL_92:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_52;
				default:
					goto IL_B0;
				}
			}
		}
		IL_3B:
		return null;
		IL_5A:
		return this.ᜃ(spr_u);
		IL_B0:
		if (false)
		{
		}
		return spr_u;
	}

	// Token: 0x06002031 RID: 8241 RVA: 0x00220AC4 File Offset: 0x0021FAC4
	internal spr\u2459 ᜁ(int A_0)
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
			{
				if (true)
				{
				}
				if (false)
				{
				}
				spr\u2459 result;
				using (List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator())
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							spr\u2542 a_ = (spr\u2542)enumerator.Current;
							spr\u2459 spr_u = spr\u24E3.ᜀ(a_, A_0);
							num = 3;
							continue;
						}
						case 2:
							goto IL_DE;
						case 3:
						{
							spr\u2459 spr_u;
							if (spr_u != null)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 5:
							goto IL_D0;
						case 6:
						{
							spr\u2459 spr_u;
							result = spr_u;
							num = 5;
							continue;
						}
						}
						IL_7C:
						num = 1;
						continue;
						goto IL_7C;
					}
					IL_D0:
					return result;
					IL_DE:
					break;
				}
				return result;
			}
			}
			return null;
		}
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x00220BD4 File Offset: 0x0021FBD4
	internal EscherShapeType ᜃ(spr\u2459 A_0)
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
		return A_0.ᜅ().ᜊ();
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x00220C1C File Offset: 0x0021FC1C
	internal int ᜀ(Document A_0, WordSubdocument A_1, int A_2, int A_3)
	{
		for (;;)
		{
			spr\u2542 spr_u = null;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8D;
				case 1:
					spr_u = A_0.Escher.ᜈ()[A_3];
					num = 6;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_0.Escher.ᜈ().ContainsKey(A_3))
						{
							num = 9;
							continue;
						}
						goto IL_67;
					}
					break;
				case 3:
				{
					spr\u2542 spr_u2 = (spr\u2542)this.ᜈ()[A_2].ᜃ();
					spr_u2.ᜄ(A_3);
					A_0.Escher.ᜀ(A_1, spr_u2);
					spr_u2.ᜀ(A_0);
					num = 7;
					continue;
				}
				case 4:
					if (this.ᜈ().ContainsKey(A_2))
					{
						num = 3;
						continue;
					}
					return -1;
				case 5:
					goto IL_67;
				case 6:
					goto IL_8D;
				case 7:
					return A_3;
				case 8:
					if (spr_u == null)
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				case 9:
					spr_u = A_0.Escher.ᜈ()[A_3];
					A_3++;
					num = 0;
					continue;
				case 10:
					if (true)
					{
					}
					if (A_0.Escher.ᜈ().ContainsKey(A_3))
					{
						num = 1;
						continue;
					}
					goto IL_8D;
				}
				break;
				IL_67:
				num = 4;
				continue;
				IL_8D:
				num = 8;
			}
		}
		return -1;
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x00220DB4 File Offset: 0x0021FDB4
	internal void ᜅ()
	{
		List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator();
		try
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_86:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_90;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					spr\u2542 spr_u = (spr\u2542)enumerator.Current;
					spr_u.\u1716();
					num = 3;
					continue;
				}
				case 4:
					goto IL_86;
				}
				IL_6D:
				num = 2;
				continue;
				goto IL_6D;
			}
			IL_90:;
		}
		finally
		{
			if (true)
			{
			}
			((IDisposable)enumerator).Dispose();
		}
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x00220E84 File Offset: 0x0021FE84
	internal void ᜀ(int A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				if (true)
				{
				}
				spr\u2192 spr_u;
				bool flag;
				int num2;
				int count;
				spr\u1DB9 spr_u1DB;
				switch (num)
				{
				case 0:
					flag = spr\u2542.ᜀ(spr_u as spr\u2542, A_0);
					num = 10;
					continue;
				case 1:
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					num = 6;
					continue;
				case 2:
					goto IL_BF;
				case 3:
					spr_u1DB.\u1714().Remove(spr_u);
					flag = true;
					num = 5;
					continue;
				case 4:
					goto IL_BF;
				case 5:
					goto IL_1D7;
				case 6:
					if (flag)
					{
						num = 12;
						continue;
					}
					spr_u = (spr_u1DB.\u1714()[num2] as spr\u2192);
					num = 7;
					continue;
				case 7:
					if (spr_u is spr\u2459)
					{
						num = 14;
						continue;
					}
					num = 13;
					continue;
				case 8:
					return;
				case 10:
					goto IL_1D7;
				case 11:
					if ((spr_u as spr\u2459).ᜅ().ᜀ() == A_0)
					{
						goto IL_121;
					}
					goto IL_1D7;
				case 12:
					return;
				case 13:
					if (spr_u is spr\u2542)
					{
						num = 0;
						continue;
					}
					goto IL_1D7;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_121;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				}
				spr_u1DB = this.ᜀ(A_1 ? ShapeDocType.HeaderFooter : ShapeDocType.Main);
				flag = false;
				spr_u = null;
				this.ᜈ().Remove(A_0);
				num2 = 0;
				count = spr_u1DB.\u1714().Count;
				num = 2;
				continue;
				IL_BF:
				num = 1;
				continue;
				IL_121:
				num = 3;
				continue;
				IL_1D7:
				num2++;
				num = 4;
			}
			return;
		}
		}
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x00221080 File Offset: 0x00220080
	internal void ᜄ(int A_0)
	{
		using (List<object>.Enumerator enumerator = this.ᜆ.\u1714().GetEnumerator())
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u2192 spr_u;
					if (spr_u is spr\u2568)
					{
						num = 3;
						continue;
					}
					break;
				}
				case 1:
					goto IL_FD;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 4:
				{
					spr\u2192 spr_u;
					(spr_u as spr\u2568).\u1714().RemoveAt(A_0 - 1);
					num = 7;
					continue;
				}
				case 5:
					goto IL_F2;
				case 6:
				{
					spr\u2192 spr_u;
					if (A_0 <= (spr_u as spr\u2568).\u1714().Count)
					{
						num = 4;
						continue;
					}
					break;
				}
				case 7:
					goto IL_F2;
				case 8:
				{
					if (!enumerator.MoveNext())
					{
						num = 5;
						continue;
					}
					spr\u2192 spr_u = (spr\u2192)enumerator.Current;
					num = 0;
					continue;
				}
				}
				IL_70:
				num = 8;
				continue;
				IL_47:
				goto IL_70;
				goto IL_47;
				IL_F2:
				num = 1;
			}
			IL_FD:;
		}
		if (true)
		{
		}
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x002211BC File Offset: 0x002201BC
	internal void ᜀ(int A_0, sprΏ A_1)
	{
		using (List<object>.Enumerator enumerator = this.ᜆ.\u1714().GetEnumerator())
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_10F;
				case 1:
				{
					spr\u2192 spr_u;
					((spr_u as spr\u2568).\u1714()[A_0 - 1] as sprΏ).ᜄ().ᜀ(A_1.ᜄ().ᜂ());
					num = 5;
					continue;
				}
				case 2:
					goto IL_11A;
				case 4:
				{
					spr\u2192 spr_u;
					if (spr_u is spr\u2568)
					{
						num = 7;
						continue;
					}
					break;
				}
				case 5:
					goto IL_10F;
				case 6:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					spr\u2192 spr_u = (spr\u2192)enumerator.Current;
					num = 4;
					continue;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 8:
				{
					spr\u2192 spr_u;
					if (A_0 <= (spr_u as spr\u2568).\u1714().Count)
					{
						num = 1;
						continue;
					}
					break;
				}
				}
				IL_70:
				num = 6;
				continue;
				IL_47:
				goto IL_70;
				goto IL_47;
				IL_10F:
				num = 2;
			}
			IL_11A:;
		}
		if (true)
		{
		}
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x00221324 File Offset: 0x00220324
	internal spr\u2459 ᜆ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1DB9 spr_u1DB = this.ᜀ(ShapeDocType.Main);
				int num = 1;
				for (;;)
				{
					List<object>.Enumerator enumerator;
					switch (num)
					{
					case 0:
						goto IL_118;
					case 1:
						if (spr_u1DB != null)
						{
							num = 0;
							continue;
						}
						goto IL_13D;
					case 2:
						if (true)
						{
						}
						try
						{
							num = 1;
							spr\u2459 result;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									spr\u2192 spr_u;
									if (spr_u is spr\u2459)
									{
										num = 4;
										continue;
									}
									break;
								}
								case 2:
									goto IL_108;
								case 3:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
									{
										if (false)
										{
										}
										if (!enumerator.MoveNext())
										{
											num = 6;
											continue;
										}
										spr\u2192 spr_u = (spr\u2192)enumerator.Current;
										break;
									}
									}
									num = 0;
									continue;
								case 4:
								{
									spr\u2192 spr_u;
									result = (spr_u as spr\u2459);
									num = 5;
									continue;
								}
								case 5:
									goto IL_FA;
								case 6:
									num = 2;
									continue;
								}
								IL_AB:
								num = 3;
								continue;
								goto IL_AB;
							}
							IL_FA:
							return result;
							IL_108:
							goto IL_13D;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_118;
					}
					break;
					IL_118:
					enumerator = spr_u1DB.\u1714().GetEnumerator();
					num = 2;
				}
			}
			IL_13D:
			return null;
		}
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x00221484 File Offset: 0x00220484
	internal bool ᜂ(int A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					spr\u2568 spr_u = this.ᜆ.ᜀ();
					num = 4;
					continue;
				}
				case 1:
					return result;
				case 2:
				{
					sprΏ sprΏ;
					if (sprΏ.ᜄ() != null)
					{
						num = 6;
						continue;
					}
					return result;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
					{
						if (false)
						{
						}
						spr\u2568 spr_u;
						sprΏ sprΏ = (sprΏ)spr_u.\u1714()[A_0 - 1];
						num = 2;
						continue;
					}
					}
					break;
				case 4:
				{
					spr\u2568 spr_u;
					if (spr_u.\u1714().Count >= A_0)
					{
						num = 3;
						continue;
					}
					return result;
				}
				case 5:
					if (this.ᜆ.ᜀ() != null)
					{
						num = 0;
						continue;
					}
					return result;
				case 6:
					result = true;
					goto IL_52;
				}
				break;
				IL_52:
				num = 1;
			}
		}
		return result;
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x00221580 File Offset: 0x00220580
	private void ᜀ(spr\u2542 A_0, Stream A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						sprΏ sprΏ = this.ᜆ.ᜀ().\u1714()[num3] as sprΏ;
						sprΏ.ᜂ(A_1);
						spr\u2459 spr_u;
						spr_u.ᜀ(sprΏ);
						num2 = 8;
						continue;
					}
					case 1:
					{
						int num3;
						if (num3 >= 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_77;
					}
					case 2:
					{
						spr\u2459 spr_u;
						int num3 = this.ᜁ(spr_u);
						num2 = 1;
						continue;
					}
					case 3:
					{
						spr\u2192 spr_u2;
						if (spr_u2 is spr\u2542)
						{
							num2 = 13;
							continue;
						}
						goto IL_12D;
					}
					case 4:
						return;
					case 5:
					{
						spr\u2459 spr_u;
						if (spr_u.ᜅ() != null)
						{
							num2 = 2;
							continue;
						}
						goto IL_77;
					}
					case 6:
						goto IL_C5;
					case 7:
					{
						spr\u2192 spr_u2;
						if (spr_u2 is spr\u2459)
						{
							num2 = 12;
							continue;
						}
						goto IL_77;
					}
					case 8:
						goto IL_77;
					case 9:
						goto IL_12D;
					case 10:
						goto IL_C5;
					case 11:
					{
						if (num >= A_0.\u1714().Count)
						{
							num2 = 4;
							continue;
						}
						spr\u2192 spr_u2 = A_0.\u1714()[num] as spr\u2192;
						num2 = 7;
						continue;
					}
					case 12:
					{
						spr\u2192 spr_u2;
						spr\u2459 spr_u = spr_u2 as spr\u2459;
						num2 = 5;
						continue;
					}
					case 13:
					{
						spr\u2192 spr_u2;
						this.ᜀ(spr_u2 as spr\u2542, A_1);
						num2 = 9;
						continue;
					}
					}
					break;
					IL_77:
					num2 = 3;
					continue;
					IL_C5:
					num2 = 11;
					continue;
					IL_12D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num++;
						num2 = 10;
						break;
					}
				}
			}
			return;
		}
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x00221764 File Offset: 0x00220764
	private void ᜃ()
	{
		switch (0)
		{
		default:
		{
			spr\u1BEE spr_u1BEE;
			int num;
			for (;;)
			{
				spr_u1BEE = this.ᜆ.ᜁ();
				num = 0;
				spr_u1BEE.ᜂ().Clear();
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					spr\u1DB9 spr_u1DB;
					switch (num3)
					{
					case 0:
						goto IL_E3;
					case 1:
						goto IL_68;
					case 2:
						if (spr_u1DB.ᜀ().ᜄ() > 1)
						{
							goto IL_16A;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num3 = 6;
							continue;
						}
						break;
					case 3:
						if (spr_u1DB.ᜄ() == ShapeDocType.HeaderFooter)
						{
							num3 = 9;
							continue;
						}
						goto IL_16A;
					case 4:
						if (num2 >= this.ᜇ.Count)
						{
							num3 = 5;
							continue;
						}
						spr_u1DB = (this.ᜇ[num2] as spr\u1DB9);
						spr_u1DB.ᜅ();
						goto IL_BD;
					case 5:
						goto IL_10C;
					case 6:
						this.ᜀ(2048, true);
						this.ᜇ.RemoveAt(num2);
						num2--;
						num--;
						num3 = 7;
						continue;
					case 7:
						goto IL_68;
					case 8:
						goto IL_E3;
					case 9:
						num3 = 2;
						continue;
					}
					break;
					IL_68:
					num2++;
					num3 = 8;
					continue;
					IL_BD:
					num3 = 3;
					continue;
					IL_E3:
					num3 = 4;
					continue;
					IL_16A:
					int num4 = this.ᜀ(spr_u1DB.ᜁ());
					this.ᜆ.ᜁ().ᜂ().Add(new sprᱛ(spr_u1DB.ᜀ().ᜁ(), num4 + 1));
					num += num4 + 1;
					num3 = 1;
				}
			}
			IL_10C:
			spr_u1BEE.ᜂ(this.ᜇ.Count);
			spr_u1BEE.ᜁ(num);
			spr_u1BEE.ᜀ((this.ᜇ.Count + 1) * 1024 + 2);
			return;
		}
		}
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x00221960 File Offset: 0x00220960
	private void ᜁ(Stream A_0)
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
		this.ᜆ.ᜆ(A_0);
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x002219A8 File Offset: 0x002209A8
	private void ᜀ(Stream A_0)
	{
		if (true)
		{
		}
		using (List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator())
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9D:
				num = 4;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (!enumerator.MoveNext())
					{
						num = 2;
						continue;
					}
					spr\u1DB9 spr_u1DB = (spr\u1DB9)enumerator.Current;
					A_0.WriteByte((byte)spr_u1DB.ᜄ());
					spr_u1DB.ᜆ(A_0);
					num = 3;
					continue;
				}
				case 2:
					goto IL_9D;
				case 4:
					goto IL_A7;
				}
				IL_84:
				num = 0;
				continue;
				goto IL_84;
			}
			IL_A7:;
		}
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x00221A88 File Offset: 0x00220A88
	internal void ᜄ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(ShapeDocType.Main, 1, 1024);
				num = 1;
				continue;
			case 1:
				goto IL_6E;
			case 2:
				return;
			case 4:
				if (this.ᜀ(ShapeDocType.HeaderFooter) == null)
				{
					num = 5;
					continue;
				}
				return;
			case 5:
				this.ᜀ(ShapeDocType.HeaderFooter, 2, 2048);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			if (this.ᜀ(ShapeDocType.Main) == null)
			{
				num = 0;
				continue;
			}
			IL_6E:
			if (true)
			{
			}
			num = 4;
		}
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x00221B50 File Offset: 0x00220B50
	private void ᜂ()
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
		this.ᜆ = new spr\u2403(this.ᜊ);
		spr\u1BEE item = new spr\u1BEE(this.ᜊ);
		this.ᜆ.\u1714().Add(item);
		this.ᜆ.\u1714().Add(new spr\u2568(this.ᜊ));
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x00221BD4 File Offset: 0x00220BD4
	private void ᜀ(ShapeDocType A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u1DB9 spr_u1DB;
			for (;;)
			{
				spr_u1DB = new spr\u1DB9(this.ᜊ);
				spr_u1DB.ᜀ(A_0);
				this.ᜇ.Add(spr_u1DB);
				spr\u2365 spr_u = new spr\u2365(this.ᜊ);
				spr_u.ᜀ(A_1);
				spr_u.ᜂ(1);
				spr_u.ᜁ(A_2);
				spr_u1DB.\u1714().Add(spr_u);
				sprᥙ sprᥙ = new sprᥙ(this.ᜊ);
				spr_u1DB.\u1714().Add(sprᥙ);
				spr\u2459 spr_u2 = new spr\u2459(this.ᜊ);
				sprᥙ.\u1714().Add(spr_u2);
				spr\u2379 item = new spr\u2379(this.ᜊ);
				spr_u2.\u1714().Add(item);
				spr\u2402 spr_u3 = new spr\u2402(this.ᜊ);
				spr_u3.ᜀ(A_2);
				spr_u3.ᜊ(true);
				spr_u3.ᜇ(true);
				spr_u3.ᜀ(EscherShapeType.msosptMin);
				spr_u2.\u1714().Add(spr_u3);
				this.ᜈ().Add(spr_u2.ᜅ().ᜀ(), spr_u2);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_19B;
					case 1:
					{
						spr\u2459 spr_u4 = new spr\u2459(this.ᜊ);
						spr_u1DB.\u1714().Add(spr_u4.ᜆ());
						this.ᜈ().Add(spr_u4.ᜅ().ᜀ(), spr_u2);
						num = 0;
						continue;
					}
					case 2:
						if (A_0 != ShapeDocType.Main)
						{
							goto IL_19D;
						}
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
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_19B:
			IL_19D:
			spr_u1DB.\u1714().Add(new spr\u22A2(this.ᜊ));
			return;
		}
		}
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x00221D94 File Offset: 0x00220D94
	private static spr\u2459 ᜀ(spr\u2542 A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u2459 spr_u3;
			for (;;)
			{
				spr\u2192 spr_u = null;
				int num = 0;
				int count = A_0.\u1714().Count;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_151;
						default:
						{
							if (false)
							{
							}
							spr\u2402 spr_u2 = spr_u as spr\u2402;
							num2 = 8;
							continue;
						}
						}
						break;
					case 1:
						spr_u3 = spr\u24E3.ᜀ(spr_u as spr\u2542, A_1);
						num2 = 9;
						continue;
					case 2:
						goto IL_D2;
					case 3:
						goto IL_151;
					case 4:
						if (num >= count)
						{
							num2 = 7;
							continue;
						}
						spr_u = (A_0.\u1714()[num] as spr\u2192);
						num2 = 5;
						continue;
					case 5:
						if (spr_u is spr\u2402)
						{
							num2 = 0;
							continue;
						}
						num2 = 11;
						continue;
					case 6:
						goto IL_D7;
					case 7:
						goto IL_FB;
					case 8:
					{
						spr\u2402 spr_u2;
						if (spr_u2.ᜀ() == A_1)
						{
							num2 = 2;
							continue;
						}
						goto IL_141;
					}
					case 9:
						if (spr_u3 != null)
						{
							num2 = 10;
							continue;
						}
						goto IL_141;
					case 10:
						return spr_u3;
					case 11:
						if (spr_u is spr\u2542)
						{
							num2 = 1;
							continue;
						}
						goto IL_141;
					}
					break;
					IL_D7:
					if (true)
					{
					}
					num2 = 4;
					continue;
					IL_151:
					goto IL_D7;
					IL_141:
					num++;
					num2 = 3;
				}
			}
			return spr_u3;
			IL_D2:
			return A_0 as spr\u2459;
			IL_FB:
			return null;
		}
		}
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x00221F24 File Offset: 0x00220F24
	private void ᜂ(spr\u2459 A_0)
	{
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
			{
				if (A_0.ᜅ().ᜊ() == EscherShapeType.msosptPictureFrame)
				{
					num = 11;
					continue;
				}
				sprẖ sprẖ = A_0.ᜌ().ᜅ()[390] as sprẖ;
				num = 8;
				continue;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
				{
					if (false)
					{
					}
					sprẖ sprẖ;
					sprẖ.ᜀ((uint)this.ᜆ.ᜀ().\u1714().Count);
					num = 3;
					continue;
				}
				}
				break;
			case 3:
				return;
			case 4:
				num = 6;
				continue;
			case 5:
				this.ᜆ.\u1714().Add(new spr\u2568(this.ᜊ));
				num = 9;
				continue;
			case 6:
				if (this.ᜆ.ᜀ() == null)
				{
					num = 5;
					continue;
				}
				goto IL_114;
			case 7:
				if (A_0.ᜊ() != null)
				{
					num = 4;
					continue;
				}
				return;
			case 8:
			{
				sprẖ sprẖ;
				if (sprẖ != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			case 9:
				goto IL_114;
			case 11:
				goto IL_157;
			case 12:
				return;
			case 13:
				if (A_0.ᜏ() != -1)
				{
					num = 12;
					continue;
				}
				goto IL_1CB;
			}
			if (A_0.ᜁ())
			{
				num = 0;
				continue;
			}
			goto IL_1CB;
			IL_114:
			this.ᜆ.ᜀ().\u1714().Add(A_0.ᜊ());
			num = 1;
			continue;
			IL_1CB:
			num = 7;
		}
		return;
		IL_157:
		if (true)
		{
		}
		A_0.ᜀ(this.ᜆ.ᜀ().\u1714().Count);
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x00222120 File Offset: 0x00221120
	internal static ShapeDocType ᜀ(WordSubdocument A_0)
	{
		int a_ = 15;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7D;
				case 1:
					num = 0;
					continue;
				case 2:
					switch (A_0)
					{
					case WordSubdocument.Main:
						return ShapeDocType.Main;
					case WordSubdocument.Footnote:
						goto IL_7F;
					case WordSubdocument.HeaderFooter:
						return ShapeDocType.HeaderFooter;
					default:
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
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
		}
		return ShapeDocType.HeaderFooter;
		IL_7D:
		IL_7F:
		throw new Exception(ClipboardData.b("≴Ṷ᝸ὺቼࡾ궂좄꾎ﲒ랖", a_) + A_0.ToString() + ClipboardData.b("啴፶ᙸ᡺ࡼቾꞆ권ﺐ떔漢삠솢즤슦", a_));
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x002221E0 File Offset: 0x002211E0
	private void ᜁ(spr\u2542 A_0)
	{
		if (true)
		{
		}
		int num = 11;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
			{
				if (num2 >= A_0.\u1714().Count)
				{
					num = 12;
					continue;
				}
				spr\u2192 spr_u = A_0.\u1714()[num2] as spr\u2192;
				goto IL_125;
			}
			case 1:
				goto IL_8E;
			case 2:
				goto IL_9D;
			case 3:
				goto IL_E9;
			case 4:
			{
				spr\u2192 spr_u;
				if (!(spr_u is spr\u2459))
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_125;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			}
			case 5:
				goto IL_9D;
			case 6:
				goto IL_E9;
			case 7:
			{
				spr\u2192 spr_u;
				this.ᜂ(spr_u as spr\u2459);
				num = 3;
				continue;
			}
			case 8:
			{
				spr\u2192 spr_u;
				if (spr_u is spr\u2542)
				{
					num = 10;
					continue;
				}
				goto IL_E9;
			}
			case 9:
				this.ᜂ(A_0 as spr\u2459);
				num = 1;
				continue;
			case 10:
			{
				spr\u2192 spr_u;
				this.ᜁ(spr_u as spr\u2542);
				num = 6;
				continue;
			}
			case 12:
				return;
			}
			if (A_0 is spr\u2459)
			{
				num = 9;
				continue;
			}
			IL_8E:
			num2 = 0;
			num = 2;
			continue;
			IL_9D:
			num = 0;
			continue;
			IL_E9:
			num2++;
			num = 5;
			continue;
			IL_125:
			num = 4;
		}
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x00222354 File Offset: 0x00221354
	private sprᩉ ᜁ()
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
		sprᩉ sprᩉ = new sprᩉ(this.ᜊ);
		sprᩉ.ᜀ(new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			0,
			byte.MaxValue,
			0,
			128,
			128,
			128,
			0,
			247,
			0,
			0,
			16
		});
		return sprᩉ;
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x002223B4 File Offset: 0x002213B4
	private void ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			IL_1C:
			sprᱛ sprᱛ = new sprᱛ(A_0, A_1);
			for (;;)
			{
				IL_24:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.ᜀ(A_0, sprᱛ))
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						}
						if (false)
						{
						}
						if (this.ᜆ.ᜁ().ᜂ().Count != 0)
						{
							num = 4;
							continue;
						}
						goto IL_73;
					case 3:
						if (true)
						{
						}
						goto IL_73;
					case 4:
						num = 0;
						continue;
					}
					goto IL_1C;
					IL_73:
					this.ᜆ.ᜁ().ᜂ().Add(sprᱛ);
					num = 1;
				}
			}
		}
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x00222484 File Offset: 0x00221484
	private bool ᜀ(int A_0, sprᱛ A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 0;
			bool result;
			using (List<sprᱛ>.Enumerator enumerator = this.ᜆ.ᜁ().ᜂ().GetEnumerator())
			{
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.ᜆ.ᜁ().ᜂ()[num] = A_1;
						result = true;
						num2 = 6;
						continue;
					case 1:
					{
						sprᱛ sprᱛ;
						if (sprᱛ.ᜀ == A_0)
						{
							num2 = 0;
							continue;
						}
						num++;
						num2 = 5;
						continue;
					}
					case 2:
						num2 = 7;
						continue;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 2;
							continue;
						}
						sprᱛ sprᱛ = enumerator.Current;
						num2 = 1;
						continue;
					}
					case 6:
						goto IL_102;
					case 7:
						goto IL_110;
					}
					IL_8C:
					num2 = 3;
					continue;
					goto IL_8C;
				}
				IL_102:
				return result;
				IL_110:
				break;
			}
			return result;
		}
		}
		return false;
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x002225C8 File Offset: 0x002215C8
	private int ᜀ(spr\u2542 A_0)
	{
		int num;
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
			num = 0;
			using (List<object>.Enumerator enumerator = A_0.\u1714().GetEnumerator())
			{
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 4;
							continue;
						}
						spr\u2192 spr_u = (spr\u2192)enumerator.Current;
						num2 = 8;
						continue;
					}
					case 3:
						goto IL_10F;
					case 4:
						num2 = 3;
						continue;
					case 5:
						num++;
						num2 = 2;
						continue;
					case 6:
					{
						spr\u2192 spr_u;
						if (spr_u is spr\u2542)
						{
							num2 = 9;
							continue;
						}
						break;
					}
					case 8:
					{
						spr\u2192 spr_u;
						if (spr_u is spr\u2459)
						{
							num2 = 5;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 9:
					{
						spr\u2192 spr_u;
						num += this.ᜀ(spr_u as spr\u2542);
						num2 = 7;
						continue;
					}
					}
					IL_86:
					num2 = 1;
					continue;
					goto IL_86;
				}
				IL_10F:;
			}
			break;
		}
		return num;
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x00222708 File Offset: 0x00221708
	private int ᜁ(spr\u2459 A_0)
	{
		uint num;
		uint num3;
		for (;;)
		{
			IL_18:
			num = A_0.ᜁ(390);
			for (;;)
			{
				IL_24:
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_8F;
					case 1:
						if (num3 != 4294967295U)
						{
							num2 = 0;
							continue;
						}
						return -1;
					case 2:
						goto IL_5E;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						}
						if (false)
						{
						}
						if (num != 4294967295U)
						{
							num2 = 2;
							continue;
						}
						num3 = A_0.ᜁ(260);
						num2 = 1;
						continue;
					}
					goto IL_18;
				}
			}
		}
		IL_5E:
		return (int)(num - 1U);
		IL_8F:
		if (true)
		{
		}
		return (int)(num3 - 1U);
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x002227AC File Offset: 0x002217AC
	private void ᜀ()
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
			using (List<object>.Enumerator enumerator = this.ᜇ.GetEnumerator())
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_A3;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						spr\u1DB9 a_ = (spr\u1DB9)enumerator.Current;
						this.ᜂ(a_);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					}
					IL_80:
					num = 3;
					continue;
					goto IL_80;
				}
				IL_A3:;
			}
			break;
		}
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x0022287C File Offset: 0x0022187C
	internal void ᜂ(spr\u2542 A_0)
	{
		int num = 0;
		for (;;)
		{
			List<object>.Enumerator enumerator;
			switch (num)
			{
			case 1:
				try
				{
					num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							spr\u2192 spr_u;
							if (spr_u is spr\u2542)
							{
								num = 8;
								continue;
							}
							goto IL_10D;
						}
						case 1:
							num = 6;
							continue;
						case 3:
						{
							sprᥙ sprᥙ;
							this.ᜈ().Add(sprᥙ.ᜀ().ᜀ(), sprᥙ);
							num = 2;
							continue;
						}
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							spr\u2192 spr_u = (spr\u2192)enumerator.Current;
							num = 0;
							continue;
						}
						case 5:
						{
							spr\u2192 spr_u;
							if (spr_u is sprᥙ)
							{
								num = 9;
								continue;
							}
							break;
						}
						case 6:
							goto IL_18E;
						case 8:
						{
							spr\u2192 spr_u;
							this.ᜂ(spr_u as spr\u2542);
							num = 10;
							continue;
						}
						case 9:
						{
							spr\u2192 spr_u;
							sprᥙ sprᥙ = spr_u as sprᥙ;
							num = 11;
							continue;
						}
						case 10:
							goto IL_10D;
						case 11:
						{
							sprᥙ sprᥙ;
							if (!this.ᜈ().ContainsKey(sprᥙ.ᜀ().ᜀ()))
							{
								num = 3;
								continue;
							}
							break;
						}
						}
						IL_EC:
						num = 4;
						continue;
						goto IL_EC;
						IL_10D:
						num = 5;
					}
					IL_18E:
					return;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_19E;
			case 2:
				goto IL_5D;
			}
			if (A_0 is spr\u2459)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					continue;
				}
			}
			IL_19E:
			enumerator = A_0.\u1714().GetEnumerator();
			num = 1;
		}
		IL_5D:
		this.ᜀ(A_0 as spr\u2459);
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x00222A6C File Offset: 0x00221A6C
	private void ᜀ(spr\u2459 A_0)
	{
		int num = 3;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				if (A_0.ᜅ().ᜋ())
				{
					num = 4;
					continue;
				}
				this.ᜈ().Add(A_0.ᜅ().ᜀ(), A_0);
				num = 2;
				continue;
			case 1:
				num = 0;
				continue;
			case 2:
				goto IL_99;
			case 4:
				goto IL_DC;
			}
			while (!this.ᜈ().ContainsKey(A_0.ᜅ().ᜀ()))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				goto IL_0A;
			}
			break;
		}
		IL_99:
		return;
		IL_DC:
		this.ᜈ().Add(A_0.ᜅ().ᜀ(), this.ᜃ(A_0));
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x00222B58 File Offset: 0x00221B58
	internal void ᜌ()
	{
		int num = 2;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 0:
				goto IL_53;
			case 1:
				this.ᜆ.\u170D();
				num = 5;
				continue;
			case 3:
			{
				if (num2 >= count)
				{
					num = 8;
					continue;
				}
				spr\u1DB9 spr_u1DB = this.ᜇ[num2] as spr\u1DB9;
				spr_u1DB.\u170D();
				num2++;
				num = 9;
				continue;
			}
			case 4:
				if (this.ᜆ != null)
				{
					num = 1;
					continue;
				}
				return;
			case 5:
				return;
			case 6:
				goto IL_117;
			case 7:
				goto IL_FA;
			case 8:
				num = 4;
				continue;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_53;
				default:
					if (false)
					{
					}
					goto IL_117;
				}
				break;
			}
			if (this.ᜈ != null)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			goto IL_FA;
			IL_53:
			this.ᜈ.Clear();
			this.ᜈ = null;
			num = 7;
			continue;
			IL_FA:
			num2 = 0;
			count = this.ᜇ.Count;
			num = 6;
			continue;
			IL_117:
			num = 3;
		}
	}

	// Token: 0x04002055 RID: 8277
	private const int ᜀ = 1;

	// Token: 0x04002056 RID: 8278
	private const int ᜁ = 2;

	// Token: 0x04002057 RID: 8279
	private const int ᜂ = 1024;

	// Token: 0x04002058 RID: 8280
	private const int ᜃ = 2048;

	// Token: 0x04002059 RID: 8281
	private const int ᜄ = 2050;

	// Token: 0x0400205A RID: 8282
	private const int ᜅ = 3074;

	// Token: 0x0400205B RID: 8283
	internal spr\u2403 ᜆ;

	// Token: 0x0400205C RID: 8284
	internal sprᵲ ᜇ;

	// Token: 0x0400205D RID: 8285
	private Dictionary<int, spr\u2542> ᜈ;

	// Token: 0x0400205E RID: 8286
	private spr\u2459 ᜉ;

	// Token: 0x0400205F RID: 8287
	private Document ᜊ;
}

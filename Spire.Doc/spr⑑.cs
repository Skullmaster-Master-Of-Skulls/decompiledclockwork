using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x0200027A RID: 634
[DefaultMember("Item")]
internal class spr\u2451 : IEnumerable
{
	// Token: 0x060021D7 RID: 8663 RVA: 0x00232CD4 File Offset: 0x00231CD4
	internal spr\u2451(sprᢟ A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x00232CF0 File Offset: 0x00231CF0
	public spr\u2587 ᜁ(BorderType A_0)
	{
		int a_ = 3;
		for (;;)
		{
			object obj = this.ᜀ.ᜀ()[A_0];
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u2587 spr_u;
					if (spr_u == null)
					{
						num = 1;
						continue;
					}
					return spr_u;
				}
				case 1:
				{
					int num2;
					spr\u2587 spr_u = new spr\u2587(this.ᜀ, num2);
					this.ᜀ.ᜀ(num2, spr_u);
					num = 3;
					continue;
				}
				case 2:
					goto IL_73;
				case 3:
				{
					spr\u2587 spr_u;
					return spr_u;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						if (obj == null)
						{
							num = 2;
							continue;
						}
						int num2 = (int)obj;
						spr\u2587 spr_u = (spr\u2587)this.ᜀ.ᜀ(num2);
						num = 0;
						continue;
					}
					}
					break;
				}
				break;
			}
		}
		IL_73:
		throw new InvalidOperationException(ClipboardData.b("㵨ͪ࡬佮ͰᙲѴɶᱸࡺॼ᩾ꎂﮈﶎ놐朗랖뾞삠햢쒤캦얨쪪쾬쎮풰鎲펴\ud8b6쮸鮺즼ힾꣀ냂꣆ꯈꇊ꣌곎ꗐ﷒", a_));
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x00232DEC File Offset: 0x00231DEC
	public spr\u2587 ᜀ(int A_0)
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
		BorderType a_ = (BorderType)this.ᜀ.ᜀ().GetKey(A_0);
		return this.ᜁ(a_);
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x00232E48 File Offset: 0x00231E48
	public spr\u2587 ᜊ()
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
		return this.ᜁ(BorderType.Left);
	}

	// Token: 0x060021DB RID: 8667 RVA: 0x00232E8C File Offset: 0x00231E8C
	public spr\u2587 ᜏ()
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
		return this.ᜁ(BorderType.Right);
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x00232ED0 File Offset: 0x00231ED0
	public spr\u2587 ᜄ()
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
		return this.ᜁ(BorderType.Top);
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x00232F14 File Offset: 0x00231F14
	public spr\u2587 ᜀ()
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
		return this.ᜁ(BorderType.Bottom);
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x00232F58 File Offset: 0x00231F58
	public spr\u2587 ᜌ()
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
		return this.ᜁ(BorderType.Horizontal);
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x00232F9C File Offset: 0x00231F9C
	public spr\u2587 ᜇ()
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
		return this.ᜁ(BorderType.Vertical);
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x00232FE0 File Offset: 0x00231FE0
	public int ᜅ()
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
		return this.ᜀ.ᜀ().Count;
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x0023302C File Offset: 0x0023202C
	public double ᜋ()
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
		return this.ᜀ(0).\u1715();
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x00233074 File Offset: 0x00232074
	public void ᜀ(double A_0)
	{
		if (true)
		{
		}
		IEnumerator enumerator = this.ᜀ.ᜀ().GetKeyList().GetEnumerator();
		try
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					BorderType a_;
					if (spr\u2451.ᜀ(a_))
					{
						goto IL_90;
					}
					break;
				}
				case 1:
				{
					BorderType a_;
					this.ᜁ(a_).ᜀ(A_0);
					num = 6;
					continue;
				}
				case 2:
					goto IL_D9;
				case 3:
					num = 2;
					continue;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num = 3;
						continue;
					}
					BorderType a_ = (BorderType)enumerator.Current;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				}
				goto IL_56;
				IL_90:
				num = 1;
				continue;
				IL_9A:
				num = 4;
				continue;
				IL_56:
				goto IL_9A;
			}
			IL_D9:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_119;
					case 1:
						disposable.Dispose();
						num = 0;
						continue;
					case 2:
						if (disposable != null)
						{
							num = 1;
							continue;
						}
						goto IL_11B;
					}
					break;
				}
			}
			IL_119:
			IL_11B:;
		}
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x002331B0 File Offset: 0x002321B0
	public BorderStyle ᜁ()
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
		return this.ᜀ(0).ᜈ();
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x002331F8 File Offset: 0x002321F8
	public void ᜀ(BorderStyle A_0)
	{
		IEnumerator enumerator = this.ᜀ.ᜀ().GetKeyList().GetEnumerator();
		try
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					BorderType a_;
					if (spr\u2451.ᜀ(a_))
					{
						goto IL_90;
					}
					break;
				}
				case 1:
					goto IL_DC;
				case 2:
				{
					BorderType a_;
					this.ᜁ(a_).ᜁ(A_0);
					num = 4;
					continue;
				}
				case 3:
					num = 1;
					continue;
				case 6:
				{
					if (!enumerator.MoveNext())
					{
						num = 3;
						continue;
					}
					BorderType a_ = (BorderType)enumerator.Current;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				}
				goto IL_4E;
				IL_90:
				num = 2;
				continue;
				IL_9A:
				num = 6;
				continue;
				IL_4E:
				goto IL_9A;
			}
			IL_DC:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						disposable.Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_11C;
					case 2:
						if (disposable != null)
						{
							num = 0;
							continue;
						}
						goto IL_11E;
					}
					break;
				}
			}
			IL_11C:
			IL_11E:;
		}
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x00233334 File Offset: 0x00232334
	public Color ᜐ()
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
		return this.ᜈ();
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x00233378 File Offset: 0x00232378
	public void ᜁ(Color A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x002333BC File Offset: 0x002323BC
	internal Color ᜈ()
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
		return this.ᜀ(0).\u1712();
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x00233404 File Offset: 0x00232404
	internal void ᜀ(Color A_0)
	{
		IEnumerator enumerator = this.ᜀ.ᜀ().GetKeyList().GetEnumerator();
		try
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B2;
				case 2:
				{
					BorderType a_;
					if (spr\u2451.ᜀ(a_))
					{
						num = 5;
						continue;
					}
					break;
				}
				case 3:
				{
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					BorderType a_ = (BorderType)enumerator.Current;
					num = 2;
					continue;
				}
				case 5:
				{
					BorderType a_;
					this.ᜁ(a_).ᜀ(A_0);
					num = 1;
					continue;
				}
				case 6:
					num = 0;
					continue;
				}
				IL_76:
				num = 3;
				continue;
				goto IL_76;
			}
			IL_B2:
			if (true)
			{
			}
		}
		finally
		{
			for (;;)
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 2;
							continue;
						case 1:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_118;
						case 2:
							goto IL_FA;
						}
						break;
					}
				}
				IL_FA:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_110;
				}
			}
			IL_110:
			if (false)
			{
			}
			IL_118:;
		}
	}

	// Token: 0x060021E9 RID: 8681 RVA: 0x0023353C File Offset: 0x0023253C
	internal bool ᜎ()
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
		return this.ᜀ(0).ᜉ();
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x00233584 File Offset: 0x00232584
	internal void ᜀ(bool A_0)
	{
		IEnumerator enumerator = this.ᜀ.ᜀ().GetKeyList().GetEnumerator();
		try
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B2;
				case 2:
				{
					BorderType a_;
					if (spr\u2451.ᜀ(a_))
					{
						num = 6;
						continue;
					}
					break;
				}
				case 4:
					num = 0;
					continue;
				case 5:
				{
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					BorderType a_ = (BorderType)enumerator.Current;
					num = 2;
					continue;
				}
				case 6:
				{
					BorderType a_;
					this.ᜁ(a_).ᜁ(A_0);
					num = 3;
					continue;
				}
				}
				IL_76:
				num = 5;
				continue;
				goto IL_76;
			}
			IL_B2:;
		}
		finally
		{
			for (;;)
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F2;
						case 1:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_118;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_F2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_108;
				}
			}
			IL_108:
			if (true)
			{
			}
			if (false)
			{
			}
			IL_118:;
		}
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x002336BC File Offset: 0x002326BC
	internal bool ᜂ(BorderType A_0)
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
		return this.ᜀ.ᜀ().Contains(A_0);
	}

	// Token: 0x060021EC RID: 8684 RVA: 0x00233710 File Offset: 0x00232710
	public double ᜆ()
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
		return this.ᜀ(0).ᜏ();
	}

	// Token: 0x060021ED RID: 8685 RVA: 0x00233758 File Offset: 0x00232758
	public void ᜁ(double A_0)
	{
		IEnumerator enumerator = this.ᜀ.ᜀ().GetKeyList().GetEnumerator();
		try
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					BorderType a_;
					if (spr\u2451.ᜀ(a_))
					{
						num = 4;
						continue;
					}
					break;
				}
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					BorderType a_ = (BorderType)enumerator.Current;
					num = 0;
					continue;
				}
				case 4:
				{
					BorderType a_;
					this.ᜁ(a_).ᜂ(A_0);
					num = 1;
					continue;
				}
				case 5:
					goto IL_A8;
				case 6:
					num = 5;
					continue;
				}
				IL_6C:
				num = 2;
				continue;
				goto IL_6C;
			}
			IL_A8:;
		}
		finally
		{
			for (;;)
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_E8;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_106;
						}
						break;
					}
				}
				IL_E8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_FE;
				}
			}
			IL_FE:
			if (false)
			{
			}
			IL_106:;
		}
		if (true)
		{
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x00233890 File Offset: 0x00232890
	public bool ᜂ()
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
		return this.ᜀ(0).\u1719();
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x002338D8 File Offset: 0x002328D8
	public void ᜁ(bool A_0)
	{
		IEnumerator enumerator = this.ᜀ.ᜀ().GetKeyList().GetEnumerator();
		try
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (!enumerator.MoveNext())
					{
						num = 5;
						continue;
					}
					BorderType a_ = (BorderType)enumerator.Current;
					num = 4;
					continue;
				}
				case 2:
					goto IL_BD;
				case 4:
				{
					BorderType a_;
					if (spr\u2451.ᜀ(a_))
					{
						num = 6;
						continue;
					}
					break;
				}
				case 5:
					num = 2;
					continue;
				case 6:
				{
					if (true)
					{
					}
					BorderType a_;
					this.ᜁ(a_).ᜀ(A_0);
					num = 1;
					continue;
				}
				}
				IL_76:
				num = 0;
				continue;
				goto IL_76;
			}
			IL_BD:;
		}
		finally
		{
			for (;;)
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_11B;
						case 1:
							disposable.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_FD;
						}
						break;
					}
				}
				IL_FD:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_113;
				}
			}
			IL_113:
			if (false)
			{
			}
			IL_11B:;
		}
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x00233A14 File Offset: 0x00232A14
	public void ᜉ()
	{
		IEnumerator enumerator = this.\u170D();
		try
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					spr\u2587 spr_u = (spr\u2587)enumerator.Current;
					spr_u.\u171A();
					num = 0;
					continue;
				}
				case 3:
					goto IL_6D;
				case 4:
					num = 3;
					continue;
				}
				IL_4B:
				num = 1;
				continue;
				goto IL_4B;
			}
			IL_6D:;
		}
		finally
		{
			for (;;)
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_CB;
						case 1:
							goto IL_AD;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_AD:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_C3;
				}
			}
			IL_C3:
			if (false)
			{
			}
			IL_CB:;
		}
		if (true)
		{
		}
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x00233B10 File Offset: 0x00232B10
	public IEnumerator \u170D()
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
		return new spr\u2451.ᜀ(this);
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x00233B54 File Offset: 0x00232B54
	internal bool ᜃ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = this.\u170D();
			bool result;
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						spr\u2587 spr_u = (spr\u2587)enumerator.Current;
						num = 4;
						continue;
					}
					case 3:
						goto IL_AA;
					case 4:
					{
						spr\u2587 spr_u;
						if (spr_u.ᜆ())
						{
							num = 6;
							continue;
						}
						break;
					}
					case 5:
						goto IL_B5;
					case 6:
						result = true;
						num = 3;
						continue;
					}
					IL_5B:
					num = 2;
					continue;
					goto IL_5B;
				}
				IL_AA:
				return result;
				IL_B5:
				return false;
			}
			finally
			{
				for (;;)
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_11A;
							case 1:
								disposable.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_FC;
							}
							break;
						}
					}
					IL_FC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_112;
					}
				}
				IL_112:
				if (false)
				{
				}
				IL_11A:;
			}
			return result;
		}
		}
	}

	// Token: 0x060021F3 RID: 8691 RVA: 0x00233C90 File Offset: 0x00232C90
	private static bool ᜀ(BorderType A_0)
	{
		if (A_0 != BorderType.DiagonalDown)
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
				return A_0 != BorderType.DiagonalUp;
			}
		}
		return false;
	}

	// Token: 0x040020BE RID: 8382
	private readonly sprᢟ ᜀ;

	// Token: 0x0200027B RID: 635
	private class ᜀ : IEnumerator
	{
		// Token: 0x060021F4 RID: 8692 RVA: 0x00233CDC File Offset: 0x00232CDC
		internal ᜀ(spr\u2451 A_0)
		{
			this.ᜀ = A_0;
			this.ᜁ = -1;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00233D00 File Offset: 0x00232D00
		public bool ᜀ()
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
				if (this.ᜁ < this.ᜀ.ᜅ() - 1)
				{
					this.ᜁ++;
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00233D64 File Offset: 0x00232D64
		public void ᜂ()
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
			this.ᜁ = -1;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00233DA8 File Offset: 0x00232DA8
		public object ᜁ()
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
			return this.ᜀ.ᜀ(this.ᜁ);
		}

		// Token: 0x040020BF RID: 8383
		private readonly spr\u2451 ᜀ;

		// Token: 0x040020C0 RID: 8384
		private int ᜁ;
	}
}

using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x02000352 RID: 850
internal class spr\u2515
{
	// Token: 0x06002D56 RID: 11606 RVA: 0x002B6B58 File Offset: 0x002B5B58
	internal void ᜀ(spr᠐ A_0, spr᠐ A_1, bool A_2, bool A_3, sprẜ A_4)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				this.ᜀ = A_0;
				this.ᜁ = A_1;
				this.ᜆ = A_2;
				this.ᜇ = A_3;
				this.ᜂ = this.ᜀ.\u1712().X;
				this.ᜃ = this.ᜀ.\u1712().Y;
				this.ᜄ = this.ᜁ.\u1712().X;
				this.ᜅ = this.ᜁ.\u1712().Y;
				this.\u1718();
				this.\u1716();
				this.\u1719();
				this.ᜁ(A_4);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜀ(A_4);
							num = 0;
							continue;
						}
						break;
					case 2:
						if (this.ᜂ())
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
			return;
		}
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x002B6C8C File Offset: 0x002B5C8C
	private void \u1719()
	{
		if (this.ᜊ() != BorderType.Horizontal)
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
				return;
			}
		}
		this.ᜃ += this.ᜇ();
		this.ᜅ += this.ᜇ();
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x002B6CFC File Offset: 0x002B5CFC
	private void \u1718()
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				BorderType borderType;
				switch (num)
				{
				case 0:
					switch (borderType)
					{
					case BorderType.Bottom:
						goto IL_1DF;
					case BorderType.Left:
						goto IL_94;
					case BorderType.Right:
						this.\u1717();
						num = 12;
						continue;
					case BorderType.Top:
					case BorderType.Horizontal:
						return;
					case BorderType.Vertical:
						num = 9;
						continue;
					}
					goto IL_271;
				case 1:
					this.ᜅ = this.ᜁ.\u1713().\u1712().Y;
					num = 6;
					continue;
				case 2:
					num = 7;
					continue;
				case 3:
					if (this.ᜁ())
					{
						num = 4;
						continue;
					}
					goto IL_212;
				case 4:
				{
					PointF pointF = this.ᜁ(this.ᜁ(this.ᜀ) / 2f);
					this.ᜂ = pointF.X;
					this.ᜃ = pointF.Y;
					PointF pointF2 = this.ᜀ(this.ᜁ(this.ᜁ) / 2f);
					this.ᜄ = pointF2.X;
					this.ᜅ = pointF2.Y;
					num = 11;
					continue;
				}
				case 5:
					return;
				case 6:
					goto IL_1DA;
				case 7:
					if (this.ᜁ.\u170D())
					{
						num = 1;
						continue;
					}
					goto IL_13E;
				case 8:
					return;
				case 9:
					if (this.ᜁ.\u1715())
					{
						num = 2;
						continue;
					}
					goto IL_13E;
				case 11:
					goto IL_212;
				case 12:
					goto IL_1A9;
				}
				if (!this.ᜆ)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
				IL_212:
				borderType = this.ᜊ();
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_271:
					num = 8;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			return;
			IL_94:
			this.ᜂ -= this.ᜇ() / 2f;
			this.ᜄ -= this.ᜇ() / 2f;
			return;
			IL_13E:
			this.\u1717();
			return;
			IL_1A9:
			return;
			IL_1DA:
			goto IL_13E;
			IL_1DF:
			this.ᜃ = this.ᜅ;
			this.ᜃ += this.ᜇ();
			this.ᜅ += this.ᜇ();
			return;
		}
		}
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x002B6F8C File Offset: 0x002B5F8C
	private void \u1717()
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
		this.ᜂ += this.ᜇ() / 2f;
		this.ᜄ += this.ᜇ() / 2f;
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x002B6FFC File Offset: 0x002B5FFC
	private void \u1716()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (true)
				{
				}
				PointF pointF = this.ᜁ(-this.ᜁ(this.ᜀ));
				this.ᜂ = pointF.X;
				this.ᜃ = pointF.Y;
				num = 4;
				continue;
			}
			case 1:
				if (this.\u1714())
				{
					num = 2;
					continue;
				}
				goto IL_DC;
			case 2:
			{
				PointF pointF2 = this.ᜀ(-this.ᜁ(this.ᜁ));
				this.ᜄ = pointF2.X;
				this.ᜅ = pointF2.Y;
				goto IL_72;
			}
			case 4:
				goto IL_7C;
			case 5:
				goto IL_DC;
			}
			if (this.\u1715())
			{
				num = 0;
				continue;
			}
			goto IL_7C;
			IL_72:
			num = 5;
			continue;
			IL_DC:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_72;
			default:
				goto IL_F2;
			}
			IL_7C:
			num = 1;
		}
		IL_F2:
		if (false)
		{
		}
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x002B7104 File Offset: 0x002B6104
	private bool \u1715()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_7F;
			case 2:
				return true;
			case 3:
				if (!this.ᜀ.ᜂ())
				{
					num = 1;
					continue;
				}
				return true;
			}
			if (this.ᜀ())
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			else
			{
				num = 3;
			}
		}
		return true;
		IL_7F:
		return this.ᜀ.ᜅ();
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x002B71A0 File Offset: 0x002B61A0
	private bool \u1714()
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 2;
				continue;
			case 1:
				goto IL_5F;
			case 2:
				if (this.ᜁ.\u1715())
				{
					num = 3;
					continue;
				}
				return false;
			case 3:
				goto IL_80;
			case 5:
				if (!this.ᜁ.ᜅ())
				{
					num = 1;
					continue;
				}
				return true;
			}
			if (this.ᜀ())
			{
				num = 0;
			}
			else
			{
				num = 5;
			}
		}
		IL_5F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
			if (false)
			{
			}
			return this.ᜁ.ᜂ();
		}
		IL_80:
		return !this.ᜆ;
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x002B726C File Offset: 0x002B626C
	private float ᜁ(spr᠐ A_0)
	{
		int a_ = 3;
		for (;;)
		{
			IL_2D:
			BorderType borderType = this.ᜊ();
			for (;;)
			{
				IL_34:
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_140;
					case 1:
						goto IL_ED;
					case 2:
						if (!A_0.\u170D())
						{
							num = 0;
							continue;
						}
						goto IL_70;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 4:
						goto IL_C5;
					case 5:
						if (true)
						{
						}
						if (!A_0.ᜀ())
						{
							num = 1;
							continue;
						}
						goto IL_EF;
					case 6:
						switch (borderType)
						{
						case BorderType.Bottom:
							goto IL_64;
						case BorderType.Left:
							goto IL_86;
						case BorderType.Right:
							goto IL_107;
						case BorderType.Top:
							goto IL_113;
						case BorderType.Horizontal:
							num = 5;
							continue;
						case BorderType.Vertical:
							num = 2;
							continue;
						default:
							num = 3;
							continue;
						}
						break;
					}
					goto IL_2D;
				}
			}
		}
		IL_64:
		return A_0.ᜎ().\u171E();
		IL_70:
		return A_0.\u1716().\u171E();
		IL_86:
		return A_0.\u1716().\u171E();
		IL_C5:
		throw new InvalidOperationException(ClipboardData.b("㱨ժ࡬ᝮŰᙲᙴͶᱸὺ嵼ᵾﮈꮊ歷뮔", a_));
		IL_ED:
		return A_0.ᜏ().\u171E();
		IL_EF:
		return A_0.ᜎ().\u171E();
		IL_107:
		return A_0.ᜉ().\u171E();
		IL_113:
		return A_0.ᜏ().\u171E();
		IL_140:
		return A_0.ᜉ().\u171E();
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x002B73D0 File Offset: 0x002B63D0
	private void ᜁ(sprẜ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				float[] a_ = null;
				int[] array = null;
				bool flag = this.ᜎ();
				int num = 1;
				for (;;)
				{
					int num2;
					bool flag2;
					float[] a_5;
					int[] array2;
					float[] array3;
					spr᪑ spr᪑;
					float a_6;
					switch (num)
					{
					case 0:
					{
						PointF a_2 = this.ᜁ(spr\u2515.ᜀ(array[num2], a_));
						num = 21;
						continue;
					}
					case 1:
						goto IL_9C;
					case 2:
						goto IL_205;
					case 3:
					{
						if (flag2)
						{
							num = 6;
							continue;
						}
						PointF a_3 = new PointF(this.ᜄ, this.ᜅ);
						num = 18;
						continue;
					}
					case 4:
						goto IL_326;
					case 5:
					{
						bool a_4 = this.ᜁ.\u1714() ^ this.\u1712() ^ this.ᜐ() ^ this.ᜏ();
						a_5 = this.ᜀ(a_4);
						array2 = this.ᜀ(this.ᜁ);
						num = 2;
						continue;
					}
					case 6:
					{
						PointF a_3 = this.ᜀ(spr\u2515.ᜀ(array2[num2], a_5));
						num = 19;
						continue;
					}
					case 7:
						num = 12;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9C;
						default:
							if (false)
							{
							}
							num = 17;
							continue;
						}
						break;
					case 9:
						goto IL_1E6;
					case 10:
					{
						PointF a_2;
						PointF a_3;
						spr᪑ = this.ᜀ(a_2, a_3, array3[num2]);
						goto IL_36C;
					}
					case 11:
						if (num2 >= array3.Length)
						{
							num = 15;
							continue;
						}
						a_6 = array3[num2] / 2f;
						this.ᜂ(a_6);
						num = 23;
						continue;
					case 12:
					{
						if (flag)
						{
							num = 0;
							continue;
						}
						PointF a_2 = new PointF(this.ᜂ, this.ᜃ);
						num = 24;
						continue;
					}
					case 13:
						if (flag2)
						{
							num = 5;
							continue;
						}
						goto IL_205;
					case 14:
						if (!this.ᜄ())
						{
							num = 8;
							continue;
						}
						num = 10;
						continue;
					case 15:
						return;
					case 16:
						goto IL_326;
					case 17:
					{
						PointF a_2;
						PointF a_3;
						spr᪑ = this.ᜀ(a_2, a_3, num2, array3);
						goto IL_36C;
					}
					case 18:
						goto IL_116;
					case 19:
						goto IL_116;
					case 20:
					{
						bool a_7 = this.ᜀ.\u1714() ^ this.\u1713() ^ this.ᜑ() ^ this.ᜏ();
						a_ = this.ᜀ(a_7);
						array = this.ᜀ(this.ᜀ);
						num = 22;
						continue;
					}
					case 21:
						goto IL_34B;
					case 22:
						goto IL_24B;
					case 23:
						if (!spr\u1CC6.ᜀ((long)num2))
						{
							num = 7;
							continue;
						}
						goto IL_1E6;
					case 24:
						goto IL_34B;
					}
					break;
					IL_9C:
					if (flag)
					{
						num = 20;
						continue;
					}
					goto IL_24B;
					IL_116:
					if (true)
					{
					}
					num = 14;
					continue;
					IL_1E6:
					this.ᜂ(a_6);
					num2++;
					num = 4;
					continue;
					IL_205:
					array3 = this.ᜀ(this.ᜏ());
					num2 = 0;
					num = 16;
					continue;
					IL_24B:
					a_5 = null;
					array2 = null;
					flag2 = this.\u170D();
					num = 13;
					continue;
					IL_326:
					num = 11;
					continue;
					IL_34B:
					num = 3;
					continue;
					IL_36C:
					spr᪑ a_8 = spr᪑;
					A_0.ᜀ(a_8);
					num = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x002B7764 File Offset: 0x002B6764
	private bool \u1713()
	{
		int a_ = 2;
		for (;;)
		{
			BorderType borderType = this.ᜊ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						switch (borderType)
						{
						case BorderType.Bottom:
						case BorderType.Horizontal:
							return true;
						case BorderType.Left:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_84;
							}
							break;
						case BorderType.Right:
						case BorderType.Vertical:
							return true;
						case BorderType.Top:
							return false;
						}
						break;
					}
					num = 1;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_60;
				}
				break;
			}
		}
		return false;
		IL_60:
		throw new InvalidOperationException(ClipboardData.b("㵧ѩݫmὯձᩳ噵᩷ᕹ๻᩽ꒃ憎ꂍ", a_));
		IL_84:
		if (true)
		{
		}
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x002B7818 File Offset: 0x002B6818
	private bool \u1712()
	{
		int a_ = 15;
		for (;;)
		{
			BorderType borderType = this.ᜊ();
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_72;
				case 1:
					num = 0;
					continue;
				case 2:
					for (;;)
					{
						switch (borderType)
						{
						case BorderType.Bottom:
						case BorderType.Horizontal:
							return false;
						case BorderType.Left:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_8C;
							}
							break;
						case BorderType.Right:
						case BorderType.Vertical:
							return false;
						case BorderType.Top:
							return true;
						}
						break;
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		return true;
		IL_72:
		throw new InvalidOperationException(ClipboardData.b("⁴᥶ቸᕺቼࡾꎂﮈﶎ놐ﲘ떚", a_));
		IL_8C:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x002B78CC File Offset: 0x002B68CC
	private bool ᜑ()
	{
		int a_ = 14;
		for (;;)
		{
			BorderType borderType = this.ᜊ();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_84;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						switch (borderType)
						{
						case BorderType.Bottom:
						case BorderType.Right:
							goto IL_84;
						case BorderType.Left:
						case BorderType.Top:
						case BorderType.Horizontal:
						case BorderType.Vertical:
							return false;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_9B;
					}
					break;
				}
				break;
			}
			}
		}
		return false;
		IL_84:
		return this.ᜋ().ᜉ();
		IL_9B:
		throw new InvalidOperationException(ClipboardData.b("ⅳᡵ፷ᑹ፻ॽꊁ慎ﲍ낏ﶗ뒙", a_));
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x002B798C File Offset: 0x002B698C
	private bool ᜐ()
	{
		int a_ = 9;
		for (;;)
		{
			IL_2D:
			BorderType borderType = this.ᜊ();
			for (;;)
			{
				IL_34:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (borderType)
						{
						case BorderType.Bottom:
							return false;
						case BorderType.Left:
							num = 2;
							continue;
						case BorderType.Right:
							num = 3;
							continue;
						case BorderType.Top:
							goto IL_D7;
						case BorderType.Horizontal:
							goto IL_E3;
						case BorderType.Vertical:
							goto IL_64;
						default:
							num = 6;
							continue;
						}
						break;
					case 1:
						goto IL_D5;
					case 2:
						if (!this.ᜁ.\u1715())
						{
							return false;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 3:
						if (!this.ᜁ.\u1715())
						{
							num = 4;
							continue;
						}
						return false;
					case 4:
						goto IL_112;
					case 5:
						goto IL_C6;
					case 6:
						num = 1;
						continue;
					}
					goto IL_2D;
				}
			}
		}
		IL_64:
		if (true)
		{
		}
		return false;
		IL_C6:
		return this.ᜋ().ᜉ();
		IL_D5:
		throw new InvalidOperationException(ClipboardData.b("㩮ὰᡲ᭴ᡶ๸ᕺ嵼ᵾﮈꮊ歷뮔", a_));
		IL_D7:
		return this.ᜋ().ᜉ();
		IL_E3:
		return this.ᜋ().ᜉ();
		IL_112:
		return this.ᜋ().ᜉ();
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x002B7AD4 File Offset: 0x002B6AD4
	private bool ᜏ()
	{
		int a_ = 10;
		for (;;)
		{
			BorderType borderType = this.ᜊ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					if (true)
					{
					}
					switch (borderType)
					{
					case BorderType.Bottom:
					case BorderType.Right:
						goto IL_66;
					case BorderType.Left:
					case BorderType.Top:
						return false;
					case BorderType.Horizontal:
					case BorderType.Vertical:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_97;
						}
						break;
					}
					num = 2;
					continue;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_66:
		return !this.ᜋ().ᜉ();
		IL_7F:
		throw new InvalidOperationException(ClipboardData.b("╯ᱱέᡵ᝷൹ቻ幽겋揄뢕", a_));
		IL_97:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x002B7B94 File Offset: 0x002B6B94
	private float[] ᜀ(bool A_0)
	{
		float[] array;
		for (;;)
		{
			array = spr\u2587.ᜁ(this.ᜅ(), this.ᜆ());
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return array;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return array;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_0)
						{
							num = 2;
							continue;
						}
						return array;
					}
					break;
				case 2:
					Array.Reverse(array);
					num = 0;
					continue;
				}
				break;
			}
		}
		return array;
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x002B7C1C File Offset: 0x002B6C1C
	private void ᜀ(spr\u23F1 A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			float[] array = spr\u2587.ᜃ(this.ᜅ(), 1f);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (array != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				case 1:
					return;
				case 2:
					A_0.ᜁ(array);
					num = 1;
					continue;
				}
				break;
			}
		}
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x002B7CA4 File Offset: 0x002B6CA4
	private void ᜀ(sprẜ A_0)
	{
		switch (0)
		{
		default:
		{
			PointF a_;
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_F8:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					this.ᜂ = this.ᜀ.\u1712().X;
					this.ᜃ = this.ᜀ.\u1712().Y;
					this.ᜄ = this.ᜁ.\u1712().X;
					this.ᜅ = this.ᜁ.\u1712().Y;
					this.ᜂ(-this.ᜇ() / 2f);
					a_ = this.ᜁ(-this.ᜇ());
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_11B;
					case 1:
						goto IL_104;
					case 2:
						if (!this.ᜁ())
						{
							num = 3;
							continue;
						}
						goto IL_F8;
					case 3:
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_104:
			if (true)
			{
			}
			float num2 = (float)0;
			goto IL_11E;
			IL_11B:
			num2 = (float)1;
			IL_11E:
			float num3 = num2;
			PointF a_2 = this.ᜀ(this.ᜇ() * num3);
			spr\u1B70 spr_u1B = spr\u1B70.ᜀ(a_, a_2);
			spr_u1B.ᜀ(new spr\u23F1(spr\u2262.ᜋ, this.ᜇ()));
			A_0.ᜀ(spr_u1B);
			return;
		}
		}
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x002B7E08 File Offset: 0x002B6E08
	private void ᜂ(float A_0)
	{
		int a_ = 15;
		for (;;)
		{
			BorderType borderType = this.ᜊ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						switch (borderType)
						{
						case BorderType.Bottom:
						case BorderType.Horizontal:
							goto IL_AD;
						case BorderType.Left:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_E0;
							}
							break;
						case BorderType.Right:
						case BorderType.Vertical:
							goto IL_79;
						case BorderType.Top:
							goto IL_5C;
						}
						break;
					}
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					goto IL_AB;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_5C:
		this.ᜃ += A_0;
		this.ᜅ += A_0;
		return;
		IL_79:
		this.ᜂ -= A_0;
		this.ᜄ -= A_0;
		return;
		IL_AB:
		throw new InvalidOperationException(ClipboardData.b("⁴᥶ᱸͺർ᩾ꦈﶎ랖爵辠", a_));
		IL_AD:
		this.ᜃ -= A_0;
		this.ᜅ -= A_0;
		return;
		IL_E0:
		if (false)
		{
		}
		this.ᜂ += A_0;
		this.ᜄ += A_0;
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x002B7F2C File Offset: 0x002B6F2C
	private PointF ᜁ(float A_0)
	{
		if (!this.ᜀ())
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
				return new PointF(this.ᜂ - A_0, this.ᜃ);
			}
		}
		return new PointF(this.ᜂ, this.ᜃ - A_0);
	}

	// Token: 0x06002D69 RID: 11625 RVA: 0x002B7F98 File Offset: 0x002B6F98
	private PointF ᜀ(float A_0)
	{
		if (this.ᜀ())
		{
			if (true)
			{
			}
		}
		else
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
				return new PointF(this.ᜄ + A_0, this.ᜅ);
			}
		}
		return new PointF(this.ᜄ, this.ᜅ + A_0);
	}

	// Token: 0x06002D6A RID: 11626 RVA: 0x002B8004 File Offset: 0x002B7004
	private int[] ᜀ(spr᠐ A_0)
	{
		for (;;)
		{
			int num = this.ᜋ().\u170D();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8B;
			default:
			{
				if (false)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						num2 = 1;
						continue;
					case 1:
						goto IL_AD;
					case 2:
						switch (num)
						{
						case 3:
							goto IL_8B;
						case 4:
							goto IL_AF;
						case 5:
							goto IL_74;
						default:
							num2 = 0;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
			}
		}
		IL_74:
		return this.ᜀ(A_0, spr\u2515.ᜋ, spr\u2515.ᜌ, spr\u2515.\u170D);
		IL_8B:
		return this.ᜀ(A_0, spr\u2515.ᜈ, spr\u2515.ᜉ, spr\u2515.ᜊ);
		IL_AD:
		IL_AF:
		return null;
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x002B80C4 File Offset: 0x002B70C4
	private int[] ᜀ(spr᠐ A_0, int[] A_1, int[] A_2, int[] A_3)
	{
		int a_ = 9;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_239;
			default:
			{
				if (false)
				{
				}
				int num = A_0.ᜆ();
				int num2 = 12;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return A_2;
					case 1:
						num2 = 14;
						continue;
					case 2:
						if (!A_0.ᜋ())
						{
							num2 = 0;
							continue;
						}
						return A_1;
					case 3:
						return A_2;
					case 4:
						return A_2;
					case 5:
						if (!A_0.ᜀ())
						{
							num2 = 4;
							continue;
						}
						return A_1;
					case 6:
						return A_1;
					case 7:
						if (A_0.ᜋ())
						{
							num2 = 1;
							continue;
						}
						goto IL_216;
					case 8:
						if (A_0.ᜄ())
						{
							num2 = 15;
							continue;
						}
						goto IL_EF;
					case 9:
						if (A_0.ᜀ())
						{
							num2 = 21;
							continue;
						}
						goto IL_EF;
					case 10:
						return A_1;
					case 11:
						goto IL_17B;
					case 12:
						switch (num)
						{
						case 2:
						{
							BorderType borderType = this.ᜊ();
							num2 = 17;
							continue;
						}
						case 3:
						{
							BorderType borderType2 = this.ᜊ();
							num2 = 23;
							continue;
						}
						case 4:
							return A_3;
						default:
							num2 = 13;
							continue;
						}
						break;
					case 13:
						num2 = 11;
						continue;
					case 14:
						if (A_0.\u170D())
						{
							num2 = 22;
							continue;
						}
						goto IL_216;
					case 15:
						return A_3;
					case 16:
						num2 = 10;
						continue;
					case 17:
					{
						BorderType borderType;
						switch (borderType)
						{
						case BorderType.Horizontal:
							num2 = 20;
							continue;
						case BorderType.Vertical:
							num2 = 24;
							continue;
						default:
							num2 = 16;
							continue;
						}
						break;
					}
					case 18:
						goto IL_164;
					case 19:
						num2 = 6;
						continue;
					case 20:
						if (!A_0.ᜀ())
						{
							num2 = 18;
							continue;
						}
						return A_1;
					case 21:
						num2 = 8;
						continue;
					case 22:
						return A_3;
					case 23:
					{
						BorderType borderType2;
						switch (borderType2)
						{
						case BorderType.Horizontal:
							num2 = 9;
							continue;
						case BorderType.Vertical:
							num2 = 7;
							continue;
						default:
							num2 = 19;
							continue;
						}
						break;
					}
					case 24:
						if (!A_0.ᜋ())
						{
							num2 = 3;
							continue;
						}
						return A_1;
					}
					break;
					IL_EF:
					num2 = 5;
					continue;
					IL_216:
					num2 = 2;
				}
				break;
			}
			}
		}
		return A_1;
		IL_164:
		goto IL_239;
		IL_17B:
		throw new InvalidOperationException(ClipboardData.b("㩮ὰᙲ൴ݶᱸ᡺ॼ᩾ꎂﶎ놐ﲒ랖滛쒠삢톤슦춨讪쾬삮쎰ힲ킴얶쪸鮺\udcbc쮾ꋂ럆ꛈꋊꏌ믎￐", a_));
		IL_239:
		if (true)
		{
		}
		return A_2;
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x002B8380 File Offset: 0x002B7380
	private static float ᜀ(int A_0, float[] A_1)
	{
		float num;
		for (;;)
		{
			num = 0f;
			int num2 = A_1.Length - 1;
			int num3 = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
					goto IL_36;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_36;
					}
					break;
				case 2:
					if (A_0 <= 0)
					{
						num3 = 3;
						continue;
					}
					num += A_1[num2];
					num2--;
					A_0--;
					num3 = 1;
					continue;
				case 3:
					return num;
				}
				break;
				IL_36:
				num3 = 2;
			}
		}
		return num;
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x002B841C File Offset: 0x002B741C
	private bool ᜎ()
	{
		if (this.ᜀ())
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
				return this.ᜀ.ᜄ();
			}
		}
		return this.ᜀ.\u170D();
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x002B8478 File Offset: 0x002B7478
	private bool \u170D()
	{
		if (this.ᜀ())
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
				return this.ᜁ.ᜀ();
			}
		}
		return this.ᜁ.ᜋ();
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x002B84D4 File Offset: 0x002B74D4
	private spr\u1B70 ᜀ(PointF A_0, PointF A_1, int A_2, float[] A_3)
	{
		int a_ = 11;
		for (;;)
		{
			float num = A_3[A_2];
			spr\u1B70 spr_u1B = null;
			BorderStyle borderStyle = this.ᜅ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_EA;
				case 1:
					if (A_3.Length == 1)
					{
						num2 = 4;
						continue;
					}
					return spr_u1B;
				case 2:
					return spr_u1B;
				case 3:
					goto IL_EA;
				case 4:
					goto IL_185;
				case 5:
					goto IL_EA;
				case 6:
					goto IL_EA;
				case 7:
					switch (borderStyle)
					{
					case BorderStyle.Single:
					case BorderStyle.Thick:
					case BorderStyle.Double:
					case BorderStyle.Hairline:
					case BorderStyle.Dot:
					case BorderStyle.DashLargeGap:
					case BorderStyle.DotDash:
					case BorderStyle.DotDotDash:
					case BorderStyle.Triple:
					case BorderStyle.ThinThickSmallGap:
					case BorderStyle.ThinThinSmallGap:
					case BorderStyle.ThinThickThinSmallGap:
					case BorderStyle.ThinThickMediumGap:
					case BorderStyle.ThickThinMediumGap:
					case BorderStyle.ThickThickThinMediumGap:
					case BorderStyle.ThinThickLargeGap:
					case BorderStyle.ThickThinLargeGap:
					case BorderStyle.ThinThickThinLargeGap:
					case BorderStyle.DashSmallGap:
					case BorderStyle.Outset:
					case BorderStyle.Inset:
						spr_u1B = spr\u1D5D.ᜀ(A_0, A_1, num, this.ᜉ());
						num2 = 0;
						continue;
					case (BorderStyle)4:
						goto IL_EA;
					case BorderStyle.Wave:
						spr_u1B = spr\u1D5D.ᜂ(A_0, A_1, this.ᜇ(), this.ᜉ(), this.ᜁ());
						num2 = 10;
						continue;
					case BorderStyle.DoubleWave:
						spr_u1B = spr\u1D5D.ᜁ(A_0, A_1, this.ᜇ(), this.ᜉ(), this.ᜁ());
						num2 = 6;
						continue;
					case BorderStyle.DashDotStroker:
						spr_u1B = spr\u1D5D.ᜀ(A_0, A_1, this.ᜇ(), this.ᜉ(), this.ᜁ());
						num2 = 3;
						continue;
					case BorderStyle.Emboss3D:
						spr_u1B = spr\u1D5D.ᜀ(A_0, A_1, this.ᜀ(A_2, num, false));
						num2 = 5;
						continue;
					case BorderStyle.Engrave3D:
						spr_u1B = spr\u1D5D.ᜀ(A_0, A_1, this.ᜀ(A_2, num, true));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_185;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 13;
							continue;
						}
						break;
					default:
						num2 = 9;
						continue;
					}
					break;
				case 8:
					goto IL_EA;
				case 9:
					num2 = 8;
					continue;
				case 10:
					goto IL_EA;
				case 11:
					if (spr_u1B == null)
					{
						num2 = 12;
						continue;
					}
					num2 = 1;
					continue;
				case 12:
					goto IL_103;
				case 13:
					goto IL_EA;
				}
				break;
				IL_EA:
				num2 = 11;
				continue;
				IL_185:
				this.ᜀ(spr_u1B.ᜆ());
				num2 = 2;
			}
		}
		IL_103:
		throw new InvalidOperationException(ClipboardData.b("⑰ᵲṴ᥶ᙸ౺፼彾力권ﶒ랖쒠趢", a_));
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x002B8748 File Offset: 0x002B7748
	private spr\u24A6 ᜀ(PointF A_0, PointF A_1, float A_2)
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
		return this.ᜀ(A_0, A_1, A_2, this.ᜁ());
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x002B8794 File Offset: 0x002B7794
	private spr\u24A6 ᜀ(PointF A_0, PointF A_1, float A_2, bool A_3)
	{
		switch (0)
		{
		default:
		{
			spr\u24A6 spr_u24A;
			for (;;)
			{
				spr\u213A spr_u213A = spr\u2193.ᜀ((PageBorderArt)this.ᜅ());
				int num = 1;
				for (;;)
				{
					int num2;
					int num3;
					PageBorderArtElementPosition pageBorderArtElementPosition;
					float num5;
					float a_;
					float num4;
					SizeF sizeF;
					float num6;
					float num7;
					switch (num)
					{
					case 0:
						if (num2 >= num3)
						{
							goto IL_23B;
						}
						pageBorderArtElementPosition = this.ᜀ(num2, num3, A_3);
						num4 = this.ᜀ(A_2, num5, a_, pageBorderArtElementPosition);
						num = 12;
						continue;
					case 1:
						if (!A_3)
						{
							num = 4;
							continue;
						}
						num = 3;
						continue;
					case 2:
						sizeF = new SizeF(A_2, num4);
						goto IL_159;
					case 3:
						num6 = A_1.X - A_0.X;
						goto IL_95;
					case 4:
						num = 8;
						continue;
					case 5:
						num = 9;
						continue;
					case 6:
						goto IL_227;
					case 7:
						num7 = spr_u213A.ᜀ();
						goto IL_1D8;
					case 8:
						num6 = A_1.Y - A_0.Y;
						goto IL_95;
					case 9:
						num7 = spr_u213A.ᜂ();
						goto IL_1D8;
					case 10:
						sizeF = new SizeF(num4, A_2);
						goto IL_159;
					case 11:
						goto IL_227;
					case 12:
						if (!A_3)
						{
							num = 14;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23B;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 13:
						return spr_u24A;
					case 14:
						if (true)
						{
						}
						num = 2;
						continue;
					case 15:
						if (!A_3)
						{
							num = 5;
							continue;
						}
						num = 7;
						continue;
					}
					break;
					IL_95:
					float num8 = num6;
					num = 15;
					continue;
					IL_159:
					SizeF a_2 = sizeF;
					float num9;
					spr\u1DB3 a_3 = new spr\u1DB3(this.ᜀ(A_0, num9, A_2, A_3), a_2, spr_u213A.ᜀ(this.ᜊ(), pageBorderArtElementPosition));
					spr_u24A.ᜁ(a_3);
					num9 += num4;
					num2++;
					num = 6;
					continue;
					IL_1D8:
					num5 = num7;
					num5 = this.ᜂ(num5, num8, A_2, A_3);
					num3 = this.ᜁ(num8, A_2, num5, A_3);
					a_ = this.ᜀ(num8, A_2, num5, A_3);
					num9 = 0f;
					spr_u24A = new spr\u24A6();
					num2 = 0;
					num = 11;
					continue;
					IL_227:
					num = 0;
					continue;
					IL_23B:
					num = 13;
				}
			}
			return spr_u24A;
		}
		}
	}

	// Token: 0x06002D72 RID: 11634 RVA: 0x002B89EC File Offset: 0x002B79EC
	private float ᜂ(float A_0, float A_1, float A_2, bool A_3)
	{
		int num = 4;
		for (;;)
		{
			float num2;
			int num3;
			switch (num)
			{
			case 0:
				return A_0;
			case 1:
				if (true)
				{
				}
				A_0 = num2 / A_2;
				num = 0;
				continue;
			case 2:
				num3 = 2;
				goto IL_7A;
			case 3:
				if (num2 < A_2 * A_0)
				{
					num = 1;
					continue;
				}
				return A_0;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_68;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 6:
				goto IL_68;
			}
			if (!A_3)
			{
				num = 5;
				continue;
			}
			num = 2;
			continue;
			IL_7A:
			int num4 = num3;
			num2 = A_1 - (float)num4 * A_2;
			num = 3;
			continue;
			IL_68:
			num3 = 0;
			goto IL_7A;
		}
		return A_0;
	}

	// Token: 0x06002D73 RID: 11635 RVA: 0x002B8AB0 File Offset: 0x002B7AB0
	private PageBorderArtElementPosition ᜀ(int A_0, int A_1, bool A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_65;
			case 2:
				if (A_0 != A_1 - 1)
				{
					num = 5;
					continue;
				}
				return PageBorderArtElementPosition.Last;
			case 3:
				if (A_0 != 0)
				{
					num = 1;
					continue;
				}
				return PageBorderArtElementPosition.First;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_65;
				default:
					goto IL_51;
				}
				break;
			case 5:
				return PageBorderArtElementPosition.Middle;
			}
			if (true)
			{
			}
			if (!A_2)
			{
				num = 4;
				continue;
			}
			num = 3;
			continue;
			IL_65:
			num = 2;
		}
		IL_51:
		if (false)
		{
		}
		return PageBorderArtElementPosition.Middle;
	}

	// Token: 0x06002D74 RID: 11636 RVA: 0x002B8B58 File Offset: 0x002B7B58
	private float ᜀ(float A_0, float A_1, float A_2, PageBorderArtElementPosition A_3)
	{
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_3 == PageBorderArtElementPosition.Middle)
			{
				return A_0 * A_1 + A_2;
			}
			break;
		}
		return A_0;
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x002B8BA4 File Offset: 0x002B7BA4
	private PointF ᜀ(PointF A_0, float A_1, float A_2, bool A_3)
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
			if (!A_3)
			{
				if (true)
				{
				}
				return new PointF(A_0.X - A_2 / 2f, A_0.Y + A_1);
			}
			break;
		}
		return new PointF(A_0.X + A_1, A_0.Y - A_2 / 2f);
	}

	// Token: 0x06002D76 RID: 11638 RVA: 0x002B8C20 File Offset: 0x002B7C20
	private int ᜁ(float A_0, float A_1, float A_2, bool A_3)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_49;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_3E;
				}
				if (true)
				{
				}
				if (!A_3)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_49:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_69;
			}
		}
		IL_3E:
		int num2 = 2;
		goto IL_72;
		IL_69:
		if (false)
		{
		}
		num2 = 0;
		IL_72:
		int num3 = num2;
		float num4 = (float)num3 * A_1;
		int num5 = (int)((A_0 - num4) / (A_1 * A_2));
		return num3 + num5;
	}

	// Token: 0x06002D77 RID: 11639 RVA: 0x002B8CB4 File Offset: 0x002B7CB4
	private float ᜀ(float A_0, float A_1, float A_2, bool A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6B:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 2;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6B;
			case 1:
				goto IL_82;
			case 3:
				goto IL_76;
			}
			if (true)
			{
			}
			if (!A_3)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_76:
		int num2 = 2;
		goto IL_85;
		IL_82:
		num2 = 0;
		IL_85:
		int num3 = num2;
		int num4 = this.ᜁ(A_0, A_1, A_2, A_3) - num3;
		float num5 = (float)num3 * A_1;
		float num6 = (float)num4 * A_1 * A_2;
		return (A_0 - num5 - num6) / (float)num4;
	}

	// Token: 0x06002D78 RID: 11640 RVA: 0x002B8D6C File Offset: 0x002B7D6C
	private spr\u23F1 ᜀ(int A_0, float A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u2262[] array2;
			for (;;)
			{
				spr\u2262[] array = this.ᜌ();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						Array.Reverse(array2);
						num = 5;
						continue;
					case 1:
						array2 = new spr\u2262[]
						{
							array[0],
							spr\u2262.ទ,
							array[1],
							spr\u2262.ទ,
							array[2]
						};
						num = 3;
						continue;
					case 2:
						goto IL_54;
					case 3:
						goto IL_54;
					case 4:
						if (A_2)
						{
							num = 1;
							continue;
						}
						array2 = new spr\u2262[]
						{
							array[2],
							spr\u2262.ទ,
							array[1],
							spr\u2262.ទ,
							array[0]
						};
						num = 2;
						continue;
					case 5:
						goto IL_131;
					case 6:
						if (this.ᜏ())
						{
							goto IL_84;
						}
						goto IL_133;
					}
					break;
					IL_54:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_84:
						if (true)
						{
						}
						num = 0;
						break;
					default:
						if (false)
						{
						}
						num = 6;
						break;
					}
				}
			}
			IL_131:
			IL_133:
			return new spr\u23F1(array2[A_0], A_1);
		}
		}
	}

	// Token: 0x06002D79 RID: 11641 RVA: 0x002B8EB8 File Offset: 0x002B7EB8
	private spr\u2262[] ᜌ()
	{
		spr\u2262[] array;
		for (;;)
		{
			array = new spr\u2262[3];
			spr᪅ spr᪅ = new spr᪅(this.ᜉ());
			if (true)
			{
			}
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return array;
				case 1:
					return array;
				case 2:
					if (spr᪅.ᜃ() < 170f)
					{
						num = 6;
						continue;
					}
					array[0] = spr᪅.ᜀ(this.ᜉ(), -170f);
					array[1] = spr᪅.ᜀ(this.ᜉ(), -85f);
					array[2] = this.ᜉ();
					num = 3;
					continue;
				case 3:
					return array;
				case 4:
					array[0] = this.ᜉ();
					array[1] = spr᪅.ᜀ(this.ᜉ(), 85f);
					array[2] = spr᪅.ᜀ(this.ᜉ(), 170f);
					num = 0;
					continue;
				case 5:
					if (spr᪅.ᜃ() < 85f)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return array;
					default:
						if (false)
						{
						}
						array[0] = spr᪅.ᜀ(this.ᜉ(), -85f);
						array[1] = this.ᜉ();
						array[2] = spr᪅.ᜀ(this.ᜉ(), 85f);
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		return array;
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x002B9030 File Offset: 0x002B8030
	private spr\u2587 ᜋ()
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
			if (this.ᜀ())
			{
				return this.ᜀ.ᜏ();
			}
			break;
		}
		return this.ᜀ.\u1716();
	}

	// Token: 0x06002D7B RID: 11643 RVA: 0x002B908C File Offset: 0x002B808C
	private BorderType ᜊ()
	{
		if (true)
		{
		}
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 9;
				continue;
			case 1:
				num = 4;
				continue;
			case 2:
				if (this.ᜇ)
				{
					num = 15;
					continue;
				}
				return BorderType.Horizontal;
			case 3:
				return BorderType.Bottom;
			case 4:
				if (this.ᜁ.\u1715())
				{
					goto IL_8F;
				}
				goto IL_EF;
			case 5:
				return BorderType.Right;
			case 6:
				if (this.ᜀ.ᜇ())
				{
					num = 8;
					continue;
				}
				goto IL_13E;
			case 7:
				if (this.ᜁ.ᜊ())
				{
					num = 17;
					continue;
				}
				goto IL_C7;
			case 8:
				num = 13;
				continue;
			case 9:
				if (this.ᜁ.ᜌ())
				{
					num = 11;
					continue;
				}
				goto IL_114;
			case 11:
				return BorderType.Top;
			case 12:
				if (this.ᜀ.ᜌ())
				{
					num = 0;
					continue;
				}
				goto IL_114;
			case 13:
				if (this.ᜁ.ᜇ())
				{
					num = 5;
					continue;
				}
				goto IL_13E;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8F;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 15:
				return BorderType.Vertical;
			case 16:
				if (this.ᜀ.\u1715())
				{
					num = 1;
					continue;
				}
				goto IL_EF;
			case 17:
				return BorderType.Left;
			}
			if (this.ᜀ.ᜊ())
			{
				num = 14;
				continue;
			}
			goto IL_C7;
			IL_8F:
			num = 3;
			continue;
			IL_C7:
			num = 6;
			continue;
			IL_EF:
			num = 2;
			continue;
			IL_114:
			num = 16;
			continue;
			IL_13E:
			num = 12;
		}
		return BorderType.Bottom;
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x002B927C File Offset: 0x002B827C
	private spr\u2262 ᜉ()
	{
		spr\u2262 spr_u;
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
			spr_u = spr\u2262.ᜀ(this.ᜋ().\u1712());
			if (spr_u.ᜇ())
			{
				return spr\u2262.ᜋ;
			}
			break;
		}
		return spr_u;
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x002B92DC File Offset: 0x002B82DC
	private float ᜈ()
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
		return (float)this.ᜋ().\u1715();
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x002B9324 File Offset: 0x002B8324
	private float ᜇ()
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
		return this.ᜋ().\u171E();
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x002B936C File Offset: 0x002B836C
	private float ᜆ()
	{
		for (;;)
		{
			BorderStyle borderStyle = this.ᜅ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (borderStyle)
					{
					case BorderStyle.Single:
					case BorderStyle.Dot:
					case BorderStyle.DashLargeGap:
					case BorderStyle.DotDash:
					case BorderStyle.DotDotDash:
					case BorderStyle.Wave:
					case BorderStyle.DoubleWave:
					case BorderStyle.DashSmallGap:
					case BorderStyle.DashDotStroker:
						goto IL_F2;
					case BorderStyle.Thick:
					case BorderStyle.Hairline:
						goto IL_AE;
					case BorderStyle.Double:
					case BorderStyle.Triple:
					case BorderStyle.ThinThickSmallGap:
					case BorderStyle.ThinThinSmallGap:
					case BorderStyle.ThinThickThinSmallGap:
					case BorderStyle.ThinThickMediumGap:
					case BorderStyle.ThickThinMediumGap:
					case BorderStyle.ThickThickThinMediumGap:
					case BorderStyle.ThinThickLargeGap:
					case BorderStyle.ThickThinLargeGap:
					case BorderStyle.ThinThickThinLargeGap:
					case BorderStyle.Emboss3D:
					case BorderStyle.Engrave3D:
						goto IL_B4;
					case (BorderStyle)4:
						goto IL_F9;
					case BorderStyle.Outset:
					case BorderStyle.Inset:
						goto IL_EC;
					}
					goto IL_97;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_97;
					default:
						goto IL_E4;
					}
					break;
				}
				break;
				IL_97:
				num = 0;
			}
		}
		IL_AE:
		return 1f;
		IL_B4:
		if (true)
		{
		}
		return this.ᜈ();
		IL_E4:
		if (false)
		{
		}
		goto IL_F9;
		IL_EC:
		return 1f;
		IL_F2:
		return this.ᜇ();
		IL_F9:
		return this.ᜇ();
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x002B9478 File Offset: 0x002B8478
	private BorderStyle ᜅ()
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
		return this.ᜋ().ᜈ();
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x002B94C0 File Offset: 0x002B84C0
	private bool ᜄ()
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
		return this.ᜋ().\u1714();
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x002B9508 File Offset: 0x002B8508
	private bool ᜃ()
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
		return this.ᜋ().\u171B();
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x002B9550 File Offset: 0x002B8550
	private bool ᜂ()
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						if (this.ᜊ() != BorderType.Bottom)
						{
							num = 2;
							continue;
						}
						return true;
					}
					break;
				case 1:
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					goto IL_89;
				}
				if (!this.ᜋ().\u1719())
				{
					return false;
				}
				num = 1;
			}
		}
		return true;
		IL_89:
		return this.ᜊ() == BorderType.Right;
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x002B95EC File Offset: 0x002B85EC
	private bool ᜁ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_73:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_30;
		}
		BorderType borderType;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				return false;
			case 1:
				goto IL_73;
			case 2:
				switch (borderType)
				{
				case BorderType.Bottom:
				case BorderType.Top:
				case BorderType.Horizontal:
					return true;
				case BorderType.Left:
				case BorderType.Right:
					return false;
				default:
					num = 1;
					continue;
				}
				break;
			}
			goto IL_30;
		}
		return true;
		IL_30:
		if (true)
		{
		}
		borderType = this.ᜊ();
		num = 2;
		goto IL_1E;
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x002B967C File Offset: 0x002B867C
	private bool ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_75:
			num = 1;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_38;
		}
		BorderType borderType;
		for (;;)
		{
			IL_26:
			switch (num)
			{
			case 0:
				goto IL_75;
			case 1:
				return false;
			case 2:
				switch (borderType)
				{
				case BorderType.Left:
				case BorderType.Right:
				case BorderType.Vertical:
					return true;
				case BorderType.Top:
				case BorderType.Horizontal:
					return false;
				default:
					num = 0;
					continue;
				}
				break;
			}
			goto IL_38;
		}
		return true;
		IL_38:
		borderType = this.ᜊ();
		num = 2;
		goto IL_26;
	}

	// Token: 0x06002D87 RID: 11655 RVA: 0x002B9724 File Offset: 0x002B8724
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2515()
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
		spr\u2515.ᜈ = new int[]
		{
			3,
			0,
			1
		};
		spr\u2515.ᜉ = new int[]
		{
			1,
			0,
			3
		};
		spr\u2515.ᜊ = new int[]
		{
			1,
			0,
			1
		};
		spr\u2515.ᜋ = new int[]
		{
			5,
			0,
			3,
			0,
			1
		};
		spr\u2515.ᜌ = new int[]
		{
			1,
			0,
			3,
			0,
			5
		};
		spr\u2515.\u170D = new int[]
		{
			1,
			0,
			3,
			0,
			1
		};
	}

	// Token: 0x04002686 RID: 9862
	private spr᠐ ᜀ;

	// Token: 0x04002687 RID: 9863
	private spr᠐ ᜁ;

	// Token: 0x04002688 RID: 9864
	private float ᜂ;

	// Token: 0x04002689 RID: 9865
	private float ᜃ;

	// Token: 0x0400268A RID: 9866
	private float ᜄ;

	// Token: 0x0400268B RID: 9867
	private float ᜅ;

	// Token: 0x0400268C RID: 9868
	private bool ᜆ;

	// Token: 0x0400268D RID: 9869
	private bool ᜇ;

	// Token: 0x0400268E RID: 9870
	private static readonly int[] ᜈ;

	// Token: 0x0400268F RID: 9871
	private static readonly int[] ᜉ;

	// Token: 0x04002690 RID: 9872
	private static readonly int[] ᜊ;

	// Token: 0x04002691 RID: 9873
	private static readonly int[] ᜋ;

	// Token: 0x04002692 RID: 9874
	private static readonly int[] ᜌ;

	// Token: 0x04002693 RID: 9875
	private static readonly int[] \u170D;
}

using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape.Ps.Wrapping;

// Token: 0x02000374 RID: 884
[DefaultMember("Item")]
internal class sprᲨ
{
	// Token: 0x0600318D RID: 12685 RVA: 0x002DE4D4 File Offset: 0x002DD4D4
	internal sprᲨ() : this(new PointF[0])
	{
	}

	// Token: 0x0600318E RID: 12686 RVA: 0x002DE4F0 File Offset: 0x002DD4F0
	internal sprᲨ(RectangleF A_0) : this(new PointF[]
	{
		new PointF(A_0.Left, A_0.Top),
		new PointF(A_0.Right, A_0.Top),
		new PointF(A_0.Right, A_0.Bottom),
		new PointF(A_0.Left, A_0.Bottom)
	})
	{
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x002DE588 File Offset: 0x002DD588
	internal sprᲨ(PointF[] A_0)
	{
		int a_ = 6;
		base..ctor();
		if (A_0 == null)
		{
			throw new NullReferenceException(ClipboardData.b("ᱫŭ᥯ᱱsյ", a_));
		}
		this.ᜀ = new ArrayList(A_0.Length);
		for (int i = 0; i < A_0.Length; i++)
		{
			spr\u2251 a_2 = new spr\u2251(A_0[i]);
			this.ᜁ(a_2);
		}
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x002DE5F8 File Offset: 0x002DD5F8
	internal sprᲨ(PointF[] A_0, bool A_1) : this(A_0)
	{
		this.ᜀ(A_1);
	}

	// Token: 0x06003191 RID: 12689 RVA: 0x002DE614 File Offset: 0x002DD614
	internal void ᜀ(bool A_0)
	{
		if (this.ᜂ() == A_0)
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
		this.ᜀ.Reverse();
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x002DE668 File Offset: 0x002DD668
	internal void ᜀ(ArrayList A_0, bool A_1)
	{
		if (A_1)
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
				this.ᜀ((PointF[])A_0.ToArray(typeof(PointF)));
				return;
			}
		}
		this.ᜀ.AddRange(A_0);
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x002DE6D0 File Offset: 0x002DD6D0
	internal void ᜀ(PointF[] A_0)
	{
		spr\u2251[] array;
		for (;;)
		{
			array = new spr\u2251[A_0.Length];
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num < A_0.Length)
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
							array[num] = new spr\u2251(A_0[num]);
							num++;
							num2 = 3;
							continue;
						}
					}
					num2 = 2;
					continue;
				case 1:
					goto IL_2D;
				case 2:
					goto IL_43;
				case 3:
					goto IL_2D;
				}
				break;
				IL_2D:
				num2 = 0;
			}
		}
		IL_43:
		this.ᜀ.AddRange(array);
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x002DE784 File Offset: 0x002DD784
	internal void ᜀ(int A_0, ArrayList A_1)
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
		this.ᜀ.InsertRange(A_0, A_1);
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x002DE7CC File Offset: 0x002DD7CC
	internal void ᜁ(PointF A_0)
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
		this.ᜁ(new spr\u2251(A_0));
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x002DE814 File Offset: 0x002DD814
	internal void ᜁ(spr\u2251 A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x06003197 RID: 12695 RVA: 0x002DE85C File Offset: 0x002DD85C
	internal void ᜁ(int A_0, spr\u2251 A_1)
	{
		int a_ = 16;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 2:
				if (A_0 >= this.ᜅ())
				{
					goto IL_8A;
				}
				goto IL_94;
			case 3:
				goto IL_49;
			}
			if (true)
			{
			}
			if (0 <= A_0)
			{
				num = 0;
				continue;
			}
			goto IL_49;
			IL_8A:
			num = 3;
			continue;
			IL_49:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8A;
			default:
				goto IL_5F;
			}
		}
		IL_5F:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ήᙷṹ᥻ٽ", a_));
		IL_94:
		this.ᜀ.Insert(A_0, A_1);
	}

	// Token: 0x06003198 RID: 12696 RVA: 0x002DE90C File Offset: 0x002DD90C
	internal void ᜀ(int A_0)
	{
		int a_ = 4;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0 >= this.ᜅ())
				{
					goto IL_8A;
				}
				goto IL_94;
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_37;
			}
			if (0 <= A_0)
			{
				num = 2;
				continue;
			}
			goto IL_37;
			IL_8A:
			num = 3;
			continue;
			IL_37:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8A;
			default:
				goto IL_57;
			}
		}
		IL_57:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ͩɫ੭ᕯੱ", a_));
		IL_94:
		this.ᜀ.RemoveAt(A_0);
	}

	// Token: 0x06003199 RID: 12697 RVA: 0x002DE9BC File Offset: 0x002DD9BC
	internal int ᜀ(spr\u2251 A_0)
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
		return this.ᜀ.IndexOf(A_0);
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x002DEA04 File Offset: 0x002DDA04
	internal ArrayList ᜂ(int A_0)
	{
		ArrayList arrayList;
		for (;;)
		{
			arrayList = new ArrayList(this.ᜅ());
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return arrayList;
				case 1:
					goto IL_30;
				case 2:
					if (num >= this.ᜅ())
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
							num2 = 0;
							continue;
						}
					}
					else
					{
						if (true)
						{
						}
						arrayList.Add(this.ᜃ(this.ᜁ(A_0 + num)));
						num++;
					}
					num2 = 3;
					continue;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
		return arrayList;
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x002DEAB4 File Offset: 0x002DDAB4
	internal int ᜁ(int A_0)
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
		return A_0 % this.ᜅ();
	}

	// Token: 0x0600319C RID: 12700 RVA: 0x002DEAF8 File Offset: 0x002DDAF8
	internal PointF[] ᜀ()
	{
		PointF[] array;
		for (;;)
		{
			array = new PointF[this.ᜀ.Count];
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_3D;
				case 1:
					if (num >= this.ᜀ.Count)
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
							num2 = 2;
							continue;
						}
					}
					else
					{
						PointF pointF = ((spr\u2251)this.ᜀ[num]).ᜁ();
						array[num] = new PointF(pointF.X, pointF.Y);
						num++;
					}
					num2 = 0;
					continue;
				case 2:
					return array;
				case 3:
					if (true)
					{
					}
					goto IL_3D;
				}
				break;
				IL_3D:
				num2 = 1;
			}
		}
		return array;
	}

	// Token: 0x0600319D RID: 12701 RVA: 0x002DEBD4 File Offset: 0x002DDBD4
	internal Point[] ᜃ()
	{
		Point[] array2;
		for (;;)
		{
			PointF[] array = this.ᜀ();
			array2 = new Point[array.Length];
			int num = 0;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return array2;
				case 1:
					goto IL_3C;
				case 2:
					goto IL_3C;
				case 3:
					if (num >= array.Length)
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
							num2 = 0;
							continue;
						}
					}
					else
					{
						array2[num] = spr\u23C4.ᜀ(array[num]);
						num++;
					}
					num2 = 2;
					continue;
				}
				break;
				IL_3C:
				num2 = 3;
			}
		}
		return array2;
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x002DEC8C File Offset: 0x002DDC8C
	internal void ᜀ(spr\u25FD A_0)
	{
		int num = 2;
		for (;;)
		{
			IEnumerator enumerator;
			switch (num)
			{
			case 0:
				if (A_0.ᜈ())
				{
					num = 3;
					continue;
				}
				goto IL_124;
			case 1:
				try
				{
					num = 2;
					for (;;)
					{
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
								num = 1;
								continue;
							}
							break;
						case 1:
							goto IL_DE;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							spr\u2251 spr_u = (spr\u2251)enumerator.Current;
							spr_u.ᜀ(A_0.ᜀ(spr_u.ᜁ()));
							num = 3;
							continue;
						}
						}
						IL_75:
						num = 4;
						continue;
						goto IL_75;
					}
					IL_DE:
					return;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						num = 0;
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
								goto IL_123;
							case 1:
								goto IL_121;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_121:
					IL_123:;
				}
				goto IL_124;
			case 3:
				return;
			case 4:
				num = 0;
				continue;
			}
			if (!spr\u25FD.ᜁ(A_0, null))
			{
				if (true)
				{
				}
				num = 4;
				continue;
			}
			break;
			IL_124:
			enumerator = this.ᜀ.GetEnumerator();
			num = 1;
		}
	}

	// Token: 0x0600319F RID: 12703 RVA: 0x002DEE0C File Offset: 0x002DDE0C
	internal void ᜀ(float A_0, float A_1)
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
		this.ᜀ(new spr\u25FD(A_0, 0f, 0f, A_1, 0f, 0f));
	}

	// Token: 0x060031A0 RID: 12704 RVA: 0x002DEE68 File Offset: 0x002DDE68
	internal bool ᜀ(PointF A_0)
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
		return sprᲨ.ᜀ(this.ᜀ(), A_0);
	}

	// Token: 0x060031A1 RID: 12705 RVA: 0x002DEEB0 File Offset: 0x002DDEB0
	internal void ᜇ()
	{
		IEnumerator enumerator = this.ᜀ.GetEnumerator();
		try
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 1:
					IL_85:
					num = 2;
					continue;
				case 2:
					goto IL_8F;
				case 3:
				{
					if (!enumerator.MoveNext())
					{
						num = 1;
						continue;
					}
					spr\u2251 spr_u = (spr\u2251)enumerator.Current;
					spr_u.ᜀ(VertexType.Simple);
					num = 0;
					continue;
				}
				}
				IL_51:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				goto IL_51;
			}
			IL_8F:;
		}
		finally
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
						goto IL_D1;
					case 1:
						goto IL_CF;
					case 2:
						disposable.Dispose();
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_CF:
			IL_D1:;
		}
		if (true)
		{
		}
	}

	// Token: 0x060031A2 RID: 12706 RVA: 0x002DEFB4 File Offset: 0x002DDFB4
	internal bool ᜀ(VertexType A_0)
	{
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = this.ᜀ.GetEnumerator();
			bool result;
			try
			{
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						result = false;
						num = 2;
						continue;
					case 1:
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E0;
						default:
						{
							if (false)
							{
							}
							spr\u2251 spr_u = (spr\u2251)enumerator.Current;
							num = 5;
							continue;
						}
						}
						break;
					case 2:
						goto IL_D2;
					case 3:
						num = 4;
						continue;
					case 4:
						goto IL_E0;
					case 5:
					{
						spr\u2251 spr_u;
						if (spr_u.ᜃ() != A_0)
						{
							num = 0;
							continue;
						}
						break;
					}
					}
					IL_60:
					num = 1;
					continue;
					goto IL_60;
				}
				IL_D2:
				return result;
				IL_E0:
				goto IL_1C;
			}
			finally
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
							goto IL_129;
						case 1:
							goto IL_127;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_127:
				IL_129:;
			}
			return result;
			IL_1C:
			if (true)
			{
			}
			return true;
		}
		}
	}

	// Token: 0x060031A3 RID: 12707 RVA: 0x002DF0FC File Offset: 0x002DE0FC
	internal static bool ᜀ(PointF[] A_0, PointF A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				int num = 6;
				for (;;)
				{
					PointF pointF;
					PointF pointF2;
					int num2;
					switch (num)
					{
					case 0:
						flag = !flag;
						num = 14;
						continue;
					case 1:
					{
						if (pointF.X > pointF2.X)
						{
							num = 5;
							continue;
						}
						PointF pointF3 = pointF;
						PointF pointF4 = pointF2;
						num = 7;
						continue;
					}
					case 2:
						if (num2 >= A_0.Length)
						{
							num = 11;
							continue;
						}
						pointF = new PointF(A_0[num2].X, A_0[num2].Y);
						num = 1;
						continue;
					case 3:
						num = 8;
						continue;
					case 4:
						goto IL_179;
					case 5:
					{
						PointF pointF3 = pointF2;
						PointF pointF4 = pointF;
						num = 12;
						continue;
					}
					case 6:
						if (A_0.Length < 3)
						{
							num = 9;
							continue;
						}
						pointF2 = new PointF(A_0[A_0.Length - 1].X, A_0[A_0.Length - 1].Y);
						num2 = 0;
						goto IL_E8;
					case 7:
						goto IL_82;
					case 8:
					{
						PointF pointF3;
						PointF pointF4;
						if ((A_1.Y - pointF3.Y) * (pointF4.X - pointF3.X) < (pointF4.Y - pointF3.Y) * (A_1.X - pointF3.X))
						{
							num = 0;
							continue;
						}
						goto IL_F9;
					}
					case 9:
						return false;
					case 10:
						if (pointF.X < A_1.X == A_1.X <= pointF2.X)
						{
							num = 3;
							continue;
						}
						goto IL_F9;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E8;
						default:
							goto IL_22A;
						}
						break;
					case 12:
						goto IL_82;
					case 13:
						goto IL_179;
					case 14:
						goto IL_F9;
					}
					break;
					IL_82:
					num = 10;
					continue;
					IL_E8:
					num = 13;
					continue;
					IL_F9:
					pointF2 = pointF;
					num2++;
					num = 4;
					continue;
					IL_179:
					num = 2;
				}
			}
			return false;
			IL_22A:
			if (false)
			{
			}
			if (true)
			{
			}
			return flag;
		}
		}
	}

	// Token: 0x060031A4 RID: 12708 RVA: 0x002DF344 File Offset: 0x002DE344
	internal sprᲨ ᜁ()
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
		return new sprᲨ(this.ᜀ());
	}

	// Token: 0x060031A5 RID: 12709 RVA: 0x002DF38C File Offset: 0x002DE38C
	internal int ᜅ()
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
		return this.ᜀ.Count;
	}

	// Token: 0x060031A6 RID: 12710 RVA: 0x002DF3D4 File Offset: 0x002DE3D4
	internal spr\u2251 ᜃ(int A_0)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 2:
				if (A_0 >= this.ᜅ())
				{
					goto IL_82;
				}
				goto IL_94;
			case 3:
				goto IL_37;
			}
			if (0 <= A_0)
			{
				num = 0;
				continue;
			}
			goto IL_37;
			IL_82:
			if (true)
			{
			}
			num = 3;
			continue;
			IL_37:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_82;
			default:
				goto IL_57;
			}
		}
		IL_57:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ṷ᝸ὺ᡼ݾ", a_));
		IL_94:
		return (spr\u2251)this.ᜀ[A_0];
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x002DF488 File Offset: 0x002DE488
	internal void ᜀ(int A_0, spr\u2251 A_1)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				if (A_0 >= this.ᜅ())
				{
					goto IL_8A;
				}
				goto IL_94;
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_49;
			}
			if (0 <= A_0)
			{
				num = 2;
				continue;
			}
			goto IL_49;
			IL_8A:
			num = 3;
			continue;
			IL_49:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8A;
			default:
				goto IL_5F;
			}
		}
		IL_5F:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("᭱ᩳትᵷɹ", a_));
		IL_94:
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x002DF538 File Offset: 0x002DE538
	internal bool ᜂ()
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
		return sprὍ.ᜀ(this.ᜀ());
	}

	// Token: 0x060031A9 RID: 12713 RVA: 0x002DF580 File Offset: 0x002DE580
	internal int ᜈ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = 0;
				float num = float.MaxValue;
				float num2 = float.MaxValue;
				int num3 = 0;
				int num4 = 4;
				for (;;)
				{
					PointF pointF;
					switch (num4)
					{
					case 0:
						if (pointF.X == num)
						{
							num4 = 1;
							continue;
						}
						goto IL_5E;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							num4 = 8;
							continue;
						}
						break;
					case 2:
						if (pointF.X >= num)
						{
							num4 = 5;
							continue;
						}
						goto IL_136;
					case 3:
						if (true)
						{
						}
						goto IL_5E;
					case 4:
						goto IL_C3;
					case 5:
						num4 = 0;
						continue;
					case 6:
						if (num3 >= this.ᜅ())
						{
							goto IL_D8;
						}
						pointF = this.ᜃ(num3).ᜁ();
						num4 = 2;
						continue;
					case 7:
						return result;
					case 8:
						if (pointF.Y < num2)
						{
							num4 = 10;
							continue;
						}
						goto IL_5E;
					case 9:
						goto IL_C3;
					case 10:
						goto IL_136;
					}
					break;
					IL_5E:
					num3++;
					num4 = 9;
					continue;
					IL_C3:
					num4 = 6;
					continue;
					IL_D8:
					num4 = 7;
					continue;
					IL_136:
					num = pointF.X;
					num2 = pointF.Y;
					result = num3;
					num4 = 3;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x002DF6F0 File Offset: 0x002DE6F0
	internal int ᜆ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = 0;
				float num = float.MinValue;
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					PointF pointF;
					switch (num3)
					{
					case 0:
						goto IL_A4;
					case 1:
						goto IL_48;
					case 2:
						if (pointF.X > num)
						{
							num3 = 6;
							continue;
						}
						goto IL_48;
					case 3:
						if (num2 >= this.ᜅ())
						{
							num3 = 4;
							continue;
						}
						goto IL_76;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
							goto IL_DB;
						}
						break;
					case 5:
						goto IL_A4;
					case 6:
						num = pointF.X;
						result = num2;
						num3 = 1;
						continue;
					}
					break;
					IL_48:
					num2++;
					num3 = 5;
					continue;
					IL_76:
					pointF = this.ᜃ(num2).ᜁ();
					num3 = 2;
					continue;
					IL_A4:
					num3 = 3;
				}
			}
			IL_DB:
			if (true)
			{
			}
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x060031AB RID: 12715 RVA: 0x002DF7EC File Offset: 0x002DE7EC
	internal int ᜄ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = 0;
				float num = float.MaxValue;
				int num2 = 0;
				int num3 = 3;
				for (;;)
				{
					PointF pointF;
					switch (num3)
					{
					case 0:
						goto IL_48;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7E;
						default:
							goto IL_E6;
						}
						break;
					case 2:
						if (pointF.Y < num)
						{
							num3 = 6;
							continue;
						}
						goto IL_48;
					case 3:
						goto IL_AF;
					case 4:
						goto IL_AF;
					case 5:
						if (num2 >= this.ᜅ())
						{
							num3 = 1;
							continue;
						}
						goto IL_7E;
					case 6:
						num = pointF.Y;
						result = num2;
						num3 = 0;
						continue;
					}
					break;
					IL_48:
					if (true)
					{
					}
					num2++;
					num3 = 4;
					continue;
					IL_7E:
					pointF = this.ᜃ(num2).ᜁ();
					num3 = 2;
					continue;
					IL_AF:
					num3 = 5;
				}
			}
			IL_E6:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x002DF8E8 File Offset: 0x002DE8E8
	internal int ᜉ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = 0;
				float num = float.MinValue;
				int num2 = 0;
				int num3 = 4;
				for (;;)
				{
					PointF pointF;
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
							goto IL_E3;
						}
						break;
					case 1:
						goto IL_A4;
					case 2:
						if (true)
						{
						}
						if (num2 >= this.ᜅ())
						{
							num3 = 0;
							continue;
						}
						goto IL_76;
					case 3:
						if (pointF.Y > num)
						{
							num3 = 5;
							continue;
						}
						goto IL_48;
					case 4:
						goto IL_A4;
					case 5:
						num = pointF.Y;
						result = num2;
						num3 = 6;
						continue;
					case 6:
						goto IL_48;
					}
					break;
					IL_48:
					num2++;
					num3 = 1;
					continue;
					IL_76:
					pointF = this.ᜃ(num2).ᜁ();
					num3 = 3;
					continue;
					IL_A4:
					num3 = 2;
				}
			}
			IL_E3:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x04002719 RID: 10009
	private readonly ArrayList ᜀ;
}

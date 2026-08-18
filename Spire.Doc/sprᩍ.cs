using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Fields.Shape;
using Spire.Doc.Interface;

// Token: 0x0200018C RID: 396
internal class sprᩍ : ParagraphBase, sprᰎ, ICompositeObject
{
	// Token: 0x06000DEF RID: 3567 RVA: 0x000E5EEC File Offset: 0x000E4EEC
	internal sprṾ ᝪ()
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
		return new sprṾ(this);
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x000E5F30 File Offset: 0x000E4F30
	internal sprᩍ(Document A_0) : base(A_0)
	{
		this.ᜂ = new spr\u261A(A_0);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000E5F5C File Offset: 0x000E4F5C
	internal Image ᝩ()
	{
		int num = 2;
		for (;;)
		{
			MemoryStream memoryStream;
			switch (num)
			{
			case 0:
				goto IL_B3;
			case 1:
				try
				{
					this.ᝪ().ᜀ(memoryStream);
					this.ᜃ = Image.FromStream(memoryStream);
					goto IL_C9;
				}
				finally
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_A8:
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
							goto IL_B0;
						case 2:
							goto IL_A0;
						}
						if (memoryStream == null)
						{
							goto IL_B2;
						}
						num = 2;
					}
					IL_A0:
					((IDisposable)memoryStream).Dispose();
					goto IL_A8;
					IL_B0:
					IL_B2:;
				}
				goto IL_B3;
			}
			if (true)
			{
			}
			if (this.ᜃ == null)
			{
				num = 0;
				continue;
			}
			break;
			IL_B3:
			memoryStream = new MemoryStream();
			num = 1;
		}
		IL_C9:
		return this.ᜃ;
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x000E6048 File Offset: 0x000E5048
	internal new void ᜀ(Graphics A_0)
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
		spr\u23A8 spr_u23A = new spr\u23A8();
		spr\u1F9B a_ = new spr\u1F9B(this);
		spr\u24A6 a_2 = spr\u241F.ᜀ(a_, null);
		spr_u23A.ᜀ(a_2, A_0);
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x000E60A0 File Offset: 0x000E50A0
	internal new void ᜀ(Graphics A_0, PointF A_1)
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
		spr\u23A8 spr_u23A = new spr\u23A8();
		spr\u1F9B a_ = new spr\u1F9B(this);
		spr\u24A6 a_2 = spr\u241F.ᜀ(a_, null);
		spr_u23A.ᜀ(a_2, A_0, A_1);
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x000E60FC File Offset: 0x000E50FC
	internal new void ᜀ(spr\u2403 A_0)
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
		this.ᜂ.ᜀ(A_0);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x000E6144 File Offset: 0x000E5144
	internal spr\u2403 \u1778()
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
		return this.ᜂ.ᜀ();
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x000E618C File Offset: 0x000E518C
	internal new void ᜁ(spr\u2459 A_0)
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
		this.ᜂ.ᜀ(A_0.ᜌ().ᜅ(), this);
		this.ᜂ.ᜀ(A_0.\u1713().ᜆ(), this);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x000E61F8 File Offset: 0x000E51F8
	internal new void ᜀ(spr\u24D8 A_0)
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
		this.ᜂ.ᜀ(A_0, this);
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000E6240 File Offset: 0x000E5240
	public virtual DocumentObjectType ᜁ()
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
		return DocumentObjectType.Shape;
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x000E6280 File Offset: 0x000E5280
	protected virtual void ᜂ()
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
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x000E62BC File Offset: 0x000E52BC
	internal bool ᝠ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_43;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				goto IL_129;
			}
			if (this.DocumentObjectType == DocumentObjectType.Shape)
			{
				num = 0;
			}
			else
			{
				List<spr\u1937>.Enumerator enumerator = this.ᜀ(DocumentObjectType.Shape).GetEnumerator();
				num = 2;
			}
		}
		IL_43:
		IL_FF:
		return this.ᝰ().Count > 0;
		IL_129:
		try
		{
			num = 0;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_7E;
				case 2:
				{
					spr\u1937 spr_u;
					if (spr_u.ᝰ().Count > 0)
					{
						num = 4;
						continue;
					}
					break;
				}
				case 3:
					goto IL_EF;
				case 4:
					result = true;
					num = 5;
					continue;
				case 5:
					goto IL_9B;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				IL_76:
				num = 1;
				continue;
				goto IL_76;
				IL_7E:
				List<spr\u1937>.Enumerator enumerator;
				if (!enumerator.MoveNext())
				{
					num = 6;
				}
				else
				{
					spr\u1937 spr_u = enumerator.Current;
					num = 2;
				}
			}
			IL_9B:
			return result;
			IL_EF:
			return false;
		}
		finally
		{
			List<spr\u1937>.Enumerator enumerator;
			((IDisposable)enumerator).Dispose();
		}
		goto IL_FF;
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x000E6408 File Offset: 0x000E5408
	private new List<spr\u1937> ᜀ(DocumentObjectType A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			List<spr\u1937> list = new List<spr\u1937>();
			IEnumerator enumerator = this.ᝰ().GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						DocumentObject documentObject = (DocumentObject)enumerator.Current;
						num = 6;
						continue;
					}
					case 2:
						num = 4;
						continue;
					case 3:
					{
						DocumentObject documentObject;
						list.Add((spr\u1937)documentObject);
						num = 5;
						continue;
					}
					case 4:
						goto IL_CC;
					case 6:
					{
						DocumentObject documentObject;
						if (documentObject.DocumentObjectType == A_0)
						{
							num = 3;
							continue;
						}
						break;
					}
					}
					IL_8D:
					num = 1;
					continue;
					goto IL_8D;
				}
				IL_CC:;
			}
			finally
			{
				int num;
				IDisposable disposable;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_11D:
					disposable.Dispose();
					num = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_FF;
				}
				for (;;)
				{
					IL_EC:
					switch (num)
					{
					case 0:
						goto IL_12C;
					case 1:
						goto IL_11B;
					case 2:
						if (disposable != null)
						{
							num = 1;
							continue;
						}
						goto IL_12E;
					}
					goto IL_FF;
				}
				IL_11B:
				goto IL_11D;
				IL_12C:
				IL_12E:
				goto EndFinally_6;
				IL_FF:
				disposable = (enumerator as IDisposable);
				num = 2;
				goto IL_EC;
				EndFinally_6:;
			}
			return list;
		}
		}
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x000E6558 File Offset: 0x000E5558
	internal new void ᜀ(Spire.Doc.Fields.Shape.ShapeType A_0)
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
		this.ᜀ(4155, A_0);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x000E65A4 File Offset: 0x000E55A4
	[EditorBrowsable(EditorBrowsableState.Never)]
	public object ᜊ(int A_0)
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
		return this.ᜅ.ᜁ(A_0);
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x000E65EC File Offset: 0x000E55EC
	[EditorBrowsable(EditorBrowsableState.Never)]
	public object ᜄ(int A_0)
	{
		spr\u2588 spr_u = sprᢴ.ᜀ(this.\u1774());
		if (spr_u != null)
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
				return spr_u.ᜃ(A_0);
			}
		}
		return spr\u2588.ᜀ(A_0);
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x000E6648 File Offset: 0x000E5648
	[EditorBrowsable(EditorBrowsableState.Never)]
	public object ᜈ(int A_0)
	{
		if (true)
		{
		}
		object obj = this.ᜊ(A_0);
		if (obj == null)
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
				return this.ᜄ(A_0);
			}
		}
		return obj;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x000E669C File Offset: 0x000E569C
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void ᜀ(int A_0, object A_1)
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
		this.ᜅ.ᜁ(A_0, A_1);
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x000E66E4 File Offset: 0x000E56E4
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void ᜋ(int A_0)
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
		this.ᜅ.Remove(A_0);
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x000E672C File Offset: 0x000E572C
	internal new void ᜀ(double A_0, double A_1)
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
		this.ᜅ(A_0);
		this.ᜇ(A_1);
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x000E6778 File Offset: 0x000E5778
	internal void ᜅ(double A_0)
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
		this.ᜁ(A_0, false);
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x000E67BC File Offset: 0x000E57BC
	private new void ᜁ(double A_0, bool A_1)
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ(4131, this.ᜀ(A_0, A_1, ClipboardData.b("ᡮᡰᝲŴὶ", a_)));
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x000E6828 File Offset: 0x000E5828
	internal void ᜇ(double A_0)
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
		this.ᜀ(A_0, false);
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x000E686C File Offset: 0x000E586C
	private new void ᜀ(double A_0, bool A_1)
	{
		int a_ = 5;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ(4132, this.ᜀ(A_0, A_1, ClipboardData.b("ͪ࡬ٮᙰ᭲Ŵ", a_)));
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x000E68D8 File Offset: 0x000E58D8
	internal void \u1777()
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
		this.ᜅ(this.\u177D());
		this.ᜇ(this.ន());
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x000E692C File Offset: 0x000E592C
	private new double ᜀ(double A_0, bool A_1, string A_2)
	{
		int a_ = 5;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1)
				{
					num = 1;
					continue;
				}
				goto IL_13D;
			case 1:
				goto IL_85;
			case 2:
				if (base.ParentObject == null)
				{
					num = 6;
					continue;
				}
				num = 0;
				continue;
			case 3:
				num = 5;
				continue;
			case 4:
				goto IL_136;
			case 5:
				if (!A_1)
				{
					goto IL_163;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 6:
				return A_0;
			case 7:
				if (this.ᝏ())
				{
					num = 8;
					continue;
				}
				return A_0;
			case 8:
				if (true)
				{
				}
				num = 2;
				continue;
			case 9:
				if (A_0 > sprᩍ.ᜆ)
				{
					num = 11;
					continue;
				}
				return A_0;
			case 11:
				num = 7;
				continue;
			}
			if (A_0 < 0.0)
			{
				num = 3;
			}
			else
			{
				num = 9;
			}
		}
		IL_85:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᵪ౬ͮѰᙲ", a_), string.Format(ClipboardData.b("㡪լ๮Űᙲ啴౶䥸ٺ嵼᱾ﶈꮊ놐膠힢춤욦잨讪횬麮첰鎲어\ud8b6킸햺즼첾", a_), A_2, sprᜌ.ᜃ(sprᩍ.ᜆ)));
		IL_136:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᵪ౬ͮѰᙲ", a_), string.Format(ClipboardData.b("㡪լ๮Űᙲ啴౶䥸ٺ嵼᱾ﶈꮊ놐ﾒ뮚삠춢薤鞦螨", a_), A_2));
		IL_13D:
		return sprᩍ.ᜇ;
		IL_163:
		return 0.0;
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x000E6AD4 File Offset: 0x000E5AD4
	public PointF ᜂ(PointF A_0)
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
		A_0 = new PointF(A_0.X - (float)this.\u1779().X, A_0.Y - (float)this.\u1779().Y);
		A_0 = new PointF((float)((double)A_0.X * (this.\u177D() / (double)this.ព().Width)), (float)((double)A_0.Y * (this.ន() / (double)this.ព().Height)));
		A_0 = new PointF(A_0.X + (float)this.\u177A(), A_0.Y + (float)this.ᝣ());
		return A_0;
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x000E6BB4 File Offset: 0x000E5BB4
	internal bool \u175D()
	{
		spr\u2055[] array;
		for (;;)
		{
			array = (spr\u2055[])this.\u175E().ᜁ(325);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1774() == Spire.Doc.Fields.Shape.ShapeType.NonPrimitive)
					{
						num = 6;
						continue;
					}
					goto IL_D0;
				case 1:
					num = 4;
					continue;
				case 2:
					if (array != null)
					{
						num = 7;
						continue;
					}
					goto IL_D0;
				case 3:
					num = 2;
					continue;
				case 4:
					if (spr\u2109.ᜀ(this.ន()))
					{
						num = 3;
						continue;
					}
					goto IL_D0;
				case 5:
					if (spr\u2109.ᜀ(this.\u177D()))
					{
						num = 1;
						continue;
					}
					goto IL_D0;
				case 6:
					if (true)
					{
					}
					num = 5;
					continue;
				case 7:
					goto IL_76;
				}
				break;
			}
		}
		IL_76:
		IL_C9:
		return array.Length > 0;
		IL_D0:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_C9;
		default:
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x000E6CB0 File Offset: 0x000E5CB0
	internal void គ()
	{
		switch (0)
		{
		default:
		{
			spr\u2055[] array;
			for (;;)
			{
				array = (spr\u2055[])this.\u175E().ᜁ(325);
				PointF[] array2 = sprᩍ.ᜀ(array);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (array2 == null)
						{
							num = 0;
							continue;
						}
						RectangleF rectangleF = sprὍ.ᜁ(array2);
						this.ᜊ(spr\u23C4.ᜊ((double)rectangleF.Width));
						this.ᜄ(spr\u23C4.ᜊ((double)rectangleF.Height));
						this.ᜋ(spr\u23C4.ᜊ((double)rectangleF.X));
						this.ᜂ(spr\u23C4.ᜊ((double)rectangleF.Y));
						int num2 = 0;
						num = 2;
						continue;
					}
					case 2:
						goto IL_11A;
					case 3:
						goto IL_143;
					case 4:
						goto IL_11A;
					case 5:
					{
						if (true)
						{
						}
						int num2;
						if (num2 >= array.Length)
						{
							num = 3;
							continue;
						}
						RectangleF rectangleF;
						array[num2] = new spr\u2055(array[num2].ᜂ().ᜂ() - (int)rectangleF.X, array[num2].ᜁ().ᜂ() - (int)rectangleF.Y);
						num2++;
						num = 4;
						continue;
					}
					}
					break;
					IL_11A:
					num = 5;
				}
			}
			return;
			IL_143:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			this.ᜀ(325, array);
			return;
		}
		}
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x000E6E2C File Offset: 0x000E5E2C
	internal void រ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (this.\u175E().Contains(917))
				{
					num = 3;
					continue;
				}
				goto IL_8A;
			case 2:
				num = 1;
				continue;
			case 3:
				this.ᜇ(spr\u23C4.ᜑ((int)this.ᜊ(917)));
				this.ᜋ(917);
				num = 4;
				continue;
			case 4:
				goto IL_64;
			}
			if (!this.ᝫ())
			{
				break;
			}
			num = 2;
		}
		IL_64:
		IL_8A:
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_64;
		}
		if (false)
		{
		}
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x000E6EF4 File Offset: 0x000E5EF4
	private new static PointF[] ᜀ(spr\u2055[] A_0)
	{
		int num = 2;
		PointF[] array;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (num2 >= A_0.Length)
				{
					num = 3;
					continue;
				}
				spr\u2055 spr_u = A_0[num2];
				array[num2] = new PointF((float)spr_u.ᜂ().ᜂ(), (float)spr_u.ᜁ().ᜂ());
				num2++;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C5;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			}
			case 1:
				num = 4;
				continue;
			case 3:
				return array;
			case 4:
				goto IL_C5;
			case 5:
			{
				array = new PointF[A_0.Length];
				int num2 = 0;
				num = 7;
				continue;
			}
			case 6:
				goto IL_48;
			case 7:
				goto IL_48;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			goto IL_FF;
			IL_48:
			num = 0;
			continue;
			IL_C5:
			if (A_0.Length <= 0)
			{
				goto IL_FF;
			}
			num = 5;
		}
		return array;
		IL_FF:
		return null;
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x000E7004 File Offset: 0x000E6004
	internal int \u175A()
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
		return (int)this.ᜈ(4124);
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x000E7050 File Offset: 0x000E6050
	internal void \u170D(int A_0)
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
		this.ᜀ(4124, A_0);
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x000E709C File Offset: 0x000E609C
	internal Spire.Doc.Fields.Shape.ShapeType \u1774()
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
		return (Spire.Doc.Fields.Shape.ShapeType)this.ᜅ.ᜃ(4155);
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x000E70EC File Offset: 0x000E60EC
	public bool ណ()
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
		return this.\u1774() == Spire.Doc.Fields.Shape.ShapeType.Group;
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x000E7130 File Offset: 0x000E6130
	public bool ឃ()
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
		return this.\u1774() == Spire.Doc.Fields.Shape.ShapeType.Image;
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x000E7178 File Offset: 0x000E6178
	internal bool \u1758()
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
		return this.\u1774() == Spire.Doc.Fields.Shape.ShapeType.OleObject;
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x000E71C0 File Offset: 0x000E61C0
	internal bool ឌ()
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
		return this.\u1774() == Spire.Doc.Fields.Shape.ShapeType.OleControl;
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x000E7208 File Offset: 0x000E6208
	internal bool ត()
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
			if (this.\u1758())
			{
				return true;
			}
			break;
		}
		return this.ឌ();
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x000E7258 File Offset: 0x000E6258
	public bool ᝫ()
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
		return (bool)this.ᜈ(948);
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x000E72A4 File Offset: 0x000E62A4
	internal bool ប()
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
		return (bool)this.ᜈ(946);
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000E72F0 File Offset: 0x000E62F0
	public bool ᝉ()
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
		return (bool)this.ᜈ(241);
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x000E733C File Offset: 0x000E633C
	public bool ᝥ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Spire.Doc.Fields.Shape.ShapeType shapeType = this.\u1774();
		if (shapeType != Spire.Doc.Fields.Shape.ShapeType.Group)
		{
			return true;
		}
		return false;
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x000E7388 File Offset: 0x000E6388
	internal bool ឞ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
				goto IL_67;
			case 2:
				if (true)
				{
				}
				num = 6;
				continue;
			case 4:
				if (!this.ត())
				{
					num = 0;
					continue;
				}
				return false;
			case 5:
				num = 4;
				continue;
			case 6:
				if (!this.ᝉ())
				{
					num = 1;
					continue;
				}
				return false;
			case 7:
				if (!this.ᝫ())
				{
					num = 2;
					continue;
				}
				return false;
			}
			if (this.ឃ())
			{
				return false;
			}
			num = 5;
		}
		IL_67:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
			if (false)
			{
			}
			return !this.ᜀ();
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x000E7468 File Offset: 0x000E6468
	private new bool ᜀ()
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
		return this.\u1774() == Spire.Doc.Fields.Shape.ShapeType.CustomShape;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x000E74B0 File Offset: 0x000E64B0
	internal bool \u1752()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_38;
		}
		if (false)
		{
		}
		if (true)
		{
		}
		if (!this.វ())
		{
			return false;
		}
		IL_38:
		return this.ឞ();
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x000E7500 File Offset: 0x000E6500
	internal bool ᝧ()
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
		return (bool)this.ᜈ(4123);
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x000E754C File Offset: 0x000E654C
	internal void \u170D(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_60;
				}
				break;
			case 2:
				this.ᜀ(4123, true);
				num = 0;
				continue;
			}
			if (!A_0)
			{
				return;
			}
			if (true)
			{
			}
			num = 2;
		}
		IL_60:
		if (false)
		{
		}
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x000E75CC File Offset: 0x000E65CC
	internal bool \u175F()
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
		return (bool)this.ᜈ(945);
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x000E7618 File Offset: 0x000E6618
	internal void ᜎ(bool A_0)
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
		this.ᜀ(945, A_0);
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x000E7664 File Offset: 0x000E6664
	public double \u177A()
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
		return (double)this.ᜈ(4129);
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x000E76B0 File Offset: 0x000E66B0
	public void ᜋ(double A_0)
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
		this.ᜀ(4129, A_0);
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x000E76FC File Offset: 0x000E66FC
	public double ᝣ()
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
		return (double)this.ᜈ(4130);
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x000E7748 File Offset: 0x000E6748
	public void ᜂ(double A_0)
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
		this.ᜀ(4130, A_0);
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x000E7794 File Offset: 0x000E6794
	public double យ()
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
		return this.\u177A() + this.\u177D();
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x000E77DC File Offset: 0x000E67DC
	public double \u177E()
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
		return this.ᝣ() + this.ន();
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x000E7824 File Offset: 0x000E6824
	public double \u177D()
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
		return (double)this.ᜈ(4131);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x000E7870 File Offset: 0x000E6870
	public void ᜊ(double A_0)
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
		this.ᜁ(A_0, true);
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x000E78B4 File Offset: 0x000E68B4
	public double ន()
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
		return (double)this.ᜈ(4132);
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x000E7900 File Offset: 0x000E6900
	public void ᜄ(double A_0)
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
		this.ᜀ(A_0, true);
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x000E7944 File Offset: 0x000E6944
	public RectangleF \u1754()
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
		return new RectangleF((float)this.\u177A(), (float)this.ᝣ(), (float)this.\u177D(), (float)this.ន());
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x000E79A0 File Offset: 0x000E69A0
	public new void ᜁ(RectangleF A_0)
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
		this.ᜋ((double)A_0.Left);
		this.ᜂ((double)A_0.Top);
		this.ᜊ((double)A_0.Width);
		this.ᜄ((double)A_0.Height);
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x000E7A14 File Offset: 0x000E6A14
	public RectangleF ᝎ()
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
		return this.ᜀ(this.\u1754());
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x000E7A5C File Offset: 0x000E6A5C
	public SizeF \u1753()
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
		return this.ᝎ().Size;
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x000E7AA8 File Offset: 0x000E6AA8
	internal SizeF ᝡ()
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
		return new SizeF((float)this.\u177D(), (float)this.ន());
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x000E7AF8 File Offset: 0x000E6AF8
	internal double ᝮ()
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
		return this.\u177D() / 2.0;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x000E7B44 File Offset: 0x000E6B44
	internal double ᝨ()
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
		return this.ន() / 2.0;
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x000E7B90 File Offset: 0x000E6B90
	internal object ល()
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
		return this.ᜊ(1986);
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x000E7BD8 File Offset: 0x000E6BD8
	internal object ឣ()
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
		return this.ᜊ(1987);
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x000E7C20 File Offset: 0x000E6C20
	internal object \u176D()
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
		return this.ᜊ(1984);
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x000E7C68 File Offset: 0x000E6C68
	internal object \u175B()
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
		return this.ᜊ(1985);
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x000E7CB0 File Offset: 0x000E6CB0
	internal RelativeWidth ង()
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
		return (RelativeWidth)this.ᜈ(1988);
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x000E7CFC File Offset: 0x000E6CFC
	internal RelativeHeight \u1756()
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
		return (RelativeHeight)this.ᜈ(1989);
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x000E7D48 File Offset: 0x000E6D48
	internal void \u1773()
	{
		if (true)
		{
		}
		int num = 2;
		Section section;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				return;
			case 3:
				if (section == null)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				}
				goto Block_3;
			}
			if (!this.ᝏ())
			{
				num = 1;
				continue;
			}
			section = (base.GetAncestor(DocumentObjectType.Section) as Section);
			IL_48:
			num = 3;
		}
		return;
		Block_3:
		if (false)
		{
		}
		PageSetup pageSetup = section.PageSetup;
		this.ᜃ(pageSetup);
		this.ᜂ(pageSetup);
		this.ᜁ(pageSetup);
		this.ᜀ(pageSetup);
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x000E7DFC File Offset: 0x000E6DFC
	internal new RectangleF ᜀ(RectangleF A_0)
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
		PointF pointF = this.ᜁ(A_0.Location);
		PointF pointF2 = this.ᜁ(new PointF(A_0.Right, A_0.Bottom));
		return new RectangleF(pointF.X, pointF.Y, pointF2.X - pointF.X, pointF2.Y - pointF.Y);
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000E7E90 File Offset: 0x000E6E90
	internal new PointF ᜁ(PointF A_0)
	{
		for (;;)
		{
			DocumentObject documentObject = base.ParentObject;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_67;
				case 1:
					if (!(documentObject is sprᩍ))
					{
						num = 0;
						continue;
					}
					A_0 = ((sprᩍ)documentObject).ᜂ(A_0);
					documentObject = documentObject.Owner;
					num = 2;
					continue;
				case 2:
					IL_89:
					goto IL_29;
				case 3:
					goto IL_29;
				}
				break;
				IL_29:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_89;
				}
				if (false)
				{
				}
				num = 1;
			}
		}
		IL_67:
		if (true)
		{
		}
		return A_0;
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x000E7F34 File Offset: 0x000E6F34
	private void ᜃ(PageSetup A_0)
	{
		switch (0)
		{
		default:
		{
			double num2;
			for (;;)
			{
				object obj = this.ល();
				int num = 4;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						return;
					case 2:
						goto IL_111;
					case 3:
					{
						RelativeHorizontalPosition relativeHorizontalPosition;
						switch (relativeHorizontalPosition)
						{
						case RelativeHorizontalPosition.Margin:
							goto IL_6C;
						case RelativeHorizontalPosition.Page:
							goto IL_54;
						case RelativeHorizontalPosition.Column:
						case RelativeHorizontalPosition.Character:
							return;
						case RelativeHorizontalPosition.LeftMargin:
						case RelativeHorizontalPosition.OutsideMargin:
							goto IL_114;
						case RelativeHorizontalPosition.RightMargin:
						case RelativeHorizontalPosition.InsideMargin:
							this.ᜋ((double)A_0.Margins.Right * num2);
							num = 2;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					}
					case 4:
					{
						if (obj == null)
						{
							num = 0;
							continue;
						}
						num2 = (double)((int)obj) / 1000.0;
						RelativeHorizontalPosition relativeHorizontalPosition = this.ថ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					}
					break;
				}
			}
			return;
			IL_54:
			this.ᜋ((double)A_0.PageSize.Width * num2);
			return;
			IL_6C:
			this.ᜋ((double)A_0.ClientWidth * num2);
			return;
			IL_111:
			return;
			IL_114:
			this.ᜋ((double)A_0.Margins.Left * num2);
			return;
		}
		}
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x000E8078 File Offset: 0x000E7078
	private void ᜂ(PageSetup A_0)
	{
		switch (0)
		{
		default:
		{
			double num2;
			for (;;)
			{
				object obj = this.ឣ();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						RelativeVerticalPosition relativeVerticalPosition;
						switch (relativeVerticalPosition)
						{
						case RelativeVerticalPosition.Margin:
							goto IL_6C;
						case RelativeVerticalPosition.Page:
							goto IL_54;
						case RelativeVerticalPosition.Paragraph:
						case RelativeVerticalPosition.Line:
							return;
						case RelativeVerticalPosition.TopMargin:
						case RelativeVerticalPosition.InsideMargin:
						case RelativeVerticalPosition.OutsideMargin:
							goto IL_114;
						case RelativeVerticalPosition.BottomMargin:
							this.ᜂ((double)A_0.Margins.Bottom * num2);
							num = 3;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					}
					case 1:
						return;
					case 2:
						return;
					case 3:
						goto IL_111;
					case 4:
					{
						if (true)
						{
						}
						if (obj == null)
						{
							num = 2;
							continue;
						}
						num2 = (double)((int)obj) / 1000.0;
						RelativeVerticalPosition relativeVerticalPosition = this.ធ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
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
					break;
				}
			}
			return;
			IL_54:
			this.ᜂ((double)A_0.PageSize.Height * num2);
			return;
			IL_6C:
			this.ᜂ((double)A_0.ClientWidth * num2);
			return;
			IL_111:
			return;
			IL_114:
			this.ᜂ((double)A_0.Margins.Top * num2);
			return;
		}
		}
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x000E81BC File Offset: 0x000E71BC
	private new void ᜁ(PageSetup A_0)
	{
		switch (0)
		{
		default:
		{
			object obj;
			double num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8C:
				num = (double)((int)obj) / 1000.0;
				num2 = 5;
				break;
			case 1:
				goto IL_2E;
			default:
				goto IL_2E;
			}
			for (;;)
			{
				IL_36:
				switch (num2)
				{
				case 0:
					if (obj == null)
					{
						num2 = 2;
						continue;
					}
					goto IL_8C;
				case 1:
					goto IL_12C;
				case 2:
					return;
				case 3:
					return;
				case 4:
				{
					RelativeWidth relativeWidth;
					switch (relativeWidth)
					{
					case RelativeWidth.Margin:
						goto IL_73;
					case RelativeWidth.Page:
						goto IL_D7;
					case RelativeWidth.LeftMargin:
					case RelativeWidth.OutsideMargin:
						goto IL_C1;
					case RelativeWidth.RightMargin:
					case RelativeWidth.InsideMargin:
						goto IL_132;
					default:
						num2 = 1;
						continue;
					}
					break;
				}
				case 5:
				{
					if (num <= 0.0)
					{
						num2 = 3;
						continue;
					}
					RelativeWidth relativeWidth = this.ង();
					num2 = 4;
					continue;
				}
				}
				goto IL_55;
			}
			return;
			IL_73:
			this.ᜊ((double)A_0.ClientWidth * num);
			return;
			IL_C1:
			this.ᜊ((double)A_0.Margins.Left * num);
			return;
			IL_D7:
			this.ᜊ((double)A_0.PageSize.Width * num);
			return;
			IL_12C:
			if (true)
			{
			}
			return;
			IL_132:
			this.ᜊ((double)A_0.Margins.Right * num);
			return;
			IL_2E:
			if (false)
			{
			}
			IL_55:
			obj = this.\u176D();
			num2 = 0;
			goto IL_36;
		}
		}
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x000E8310 File Offset: 0x000E7310
	private new void ᜀ(PageSetup A_0)
	{
		double num2;
		for (;;)
		{
			object obj = this.\u175B();
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					return;
				case 2:
				{
					RelativeHeight relativeHeight;
					switch (relativeHeight)
					{
					case RelativeHeight.Margin:
						goto IL_47;
					case RelativeHeight.Page:
						goto IL_CA;
					case RelativeHeight.TopMargin:
					case RelativeHeight.InsideMargin:
					case RelativeHeight.OutsideMargin:
						goto IL_AA;
					case RelativeHeight.BottomMargin:
						goto IL_11B;
					default:
						num = 0;
						continue;
					}
					break;
				}
				case 3:
					goto IL_3A;
				case 4:
				{
					if (num2 <= 0.0)
					{
						num = 1;
						continue;
					}
					RelativeHeight relativeHeight = this.\u1756();
					num = 2;
					continue;
				}
				case 5:
					if (obj == null)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num2 = (double)((int)obj) / 1000.0;
						num = 4;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_3A:
		if (true)
		{
		}
		return;
		IL_47:
		this.ᜄ((double)A_0.ClientWidth * num2);
		return;
		IL_AA:
		this.ᜄ((double)A_0.Margins.Top * num2);
		return;
		IL_CA:
		this.ᜄ((double)A_0.ClientHeight * num2);
		return;
		IL_11B:
		this.ᜄ((double)A_0.Margins.Bottom * num2);
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000E844C File Offset: 0x000E744C
	internal FlipOrientation ᝑ()
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
		return (FlipOrientation)this.ᜈ(4096);
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x000E8498 File Offset: 0x000E7498
	internal new void ᜀ(FlipOrientation A_0)
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
		this.ᜀ(4096, A_0);
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x000E84E4 File Offset: 0x000E74E4
	public RelativeHorizontalPosition ថ()
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
		return (RelativeHorizontalPosition)this.ᜈ(912);
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x000E8530 File Offset: 0x000E7530
	public new void ᜀ(RelativeHorizontalPosition A_0)
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
		this.ᜀ(912, A_0);
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x000E857C File Offset: 0x000E757C
	public RelativeVerticalPosition ធ()
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
		return (RelativeVerticalPosition)this.ᜈ(914);
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x000E85C8 File Offset: 0x000E75C8
	public new void ᜀ(RelativeVerticalPosition A_0)
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
		this.ᜀ(914, A_0);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x000E8614 File Offset: 0x000E7614
	public Spire.Doc.Fields.Shape.HorizontalAlignment ᝊ()
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
		return (Spire.Doc.Fields.Shape.HorizontalAlignment)this.ᜈ(911);
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x000E8660 File Offset: 0x000E7660
	public new void ᜀ(Spire.Doc.Fields.Shape.HorizontalAlignment A_0)
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
		this.ᜀ(911, A_0);
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x000E86AC File Offset: 0x000E76AC
	public Spire.Doc.Fields.Shape.VerticalAlignment \u175C()
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
		return (Spire.Doc.Fields.Shape.VerticalAlignment)this.ᜈ(913);
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000E86F8 File Offset: 0x000E76F8
	public new void ᜀ(Spire.Doc.Fields.Shape.VerticalAlignment A_0)
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
		this.ᜀ(913, A_0);
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x000E8744 File Offset: 0x000E7744
	public TextWrappingStyle ᝋ()
	{
		for (;;)
		{
			IL_20:
			object obj = this.ᜊ(954);
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if ((bool)obj)
						{
							num = 1;
							continue;
						}
						goto IL_83;
					case 1:
						goto IL_81;
					case 2:
						if (obj != null)
						{
							num = 3;
							continue;
						}
						goto IL_83;
					case 3:
						num = 0;
						continue;
					}
					goto IL_20;
				}
				IL_81:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_61;
				}
			}
		}
		IL_61:
		if (false)
		{
		}
		return TextWrappingStyle.Behind;
		IL_83:
		return (TextWrappingStyle)this.ᜈ(4097);
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000E87E4 File Offset: 0x000E77E4
	public new void ᜀ(TextWrappingStyle A_0)
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
		this.ᜀ(4097, A_0);
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000E8830 File Offset: 0x000E7830
	public TextWrappingType ច()
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
		return (TextWrappingType)this.ᜈ(4098);
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000E887C File Offset: 0x000E787C
	public new void ᜀ(TextWrappingType A_0)
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
		this.ᜀ(4098, A_0);
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x000E88C8 File Offset: 0x000E78C8
	public bool ឆ()
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
		return (bool)this.ᜈ(4099);
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000E8914 File Offset: 0x000E7914
	public void ᜋ(bool A_0)
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
		this.ᜀ(4099, A_0);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x000E8960 File Offset: 0x000E7960
	public bool ស()
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
		return (bool)this.ᜈ(950);
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x000E89AC File Offset: 0x000E79AC
	public void ᜌ(bool A_0)
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
		this.ᜀ(950, A_0);
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000E89F8 File Offset: 0x000E79F8
	internal bool \u1775()
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
		return (bool)this.ᜈ(944);
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x000E8A44 File Offset: 0x000E7A44
	internal void ᜏ(bool A_0)
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
		this.ᜀ(944, A_0);
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x000E8A90 File Offset: 0x000E7A90
	public bool ᝯ()
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
		return (bool)this.ᜈ(954);
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000E8ADC File Offset: 0x000E7ADC
	public void ᜊ(bool A_0)
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
		this.ᜀ(954, A_0);
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x000E8B28 File Offset: 0x000E7B28
	public double \u177C()
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
		return spr\u23C4.ᜋ((int)this.ᜈ(901));
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x000E8B78 File Offset: 0x000E7B78
	public void ᜆ(double A_0)
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
		this.ᜀ(901, spr\u23C4.ᜈ(A_0));
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x000E8BCC File Offset: 0x000E7BCC
	public double ក()
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
		return spr\u23C4.ᜋ((int)this.ᜈ(903));
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x000E8C1C File Offset: 0x000E7C1C
	public void ᜃ(double A_0)
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
		this.ᜀ(903, spr\u23C4.ᜈ(A_0));
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000E8C70 File Offset: 0x000E7C70
	public double ឝ()
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
		return spr\u23C4.ᜋ((int)this.ᜈ(900));
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x000E8CC0 File Offset: 0x000E7CC0
	public new void ᜁ(double A_0)
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
		this.ᜀ(900, spr\u23C4.ᜈ(A_0));
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x000E8D14 File Offset: 0x000E7D14
	public double ភ()
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
		return spr\u23C4.ᜋ((int)this.ᜈ(902));
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x000E8D64 File Offset: 0x000E7D64
	public void ᜉ(double A_0)
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
		this.ᜀ(902, spr\u23C4.ᜈ(A_0));
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x000E8DB8 File Offset: 0x000E7DB8
	public bool វ()
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
		return this.ᝋ() == TextWrappingStyle.Inline;
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x000E8DFC File Offset: 0x000E7DFC
	public int \u1755()
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
		return (int)this.ᜈ(4154);
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x000E8E48 File Offset: 0x000E7E48
	public void ᜑ(int A_0)
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
		this.ᜀ(4154, A_0);
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x000E8E94 File Offset: 0x000E7E94
	internal int \u1759()
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
		return this.ᜄ;
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x000E8ED8 File Offset: 0x000E7ED8
	internal void ᜉ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x000E8F1C File Offset: 0x000E7F1C
	public double ម()
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
		return spr\u23C4.ᜀ((int)this.ᜈ(4));
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x000E8F68 File Offset: 0x000E7F68
	public void ᜈ(double A_0)
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
		this.ᜀ(4, spr\u23C4.ᜄ(A_0));
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x000E8FB8 File Offset: 0x000E7FB8
	public Point \u1779()
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
		return new Point(this.ᝍ(), this.ឈ());
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x000E9004 File Offset: 0x000E8004
	public new void ᜀ(Point A_0)
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
		this.ᜆ(A_0.X);
		this.ᜌ(A_0.Y);
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x000E905C File Offset: 0x000E805C
	internal int ᝍ()
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
		return (int)this.ᜈ(4125);
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x000E90A8 File Offset: 0x000E80A8
	internal void ᜆ(int A_0)
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
		this.ᜀ(4125, A_0);
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x000E90F4 File Offset: 0x000E80F4
	internal int ឈ()
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
		return (int)this.ᜈ(4126);
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x000E9140 File Offset: 0x000E8140
	internal void ᜌ(int A_0)
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
		this.ᜀ(4126, A_0);
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x000E918C File Offset: 0x000E818C
	public Size ព()
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
		return new Size(this.\u1776(), this.ឍ());
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x000E91D8 File Offset: 0x000E81D8
	public new void ᜀ(Size A_0)
	{
		int a_ = 8;
		int num = 3;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_AC;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_0.Height <= 0)
					{
						num = 2;
						continue;
					}
					goto IL_AC;
				case 2:
					goto IL_AA;
				}
				if (A_0.Width <= 0)
				{
					goto IL_63;
				}
				num = 0;
				break;
			}
		}
		IL_63:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᡭᅯṱų፵", a_), ClipboardData.b("≭Ὧᅱᕳ᩵塷᥹፻ᅽﺉ꺍ﶗ몙\uda9f잡蒣얥즧쒩슫솭쒯銱횳펵颷횹\ud9bb춽뎿냃껅꧇꓉ꇍꋏ뇓ꟕ귗믙냛ﻝ铟跡쓣鳥跧飩菫샭", a_));
		IL_AA:
		goto IL_63;
		IL_AC:
		this.ᜁ(A_0);
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x000E9298 File Offset: 0x000E8298
	internal int \u1776()
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
		return (int)this.ᜈ(4127);
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x000E92E4 File Offset: 0x000E82E4
	internal int ឍ()
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
		return (int)this.ᜈ(4128);
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x000E9330 File Offset: 0x000E8330
	internal new void ᜁ(Size A_0)
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
		this.ᜏ(A_0.Width);
		this.ᜎ(A_0.Height);
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x000E9388 File Offset: 0x000E8388
	internal void ᜏ(int A_0)
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
		this.ᜀ(4127, A_0);
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x000E93D0 File Offset: 0x000E83D0
	internal void ᜎ(int A_0)
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
		this.ᜀ(4128, A_0);
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x000E9418 File Offset: 0x000E8418
	private new void ᜀ(int A_0, int A_1)
	{
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_6E;
			case 2:
				A_1 = (int)this.ᜄ(A_0);
				num = 1;
				continue;
			}
			if (A_1 > 0)
			{
				break;
			}
			num = 2;
		}
		IL_6E:
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x000E94A4 File Offset: 0x000E84A4
	public string ᝈ()
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
		return (string)this.ᜈ(909);
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x000E94F0 File Offset: 0x000E84F0
	public void ᜆ(string A_0)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ᡭᅯṱų፵", a_));
		this.ᜀ(909, A_0);
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x000E9554 File Offset: 0x000E8554
	public string ᝬ()
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
		return (string)this.ᜈ(898);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x000E95A0 File Offset: 0x000E85A0
	public void ᜄ(string A_0)
	{
		int a_ = 15;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ʹᙶᕸ๺᡼", a_));
		this.ᜀ(898, A_0);
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x000E9604 File Offset: 0x000E8604
	internal double ខ()
	{
		double num;
		for (;;)
		{
			IL_26:
			if (true)
			{
			}
			num = 0.0;
			DocumentObject documentObject = this;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_91:
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 4;
					continue;
				case 1:
					goto IL_5E;
				case 2:
					if (documentObject != null)
					{
						num2 = 0;
						continue;
					}
					return num;
				case 3:
					return num;
				case 4:
					if (documentObject.DocumentObjectType != DocumentObjectType.ShapeGroup)
					{
						num2 = 3;
						continue;
					}
					goto IL_7C;
				}
				goto IL_26;
			}
			IL_7C:
			num += ((sprᩍ)documentObject).ម();
			documentObject = documentObject.ParentObject;
			goto IL_91;
			IL_5E:
			goto IL_7C;
		}
		return num;
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x000E96C0 File Offset: 0x000E86C0
	internal bool ᝦ()
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
		return spr\u1CC6.ᜋ(this.ᝬ());
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x000E9708 File Offset: 0x000E8708
	internal bool ទ()
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (!this.ឃ())
				{
					num = 4;
					continue;
				}
				goto IL_87;
			case 2:
				if (this.ᝦ())
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				return false;
			case 3:
				num = 2;
				continue;
			case 4:
				goto IL_59;
			}
			if (!this.វ())
			{
				return false;
			}
			num = 3;
		}
		IL_59:
		return this.\u1758();
		IL_87:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
			if (false)
			{
			}
			return true;
		}
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x000E97BC File Offset: 0x000E87BC
	internal string \u177F()
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
		return spr\u1A74.ᜐ(this.ᝬ());
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x000E9804 File Offset: 0x000E8804
	internal string ឋ()
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
		return spr\u1A74.ᜏ(this.ᝬ());
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x000E984C File Offset: 0x000E884C
	public string ឡ()
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
		return (string)this.ᜈ(4120);
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x000E9898 File Offset: 0x000E8898
	public void ᜂ(string A_0)
	{
		int a_ = 15;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ʹᙶᕸ๺᡼", a_));
		this.ᜀ(4120, A_0);
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000E98FC File Offset: 0x000E88FC
	public string ជ()
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
		return (string)this.ᜈ(897);
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x000E9948 File Offset: 0x000E8948
	public void ᜃ(string A_0)
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("๷᭹ၻ୽", a_));
		this.ᜀ(897, A_0);
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x000E99AC File Offset: 0x000E89AC
	internal spr\u2588 \u175E()
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
		return this.ᜅ;
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x000E99F0 File Offset: 0x000E89F0
	internal new void ᜀ(spr\u2588 A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x000E9A34 File Offset: 0x000E8A34
	public Font ញ()
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
		return null;
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x000E9A70 File Offset: 0x000E8A70
	internal ConnectorType ᜄ()
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
		return (ConnectorType)this.ᜈ(771);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x000E9ABC File Offset: 0x000E8ABC
	internal new void ᜀ(ConnectorType A_0)
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
		this.ᜀ(771, A_0);
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000E9B08 File Offset: 0x000E8B08
	internal bool ᝌ()
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
		return this.ᜄ() != ConnectorType.None;
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x000E9B50 File Offset: 0x000E8B50
	internal int \u1757()
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
		return (int)this.ᜈ(128);
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x000E9B9C File Offset: 0x000E8B9C
	internal void ᜇ(int A_0)
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
		this.ᜀ(128, A_0);
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x000E9BE8 File Offset: 0x000E8BE8
	internal int \u177B()
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
		return (int)this.ᜈ(138);
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x000E9C34 File Offset: 0x000E8C34
	internal void ᜐ(int A_0)
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
		this.ᜀ(138, A_0);
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x000E9C80 File Offset: 0x000E8C80
	public bool ᝏ()
	{
		if (base.ParentObject != null)
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
				return base.ParentObject.DocumentObjectType != DocumentObjectType.ShapeGroup;
			}
		}
		return true;
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x000E9CDC File Offset: 0x000E8CDC
	public Paragraph \u1771()
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
		return base.Owner.Owner as Paragraph;
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x000E9D28 File Offset: 0x000E8D28
	public string អ()
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
		return (string)this.ᜈ(896);
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x000E9D74 File Offset: 0x000E8D74
	public void ᜅ(string A_0)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ѱᕳ᩵൷ό", a_));
		this.ᜀ(896, A_0);
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x000E9DD8 File Offset: 0x000E8DD8
	internal bool ᝐ()
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
		return (bool)this.ᜈ(958);
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x000E9E24 File Offset: 0x000E8E24
	internal void ᜉ(bool A_0)
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
		this.ᜀ(958, A_0);
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x000E9E70 File Offset: 0x000E8E70
	internal bool \u1772()
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
		return (bool)this.ᜈ(1983);
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x000E9EBC File Offset: 0x000E8EBC
	internal byte[] ផ()
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
		return (byte[])this.ᜊ(1792);
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x000E9F08 File Offset: 0x000E8F08
	internal bool ឥ()
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
		return (bool)this.ᜈ(1855);
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x000E9F54 File Offset: 0x000E8F54
	internal PointF[] ᝤ()
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
		return sprᝀ.ᜀ((spr\u2055[])this.ᜈ(899));
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x000E9FA4 File Offset: 0x000E8FA4
	internal float ᝢ()
	{
		object obj = this.ᜈ(129);
		if (obj != null)
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
				return float.Parse(obj.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture) / 12700f;
			}
		}
		return float.MaxValue;
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x000EA014 File Offset: 0x000E9014
	internal float ឤ()
	{
		object obj = this.ᜈ(131);
		if (obj != null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5B;
			}
			if (false)
			{
			}
			return float.Parse(obj.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture) / 12700f;
		}
		IL_5B:
		return float.MaxValue;
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x000EA084 File Offset: 0x000E9084
	internal float ដ()
	{
		object obj = this.ᜈ(130);
		if (obj != null)
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
				return float.Parse(obj.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture) / 12700f;
			}
		}
		return float.MaxValue;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x000EA0F4 File Offset: 0x000E90F4
	internal float ហ()
	{
		object obj = this.ᜈ(132);
		if (obj != null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5B;
			}
			if (false)
			{
			}
			return float.Parse(obj.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture) / 12700f;
		}
		IL_5B:
		return float.MaxValue;
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x000EA164 File Offset: 0x000E9164
	public DocumentObjectCollection ᝰ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜈ = new DocumentItemCollection(base.Document, this);
				num = 1;
				continue;
			case 1:
				goto IL_76;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_76;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (this.ᜈ != null)
				{
					goto IL_78;
				}
				num = 0;
				break;
			}
		}
		IL_76:
		IL_78:
		return this.ᜈ;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x000EA1F0 File Offset: 0x000E91F0
	// Note: this type is marked as 'beforefieldinit'.
	static sprᩍ()
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
		sprᩍ.ᜆ = 1584.0;
		sprᩍ.ᜇ = 0.75;
	}

	// Token: 0x04001737 RID: 5943
	internal new const int ᜀ = 1024;

	// Token: 0x04001738 RID: 5944
	internal new const int ᜁ = 1024;

	// Token: 0x04001739 RID: 5945
	private spr\u261A ᜂ;

	// Token: 0x0400173A RID: 5946
	private Image ᜃ;

	// Token: 0x0400173B RID: 5947
	private new int ᜄ;

	// Token: 0x0400173C RID: 5948
	private spr\u2588 ᜅ = new spr\u2588();

	// Token: 0x0400173D RID: 5949
	internal static double ᜆ;

	// Token: 0x0400173E RID: 5950
	internal static double ᜇ;

	// Token: 0x0400173F RID: 5951
	private DocumentObjectCollection ᜈ;
}

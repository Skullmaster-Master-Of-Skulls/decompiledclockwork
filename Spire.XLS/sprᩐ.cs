using System;
using System.Drawing;
using System.Threading;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x020004C1 RID: 1217
internal class sprᩐ : CommonWrapper, IGradient, IOptimizedUpdate
{
	// Token: 0x06004ADF RID: 19167 RVA: 0x002D7128 File Offset: 0x002D6128
	public sprᩐ()
	{
	}

	// Token: 0x06004AE0 RID: 19168 RVA: 0x002D713C File Offset: 0x002D613C
	public sprᩐ(XlsShapeFill A_0)
	{
		int a_ = 10;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("✿ぁ╃≅ⅇ⽉≋㩍", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x06004AE1 RID: 19169 RVA: 0x002D7178 File Offset: 0x002D6178
	public OColor ᜉ()
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
		return this.ᜀ.BackColorObject;
	}

	// Token: 0x06004AE2 RID: 19170 RVA: 0x002D71C0 File Offset: 0x002D61C0
	public Color ᜃ()
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
		return this.ᜀ.BackColor;
	}

	// Token: 0x06004AE3 RID: 19171 RVA: 0x002D7208 File Offset: 0x002D6208
	public void ᜁ(Color A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.BeginUpdate();
					this.ᜀ.BackColor = A_0;
					this.EndUpdate();
					if (true)
					{
					}
					num = 1;
					continue;
				}
				if (!(A_0 != this.ᜃ()))
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
	}

	// Token: 0x06004AE4 RID: 19172 RVA: 0x002D729C File Offset: 0x002D629C
	public ExcelColors ᜇ()
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
		return this.ᜀ.BackKnownColor;
	}

	// Token: 0x06004AE5 RID: 19173 RVA: 0x002D72E4 File Offset: 0x002D62E4
	public void ᜀ(ExcelColors A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					if (true)
					{
					}
					this.BeginUpdate();
					this.ᜀ.BackKnownColor = A_0;
					this.EndUpdate();
					num = 0;
					continue;
				}
				if (A_0 == this.ᜇ())
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
	}

	// Token: 0x06004AE6 RID: 19174 RVA: 0x002D7370 File Offset: 0x002D6370
	public OColor ᜈ()
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
		return this.ᜀ.ForeColorObject;
	}

	// Token: 0x06004AE7 RID: 19175 RVA: 0x002D73B8 File Offset: 0x002D63B8
	public Color ᜁ()
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
		return this.ᜀ.ForeColor;
	}

	// Token: 0x06004AE8 RID: 19176 RVA: 0x002D7400 File Offset: 0x002D6400
	public void ᜀ(Color A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.BeginUpdate();
					this.ᜀ.ForeColor = A_0;
					this.EndUpdate();
					num = 1;
					continue;
				}
				if (!(A_0 != this.ᜁ()))
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 2;
					break;
				}
			}
		}
	}

	// Token: 0x06004AE9 RID: 19177 RVA: 0x002D7494 File Offset: 0x002D6494
	public ExcelColors ᜅ()
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
		return this.ᜀ.ForeKnownColor;
	}

	// Token: 0x06004AEA RID: 19178 RVA: 0x002D74DC File Offset: 0x002D64DC
	public void ᜁ(ExcelColors A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.BeginUpdate();
					this.ᜀ.ForeKnownColor = A_0;
					this.EndUpdate();
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (A_0 == this.ᜅ())
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06004AEB RID: 19179 RVA: 0x002D7568 File Offset: 0x002D6568
	public GradientStyleType ᜊ()
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
		return this.ᜀ.GradientStyle;
	}

	// Token: 0x06004AEC RID: 19180 RVA: 0x002D75B0 File Offset: 0x002D65B0
	public void ᜀ(GradientStyleType A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.BeginUpdate();
					this.ᜀ.GradientStyle = A_0;
					this.EndUpdate();
					num = 2;
					continue;
				case 2:
					return;
				}
				if (true)
				{
				}
				if (A_0 == this.ᜊ())
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}
	}

	// Token: 0x06004AED RID: 19181 RVA: 0x002D763C File Offset: 0x002D663C
	public GradientVariantsType ᜄ()
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
		return this.ᜀ.GradientVariant;
	}

	// Token: 0x06004AEE RID: 19182 RVA: 0x002D7684 File Offset: 0x002D6684
	public void ᜁ(GradientVariantsType A_0)
	{
		for (;;)
		{
			this.ᜀ(A_0);
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.BeginUpdate();
					this.ᜀ.GradientVariant = A_0;
					this.EndUpdate();
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
					break;
				case 1:
					return;
				case 2:
					if (A_0 != this.ᜄ())
					{
						num = 0;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06004AEF RID: 19183 RVA: 0x002D771C File Offset: 0x002D671C
	public int ᜀ(IGradient A_0)
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
		return this.ᜀ.CompareTo(A_0);
	}

	// Token: 0x06004AF0 RID: 19184 RVA: 0x002D7764 File Offset: 0x002D6764
	public void ᜋ()
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
		this.BeginUpdate();
		this.ᜀ.TwoColorGradient();
		this.EndUpdate();
	}

	// Token: 0x06004AF1 RID: 19185 RVA: 0x002D77B8 File Offset: 0x002D67B8
	public void ᜀ(GradientStyleType A_0, GradientVariantsType A_1)
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
		this.BeginUpdate();
		this.ᜀ.TwoColorGradient(A_0, A_1);
		this.EndUpdate();
	}

	// Token: 0x06004AF2 RID: 19186 RVA: 0x002D780C File Offset: 0x002D680C
	public XlsShapeFill ᜂ()
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

	// Token: 0x06004AF3 RID: 19187 RVA: 0x002D7850 File Offset: 0x002D6850
	public void ᜀ(EventHandler A_0)
	{
		for (;;)
		{
			EventHandler eventHandler = this.ᜁ;
			int num = 0;
			for (;;)
			{
				EventHandler eventHandler2;
				switch (num)
				{
				case 0:
					goto IL_25;
				case 1:
					if (eventHandler == eventHandler2)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					goto IL_25;
				case 2:
					goto IL_7C;
				}
				break;
				IL_25:
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, A_0);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜁ, value, eventHandler2);
				num = 1;
			}
		}
		IL_7C:
		if (true)
		{
		}
	}

	// Token: 0x06004AF4 RID: 19188 RVA: 0x002D78E4 File Offset: 0x002D68E4
	public void ᜁ(EventHandler A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			EventHandler eventHandler = this.ᜁ;
			int num = 0;
			for (;;)
			{
				EventHandler eventHandler2;
				switch (num)
				{
				case 0:
					goto IL_2D;
				case 1:
					if (eventHandler != eventHandler2)
					{
						goto IL_2D;
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
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				break;
				IL_2D:
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, A_0);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜁ, value, eventHandler2);
				num = 1;
			}
		}
	}

	// Token: 0x06004AF5 RID: 19189 RVA: 0x002D797C File Offset: 0x002D697C
	public virtual void ᜀ()
	{
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_80;
				case 1:
					this.ᜀ = this.ᜀ.Clone(this.ᜀ.Parent);
					num = 0;
					continue;
				}
				if (base.BeginCallsCount != 0)
				{
					goto IL_82;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					break;
				}
			}
		}
		IL_80:
		IL_82:
		base.BeginUpdate();
	}

	// Token: 0x06004AF6 RID: 19190 RVA: 0x002D7A14 File Offset: 0x002D6A14
	public virtual void ᜆ()
	{
		for (;;)
		{
			base.EndUpdate();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsWorkbook xlsWorkbook = ((spr\u192F)this.ᜀ.Parent).ᜎ();
					xlsWorkbook.SetChanged();
					num = 2;
					continue;
				}
				case 1:
					if (base.BeginCallsCount == 0)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					if (this.ᜁ == null)
					{
						return;
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
						num = 3;
						continue;
					}
					break;
				case 3:
					this.ᜁ(this, EventArgs.Empty);
					num = 4;
					continue;
				case 4:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06004AF7 RID: 19191 RVA: 0x002D7AE8 File Offset: 0x002D6AE8
	private void ᜀ(GradientVariantsType A_0)
	{
		int a_ = 2;
		for (;;)
		{
			for (;;)
			{
				if (true)
				{
				}
				GradientStyleType gradientStyleType = this.ᜊ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (gradientStyleType)
						{
						case GradientStyleType.Horizontal:
						case GradientStyleType.Vertical:
						case GradientStyleType.Diagonl_Up:
						case GradientStyleType.Diagonl_Down:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						case GradientStyleType.From_Corner:
							return;
						case GradientStyleType.From_Center:
							num = 2;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					case 1:
						num = 5;
						continue;
					case 2:
						if (A_0 != GradientVariantsType.ShadingVariants2)
						{
							num = 6;
							continue;
						}
						goto IL_F2;
					case 3:
						goto IL_F0;
					case 4:
						if (A_0 != GradientVariantsType.ShadingVariants3)
						{
							num = 1;
							continue;
						}
						goto IL_F2;
					case 5:
						if (A_0 == GradientVariantsType.ShadingVariants4)
						{
							num = 8;
							continue;
						}
						return;
					case 6:
						num = 4;
						continue;
					case 7:
						return;
					case 8:
						goto IL_121;
					case 9:
						if (A_0 == GradientVariantsType.ShadingVariants4)
						{
							num = 3;
							continue;
						}
						return;
					}
					break;
				}
			}
		}
		return;
		IL_F0:
		throw new ArgumentException(RecordTableEnumerator.b("欷刹崻娽⤿ⱁ⍃晅㹇⭉㹋❍ㅏ㱑⁓癕汗穙㕛ⵝ䁟ౡୣብ䡧ᱩ൫ɭ᥯ᙱ味ၵ᝷ࡹ屻ᵽﺉ겋ﾕﶗ뺝펟횡\udda3쪥춧蒩", a_));
		IL_F2:
		throw new ArgumentException(RecordTableEnumerator.b("笷伹主䰽┿ⱁぃ晅㭇≉ⵋ⩍㥏㱑㍓癕⹗㭙⹛㝝şౡၣ䙥ŧᥩ䱫mὯٱ味u᥷ᙹᕻ᩽ꁿꢇﺋﶏ늑鍊肟얡횣장첧쎩즫삭쒯銱잳습솷횹\ud9bb邽", a_));
		IL_121:
		goto IL_F2;
	}

	// Token: 0x04002201 RID: 8705
	private XlsShapeFill ᜀ;

	// Token: 0x04002202 RID: 8706
	private EventHandler ᜁ;
}

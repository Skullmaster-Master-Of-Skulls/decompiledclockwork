using System;
using System.Collections;
using System.Reflection;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004D0 RID: 1232
[DefaultMember("Item")]
internal class spr\u21A1 : XlsObject, IConditionalFormats
{
	// Token: 0x06004BC6 RID: 19398 RVA: 0x002E7428 File Offset: 0x002E6428
	internal spr\u21A1(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06004BC7 RID: 19399 RVA: 0x002E7440 File Offset: 0x002E6440
	private void ᜁ()
	{
		int a_ = 0;
		this.ᜀ = (base.FindParent(typeof(spr\u1CCF)) as spr\u1CCF);
		if (this.ᜀ == null)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䘵夷䠹夻倽㐿", a_), RecordTableEnumerator.b("昵夷䠹夻倽㐿扁⭃⑅≇⽉⽋㩍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣eݧὩɫ੭幯", a_));
			}
		}
	}

	// Token: 0x06004BC8 RID: 19400 RVA: 0x002E74CC File Offset: 0x002E64CC
	private void ᜀ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06004BC9 RID: 19401 RVA: 0x002E750C File Offset: 0x002E650C
	public IConditionalFormats ᜁ(int A_0)
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
		return ((XlsRange)this.ᜀ.ᜂ(A_0)).ConditionalFormats;
	}

	// Token: 0x06004BCA RID: 19402 RVA: 0x002E7560 File Offset: 0x002E6560
	public int ᜈ()
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
		return this.ᜀ.ᜯ();
	}

	// Token: 0x06004BCB RID: 19403 RVA: 0x002E75A8 File Offset: 0x002E65A8
	int IConditionalFormats.ᜄ()
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
		return 0;
	}

	// Token: 0x06004BCC RID: 19404 RVA: 0x002E75E4 File Offset: 0x002E65E4
	IConditionalFormat IConditionalFormats.ᜂ(int A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06004BCD RID: 19405 RVA: 0x002E7624 File Offset: 0x002E6624
	public IConditionalFormat ᜃ()
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
		this.ᜁ(0).AddCondition();
		int index = this.ᜁ(0).Count - 1;
		this.ᜀ();
		return ((IConditionalFormats)this)[index];
	}

	// Token: 0x06004BCE RID: 19406 RVA: 0x002E768C File Offset: 0x002E668C
	public void ᜆ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06004BCF RID: 19407 RVA: 0x002E76CC File Offset: 0x002E66CC
	public void ᜀ(int A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06004BD0 RID: 19408 RVA: 0x002E770C File Offset: 0x002E670C
	public IEnumerator ᜂ()
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
		return null;
	}

	// Token: 0x06004BD1 RID: 19409 RVA: 0x002E7748 File Offset: 0x002E6748
	public void ᜇ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06004BD2 RID: 19410 RVA: 0x002E7788 File Offset: 0x002E6788
	public void ᜅ()
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
		throw new NotImplementedException();
	}

	// Token: 0x04002282 RID: 8834
	private spr\u1CCF ᜀ;
}

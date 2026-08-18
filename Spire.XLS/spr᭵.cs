using System;
using System.Collections;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004D1 RID: 1233
[DefaultMember("Item")]
[CLSCompliant(false)]
internal class spr\u1B75 : CollectionBase
{
	// Token: 0x06004BD3 RID: 19411 RVA: 0x002E77C8 File Offset: 0x002E67C8
	public BiffRecordRaw ᜀ(int A_0)
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
		return (BiffRecordRaw)base.List[A_0];
	}

	// Token: 0x06004BD4 RID: 19412 RVA: 0x002E7814 File Offset: 0x002E6814
	public void ᜀ(int A_0, BiffRecordRaw A_1)
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
		base.List[A_0] = A_1;
	}

	// Token: 0x06004BD5 RID: 19413 RVA: 0x002E785C File Offset: 0x002E685C
	public int ᜃ(BiffRecordRaw A_0)
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
		return base.List.Add(A_0);
	}

	// Token: 0x06004BD6 RID: 19414 RVA: 0x002E78A4 File Offset: 0x002E68A4
	public bool ᜀ(BiffRecordRaw A_0)
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
		return base.InnerList.Contains(A_0);
	}

	// Token: 0x06004BD7 RID: 19415 RVA: 0x002E78EC File Offset: 0x002E68EC
	public void ᜀ(BiffRecordRaw[] A_0, int A_1)
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
		base.List.CopyTo(A_0, A_1);
	}

	// Token: 0x06004BD8 RID: 19416 RVA: 0x002E7934 File Offset: 0x002E6934
	public spr\u1B75.ᜀ ᜀ()
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
		return new spr\u1B75.ᜀ(this);
	}

	// Token: 0x06004BD9 RID: 19417 RVA: 0x002E7978 File Offset: 0x002E6978
	public int ᜁ(BiffRecordRaw A_0)
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
		return base.InnerList.IndexOf(A_0);
	}

	// Token: 0x06004BDA RID: 19418 RVA: 0x002E79C0 File Offset: 0x002E69C0
	public void ᜁ(int A_0, BiffRecordRaw A_1)
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
		base.List.Insert(A_0, A_1);
	}

	// Token: 0x06004BDB RID: 19419 RVA: 0x002E7A08 File Offset: 0x002E6A08
	public void ᜂ(BiffRecordRaw A_0)
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
		base.List.Remove(A_0);
	}

	// Token: 0x020004D2 RID: 1234
	internal class ᜀ : IEnumerator
	{
		// Token: 0x06004BDD RID: 19421 RVA: 0x002E7A64 File Offset: 0x002E6A64
		public ᜀ(spr\u1B75 A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x002E7A88 File Offset: 0x002E6A88
		public BiffRecordRaw ᜁ()
		{
			int a_ = 7;
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
				if (this.ᜁ != null)
				{
					return this.ᜁ;
				}
				break;
			}
			throw new ArgumentException(RecordTableEnumerator.b("縼帾ⵀ⽂敄ᕆⱈ㡊⡌㭎祐穒畔㙖㝘㽚絜⭞ॠ٢୤䝦⑨Ѫ᭬੮㽰ᙲ൴Ͷ典剺嵼ቾꮊ릖", a_));
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x002E7AF0 File Offset: 0x002E6AF0
		object IEnumerator.ᜃ()
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
			return this.ᜁ();
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x002E7B34 File Offset: 0x002E6B34
		public bool ᜀ()
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
				if (this.ᜂ >= this.ᜀ.Count - 1)
				{
					this.ᜁ = null;
					return false;
				}
				break;
			}
			if (true)
			{
			}
			this.ᜂ++;
			this.ᜁ = this.ᜀ.ᜀ(this.ᜂ);
			return true;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x002E7BB8 File Offset: 0x002E6BB8
		public void ᜂ()
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
			this.ᜂ = -1;
			this.ᜁ = null;
		}

		// Token: 0x04002283 RID: 8835
		private spr\u1B75 ᜀ;

		// Token: 0x04002284 RID: 8836
		private BiffRecordRaw ᜁ;

		// Token: 0x04002285 RID: 8837
		private int ᜂ = -1;
	}
}

using System;
using System.Collections;
using System.Reflection;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000526 RID: 1318
[DefaultMember("Item")]
internal class spr\u256D : TypedSortedListEx<string, ICustomProperty>, IWorksheetCustomProperties
{
	// Token: 0x0600508B RID: 20619 RVA: 0x00329170 File Offset: 0x00328170
	public spr\u256D()
	{
	}

	// Token: 0x0600508C RID: 20620 RVA: 0x00329184 File Offset: 0x00328184
	public spr\u256D(IList A_0, int A_1)
	{
		int a_ = 18;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑❓", a_));
		}
		int count = A_0.Count;
		if (A_1 >= 0)
		{
			if (A_1 < count)
			{
				while (A_1 < count)
				{
					sprế sprế = A_0[A_1] as sprế;
					if (sprế == null)
					{
						return;
					}
					this.ᜀ(sprế);
					A_1++;
				}
				return;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭇㽉㽋㩍㽏㽑ѓ⑕㝗⩙㥛ⱝᑟ᭡㑣॥᭧", a_));
	}

	// Token: 0x0600508D RID: 20621 RVA: 0x00329208 File Offset: 0x00328208
	public ICustomProperty ᜀ(int A_0)
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
		return this.GetByIndex(A_0);
	}

	// Token: 0x0600508E RID: 20622 RVA: 0x0032924C File Offset: 0x0032824C
	public ICustomProperty ᜁ(string A_0)
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
		return this.ᜁ(A_0);
	}

	// Token: 0x0600508F RID: 20623 RVA: 0x00329290 File Offset: 0x00328290
	public void ᜀ(RecordArrayList A_0)
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.Count;
			for (;;)
			{
				IL_21:
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2B;
					case 1:
						goto IL_2B;
					case 2:
						if (num < count)
						{
							if (true)
							{
							}
							WorksheetCustomProperty worksheetCustomProperty = this.GetByIndex(num) as WorksheetCustomProperty;
							worksheetCustomProperty.ᜀ(A_0);
							num++;
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_21;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 3:
						return;
					}
					goto IL_18;
					IL_2B:
					num2 = 2;
				}
			}
		}
	}

	// Token: 0x06005090 RID: 20624 RVA: 0x00329334 File Offset: 0x00328334
	public ICustomProperty ᜂ(string A_0)
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
		WorksheetCustomProperty a_ = new WorksheetCustomProperty(A_0);
		return this.ᜀ(a_);
	}

	// Token: 0x06005091 RID: 20625 RVA: 0x00329380 File Offset: 0x00328380
	public ICustomProperty ᜀ(ICustomProperty A_0)
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
		this.Add(A_0.Name, A_0);
		return A_0;
	}

	// Token: 0x06005092 RID: 20626 RVA: 0x003293CC File Offset: 0x003283CC
	internal void ᜀ(sprế A_0)
	{
		int a_ = 12;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㉁㙃⥅㡇⽉㹋㩍⥏", a_));
			}
		}
		WorksheetCustomProperty a_2 = new WorksheetCustomProperty(A_0);
		this.ᜀ(a_2);
	}

	// Token: 0x06005093 RID: 20627 RVA: 0x00329438 File Offset: 0x00328438
	public bool ᜀ(string A_0)
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
		return base.Contains(A_0);
	}
}

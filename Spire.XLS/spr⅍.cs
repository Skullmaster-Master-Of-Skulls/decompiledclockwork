using System;
using System.Windows.Forms;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004F7 RID: 1271
internal abstract class spr\u214D
{
	// Token: 0x06004D9E RID: 19870 RVA: 0x002F67B4 File Offset: 0x002F57B4
	protected spr\u214D()
	{
	}

	// Token: 0x06004D9F RID: 19871 RVA: 0x002F67D4 File Offset: 0x002F57D4
	protected spr\u214D(IWorksheet A_0) : this(A_0, null)
	{
	}

	// Token: 0x06004DA0 RID: 19872 RVA: 0x002F67EC File Offset: 0x002F57EC
	protected spr\u214D(IWorkbook A_0) : this(A_0, null)
	{
	}

	// Token: 0x06004DA1 RID: 19873 RVA: 0x002F6804 File Offset: 0x002F5804
	protected spr\u214D(IWorkbook A_0, spr\u214D A_1)
	{
		this.ᜀ = A_1;
		this.ᜀ(A_0);
	}

	// Token: 0x06004DA2 RID: 19874 RVA: 0x002F6830 File Offset: 0x002F5830
	protected spr\u214D(IWorksheet A_0, spr\u214D A_1)
	{
		this.ᜀ = A_1;
		this.ᜀ(A_0);
	}

	// Token: 0x06004DA3 RID: 19875 RVA: 0x002F685C File Offset: 0x002F585C
	public spr\u214D ᜈ()
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

	// Token: 0x06004DA4 RID: 19876 RVA: 0x002F68A0 File Offset: 0x002F58A0
	public void ᜀ(spr\u214D A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06004DA5 RID: 19877 RVA: 0x002F68E4 File Offset: 0x002F58E4
	public virtual string ᜇ()
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
		return this.ᜃ;
	}

	// Token: 0x06004DA6 RID: 19878 RVA: 0x002F6928 File Offset: 0x002F5928
	public virtual void ᜂ(string A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004DA7 RID: 19879 RVA: 0x002F696C File Offset: 0x002F596C
	public IWorkbook ᜅ()
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
		return this.ᜁ;
	}

	// Token: 0x06004DA8 RID: 19880 RVA: 0x002F69B0 File Offset: 0x002F59B0
	public void ᜁ(IWorkbook A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06004DA9 RID: 19881 RVA: 0x002F69F4 File Offset: 0x002F59F4
	public IWorksheet ᜆ()
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
		return this.ᜂ;
	}

	// Token: 0x06004DAA RID: 19882 RVA: 0x002F6A38 File Offset: 0x002F5A38
	public void ᜁ(IWorksheet A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06004DAB RID: 19883 RVA: 0x002F6A7C File Offset: 0x002F5A7C
	public virtual void ᜀ(IWorkbook A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06004DAC RID: 19884 RVA: 0x002F6AC0 File Offset: 0x002F5AC0
	public virtual void ᜀ(IWorksheet A_0)
	{
		for (;;)
		{
			this.ᜂ = A_0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (A_0 != null)
					{
						num = 2;
						continue;
					}
					return;
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
						this.ᜁ = A_0.Workbook;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
	}

	// Token: 0x06004DAD RID: 19885 RVA: 0x002F6B44 File Offset: 0x002F5B44
	public virtual void ᜄ()
	{
		IDataObject dataObject;
		for (;;)
		{
			dataObject = this.ᜀ();
			spr\u214D spr_u214D = this.ᜈ();
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_55;
				case 1:
					goto IL_42;
				case 2:
					goto IL_42;
				case 3:
					if (spr_u214D == null)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						spr_u214D.ᜀ(dataObject);
						spr_u214D = spr_u214D.ᜈ();
						num = 1;
						continue;
					}
					break;
				}
				break;
				IL_42:
				num = 3;
			}
		}
		IL_55:
		IL_8E:
		Clipboard.SetDataObject(dataObject, true);
	}

	// Token: 0x06004DAE RID: 19886 RVA: 0x002F6BE8 File Offset: 0x002F5BE8
	public virtual void ᜂ(IXLSRange A_0)
	{
		IDataObject dataObject;
		for (;;)
		{
			dataObject = this.ᜀ(A_0);
			spr\u214D spr_u214D = this.ᜈ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_43;
				case 1:
					if (spr_u214D == null)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						spr_u214D.ᜀ(dataObject, A_0);
						spr_u214D = spr_u214D.ᜈ();
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_43;
				case 3:
					goto IL_56;
				}
				break;
				IL_43:
				num = 1;
			}
		}
		IL_56:
		IL_90:
		Clipboard.SetDataObject(dataObject, true);
	}

	// Token: 0x06004DAF RID: 19887 RVA: 0x002F6C8C File Offset: 0x002F5C8C
	public virtual IWorkbook ᜀ(IWorkbooks A_0)
	{
		int a_ = 1;
		int num = 0;
		IDataObject dataObject;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_44;
			case 2:
				goto IL_9E;
			case 3:
				goto IL_DF;
			case 4:
				goto IL_76;
			case 5:
				if (dataObject == null)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			case 6:
				if (this.ᜈ() != null)
				{
					num = 3;
					continue;
				}
				goto IL_FF;
			case 7:
				if (dataObject.GetDataPresent(this.ᜇ(), true))
				{
					num = 4;
					continue;
				}
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				dataObject = Clipboard.GetDataObject();
				num = 5;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶嘸䤺嘼崾⹀ⱂ⹄㑆", a_));
		IL_76:
		if (true)
		{
		}
		return this.ᜀ(dataObject, A_0);
		IL_9E:
		return null;
		IL_DF:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_44;
		default:
			if (false)
			{
			}
			return this.ᜈ().ᜀ(A_0);
		}
		IL_FF:
		return null;
	}

	// Token: 0x06004DB0 RID: 19888
	public abstract IDataObject ᜀ();

	// Token: 0x06004DB1 RID: 19889
	public abstract IDataObject ᜀ(IXLSRange A_0);

	// Token: 0x06004DB2 RID: 19890
	protected abstract IWorkbook ᜀ(IDataObject A_0, IWorkbooks A_1);

	// Token: 0x06004DB3 RID: 19891
	protected abstract void ᜀ(IDataObject A_0);

	// Token: 0x06004DB4 RID: 19892
	protected abstract void ᜀ(IDataObject A_0, IXLSRange A_1);

	// Token: 0x0400232B RID: 9003
	private spr\u214D ᜀ;

	// Token: 0x0400232C RID: 9004
	private IWorkbook ᜁ;

	// Token: 0x0400232D RID: 9005
	private IWorksheet ᜂ;

	// Token: 0x0400232E RID: 9006
	private string ᜃ = string.Empty;
}

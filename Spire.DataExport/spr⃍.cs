using System;
using System.Collections;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

// Token: 0x02000054 RID: 84
internal class spr\u20CD : PropertyDescriptor
{
	// Token: 0x060002AF RID: 687 RVA: 0x0001921C File Offset: 0x0001821C
	public spr\u20CD(IniSetting A_0, Attribute[] A_1) : base(A_0.Name, A_1)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00019240 File Offset: 0x00018240
	public override AttributeCollection get_Attributes()
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
		return new AttributeCollection(null);
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00019284 File Offset: 0x00018284
	public virtual object ᜀ(object A_0)
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
		return this.ᜀ.Value;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x000192CC File Offset: 0x000182CC
	public virtual Type ᜂ()
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
		return this.ᜀ.OriginalType;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x00019314 File Offset: 0x00018314
	public virtual Type ᜁ()
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
		return this.ᜀ.GetType();
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0001935C File Offset: 0x0001835C
	public override string get_Description()
	{
		int a_ = 14;
		while (this.ᜀ.Description.Length == 0)
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
				return this.ᜀ.TypeName + HyperlinksCollectionEditor.b("ةఫ", a_) + this.ᜀ.Value;
			}
		}
		return string.Concat(new object[]
		{
			this.ᜀ.TypeName,
			HyperlinksCollectionEditor.b("ةఫ", a_),
			this.ᜀ.Value,
			HyperlinksCollectionEditor.b("\u2029", a_),
			this.ᜀ.Description
		});
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00019438 File Offset: 0x00018438
	public override string get_Name()
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
		return this.ᜀ.Name;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00019480 File Offset: 0x00018480
	public override string get_DisplayName()
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
		return this.ᜀ.Name;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x000194C8 File Offset: 0x000184C8
	public virtual bool ᜁ(object A_0)
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
		return true;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00019504 File Offset: 0x00018504
	public virtual bool ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_59;
			case 2:
				this.ᜀ.ReadonlyInPG = true;
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (this.ᜀ.OriginalType != typeof(ArrayList))
			{
				break;
			}
			num = 2;
		}
		IL_59:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_59;
		default:
			if (false)
			{
			}
			return this.ᜀ.ReadonlyInPG;
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x000195A0 File Offset: 0x000185A0
	public virtual void ᜃ(object A_0)
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

	// Token: 0x060002BA RID: 698 RVA: 0x000195DC File Offset: 0x000185DC
	public virtual void ᜀ(object A_0, object A_1)
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
		this.ᜀ.Value = A_1;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00019624 File Offset: 0x00018624
	public virtual bool ᜂ(object A_0)
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
		return false;
	}

	// Token: 0x040000C1 RID: 193
	private IniSetting ᜀ;
}

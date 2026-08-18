using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

// Token: 0x02000138 RID: 312
internal class sprἭ : PropertyDescriptor
{
	// Token: 0x0600079E RID: 1950 RVA: 0x0004D050 File Offset: 0x0004C050
	public sprἭ(IniSection A_0, Attribute[] A_1) : base(A_0.Name, A_1)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0004D074 File Offset: 0x0004C074
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

	// Token: 0x060007A0 RID: 1952 RVA: 0x0004D0B8 File Offset: 0x0004C0B8
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
		return this.ᜀ.Settings;
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0004D100 File Offset: 0x0004C100
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
		return this.ᜀ.GetType();
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0004D148 File Offset: 0x0004C148
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
		return this.ᜀ.Settings.GetType();
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0004D194 File Offset: 0x0004C194
	public override string get_Description()
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return HyperlinksCollectionEditor.b("眣䌥䬧帩䔫䄭帯ሱ圳夵嘷丹崻圽⸿ㅁ摃", a_) + this.ᜀ.Settings.Count + HyperlinksCollectionEditor.b("У唥䴧帩堫䜭帯唱䜳", a_);
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0004D210 File Offset: 0x0004C210
	public override string get_Category()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return HyperlinksCollectionEditor.b("礩䤫䴭䐯嬱嬳堵䬷", a_);
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0004D264 File Offset: 0x0004C264
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

	// Token: 0x060007A6 RID: 1958 RVA: 0x0004D2AC File Offset: 0x0004C2AC
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

	// Token: 0x060007A7 RID: 1959 RVA: 0x0004D2F4 File Offset: 0x0004C2F4
	public virtual bool ᜀ()
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

	// Token: 0x060007A8 RID: 1960 RVA: 0x0004D330 File Offset: 0x0004C330
	public virtual bool ᜁ(object A_0)
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
		return false;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0004D36C File Offset: 0x0004C36C
	public virtual void ᜃ(object A_0)
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
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0004D3A8 File Offset: 0x0004C3A8
	public virtual void ᜀ(object A_0, object A_1)
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
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0004D3E4 File Offset: 0x0004C3E4
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

	// Token: 0x04000617 RID: 1559
	private IniSection ᜀ;
}

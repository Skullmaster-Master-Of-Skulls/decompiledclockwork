using System;
using System.Runtime.InteropServices;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200031C RID: 796
internal class sprᶙ
{
	// Token: 0x0600312F RID: 12591 RVA: 0x001C6D2C File Offset: 0x001C5D2C
	public sprᶙ() : this(8228)
	{
	}

	// Token: 0x06003130 RID: 12592 RVA: 0x001C6D44 File Offset: 0x001C5D44
	public sprᶙ(int A_0)
	{
		int a_ = 10;
		base..ctor();
		if (A_0 < 1024)
		{
			A_0 = 1024;
		}
		if (A_0 > 2147483647)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ཁ⅃⭅❇㡉㕋్㱏㵑㝓㵕", a_), RecordTableEnumerator.b("ᘿ⍁⡃㍅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝ݟၡţݥᱧཀྵṫ乭", a_) + int.MaxValue.ToString());
		}
		this.ᜄ = Marshal.AllocCoTaskMem(A_0);
		this.ᜅ = A_0;
		if (this.ᜄ.ToInt64() == 0L)
		{
			throw new OutOfMemoryException(RecordTableEnumerator.b("Ŀ㉁㑃⩅ⅇ⥉ⵋ㩍㥏㵑㩓癕⽗㭙⽛繝ᕟౡգѥѧཀྵ䱫ᩭὯ剱ᕳ᩵ᑷᕹύώꒃﲍ늑歹蓮", a_));
		}
	}

	// Token: 0x06003131 RID: 12593 RVA: 0x001C6DEC File Offset: 0x001C5DEC
	public void ᜀ(int A_0)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_116;
			case 1:
				goto IL_96;
			case 2:
				return;
			case 3:
				goto IL_AD;
			case 4:
				if (A_0 > 2147483647)
				{
					num = 0;
					continue;
				}
				this.ᜄ = Marshal.ReAllocCoTaskMem(this.ᜄ, A_0);
				this.ᜅ = A_0;
				num = 1;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_96;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 <= this.ᜅ)
			{
				num = 2;
				continue;
			}
			num = 4;
			continue;
			IL_96:
			if (this.ᜄ.ToInt64() != 0L)
			{
				return;
			}
			num = 3;
		}
		return;
		IL_AD:
		throw new OutOfMemoryException(RecordTableEnumerator.b("礷䨹䰻刽⤿⅁╃㉅ⅇ╉≋湍❏㍑❓癕ⵗ㑙㵛㱝౟ݡ䑣ብݧ䩩൫ɭᱯᵱᝳ᝵౷ό屻፽ꪉﾏﾓ", a_));
		IL_116:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷縹夻䴽⤿ぁ⅃≅ᭇ⍉㙋⭍", a_), RecordTableEnumerator.b("渷嬹倻䬽┿扁❃❅♇⑉⍋㩍灏けㅓ癕㽗⡙㥛㽝ᑟݡᙣ䙥ᱧɩ൫m偯", a_) + int.MaxValue);
	}

	// Token: 0x06003132 RID: 12594 RVA: 0x001C6F14 File Offset: 0x001C5F14
	public void ᜀ(byte[] A_0)
	{
		int a_ = 11;
		int num = 3;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (num2 == 0)
				{
					num = 2;
					continue;
				}
				goto IL_92;
			case 1:
				goto IL_3C;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					goto IL_76;
				}
				break;
			}
			goto IL_29;
			IL_2C:
			if (true)
			{
			}
			num = 1;
			continue;
			IL_29:
			if (A_0 == null)
			{
				goto IL_2C;
			}
			num2 = A_0.Length;
			num = 0;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄͆⡈㽊ⱌ", a_));
		IL_76:
		if (false)
		{
		}
		return;
		IL_92:
		this.ᜀ(num2);
		Marshal.Copy(A_0, 0, this.ᜄ, num2);
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x001C6FC8 File Offset: 0x001C5FC8
	public void ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 19;
		int num = 0;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				if (A_1 < 0)
				{
					num = 5;
					continue;
				}
				num2 = A_0.Length - A_1;
				num = 3;
				continue;
			case 2:
				return;
			case 3:
				goto IL_72;
			case 4:
				goto IL_62;
			case 5:
				goto IL_C7;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			if (true)
			{
			}
			num = 1;
			continue;
			IL_72:
			if (num2 != 0)
			{
				goto IL_C9;
			}
			num = 2;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("⡈㥊㽌୎ぐ❒㑔", a_));
		IL_C7:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁈ᡊ㥌⹎⍐❒᱔㥖㵘㹚╜", a_));
		IL_C9:
		this.ᜀ(num2);
		Marshal.Copy(A_0, A_1, this.ᜄ, num2);
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x001C70B4 File Offset: 0x001C60B4
	public void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 3;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_44;
			case 2:
			{
				int num2;
				if (num2 >= A_2)
				{
					goto IL_FF;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			}
			case 3:
				if (A_1 < 0)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 4:
				goto IL_E9;
			case 5:
				goto IL_71;
			case 6:
			{
				if (A_2 <= 0)
				{
					num = 0;
					continue;
				}
				int num2 = A_0.Length - A_1;
				num = 2;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_5D:
			num = 3;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似笾⁀㝂⑄", a_));
		IL_71:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸栺䤼帾㍀㝂ౄ⥆ⵈ⹊㕌", a_));
		IL_E9:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸砺刼䨾⽀㝂敄⹆㩈歊㥌⁎㹐獒㥔㙖⭘㱚㡜", a_));
		IL_FF:
		this.ᜀ(A_2);
		Marshal.Copy(A_0, A_1, this.ᜄ, A_2);
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x001C71D8 File Offset: 0x001C61D8
	public object ᜀ(Type A_0)
	{
		int a_ = 2;
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
			}
			IL_24:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尷弹伻䨽⤿ⱁ╃㉅ⅇ╉≋", a_));
		}
		return Marshal.PtrToStructure(this.ᜄ, A_0);
	}

	// Token: 0x06003136 RID: 12598 RVA: 0x001C7244 File Offset: 0x001C6244
	public void ᜀ(object A_0)
	{
		int a_ = 0;
		if (A_0 == null)
		{
			for (;;)
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
					goto IL_2C;
				}
			}
			IL_2C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("刵崷䤹䠻圽⸿⍁ぃ⽅❇⑉", a_));
		}
		Marshal.PtrToStructure(this.ᜄ, A_0);
	}

	// Token: 0x06003137 RID: 12599 RVA: 0x001C72B0 File Offset: 0x001C62B0
	public void ᜀ(byte[] A_0, object A_1)
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
		this.ᜀ(A_1);
	}

	// Token: 0x06003138 RID: 12600 RVA: 0x001C72FC File Offset: 0x001C62FC
	public void ᜀ(byte[] A_0, int A_1, object A_2)
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
		this.ᜀ(A_0, A_1);
		this.ᜀ(A_2);
	}

	// Token: 0x06003139 RID: 12601 RVA: 0x001C7348 File Offset: 0x001C6348
	public void ᜀ(byte[] A_0, int A_1, int A_2, object A_3)
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
		this.ᜀ(A_0, A_1, A_2);
		this.ᜀ(A_3);
	}

	// Token: 0x0600313A RID: 12602 RVA: 0x001C7394 File Offset: 0x001C6394
	public void ᜀ(object A_0, byte[] A_1, int A_2)
	{
		int a_ = 4;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6E;
			case 1:
				goto IL_7C;
			case 2:
				goto IL_E9;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			case 5:
				goto IL_62;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			if (true)
			{
			}
			num = 4;
			continue;
			IL_6E:
			if (A_1.Length >= A_2)
			{
				goto IL_EB;
			}
			num = 1;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹医䬽㈿⅁⅃", a_));
		IL_7C:
		throw new ArgumentException(RecordTableEnumerator.b("嬹主䰽п❁㝃㉅ⅇ⑉ⵋ㩍㥏㵑㩓", a_), RecordTableEnumerator.b("笹主䰽ℿ㭁摃㉅❇╉汋㵍㡏㵑♓≕", a_));
		IL_E9:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽п❁㝃㉅ⅇ⑉ⵋ㩍㥏㵑㩓", a_));
		IL_EB:
		this.ᜀ(A_2);
		Marshal.StructureToPtr(A_0, this.ᜄ, false);
		Marshal.Copy(this.ᜄ, A_1, 0, A_2);
		Marshal.DestroyStructure(this.ᜄ, A_0.GetType());
	}

	// Token: 0x040015B2 RID: 5554
	private const int ᜀ = 8228;

	// Token: 0x040015B3 RID: 5555
	private const int ᜁ = 1024;

	// Token: 0x040015B4 RID: 5556
	private const int ᜂ = 2147483647;

	// Token: 0x040015B5 RID: 5557
	private const string ᜃ = "Application was unable to allocate memory block";

	// Token: 0x040015B6 RID: 5558
	private IntPtr ᜄ;

	// Token: 0x040015B7 RID: 5559
	private int ᜅ;
}

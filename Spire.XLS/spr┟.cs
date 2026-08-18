using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x02000267 RID: 615
[CLSCompliant(false)]
internal abstract class spr\u251F : BiffRecordRaw, IDisposable
{
	// Token: 0x060024D3 RID: 9427 RVA: 0x00155D68 File Offset: 0x00154D68
	protected spr\u251F()
	{
		if (this.NeedDataArray)
		{
			this.ᜀ = new byte[0];
		}
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x00155D94 File Offset: 0x00154D94
	protected spr\u251F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x00155DAC File Offset: 0x00154DAC
	protected spr\u251F(BinaryReader A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x00155DC4 File Offset: 0x00154DC4
	protected spr\u251F(int A_0)
	{
		int a_ = 1;
		base..ctor(A_0);
		if (A_0 < 0)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶欸帺丼娾㍀㕂⁄", a_), RecordTableEnumerator.b("收尸䠺堼䴾㝀♂⅄杆⑈⹊⁌⁎⍐⩒畔㑖㙘⹚㍜⭞䅠๢ၤᑦᵨ䭪ཬ੮兰ᑲݴቶᡸེ᡼ൾꆀꮊﲒ뮔", a_));
		}
		this.ᜀ = new byte[A_0];
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x00155E18 File Offset: 0x00154E18
	protected virtual void ᜣ()
	{
		try
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
			this.ᜱ();
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x00155E74 File Offset: 0x00154E74
	public virtual byte[] ᜯ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜀ(ExcelVersion.Version97to2003);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_6B;
			}
			IL_1C:
			if (base.NeedInfill)
			{
				num = 1;
				continue;
			}
			break;
			IL_08:
			goto IL_1C;
		}
		IL_6B:
		return this.ᜀ;
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x00155EF4 File Offset: 0x00154EF4
	public virtual void ᜁ(byte[] A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				this.ᜀ = A_0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				num = 0;
				continue;
			}
			IL_1C:
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			break;
			IL_08:
			goto IL_1C;
		}
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x00155F6C File Offset: 0x00154F6C
	public virtual bool ᜭ()
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

	// Token: 0x060024DB RID: 9435 RVA: 0x00155FB0 File Offset: 0x00154FB0
	public virtual void ᜌ(bool A_0)
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

	// Token: 0x060024DC RID: 9436 RVA: 0x00155FF4 File Offset: 0x00154FF4
	public virtual int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, int A_3)
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
		return this.ᜀ(A_0, A_2, A_3);
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x0015603C File Offset: 0x0015503C
	public virtual int ᜀ(BinaryWriter A_0, IEncryptor A_1, int A_2)
	{
		int a_ = 18;
		int num = 14;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				this.ᜀ(ExcelVersion.Version97to2003);
				goto IL_B2;
			case 1:
				goto IL_21D;
			case 2:
				goto IL_21D;
			case 3:
				num2 = 0;
				goto IL_103;
			case 4:
				if (num3 > 0)
				{
					num = 5;
					continue;
				}
				goto IL_2A0;
			case 5:
				num = 11;
				continue;
			case 6:
				num = 9;
				continue;
			case 7:
				goto IL_126;
			case 8:
				goto IL_249;
			case 9:
				num2 = this.ᜀ.Length;
				goto IL_103;
			case 10:
			{
				int startDecodingOffset = this.StartDecodingOffset;
				A_1.Encrypt(this.ᜀ, startDecodingOffset, this.m_iLength - startDecodingOffset, (long)(A_2 + startDecodingOffset));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B2;
				default:
					if (false)
					{
					}
					num = 19;
					continue;
				}
				break;
			}
			case 11:
				if (A_1 != null)
				{
					num = 10;
					continue;
				}
				goto IL_12B;
			case 12:
				if (this.m_iLength < 0)
				{
					num = 8;
					continue;
				}
				A_0.Write((ushort)this.m_iCode);
				A_0.Write((ushort)this.m_iLength);
				A_2 += 4;
				num = 16;
				continue;
			case 13:
				goto IL_74;
			case 15:
				if (base.NeedInfill)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				base.NeedInfill = true;
				num = 1;
				continue;
			case 16:
				if (this.ᜀ != null)
				{
					num = 6;
					continue;
				}
				num = 3;
				continue;
			case 17:
				if (num3 < this.m_iLength)
				{
					num = 7;
					continue;
				}
				num = 4;
				continue;
			case 18:
				goto IL_149;
			case 19:
				goto IL_12B;
			}
			if (A_0 == null)
			{
				num = 13;
				continue;
			}
			num = 15;
			continue;
			IL_B2:
			num = 2;
			continue;
			IL_103:
			num3 = num2;
			num = 17;
			continue;
			IL_12B:
			A_0.Write(this.ᜀ, 0, this.m_iLength);
			num = 18;
			continue;
			IL_21D:
			num = 12;
		}
		IL_74:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_126:
		throw new ApplicationException(RecordTableEnumerator.b("ч⽉≋⥍⑏㩑瑓㥕㹗穙㡛㽝ᑟ͡䑣ཥ᭧䩩୫ᱭᕯ፱s፵੷婹ࡻᙽꒃﺉﲍﺏ뚕솟얡솣蚥쮧얩슫\udaad톯\udbb1\udab3억隷\udebb풽ꖿꇁ냃鳇돉볋ꯍ믑ꟓ", a_) + base.GetType().Name);
		IL_149:
		goto IL_2A0;
		IL_249:
		throw new ApplicationException(RecordTableEnumerator.b("὇㡉⍋⁍㝏牑ٓ㍕㭗㕙⹛㩝䁟١գብ१䩩իmᙯ᭱ᡳ᩵噷婹", a_) + base.TypeCode.ToString());
		IL_2A0:
		return this.m_iLength + 4;
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x001562F4 File Offset: 0x001552F4
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
				return;
			default:
			{
				if (false)
				{
				}
				this.ᜀ = new byte[A_2];
				this.m_iLength = A_2;
				A_0.CopyTo(A_1, this.ᜀ, 0, A_2);
				this.ᜂ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.NeedDataArray)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						this.ᜀ = new byte[0];
						this.AutoGrowData = true;
						num = 2;
						continue;
					case 2:
						return;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x060024DF RID: 9439
	public abstract void ᜀ(ExcelVersion A_0);

	// Token: 0x060024E0 RID: 9440 RVA: 0x001563A8 File Offset: 0x001553A8
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int a_ = 18;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.m_iLength > 0)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				return;
			case 1:
				A_0.WriteBytes(A_1, this.ᜀ, 0, this.m_iLength);
				num = 4;
				continue;
			case 2:
				goto IL_54;
			case 4:
				return;
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
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				this.ᜀ(ExcelVersion.Version97to2003);
				break;
			}
			num = 0;
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("㡇㡉⍋㡍㥏㙑ㅓ⑕", a_));
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x00156474 File Offset: 0x00155474
	protected void ᜂ(int A_0, int A_1)
	{
		int a_ = 18;
		for (;;)
		{
			if (true)
			{
			}
			int num = this.ᜀ.Length;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (A_1 > num)
					{
						num2 = 3;
						continue;
					}
					num2 = 9;
					continue;
				case 1:
					num2 = 4;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_95;
					default:
						if (false)
						{
						}
						if (A_0 >= 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_B1;
					}
					break;
				case 3:
					goto IL_AF;
				case 4:
					if (A_0 > num)
					{
						num2 = 8;
						continue;
					}
					num2 = 7;
					continue;
				case 5:
					goto IL_12E;
				case 6:
					goto IL_95;
				case 7:
					if (A_1 >= 0)
					{
						num2 = 6;
						continue;
					}
					goto IL_7C;
				case 8:
					goto IL_110;
				case 9:
					if (A_1 + A_0 > num)
					{
						num2 = 5;
						continue;
					}
					return;
				}
				break;
				IL_95:
				num2 = 0;
			}
		}
		IL_7C:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⑇⽉≋⥍⑏㩑", a_), "");
		IL_AF:
		goto IL_7C;
		IL_B1:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("❇ⱉ⩋㵍㕏♑", a_), "");
		IL_110:
		goto IL_B1;
		IL_12E:
		throw new ArgumentException(RecordTableEnumerator.b("ч⽉≋⥍⑏㩑瑓㥕⩗穙㍛㡝ٟᅡţብ䡧ɩ൫ᵭ偯ձٳ᥵ᙷᵹ屻ࡽꚇ", a_), RecordTableEnumerator.b("⑇⽉≋⥍⑏㩑瑓灕硗㕙㩛㡝፟ݡၣ", a_));
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x001565D4 File Offset: 0x001555D4
	protected new byte[] ᜃ(int A_0, int A_1)
	{
		if (A_1 != 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				this.ᜂ(A_0, A_1);
				byte[] array = new byte[A_1];
				Buffer.BlockCopy(this.ᜀ, A_0, array, 0, A_1);
				return array;
			}
			}
		}
		if (true)
		{
		}
		return new byte[0];
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x0015663C File Offset: 0x0015563C
	protected byte ᜎ(int A_0)
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
		this.ᜂ(A_0, 1);
		return this.ᜀ[A_0];
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x00156688 File Offset: 0x00155688
	protected ushort ᜌ(int A_0)
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
		this.ᜂ(A_0, 2);
		return BitConverter.ToUInt16(this.ᜀ, A_0);
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x001566D8 File Offset: 0x001556D8
	protected short ᜐ(int A_0)
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
		this.ᜂ(A_0, 2);
		return BitConverter.ToInt16(this.ᜀ, A_0);
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x00156728 File Offset: 0x00155728
	protected int ᜑ(int A_0)
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
		this.ᜂ(A_0, 4);
		return BitConverter.ToInt32(this.ᜀ, A_0);
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00156778 File Offset: 0x00155778
	protected uint \u1714(int A_0)
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
		this.ᜂ(A_0, 4);
		return BitConverter.ToUInt32(this.ᜀ, A_0);
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x001567C8 File Offset: 0x001557C8
	protected long \u1712(int A_0)
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
		this.ᜂ(A_0, 28);
		return BitConverter.ToInt64(this.ᜀ, A_0);
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x00156818 File Offset: 0x00155818
	protected ulong ᜋ(int A_0)
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
		this.ᜂ(A_0, 8);
		return BitConverter.ToUInt64(this.ᜀ, A_0);
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00156868 File Offset: 0x00155868
	protected float \u1713(int A_0)
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
		this.ᜂ(A_0, 4);
		return BitConverter.ToSingle(this.ᜀ, A_0);
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x001568B8 File Offset: 0x001558B8
	protected double \u170D(int A_0)
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
		this.ᜂ(A_0, 8);
		return BitConverter.ToDouble(this.ᜀ, A_0);
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x00156908 File Offset: 0x00155908
	protected bool ᜁ(int A_0, int A_1)
	{
		int a_ = 1;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_9E;
			case 3:
				if (A_1 <= 7)
				{
					goto IL_A0;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A0;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (A_1 < 0)
			{
				break;
			}
			num = 0;
		}
		IL_41:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唶倸伺洼倾㉀", a_), RecordTableEnumerator.b("甶倸伺ᴼ漾⹀あⱄ㍆⁈⑊⍌潎㉐㉒㭔睖㭘㹚絜╞Ѡᅢ੤୦౨ᡪṬ佮ṰŲ啴ၶ୸Ṻᱼ୾ꖄ꾎Ꚑ붒", a_));
		IL_9E:
		goto IL_41;
		IL_A0:
		this.ᜂ(A_0, 1);
		return ((int)this.ᜀ[A_0] & 1 << A_1) != 0;
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x001569D4 File Offset: 0x001559D4
	protected string ᜀ(ref int A_0, out bool A_1)
	{
		if (true)
		{
		}
		int num = (int)this.ᜌ(A_0);
		A_0 += 2;
		A_1 = false;
		if (num <= 0)
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
				A_0++;
				return string.Empty;
			}
		}
		int num2;
		string result = this.ᜀ(A_0, num, out num2);
		A_0 += num2 + 1;
		A_1 = (num2 == num);
		return result;
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x00156A50 File Offset: 0x00155A50
	protected string ᜋ(ref int A_0)
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
		bool flag;
		return this.ᜀ(ref A_0, out flag);
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x00156A94 File Offset: 0x00155A94
	protected string ᜀ(ref int A_0, int A_1)
	{
		if (A_1 <= 0)
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
				return string.Empty;
			}
		}
		int num;
		string result = this.ᜀ(A_0, A_1, out num);
		A_0 += num + 1;
		return result;
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00156AF0 File Offset: 0x00155AF0
	protected string ᜏ(int A_0)
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
		int a_ = (int)this.ᜀ[A_0];
		return this.ᜅ(A_0 + 1, a_);
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x00156B40 File Offset: 0x00155B40
	protected string ᜀ(int A_0, out int A_1)
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
		int a_ = (int)this.ᜀ[A_0];
		return this.ᜀ(A_0 + 1, a_, out A_1);
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x00156B90 File Offset: 0x00155B90
	protected internal string ᜅ(int A_0, int A_1)
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
		int num;
		return this.ᜀ(A_0, A_1, out num);
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x00156BD8 File Offset: 0x00155BD8
	protected internal string ᜀ(int A_0, int A_1, out int A_2)
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
		return this.ᜀ(A_0, A_1, out A_2, false);
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x00156C20 File Offset: 0x00155C20
	protected internal string ᜀ(int A_0, int A_1, out int A_2, bool A_3)
	{
		int a_ = 7;
		for (;;)
		{
			byte b = this.ᜀ[A_0];
			int num = 3;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 1:
					if (num2 > this.m_iLength)
					{
						num = 2;
						continue;
					}
					num = 8;
					continue;
				case 2:
					goto IL_145;
				case 3:
					if (b != 0)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_91;
				case 4:
					num = 11;
					continue;
				case 5:
					num3 = 2 * A_1;
					goto IL_11C;
				case 6:
					goto IL_91;
				case 7:
					num3 = A_1;
					goto IL_11C;
				case 8:
					if (b == 0)
					{
						num = 9;
						continue;
					}
					num = 0;
					continue;
				case 9:
					A_2 = A_1;
					this.ᜂ(A_0 + 1, A_1);
					num = 10;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_123;
					default:
						goto IL_FE;
					}
					break;
				case 11:
					if (A_3)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				}
				break;
				IL_91:
				num = 7;
				continue;
				IL_123:
				num = 1;
				continue;
				IL_11C:
				num2 = num3;
				num2 += A_0 + 1;
				goto IL_123;
			}
		}
		IL_6F:
		A_2 = (A_3 ? A_1 : (A_1 * 2));
		this.ᜂ(A_0 + 1, A_2);
		return Encoding.Unicode.GetString(this.ᜀ, A_0 + 1, A_2);
		IL_FE:
		if (false)
		{
		}
		return BiffRecordRaw.LatinEncoding.GetString(this.ᜀ, A_0 + 1, A_1);
		IL_145:
		throw new sprῩ(string.Format(RecordTableEnumerator.b("渼䬾㍀⩂⭄⁆楈⩊⍌⭎煐㹒੔㍖㡘⽚㱜罞`ᅢᝤ٦ၨ䭪६n兰ᵲᩴͶ奸ᵺᑼ୾ꆀꮊﮎ戀랖ꮚ놞", a_), base.TypeCode));
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00156DCC File Offset: 0x00155DCC
	protected string ᜀ(int A_0, IList<int> A_1, int A_2, ref int A_3, out int A_4, out byte[] A_5, out byte[] A_6)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string text = null;
				int num = 3;
				A_5 = null;
				A_6 = null;
				ushort num2 = BitConverter.ToUInt16(this.ᜀ, A_0);
				byte b = this.ᜀ[A_0 + 2];
				bool flag = (b & 1) == 1;
				bool flag2 = (b & 4) != 0;
				bool flag3 = (b & 8) != 0;
				int num3 = 3;
				short num4 = 0;
				int num5 = 0;
				int num6 = 8;
				for (;;)
				{
					int num7;
					int num8;
					int num10;
					int num9;
					Encoding encoding;
					string text2;
					int num12;
					Encoding encoding2;
					string text3;
					Encoding encoding3;
					switch (num6)
					{
					case 0:
						goto IL_4FC;
					case 1:
						goto IL_456;
					case 2:
						if (text == null)
						{
							num6 = 35;
							continue;
						}
						return text;
					case 3:
						goto IL_4BD;
					case 4:
						if (this.ᜀ[num7 + num8] == 1)
						{
							num6 = 26;
							continue;
						}
						goto IL_43C;
					case 5:
						num4 = this.ᜐ(A_0 + num3);
						num3 += 2;
						num += 2;
						num6 = 9;
						continue;
					case 6:
						num9 = (int)num2 - num10;
						goto IL_257;
					case 7:
						goto IL_456;
					case 8:
						if (true)
						{
						}
						if (flag3)
						{
							num6 = 5;
							continue;
						}
						goto IL_171;
					case 9:
						goto IL_171;
					case 10:
						encoding = Encoding.Unicode;
						goto IL_422;
					case 11:
						A_6 = this.ᜃ(A_0 + num, num5);
						num += num5;
						num6 = 0;
						continue;
					case 12:
						goto IL_43C;
					case 13:
						if (!flag)
						{
							num6 = 17;
							continue;
						}
						num6 = 31;
						continue;
					case 14:
					{
						string @string;
						text2 = @string;
						goto IL_2E3;
					}
					case 15:
					{
						int num11 = (int)(num4 * 4);
						A_5 = this.ᜃ(A_0 + num, num11);
						num += num11;
						num6 = 3;
						continue;
					}
					case 16:
					{
						if (num12 <= num8)
						{
							num6 = 38;
							continue;
						}
						string string2 = encoding2.GetString(this.ᜀ, num7, num8);
						num6 = 46;
						continue;
					}
					case 17:
						num6 = 42;
						continue;
					case 18:
					{
						string string2;
						text3 = string2;
						goto IL_350;
					}
					case 19:
						goto IL_478;
					case 20:
						if (flag3)
						{
							num6 = 15;
							continue;
						}
						goto IL_4BD;
					case 21:
						num6 = 45;
						continue;
					case 22:
						if (num10 >= (int)num2)
						{
							num6 = 19;
							continue;
						}
						num6 = 36;
						continue;
					case 23:
						num5 = this.ᜑ(A_0 + num3);
						num3 += 4;
						num += 4;
						num6 = 24;
						continue;
					case 24:
						goto IL_1B3;
					case 25:
						if (flag2)
						{
							num6 = 23;
							continue;
						}
						goto IL_1B3;
					case 26:
						goto IL_3CD;
					case 27:
						if (!flag)
						{
							num6 = 21;
							continue;
						}
						num6 = 10;
						continue;
					case 28:
						num9 = ((int)num2 - num10) * 2;
						goto IL_257;
					case 29:
						num6 = 4;
						continue;
					case 30:
						num10 += (flag ? (num8 / 2) : num8);
						num6 = 43;
						continue;
					case 31:
						encoding3 = Encoding.Unicode;
						goto IL_200;
					case 32:
						goto IL_2BD;
					case 33:
					{
						string string2;
						text3 = text + string2;
						goto IL_350;
					}
					case 34:
						num6 = 33;
						continue;
					case 35:
						goto IL_537;
					case 36:
						if (!flag)
						{
							num6 = 39;
							continue;
						}
						num6 = 28;
						continue;
					case 37:
						if (flag2)
						{
							num6 = 11;
							continue;
						}
						goto IL_4FC;
					case 38:
					{
						string @string = encoding2.GetString(this.ᜀ, num7, num12);
						num6 = 40;
						continue;
					}
					case 39:
						num6 = 6;
						continue;
					case 40:
						if (text != null)
						{
							num6 = 32;
							continue;
						}
						num6 = 14;
						continue;
					case 41:
					{
						string @string;
						text2 = text + @string;
						goto IL_2E3;
					}
					case 42:
						encoding3 = BiffRecordRaw.LatinEncoding;
						goto IL_200;
					case 43:
						if (this.ᜀ[num7 + num8] != 0)
						{
							num6 = 29;
							continue;
						}
						goto IL_3CD;
					case 44:
						goto IL_478;
					case 45:
						encoding = BiffRecordRaw.LatinEncoding;
						goto IL_422;
					case 46:
						if (text != null)
						{
							num6 = 34;
							continue;
						}
						num6 = 18;
						continue;
					}
					break;
					IL_171:
					num6 = 25;
					continue;
					IL_1B3:
					num7 = A_0 + num3;
					num10 = 0;
					num6 = 13;
					continue;
					IL_200:
					encoding2 = encoding3;
					num6 = 1;
					continue;
					IL_257:
					num12 = num9;
					int num13 = BiffRecordRaw.FindNextBreak(A_1, A_2, num7, ref A_3);
					num8 = num13 - num7;
					num6 = 16;
					continue;
					IL_2BD:
					num6 = 41;
					continue;
					IL_4FC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2BD;
					default:
						if (false)
						{
						}
						A_4 = num;
						num6 = 2;
						continue;
					}
					IL_2E3:
					text = text2;
					num += num12;
					num6 = 44;
					continue;
					IL_350:
					text = text3;
					num6 = 30;
					continue;
					IL_3CD:
					flag = (this.ᜀ[num7 + num8] == 1);
					num6 = 27;
					continue;
					IL_422:
					encoding2 = encoding;
					num7++;
					num++;
					num6 = 12;
					continue;
					IL_43C:
					num7 += num8;
					num += num8;
					num6 = 7;
					continue;
					IL_456:
					num6 = 22;
					continue;
					IL_478:
					num6 = 20;
					continue;
					IL_4BD:
					num6 = 37;
				}
			}
			IL_537:
			return string.Empty;
		}
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x0015731C File Offset: 0x0015631C
	protected TAddr \u1716(int A_0)
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
		return new TAddr
		{
			FirstRow = (int)this.ᜌ(A_0),
			LastRow = (int)this.ᜌ(A_0 + 2),
			FirstCol = (int)this.ᜌ(A_0 + 4),
			LastCol = (int)this.ᜌ(A_0 + 6)
		};
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x001573A0 File Offset: 0x001563A0
	protected Rectangle ᜊ(int A_0)
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
		int top = (int)this.ᜌ(A_0);
		int bottom = (int)this.ᜌ(A_0 + 2);
		int left = (int)this.ᜌ(A_0 + 4);
		int right = (int)this.ᜌ(A_0 + 6);
		return Rectangle.FromLTRB(left, top, right, bottom);
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x0015740C File Offset: 0x0015640C
	protected void ᜄ(int A_0, int A_1)
	{
		int num = 5;
		for (;;)
		{
			int num2;
			byte[] dst;
			int num3;
			int num4;
			switch (num)
			{
			case 0:
				num2 = 0;
				goto IL_FA;
			case 1:
				Buffer.BlockCopy(this.ᜀ, 0, dst, 0, num3);
				num = 3;
				continue;
			case 2:
				dst = new byte[num4];
				num = 4;
				continue;
			case 3:
				goto IL_74;
			case 4:
				if (num3 > 0)
				{
					goto IL_160;
				}
				goto IL_74;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_160;
				default:
					if (false)
					{
					}
					if (num4 > num3)
					{
						num = 2;
						continue;
					}
					return;
				}
				break;
			case 7:
				if (A_0 + A_1 > this.ᜀ.Length)
				{
					num = 13;
					continue;
				}
				return;
			case 8:
				if (true)
				{
				}
				num = 7;
				continue;
			case 9:
				num2 = this.ᜀ.Length;
				goto IL_FA;
			case 10:
				return;
			case 11:
				if (this.ᜀ != null)
				{
					num = 12;
					continue;
				}
				num = 0;
				continue;
			case 12:
				num = 9;
				continue;
			case 13:
				goto IL_5A;
			}
			if (this.ᜀ != null)
			{
				num = 8;
				continue;
			}
			IL_5A:
			num = 11;
			continue;
			IL_74:
			this.ᜀ = dst;
			num = 10;
			continue;
			IL_FA:
			num3 = num2;
			num4 = Math.Min(A_0 * 2 + A_1 + 16, this.MaximumMemorySize);
			num = 6;
			continue;
			IL_160:
			num = 1;
		}
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x00157594 File Offset: 0x00156594
	protected internal void \u1715(int A_0)
	{
		if (this.ᜀ.Length > A_0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_23;
				}
			}
			IL_23:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
		}
		byte[] dst = new byte[A_0];
		Buffer.BlockCopy(this.ᜀ, 0, dst, 0, this.ᜀ.Length);
		this.ᜀ = dst;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x00157604 File Offset: 0x00156604
	protected internal void ᜀ(int A_0, byte[] A_1, int A_2, int A_3)
	{
		int a_ = 10;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.AutoGrowData)
				{
					num = 1;
					continue;
				}
				num = 11;
				continue;
			case 1:
				this.ᜄ(A_0, A_3);
				num = 4;
				continue;
			case 2:
				if (A_2 < 0)
				{
					num = 12;
					continue;
				}
				num = 8;
				continue;
			case 3:
				if (A_2 + A_3 > A_1.Length)
				{
					num = 10;
					continue;
				}
				num = 0;
				continue;
			case 4:
				goto IL_142;
			case 6:
				goto IL_7C;
			case 7:
				goto IL_1ED;
			case 8:
				if (A_3 < 0)
				{
					num = 7;
					continue;
				}
				num = 3;
				continue;
			case 9:
				goto IL_10E;
			case 10:
				goto IL_C0;
			case 11:
				if (A_0 + A_3 > this.ᜀ.Length)
				{
					num = 9;
					continue;
				}
				goto IL_1F2;
			case 12:
				goto IL_E2;
			}
			if (A_1 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_147;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 6;
					break;
				}
			}
			else
			{
				num = 2;
			}
		}
		IL_7C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㘿⍁⡃㍅ⵇ", a_));
		IL_C0:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㘿⍁⡃㍅ⵇ", a_), RecordTableEnumerator.b("ဿⵁ㝃⽅㱇⍉⍋⁍灏㵑♓癕㑗㽙㉛㥝ᑟ੡䑣๥१ᥩ䱫ᥭɯᵱᩳᅵ塷౹ᵻችꪃ", a_));
		IL_E2:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("〿ⵁ㝃", a_), RecordTableEnumerator.b("ဿⵁ㝃⽅㱇⍉⍋⁍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ᱥ൧ᡩͫɭᕯűݳ塵", a_));
		IL_10E:
		goto IL_147;
		IL_142:
		goto IL_1F2;
		IL_147:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⴿᵁ⁃❅㱇⭉", a_), RecordTableEnumerator.b("िⱁぃ⍅㩇⑉ⵋ≍灏㍑♓⑕㥗⍙籛ⵝय़ᡡţ䙥ŧᥩ䱫ᩭὯᵱ味յᕷ᭹ၻች깿", a_));
		IL_1ED:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⰿ❁⩃ⅅ㱇≉", a_), RecordTableEnumerator.b("ి❁⩃ⅅ㱇≉汋⅍㙏牑こ㝕ⱗ㭙籛⩝ཟ䉡ݣ॥ᡧ፩䱫ͭկűs噵᩷ό屻᥽겋揄望뚕ﾙ躟", a_));
		IL_1F2:
		Buffer.BlockCopy(A_1, A_2, this.ᜀ, A_0, A_3);
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x00157814 File Offset: 0x00156814
	protected internal void ᜁ(int A_0, byte[] A_1)
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
		this.ᜀ(A_0, A_1, 0, A_1.Length);
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x0015785C File Offset: 0x0015685C
	protected internal void ᜀ(int A_0, byte A_1)
	{
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_50;
			case 2:
				this.ᜄ(A_0, 1);
				num = 1;
				continue;
			}
			if (!this.AutoGrowData)
			{
				goto IL_6E;
			}
			num = 2;
		}
		IL_50:
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
		IL_6E:
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x001578E0 File Offset: 0x001568E0
	protected internal void ᜀ(int A_0, byte A_1, int A_2)
	{
		byte[] array;
		for (;;)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				num = 0;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				array = new byte[A_2];
				num2 = 0;
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_63;
				case 1:
					if (num2 >= A_2)
					{
						goto IL_5B;
					}
					array[num2] = A_1;
					num2++;
					num = 2;
					continue;
				case 2:
					goto IL_4F;
				case 3:
					goto IL_4F;
				}
				break;
				IL_4F:
				num = 1;
			}
		}
		IL_63:
		this.ᜀ(A_0, array, 0, A_2);
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x00157978 File Offset: 0x00156978
	protected internal void ᜀ(int A_0, ushort A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜄ(A_0, 2);
				num = 1;
				continue;
			case 1:
				goto IL_48;
			}
			if (!this.AutoGrowData)
			{
				goto IL_6E;
			}
			num = 0;
		}
		IL_48:
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
			break;
		}
		IL_6E:
		byte b = (byte)(A_1 & 255);
		byte b2 = (byte)(A_1 >> 8 & 255);
		this.ᜀ[A_0] = b;
		this.ᜀ[A_0 + 1] = b2;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x00157A1C File Offset: 0x00156A1C
	protected internal void ᜀ(int A_0, short A_1)
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
		this.ᜀ(A_0, BitConverter.GetBytes(A_1), 0, 2);
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x00157A68 File Offset: 0x00156A68
	protected internal void ᜆ(int A_0, int A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.ᜄ(A_0, 4);
				num = 1;
				continue;
			case 1:
				goto IL_50;
			}
			if (!this.AutoGrowData)
			{
				goto IL_6E;
			}
			num = 0;
		}
		IL_50:
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
		IL_6E:
		Buffer.BlockCopy(BitConverter.GetBytes(A_1), 0, this.ᜀ, A_0, 4);
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x00157AF8 File Offset: 0x00156AF8
	protected internal void ᜀ(int A_0, uint A_1)
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
		this.ᜀ(A_0, BitConverter.GetBytes(A_1), 0, 4);
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x00157B44 File Offset: 0x00156B44
	protected internal void ᜀ(int A_0, long A_1)
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
		this.ᜀ(A_0, BitConverter.GetBytes(A_1), 0, 8);
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x00157B90 File Offset: 0x00156B90
	protected internal void ᜀ(int A_0, ulong A_1)
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
		this.ᜀ(A_0, BitConverter.GetBytes(A_1), 0, 8);
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x00157BDC File Offset: 0x00156BDC
	protected internal void ᜀ(int A_0, float A_1)
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
		this.ᜀ(A_0, BitConverter.GetBytes(A_1), 0, 4);
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x00157C28 File Offset: 0x00156C28
	protected internal void ᜀ(int A_0, double A_1)
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
		this.ᜀ(A_0, BitConverter.GetBytes(A_1), 0, 8);
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x00157C74 File Offset: 0x00156C74
	protected internal void ᜀ(int A_0, bool A_1, int A_2)
	{
		int a_ = 5;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.AutoGrowData)
				{
					num = 5;
					continue;
				}
				goto IL_56;
			case 1:
				if (A_1)
				{
					num = 3;
					continue;
				}
				goto IL_125;
			case 2:
				num = 8;
				continue;
			case 3:
				goto IL_6C;
			case 4:
				goto IL_BE;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					this.ᜄ(A_0, 1);
					num = 6;
					continue;
				}
				break;
			case 6:
				goto IL_56;
			case 8:
				if (A_2 > 7)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_2 >= 0)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			goto IL_EB;
			IL_56:
			num = 1;
		}
		IL_6C:
		byte[] array = this.ᜀ;
		array[A_0] |= (byte)(1 << A_2);
		return;
		IL_BE:
		IL_EB:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夺吼䬾ᅀⱂ㙄", a_), RecordTableEnumerator.b("示吼䬾慀ፂ⩄㑆⁈㽊⑌⁎㽐獒㙔㙖㝘筚㽜㩞䅠ᥢdᕦ٨䭪ɬᵮ兰ᑲݴቶᡸེ᡼ൾꆀꮊ몌ꆎ", a_));
		IL_125:
		byte[] array2 = this.ᜀ;
		array2[A_0] &= (byte)(~(byte)(1 << A_2));
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x00157DC8 File Offset: 0x00156DC8
	protected internal void ᜀ(ref int A_0, string A_1, bool A_2)
	{
		int num = 9;
		byte[] bytes;
		for (;;)
		{
			Encoding encoding2;
			Encoding encoding;
			switch (num)
			{
			case 0:
				encoding = encoding2;
				goto IL_F3;
			case 1:
				goto IL_64;
			case 2:
				goto IL_56;
			case 3:
				if (!A_2)
				{
					num = 10;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 4:
				num = 1;
				continue;
			case 5:
				goto IL_9C;
			case 6:
				if (true)
				{
				}
				encoding = Encoding.Unicode;
				goto IL_F3;
			case 7:
				if (!A_2)
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
			case 8:
				num = 11;
				continue;
			case 10:
				num = 6;
				continue;
			case 11:
				if (A_1.Length == 0)
				{
					num = 5;
					continue;
				}
				goto IL_9E;
			}
			if (A_1 != null)
			{
				num = 8;
				continue;
			}
			break;
			IL_9E:
			encoding2 = Encoding.Default;
			num = 3;
			continue;
			IL_F3:
			encoding2 = encoding;
			bytes = encoding2.GetBytes(A_1);
			num = 7;
		}
		return;
		IL_56:
		byte b = 0;
		goto IL_11D;
		IL_64:
		b = 1;
		goto IL_11D;
		IL_9C:
		return;
		IL_11D:
		byte a_ = b;
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0 + 1, bytes, 0, bytes.Length);
		A_0 += bytes.Length + 1;
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x00157F18 File Offset: 0x00156F18
	protected internal int ᜂ(int A_0, string A_1)
	{
		if (spr\u251F.ᜀ(A_1))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_20;
				}
			}
			IL_20:
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜁ(A_0, A_1, true, true);
		}
		return this.ᜁ(A_0, A_1);
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x00157F70 File Offset: 0x00156F70
	internal static bool ᜀ(string A_0)
	{
		bool result;
		for (;;)
		{
			result = true;
			int num = 7;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					num = 9;
					continue;
				case 1:
					return result;
				case 2:
					return result;
				case 3:
					goto IL_87;
				case 4:
					if (A_0[num2] > '\u007f')
					{
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						if (false)
						{
						}
						num2++;
						num = 5;
						continue;
					}
					break;
				case 5:
					if (true)
					{
					}
					goto IL_87;
				case 6:
					goto IL_92;
				case 7:
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					num = 10;
					continue;
				case 8:
					result = false;
					num = 2;
					continue;
				case 9:
					num3 = 0;
					goto IL_F0;
				case 10:
					num3 = A_0.Length;
					goto IL_F0;
				}
				break;
				IL_92:
				int num4;
				if (num2 >= num4)
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
				IL_87:
				num = 6;
				continue;
				IL_F0:
				num4 = num3;
				num2 = 0;
				num = 3;
			}
		}
		return result;
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x00158080 File Offset: 0x00157080
	protected internal int ᜁ(int A_0, string A_1)
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
		return this.ᜁ(A_0, A_1, false, false);
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x001580C8 File Offset: 0x001570C8
	protected internal int ᜁ(int A_0, string A_1, bool A_2, bool A_3)
	{
		int num = 4;
		byte[] bytes;
		for (;;)
		{
			Encoding encoding;
			switch (num)
			{
			case 0:
				if (A_2)
				{
					num = 7;
					continue;
				}
				return 0;
			case 1:
				if (A_1.Length == 0)
				{
					num = 12;
					continue;
				}
				num = 6;
				continue;
			case 2:
				goto IL_65;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_106;
				}
				break;
			case 5:
				encoding = Encoding.Default;
				goto IL_10F;
			case 6:
				if (!A_3)
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
			case 7:
				goto IL_C7;
			case 8:
				num = 3;
				continue;
			case 9:
				if (!A_3)
				{
					num = 8;
					continue;
				}
				if (true)
				{
				}
				num = 2;
				continue;
			case 10:
				num = 1;
				continue;
			case 11:
				num = 13;
				continue;
			case 12:
				goto IL_AE;
			case 13:
				encoding = Encoding.Unicode;
				goto IL_10F;
			}
			if (A_1 != null)
			{
				num = 10;
				continue;
			}
			IL_AE:
			num = 0;
			continue;
			IL_10F:
			Encoding encoding2 = encoding;
			bytes = encoding2.GetBytes(A_1);
			num = 9;
		}
		IL_65:
		byte b = 0;
		goto IL_14B;
		IL_C7:
		this.ᜀ(A_0, 0);
		return 1;
		IL_106:
		if (false)
		{
		}
		b = 1;
		IL_14B:
		byte a_ = b;
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0 + 1, bytes, 0, bytes.Length);
		return bytes.Length + 1;
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x00158240 File Offset: 0x00157240
	protected internal int ᜀ(int A_0, string A_1)
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
		this.ᜀ(A_0, (byte)A_1.Length);
		return this.ᜁ(A_0 + 1, A_1) + 1;
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x00158298 File Offset: 0x00157298
	protected internal new int ᜃ(int A_0, string A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_73;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_63;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_63;
			}
			if (A_1 == null)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_63:
		ushort num2 = (ushort)A_1.Length;
		goto IL_76;
		IL_73:
		num2 = 0;
		IL_76:
		ushort a_ = num2;
		this.ᜀ(A_0, a_);
		return 2 + this.ᜁ(A_0 + 2, A_1);
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x00158334 File Offset: 0x00157334
	protected internal int ᜀ(int A_0, string A_1, bool A_2, bool A_3)
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
		this.ᜀ(A_0, (ushort)A_1.Length);
		return 2 + this.ᜁ(A_0 + 2, A_1, A_2, A_3);
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x0015838C File Offset: 0x0015738C
	protected internal void ᜀ(ref int A_0, string A_1)
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
		this.ᜀ(A_0, (ushort)A_1.Length);
		A_0 += 2;
		this.ᜀ(ref A_0, A_1, false);
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x001583E8 File Offset: 0x001573E8
	protected internal void ᜁ(ref int A_0, string A_1, bool A_2)
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
		this.ᜀ(A_0, (ushort)A_1.Length);
		A_0 += 2;
		this.ᜀ(ref A_0, A_1, A_2);
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x00158444 File Offset: 0x00157444
	protected internal void ᜀ(int A_0, TAddr A_1)
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
		this.ᜀ(A_0, (ushort)A_1.FirstRow);
		this.ᜀ(A_0 + 2, (ushort)A_1.LastRow);
		this.ᜀ(A_0 + 4, (ushort)A_1.FirstCol);
		this.ᜀ(A_0 + 6, (ushort)A_1.LastCol);
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x001584C4 File Offset: 0x001574C4
	protected internal void ᜀ(int A_0, Rectangle A_1)
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
		this.ᜀ(A_0, (ushort)A_1.Top);
		this.ᜀ(A_0 + 2, (ushort)A_1.Bottom);
		this.ᜀ(A_0 + 4, (ushort)A_1.Left);
		this.ᜀ(A_0 + 6, (ushort)A_1.Right);
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x00158544 File Offset: 0x00157544
	protected SortedList<spr\u2429, FieldInfo> ᜬ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 8;
			SortedList<spr\u2429, FieldInfo> sortedList;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_165;
				case 1:
					goto IL_72;
				case 2:
					BiffRecordRaw.ᜃ[this.m_iCode] = sortedList;
					num = 3;
					continue;
				case 3:
					return sortedList;
				case 4:
				{
					object[] customAttributes;
					if (customAttributes.Length > 0)
					{
						num = 5;
						continue;
					}
					goto IL_165;
				}
				case 5:
				{
					object[] customAttributes;
					FieldInfo fieldInfo;
					sortedList.Add((spr\u2429)customAttributes[0], fieldInfo);
					num = 0;
					continue;
				}
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_165;
					default:
					{
						if (false)
						{
						}
						Type type = base.GetType();
						FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
						sortedList = new SortedList<spr\u2429, FieldInfo>(new spr\u19AF());
						num2 = 0;
						int num3 = fields.Length;
						num = 1;
						continue;
					}
					}
					break;
				case 7:
					goto IL_72;
				case 9:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 2;
						continue;
					}
					FieldInfo[] fields;
					FieldInfo fieldInfo = fields[num2];
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(spr\u2429), true);
					num = 4;
					continue;
				}
				}
				if (!BiffRecordRaw.ᜃ.TryGetValue(this.m_iCode, out sortedList))
				{
					num = 6;
					continue;
				}
				break;
				IL_72:
				num = 9;
				continue;
				IL_165:
				num2++;
				num = 7;
			}
			return sortedList;
		}
		}
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x001586CC File Offset: 0x001576CC
	protected void ᜰ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_73:
			if (true)
			{
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		IList<spr\u2429> keys;
		IList<FieldInfo> values;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_92;
			case 1:
			{
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				spr\u2429 a_ = keys[num2];
				FieldInfo fieldInfo = values[num2];
				fieldInfo.SetValue(this, this.ᜀ(a_));
				num2++;
				num = 3;
				continue;
			}
			case 2:
				goto IL_71;
			case 3:
				goto IL_D0;
			}
			goto IL_43;
		}
		IL_71:
		goto IL_73;
		IL_92:
		Debug.IndentLevel = 0;
		return;
		IL_D0:
		goto IL_73;
		IL_43:
		SortedList<spr\u2429, FieldInfo> sortedList = this.ᜬ();
		Debug.IndentLevel = 1;
		keys = sortedList.Keys;
		values = sortedList.Values;
		num2 = 0;
		count = sortedList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x001587B4 File Offset: 0x001577B4
	protected object ᜀ(spr\u2429 A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int a_2;
			byte b;
			ushort num5;
			ushort num6;
			for (;;)
			{
				IL_CB:
				a_2 = A_0.ᜄ();
				int num = 18;
				for (;;)
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
						switch (num)
						{
						case 0:
							goto IL_18D;
						case 1:
							if (A_0.ᜆ())
							{
								num = 21;
								continue;
							}
							num = 29;
							continue;
						case 2:
							goto IL_420;
						case 3:
						{
							b = this.ᜀ[A_0.ᜄ()];
							int num2 = A_0.ᜄ() + (int)b;
							num = 13;
							continue;
						}
						case 4:
						{
							if (A_0.ᜃ())
							{
								num = 17;
								continue;
							}
							int num3 = A_0.ᜁ();
							num = 7;
							continue;
						}
						case 5:
							if (A_0.ᜀ())
							{
								num = 3;
								continue;
							}
							num = 4;
							continue;
						case 6:
							goto IL_42E;
						case 7:
						{
							int num3;
							switch (num3)
							{
							case 1:
								goto IL_366;
							case 2:
								num = 28;
								continue;
							case 3:
								goto IL_50B;
							case 4:
								goto IL_4E3;
							default:
								num = 19;
								continue;
							}
							break;
						}
						case 8:
							goto IL_2C0;
						case 9:
							if (A_0.ᜅ())
							{
								num = 16;
								continue;
							}
							goto IL_310;
						case 10:
							goto IL_244;
						case 11:
							goto IL_506;
						case 12:
						{
							int num4;
							if (num4 > this.ᜀ.Length)
							{
								num = 8;
								continue;
							}
							num = 32;
							continue;
						}
						case 13:
						{
							int num2;
							if (num2 > this.ᜀ.Length)
							{
								num = 10;
								continue;
							}
							num = 15;
							continue;
						}
						case 14:
							num = 6;
							continue;
						case 15:
							if (b != 0)
							{
								num = 22;
								continue;
							}
							goto IL_3CB;
						case 16:
							goto IL_3C6;
						case 17:
						{
							num5 = this.ᜌ(A_0.ᜄ());
							int num4 = A_0.ᜄ() + (int)num5 + 2;
							num = 12;
							continue;
						}
						case 18:
							if (A_0.ᜂ())
							{
								num = 27;
								continue;
							}
							num = 20;
							continue;
						case 19:
							num = 23;
							continue;
						case 20:
							if (A_0.ᜇ())
							{
								num = 25;
								continue;
							}
							num = 31;
							continue;
						case 21:
							goto IL_26C;
						case 22:
							goto IL_30E;
						case 23:
						{
							int num3;
							if (num3 != 8)
							{
								num = 14;
								continue;
							}
							num = 1;
							continue;
						}
						case 24:
							if (A_0.ᜆ())
							{
								num = 11;
								continue;
							}
							num = 9;
							continue;
						case 25:
							goto IL_45D;
						case 26:
							goto IL_482;
						case 27:
							goto IL_F5;
						case 28:
							if (A_0.ᜅ())
							{
								num = 26;
								continue;
							}
							goto IL_FA;
						case 29:
							if (A_0.ᜅ())
							{
								num = 30;
								continue;
							}
							goto IL_271;
						case 30:
							goto IL_1C2;
						case 31:
							if (A_0.ᜈ())
							{
								num = 33;
								continue;
							}
							num = 5;
							continue;
						case 32:
							if (num5 != 0)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_4B2;
						case 33:
							num6 = this.ᜌ(A_0.ᜄ());
							num = 34;
							continue;
						case 34:
							if (num6 <= 0)
							{
								num = 0;
								continue;
							}
							goto IL_2DA;
						}
						goto IL_CB;
					}
					IL_4E3:
					num = 24;
				}
			}
			IL_F5:
			return this.ᜁ(A_0.ᜄ(), A_0.ᜁ());
			IL_FA:
			return this.ᜌ(a_2);
			IL_18D:
			return string.Empty;
			IL_1C2:
			return this.\u1712(a_2);
			IL_244:
			throw new sprῩ(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐Œご㑖㙘⥚㥜罞ՠɢᅤ٦卨䭪Ṭ᭮Ͱᩲ᭴ၶ奸ቺ๼彾Ꞇ뾐", a_));
			IL_26C:
			return this.\u170D(a_2);
			IL_271:
			return this.ᜋ(a_2);
			IL_2C0:
			throw new sprῩ(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐Œご㑖㙘⥚㥜罞ՠɢᅤ٦卨䭪Ṭ᭮Ͱᩲ᭴ၶ奸ቺ๼彾Ꞇ뾐", a_));
			IL_2DA:
			return this.ᜅ(A_0.ᜄ() + 2, (int)num6);
			IL_30E:
			return BiffRecordRaw.LatinEncoding.GetString(this.ᜃ(A_0.ᜄ() + 1, (int)b), 0, (int)b);
			IL_310:
			return this.\u1714(a_2);
			IL_366:
			return this.ᜎ(a_2);
			IL_3C6:
			return this.ᜑ(a_2);
			IL_3CB:
			return "";
			IL_420:
			return BiffRecordRaw.LatinEncoding.GetString(this.ᜃ(A_0.ᜄ() + 2, (int)num5), 0, (int)num5);
			IL_42E:
			goto IL_50B;
			IL_45D:
			byte a_3 = this.ᜀ[A_0.ᜄ()];
			return this.ᜅ(A_0.ᜄ() + 1, (int)a_3);
			IL_482:
			return this.ᜐ(a_2);
			IL_4B2:
			return "";
			IL_506:
			return this.\u1713(a_2);
			IL_50B:
			throw new ApplicationException(string.Concat(new object[]
			{
				RecordTableEnumerator.b("ن㱈㽊≌ᵎ㑐㉒ㅔ㉖⭘筚灜罞㑠ൢ๤०٨ᱪͬ佮ɰᩲུቶ奸ᑺ᭼彾ꦈ﶐뮔랖쮘ﺚﺜ펠잢认", a_),
				base.TypeCode,
				RecordTableEnumerator.b("楆楈ࡊ≌⭎㑐獒", a_),
				base.RecordCode
			}));
		}
		}
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x00158D24 File Offset: 0x00157D24
	protected int ᜮ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8A:
			num = 2;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_4B;
			}
			break;
		}
		int num2;
		int count;
		IList<spr\u2429> keys;
		IList<FieldInfo> values;
		int num4;
		for (;;)
		{
			IL_34:
			switch (num)
			{
			case 0:
				goto IL_FB;
			case 1:
				goto IL_88;
			case 2:
			{
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				spr\u2429 spr_u = keys[num2];
				FieldInfo fieldInfo = values[num2];
				object value = fieldInfo.GetValue(this);
				int num3 = this.ᜀ(spr_u, value);
				num4 = Math.Max(num4, spr_u.ᜄ() + num3);
				num2++;
				num = 0;
				continue;
			}
			case 3:
				goto IL_A2;
			}
			goto IL_4B;
		}
		IL_88:
		goto IL_8A;
		IL_A2:
		bool autoGrowData;
		this.AutoGrowData = autoGrowData;
		return num4;
		IL_FB:
		goto IL_8A;
		IL_4B:
		SortedList<spr\u2429, FieldInfo> sortedList = this.ᜬ();
		autoGrowData = this.AutoGrowData;
		this.AutoGrowData = true;
		num4 = 0;
		keys = sortedList.Keys;
		values = sortedList.Values;
		num2 = 0;
		count = sortedList.Count;
		num = 1;
		goto IL_34;
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x00158E38 File Offset: 0x00157E38
	protected int ᜀ(spr\u2429 A_0, object A_1)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = 0;
				int num = A_0.ᜄ();
				int num2 = 36;
				for (;;)
				{
					IL_13:
					int num3;
					int num5;
					switch (num2)
					{
					case 0:
						goto IL_279;
					case 1:
						this.ᜀ(num, (short)A_1);
						num2 = 35;
						continue;
					case 2:
						goto IL_279;
					case 3:
						return result;
					case 4:
						if (A_0.ᜅ())
						{
							num2 = 1;
							continue;
						}
						this.ᜀ(num, (ushort)A_1);
						num2 = 39;
						continue;
					case 5:
						goto IL_279;
					case 6:
						if (num3 > 0)
						{
							num2 = 12;
							continue;
						}
						return result;
					case 7:
						goto IL_589;
					case 8:
					{
						string text;
						if (text == null)
						{
							num2 = 28;
							continue;
						}
						num2 = 41;
						continue;
					}
					case 9:
						while (A_0.ᜆ())
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
								num2 = 44;
								goto IL_13;
							}
						}
						num2 = 18;
						continue;
					case 10:
						return result;
					case 11:
						goto IL_279;
					case 12:
					{
						string text;
						byte[] bytes = Encoding.Unicode.GetBytes(text);
						this.ᜀ(num + 2, 1);
						this.ᜀ(num + 3, bytes, 0, bytes.Length);
						result = 3 + bytes.Length;
						num2 = 30;
						continue;
					}
					case 13:
						return result;
					case 14:
						return result;
					case 15:
					{
						int num4;
						if (num4 != 8)
						{
							num2 = 25;
							continue;
						}
						num2 = 9;
						continue;
					}
					case 16:
					{
						byte[] bytes2 = BiffRecordRaw.LatinEncoding.GetBytes((string)A_1);
						this.ᜀ(num, (byte)bytes2.Length);
						this.ᜀ(num + 1, bytes2, 0, bytes2.Length);
						result = 1 + bytes2.Length;
						num2 = 7;
						continue;
					}
					case 17:
						this.ᜀ(num, (bool)A_1, A_0.ᜁ());
						num2 = 10;
						continue;
					case 18:
						if (A_0.ᜅ())
						{
							num2 = 42;
							continue;
						}
						this.ᜀ(num, (ulong)A_1);
						num2 = 0;
						continue;
					case 19:
						if (A_0.ᜅ())
						{
							num2 = 40;
							continue;
						}
						this.ᜀ(num, (uint)A_1);
						num2 = 29;
						continue;
					case 20:
					{
						int num4;
						switch (num4)
						{
						case 1:
							this.ᜀ(num, (byte)A_1);
							num2 = 23;
							continue;
						case 2:
							num2 = 4;
							continue;
						case 3:
							goto IL_279;
						case 4:
							num2 = 21;
							continue;
						default:
							num2 = 37;
							continue;
						}
						break;
					}
					case 21:
						if (A_0.ᜆ())
						{
							num2 = 27;
							continue;
						}
						num2 = 19;
						continue;
					case 22:
					{
						string text2 = (string)A_1;
						byte[] bytes3 = Encoding.Unicode.GetBytes(text2);
						this.ᜀ(num, (byte)text2.Length);
						this.ᜀ(num + 1, 1);
						this.ᜀ(num + 2, bytes3, 0, bytes3.Length);
						result = 2 + bytes3.Length;
						num2 = 13;
						continue;
					}
					case 23:
						goto IL_279;
					case 24:
						goto IL_279;
					case 25:
						num2 = 24;
						continue;
					case 26:
					{
						string text = (string)A_1;
						num2 = 8;
						continue;
					}
					case 27:
						this.ᜀ(num, (float)A_1);
						num2 = 2;
						continue;
					case 28:
						num2 = 45;
						continue;
					case 29:
						goto IL_279;
					case 30:
						return result;
					case 31:
					{
						if (true)
						{
						}
						byte[] bytes4 = BiffRecordRaw.LatinEncoding.GetBytes((string)A_1);
						this.ᜀ(num, (ushort)bytes4.Length);
						this.ᜀ(num + 2, bytes4, 0, bytes4.Length);
						result = 2 + bytes4.Length;
						num2 = 3;
						continue;
					}
					case 32:
						if (A_0.ᜇ())
						{
							num2 = 22;
							continue;
						}
						num2 = 33;
						continue;
					case 33:
						if (A_0.ᜈ())
						{
							num2 = 26;
							continue;
						}
						num2 = 43;
						continue;
					case 34:
						if (A_0.ᜃ())
						{
							num2 = 31;
							continue;
						}
						num2 = 32;
						continue;
					case 35:
						goto IL_279;
					case 36:
						if (A_0.ᜀ())
						{
							num2 = 16;
							continue;
						}
						goto IL_589;
					case 37:
						num2 = 15;
						continue;
					case 38:
						goto IL_279;
					case 39:
						goto IL_279;
					case 40:
						this.ᜆ(num, (int)A_1);
						num2 = 11;
						continue;
					case 41:
					{
						string text;
						num5 = text.Length;
						goto IL_375;
					}
					case 42:
						this.ᜀ(num, (long)A_1);
						num2 = 5;
						continue;
					case 43:
					{
						if (A_0.ᜂ())
						{
							num2 = 17;
							continue;
						}
						int num4 = A_0.ᜁ();
						num2 = 20;
						continue;
					}
					case 44:
						this.ᜀ(num, (double)A_1);
						num2 = 38;
						continue;
					case 45:
						num5 = 0;
						goto IL_375;
					}
					break;
					IL_279:
					result = A_0.ᜁ();
					num2 = 14;
					continue;
					IL_375:
					num3 = num5;
					this.ᜀ(num, (ushort)num3);
					result = 2;
					num2 = 6;
					continue;
					IL_589:
					num2 = 34;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x00159434 File Offset: 0x00158434
	public virtual void \u1732()
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
		this.ᜀ = new byte[0];
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x0015947C File Offset: 0x0015847C
	public virtual bool ᜁ(BiffRecordRaw A_0)
	{
		for (;;)
		{
			for (;;)
			{
				spr\u251F spr_u251F = A_0 as spr\u251F;
				int num = 5;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_B3;
					case 1:
					{
						int num2;
						if (this.ᜀ[num2] != spr_u251F.ᜀ[num2])
						{
							num = 7;
							continue;
						}
						num2++;
						num = 4;
						continue;
					}
					case 2:
					{
						int num2;
						if (num2 >= this.m_iLength)
						{
							num = 6;
							continue;
						}
						num = 1;
						continue;
					}
					case 3:
						if (this.m_iLength == spr_u251F.m_iLength)
						{
							num = 9;
							continue;
						}
						return true;
					case 4:
						goto IL_B3;
					case 5:
						if (spr_u251F != null)
						{
							num = 8;
							continue;
						}
						return false;
					case 6:
						goto IL_D2;
					case 7:
						goto IL_FF;
					case 8:
						this.ᜀ(ExcelVersion.Version2007);
						spr_u251F.ᜀ(ExcelVersion.Version2007);
						num = 3;
						continue;
					case 9:
					{
						int num2 = 0;
						num = 0;
						continue;
					}
					}
					break;
					IL_B3:
					num = 2;
				}
			}
			IL_FF:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_115;
			}
		}
		return true;
		IL_D2:
		return true;
		IL_115:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x001595AC File Offset: 0x001585AC
	public virtual void ᜂ(BiffRecordRaw A_0)
	{
		int a_ = 8;
		if (base.RecordCode != A_0.RecordCode)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5B;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("氽┿⅁⭃㑅ⱇ㥉汋㵍㡏㵑⅓㩕㱗穙㑛㽝ᙟݡ䑣ᕥ१ݩ५乭ѯୱѳ፵塷ᱹ፻౽ꁿꒉ", a_));
		}
		IL_5B:
		spr\u251F spr_u251F = A_0 as spr\u251F;
		this.ᜀ(ExcelVersion.Version2007);
		spr_u251F.ᜀ = new byte[base.Length];
		Array.Copy(this.ᜀ, 0, spr_u251F.ᜀ, 0, base.Length);
		spr_u251F.m_iLength = this.m_iLength;
		spr_u251F.ᜂ();
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x00159660 File Offset: 0x00158660
	protected internal void ᜂ(byte[] A_0)
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

	// Token: 0x0600251C RID: 9500 RVA: 0x001596A4 File Offset: 0x001586A4
	protected void ᜀ(byte[] A_0, bool A_1)
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
		base.NeedInfill = A_1;
	}

	// Token: 0x0600251D RID: 9501
	public abstract void ᜂ();

	// Token: 0x0600251E RID: 9502 RVA: 0x001596F0 File Offset: 0x001586F0
	public virtual int ᜁ(ExcelVersion A_0)
	{
		int minimumRecordSize;
		for (;;)
		{
			minimumRecordSize = this.MinimumRecordSize;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.NeedInfill)
					{
						num = 1;
						continue;
					}
					goto IL_96;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						this.ᜀ(A_0);
						base.NeedInfill = false;
						num = 3;
						continue;
					}
					break;
				case 2:
					if (minimumRecordSize == this.MaximumRecordSize)
					{
						num = 4;
						continue;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				case 3:
					goto IL_54;
				case 4:
					return minimumRecordSize;
				}
				break;
			}
		}
		return minimumRecordSize;
		IL_54:
		IL_96:
		return this.m_iLength;
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x001597A4 File Offset: 0x001587A4
	public virtual void ᜱ()
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
		this.\u171F();
		this.ᜀ = null;
		GC.SuppressFinalize(this);
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x001597F4 File Offset: 0x001587F4
	protected virtual void \u171F()
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

	// Token: 0x04001296 RID: 4758
	protected internal new byte[] ᜀ;

	// Token: 0x04001297 RID: 4759
	private bool ᜁ;
}

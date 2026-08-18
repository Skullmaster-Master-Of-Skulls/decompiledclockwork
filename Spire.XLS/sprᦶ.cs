using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Spire.Xls.Calculation;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002FE RID: 766
[DefaultMember("Item")]
[Serializable]
internal class sprᦶ : IFormulaEngine, ISerializable
{
	// Token: 0x06002F4C RID: 12108 RVA: 0x001A8660 File Offset: 0x001A7660
	public sprᦶ()
	{
		this.ᜀ = true;
		this.ᜆ = "";
		base..ctor();
		this.ᜁ = null;
	}

	// Token: 0x06002F4D RID: 12109 RVA: 0x001A868C File Offset: 0x001A768C
	public sprᦶ(int A_0, int A_1)
	{
		this.ᜀ = true;
		this.ᜆ = "";
		base..ctor();
		this.ᜁ = new object[A_0, A_1];
	}

	// Token: 0x06002F4E RID: 12110 RVA: 0x001A86C0 File Offset: 0x001A76C0
	protected sprᦶ(SerializationInfo A_0, StreamingContext A_1)
	{
		int a_ = 9;
		this.ᜀ = true;
		this.ᜆ = "";
		base..ctor();
		this.ᜆ = (string)A_0.GetValue(RecordTableEnumerator.b("儾⁀⹂⁄", a_), typeof(string));
		int num = (int)A_0.GetValue(RecordTableEnumerator.b("䴾⹀㑂ل⡆㱈╊㥌", a_), typeof(int));
		int num2 = (int)A_0.GetValue(RecordTableEnumerator.b("尾⹀⽂ل⡆㱈╊㥌", a_), typeof(int));
		this.ᜁ = new object[num, num2];
		string text = (string)A_0.GetValue(RecordTableEnumerator.b("嬾⁀㝂⑄", a_), typeof(string));
		int num3 = 0;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				int num4 = text.Substring(num3).IndexOf(sprᦶ.ᜂ);
				this.ᜁ[i, j] = text.Substring(num3, num4);
				num3 = num3 + num4 + 1;
			}
		}
	}

	// Token: 0x06002F4F RID: 12111 RVA: 0x001A87EC File Offset: 0x001A77EC
	public void ᜁ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			ValueChangedEventHandler valueChangedEventHandler = this.ᜈ;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					IL_78:
					ValueChangedEventHandler valueChangedEventHandler2;
					if (valueChangedEventHandler == valueChangedEventHandler2)
					{
						num = 2;
						continue;
					}
					goto IL_37;
				}
				case 1:
					goto IL_37;
				case 2:
					return;
				}
				break;
				IL_37:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_78;
				default:
				{
					if (false)
					{
					}
					ValueChangedEventHandler valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Combine(valueChangedEventHandler2, A_0);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜈ, value, valueChangedEventHandler2);
					num = 0;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002F50 RID: 12112 RVA: 0x001A8884 File Offset: 0x001A7884
	public void ᜂ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			ValueChangedEventHandler valueChangedEventHandler = this.ᜈ;
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					IL_78:
					ValueChangedEventHandler valueChangedEventHandler2;
					if (valueChangedEventHandler == valueChangedEventHandler2)
					{
						num = 1;
						continue;
					}
					goto IL_37;
				}
				case 1:
					return;
				case 2:
					goto IL_37;
				}
				break;
				IL_37:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_78;
				default:
				{
					if (false)
					{
					}
					ValueChangedEventHandler valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Remove(valueChangedEventHandler2, A_0);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜈ, value, valueChangedEventHandler2);
					num = 0;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002F51 RID: 12113 RVA: 0x001A891C File Offset: 0x001A791C
	public void ᜀ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			ValueChangedEventHandler valueChangedEventHandler = this.ᜉ;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					IL_70:
					ValueChangedEventHandler valueChangedEventHandler2;
					if (valueChangedEventHandler == valueChangedEventHandler2)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_2F;
				}
				case 2:
					goto IL_2F;
				}
				break;
				IL_2F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_70;
				default:
				{
					if (false)
					{
					}
					ValueChangedEventHandler valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Combine(valueChangedEventHandler2, A_0);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜉ, value, valueChangedEventHandler2);
					num = 1;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002F52 RID: 12114 RVA: 0x001A89B4 File Offset: 0x001A79B4
	public void ᜃ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			ValueChangedEventHandler valueChangedEventHandler = this.ᜉ;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					IL_70:
					ValueChangedEventHandler valueChangedEventHandler2;
					if (valueChangedEventHandler == valueChangedEventHandler2)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_2F;
				}
				case 1:
					goto IL_2F;
				case 2:
					return;
				}
				break;
				IL_2F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_70;
				default:
				{
					if (false)
					{
					}
					ValueChangedEventHandler valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Remove(valueChangedEventHandler2, A_0);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜉ, value, valueChangedEventHandler2);
					num = 0;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002F53 RID: 12115 RVA: 0x001A8A4C File Offset: 0x001A7A4C
	public bool ᜇ()
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

	// Token: 0x06002F54 RID: 12116 RVA: 0x001A8A90 File Offset: 0x001A7A90
	public void ᜁ(bool A_0)
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

	// Token: 0x06002F55 RID: 12117 RVA: 0x001A8AD4 File Offset: 0x001A7AD4
	public virtual int ᜁ()
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
		return this.ᜁ.GetLength(1);
	}

	// Token: 0x06002F56 RID: 12118 RVA: 0x001A8B1C File Offset: 0x001A7B1C
	public static char ᜂ()
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
		return sprᦶ.ᜂ;
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x001A8B5C File Offset: 0x001A7B5C
	public static void ᜀ(char A_0)
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
		sprᦶ.ᜂ = A_0;
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x001A8BA0 File Offset: 0x001A7BA0
	public FormulaEngine ᜃ()
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

	// Token: 0x06002F59 RID: 12121 RVA: 0x001A8BE4 File Offset: 0x001A7BE4
	public bool ᜆ()
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
		return this.ᜅ;
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x001A8C28 File Offset: 0x001A7C28
	public void ᜀ(bool A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x001A8C6C File Offset: 0x001A7C6C
	public string ᜄ()
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
		return this.ᜆ;
	}

	// Token: 0x06002F5C RID: 12124 RVA: 0x001A8CB0 File Offset: 0x001A7CB0
	public void ᜂ(string A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06002F5D RID: 12125 RVA: 0x001A8CF4 File Offset: 0x001A7CF4
	public virtual int ᜀ()
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
		return this.ᜁ.GetLength(0);
	}

	// Token: 0x06002F5E RID: 12126 RVA: 0x001A8D3C File Offset: 0x001A7D3C
	public object ᜁ(int A_0, int A_1)
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
		return ((IFormulaEngine)this).GetCaculateValue(A_0, A_1);
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x001A8D80 File Offset: 0x001A7D80
	public void ᜀ(int A_0, int A_1, object A_2)
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
		this.ᜀ(A_0, A_1, A_2.ToString());
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x001A8DCC File Offset: 0x001A7DCC
	public static sprᦶ ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			sprᦶ result = null;
			goto IL_24;
			try
			{
				for (;;)
				{
					IL_24:
					StreamReader streamReader = new StreamReader(A_0);
					try
					{
						string s = streamReader.ReadLine();
						int.Parse(s);
						result = sprᦶ.ᜀ(streamReader);
						streamReader.Close();
					}
					finally
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								((IDisposable)streamReader).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_85;
							}
							if (streamReader == null)
							{
								break;
							}
							num = 1;
						}
						IL_85:;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_9E;
					}
				}
				IL_9E:
				if (false)
				{
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		}
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x001A8EAC File Offset: 0x001A7EAC
	public void ᜀ(SerializationInfo A_0, StreamingContext A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				A_0.AddValue(RecordTableEnumerator.b("⽀≂⡄≆", a_), this.ᜆ);
				A_0.AddValue(RecordTableEnumerator.b("㍀ⱂ㉄ц♈㹊⍌㭎", a_), this.ᜀ());
				A_0.AddValue(RecordTableEnumerator.b("≀ⱂ⥄ц♈㹊⍌㭎", a_), this.ᜁ());
				this.ᜄ = true;
				stringBuilder = new StringBuilder();
				int num = 1;
				int num2 = 3;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_E3;
					case 1:
						goto IL_137;
					case 2:
						if (num3 > this.ᜁ())
						{
							num2 = 7;
							continue;
						}
						goto IL_139;
					case 3:
						goto IL_116;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_139;
						default:
							if (false)
							{
							}
							goto IL_116;
						}
						break;
					case 5:
						goto IL_E3;
					case 6:
						if (num > this.ᜀ())
						{
							num2 = 1;
							continue;
						}
						num3 = 1;
						num2 = 0;
						continue;
					case 7:
						num++;
						num2 = 4;
						continue;
					}
					break;
					IL_E3:
					num2 = 2;
					continue;
					IL_116:
					num2 = 6;
					continue;
					IL_139:
					object caculateValue = ((IFormulaEngine)this).GetCaculateValue(num, num3);
					stringBuilder.AppendFormat(RecordTableEnumerator.b("㩀獂㡄㱆硈㙊", a_), caculateValue, sprᦶ.ᜂ);
					num3++;
					num2 = 5;
				}
			}
			IL_137:
			if (true)
			{
			}
			A_0.AddValue(RecordTableEnumerator.b("╀≂ㅄ♆", a_), stringBuilder.ToString());
			this.ᜄ = false;
			return;
		}
		}
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x001A905C File Offset: 0x001A805C
	public virtual object ᜀ(int A_0, int A_1)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (this.ᜄ)
				{
					num = 8;
					continue;
				}
				goto IL_C4;
			case 2:
			{
				string text = this.ᜃ.ᜀ.ᜀ(this, A_0, A_1);
				num = 9;
				continue;
			}
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C4;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 4:
			{
				string text;
				return text;
			}
			case 5:
				if (this.ᜃ != null)
				{
					num = 2;
					continue;
				}
				goto IL_C4;
			case 6:
				if (A_1 <= this.ᜁ.GetLength(1))
				{
					num = 3;
					continue;
				}
				goto IL_13B;
			case 7:
				num = 6;
				continue;
			case 8:
				num = 5;
				continue;
			case 9:
			{
				string text;
				if (text.Length > 0)
				{
					num = 4;
					continue;
				}
				goto IL_C4;
			}
			}
			if (true)
			{
			}
			if (A_0 > this.ᜁ.GetLength(0))
			{
				goto IL_13B;
			}
			num = 7;
		}
		IL_C4:
		return this.ᜁ[A_0 - 1, A_1 - 1];
		IL_13B:
		return RecordTableEnumerator.b("硇", a_);
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x001A91B4 File Offset: 0x001A81B4
	protected virtual void ᜀ(ValueChangedEventArgs A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜈ(this, A_0);
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			if (this.ᜈ == null)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x001A9234 File Offset: 0x001A8234
	protected virtual void ᜁ(ValueChangedEventArgs A_0)
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
					return;
				default:
					if (false)
					{
					}
					this.ᜉ(this, A_0);
					if (true)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			if (this.ᜉ == null)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x001A92B4 File Offset: 0x001A82B4
	public static sprᦶ ᜀ(StreamReader A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			sprᦶ sprᦶ;
			for (;;)
			{
				string text = A_0.ReadLine();
				int num = int.Parse(text);
				text = A_0.ReadLine();
				int a_ = int.Parse(text);
				sprᦶ = new sprᦶ(num, a_);
				text = A_0.ReadLine();
				sprᦶ.ᜆ = text;
				int num2 = 0;
				int num3 = 4;
				for (;;)
				{
					int num4;
					string[] array;
					int num5;
					switch (num3)
					{
					case 0:
						goto IL_BC;
					case 1:
						goto IL_BC;
					case 2:
						if (num4 >= array.Length)
						{
							num3 = 6;
							continue;
						}
						goto IL_133;
					case 3:
						if (num2 >= num)
						{
							num3 = 7;
							continue;
						}
						text = A_0.ReadLine();
						num5 = 0;
						array = text.Split(new char[]
						{
							sprᦶ.ᜂ
						});
						num4 = 0;
						num3 = 1;
						continue;
					case 4:
						goto IL_114;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_133;
						default:
							if (false)
							{
							}
							goto IL_114;
						}
						break;
					case 6:
						num2++;
						num3 = 5;
						continue;
					case 7:
						return sprᦶ;
					}
					break;
					IL_BC:
					num3 = 2;
					continue;
					IL_114:
					num3 = 3;
					continue;
					IL_133:
					string text2 = array[num4];
					sprᦶ.ᜁ[num2, num5] = text2;
					num5++;
					num4++;
					num3 = 0;
				}
			}
			return sprᦶ;
		}
		}
	}

	// Token: 0x06002F66 RID: 12134 RVA: 0x001A942C File Offset: 0x001A842C
	public virtual void ᜀ(int A_0, int A_1, string A_2)
	{
		int num = 3;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				return;
			case 1:
				return;
			case 2:
				if (this.ᜇ())
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_85;
			}
			while (!this.ᜆ())
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
					this.ᜀ(A_2, A_0, A_1);
					num = 2;
					goto IL_0A;
				}
			}
			num = 0;
		}
		return;
		IL_85:
		this.ᜃ().ᜀ.ᜈ(ref A_2);
		ValueChangedEventArgs a_ = new ValueChangedEventArgs(A_0, A_1, A_2);
		this.ᜁ(a_);
	}

	// Token: 0x06002F67 RID: 12135 RVA: 0x001A94E0 File Offset: 0x001A84E0
	public virtual void ᜀ(object A_0, int A_1, int A_2)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ValueChangedEventArgs a_ = new ValueChangedEventArgs(A_1, A_2, A_0.ToString());
				this.ᜀ(a_);
				num = 2;
				continue;
			}
			case 1:
				if (this.ᜈ != null)
				{
					num = 0;
					continue;
				}
				goto IL_B0;
			case 2:
				goto IL_72;
			case 3:
				this.ᜁ[A_1 - 1, A_2 - 1] = A_0;
				num = 1;
				continue;
			}
			if (this.ᜆ())
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_72;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
		}
		IL_72:
		IL_B0:
		if (true)
		{
		}
	}

	// Token: 0x06002F68 RID: 12136 RVA: 0x001A95A8 File Offset: 0x001A85A8
	public virtual void ᜅ()
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

	// Token: 0x06002F69 RID: 12137 RVA: 0x001A95E4 File Offset: 0x001A85E4
	public void ᜃ(string A_0)
	{
		try
		{
			StreamWriter streamWriter = new StreamWriter(A_0);
			try
			{
				streamWriter.WriteLine(this.ᜇ);
				this.ᜀ(streamWriter);
				streamWriter.Close();
			}
			finally
			{
				for (;;)
				{
					IL_24:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7F;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_24;
							default:
								if (false)
								{
								}
								((IDisposable)streamWriter).Dispose();
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						if (streamWriter == null)
						{
							goto IL_81;
						}
						num = 2;
					}
				}
				IL_7F:
				IL_81:;
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	// Token: 0x06002F6A RID: 12138 RVA: 0x001A96A4 File Offset: 0x001A86A4
	public void ᜀ(StreamWriter A_0, bool A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_5B:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_104:
					if (num <= 0)
					{
						goto IL_205;
					}
					num2 = 10;
					break;
				case 1:
					goto IL_7B;
				default:
					goto IL_7B;
				}
				int num3;
				int length;
				int length2;
				for (;;)
				{
					IL_10:
					switch (num2)
					{
					case 0:
						goto IL_149;
					case 1:
					{
						string text = this.ᜃ.ᜀ.ᜀ(this, num3 + 1, num + 1);
						num2 = 6;
						continue;
					}
					case 2:
						if (num >= length)
						{
							num2 = 4;
							continue;
						}
						num2 = 14;
						continue;
					case 3:
						return;
					case 4:
						A_0.WriteLine("");
						num3++;
						num2 = 16;
						continue;
					case 5:
						if (!A_1)
						{
							num2 = 1;
							continue;
						}
						goto IL_1E1;
					case 6:
					{
						string text;
						if (text.Length > 0)
						{
							num2 = 8;
							continue;
						}
						goto IL_1E1;
					}
					case 7:
						goto IL_167;
					case 8:
					{
						string text;
						A_0.Write(text);
						num2 = 12;
						continue;
					}
					case 9:
						goto IL_1DF;
					case 10:
						A_0.Write(sprᦶ.ᜂ);
						num2 = 9;
						continue;
					case 11:
						if (num3 >= length2)
						{
							num2 = 3;
							continue;
						}
						num = 0;
						num2 = 15;
						continue;
					case 12:
						goto IL_D0;
					case 13:
						goto IL_D0;
					case 14:
						goto IL_104;
					case 15:
						goto IL_149;
					case 16:
						goto IL_167;
					}
					goto IL_5B;
					IL_D0:
					num++;
					num2 = 0;
					continue;
					IL_149:
					num2 = 2;
					continue;
					IL_167:
					num2 = 11;
					continue;
					IL_1E1:
					A_0.Write(this.ᜁ[num3, num]);
					num2 = 13;
				}
				IL_1DF:
				goto IL_205;
				IL_7B:
				if (false)
				{
				}
				length2 = this.ᜁ.GetLength(0);
				length = this.ᜁ.GetLength(1);
				A_0.WriteLine(length2);
				A_0.WriteLine(length);
				A_0.WriteLine(this.ᜆ);
				num3 = 0;
				if (true)
				{
				}
				num2 = 7;
				goto IL_10;
				IL_205:
				num2 = 5;
				goto IL_10;
			}
			return;
		}
	}

	// Token: 0x06002F6B RID: 12139 RVA: 0x001A98D8 File Offset: 0x001A88D8
	public void ᜀ(StreamWriter A_0)
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
		this.ᜀ(A_0, false);
	}

	// Token: 0x06002F6C RID: 12140 RVA: 0x001A991C File Offset: 0x001A891C
	public void ᜁ(string A_0)
	{
		try
		{
			StreamWriter streamWriter = new StreamWriter(A_0);
			try
			{
				this.ᜀ(streamWriter, true);
				streamWriter.Close();
			}
			finally
			{
				for (;;)
				{
					IL_19:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6C;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19;
							default:
								if (false)
								{
								}
								((IDisposable)streamWriter).Dispose();
								num = 0;
								continue;
							}
							break;
						}
						if (streamWriter == null)
						{
							goto IL_6E;
						}
						num = 2;
					}
				}
				IL_6C:
				IL_6E:;
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		if (true)
		{
		}
	}

	// Token: 0x06002F6D RID: 12141 RVA: 0x001A99D4 File Offset: 0x001A89D4
	// Note: this type is marked as 'beforefieldinit'.
	static sprᦶ()
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
		sprᦶ.ᜂ = '\t';
	}

	// Token: 0x04001533 RID: 5427
	private bool ᜀ;

	// Token: 0x04001534 RID: 5428
	internal object[,] ᜁ;

	// Token: 0x04001535 RID: 5429
	[ThreadStatic]
	private static char ᜂ;

	// Token: 0x04001536 RID: 5430
	internal FormulaEngine ᜃ;

	// Token: 0x04001537 RID: 5431
	private bool ᜄ;

	// Token: 0x04001538 RID: 5432
	private bool ᜅ;

	// Token: 0x04001539 RID: 5433
	private string ᜆ;

	// Token: 0x0400153A RID: 5434
	private int ᜇ;

	// Token: 0x0400153B RID: 5435
	private ValueChangedEventHandler ᜈ;

	// Token: 0x0400153C RID: 5436
	private ValueChangedEventHandler ᜉ;
}

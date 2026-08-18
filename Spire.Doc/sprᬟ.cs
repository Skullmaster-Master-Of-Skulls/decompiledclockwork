using System;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;

// Token: 0x02000233 RID: 563
internal class sprᬟ : spr\u2562
{
	// Token: 0x06001AE7 RID: 6887 RVA: 0x001C3F10 File Offset: 0x001C2F10
	internal override int ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6C;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_6C;
				case 2:
					this.ᜄ = 93;
					num = 0;
					continue;
				}
				if (this.ᜄ != 0)
				{
					goto IL_6E;
				}
				num = 2;
				break;
			}
		}
		IL_6C:
		IL_6E:
		return this.ᜄ;
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x001C3F94 File Offset: 0x001C2F94
	internal string ᜂ()
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

	// Token: 0x06001AE9 RID: 6889 RVA: 0x001C3FD8 File Offset: 0x001C2FD8
	internal string ᜁ()
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
		return this.ᜈ;
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x001C401C File Offset: 0x001C301C
	internal sprᬟ(spr\u2578 A_0)
	{
		this.ᜆ = string.Empty;
		this.ᜇ = string.Empty;
		this.ᜈ = string.Empty;
		this.ᜉ = 1907505652U;
		this.ᜊ = string.Empty;
		this.ᜋ = string.Empty;
		this.ᜌ = string.Empty;
		base..ctor();
		byte[] array = new byte[A_0.Length];
		A_0.Read(array, 0, array.Length);
		this.ᜁ(array, 0);
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x001C40A0 File Offset: 0x001C30A0
	internal sprᬟ(OleObjectType A_0)
	{
		int a_ = 4;
		this.ᜆ = string.Empty;
		this.ᜇ = string.Empty;
		this.ᜈ = string.Empty;
		this.ᜉ = 1907505652U;
		this.ᜊ = string.Empty;
		this.ᜋ = string.Empty;
		this.ᜌ = string.Empty;
		base..ctor();
		this.ᜅ = new sprᬟ.ᜀ();
		switch (A_0)
		{
		case OleObjectType.AdobeAcrobatDocument:
			this.ᜆ = ClipboardData.b("⭩ཫᱭὯၱᕳɵ塷㹹፻ᵽﲇ誉", a_);
			this.ᜈ = ClipboardData.b("⭩ཫᱭὯ㝱౳ᕵၷ呹㡻ᅽﺉꊋ릍邏", a_);
			return;
		case OleObjectType.BitmapImage:
		case OleObjectType.MediaClip:
		case OleObjectType.Equation:
		case OleObjectType.GraphChart:
		case OleObjectType.Excel_97_2003_Worksheet:
		case OleObjectType.ExcelBinaryWorksheet:
		case OleObjectType.ExcelChart:
		case OleObjectType.ExcelMacroWorksheet:
		case OleObjectType.ExcelWorksheet:
		case OleObjectType.PowerPoint_97_2003_Presentation:
		case OleObjectType.PowerPoint_97_2003_Slide:
		case OleObjectType.PowerPointMacroPresentation:
		case OleObjectType.PowerPointMacroSlide:
		case OleObjectType.PowerPointPresentation:
		case OleObjectType.PowerPointSlide:
		case OleObjectType.Word_97_2003_Document:
		case OleObjectType.WordDocument:
		case OleObjectType.WordMacroDocument:
		case OleObjectType.MIDISequence:
		case OleObjectType.Package:
		case OleObjectType.VideoClip:
			this.ᜆ = spr\u20F5.ᜀ(A_0, true) + ClipboardData.b("橩", a_);
			break;
		case OleObjectType.VisioDrawing:
		case OleObjectType.OpenDocumentPresentation:
		case OleObjectType.OpenDocumentSpreadsheet:
		case OleObjectType.OpenDocumentText:
		case OleObjectType.OpenOfficeSpreadsheet1_1:
		case OleObjectType.OpenOfficeText_1_1:
			break;
		case OleObjectType.WaveSound:
			this.ᜆ = ClipboardData.b("㵩൫ᡭᕯ剱❳᥵൷ᑹ᡻繽", a_);
			this.ᜈ = ClipboardData.b("㥩ͫ᭭ṯᙱ♳፵᭷穹", a_);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x001C420C File Offset: 0x001C320C
	internal override void ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					this.ᜄ = A_0.Length;
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
					this.ᜅ = new sprᬟ.ᜀ();
					this.ᜅ.ᜁ(A_0, A_1);
					A_1 += this.ᜅ.ᜀ();
					int num = spr\u2562.ᜃ(A_0, ref A_1);
					int num2 = 9;
					for (;;)
					{
						uint num3;
						switch (num2)
						{
						case 0:
						{
							byte[] bytes = spr\u2562.ᜀ(A_0, num, ref A_1);
							this.ᜊ = unicodeEncoding.GetString(bytes);
							num2 = 10;
							continue;
						}
						case 1:
							if (this.ᜉ == 1907505652U)
							{
								num2 = 20;
								continue;
							}
							return;
						case 2:
							goto IL_3D6;
						case 3:
							if (num > 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_45C;
						case 4:
							if (num3 > 400U)
							{
								num2 = 28;
								continue;
							}
							goto IL_376;
						case 5:
							if (num > 0)
							{
								num2 = 32;
								continue;
							}
							goto IL_376;
						case 6:
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
								byte[] bytes2 = spr\u2562.ᜀ(A_0, num, ref A_1);
								this.ᜈ = asciiencoding.GetString(bytes2);
								num2 = 35;
								continue;
							}
							}
							break;
						case 7:
							goto IL_27C;
						case 8:
							goto IL_2CE;
						case 9:
							if (num > 0)
							{
								num2 = 21;
								continue;
							}
							goto IL_48B;
						case 10:
							goto IL_45C;
						case 11:
							if (num3 == 4294967294U)
							{
								num2 = 2;
								continue;
							}
							num2 = 23;
							continue;
						case 12:
							if (num3 == 4294967294U)
							{
								num2 = 16;
								continue;
							}
							num2 = 4;
							continue;
						case 13:
							goto IL_376;
						case 14:
							goto IL_48B;
						case 15:
							num2 = 24;
							continue;
						case 16:
							goto IL_16E;
						case 17:
							num2 = 11;
							continue;
						case 18:
							num2 = 12;
							continue;
						case 19:
							if (num3 > 0U)
							{
								num2 = 15;
								continue;
							}
							goto IL_27C;
						case 20:
							num = spr\u2562.ᜃ(A_0, ref A_1);
							if (true)
							{
							}
							num2 = 3;
							continue;
						case 21:
						{
							byte[] bytes3 = spr\u2562.ᜀ(A_0, num, ref A_1);
							this.ᜆ = asciiencoding.GetString(bytes3);
							num2 = 14;
							continue;
						}
						case 22:
							num2 = 25;
							continue;
						case 23:
							if (num3 > 400U)
							{
								num2 = 33;
								continue;
							}
							goto IL_27C;
						case 24:
							if (num3 != 4294967295U)
							{
								num2 = 17;
								continue;
							}
							goto IL_3D6;
						case 25:
							if (num <= 40)
							{
								num2 = 6;
								continue;
							}
							goto IL_218;
						case 26:
							if (num > 0)
							{
								num2 = 22;
								continue;
							}
							goto IL_218;
						case 27:
							if (num3 != 4294967295U)
							{
								num2 = 18;
								continue;
							}
							goto IL_16E;
						case 28:
							goto IL_277;
						case 29:
							num2 = 30;
							continue;
						case 30:
							if (num <= 40)
							{
								num2 = 31;
								continue;
							}
							return;
						case 31:
						{
							byte[] bytes4 = spr\u2562.ᜀ(A_0, num, ref A_1);
							this.ᜌ = unicodeEncoding.GetString(bytes4);
							num2 = 8;
							continue;
						}
						case 32:
							num2 = 27;
							continue;
						case 33:
							goto IL_3D1;
						case 34:
							if (num > 0)
							{
								num2 = 29;
								continue;
							}
							return;
						case 35:
							goto IL_218;
						}
						break;
						IL_16E:
						byte[] bytes5 = spr\u2562.ᜀ(A_0, 4, ref A_1);
						this.ᜆ = asciiencoding.GetString(bytes5);
						num2 = 13;
						continue;
						IL_218:
						this.ᜉ = spr\u2562.ᜀ(A_0, ref A_1);
						num2 = 1;
						continue;
						IL_27C:
						num = spr\u2562.ᜃ(A_0, ref A_1);
						num2 = 34;
						continue;
						IL_376:
						num = spr\u2562.ᜃ(A_0, ref A_1);
						num2 = 26;
						continue;
						IL_3D6:
						byte[] bytes6 = spr\u2562.ᜀ(A_0, 4, ref A_1);
						this.ᜋ = unicodeEncoding.GetString(bytes6);
						num2 = 7;
						continue;
						IL_45C:
						num3 = spr\u2562.ᜀ(A_0, ref A_1);
						num2 = 19;
						continue;
						IL_48B:
						num3 = spr\u2562.ᜀ(A_0, ref A_1);
						num2 = 5;
					}
				}
			}
			IL_277:
			throw new InvalidDataException(ClipboardData.b("㥵㑷㽹屻ൽꪉ낏ﲑﮓ뢗ﶛ즟욡", a_));
			IL_2CE:
			return;
			IL_3D1:
			throw new InvalidDataException(ClipboardData.b("㥵㑷㽹屻ൽꪉ낏ﲑﮓ뢗ﶛ즟욡", a_));
		}
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x001C46D4 File Offset: 0x001C36D4
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(ClipboardData.b("♧թᡫ乭᥯άѳ᩵ᵷ᝹᥻ၽ", a_));
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x001C472C File Offset: 0x001C372C
	internal void ᜀ(sprᤘ A_0)
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
		int a_ = 4;
		this.ᜅ.ᜀ(A_0);
		this.ᜀ(A_0, this.ᜆ);
		this.ᜀ(A_0, this.ᜇ);
		this.ᜀ(A_0, this.ᜈ);
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0, a_);
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x001C47BC File Offset: 0x001C37BC
	private void ᜀ(sprᤘ A_0, int A_1)
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
		byte[] buffer = new byte[A_1];
		A_0.Write(buffer, 0, A_1);
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x001C4808 File Offset: 0x001C3808
	private void ᜀ(sprᤘ A_0, string A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] array = new byte[4];
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				int num = 0;
				byte[] bytes = asciiencoding.GetBytes(A_1);
				spr\u2562.ᜀ(array, ref num, bytes.Length);
				A_0.Write(array, 0, array.Length);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						A_0.Write(bytes, 0, bytes.Length);
						num2 = 0;
						continue;
					case 2:
						if (bytes.Length <= 0)
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
							num2 = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			return;
		}
	}

	// Token: 0x04001E87 RID: 7815
	private new const int ᜀ = 93;

	// Token: 0x04001E88 RID: 7816
	private new const int ᜁ = 400;

	// Token: 0x04001E89 RID: 7817
	private new const int ᜂ = 40;

	// Token: 0x04001E8A RID: 7818
	private new const uint ᜃ = 1907505652U;

	// Token: 0x04001E8B RID: 7819
	private new int ᜄ;

	// Token: 0x04001E8C RID: 7820
	private sprᬟ.ᜀ ᜅ;

	// Token: 0x04001E8D RID: 7821
	private string ᜆ;

	// Token: 0x04001E8E RID: 7822
	private string ᜇ;

	// Token: 0x04001E8F RID: 7823
	private string ᜈ;

	// Token: 0x04001E90 RID: 7824
	private uint ᜉ;

	// Token: 0x04001E91 RID: 7825
	private string ᜊ;

	// Token: 0x04001E92 RID: 7826
	private string ᜋ;

	// Token: 0x04001E93 RID: 7827
	private string ᜌ;

	// Token: 0x02000234 RID: 564
	private new class ᜀ : spr\u2562
	{
		// Token: 0x06001AF1 RID: 6897 RVA: 0x001C48C8 File Offset: 0x001C38C8
		internal override int ᜀ()
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
			return 28;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x001C4908 File Offset: 0x001C3908
		internal ᜀ()
		{
			this.ᜂ = -131071;
			this.ᜃ = 2563;
			this.ᜄ = new byte[20];
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x001C4940 File Offset: 0x001C3940
		internal override void ᜁ(byte[] A_0, int A_1)
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
			this.ᜂ = spr\u2562.ᜃ(A_0, ref A_1);
			this.ᜃ = spr\u2562.ᜃ(A_0, ref A_1);
			this.ᜄ = spr\u2562.ᜀ(A_0, 20, ref A_1);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x001C49A8 File Offset: 0x001C39A8
		internal override int ᜀ(byte[] A_0, int A_1)
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
			spr\u2562.ᜀ(A_0, ref A_1, -131071);
			spr\u2562.ᜀ(A_0, ref A_1, 2563);
			this.ᜄ = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				101,
				202,
				1,
				184,
				252,
				161,
				208,
				17,
				133,
				173,
				68,
				69,
				83,
				84,
				0,
				0
			};
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜄ);
			return A_1;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x001C4A24 File Offset: 0x001C3A24
		internal void ᜀ(sprᤘ A_0)
		{
			int count = 4;
			byte[] bytes = BitConverter.GetBytes(this.ᜂ);
			A_0.Write(bytes, 0, count);
			bytes = BitConverter.GetBytes(this.ᜃ);
			A_0.Write(bytes, 0, count);
			if (this.ᜄ == null)
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
					A_0.Write(new byte[20], 0, 20);
					return;
				}
			}
			A_0.Write(this.ᜄ, 0, 20);
		}

		// Token: 0x04001E94 RID: 7828
		internal new const int ᜀ = 28;

		// Token: 0x04001E95 RID: 7829
		internal new const int ᜁ = 20;

		// Token: 0x04001E96 RID: 7830
		internal new int ᜂ;

		// Token: 0x04001E97 RID: 7831
		internal new int ᜃ;

		// Token: 0x04001E98 RID: 7832
		internal new byte[] ᜄ;
	}
}

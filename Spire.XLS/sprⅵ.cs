using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000239 RID: 569
internal abstract class spr\u2175
{
	// Token: 0x0600227A RID: 8826
	public abstract void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3);

	// Token: 0x0600227B RID: 8827
	public abstract void ᜀ(XmlWriter A_0, Type A_1);

	// Token: 0x0600227C RID: 8828 RVA: 0x00133700 File Offset: 0x00132700
	protected static string ᜀ(XlsShape A_0)
	{
		int a_ = 10;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᮋ sprᮋ = A_0.ClientAnchor;
		int num = sprᮋ.ᜃ();
		int num2 = A_0.ᜅ(num + 1, sprᮋ.ᜀ(), true);
		string text = num.ToString();
		string text2 = num2.ToString();
		num = sprᮋ.ᜎ();
		num2 = A_0.ᜅ(num + 1, sprᮋ.ᜄ(), true);
		string text3 = num.ToString();
		string text4 = num2.ToString();
		num = sprᮋ.ᜉ();
		num2 = A_0.ᜅ(num + 1, sprᮋ.ᜁ(), false);
		string text5 = num.ToString();
		string text6 = num2.ToString();
		num = sprᮋ.ᜇ();
		num2 = A_0.ᜅ(num + 1, sprᮋ.ᜆ(), false);
		string text7 = num.ToString();
		string text8 = num2.ToString();
		return string.Join(RecordTableEnumerator.b("氿扁", a_), new string[]
		{
			text,
			text2,
			text5,
			text6,
			text3,
			text4,
			text7,
			text8
		});
	}

	// Token: 0x0600227D RID: 8829 RVA: 0x00133844 File Offset: 0x00132844
	protected void ᜀ(XmlWriter A_0, XlsShape A_1, string A_2)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_52;
				}
				goto Block_3;
			case 1:
				goto IL_65;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			IL_52:
			num = 2;
		}
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⭉㱋⭍", a_));
		Block_3:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("Յ⑇⍉⥋⁍⑏ᙑ㕓≕㥗", a_), RecordTableEnumerator.b("㍅㩇⑉癋㵍㍏㩑ㅓ㭕㥗⥙煛㍝य़šᙣ॥᭧թ੫ᩭ嵯ᅱ᭳᭵䉷ᕹ᩻᡽벅ﲏ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ॅ⩇⁉⥋ⵍ⑏ّⵓ♕㵗", a_), A_2);
		A_0.WriteElementString(RecordTableEnumerator.b("୅❇㱉⥋᥍㥏♑㱓ᕕ㵗㙙せⵝ", a_), RecordTableEnumerator.b("㍅㩇⑉癋㵍㍏㩑ㅓ㭕㥗⥙煛㍝य़šᙣ॥᭧թ੫ᩭ嵯ᅱ᭳᭵䉷ᕹ᩻᡽벅ﲏ", a_), A_1.IsMoveWithCell.ToString());
		A_0.WriteElementString(RecordTableEnumerator.b("ᕅⅇぉ⥋᥍㥏♑㱓ᕕ㵗㙙せⵝ", a_), RecordTableEnumerator.b("㍅㩇⑉癋㵍㍏㩑ㅓ㭕㥗⥙煛㍝य़šᙣ॥᭧թ੫ᩭ嵯ᅱ᭳᭵䉷ᕹ᩻᡽벅ﲏ", a_), A_1.IsSizeWithCell.ToString());
		string value = spr\u2175.ᜀ(A_1);
		A_0.WriteElementString(RecordTableEnumerator.b("݅♇⥉⑋⅍≏", a_), RecordTableEnumerator.b("㍅㩇⑉癋㵍㍏㩑ㅓ㭕㥗⥙煛㍝य़šᙣ॥᭧թ੫ᩭ嵯ᅱ᭳᭵䉷ᕹ᩻᡽벅ﲏ", a_), value);
		this.ᜀ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x0600227E RID: 8830 RVA: 0x001339C4 File Offset: 0x001329C4
	protected virtual void ᜀ(XmlWriter A_0, XlsShape A_1)
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

	// Token: 0x0600227F RID: 8831 RVA: 0x00133A00 File Offset: 0x00132A00
	protected virtual void ᜁ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 13;
				continue;
			case 1:
				return;
			case 2:
				goto IL_5F;
			case 3:
				goto IL_1D1;
			case 4:
				goto IL_74;
			case 6:
				goto IL_14A;
			case 7:
				goto IL_A0;
			case 8:
				goto IL_232;
			case 9:
			{
				ShapeFillType fillType;
				switch (fillType)
				{
				case ShapeFillType.SolidColor:
				{
					TextBoxShapeBase textBoxShapeBase;
					this.ᜂ(A_0, textBoxShapeBase);
					num = 11;
					continue;
				}
				case ShapeFillType.Pattern:
				{
					sprវ a_2 = A_2.ᜋ();
					A_0.WriteAttributeString(RecordTableEnumerator.b("㕀㩂㕄≆", a_), RecordTableEnumerator.b("ㅀ≂ㅄ㍆ⱈ㥊⍌", a_));
					TextBoxShapeBase textBoxShapeBase;
					A_0.WriteAttributeString(RecordTableEnumerator.b("≀ⱂ⥄⡆㭈", a_), this.ᜁ(textBoxShapeBase.Fill.BackColor));
					A_0.WriteAttributeString(RecordTableEnumerator.b("≀ⱂ⥄⡆㭈祊", a_), this.ᜁ(textBoxShapeBase.Fill.ForeColor));
					this.ᜃ(A_0, textBoxShapeBase, a_2, A_3);
					this.ᜄ(A_0, textBoxShapeBase);
					num = 6;
					continue;
				}
				case ShapeFillType.Texture:
				{
					A_0.WriteAttributeString(RecordTableEnumerator.b("㕀㩂㕄≆", a_), RecordTableEnumerator.b("㕀⩂⥄≆", a_));
					sprវ a_2 = A_2.ᜋ();
					TextBoxShapeBase textBoxShapeBase;
					this.ᜁ(A_0, textBoxShapeBase, a_2, A_3);
					this.ᜄ(A_0, textBoxShapeBase);
					num = 8;
					continue;
				}
				case ShapeFillType.Picture:
				{
					A_0.WriteAttributeString(RecordTableEnumerator.b("㕀㩂㕄≆", a_), RecordTableEnumerator.b("❀ㅂ⑄⩆ⱈ", a_));
					sprវ a_2 = A_2.ᜋ();
					TextBoxShapeBase textBoxShapeBase;
					this.ᜅ(A_0, textBoxShapeBase, a_2, A_3);
					num = 3;
					continue;
				}
				case ShapeFillType.UnknownGradient:
				case (ShapeFillType)5:
				case (ShapeFillType)6:
					goto IL_2DB;
				case ShapeFillType.Gradient:
				{
					TextBoxShapeBase textBoxShapeBase;
					this.ᜃ(A_0, textBoxShapeBase);
					num = 4;
					continue;
				}
				default:
					if (true)
					{
					}
					num = 10;
					continue;
				}
				break;
			}
			case 10:
				num = 7;
				continue;
			case 11:
				goto IL_18D;
			case 12:
				goto IL_16B;
			case 13:
			{
				if (!A_1.HasFill)
				{
					num = 1;
					continue;
				}
				TextBoxShapeBase textBoxShapeBase = A_1 as TextBoxShapeBase;
				A_0.WriteStartElement(RecordTableEnumerator.b("❀⩂⥄⭆", a_), RecordTableEnumerator.b("㑀ㅂ⭄絆㩈⡊╌⩎㱐㉒♔穖㑘㉚㹜ⵞ๠ၢ੤Ŧᵨ䙪๬nᱰ䥲ʹ᩶ᕸ", a_));
				ShapeFillType fillType = textBoxShapeBase.Fill.FillType;
				num = 9;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_5F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_74:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			break;
		}
		IL_A0:
		IL_14A:
		goto IL_2DB;
		IL_16B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕀♂㵄㍆ୈ⑊㕌", a_));
		IL_18D:
		IL_1D1:
		IL_232:
		IL_2DB:
		A_0.WriteEndElement();
	}

	// Token: 0x06002280 RID: 8832 RVA: 0x00133CF0 File Offset: 0x00132CF0
	protected virtual void ᜂ(XmlWriter A_0, TextBoxShapeBase A_1)
	{
		int a_ = 12;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!spr\u2175.ᜀ(A_1.Fill.ForeColor))
				{
					num = 1;
					continue;
				}
				goto IL_117;
			case 1:
				goto IL_90;
			case 2:
				goto IL_112;
			case 3:
				goto IL_43;
			case 5:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_90;
				}
				if (false)
				{
				}
				num = 0;
				continue;
			case 6:
				goto IL_F4;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 5;
			continue;
			IL_90:
			if (true)
			{
			}
			string value = this.ᜁ(A_1.Fill.ForeColor);
			A_0.WriteAttributeString(RecordTableEnumerator.b("⅁⭃⩅❇㡉", a_), value);
			num = 6;
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_F4:
		goto IL_117;
		IL_112:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙁⅃㹅㱇ࡉ⍋㙍", a_));
		IL_117:
		this.ᜄ(A_0, A_1);
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x00133E1C File Offset: 0x00132E1C
	protected virtual void ᜃ(XmlWriter A_0, TextBoxShapeBase A_1)
	{
		int a_ = 19;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_EC:
			num = 9;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_D7;
			case 2:
				goto IL_15F;
			case 3:
				goto IL_108;
			case 4:
			{
				GradientColorType gradientColorType;
				switch (gradientColorType)
				{
				case GradientColorType.OneColor:
					if (true)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("⩈⑊⅌⁎⍐", a_), this.ᜁ(A_1.Fill.BackColor));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⩈⑊⅌⁎⍐慒", a_), this.ᜁ(A_1.Fill.GradientDegree));
					num = 1;
					continue;
				case GradientColorType.TwoColor:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⩈⑊⅌⁎⍐", a_), this.ᜁ(A_1.Fill.BackColor));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⩈⑊⅌⁎⍐慒", a_), this.ᜁ(A_1.Fill.ForeColor));
					num = 6;
					continue;
				case GradientColorType.Preset:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⑈⹊㥌❎㹐㝒", a_), RecordTableEnumerator.b("❈⑊⍌⩎", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⩈⑊⅌⁎⍐⁒", a_), this.ᜀ(A_1.Fill.PresetGradientType));
					num = 2;
					continue;
				default:
					num = 5;
					continue;
				}
				break;
			}
			case 5:
				num = 8;
				continue;
			case 6:
				goto IL_1CD;
			case 7:
				goto IL_75;
			case 8:
				goto IL_E7;
			case 9:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				GradientColorType gradientColorType = A_1.Fill.GradientColorType;
				num = 4;
				continue;
			}
			}
			if (A_0 != null)
			{
				goto IL_EC;
			}
			num = 7;
		}
		IL_75:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_D7:
		IL_E7:
		goto IL_21C;
		IL_108:
		throw new ArgumentNullException(RecordTableEnumerator.b("㵈⹊㕌㭎ፐ㱒ⵔ", a_));
		IL_15F:
		IL_1CD:
		IL_21C:
		this.ᜅ(A_0, A_1);
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x00134050 File Offset: 0x00133050
	protected virtual void ᜁ(XmlWriter A_0, TextBoxShapeBase A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 4;
				GradientTextureType texture;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (texture != GradientTextureType.UserDefined)
						{
							num = 7;
							continue;
						}
						goto IL_277;
					case 1:
						if (A_1 == null)
						{
							num = 9;
							continue;
						}
						num = 8;
						continue;
					case 2:
						if (A_3 == null)
						{
							num = 5;
							continue;
						}
						texture = A_1.Fill.Texture;
						num = 0;
						continue;
					case 3:
						goto IL_25E;
					case 5:
						goto IL_135;
					case 6:
						goto IL_79;
					case 7:
						goto IL_103;
					case 8:
						if (A_2 == null)
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
					case 9:
						goto IL_C9;
					}
					if (A_0 == null)
					{
						num = 6;
					}
					else
					{
						num = 1;
					}
				}
				IL_79:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
				IL_103:
				string str = RecordTableEnumerator.b("洸帺䔼䬾", a_);
				int num2 = (int)texture;
				byte[] resData = XlsShapeFill.GetResData(str + num2.ToString());
				byte[] array = new byte[resData.Length - 25];
				Array.Copy(resData, 25, array, 0, array.Length);
				MemoryStream memoryStream = new MemoryStream();
				XlsShapeFill.ᜀ(memoryStream, resData);
				memoryStream.Write(array, 0, array.Length);
				Image image = Image.FromStream(memoryStream, true, true);
				string text = texture.ToString();
				text = text.Replace('_', ' ').Trim();
				string arg = A_2.ᜀ(image, null);
				string text2 = A_3.GenerateRelationId();
				A_3[text2] = new sprᦨ('/' + arg, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ቼ᥾춈搜ﲐﮔ뚘ꦚ궜꾞鞠貢힤슦얨쪪\ud9ac욮\udeb0\uddb2운\udfb6킸쮺캼邾ꣀ껂꓄ꃆ곈", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䬸帺儼嘾╀", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ᑺ᭼᥾", a_), text2);
				A_0.WriteAttributeString(RecordTableEnumerator.b("䴸刺䤼匾⑀", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ᑺ᭼᥾", a_), text);
				return;
				IL_135:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺儼帾㕀⩂⩄⥆㩈", a_));
				IL_25E:
				throw new ArgumentNullException(RecordTableEnumerator.b("儸吺儼嬾⑀ㅂ", a_));
				IL_277:
				image = A_1.Fill.Picture;
				string pictureName = A_1.Fill.PictureName;
				A_1.Fill.CustomTexture(image, pictureName);
				this.ᜄ(A_0, A_1, A_2, A_3);
				return;
			}
			}
			break;
		}
		IL_C9:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴸帺䔼䬾̀ⱂ㵄", a_));
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x00134308 File Offset: 0x00133308
	protected virtual void ᜃ(XmlWriter A_0, TextBoxShapeBase A_1, sprវ A_2, RelationsCollection A_3)
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
		throw new NotImplementedException(RecordTableEnumerator.b("样嬹䠻䨽┿ぁ⩃", a_));
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x00134360 File Offset: 0x00133360
	protected virtual void ᜅ(XmlWriter A_0, TextBoxShapeBase A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 1;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				default:
					goto IL_F1;
				}
				break;
			case 1:
				goto IL_44;
			case 2:
				goto IL_6A;
			case 3:
				if (A_2 == null)
				{
					num = 6;
					continue;
				}
				num = 2;
				continue;
			case 4:
				goto IL_80;
			case 6:
				goto IL_98;
			case 7:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_10D;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 3;
			continue;
			IL_6A:
			if (A_3 == null)
			{
				num = 4;
			}
			else
			{
				num = 7;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_80:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸场尼䬾⡀ⱂ⭄㑆", a_));
		IL_98:
		throw new ArgumentNullException(RecordTableEnumerator.b("弶嘸场夼娾㍀", a_));
		IL_F1:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䌶尸䌺䤼紾⹀㭂", a_));
		IL_10D:
		Image picture = A_1.Fill.Picture;
		string pictureName = A_1.Fill.PictureName;
		ShapeFillType fillType = A_1.Fill.FillType;
		A_1.Fill.FillType = ShapeFillType.SolidColor;
		A_0.WriteAttributeString(RecordTableEnumerator.b("堶䤸娺帼嘾㕀㩂", a_), this.ᜀ(A_1.Fill.Transparency));
		A_1.Fill.CustomPicture(picture, pictureName);
		this.ᜄ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06002285 RID: 8837 RVA: 0x001344E8 File Offset: 0x001334E8
	protected virtual void ᜄ(XmlWriter A_0, TextBoxShapeBase A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9A;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9A;
					default:
						goto IL_77;
					}
					break;
				case 2:
					if (A_2 == null)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 3:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					goto IL_127;
				case 5:
					goto IL_C4;
				case 6:
					goto IL_5F;
				case 7:
					goto IL_111;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 2;
				continue;
				IL_9A:
				if (A_3 == null)
				{
					num = 1;
				}
				else
				{
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
			IL_77:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻刽ℿ㙁ⵃ⥅♇㥉", a_));
			IL_C4:
			throw new ArgumentNullException(RecordTableEnumerator.b("刹医刽␿❁㙃", a_));
			IL_111:
			throw new ArgumentNullException(RecordTableEnumerator.b("丹夻䘽㐿A⭃㹅", a_));
			IL_127:
			Image picture = A_1.Fill.Picture;
			string pictureName = A_1.Fill.PictureName;
			string arg = A_2.ᜀ(picture, null);
			string text = A_3.GenerateRelationId();
			A_3[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ᅽ캉ﾑ떙꺛꺝邟钡讣풥춧용춫\udaad\ud9af\uddb1\udab3억킷펹첻춽ꯁ꧃Ʂ꿇꿉", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("䠹夻刽⤿♁", a_), RecordTableEnumerator.b("伹主倽稿ㅁ❃⹅ⵇ❉ⵋ㵍絏㽑㵓㕕⩗㕙⽛ㅝٟᙡ䥣եݧݩ噫ŭᙯᑱᵳᕵᵷ䁹፻᡽", a_), text);
			A_0.WriteAttributeString(RecordTableEnumerator.b("丹唻䨽ⰿ❁", a_), RecordTableEnumerator.b("伹主倽稿ㅁ❃⹅ⵇ❉ⵋ㵍絏㽑㵓㕕⩗㕙⽛ㅝٟᙡ䥣եݧݩ噫ŭᙯᑱᵳᕵᵷ䁹፻᡽", a_), pictureName);
			return;
		}
		}
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x001346B4 File Offset: 0x001336B4
	protected virtual void ᜅ(XmlWriter A_0, TextBoxShapeBase A_1)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			GradientVariantsType gradientVariant;
			switch (num)
			{
			case 0:
				goto IL_7E;
			case 1:
				goto IL_79;
			case 3:
				return;
			case 4:
				goto IL_7E;
			case 5:
			{
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				A_0.WriteAttributeString(RecordTableEnumerator.b("❇㩉ⵋⵍ㥏♑ⵓ", a_), this.ᜀ(A_1.Fill.TransparencyFrom));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㩇⽉⽋⅍㱏㵑♓", a_), RecordTableEnumerator.b("㱇", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㩇╉㡋⽍⑏㝑", a_), RecordTableEnumerator.b("㱇", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("❇㩉ⵋⵍ㥏♑ⵓ摕", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), this.ᜀ(A_1.Fill.TransparencyTo));
				GradientStyleType gradientStyle = A_1.Fill.GradientStyle;
				num = 6;
				continue;
			}
			case 6:
			{
				GradientStyleType gradientStyle;
				switch (gradientStyle)
				{
				case GradientStyleType.Horizontal:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕", a_));
					num = 7;
					continue;
				case GradientStyleType.Vertical:
				{
					double num2 = -90.0;
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥇⑉⭋≍㕏", a_), num2.ToString());
					num = 15;
					continue;
				}
				case GradientStyleType.Diagonl_Up:
				{
					double num2 = -135.0;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥇⑉⭋≍㕏", a_), num2.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕", a_));
					num = 12;
					continue;
				}
				case GradientStyleType.Diagonl_Down:
				{
					double num2 = -45.0;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥇⑉⭋≍㕏", a_), num2.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕", a_));
					num = 4;
					continue;
				}
				case GradientStyleType.From_Corner:
				{
					double num2 = -45.0;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥇⑉⭋≍㕏", a_), num2.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕੗㭙㡛㝝ş๡", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⹇⍉⁋≍", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⵇ㉉㡋", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_), RecordTableEnumerator.b("㹇⍉⥋㥍", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕᭗㽙㉛⩝՟ၡ", a_));
					A_0.WriteEndElement();
					num = 9;
					continue;
				}
				case GradientStyleType.From_Center:
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
						double num2 = -45.0;
						A_0.WriteAttributeString(RecordTableEnumerator.b("⥇⑉⭋≍㕏", a_), num2.ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("⽇㡉ⵋ⩍㥏㝑㩓≕੗㭙㡛㝝ş๡", a_));
						num = 0;
						continue;
					}
					}
					break;
				default:
					num = 11;
					continue;
				}
				break;
			}
			case 7:
				goto IL_7E;
			case 8:
				goto IL_7E;
			case 9:
				goto IL_7E;
			case 10:
				goto IL_247;
			case 11:
				num = 8;
				continue;
			case 12:
				goto IL_7E;
			case 13:
				switch (gradientVariant)
				{
				case GradientVariantsType.ShadingVariants1:
					goto IL_1FC;
				case GradientVariantsType.ShadingVariants2:
					return;
				case GradientVariantsType.ShadingVariants3:
					goto IL_3C6;
				case GradientVariantsType.ShadingVariants4:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⹇╉⽋㭍⍏", a_), -50 + RecordTableEnumerator.b("浇", a_));
					num = 14;
					continue;
				default:
					num = 3;
					continue;
				}
				break;
			case 14:
				goto IL_356;
			case 15:
				goto IL_7E;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 5;
			continue;
			IL_7E:
			gradientVariant = A_1.Fill.GradientVariant;
			num = 13;
		}
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_1FC:
		A_0.WriteAttributeString(RecordTableEnumerator.b("⹇╉⽋㭍⍏", a_), 100 + RecordTableEnumerator.b("浇", a_));
		return;
		IL_247:
		throw new ArgumentNullException(RecordTableEnumerator.b("㱇⽉㑋㩍቏㵑ⱓ", a_));
		IL_356:
		return;
		IL_3C6:
		A_0.WriteAttributeString(RecordTableEnumerator.b("⹇╉⽋㭍⍏", a_), 50 + RecordTableEnumerator.b("浇", a_));
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x00134BE4 File Offset: 0x00133BE4
	protected virtual void ᜄ(XmlWriter A_0, TextBoxShapeBase A_1)
	{
		int a_ = 16;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_57:
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 0;
			}
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_8B;
			case 3:
				goto IL_62;
			}
			break;
		}
		goto IL_57;
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉅ⵇ㉉㡋్㽏⩑", a_));
		IL_A1:
		ShapeFillType fillType = A_1.Fill.FillType;
		A_1.Fill.FillType = ShapeFillType.SolidColor;
		A_0.WriteAttributeString(RecordTableEnumerator.b("⥅㡇⭉⽋❍⑏⭑", a_), this.ᜀ(A_1.Fill.Transparency));
		A_1.Fill.FillType = fillType;
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑅ⵇ⥉⍋≍㽏⁑", a_), RecordTableEnumerator.b("㉅", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑅❇㹉ⵋ㩍㕏", a_), RecordTableEnumerator.b("㉅", a_));
	}

	// Token: 0x06002288 RID: 8840 RVA: 0x00134D20 File Offset: 0x00133D20
	protected virtual void ᜂ(XmlWriter A_0, TextBoxShapeBase A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 2;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			num = 7;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B4;
			case 1:
				goto IL_167;
			case 2:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䬷丹主儽⬿❁", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_));
				num = 6;
				continue;
			case 3:
				this.ᜀ(A_0, A_1, A_2, A_3);
				A_0.WriteAttributeString(RecordTableEnumerator.b("嬷唹倻儽㈿灁", a_), '#' + this.ᜁ(A_1.Line.ForeColor));
				num = 0;
				continue;
			case 4:
				goto IL_182;
			case 5:
				goto IL_6B;
			case 6:
				if (A_1.Line.HasPattern)
				{
					num = 3;
					continue;
				}
				A_0.WriteAttributeString(RecordTableEnumerator.b("尷嬹伻嘽㌿㙁㵃⩅ⵇ", a_), this.ᜀ(A_1.Line.DashStyle));
				A_0.WriteAttributeString(RecordTableEnumerator.b("吷匹刻嬽㌿㙁㵃⩅ⵇ", a_), this.ᜀ(A_1.Line.Style));
				A_0.WriteAttributeString(RecordTableEnumerator.b("嬷唹倻儽㈿灁", a_), '#' + this.ᜁ(A_1.Line.ForeColor));
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 2;
			}
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_B4:
		IL_167:
		goto IL_1DE;
		IL_182:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰷弹䐻䨽ȿⵁ㱃", a_));
		IL_1DE:
		A_0.WriteEndElement();
	}

	// Token: 0x06002289 RID: 8841 RVA: 0x00134F14 File Offset: 0x00133F14
	protected virtual void ᜀ(XmlWriter A_0, TextBoxShapeBase A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 18;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_65;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 2;
				break;
			}
			break;
		}
		for (;;)
		{
			IL_3E:
			switch (num)
			{
			case 0:
				if (A_2 == null)
				{
					num = 7;
					continue;
				}
				num = 3;
				continue;
			case 1:
				goto IL_BB;
			case 3:
				if (A_3 == null)
				{
					num = 4;
					continue;
				}
				goto IL_121;
			case 4:
				goto IL_10B;
			case 5:
				goto IL_79;
			case 6:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
			case 7:
				goto IL_A4;
			}
			goto IL_65;
		}
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_A4:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁇╉⁋⩍㕏⁑", a_));
		IL_BB:
		throw new ArgumentNullException(RecordTableEnumerator.b("㱇⽉㑋㩍቏㵑ⱓ", a_));
		IL_10B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⁋⽍⑏㭑㭓㡕⭗", a_));
		IL_121:
		GradientPatternType pattern = A_1.Line.Pattern;
		string str = RecordTableEnumerator.b("ᡇ⭉㡋㩍", a_);
		int num2 = (int)pattern;
		byte[] resData = XlsShapeFill.GetResData(str + num2.ToString());
		byte[] array = new byte[resData.Length - 25];
		Array.Copy(resData, 25, array, 0, array.Length);
		MemoryStream memoryStream = new MemoryStream();
		XlsShapeFill.ᜀ(memoryStream, resData);
		memoryStream.Write(array, 0, array.Length);
		Image a_2 = spr\u17FF.ᜀ(memoryStream);
		string value = pattern.ToString();
		value = this.ᜀ(pattern);
		string arg = A_2.ᜀ(a_2, null);
		string text = A_3.GenerateRelationId();
		A_3[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﮑ\udc97ﾛ춟잡쪣튥螧颩鲫麭蚯鶱욳펵풷\udbb9좻ힽ꾿곁럃껅ꇇ뫉뿋맏뿑뗓뇕뷗", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㩇⽉⁋❍㑏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), text);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㱇⍉㡋≍㕏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), value);
		return;
		IL_65:
		if (A_0 == null)
		{
			if (true)
			{
			}
			num = 5;
			goto IL_3E;
		}
		num = 6;
		goto IL_3E;
	}

	// Token: 0x0600228A RID: 8842 RVA: 0x00135148 File Offset: 0x00134148
	protected string ᜁ(double A_0)
	{
		int a_ = 4;
		string text;
		for (;;)
		{
			for (;;)
			{
				text = null;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return text;
					case 1:
						if (A_0 > 0.5)
						{
							num = 2;
							continue;
						}
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
							text = RecordTableEnumerator.b("尹唻刽ⰿ扁⡃⽅⽇≉㡋⭍㹏穑", a_);
							text += (int)(A_0 * 255.0);
							text += RecordTableEnumerator.b("ጹ", a_);
							num = 0;
							continue;
						}
						break;
					case 2:
						text = RecordTableEnumerator.b("尹唻刽ⰿ扁⁃❅㩇ⅉ⥋⁍硏", a_);
						text += (int)(A_0 * 255.0 + 0.5);
						text += RecordTableEnumerator.b("ጹ", a_);
						num = 3;
						continue;
					case 3:
						return text;
					}
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x0600228B RID: 8843 RVA: 0x00135264 File Offset: 0x00134264
	protected string ᜁ(Color A_0)
	{
		int a_ = 5;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return '#' + this.ᜀ((A_0.ToArgb() & 16777215).ToString(RecordTableEnumerator.b("挺଼", a_)));
	}

	// Token: 0x0600228C RID: 8844 RVA: 0x001352E0 File Offset: 0x001342E0
	protected string ᜀ(GradientPresetType A_0)
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		ResourceManager resourceManager = new ResourceManager(RecordTableEnumerator.b("漻丽⤿ぁ⅃桅၇ىὋ恍ُὑᡓᅕ⩗㭙㡛㝝՟ౡၣ", a_), typeof(spr\u1A65).Assembly);
		return resourceManager.GetString(A_0.ToString(), CultureInfo.CurrentCulture);
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x00135360 File Offset: 0x00134360
	protected string ᜀ(GradientPatternType A_0)
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string text = A_0.ToString();
		text = text.Remove(0, RecordTableEnumerator.b("䤸娺䤼怾", a_).ToString().Length - 1);
		text = text.Replace('_', ' ');
		return text.Trim();
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x001353E8 File Offset: 0x001343E8
	protected string ᜀ(double A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0 = 1.0 - A_0;
		return (int)(A_0 * 65536.0) + RecordTableEnumerator.b("⁅", a_);
	}

	// Token: 0x0600228F RID: 8847 RVA: 0x00135460 File Offset: 0x00134460
	protected string ᜀ(string A_0)
	{
		int a_ = 2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_61:
			goto IL_63;
		case 1:
			goto IL_29;
		default:
			goto IL_29;
		}
		int num;
		int num2;
		for (;;)
		{
			IL_39:
			switch (num)
			{
			case 0:
				if (A_0.StartsWith(RecordTableEnumerator.b("࠷", a_)))
				{
					num = 4;
					continue;
				}
				return A_0;
			case 1:
				goto IL_94;
			case 2:
				goto IL_61;
			case 3:
				if (num2 >= A_0.Length)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			case 4:
				A_0 = A_0.Remove(0, 1);
				num2++;
				num = 1;
				continue;
			case 5:
				goto IL_7C;
			}
			goto IL_57;
		}
		IL_7C:
		return A_0;
		IL_94:
		goto IL_63;
		IL_29:
		if (false)
		{
		}
		if (true)
		{
		}
		IL_57:
		num2 = 0;
		num = 2;
		goto IL_39;
		IL_63:
		num = 3;
		goto IL_39;
	}

	// Token: 0x06002290 RID: 8848 RVA: 0x00135530 File Offset: 0x00134530
	protected string ᜀ(ShapeDashLineStyleType A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		int num;
		IEnumerator<string> enumerator2;
		for (;;)
		{
			IL_30:
			IEnumerator<ShapeDashLineStyleType> enumerator;
			switch (num)
			{
			case 0:
				if (enumerator.Current != A_0)
				{
					num = 2;
					continue;
				}
				goto IL_FB;
			case 1:
				spr\u2316.ᜁ();
				num = 3;
				continue;
			case 2:
				goto IL_D8;
			case 3:
				goto IL_89;
			case 4:
				goto IL_D8;
			case 5:
				goto IL_F9;
			case 6:
				if (!enumerator2.MoveNext())
				{
					num = 5;
					continue;
				}
				enumerator.MoveNext();
				num = 0;
				continue;
			}
			if (spr\u2316.ᜏ == null)
			{
				num = 1;
				continue;
			}
			IL_89:
			Dictionary<string, ShapeDashLineStyleType> ᜏ = spr\u2316.ᜏ;
			enumerator2 = ᜏ.Keys.GetEnumerator();
			enumerator = ᜏ.Values.GetEnumerator();
			if (true)
			{
			}
			num = 4;
			continue;
			IL_D8:
			num = 6;
		}
		IL_F9:
		IL_FB:
		return enumerator2.Current;
		IL_20:
		if (false)
		{
		}
		num = 7;
		goto IL_30;
	}

	// Token: 0x06002291 RID: 8849 RVA: 0x00135640 File Offset: 0x00134640
	protected string ᜀ(ShapeLineStyleType A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		int num;
		IEnumerator<string> enumerator;
		for (;;)
		{
			IL_30:
			IEnumerator<ShapeLineStyleType> enumerator2;
			switch (num)
			{
			case 0:
				goto IL_D0;
			case 1:
				if (!enumerator.MoveNext())
				{
					num = 3;
					continue;
				}
				enumerator2.MoveNext();
				num = 5;
				continue;
			case 3:
				goto IL_F9;
			case 4:
				goto IL_89;
			case 5:
				if (enumerator2.Current != A_0)
				{
					num = 6;
					continue;
				}
				goto IL_FB;
			case 6:
				goto IL_D0;
			case 7:
				spr\u2316.ᜂ();
				num = 4;
				continue;
			}
			if (spr\u2316.ᜎ == null)
			{
				num = 7;
				continue;
			}
			IL_89:
			Dictionary<string, ShapeLineStyleType> ᜎ = spr\u2316.ᜎ;
			enumerator = ᜎ.Keys.GetEnumerator();
			enumerator2 = ᜎ.Values.GetEnumerator();
			num = 0;
			continue;
			IL_D0:
			if (true)
			{
			}
			num = 1;
		}
		IL_F9:
		IL_FB:
		return enumerator.Current;
		IL_20:
		if (false)
		{
		}
		num = 2;
		goto IL_30;
	}

	// Token: 0x06002292 RID: 8850 RVA: 0x00135750 File Offset: 0x00134750
	public static bool ᜀ(Color A_0)
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
		return A_0 == spr\u1D39.ᜂ;
	}

	// Token: 0x04001203 RID: 4611
	public const string ᜀ = "f";

	// Token: 0x04001204 RID: 4612
	public const string ᜁ = "t";
}

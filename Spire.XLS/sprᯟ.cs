using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004C9 RID: 1225
internal class sprᯟ : IDisposable
{
	// Token: 0x06004B58 RID: 19288 RVA: 0x002DDE34 File Offset: 0x002DCE34
	private XmlTextWriter ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x06004B59 RID: 19289 RVA: 0x002DDE78 File Offset: 0x002DCE78
	public sprᯟ()
	{
		this.ᜉ = new sprᯟ.ᜁ();
	}

	// Token: 0x06004B5A RID: 19290 RVA: 0x002DDEA4 File Offset: 0x002DCEA4
	public void ᜀ(Stream A_0, XlsWorkbook A_1, string A_2, HTMLOptions A_3)
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
		this.ᜈ = sprᯟ.ConversionMode.Workbook;
		this.ᜃ = new XmlTextWriter(A_0, Encoding.UTF8);
		this.ᜃ.Formatting = Formatting.Indented;
		this.ᜄ = new Dictionary<string, LinkedList<string>>();
		this.ᜆ = new List<spr\u25A6.ᜀ>();
		this.ᜁ();
		this.ᜁ(A_1, A_2, A_3);
		this.ᜀ(A_1, A_2);
		this.ᜀ(A_1, A_2, A_3);
	}

	// Token: 0x06004B5B RID: 19291 RVA: 0x002DDF3C File Offset: 0x002DCF3C
	public void ᜀ(Stream A_0, XlsWorksheet A_1, string A_2, HTMLOptions A_3)
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
		this.ᜈ = sprᯟ.ConversionMode.Worksheet;
		this.ᜃ = new XmlTextWriter(A_0, Encoding.UTF8);
		this.ᜃ.Formatting = Formatting.Indented;
		this.ᜄ = new Dictionary<string, LinkedList<string>>();
		this.ᜆ = new List<spr\u25A6.ᜀ>();
		this.ᜁ();
		this.ᜀ(A_1, A_3, this.ᜃ);
		this.ᜂ().WriteEndElement();
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜄ);
		this.ᜀ(A_1, A_2, A_3, this.ᜃ);
		this.ᜂ().WriteEndElement();
		this.ᜀ();
	}

	// Token: 0x06004B5C RID: 19292 RVA: 0x002DE004 File Offset: 0x002DD004
	private void ᜁ(XlsWorkbook A_0, string A_1, HTMLOptions A_2)
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		string name = new DirectoryInfo(A_1).Name;
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.\u1712);
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u173B, RecordTableEnumerator.b("мా摀潂牄扆", a_));
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u173C, RecordTableEnumerator.b("഼", a_));
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.\u1713);
		string path = string.Format(RecordTableEnumerator.b("䘼༾㱀浂ⵄ㍆⑈❊", a_), A_0.Worksheets[0].Name);
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u1717, Path.Combine(name, path));
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u173D, RecordTableEnumerator.b("䤼倾ㅀ", a_));
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u173E, RecordTableEnumerator.b("尼䨾㕀ⱂ", a_));
		this.ᜂ().WriteEndElement();
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.\u1713);
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u1717, Path.Combine(name, RecordTableEnumerator.b("䤼帾⍀あ歄⽆㵈♊⅌", a_)));
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u173D, RecordTableEnumerator.b("弼倾㕀㝂⩄⩆", a_));
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u173E, RecordTableEnumerator.b("尼䨾㕀ⱂ", a_));
		this.ᜂ().WriteEndElement();
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.\u1714);
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜄ);
		this.ᜂ().WriteString(RecordTableEnumerator.b("椼圾⡀あ敄㍆ⱈ㍊㥌潎♐㩒㥔㭖祘㩚ⵜ⽞Ѡɢᝤ䝦٨ժŬ᙮兰ᩲ፴坶൸፺᡼彾愈ﾌ꾎ﲒ릘膠킢키힦\ud9a8쒪\udfac\udbae醰햲잴횶풸\udeba캼醾", a_));
		this.ᜀ();
		this.ᜀ();
		this.ᜀ();
		this.ᜀ();
	}

	// Token: 0x06004B5D RID: 19293 RVA: 0x002DE200 File Offset: 0x002DD200
	private void ᜀ(XlsWorkbook A_0, string A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				string name = new DirectoryInfo(A_1).Name;
				int count = A_0.Worksheets.Count;
				string path = Path.Combine(A_1, RecordTableEnumerator.b("伺尼崾㉀浂ⵄ㍆⑈❊", a_));
				FileStream w = new FileStream(path, FileMode.OpenOrCreate);
				this.ᜃ = new XmlTextWriter(w, Encoding.UTF8);
				this.ᜃ.Formatting = Formatting.Indented;
				this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜀ);
				this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜃ);
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u1736, RecordTableEnumerator.b("伺堼䜾㕀求♄㑆㩈", a_));
				this.ᜂ().WriteString(sprᯟ.ᜃ.ᜈ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜀ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᝆ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜄ);
				this.ᜂ().WriteString(RecordTableEnumerator.b("堺刼匾ⵀ≂㕄㑆ⱈ", a_));
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᝇ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜄ);
				this.ᜂ().WriteString(RecordTableEnumerator.b("଺", a_));
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᝈ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜄ);
				this.ᜂ().WriteString(RecordTableEnumerator.b("䠺唼倾㙀", a_));
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜁ);
				this.ᜂ().WriteString(sprᯟ.ᜃ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜀ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜮ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u173F + sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜬ + sprᯟ.ᜀ.ᜄ + RecordTableEnumerator.b("洺堼䴾╀≂⭄♆", a_) + sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.\u170D + sprᯟ.ᜀ.ᜄ + RecordTableEnumerator.b("਺฼伾㥀", a_) + sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜥ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.ᜂ + sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜅ + sprᯟ.ᜀ.ᜄ + RecordTableEnumerator.b("䤺娼崾楀煂灄牆效祊硌穎結慒恔扖灘", a_) + sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜁ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᝅ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜀ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜮ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u173F + sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜁ);
				this.ᜂ().WriteString(RecordTableEnumerator.b("ᔺ攼฾", a_));
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜀ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.\u1715);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜄ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.\u1716);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜐ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜄ);
				this.ᜂ().WriteString(RecordTableEnumerator.b("଺堼刾", a_));
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜂ.ᜰ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜄ);
				this.ᜂ().WriteString(RecordTableEnumerator.b("଺䴼䜾", a_));
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜅ);
				this.ᜂ().WriteString(sprᯟ.ᜀ.ᜁ);
				this.ᜀ();
				this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜄ);
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᝀ, RecordTableEnumerator.b("䤺娼崾楀獂楄睆效祊硌穎硐", a_));
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᝁ, RecordTableEnumerator.b("䤺娼崾楀獂楄睆效筊摌", a_));
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᝂ, RecordTableEnumerator.b("䤺娼崾楀獂楄睆效筊摌", a_));
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᜆ, RecordTableEnumerator.b("ᠺԼ༾祀獂組睆", a_));
				this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜈ);
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᜉ, RecordTableEnumerator.b("଺", a_));
				this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜊ);
				this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᜈ, RecordTableEnumerator.b("挺఼", a_));
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_526;
					case 1:
						goto IL_545;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54A;
						default:
							if (false)
							{
							}
							goto IL_526;
						}
						break;
					case 3:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						goto IL_54A;
					}
					break;
					IL_526:
					num2 = 3;
					continue;
					IL_54A:
					this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜉ);
					string value = string.Format(RecordTableEnumerator.b("䀺഼䈾潀⭂ㅄ⩆╈", a_), A_0.Worksheets[num].Name);
					this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜅ);
					this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᝄ, value);
					this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.ᝃ, sprᯟ.ᜂ.ᜰ);
					this.ᜂ().WriteString(A_0.Worksheets[num].Name);
					this.ᜂ().WriteEndElement();
					this.ᜂ().WriteEndElement();
					num++;
					if (true)
					{
					}
					num2 = 2;
				}
			}
			IL_545:
			this.ᜀ();
			this.ᜀ();
			this.ᜀ();
			this.ᜀ();
			this.ᜂ().Close();
			return;
		}
	}

	// Token: 0x06004B5E RID: 19294 RVA: 0x002DE85C File Offset: 0x002DD85C
	private void ᜀ(XlsWorksheet A_0, HTMLOptions A_1, XmlWriter A_2)
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜃ);
		this.ᜂ().WriteAttributeString(sprᯟ.ᜂ.\u1736, RecordTableEnumerator.b("䈵崷䈹䠻ᄽ⌿ㅁ㝃", a_));
		string text = this.ᜀ(A_0, A_1);
		this.ᜂ().WriteString(text);
		this.ᜀ();
	}

	// Token: 0x06004B5F RID: 19295 RVA: 0x002DE8EC File Offset: 0x002DD8EC
	private void ᜁ()
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
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜀ);
		this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜁ);
	}

	// Token: 0x06004B60 RID: 19296 RVA: 0x002DE948 File Offset: 0x002DD948
	private void ᜀ()
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
		this.ᜂ().WriteEndElement();
		this.ᜂ().Flush();
	}

	// Token: 0x06004B61 RID: 19297 RVA: 0x002DE99C File Offset: 0x002DD99C
	private string ᜀ(XlsWorksheet A_0, HTMLOptions A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				new sprᱥ(new sprᱥ.ᜀ(A_0.GetRowHeightPixels));
				new sprᱥ(new sprᱥ.ᜀ(A_0.GetColumnWidthPixels));
				stringBuilder = new StringBuilder();
				IXLSRange allocatedRange = A_0.AllocatedRange;
				int row = allocatedRange.Row;
				int column = allocatedRange.Column;
				int lastRow = allocatedRange.LastRow;
				int lastColumn = allocatedRange.LastColumn;
				IPictures pictures = A_0.Pictures;
				int num = 0;
				int num2 = 0;
				string text = null;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_89A:
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						sprᯟ.ᜂ.ᜌ,
						sprᯟ.ᜀ.ᜄ,
						sprᯟ.ᜂ.ᜎ,
						sprᯟ.ᜀ.ᜅ
					});
					text2 = this.ᜇ;
					this.ᜇ = string.Concat(new string[]
					{
						text2,
						sprᯟ.ᜂ.ᜌ,
						sprᯟ.ᜀ.ᜄ,
						sprᯟ.ᜂ.ᜎ,
						sprᯟ.ᜀ.ᜅ
					});
					num7 = 67;
					break;
				}
				default:
					if (false)
					{
					}
					num7 = 16;
					break;
				}
				for (;;)
				{
					IXLSRange ixlsrange;
					int num10;
					int num11;
					string text5;
					string text6;
					Dictionary<string, string> dictionary;
					Dictionary<string, string> dictionary2;
					string str2;
					string value3;
					switch (num7)
					{
					case 0:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜰ,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜰ,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 38;
						continue;
					}
					case 1:
						num7 = 60;
						continue;
					case 2:
						goto IL_313;
					case 3:
						if (ixlsrange.VerticalAlignment == VerticalAlignType.Top)
						{
							num7 = 0;
							continue;
						}
						goto IL_C50;
					case 4:
					{
						num4 = this.ᜆ[num6].ᜂ();
						num5 = this.ᜆ[num6].ᜅ();
						int num8 = this.ᜆ[num6].ᜇ();
						int num9 = this.ᜆ[num6].ᜃ();
						string name = (num8 + 1).ToString() + this.ᜀ(num9 + 1);
						IXLSRange a_2 = A_0.AllocatedRange[name];
						num7 = 57;
						continue;
					}
					case 5:
						text = text + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u1712;
						this.ᜇ = this.ᜇ + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u1712;
						num7 = 21;
						continue;
					case 6:
					{
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							sprᯟ.ᜂ.ᜥ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜂ,
							sprᯟ.ᜀ.ᜅ
						});
						string text4 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text4,
							sprᯟ.ᜂ.ᜥ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜂ,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 22;
						continue;
					}
					case 7:
						if (num10 > lastRow)
						{
							num7 = 52;
							continue;
						}
						num11 = 1;
						num7 = 34;
						continue;
					case 8:
						num10++;
						num7 = 14;
						continue;
					case 9:
					{
						if (num11 > lastColumn)
						{
							num7 = 8;
							continue;
						}
						num++;
						string str = num10.ToString();
						text5 = str + this.ᜀ(num11);
						ixlsrange = A_0.AllocatedRange[text5];
						num7 = 56;
						continue;
					}
					case 10:
						goto IL_632;
					case 11:
						if (ixlsrange.Style.Font.IsBold)
						{
							num7 = 6;
							continue;
						}
						goto IL_998;
					case 12:
						text = text + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.ᜐ;
						this.ᜇ = this.ᜇ + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.ᜐ;
						num7 = 53;
						continue;
					case 13:
						text = text + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.ᜑ;
						this.ᜇ = this.ᜇ + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.ᜑ;
						num7 = 30;
						continue;
					case 14:
						goto IL_6F9;
					case 15:
						goto IL_4E7;
					case 16:
						if (A_0.HasMergedCells)
						{
							num7 = 43;
							continue;
						}
						goto IL_AF9;
					case 17:
						goto IL_110D;
					case 18:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.\u1733,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.\u1733,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 32;
						continue;
					}
					case 19:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜱ,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜱ,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 15;
						continue;
					}
					case 20:
						if (num11 == num5 + 1)
						{
							num7 = 82;
							continue;
						}
						goto IL_44D;
					case 21:
						goto IL_423;
					case 22:
						goto IL_998;
					case 23:
						if (ixlsrange.VerticalAlignment == VerticalAlignType.Bottom)
						{
							num7 = 19;
							continue;
						}
						goto IL_4E7;
					case 24:
						this.ᜀ(A_0, stringBuilder);
						num7 = 49;
						continue;
					case 25:
						if (ixlsrange.HorizontalAlignment == HorizontalAlignType.Distributed)
						{
							num7 = 33;
							continue;
						}
						goto IL_148B;
					case 26:
						goto IL_12AC;
					case 27:
						if (!text6.Equals(RecordTableEnumerator.b("ୄ⡆❈⹊", a_)))
						{
							num7 = 76;
							continue;
						}
						goto IL_7F9;
					case 28:
						goto IL_148B;
					case 29:
						if (ixlsrange.Style.Font.IsStrikethrough)
						{
							num7 = 31;
							continue;
						}
						goto IL_1031;
					case 30:
						goto IL_1508;
					case 31:
						goto IL_89A;
					case 32:
						goto IL_14B4;
					case 33:
						text = text + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u1732;
						this.ᜇ = this.ᜇ + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u1732;
						num7 = 28;
						continue;
					case 34:
						goto IL_12AC;
					case 35:
						try
						{
							num7 = 1;
							for (;;)
							{
								switch (num7)
								{
								case 2:
								{
									Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num7 = 3;
										continue;
									}
									string text7 = enumerator.Current;
									LinkedList<string> linkedList = this.ᜄ[text7];
									stringBuilder.AppendLine();
									stringBuilder.Append(linkedList.First.Value);
									stringBuilder.Append(text7);
									num7 = 0;
									continue;
								}
								case 3:
									num7 = 4;
									continue;
								case 4:
									goto IL_D11;
								}
								IL_CA2:
								num7 = 2;
								continue;
								goto IL_CA2;
							}
							IL_D11:
							goto IL_1316;
						}
						finally
						{
							Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						goto IL_D24;
						IL_1316:
						num7 = 79;
						continue;
					case 36:
						if (ixlsrange.VerticalAlignment == VerticalAlignType.Distributed)
						{
							num7 = 39;
							continue;
						}
						goto IL_14DE;
					case 37:
						try
						{
							num7 = 3;
							for (;;)
							{
								switch (num7)
								{
								case 1:
								{
									Dictionary<string, string>.KeyCollection.Enumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num7 = 6;
										continue;
									}
									string text8 = enumerator2.Current;
									num7 = 4;
									continue;
								}
								case 2:
									goto IL_61F;
								case 4:
								{
									string text8;
									if (!dictionary.ContainsKey(text8))
									{
										num7 = 5;
										continue;
									}
									break;
								}
								case 5:
								{
									string text2 = this.ᜇ;
									string text8;
									this.ᜇ = string.Concat(new string[]
									{
										text2,
										text8,
										sprᯟ.ᜀ.ᜄ,
										dictionary2[text8],
										sprᯟ.ᜀ.ᜅ
									});
									num7 = 0;
									continue;
								}
								case 6:
									num7 = 2;
									continue;
								}
								IL_572:
								num7 = 1;
								continue;
								goto IL_572;
							}
							IL_61F:
							goto IL_89F;
						}
						finally
						{
							Dictionary<string, string>.KeyCollection.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_632;
						IL_89F:
						num6++;
						num7 = 58;
						continue;
					case 38:
						goto IL_C50;
					case 39:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.\u1732,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.\u1732,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 64;
						continue;
					}
					case 40:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.\u1712,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜯ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.\u1712,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 48;
						continue;
					}
					case 41:
						goto IL_1302;
					case 42:
						str2 = sprᯟ.ᜂ.\u1734;
						num7 = 46;
						continue;
					case 43:
					{
						spr\u1FBC spr_u1FBC = A_0.MergeCells;
						spr_u1FBC.ᜀ(A_0[row, column, lastRow, lastColumn], this.ᜆ);
						num7 = 45;
						continue;
					}
					case 44:
						if (ixlsrange.VerticalAlignment == VerticalAlignType.Center)
						{
							num7 = 40;
							continue;
						}
						goto IL_1381;
					case 45:
						goto IL_AF9;
					case 46:
						if (ixlsrange.HasNumber)
						{
							num7 = 66;
							continue;
						}
						goto IL_71F;
					case 47:
						if (A_1.ImagePath != null)
						{
							num7 = 24;
							continue;
						}
						goto IL_159F;
					case 48:
						goto IL_1381;
					case 49:
						goto IL_159D;
					case 50:
						goto IL_1302;
					case 51:
						num7 = 47;
						continue;
					case 52:
					{
						string value = string.Concat(new string[]
						{
							sprᯟ.ᜃ.ᜈ,
							sprᯟ.ᜀ.ᜀ,
							sprᯟ.ᜂ.ᝆ,
							sprᯟ.ᜀ.ᜄ,
							RecordTableEnumerator.b("♄⡆╈❊ⱌ㽎≐㙒", a_),
							sprᯟ.ᜀ.ᜅ,
							sprᯟ.ᜂ.ᝇ,
							sprᯟ.ᜀ.ᜄ,
							RecordTableEnumerator.b("畄", a_),
							sprᯟ.ᜀ.ᜅ,
							sprᯟ.ᜂ.ᝈ,
							sprᯟ.ᜀ.ᜄ,
							RecordTableEnumerator.b("㙄⽆♈㱊", a_),
							sprᯟ.ᜀ.ᜁ
						});
						stringBuilder.Append(value);
						Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator = this.ᜄ.Keys.GetEnumerator();
						num7 = 35;
						continue;
					}
					case 53:
						goto IL_1357;
					case 54:
						if (A_0.HasMergedCells)
						{
							num7 = 1;
							continue;
						}
						goto IL_44D;
					case 55:
						goto IL_D24;
					case 56:
					{
						if (A_1.TextMode == HTMLOptions.GetText.NumberText)
						{
							num7 = 65;
							continue;
						}
						string value2 = ixlsrange.Value;
						num7 = 55;
						continue;
					}
					case 57:
						if (num10 == num4 + 1)
						{
							num7 = 10;
							continue;
						}
						goto IL_44D;
					case 58:
						goto IL_44D;
					case 59:
						goto IL_71F;
					case 60:
						if (num6 < this.ᜆ.Count)
						{
							num7 = 4;
							continue;
						}
						goto IL_44D;
					case 61:
						if (ixlsrange.HorizontalAlignment == HorizontalAlignType.Center)
						{
							num7 = 5;
							continue;
						}
						goto IL_423;
					case 62:
						if (ixlsrange.VerticalAlignment == VerticalAlignType.Justify)
						{
							num7 = 18;
							continue;
						}
						goto IL_14B4;
					case 63:
						if (ixlsrange.HorizontalAlignment == HorizontalAlignType.Justify)
						{
							num7 = 78;
							continue;
						}
						goto IL_313;
					case 64:
						goto IL_14DE;
					case 65:
					{
						string numberText = ixlsrange.NumberText;
						num7 = 77;
						continue;
					}
					case 66:
						str2 = sprᯟ.ᜂ.ᜑ;
						num7 = 59;
						continue;
					case 67:
						goto IL_1031;
					case 68:
						goto IL_6F9;
					case 69:
						if (ixlsrange.HorizontalAlignment == HorizontalAlignType.General)
						{
							num7 = 42;
							continue;
						}
						goto IL_110D;
					case 70:
						if (ixlsrange.HorizontalAlignment == HorizontalAlignType.Right)
						{
							num7 = 13;
							continue;
						}
						goto IL_1508;
					case 71:
						goto IL_86C;
					case 72:
						goto IL_7F9;
					case 73:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜌ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜁ,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜌ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜁ,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 71;
						continue;
					}
					case 74:
					{
						LinkedList<string> linkedList2 = new LinkedList<string>();
						linkedList2.AddFirst(value3);
						num3++;
						linkedList2.AddLast(text5);
						this.ᜄ.Add(this.ᜇ, linkedList2);
						num2++;
						num7 = 50;
						continue;
					}
					case 75:
						if (ixlsrange.HorizontalAlignment == HorizontalAlignType.Left)
						{
							num7 = 12;
							continue;
						}
						goto IL_1357;
					case 76:
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜮ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜄ,
							sprᯟ.ᜀ.ᜅ
						});
						text2 = this.ᜇ;
						this.ᜇ = string.Concat(new string[]
						{
							text2,
							sprᯟ.ᜂ.ᜮ,
							sprᯟ.ᜀ.ᜄ,
							sprᯟ.ᜂ.ᜄ,
							sprᯟ.ᜀ.ᜅ
						});
						num7 = 72;
						continue;
					}
					case 77:
						goto IL_D24;
					case 78:
						text = text + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u1733;
						this.ᜇ = this.ᜇ + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + sprᯟ.ᜂ.\u1733;
						num7 = 2;
						continue;
					case 79:
						if (A_0.HasPictures)
						{
							num7 = 51;
							continue;
						}
						goto IL_159F;
					case 80:
						if (ixlsrange.Style.Font.IsItalic)
						{
							num7 = 73;
							continue;
						}
						goto IL_86C;
					case 81:
					{
						if (!this.ᜄ.ContainsKey(this.ᜇ))
						{
							num7 = 74;
							continue;
						}
						LinkedList<string> linkedList3 = this.ᜄ[this.ᜇ];
						linkedList3.AddLast(text5);
						num7 = 41;
						continue;
					}
					case 82:
					{
						dictionary = this.ᜀ(ixlsrange);
						IXLSRange a_2;
						dictionary2 = this.ᜀ(a_2);
						Dictionary<string, string>.KeyCollection.Enumerator enumerator2 = dictionary2.Keys.GetEnumerator();
						num7 = 37;
						continue;
					}
					}
					break;
					IL_313:
					num7 = 25;
					continue;
					IL_423:
					num7 = 75;
					continue;
					IL_44D:
					this.ᜇ = this.ᜀ(ixlsrange, this.ᜇ);
					text = sprᯟ.ᜀ.ᜃ + text5 + this.ᜇ;
					num7 = 11;
					continue;
					IL_4E7:
					num7 = 3;
					continue;
					IL_632:
					if (true)
					{
					}
					num7 = 20;
					continue;
					IL_6F9:
					num7 = 7;
					continue;
					IL_71F:
					text = text + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + str2;
					this.ᜇ = this.ᜇ + sprᯟ.ᜂ.ᜏ + sprᯟ.ᜀ.ᜄ + str2;
					num7 = 17;
					continue;
					IL_7F9:
					num7 = 23;
					continue;
					IL_86C:
					num7 = 29;
					continue;
					IL_998:
					num7 = 80;
					continue;
					IL_AF9:
					num10 = 1;
					num7 = 68;
					continue;
					IL_C50:
					num7 = 44;
					continue;
					IL_D24:
					A_0.GetColumnWidthPixels(num11);
					int rowHeightPixels = A_0.GetRowHeightPixels(num10);
					int rotation = ixlsrange.Style.Rotation;
					string text9 = ixlsrange.Style.Font.FontName.ToString();
					Color color = this.ᜀ(ixlsrange.Style.Font.Color);
					int r = (int)color.R;
					int g = (int)color.G;
					int b = (int)color.B;
					string text10 = string.Concat(new object[]
					{
						sprᯟ.ᜂ.ᜡ,
						sprᯟ.ᜀ.ᜆ,
						r,
						sprᯟ.ᜀ.ᜂ,
						g,
						sprᯟ.ᜀ.ᜂ,
						b,
						sprᯟ.ᜀ.ᜇ
					});
					double size = ixlsrange.Style.Font.Size;
					string name2 = ixlsrange.Style.Color.Name;
					ixlsrange.Style.KnownColor.ToString();
					int r2 = (int)ixlsrange.Style.Color.R;
					int g2 = (int)ixlsrange.Style.Color.G;
					int b2 = (int)ixlsrange.Style.Color.B;
					string text11 = string.Concat(new object[]
					{
						sprᯟ.ᜂ.ᜡ,
						sprᯟ.ᜀ.ᜆ,
						r2,
						sprᯟ.ᜀ.ᜂ,
						g2,
						sprᯟ.ᜀ.ᜂ,
						b2,
						sprᯟ.ᜀ.ᜇ
					});
					text6 = ixlsrange.Style.Font.Underline.ToString(CultureInfo.InstalledUICulture.NumberFormat);
					this.ᜇ = string.Concat(new object[]
					{
						sprᯟ.ᜀ.ᜀ,
						sprᯟ.ᜂ.ᜊ,
						sprᯟ.ᜀ.ᜄ,
						text10,
						sprᯟ.ᜀ.ᜅ,
						sprᯟ.ᜂ.ᜬ,
						sprᯟ.ᜀ.ᜄ,
						text9,
						sprᯟ.ᜀ.ᜅ,
						sprᯟ.ᜂ.\u170D,
						sprᯟ.ᜀ.ᜄ,
						size,
						RecordTableEnumerator.b("㕄㍆", a_),
						sprᯟ.ᜀ.ᜅ,
						sprᯟ.ᜂ.ᜅ,
						sprᯟ.ᜀ.ᜄ,
						text11,
						sprᯟ.ᜀ.ᜅ,
						sprᯟ.ᜂ.\u1738,
						sprᯟ.ᜀ.ᜄ,
						rowHeightPixels,
						sprᯟ.ᜀ.ᜅ
					});
					dictionary = new Dictionary<string, string>();
					dictionary2 = new Dictionary<string, string>();
					num7 = 54;
					continue;
					IL_1031:
					num7 = 27;
					continue;
					IL_110D:
					text += sprᯟ.ᜀ.ᜁ;
					this.ᜇ += sprᯟ.ᜀ.ᜁ;
					value3 = RecordTableEnumerator.b("歄὆", a_) + num3;
					num7 = 81;
					continue;
					IL_12AC:
					num7 = 9;
					continue;
					IL_1302:
					num11++;
					num7 = 26;
					continue;
					IL_1357:
					num7 = 70;
					continue;
					IL_1381:
					num7 = 36;
					continue;
					IL_148B:
					num7 = 69;
					continue;
					IL_14B4:
					num7 = 61;
					continue;
					IL_14DE:
					num7 = 62;
					continue;
					IL_1508:
					num7 = 63;
				}
			}
			IL_159D:
			IL_159F:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x06004B62 RID: 19298 RVA: 0x002DFF6C File Offset: 0x002DEF6C
	private Color ᜀ(Color A_0)
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
		return Color.FromArgb(255, (int)A_0.R, (int)A_0.G, (int)A_0.B);
	}

	// Token: 0x06004B63 RID: 19299 RVA: 0x002DFFC8 File Offset: 0x002DEFC8
	private string ᜀ(IXLSRange A_0, string A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			string text8;
			string text12;
			string text16;
			string text20;
			for (;;)
			{
				string b = RecordTableEnumerator.b("猼倾⽀♂", a_);
				string text = A_0.Borders[BordersLineType.EdgeTop].LineStyle.ToString();
				string text2 = A_0.Borders[BordersLineType.EdgeBottom].LineStyle.ToString();
				string text3 = A_0.Borders[BordersLineType.EdgeLeft].LineStyle.ToString();
				string text4 = A_0.Borders[BordersLineType.EdgeRight].LineStyle.ToString();
				string text5 = A_0.Style.Borders[BordersLineType.EdgeTop].Color.R.ToString();
				string text6 = A_0.Style.Borders[BordersLineType.EdgeTop].Color.G.ToString();
				string text7 = A_0.Style.Borders[BordersLineType.EdgeTop].Color.B.ToString();
				text8 = string.Concat(new string[]
				{
					sprᯟ.ᜂ.ᜡ,
					sprᯟ.ᜀ.ᜆ,
					text5,
					sprᯟ.ᜀ.ᜂ,
					text6,
					sprᯟ.ᜀ.ᜂ,
					text7,
					sprᯟ.ᜀ.ᜇ
				});
				string text9 = A_0.Style.Borders[BordersLineType.EdgeBottom].Color.R.ToString();
				string text10 = A_0.Style.Borders[BordersLineType.EdgeBottom].Color.G.ToString();
				string text11 = A_0.Style.Borders[BordersLineType.EdgeBottom].Color.B.ToString();
				text12 = string.Concat(new string[]
				{
					sprᯟ.ᜂ.ᜡ,
					sprᯟ.ᜀ.ᜆ,
					text9,
					sprᯟ.ᜀ.ᜂ,
					text10,
					sprᯟ.ᜀ.ᜂ,
					text11,
					sprᯟ.ᜀ.ᜇ
				});
				string text13 = A_0.Style.Borders[BordersLineType.EdgeLeft].Color.R.ToString();
				string text14 = A_0.Style.Borders[BordersLineType.EdgeLeft].Color.G.ToString();
				string text15 = A_0.Style.Borders[BordersLineType.EdgeLeft].Color.B.ToString();
				text16 = string.Concat(new string[]
				{
					sprᯟ.ᜂ.ᜡ,
					sprᯟ.ᜀ.ᜆ,
					text13,
					sprᯟ.ᜀ.ᜂ,
					text14,
					sprᯟ.ᜀ.ᜂ,
					text15,
					sprᯟ.ᜀ.ᜇ
				});
				string text17 = A_0.Style.Borders[BordersLineType.EdgeRight].Color.R.ToString();
				string text18 = A_0.Style.Borders[BordersLineType.EdgeRight].Color.G.ToString();
				string text19 = A_0.Style.Borders[BordersLineType.EdgeRight].Color.B.ToString();
				text20 = string.Concat(new string[]
				{
					sprᯟ.ᜂ.ᜡ,
					sprᯟ.ᜀ.ᜆ,
					text17,
					sprᯟ.ᜀ.ᜂ,
					text18,
					sprᯟ.ᜀ.ᜂ,
					text19,
					sprᯟ.ᜀ.ᜇ
				});
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
					{
						object obj = A_1;
						A_1 = string.Concat(new object[]
						{
							obj,
							sprᯟ.ᜂ.\u171A,
							sprᯟ.ᜀ.ᜄ,
							this.ᜀ(text2),
							sprᯟ.ᜀ.ᜅ,
							sprᯟ.ᜂ.ᜧ,
							sprᯟ.ᜀ.ᜄ,
							this.ᜁ(text2),
							sprᯟ.ᜀ.ᜅ
						});
						num = 3;
						continue;
					}
					case 1:
						goto IL_5AD;
					case 2:
						if (text != b)
						{
							num = 8;
							continue;
						}
						goto IL_50D;
					case 3:
						goto IL_4E4;
					case 4:
						if (text2 != b)
						{
							num = 0;
							continue;
						}
						goto IL_4E4;
					case 5:
					{
						object obj2 = A_1;
						A_1 = string.Concat(new object[]
						{
							obj2,
							sprᯟ.ᜂ.\u171B,
							sprᯟ.ᜀ.ᜄ,
							this.ᜀ(text3),
							sprᯟ.ᜀ.ᜅ,
							sprᯟ.ᜂ.ᜨ,
							sprᯟ.ᜀ.ᜄ,
							this.ᜁ(text3),
							sprᯟ.ᜀ.ᜅ
						});
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63B;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					}
					case 6:
						goto IL_63B;
					case 7:
					{
						object obj3 = A_1;
						A_1 = string.Concat(new object[]
						{
							obj3,
							sprᯟ.ᜂ.\u171C,
							sprᯟ.ᜀ.ᜄ,
							this.ᜀ(text4),
							sprᯟ.ᜀ.ᜅ,
							sprᯟ.ᜂ.ᜩ,
							sprᯟ.ᜀ.ᜄ,
							this.ᜁ(text4),
							sprᯟ.ᜀ.ᜅ
						});
						num = 1;
						continue;
					}
					case 8:
					{
						object obj4 = A_1;
						A_1 = string.Concat(new object[]
						{
							obj4,
							sprᯟ.ᜂ.\u1719,
							sprᯟ.ᜀ.ᜄ,
							this.ᜀ(text),
							sprᯟ.ᜀ.ᜅ,
							sprᯟ.ᜂ.ᜦ,
							sprᯟ.ᜀ.ᜄ,
							this.ᜁ(text),
							sprᯟ.ᜀ.ᜅ
						});
						num = 10;
						continue;
					}
					case 9:
						goto IL_62F;
					case 10:
						goto IL_50D;
					case 11:
						if (text3 != b)
						{
							num = 5;
							continue;
						}
						goto IL_62F;
					}
					break;
					IL_4E4:
					num = 11;
					continue;
					IL_50D:
					num = 4;
					continue;
					IL_62F:
					num = 6;
					continue;
					IL_63B:
					if (!(text4 != b))
					{
						goto IL_6D6;
					}
					num = 7;
				}
			}
			IL_5AD:
			IL_6D6:
			string text21 = A_1;
			A_1 = string.Concat(new string[]
			{
				text21,
				sprᯟ.ᜂ.\u171D,
				sprᯟ.ᜀ.ᜄ,
				text8,
				sprᯟ.ᜀ.ᜅ,
				sprᯟ.ᜂ.\u171E,
				sprᯟ.ᜀ.ᜄ,
				text12,
				sprᯟ.ᜀ.ᜅ,
				sprᯟ.ᜂ.\u171F,
				sprᯟ.ᜀ.ᜄ,
				text16,
				sprᯟ.ᜀ.ᜅ,
				sprᯟ.ᜂ.ᜠ,
				sprᯟ.ᜀ.ᜄ,
				text20,
				sprᯟ.ᜀ.ᜅ
			});
			return A_1;
		}
		}
	}

	// Token: 0x06004B64 RID: 19300 RVA: 0x002E0754 File Offset: 0x002DF754
	private float ᜁ(string A_0)
	{
		int a_ = 19;
		float result;
		for (;;)
		{
			result = 0f;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			default:
			{
				if (false)
				{
				}
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						num = 6;
						continue;
					case 2:
						num = 12;
						continue;
					case 3:
						return result;
					case 4:
						return result;
					case 5:
						spr\u22D2.\u1777 = new Dictionary<string, int>(13)
						{
							{
								RecordTableEnumerator.b("ň⩊⑌㵎", a_),
								0
							},
							{
								RecordTableEnumerator.b("ൈ⑊㡌ⵎ㵐㙒", a_),
								1
							},
							{
								RecordTableEnumerator.b("ᵈ⍊⑌ⅎ", a_),
								2
							},
							{
								RecordTableEnumerator.b("ൈ⩊㹌❎㑐㝒", a_),
								3
							},
							{
								RecordTableEnumerator.b("ൈ⑊㥌㭎㑐㝒", a_),
								4
							},
							{
								RecordTableEnumerator.b("ൈ⩊㹌❎๐㝒㩔⍖", a_),
								5
							},
							{
								RecordTableEnumerator.b("ᩈ❊ⱌⅎ═㙒ㅔࡖ㵘㩚⹜㝞㹠ݢ੤፦", a_),
								6
							},
							{
								RecordTableEnumerator.b("ൈ⩊㹌❎๐㝒㩔⍖٘㽚㉜⭞", a_),
								7
							},
							{
								RecordTableEnumerator.b("ш⹊⥌♎⑐㹒", a_),
								8
							},
							{
								RecordTableEnumerator.b("ш⹊⥌♎⑐㹒੔㍖㡘⡚㕜㩞ՠ", a_),
								9
							},
							{
								RecordTableEnumerator.b("ш⹊⥌♎⑐㹒੔㍖㡘⡚㕜^ՠౢᅤ", a_),
								10
							},
							{
								RecordTableEnumerator.b("ш⹊⥌♎⑐㹒੔㍖㡘⡚㕜^ՠౢᅤ㡦൨Ѫᥬ", a_),
								11
							},
							{
								RecordTableEnumerator.b("ᵈ⍊⑌ⱎ㩐", a_),
								12
							}
						};
						num = 8;
						continue;
					case 6:
						if (spr\u22D2.\u1777 == null)
						{
							num = 5;
							continue;
						}
						goto IL_2B8;
					case 7:
						if (A_0 != null)
						{
							num = 1;
							continue;
						}
						return result;
					case 8:
						goto IL_2B8;
					case 9:
					{
						int num2;
						if (spr\u22D2.\u1777.TryGetValue(A_0, out num2))
						{
							num = 2;
							continue;
						}
						return result;
					}
					case 10:
						return result;
					case 11:
						num = 0;
						continue;
					case 12:
					{
						int num2;
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							result = 0.5f;
							num = 14;
							continue;
						case 1:
							result = 3f;
							num = 10;
							continue;
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
							result = 1f;
							num = 13;
							continue;
						case 8:
						case 9:
						case 10:
						case 11:
							result = 2f;
							num = 4;
							continue;
						case 12:
							result = 3f;
							num = 3;
							continue;
						default:
							num = 11;
							continue;
						}
						break;
					}
					case 13:
						return result;
					case 14:
						return result;
					}
					break;
					IL_2B8:
					num = 9;
				}
				break;
			}
			}
		}
		return result;
	}

	// Token: 0x06004B65 RID: 19301 RVA: 0x002E0A50 File Offset: 0x002DFA50
	private string ᜀ(string A_0)
	{
		int a_ = 6;
		string result;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_239:
			result = sprᯟ.ᜂ.ᜫ;
			num = 9;
			break;
		default:
			if (false)
			{
			}
			goto IL_65;
		}
		for (;;)
		{
			IL_27:
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 1:
				goto IL_2AE;
			case 2:
				if (A_0 != null)
				{
					num = 6;
					continue;
				}
				return result;
			case 3:
				return result;
			case 4:
				return result;
			case 5:
				if (spr\u22D2.\u1778 == null)
				{
					num = 11;
					continue;
				}
				goto IL_2AE;
			case 6:
				num = 5;
				continue;
			case 7:
				return result;
			case 8:
			{
				int num2;
				switch (num2)
				{
				case 0:
				case 1:
				case 2:
				case 3:
					result = sprᯟ.ᜂ.ᜢ;
					num = 3;
					continue;
				case 4:
					if (true)
					{
					}
					result = sprᯟ.ᜂ.ᜃ;
					num = 4;
					continue;
				case 5:
				case 6:
					result = sprᯟ.ᜂ.ᜪ;
					num = 7;
					continue;
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
					goto IL_239;
				default:
					num = 10;
					continue;
				}
				break;
			}
			case 9:
				return result;
			case 10:
				num = 12;
				continue;
			case 11:
				spr\u22D2.\u1778 = new Dictionary<string, int>(13)
				{
					{
						RecordTableEnumerator.b("栻嘽⤿ⱁ", a_),
						0
					},
					{
						RecordTableEnumerator.b("焻嬽␿⭁ㅃ⭅", a_),
						1
					},
					{
						RecordTableEnumerator.b("栻嘽⤿⅁⽃", a_),
						2
					},
					{
						RecordTableEnumerator.b("琻弽⤿ぁ", a_),
						3
					},
					{
						RecordTableEnumerator.b("砻儽㔿⁁⡃⍅", a_),
						4
					},
					{
						RecordTableEnumerator.b("砻弽㌿⩁⅃≅", a_),
						5
					},
					{
						RecordTableEnumerator.b("焻嬽␿⭁ㅃ⭅ᝇ⹉ⵋ㵍㡏㝑こ", a_),
						6
					},
					{
						RecordTableEnumerator.b("砻儽㐿㙁⅃≅", a_),
						7
					},
					{
						RecordTableEnumerator.b("砻弽㌿⩁ᭃ≅❇㹉", a_),
						8
					},
					{
						RecordTableEnumerator.b("焻嬽␿⭁ㅃ⭅ᝇ⹉ⵋ㵍㡏൑こ㥕ⱗ", a_),
						9
					},
					{
						RecordTableEnumerator.b("漻刽ℿⱁぃ⍅ⱇᕉ⡋⽍⍏㩑୓㉕㝗⹙", a_),
						10
					},
					{
						RecordTableEnumerator.b("砻弽㌿⩁ᭃ≅❇㹉ፋ⩍㽏♑", a_),
						11
					},
					{
						RecordTableEnumerator.b("焻嬽␿⭁ㅃ⭅ᝇ⹉ⵋ㵍㡏൑こ㥕ⱗՙ㡛ㅝᑟ", a_),
						12
					}
				};
				num = 1;
				continue;
			case 12:
				return result;
			case 13:
			{
				int num2;
				if (spr\u22D2.\u1778.TryGetValue(A_0, out num2))
				{
					num = 0;
					continue;
				}
				return result;
			}
			}
			goto IL_65;
			IL_2AE:
			num = 13;
		}
		return result;
		IL_65:
		result = sprᯟ.ᜂ.ᜢ;
		num = 2;
		goto IL_27;
	}

	// Token: 0x06004B66 RID: 19302 RVA: 0x002E0D38 File Offset: 0x002DFD38
	private Dictionary<string, string> ᜀ(IXLSRange A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			Dictionary<string, string> dictionary;
			for (;;)
			{
				dictionary = new Dictionary<string, string>();
				string b = RecordTableEnumerator.b("焾⹀ⵂ⁄", a_);
				string text = A_0.Borders[BordersLineType.EdgeTop].LineStyle.ToString();
				string text2 = A_0.Borders[BordersLineType.EdgeBottom].LineStyle.ToString();
				string text3 = A_0.Borders[BordersLineType.EdgeLeft].LineStyle.ToString();
				string text4 = A_0.Borders[BordersLineType.EdgeRight].LineStyle.ToString();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_19A;
					case 1:
						if (text != b)
						{
							num = 6;
							continue;
						}
						goto IL_19A;
					case 2:
						return dictionary;
					case 3:
						dictionary.Add(sprᯟ.ᜂ.\u171C, this.ᜀ(text4));
						dictionary.Add(sprᯟ.ᜂ.ᜩ, this.ᜁ(text4).ToString());
						num = 2;
						continue;
					case 4:
						dictionary.Add(sprᯟ.ᜂ.\u171B, this.ᜀ(text3));
						dictionary.Add(sprᯟ.ᜂ.ᜨ, this.ᜁ(text3).ToString());
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24A;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 5:
						if (text3 != b)
						{
							num = 4;
							continue;
						}
						goto IL_23E;
					case 6:
						dictionary.Add(sprᯟ.ᜂ.\u1719, this.ᜀ(text));
						dictionary.Add(sprᯟ.ᜂ.ᜦ, this.ᜁ(text).ToString());
						num = 0;
						continue;
					case 7:
						if (text2 != b)
						{
							num = 8;
							continue;
						}
						goto IL_170;
					case 8:
						dictionary.Add(sprᯟ.ᜂ.\u171A, this.ᜀ(text2));
						dictionary.Add(sprᯟ.ᜂ.ᜧ, this.ᜁ(text2).ToString());
						num = 10;
						continue;
					case 9:
						goto IL_23E;
					case 10:
						goto IL_170;
					case 11:
						goto IL_24A;
					}
					break;
					IL_170:
					num = 5;
					continue;
					IL_19A:
					num = 7;
					continue;
					IL_23E:
					num = 11;
					continue;
					IL_24A:
					if (!(text4 != b))
					{
						return dictionary;
					}
					num = 3;
				}
			}
			return dictionary;
		}
		}
	}

	// Token: 0x06004B67 RID: 19303 RVA: 0x002E0FEC File Offset: 0x002DFFEC
	private void ᜀ(XlsWorksheet A_0, StringBuilder A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				IPictures pictures = A_0.Pictures;
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						if (num < pictures.Count)
						{
							IPictureShape pictureShape = pictures[num];
							Image picture = pictureShape.Picture;
							int left = pictureShape.Left;
							int top = pictureShape.Top;
							int height = pictureShape.Height;
							int width = pictureShape.Width;
							string str = RecordTableEnumerator.b("橃⽅ぇ", a_) + num;
							string text = str + sprᯟ.ᜀ.ᜀ;
							text = string.Concat(new object[]
							{
								text,
								sprᯟ.ᜂ.\u1713,
								sprᯟ.ᜀ.ᜄ,
								left,
								RecordTableEnumerator.b("㑃㹅", a_),
								sprᯟ.ᜀ.ᜅ
							});
							text = string.Concat(new object[]
							{
								text,
								sprᯟ.ᜂ.\u1714,
								sprᯟ.ᜀ.ᜄ,
								top,
								RecordTableEnumerator.b("㑃㹅", a_),
								sprᯟ.ᜀ.ᜅ
							});
							text = string.Concat(new string[]
							{
								text,
								sprᯟ.ᜂ.\u1738,
								sprᯟ.ᜀ.ᜄ,
								height.ToString(CultureInfo.InvariantCulture),
								RecordTableEnumerator.b("㑃㹅", a_),
								sprᯟ.ᜀ.ᜅ
							});
							text = string.Concat(new object[]
							{
								text,
								sprᯟ.ᜂ.\u1739,
								sprᯟ.ᜀ.ᜄ,
								width,
								RecordTableEnumerator.b("㑃㹅", a_),
								sprᯟ.ᜀ.ᜅ
							});
							text = string.Concat(new string[]
							{
								text,
								sprᯟ.ᜂ.\u1715,
								sprᯟ.ᜀ.ᜄ,
								sprᯟ.ᜂ.\u1716,
								sprᯟ.ᜀ.ᜁ
							});
							A_1.Append(text);
							A_1.AppendLine();
							num++;
							num2 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_290;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 1:
						goto IL_51;
					case 2:
						return;
					case 3:
						goto IL_290;
					}
					break;
					IL_51:
					num2 = 0;
					continue;
					IL_290:
					goto IL_51;
				}
			}
			return;
		}
	}

	// Token: 0x06004B68 RID: 19304 RVA: 0x002E1290 File Offset: 0x002E0290
	private List<string> ᜀ(XlsWorksheet A_0)
	{
		List<string> list;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u1FBC spr_u1FBC = A_0.MergeCells;
					IXLSRange allocatedRange = A_0.AllocatedRange;
					int row = allocatedRange.Row;
					int column = allocatedRange.Column;
					int lastRow = allocatedRange.LastRow;
					int lastColumn = allocatedRange.LastColumn;
					spr_u1FBC.ᜀ(A_0[row, column, lastRow, lastColumn], this.ᜆ);
					list = new List<string>();
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						if (true)
						{
						}
						int num3;
						int num5;
						switch (num2)
						{
						case 0:
							goto IL_1C8;
						case 1:
							goto IL_1A2;
						case 2:
						{
							int num4;
							if (num3 != num4)
							{
								num2 = 3;
								continue;
							}
							goto IL_D9;
						}
						case 3:
							goto IL_1F7;
						case 4:
							return list;
						case 5:
						{
							int num6;
							if (num5 == num6)
							{
								num2 = 12;
								continue;
							}
							goto IL_1F7;
						}
						case 6:
							goto IL_1A2;
						case 7:
						{
							int num7;
							if (num3 > num7)
							{
								num2 = 11;
								continue;
							}
							num2 = 5;
							continue;
						}
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								goto IL_273;
							}
							break;
						case 9:
							goto IL_1C8;
						case 10:
							num++;
							num2 = 9;
							continue;
						case 11:
							num5++;
							num2 = 1;
							continue;
						case 12:
							num2 = 2;
							continue;
						case 13:
						{
							int num8;
							if (num5 > num8)
							{
								num2 = 10;
								continue;
							}
							int num4;
							num3 = num4;
							num2 = 8;
							continue;
						}
						case 14:
							goto IL_D9;
						case 15:
						{
							if (num >= this.ᜆ.Count)
							{
								num2 = 4;
								continue;
							}
							int num6 = this.ᜆ[num].ᜂ() + 1;
							int num8 = this.ᜆ[num].ᜇ() + 1;
							int num4 = this.ᜆ[num].ᜅ() + 1;
							int num7 = this.ᜆ[num].ᜃ() + 1;
							num5 = num6;
							num2 = 6;
							continue;
						}
						case 16:
							goto IL_273;
						}
						break;
						IL_D9:
						num3++;
						num2 = 16;
						continue;
						IL_1A2:
						num2 = 13;
						continue;
						IL_1C8:
						num2 = 15;
						continue;
						IL_1F7:
						string item = string.Concat(new object[]
						{
							sprᯟ.ᜀ.ᜆ,
							num5,
							sprᯟ.ᜀ.ᜂ,
							num3,
							sprᯟ.ᜀ.ᜇ
						});
						list.Add(item);
						num2 = 14;
						continue;
						IL_273:
						num2 = 7;
					}
				}
				break;
			}
		}
		return list;
	}

	// Token: 0x06004B69 RID: 19305 RVA: 0x002E1568 File Offset: 0x002E0568
	private void ᜀ(XlsWorksheet A_0, string A_1, HTMLOptions A_2, XmlTextWriter A_3)
	{
		int a_ = 8;
		switch (0)
		{
		default:
			for (;;)
			{
				new StringBuilder();
				IXLSRange allocatedRange = A_0.AllocatedRange;
				int row = allocatedRange.Row;
				int column = allocatedRange.Column;
				int lastRow = allocatedRange.LastRow;
				int lastColumn = allocatedRange.LastColumn;
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				IPictures pictures = A_0.Pictures;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 63;
				for (;;)
				{
					int num11;
					Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator;
					string value;
					string text4;
					List<int> list3;
					int num18;
					switch (num5)
					{
					case 0:
						A_3.WriteAttributeString(sprᯟ.ᜂ.ᜣ, num3.ToString());
						num5 = 67;
						continue;
					case 1:
						A_3.WriteRaw(RecordTableEnumerator.b("ᠽ⸿⁁㝃㙅獇", a_));
						num5 = 71;
						continue;
					case 2:
					{
						int num6;
						int num7;
						num2 = num6 - num7 + 1;
						int num8;
						int num9;
						num3 = num8 - num9 + 1;
						num5 = 54;
						continue;
					}
					case 3:
					{
						int num10 = 1;
						num5 = 75;
						continue;
					}
					case 4:
					{
						int num7;
						if (num11 == num7 + 1)
						{
							num5 = 2;
							continue;
						}
						goto IL_5E7;
					}
					case 5:
					{
						try
						{
							num5 = 4;
							for (;;)
							{
								switch (num5)
								{
								case 1:
								{
									LinkedList<string> linkedList;
									string text;
									if (linkedList.Contains(text))
									{
										goto IL_6A8;
									}
									break;
								}
								case 2:
									if (!enumerator.MoveNext())
									{
										num5 = 3;
										continue;
									}
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_6A8;
									default:
									{
										if (false)
										{
										}
										string key = enumerator.Current;
										LinkedList<string> linkedList = this.ᜄ[key];
										num5 = 1;
										continue;
									}
									}
									break;
								case 3:
									num5 = 6;
									continue;
								case 5:
								{
									LinkedList<string> linkedList;
									value = linkedList.First.Value.Substring(1);
									num5 = 0;
									continue;
								}
								case 6:
									goto IL_6E5;
								}
								goto IL_63C;
								IL_6A8:
								num5 = 5;
								continue;
								IL_6B6:
								num5 = 2;
								continue;
								IL_63C:
								goto IL_6B6;
							}
							IL_6E5:
							goto IL_8DD;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_6F8;
						IL_8DD:
						A_3.WriteAttributeString(sprᯟ.ᜂ.ᜈ, value);
						string text2 = null;
						num5 = 74;
						continue;
					}
					case 6:
						goto IL_B52;
					case 7:
					{
						int num10;
						if (num10 > lastRow)
						{
							num5 = 42;
							continue;
						}
						int rowHeightPixels = A_0.GetRowHeightPixels(num10);
						A_3.WriteStartElement(sprᯟ.ᜃ.ᜊ);
						num4 += rowHeightPixels;
						num11 = 1;
						num5 = 6;
						continue;
					}
					case 8:
					{
						string text2;
						if (text2.Equals(""))
						{
							num5 = 1;
							continue;
						}
						goto IL_201;
					}
					case 9:
						goto IL_B80;
					case 10:
						A_3.WriteStartElement(sprᯟ.ᜃ.ᜉ);
						num5 = 19;
						continue;
					case 11:
					{
						string text2;
						if (!text2.Equals(""))
						{
							num5 = 29;
							continue;
						}
						goto IL_2A2;
					}
					case 12:
					{
						if (num11 > lastColumn)
						{
							num5 = 35;
							continue;
						}
						int num10;
						string text3 = num10.ToString();
						string text = text3 + this.ᜀ(num11);
						IXLSRange ixlsrange = A_0.AllocatedRange[text];
						ixlsrange.ColumnWidth + RecordTableEnumerator.b("ሽ", a_);
						num5 = 73;
						continue;
					}
					case 13:
						goto IL_858;
					case 14:
						goto IL_2BA;
					case 15:
						if (A_0.HasMergedCells)
						{
							num5 = 48;
							continue;
						}
						goto IL_A34;
					case 16:
						A_3.WriteStartElement(sprᯟ.ᜃ.ᜉ);
						text4 = RecordTableEnumerator.b("堽ℿ⹁㝃⍅", a_);
						num5 = 66;
						continue;
					case 17:
					{
						int num10;
						string item = string.Concat(new object[]
						{
							sprᯟ.ᜀ.ᜆ,
							num10,
							sprᯟ.ᜀ.ᜂ,
							num11,
							sprᯟ.ᜀ.ᜇ
						});
						num5 = 23;
						continue;
					}
					case 18:
						goto IL_CFF;
					case 19:
						goto IL_742;
					case 20:
						num5 = 4;
						continue;
					case 21:
						goto IL_B52;
					case 22:
						goto IL_6F8;
					case 23:
					{
						string item;
						if (!list.Contains(item))
						{
							num5 = 16;
							continue;
						}
						goto IL_9B8;
					}
					case 24:
						goto IL_9F4;
					case 25:
					{
						string text2;
						IXLSRange ixlsrange;
						int num12;
						this.ᜀ(text2, ixlsrange, num12, list3, num11, A_0, A_2);
						num5 = 55;
						continue;
					}
					case 26:
						if (num3 > 1)
						{
							num5 = 0;
							continue;
						}
						goto IL_88C;
					case 27:
						this.ᜀ(A_0, A_3, A_1, A_2);
						num5 = 34;
						continue;
					case 28:
						goto IL_B80;
					case 29:
					{
						string text3;
						IXLSRange ixlsrange;
						int num12 = this.ᜀ(ixlsrange, A_3, list3, num11, text3, A_0, A_2);
						int num13 = num11;
						int num14 = 0;
						num5 = 61;
						continue;
					}
					case 30:
						goto IL_9F4;
					case 31:
						A_3.WriteRaw(RecordTableEnumerator.b("ᠽ⸿⁁㝃㙅獇", a_));
						num5 = 14;
						continue;
					case 32:
					{
						string text5;
						if (text5.Equals(""))
						{
							num5 = 31;
							continue;
						}
						IXLSRange ixlsrange;
						int num15;
						this.ᜀ(text5, ixlsrange, num15, list3, num11, A_0, A_2);
						num5 = 46;
						continue;
					}
					case 33:
						num5 = 11;
						continue;
					case 34:
						goto IL_990;
					case 35:
					{
						if (true)
						{
						}
						A_3.WriteEndElement();
						int num10;
						num10++;
						num5 = 18;
						continue;
					}
					case 36:
						goto IL_365;
					case 37:
					{
						int num12;
						int num14;
						if (num14 >= num12)
						{
							num5 = 25;
							continue;
						}
						int num10;
						int num13;
						string item = string.Concat(new object[]
						{
							sprᯟ.ᜀ.ᜆ,
							num10,
							sprᯟ.ᜀ.ᜂ,
							num13,
							sprᯟ.ᜀ.ᜇ
						});
						list.Add(item);
						num13++;
						num14++;
						num5 = 22;
						continue;
					}
					case 38:
						goto IL_D69;
					case 39:
						if (A_2.ImagePath != null)
						{
							num5 = 27;
							continue;
						}
						goto IL_990;
					case 40:
						if (!A_0.HasMergedCells)
						{
							num5 = 45;
							continue;
						}
						goto IL_9B8;
					case 41:
					{
						IXLSRange ixlsrange;
						string text2 = ixlsrange.NumberText;
						num5 = 65;
						continue;
					}
					case 42:
						goto IL_D1F;
					case 43:
					{
						int num9 = this.ᜆ[num].ᜂ();
						int num7 = this.ᜆ[num].ᜅ();
						int num8 = this.ᜆ[num].ᜇ();
						int num6 = this.ᜆ[num].ᜃ();
						num5 = 77;
						continue;
					}
					case 44:
						if (text4.Equals(RecordTableEnumerator.b("堽ℿ⹁㝃⍅", a_)))
						{
							num5 = 33;
							continue;
						}
						goto IL_2A2;
					case 45:
					{
						string text3;
						IXLSRange ixlsrange;
						int num15 = this.ᜀ(ixlsrange, A_3, list3, num11, text3, A_0, A_2);
						int num16 = num11;
						int num17 = 0;
						num5 = 28;
						continue;
					}
					case 46:
						goto IL_2BA;
					case 47:
						if (text4.Equals(RecordTableEnumerator.b("䨽㈿㝁⅃", a_)))
						{
							num5 = 76;
							continue;
						}
						goto IL_80E;
					case 48:
					{
						list = this.ᜀ(A_0);
						spr\u1FBC spr_u1FBC = A_0.MergeCells;
						spr_u1FBC.ᜀ(A_0[row, column, lastRow, lastColumn], this.ᜆ);
						num5 = 69;
						continue;
					}
					case 49:
						if (A_0.HasMergedCells)
						{
							num5 = 17;
							continue;
						}
						goto IL_D69;
					case 50:
					{
						string value2 = "";
						Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator2 = this.ᜄ.Keys.GetEnumerator();
						num5 = 72;
						continue;
					}
					case 51:
					{
						int num15;
						int num17;
						if (num17 >= num15)
						{
							num5 = 50;
							continue;
						}
						int num10;
						int num16;
						string item2 = string.Concat(new object[]
						{
							sprᯟ.ᜀ.ᜆ,
							num10,
							sprᯟ.ᜀ.ᜂ,
							num16,
							sprᯟ.ᜀ.ᜇ
						});
						list2.Add(item2);
						num16++;
						num17++;
						num5 = 9;
						continue;
					}
					case 52:
					{
						int num10;
						string item3 = string.Concat(new object[]
						{
							sprᯟ.ᜀ.ᜆ,
							num10,
							sprᯟ.ᜀ.ᜂ,
							num11,
							sprᯟ.ᜀ.ᜇ
						});
						num5 = 56;
						continue;
					}
					case 53:
						goto IL_9B8;
					case 54:
						if (num2 > 1)
						{
							num5 = 70;
							continue;
						}
						goto IL_9CF;
					case 55:
						goto IL_2A2;
					case 56:
					{
						string item3;
						if (!list2.Contains(item3))
						{
							num5 = 10;
							continue;
						}
						goto IL_9B8;
					}
					case 57:
						goto IL_9CF;
					case 58:
						goto IL_80E;
					case 59:
						goto IL_5E7;
					case 60:
						goto IL_365;
					case 61:
						goto IL_6F8;
					case 62:
						num5 = 39;
						continue;
					case 63:
						if (A_0.HasPictures)
						{
							num5 = 62;
							continue;
						}
						goto IL_990;
					case 64:
					{
						if (num18 > lastColumn)
						{
							num5 = 3;
							continue;
						}
						A_3.WriteStartElement(sprᯟ.ᜃ.\u170D);
						int columnWidthPixels = A_0.GetColumnWidthPixels(num18);
						list3.Add(columnWidthPixels);
						A_3.WriteAttributeString(sprᯟ.ᜂ.\u1739, A_0.GetColumnWidthPixels(num18).ToString());
						A_3.WriteEndElement();
						num18++;
						num5 = 30;
						continue;
					}
					case 65:
						goto IL_858;
					case 66:
						if (num < this.ᜆ.Count)
						{
							num5 = 43;
							continue;
						}
						goto IL_5E7;
					case 67:
						goto IL_88C;
					case 68:
					{
						IXLSRange ixlsrange;
						string text5 = ixlsrange.NumberText;
						num5 = 60;
						continue;
					}
					case 69:
						goto IL_A34;
					case 70:
						A_3.WriteAttributeString(sprᯟ.ᜂ.ᜤ, num2.ToString());
						num5 = 57;
						continue;
					case 71:
						goto IL_201;
					case 72:
					{
						string value2;
						try
						{
							num5 = 4;
							for (;;)
							{
								switch (num5)
								{
								case 0:
								{
									LinkedList<string> linkedList2;
									value2 = linkedList2.First.Value.Substring(1);
									num5 = 2;
									continue;
								}
								case 1:
									num5 = 6;
									continue;
								case 3:
								{
									string text;
									LinkedList<string> linkedList2;
									if (linkedList2.Contains(text))
									{
										num5 = 0;
										continue;
									}
									break;
								}
								case 5:
								{
									Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num5 = 1;
										continue;
									}
									string key2 = enumerator2.Current;
									LinkedList<string> linkedList2 = this.ᜄ[key2];
									num5 = 3;
									continue;
								}
								case 6:
									goto IL_5D4;
								}
								IL_56E:
								num5 = 5;
								continue;
								goto IL_56E;
							}
							IL_5D4:
							goto IL_255;
						}
						finally
						{
							Dictionary<string, LinkedList<string>>.KeyCollection.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_5E7;
						IL_255:
						A_3.WriteAttributeString(sprᯟ.ᜂ.ᜈ, value2);
						string text5 = null;
						num5 = 78;
						continue;
					}
					case 73:
						if (!A_0.HasMergedCells)
						{
							num5 = 52;
							continue;
						}
						goto IL_742;
					case 74:
					{
						if (A_2.TextMode == HTMLOptions.GetText.NumberText)
						{
							num5 = 41;
							continue;
						}
						IXLSRange ixlsrange;
						string text2 = ixlsrange.Value;
						num5 = 13;
						continue;
					}
					case 75:
						goto IL_CFF;
					case 76:
					{
						string text2;
						A_3.WriteString(text2);
						num5 = 58;
						continue;
					}
					case 77:
					{
						int num9;
						int num10;
						if (num10 == num9 + 1)
						{
							num5 = 20;
							continue;
						}
						goto IL_5E7;
					}
					case 78:
					{
						if (A_2.TextMode == HTMLOptions.GetText.NumberText)
						{
							num5 = 68;
							continue;
						}
						IXLSRange ixlsrange;
						string text5 = ixlsrange.Value;
						num5 = 36;
						continue;
					}
					}
					break;
					IL_201:
					num5 = 44;
					continue;
					IL_2A2:
					A_3.WriteEndElement();
					num5 = 38;
					continue;
					IL_2BA:
					A_3.WriteEndElement();
					num5 = 53;
					continue;
					IL_365:
					num5 = 32;
					continue;
					IL_5E7:
					value = "";
					enumerator = this.ᜄ.Keys.GetEnumerator();
					num5 = 5;
					continue;
					IL_6F8:
					num5 = 37;
					continue;
					IL_742:
					num5 = 49;
					continue;
					IL_80E:
					num5 = 8;
					continue;
					IL_858:
					num5 = 47;
					continue;
					IL_88C:
					num++;
					text4 = RecordTableEnumerator.b("䨽㈿㝁⅃", a_);
					num5 = 59;
					continue;
					IL_990:
					num5 = 15;
					continue;
					IL_9B8:
					num11++;
					num5 = 21;
					continue;
					IL_9CF:
					num5 = 26;
					continue;
					IL_9F4:
					num5 = 64;
					continue;
					IL_A34:
					A_3.WriteStartElement(sprᯟ.ᜃ.ᜈ);
					A_3.WriteAttributeString(sprᯟ.ᜂ.ᝉ, RecordTableEnumerator.b("฽", a_));
					list3 = new List<int>();
					num18 = 1;
					num5 = 24;
					continue;
					IL_B52:
					num5 = 12;
					continue;
					IL_B80:
					num5 = 51;
					continue;
					IL_CFF:
					num5 = 7;
					continue;
					IL_D69:
					num5 = 40;
				}
			}
			IL_D1F:
			A_3.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004B6A RID: 19306 RVA: 0x002E232C File Offset: 0x002E132C
	private void ᜀ(string A_0, IXLSRange A_1, int A_2, List<int> A_3, int A_4, XlsWorksheet A_5, HTMLOptions A_6)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num5;
			string text2;
			string text3;
			for (;;)
			{
				IL_AB:
				int num = 0;
				int num2 = A_4;
				int num3 = 16;
				for (;;)
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
						Font a_2;
						SizeF sizeF;
						int num4;
						string text;
						double num6;
						switch (num3)
						{
						case 0:
							goto IL_2D6;
						case 1:
							sizeF = this.ᜉ.ᜀ(A_1.NumberText, a_2);
							num3 = 0;
							continue;
						case 2:
							num3 = 12;
							continue;
						case 3:
							if (num4 > num)
							{
								num3 = 2;
								continue;
							}
							goto IL_343;
						case 4:
							text = A_1.NumberText.Substring(0, A_1.NumberText.Length - num5);
							num3 = 10;
							continue;
						case 5:
							goto IL_2A4;
						case 6:
						{
							double num7;
							if (num6 <= num7)
							{
								num3 = 15;
								continue;
							}
							num5++;
							num3 = 13;
							continue;
						}
						case 7:
							goto IL_37D;
						case 8:
							goto IL_244;
						case 9:
							goto IL_129;
						case 10:
							goto IL_129;
						case 11:
							if (A_6.TextMode == HTMLOptions.GetText.NumberText)
							{
								if (true)
								{
								}
								num3 = 1;
								continue;
							}
							sizeF = this.ᜉ.ᜀ(A_1.Value, a_2);
							num3 = 20;
							continue;
						case 12:
						{
							if (A_4 == A_5.AllocatedRange.LastColumn)
							{
								num3 = 5;
								continue;
							}
							A_0.ToCharArray();
							text = "";
							double num7 = double.Parse(num.ToString());
							text2 = null;
							num3 = 26;
							continue;
						}
						case 13:
							goto IL_244;
						case 14:
							if (num2 > A_4 + A_2)
							{
								num3 = 25;
								continue;
							}
							num += int.Parse(A_3[num2 - 1].ToString());
							num2++;
							num3 = 23;
							continue;
						case 15:
							text3 = text;
							num3 = 19;
							continue;
						case 16:
							goto IL_31C;
						case 17:
							goto IL_37D;
						case 18:
							text2 = A_1.NumberText;
							num3 = 17;
							continue;
						case 19:
							goto IL_23F;
						case 20:
							goto IL_2D6;
						case 21:
							goto IL_26A;
						case 22:
							if (num5 >= text2.Length)
							{
								num3 = 21;
								continue;
							}
							num3 = 24;
							continue;
						case 23:
							goto IL_31C;
						case 24:
							if (A_6.TextMode == HTMLOptions.GetText.NumberText)
							{
								num3 = 4;
								continue;
							}
							text = A_1.Value.Substring(0, A_1.Value.Length - num5);
							num3 = 9;
							continue;
						case 25:
						{
							IFont font = A_1.Style.Font;
							a_2 = font.GenerateNativeFont();
							goto IL_186;
						}
						case 26:
							if (A_6.TextMode == HTMLOptions.GetText.NumberText)
							{
								num3 = 18;
								continue;
							}
							text2 = A_1.Value;
							num3 = 7;
							continue;
						}
						goto IL_AB;
						IL_129:
						num6 = double.Parse(this.ᜉ.ᜀ(text, a_2).Width.ToString());
						num3 = 6;
						continue;
						IL_244:
						num3 = 22;
						continue;
						IL_2D6:
						string s = sizeF.Width.ToString();
						num6 = double.Parse(s);
						num4 = Convert.ToInt32(num6);
						num3 = 3;
						continue;
						IL_31C:
						num3 = 14;
						continue;
						IL_37D:
						text3 = "";
						num5 = 0;
						num3 = 8;
						continue;
					}
					}
					IL_186:
					num3 = 11;
				}
			}
			IL_23F:
			IL_26A:
			goto IL_3E0;
			IL_2A4:
			IL_343:
			this.ᜂ().WriteString(A_0);
			return;
			IL_3E0:
			int startIndex = text2.Length - num5;
			string text4 = text2.Substring(startIndex, num5);
			this.ᜂ().WriteString(text3);
			this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜎ);
			this.ᜂ().WriteAttributeString(sprᯟ.ᜃ.ᜃ, RecordTableEnumerator.b("强吼䰾ㅀ⽂⑄㹆獈╊≌ⅎ㑐", a_));
			this.ᜂ().WriteString(text4);
			this.ᜂ().WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004B6B RID: 19307 RVA: 0x002E2788 File Offset: 0x002E1788
	private int ᜀ(IXLSRange A_0, XmlTextWriter A_1, List<int> A_2, int A_3, string A_4, XlsWorksheet A_5, HTMLOptions A_6)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				IFont font = A_0.Style.Font;
				Font a_ = font.GenerateNativeFont();
				num = 0;
				string a = null;
				int num2 = 23;
				for (;;)
				{
					int num3;
					int num4;
					SizeF sizeF;
					int num5;
					int num6;
					switch (num2)
					{
					case 0:
						goto IL_280;
					case 1:
						num3 += int.Parse(A_2[num4].ToString());
						num++;
						num2 = 27;
						continue;
					case 2:
						goto IL_2EF;
					case 3:
						if (a != "")
						{
							num2 = 15;
							continue;
						}
						goto IL_1AA;
					case 4:
						sizeF = this.ᜉ.ᜀ(A_0.NumberText, a_);
						num2 = 0;
						continue;
					case 5:
						A_1.WriteAttributeString(sprᯟ.ᜂ.ᜤ, num.ToString());
						num2 = 18;
						continue;
					case 6:
						if (num5 >= num3)
						{
							num2 = 7;
							continue;
						}
						goto IL_1AA;
					case 7:
					{
						string str = this.ᜀ(num6);
						num6++;
						string name = A_4 + str;
						A_0 = A_5.AllocatedRange[name];
						string a2 = null;
						num2 = 14;
						continue;
					}
					case 8:
						goto IL_135;
					case 9:
						if (num4 >= A_2.Count)
						{
							num2 = 22;
							continue;
						}
						num2 = 6;
						continue;
					case 10:
						num2 = 16;
						continue;
					case 11:
						if (true)
						{
						}
						goto IL_175;
					case 12:
						goto IL_175;
					case 13:
						goto IL_135;
					case 14:
					{
						if (A_6.TextMode == HTMLOptions.GetText.NumberText)
						{
							num2 = 19;
							continue;
						}
						string a2 = A_0.Value;
						num2 = 11;
						continue;
					}
					case 15:
						num2 = 17;
						continue;
					case 16:
					{
						string a2;
						if (a2 == "")
						{
							num2 = 1;
							continue;
						}
						goto IL_246;
					}
					case 17:
						if (A_6.TextMode == HTMLOptions.GetText.NumberText)
						{
							num2 = 4;
							continue;
						}
						sizeF = this.ᜉ.ᜀ(A_0.Value, a_);
						num2 = 24;
						continue;
					case 18:
						return num;
					case 19:
					{
						string a2 = A_0.NumberText;
						num2 = 12;
						continue;
					}
					case 20:
						goto IL_2EF;
					case 21:
					{
						string a2;
						if (!(a2 != ""))
						{
							num2 = 10;
							continue;
						}
						goto IL_1AA;
					}
					case 22:
						goto IL_1AA;
					case 23:
						goto IL_AB;
					case 24:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AB;
						default:
							if (false)
							{
							}
							goto IL_280;
						}
						break;
					case 25:
						if (num > 0)
						{
							num2 = 5;
							continue;
						}
						return num;
					case 26:
						a = A_0.NumberText;
						num2 = 20;
						continue;
					case 27:
						goto IL_246;
					}
					break;
					IL_AB:
					if (A_6.TextMode == HTMLOptions.GetText.NumberText)
					{
						num2 = 26;
						continue;
					}
					a = A_0.Value;
					num2 = 2;
					continue;
					IL_135:
					num2 = 9;
					continue;
					IL_175:
					num2 = 21;
					continue;
					IL_1AA:
					num2 = 25;
					continue;
					IL_246:
					num4++;
					num2 = 13;
					continue;
					IL_280:
					num5 = Convert.ToInt32(double.Parse(sizeF.Width.ToString()));
					int num7 = A_3 - 1;
					num6 = A_3 + 1;
					num3 = int.Parse(A_2[num7].ToString());
					num = 0;
					num4 = num7 + 1;
					num2 = 8;
					continue;
					IL_2EF:
					num2 = 3;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06004B6C RID: 19308 RVA: 0x002E2B6C File Offset: 0x002E1B6C
	private void ᜀ(XlsWorksheetBase A_0, XmlTextWriter A_1, string A_2, HTMLOptions A_3)
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					IPictures pictures = A_0.Pictures;
					string name = new DirectoryInfo(A_2).Name;
					int num = 0;
					if (true)
					{
					}
					int num2 = 8;
					for (;;)
					{
						Image picture;
						string filename;
						string text2;
						switch (num2)
						{
						case 0:
						{
							if (num >= pictures.Count)
							{
								num2 = 7;
								continue;
							}
							IPictureShape pictureShape = pictures[num];
							picture = pictureShape.Picture;
							ImageFormat rawFormat = picture.RawFormat;
							this.ᜀ(picture, rawFormat);
							int left = pictureShape.Left;
							int top = pictureShape.Top;
							int height = pictureShape.Height;
							int width = pictureShape.Width;
							string str = this.ᜀ(rawFormat);
							string name2 = A_0.Name;
							string path = string.Format(RecordTableEnumerator.b("㱆祈㙊捌", a_) + str, name2 + num);
							filename = Path.Combine(A_2, path);
							string value = string.Format(RecordTableEnumerator.b("㱆祈㙊捌", a_) + str, name2 + num);
							string text = RecordTableEnumerator.b("楆⁈㍊", a_) + num;
							A_1.WriteStartElement(sprᯟ.ᜃ.ᜌ);
							A_1.WriteAttributeString(sprᯟ.ᜂ.ᜈ, text.Substring(1));
							num2 = 9;
							continue;
						}
						case 1:
						{
							string str;
							string name2;
							string value = Path.Combine(A_3.ImagePath, string.Format(RecordTableEnumerator.b("㱆祈㙊捌", a_) + str, name2 + num));
							A_1.WriteAttributeString(sprᯟ.ᜂ.\u1717, value);
							num2 = 6;
							continue;
						}
						case 2:
						{
							IPictureShape pictureShape;
							text2 = pictureShape.AlternativeText;
							goto IL_259;
						}
						case 3:
							num2 = 2;
							continue;
						case 4:
							goto IL_28B;
						case 5:
						{
							IPictureShape pictureShape;
							if (!string.IsNullOrEmpty(pictureShape.AlternativeText))
							{
								num2 = 3;
								continue;
							}
							num2 = 10;
							continue;
						}
						case 6:
							goto IL_28B;
						case 7:
							goto IL_12F;
						case 8:
							goto IL_10B;
						case 9:
						{
							if (this.ᜈ == sprᯟ.ConversionMode.Worksheet)
							{
								num2 = 1;
								continue;
							}
							string value;
							A_1.WriteAttributeString(sprᯟ.ᜂ.\u1717, value);
							num2 = 4;
							continue;
						}
						case 10:
							text2 = RecordTableEnumerator.b("ๆ⑈⩊⩌⩎", a_);
							goto IL_259;
						case 11:
							goto IL_10B;
						}
						break;
						IL_10B:
						num2 = 0;
						continue;
						IL_259:
						string value2 = text2;
						A_1.WriteAttributeString(sprᯟ.ᜂ.\u1718, value2);
						A_1.WriteEndElement();
						picture.Save(filename);
						num++;
						num2 = 11;
						continue;
						IL_28B:
						num2 = 5;
					}
				}
				IL_12F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_145;
				}
			}
			IL_145:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06004B6D RID: 19309 RVA: 0x002E2E4C File Offset: 0x002E1E4C
	private void ᜀ(Image A_0, ImageFormat A_1)
	{
		MemoryStream memoryStream = new MemoryStream();
		try
		{
			A_0.Save(memoryStream, A_1);
			byte[] inArray = memoryStream.ToArray();
			Convert.ToBase64String(inArray);
		}
		finally
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)memoryStream).Dispose();
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 0;
				}
				break;
			}
			}
			IL_7B:;
		}
	}

	// Token: 0x06004B6E RID: 19310 RVA: 0x002E2EF4 File Offset: 0x002E1EF4
	private string ᜀ(ImageFormat A_0)
	{
		int a_ = 0;
		int num = 4;
		for (;;)
		{
			string result;
			switch (num)
			{
			case 0:
				result = RecordTableEnumerator.b("吵唷䨹", a_);
				num = 9;
				continue;
			case 1:
				return result;
			case 2:
				result = RecordTableEnumerator.b("匵唷尹", a_);
				num = 1;
				continue;
			case 3:
				if (A_0.Equals(ImageFormat.Emf))
				{
					num = 2;
					continue;
				}
				num = 17;
				continue;
			case 5:
				result = RecordTableEnumerator.b("尵䠷弹嬻", a_);
				num = 11;
				continue;
			case 6:
				return result;
			case 7:
				if (A_0.Equals(ImageFormat.Jpeg))
				{
					num = 5;
					continue;
				}
				num = 10;
				continue;
			case 8:
				result = RecordTableEnumerator.b("儵儷尹", a_);
				num = 15;
				continue;
			case 9:
				return result;
			case 10:
				if (A_0.Equals(ImageFormat.Png))
				{
					num = 16;
					continue;
				}
				num = 3;
				continue;
			case 11:
				return result;
			case 12:
				if (A_0.Equals(ImageFormat.Bmp))
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
			case 13:
				return result;
			case 14:
				goto IL_93;
			case 15:
				return result;
			case 16:
				result = RecordTableEnumerator.b("䘵嘷崹", a_);
				num = 6;
				continue;
			case 17:
				if (A_0.Equals(ImageFormat.Gif))
				{
					num = 8;
					continue;
				}
				result = RecordTableEnumerator.b("䘵嘷崹", a_);
				num = 13;
				continue;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 14;
				}
				else
				{
					num = 12;
				}
				break;
			}
		}
		IL_93:
		throw new ArgumentNullException(RecordTableEnumerator.b("倵圷䠹儻弽㐿", a_));
	}

	// Token: 0x06004B6F RID: 19311 RVA: 0x002E3140 File Offset: 0x002E2140
	private string ᜀ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_81:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		string text;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				return text;
			case 1:
				if (true)
				{
				}
				if (A_0 < 0)
				{
					goto IL_81;
				}
				goto IL_4F;
			case 2:
				goto IL_4F;
			}
			goto IL_3A;
			IL_4F:
			int num2 = A_0 % 26;
			A_0 = A_0 / 26 - 1;
			text = (char)(65 + num2) + text;
			num = 1;
		}
		return text;
		IL_3A:
		A_0--;
		text = string.Empty;
		num = 2;
		goto IL_28;
	}

	// Token: 0x06004B70 RID: 19312 RVA: 0x002E31DC File Offset: 0x002E21DC
	private void ᜀ(XlsWorkbook A_0, string A_1, HTMLOptions A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_40:
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_20D:
					num = 2;
					break;
				default:
				{
					if (false)
					{
					}
					string name = new DirectoryInfo(A_1).Name;
					count = A_0.Worksheets.Count;
					num2 = 0;
					num = 7;
					break;
				}
				}
				string text;
				FileStream fileStream;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (File.Exists(text))
						{
							num = 4;
							continue;
						}
						goto IL_205;
					case 1:
						goto IL_16B;
					case 2:
						if (true)
						{
						}
						try
						{
							XlsWorksheet a_2 = (XlsWorksheet)A_0.Worksheets[num2];
							this.ᜃ = new XmlTextWriter(fileStream, Encoding.UTF8);
							this.ᜃ.Formatting = Formatting.Indented;
							this.ᜄ = new Dictionary<string, LinkedList<string>>();
							this.ᜆ = new List<spr\u25A6.ᜀ>();
							this.ᜁ();
							this.ᜀ(a_2, A_2, this.ᜃ);
							this.ᜂ().WriteStartElement(sprᯟ.ᜃ.ᜄ);
							this.ᜀ(a_2, A_1, A_2, this.ᜃ);
							this.ᜂ().WriteEndElement();
							this.ᜀ();
							fileStream.Close();
							goto IL_1F0;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_156;
								case 2:
									((IDisposable)fileStream).Dispose();
									num = 1;
									continue;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 2;
							}
							IL_156:;
						}
						goto IL_159;
						IL_1F0:
						num2++;
						num = 3;
						continue;
					case 3:
						goto IL_170;
					case 4:
						goto IL_159;
					case 5:
						return;
					case 6:
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						text = string.Format(RecordTableEnumerator.b("㉈筊が慎㥐❒㡔㭖", a_), A_0.Worksheets[num2].Name);
						text = Path.Combine(A_1, text);
						num = 0;
						continue;
					case 7:
						goto IL_170;
					}
					goto IL_40;
					IL_159:
					File.Delete(text);
					num = 1;
					continue;
					IL_170:
					num = 6;
				}
				IL_205:
				fileStream = new FileStream(text, FileMode.CreateNew);
				goto IL_20D;
				IL_16B:
				goto IL_205;
			}
			return;
		}
	}

	// Token: 0x06004B71 RID: 19313 RVA: 0x002E3420 File Offset: 0x002E2420
	public void ᜃ()
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
		this.ᜄ.Clear();
		this.ᜃ.Close();
	}

	// Token: 0x0400220A RID: 8714
	private const string ᜀ = "pt";

	// Token: 0x0400220B RID: 8715
	private const string ᜁ = "px";

	// Token: 0x0400220C RID: 8716
	private const string ᜂ = "em";

	// Token: 0x0400220D RID: 8717
	private XmlTextWriter ᜃ;

	// Token: 0x0400220E RID: 8718
	private Dictionary<string, LinkedList<string>> ᜄ;

	// Token: 0x0400220F RID: 8719
	private StringBuilder ᜅ = new StringBuilder();

	// Token: 0x04002210 RID: 8720
	private List<spr\u25A6.ᜀ> ᜆ;

	// Token: 0x04002211 RID: 8721
	private string ᜇ;

	// Token: 0x04002212 RID: 8722
	private sprᯟ.ConversionMode ᜈ;

	// Token: 0x04002213 RID: 8723
	private sprᯟ.ᜁ ᜉ;

	// Token: 0x020004CA RID: 1226
	private enum ConversionMode
	{
		// Token: 0x04002215 RID: 8725
		Workbook,
		// Token: 0x04002216 RID: 8726
		Worksheet
	}

	// Token: 0x020004CB RID: 1227
	private class ᜁ
	{
		// Token: 0x06004B72 RID: 19314 RVA: 0x002E3474 File Offset: 0x002E2474
		internal Graphics ᜀ()
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

		// Token: 0x06004B73 RID: 19315 RVA: 0x002E34B8 File Offset: 0x002E24B8
		public ᜁ()
		{
			this.ᜁ();
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x002E34D4 File Offset: 0x002E24D4
		public void ᜁ()
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
			Bitmap image = new Bitmap(1, 1);
			this.ᜀ = Graphics.FromImage(image);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x002E3524 File Offset: 0x002E2524
		public SizeF ᜀ(string A_0, Font A_1)
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
			return this.ᜀ().MeasureString(A_0, A_1, new SizeF(float.MaxValue, float.MaxValue), StringFormat.GenericTypographic);
		}

		// Token: 0x04002217 RID: 8727
		private Graphics ᜀ;
	}

	// Token: 0x020004CC RID: 1228
	private class ᜃ
	{
		// Token: 0x06004B77 RID: 19319 RVA: 0x002E3594 File Offset: 0x002E2594
		// Note: this type is marked as 'beforefieldinit'.
		static ᜃ()
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
			sprᯟ.ᜃ.ᜀ = RecordTableEnumerator.b("儸伺值匾", a_);
			sprᯟ.ᜃ.ᜁ = RecordTableEnumerator.b("儸帺尼嬾", a_);
			sprᯟ.ᜃ.ᜂ = RecordTableEnumerator.b("䴸刺䤼匾⑀", a_);
			sprᯟ.ᜃ.ᜃ = RecordTableEnumerator.b("䨸伺䐼匾⑀", a_);
			sprᯟ.ᜃ.ᜄ = RecordTableEnumerator.b("嬸吺夼䘾", a_);
			sprᯟ.ᜃ.ᜅ = RecordTableEnumerator.b("堸", a_);
			sprᯟ.ᜃ.ᜆ = RecordTableEnumerator.b("椸", a_);
			sprᯟ.ᜃ.ᜇ = RecordTableEnumerator.b("弸吺匼䬾", a_);
			sprᯟ.ᜃ.ᜈ = RecordTableEnumerator.b("䴸娺弼匾⑀", a_);
			sprᯟ.ᜃ.ᜉ = RecordTableEnumerator.b("䴸强", a_);
			sprᯟ.ᜃ.ᜊ = RecordTableEnumerator.b("䴸䤺", a_);
			sprᯟ.ᜃ.ᜋ = RecordTableEnumerator.b("䴸区", a_);
			sprᯟ.ᜃ.ᜌ = RecordTableEnumerator.b("倸嘺娼", a_);
			sprᯟ.ᜃ.\u170D = RecordTableEnumerator.b("稸吺儼", a_);
			sprᯟ.ᜃ.ᜎ = RecordTableEnumerator.b("䨸䬺尼儾", a_);
			sprᯟ.ᜃ.ᜏ = RecordTableEnumerator.b("䨸堺似嘾ㅀ㝂", a_);
			sprᯟ.ᜃ.ᜐ = RecordTableEnumerator.b("倸崺似帾ⱀ♂", a_);
			sprᯟ.ᜃ.ᜑ = RecordTableEnumerator.b("倸唺䴼䨾㕀", a_);
			sprᯟ.ᜃ.\u1712 = RecordTableEnumerator.b("弸䤺尼刾⑀あ⁄㍆", a_);
			sprᯟ.ᜃ.\u1713 = RecordTableEnumerator.b("弸䤺尼刾⑀", a_);
			sprᯟ.ᜃ.\u1714 = RecordTableEnumerator.b("圸吺嬼䴾⁀⹂⁄㑆", a_);
		}

		// Token: 0x04002218 RID: 8728
		public static string ᜀ;

		// Token: 0x04002219 RID: 8729
		public static string ᜁ;

		// Token: 0x0400221A RID: 8730
		public static string ᜂ;

		// Token: 0x0400221B RID: 8731
		public static string ᜃ;

		// Token: 0x0400221C RID: 8732
		public static string ᜄ;

		// Token: 0x0400221D RID: 8733
		public static string ᜅ;

		// Token: 0x0400221E RID: 8734
		public static string ᜆ;

		// Token: 0x0400221F RID: 8735
		public static string ᜇ;

		// Token: 0x04002220 RID: 8736
		public static string ᜈ;

		// Token: 0x04002221 RID: 8737
		public static string ᜉ;

		// Token: 0x04002222 RID: 8738
		public static string ᜊ;

		// Token: 0x04002223 RID: 8739
		public static string ᜋ;

		// Token: 0x04002224 RID: 8740
		public static string ᜌ;

		// Token: 0x04002225 RID: 8741
		public static string \u170D;

		// Token: 0x04002226 RID: 8742
		public static string ᜎ;

		// Token: 0x04002227 RID: 8743
		public static string ᜏ;

		// Token: 0x04002228 RID: 8744
		public static string ᜐ;

		// Token: 0x04002229 RID: 8745
		public static string ᜑ;

		// Token: 0x0400222A RID: 8746
		public static string \u1712;

		// Token: 0x0400222B RID: 8747
		public static string \u1713;

		// Token: 0x0400222C RID: 8748
		public static string \u1714;
	}

	// Token: 0x020004CD RID: 1229
	private class ᜂ
	{
		// Token: 0x06004B79 RID: 19321 RVA: 0x002E377C File Offset: 0x002E277C
		// Note: this type is marked as 'beforefieldinit'.
		static ᜂ()
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			sprᯟ.ᜂ.ᜀ = RecordTableEnumerator.b("圵吷匹嬻倽", a_);
			sprᯟ.ᜂ.ᜁ = RecordTableEnumerator.b("張䰷嬹倻圽⌿", a_);
			sprᯟ.ᜂ.ᜂ = RecordTableEnumerator.b("吵圷嘹堻", a_);
			sprᯟ.ᜂ.ᜃ = RecordTableEnumerator.b("刵圷伹帻刽┿", a_);
			sprᯟ.ᜂ.ᜄ = RecordTableEnumerator.b("䌵嘷帹夻䰽ⰿ⭁⩃⍅", a_);
			sprᯟ.ᜂ.ᜅ = RecordTableEnumerator.b("吵夷夹圻夽㈿ⵁㅃ⡅ⱇ杉⽋⅍㱏㵑♓", a_);
			sprᯟ.ᜂ.ᜆ = RecordTableEnumerator.b("吵強礹医刽⼿ぁ", a_);
			sprᯟ.ᜂ.ᜇ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁❃⥅⑇╉㹋", a_);
			sprᯟ.ᜂ.ᜈ = RecordTableEnumerator.b("唵吷嬹伻䴽", a_);
			sprᯟ.ᜂ.ᜉ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿", a_);
			sprᯟ.ᜂ.ᜊ = RecordTableEnumerator.b("唵圷嘹医䰽", a_);
			sprᯟ.ᜂ.ᜋ = RecordTableEnumerator.b("倵圷吹䠻ጽ⸿⍁⥃⍅", a_);
			sprᯟ.ᜂ.ᜌ = RecordTableEnumerator.b("倵圷吹䠻ጽ㌿㙁㵃⩅ⵇ", a_);
			sprᯟ.ᜂ.\u170D = RecordTableEnumerator.b("倵圷吹䠻ጽ㌿⭁㹃⍅", a_);
			sprᯟ.ᜂ.ᜎ = RecordTableEnumerator.b("䔵䰷䠹唻唽┿潁ぃ⹅㩇╉㥋⥍㡏", a_);
			sprᯟ.ᜂ.ᜏ = RecordTableEnumerator.b("䈵崷䈹䠻ጽℿ⹁ⵃⅅ♇", a_);
			sprᯟ.ᜂ.ᜐ = RecordTableEnumerator.b("娵崷尹䠻", a_);
			sprᯟ.ᜂ.ᜑ = RecordTableEnumerator.b("䐵儷崹吻䨽", a_);
			sprᯟ.ᜂ.\u1712 = RecordTableEnumerator.b("唵崷吹䠻嬽㈿", a_);
			sprᯟ.ᜂ.\u1713 = RecordTableEnumerator.b("嬵夷䠹嬻圽⸿潁⡃⍅⹇㹉", a_);
			sprᯟ.ᜂ.\u1714 = RecordTableEnumerator.b("嬵夷䠹嬻圽⸿潁ぃ⥅㡇", a_);
			sprᯟ.ᜂ.\u1715 = RecordTableEnumerator.b("䘵圷䤹唻䨽⤿ⵁ⩃", a_);
			sprᯟ.ᜂ.\u1716 = RecordTableEnumerator.b("圵娷䤹医刽㔿㙁⅃", a_);
			sprᯟ.ᜂ.\u1717 = RecordTableEnumerator.b("䔵䨷夹", a_);
			sprᯟ.ᜂ.\u1718 = RecordTableEnumerator.b("圵吷丹", a_);
			sprᯟ.ᜂ.\u1719 = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁ぃ⥅㡇", a_);
			sprᯟ.ᜂ.\u171A = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁♃⥅㱇㹉⍋⍍", a_);
			sprᯟ.ᜂ.\u171B = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁⡃⍅⹇㹉", a_);
			sprᯟ.ᜂ.\u171C = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁㙃⽅⽇≉㡋", a_);
			sprᯟ.ᜂ.\u171D = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁ぃ⥅㡇杉⽋⅍㱏㵑♓", a_);
			sprᯟ.ᜂ.\u171E = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁♃⥅㱇㹉⍋⍍絏ㅑ㭓㩕㝗⡙", a_);
			sprᯟ.ᜂ.\u171F = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁⡃⍅⹇㹉態ⵍ㽏㹑㭓⑕", a_);
			sprᯟ.ᜂ.ᜠ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁㙃⽅⽇≉㡋捍㍏㵑㡓㥕⩗", a_);
			sprᯟ.ᜂ.ᜡ = RecordTableEnumerator.b("䐵強堹", a_);
			sprᯟ.ᜂ.ᜢ = RecordTableEnumerator.b("䔵圷嘹唻娽", a_);
			sprᯟ.ᜂ.ᜣ = RecordTableEnumerator.b("搵眷洹漻渽Ŀు", a_);
			sprᯟ.ᜂ.ᜤ = RecordTableEnumerator.b("电眷瘹漻渽Ŀు", a_);
			sprᯟ.ᜂ.ᜥ = RecordTableEnumerator.b("倵圷吹䠻ጽ㜿❁ⵃⅅ⁇㹉", a_);
			sprᯟ.ᜂ.ᜦ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁ぃ⥅㡇杉㭋❍㑏♑㱓", a_);
			sprᯟ.ᜂ.ᜧ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁♃⥅㱇㹉⍋⍍絏║㵓㉕ⱗ㉙", a_);
			sprᯟ.ᜂ.ᜨ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁⡃⍅⹇㹉態㥍㥏㙑⁓㹕", a_);
			sprᯟ.ᜂ.ᜩ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁㙃⽅⽇≉㡋捍❏㭑こ≕し", a_);
			sprᯟ.ᜂ.ᜪ = RecordTableEnumerator.b("刵夷䤹吻嬽␿", a_);
			sprᯟ.ᜂ.ᜫ = RecordTableEnumerator.b("刵圷丹䠻嬽␿", a_);
			sprᯟ.ᜂ.ᜬ = RecordTableEnumerator.b("倵圷吹䠻ጽ☿⍁⥃⽅⑇㍉", a_);
			sprᯟ.ᜂ.ᜭ = RecordTableEnumerator.b("䌵嘷帹夻䰽ⰿ⭁⩃⍅", a_);
			sprᯟ.ᜂ.ᜮ = RecordTableEnumerator.b("䈵崷䈹䠻ጽ␿❁❃⥅㩇⭉㡋❍㽏㱑", a_);
			sprᯟ.ᜂ.ᜯ = RecordTableEnumerator.b("䀵崷䠹䠻圽⌿⍁⡃歅⥇♉╋⥍㹏", a_);
			sprᯟ.ᜂ.ᜰ = RecordTableEnumerator.b("䈵圷䨹", a_);
			sprᯟ.ᜂ.ᜱ = RecordTableEnumerator.b("吵圷丹䠻儽ⴿ", a_);
			sprᯟ.ᜂ.\u1732 = RecordTableEnumerator.b("刵儷䤹䠻䰽⤿⁁ㅃ㉅ⵇ⹉", a_);
			sprᯟ.ᜂ.\u1733 = RecordTableEnumerator.b("尵䴷䤹䠻圽☿㭁", a_);
			sprᯟ.ᜂ.\u1734 = RecordTableEnumerator.b("儵崷吹夻䰽ℿ⹁", a_);
			sprᯟ.ᜂ.\u1735 = RecordTableEnumerator.b("張尷", a_);
			sprᯟ.ᜂ.\u1736 = RecordTableEnumerator.b("䈵䄷䨹夻", a_);
			sprᯟ.ᜂ.\u1737 = RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_);
			sprᯟ.ᜂ.\u1738 = RecordTableEnumerator.b("帵崷匹嬻嘽㐿", a_);
			sprᯟ.ᜂ.\u1739 = RecordTableEnumerator.b("䄵儷帹䠻嘽", a_);
			sprᯟ.ᜂ.\u173A = RecordTableEnumerator.b("䔵嬷䠹医刽ⰿ⁁╃㑅", a_);
			sprᯟ.ᜂ.\u173B = RecordTableEnumerator.b("䐵圷䴹伻", a_);
			sprᯟ.ᜂ.\u173C = RecordTableEnumerator.b("倵䨷嬹儻嬽∿ⵁ㙃≅ⵇ㡉", a_);
			sprᯟ.ᜂ.\u173D = RecordTableEnumerator.b("堵夷圹夻", a_);
			sprᯟ.ᜂ.\u173E = RecordTableEnumerator.b("䔵嬷䠹医刽ⰿ⭁⩃ⅅ", a_);
			sprᯟ.ᜂ.\u173F = RecordTableEnumerator.b("堵圷吹夻", a_);
			sprᯟ.ᜂ.ᝀ = RecordTableEnumerator.b("圵吷匹刻唽", a_);
			sprᯟ.ᜂ.ᝁ = RecordTableEnumerator.b("䀵吷匹刻唽", a_);
			sprᯟ.ᜂ.ᝂ = RecordTableEnumerator.b("娵儷吹圻", a_);
			sprᯟ.ᜂ.ᝃ = RecordTableEnumerator.b("䈵夷䠹嬻嬽㐿", a_);
			sprᯟ.ᜂ.ᝄ = RecordTableEnumerator.b("帵䨷弹娻", a_);
			sprᯟ.ᜂ.ᝅ = RecordTableEnumerator.b("圵ȷ刹医䠽┿ぁ", a_);
			sprᯟ.ᜂ.ᝆ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁❃⥅⑇♉ⵋ㹍⍏㝑", a_);
			sprᯟ.ᜂ.ᝇ = RecordTableEnumerator.b("吵圷䠹堻嬽㈿潁㝃㙅⥇⥉╋⁍㝏", a_);
			sprᯟ.ᜂ.ᝈ = RecordTableEnumerator.b("匵唷䨹䠻䜽洿⅁⅃⩅⑇㥉", a_);
			sprᯟ.ᜂ.ᝉ = RecordTableEnumerator.b("唵崷嘹倻䴽〿⍁❃⽅♇ⵉ", a_);
		}

		// Token: 0x0400222D RID: 8749
		public static string ᜀ;

		// Token: 0x0400222E RID: 8750
		public static string ᜁ;

		// Token: 0x0400222F RID: 8751
		public static string ᜂ;

		// Token: 0x04002230 RID: 8752
		public static string ᜃ;

		// Token: 0x04002231 RID: 8753
		public static string ᜄ;

		// Token: 0x04002232 RID: 8754
		public static string ᜅ;

		// Token: 0x04002233 RID: 8755
		public static string ᜆ;

		// Token: 0x04002234 RID: 8756
		public static string ᜇ;

		// Token: 0x04002235 RID: 8757
		public static string ᜈ;

		// Token: 0x04002236 RID: 8758
		public static string ᜉ;

		// Token: 0x04002237 RID: 8759
		public static string ᜊ;

		// Token: 0x04002238 RID: 8760
		public static string ᜋ;

		// Token: 0x04002239 RID: 8761
		public static string ᜌ;

		// Token: 0x0400223A RID: 8762
		public static string \u170D;

		// Token: 0x0400223B RID: 8763
		public static string ᜎ;

		// Token: 0x0400223C RID: 8764
		public static string ᜏ;

		// Token: 0x0400223D RID: 8765
		public static string ᜐ;

		// Token: 0x0400223E RID: 8766
		public static string ᜑ;

		// Token: 0x0400223F RID: 8767
		public static string \u1712;

		// Token: 0x04002240 RID: 8768
		public static string \u1713;

		// Token: 0x04002241 RID: 8769
		public static string \u1714;

		// Token: 0x04002242 RID: 8770
		public static string \u1715;

		// Token: 0x04002243 RID: 8771
		public static string \u1716;

		// Token: 0x04002244 RID: 8772
		public static string \u1717;

		// Token: 0x04002245 RID: 8773
		public static string \u1718;

		// Token: 0x04002246 RID: 8774
		public static string \u1719;

		// Token: 0x04002247 RID: 8775
		public static string \u171A;

		// Token: 0x04002248 RID: 8776
		public static string \u171B;

		// Token: 0x04002249 RID: 8777
		public static string \u171C;

		// Token: 0x0400224A RID: 8778
		public static string \u171D;

		// Token: 0x0400224B RID: 8779
		public static string \u171E;

		// Token: 0x0400224C RID: 8780
		public static string \u171F;

		// Token: 0x0400224D RID: 8781
		public static string ᜠ;

		// Token: 0x0400224E RID: 8782
		public static string ᜡ;

		// Token: 0x0400224F RID: 8783
		public static string ᜢ;

		// Token: 0x04002250 RID: 8784
		public static string ᜣ;

		// Token: 0x04002251 RID: 8785
		public static string ᜤ;

		// Token: 0x04002252 RID: 8786
		public static string ᜥ;

		// Token: 0x04002253 RID: 8787
		public static string ᜦ;

		// Token: 0x04002254 RID: 8788
		public static string ᜧ;

		// Token: 0x04002255 RID: 8789
		public static string ᜨ;

		// Token: 0x04002256 RID: 8790
		public static string ᜩ;

		// Token: 0x04002257 RID: 8791
		public static string ᜪ;

		// Token: 0x04002258 RID: 8792
		public static string ᜫ;

		// Token: 0x04002259 RID: 8793
		public static string ᜬ;

		// Token: 0x0400225A RID: 8794
		public static string ᜭ;

		// Token: 0x0400225B RID: 8795
		public static string ᜮ;

		// Token: 0x0400225C RID: 8796
		public static string ᜯ;

		// Token: 0x0400225D RID: 8797
		public static string ᜰ;

		// Token: 0x0400225E RID: 8798
		public static string ᜱ;

		// Token: 0x0400225F RID: 8799
		public static string \u1732;

		// Token: 0x04002260 RID: 8800
		public static string \u1733;

		// Token: 0x04002261 RID: 8801
		public static string \u1734;

		// Token: 0x04002262 RID: 8802
		public static string \u1735;

		// Token: 0x04002263 RID: 8803
		public static string \u1736;

		// Token: 0x04002264 RID: 8804
		public static string \u1737;

		// Token: 0x04002265 RID: 8805
		public static string \u1738;

		// Token: 0x04002266 RID: 8806
		public static string \u1739;

		// Token: 0x04002267 RID: 8807
		public static string \u173A;

		// Token: 0x04002268 RID: 8808
		public static string \u173B;

		// Token: 0x04002269 RID: 8809
		public static string \u173C;

		// Token: 0x0400226A RID: 8810
		public static string \u173D;

		// Token: 0x0400226B RID: 8811
		public static string \u173E;

		// Token: 0x0400226C RID: 8812
		public static string \u173F;

		// Token: 0x0400226D RID: 8813
		public static string ᝀ;

		// Token: 0x0400226E RID: 8814
		public static string ᝁ;

		// Token: 0x0400226F RID: 8815
		public static string ᝂ;

		// Token: 0x04002270 RID: 8816
		public static string ᝃ;

		// Token: 0x04002271 RID: 8817
		public static string ᝄ;

		// Token: 0x04002272 RID: 8818
		public static string ᝅ;

		// Token: 0x04002273 RID: 8819
		public static string ᝆ;

		// Token: 0x04002274 RID: 8820
		public static string ᝇ;

		// Token: 0x04002275 RID: 8821
		public static string ᝈ;

		// Token: 0x04002276 RID: 8822
		public static string ᝉ;
	}

	// Token: 0x020004CE RID: 1230
	private class ᜀ
	{
		// Token: 0x06004B7B RID: 19323 RVA: 0x002E3D54 File Offset: 0x002E2D54
		// Note: this type is marked as 'beforefieldinit'.
		static ᜀ()
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
			sprᯟ.ᜀ.ᜀ = RecordTableEnumerator.b("䔽", a_);
			sprᯟ.ᜀ.ᜁ = RecordTableEnumerator.b("䌽", a_);
			sprᯟ.ᜀ.ᜂ = RecordTableEnumerator.b("ሽ", a_);
			sprᯟ.ᜀ.ᜃ = RecordTableEnumerator.b("ွ", a_);
			sprᯟ.ᜀ.ᜄ = RecordTableEnumerator.b("н", a_);
			sprᯟ.ᜀ.ᜅ = RecordTableEnumerator.b("Խ", a_);
			sprᯟ.ᜀ.ᜆ = RecordTableEnumerator.b("ᘽ", a_);
			sprᯟ.ᜀ.ᜇ = RecordTableEnumerator.b("᜽", a_);
		}

		// Token: 0x04002277 RID: 8823
		public static string ᜀ;

		// Token: 0x04002278 RID: 8824
		public static string ᜁ;

		// Token: 0x04002279 RID: 8825
		public static string ᜂ;

		// Token: 0x0400227A RID: 8826
		public static string ᜃ;

		// Token: 0x0400227B RID: 8827
		public static string ᜄ;

		// Token: 0x0400227C RID: 8828
		public static string ᜅ;

		// Token: 0x0400227D RID: 8829
		public static string ᜆ;

		// Token: 0x0400227E RID: 8830
		public static string ᜇ;
	}
}

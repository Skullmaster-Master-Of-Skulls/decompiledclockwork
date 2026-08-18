using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using Spire.CompoundFile.Doc;

// Token: 0x02000212 RID: 530
internal class spr\u1B3B
{
	// Token: 0x06001907 RID: 6407 RVA: 0x00186248 File Offset: 0x00185248
	public bool ᜀ(XmlWriter A_0, string A_1, object A_2)
	{
		for (;;)
		{
			this.ᜁ = A_0;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CE;
				case 1:
					goto IL_8A;
				case 2:
					this.ᜀ(A_1, A_2 as Matrix);
					num = 0;
					continue;
				case 3:
					if (true)
					{
					}
					this.ᜀ(A_1, (Font)A_2);
					num = 4;
					continue;
				case 4:
					goto IL_6D;
				case 5:
					if (A_2 is Font)
					{
						num = 3;
						continue;
					}
					return false;
				case 6:
					if (A_2 is Color)
					{
						num = 7;
						continue;
					}
					num = 5;
					continue;
				case 7:
					this.ᜀ(A_1, (Color)A_2);
					num = 1;
					continue;
				case 8:
					if (A_2 is Matrix)
					{
						num = 2;
						continue;
					}
					goto IL_CE;
				}
				break;
				IL_CE:
				num = 6;
			}
		}
		IL_6D:
		return true;
		IL_8A:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
			if (false)
			{
			}
			break;
		}
		return true;
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x00186368 File Offset: 0x00185368
	public object ᜀ(XmlReader A_0, Type A_1)
	{
		for (;;)
		{
			this.ᜀ = A_0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1.Equals(typeof(Matrix)))
					{
						num = 1;
						continue;
					}
					num = 5;
					continue;
				case 1:
					goto IL_51;
				case 2:
					goto IL_96;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (A_1.Equals(typeof(Font)))
						{
							num = 7;
							continue;
						}
						goto IL_11D;
					}
					break;
				case 4:
					goto IL_C0;
				case 5:
					if (A_1.Equals(typeof(Color)))
					{
						num = 4;
						continue;
					}
					if (true)
					{
					}
					num = 6;
					continue;
				case 6:
					if (A_1.Equals(typeof(Color)))
					{
						num = 2;
						continue;
					}
					num = 3;
					continue;
				case 7:
					goto IL_114;
				}
				break;
			}
		}
		IL_51:
		return this.ᜀ();
		IL_96:
		return this.ᜁ();
		IL_C0:
		return this.ᜀ();
		IL_114:
		return this.ᜂ();
		IL_11D:
		return null;
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x00186494 File Offset: 0x00185494
	private void ᜀ(string A_0, Matrix A_1)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_0.Length == 0)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			case 1:
				goto IL_46;
			case 3:
				goto IL_E1;
			case 4:
				goto IL_5E;
			case 5:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				goto IL_E3;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_46:
		goto IL_90;
		IL_5E:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_90:
			throw new ArgumentNullException(ClipboardData.b("᥶ᡸᙺ᡼", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("᩶ᡸེོᙾ呂", a_));
		}
		IL_E1:
		throw new ArgumentException(ClipboardData.b("᥶ᡸᙺ᡼彾검ꎂﮈ놐練릘膠솢삤螦첨욪\uddac\udbae좰", a_));
		IL_E3:
		this.ᜁ.WriteStartElement(A_0);
		float[] elements = A_1.Elements;
		this.ᜁ.WriteAttributeString(ClipboardData.b("᩶䡸䩺", a_), XmlConvert.ToString(elements[0]));
		this.ᜁ.WriteAttributeString(ClipboardData.b("᩶䡸䥺", a_), XmlConvert.ToString(elements[1]));
		this.ᜁ.WriteAttributeString(ClipboardData.b("᩶䭸䩺", a_), XmlConvert.ToString(elements[2]));
		this.ᜁ.WriteAttributeString(ClipboardData.b("᩶䭸䥺", a_), XmlConvert.ToString(elements[3]));
		this.ᜁ.WriteAttributeString(ClipboardData.b("፶䡸", a_), XmlConvert.ToString(elements[4]));
		this.ᜁ.WriteAttributeString(ClipboardData.b("፶䭸", a_), XmlConvert.ToString(elements[5]));
		this.ᜁ.WriteEndElement();
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x00186668 File Offset: 0x00185668
	private void ᜀ(string A_0, Color A_1)
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
		this.ᜁ.WriteStartElement(A_0);
		this.ᜁ.WriteAttributeString(ClipboardData.b("ᥬ᙮Űᙲ", a_), ClipboardData.b("⹬nᵰᱲݴ", a_));
		this.ᜁ.WriteAttributeString(ClipboardData.b("౬ᵮᙰᅲ", a_), XmlConvert.ToString(A_1.ToArgb()));
		this.ᜁ.WriteEndElement();
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x00186710 File Offset: 0x00185710
	private void ᜀ(string A_0, Font A_1)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜁ.WriteStartElement(A_0);
		this.ᜁ.WriteAttributeString(ClipboardData.b("հੲմቶ", a_), ClipboardData.b("㝰ᱲ᭴Ͷ", a_));
		this.ᜁ.WriteAttributeString(ClipboardData.b("ᝰᱲ᭴Ͷ㝸᩺ၼ᩾", a_), A_1.Name);
		this.ᜁ.WriteAttributeString(ClipboardData.b("ɰᩲུቶ", a_), A_1.SizeInPoints.ToString());
		this.ᜁ.WriteEndElement();
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x001867D8 File Offset: 0x001857D8
	private Font ᜂ()
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
		string attribute = this.ᜀ.GetAttribute(ClipboardData.b("ታ᥵ᙷ๹㉻ώ", a_));
		string attribute2 = this.ᜀ.GetAttribute(ClipboardData.b("ݳήɷό", a_));
		this.ᜀ.GetAttribute(ClipboardData.b("ݳɵŷᙹ᥻", a_));
		this.ᜀ.Read();
		return spr\u215C.ᜀ(attribute, (float)int.Parse(attribute2));
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x00186884 File Offset: 0x00185884
	private Color ᜁ()
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
		string attribute = this.ᜀ.GetAttribute(ClipboardData.b("ᕳѵί᡹", a_));
		this.ᜀ.Read();
		return Color.FromArgb(int.Parse(attribute));
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x001868FC File Offset: 0x001858FC
	private Matrix ᜀ()
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string attribute = this.ᜀ.GetAttribute(ClipboardData.b("ά䕳䝵", a_));
		float m = XmlConvert.ToSingle(attribute);
		attribute = this.ᜀ.GetAttribute(ClipboardData.b("ά䕳䑵", a_));
		float m2 = XmlConvert.ToSingle(attribute);
		attribute = this.ᜀ.GetAttribute(ClipboardData.b("ά䙳䝵", a_));
		float m3 = XmlConvert.ToSingle(attribute);
		attribute = this.ᜀ.GetAttribute(ClipboardData.b("ά䙳䑵", a_));
		float m4 = XmlConvert.ToSingle(attribute);
		attribute = this.ᜀ.GetAttribute(ClipboardData.b("ᙱ䕳", a_));
		float dx = XmlConvert.ToSingle(attribute);
		attribute = this.ᜀ.GetAttribute(ClipboardData.b("ᙱ䙳", a_));
		float dy = XmlConvert.ToSingle(attribute);
		Matrix result = new Matrix(m, m2, m3, m4, dx, dy);
		this.ᜀ.Read();
		return result;
	}

	// Token: 0x04001CD6 RID: 7382
	private XmlReader ᜀ;

	// Token: 0x04001CD7 RID: 7383
	private XmlWriter ᜁ;
}

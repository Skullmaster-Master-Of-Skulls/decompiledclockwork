using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

// Token: 0x02000367 RID: 871
internal class sprṑ : IXDLSAttributeWriter, IXDLSContentWriter
{
	// Token: 0x060030EC RID: 12524 RVA: 0x002CEF7C File Offset: 0x002CDF7C
	public sprṑ(XmlWriter A_0)
	{
		int a_ = 0;
		this.ᜃ = ClipboardData.b("⥥╧♩", a_);
		this.ᜄ = new spr\u1B3B();
		base..ctor();
		this.ᜂ = A_0;
	}

	// Token: 0x060030ED RID: 12525 RVA: 0x002CEFC0 File Offset: 0x002CDFC0
	public void ᜀ(IDocumentSerializable A_0)
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
		A_0.XDLSHolder.BeforeSerialization();
		this.ᜀ(this.ᜃ, A_0, false);
	}

	// Token: 0x060030EE RID: 12526 RVA: 0x002CF014 File Offset: 0x002CE014
	private void ᜀ(string A_0, IDocumentSerializable A_1, bool A_2)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_D7;
			case 3:
				this.ᜀ(ClipboardData.b("ٮᕰ", a_), A_1.XDLSHolder.ID);
				if (true)
				{
				}
				num = 1;
				continue;
			case 4:
				num = 6;
				continue;
			case 5:
				if (A_2)
				{
					num = 4;
					continue;
				}
				goto IL_D7;
			case 6:
				if (A_1.XDLSHolder.EnableID)
				{
					num = 3;
					continue;
				}
				goto IL_D7;
			case 7:
				this.ᜂ.WriteStartElement(A_0);
				num = 5;
				continue;
			}
			IL_39:
			if (!A_1.XDLSHolder.SkipMe)
			{
				num = 7;
				continue;
			}
			break;
			IL_D7:
			A_1.WriteXmlAttributes(this);
			A_1.WriteXmlContent(this);
			this.ᜂ.WriteEndElement();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_39;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x060030EF RID: 12527 RVA: 0x002CF13C File Offset: 0x002CE13C
	private void ᜀ(string A_0, IXDLSSerializableCollection A_1)
	{
		int num = 2;
		for (;;)
		{
			IEnumerator enumerator;
			switch (num)
			{
			case 0:
				goto IL_131;
			case 1:
				return;
			case 3:
				try
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AA;
							default:
							{
								if (false)
								{
								}
								IDocumentSerializable documentSerializable;
								this.ᜀ(A_1.TagItemName, documentSerializable, true);
								num = 4;
								continue;
							}
							}
							break;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							IDocumentSerializable documentSerializable = (IDocumentSerializable)enumerator.Current;
							num = 5;
							continue;
						}
						case 3:
							goto IL_EE;
						case 5:
						{
							IDocumentSerializable documentSerializable;
							if (documentSerializable != null)
							{
								num = 0;
								continue;
							}
							break;
						}
						case 6:
							goto IL_AA;
						}
						IL_92:
						num = 2;
						continue;
						goto IL_92;
						IL_AA:
						num = 3;
					}
					IL_EE:
					goto IL_154;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_130;
							case 1:
								disposable.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_12E;
							}
							break;
						}
					}
					IL_12E:
					IL_130:;
				}
				goto IL_131;
				IL_154:
				this.ᜂ.WriteEndElement();
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (A_1.Count > 0)
			{
				num = 0;
				continue;
			}
			break;
			IL_131:
			this.ᜂ.WriteStartElement(A_0);
			enumerator = A_1.GetEnumerator();
			num = 3;
		}
	}

	// Token: 0x060030F0 RID: 12528 RVA: 0x002CF2D0 File Offset: 0x002CE2D0
	protected virtual void ᜁ(string A_0, object A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				goto IL_5F;
			}
			if (this.ᜄ.ᜀ(this.ᜂ, A_0, A_1))
			{
				break;
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
				num = 2;
				continue;
			}
			IL_5F:
			this.ᜀ(A_0, A_1);
			if (true)
			{
			}
			num = 0;
		}
	}

	// Token: 0x060030F1 RID: 12529 RVA: 0x002CF358 File Offset: 0x002CE358
	private void ᜀ(string A_0, object A_1)
	{
		for (;;)
		{
			IXmlSerializable xmlSerializable = A_1 as IXmlSerializable;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (xmlSerializable != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
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
						xmlSerializable.WriteXml(this.ᜂ);
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x060030F2 RID: 12530 RVA: 0x002CF3DC File Offset: 0x002CE3DC
	public void ᜀ(string A_0, float A_1)
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
		this.ᜂ.WriteAttributeString(A_0, XmlConvert.ToString(A_1));
	}

	// Token: 0x060030F3 RID: 12531 RVA: 0x002CF42C File Offset: 0x002CE42C
	public void ᜀ(string A_0, double A_1)
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
		this.ᜂ.WriteAttributeString(A_0, XmlConvert.ToString(A_1));
	}

	// Token: 0x060030F4 RID: 12532 RVA: 0x002CF47C File Offset: 0x002CE47C
	public void ᜀ(string A_0, int A_1)
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
		this.ᜂ.WriteAttributeString(A_0, XmlConvert.ToString(A_1));
	}

	// Token: 0x060030F5 RID: 12533 RVA: 0x002CF4CC File Offset: 0x002CE4CC
	public void ᜀ(string A_0, string A_1)
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
		this.ᜂ.WriteAttributeString(A_0, A_1);
	}

	// Token: 0x060030F6 RID: 12534 RVA: 0x002CF514 File Offset: 0x002CE514
	public void ᜀ(string A_0, Enum A_1)
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
		this.ᜂ.WriteAttributeString(A_0, A_1.ToString());
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x002CF564 File Offset: 0x002CE564
	public void ᜀ(string A_0, bool A_1)
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
		this.ᜂ.WriteAttributeString(A_0, XmlConvert.ToString(A_1));
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x002CF5B4 File Offset: 0x002CE5B4
	public void ᜀ(string A_0, Color A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 1;
			StringBuilder stringBuilder;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1.IsEmpty)
					{
						num = 6;
						continue;
					}
					goto IL_1AE;
				case 2:
					if (A_0.Length != 0)
					{
						if (true)
						{
						}
						stringBuilder = new StringBuilder();
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_1A9;
				case 4:
					goto IL_168;
				case 5:
					goto IL_54;
				case 6:
					stringBuilder.Append(ClipboardData.b("䩨", a_));
					stringBuilder.Append(A_1.A.ToString(ClipboardData.b("ㅨ奪", a_)));
					stringBuilder.Append(A_1.R.ToString(ClipboardData.b("ㅨ奪", a_)));
					stringBuilder.Append(A_1.G.ToString(ClipboardData.b("ㅨ奪", a_)));
					stringBuilder.Append(A_1.B.ToString(ClipboardData.b("ㅨ奪", a_)));
					num = 4;
					continue;
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
			IL_54:
			throw new ArgumentNullException(ClipboardData.b("ݨ੪l੮", a_));
			IL_168:
			goto IL_1AE;
			IL_1A9:
			throw new ArgumentException(ClipboardData.b("ݨ੪l੮兰干啴Ѷ൸ॺᑼᅾꎂꮊ뎒릘ﺚ햠\udaa2", a_));
			IL_1AE:
			this.ᜂ.WriteAttributeString(A_0, stringBuilder.ToString());
			return;
		}
		}
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x002CF784 File Offset: 0x002CE784
	public void ᜀ(string A_0, DateTime A_1)
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
		this.ᜂ.WriteAttributeString(A_0, XmlConvert.ToString(A_1, XmlDateTimeSerializationMode.Utc));
	}

	// Token: 0x060030FA RID: 12538 RVA: 0x002CF7D4 File Offset: 0x002CE7D4
	public void ᜁ(string A_0, string A_1)
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
		this.ᜂ.WriteStartElement(A_0);
		this.ᜂ.WriteString(A_1);
		this.ᜂ.WriteEndElement();
	}

	// Token: 0x060030FB RID: 12539 RVA: 0x002CF834 File Offset: 0x002CE834
	public void ᜀ(string A_0, byte[] A_1)
	{
		int a_ = 14;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8C;
				}
				break;
			case 1:
				if (A_0.Length == 0)
				{
					num = 3;
					continue;
				}
				goto IL_A6;
			case 3:
				goto IL_74;
			}
			IL_33:
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 1;
			continue;
			goto IL_33;
		}
		IL_74:
		if (true)
		{
		}
		throw new ArgumentException(ClipboardData.b("ᩳ᝵ᕷό屻卽ꁿ꺍望뚕뺝슟잡蒣쎥얧\udaa9\ud8ab힭", a_));
		IL_8C:
		if (false)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᩳ᝵ᕷό", a_));
		IL_A6:
		this.ᜀ().WriteStartElement(A_0);
		this.ᜀ().WriteBase64(A_1, 0, A_1.Length);
		this.ᜀ().WriteEndElement();
	}

	// Token: 0x060030FC RID: 12540 RVA: 0x002CF910 File Offset: 0x002CE910
	public void ᜂ(string A_0, object A_1)
	{
		int a_ = 13;
		IDocumentSerializable documentSerializable;
		IXDLSSerializableCollection ixdlsserializableCollection;
		for (;;)
		{
			if (true)
			{
			}
			documentSerializable = (A_1 as IDocumentSerializable);
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_98;
				case 1:
					goto IL_22B;
				case 2:
					if (ixdlsserializableCollection != null)
					{
						num = 4;
						continue;
					}
					num = 12;
					continue;
				case 3:
					if (A_1 is float)
					{
						num = 10;
						continue;
					}
					num = 9;
					continue;
				case 4:
					goto IL_140;
				case 5:
					goto IL_208;
				case 6:
					if (A_1 is int)
					{
						num = 5;
						continue;
					}
					num = 3;
					continue;
				case 7:
					if (A_1 is Enum)
					{
						num = 13;
						continue;
					}
					goto IL_308;
				case 8:
					if (documentSerializable != null)
					{
						num = 11;
						continue;
					}
					ixdlsserializableCollection = (A_1 as IXDLSSerializableCollection);
					num = 2;
					continue;
				case 9:
					if (A_1 is bool)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
				case 10:
					goto IL_2A5;
				case 11:
					goto IL_78;
				case 12:
					if (A_1 is string)
					{
						num = 1;
						continue;
					}
					num = 6;
					continue;
				case 13:
					goto IL_11B;
				}
				break;
			}
		}
		IL_78:
		this.ᜀ(A_0, documentSerializable, false);
		return;
		IL_98:
		this.ᜂ.WriteStartElement(A_0);
		this.ᜀ(ClipboardData.b("ݲ౴ݶᱸ", a_), ClipboardData.b("ㅲᩴᡶᕸṺᱼᅾ", a_));
		this.ᜀ(ClipboardData.b("ղᑴ᭶౸Ṻ", a_), A_1.ToString());
		this.ᜂ.WriteEndElement();
		return;
		IL_9D:
		this.ᜂ.WriteStartElement(A_0);
		this.ᜀ(ClipboardData.b("ݲ౴ݶᱸ", a_), ClipboardData.b("㩲᭴Ͷ䩸䥺", a_));
		this.ᜀ(ClipboardData.b("ղᑴ᭶౸Ṻ", a_), (int)A_1);
		this.ᜂ.WriteEndElement();
		return;
		IL_11B:
		this.ᜂ.WriteStartElement(A_0);
		this.ᜀ(ClipboardData.b("ݲ౴ݶᱸ", a_), A_1.GetType().ToString());
		this.ᜀ(ClipboardData.b("ղᑴ᭶౸Ṻ", a_), A_1.ToString());
		this.ᜂ.WriteEndElement();
		return;
		IL_140:
		this.ᜀ(A_0, ixdlsserializableCollection);
		return;
		IL_208:
		goto IL_9D;
		IL_22B:
		this.ᜂ.WriteStartElement(A_0);
		this.ᜀ(ClipboardData.b("ݲ౴ݶᱸ", a_), ClipboardData.b("⁲Ŵնၸᕺ᩼", a_));
		this.ᜀ(ClipboardData.b("ղᑴ᭶౸Ṻ", a_), (string)A_1);
		this.ᜂ.WriteEndElement();
		return;
		IL_2A5:
		this.ᜂ.WriteStartElement(A_0);
		this.ᜀ(ClipboardData.b("ݲ౴ݶᱸ", a_), ClipboardData.b("⁲ᱴ᥶Ṹ᝺᡼", a_));
		this.ᜀ(ClipboardData.b("ղᑴ᭶౸Ṻ", a_), (float)A_1);
		this.ᜂ.WriteEndElement();
		return;
		IL_308:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_9D;
		default:
			if (false)
			{
			}
			this.ᜁ(A_0, A_1);
			return;
		}
	}

	// Token: 0x060030FD RID: 12541 RVA: 0x002CFC4C File Offset: 0x002CEC4C
	public void ᜁ(string A_0, int A_1)
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
		this.ᜂ.WriteStartElement(A_0);
		this.ᜀ(ClipboardData.b("Ὤ੮ᝰ", a_), A_1);
		this.ᜂ.WriteEndElement();
	}

	// Token: 0x060030FE RID: 12542 RVA: 0x002CFCBC File Offset: 0x002CECBC
	internal void ᜁ(Image A_0)
	{
		int a_ = 16;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 2:
				return;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				break;
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
				num = 0;
				continue;
			}
			IL_5E:
			MemoryStream memoryStream = this.ᜀ(A_0);
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, array.Length);
			this.ᜀ(ClipboardData.b("ήᕷ᭹᭻᭽", a_), array);
			num = 2;
		}
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x002CFD74 File Offset: 0x002CED74
	public XmlWriter ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x002CFDB8 File Offset: 0x002CEDB8
	private bool ᜀ(EmfPlusRecordType A_0, int A_1, int A_2, IntPtr A_3, PlayRecordCallback A_4)
	{
		byte[] array;
		for (;;)
		{
			if (true)
			{
			}
			array = new byte[A_2];
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						Marshal.Copy(A_3, array, 0, A_2);
						num = 0;
						continue;
					}
					break;
				case 2:
					if (A_3 != IntPtr.Zero)
					{
						num = 1;
						continue;
					}
					goto IL_7D;
				}
				break;
			}
		}
		IL_7B:
		IL_7D:
		this.ᜅ.PlayRecord(A_0, A_1, A_2, array);
		return true;
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x002CFE54 File Offset: 0x002CEE54
	private MemoryStream ᜀ(Image A_0)
	{
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			Rectangle bounds;
			Graphics graphics2;
			for (;;)
			{
				memoryStream = new MemoryStream();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E7;
					case 1:
						if (A_0 is Metafile)
						{
							num = 2;
							continue;
						}
						try
						{
							A_0.Save(memoryStream, A_0.RawFormat);
							return memoryStream;
						}
						catch
						{
							A_0.Save(memoryStream, ImageFormat.Png);
							return memoryStream;
						}
						goto IL_6C;
					case 2:
						goto IL_6C;
					}
					break;
					IL_6C:
					if (true)
					{
					}
					this.ᜅ = (A_0 as Metafile);
					bounds = this.ᜅ.GetMetafileHeader().Bounds;
					Bitmap image = new Bitmap(bounds.Width, bounds.Height, this.ᜅ.PixelFormat);
					Graphics graphics = Graphics.FromImage(image);
					IntPtr hdc = graphics.GetHdc();
					Metafile image2 = new Metafile(memoryStream, hdc, EmfType.EmfOnly);
					graphics.ReleaseHdc(hdc);
					graphics2 = Graphics.FromImage(image2);
					num = 0;
				}
			}
			IL_E7:
			try
			{
				graphics2.EnumerateMetafile(this.ᜅ, bounds.Location, new Graphics.EnumerateMetafileProc(this.ᜀ));
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_15C;
					case 2:
						goto IL_16E;
					}
					if (graphics2 == null)
					{
						break;
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
						num = 1;
						continue;
					}
					IL_15C:
					((IDisposable)graphics2).Dispose();
					num = 2;
				}
				IL_16E:;
			}
			return memoryStream;
		}
		}
	}

	// Token: 0x040026FD RID: 9981
	private const string ᜀ = "#";

	// Token: 0x040026FE RID: 9982
	private const string ᜁ = "X2";

	// Token: 0x040026FF RID: 9983
	private readonly XmlWriter ᜂ;

	// Token: 0x04002700 RID: 9984
	private string ᜃ;

	// Token: 0x04002701 RID: 9985
	private spr\u1B3B ᜄ;

	// Token: 0x04002702 RID: 9986
	private Metafile ᜅ;
}

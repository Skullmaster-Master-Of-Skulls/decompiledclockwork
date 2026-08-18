using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Common;
using Spire.DataExport.DBF;
using Spire.DataExport.ResourceMgr;

// Token: 0x02000090 RID: 144
internal class spr\u2603 : spr\u21B2
{
	// Token: 0x0600046A RID: 1130 RVA: 0x0002A63C File Offset: 0x0002963C
	public spr\u2603(ExportBase A_0, Stream A_1, TextWriter A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0002A668 File Offset: 0x00029668
	protected override void ᜀ(bool A_0)
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
			if (this.ᜅ)
			{
				return;
			}
			break;
		}
		try
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜇ.Clear();
					num = 2;
					continue;
				case 2:
					goto IL_7A;
				case 3:
					goto IL_89;
				}
				if (A_0)
				{
					num = 0;
					continue;
				}
				IL_7A:
				this.ᜅ = true;
				num = 3;
			}
			IL_89:;
		}
		finally
		{
			base.Dispose(A_0);
		}
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0002A718 File Offset: 0x00029718
	private void ᜀ(byte[] A_0)
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
		base.\u170D().Write(A_0, 0, A_0.Length);
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0002A764 File Offset: 0x00029764
	public void ᜀ(sprỚ A_0)
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
		this.ᜋ |= (A_0.ᜂ == 77);
		this.ᜇ.Add(A_0);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0002A7C4 File Offset: 0x000297C4
	public void ᜀ()
	{
		int num;
		IEnumerator enumerator2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_3F3:
			try
			{
				num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_155;
					case 3:
					{
						IEnumerator enumerator;
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						sprỚ sprỚ = (sprỚ)enumerator.Current;
						this.ᜆ.ᜆ += (int)sprỚ.ᜄ;
						num = 2;
						continue;
					}
					case 4:
						num = 1;
						continue;
					}
					IL_FF:
					num = 3;
					continue;
					goto IL_FF;
				}
				IL_155:
				goto IL_9D;
			}
			finally
			{
				for (;;)
				{
					IEnumerator enumerator;
					IDisposable disposable = enumerator as IDisposable;
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_1A2;
						case 1:
							goto IL_1A0;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_1A0:
				IL_1A2:;
			}
			goto IL_1A3;
			IL_9D:
			byte[] array = this.ᜆ.ᜀ();
			base.\u170D().Write(array, 0, array.Length);
			enumerator2 = this.ᜇ.GetEnumerator();
			num = 4;
			break;
		}
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		DateTime today;
		for (;;)
		{
			IL_36:
			switch (num)
			{
			case 0:
				if (base.ᜎ() is DBFExport)
				{
					num = 6;
					continue;
				}
				goto IL_2C0;
			case 1:
				if (base.\u170D() is FileStream)
				{
					num = 8;
					continue;
				}
				goto IL_2C0;
			case 2:
				goto IL_3F3;
			case 3:
				goto IL_364;
			case 4:
				try
				{
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_248;
						case 4:
						{
							if (!enumerator2.MoveNext())
							{
								num = 0;
								continue;
							}
							sprỚ sprỚ2 = (sprỚ)enumerator2.Current;
							byte[] array = sprỚ2.ᜀ();
							base.\u170D().Write(array, 0, array.Length);
							num = 2;
							continue;
						}
						}
						IL_222:
						num = 4;
						continue;
						goto IL_222;
					}
					IL_248:
					goto IL_3F8;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator2 as IDisposable;
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable2.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable2 != null)
								{
									num = 0;
									continue;
								}
								goto IL_295;
							case 2:
								goto IL_293;
							}
							break;
						}
					}
					IL_293:
					IL_295:;
				}
				goto IL_296;
			case 5:
				goto IL_364;
			case 6:
				goto IL_296;
			case 7:
				this.ᜆ.ᜀ = 131;
				this.ᜈ = new FileStream((base.ᜎ() as DBFExport).DbtFileName, FileMode.Create);
				this.ᜉ = new byte[512];
				Array.Clear(this.ᜉ, 0, this.ᜉ.Length);
				this.ᜈ.Write(this.ᜉ, 0, this.ᜉ.Length);
				this.ᜊ = 1;
				if (true)
				{
				}
				num = 3;
				continue;
			case 8:
				goto IL_2BB;
			case 9:
				if (this.ᜋ)
				{
					num = 7;
					continue;
				}
				goto IL_2C0;
			}
			goto IL_65;
			IL_296:
			num = 1;
			continue;
			IL_2C0:
			this.ᜆ.ᜀ = 3;
			num = 5;
			continue;
			IL_364:
			this.ᜆ.ᜁ = Convert.ToByte(today.Year.ToString().Substring(2));
			this.ᜆ.ᜂ = (byte)today.Month;
			this.ᜆ.ᜃ = (byte)today.Day;
			this.ᜆ.ᜅ = (ushort)((this.ᜇ.Count + 1) * 32 + 1);
			this.ᜆ.ᜆ = 1;
			IEnumerator enumerator = this.ᜇ.GetEnumerator();
			num = 2;
		}
		IL_2BB:
		goto IL_1A3;
		IL_3F8:
		base.\u170D().Write(new byte[]
		{
			13
		}, 0, 1);
		return;
		IL_20:
		if (false)
		{
		}
		switch (0)
		{
		}
		IL_65:
		this.ᜆ.ᜂ();
		today = DateTime.Today;
		num = 0;
		goto IL_36;
		IL_1A3:
		num = 9;
		goto IL_36;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0002AC04 File Offset: 0x00029C04
	public void ᜂ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
			{
				if (true)
				{
				}
				this.ᜈ.Seek(0L, SeekOrigin.Begin);
				byte[] bytes = BitConverter.GetBytes(this.ᜊ);
				this.ᜈ.Write(bytes, 0, bytes.Length);
				this.ᜈ.Close();
				num = 0;
				continue;
			}
			}
			IL_1C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1C;
			default:
				if (false)
				{
				}
				if (this.ᜈ == null)
				{
					return;
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0002ACB0 File Offset: 0x00029CB0
	private byte[] ᜀ(int A_0)
	{
		byte[] array;
		for (;;)
		{
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_61:
				int num;
				if (num >= array.Length)
				{
					num2 = 0;
				}
				else
				{
					array[num] = 32;
					num++;
					num2 = 3;
				}
				break;
			}
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				array = new byte[A_0];
				int num = 0;
				num2 = 2;
				break;
			}
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return array;
				case 1:
					goto IL_61;
				case 2:
					goto IL_59;
				case 3:
					goto IL_59;
				}
				break;
				IL_59:
				num2 = 1;
			}
		}
		return array;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0002AD44 File Offset: 0x00029D44
	public void ᜀ(int A_0, string A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 16;
			byte[] array2;
			byte[] array;
			string a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 9;
					continue;
				case 1:
					array = this.ᜀ(array2.Length);
					num = 6;
					continue;
				case 2:
				{
					array2 = BitConverter.GetBytes(32);
					base.\u170D().Write(array2, 0, 1);
					long position = base.\u170D().Position;
					base.\u170D().Position = 4L;
					array2 = new byte[4];
					base.\u170D().Read(array2, 0, 4);
					int num2 = BitConverter.ToInt32(array2, 0);
					num2++;
					base.\u170D().Position = 4L;
					array2 = BitConverter.GetBytes(num2);
					base.\u170D().Write(array2, 0, array2.Length);
					base.\u170D().Position = position;
					num = 19;
					continue;
				}
				case 3:
					if (array2.Length > array.Length)
					{
						num = 8;
						continue;
					}
					Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
					num = 32;
					continue;
				case 4:
				{
					byte b;
					switch (b)
					{
					case 76:
						num = 31;
						continue;
					case 77:
						goto IL_2C6;
					case 78:
					{
						if (true)
						{
						}
						ASCIIEncoding asciiencoding;
						array2 = asciiencoding.GetBytes(A_1);
						num = 3;
						continue;
					}
					default:
						num = 0;
						continue;
					}
					break;
				}
				case 5:
					goto IL_2C6;
				case 6:
					goto IL_492;
				case 7:
					num = 4;
					continue;
				case 8:
					goto IL_28E;
				case 9:
					goto IL_2C6;
				case 10:
					num = 33;
					continue;
				case 11:
				{
					if (this.ᜇ[A_0] == null)
					{
						num = 28;
						continue;
					}
					array2 = null;
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					array = this.ᜀ((int)(this.ᜇ[A_0] as sprỚ).ᜄ);
					num = 21;
					continue;
				}
				case 12:
					if (array2.Length > array.Length)
					{
						num = 1;
						continue;
					}
					goto IL_492;
				case 13:
					if (A_1.ToUpper().IndexOf(HyperlinksCollectionEditor.b("洪氬挮戰瘲", a_).ToUpper()) > -1)
					{
						num = 26;
						continue;
					}
					array[0] = 32;
					num = 25;
					continue;
				case 14:
					if (A_0 == 0)
					{
						num = 2;
						continue;
					}
					goto IL_558;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_478;
					default:
						if (false)
						{
						}
						goto IL_2C6;
					}
					break;
				case 17:
					if ((this.ᜇ[A_0] as sprỚ).ᜂ == 67)
					{
						num = 27;
						continue;
					}
					goto IL_5E2;
				case 18:
					if (array2.Length > 255)
					{
						num = 22;
						continue;
					}
					num = 12;
					continue;
				case 19:
					goto IL_558;
				case 20:
				{
					byte b;
					switch (b)
					{
					case 67:
						array2 = (base.ᜎ() as TextExport).CurrentEncoding.GetBytes(A_1);
						num = 18;
						continue;
					case 68:
						try
						{
							A_1 = DateTime.Parse(A_1).ToString(HyperlinksCollectionEditor.b("刪听嘮䠰縲破匶崸", a_));
							ASCIIEncoding asciiencoding;
							array = asciiencoding.GetBytes(A_1);
							goto IL_2C6;
						}
						catch
						{
							array = new byte[8];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = 32;
							}
							goto IL_2C6;
						}
						goto IL_501;
					default:
						num = 7;
						continue;
					}
					break;
				}
				case 21:
					if (A_1.Length > 0)
					{
						num = 29;
						continue;
					}
					goto IL_2C6;
				case 22:
					array = this.ᜀ(255);
					Array.Copy(array2, array, 255);
					num = 30;
					continue;
				case 23:
					array[0] = 84;
					num = 5;
					continue;
				case 24:
					goto IL_35B;
				case 25:
					goto IL_2C6;
				case 26:
					goto IL_478;
				case 27:
					goto IL_591;
				case 28:
					goto IL_52A;
				case 29:
				{
					byte b = (this.ᜇ[A_0] as sprỚ).ᜂ;
					num = 20;
					continue;
				}
				case 30:
					goto IL_2C6;
				case 31:
					if (A_1.ToUpper().IndexOf(HyperlinksCollectionEditor.b("缪缬種琰", a_).ToUpper()) > -1)
					{
						num = 23;
						continue;
					}
					num = 13;
					continue;
				case 32:
					goto IL_2C6;
				case 33:
					if (A_0 >= this.ᜇ.Count)
					{
						num = 24;
						continue;
					}
					goto IL_501;
				case 34:
					goto IL_2C6;
				}
				if (A_0 >= 0)
				{
					num = 10;
					continue;
				}
				goto IL_596;
				IL_2C6:
				num = 14;
				continue;
				IL_478:
				array[0] = 70;
				num = 15;
				continue;
				IL_492:
				Array.Copy(array2, array, array2.Length);
				num = 34;
				continue;
				IL_501:
				num = 11;
				continue;
				IL_558:
				a_2 = string.Empty;
				num = 17;
			}
			IL_28E:
			throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("堪稬崮帰崲刴猶尸堺吼刾⁀⽂ᙄ⹆㍈⹊", a_)), array2.Length));
			IL_35B:
			goto IL_596;
			IL_52A:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("☪✬欮匰唲瀴伶䤸吺似䬾ᙀㅂⱄ㍆ⱈ㥊睌畎ِ⅒㱔⍖㱘὚㱜⭞`佢፤٦᭨兪⍬ᩮᱰ", a_));
			IL_591:
			a_2 = (base.ᜎ() as TextExport).CurrentEncoding.GetString(array);
			base.ᜆ(a_2);
			return;
			IL_596:
			throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊьⅎ㕐㙒ⵔᡖⱘ⽚ቜ㥞⍠ౢၤ०൨ᡪ", a_)), A_0));
			IL_5E2:
			this.ᜀ(array);
			return;
		}
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0002B34C File Offset: 0x0002A34C
	public int ᜀ(ColExport A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 24;
			int result;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ColumnExport columnExport;
					if (columnExport.ColExportType == ColExportType.Binary)
					{
						num = 12;
						continue;
					}
					num = 33;
					continue;
				}
				case 1:
				{
					ExportSource dataSource;
					switch (dataSource)
					{
					case ExportSource.SqlCommand:
						num = 23;
						continue;
					case ExportSource.DataTable:
						num = 32;
						continue;
					default:
						num = 26;
						continue;
					}
					break;
				}
				case 2:
				{
					ColumnExport columnExport;
					if (columnExport.ColExportType == ColExportType.Binary)
					{
						num = 9;
						continue;
					}
					num = 25;
					continue;
				}
				case 3:
				{
					object obj;
					if (obj == null)
					{
						num = 18;
						continue;
					}
					num = 2;
					continue;
				}
				case 4:
					return result;
				case 5:
				{
					byte[] array;
					if (array.Length == 0)
					{
						num = 21;
						continue;
					}
					MemoryStream memoryStream = new MemoryStream();
					num = 19;
					continue;
				}
				case 6:
				{
					object obj;
					byte[] array = (base.ᜎ() as DBFExport).CurrentEncoding.GetBytes(obj.ToString());
					num = 11;
					continue;
				}
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_128;
				case 9:
				{
					object obj;
					byte[] array = obj as byte[];
					num = 29;
					continue;
				}
				case 10:
					goto IL_285;
				case 11:
					goto IL_128;
				case 12:
				{
					ColumnExport columnExport;
					IDataReader dataReader;
					int num2 = (int)dataReader.GetBytes(columnExport.Index, 0L, null, 0, int.MaxValue);
					byte[] array = new byte[num2];
					dataReader.GetBytes(columnExport.Index, 0L, array, 0, num2);
					num = 30;
					continue;
				}
				case 13:
				{
					if (spr\u2059.ᜀ.IsClosed)
					{
						num = 22;
						continue;
					}
					IDataReader dataReader = spr\u2059.ᜀ;
					num = 14;
					continue;
				}
				case 14:
				{
					ColumnExport columnExport;
					IDataReader dataReader;
					if (dataReader.IsDBNull(columnExport.Index))
					{
						num = 15;
						continue;
					}
					num = 0;
					continue;
				}
				case 15:
					return result;
				case 16:
				{
					byte[] array;
					if (array != null)
					{
						num = 7;
						continue;
					}
					return result;
				}
				case 17:
					return result;
				case 18:
					return result;
				case 19:
					goto IL_1CE;
				case 20:
				{
					if (!(base.ᜎ() is DBFExport))
					{
						num = 4;
						continue;
					}
					byte[] array = null;
					ColumnExport columnExport = this.ᜁ().ColumnsExport[A_0.ColumnIndex];
					ExportSource dataSource = (base.ᜎ() as DBFExport).DataSource;
					num = 1;
					continue;
				}
				case 21:
					goto IL_2EF;
				case 22:
					goto IL_221;
				case 23:
					if (spr\u2059.ᜀ == null)
					{
						num = 10;
						continue;
					}
					num = 13;
					continue;
				case 25:
				{
					ColumnExport columnExport;
					if (columnExport.ColExportType == ColExportType.String)
					{
						num = 6;
						continue;
					}
					goto IL_128;
				}
				case 26:
					num = 17;
					continue;
				case 27:
					goto IL_D5;
				case 28:
					goto IL_1F5;
				case 29:
					goto IL_128;
				case 30:
					goto IL_128;
				case 31:
				{
					ColumnExport columnExport;
					IDataReader dataReader;
					string @string = dataReader.GetString(columnExport.Index);
					byte[] array = (base.ᜎ() as DBFExport).CurrentEncoding.GetBytes(@string);
					num = 8;
					continue;
				}
				case 32:
				{
					if (spr\u2059.ᜂ == null)
					{
						num = 28;
						continue;
					}
					DataRow dataRow = spr\u2059.ᜂ;
					ColumnExport columnExport;
					object obj = dataRow[columnExport.Index];
					num = 3;
					continue;
				}
				case 33:
				{
					ColumnExport columnExport;
					if (columnExport.ColExportType == ColExportType.String)
					{
						num = 31;
						continue;
					}
					goto IL_128;
				}
				}
				if (this.ᜉ == null)
				{
					num = 27;
					continue;
				}
				result = -1;
				num = 20;
				continue;
				IL_128:
				num = 16;
			}
			IL_D5:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⌭㨯瘱嘳倵紷䈹䰻儽㈿㙁ፃ㑅ⅇ㹉⥋㱍橏桑͓⑕ㅗ⹙㥛፝՟ཡୣ䩥ṧ୩ṫ呭⽯άᅳ᭵᝷⡹᥻ᵽ", a_));
			IL_1CE:
			try
			{
				for (;;)
				{
					byte[] array;
					MemoryStream memoryStream;
					memoryStream.Write(array, 0, array.Length);
					byte value = 26;
					array = BitConverter.GetBytes((short)value);
					memoryStream.Write(array, 0, 1);
					memoryStream.Write(array, 0, 1);
					memoryStream.Position = 0L;
					result = this.ᜊ;
					int num3 = 0;
					num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (num3 < this.ᜉ.Length)
							{
								num = 2;
								continue;
							}
							goto IL_588;
						case 1:
							goto IL_588;
						case 2:
							Array.Clear(this.ᜉ, num3, this.ᜉ.Length - num3);
							num = 1;
							continue;
						case 3:
							if ((num3 = memoryStream.Read(this.ᜉ, 0, this.ᜉ.Length)) <= 0)
							{
								num = 6;
								continue;
							}
							num = 0;
							continue;
						case 4:
							goto IL_5BC;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5BC;
							default:
								if (false)
								{
								}
								goto IL_529;
							}
							break;
						case 6:
							num = 7;
							continue;
						case 7:
							goto IL_5CD;
						}
						break;
						IL_529:
						num = 3;
						continue;
						IL_588:
						this.ᜈ.Write(this.ᜉ, 0, this.ᜉ.Length);
						this.ᜊ++;
						num = 4;
						continue;
						IL_5BC:
						goto IL_529;
					}
				}
				IL_5CD:;
			}
			finally
			{
				MemoryStream memoryStream;
				memoryStream.Close();
			}
			return result;
			IL_1F5:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⌭㨯瘱嘳倵紷䈹䰻儽㈿㙁ፃ㑅ⅇ㹉⥋㱍橏桑͓⑕ㅗ⹙㥛፝՟ཡୣ䩥ṧ୩ṫ呭㍯ᵱᥳ᭵᝷ᑹ剻㵽ﺉ\ude8b", a_));
			IL_221:
			throw new ArgumentException(HyperlinksCollectionEditor.b("⌭㨯瘱嘳倵紷䈹䰻儽㈿㙁ፃ㑅ⅇ㹉⥋㱍橏桑͓⑕ㅗ⹙㥛፝՟ཡୣ䩥ṧ୩ṫ呭㍯ᵱᥳ᭵᝷ᑹ剻㩽풅", a_));
			IL_285:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⌭㨯瘱嘳倵紷䈹䰻儽㈿㙁ፃ㑅ⅇ㹉⥋㱍橏桑͓⑕ㅗ⹙㥛፝՟ཡୣ䩥ṧ୩ṫ呭㍯ᵱᥳ᭵᝷ᑹ剻㩽풅", a_));
			IL_2EF:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0002B950 File Offset: 0x0002A950
	protected DBFExport ᜁ()
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
		return base.ᜎ() as DBFExport;
	}

	// Token: 0x040002A0 RID: 672
	private new const byte ᜀ = 32;

	// Token: 0x040002A1 RID: 673
	private new const string ᜁ = "TRUE";

	// Token: 0x040002A2 RID: 674
	private new const string ᜂ = "FALSE";

	// Token: 0x040002A3 RID: 675
	private const char ᜃ = 'T';

	// Token: 0x040002A4 RID: 676
	private const char ᜄ = 'F';

	// Token: 0x040002A5 RID: 677
	private bool ᜅ;

	// Token: 0x040002A6 RID: 678
	private new spr\u205B ᜆ = new spr\u205B();

	// Token: 0x040002A7 RID: 679
	private new ArrayList ᜇ = new ArrayList();

	// Token: 0x040002A8 RID: 680
	private FileStream ᜈ;

	// Token: 0x040002A9 RID: 681
	private byte[] ᜉ;

	// Token: 0x040002AA RID: 682
	private int ᜊ;

	// Token: 0x040002AB RID: 683
	private bool ᜋ;
}

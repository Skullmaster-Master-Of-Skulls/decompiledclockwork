using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.DBF
{
	// Token: 0x02000230 RID: 560
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class DBFExport : TextExport
	{
		// Token: 0x060010B0 RID: 4272 RVA: 0x000B4804 File Offset: 0x000B3804
		protected override void InitializeVariables()
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜀ);
			base.InitializeVariables();
			this.AutoFitColWidth = false;
			base.DataFormats.NullString = HyperlinksCollectionEditor.b("䰡儣䨥䐧", a_);
			this.m_currEncoding = new ASCIIEncoding();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000B4894 File Offset: 0x000B3894
		protected override void Dispose(bool disposing)
		{
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_72;
					case 2:
						this.ᜀ.Dispose();
						this.ᜀ = null;
						num = 0;
						continue;
					case 3:
						goto IL_7A;
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
						if (this.ᜀ != null)
						{
							num = 2;
							continue;
						}
						break;
					}
					IL_72:
					num = 3;
				}
				IL_7A:;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000B4940 File Offset: 0x000B3940
		public override void SaveToFile()
		{
			for (;;)
			{
				spr\u2561.ᜀ = this.ᜁ;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						AboutDataExport.ShowAbout(false);
						num = 1;
						continue;
					case 1:
						goto IL_50;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_87;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 3:
						if (this.ᜁ)
						{
							num = 2;
							continue;
						}
						goto IL_9C;
					case 4:
						if (Environment.UserInteractive)
						{
							goto IL_87;
						}
						goto IL_9C;
					}
					break;
					IL_87:
					if (true)
					{
					}
					num = 0;
				}
			}
			IL_50:
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000B49F0 File Offset: 0x000B39F0
		public override void SaveToStream(Stream Stream)
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
			spr\u2561.ᜀ = this.ᜁ;
			base.SaveToStream(Stream);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000B4A40 File Offset: 0x000B3A40
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 9;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䐤圦夨䜪䐬䰮倰䜲尴堶圸ᐺ夼崾❀", a_), response, saveType);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)memoryStream).Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_A2;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 0;
				}
				IL_A2:;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000B4B04 File Offset: 0x000B3B04
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 16;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䴫席䀯帱崳唵夷丹唻儽⸿流⁃⑅⹇", a_), response, SaveType.Attachment);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_A2;
					case 2:
						((IDisposable)memoryStream).Dispose();
						num = 1;
						continue;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 2;
				}
				IL_A2:;
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000B4BC8 File Offset: 0x000B3BC8
		public void SaveToHttpResponse(HttpResponse response)
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
			spr\u2561.ᜀ = this.ᜁ;
			this.SaveToHttpResponse(this.FileName, response);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000B4C1C File Offset: 0x000B3C1C
		private string ᜀ(int A_0, int A_1)
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_72;
				case 1:
					A_0 = 15;
					A_1 = 4;
					num = 0;
					continue;
				}
				if (true)
				{
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
					if (A_1 + 1 < A_0)
					{
						goto IL_74;
					}
					num = 1;
					break;
				}
			}
			IL_72:
			IL_74:
			return string.Format(base.Culture, HyperlinksCollectionEditor.b("帤尦ᤨܪ嘬Ἦ䰰ल匴䰶࠸䘺䀼䈾", a_), new object[]
			{
				A_0,
				A_1
			});
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000B4CD0 File Offset: 0x000B3CD0
		private void ᜀ(string A_0, ref int A_1, ref int A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					A_1 = this.ᜃ;
					A_2 = this.ᜄ;
					int num = this.ᜁ.IndexOfName(A_0);
					int num2 = 9;
					for (;;)
					{
						string valueByIndex;
						string[] array;
						switch (num2)
						{
						case 0:
							if (A_2 <= 0)
							{
								num2 = 3;
								continue;
							}
							return;
						case 1:
							if (A_1 <= 0)
							{
								num2 = 5;
								continue;
							}
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_121;
							default:
								goto IL_17C;
							}
							break;
						case 3:
							A_1 = this.ᜃ;
							A_2 = this.ᜄ;
							if (true)
							{
							}
							num2 = 2;
							continue;
						case 4:
							valueByIndex = this.ᜁ.GetValueByIndex(num);
							num = valueByIndex.IndexOf(',');
							goto IL_121;
						case 5:
							num2 = 0;
							continue;
						case 6:
							try
							{
								A_1 = int.Parse(array[0]);
								goto IL_9D;
							}
							catch
							{
								A_1 = 0;
								goto IL_9D;
							}
							return;
							try
							{
								IL_9D:
								A_2 = int.Parse(array[1]);
								goto IL_7E;
							}
							catch
							{
								A_2 = 0;
								goto IL_7E;
							}
							goto IL_AF;
							IL_7E:
							num2 = 1;
							continue;
						case 7:
							if (num > -1)
							{
								num2 = 8;
								continue;
							}
							return;
						case 8:
							goto IL_AF;
						case 9:
							if (num > -1)
							{
								num2 = 4;
								continue;
							}
							return;
						case 10:
							if (array.Length == 2)
							{
								num2 = 6;
								continue;
							}
							return;
						}
						break;
						IL_AF:
						array = valueByIndex.Split(new char[]
						{
							','
						});
						num2 = 10;
						continue;
						IL_121:
						num2 = 7;
					}
				}
				IL_17C:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000B4EA0 File Offset: 0x000B3EA0
		internal new spr\u2603 ᜀ()
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
			return base.ᜀ() as spr\u2603;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000B4EE8 File Offset: 0x000B3EE8
		protected override Type GetWriterType()
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
			return typeof(spr\u2603);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000B4F30 File Offset: 0x000B3F30
		protected override void BeginDataExport()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				IL_3C:
				base.BeginDataExport();
				int num = 0;
				spr\u2537 spr_u = new spr\u2537();
				IEnumerator enumerator = base.ColumnsExport.GetEnumerator();
				try
				{
					int num2 = 5;
					for (;;)
					{
						sprỚ sprỚ;
						switch (num2)
						{
						case 0:
						{
							ColumnExport columnExport;
							if (columnExport.IsMemo)
							{
								num2 = 2;
								continue;
							}
							ColExportType colExportType = columnExport.ColExportType;
							num2 = 7;
							continue;
						}
						case 1:
							goto IL_1FF;
						case 2:
							goto IL_1D7;
						case 3:
							goto IL_1FF;
						case 4:
							goto IL_1FF;
						case 6:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 19;
								continue;
							}
							ColumnExport columnExport = (ColumnExport)enumerator.Current;
							sprỚ = new sprỚ();
							sprỚ.ᜂ();
							string text = columnExport.Name;
							text = spr_u.ᜀ(text);
							byte[] bytes = Encoding.ASCII.GetBytes(text.ToUpper());
							Array.Copy(bytes, 0, sprỚ.ᜀ, 0, bytes.Length);
							sprỚ.ᜁ = 0;
							num2 = 12;
							continue;
						}
						case 7:
						{
							ColExportType colExportType;
							switch (colExportType)
							{
							case ColExportType.Integer:
								sprỚ.ᜂ = 78;
								sprỚ.ᜄ = 11;
								sprỚ.ᜅ = 0;
								num2 = 14;
								continue;
							case ColExportType.Bigint:
								sprỚ.ᜂ = 78;
								sprỚ.ᜄ = 20;
								sprỚ.ᜅ = 0;
								num2 = 16;
								continue;
							case ColExportType.Float:
							case ColExportType.Currency:
							{
								int num3 = this.ᜃ;
								int num4 = this.ᜄ;
								ColumnExport columnExport;
								this.ᜀ(columnExport.Name, ref num3, ref num4);
								sprỚ.ᜂ = 78;
								sprỚ.ᜄ = (byte)num3;
								sprỚ.ᜅ = (byte)num4;
								num2 = 18;
								continue;
							}
							case ColExportType.DateTime:
							case ColExportType.Time:
								sprỚ.ᜂ = 68;
								sprỚ.ᜄ = 8;
								sprỚ.ᜅ = 0;
								num2 = 4;
								continue;
							case ColExportType.String:
							case ColExportType.Guid:
							{
								sprỚ.ᜂ = 67;
								ColumnExport columnExport;
								sprỚ.ᜄ = (byte)columnExport.Length;
								sprỚ.ᜅ = 0;
								num2 = 1;
								continue;
							}
							case ColExportType.Boolean:
								sprỚ.ᜂ = 76;
								sprỚ.ᜄ = 1;
								sprỚ.ᜅ = 0;
								num2 = 15;
								continue;
							default:
								num2 = 8;
								continue;
							}
							break;
						}
						case 8:
							num2 = 11;
							continue;
						case 10:
							num2 = 0;
							continue;
						case 11:
							sprỚ.ᜂ = 67;
							sprỚ.ᜄ = 10;
							sprỚ.ᜅ = 0;
							num2 = 3;
							continue;
						case 12:
						{
							ColumnExport columnExport;
							if (columnExport.ColExportType != ColExportType.Binary)
							{
								num2 = 10;
								continue;
							}
							goto IL_1D7;
						}
						case 13:
							goto IL_1FF;
						case 14:
							goto IL_1FF;
						case 15:
							goto IL_1FF;
						case 16:
							goto IL_1FF;
						case 17:
							goto IL_38A;
						case 18:
							goto IL_1FF;
						case 19:
							num2 = 17;
							continue;
						}
						IL_181:
						num2 = 6;
						continue;
						goto IL_181;
						IL_1D7:
						sprỚ.ᜂ = 77;
						sprỚ.ᜄ = 10;
						sprỚ.ᜅ = 0;
						num2 = 13;
						continue;
						IL_1FF:
						sprỚ.ᜃ = num;
						num += (int)sprỚ.ᜄ;
						this.ᜀ().ᜀ(sprỚ);
						num2 = 9;
					}
					IL_38A:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								disposable.Dispose();
								num2 = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num2 = 0;
									continue;
								}
								goto IL_3D4;
							case 2:
								goto IL_3D2;
							}
							break;
						}
					}
					IL_3D2:
					IL_3D4:;
				}
				this.ᜀ().ᜀ();
				return;
			}
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000B533C File Offset: 0x000B433C
		protected override void EndDataExport()
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
			this.ᜀ().\u170D().Write(new byte[]
			{
				26
			}, 0, 1);
			this.ᜀ().ᜂ();
			base.EndDataExport();
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000B53A8 File Offset: 0x000B43A8
		protected override void BeforeExport()
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.BeforeExport();
			this.ᜂ = base.Culture.NumberFormat.NumberDecimalSeparator;
			base.Culture.NumberFormat.NumberDecimalSeparator = HyperlinksCollectionEditor.b("ȫ", a_);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000B5428 File Offset: 0x000B4428
		protected override void AfterExport()
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
			base.Culture.NumberFormat.NumberDecimalSeparator = this.ᜂ;
			base.AfterExport();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000B5480 File Offset: 0x000B4480
		protected override void WriteBlankRow()
		{
			for (;;)
			{
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_2E;
					case 2:
						goto IL_2E;
					case 3:
						if (num >= base.ExportRowExport.Count)
						{
							num2 = 0;
							continue;
						}
						this.ᜀ().ᜀ(num, string.Empty);
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					}
					break;
					IL_2E:
					num2 = 3;
				}
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000B5524 File Offset: 0x000B4524
		protected override void WriteRow()
		{
			int a_ = 5;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 28;
					for (;;)
					{
						string text;
						switch (num2)
						{
						case 0:
						{
							int num3;
							if (num3 > -1)
							{
								num2 = 23;
								continue;
							}
							goto IL_144;
						}
						case 1:
							goto IL_1BB;
						case 2:
							goto IL_1BB;
						case 3:
							num2 = 26;
							continue;
						case 4:
						{
							StringBuilder stringBuilder;
							if (stringBuilder.Length >= 10)
							{
								num2 = 19;
								continue;
							}
							stringBuilder.Insert(0, ' ');
							if (true)
							{
							}
							num2 = 5;
							continue;
						}
						case 5:
							goto IL_B0;
						case 6:
							goto IL_1BB;
						case 7:
						{
							ColumnExport columnExport;
							if (columnExport.ColExportType == ColExportType.Currency)
							{
								num2 = 24;
								continue;
							}
							num2 = 20;
							continue;
						}
						case 8:
						{
							ColumnExport columnExport;
							if (columnExport.ColExportType != ColExportType.Float)
							{
								num2 = 25;
								continue;
							}
							goto IL_1FF;
						}
						case 9:
						{
							ColExport colExport;
							int num3 = this.ᜀ().ᜀ(colExport);
							text = HyperlinksCollectionEditor.b("Ġ̢Ԥܦनପബ༮ᄰጲ", a_);
							num2 = 0;
							continue;
						}
						case 10:
							goto IL_144;
						case 11:
							goto IL_1BB;
						case 12:
							if (string.Compare(text, HyperlinksCollectionEditor.b("传嘢䤤䬦", a_), true) == 0)
							{
								num2 = 17;
								continue;
							}
							goto IL_174;
						case 13:
							goto IL_B0;
						case 14:
							text = string.Empty;
							this.ᜀ().ᜀ(num, text);
							num2 = 1;
							continue;
						case 15:
						{
							if (text.Length == 0)
							{
								num2 = 14;
								continue;
							}
							int a_2 = this.ᜃ;
							int a_3 = this.ᜄ;
							this.ᜀ(base.ExportRowExport[num].Name, ref a_2, ref a_3);
							string format = this.ᜀ(a_2, a_3);
							double num4 = double.Parse(text, NumberStyles.Any);
							text = string.Format(base.Culture, format, new object[]
							{
								num4
							});
							this.ᜀ().ᜀ(num, text);
							num2 = 6;
							continue;
						}
						case 16:
							IL_3B6:
							goto IL_174;
						case 17:
							text = string.Empty;
							num2 = 16;
							continue;
						case 18:
							goto IL_1D0;
						case 19:
						{
							StringBuilder stringBuilder;
							text = stringBuilder.ToString();
							num2 = 10;
							continue;
						}
						case 20:
						{
							ColumnExport columnExport;
							if (columnExport.ColExportType != ColExportType.Binary)
							{
								num2 = 3;
								continue;
							}
							goto IL_1D0;
						}
						case 21:
						{
							if (num >= base.ExportRowExport.Count)
							{
								num2 = 29;
								continue;
							}
							ColExport colExport = base.ExportRowExport[num];
							ColumnExport columnExport = base.ColumnsExport[colExport.ColumnIndex];
							text = base.GetColumnValue(colExport);
							num2 = 12;
							continue;
						}
						case 22:
							goto IL_260;
						case 23:
						{
							int num3;
							text = num3.ToString();
							StringBuilder stringBuilder = new StringBuilder(text);
							num2 = 13;
							continue;
						}
						case 24:
							goto IL_1FF;
						case 25:
							num2 = 7;
							continue;
						case 26:
						{
							ColumnExport columnExport;
							if (columnExport.IsMemo)
							{
								num2 = 18;
								continue;
							}
							this.ᜀ().ᜀ(num, text);
							num2 = 2;
							continue;
						}
						case 27:
							if (this.ᜀ().\u170D() is FileStream)
							{
								num2 = 9;
								continue;
							}
							goto IL_1BB;
						case 28:
							goto IL_260;
						case 29:
							return;
						}
						break;
						IL_B0:
						num2 = 4;
						continue;
						IL_144:
						byte[] bytes = base.CurrentEncoding.GetBytes(text);
						this.ᜀ().\u170D().Write(bytes, 0, 10);
						num2 = 11;
						continue;
						IL_174:
						num2 = 8;
						continue;
						IL_1FF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B6;
						default:
							if (false)
							{
							}
							num2 = 15;
							continue;
						}
						IL_1BB:
						num++;
						num2 = 22;
						continue;
						IL_1D0:
						num2 = 27;
						continue;
						IL_260:
						num2 = 21;
					}
				}
				return;
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000B5978 File Offset: 0x000B4978
		protected override void SaveProperties(XMLFile File)
		{
			int a_ = 0;
			for (;;)
			{
				base.SaveProperties(File);
				File.RemoveSection(HyperlinksCollectionEditor.b("倛嬝渟攡瀣渥", a_));
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= this.ColumnsLength.Count)
						{
							num2 = 1;
							continue;
						}
						File.WriteValue(HyperlinksCollectionEditor.b("倛嬝渟攡瀣渥", a_), string.Format(HyperlinksCollectionEditor.b("望⸝崟夡ᔣ嬥", a_), HyperlinksCollectionEditor.b("瀛眝丟䜡", a_), num), this.ColumnsLength[num]);
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 1:
						goto IL_70;
					case 2:
						goto IL_52;
					case 3:
						goto IL_52;
					}
					break;
					IL_52:
					num2 = 0;
				}
			}
			IL_70:
			if (true)
			{
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000B5A78 File Offset: 0x000B4A78
		protected override void LoadProperties(XMLFile File)
		{
			int a_ = 9;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						base.LoadProperties(File);
						Array array = null;
						this.ColumnsLength.Clear();
						File.ReadValues(HyperlinksCollectionEditor.b("椤戦木氪礬朮", a_), ref array);
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return;
							case 1:
								goto IL_91;
							case 2:
							{
								int num2;
								if (num2 >= this.ColumnsLength.Count)
								{
									num = 0;
									continue;
								}
								StringListCollection columnsLength;
								int index;
								(columnsLength = this.ColumnsLength)[index = num2] = columnsLength[index] + '=' + File.ReadValue(HyperlinksCollectionEditor.b("椤戦木氪礬朮", a_), this.ColumnsLength[num2], string.Empty);
								num2++;
								num = 5;
								continue;
							}
							case 3:
							{
								this.ColumnsLength.SetStrings(array as string[]);
								int num2 = 0;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_09;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							}
							case 4:
								if (array != null)
								{
									num = 3;
									continue;
								}
								return;
							case 5:
								goto IL_91;
							}
							break;
							IL_91:
							num = 2;
						}
					}
					break;
				}
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000B5BDC File Offset: 0x000B4BDC
		internal string DbtFileName
		{
			get
			{
				int a_ = 19;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return Path.ChangeExtension(this.FileName, HyperlinksCollectionEditor.b("Į唰儲䄴", a_));
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x000B5C3C File Offset: 0x000B4C3C
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x000B5C80 File Offset: 0x000B4C80
		[Description("Determines the column precisions in the exported file.")]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StringListCollection ColumnsPrecision
		{
			get
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
				return this.ᜁ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					IL_12:
					switch (num)
					{
					case 0:
						this.ᜁ = value;
						num = 2;
						continue;
					case 1:
						num = 3;
						continue;
					case 2:
						return;
					case 3:
						while (value != this.ᜁ)
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
								num = 0;
								goto IL_12;
							}
						}
						return;
					case 4:
						if (true)
						{
						}
						break;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x000B5D18 File Offset: 0x000B4D18
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x000B5D5C File Offset: 0x000B4D5C
		[DefaultValue(15)]
		[Description("Determines the default size of float columns in the exported file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int DefaultFloatSize
		{
			get
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
			set
			{
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
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_5A;
					case 2:
						this.ᜃ = value;
						num = 1;
						continue;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					num = 2;
				}
				IL_5A:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000B5DD8 File Offset: 0x000B4DD8
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x000B5E1C File Offset: 0x000B4E1C
		[DefaultValue(4)]
		[Description("Determines the fractional part size of flat columns in the exported file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int DefaultFloatDecimal
		{
			get
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
				return this.ᜄ;
			}
			set
			{
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
					if (true)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.ᜄ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x000B5E98 File Offset: 0x000B4E98
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x000B5EDC File Offset: 0x000B4EDC
		[Description("Determines the type encoding of the result file.")]
		[DefaultValue(DbfEncodingType.ASCII)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new DbfEncodingType DataEncoding
		{
			get
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
				return this.ᜅ;
			}
			set
			{
				for (;;)
				{
					this.ᜅ = value;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (value)
							{
							case DbfEncodingType.ASCII:
								goto IL_67;
							case DbfEncodingType.OEM:
								goto IL_4B;
							case DbfEncodingType.UTF8:
								goto IL_A1;
							default:
								num = 1;
								continue;
							}
							break;
						case 1:
							num = 2;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_91;
							}
							break;
						}
						break;
					}
				}
				IL_4B:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.OEMCodePage);
				return;
				IL_67:
				this.m_currEncoding = new ASCIIEncoding();
				return;
				IL_91:
				if (false)
				{
				}
				if (true)
				{
				}
				this.m_currEncoding = new ASCIIEncoding();
				return;
				IL_A1:
				this.m_currEncoding = new UTF8Encoding();
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000B5FA4 File Offset: 0x000B4FA4
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x000B5FE8 File Offset: 0x000B4FE8
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new StringListCollection ColumnsLength
		{
			get
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
				return base.ColumnsLength;
			}
			set
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
				base.ColumnsLength = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x000B602C File Offset: 0x000B502C
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x000B6070 File Offset: 0x000B5070
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection NotTruncatableColumns
		{
			get
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
				return base.NotTruncatableColumns;
			}
			set
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
				base.NotTruncatableColumns = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x000B60B4 File Offset: 0x000B50B4
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x000B60F8 File Offset: 0x000B50F8
		[DefaultValue("")]
		[Editor(typeof(DBFFileNameEditor), typeof(UITypeEditor))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string FileName
		{
			get
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
				return base.FileName;
			}
			set
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
				base.FileName = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000B613C File Offset: 0x000B513C
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x000B6180 File Offset: 0x000B5180
		[Description("If this property is true, then length of each column in the result file is set automatically depending on the maximum number of symbols in the column cells.")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool AutoFitColWidth
		{
			get
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
				return base.AutoFitColWidth;
			}
			set
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
				base.AutoFitColWidth = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000B61C4 File Offset: 0x000B51C4
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x000B6200 File Offset: 0x000B5200
		protected override bool ConvertBinaryToHexString
		{
			get
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
				return false;
			}
			set
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
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000B623C File Offset: 0x000B523C
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x000B6280 File Offset: 0x000B5280
		[DefaultValue(false)]
		[Description("Indicate whether export long char/binary column.")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool ExportLongColumn
		{
			get
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
				return base.ExportLongColumn;
			}
			set
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
				base.ExportLongColumn = value;
			}
		}

		// Token: 0x04000C08 RID: 3080
		private string \u2460\u00A9\u008B\u0085;

		// Token: 0x04000C09 RID: 3081
		private new License ᜀ;

		// Token: 0x04000C0A RID: 3082
		private new StringListCollection ᜁ = new StringListCollection();

		// Token: 0x04000C0B RID: 3083
		private new string ᜂ = string.Empty;

		// Token: 0x04000C0C RID: 3084
		private int[] \u2593\u0081\u00A8\u009E;

		// Token: 0x04000C0D RID: 3085
		private string \u2593\u008B\u0094\u00A3;

		// Token: 0x04000C0E RID: 3086
		private int[] \u25D9\u0093\u00AC\u007F;

		// Token: 0x04000C0F RID: 3087
		private new int ᜃ = 15;

		// Token: 0x04000C10 RID: 3088
		private long[] \u2460\u008Aª\u00A9;

		// Token: 0x04000C11 RID: 3089
		private bool \u25D9\u008A\u009E\u009D;

		// Token: 0x04000C12 RID: 3090
		private new int ᜄ = 4;

		// Token: 0x04000C13 RID: 3091
		private DbfEncodingType ᜅ;
	}
}

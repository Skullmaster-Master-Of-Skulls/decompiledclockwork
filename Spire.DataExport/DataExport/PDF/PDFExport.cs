using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.EventArgs;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.PDF
{
	// Token: 0x0200022C RID: 556
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class PDFExport : FormatTextExport
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x000B0170 File Offset: 0x000AF170
		protected override void InitializeVariables()
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
			this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜀ);
			base.InitializeVariables();
			this.AutoFitColWidth = true;
			this.m_currEncoding = new ASCIIEncoding();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000B01E0 File Offset: 0x000AF1E0
		protected override void Dispose(bool disposing)
		{
			try
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
						goto IL_72;
					case 1:
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
						goto IL_7A;
					default:
						if (false)
						{
						}
						if (this.ᜀ != null)
						{
							num = 1;
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

		// Token: 0x06001060 RID: 4192 RVA: 0x000B028C File Offset: 0x000AF28C
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
						goto IL_76;
					case 2:
						if (Environment.UserInteractive)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_9C;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							if (this.ᜁ)
							{
								num = 4;
								continue;
							}
							goto IL_9C;
						}
						break;
					case 4:
						goto IL_78;
					}
					break;
					IL_78:
					num = 2;
				}
			}
			IL_76:
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000B033C File Offset: 0x000AF33C
		public void SaveToFile(string fileName)
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
			this.FileName = fileName;
			this.SaveToFile();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x000B0384 File Offset: 0x000AF384
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

		// Token: 0x06001063 RID: 4195 RVA: 0x000B03D4 File Offset: 0x000AF3D4
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 11;
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
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䘦夨嬪䄬䘮到刲䄴帶嘸唺ሼ伾╀╂", a_), response, saveType);
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

		// Token: 0x06001064 RID: 4196 RVA: 0x000B0498 File Offset: 0x000AF498
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 3;
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
				spr\u2561.ᜀ = this.ᜁ;
				MemoryStream memoryStream = new MemoryStream();
				try
				{
					if (true)
					{
					}
					base.SaveToStream(memoryStream);
					base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("縞儠匢䤤並䨨䨪夬䘮帰崲ᨴ䜶崸崺", a_), response, SaveType.Attachment);
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A2;
						case 1:
							((IDisposable)memoryStream).Dispose();
							num = 0;
							continue;
						}
						if (memoryStream == null)
						{
							break;
						}
						num = 1;
					}
					IL_A2:;
				}
				break;
			}
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000B055C File Offset: 0x000AF55C
		public void SaveToHttpResponse(HttpResponse response)
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
			spr\u2561.ᜀ = this.ᜁ;
			this.SaveToHttpResponse(this.FileName, response);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000B05B0 File Offset: 0x000AF5B0
		private void ᜀ(int A_0, int A_1)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						if (num > base.ColumnsExport.Count)
						{
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							this.ᜀ().ᜀ().ᜀ((double)((int)this.ᜂ[num]), (double)((float)A_0 + (float)this.ᜁ.GridLineWidth / 2f), (double)((int)this.ᜂ[num]), (double)((float)A_1 - (float)this.ᜁ.GridLineWidth / 2f), this.ᜁ.GridLineColor);
							num++;
							num2 = 2;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						goto IL_36;
					case 3:
						goto IL_36;
					}
					break;
					IL_36:
					num2 = 0;
				}
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000B06B4 File Offset: 0x000AF6B4
		protected override void BeginDataExport()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				base.BeginDataExport();
				this.ᜁ.HeaderFont.CalcFontWidth();
				this.ᜁ.FooterFont.CalcFontWidth();
				this.ᜁ.TitleFont.CalcFontWidth();
				this.ᜁ.DataFont.CalcFontWidth();
				this.ᜃ = this.ᜁ.PageOptions.TrimBox.ᜁ();
				this.ᜄ = this.ᜃ;
				this.ᜂ.Clear();
				this.ᜂ.Add(this.ᜁ.PageOptions.TrimBox.ᜃ());
				IEnumerator enumerator = base.ColumnsExport.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 6;
							continue;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							ColumnExport columnExport = (ColumnExport)enumerator.Current;
							num = 5;
							continue;
						}
						case 4:
						{
							ColumnExport columnExport;
							this.ᜂ.Add((int)this.ᜂ[this.ᜂ.Count - 1] + sprᤓ.ᜀ((double)columnExport.Width + this.ᜁ.ColSpacing, this.ᜁ.DataFont));
							num = 7;
							continue;
						}
						case 5:
						{
							if (this.ᜁ.DataFont.AllowCustomFont)
							{
								num = 4;
								continue;
							}
							ColumnExport columnExport;
							this.ᜂ.Add((int)this.ᜂ[this.ᜂ.Count - 1] + sprᤓ.ᜀ((double)columnExport.Width + this.ᜁ.ColSpacing, this.ᜁ.DataFont.Size, (int)this.ᜁ.DataFont.PdfFontName));
							num = 2;
							continue;
						}
						case 6:
							goto IL_243;
						}
						IL_142:
						num = 1;
						continue;
						goto IL_142;
					}
					IL_243:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 0;
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
								goto IL_285;
							case 1:
								disposable.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_283;
							}
							break;
						}
					}
					IL_283:
					IL_285:;
				}
				break;
			}
			}
			this.ᜀ().ᜀ().ᜁ(this.ᜁ.PageOptions.MediaBox.ᜂ() as sprᶆ);
			this.ᜀ().ᜀ().ᜀ(this.ᜁ.PageOptions.TrimBox.ᜂ() as sprᶆ);
			this.ᜀ().ᜀ().ᜇ();
			this.ᜀ().ᜀ().ᜈ();
			this.ᜀ().ᜀ().ᜁ((double)this.ᜁ.GridLineWidth);
			this.ᜀ().ᜀ().ᜀ(this.ᜁ.RowSpacing);
			this.ᜀ().ᜀ().ᜁ(this.ᜁ.GridLineColor);
			this.WriteHeader();
			this.ᜀ().ᜀ().ᜂ(this.ᜁ.DataFont);
			this.ᜀ().ᜀ().ᜂ(this.ᜁ.TitleFont);
			this.ᜀ().ᜀ().ᜂ(this.ᜁ.HeaderFont);
			this.ᜀ().ᜀ().ᜂ(this.ᜁ.FooterFont);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000B0A9C File Offset: 0x000AFA9C
		protected override void WriteTitleRow()
		{
			switch (0)
			{
			default:
			{
				int num = 21;
				for (;;)
				{
					int num2;
					int num4;
					int num7;
					switch (num)
					{
					case 0:
						goto IL_4EA;
					case 1:
						if (num2 >= base.Columns.Count)
						{
							num = 23;
							continue;
						}
						num = 3;
						continue;
					case 2:
						goto IL_791;
					case 3:
						if (this.ᜁ.TitleFont.AllowCustomFont)
						{
							num = 14;
							continue;
						}
						this.ᜀ().ᜀ().ᜀ((int)this.ᜂ[num2] + sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.TitleFont.Size, (int)this.ᜁ.TitleFont.PdfFontName), this.ᜃ, base.GetColumnTitle(num2), this.ᜁ.TitleFont.Color);
						num = 11;
						continue;
					case 4:
						goto IL_407;
					case 5:
						goto IL_303;
					case 6:
						this.ᜃ -= (int)((double)this.ᜁ.TitleFont.CustomFont.Size * this.ᜁ.RowSpacing / 2.0);
						num = 5;
						continue;
					case 7:
						goto IL_4EA;
					case 8:
						if (this.ᜁ.TitleFont.AllowCustomFont)
						{
							num = 6;
							continue;
						}
						this.ᜃ -= (int)((double)this.ᜁ.TitleFont.Size * this.ᜁ.RowSpacing / 2.0);
						num = 4;
						continue;
					case 9:
						num = 29;
						continue;
					case 10:
					{
						if (this.ᜁ.TitleFont.AllowCustomFont)
						{
							num = 31;
							continue;
						}
						int num3 = sprᤓ.ᜀ((double)base.GetColumnTitle(num4).Length + this.ᜁ.ColSpacing * 2.0, this.ᜁ.TitleFont.Size, (int)this.ᜁ.TitleFont.PdfFontName);
						num = 0;
						continue;
					}
					case 11:
						goto IL_1BA;
					case 12:
					{
						IEnumerator enumerator = base.ColumnsExport.GetEnumerator();
						num = 30;
						continue;
					}
					case 13:
					{
						int num3;
						int num5 = (int)this.ᜂ[num4] + num3 - (int)this.ᜂ[num4 + 1];
						int num6 = num4 + 1;
						num = 19;
						continue;
					}
					case 14:
						this.ᜀ().ᜀ().ᜀ((int)this.ᜂ[num2] + sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.TitleFont), this.ᜃ, base.GetColumnTitle(num2), this.ᜁ.TitleFont.CustomFontColor);
						num = 18;
						continue;
					case 15:
						this.ᜃ -= (int)((double)this.ᜁ.TitleFont.CustomFont.Size * this.ᜁ.RowSpacing / 2.0);
						num = 33;
						continue;
					case 16:
						this.ᜀ().ᜀ().ᜀ((double)((int)this.ᜂ[0]), (double)num7, (double)((int)this.ᜂ[this.ᜂ.Count - 1]), (double)num7, this.ᜁ.GridLineColor);
						num = 32;
						continue;
					case 17:
						goto IL_397;
					case 18:
						goto IL_1BA;
					case 19:
						goto IL_635;
					case 20:
					{
						int num6;
						if (num6 >= this.ᜂ.Count)
						{
							num = 2;
							continue;
						}
						int num5;
						this.ᜂ[num6] = (int)this.ᜂ[num6] + num5;
						num6++;
						num = 34;
						continue;
					}
					case 22:
					{
						if (num4 >= base.Columns.Count)
						{
							num = 16;
							continue;
						}
						int num3 = 0;
						int num5 = 0;
						num = 10;
						continue;
					}
					case 23:
						num = 8;
						continue;
					case 24:
						goto IL_397;
					case 25:
						goto IL_308;
					case 26:
						goto IL_297;
					case 27:
					{
						int num3;
						if ((int)this.ᜂ[num4 + 1] < (int)this.ᜂ[num4] + num3)
						{
							num = 13;
							continue;
						}
						goto IL_791;
					}
					case 28:
						goto IL_308;
					case 29:
						if (base.ColumnsExport.Count > 0)
						{
							goto IL_465;
						}
						goto IL_476;
					case 30:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 4;
										continue;
									}
									ColumnExport columnExport = (ColumnExport)enumerator.Current;
									base.Columns.Add(columnExport.Caption);
									num = 1;
									continue;
								}
								case 3:
									goto IL_249;
								case 4:
									num = 3;
									continue;
								}
								IL_223:
								num = 0;
								continue;
								goto IL_223;
							}
							IL_249:
							goto IL_476;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_296;
									case 2:
										goto IL_294;
									}
									break;
								}
							}
							IL_294:
							IL_296:;
						}
						goto IL_297;
					case 31:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_465;
						default:
						{
							if (false)
							{
							}
							int num3 = sprᤓ.ᜀ((double)base.GetColumnTitle(num4).Length + this.ᜁ.ColSpacing * 2.0, this.ᜁ.TitleFont);
							num = 7;
							continue;
						}
						}
						break;
					case 32:
						if (this.ᜁ.TitleFont.AllowCustomFont)
						{
							num = 15;
							continue;
						}
						this.ᜃ -= (int)((double)this.ᜁ.TitleFont.Size * this.ᜁ.RowSpacing / 2.0);
						num = 26;
						continue;
					case 33:
						goto IL_297;
					case 34:
						goto IL_635;
					}
					if (base.Columns.Count == 0)
					{
						num = 9;
						continue;
					}
					goto IL_476;
					IL_1BA:
					num2++;
					num = 17;
					continue;
					IL_297:
					if (true)
					{
					}
					num2 = 0;
					num = 24;
					continue;
					IL_308:
					num = 22;
					continue;
					IL_397:
					num = 1;
					continue;
					IL_465:
					num = 12;
					continue;
					IL_476:
					this.ᜀ().ᜀ().ᜁ(this.ᜁ.TitleFont);
					num7 = this.ᜃ;
					this.ᜃ -= (int)((double)this.ᜁ.TitleFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num4 = 0;
					num = 25;
					continue;
					IL_4EA:
					num = 27;
					continue;
					IL_635:
					num = 20;
					continue;
					IL_791:
					num4++;
					num = 28;
				}
				IL_303:
				IL_407:
				this.ᜀ().ᜀ().ᜀ((double)((int)this.ᜂ[0]), (double)this.ᜃ, (double)((int)this.ᜂ[this.ᜂ.Count - 1]), (double)this.ᜃ, this.ᜁ.GridLineColor);
				return;
			}
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000B1300 File Offset: 0x000B0300
		protected override void WriteBlankRow()
		{
			for (;;)
			{
				for (;;)
				{
					this.ᜃ -= (int)((double)this.ᜁ.TitleFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							goto IL_75;
						case 1:
							if (num >= base.ColumnsExport.Count)
							{
								num2 = 3;
								continue;
							}
							this.ᜀ().ᜀ().ᜀ((int)this.ᜂ[num] + sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.TitleFont), this.ᜃ, string.Empty, this.ᜁ.TitleFont.Color);
							num++;
							num2 = 2;
							continue;
						case 2:
							goto IL_75;
						case 3:
							goto IL_96;
						}
						break;
						IL_75:
						num2 = 1;
					}
				}
				IL_96:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_124;
				}
			}
			IL_124:
			if (false)
			{
			}
			this.ᜃ -= (int)((double)this.ᜁ.TitleFont.Size * this.ᜁ.RowSpacing / 2.0);
			this.ᜀ().ᜀ().ᜀ((double)((int)this.ᜂ[0]), (double)this.ᜃ, (double)((int)this.ᜂ[this.ᜂ.Count - 1]), (double)this.ᜃ, this.ᜁ.GridLineColor);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000B14C8 File Offset: 0x000B04C8
		protected override void WriteRow()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ().ᜀ().ᜁ(this.ᜁ.DataFont);
					bool flag = false;
					int num = 2;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (this.ᜁ.DataFont.AllowCustomFont)
							{
								num = 14;
								continue;
							}
							this.ᜃ -= (int)((double)this.ᜁ.DataFont.Size * this.ᜁ.RowSpacing / 2.0);
							num = 19;
							continue;
						case 1:
							this.ᜀ().ᜀ().ᜀ(this.ᜁ.DataFont.CustomFontColor);
							num = 20;
							continue;
						case 2:
							if (this.ᜁ.DataFont.AllowCustomFont)
							{
								num = 8;
								continue;
							}
							flag = (this.ᜃ - (int)((double)this.ᜁ.DataFont.Size * (1.0 + this.ᜁ.RowSpacing)) < this.ᜁ.PageOptions.TrimBox.ᜀ());
							num = 17;
							continue;
						case 3:
							goto IL_1A8;
						case 4:
							this.ᜃ -= (int)((double)this.ᜁ.DataFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
							num = 16;
							continue;
						case 5:
							this.ᜀ(this.ᜄ, this.ᜃ);
							this.ᜃ = this.ᜁ.PageOptions.TrimBox.ᜁ();
							this.ᜄ = this.ᜃ;
							this.ᜀ().ᜀ().ᜈ();
							goto IL_148;
						case 6:
						{
							if (num2 >= base.ExportRowExport.Count)
							{
								num = 18;
								continue;
							}
							string exportedValue = base.ExportRowExport[num2].GetExportedValue(true);
							Font font = spr\u2059.ᜀ();
							num = 12;
							continue;
						}
						case 7:
							goto IL_17A;
						case 8:
							flag = (this.ᜃ - (int)((double)this.ᜁ.DataFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing)) < this.ᜁ.PageOptions.TrimBox.ᜀ());
							num = 3;
							continue;
						case 9:
							goto IL_9F1;
						case 10:
							goto IL_17A;
						case 11:
							goto IL_8F0;
						case 12:
							try
							{
								for (;;)
								{
									Color a_ = Color.Empty;
									num = 1;
									for (;;)
									{
										string exportedValue;
										int num3;
										Color color;
										ColumAlign columAlign;
										switch (num)
										{
										case 0:
											num = 9;
											continue;
										case 1:
											if (this.ᜁ.DataFont.AllowCustomFont)
											{
												num = 21;
												continue;
											}
											a_ = this.ᜁ.DataFont.Color;
											num = 3;
											continue;
										case 2:
											if (this.ᜁ.DataFont.AllowCustomFont)
											{
												num = 13;
												continue;
											}
											num3 = (int)this.ᜂ[num2 + 1] - sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.DataFont.Size, (int)this.ᜁ.DataFont.PdfFontName) - sprᤓ.ᜀ(exportedValue, this.ᜁ.DataFont.Size, (int)this.ᜁ.DataFont.PdfFontName, this);
											num = 14;
											continue;
										case 3:
											goto IL_4D1;
										case 4:
											goto IL_877;
										case 5:
											if (color != Color.White)
											{
												num = 0;
												continue;
											}
											goto IL_272;
										case 6:
											if (this.ᜁ.DataFont.AllowCustomFont)
											{
												num = 23;
												continue;
											}
											num3 += (int)Math.Ceiling((double)((float)((int)this.ᜂ[num2 + 1] - (int)this.ᜂ[num2] - sprᤓ.ᜀ(exportedValue, this.ᜁ.DataFont.Size, (int)this.ᜁ.DataFont.PdfFontName, this) - sprᤓ.ᜀ(this.ᜁ.ColSpacing, this.ᜁ.DataFont.Size, (int)this.ᜁ.DataFont.PdfFontName)) / 2f));
											num = 10;
											continue;
										case 7:
											goto IL_4D1;
										case 8:
											goto IL_272;
										case 9:
											if (this.ᜁ.DataFont.AllowCustomFont)
											{
												num = 11;
												continue;
											}
											this.ᜀ().ᜀ().ᜁ((double)((int)this.ᜂ[num2]), (double)this.ᜃ - (double)this.ᜁ.DataFont.Size * this.ᜁ.RowSpacing / 2.0, (double)((int)this.ᜂ[num2 + 1] - (int)this.ᜂ[num2]), (double)this.ᜁ.DataFont.Size * (1.0 + this.ᜁ.RowSpacing) - (double)((float)this.ᜁ.GridLineWidth / 2f), color);
											num = 17;
											continue;
										case 10:
											goto IL_877;
										case 11:
											this.ᜀ().ᜀ().ᜁ((double)((int)this.ᜂ[num2]), (double)this.ᜃ - (double)this.ᜁ.DataFont.CustomFont.Size * this.ᜁ.RowSpacing / 2.0, (double)((int)this.ᜂ[num2 + 1] - (int)this.ᜂ[num2]), (double)this.ᜁ.DataFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing) - (double)((float)this.ᜁ.GridLineWidth / 2f), color);
											num = 8;
											continue;
										case 12:
											goto IL_6F7;
										case 13:
											num3 = (int)this.ᜂ[num2 + 1] - sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.DataFont) - sprᤓ.ᜀ(exportedValue, this.ᜁ.DataFont);
											num = 4;
											continue;
										case 14:
											goto IL_877;
										case 15:
											if (this.ᜁ.DataFont.AllowCustomFont)
											{
												num = 22;
												continue;
											}
											num3 = (int)this.ᜂ[num2] + sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.DataFont.Size, (int)this.ᜁ.DataFont.PdfFontName);
											num = 12;
											continue;
										case 16:
											num = 20;
											continue;
										case 17:
											goto IL_272;
										case 18:
											goto IL_877;
										case 19:
											switch (columAlign)
											{
											case ColumAlign.Center:
												num = 6;
												continue;
											case ColumAlign.Right:
												num = 2;
												continue;
											default:
												num = 16;
												continue;
											}
											break;
										case 20:
											goto IL_877;
										case 21:
											a_ = this.ᜁ.DataFont.CustomFontColor;
											num = 7;
											continue;
										case 22:
											num3 = (int)this.ᜂ[num2] + sprᤓ.ᜀ(this.ᜁ.ColSpacing / 2.0, this.ᜁ.DataFont);
											num = 24;
											continue;
										case 23:
											num3 += (int)Math.Ceiling((double)((float)((int)this.ᜂ[num2 + 1] - (int)this.ᜂ[num2] - sprᤓ.ᜀ(exportedValue, this.ᜁ.DataFont) - sprᤓ.ᜀ(this.ᜁ.ColSpacing, this.ᜁ.DataFont)) / 2f));
											num = 18;
											continue;
										case 24:
											goto IL_6F7;
										case 25:
											goto IL_89E;
										}
										break;
										IL_272:
										num3 = 0;
										num = 15;
										continue;
										IL_4D1:
										ColumAlign columAlign2 = base.ColumnsExport[num2].ColAlign;
										color = Color.White;
										Font font;
										CellParamsEventArgs cellParamsEventArgs = new CellParamsEventArgs(base.RowsCount, num2, exportedValue, columAlign2, font, color);
										base.ᜀ(this, cellParamsEventArgs);
										columAlign2 = cellParamsEventArgs.Align;
										color = cellParamsEventArgs.Background;
										num = 5;
										continue;
										IL_6F7:
										columAlign = columAlign2;
										num = 19;
										continue;
										IL_877:
										this.ᜀ().ᜀ().ᜀ(num3, this.ᜃ, exportedValue, a_);
										num = 25;
									}
								}
								IL_89E:
								goto IL_B8D;
							}
							finally
							{
								Font font;
								font.Dispose();
							}
							goto IL_8AA;
							IL_B8D:
							num2++;
							num = 7;
							continue;
						case 13:
							goto IL_C6;
						case 14:
							goto IL_8AA;
						case 15:
							if (flag)
							{
								num = 5;
								continue;
							}
							goto IL_B5E;
						case 16:
							goto IL_C6;
						case 17:
							goto IL_1A8;
						case 18:
							num = 0;
							continue;
						case 19:
							goto IL_9EC;
						case 20:
							goto IL_9F1;
						case 21:
							if (this.ᜁ.DataFont.AllowCustomFont)
							{
								num = 1;
								continue;
							}
							this.ᜀ().ᜀ().ᜀ(this.ᜁ.DataFont.Color);
							num = 9;
							continue;
						case 22:
							if (this.ᜁ.DataFont.AllowCustomFont)
							{
								num = 4;
								continue;
							}
							if (true)
							{
							}
							this.ᜃ -= (int)((double)this.ᜁ.DataFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
							num = 13;
							continue;
						case 23:
							goto IL_B5E;
						}
						break;
						IL_C6:
						num2 = 0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_148:
							num = 21;
							continue;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						IL_17A:
						num = 6;
						continue;
						IL_1A8:
						num = 15;
						continue;
						IL_8AA:
						this.ᜃ -= (int)((double)this.ᜁ.DataFont.CustomFont.Size * this.ᜁ.RowSpacing / 2.0);
						num = 11;
						continue;
						IL_9F1:
						this.ᜀ().ᜀ().ᜁ(this.ᜁ.GridLineColor);
						this.ᜀ().ᜀ().ᜀ((double)((int)this.ᜂ[0]), (double)this.ᜃ, (double)((int)this.ᜂ[this.ᜂ.Count - 1]), (double)this.ᜃ, this.ᜁ.GridLineColor);
						num = 23;
						continue;
						IL_B5E:
						num = 22;
					}
				}
				IL_8F0:
				IL_9EC:
				this.ᜀ().ᜀ().ᜀ((double)((int)this.ᜂ[0]), (double)this.ᜃ, (double)((int)this.ᜂ[this.ᜂ.Count - 1]), (double)this.ᜃ, this.ᜁ.GridLineColor);
				return;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000B216C File Offset: 0x000B116C
		protected void WriteHeader()
		{
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					this.ᜃ -= (int)((double)this.ᜁ.HeaderFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num = 10;
					continue;
				case 1:
					if (num2 >= base.Header.Count)
					{
						num = 13;
						continue;
					}
					num = 20;
					continue;
				case 2:
					this.ᜃ -= (int)((double)this.ᜁ.HeaderFont.CustomFont.Size * this.ᜁ.RowSpacing / 2.0);
					num = 4;
					continue;
				case 3:
					goto IL_387;
				case 4:
					goto IL_382;
				case 6:
					if (this.ᜁ.HeaderFont.AllowCustomFont)
					{
						num = 2;
						continue;
					}
					this.ᜃ -= (int)((double)this.ᜁ.HeaderFont.Size * this.ᜁ.RowSpacing / 2.0);
					num = 21;
					continue;
				case 7:
					this.ᜀ().ᜀ().ᜁ(this.ᜁ.HeaderFont);
					num2 = 0;
					num = 23;
					continue;
				case 8:
					if (this.ᜃ < this.ᜁ.PageOptions.TrimBox.ᜀ())
					{
						num = 19;
						continue;
					}
					goto IL_99;
				case 9:
					goto IL_F4;
				case 10:
					goto IL_41D;
				case 11:
					goto IL_11B;
				case 12:
					goto IL_99;
				case 13:
					num = 6;
					continue;
				case 14:
					this.ᜀ().ᜀ().ᜀ(this.ᜁ.HeaderFont.CustomFontColor);
					num = 9;
					continue;
				case 15:
					goto IL_41D;
				case 16:
					if (this.ᜁ.HeaderFont.AllowCustomFont)
					{
						num = 14;
						continue;
					}
					this.ᜀ().ᜀ().ᜀ(this.ᜁ.HeaderFont.Color);
					num = 18;
					continue;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11B;
					default:
						if (false)
						{
						}
						goto IL_99;
					}
					break;
				case 18:
					goto IL_F4;
				case 19:
					this.ᜀ().ᜀ().ᜈ();
					num = 16;
					continue;
				case 20:
					if (this.ᜁ.HeaderFont.AllowCustomFont)
					{
						num = 0;
						continue;
					}
					this.ᜃ -= (int)((double)this.ᜁ.HeaderFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num = 15;
					continue;
				case 21:
					goto IL_2B3;
				case 22:
					this.ᜃ = this.ᜁ.PageOptions.TrimBox.ᜁ() - (int)((double)this.ᜁ.HeaderFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num = 12;
					continue;
				case 23:
					goto IL_387;
				}
				if (true)
				{
				}
				if (base.Header.Count > 0)
				{
					num = 7;
					continue;
				}
				break;
				IL_99:
				this.ᜀ().ᜀ().ᜀ(this.ᜁ.PageOptions.TrimBox.ᜃ(), this.ᜃ, base.Header[num2], this.ᜁ.HeaderFont.Color);
				num2++;
				num = 3;
				continue;
				IL_F4:
				this.ᜀ().ᜀ().ᜁ(this.ᜁ.GridLineColor);
				num = 11;
				continue;
				IL_11B:
				if (this.ᜁ.HeaderFont.AllowCustomFont)
				{
					num = 22;
					continue;
				}
				this.ᜃ = this.ᜁ.PageOptions.TrimBox.ᜁ() - (int)((double)this.ᜁ.HeaderFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
				num = 17;
				continue;
				IL_387:
				num = 1;
				continue;
				IL_41D:
				num = 8;
			}
			IL_2B3:
			IL_382:
			this.ᜄ = this.ᜃ;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000B2674 File Offset: 0x000B1674
		protected void WriteFooter()
		{
			int num = 9;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					this.ᜀ().ᜀ().ᜀ(this.ᜁ.PageOptions.TrimBox.ᜃ(), this.ᜃ, base.Footer[num2], this.ᜁ.FooterFont.CustomFontColor);
					num = 11;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3BE;
					default:
						if (false)
						{
						}
						if (this.ᜁ.FooterFont.AllowCustomFont)
						{
							num = 18;
							continue;
						}
						if (true)
						{
						}
						this.ᜃ = this.ᜁ.PageOptions.TrimBox.ᜁ() - (int)((double)this.ᜁ.FooterFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
						num = 6;
						continue;
					}
					break;
				case 2:
					if (this.ᜁ.FooterFont.AllowCustomFont)
					{
						num = 10;
						continue;
					}
					this.ᜃ -= (int)((double)this.ᜁ.FooterFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num = 3;
					continue;
				case 3:
					goto IL_3BE;
				case 4:
					goto IL_18C;
				case 5:
					this.ᜀ().ᜀ().ᜁ(this.ᜁ.FooterFont);
					num2 = 0;
					num = 15;
					continue;
				case 6:
					goto IL_1CD;
				case 7:
					if (num2 >= base.Footer.Count)
					{
						num = 14;
						continue;
					}
					num = 2;
					continue;
				case 8:
					goto IL_3BE;
				case 10:
					this.ᜃ -= (int)((double)this.ᜁ.FooterFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num = 8;
					continue;
				case 11:
					goto IL_18C;
				case 12:
					this.ᜀ().ᜀ().ᜈ();
					num = 1;
					continue;
				case 13:
					goto IL_1CD;
				case 14:
					return;
				case 15:
					goto IL_1FD;
				case 16:
					if (this.ᜃ < this.ᜁ.PageOptions.TrimBox.ᜀ())
					{
						num = 12;
						continue;
					}
					goto IL_1CD;
				case 17:
					if (this.ᜁ.FooterFont.AllowCustomFont)
					{
						num = 0;
						continue;
					}
					this.ᜀ().ᜀ().ᜀ(this.ᜁ.PageOptions.TrimBox.ᜃ(), this.ᜃ, base.Footer[num2], this.ᜁ.FooterFont.Color);
					num = 4;
					continue;
				case 18:
					this.ᜃ = this.ᜁ.PageOptions.TrimBox.ᜁ() - (int)((double)this.ᜁ.FooterFont.CustomFont.Size * (1.0 + this.ᜁ.RowSpacing / 2.0));
					num = 13;
					continue;
				case 19:
					goto IL_1FD;
				}
				if (base.Footer.Count > 0)
				{
					num = 5;
					continue;
				}
				break;
				IL_18C:
				num2++;
				num = 19;
				continue;
				IL_1CD:
				num = 17;
				continue;
				IL_1FD:
				num = 7;
				continue;
				IL_3BE:
				num = 16;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000B2A84 File Offset: 0x000B1A84
		protected override void EndDataExport()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(this.ᜄ, this.ᜃ);
					num = 1;
					continue;
				case 1:
					goto IL_6F;
				}
				if (true)
				{
				}
				if (this.ᜃ == this.ᜁ.PageOptions.TrimBox.ᜁ())
				{
					break;
				}
				num = 0;
			}
			IL_6F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6F;
			default:
				if (false)
				{
				}
				this.WriteFooter();
				this.ᜀ().ᜀ().ᜋ();
				this.ᜀ().ᜀ().ᜉ();
				base.EndDataExport();
				return;
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000B2B4C File Offset: 0x000B1B4C
		internal new spr\u2093 ᜀ()
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
			return base.ᜀ() as spr\u2093;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000B2B94 File Offset: 0x000B1B94
		protected override Type GetWriterType()
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
			return typeof(spr\u2093);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000B2BDC File Offset: 0x000B1BDC
		internal override string NormalString(string S)
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
			StringBuilder stringBuilder = new StringBuilder(S);
			stringBuilder.Replace(HyperlinksCollectionEditor.b("稥", a_), HyperlinksCollectionEditor.b("稥琧", a_));
			stringBuilder.Replace(HyperlinksCollectionEditor.b("ล", a_), HyperlinksCollectionEditor.b("稥'", a_));
			stringBuilder.Replace(HyperlinksCollectionEditor.b("༥", a_), HyperlinksCollectionEditor.b("稥ħ", a_));
			return stringBuilder.ToString();
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000B2C98 File Offset: 0x000B1C98
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000B2CDC File Offset: 0x000B1CDC
		[Description("Gets or sets options for the result PDF file.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PDFOptions PDFOptions
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
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 2:
						this.ᜁ = value;
						num = 0;
						continue;
					case 3:
						if (value != this.ᜁ)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						if (true)
						{
						}
						break;
					}
					goto IL_2C;
					IL_2F:
					num = 1;
					continue;
					IL_2C:
					if (value != null)
					{
						goto IL_2F;
					}
					break;
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000B2D74 File Offset: 0x000B1D74
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x000B2DB8 File Offset: 0x000B1DB8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection ColumnsWidth
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
				return base.ColumnsWidth;
			}
			set
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
				base.ColumnsWidth = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x000B2DFC File Offset: 0x000B1DFC
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x000B2E40 File Offset: 0x000B1E40
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		public new StringListCollection NotTruncatableColumns
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x000B2E84 File Offset: 0x000B1E84
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x000B2EC8 File Offset: 0x000B1EC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection ColumnsAlign
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
				return base.ColumnsAlign;
			}
			set
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
				base.ColumnsAlign = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x000B2F0C File Offset: 0x000B1F0C
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x000B2F50 File Offset: 0x000B1F50
		[Editor(typeof(PDFFileNameEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public new string FileName
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
				return base.FileName;
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
				base.FileName = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x000B2F94 File Offset: 0x000B1F94
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x000B2FD8 File Offset: 0x000B1FD8
		[Description("Determines the encoding type of the result file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(PDFEncodingType.ASCII)]
		public new PDFEncodingType DataEncoding
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
					IL_14:
					this.ᜅ = value;
					for (;;)
					{
						IL_1D:
						if (true)
						{
						}
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1D;
								default:
									if (false)
									{
									}
									switch (value)
									{
									case PDFEncodingType.ASCII:
										goto IL_8B;
									case PDFEncodingType.OEM:
										goto IL_6F;
									case PDFEncodingType.UTF8:
										goto IL_A4;
									default:
										num = 1;
										continue;
									}
									break;
								}
								break;
							case 1:
								num = 2;
								continue;
							case 2:
								goto IL_A2;
							}
							goto IL_14;
						}
					}
				}
				IL_6F:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.OEMCodePage);
				return;
				IL_8B:
				this.m_currEncoding = new ASCIIEncoding();
				return;
				IL_A2:
				this.m_currEncoding = new ASCIIEncoding();
				return;
				IL_A4:
				this.m_currEncoding = new UTF8Encoding();
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000B30A0 File Offset: 0x000B20A0
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x000B30E4 File Offset: 0x000B20E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public new bool AutoFitColWidth
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x000B3128 File Offset: 0x000B2128
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x000B316C File Offset: 0x000B216C
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicate whether export long char/binary column.")]
		[Browsable(true)]
		public new bool ExportLongColumn
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
				return base.ExportLongColumn;
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
				base.ExportLongColumn = value;
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001081 RID: 4225 RVA: 0x000B31B0 File Offset: 0x000B21B0
		// (remove) Token: 0x06001082 RID: 4226 RVA: 0x000B31F4 File Offset: 0x000B21F4
		protected new event CellParamsEventHandler GetCellParams
		{
			add
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
				base.GetCellParams += value;
			}
			remove
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
				base.GetCellParams -= value;
			}
		}

		// Token: 0x04000BEE RID: 3054
		private new License ᜀ;

		// Token: 0x04000BEF RID: 3055
		private int[] \u2593\u008D\u0097\u0099;

		// Token: 0x04000BF0 RID: 3056
		private new PDFOptions ᜁ = new PDFOptions();

		// Token: 0x04000BF1 RID: 3057
		private byte[] \u2460\u00A4\u00A1\u0088;

		// Token: 0x04000BF2 RID: 3058
		private new ArrayList ᜂ = new ArrayList();

		// Token: 0x04000BF3 RID: 3059
		private long[] \u2609\u00AB\u0094\u00A1;

		// Token: 0x04000BF4 RID: 3060
		private long \u2593\u00A9\u0099\u00AE;

		// Token: 0x04000BF5 RID: 3061
		private new int ᜃ;

		// Token: 0x04000BF6 RID: 3062
		private new int ᜄ;

		// Token: 0x04000BF7 RID: 3063
		private PDFEncodingType ᜅ;
	}
}

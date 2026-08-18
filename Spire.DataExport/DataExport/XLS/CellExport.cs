using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.EventArgs;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001BD RID: 445
	[ToolboxItem(true)]
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	public class CellExport : FormatTextExport
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x00082D64 File Offset: 0x00081D64
		public CellExport()
		{
			int a_ = 17;
			this.ᜀ = 40;
			this.\u1714 = new SheetOptions();
			this.ᜥ = new CellGraphic();
			this.ᜦ = new uint[16];
			this.ᜮ = new ArrayList();
			this.ᜯ = true;
			this.ᜰ = HyperlinksCollectionEditor.b("縬䜮吰嘲䄴ض", a_);
			this.ᜱ = 3;
			base..ctor();
			if (base.DesignMode)
			{
				this.ᜀ(this.DataSource, this.SQLCommand, this.DataTable, this.ListView);
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00082E04 File Offset: 0x00081E04
		protected override void InitializeVariables()
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜂ);
					base.InitializeVariables();
					this.\u1715 = new ColumnFormats(this);
					this.\u1716 = new ItemStyles(this);
					this.\u171E = new CellHyperlinks(this);
					this.\u171F = new CellNotes(this);
					this.ᜠ = new Charts(this);
					this.\u1718 = new WorkSheets(this);
					this.ᜡ = new CellPictures(this);
					this.ᜢ = new CellImages(this);
					this.ᜣ = new Cells(this);
					this.ᜤ = new MergedCellList(this);
					this.AutoFitColWidth = false;
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_EB;
						case 1:
							if (num >= 16)
							{
								num2 = 3;
								continue;
							}
							this.ExtendedPalette[num] = spr\u2009.᠑[num + 40];
							num++;
							num2 = 0;
							continue;
						case 2:
							goto IL_EB;
						case 3:
							return;
						}
						break;
						IL_EB:
						num2 = 1;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00082F48 File Offset: 0x00081F48
		public override void SaveToFile()
		{
			for (;;)
			{
				for (;;)
				{
					spr\u2561.ᜀ = this.ᜁ;
					int num = 4;
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
								if (false)
								{
								}
								AboutDataExport.ShowAbout(false);
								num = 3;
								continue;
							}
							break;
						case 1:
							num = 2;
							continue;
						case 2:
							if (Environment.UserInteractive)
							{
								num = 0;
								continue;
							}
							goto IL_9F;
						case 3:
							goto IL_7E;
						case 4:
							if (this.ᜁ)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_9F;
						}
						break;
					}
				}
			}
			IL_7E:
			IL_9F:
			base.SaveToFile();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00082FFC File Offset: 0x00081FFC
		public void SaveToFile(string fileName)
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
			this.FileName = fileName;
			this.SaveToFile();
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00083044 File Offset: 0x00082044
		public override void SaveToStream(Stream Stream)
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
			base.SaveToStream(Stream);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00083094 File Offset: 0x00082094
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 16;
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䴫席䀯帱崳唵夷丹唻儽⸿流㉃⡅ⱇ摉⅋㵍絏㝑ⱓ㕕㵗㙙", a_), response, saveType);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)memoryStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_98;
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
						if (memoryStream == null)
						{
							goto IL_9A;
						}
						break;
					}
					num = 1;
				}
				IL_98:
				IL_9A:;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00083158 File Offset: 0x00082158
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 12;
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				if (true)
				{
				}
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䤧娩尫䈭夯儱唳䈵儷唹刻ᄽ㘿ⱁ⁃桅╇㥉態⭍⡏ㅑㅓ㩕", a_), response, SaveType.Attachment);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_98;
					case 1:
						((IDisposable)memoryStream).Dispose();
						num = 0;
						continue;
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
						if (memoryStream == null)
						{
							goto IL_9A;
						}
						break;
					}
					num = 1;
				}
				IL_98:
				IL_9A:;
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0008321C File Offset: 0x0008221C
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

		// Token: 0x06000C82 RID: 3202 RVA: 0x00083270 File Offset: 0x00082270
		public void SetColumnWidth(ushort Col, ushort Width)
		{
			for (;;)
			{
				IL_00:
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.ᜑ = new Hashtable();
						num = 1;
						continue;
					case 1:
						goto IL_58;
					case 2:
						goto IL_9A;
					case 3:
						if (this.ᜑ.ContainsKey(Col))
						{
							num = 2;
							continue;
						}
						goto IL_BC;
					}
					if (this.ᜑ == null)
					{
						num = 0;
						continue;
					}
					IL_58:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 3;
						break;
					}
				}
			}
			IL_9A:
			this.ᜑ[Col] = Width;
			return;
			IL_BC:
			this.ᜑ.Add(Col, Width);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00083350 File Offset: 0x00082350
		private void ᜀ(ExportSource A_0, IDbCommand A_1, DataTable A_2, ListView A_3)
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
			this.\u1714.AggregateFormat.ExportSource = A_0;
			this.\u1714.TitlesFormat.ExportSource = A_0;
			this.\u1714.CustomDataFormat.ExportSource = A_0;
			this.\u1714.FooterFormat.ExportSource = A_0;
			this.\u1714.HeaderFormat.ExportSource = A_0;
			this.\u1714.HyperlinkFormat.ExportSource = A_0;
			this.\u1714.AggregateFormat.Command = A_1;
			this.\u1714.TitlesFormat.Command = A_1;
			this.\u1714.CustomDataFormat.Command = A_1;
			this.\u1714.FooterFormat.Command = A_1;
			this.\u1714.HeaderFormat.Command = A_1;
			this.\u1714.HyperlinkFormat.Command = A_1;
			this.\u1714.AggregateFormat.DataTable = A_2;
			this.\u1714.TitlesFormat.DataTable = A_2;
			this.\u1714.CustomDataFormat.DataTable = A_2;
			this.\u1714.FooterFormat.DataTable = A_2;
			this.\u1714.HeaderFormat.DataTable = A_2;
			this.\u1714.HyperlinkFormat.DataTable = A_2;
			this.\u1714.AggregateFormat.ListView = A_3;
			this.\u1714.TitlesFormat.ListView = A_3;
			this.\u1714.CustomDataFormat.ListView = A_3;
			this.\u1714.FooterFormat.ListView = A_3;
			this.\u1714.HeaderFormat.ListView = A_3;
			this.\u1714.HyperlinkFormat.ListView = A_3;
		}

		// Token: 0x06000C84 RID: 3204
		[DllImport("gdi32")]
		private static extern IntPtr GetStockObject(int A_0);

		// Token: 0x06000C85 RID: 3205
		[DllImport("gdi32")]
		private static extern int GetOutlineTextMetrics(IntPtr A_0, int A_1, ref CellExport.ᜀ A_2);

		// Token: 0x06000C86 RID: 3206 RVA: 0x0008352C File Offset: 0x0008252C
		protected override void Dispose(bool Disposing)
		{
			if (!this.ᜁ)
			{
				if (true)
				{
				}
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_C9;
						case 2:
							goto IL_DB;
						case 3:
							goto IL_A9;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_64;
							default:
								if (false)
								{
								}
								this.ᜂ.Dispose();
								this.ᜂ = null;
								num = 1;
								continue;
							}
							break;
						case 5:
							this.\u1714.Dispose();
							this.ᜥ.Dispose();
							goto IL_64;
						case 6:
							if (this.ᜂ != null)
							{
								num = 4;
								continue;
							}
							goto IL_C9;
						}
						if (Disposing)
						{
							num = 5;
							continue;
						}
						goto IL_A9;
						IL_64:
						num = 3;
						continue;
						IL_A9:
						num = 6;
						continue;
						IL_C9:
						this.ᜁ = true;
						num = 2;
					}
					IL_DB:;
				}
				finally
				{
					base.Dispose(Disposing);
				}
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00083638 File Offset: 0x00082638
		private void ᜧ()
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
			this.GetTempFileName();
			this.ᜃ = new spr\u215F();
			this.ᜄ = new FontList();
			this.ᜅ = new sprᢁ();
			this.ᜆ = new spr\u2363();
			this.ᜇ = new spr\u1D65();
			this.ᜈ = new spr\u2398();
			this.ᜉ = new sprᦛ();
			this.ᜊ = 49;
			this.ᜋ = 15;
			this.ᜌ = 4;
			this.ᜎ = new ArrayList();
			this.ᜐ = new Hashtable();
			this.\u1712 = new sprấ(this.m_tempFileName);
			this.\u1713 = new byte[8224];
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00083714 File Offset: 0x00082714
		private void ᜦ()
		{
			for (;;)
			{
				this.\u1713 = null;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜅ = null;
						num = 16;
						continue;
					case 1:
						this.ᜃ = null;
						num = 19;
						continue;
					case 2:
						if (this.ᜃ != null)
						{
							num = 1;
							continue;
						}
						goto IL_290;
					case 3:
						this.ᜎ = null;
						num = 14;
						continue;
					case 4:
						this.ᜈ = null;
						num = 18;
						continue;
					case 5:
						if (this.\u1712 != null)
						{
							num = 27;
							continue;
						}
						goto IL_26A;
					case 6:
						this.ᜆ = null;
						num = 22;
						continue;
					case 7:
						this.ᜇ = null;
						num = 17;
						continue;
					case 8:
						if (this.ᜅ != null)
						{
							num = 0;
							continue;
						}
						goto IL_2CA;
					case 9:
						return;
					case 10:
						if (this.ᜄ != null)
						{
							num = 29;
							continue;
						}
						goto IL_1AF;
					case 11:
						if (true)
						{
						}
						goto IL_EA;
					case 12:
						if (this.ᜇ != null)
						{
							num = 7;
							continue;
						}
						goto IL_110;
					case 13:
						if (this.ᜈ != null)
						{
							num = 4;
							continue;
						}
						goto IL_24A;
					case 14:
						goto IL_166;
					case 15:
						if (this.ᜉ != null)
						{
							num = 21;
							continue;
						}
						goto IL_EA;
					case 16:
						goto IL_2CA;
					case 17:
						goto IL_110;
					case 18:
						goto IL_24A;
					case 19:
						goto IL_290;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AB;
						default:
							if (false)
							{
							}
							if (this.ᜐ != null)
							{
								num = 24;
								continue;
							}
							return;
						}
						break;
					case 21:
						this.ᜉ = null;
						num = 11;
						continue;
					case 22:
						goto IL_B0;
					case 23:
						if (this.ᜆ != null)
						{
							num = 6;
							continue;
						}
						goto IL_B0;
					case 24:
						this.ᜐ = null;
						num = 9;
						continue;
					case 25:
						goto IL_1AF;
					case 26:
						if (this.ᜎ != null)
						{
							num = 3;
							continue;
						}
						goto IL_166;
					case 27:
						goto IL_AB;
					case 28:
						goto IL_26A;
					case 29:
						this.ᜄ = null;
						num = 25;
						continue;
					}
					break;
					IL_AB:
					this.\u1712.Close();
					this.\u1712 = null;
					num = 28;
					continue;
					IL_B0:
					num = 12;
					continue;
					IL_EA:
					num = 13;
					continue;
					IL_110:
					num = 26;
					continue;
					IL_166:
					num = 20;
					continue;
					IL_1AF:
					num = 8;
					continue;
					IL_24A:
					num = 2;
					continue;
					IL_26A:
					num = 15;
					continue;
					IL_290:
					num = 10;
					continue;
					IL_2CA:
					num = 23;
				}
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00083A28 File Offset: 0x00082A28
		private void ᜥ()
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
			this.ᜃ = new spr\u215F();
			this.ᜄ = new FontList();
			this.ᜅ = new sprᢁ();
			this.ᜆ = new spr\u2363();
			this.ᜇ = new spr\u1D65();
			this.ᜈ = new spr\u2398();
			this.ᜉ = new sprᦛ();
			this.ᜊ = 49;
			this.ᜋ = 15;
			this.ᜌ = 4;
			this.ᜎ = new ArrayList();
			this.ᜐ = new Hashtable();
			this.\u1712 = new sprấ(this.FileName);
			this.\u1713 = new byte[8224];
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00083B00 File Offset: 0x00082B00
		private void ᜤ()
		{
			for (;;)
			{
				this.\u1713 = null;
				if (true)
				{
				}
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B3;
						default:
							if (false)
							{
							}
							if (this.ᜐ != null)
							{
								num = 27;
								continue;
							}
							return;
						}
						break;
					case 1:
						return;
					case 2:
						this.ᜇ = null;
						num = 22;
						continue;
					case 3:
						if (this.ᜇ != null)
						{
							num = 2;
							continue;
						}
						goto IL_118;
					case 4:
						this.ᜅ = null;
						num = 7;
						continue;
					case 5:
						if (this.ᜆ != null)
						{
							num = 16;
							continue;
						}
						goto IL_B8;
					case 6:
						goto IL_166;
					case 7:
						goto IL_2CA;
					case 8:
						goto IL_B3;
					case 9:
						goto IL_B8;
					case 10:
						this.ᜎ = null;
						num = 6;
						continue;
					case 11:
						if (this.ᜅ != null)
						{
							num = 4;
							continue;
						}
						goto IL_2CA;
					case 12:
						goto IL_24A;
					case 13:
						goto IL_1AF;
					case 14:
						goto IL_290;
					case 15:
						if (this.\u1712 != null)
						{
							num = 8;
							continue;
						}
						goto IL_26A;
					case 16:
						this.ᜆ = null;
						num = 9;
						continue;
					case 17:
						if (this.ᜉ != null)
						{
							num = 20;
							continue;
						}
						goto IL_F2;
					case 18:
						if (this.ᜃ != null)
						{
							num = 23;
							continue;
						}
						goto IL_290;
					case 19:
						this.ᜈ = null;
						num = 12;
						continue;
					case 20:
						this.ᜉ = null;
						num = 25;
						continue;
					case 21:
						if (this.ᜄ != null)
						{
							num = 26;
							continue;
						}
						goto IL_1AF;
					case 22:
						goto IL_118;
					case 23:
						this.ᜃ = null;
						num = 14;
						continue;
					case 24:
						if (this.ᜎ != null)
						{
							num = 10;
							continue;
						}
						goto IL_166;
					case 25:
						goto IL_F2;
					case 26:
						this.ᜄ = null;
						num = 13;
						continue;
					case 27:
						this.ᜐ = null;
						num = 1;
						continue;
					case 28:
						if (this.ᜈ != null)
						{
							num = 19;
							continue;
						}
						goto IL_24A;
					case 29:
						goto IL_26A;
					}
					break;
					IL_B3:
					this.\u1712.Close();
					this.\u1712 = null;
					num = 29;
					continue;
					IL_B8:
					num = 3;
					continue;
					IL_F2:
					num = 28;
					continue;
					IL_118:
					num = 24;
					continue;
					IL_166:
					num = 0;
					continue;
					IL_1AF:
					num = 11;
					continue;
					IL_24A:
					num = 18;
					continue;
					IL_26A:
					num = 17;
					continue;
					IL_290:
					num = 21;
					continue;
					IL_2CA:
					num = 5;
				}
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00083E14 File Offset: 0x00082E14
		protected override void BeginDataExport()
		{
			for (;;)
			{
				base.Culture = new CultureInfo(base.DataFormats.CultureName);
				base.Stoped = false;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						base.SkipRows = Math.Min(base.SkipRows, base.ListView.Items.Count);
						num = 3;
						continue;
					case 1:
						base.MaxRows = Math.Min(base.MaxRows, base.ListView.Items.Count);
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
						break;
					case 2:
						goto IL_A9;
					case 3:
						if (true)
						{
						}
						if (base.MaxRows > 0)
						{
							num = 1;
							continue;
						}
						goto IL_F8;
					case 4:
						if (base.DataSource == ExportSource.ListView)
						{
							num = 0;
							continue;
						}
						goto IL_F8;
					}
					break;
				}
			}
			IL_A9:
			IL_F8:
			base.ᜁ(this, new EventArgs());
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00083F28 File Offset: 0x00082F28
		private void ᜣ()
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
			this.ᜈ(5);
			this.\u171F();
			this.ᜀ(66, (ushort)Encoding.Default.CodePage);
			this.ᜀ(353, 0);
			this.ᜀ(156, 14);
			this.ᜀ(25, 0);
			this.ᜀ(18, 0);
			this.ᜀ(19, 0);
			this.ᜀ(431, 0);
			this.ᜀ(444, 0);
			this.\u171E();
			this.ᜀ(64, 0);
			this.ᜀ(141, 0);
			this.ᜀ(34, 0);
			this.ᜀ(14, 1);
			this.ᜀ(439, 0);
			this.ᜀ(218, 0);
			this.\u170D = this.\u1712.Position;
			this.\u171D();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0008402C File Offset: 0x0008302C
		private void ᜂ(WorkSheet A_0)
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
			A_0.ColumnList.ᜂ();
			spr\u25D7 spr_u25D = new spr\u25D7();
			this.ᜈ.ᜀ(spr_u25D);
			spr_u25D.ᜅ(A_0.Index);
			spr_u25D.ᜀ(A_0.SheetName);
			spr_u25D.ᜇ((int)this.\u1712.Position);
			this.ᜈ(16);
			this.ᜀ(13, 1);
			this.ᜀ(12, 100);
			this.ᜀ(15, 1);
			this.ᜀ(17, 0);
			this.\u171C();
			this.ᜀ(95, 1);
			this.ᜀ(42, 0);
			this.ᜀ(43, 0);
			this.ᜀ(130, 1);
			this.\u171B();
			this.ᜇ((ushort)A_0.DefColWidth);
			this.ᜀ(A_0.DefRowHeight);
			this.ᜀ(129, 1217);
			this.ᜀ(20, A_0.Options.PageHeader);
			this.ᜀ(21, A_0.Options.PageFooter);
			this.ᜀ(131, 0);
			this.ᜀ(132, 0);
			this.ᜀ(A_0.Background);
			this.ᜁ(A_0.Index);
			spr_u25D.ᜄ((int)this.\u1712.Position);
			this.ᜀ(0, 0, 0, 0);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000841AC File Offset: 0x000831AC
		private new void ᜁ(WorkSheet A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
				for (;;)
				{
					ushort num = 0;
					string text = string.Empty;
					IEnumerator enumerator = A_0.Cells.GetEnumerator();
					int num2 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							try
							{
								num2 = 21;
								for (;;)
								{
									Cell cell;
									switch (num2)
									{
									case 0:
										this.ᜀ((int)num, cell.Row - 1);
										num2 = 33;
										continue;
									case 1:
										if (!enumerator.MoveNext())
										{
											num2 = 13;
											continue;
										}
										cell = (Cell)enumerator.Current;
										num2 = 16;
										continue;
									case 2:
										if (A_0.NeedCheckRowHeight)
										{
											num2 = 32;
											continue;
										}
										break;
									case 5:
										this.ᜁ((ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num);
										num2 = 11;
										continue;
									case 6:
										if (A_0.NeedCheckRowHeight)
										{
											num2 = 37;
											continue;
										}
										break;
									case 7:
										num2 = 17;
										continue;
									case 8:
										this.ᜀ((int)num, cell.Row - 1);
										num2 = 24;
										continue;
									case 9:
										this.ᜀ(A_0, (ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num, text);
										num2 = 35;
										continue;
									case 10:
										try
										{
											DateTime d = Convert.ToDateTime(cell.Value);
											TimeSpan timeSpan = d - spr\u1C2B.ᡞ;
											this.ᜀ((ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num, timeSpan.TotalDays);
											goto IL_286;
										}
										catch (Exception ex)
										{
											throw new Exception(ex.Message + HyperlinksCollectionEditor.b("┧\u2029漫䬭尯帱焳丵䠷唹主䨽稿硁ፃ㑅ⅇ㹉⥋ᵍ㡏㝑ㅓ≕ṗ㍙㉛㝝፟੡䡣ၥ१ᡩ噫", a_));
										}
										goto IL_198;
										IL_286:
										num2 = 6;
										continue;
									case 11:
										goto IL_2AE;
									case 12:
										if (text.StartsWith(HyperlinksCollectionEditor.b("ᔧ", a_)))
										{
											num2 = 9;
											continue;
										}
										goto IL_3A4;
									case 13:
										num2 = 23;
										continue;
									case 14:
										this.ᜁ((ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num);
										num2 = 28;
										continue;
									case 15:
										if (A_0.NeedCheckRowHeight)
										{
											num2 = 0;
											continue;
										}
										break;
									case 16:
										if (cell.IsCorrect())
										{
											num2 = 26;
											continue;
										}
										break;
									case 18:
										if (this.ᜬ)
										{
											num2 = 25;
											continue;
										}
										goto IL_3A4;
									case 20:
										this.ᜀ((int)num, cell.Row - 1);
										num2 = 4;
										continue;
									case 22:
										goto IL_2AE;
									case 23:
										goto IL_61C;
									case 25:
										num2 = 12;
										continue;
									case 26:
									{
										CellType cellType = cell.CellType;
										num2 = 36;
										continue;
									}
									case 27:
										goto IL_51E;
									case 28:
										goto IL_51E;
									case 29:
										if (text.Length == 0)
										{
											num2 = 5;
											continue;
										}
										goto IL_198;
									case 30:
										if (A_0.NeedCheckRowHeight)
										{
											num2 = 8;
											continue;
										}
										break;
									case 31:
										if (A_0.NeedCheckRowHeight)
										{
											num2 = 20;
											continue;
										}
										break;
									case 32:
										this.ᜀ((int)num, cell.Row - 1);
										num2 = 3;
										continue;
									case 34:
										if (text.Length == 0)
										{
											num2 = 14;
											continue;
										}
										num2 = 18;
										continue;
									case 35:
										goto IL_51E;
									case 36:
									{
										CellType cellType;
										switch (cellType)
										{
										case CellType.Boolean:
											num = A_0.ᜀ(HyperlinksCollectionEditor.b("漧伩䈫䬭䈯匱堳", a_), cell.Format);
											this.ᜀ((ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num, (bool)cell.Value);
											num2 = 30;
											continue;
										case CellType.DateTime:
											num = A_0.ᜀ(cell.DateTimeFormat, cell.Format);
											num2 = 10;
											continue;
										case CellType.Numeric:
											num = A_0.ᜀ(cell.NumericFormat, cell.Format);
											this.ᜀ((ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num, (double)cell.Value);
											num2 = 15;
											continue;
										case CellType.String:
											num = A_0.ᜀ(HyperlinksCollectionEditor.b("漧伩䈫䬭䈯匱堳", a_), cell.Format);
											text = cell.Value.ToString();
											num2 = 34;
											continue;
										case CellType.Formula:
											num = A_0.ᜀ(HyperlinksCollectionEditor.b("漧伩䈫䬭䈯匱堳", a_), cell.Format);
											text = cell.Value.ToString();
											num2 = 29;
											continue;
										default:
											num2 = 7;
											continue;
										}
										break;
									}
									case 37:
										this.ᜀ((int)num, cell.Row - 1);
										num2 = 19;
										continue;
									}
									goto IL_132;
									IL_198:
									this.ᜀ(A_0, (ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num, text);
									num2 = 22;
									continue;
									IL_1C4:
									num2 = 1;
									continue;
									IL_132:
									goto IL_1C4;
									IL_2AE:
									num2 = 2;
									continue;
									IL_3A4:
									this.ᜀ((ushort)(cell.Row - 1), (ushort)(cell.Column - 1), num, text);
									num2 = 27;
									continue;
									IL_51E:
									num2 = 31;
								}
								IL_61C:
								goto IL_8BB;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_667;
										case 1:
											if (disposable != null)
											{
												num2 = 2;
												continue;
											}
											goto IL_669;
										case 2:
											disposable.Dispose();
											num2 = 0;
											continue;
										}
										break;
									}
								}
								IL_667:
								IL_669:;
							}
							goto Block_2;
						case 1:
							goto IL_74F;
						case 2:
							goto IL_66A;
						}
						break;
						Block_3:
						IEnumerator enumerator2;
						try
						{
							IL_74F:
							num2 = 6;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_86D;
									default:
									{
										if (false)
										{
										}
										CellHyperlink cellHyperlink;
										num = A_0.ᜀ(HyperlinksCollectionEditor.b("漧伩䈫䬭䈯匱堳", a_), cellHyperlink.Format);
										this.ᜈ.ᜀ(A_0.Index, cellHyperlink.Row - 1, cellHyperlink.Col - 1);
										this.ᜀ((ushort)(cellHyperlink.Row - 1), (ushort)(cellHyperlink.Col - 1), num, cellHyperlink.Title);
										num2 = 1;
										continue;
									}
									}
									break;
								case 2:
								{
									CellHyperlink cellHyperlink;
									if (cellHyperlink.IsValid())
									{
										num2 = 0;
										continue;
									}
									break;
								}
								case 3:
								{
									if (!enumerator2.MoveNext())
									{
										num2 = 4;
										continue;
									}
									CellHyperlink cellHyperlink = (CellHyperlink)enumerator2.Current;
									num2 = 2;
									continue;
								}
								case 4:
									num2 = 5;
									continue;
								case 5:
									goto IL_86D;
								}
								IL_83B:
								num2 = 3;
								continue;
								goto IL_83B;
							}
							IL_86D:
							goto IL_5E;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator2 as IDisposable;
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_8B8;
									case 1:
										disposable2.Dispose();
										num2 = 0;
										continue;
									case 2:
										if (disposable2 != null)
										{
											num2 = 1;
											continue;
										}
										goto IL_8BA;
									}
									break;
								}
							}
							IL_8B8:
							IL_8BA:;
						}
						goto IL_8BB;
						IL_5E:
						this.\u171A();
						this.\u1719();
						IEnumerator enumerator3 = A_0.Hyperlinks.GetEnumerator();
						num2 = 2;
						continue;
						Block_2:
						try
						{
							IL_66A:
							num2 = 5;
							for (;;)
							{
								switch (num2)
								{
								case 1:
									goto IL_701;
								case 2:
								{
									if (!enumerator3.MoveNext())
									{
										num2 = 4;
										continue;
									}
									CellHyperlink cellHyperlink2 = (CellHyperlink)enumerator3.Current;
									num2 = 6;
									continue;
								}
								case 3:
								{
									CellHyperlink cellHyperlink2;
									this.ᜀ(cellHyperlink2);
									num2 = 0;
									continue;
								}
								case 4:
									num2 = 1;
									continue;
								case 6:
								{
									CellHyperlink cellHyperlink2;
									if (cellHyperlink2.IsValid())
									{
										num2 = 3;
										continue;
									}
									break;
								}
								}
								IL_6D8:
								num2 = 2;
								continue;
								goto IL_6D8;
							}
							IL_701:
							goto IL_8E5;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable3 = enumerator3 as IDisposable;
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_74C;
									case 1:
										disposable3.Dispose();
										num2 = 0;
										continue;
									case 2:
										if (disposable3 != null)
										{
											num2 = 1;
											continue;
										}
										goto IL_74E;
									}
									break;
								}
							}
							IL_74C:
							IL_74E:;
						}
						goto Block_3;
						IL_8BB:
						this.ᜀ(A_0.MergedCells);
						enumerator2 = A_0.Hyperlinks.GetEnumerator();
						num2 = 1;
					}
				}
				IL_8E5:
				this.ᜀ(A_0);
				this.\u171D();
				return;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00084B10 File Offset: 0x00083B10
		private void ᜢ()
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				byte[] array = null;
				long position = this.\u1712.Position;
				IEnumerator enumerator = this.ᜈ.ᜀ();
				try
				{
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							num = 12;
							continue;
						case 1:
						{
							spr\u25D7 spr_u25D;
							if (spr_u25D.ᜆ() > -1)
							{
								num = 9;
								continue;
							}
							break;
						}
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							spr\u25D7 spr_u25D = (spr\u25D7)enumerator.Current;
							num = 11;
							continue;
						}
						case 3:
							num = 8;
							continue;
						case 6:
							num = 10;
							continue;
						case 7:
							num = 1;
							continue;
						case 8:
						{
							spr\u25D7 spr_u25D;
							if (spr_u25D.ᜂ() > -1)
							{
								num = 0;
								continue;
							}
							break;
						}
						case 9:
						{
							spr\u25D7 spr_u25D;
							this.\u1712.Seek((long)(spr_u25D.ᜈ() + sizeof(spr\u1DCF)), SeekOrigin.Begin);
							spr\u25D7 spr_u25D2 = spr_u25D;
							spr_u25D2.ᜁ(spr_u25D2.ᜂ() + 1);
							spr\u25D7 spr_u25D3 = spr_u25D;
							spr_u25D3.ᜃ(spr_u25D3.ᜆ() + 1);
							array = BitConverter.GetBytes(spr_u25D.ᜇ());
							this.\u1712.ᜁ(array, array.Length);
							array = BitConverter.GetBytes(spr_u25D.ᜂ());
							this.\u1712.ᜁ(array, array.Length);
							array = BitConverter.GetBytes(spr_u25D.ᜄ());
							this.\u1712.ᜁ(array, 2);
							array = BitConverter.GetBytes(spr_u25D.ᜆ());
							this.\u1712.ᜁ(array, 2);
							num = 4;
							continue;
						}
						case 10:
							goto IL_1733;
						case 11:
						{
							spr\u25D7 spr_u25D;
							if (spr_u25D.ᜇ() > -1)
							{
								num = 3;
								continue;
							}
							break;
						}
						case 12:
						{
							spr\u25D7 spr_u25D;
							if (spr_u25D.ᜄ() > -1)
							{
								num = 7;
								continue;
							}
							break;
						}
						}
						IL_1619:
						num = 2;
						continue;
						goto IL_1619;
					}
					IL_1733:
					goto IL_1531;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_177E;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_1780;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_177E:
					IL_1780:;
				}
				return;
				for (;;)
				{
					IL_1531:
					this.\u1712.Seek(position, SeekOrigin.Begin);
					MemoryStream memoryStream = new MemoryStream();
					try
					{
						for (;;)
						{
							CellFont defaultFont = this.\u1714.DefaultFont;
							int num2 = defaultFont.Name.Length;
							int num3 = sizeof(spr\u221A) + num2 * 2;
							array = new byte[num3];
							spr\u20CC spr_u20CC = new spr\u20CC(null, 49, (ushort)num3, array);
							int num = 3;
							for (;;)
							{
								int num4;
								spr\u17EF spr_u17EF;
								spr\u1D4A spr_u1D4A;
								int num5;
								spr\u22C9 a_2;
								byte[] array2;
								int num7;
								int num8;
								IEnumerator enumerator3;
								spr\u1885 spr_u;
								spr\u2116 spr_u2;
								IEnumerator enumerator7;
								int value;
								IEnumerator enumerator8;
								IEnumerator enumerator9;
								sprᬇ sprᬇ;
								IEnumerator enumerator10;
								switch (num)
								{
								case 0:
									goto IL_759;
								case 1:
								{
									memoryStream.Write(array, 0, num3);
									IEnumerator enumerator2 = this.ᜈ.ᜀ();
									num = 19;
									continue;
								}
								case 2:
									goto IL_B79;
								case 3:
									goto IL_4C6;
								case 4:
									goto IL_107B;
								case 5:
									if (num4 + 8 <= 27)
									{
										num = 11;
										continue;
									}
									goto IL_AC7;
								case 6:
									try
									{
										spr_u17EF.ᜀ(memoryStream);
										goto IL_202;
									}
									finally
									{
										spr_u17EF.Dispose();
									}
									goto IL_759;
								case 7:
									try
									{
										for (;;)
										{
											spr_u1D4A.ᜀ((ushort)num5);
											int num6 = 0;
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_836;
												case 1:
													goto IL_8DC;
												case 2:
													spr_u1D4A.ᜀ(memoryStream);
													num = 1;
													continue;
												case 3:
													goto IL_836;
												case 4:
													if (num6 >= num5)
													{
														num = 2;
														continue;
													}
													array2 = spr\u22C9.ᜀ(a_2);
													Array.Copy(array, 2 + num6 * sizeof(spr\u22C9), array2, 0, array2.Length);
													spr\u22C9.ᜀ(array2, ref a_2);
													a_2.ᜀ = 0;
													a_2.ᜁ = (ushort)num6;
													a_2.ᜂ = (ushort)num6;
													array2 = spr\u22C9.ᜀ(a_2);
													Array.Copy(array2, 0, array, 2 + num6 * sizeof(spr\u22C9), array2.Length);
													num6++;
													num = 3;
													continue;
												}
												break;
												IL_836:
												num = 4;
											}
										}
										IL_8DC:
										goto IL_130C;
									}
									finally
									{
										spr_u1D4A.Dispose();
									}
									goto IL_8E9;
								case 8:
									goto IL_B79;
								case 9:
									if (num7 >= 56)
									{
										num = 15;
										continue;
									}
									goto IL_8E9;
								case 10:
									goto IL_2F7;
								case 11:
									array2 = BitConverter.GetBytes(this.ExtendedPalette[num4 + 8 - 24]);
									Array.Copy(array2, 0, array, num8, array2.Length);
									num8 += array2.Length;
									num = 24;
									continue;
								case 12:
									if (num4 >= 56)
									{
										num = 1;
										continue;
									}
									goto IL_71B;
								case 13:
								{
									try
									{
										num = 4;
										for (;;)
										{
											switch (num)
											{
											case 0:
											{
												if (!enumerator3.MoveNext())
												{
													num = 3;
													continue;
												}
												CellFont cellFont = (CellFont)enumerator3.Current;
												num2 = cellFont.Name.Length;
												num3 = sizeof(spr\u221A) + num2 * 2;
												array = new byte[num3];
												spr_u20CC = new spr\u20CC(null, 49, (ushort)num3, array);
												num = 2;
												continue;
											}
											case 1:
												goto IL_14CA;
											case 2:
												try
												{
													CellFont cellFont;
													cellFont.ᜀ(spr_u20CC);
													spr_u20CC.ᜀ(memoryStream);
												}
												finally
												{
													spr_u20CC.Dispose();
												}
												break;
											case 3:
												num = 1;
												continue;
											}
											IL_149B:
											num = 0;
											continue;
											goto IL_149B;
										}
										IL_14CA:
										goto IL_EB9;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable2 = enumerator3 as IDisposable;
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 0:
													if (disposable2 != null)
													{
														num = 1;
														continue;
													}
													goto IL_1517;
												case 1:
													disposable2.Dispose();
													num = 2;
													continue;
												case 2:
													goto IL_1515;
												}
												break;
											}
										}
										IL_1515:
										IL_1517:;
									}
									goto IL_1518;
									IL_EB9:
									IEnumerator enumerator4 = this.ᜃ.ᜀ();
									num = 29;
									continue;
								}
								case 14:
									try
									{
										for (;;)
										{
											int num9 = 0;
											num = 0;
											for (;;)
											{
												IEnumerator enumerator5;
												switch (num)
												{
												case 0:
													goto IL_6DF;
												case 1:
													goto IL_70E;
												case 2:
													try
													{
														num = 4;
														for (;;)
														{
															switch (num)
															{
															case 0:
																num = 1;
																continue;
															case 1:
																goto IL_629;
															case 3:
															{
																if (!enumerator5.MoveNext())
																{
																	num = 0;
																	continue;
																}
																spr\u17ED spr_u17ED = (spr\u17ED)enumerator5.Current;
																Array.Clear(array, 0, num3);
																spr_u17ED.ᜀ(spr_u);
																spr_u.ᜀ(memoryStream);
																num = 2;
																continue;
															}
															}
															IL_603:
															num = 3;
															continue;
															goto IL_603;
														}
														IL_629:
														goto IL_702;
													}
													finally
													{
														for (;;)
														{
															IDisposable disposable3 = enumerator5 as IDisposable;
															num = 0;
															for (;;)
															{
																switch (num)
																{
																case 0:
																	if (disposable3 != null)
																	{
																		num = 2;
																		continue;
																	}
																	goto IL_676;
																case 1:
																	goto IL_674;
																case 2:
																	disposable3.Dispose();
																	num = 1;
																	continue;
																}
																break;
															}
														}
														IL_674:
														IL_676:;
													}
													goto IL_677;
													IL_702:
													num = 1;
													continue;
												case 3:
													goto IL_6DF;
												case 4:
													if (num9 >= 16)
													{
														num = 5;
														continue;
													}
													Array.Clear(spr_u.ᜢ(), 0, num3);
													array2 = spr\u2074.ᜀ()[num9].ᜀ();
													Array.Copy(array2, spr_u.ᜢ(), array2.Length);
													spr_u.ᜀ(memoryStream);
													num9++;
													num = 3;
													continue;
												case 5:
													goto IL_677;
												}
												break;
												IL_677:
												enumerator5 = this.ᜅ.ᜀ();
												num = 2;
												continue;
												IL_6DF:
												num = 4;
											}
										}
										IL_70E:
										goto IL_543;
									}
									finally
									{
										spr_u.Dispose();
									}
									goto IL_71B;
								case 15:
									goto IL_F78;
								case 16:
									try
									{
										num = 7;
										for (;;)
										{
											switch (num)
											{
											case 0:
											{
												IEnumerator enumerator6;
												if (!enumerator6.MoveNext())
												{
													num = 8;
													continue;
												}
												CellPicture cellPicture = (CellPicture)enumerator6.Current;
												num = 6;
												continue;
											}
											case 1:
												goto IL_99C;
											case 2:
											{
												CellPictureType cellPictureType;
												if (cellPictureType != CellPictureType.Undefined)
												{
													num = 5;
													continue;
												}
												break;
											}
											case 4:
												goto IL_A79;
											case 5:
											{
												CellPicture cellPicture;
												CellPictureType cellPictureType;
												spr_u2.ᜃ().ᜂ(new sprẐ(2, 5, cellPicture.FileName, null, (int)cellPictureType, cellPicture.CalcRefCount()));
												spr_u2.ᜃ().ᜂ(new spr\u2155(0, 1130, cellPicture.FileName, null, (int)cellPictureType));
												num = 3;
												continue;
											}
											case 6:
											{
												CellPicture cellPicture;
												if (cellPicture.Stream.Length == 0L)
												{
													num = 9;
													continue;
												}
												break;
											}
											case 8:
												num = 4;
												continue;
											case 9:
											{
												CellPictureType cellPictureType = CellPictureType.Undefined;
												num = 11;
												continue;
											}
											case 10:
											{
												CellPicture cellPicture;
												CellPictureType cellPictureType = sprᮌ.ᜀ(cellPicture.FileName);
												num = 1;
												continue;
											}
											case 11:
											{
												CellPicture cellPicture;
												if (sprᮌ.ᜁ(cellPicture.FileName))
												{
													num = 10;
													continue;
												}
												goto IL_99C;
											}
											}
											goto IL_959;
											IL_99C:
											num = 2;
											continue;
											IL_9F2:
											num = 0;
											continue;
											IL_959:
											goto IL_9F2;
										}
										IL_A79:
										goto IL_CF8;
									}
									finally
									{
										for (;;)
										{
											IEnumerator enumerator6;
											IDisposable disposable4 = enumerator6 as IDisposable;
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													disposable4.Dispose();
													num = 1;
													continue;
												case 1:
													goto IL_AC4;
												case 2:
													if (disposable4 != null)
													{
														num = 0;
														continue;
													}
													goto IL_AC6;
												}
												break;
											}
										}
										IL_AC4:
										IL_AC6:;
									}
									goto IL_AC7;
								case 17:
								{
									try
									{
										num = 3;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 2;
												continue;
											case 1:
											{
												if (!enumerator7.MoveNext())
												{
													num = 0;
													continue;
												}
												spr\u25D7 spr_u25D4 = (spr\u25D7)enumerator7.Current;
												memoryStream.Seek((long)(spr_u25D4.ᜃ() + sizeof(spr\u1DCF)), SeekOrigin.Begin);
												value = (int)((long)spr_u25D4.ᜁ() + memoryStream.Length);
												array = BitConverter.GetBytes(value);
												memoryStream.Write(array, 0, array.Length);
												num = 4;
												continue;
											}
											case 2:
												goto IL_DC4;
											}
											IL_D40:
											num = 1;
											continue;
											goto IL_D40;
										}
										IL_DC4:
										goto IL_132A;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable = enumerator7 as IDisposable;
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
														break;
													default:
														if (false)
														{
														}
														break;
													}
													disposable.Dispose();
													num = 2;
													continue;
												case 1:
													if (disposable != null)
													{
														num = 0;
														continue;
													}
													goto IL_E2D;
												case 2:
													goto IL_E2B;
												}
												break;
											}
										}
										IL_E2B:
										IL_E2D:;
									}
									goto IL_E2E;
									IL_132A:
									MemoryStream memoryStream2 = new MemoryStream();
									num = 23;
									continue;
								}
								case 18:
									if (num8 > 0)
									{
										num = 0;
										continue;
									}
									goto IL_202;
								case 19:
								{
									try
									{
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 1:
												goto IL_CAA;
											case 2:
											{
												IEnumerator enumerator2;
												if (!enumerator2.MoveNext())
												{
													num = 3;
													continue;
												}
												spr\u25D7 spr_u25D5 = (spr\u25D7)enumerator2.Current;
												spr_u25D5.ᜂ((int)memoryStream.Position);
												num2 = spr_u25D5.ᜀ().Length;
												num3 = sizeof(spr\u2447) + num2 * 2;
												array = new byte[num3];
												spr᭒ spr᭒ = new spr᭒(null, 133, (ushort)num3, array);
												num = 4;
												continue;
											}
											case 3:
												goto IL_C9E;
											case 4:
												try
												{
													spr᭒ spr᭒;
													spr᭒.ᜨ();
													spr᭒.ᜀ(0);
													spr᭒.ᜂ(0);
													spr᭒.ᜀ(0);
													spr᭒.ᜁ((byte)num2);
													spr᭒.ᜃ(1);
													spr\u25D7 spr_u25D5;
													spr᭒.ᜀ(spr_u25D5.ᜀ());
													spr᭒.ᜀ(memoryStream);
													break;
												}
												finally
												{
													spr᭒ spr᭒;
													spr᭒.Dispose();
												}
												goto IL_C9E;
											}
											IL_C26:
											num = 2;
											continue;
											goto IL_C26;
											IL_C9E:
											num = 1;
										}
										IL_CAA:
										goto IL_1342;
									}
									finally
									{
										for (;;)
										{
											IEnumerator enumerator2;
											IDisposable disposable5 = enumerator2 as IDisposable;
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 0:
													if (disposable5 != null)
													{
														num = 1;
														continue;
													}
													goto IL_CF7;
												case 1:
													disposable5.Dispose();
													num = 2;
													continue;
												case 2:
													goto IL_CF5;
												}
												break;
											}
										}
										IL_CF5:
										IL_CF7:;
									}
									goto IL_CF8;
									IL_1342:
									num3 = sizeof(spr\u1AD8);
									array = new byte[num3];
									sprẇ sprẇ = new sprẇ(null, 140, (ushort)num3, array);
									num = 31;
									continue;
								}
								case 20:
									goto IL_E2E;
								case 21:
									goto IL_7B5;
								case 22:
									num = 25;
									continue;
								case 23:
									try
									{
										for (;;)
										{
											this.\u1712.Seek(this.\u170D, SeekOrigin.Begin);
											MemoryStream memoryStream2;
											memoryStream2.Position = 0L;
											array = new byte[this.\u1712.Length - this.\u1712.Position];
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_4B9;
												case 1:
													goto IL_423;
												case 2:
													if (this.\u1712.Read(array, 0, array.Length) != array.Length)
													{
														num = 1;
														continue;
													}
													memoryStream2.Write(array, 0, array.Length);
													this.\u1712.Seek(this.\u170D, SeekOrigin.Begin);
													memoryStream.Position = 0L;
													this.\u1712.ᜀ(memoryStream, memoryStream.Length);
													memoryStream2.Position = 0L;
													this.\u1712.ᜀ(memoryStream2, memoryStream2.Length);
													this.\u1712.Seek(0L, SeekOrigin.End);
													num = 0;
													continue;
												}
												break;
											}
										}
										IL_423:
										throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搬䄮䜰刲头帶崸琺䴼娾㍀≂ㅄ⹆♈╊ቌᵎ㑐㉒ㅔ㹖㝘㱚๜⭞፠٢Ѥ੦", a_)));
										IL_4B9:
										goto IL_1518;
									}
									finally
									{
										MemoryStream memoryStream2;
										memoryStream2.Close();
									}
									goto Block_5;
								case 24:
									goto IL_F78;
								case 25:
									if (this.ᜭ)
									{
										num = 34;
										continue;
									}
									goto IL_130C;
								case 26:
									try
									{
										num = 4;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 3;
												continue;
											case 1:
											{
												if (!enumerator8.MoveNext())
												{
													num = 0;
													continue;
												}
												WorkSheet workSheet = (WorkSheet)enumerator8.Current;
												num8 += workSheet.Images.Count;
												num = 2;
												continue;
											}
											case 3:
												goto IL_1007;
											}
											IL_FB7:
											num = 1;
											continue;
											goto IL_FB7;
										}
										IL_1007:
										goto IL_E94;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable6 = enumerator8 as IDisposable;
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 0:
													if (disposable6 != null)
													{
														num = 1;
														continue;
													}
													goto IL_1054;
												case 1:
													disposable6.Dispose();
													num = 2;
													continue;
												case 2:
													goto IL_1052;
												}
												break;
											}
										}
										IL_1052:
										IL_1054:;
									}
									goto Block_21;
									IL_E94:
									num = 18;
									continue;
								case 27:
									num = 5;
									continue;
								case 28:
									goto IL_7B5;
								case 29:
									goto IL_10A4;
								case 30:
									if ((int)spr\u2009.᠓[num7] == num4 + 8)
									{
										num = 20;
										continue;
									}
									num7++;
									num = 8;
									continue;
								case 31:
									goto IL_1055;
								case 32:
									goto IL_1524;
								case 33:
									goto IL_F78;
								case 34:
									goto IL_22D;
								case 35:
									try
									{
										num = 4;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_1B4;
											case 1:
											{
												if (!enumerator9.MoveNext())
												{
													num = 2;
													continue;
												}
												WorkSheet workSheet2 = (WorkSheet)enumerator9.Current;
												num8 += workSheet2.Notes.Count;
												num = 3;
												continue;
											}
											case 2:
												num = 0;
												continue;
											}
											IL_18E:
											num = 1;
											continue;
											goto IL_18E;
										}
										IL_1B4:
										goto IL_11D8;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable7 = enumerator9 as IDisposable;
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 0:
													if (disposable7 != null)
													{
														num = 2;
														continue;
													}
													goto IL_201;
												case 1:
													goto IL_1FF;
												case 2:
													disposable7.Dispose();
													num = 1;
													continue;
												}
												break;
											}
										}
										IL_1FF:
										IL_201:;
									}
									goto IL_202;
								case 36:
								{
									try
									{
										for (;;)
										{
											int num10 = 0;
											num = 4;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_F6B;
												case 1:
													num = 0;
													continue;
												case 2:
													goto IL_F43;
												case 3:
													if (num10 >= 6)
													{
														num = 1;
														continue;
													}
													Array.Clear(array, 0, num3);
													array2 = STYLE_DEFAULT.BiffStyleArray[num10].GetBytes();
													Array.Copy(array2, sprᬇ.ᜢ(), array2.Length);
													sprᬇ.ᜀ(memoryStream);
													num10++;
													num = 2;
													continue;
												case 4:
													goto IL_F43;
												}
												break;
												IL_F43:
												num = 3;
											}
										}
										IL_F6B:
										goto IL_268;
									}
									finally
									{
										sprᬇ.Dispose();
									}
									goto IL_F78;
									IL_268:
									num3 = 230;
									array = new byte[num3];
									Array.Clear(array, 0, num3);
									num8 = 0;
									ushort value2 = 146;
									array2 = BitConverter.GetBytes(value2);
									Array.Copy(array2, 0, array, num8, array2.Length);
									num8 += array2.Length;
									num2 = num3 - 4;
									array2 = BitConverter.GetBytes(num2);
									Array.Copy(array2, 0, array, num8, 2);
									num8 += 2;
									value = 56;
									array2 = BitConverter.GetBytes(value);
									Array.Copy(array2, 0, array, num8, 2);
									num8 += 2;
									num4 = 0;
									num = 21;
									continue;
								}
								case 37:
									try
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 2:
												goto IL_129C;
											case 3:
											{
												if (!enumerator10.MoveNext())
												{
													num = 4;
													continue;
												}
												WorkSheet workSheet3 = (WorkSheet)enumerator10.Current;
												num8 += workSheet3.Charts.Count;
												num5++;
												num = 0;
												continue;
											}
											case 4:
												num = 2;
												continue;
											}
											IL_1246:
											num = 3;
											continue;
											goto IL_1246;
										}
										IL_129C:;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable8 = enumerator10 as IDisposable;
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_12E4;
												case 1:
													disposable8.Dispose();
													num = 0;
													continue;
												case 2:
													if (disposable8 != null)
													{
														num = 1;
														continue;
													}
													goto IL_12E6;
												}
												break;
											}
										}
										IL_12E4:
										IL_12E6:;
									}
									num = 38;
									continue;
								case 38:
									if (num8 <= 0)
									{
										num = 22;
										continue;
									}
									goto IL_22D;
								case 39:
									if (num4 + 8 >= 24)
									{
										num = 27;
										continue;
									}
									goto IL_AC7;
								case 40:
								{
									spr_u2 = new spr\u2116(61441, 15, 2);
									IEnumerator enumerator6 = this.ᜡ.GetEnumerator();
									num = 16;
									continue;
								}
								case 41:
									if (this.ᜡ.Count > 0)
									{
										num = 40;
										continue;
									}
									goto IL_2F7;
								}
								break;
								IL_202:
								this.ᜉ.ᜀ(memoryStream);
								enumerator7 = this.ᜈ.ᜀ();
								num = 17;
								continue;
								IL_22D:
								num3 = sizeof(sprᲂ);
								array = new byte[num3];
								Array.Clear(array, 0, num3);
								sprᨫ sprᨫ = new sprᨫ(null, 430, (ushort)num3, array);
								num = 4;
								continue;
								IL_2F7:
								spr\u19F3 spr_u19F;
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(191, 524296U));
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, 134217737U));
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(448, 134217792U));
								spr\u2116 spr_u3;
								spr_u3.ᜃ().ᜂ(spr_u19F);
								spr_u3.ᜃ().ᜂ(new spr\u1D78(0, 4));
								num3 = spr_u3.ᜀ();
								array = new byte[num3];
								value = 0;
								spr_u3.ᜀ(array, ref value);
								spr_u17EF = new spr\u17EF(null, 235, num3, array);
								num = 6;
								continue;
								IL_543:
								num3 = sizeof(sprᣭ);
								array = new byte[num3];
								sprᬇ = new sprᬇ(null, 659, (ushort)num3, array);
								num = 36;
								continue;
								Block_5:
								try
								{
									IL_4C6:
									for (;;)
									{
										defaultFont.ᜀ(spr_u20CC);
										int num11 = 0;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_514;
											case 1:
												goto IL_514;
											case 2:
												goto IL_536;
											case 3:
												num = 2;
												continue;
											case 4:
												if (num11 >= 4)
												{
													num = 3;
													continue;
												}
												spr_u20CC.ᜀ(memoryStream);
												num11++;
												num = 0;
												continue;
											}
											break;
											IL_514:
											num = 4;
										}
									}
									IL_536:
									goto IL_B37;
								}
								finally
								{
									spr_u20CC.Dispose();
								}
								goto IL_543;
								IL_B37:
								enumerator3 = this.ᜄ.GetEnumerator();
								num = 13;
								continue;
								IL_71B:
								num = 39;
								continue;
								IL_759:
								spr_u3 = new spr\u2116(61440, 15, 0);
								spr_u19F = new spr\u19F3(3, 3);
								spr_u3.ᜃ().ᜂ(new sprᯌ(0, 0, num8));
								num = 41;
								continue;
								IL_7B5:
								num = 12;
								continue;
								IL_8E9:
								num = 30;
								continue;
								IL_AC7:
								num7 = 0;
								num = 2;
								continue;
								Block_21:
								try
								{
									IL_1055:
									sprẇ sprẇ;
									sprẇ.ᜁ(1);
									sprẇ.ᜀ(1);
									sprẇ.ᜀ(memoryStream);
									goto IL_B55;
								}
								finally
								{
									sprẇ sprẇ;
									sprẇ.Dispose();
								}
								goto Block_22;
								IL_B55:
								num8 = 0;
								num5 = 0;
								enumerator10 = this.\u1718.GetEnumerator();
								num = 37;
								continue;
								IL_B79:
								num = 9;
								continue;
								IL_CF8:
								spr_u3.ᜃ().ᜂ(spr_u2);
								num = 10;
								continue;
								IL_E2E:
								array2 = BitConverter.GetBytes(spr\u2009.᠑[num7]);
								Array.Copy(array2, 0, array, num8, array2.Length);
								num8 += array2.Length;
								num = 33;
								continue;
								Block_23:
								try
								{
									IL_10A4:
									num = 4;
									for (;;)
									{
										IEnumerator enumerator4;
										sprᵾ sprᵾ;
										sprḓ sprḓ;
										switch (num)
										{
										case 0:
											goto IL_118A;
										case 1:
											num = 0;
											continue;
										case 2:
											if (!enumerator4.MoveNext())
											{
												num = 1;
												continue;
											}
											goto IL_1130;
										case 3:
											try
											{
												sprᵾ.ᜁ(sprḓ.ᜀ());
												sprᵾ.ᜀ((ushort)num2);
												sprᵾ.ᜀ(1);
												sprᵾ.ᜀ(sprḓ.ᜁ());
												sprᵾ.ᜀ(memoryStream);
												break;
											}
											finally
											{
												sprᵾ.Dispose();
											}
											goto IL_1130;
										}
										IL_10CC:
										num = 2;
										continue;
										goto IL_10CC;
										IL_1130:
										sprḓ = (sprḓ)enumerator4.Current;
										num2 = sprḓ.ᜁ().Length;
										num3 = 5 + num2 * 2;
										array = new byte[num3];
										sprᵾ = new sprᵾ(null, 1054, (ushort)num3, array);
										num = 3;
									}
									IL_118A:
									goto IL_E62;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator4;
										IDisposable disposable9 = enumerator4 as IDisposable;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												disposable9.Dispose();
												num = 2;
												continue;
											case 1:
												if (disposable9 != null)
												{
													num = 0;
													continue;
												}
												goto IL_11D7;
											case 2:
												goto IL_11D5;
											}
											break;
										}
									}
									IL_11D5:
									IL_11D7:;
								}
								goto IL_11D8;
								IL_E62:
								num3 = sizeof(spr\u2245);
								array = new byte[num3];
								spr_u = new spr\u1885(null, 224, (ushort)num3, array);
								num = 14;
								continue;
								Block_22:
								try
								{
									IL_107B:
									sprᨫ.ᜀ((ushort)this.Sheets.Count);
									sprᨫ.ᜀ(memoryStream);
									goto IL_13B8;
								}
								finally
								{
									sprᨫ.Dispose();
								}
								goto Block_23;
								IL_13B8:
								a_2.ᜀ = 0;
								a_2.ᜁ = 0;
								a_2.ᜂ = 0;
								num3 = 2 + num5 * sizeof(spr\u22C9);
								array = new byte[num3];
								Array.Clear(array, 0, num3);
								spr_u1D4A = new spr\u1D4A(null, 23, (ushort)num3, array);
								num = 7;
								continue;
								IL_F78:
								num4++;
								num = 28;
								continue;
								IL_11D8:
								enumerator8 = this.\u1718.GetEnumerator();
								num = 26;
								continue;
								IL_130C:
								enumerator9 = this.\u1718.GetEnumerator();
								num = 35;
								continue;
								IL_1518:
								num = 32;
							}
						}
						IL_1524:
						break;
					}
					finally
					{
						memoryStream.Close();
					}
				}
				return;
			}
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000864B4 File Offset: 0x000854B4
		private void ᜀ(CellNoteFormat A_0, spr\u19F3 A_1)
		{
			if (true)
			{
			}
			int num = 1;
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
						A_1.ᜁ().ᜂ(new spr\u17D3(386, 64881U - (uint)(A_0.Transparency - 1) * 656U));
						num = 2;
						continue;
					case 2:
						return;
					}
					if (A_0.Transparency <= 0)
					{
						return;
					}
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00086554 File Offset: 0x00085554
		private string ᜀ(string[] A_0)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 2;
				StringBuilder stringBuilder;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						if (num2 >= A_0.Length)
						{
							num = 4;
							continue;
						}
						string arg = A_0[num2];
						stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("尦ᤨ嘪嘬Ḯ䰰", a_), arg, ' ');
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_9D;
					case 3:
						goto IL_4D;
					case 4:
						goto IL_EC;
					case 5:
						goto IL_9D;
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					stringBuilder = new StringBuilder(A_0.Length);
					num2 = 0;
					num = 5;
					continue;
					IL_9D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
				IL_4D:
				return string.Empty;
				IL_EC:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00086658 File Offset: 0x00085658
		private void ᜀ(WorkSheet A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					CellNotes notes = A_0.Notes;
					Charts charts = A_0.Charts;
					CellImages images = A_0.Images;
					int num = 8;
					for (;;)
					{
						spr\u1BED spr_u1BED;
						int num2;
						ushort num3;
						IEnumerator enumerator;
						IEnumerator enumerator2;
						spr\u2116 spr_u;
						int num4;
						spr\u19F3 spr_u19F;
						int num5;
						int num6;
						int num7;
						switch (num)
						{
						case 0:
							goto IL_C97;
						case 1:
							if (images.Count == 0)
							{
								num = 6;
								continue;
							}
							goto IL_E5B;
						case 2:
							try
							{
								num = 6;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1FD;
									case 1:
										num = 0;
										continue;
									case 2:
									{
										CellNote cellNote;
										if (cellNote.IsValid())
										{
											num = 3;
											continue;
										}
										break;
									}
									case 3:
									{
										spr_u1BED.ᜀ(this.\u1712, num2);
										num2++;
										this.ᜆ(num3);
										num3 += 1;
										spr_u1BED.ᜀ(this.\u1712, num2);
										num2++;
										CellNote cellNote;
										string text = this.ᜀ(cellNote.Lines.GetStrings()).Trim();
										ushort a_ = (ushort)text.Length;
										this.ᜀ(a_, cellNote.Format);
										this.ᜀ(text);
										this.ᜀ(a_, cellNote.Format.Font);
										num = 4;
										continue;
									}
									case 5:
									{
										if (!enumerator.MoveNext())
										{
											num = 1;
											continue;
										}
										CellNote cellNote = (CellNote)enumerator.Current;
										num = 2;
										continue;
									}
									}
									IL_1CB:
									num = 5;
									continue;
									goto IL_1CB;
								}
								IL_1FD:
								goto IL_10ED;
							}
							finally
							{
								for (;;)
								{
									IL_217:
									IDisposable disposable;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_249:
										num = 1;
										break;
									default:
										if (false)
										{
										}
										disposable = (enumerator as IDisposable);
										num = 0;
										break;
									}
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_245;
										case 1:
											disposable.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_264;
										}
										goto IL_217;
									}
									IL_245:
									if (disposable != null)
									{
										goto IL_249;
									}
									break;
								}
								IL_264:;
							}
							goto Block_3;
						case 3:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator2.MoveNext())
										{
											num = 3;
											continue;
										}
										Chart chart = (Chart)enumerator2.Current;
										spr_u = new spr\u2116(61444, 15, 0);
										spr_u.ᜃ().ᜂ(new spr\u2401(2, 201, (ushort)(1025 + num4), 2560));
										spr_u19F = new spr\u19F3(3, 9);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(127, 17039620U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(191, 524296U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, 134217806U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, 134217805U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(447, 1048592U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(448, 134217805U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(511, 524296U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(575, 131072U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(959, 524288U));
										spr_u.ᜃ().ᜂ(spr_u19F);
										spr_u.ᜃ().ᜂ(new spr\u234D(0, 0, 0, chart.ᜀ()));
										spr_u.ᜃ().ᜂ(new spr\u2213(0, 0));
										spr_u1BED.ᜂ().ᜂ(spr_u);
										num4++;
										num = 4;
										continue;
									}
									case 1:
										goto IL_1305;
									case 3:
										num = 1;
										continue;
									}
									IL_12D3:
									num = 0;
									continue;
									goto IL_12D3;
								}
								IL_1305:
								goto IL_DB3;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator2 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable2.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_1350;
										case 2:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_1352;
										}
										break;
									}
								}
								IL_1350:
								IL_1352:;
							}
							goto IL_1353;
						case 4:
							goto IL_DF8;
						case 5:
							try
							{
								num = 6;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										CellNote cellNote2;
										this.ᜀ(cellNote2, num5 + 1);
										num5++;
										num = 5;
										continue;
									}
									case 1:
									{
										CellNote cellNote2;
										if (cellNote2.IsValid())
										{
											num = 0;
											continue;
										}
										break;
									}
									case 2:
										num = 4;
										continue;
									case 3:
									{
										IEnumerator enumerator3;
										if (!enumerator3.MoveNext())
										{
											num = 2;
											continue;
										}
										CellNote cellNote2 = (CellNote)enumerator3.Current;
										num = 1;
										continue;
									}
									case 4:
										goto IL_D65;
									}
									IL_CF1:
									num = 3;
									continue;
									goto IL_CF1;
								}
								IL_D65:
								return;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator3;
									IDisposable disposable3 = enumerator3 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable3.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_DB0;
										case 2:
											if (disposable3 != null)
											{
												num = 0;
												continue;
											}
											goto IL_DB2;
										}
										break;
									}
								}
								IL_DB0:
								IL_DB2:;
							}
							goto IL_DB3;
						case 6:
							goto IL_1392;
						case 7:
							goto IL_DF8;
						case 8:
							if (notes.Count == 0)
							{
								num = 18;
								continue;
							}
							goto IL_E5B;
						case 9:
						{
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IEnumerator enumerator4;
										if (!enumerator4.MoveNext())
										{
											num = 2;
											continue;
										}
										Chart a_2 = (Chart)enumerator4.Current;
										spr_u1BED.ᜀ(this.\u1712, num2);
										num2++;
										this.ᜅ(num3);
										num3 += 1;
										this.ᜀ(A_0.Index, a_2);
										num = 1;
										continue;
									}
									case 2:
										num = 3;
										continue;
									case 3:
										goto IL_109F;
									}
									IL_1073:
									num = 0;
									continue;
									goto IL_1073;
								}
								IL_109F:
								goto IL_DD2;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator4;
									IDisposable disposable4 = enumerator4 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable4.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_10EA;
										case 2:
											if (disposable4 != null)
											{
												num = 0;
												continue;
											}
											goto IL_10EC;
										}
										break;
									}
								}
								IL_10EA:
								IL_10EC:;
							}
							goto IL_10ED;
							IL_DD2:
							num5 = 0;
							IEnumerator enumerator3 = notes.GetEnumerator();
							num = 5;
							continue;
						}
						case 10:
							if (charts.Count == 0)
							{
								num = 11;
								continue;
							}
							goto IL_E5B;
						case 11:
							num = 1;
							continue;
						case 12:
							if (num6 >= images.Count)
							{
								num = 15;
								continue;
							}
							spr_u1BED.ᜀ(this.\u1712, num2);
							num2++;
							this.ᜄ(num3);
							num3 += 1;
							num6++;
							num = 0;
							continue;
						case 13:
							goto IL_1353;
						case 14:
							goto IL_C97;
						case 15:
						{
							IEnumerator enumerator4 = charts.GetEnumerator();
							if (true)
							{
							}
							num = 9;
							continue;
						}
						case 16:
							goto IL_267;
						case 17:
							if (num7 >= images.Count)
							{
								num = 13;
								continue;
							}
							spr_u = new spr\u2116(61444, 15, 0);
							spr_u.ᜃ().ᜂ(new spr\u2401(2, 75, (ushort)(1025 + num7), 2560));
							spr_u19F = new spr\u19F3(3, 4);
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(127, 8388608U));
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(16644, (uint)images[num7].PictureIndex));
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(49413, images[num7].Title));
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(447, 65536U));
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(831, 1048576U));
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(50048, images[num7].Title));
							spr_u19F.ᜁ().ᜂ(new spr\u17D3(959, 524288U));
							spr_u.ᜃ().ᜂ(spr_u19F);
							spr_u.ᜃ().ᜂ(new spr\u234D(0, 0, 2, images[num7].ᜀ()));
							spr_u.ᜃ().ᜂ(new spr\u2213(0, 0));
							spr_u1BED.ᜂ().ᜂ(spr_u);
							num7++;
							num = 4;
							continue;
						case 18:
							num = 10;
							continue;
						}
						break;
						IL_C97:
						num = 12;
						continue;
						IL_DB3:
						num2 = 0;
						num3 = 1;
						enumerator = notes.GetEnumerator();
						num = 2;
						continue;
						IL_DF8:
						num = 17;
						continue;
						Block_3:
						IEnumerator enumerator5;
						try
						{
							IL_267:
							num = 20;
							for (;;)
							{
								CellNote cellNote3;
								CellNoteFillType fillType;
								switch (num)
								{
								case 0:
									goto IL_7D7;
								case 1:
									goto IL_B35;
								case 2:
									goto IL_B35;
								case 3:
									if (!enumerator5.MoveNext())
									{
										num = 8;
										continue;
									}
									cellNote3 = (CellNote)enumerator5.Current;
									num = 12;
									continue;
								case 4:
									goto IL_6B7;
								case 5:
									if (num5 == 90)
									{
										num = 27;
										continue;
									}
									goto IL_5A8;
								case 6:
									num = 2;
									continue;
								case 7:
									goto IL_B35;
								case 8:
									num = 14;
									continue;
								case 9:
									goto IL_5A8;
								case 10:
									spr_u = new spr\u2116(61444, 15, 0);
									spr_u.ᜀ(sizeof(spr\u1CC5));
									spr_u.ᜃ().ᜂ(new spr\u2401(2, 202, (ushort)(1025 + num5), 2560));
									spr_u19F = new spr\u19F3(3, 0);
									spr_u19F.ᜁ().ᜂ(new spr\u17D3(128, (uint)(num5 + 1)));
									num = 5;
									continue;
								case 11:
									switch (fillType)
									{
									case CellNoteFillType.Solid:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										num = 28;
										continue;
									case CellNoteFillType.Gradient:
									{
										CellNoteGradient gradient = cellNote3.Format.Gradient;
										num = 16;
										continue;
									}
									default:
										num = 6;
										continue;
									}
									break;
								case 12:
									if (cellNote3.IsValid())
									{
										num = 10;
										continue;
									}
									break;
								case 14:
									goto IL_C48;
								case 15:
									goto IL_6B7;
								case 16:
								{
									CellNoteGradient gradient;
									switch (gradient)
									{
									case CellNoteGradient.Horizontal:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(384, 7U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, (uint)spr\u2059.ᜁ(cellNote3.Format.BackgroundColor)));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(396, 100U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(406, 10289152U));
										num = 17;
										continue;
									case CellNoteGradient.Vertical:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(384, 7U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, (uint)spr\u2059.ᜁ(cellNote3.Format.BackgroundColor)));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(395, 4289069056U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(396, 100U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(406, 10289152U));
										num = 7;
										continue;
									case CellNoteGradient.DiagonalUp:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(384, 7U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, (uint)spr\u2059.ᜁ(cellNote3.Format.BackgroundColor)));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(395, 4286119936U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(396, 100U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(406, 10289152U));
										num = 22;
										continue;
									case CellNoteGradient.DiagonalDown:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(384, 7U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, (uint)spr\u2059.ᜁ(cellNote3.Format.BackgroundColor)));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(395, 4292018176U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(396, 100U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(406, 10289152U));
										num = 23;
										continue;
									case CellNoteGradient.FromCorner:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(384, 5U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, (uint)spr\u2059.ᜁ(cellNote3.Format.BackgroundColor)));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(395, 4292018176U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(396, 100U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(406, 10289152U));
										num = 1;
										continue;
									case CellNoteGradient.FromCenter:
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(384, 6U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(385, (uint)spr\u2059.ᜁ(cellNote3.Format.ForegroundColor)));
										this.ᜀ(cellNote3.Format, spr_u19F);
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(387, (uint)spr\u2059.ᜁ(cellNote3.Format.BackgroundColor)));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(395, 4292018176U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(396, 100U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(397, 32768U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(398, 32768U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(399, 32768U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(400, 32768U));
										spr_u19F.ᜁ().ᜂ(new spr\u17D3(406, 10289152U));
										num = 21;
										continue;
									default:
										num = 19;
										continue;
									}
									break;
								}
								case 17:
									goto IL_B35;
								case 18:
									if (cellNote3.Format.FillType != CellNoteFillType.Solid)
									{
										num = 24;
										continue;
									}
									goto IL_7D7;
								case 19:
									num = 26;
									continue;
								case 21:
									goto IL_B35;
								case 22:
									goto IL_B35;
								case 23:
									goto IL_B35;
								case 24:
									num = 25;
									continue;
								case 25:
									if (cellNote3.Format.FillType == CellNoteFillType.Gradient)
									{
										num = 0;
										continue;
									}
									spr_u19F.ᜁ().ᜂ(new spr\u17D3(447, 1376276U));
									spr_u19F.ᜁ().ᜂ(new spr\u17D3(511, 524296U));
									num = 15;
									continue;
								case 26:
									goto IL_B35;
								case 27:
									spr_u19F.ᜁ().ᜂ(new spr\u17D3(133, 1U));
									num = 9;
									continue;
								case 28:
									goto IL_B35;
								}
								goto IL_2ED;
								IL_5A8:
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(125, 1U));
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(191, 524296U));
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(344, 0U));
								fillType = cellNote3.Format.FillType;
								num = 11;
								continue;
								IL_6B7:
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(513, 0U));
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(575, 196611U));
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(959, 655362U));
								spr_u.ᜃ().ᜂ(spr_u19F);
								spr_u.ᜃ().ᜂ(new spr\u234D(0, 0, 3, cellNote3.ᜀ()));
								spr_u.ᜃ().ᜂ(new spr\u2213(0, 0));
								spr_u1BED.ᜂ().ᜂ(spr_u);
								spr_u1BED.ᜂ().ᜂ(new spr\u1F5C(0, 0));
								num5++;
								num = 13;
								continue;
								IL_7D7:
								spr_u19F.ᜁ().ᜂ(new spr\u17D3(447, 1114128U));
								num = 4;
								continue;
								IL_B0C:
								num = 3;
								continue;
								IL_2ED:
								goto IL_B0C;
								IL_B35:
								num = 18;
							}
							IL_C48:
							goto IL_E47;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable5 = enumerator5 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable5 != null)
										{
											num = 2;
											continue;
										}
										goto IL_C95;
									case 1:
										goto IL_C93;
									case 2:
										disposable5.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_C93:
							IL_C95:;
						}
						return;
						IL_E47:
						num7 = 0;
						num = 7;
						continue;
						IL_E5B:
						spr_u1BED = new spr\u1BED();
						spr_u = null;
						spr_u19F = null;
						num5 = 0;
						enumerator5 = notes.GetEnumerator();
						num = 16;
						continue;
						IL_10ED:
						num6 = 0;
						num = 14;
						continue;
						IL_1353:
						num4 = 0;
						enumerator2 = charts.GetEnumerator();
						num = 3;
					}
				}
				return;
				IL_1392:
				return;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00087A9C File Offset: 0x00086A9C
		internal void ᜀ(string A_0, int A_1, int A_2)
		{
			int a_ = 15;
			for (;;)
			{
				switch (0)
				{
				default:
				{
					Graphics graphics = Graphics.FromHwnd((IntPtr)0);
					try
					{
						for (;;)
						{
							Font font = null;
							int num = 8;
							for (;;)
							{
								int num2;
								switch (num)
								{
								case 0:
								{
									spr\u17ED spr_u17ED;
									if (spr_u17ED != null)
									{
										num = 9;
										continue;
									}
									goto IL_148;
								}
								case 1:
									font = spr\u2059.ᜀ();
									num = 3;
									continue;
								case 2:
									goto IL_1E0;
								case 3:
									goto IL_AB;
								case 4:
									if (num2 > (int)this.ᜎ[A_2])
									{
										num = 10;
										continue;
									}
									goto IL_1D4;
								case 5:
								{
									spr\u17ED spr_u17ED;
									if (spr_u17ED.ᜅ() != null)
									{
										num = 12;
										continue;
									}
									goto IL_148;
								}
								case 6:
									goto IL_1D4;
								case 7:
									goto IL_AB;
								case 8:
								{
									if (A_1 == 15)
									{
										num = 1;
										continue;
									}
									spr\u17ED spr_u17ED = this.ᜅ.ᜁ((ushort)A_1);
									num = 0;
									continue;
								}
								case 9:
									num = 5;
									continue;
								case 10:
									this.ᜎ[A_2] = num2;
									num = 6;
									continue;
								case 11:
									goto IL_AB;
								case 12:
								{
									font = Font.FromHfont(CellExport.GetStockObject(0));
									Color black = Color.Black;
									spr\u17ED spr_u17ED;
									spr_u17ED.ᜅ().AssignTo(ref font, out black);
									num = 11;
									continue;
								}
								}
								break;
								IL_AB:
								int num3 = (int)graphics.MeasureString(A_0 + 'I', font).Width;
								font = spr\u2059.ᜀ();
								int num4 = (int)graphics.MeasureString(HyperlinksCollectionEditor.b("ᬪ", a_), font).Width;
								double num5 = (double)(num3 / num4);
								num2 = (int)num5;
								num = 4;
								continue;
								IL_148:
								font = spr\u2059.ᜀ();
								num = 7;
								continue;
								IL_1D4:
								num = 2;
							}
						}
						IL_1E0:;
					}
					finally
					{
						graphics.Dispose();
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					goto Block_2;
				}
				}
			}
			Block_2:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00087CDC File Offset: 0x00086CDC
		internal new void ᜁ(string A_0, int A_1, int A_2)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 8;
				int num6;
				for (;;)
				{
					double num3;
					switch (num)
					{
					case 0:
						goto IL_8E;
					case 1:
						if (this.SheetOptions.TitlesFormat.Rotation > 180)
						{
							num = 12;
							continue;
						}
						goto IL_3B6;
					case 2:
						try
						{
							for (;;)
							{
								Font font = null;
								num = 7;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1D1;
									case 1:
										num = 3;
										continue;
									case 2:
										goto IL_294;
									case 3:
									{
										spr\u17ED spr_u17ED;
										if (spr_u17ED.ᜅ() != null)
										{
											num = 4;
											continue;
										}
										goto IL_1BD;
									}
									case 4:
									{
										font = Font.FromHfont(CellExport.GetStockObject(0));
										Color black = Color.Black;
										spr\u17ED spr_u17ED;
										spr_u17ED.ᜅ().AssignTo(ref font, out black);
										num = 0;
										continue;
									}
									case 5:
										font = spr\u2059.ᜀ();
										num = 6;
										continue;
									case 6:
										goto IL_1D1;
									case 7:
									{
										if (A_1 == 15)
										{
											num = 5;
											continue;
										}
										spr\u17ED spr_u17ED = this.ᜅ.ᜁ((ushort)A_1);
										num = 9;
										continue;
									}
									case 8:
										goto IL_1D1;
									case 9:
									{
										spr\u17ED spr_u17ED;
										if (spr_u17ED != null)
										{
											num = 1;
											continue;
										}
										goto IL_1BD;
									}
									}
									break;
									IL_1BD:
									font = spr\u2059.ᜀ();
									num = 8;
									continue;
									IL_1D1:
									Graphics graphics;
									int num2 = (int)((double)graphics.MeasureString(A_0 + 'I', font).Width * Math.Cos(num3 * 3.141592653589793 / 180.0));
									font = spr\u2059.ᜀ();
									int num4 = (int)graphics.MeasureString(HyperlinksCollectionEditor.b("ⴜ", a_), font).Width;
									double num5 = (double)(num2 / num4);
									num6 = (int)(num5 * (2.0 - (1.0 - Math.Sin(num3 * 3.141592653589793 / 180.0))) + (double)(graphics.MeasureString(A_0, font).Height / (float)num4)) + 1;
									num = 2;
								}
							}
							IL_294:
							goto IL_422;
						}
						finally
						{
							Graphics graphics;
							graphics.Dispose();
						}
						goto IL_2A0;
					case 3:
						if (this.SheetOptions.TitlesFormat.Rotation > 90)
						{
							num = 13;
							continue;
						}
						goto IL_A3;
					case 4:
						num3 = (double)(this.SheetOptions.TitlesFormat.Rotation - 100);
						num = 16;
						continue;
					case 5:
						if (this.SheetOptions.TitlesFormat.Rotation != 255)
						{
							num = 4;
							continue;
						}
						goto IL_A3;
					case 6:
						IL_D9:
						num = 11;
						continue;
					case 7:
						goto IL_9E;
					case 9:
						goto IL_2D2;
					case 10:
						if (this.SheetOptions.TitlesFormat.Rotation != 90)
						{
							if (true)
							{
							}
							num = 6;
							continue;
						}
						goto IL_8E;
					case 11:
					{
						if (this.SheetOptions.TitlesFormat.Rotation == 255)
						{
							num = 0;
							continue;
						}
						Graphics graphics = Graphics.FromHwnd((IntPtr)0);
						num = 2;
						continue;
					}
					case 12:
						goto IL_2A0;
					case 13:
						num = 5;
						continue;
					case 14:
						num = 1;
						continue;
					case 15:
						if (this.SheetOptions.TitlesFormat.Rotation != 255)
						{
							num = 9;
							continue;
						}
						goto IL_3B6;
					case 16:
						goto IL_A3;
					}
					if (this.SheetOptions.TitlesFormat.Rotation >= 0)
					{
						num = 14;
						continue;
					}
					return;
					IL_8E:
					num6 = this.ᜱ;
					num = 7;
					continue;
					IL_A3:
					num6 = 0;
					num = 10;
					continue;
					IL_3B6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D9;
					}
					if (false)
					{
					}
					num3 = (double)this.SheetOptions.TitlesFormat.Rotation;
					num = 3;
					continue;
					IL_2A0:
					num = 15;
				}
				IL_9E:
				goto IL_422;
				IL_2D2:
				return;
				IL_422:
				this.ᜎ[A_2] = num6;
				return;
			}
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0008813C File Offset: 0x0008713C
		private void ᜡ()
		{
			switch (0)
			{
			default:
			{
				long position = this.\u1712.Position;
				try
				{
					IEnumerator enumerator = this.ᜮ.GetEnumerator();
					try
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								spr\u193C spr_u193C = (spr\u193C)enumerator.Current;
								DataRange dataRange = new DataRange();
								this.\u1712.Seek((long)spr_u193C.ᜁ, SeekOrigin.Begin);
								this.\u1718[spr_u193C.ᜀ].ColumnList.ᜀ(spr_u193C.ᜂ, dataRange);
								sprᰐ a_;
								a_.ᜀ = 59;
								a_.ᜁ.ᜀ = (ushort)spr_u193C.ᜀ;
								a_.ᜁ.ᜁ = (ushort)(dataRange.RowX - 1);
								a_.ᜁ.ᜂ = (ushort)(dataRange.RowY - 1);
								a_.ᜁ.ᜃ = (ushort)(dataRange.ColX - 1);
								a_.ᜁ.ᜄ = (ushort)(dataRange.ColY - 1);
								byte[] array = sprᰐ.ᜀ(a_);
								this.\u1712.ᜁ(array, array.Length);
								num = 2;
								continue;
							}
							case 3:
								num = 4;
								continue;
							case 4:
								goto IL_177;
							}
							IL_145:
							num = 0;
							continue;
							goto IL_145;
						}
						IL_177:;
					}
					finally
					{
						IDisposable disposable;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							IL_1B3:
							int num = 1;
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
									goto IL_1DD;
								case 2:
									goto IL_1DB;
								}
								goto IL_1AA;
							}
							IL_1DB:
							IL_1DD:
							goto EndFinally_7;
						}
						default:
							if (false)
							{
							}
							break;
						}
						IL_1AA:
						disposable = (enumerator as IDisposable);
						goto IL_1B3;
						EndFinally_7:;
					}
				}
				finally
				{
					if (true)
					{
					}
					this.\u1712.Seek(position, SeekOrigin.Begin);
				}
				return;
			}
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00088374 File Offset: 0x00087374
		private void ᜠ()
		{
			long position;
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
				switch (0)
				{
				default:
				{
					position = this.\u1712.Position;
					this.\u1712.Seek((long)this.ᜏ, SeekOrigin.Begin);
					IEnumerator enumerator = this.ᜎ.GetEnumerator();
					try
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								int num2 = (int)enumerator.Current;
								this.\u1712.Seek(8L, SeekOrigin.Current);
								ushort value = (ushort)(num2 * 256);
								byte[] bytes = BitConverter.GetBytes(value);
								this.\u1712.ᜁ(bytes, bytes.Length);
								this.\u1712.Seek(5L, SeekOrigin.Current);
								num = 4;
								continue;
							}
							case 2:
								num = 3;
								continue;
							case 3:
								goto IL_115;
							}
							IL_E9:
							num = 0;
							continue;
							goto IL_E9;
						}
						IL_115:;
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
										num = 2;
										continue;
									}
									goto IL_15F;
								case 1:
									goto IL_15D;
								case 2:
									disposable.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_15D:
						IL_15F:;
					}
					break;
				}
				}
				break;
			}
			this.\u1712.Seek(position, SeekOrigin.Begin);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00088500 File Offset: 0x00087500
		internal void ᜀ(int A_0, int A_1)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 19;
				int num2;
				for (;;)
				{
					if (true)
					{
					}
					int num3;
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
						spr\u17ED spr_u17ED;
						Graphics graphics;
						switch (num)
						{
						case 0:
							num = 20;
							continue;
						case 1:
							num = 12;
							continue;
						case 2:
							try
							{
								Font font = Font.FromHfont(CellExport.GetStockObject(0));
								Color black = Color.Black;
								spr_u17ED.ᜅ().AssignTo(ref font, out black);
								CellExport.ᜀ ᜀ = default(CellExport.ᜀ);
								ᜀ.ᜀ();
								CellExport.GetOutlineTextMetrics(graphics.GetHdc(), sizeof(CellExport.ᜀ), ref ᜀ);
								num2 = ᜀ.ᜁ.ᜀ - ᜀ.ᜁ.ᜃ - ᜀ.ᜁ.ᜂ + Math.Max(ᜀ.ᜁ.ᜃ, ᜀ.ᜁ.ᜂ) * 2 + ᜀ.ᜁ.ᜄ + 1;
								num2 *= 1440 / ᜀ.ᜁ.ᜊ;
								goto IL_2C2;
							}
							finally
							{
								graphics.Dispose();
							}
							goto IL_22D;
							IL_2C2:
							num3 = 0;
							num = 9;
							continue;
						case 3:
							return;
						case 4:
							if (spr_u17ED.ᜅ().Size == 10f)
							{
								num = 0;
								continue;
							}
							goto IL_380;
						case 5:
							num = 6;
							continue;
						case 6:
							if (string.Compare(spr_u17ED.ᜅ().Name, HyperlinksCollectionEditor.b("椧堩䔫伭尯", a_), true) == 0)
							{
								num = 13;
								continue;
							}
							goto IL_380;
						case 7:
							goto IL_13B;
						case 8:
							if (spr_u17ED.ᜅ() != null)
							{
								num = 5;
								continue;
							}
							return;
						case 9:
							if (this.ᜐ.ContainsKey(A_1))
							{
								num = 15;
								continue;
							}
							goto IL_22D;
						case 10:
							num = 8;
							continue;
						case 11:
							if (spr_u17ED != null)
							{
								num = 10;
								continue;
							}
							return;
						case 12:
							if (!spr_u17ED.ᜅ().Strikeout)
							{
								num = 3;
								continue;
							}
							goto IL_380;
						case 13:
							num = 4;
							continue;
						case 14:
							return;
						case 15:
							num3 = (int)this.ᜐ[A_1];
							num = 17;
							continue;
						case 16:
							num = 18;
							continue;
						case 17:
							goto IL_127;
						case 18:
							if (!spr_u17ED.ᜅ().Italic)
							{
								num = 1;
								continue;
							}
							goto IL_380;
						case 20:
							if (!spr_u17ED.ᜅ().Bold)
							{
								num = 16;
								continue;
							}
							goto IL_380;
						case 21:
							goto IL_250;
						}
						if (A_0 == 15)
						{
							num = 14;
							continue;
						}
						spr_u17ED = this.ᜅ.ᜁ((ushort)A_0);
						num = 11;
						continue;
						IL_22D:
						this.ᜐ.Add(A_1, num2);
						num = 21;
						continue;
						IL_380:
						num2 = 0;
						graphics = Graphics.FromHwnd((IntPtr)0);
						num = 2;
						continue;
					}
					}
					IL_127:
					if (num2 <= num3)
					{
						return;
					}
					num = 7;
				}
				return;
				IL_13B:
				this.ᜐ[A_1] = num2;
				return;
				IL_250:
				return;
			}
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000888BC File Offset: 0x000878BC
		private new void ᜁ(ushort A_0, ushort A_1)
		{
			int a_ = 12;
			for (;;)
			{
				byte[] bytes = BitConverter.GetBytes(A_0);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u1712.ᜁ(bytes, bytes.Length) != bytes.Length)
						{
							num = 3;
							continue;
						}
						num = 6;
						continue;
					case 1:
						goto IL_5F;
					case 2:
						if (this.\u1712.ᜁ(this.\u1713, (int)A_1) != (int)A_1)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						goto IL_13A;
					case 4:
						goto IL_89;
					case 5:
						if (this.\u1712.ᜁ(bytes, bytes.Length) != bytes.Length)
						{
							num = 1;
							continue;
						}
						if (true)
						{
						}
						bytes = BitConverter.GetBytes(A_1);
						num = 0;
						continue;
					case 6:
						if (A_1 > 0)
						{
							num = 7;
							continue;
						}
						return;
					case 7:
						num = 2;
						continue;
					}
					break;
				}
			}
			IL_5F:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("愧䐩娫伭尯嬱倳礵䠷弹主弽㐿⭁⭃⡅ᝇ౉╋≍㕏Ց♓㽕ⱗ㽙", a_)));
			IL_89:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A7:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("愧䐩娫伭尯嬱倳礵䠷弹主弽㐿⭁⭃⡅ᝇ౉╋≍㕏Ց♓㽕ⱗ㽙", a_)));
			default:
				if (false)
				{
				}
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("愧䐩娫伭尯嬱倳礵䠷弹主弽㐿⭁⭃⡅ᝇ౉╋≍㕏Ց♓㽕ⱗ㽙", a_)));
			}
			IL_13A:
			goto IL_A7;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00088A34 File Offset: 0x00087A34
		private void ᜀ(ushort A_0, ushort A_1)
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
			byte[] bytes = BitConverter.GetBytes(A_1);
			Array.Copy(bytes, this.\u1713, bytes.Length);
			this.ᜁ(A_0, (ushort)bytes.Length);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00088A90 File Offset: 0x00087A90
		private unsafe void ᜈ(ushort A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1CD1);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						IL_A2:
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 3;
							continue;
							break;
						}
					}
					case 1:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_63;
					}
					case 2:
						goto IL_63;
					case 3:
						goto IL_61;
					case 4:
						goto IL_95;
					case 5:
						num2 = 0;
						continue;
					}
					break;
					IL_63:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A2;
					default:
						if (false)
						{
						}
						ptr = null;
						num2 = 4;
						break;
					}
				}
			}
			IL_61:
			IL_95:
			((spr\u1CD1*)ptr)->ᜀ = 1536;
			((spr\u1CD1*)ptr)->ᜁ = A_0;
			((spr\u1CD1*)ptr)->ᜂ = 3515;
			((spr\u1CD1*)ptr)->ᜃ = 1996;
			((spr\u1CD1*)ptr)->ᜄ = 0;
			((spr\u1CD1*)ptr)->ᜅ = 518;
			ptr = null;
			this.ᜁ(2057, num);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00088BAC File Offset: 0x00087BAC
		private void \u171F()
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				ushort num;
				for (;;)
				{
					num = 112;
					int num2 = 0;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_5C;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_86;
							default:
								if (false)
								{
								}
								goto IL_5C;
							}
							break;
						case 2:
							goto IL_84;
						case 3:
							if (num2 >= (int)num)
							{
								num3 = 2;
								continue;
							}
							goto IL_86;
						}
						break;
						IL_5C:
						if (true)
						{
						}
						num3 = 3;
						continue;
						IL_86:
						this.\u1713[num2] = 32;
						num2++;
						num3 = 0;
					}
				}
				IL_84:
				this.\u1713[0] = 1;
				this.\u1713[1] = 0;
				this.\u1713[2] = 0;
				string s = string.Format(HyperlinksCollectionEditor.b("儩ᰫ匭ု䤱Գ䬵", a_), HyperlinksCollectionEditor.b("礩尫䜭䈯圱ᨳ爵夷丹崻笽㠿㉁⭃㑅㱇", a_), HyperlinksCollectionEditor.b("ᠩȫḭį", a_));
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				Array.Copy(bytes, 0, this.\u1713, 3, bytes.Length);
				this.ᜁ(92, num);
				return;
			}
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00088CCC File Offset: 0x00087CCC
		private unsafe void \u171E()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(sprᬙ);
				Array.Clear(this.\u1713, 0, (int)num);
				if (true)
				{
				}
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_6B;
					}
					case 1:
						num2 = 4;
						continue;
					case 2:
						goto IL_69;
					case 3:
						goto IL_6B;
					case 4:
					{
						IL_A2:
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 3;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 2;
							continue;
							break;
						}
					}
					case 5:
						goto IL_95;
					}
					break;
					IL_6B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A2;
					default:
						if (false)
						{
						}
						ptr = null;
						num2 = 5;
						break;
					}
				}
			}
			IL_69:
			IL_95:
			((sprᬙ*)ptr)->ᜀ = 120;
			((sprᬙ*)ptr)->ᜁ = 120;
			((sprᬙ*)ptr)->ᜂ = 23820;
			((sprᬙ*)ptr)->ᜃ = 15720;
			((sprᬙ*)ptr)->ᜄ = 56;
			((sprᬙ*)ptr)->ᜅ = 0;
			((sprᬙ*)ptr)->ᜆ = 0;
			((sprᬙ*)ptr)->ᜇ = 1;
			((sprᬙ*)ptr)->ᜈ = 600;
			ptr = null;
			this.ᜁ(61, num);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00088DFC File Offset: 0x00087DFC
		private new void \u171D()
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
			this.ᜁ(10, 0);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00088E40 File Offset: 0x00087E40
		private new void \u171C()
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
			double value = 0.001;
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, this.\u1713, bytes.Length);
			this.ᜁ(16, (ushort)bytes.Length);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00088EA8 File Offset: 0x00087EA8
		private void \u171B()
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
			ushort num = 8;
			Array.Clear(this.\u1713, 0, (int)num);
			this.ᜁ(128, num);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00088F00 File Offset: 0x00087F00
		private unsafe void ᜇ(ushort A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u2405);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A9:
					if (true)
					{
					}
					num2 = 3;
					break;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_97;
					case 1:
						num2 = 5;
						continue;
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_7F;
					}
					case 3:
						goto IL_7F;
					case 4:
						goto IL_7D;
					case 5:
					{
						byte[] u;
						if (u.Length == 0)
						{
							goto IL_A9;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 4;
							continue;
							break;
						}
					}
					}
					break;
					IL_7F:
					ptr = null;
					num2 = 0;
				}
			}
			IL_7D:
			IL_97:
			((spr\u2405*)ptr)->ᜀ = A_0;
			ptr = null;
			this.ᜁ(85, num);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00088FE0 File Offset: 0x00087FE0
		private unsafe void ᜀ(double A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				if (true)
				{
				}
				num = (ushort)sizeof(spr\u2023);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B4:
					num2 = 2;
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							goto IL_B4;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 3;
							continue;
							break;
						}
					}
					case 1:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_94;
					}
					case 2:
						goto IL_94;
					case 3:
						goto IL_88;
					case 4:
						num2 = 0;
						continue;
					case 5:
						goto IL_A2;
					}
					break;
					IL_94:
					ptr = null;
					num2 = 5;
				}
			}
			IL_88:
			IL_A2:
			((spr\u2023*)ptr)->ᜀ = 0;
			((spr\u2023*)ptr)->ᜁ = (ushort)(A_0 * 20.0);
			ptr = null;
			this.ᜁ(549, num);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000890D8 File Offset: 0x000880D8
		private void ᜀ(ushort A_0, string A_1)
		{
			int a_ = 0;
			ushort num;
			for (;;)
			{
				num = 0;
				byte[] bytes = Encoding.ASCII.GetBytes(A_1);
				int num2 = bytes.Length;
				int num3 = 5;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						num = (ushort)(num2 + 3);
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						}
						if (false)
						{
						}
						num3 = 1;
						continue;
					case 1:
						if ((int)num > this.\u1713.Length)
						{
							num3 = 4;
							continue;
						}
						Array.Clear(this.\u1713, 0, (int)num);
						this.\u1713[0] = (byte)num2;
						this.\u1713[1] = 0;
						this.\u1713[2] = 0;
						Array.Copy(bytes, 0, this.\u1713, 3, num2);
						num3 = 3;
						continue;
					case 2:
						goto IL_120;
					case 3:
						goto IL_97;
					case 4:
						goto IL_111;
					case 5:
						goto IL_43;
					}
					break;
					IL_43:
					if (num2 > 0)
					{
						num3 = 0;
					}
					else
					{
						num = 0;
						num3 = 2;
					}
				}
			}
			IL_97:
			goto IL_122;
			IL_111:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("崛氝䜟儡笣戥䤧帩䴫稭弯崱砳夵嘷崹", a_)));
			IL_120:
			IL_122:
			this.ᜁ(A_0, num);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00089210 File Offset: 0x00088210
		private new void ᜁ(int A_0)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				spr\u1A1D spr_u1A1D;
				int a_2;
				for (;;)
				{
					spr_u1A1D = new spr\u1A1D(null);
					Hashtable hashtable = new Hashtable();
					a_2 = -1;
					int num = 2;
					for (;;)
					{
						Graphics graphics;
						switch (num)
						{
						case 0:
							try
							{
								for (;;)
								{
									IL_CB:
									Font font = spr\u2059.ᜀ();
									int num2 = (int)graphics.MeasureString(HyperlinksCollectionEditor.b("Ἦ", a_), font).Width;
									int num3 = 0;
									num = 6;
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
												goto IL_2BB;
											case 1:
												a_2 = num3 + (int)this.Sheets[A_0].StartDataCol;
												num = 4;
												continue;
											case 2:
												goto IL_36B;
											case 3:
											{
												IEnumerator enumerator = hashtable.Keys.GetEnumerator();
												num = 11;
												continue;
											}
											case 4:
												goto IL_35A;
											case 5:
												goto IL_1A1;
											case 6:
												goto IL_2BB;
											case 7:
												if (!hashtable.ContainsKey((ushort)(num3 + (int)this.Sheets[A_0].StartDataCol + 1)))
												{
													num = 8;
													continue;
												}
												goto IL_1A1;
											case 8:
											{
												sprᥔ a_3 = new sprᥔ((ushort)(num3 + (int)this.Sheets[A_0].StartDataCol), (ushort)Math.Round((double)((this.Sheets[A_0].ColumnsExport[num3].Width + 1 + 1 / num2) * 256)), 15, 0);
												spr_u1A1D.ᜀ(a_3);
												num = 5;
												continue;
											}
											case 9:
												if (num3 >= this.Sheets[A_0].ColumnsExport.Count)
												{
													num = 3;
													continue;
												}
												num = 7;
												continue;
											case 10:
												if (num3 == 0)
												{
													num = 1;
													continue;
												}
												goto IL_18A;
											case 11:
												try
												{
													num = 3;
													for (;;)
													{
														switch (num)
														{
														case 0:
															goto IL_26D;
														case 1:
														{
															IEnumerator enumerator;
															if (!enumerator.MoveNext())
															{
																num = 4;
																continue;
															}
															ushort num4 = (ushort)enumerator.Current;
															sprᥔ a_4 = new sprᥔ(num4 - 1, (ushort)Math.Round((double)(((int)((ushort)hashtable[num4] + 1) + 1 / num2) * 256)), 15, 0);
															spr_u1A1D.ᜀ(a_4);
															num = 2;
															continue;
														}
														case 4:
															num = 0;
															continue;
														}
														IL_241:
														num = 1;
														continue;
														goto IL_241;
													}
													IL_26D:
													goto IL_35F;
												}
												finally
												{
													for (;;)
													{
														IEnumerator enumerator;
														IDisposable disposable = enumerator as IDisposable;
														num = 2;
														for (;;)
														{
															switch (num)
															{
															case 0:
																goto IL_2B8;
															case 1:
																disposable.Dispose();
																num = 0;
																continue;
															case 2:
																if (disposable != null)
																{
																	num = 1;
																	continue;
																}
																goto IL_2BA;
															}
															break;
														}
													}
													IL_2B8:
													IL_2BA:;
												}
												goto IL_2BB;
												IL_35F:
												num = 2;
												continue;
											}
											goto IL_CB;
											IL_1A1:
											num = 10;
											continue;
											IL_2BB:
											num = 9;
											continue;
										}
										IL_18A:
										num3++;
										num = 0;
										continue;
										IL_35A:
										goto IL_18A;
									}
								}
								IL_36B:
								goto IL_3C3;
							}
							finally
							{
								if (true)
								{
								}
								graphics.Dispose();
							}
							goto IL_37C;
						case 1:
							hashtable = (Hashtable)this.Sheets[A_0].ᜯ.Clone();
							num = 3;
							continue;
						case 2:
							if (this.Sheets[A_0].ᜯ != null)
							{
								num = 1;
								continue;
							}
							goto IL_37C;
						case 3:
							goto IL_37C;
						}
						break;
						IL_37C:
						graphics = Graphics.FromHwnd((IntPtr)0);
						num = 0;
					}
				}
				IL_3C3:
				spr_u1A1D.ᜀ();
				spr_u1A1D.ᜀ(this.\u1712, a_2, ref this.ᜏ);
				return;
			}
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00089630 File Offset: 0x00088630
		internal unsafe void ᜀ(WorkSheet A_0, ushort A_1, ushort A_2, ushort A_3, string A_4)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				ushort num;
				for (;;)
				{
					sprᲺ sprᲺ = new sprᲺ(A_4, A_0);
					sprᲺ.ᜄ();
					array = new byte[(int)((ushort)sprᲺ.ᜇ.Length)];
					num = (ushort)(sizeof(spr\u23F3) + sprᲺ.ᜇ.Length);
					Array.Clear(this.\u1713, 0, (int)num);
					Array.Clear(array, 0, sprᲺ.ᜇ.Length);
					int num2 = 0;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 6;
								continue;
							}
							goto IL_16C;
						}
						case 1:
							if (num3 >= sprᲺ.ᜇ.Length)
							{
								num2 = 4;
								continue;
							}
							array[num3] = (byte)sprᲺ.ᜇ[num3];
							num3++;
							num2 = 2;
							continue;
						case 2:
							goto IL_140;
						case 3:
							goto IL_140;
						case 4:
							goto IL_167;
						case 5:
							goto IL_16C;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_279;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num2 = 7;
								continue;
							}
							break;
						case 7:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 5;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 8;
								continue;
								break;
							}
						}
						case 8:
							goto IL_17D;
						case 9:
							goto IL_17D;
						}
						break;
						IL_140:
						num2 = 1;
						continue;
						IL_16C:
						byte* ptr = null;
						num2 = 9;
						continue;
						IL_279:
						num2 = 3;
						continue;
						IL_17D:
						((spr\u23F3*)ptr)->ᜀ = A_1;
						((spr\u23F3*)ptr)->ᜁ = A_2;
						((spr\u23F3*)ptr)->ᜂ = A_3;
						((spr\u23F3*)ptr)->ᜃ = (byte)sprᲺ.ᜅ[0];
						((spr\u23F3*)ptr)->ᜄ = (byte)sprᲺ.ᜅ[1];
						((spr\u23F3*)ptr)->ᜅ = (byte)sprᲺ.ᜅ[2];
						((spr\u23F3*)ptr)->ᜆ = (byte)sprᲺ.ᜅ[3];
						((spr\u23F3*)ptr)->ᜇ = (byte)sprᲺ.ᜅ[4];
						((spr\u23F3*)ptr)->ᜈ = (byte)sprᲺ.ᜅ[5];
						((spr\u23F3*)ptr)->ᜉ = (byte)sprᲺ.ᜅ[6];
						((spr\u23F3*)ptr)->ᜊ = (byte)sprᲺ.ᜅ[7];
						((spr\u23F3*)ptr)->ᜋ = (ushort)sprᲺ.ᜄ;
						((spr\u23F3*)ptr)->ᜌ = 0;
						((spr\u23F3*)ptr)->\u170D = 0;
						((spr\u23F3*)ptr)->ᜎ = 0;
						((spr\u23F3*)ptr)->ᜏ = 0;
						((spr\u23F3*)ptr)->ᜐ = (ushort)sprᲺ.ᜇ.Length;
						ptr = null;
						num3 = 0;
						goto IL_279;
					}
				}
				IL_167:
				Array.Copy(array, 0, this.\u1713, sizeof(spr\u23F3), array.Length);
				this.ᜁ(6, num);
				return;
			}
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000898E8 File Offset: 0x000888E8
		internal unsafe void ᜀ(ushort A_0, ushort A_1, ushort A_2, string A_3)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(sprᰕ);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B4:
					num2 = 4;
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							goto IL_B4;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 5;
							continue;
							break;
						}
					}
					case 1:
					{
						if (true)
						{
						}
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_94;
					}
					case 2:
						goto IL_A2;
					case 3:
						num2 = 0;
						continue;
					case 4:
						goto IL_94;
					case 5:
						goto IL_88;
					}
					break;
					IL_94:
					ptr = null;
					num2 = 2;
				}
			}
			IL_88:
			IL_A2:
			((sprᰕ*)ptr)->ᜀ = A_0;
			((sprᰕ*)ptr)->ᜁ = A_1;
			((sprᰕ*)ptr)->ᜂ = A_2;
			((sprᰕ*)ptr)->ᜃ = this.ᜉ.ᜀ(A_3);
			ptr = null;
			this.ᜁ(253, num);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000899F4 File Offset: 0x000889F4
		internal unsafe void ᜀ(ushort A_0, ushort A_1, ushort A_2, bool A_3)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1DF6);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_E2;
					case 1:
						goto IL_BF;
					case 2:
					{
						IL_41:
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_74;
					}
					case 3:
						if (true)
						{
						}
						goto IL_74;
					case 4:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 3;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					case 5:
						num2 = 4;
						continue;
					case 6:
						goto IL_BF;
					}
					break;
					IL_BF:
					((spr\u1DF6*)ptr)->ᜀ = A_0;
					((spr\u1DF6*)ptr)->ᜁ = A_1;
					((spr\u1DF6*)ptr)->ᜂ = A_2;
					num2 = 0;
					continue;
					IL_74:
					ptr = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					}
					if (false)
					{
					}
					num2 = 6;
				}
			}
			IL_E2:
			((spr\u1DF6*)ptr)->ᜃ = (A_3 ? 1 : 0);
			((spr\u1DF6*)ptr)->ᜄ = 0;
			ptr = null;
			this.ᜁ(517, num);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00089B10 File Offset: 0x00088B10
		internal unsafe void ᜀ(ushort A_0, ushort A_1, ushort A_2, double A_3)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1BE8);
				Array.Clear(this.\u1713, 0, (int)num);
				if (true)
				{
				}
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B4:
					num2 = 0;
					break;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_94;
					case 1:
						goto IL_88;
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_94;
					}
					case 3:
					{
						byte[] u;
						if (u.Length == 0)
						{
							goto IL_B4;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					case 4:
						num2 = 3;
						continue;
					case 5:
						goto IL_A2;
					}
					break;
					IL_94:
					ptr = null;
					num2 = 5;
				}
			}
			IL_88:
			IL_A2:
			((spr\u1BE8*)ptr)->ᜀ = A_0;
			((spr\u1BE8*)ptr)->ᜁ = A_1;
			((spr\u1BE8*)ptr)->ᜂ = A_2;
			((spr\u1BE8*)ptr)->ᜃ = A_3;
			ptr = null;
			this.ᜁ(515, num);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00089C10 File Offset: 0x00088C10
		internal new unsafe void ᜁ(ushort A_0, ushort A_1, ushort A_2)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_3C:
				num = (ushort)sizeof(sprᜯ);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 1;
				for (;;)
				{
					byte[] u;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							num2 = 3;
							continue;
						case 1:
							goto IL_59;
						case 2:
							goto IL_8A;
						case 3:
							if (u.Length == 0)
							{
								num2 = 2;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 4;
								continue;
								break;
							}
						case 4:
							goto IL_88;
						case 5:
							goto IL_98;
						}
						goto IL_3C;
					}
					IL_59:
					if (true)
					{
					}
					if ((u = this.\u1713) != null)
					{
						num2 = 0;
						continue;
					}
					IL_8A:
					ptr = null;
					num2 = 5;
				}
			}
			IL_88:
			IL_98:
			((sprᜯ*)ptr)->ᜀ = A_0;
			((sprᜯ*)ptr)->ᜁ = A_1;
			((sprᜯ*)ptr)->ᜂ = A_2;
			ptr = null;
			this.ᜁ(513, num);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00089D08 File Offset: 0x00088D08
		internal unsafe void ᜀ(ushort A_0, ushort A_1, ushort A_2, ushort A_3, Aggregate A_4, ushort A_5)
		{
			switch (0)
			{
			default:
			{
				ushort num;
				byte* ptr;
				for (;;)
				{
					num = (ushort)sizeof(sprᮻ);
					Array.Clear(this.\u1713, 0, (int)num);
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_157;
						case 1:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_B2;
						}
						case 2:
							goto IL_B2;
						case 3:
							num2 = 11;
							continue;
						case 4:
							num2 = 12;
							continue;
						case 5:
							switch (A_4)
							{
							case Aggregate.Sum:
								((sprᮻ*)ptr)->ᜐ = 4;
								num2 = 8;
								continue;
							case Aggregate.Avg:
								((sprᮻ*)ptr)->ᜐ = 5;
								num2 = 9;
								continue;
							case Aggregate.Min:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_131;
								default:
									if (false)
									{
									}
									((sprᮻ*)ptr)->ᜐ = 6;
									num2 = 7;
									continue;
								}
								break;
							case Aggregate.Max:
								((sprᮻ*)ptr)->ᜐ = 7;
								num2 = 6;
								continue;
							default:
								num2 = 4;
								continue;
							}
							break;
						case 6:
							goto IL_14A;
						case 7:
							goto IL_F6;
						case 8:
							goto IL_131;
						case 9:
							goto IL_9C;
						case 10:
							goto IL_157;
						case 11:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 2;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 0;
								continue;
								break;
							}
						}
						case 12:
							goto IL_AD;
						}
						break;
						IL_B2:
						ptr = null;
						num2 = 10;
						continue;
						IL_157:
						((sprᮻ*)ptr)->ᜀ = A_0;
						((sprᮻ*)ptr)->ᜁ = A_1;
						((sprᮻ*)ptr)->ᜂ = A_5;
						((sprᮻ*)ptr)->ᜃ = 0.0;
						((sprᮻ*)ptr)->ᜄ = 2;
						((sprᮻ*)ptr)->ᜅ = 0;
						((sprᮻ*)ptr)->ᜆ = 13;
						((sprᮻ*)ptr)->ᜇ = 37;
						((sprᮻ*)ptr)->ᜈ = A_2;
						((sprᮻ*)ptr)->ᜉ = A_3;
						((sprᮻ*)ptr)->ᜊ = (byte)A_1;
						((sprᮻ*)ptr)->ᜋ = 192;
						((sprᮻ*)ptr)->ᜌ = (byte)A_1;
						((sprᮻ*)ptr)->\u170D = 192;
						((sprᮻ*)ptr)->ᜎ = 66;
						((sprᮻ*)ptr)->ᜏ = 1;
						num2 = 5;
					}
				}
				IL_9C:
				IL_AD:
				IL_F6:
				IL_131:
				goto IL_23F;
				IL_14A:
				if (true)
				{
				}
				IL_23F:
				ptr = null;
				this.ᜁ(6, num);
				return;
			}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00089F60 File Offset: 0x00088F60
		private void ᜀ(MergedCellList A_0)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				ushort num4;
				for (;;)
				{
					int num = 0;
					IEnumerator enumerator = A_0.GetEnumerator();
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							if (num == 0)
							{
								num2 = 3;
								continue;
							}
							goto IL_B6;
						case 1:
							goto IL_ED;
						case 2:
							try
							{
								num2 = 3;
								for (;;)
								{
									switch (num2)
									{
									case 0:
									{
										MergedCells mergedCells;
										int value = mergedCells.StartRow - 1;
										byte[] bytes = BitConverter.GetBytes(value);
										int num3;
										Array.Copy(bytes, 0, this.\u1713, num3, 2);
										value = mergedCells.EndRow - 1;
										bytes = BitConverter.GetBytes(value);
										Array.Copy(bytes, 0, this.\u1713, num3 + 2, 2);
										value = mergedCells.StartCol - 1;
										bytes = BitConverter.GetBytes(value);
										Array.Copy(bytes, 0, this.\u1713, num3 + 4, 2);
										value = mergedCells.EndCol - 1;
										bytes = BitConverter.GetBytes(value);
										Array.Copy(bytes, 0, this.\u1713, num3 + 6, 2);
										num3 += sizeof(spr\u20F2);
										num2 = 1;
										continue;
									}
									case 2:
										num2 = 6;
										continue;
									case 4:
									{
										MergedCells mergedCells;
										if (mergedCells.IsCorrect())
										{
											num2 = 0;
											continue;
										}
										break;
									}
									case 5:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 2;
											continue;
										}
										MergedCells mergedCells = (MergedCells)enumerator2.Current;
										num2 = 4;
										continue;
									}
									case 6:
										goto IL_23B;
									}
									IL_125:
									num2 = 5;
									continue;
									goto IL_125;
								}
								IL_23B:
								goto IL_3B6;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 1;
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
											goto IL_288;
										case 2:
											goto IL_286;
										}
										break;
									}
								}
								IL_286:
								IL_288:;
							}
							goto Block_3;
						case 3:
							return;
						case 4:
							goto IL_289;
						case 5:
						{
							if ((int)num4 > this.\u1713.Length)
							{
								num2 = 1;
								continue;
							}
							Array.Clear(this.\u1713, 0, (int)num4);
							int num3 = 0;
							int value = A_0.Count;
							byte[] bytes = BitConverter.GetBytes(value);
							Array.Copy(bytes, 0, this.\u1713, num3, 2);
							num3 += 2;
							IEnumerator enumerator2 = A_0.GetEnumerator();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B6;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						}
						}
						break;
						IL_B6:
						num4 = (ushort)(2 + sizeof(spr\u20F2) * A_0.Count);
						num2 = 5;
						continue;
						Block_3:
						try
						{
							IL_289:
							num2 = 4;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_31A;
								case 1:
								{
									if (!enumerator.MoveNext())
									{
										num2 = 6;
										continue;
									}
									MergedCells mergedCells2 = (MergedCells)enumerator.Current;
									num2 = 3;
									continue;
								}
								case 3:
								{
									MergedCells mergedCells2;
									if (mergedCells2.IsCorrect())
									{
										num2 = 5;
										continue;
									}
									break;
								}
								case 5:
									num++;
									num2 = 2;
									continue;
								case 6:
									num2 = 0;
									continue;
								}
								IL_2F1:
								num2 = 1;
								continue;
								goto IL_2F1;
							}
							IL_31A:
							goto IL_384;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator as IDisposable;
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										disposable2.Dispose();
										num2 = 2;
										continue;
									case 1:
										if (disposable2 != null)
										{
											num2 = 0;
											continue;
										}
										goto IL_364;
									case 2:
										goto IL_362;
									}
									break;
								}
							}
							IL_362:
							IL_364:;
						}
						goto IL_365;
						IL_384:
						num2 = 0;
					}
				}
				IL_ED:
				IL_365:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("弝刟䔡圣礥氧䬩堫伭搯崱嬳稵圷吹嬻", a_)));
				IL_3B6:
				this.ᜁ(229, num4);
				return;
			}
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0008A364 File Offset: 0x00089364
		private unsafe void \u171A()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_3C:
				num = (ushort)sizeof(spr\u2508);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 4;
				for (;;)
				{
					byte[] u;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_98;
						case 1:
							goto IL_8A;
						case 2:
							goto IL_88;
						case 3:
							num2 = 5;
							continue;
						case 4:
							goto IL_59;
						case 5:
							if (u.Length == 0)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 2;
								continue;
								break;
							}
						}
						goto IL_3C;
					}
					IL_59:
					if ((u = this.\u1713) != null)
					{
						num2 = 3;
						continue;
					}
					IL_8A:
					ptr = null;
					num2 = 0;
				}
			}
			IL_88:
			IL_98:
			((spr\u2508*)ptr)->ᜀ = 1718;
			byte* ptr2 = ptr;
			((spr\u2508*)ptr2)->ᜀ = (((spr\u2508*)ptr2)->ᜀ | 2);
			byte* ptr3 = ptr;
			((spr\u2508*)ptr3)->ᜀ = (((spr\u2508*)ptr3)->ᜀ | 4);
			byte* ptr4 = ptr;
			((spr\u2508*)ptr4)->ᜀ = (((spr\u2508*)ptr4)->ᜀ ^ 512);
			byte* ptr5 = ptr;
			((spr\u2508*)ptr5)->ᜀ = (((spr\u2508*)ptr5)->ᜀ | 512);
			((spr\u2508*)ptr)->ᜁ = 0;
			((spr\u2508*)ptr)->ᜂ = 0;
			((spr\u2508*)ptr)->ᜃ = 0;
			((spr\u2508*)ptr)->ᜄ = 0;
			((spr\u2508*)ptr)->ᜅ = 0;
			((spr\u2508*)ptr)->ᜆ = 0;
			ptr = null;
			this.ᜁ(574, num);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0008A4C8 File Offset: 0x000894C8
		private unsafe void ᜀ(int A_0, int A_1, ushort A_2, ushort A_3)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_44:
				num = (ushort)sizeof(spr\u2069);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 4;
				for (;;)
				{
					byte[] u;
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
						switch (num2)
						{
						case 0:
							if (u.Length == 0)
							{
								num2 = 2;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 5;
								continue;
								break;
							}
						case 1:
							num2 = 0;
							continue;
						case 2:
							goto IL_8A;
						case 3:
							goto IL_98;
						case 4:
							goto IL_61;
						case 5:
							goto IL_88;
						}
						goto IL_44;
					}
					IL_61:
					if ((u = this.\u1713) != null)
					{
						num2 = 1;
						continue;
					}
					IL_8A:
					ptr = null;
					num2 = 3;
				}
			}
			IL_88:
			IL_98:
			((spr\u2069*)ptr)->ᜀ = A_0;
			((spr\u2069*)ptr)->ᜁ = A_1;
			((spr\u2069*)ptr)->ᜂ = A_2;
			((spr\u2069*)ptr)->ᜃ = A_3;
			((spr\u2069*)ptr)->ᜄ = 0;
			ptr = null;
			this.ᜁ(512, num);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0008A5D0 File Offset: 0x000895D0
		private unsafe void \u1719()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_3C:
				num = (ushort)sizeof(spr\u17B6);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					byte[] u;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_7D;
						case 1:
							goto IL_7F;
						case 2:
							goto IL_59;
						case 3:
							if (u.Length == 0)
							{
								num2 = 1;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 0;
								continue;
								break;
							}
						case 4:
							num2 = 3;
							continue;
						case 5:
							goto IL_8D;
						}
						goto IL_3C;
					}
					IL_59:
					if ((u = this.\u1713) != null)
					{
						num2 = 4;
						continue;
					}
					IL_7F:
					ptr = null;
					num2 = 5;
				}
			}
			IL_7D:
			IL_8D:
			if (true)
			{
			}
			((spr\u17B6*)ptr)->ᜀ = 3;
			((spr\u17B6*)ptr)->ᜁ = 0;
			((spr\u17B6*)ptr)->ᜂ = 0;
			((spr\u17B6*)ptr)->ᜃ = 0;
			((spr\u17B6*)ptr)->ᜄ = 1;
			((spr\u17B6*)ptr)->ᜅ = 0;
			((spr\u17B6*)ptr)->ᜆ = 0;
			((spr\u17B6*)ptr)->ᜇ = 0;
			((spr\u17B6*)ptr)->ᜈ = 0;
			ptr = null;
			this.ᜁ(29, num);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0008A6F0 File Offset: 0x000896F0
		private void ᜀ(CellHyperlink A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
				for (;;)
				{
					ushort num = (ushort)A_0.Size;
					int num2 = 13;
					for (;;)
					{
						XlsHyperlinkStyle style;
						int num3;
						int num4;
						byte[] bytes;
						string text;
						UnicodeEncoding unicodeEncoding;
						switch (num2)
						{
						case 0:
							goto IL_50C;
						case 1:
							return;
						case 2:
							num2 = 0;
							continue;
						case 3:
							if (A_0.Tip.Length > 0)
							{
								num2 = 9;
								continue;
							}
							return;
						case 4:
							num2 = 12;
							continue;
						case 5:
							switch (style)
							{
							case XlsHyperlinkStyle.URL:
								if (true)
								{
								}
								Array.Copy(spr\u2009.\u180C, 0, this.\u1713, num3, spr\u2009.\u180C.Length);
								num3 += spr\u2009.\u180C.Length;
								num4 = A_0.Target.Length * 2 + 2;
								bytes = BitConverter.GetBytes(num4);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								num3 += bytes.Length;
								text = A_0.Target + '\0';
								bytes = unicodeEncoding.GetBytes(text);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								num2 = 10;
								continue;
							case XlsHyperlinkStyle.LocalFile:
							{
								Array.Copy(spr\u2009.\u180D, 0, this.\u1713, num3, spr\u2009.\u180D.Length);
								num3 += spr\u2009.\u180D.Length;
								num4 = 0;
								bytes = BitConverter.GetBytes(num4);
								Array.Copy(bytes, 0, this.\u1713, num3, 1);
								num3 += 2;
								text = A_0.ShortTarget + '\0';
								num4 = text.Length;
								bytes = BitConverter.GetBytes(num4);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								num3 += bytes.Length;
								bytes = Encoding.ASCII.GetBytes(text);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								Array.Copy(spr\u2009.\u180E, 0, this.\u1713, num3, spr\u2009.\u180E.Length);
								num3 += spr\u2009.\u180E.Length;
								text = A_0.Target;
								num4 = text.Length;
								int value = num4 + 6;
								bytes = BitConverter.GetBytes(value);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								num3 += bytes.Length;
								bytes = BitConverter.GetBytes(num4);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								num3 += bytes.Length;
								Array.Copy(spr\u2009.\u180F, 0, this.\u1713, num3, 2);
								num3 += 2;
								bytes = unicodeEncoding.GetBytes(text);
								Array.Copy(bytes, 0, this.\u1713, num3, num4);
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_77;
								default:
									if (false)
									{
									}
									num2 = 6;
									continue;
								}
								break;
							}
							default:
								num2 = 4;
								continue;
							}
							break;
						case 6:
							goto IL_2FD;
						case 7:
						{
							XlsHyperlinkStyle style2;
							switch (style2)
							{
							case XlsHyperlinkStyle.URL:
							case XlsHyperlinkStyle.LocalFile:
							{
								int value2 = 23;
								bytes = BitConverter.GetBytes(value2);
								Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
								num3 += bytes.Length;
								num2 = 8;
								continue;
							}
							default:
								num2 = 2;
								continue;
							}
							break;
						}
						case 8:
							goto IL_50C;
						case 9:
							num3 = 0;
							sprᮌ.ᜀ(this.\u1713, num3, 2048);
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Row - 1));
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Row - 1));
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Col - 1));
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Col - 1));
							num3 += 2;
							num4 = (A_0.Tip.Length + 1) * 2;
							text = A_0.Tip + '\0';
							bytes = unicodeEncoding.GetBytes(text);
							Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
							num3 += bytes.Length;
							this.ᜁ(2048, (ushort)num3);
							num2 = 1;
							continue;
						case 10:
							goto IL_2FD;
						case 11:
							goto IL_80;
						case 12:
							goto IL_2FD;
						case 13:
						{
							if ((int)num > this.\u1713.Length)
							{
								goto IL_77;
							}
							Array.Clear(this.\u1713, 0, (int)num);
							num3 = 0;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Row - 1));
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Row - 1));
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Col - 1));
							num3 += 2;
							sprᮌ.ᜀ(this.\u1713, num3, (ushort)(A_0.Col - 1));
							num3 += 2;
							Array.Copy(spr\u2009.\u180B, 0, this.\u1713, num3, spr\u2009.\u180B.Length);
							num3 += spr\u2009.\u180B.Length;
							int value3 = 2;
							bytes = BitConverter.GetBytes(value3);
							Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
							num3 += bytes.Length;
							XlsHyperlinkStyle style2 = A_0.Style;
							num2 = 7;
							continue;
						}
						}
						break;
						IL_77:
						num2 = 11;
						continue;
						IL_2FD:
						this.ᜁ(440, num);
						num2 = 3;
						continue;
						IL_50C:
						num4 = A_0.Title.Length + 1;
						bytes = BitConverter.GetBytes(num4);
						Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
						num3 += bytes.Length;
						text = A_0.Title + '\0';
						unicodeEncoding = new UnicodeEncoding();
						bytes = unicodeEncoding.GetBytes(text);
						Array.Copy(bytes, 0, this.\u1713, num3, bytes.Length);
						num3 += bytes.Length;
						style = A_0.Style;
						num2 = 5;
					}
				}
				IL_80:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("怠儢䈤否瘨漪䰬嬮倰朲娴堶甸吺匼堾", a_)));
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0008ACAC File Offset: 0x00089CAC
		private void ᜆ(ushort A_0)
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
			spr\u1DCF a_;
			a_.ᜀ = 93;
			a_.ᜁ = (ushort)(sizeof(spr\u1B3E) + sizeof(spr\u1DCF) * 3);
			byte[] array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 21;
			a_.ᜁ = (ushort)sizeof(spr\u1B3E);
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			spr\u1B3E a_2;
			a_2.ᜀ = 25;
			a_2.ᜁ = A_0;
			a_2.ᜂ = 17;
			a_2.ᜃ.ᜀ = 0L;
			a_2.ᜃ.ᜁ = 0;
			array = spr\u1B3E.ᜀ(a_2);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 13;
			a_.ᜁ = 0;
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 0;
			a_.ᜁ = 0;
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0008ADE8 File Offset: 0x00089DE8
		private unsafe void ᜀ(ushort A_0, CellNoteFormat A_1)
		{
			switch (0)
			{
			default:
			{
				ushort num;
				byte* ptr2;
				for (;;)
				{
					num = (ushort)sizeof(sprᤊ);
					Array.Clear(this.\u1713, 0, (int)num);
					int num2 = 12;
					for (;;)
					{
						HorizontalAlignment horizontal;
						VerticalAlignment vertical;
						switch (num2)
						{
						case 0:
							num2 = 19;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_140;
							default:
								if (false)
								{
								}
								num2 = 15;
								continue;
							}
							break;
						case 2:
							goto IL_1D3;
						case 3:
							goto IL_133;
						case 4:
							goto IL_133;
						case 5:
							goto IL_133;
						case 6:
							goto IL_25A;
						case 7:
							goto IL_1D3;
						case 8:
							switch (horizontal)
							{
							case HorizontalAlignment.General:
							case HorizontalAlignment.Left:
							{
								byte* ptr = ptr2;
								((sprᤊ*)ptr)->ᜀ = (((sprᤊ*)ptr)->ᜀ | 2);
								num2 = 17;
								continue;
							}
							case HorizontalAlignment.Center:
							{
								byte* ptr3 = ptr2;
								((sprᤊ*)ptr3)->ᜀ = (((sprᤊ*)ptr3)->ᜀ | 4);
								num2 = 3;
								continue;
							}
							case HorizontalAlignment.Right:
							{
								byte* ptr4 = ptr2;
								((sprᤊ*)ptr4)->ᜀ = (((sprᤊ*)ptr4)->ᜀ | 6);
								num2 = 4;
								continue;
							}
							case HorizontalAlignment.Fill:
							{
								byte* ptr5 = ptr2;
								((sprᤊ*)ptr5)->ᜀ = (((sprᤊ*)ptr5)->ᜀ | 8);
								num2 = 5;
								continue;
							}
							default:
								num2 = 16;
								continue;
							}
							break;
						case 9:
							goto IL_133;
						case 10:
							switch (vertical)
							{
							case VerticalAlignment.Top:
							{
								byte* ptr6 = ptr2;
								((sprᤊ*)ptr6)->ᜀ = (((sprᤊ*)ptr6)->ᜀ | 16);
								num2 = 6;
								continue;
							}
							case VerticalAlignment.Center:
							{
								byte* ptr7 = ptr2;
								((sprᤊ*)ptr7)->ᜀ = (((sprᤊ*)ptr7)->ᜀ | 32);
								num2 = 14;
								continue;
							}
							case VerticalAlignment.Bottom:
							{
								byte* ptr8 = ptr2;
								((sprᤊ*)ptr8)->ᜀ = (((sprᤊ*)ptr8)->ᜀ | 48);
								num2 = 18;
								continue;
							}
							case VerticalAlignment.Justify:
							{
								byte* ptr9 = ptr2;
								((sprᤊ*)ptr9)->ᜀ = (((sprᤊ*)ptr9)->ᜀ | 64);
								num2 = 11;
								continue;
							}
							default:
								num2 = 1;
								continue;
							}
							break;
						case 11:
							goto IL_1AD;
						case 12:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_11F;
						}
						case 13:
							goto IL_11F;
						case 14:
							goto IL_11A;
						case 15:
							goto IL_B9;
						case 16:
							num2 = 9;
							continue;
						case 17:
							goto IL_133;
						case 18:
							goto IL_238;
						case 19:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 13;
								continue;
							}
							fixed (byte* ptr2 = &u[0])
							{
								if (true)
								{
								}
								num2 = 2;
								continue;
								break;
							}
						}
						}
						break;
						IL_11F:
						ptr2 = null;
						num2 = 7;
						continue;
						IL_140:
						num2 = 10;
						continue;
						IL_133:
						vertical = A_1.Alignment.Vertical;
						goto IL_140;
						IL_1D3:
						((sprᤊ*)ptr2)->ᜀ = 0;
						horizontal = A_1.Alignment.Horizontal;
						num2 = 8;
					}
				}
				IL_B9:
				IL_11A:
				IL_1AD:
				IL_238:
				IL_25A:
				((sprᤊ*)ptr2)->ᜁ = (ushort)A_1.Orientation;
				((sprᤊ*)ptr2)->ᜃ = A_0;
				((sprᤊ*)ptr2)->ᜄ = (ushort)sizeof(spr\u2510);
				ptr2 = null;
				this.ᜁ(438, num);
				return;
			}
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0008B0FC File Offset: 0x0008A0FC
		private void ᜀ(string A_0)
		{
			int a_ = 0;
			if (true)
			{
			}
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
				byte[] bytes = Encoding.Unicode.GetBytes(A_0);
				if (bytes.Length + 1 <= this.\u1713.Length)
				{
					Array.Clear(this.\u1713, 0, bytes.Length + 1);
					this.\u1713[0] = 1;
					Array.Copy(bytes, 0, this.\u1713, 1, bytes.Length);
					this.ᜁ(60, (ushort)(bytes.Length + 1));
					return;
				}
				break;
			}
			}
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("崛氝䜟儡笣戥䤧帩䴫稭弯崱砳夵嘷崹", a_)));
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0008B1B4 File Offset: 0x0008A1B4
		private unsafe void ᜀ(ushort A_0, CellFont A_1)
		{
			switch (0)
			{
			default:
			{
				ushort num;
				int num2;
				byte* ptr;
				for (;;)
				{
					num = (ushort)sizeof(spr\u2510);
					Array.Clear(this.\u1713, 0, (int)num);
					num2 = this.ᜄ.ListIndexByFont(A_1);
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_7E;
						case 1:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num3 = 0;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num3 = 4;
								continue;
								break;
							}
						}
						case 2:
							goto IL_9C;
						case 3:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num3 = 6;
								continue;
							}
							goto IL_7E;
						}
						case 4:
							goto IL_11B;
						case 5:
							if (num2 == -1)
							{
								num3 = 8;
								continue;
							}
							num2 += 5;
							num3 = 7;
							continue;
						case 6:
							if (true)
							{
							}
							num3 = 1;
							continue;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C4;
							default:
								if (false)
								{
								}
								goto IL_9C;
							}
							break;
						case 8:
							goto IL_C4;
						case 9:
							goto IL_97;
						}
						break;
						IL_7E:
						ptr = null;
						num3 = 9;
						continue;
						IL_9C:
						num3 = 3;
						continue;
						IL_C4:
						CellFont cellFont = new CellFont();
						cellFont.Assign(A_1, Color.Black);
						this.ᜄ.Add(cellFont);
						this.ᜌ++;
						num2 = this.ᜌ;
						num3 = 2;
					}
				}
				IL_97:
				IL_11B:
				((spr\u2510*)ptr)->ᜁ = (ushort)num2;
				((spr\u2510*)ptr)->ᜃ = A_0;
				ptr = null;
				this.ᜁ(60, num);
				return;
			}
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0008B358 File Offset: 0x0008A358
		private void ᜅ(ushort A_0)
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
			spr\u1DCF a_;
			a_.ᜀ = 93;
			a_.ᜁ = (ushort)(sizeof(spr\u1B3E) + sizeof(spr\u1DCF) * 3);
			byte[] array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 21;
			a_.ᜁ = (ushort)sizeof(spr\u1B3E);
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			spr\u1B3E a_2;
			a_2.ᜀ = 5;
			a_2.ᜁ = A_0;
			a_2.ᜂ = 17;
			a_2.ᜃ.ᜀ = 0L;
			a_2.ᜃ.ᜁ = 0;
			array = spr\u1B3E.ᜀ(a_2);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 0;
			a_.ᜁ = 0;
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0008B46C File Offset: 0x0008A46C
		private void ᜄ(ushort A_0)
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
			spr\u1DCF a_;
			a_.ᜀ = 93;
			a_.ᜁ = (ushort)(sizeof(spr\u1B3E) + sizeof(spr\u1DCF) * 4);
			byte[] array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 21;
			a_.ᜁ = (ushort)sizeof(spr\u1B3E);
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			spr\u1B3E a_2;
			a_2.ᜀ = 8;
			a_2.ᜁ = A_0;
			a_2.ᜂ = 17;
			a_2.ᜃ.ᜀ = 0L;
			a_2.ᜃ.ᜁ = 0;
			array = spr\u1B3E.ᜀ(a_2);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 7;
			a_.ᜁ = 0;
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 8;
			a_.ᜁ = 0;
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
			a_.ᜀ = 0;
			a_.ᜁ = 0;
			array = spr\u1DCF.ᜀ(a_);
			this.\u1712.ᜁ(array, array.Length);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0008B5CC File Offset: 0x0008A5CC
		private unsafe void ᜀ(CellNote A_0, int A_1)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u25D4);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_88;
					case 1:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 2;
							continue;
						}
						goto IL_8A;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AA;
						default:
							if (false)
							{
							}
							num2 = 4;
							continue;
						}
						break;
					case 3:
						goto IL_98;
					case 4:
					{
						byte[] u;
						if (u.Length == 0)
						{
							goto IL_AA;
						}
						fixed (byte* ptr = &u[0])
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
							break;
						}
					}
					case 5:
						goto IL_8A;
					}
					break;
					IL_8A:
					ptr = null;
					num2 = 3;
					continue;
					IL_AA:
					num2 = 5;
				}
			}
			IL_88:
			IL_98:
			((spr\u25D4*)ptr)->ᜁ = (ushort)(A_0.Col - 1);
			((spr\u25D4*)ptr)->ᜀ = (ushort)(A_0.Row - 1);
			((spr\u25D4*)ptr)->ᜂ = 0;
			((spr\u25D4*)ptr)->ᜃ = (ushort)A_1;
			ptr = null;
			this.ᜁ(28, num);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0008B6D8 File Offset: 0x0008A6D8
		private void ᜀ(int A_0, Chart A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜈ(32);
					this.ᜀ(20, string.Empty);
					this.ᜀ(21, string.Empty);
					this.ᜀ(131, 0);
					this.ᜀ(132, 0);
					this.\u1718();
					this.ᜀ(51, 3);
					this.ᜀ(240, 0, 5);
					this.ᜀ(200, 1, 6);
					this.ᜀ(200, 0, 7);
					this.ᜀ(18, 0);
					this.ᜀ(4097, 0);
					this.\u1717();
					this.ᜁ(4147, 0);
					this.\u1716();
					this.\u1715();
					this.ᜀ(false, true);
					this.ᜁ(4147, 0);
					this.ᜀ(0U, 0, ushort.MaxValue, 9, 77);
					this.ᜀ(16777215U, 0U, 1, 1, 78, 77);
					this.ᜁ(4148, 0);
					DataRange dataRange = null;
					int num = 0;
					int num2 = 44;
					for (;;)
					{
						int num4;
						int num3;
						RangeType categoryLabelsType;
						RangeType dataRangeType;
						int num5;
						switch (num2)
						{
						case 0:
							goto IL_5DB;
						case 1:
							dataRange.ᜄ = true;
							dataRange.ᜅ = A_1.CategoryLabelsColumn;
							num2 = 45;
							continue;
						case 2:
							goto IL_5DB;
						case 3:
							goto IL_5DB;
						case 4:
							goto IL_98A;
						case 5:
							num2 = 10;
							continue;
						case 6:
							goto IL_AA3;
						case 7:
							goto IL_47C;
						case 8:
							goto IL_98F;
						case 9:
							goto IL_6E7;
						case 10:
							goto IL_98F;
						case 11:
							num3 = num4;
							goto IL_B5F;
						case 12:
						{
							ChartStyle style;
							switch (style)
							{
							case ChartStyle.Column:
							case ChartStyle.Column3d:
								this.ᜀ(false);
								num2 = 32;
								continue;
							case ChartStyle.Bar:
							case ChartStyle.Bar3d:
								this.ᜀ(true);
								num2 = 0;
								continue;
							case ChartStyle.Line:
							case ChartStyle.Line3d:
								this.ᜉ();
								num2 = 21;
								continue;
							case ChartStyle.LineMark:
								goto IL_5DB;
							case ChartStyle.Pie:
							case ChartStyle.Pie3d:
								this.ᜈ();
								num2 = 2;
								continue;
							case ChartStyle.Area:
							case ChartStyle.Area3d:
								this.ᜇ();
								num2 = 3;
								continue;
							case ChartStyle.Surface:
							case ChartStyle.Surface3d:
								this.ᜆ();
								num2 = 59;
								continue;
							case ChartStyle.Radar:
								this.ᜅ();
								num2 = 14;
								continue;
							case ChartStyle.RadarArea:
								goto IL_215;
							default:
								num2 = 36;
								continue;
							}
							break;
						}
						case 13:
							goto IL_5DB;
						case 14:
							goto IL_5DB;
						case 15:
							switch (categoryLabelsType)
							{
							case RangeType.Column:
								dataRange = new DataRange();
								num2 = 30;
								continue;
							case RangeType.Custom:
								if (true)
								{
								}
								this.ᜀ(2, num4, A_1.CategoryLabels);
								num2 = 31;
								continue;
							default:
								num2 = 57;
								continue;
							}
							break;
						case 16:
							num3 = A_0;
							goto IL_B5F;
						case 17:
							if (A_1.Style == ChartStyle.Surface3d)
							{
								num2 = 7;
								continue;
							}
							goto IL_6C2;
						case 18:
							goto IL_5DB;
						case 19:
							if (A_1.ShowLegend)
							{
								num2 = 62;
								continue;
							}
							goto IL_6E7;
						case 20:
							goto IL_798;
						case 21:
							goto IL_5DB;
						case 22:
							goto IL_6C2;
						case 23:
							goto IL_449;
						case 24:
							if (A_1.Style != ChartStyle.Bar3d)
							{
								num2 = 25;
								continue;
							}
							goto IL_47C;
						case 25:
							num2 = 26;
							continue;
						case 26:
							if (A_1.Style != ChartStyle.Line3d)
							{
								num2 = 38;
								continue;
							}
							goto IL_47C;
						case 27:
							num2 = 17;
							continue;
						case 28:
							if (A_1.Style != ChartStyle.Pie3d)
							{
								num2 = 52;
								continue;
							}
							goto IL_47C;
						case 29:
							goto IL_652;
						case 30:
							if (this.\u1718[num4].ColumnList.ᜀ() == 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_AF6;
						case 31:
							goto IL_798;
						case 32:
							goto IL_5DB;
						case 33:
							switch (dataRangeType)
							{
							case RangeType.Column:
								dataRange = new DataRange();
								num2 = 39;
								continue;
							case RangeType.Custom:
								this.ᜀ(1, num4, A_1.Series[num].DataRange);
								num2 = 8;
								continue;
							default:
								num2 = 5;
								continue;
							}
							break;
						case 34:
							this.ᜑ();
							this.ᜁ(4147, 0);
							this.ᜀ(false, 0U, 0U, 52U, 22U);
							this.ᜂ(5);
							this.ᜀ(0, -1, null);
							this.ᜀ(new byte[0], A_1.Title, true);
							this.ᜂ();
							this.ᜁ(4148, 0);
							num2 = 4;
							continue;
						case 35:
							this.ᜀ(new byte[0], A_1.Series[num].Title, true);
							num2 = 6;
							continue;
						case 36:
							num2 = 13;
							continue;
						case 37:
							if (A_1.Style != ChartStyle.Column3d)
							{
								num2 = 63;
								continue;
							}
							goto IL_47C;
						case 38:
							num2 = 28;
							continue;
						case 39:
							if (this.\u1718[num4].ColumnList.ᜀ() == 0)
							{
								num2 = 60;
								continue;
							}
							goto IL_680;
						case 40:
							num5 = num4;
							goto IL_593;
						case 41:
							if (A_1.Style != ChartStyle.Area3d)
							{
								num2 = 27;
								continue;
							}
							goto IL_47C;
						case 42:
							num5 = A_0;
							goto IL_593;
						case 43:
							if (num >= A_1.Series.Count)
							{
								num2 = 61;
								continue;
							}
							this.\u1714();
							this.ᜁ(4147, 0);
							this.ᜀ(0, -1, null);
							num2 = 58;
							continue;
						case 44:
							goto IL_652;
						case 45:
							goto IL_AF6;
						case 46:
							goto IL_98F;
						case 47:
							num2 = 16;
							continue;
						case 48:
							this.ᜀ(0U, 0, 0, 0, 8);
							this.ᜀ(10526880U, 9474192U, 1, 0, (ushort)spr\u2009.᠓[(int)A_1.Series[num].Color], 8);
							num2 = 23;
							continue;
						case 49:
							if (!A_1.AutoColor)
							{
								num2 = 48;
								continue;
							}
							goto IL_449;
						case 50:
							goto IL_798;
						case 51:
							num2 = 42;
							continue;
						case 52:
							num2 = 41;
							continue;
						case 53:
							if (A_1.Title.Length > 0)
							{
								num2 = 34;
								continue;
							}
							goto IL_BAB;
						case 54:
							if (num4 < 0)
							{
								num2 = 47;
								continue;
							}
							num2 = 11;
							continue;
						case 55:
							if (num4 < 0)
							{
								num2 = 51;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_215;
							default:
								if (false)
								{
								}
								num2 = 40;
								continue;
							}
							break;
						case 56:
							goto IL_680;
						case 57:
							num2 = 50;
							continue;
						case 58:
							if (A_1.Series[num].Title.Length > 0)
							{
								num2 = 35;
								continue;
							}
							goto IL_AA3;
						case 59:
							goto IL_5DB;
						case 60:
							dataRange.ᜄ = true;
							dataRange.ᜅ = A_1.Series[num].DataColumn;
							num2 = 56;
							continue;
						case 61:
						{
							this.\u1712();
							this.ᜃ(2);
							this.ᜑ();
							this.ᜁ(4147, 0);
							this.ᜀ(false, 0U, 0U, 0U, 0U);
							this.ᜂ(7);
							this.ᜀ(0, -1, null);
							this.ᜁ(4148, 0);
							this.ᜃ(3);
							this.ᜑ();
							this.ᜁ(4147, 0);
							this.ᜀ(false, 0U, 0U, 0U, 0U);
							this.ᜂ(6);
							this.ᜀ(0, -1, null);
							this.ᜁ(4148, 0);
							this.ᜐ();
							this.ᜏ();
							this.ᜁ(4147, 0);
							this.ᜀ(false, 129U, 719U, 2968U, 3105U);
							this.ᜁ(0);
							this.ᜁ(4147, 0);
							this.ᜎ();
							this.\u170D();
							this.ᜌ();
							this.ᜁ(4148, 0);
							this.ᜁ(1);
							this.ᜁ(4147, 0);
							this.ᜋ();
							this.ᜌ();
							this.ᜊ();
							this.ᜀ(0U, 0, ushort.MaxValue, 9, 77);
							this.ᜁ(4148, 0);
							this.ᜁ(4149, 0);
							this.ᜀ(true, true);
							this.ᜁ(4147, 0);
							this.ᜀ(8421504U, 0, 0, 0, 23);
							this.ᜀ(12632256U, 0U, 1, 0, 22, 79);
							this.ᜁ(4148, 0);
							this.ᜀ(A_1.Style);
							this.ᜁ(4147, 0);
							ChartStyle style = A_1.Style;
							num2 = 12;
							continue;
						}
						case 62:
							this.ᜀ((byte)A_1.LegendPlacement);
							this.ᜁ(4147, 0);
							this.ᜀ(true, 3239U, 1947U, 0U, 0U);
							this.ᜑ();
							this.ᜁ(4147, 0);
							this.ᜀ(false, 0U, 0U, 0U, 0U);
							this.ᜀ(0, -1, null);
							this.ᜁ(4148, 0);
							this.ᜁ(4148, 0);
							num2 = 9;
							continue;
						case 63:
							num2 = 24;
							continue;
						}
						break;
						IL_215:
						this.ᜄ();
						num2 = 18;
						continue;
						IL_449:
						this.ᜁ(4148, 0);
						this.\u1713();
						this.ᜁ(4148, 0);
						num++;
						num2 = 29;
						continue;
						IL_47C:
						this.ᜃ();
						num2 = 22;
						continue;
						IL_593:
						num4 = num5;
						dataRangeType = A_1.Series[num].DataRangeType;
						num2 = 33;
						continue;
						IL_5DB:
						num2 = 37;
						continue;
						IL_652:
						num2 = 43;
						continue;
						IL_680:
						this.\u1718[num4].ColumnList.ᜀ(A_1.Series[num].DataColumn, dataRange);
						this.ᜀ(1, num4, dataRange);
						num2 = 46;
						continue;
						IL_6C2:
						num2 = 19;
						continue;
						IL_6E7:
						this.ᜁ(4148, 0);
						this.ᜁ(4148, 0);
						num2 = 53;
						continue;
						IL_798:
						this.ᜀ(3, -1, null);
						this.ᜀ(num);
						this.ᜁ(4147, 0);
						this.ᜀ(4191, 0);
						num2 = 49;
						continue;
						IL_98F:
						num4 = this.ᜁ(A_1.DataRangeSheet);
						num2 = 54;
						continue;
						IL_AA3:
						num4 = this.ᜁ(A_1.Series[num].DataRangeSheet);
						num2 = 55;
						continue;
						IL_AF6:
						this.Sheets[num4].ColumnList.ᜀ(A_1.CategoryLabelsColumn, dataRange);
						this.ᜀ(2, num4, dataRange);
						num2 = 20;
						continue;
						IL_B5F:
						num4 = num3;
						categoryLabelsType = A_1.CategoryLabelsType;
						num2 = 15;
					}
				}
				IL_98A:
				IL_BAB:
				this.ᜁ(4148, 0);
				this.ᜀ(0, 13, 0, 1);
				this.ᜀ(1);
				this.ᜀ(2);
				this.ᜀ(3);
				this.\u171D();
				return;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0008C2C4 File Offset: 0x0008B2C4
		private unsafe void \u1718()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				if (true)
				{
				}
				num = (ushort)sizeof(sprṄ);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_73;
					case 1:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 4;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 0;
							continue;
							break;
						}
					}
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_75;
					}
					case 3:
						goto IL_9F;
					case 4:
						goto IL_75;
					case 5:
						num2 = 1;
						continue;
					}
					break;
					IL_75:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						ptr = null;
						break;
					}
					num2 = 3;
				}
			}
			IL_73:
			IL_9F:
			((sprṄ*)ptr)->ᜀ = 0;
			((sprṄ*)ptr)->ᜁ = 18;
			((sprṄ*)ptr)->ᜂ = 1;
			((sprṄ*)ptr)->ᜃ = 1;
			((sprṄ*)ptr)->ᜄ = 1;
			((sprṄ*)ptr)->ᜅ = 0;
			byte* ptr2 = ptr;
			((sprṄ*)ptr2)->ᜅ = (((sprṄ*)ptr2)->ᜅ | 1);
			byte* ptr3 = ptr;
			((sprṄ*)ptr3)->ᜅ = (((sprṄ*)ptr3)->ᜅ | 2);
			byte* ptr4 = ptr;
			((sprṄ*)ptr4)->ᜅ = (((sprṄ*)ptr4)->ᜅ | 8);
			byte* ptr5 = ptr;
			((sprṄ*)ptr5)->ᜅ = (((sprṄ*)ptr5)->ᜅ | 16);
			byte* ptr6 = ptr;
			((sprṄ*)ptr6)->ᜅ = (((sprṄ*)ptr6)->ᜅ | 32);
			((sprṄ*)ptr)->ᜆ = 0;
			((sprṄ*)ptr)->ᜇ = 2208;
			((sprṄ*)ptr)->ᜈ = 0.0;
			((sprṄ*)ptr)->ᜉ = 0.0;
			((sprṄ*)ptr)->ᜊ = 1;
			ptr = null;
			this.ᜁ(161, num);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0008C460 File Offset: 0x0008B460
		private unsafe void ᜀ(CellGraphic A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = A_0.Stream.Length == 0L;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					Image image = null;
					Bitmap bitmap = null;
					int num5 = 13;
					for (;;)
					{
						int num6;
						ushort num7;
						int num8;
						Graphics graphics;
						Bitmap bitmap2;
						switch (num5)
						{
						case 0:
							if (A_0.FileName.Length > 0)
							{
								num5 = 8;
								continue;
							}
							return;
						case 1:
							goto IL_129;
						case 2:
							if (num6 > 8212)
							{
								num5 = 39;
								continue;
							}
							num3 = num6;
							num5 = 42;
							continue;
						case 3:
							if (num4 + (int)num7 > 8220)
							{
								num5 = 6;
								continue;
							}
							goto IL_4FB;
						case 4:
							image = sprᮌ.ᜃ(A_0.FileName);
							num = image.Height;
							num2 = image.Width;
							num5 = 32;
							continue;
						case 5:
							goto IL_2C1;
						case 6:
							num8 = (int)(8220 - num7);
							num5 = 17;
							continue;
						case 7:
							goto IL_1F6;
						case 8:
							num5 = 24;
							continue;
						case 9:
							goto IL_129;
						case 10:
							goto IL_273;
						case 11:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num5 = 35;
								continue;
							}
							goto IL_114;
						}
						case 12:
							goto IL_2C6;
						case 13:
							if (flag)
							{
								num5 = 38;
								continue;
							}
							num3 = 0;
							num = A_0.Height;
							num2 = A_0.Width;
							num4 = (int)A_0.Stream.Length;
							num5 = 9;
							continue;
						case 14:
							num5 = 20;
							continue;
						case 15:
							A_0.Stream.SetLength(0L);
							num5 = 5;
							continue;
						case 16:
							if (A_0.GraphicType != XlsGraphicType.WMF)
							{
								num5 = 40;
								continue;
							}
							goto IL_38B;
						case 17:
							goto IL_61A;
						case 18:
							goto IL_3C6;
						case 19:
							if (A_0.GraphicType != XlsGraphicType.GIF)
							{
								num5 = 27;
								continue;
							}
							goto IL_38B;
						case 20:
							if (flag)
							{
								num5 = 15;
								continue;
							}
							return;
						case 21:
							if (A_0.GraphicType != XlsGraphicType.EMF)
							{
								num5 = 36;
								continue;
							}
							goto IL_38B;
						case 22:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num5 = 30;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num5 = 18;
								continue;
								break;
							}
						}
						case 23:
							num5 = 16;
							continue;
						case 24:
							if (sprᮌ.ᜁ(A_0.FileName))
							{
								num5 = 23;
								continue;
							}
							return;
						case 25:
							num5 = 19;
							continue;
						case 26:
						{
							try
							{
								num5 = 0;
								for (;;)
								{
									switch (num5)
									{
									case 1:
										goto IL_4EE;
									case 2:
										goto IL_4D3;
									case 3:
										graphics.DrawImage(image, 0, 0);
										num5 = 4;
										continue;
									case 4:
										goto IL_4D3;
									}
									if (sprᮌ.ᜀ())
									{
										num5 = 3;
										continue;
									}
									graphics.DrawImage(bitmap, 0, 0);
									num5 = 2;
									continue;
									IL_4D3:
									bitmap2.Save(A_0.Stream, ImageFormat.Bmp);
									num5 = 1;
								}
								IL_4EE:
								goto IL_5C7;
							}
							finally
							{
								graphics.Dispose();
							}
							goto IL_4FB;
							IL_5C7:
							A_0.Stream.Seek(10L, SeekOrigin.Begin);
							byte[] array = new byte[4];
							A_0.Stream.Read(array, 0, 4);
							num3 = BitConverter.ToInt32(array, 0);
							num4 = (int)(A_0.Stream.Length - (long)num3);
							num5 = 1;
							continue;
						}
						case 27:
							num5 = 37;
							continue;
						case 28:
							goto IL_1F6;
						case 29:
							goto IL_61A;
						case 30:
							goto IL_114;
						case 31:
							if (A_0.GraphicType != XlsGraphicType.JPG)
							{
								num5 = 45;
								continue;
							}
							goto IL_38B;
						case 32:
							goto IL_2C6;
						case 33:
							goto IL_26E;
						case 34:
							if (num6 <= 0)
							{
								num5 = 14;
								continue;
							}
							num5 = 2;
							continue;
						case 35:
							num5 = 22;
							continue;
						case 36:
							num5 = 31;
							continue;
						case 37:
							if (A_0.GraphicType != XlsGraphicType.ICO)
							{
								num5 = 33;
								continue;
							}
							goto IL_38B;
						case 38:
							num5 = 0;
							continue;
						case 39:
							num3 = 8212;
							num5 = 10;
							continue;
						case 40:
							num5 = 21;
							continue;
						case 41:
							if (A_0.GraphicType != XlsGraphicType.BMP)
							{
								num5 = 25;
								continue;
							}
							goto IL_38B;
						case 42:
							goto IL_273;
						case 43:
							if (sprᮌ.ᜀ())
							{
								num5 = 4;
								continue;
							}
							if (true)
							{
							}
							bitmap = new Bitmap(A_0.FileName);
							num = bitmap.Height;
							num2 = bitmap.Width;
							goto IL_58D;
						case 44:
							goto IL_3C6;
						case 45:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_58D;
							default:
								if (false)
								{
								}
								num5 = 41;
								continue;
							}
							break;
						}
						break;
						IL_114:
						byte* ptr = null;
						num5 = 44;
						continue;
						IL_129:
						num7 = (ushort)sizeof(spr\u1FAB);
						int num9 = (int)(num7 - 8);
						num8 = 0;
						num5 = 3;
						continue;
						IL_1F6:
						num5 = 34;
						continue;
						IL_273:
						A_0.Stream.Read(this.\u1713, 0, num3);
						this.ᜁ(60, (ushort)num3);
						num6 -= num3;
						num5 = 7;
						continue;
						IL_2C6:
						bitmap2 = new Bitmap(num2, num, PixelFormat.Format24bppRgb);
						graphics = Graphics.FromImage(bitmap2);
						num5 = 26;
						continue;
						IL_38B:
						num5 = 43;
						continue;
						IL_3C6:
						((spr\u1FAB*)ptr)->ᜀ = 9;
						((spr\u1FAB*)ptr)->ᜁ = 1;
						((spr\u1FAB*)ptr)->ᜂ = (uint)num9;
						((spr\u1FAB*)ptr)->ᜃ = 12;
						((spr\u1FAB*)ptr)->ᜄ = 0;
						((spr\u1FAB*)ptr)->ᜅ = (ushort)num2;
						((spr\u1FAB*)ptr)->ᜆ = (ushort)num;
						((spr\u1FAB*)ptr)->ᜇ = 1;
						((spr\u1FAB*)ptr)->ᜈ = 24;
						ptr = null;
						A_0.Stream.Seek((long)num3, SeekOrigin.Begin);
						A_0.Stream.Read(this.\u1713, (int)num7, num8);
						this.ᜁ(233, (ushort)(num8 + (int)num7));
						num6 = num4 - num8;
						num5 = 28;
						continue;
						IL_4FB:
						num8 = num4;
						num5 = 29;
						continue;
						IL_58D:
						num5 = 12;
						continue;
						IL_61A:
						num9 += num4;
						num5 = 11;
					}
				}
				IL_26E:
				return;
				IL_2C1:
				return;
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0008CB4C File Offset: 0x0008BB4C
		private unsafe void ᜀ(ushort A_0, ushort A_1, ushort A_2)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u2329);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							if (true)
							{
							}
							num2 = 1;
							continue;
							break;
						}
					}
					case 1:
						goto IL_69;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_69;
						default:
							if (false)
							{
							}
							goto IL_6B;
						}
						break;
					case 3:
						goto IL_76;
					case 4:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_6B;
					}
					case 5:
						num2 = 0;
						continue;
					}
					break;
					IL_6B:
					ptr = null;
					num2 = 3;
				}
			}
			IL_69:
			IL_76:
			((spr\u2329*)ptr)->ᜀ = 5685;
			((spr\u2329*)ptr)->ᜁ = 4380;
			((spr\u2329*)ptr)->ᜂ = A_0;
			((spr\u2329*)ptr)->ᜃ = A_1;
			((spr\u2329*)ptr)->ᜄ = 0;
			ptr = null;
			this.ᜁ(4192, num);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0008CC50 File Offset: 0x0008BC50
		private unsafe void \u1717()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u251A);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 4;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					case 1:
						goto IL_69;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_69;
						default:
							if (false)
							{
							}
							goto IL_6B;
						}
						break;
					case 3:
						num2 = 0;
						continue;
					case 4:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_6B;
					}
					case 5:
						goto IL_76;
					}
					break;
					IL_6B:
					ptr = null;
					num2 = 5;
				}
			}
			IL_69:
			IL_76:
			((spr\u251A*)ptr)->ᜀ = 61440U;
			((spr\u251A*)ptr)->ᜁ = 0U;
			((spr\u251A*)ptr)->ᜂ = 15728616U;
			((spr\u251A*)ptr)->ᜃ = 11698128U;
			ptr = null;
			this.ᜁ(4098, num);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0008CD50 File Offset: 0x0008BD50
		private unsafe void \u1716()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u25DE);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 3;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					case 1:
						goto IL_61;
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_63;
					}
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							if (false)
							{
							}
							goto IL_63;
						}
						break;
					case 4:
						num2 = 0;
						continue;
					case 5:
						goto IL_6E;
					}
					break;
					IL_63:
					ptr = null;
					num2 = 5;
				}
			}
			IL_61:
			IL_6E:
			((spr\u25DE*)ptr)->ᜀ = 1;
			((spr\u25DE*)ptr)->ᜁ = 1;
			ptr = null;
			this.ᜁ(160, num);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0008CE34 File Offset: 0x0008BE34
		private unsafe void \u1715()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u25DB);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							if (false)
							{
							}
							goto IL_63;
						}
						break;
					case 1:
						goto IL_61;
					case 2:
						num2 = 5;
						continue;
					case 3:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 2;
							continue;
						}
						goto IL_63;
					}
					case 4:
						goto IL_6E;
					case 5:
					{
						byte[] u;
						if (u.Length == 0)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					}
					break;
					IL_63:
					ptr = null;
					num2 = 4;
				}
			}
			IL_61:
			IL_6E:
			((spr\u25DB*)ptr)->ᜀ = 65536U;
			((spr\u25DB*)ptr)->ᜁ = 65536U;
			ptr = null;
			this.ᜁ(4196, num);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0008CF20 File Offset: 0x0008BF20
		private unsafe void ᜀ(bool A_0, bool A_1)
		{
			ushort num;
			byte* ptr2;
			for (;;)
			{
				IL_38:
				num = (ushort)sizeof(sprἢ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_4D:
					int num2 = 8;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_94;
						case 1:
							goto IL_94;
						case 2:
							goto IL_106;
						case 3:
							goto IL_BF;
						case 4:
							if (A_0)
							{
								num2 = 6;
								continue;
							}
							goto IL_125;
						case 5:
							if (A_1)
							{
								num2 = 7;
								continue;
							}
							goto IL_162;
						case 6:
						{
							byte* ptr = ptr2;
							((sprἢ*)ptr)->ᜁ = ((sprἢ*)ptr)->ᜁ + 1;
							num2 = 9;
							continue;
						}
						case 7:
						{
							byte* ptr3 = ptr2;
							((sprἢ*)ptr3)->ᜁ = ((sprἢ*)ptr3)->ᜁ + 2;
							num2 = 2;
							continue;
						}
						case 8:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 10;
								continue;
							}
							goto IL_BF;
						}
						case 9:
							if (true)
							{
							}
							goto IL_125;
						case 10:
							num2 = 11;
							continue;
						case 11:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 3;
								continue;
							}
							fixed (byte* ptr2 = &u[0])
							{
								num2 = 0;
								continue;
								break;
							}
						}
						}
						goto IL_38;
						IL_94:
						((sprἢ*)ptr2)->ᜀ = 0;
						((sprἢ*)ptr2)->ᜁ = 0;
						num2 = 4;
						continue;
						IL_BF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						default:
							if (false)
							{
							}
							ptr2 = null;
							num2 = 1;
							continue;
						}
						IL_125:
						num2 = 5;
					}
				}
			}
			IL_106:
			IL_162:
			ptr2 = null;
			this.ᜁ(4146, num);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0008D0A0 File Offset: 0x0008C0A0
		private unsafe void ᜀ(uint A_0, ushort A_1, ushort A_2, ushort A_3, ushort A_4)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(sprợ);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							if (false)
							{
							}
							goto IL_63;
						}
						break;
					case 1:
						goto IL_61;
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_63;
					}
					case 3:
						num2 = 5;
						continue;
					case 4:
						goto IL_76;
					case 5:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 0;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					}
					break;
					IL_63:
					ptr = null;
					if (true)
					{
					}
					num2 = 4;
				}
			}
			IL_61:
			IL_76:
			((sprợ*)ptr)->ᜀ = A_0;
			((sprợ*)ptr)->ᜁ = A_1;
			((sprợ*)ptr)->ᜂ = A_2;
			((sprợ*)ptr)->ᜃ = A_3;
			((sprợ*)ptr)->ᜄ = A_4;
			ptr = null;
			this.ᜁ(4103, num);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0008D1A0 File Offset: 0x0008C1A0
		private unsafe void ᜀ(uint A_0, uint A_1, ushort A_2, ushort A_3, ushort A_4, ushort A_5)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u181C);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 4;
						continue;
					case 1:
						goto IL_61;
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_63;
					}
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							if (false)
							{
							}
							goto IL_63;
						}
						break;
					case 4:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 3;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					case 5:
						goto IL_6E;
					}
					break;
					IL_63:
					ptr = null;
					num2 = 5;
				}
			}
			IL_61:
			IL_6E:
			((spr\u181C*)ptr)->ᜀ = A_0;
			((spr\u181C*)ptr)->ᜁ = A_1;
			((spr\u181C*)ptr)->ᜂ = A_2;
			((spr\u181C*)ptr)->ᜃ = A_3;
			((spr\u181C*)ptr)->ᜄ = A_4;
			((spr\u181C*)ptr)->ᜅ = A_5;
			ptr = null;
			this.ᜁ(4106, num);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0008D2A8 File Offset: 0x0008C2A8
		private unsafe void \u1714()
		{
			if (true)
			{
			}
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_28:
				num = (ushort)sizeof(sprṌ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_3D:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_75;
						}
						case 1:
							goto IL_75;
						case 2:
							num2 = 3;
							continue;
						case 3:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 1;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 4;
								continue;
								break;
							}
						}
						case 4:
							goto IL_73;
						case 5:
							goto IL_9F;
						}
						goto IL_28;
						IL_75:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							if (false)
							{
							}
							ptr = null;
							num2 = 5;
							break;
						}
					}
				}
			}
			IL_73:
			IL_9F:
			((sprṌ*)ptr)->ᜀ = 1;
			((sprṌ*)ptr)->ᜁ = 1;
			((sprṌ*)ptr)->ᜂ = 13;
			((sprṌ*)ptr)->ᜃ = 13;
			((sprṌ*)ptr)->ᜄ = 1;
			((sprṌ*)ptr)->ᜅ = 0;
			ptr = null;
			this.ᜁ(4099, num);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0008D3B4 File Offset: 0x0008C3B4
		internal new int ᜁ(string A_0)
		{
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 >= this.Sheets.Count)
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				}
				case 1:
					if (this.SheetName.Equals(A_0))
					{
						num = 12;
						continue;
					}
					return -1;
				case 2:
					if (this.Sheets.Count > 0)
					{
						num = 9;
						continue;
					}
					goto IL_DE;
				case 3:
					num = 14;
					continue;
				case 4:
					num = 2;
					continue;
				case 5:
					num = 10;
					continue;
				case 6:
					goto IL_127;
				case 7:
				{
					int num2;
					return num2;
				}
				case 8:
				{
					int num2;
					if (this.Sheets[num2].SheetName.Equals(A_0))
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_83;
					default:
						if (false)
						{
						}
						num2++;
						num = 11;
						continue;
					}
					break;
				}
				case 9:
				{
					int num2 = 0;
					num = 6;
					continue;
				}
				case 10:
					goto IL_83;
				case 11:
					goto IL_127;
				case 12:
					return 0;
				case 14:
					if (this.Sheets != null)
					{
						num = 4;
						continue;
					}
					goto IL_DE;
				}
				if (true)
				{
				}
				if (!A_0.Equals(string.Empty))
				{
					num = 3;
					continue;
				}
				break;
				IL_DE:
				num = 1;
				continue;
				IL_127:
				num = 0;
			}
			IL_83:
			return -1;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0008D55C File Offset: 0x0008C55C
		private unsafe void ᜀ(byte A_0, int A_1, DataRange A_2)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_214:
				num = 11;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_99;
				}
				break;
			}
			byte* ptr;
			ushort num2;
			for (;;)
			{
				IL_36:
				byte b;
				switch (num)
				{
				case 0:
					goto IL_2F7;
				case 1:
					goto IL_C2;
				case 2:
				{
					spr\u193C spr_u193C = default(spr\u193C);
					spr_u193C.ᜀ = A_1;
					spr_u193C.ᜁ = (int)(this.\u1712.Position + 4L + (long)((ulong)((ushort)sizeof(sprḺ))));
					spr_u193C.ᜂ = A_2.ᜅ;
					this.ᜮ.Add(spr_u193C);
					num = 7;
					continue;
				}
				case 3:
					switch (b)
					{
					case 0:
						((sprḺ*)ptr)->ᜁ = 1;
						num = 1;
						continue;
					case 1:
						((sprḺ*)ptr)->ᜁ = 2;
						num = 8;
						continue;
					case 2:
						((sprḺ*)ptr)->ᜁ = 2;
						num = 16;
						continue;
					case 3:
						((sprḺ*)ptr)->ᜁ = 1;
						num = 19;
						continue;
					default:
						num = 21;
						continue;
					}
					break;
				case 4:
					num = 9;
					continue;
				case 5:
					if (A_1 >= 0)
					{
						num = 15;
						continue;
					}
					goto IL_17F;
				case 6:
					((sprḺ*)ptr)->ᜄ = (ushort)sizeof(sprᰐ);
					num = 13;
					continue;
				case 7:
					goto IL_225;
				case 8:
					goto IL_C2;
				case 9:
				{
					byte[] u;
					if (u.Length == 0)
					{
						goto IL_214;
					}
					fixed (byte* ptr = &u[0])
					{
						num = 0;
						continue;
						break;
					}
				}
				case 10:
					goto IL_C2;
				case 11:
					goto IL_10C;
				case 12:
					if (A_1 >= 0)
					{
						num = 6;
						continue;
					}
					((sprḺ*)ptr)->ᜄ = 0;
					num = 20;
					continue;
				case 13:
					if (A_2.ᜄ)
					{
						num = 2;
						continue;
					}
					goto IL_225;
				case 14:
					goto IL_17F;
				case 15:
					num2 += (ushort)sizeof(sprᰐ);
					num = 14;
					continue;
				case 16:
					goto IL_C2;
				case 17:
					goto IL_2BC;
				case 18:
				{
					byte[] u;
					if ((u = this.\u1713) != null)
					{
						num = 4;
						continue;
					}
					goto IL_10C;
				}
				case 19:
					goto IL_C2;
				case 20:
					goto IL_1E1;
				case 21:
					num = 10;
					continue;
				case 22:
					goto IL_2F7;
				}
				goto IL_99;
				IL_C2:
				((sprḺ*)ptr)->ᜂ = 0;
				((sprḺ*)ptr)->ᜃ = 0;
				num = 12;
				continue;
				IL_10C:
				ptr = null;
				num = 22;
				continue;
				IL_17F:
				Array.Clear(this.\u1713, 0, (int)num2);
				num = 18;
				continue;
				IL_225:
				sprᰐ a_;
				a_.ᜀ = 59;
				a_.ᜁ.ᜀ = (ushort)A_1;
				a_.ᜁ.ᜁ = (ushort)(A_2.RowX - 1);
				a_.ᜁ.ᜂ = (ushort)(A_2.RowY - 1);
				a_.ᜁ.ᜃ = (ushort)(A_2.ColX - 1);
				a_.ᜁ.ᜄ = (ushort)(A_2.ColY - 1);
				byte[] array = sprᰐ.ᜀ(a_);
				Array.Copy(array, 0, this.\u1713, sizeof(sprḺ), array.Length);
				num = 17;
				continue;
				IL_2F7:
				((sprḺ*)ptr)->ᜀ = A_0;
				b = ((sprḺ*)ptr)->ᜀ;
				num = 3;
			}
			IL_1E1:
			IL_2BC:
			if (true)
			{
			}
			ptr = null;
			this.ᜁ(4177, num2);
			return;
			IL_99:
			num2 = (ushort)sizeof(sprḺ);
			num = 5;
			goto IL_36;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0008D918 File Offset: 0x0008C918
		private unsafe void ᜀ(byte[] A_0, string A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				ushort num;
				for (;;)
				{
					if (true)
					{
					}
					byte b = 0;
					num = 0;
					byte[] array = null;
					int num2 = 8;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_D0;
						case 1:
							num2 = 2;
							continue;
						case 2:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 0;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 6;
								continue;
								break;
							}
						}
						case 3:
							if (A_2)
							{
								goto IL_C2;
							}
							this.\u1713[3] = 0;
							Array.Copy(A_0, 0, this.\u1713, 4, A_0.Length);
							num2 = 11;
							continue;
						case 4:
							b = (byte)A_1.Length;
							array = Encoding.Unicode.GetBytes(A_1);
							num = (ushort)(sizeof(spr\u19F2) + array.Length + 1);
							num2 = 12;
							continue;
						case 5:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_D0;
						}
						case 6:
							goto IL_93;
						case 7:
							goto IL_1C5;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C2;
							default:
								if (false)
								{
								}
								if (A_2)
								{
									num2 = 4;
									continue;
								}
								b = (byte)A_0.Length;
								num = (ushort)(sizeof(spr\u19F2) + A_0.Length + 1);
								num2 = 7;
								continue;
							}
							break;
						case 9:
							goto IL_93;
						case 10:
							goto IL_12B;
						case 11:
							goto IL_1A0;
						case 12:
							goto IL_1C5;
						case 13:
							this.\u1713[3] = 1;
							Array.Copy(array, 0, this.\u1713, 4, array.Length);
							num2 = 10;
							continue;
						}
						break;
						IL_93:
						byte* ptr;
						((spr\u19F2*)ptr)->ᜀ = 0;
						((spr\u19F2*)ptr)->ᜁ = b;
						ptr = null;
						num2 = 3;
						continue;
						IL_C2:
						num2 = 13;
						continue;
						IL_D0:
						ptr = null;
						num2 = 9;
						continue;
						IL_1C5:
						Array.Clear(this.\u1713, 0, (int)num);
						num2 = 5;
					}
				}
				IL_12B:
				IL_1A0:
				this.ᜁ(4109, num);
				return;
			}
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0008DB2C File Offset: 0x0008CB2C
		private unsafe void ᜀ(int A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(spr\u2090);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_75;
						case 1:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 0;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 4;
								continue;
								break;
							}
						}
						case 2:
						{
							if (true)
							{
							}
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_75;
						}
						case 3:
							num2 = 1;
							continue;
						case 4:
							goto IL_73;
						case 5:
							goto IL_9F;
						}
						goto IL_20;
						IL_75:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							ptr = null;
							num2 = 5;
							break;
						}
					}
				}
			}
			IL_73:
			IL_9F:
			((spr\u2090*)ptr)->ᜀ = ushort.MaxValue;
			((spr\u2090*)ptr)->ᜁ = (ushort)A_0;
			((spr\u2090*)ptr)->ᜂ = (ushort)A_0;
			((spr\u2090*)ptr)->ᜃ = 0;
			ptr = null;
			this.ᜁ(4102, num);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0008DC2C File Offset: 0x0008CC2C
		private unsafe void \u1713()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(spr\u2254);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 4;
							continue;
						case 1:
							goto IL_75;
						case 2:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_75;
						}
						case 3:
							goto IL_6B;
						case 4:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 1;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 3;
								continue;
								break;
							}
						}
						case 5:
							goto IL_9F;
						}
						goto IL_20;
						IL_75:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							ptr = null;
							num2 = 5;
							break;
						}
					}
				}
			}
			IL_6B:
			if (true)
			{
			}
			IL_9F:
			((spr\u2254*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4165, num);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0008DD10 File Offset: 0x0008CD10
		private unsafe void \u1712()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprᯑ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								if (true)
								{
								}
								num2 = 4;
								continue;
							}
							goto IL_75;
						}
						case 1:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 3;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 2;
								continue;
								break;
							}
						}
						case 2:
							goto IL_73;
						case 3:
							goto IL_75;
						case 4:
							num2 = 1;
							continue;
						case 5:
							goto IL_9F;
						}
						goto IL_20;
						IL_75:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							ptr = null;
							num2 = 5;
							break;
						}
					}
				}
			}
			IL_73:
			IL_9F:
			((sprᯑ*)ptr)->ᜀ = 10;
			((sprᯑ*)ptr)->ᜁ = 0;
			((sprᯑ*)ptr)->ᜂ = 0;
			ptr = null;
			this.ᜁ(4164, num);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0008DE04 File Offset: 0x0008CE04
		private unsafe void ᜃ(ushort A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprḣ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_6B;
						case 1:
							num2 = 4;
							continue;
						case 2:
							goto IL_6D;
						case 3:
							goto IL_9F;
						case 4:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 2;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 0;
								continue;
								break;
							}
						}
						case 5:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_6D;
						}
						}
						goto IL_20;
						IL_6D:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							ptr = null;
							num2 = 3;
							break;
						}
					}
				}
			}
			IL_6B:
			IL_9F:
			((sprḣ*)ptr)->ᜀ = A_0;
			ptr = null;
			this.ᜁ(4132, num);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0008DEE8 File Offset: 0x0008CEE8
		private unsafe void ᜑ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprᲢ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_9F;
						case 1:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 5;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 2;
								continue;
								break;
							}
						}
						case 2:
							goto IL_6B;
						case 3:
							num2 = 1;
							continue;
						case 4:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_75;
						}
						case 5:
							goto IL_75;
						}
						goto IL_20;
						IL_75:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							ptr = null;
							num2 = 0;
							break;
						}
					}
				}
			}
			IL_6B:
			if (true)
			{
			}
			IL_9F:
			((sprᲢ*)ptr)->ᜀ = 2;
			((sprᲢ*)ptr)->ᜁ = 2;
			((sprᲢ*)ptr)->ᜂ = 1;
			((sprᲢ*)ptr)->ᜃ = 0U;
			((sprᲢ*)ptr)->ᜄ = 1643U;
			((sprᲢ*)ptr)->ᜅ = 82U;
			((sprᲢ*)ptr)->ᜆ = 714U;
			((sprᲢ*)ptr)->ᜇ = 397U;
			((sprᲢ*)ptr)->ᜈ = 129;
			((sprᲢ*)ptr)->ᜉ = 77;
			((sprᲢ*)ptr)->ᜊ = 15648;
			((sprᲢ*)ptr)->ᜋ = 0;
			ptr = null;
			this.ᜁ(4133, num);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0008E038 File Offset: 0x0008D038
		private unsafe void ᜀ(bool A_0, uint A_1, uint A_2, uint A_3, uint A_4)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_30:
				if (true)
				{
				}
				num = (ushort)sizeof(spr\u2273);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_4D:
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_D0;
						case 1:
							goto IL_FE;
						case 2:
							((spr\u2273*)ptr)->ᜀ = 5;
							num2 = 7;
							continue;
						case 3:
							if (A_0)
							{
								num2 = 2;
								continue;
							}
							((spr\u2273*)ptr)->ᜀ = 2;
							num2 = 0;
							continue;
						case 4:
							goto IL_FE;
						case 5:
							goto IL_D2;
						case 6:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 8;
								continue;
							}
							goto IL_D2;
						}
						case 7:
							goto IL_86;
						case 8:
							num2 = 9;
							continue;
						case 9:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 5;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								num2 = 4;
								continue;
								break;
							}
						}
						}
						goto IL_30;
						IL_D2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						default:
							if (false)
							{
							}
							ptr = null;
							num2 = 1;
							continue;
						}
						IL_FE:
						num2 = 3;
					}
				}
			}
			IL_86:
			IL_D0:
			((spr\u2273*)ptr)->ᜁ = 2;
			((spr\u2273*)ptr)->ᜂ = A_1;
			((spr\u2273*)ptr)->ᜃ = A_2;
			((spr\u2273*)ptr)->ᜄ = A_3;
			((spr\u2273*)ptr)->ᜅ = A_4;
			ptr = null;
			this.ᜁ(4175, num);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0008E19C File Offset: 0x0008D19C
		private unsafe void ᜂ(ushort A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u20A7);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 4;
							continue;
							break;
						}
					}
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 2:
						goto IL_75;
					case 3:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_75;
					}
					case 4:
						goto IL_69;
					case 5:
						goto IL_80;
					}
					break;
					IL_78:
					num2 = 5;
					continue;
					IL_75:
					ptr = null;
					goto IL_78;
				}
			}
			IL_69:
			IL_80:
			((spr\u20A7*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4134, num);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0008E27C File Offset: 0x0008D27C
		private unsafe void ᜐ()
		{
			if (true)
			{
			}
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(sprᮝ);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_80;
					case 1:
						goto IL_75;
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_75;
					}
					case 3:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 1;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 5;
							continue;
							break;
						}
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 5:
						goto IL_69;
					}
					break;
					IL_78:
					num2 = 0;
					continue;
					IL_75:
					ptr = null;
					goto IL_78;
				}
			}
			IL_69:
			IL_80:
			((sprᮝ*)ptr)->ᜀ = 1;
			ptr = null;
			this.ᜁ(4166, num);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0008E35C File Offset: 0x0008D35C
		private unsafe void ᜏ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u24A0);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 5;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 3;
							continue;
							break;
						}
					}
					case 1:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							if (true)
							{
							}
							num2 = 2;
							continue;
						}
						goto IL_75;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 3:
						goto IL_69;
					case 4:
						goto IL_80;
					case 5:
						goto IL_75;
					}
					break;
					IL_78:
					num2 = 4;
					continue;
					IL_75:
					ptr = null;
					goto IL_78;
				}
			}
			IL_69:
			IL_80:
			((spr\u24A0*)ptr)->ᜀ = 0;
			((spr\u24A0*)ptr)->ᜁ = 490U;
			((spr\u24A0*)ptr)->ᜂ = 895U;
			((spr\u24A0*)ptr)->ᜃ = 2606U;
			((spr\u24A0*)ptr)->ᜄ = 2456U;
			ptr = null;
			this.ᜁ(4161, num);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0008E46C File Offset: 0x0008D46C
		private new unsafe void ᜁ(ushort A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u204F);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 1:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 4;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 3;
							continue;
							break;
						}
					}
					case 2:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_63;
					}
					case 3:
						goto IL_61;
					case 4:
						goto IL_63;
					case 5:
						goto IL_80;
					}
					break;
					IL_78:
					num2 = 5;
					continue;
					IL_63:
					if (true)
					{
					}
					ptr = null;
					goto IL_78;
				}
			}
			IL_61:
			IL_80:
			((spr\u204F*)ptr)->ᜀ = A_0;
			ptr = null;
			this.ᜁ(4125, num);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0008E54C File Offset: 0x0008D54C
		private unsafe void ᜎ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1BFC);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_80;
					case 1:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_75;
					}
					case 2:
						goto IL_75;
					case 3:
						goto IL_61;
					case 4:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 3;
							continue;
							break;
						}
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 4;
							continue;
						}
						break;
					}
					break;
					IL_78:
					num2 = 0;
					continue;
					IL_75:
					ptr = null;
					goto IL_78;
				}
			}
			IL_61:
			if (true)
			{
			}
			IL_80:
			((spr\u1BFC*)ptr)->ᜀ = 1;
			((spr\u1BFC*)ptr)->ᜁ = 1;
			((spr\u1BFC*)ptr)->ᜂ = 1;
			((spr\u1BFC*)ptr)->ᜃ = 1;
			ptr = null;
			this.ᜁ(4128, num);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0008E644 File Offset: 0x0008D644
		private unsafe void \u170D()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1AF6);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_80;
					case 1:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 3;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 5;
							continue;
							break;
						}
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 3:
						goto IL_75;
					case 4:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							if (true)
							{
							}
							num2 = 2;
							continue;
						}
						goto IL_75;
					}
					case 5:
						goto IL_69;
					}
					break;
					IL_78:
					num2 = 0;
					continue;
					IL_75:
					ptr = null;
					goto IL_78;
				}
			}
			IL_69:
			IL_80:
			((spr\u1AF6*)ptr)->ᜀ = 0;
			((spr\u1AF6*)ptr)->ᜁ = 0;
			((spr\u1AF6*)ptr)->ᜂ = 1;
			((spr\u1AF6*)ptr)->ᜃ = 0;
			((spr\u1AF6*)ptr)->ᜄ = 1;
			((spr\u1AF6*)ptr)->ᜅ = 0;
			((spr\u1AF6*)ptr)->ᜆ = 0;
			((spr\u1AF6*)ptr)->ᜇ = 0;
			((spr\u1AF6*)ptr)->ᜈ = 239;
			ptr = null;
			this.ᜁ(4194, num);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0008E768 File Offset: 0x0008D768
		private unsafe void ᜌ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u17D7);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 1:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_63;
					}
					case 2:
					{
						byte[] u;
						if (u.Length == 0)
						{
							if (true)
							{
							}
							num2 = 5;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 4;
							continue;
							break;
						}
					}
					case 3:
						goto IL_78;
					case 4:
						goto IL_61;
					case 5:
						goto IL_63;
					}
					break;
					IL_70:
					num2 = 3;
					continue;
					IL_63:
					ptr = null;
					goto IL_70;
				}
			}
			IL_61:
			IL_78:
			((spr\u17D7*)ptr)->ᜀ = 2;
			((spr\u17D7*)ptr)->ᜁ = 0;
			((spr\u17D7*)ptr)->ᜂ = 3;
			((spr\u17D7*)ptr)->ᜃ = 1;
			((spr\u17D7*)ptr)->ᜄ = 0U;
			((spr\u17D7*)ptr)->ᜆ = 35;
			((spr\u17D7*)ptr)->ᜇ = 77;
			ptr = null;
			this.ᜁ(4126, num);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0008E87C File Offset: 0x0008D87C
		private unsafe void ᜋ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				if (true)
				{
				}
				num = (ushort)sizeof(spr\u1F1E);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_80;
					case 1:
						goto IL_69;
					case 2:
						goto IL_75;
					case 3:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 1;
							continue;
							break;
						}
					}
					case 4:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_75;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					}
					break;
					IL_78:
					num2 = 0;
					continue;
					IL_75:
					ptr = null;
					goto IL_78;
				}
			}
			IL_69:
			IL_80:
			((spr\u1F1E*)ptr)->ᜀ = 0.0;
			((spr\u1F1E*)ptr)->ᜁ = 0.0;
			((spr\u1F1E*)ptr)->ᜂ = 0.0;
			((spr\u1F1E*)ptr)->ᜃ = 0.0;
			((spr\u1F1E*)ptr)->ᜄ = 0.0;
			((spr\u1F1E*)ptr)->ᜅ = 31;
			ptr = null;
			this.ᜁ(4127, num);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0008E9AC File Offset: 0x0008D9AC
		private unsafe void ᜊ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(spr\u2376);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_7F;
						}
						case 1:
							goto IL_7D;
						case 2:
							if (true)
							{
							}
							goto IL_7F;
						case 3:
							num2 = 5;
							continue;
						case 4:
							goto IL_97;
						case 5:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 2;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								break;
							}
						}
						}
						goto IL_20;
						IL_7F:
						ptr = null;
						num2 = 4;
					}
				}
			}
			IL_7D:
			IL_97:
			((spr\u2376*)ptr)->ᜀ = 1;
			ptr = null;
			this.ᜁ(4129, num);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0008EA90 File Offset: 0x0008DA90
		private unsafe void ᜀ(ChartStyle A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1DF3);
				Array.Clear(this.\u1713, 0, (int)num);
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if ((A_0 & ChartStyle.Pie) != ChartStyle.Pie)
						{
							num2 = 7;
							continue;
						}
						goto IL_112;
					case 1:
						goto IL_CD;
					case 2:
						goto IL_112;
					case 3:
						goto IL_84;
					case 4:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 11;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 8;
							continue;
							break;
						}
					}
					case 5:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 6;
							continue;
						}
						goto IL_A2;
					}
					case 6:
						num2 = 4;
						continue;
					case 7:
						num2 = 9;
						continue;
					case 8:
						goto IL_84;
					case 9:
						if ((A_0 & ChartStyle.Pie3d) == ChartStyle.Pie3d)
						{
							num2 = 2;
							continue;
						}
						((spr\u1DF3*)ptr)->ᜁ = 0;
						num2 = 1;
						continue;
					case 10:
						goto IL_125;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_84;
						}
						if (false)
						{
						}
						goto IL_A2;
					}
					break;
					IL_84:
					num2 = 0;
					continue;
					IL_A2:
					if (true)
					{
					}
					ptr = null;
					num2 = 3;
					continue;
					IL_112:
					((spr\u1DF3*)ptr)->ᜁ = 1;
					num2 = 10;
				}
			}
			IL_CD:
			IL_125:
			((spr\u1DF3*)ptr)->ᜂ = 0;
			ptr = null;
			this.ᜁ(4116, num);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0008EBF4 File Offset: 0x0008DBF4
		private unsafe void ᜀ(bool A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				num = (ushort)sizeof(spr\u1AE1);
				Array.Clear(this.\u1713, 0, (int)num);
				if (true)
				{
				}
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_CD;
					case 1:
						goto IL_69;
					case 2:
						goto IL_CF;
					case 3:
					{
						byte[] u;
						if ((u = this.\u1713) != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_CF;
					}
					case 4:
						num2 = 6;
						continue;
					case 5:
						if (!A_0)
						{
							((spr\u1AE1*)ptr)->ᜂ = 0;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_69;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 6:
					{
						byte[] u;
						if (u.Length == 0)
						{
							num2 = 2;
							continue;
						}
						fixed (byte* ptr = &u[0])
						{
							num2 = 7;
							continue;
							break;
						}
					}
					case 7:
						goto IL_DF;
					case 8:
						goto IL_DF;
					case 9:
						goto IL_79;
					}
					break;
					IL_69:
					((spr\u1AE1*)ptr)->ᜂ = 1;
					num2 = 9;
					continue;
					IL_CF:
					ptr = null;
					num2 = 8;
					continue;
					IL_DF:
					((spr\u1AE1*)ptr)->ᜀ = 0;
					((spr\u1AE1*)ptr)->ᜁ = 150;
					num2 = 5;
				}
			}
			IL_79:
			IL_CD:
			ptr = null;
			this.ᜁ(4119, num);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0008ED40 File Offset: 0x0008DD40
		private unsafe void ᜉ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(spr\u22EF);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_88;
						case 1:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 3;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									num2 = 0;
									continue;
								}
								break;
							}
						}
						case 2:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 4;
								continue;
							}
							goto IL_94;
						}
						case 3:
							goto IL_94;
						case 4:
							num2 = 1;
							continue;
						case 5:
							goto IL_A2;
						}
						goto IL_20;
						IL_94:
						ptr = null;
						num2 = 5;
					}
				}
			}
			IL_88:
			IL_A2:
			((spr\u22EF*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4120, num);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0008EE28 File Offset: 0x0008DE28
		private unsafe void ᜈ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprẮ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (true)
							{
							}
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 4;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								break;
							}
						}
						case 1:
							goto IL_7D;
						case 2:
							num2 = 0;
							continue;
						case 3:
							goto IL_97;
						case 4:
							goto IL_7F;
						case 5:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_7F;
						}
						}
						goto IL_20;
						IL_7F:
						ptr = null;
						num2 = 3;
					}
				}
			}
			IL_7D:
			IL_97:
			((sprẮ*)ptr)->ᜀ = 0;
			((sprẮ*)ptr)->ᜁ = 0;
			((sprẮ*)ptr)->ᜂ = 1;
			ptr = null;
			this.ᜁ(4121, num);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0008EF1C File Offset: 0x0008DF1C
		private unsafe void ᜇ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprᦦ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_91;
						}
						case 1:
							num2 = 5;
							continue;
						case 2:
							goto IL_91;
						case 3:
							goto IL_9F;
						case 4:
							goto IL_7D;
						case 5:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 2;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 4;
									continue;
								}
								break;
							}
						}
						}
						goto IL_20;
						IL_91:
						ptr = null;
						num2 = 3;
					}
				}
			}
			IL_7D:
			if (true)
			{
			}
			IL_9F:
			((sprᦦ*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4122, num);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0008F000 File Offset: 0x0008E000
		private unsafe void ᜆ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprḲ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 1;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 4;
									continue;
								}
								break;
							}
						}
						case 1:
							goto IL_7F;
						case 2:
							num2 = 0;
							continue;
						case 3:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_7F;
						}
						case 4:
							goto IL_7D;
						case 5:
							goto IL_97;
						}
						goto IL_20;
						IL_7F:
						ptr = null;
						num2 = 5;
					}
				}
			}
			IL_7D:
			goto IL_BE;
			IL_97:
			if (true)
			{
			}
			IL_BE:
			((sprḲ*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4159, num);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0008F0E4 File Offset: 0x0008E0E4
		private unsafe void ᜅ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(spr\u23C5);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_94;
						}
						case 1:
							goto IL_A2;
						case 2:
							num2 = 5;
							continue;
						case 3:
							goto IL_88;
						case 4:
							goto IL_94;
						case 5:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 4;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							}
						}
						}
						goto IL_20;
						IL_94:
						ptr = null;
						num2 = 1;
					}
				}
			}
			IL_88:
			IL_A2:
			((spr\u23C5*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4158, num);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0008F1CC File Offset: 0x0008E1CC
		private unsafe void ᜄ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(spr\u1D40);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_97;
						case 1:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 5;
								continue;
							}
							goto IL_7F;
						}
						case 2:
						{
							if (true)
							{
							}
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 4;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							}
						}
						case 3:
							goto IL_7D;
						case 4:
							goto IL_7F;
						case 5:
							num2 = 2;
							continue;
						}
						goto IL_20;
						IL_7F:
						ptr = null;
						num2 = 0;
					}
				}
			}
			IL_7D:
			IL_97:
			((spr\u1D40*)ptr)->ᜀ = 0;
			ptr = null;
			this.ᜁ(4160, num);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0008F2B0 File Offset: 0x0008E2B0
		private unsafe void ᜃ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_28:
				num = (ushort)sizeof(spr\u1FFC);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_3D:
					int num2 = 3;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 5;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_3D;
								default:
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								break;
							}
						}
						case 1:
							goto IL_88;
						case 2:
							goto IL_A2;
						case 3:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 4;
								continue;
							}
							goto IL_94;
						}
						case 4:
							num2 = 0;
							continue;
						case 5:
							goto IL_94;
						}
						goto IL_28;
						IL_94:
						ptr = null;
						num2 = 2;
					}
				}
			}
			IL_88:
			IL_A2:
			((spr\u1FFC*)ptr)->ᜀ = 20;
			((spr\u1FFC*)ptr)->ᜁ = 15;
			((spr\u1FFC*)ptr)->ᜂ = 30;
			((spr\u1FFC*)ptr)->ᜃ = 100;
			((spr\u1FFC*)ptr)->ᜄ = 100;
			((spr\u1FFC*)ptr)->ᜅ = 150;
			((spr\u1FFC*)ptr)->ᜆ = 21;
			ptr = null;
			this.ᜁ(4154, num);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0008F3D0 File Offset: 0x0008E3D0
		private unsafe void ᜀ(byte A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprᯠ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_88;
						case 1:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 3;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 0;
									continue;
								}
								break;
							}
						}
						case 2:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 5;
								continue;
							}
							goto IL_94;
						}
						case 3:
							goto IL_94;
						case 4:
							goto IL_A2;
						case 5:
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						goto IL_20;
						IL_94:
						ptr = null;
						num2 = 4;
					}
				}
			}
			IL_88:
			IL_A2:
			((sprᯠ*)ptr)->ᜀ = 3239U;
			((sprᯠ*)ptr)->ᜁ = 1947U;
			((sprᯠ*)ptr)->ᜂ = 710U;
			((sprᯠ*)ptr)->ᜃ = 333U;
			((sprᯠ*)ptr)->ᜄ = A_0;
			((sprᯠ*)ptr)->ᜅ = 1;
			((sprᯠ*)ptr)->ᜆ = 31;
			ptr = null;
			this.ᜁ(4117, num);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0008F4F8 File Offset: 0x0008E4F8
		private unsafe void ᜂ()
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprក);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_91;
						case 1:
							goto IL_9F;
						case 2:
							num2 = 4;
							continue;
						case 3:
							goto IL_7D;
						case 4:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 0;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							}
						}
						case 5:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_91;
						}
						}
						goto IL_20;
						IL_91:
						ptr = null;
						num2 = 1;
					}
				}
			}
			IL_7D:
			if (true)
			{
			}
			IL_9F:
			((sprក*)ptr)->ᜀ = 1;
			((sprក*)ptr)->ᜁ = 0;
			((sprក*)ptr)->ᜂ = 0;
			ptr = null;
			this.ᜁ(4135, num);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0008F5EC File Offset: 0x0008E5EC
		private unsafe void ᜀ(ushort A_0)
		{
			ushort num;
			byte* ptr;
			for (;;)
			{
				IL_20:
				num = (ushort)sizeof(sprᠲ);
				Array.Clear(this.\u1713, 0, (int)num);
				for (;;)
				{
					IL_35:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_88;
						case 1:
							goto IL_A2;
						case 2:
						{
							byte[] u;
							if ((u = this.\u1713) != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_94;
						}
						case 3:
							num2 = 5;
							continue;
						case 4:
							goto IL_94;
						case 5:
						{
							byte[] u;
							if (u.Length == 0)
							{
								num2 = 4;
								continue;
							}
							fixed (byte* ptr = &u[0])
							{
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_35;
								default:
									if (false)
									{
									}
									num2 = 0;
									continue;
								}
								break;
							}
						}
						}
						goto IL_20;
						IL_94:
						ptr = null;
						num2 = 1;
					}
				}
			}
			IL_88:
			IL_A2:
			((sprᠲ*)ptr)->ᜀ = A_0;
			ptr = null;
			this.ᜁ(4197, num);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0008F6D4 File Offset: 0x0008E6D4
		private new WorkSheet ᜁ()
		{
			int a_ = 2;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			WorkSheet workSheet = new WorkSheet();
			workSheet.DataExported = false;
			workSheet.SheetName = HyperlinksCollectionEditor.b("嬝嘟䌡䠣匥䤧帩䔫䄭帯", a_);
			Cell cell = workSheet.AddString(2, 1, HyperlinksCollectionEditor.b("䴝借䬡嘣䌥ا温䴫娭儯眱䰳䘵圷䠹䠻ḽ̿ⵁ⥃㙅❇⑉⥋⁍⑏牑ݓ⍕ㅗ⹙籛㡝ཟၡ䑣⭥ŧ३ṫŭͯᵱታɵ塷ⱹᕻൽꚅ\udb87ﺉ曆憐﶑몓\ud895\udd97캙", a_));
			cell.Format.Font.Bold = true;
			cell = workSheet.AddString(2, 2, HyperlinksCollectionEditor.b("笝ട䬡䜣䌥䨧䘩夫䬭ု儱嬳嬵䠷嬹刻䜽怿แぃ≅晇橉繋繍恏恑祓摕桗橙橛繝⅟๡ࡣ䙥ᩧͩ୫٭ѯű味ѵᵷॹ᥻౽", a_));
			cell.Format.Font.Bold = true;
			workSheet.AddString(2, 4, HyperlinksCollectionEditor.b("嘝伟伡䄣إ砧䬩䬫䬭", a_));
			workSheet.AddHyperLink(2, 5, HyperlinksCollectionEditor.b("瘝吟嘡吣ᰥܧԩ嬫夭䜯ᰱ儳ᬵ儷夹夻尽ⰿ㝁⅃桅⭇╉⅋慍㑏㍑⁓㝕㵗≙ⱛㅝ቟ᙡ䩣๥ᱧݩ", a_), HyperlinksCollectionEditor.b("瘝吟嘡吣ᰥܧԩ嬫夭䜯ᰱ儳ᬵ儷夹夻尽ⰿ㝁⅃桅⭇╉⅋慍㑏㍑⁓㝕㵗≙ⱛㅝ቟ᙡ䩣๥ᱧݩ", a_));
			workSheet.AddString(2, 7, HyperlinksCollectionEditor.b("崝伟䰡倣䜥䬧帩ఫ笭振", a_));
			workSheet.AddHyperLink(2, 8, HyperlinksCollectionEditor.b("洝唟刡吣䤥娧帩氫䬭ᴯ嬱圳匵娷嘹䤻嬽渿⅁⭃⭅", a_), HyperlinksCollectionEditor.b("猝䄟䬡䠣別䜧ဩ弫嬭䀯䈱嬳䐵䰷稹夻ጽ⤿⅁⅃⑅⑇㽉⥋恍㍏㵑㥓", a_));
			workSheet.AddString(2, 10, HyperlinksCollectionEditor.b("尝唟嬡У栥䜧崩ഫ", a_));
			workSheet.AddHyperLink(2, 11, HyperlinksCollectionEditor.b("瘝吟嘡吣ᰥܧԩ嬫夭䜯ᰱ儳ᬵ儷夹夻尽ⰿ㝁⅃桅⭇╉⅋慍㑏㍑⁓㝕㵗≙ⱛㅝ቟ᙡ䭣ͥၧᩩͫᱭѯݱٳᕵၷ᭹ཻ᭽깿", a_), HyperlinksCollectionEditor.b("瘝吟嘡吣ᰥܧԩ嬫夭䜯ᰱ儳ᬵ儷夹夻尽ⰿ㝁⅃桅⭇╉⅋慍㑏㍑⁓㝕㵗≙ⱛㅝ቟ᙡ䭣ͥၧᩩͫᱭѯݱٳᕵၷ᭹ཻ᭽깿", a_));
			this.Sheets.Add(workSheet);
			return workSheet;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0008F850 File Offset: 0x0008E850
		protected void ExecuteExport()
		{
			int a_ = 1;
			switch (0)
			{
			default:
				for (;;)
				{
					WorkSheet workSheet = null;
					this.ᜣ();
					int num = 0;
					IEnumerator enumerator = this.\u1718.GetEnumerator();
					if (true)
					{
					}
					int num2 = 5;
					for (;;)
					{
						bool flag;
						bool flag3;
						switch (num2)
						{
						case 0:
							if (this.\u1718.Count != 0)
							{
								num2 = 4;
								continue;
							}
							num2 = 3;
							continue;
						case 1:
							try
							{
								num2 = 4;
								for (;;)
								{
									int num3;
									IEnumerator enumerator2;
									switch (num2)
									{
									case 0:
										if (this.Sheets[num3].DataExported)
										{
											num2 = 3;
											continue;
										}
										goto IL_58E;
									case 1:
										if (this.Sheets[num3].Exported)
										{
											num2 = 18;
											continue;
										}
										goto IL_4B1;
									case 2:
										goto IL_10DE;
									case 3:
										goto IL_2B6;
									case 5:
										num2 = 24;
										continue;
									case 6:
										if (this.\u1718[0].DataTable.TableName != string.Empty)
										{
											num2 = 27;
											continue;
										}
										goto IL_38E;
									case 7:
										goto IL_369;
									case 8:
										goto IL_4AC;
									case 9:
										if (num3 >= this.Sheets.Count)
										{
											num2 = 17;
											continue;
										}
										num2 = 1;
										continue;
									case 10:
										goto IL_33A;
									case 11:
										try
										{
											num2 = 4;
											for (;;)
											{
												switch (num2)
												{
												case 0:
												{
													WorkSheet workSheet2;
													if (workSheet2.Exported)
													{
														num2 = 1;
														continue;
													}
													break;
												}
												case 1:
													num2 = 5;
													continue;
												case 2:
												{
													if (!enumerator2.MoveNext())
													{
														num2 = 3;
														continue;
													}
													WorkSheet workSheet2 = (WorkSheet)enumerator2.Current;
													num2 = 0;
													continue;
												}
												case 3:
													goto IL_25C;
												case 5:
												{
													WorkSheet workSheet2;
													if (workSheet2.DataExported)
													{
														num2 = 6;
														continue;
													}
													break;
												}
												case 6:
													try
													{
														WorkSheet workSheet2;
														spr\u2059.ᜀ(workSheet2.DataSource, workSheet2.SQLCommand, workSheet2.DataTable, workSheet2.ListView);
														break;
													}
													catch (Exception ex)
													{
														WorkSheet workSheet2;
														throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("尜洞䘠倢稤琦䄨个䠬嬮田嘲匴帶圸帺", a_)), workSheet2.SheetName, ex.Message));
													}
													goto IL_25C;
												case 7:
													goto IL_268;
												}
												IL_1E6:
												num2 = 2;
												continue;
												goto IL_1E6;
												IL_25C:
												num2 = 7;
											}
											IL_268:
											goto IL_3B2;
										}
										finally
										{
											for (;;)
											{
												IDisposable disposable = enumerator2 as IDisposable;
												num2 = 0;
												for (;;)
												{
													switch (num2)
													{
													case 0:
														if (disposable != null)
														{
															num2 = 1;
															continue;
														}
														goto IL_2B5;
													case 1:
														disposable.Dispose();
														num2 = 2;
														continue;
													case 2:
														goto IL_2B3;
													}
													break;
												}
											}
											IL_2B3:
											IL_2B5:;
										}
										goto IL_2B6;
										IL_3B2:
										this.BeginDataExport();
										this.BeforeExport();
										num2 = 21;
										continue;
									case 12:
										goto IL_33A;
									case 13:
										if (this.\u1718[0].DataSource == ExportSource.DataTable)
										{
											num2 = 16;
											continue;
										}
										goto IL_38E;
									case 14:
										goto IL_10D2;
									case 15:
										this.\u1718.Remove(workSheet);
										num2 = 14;
										continue;
									case 16:
										num2 = 28;
										continue;
									case 17:
										this.ᜡ();
										this.ᜢ();
										this.AfterExport();
										this.EndDataExport();
										num2 = 30;
										continue;
									case 18:
										this.ᜁ(this, num3);
										num2 = 0;
										continue;
									case 19:
										this.\u1718.Add(new WorkSheet()).LoadFromXLS();
										num2 = 13;
										continue;
									case 20:
										num2 = 8;
										continue;
									case 21:
										if (base.Stoped)
										{
											num2 = 20;
											continue;
										}
										num3 = 0;
										num2 = 10;
										continue;
									case 22:
										goto IL_4B1;
									case 23:
										num2 = 6;
										continue;
									case 24:
										if (workSheet != null)
										{
											num2 = 15;
											continue;
										}
										goto IL_10D2;
									case 25:
										workSheet = this.ᜁ();
										num2 = 7;
										continue;
									case 26:
										goto IL_58E;
									case 27:
										this.\u1718[0].SheetName = this.\u1718[0].DataTable.TableName;
										num2 = 31;
										continue;
									case 28:
										if (this.\u1718[0].DataTable != null)
										{
											num2 = 23;
											continue;
										}
										goto IL_38E;
									case 29:
										if (spr\u2561.ᜀ)
										{
											num2 = 25;
											continue;
										}
										goto IL_369;
									case 30:
										if (spr\u2561.ᜀ)
										{
											num2 = 5;
											continue;
										}
										goto IL_10D2;
									case 31:
										goto IL_38E;
									}
									if (flag)
									{
										num2 = 19;
										continue;
									}
									goto IL_38E;
									try
									{
										IL_58E:
										num2 = 11;
										for (;;)
										{
											switch (num2)
											{
											case 0:
											{
												ExportSource dataSource;
												if (dataSource == ExportSource.ListView)
												{
													num2 = 3;
													continue;
												}
												goto IL_DF0;
											}
											case 1:
											{
												IEnumerator enumerator3 = this.Sheets[num3].ColumnsExport.GetEnumerator();
												num2 = 13;
												continue;
											}
											case 2:
												if (this.Sheets[num3].AutoFitColWidth)
												{
													num2 = 18;
													continue;
												}
												num2 = 27;
												continue;
											case 3:
												this.Sheets[num3].SkipRows = Math.Min(this.Sheets[num3].SkipRows, this.Sheets[num3].ListView.Items.Count);
												num2 = 22;
												continue;
											case 4:
												goto IL_DBC;
											case 5:
												goto IL_FEA;
											case 6:
												if (base.Stoped)
												{
													num2 = 17;
													continue;
												}
												num2 = 9;
												continue;
											case 7:
												goto IL_F7A;
											case 8:
												if (this.Sheets[num3].DataExported)
												{
													num2 = 4;
													continue;
												}
												goto IL_1084;
											case 9:
												if (this.Sheets[num3].DataSource == ExportSource.SqlCommand)
												{
													num2 = 30;
													continue;
												}
												goto IL_8A8;
											case 10:
												this.Sheets[num3].MaxRows = Math.Min(this.Sheets[num3].MaxRows, this.Sheets[num3].ListView.Items.Count);
												goto IL_E7F;
											case 12:
												goto IL_EDD;
											case 13:
											{
												try
												{
													num2 = 0;
													for (;;)
													{
														switch (num2)
														{
														case 1:
															goto IL_75F;
														case 2:
														{
															IEnumerator enumerator3;
															if (!enumerator3.MoveNext())
															{
																num2 = 4;
																continue;
															}
															ColumnExport columnExport = (ColumnExport)enumerator3.Current;
															this.ᜎ.Add(columnExport.Width);
															num2 = 3;
															continue;
														}
														case 4:
															num2 = 1;
															continue;
														}
														IL_739:
														num2 = 2;
														continue;
														goto IL_739;
													}
													IL_75F:
													goto IL_F19;
												}
												finally
												{
													for (;;)
													{
														IEnumerator enumerator3;
														IDisposable disposable2 = enumerator3 as IDisposable;
														num2 = 2;
														for (;;)
														{
															switch (num2)
															{
															case 0:
																goto IL_7AA;
															case 1:
																disposable2.Dispose();
																num2 = 0;
																continue;
															case 2:
																if (disposable2 != null)
																{
																	num2 = 1;
																	continue;
																}
																goto IL_7AC;
															}
															break;
														}
													}
													IL_7AA:
													IL_7AC:;
												}
												goto IL_7AD;
												IL_F19:
												ExportSource dataSource = this.Sheets[num3].DataSource;
												switch ((1 == 1) ? 1 : 0)
												{
												case 0:
												case 2:
													goto IL_E7F;
												default:
													if (false)
													{
													}
													num2 = 0;
													continue;
												}
												break;
											}
											case 14:
												goto IL_1084;
											case 15:
												goto IL_10A3;
											case 16:
												goto IL_8A8;
											case 17:
												num2 = 31;
												continue;
											case 18:
												this.ᜠ();
												num2 = 24;
												continue;
											case 19:
												this.Sheets[num3].ᜀ(this.ExportLongColumn);
												num2 = 5;
												continue;
											case 20:
												if (this.Sheets[num3].AllowTitles)
												{
													num2 = 23;
													continue;
												}
												goto IL_EDD;
											case 21:
												this.ᜠ();
												num2 = 28;
												continue;
											case 22:
												if (this.Sheets[num3].MaxRows > 0)
												{
													num2 = 10;
													continue;
												}
												goto IL_DF0;
											case 23:
												goto IL_7AD;
											case 24:
												goto IL_1064;
											case 25:
												goto IL_DF0;
											case 26:
												if (base.Stoped)
												{
													num2 = 33;
													continue;
												}
												num2 = 8;
												continue;
											case 27:
												if (this.Sheets[num3].AutoFitTitleWidth)
												{
													num2 = 21;
													continue;
												}
												goto IL_1064;
											case 28:
												goto IL_1064;
											case 29:
												goto IL_E90;
											case 30:
												Monitor.Enter(this);
												spr\u2059.ᜀ = this.Sheets[num3].SQLCommand.ExecuteReader();
												num2 = 16;
												continue;
											case 31:
												goto IL_F8B;
											case 32:
												if (this.Sheets[num3].DataExported)
												{
													num2 = 1;
													continue;
												}
												goto IL_E90;
											case 33:
												num2 = 7;
												continue;
											}
											if (this.Sheets[num3].DataExported)
											{
												num2 = 19;
												continue;
											}
											goto IL_FEA;
											try
											{
												IL_8A8:
												for (;;)
												{
													spr\u2059.ᜀ(this.Sheets[num3].DataSource, this.Sheets[num3].DataTable, this.Sheets[num3].ListView);
													int a_2 = this.Sheets[num3].RecordCounter;
													spr\u2059.ᜀ(this.Sheets[num3].DataSource, spr\u2059.ᜀ, this.Sheets[num3].DataTable, this.Sheets[num3].SkipRows, this.m_skippedRecord, this, ref a_2);
													num2 = 14;
													for (;;)
													{
														ExportRowEventArgs exportRowEventArgs;
														bool flag2;
														switch (num2)
														{
														case 0:
															if (exportRowEventArgs.Accept)
															{
																num2 = 12;
																continue;
															}
															goto IL_9FC;
														case 1:
															goto IL_C95;
														case 2:
															this.Sheets[num3].ᜋ();
															num2 = 8;
															continue;
														case 3:
															num2 = 18;
															continue;
														case 4:
															if (base.Stoped)
															{
																num2 = 11;
																continue;
															}
															goto IL_C95;
														case 5:
															num2 = 21;
															continue;
														case 6:
															num2 = 17;
															continue;
														case 7:
															if (base.CanContinue())
															{
																num2 = 1;
																continue;
															}
															goto IL_D52;
														case 8:
															goto IL_B89;
														case 9:
															goto IL_AC1;
														case 10:
															goto IL_9FC;
														case 11:
															num2 = 7;
															continue;
														case 12:
														{
															XLSExportRowEventArgs xlsexportRowEventArgs = new XLSExportRowEventArgs(num3, this.Sheets[num3].ExportRowExport, exportRowEventArgs.Accept);
															this.ᜀ(this, xlsexportRowEventArgs);
															flag2 = xlsexportRowEventArgs.Accept;
															num2 = 10;
															continue;
														}
														case 13:
															goto IL_D5E;
														case 14:
															if (base.Stoped)
															{
																num2 = 5;
																continue;
															}
															this.Sheets[num3].RecordCounter = 0;
															num2 = 20;
															continue;
														case 15:
															if (!spr\u2059.ᜀ(this.Sheets[num3].DataSource, this.Sheets[num3].DataTable, this.Sheets[num3].ListView, this.Sheets[num3].RecordCounter, this.Sheets[num3].MaxRows, this.Sheets[num3].SkipRows))
															{
																num2 = 6;
																continue;
															}
															goto IL_D52;
														case 16:
															goto IL_C33;
														case 17:
															if (this.Sheets[num3].MaxRows != 0)
															{
																num2 = 25;
																continue;
															}
															goto IL_CB9;
														case 18:
															if (this.Sheets[num3].RecordCounter < this.ᜀ)
															{
																num2 = 16;
																continue;
															}
															goto IL_D52;
														case 19:
															if (this.Sheets[num3].RecordCounter >= this.Sheets[num3].MaxRows)
															{
																num2 = 22;
																continue;
															}
															goto IL_CB9;
														case 20:
															goto IL_AC1;
														case 21:
															goto IL_B60;
														case 22:
															goto IL_D52;
														case 23:
															if (flag2)
															{
																num2 = 2;
																continue;
															}
															goto IL_B89;
														case 24:
															if (spr\u2561.ᜀ)
															{
																num2 = 3;
																continue;
															}
															goto IL_C33;
														case 25:
															num2 = 19;
															continue;
														}
														break;
														IL_9FC:
														num2 = 23;
														continue;
														IL_AC1:
														num2 = 15;
														continue;
														IL_B89:
														a_2 = this.Sheets[num3].RecordCounter;
														spr\u2059.ᜀ(this.Sheets[num3].DataSource, spr\u2059.ᜀ, this.Sheets[num3].DataTable, ref a_2);
														this.Sheets[num3].RecordCounter = a_2;
														this.\u171D++;
														base.ᜄ(this, this.\u171D);
														this.ᜀ(this, new XLSDataRowEventArgs(num3, this.Sheets[num3].RecordCounter));
														Thread.Sleep(0);
														num2 = 9;
														continue;
														IL_C33:
														this.Sheets[num3].ᜆ();
														flag2 = true;
														exportRowEventArgs = new ExportRowEventArgs(this.Sheets[num3].ExportRowExport, flag2);
														base.ᜀ(this, exportRowEventArgs);
														num2 = 0;
														continue;
														IL_C95:
														num2 = 24;
														continue;
														IL_CB9:
														num2 = 4;
														continue;
														IL_D52:
														num2 = 13;
													}
												}
												IL_B60:
												break;
												IL_D5E:
												goto IL_653;
											}
											finally
											{
												num2 = 1;
												for (;;)
												{
													switch (num2)
													{
													case 0:
														spr\u2059.ᜀ.Close();
														Monitor.Exit(this);
														num2 = 2;
														continue;
													case 2:
														goto IL_DB9;
													}
													if (this.Sheets[num3].DataSource != ExportSource.SqlCommand)
													{
														break;
													}
													num2 = 0;
												}
												IL_DB9:;
											}
											goto IL_DBC;
											IL_653:
											this.Sheets[num3].ᜈ();
											num2 = 2;
											continue;
											IL_7AD:
											this.Sheets[num3].ᜉ();
											num2 = 12;
											continue;
											IL_DBC:
											num2 = 20;
											continue;
											IL_DF0:
											this.Sheets[num3].TotalCols = this.Sheets[num3].ColumnsExport.Count + (int)this.Sheets[num3].StartDataCol;
											num2 = 29;
											continue;
											IL_E7F:
											num2 = 25;
											continue;
											IL_E90:
											this.ᜂ(this.Sheets[num3]);
											this.Sheets[num3].ᜊ();
											num2 = 26;
											continue;
											IL_EDD:
											num2 = 6;
											continue;
											IL_FEA:
											base.Stoped = false;
											this.ᜎ.Clear();
											num2 = 32;
											continue;
											IL_1064:
											this.Sheets[num3].ᜂ();
											num2 = 14;
											continue;
											IL_1084:
											this.ᜁ(this.Sheets[num3]);
											num2 = 15;
										}
										IL_F7A:
										IL_F8B:
										break;
										IL_10A3:
										goto IL_150;
									}
									finally
									{
										spr\u2059.ᜀ(this.Sheets[num3].DataSource, this.Sheets[num3].ListView);
									}
									goto IL_10D2;
									IL_150:
									this.ᜀ(this, num3);
									num2 = 22;
									continue;
									IL_2B6:
									spr\u2059.ᜁ(this.Sheets[num3].DataSource, this.Sheets[num3].ListView);
									num2 = 26;
									continue;
									IL_33A:
									num2 = 9;
									continue;
									IL_369:
									this.\u171D = 0;
									enumerator2 = this.\u1718.GetEnumerator();
									num2 = 11;
									continue;
									IL_38E:
									num2 = 29;
									continue;
									IL_4B1:
									num3++;
									num2 = 12;
									continue;
									IL_10D2:
									num2 = 2;
								}
								IL_4AC:
								IL_10DE:
								return;
							}
							finally
							{
								num2 = 5;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										if (this.Sheets.Count == 1)
										{
											num2 = 2;
											continue;
										}
										goto IL_111B;
									case 1:
										goto IL_112F;
									case 2:
										this.Sheets[0].SaveToXLS();
										num2 = 3;
										continue;
									case 3:
										goto IL_111B;
									case 4:
										num2 = 0;
										continue;
									}
									if (flag)
									{
										num2 = 4;
										continue;
									}
									break;
									IL_111B:
									this.\u1718.Clear();
									num2 = 1;
								}
								IL_112F:;
							}
							goto IL_1170;
						case 2:
							flag3 = (num == 0);
							goto IL_12A4;
						case 3:
							flag3 = true;
							goto IL_12A4;
						case 4:
							goto IL_1170;
						case 5:
							try
							{
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
									{
										WorkSheet workSheet3;
										if (workSheet3.Exported)
										{
											num2 = 5;
											continue;
										}
										break;
									}
									case 2:
										goto IL_1216;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											num2 = 4;
											continue;
										}
										WorkSheet workSheet3 = (WorkSheet)enumerator.Current;
										num2 = 0;
										continue;
									}
									case 4:
										num2 = 2;
										continue;
									case 5:
										num++;
										num2 = 6;
										continue;
									}
									IL_11DE:
									num2 = 3;
									continue;
									goto IL_11DE;
								}
								IL_1216:;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable3 = enumerator as IDisposable;
									num2 = 0;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											if (disposable3 != null)
											{
												num2 = 2;
												continue;
											}
											goto IL_1260;
										case 1:
											goto IL_125E;
										case 2:
											disposable3.Dispose();
											num2 = 1;
											continue;
										}
										break;
									}
								}
								IL_125E:
								IL_1260:;
							}
							num2 = 0;
							continue;
						}
						break;
						IL_1170:
						num2 = 2;
						continue;
						IL_12A4:
						flag = flag3;
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00090BC0 File Offset: 0x0008FBC0
		protected override void ExportToFile()
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜥ();
				try
				{
					this.ExecuteExport();
				}
				finally
				{
					this.ᜤ();
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3F;
				}
			}
			IL_3F:
			if (false)
			{
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00090C24 File Offset: 0x0008FC24
		protected override void ExportToStream(Stream Stream)
		{
			for (;;)
			{
				this.ᜧ();
				try
				{
					this.ExecuteExport();
				}
				finally
				{
					this.ᜦ();
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
					goto IL_3F;
				}
			}
			IL_3F:
			if (false)
			{
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00090C88 File Offset: 0x0008FC88
		public Cell AddBoolean(ushort Col, ushort Row, bool Value)
		{
			for (;;)
			{
				int num = this.ᜣ.IndexOf(Col, Row);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
				{
					if (false)
					{
					}
					int num2 = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							this.ᜣ.Remove(this.ᜣ[num]);
							num2 = 1;
							continue;
						case 1:
							goto IL_88;
						case 2:
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_8A;
						}
						break;
					}
					break;
				}
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜣ.Add(new Cell());
			cell.CellType = CellType.Boolean;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00090D54 File Offset: 0x0008FD54
		public Cell AddDateTime(ushort Col, ushort Row, DateTime Value)
		{
			for (;;)
			{
				int num = this.ᜣ.IndexOf(Col, Row);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7D;
				default:
				{
					if (false)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							this.ᜣ.Remove(this.ᜣ[num]);
							num2 = 2;
							continue;
						case 1:
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_7F;
						case 2:
							goto IL_7D;
						}
						break;
					}
					break;
				}
				}
			}
			IL_7D:
			IL_7F:
			if (true)
			{
			}
			Cell cell = this.ᜣ.Add(new Cell());
			cell.CellType = CellType.DateTime;
			cell.DateTimeFormat = string.Empty;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00090E28 File Offset: 0x0008FE28
		public Cell AddNumeric(ushort Col, ushort Row, string NumericFormat, double Value)
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
			Cell cell = this.ᜣ.Add(new Cell());
			cell.CellType = CellType.Numeric;
			cell.NumericFormat = NumericFormat;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00090EA0 File Offset: 0x0008FEA0
		public Cell AddNumeric(ushort Col, ushort Row, double Value)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = this.ᜣ.IndexOf(Col, Row);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
				{
					if (false)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							this.ᜣ.Remove(this.ᜣ[num]);
							num2 = 2;
							continue;
						case 1:
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_8A;
						case 2:
							goto IL_88;
						}
						break;
					}
					break;
				}
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜣ.Add(new Cell());
			cell.CellType = CellType.Numeric;
			cell.NumericFormat = string.Empty;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00090F78 File Offset: 0x0008FF78
		public Cell AddString(ushort Col, ushort Row, string Value)
		{
			for (;;)
			{
				int num = this.ᜣ.IndexOf(Col, Row);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
				{
					if (false)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							this.ᜣ.Remove(this.ᜣ[num]);
							num2 = 2;
							continue;
						case 1:
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_8A;
						case 2:
							goto IL_88;
						}
						break;
					}
					break;
				}
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜣ.Add(new Cell());
			cell.CellType = CellType.String;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00091040 File Offset: 0x00090040
		public Cell AddFormula(ushort Col, ushort Row, string Value)
		{
			for (;;)
			{
				int num = this.ᜣ.IndexOf(Col, Row);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
				{
					if (false)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_88;
						case 1:
							if (num >= 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_8A;
						case 2:
							if (true)
							{
							}
							this.ᜣ.Remove(this.ᜣ[num]);
							num2 = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜣ.Add(new Cell());
			cell.CellType = CellType.Formula;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00091108 File Offset: 0x00090108
		public CellHyperlink AddHyperLink(ushort Col, ushort Row, string Title, string Url)
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
			CellHyperlink cellHyperlink = this.\u171E.Add(new CellHyperlink());
			cellHyperlink.Col = (int)Col;
			cellHyperlink.Row = (int)Row;
			cellHyperlink.Title = Title;
			cellHyperlink.Target = Url;
			cellHyperlink.Tip = Url;
			return cellHyperlink;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0009117C File Offset: 0x0009017C
		public MergedCells AddMergedCells(ushort StartRow, ushort EndRow, ushort StartCol, ushort EndCol)
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
			MergedCells mergedCells = this.ᜤ.Add(new MergedCells());
			mergedCells.StartRow = (int)StartRow;
			mergedCells.EndRow = (int)EndRow;
			mergedCells.StartCol = (int)StartCol;
			mergedCells.EndCol = (int)EndCol;
			return mergedCells;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000911E8 File Offset: 0x000901E8
		public void DefineExtendedColor(byte Index, Color Color)
		{
			int a_ = 14;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 2:
					num = 3;
					continue;
				case 3:
					if (Index > 15)
					{
						num = 0;
						continue;
					}
					goto IL_A0;
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
					if (Index < 0)
					{
						goto IL_5D;
					}
					num = 2;
					break;
				}
			}
			IL_5D:
			throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挩䈫堭儯帱崳刵眷䨹夻䰽ℿ㙁ⵃ⥅♇ᕉՋ⁍㑏㝑ⱓᥕⵗ⹙፛㡝≟ൡᅣࡥ౧ᥩ", a_)), Index));
			IL_9E:
			goto IL_5D;
			IL_A0:
			if (true)
			{
			}
			this.ᜦ[(int)Index] = (uint)Color.ToArgb();
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000912AC File Offset: 0x000902AC
		internal FontList FontList
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
				return this.ᜄ;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000912F0 File Offset: 0x000902F0
		internal spr\u2398 BoundSheetList
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
				return this.ᜈ;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00091334 File Offset: 0x00090334
		internal ArrayList ColWidthList
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
				return this.ᜎ;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00091378 File Offset: 0x00090378
		internal Hashtable RowHeightList
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
				return this.ᜐ;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000913BC File Offset: 0x000903BC
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00091400 File Offset: 0x00090400
		[Description("Indicates the stage of the export process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public XlsExportStage ExportStage
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
				return this.\u1719;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							this.\u1719 = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						if (value == this.\u1719)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0009147C File Offset: 0x0009047C
		[Browsable(false)]
		public int TotalCounter
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
				return this.\u171D;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000914C0 File Offset: 0x000904C0
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00091504 File Offset: 0x00090504
		internal sprᢁ FormatList
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜅ)
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
							goto IL_27;
						default:
							if (false)
							{
							}
							this.ᜅ = value;
							num = 3;
							continue;
						}
						break;
					case 3:
						return;
					case 4:
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_24;
					IL_27:
					num = 4;
					continue;
					IL_24:
					if (value != null)
					{
						goto IL_27;
					}
					break;
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0009159C File Offset: 0x0009059C
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x000915E0 File Offset: 0x000905E0
		internal spr\u2363 FormatFieldList
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
				return this.ᜆ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5C;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							if (false)
							{
							}
							this.ᜆ = value;
							num = 0;
							continue;
						}
						break;
					case 2:
						num = 3;
						continue;
					case 3:
						if (value != this.ᜆ)
						{
							num = 1;
							continue;
						}
						return;
					}
					goto IL_24;
					IL_27:
					num = 2;
					continue;
					IL_24:
					if (value != null)
					{
						goto IL_27;
					}
					return;
				}
				IL_5C:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00091678 File Offset: 0x00090678
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x000916BC File Offset: 0x000906BC
		internal spr\u1D65 FormatColRowList
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
				return this.ᜇ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 3;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F;
						default:
							if (false)
							{
							}
							this.ᜇ = value;
							num = 4;
							continue;
						}
						break;
					case 3:
						if (value != this.ᜇ)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						return;
					}
					goto IL_24;
					IL_2F:
					num = 1;
					continue;
					IL_24:
					if (true)
					{
					}
					if (value != null)
					{
						goto IL_2F;
					}
					break;
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00091754 File Offset: 0x00090754
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00091798 File Offset: 0x00090798
		internal int LastTextFormat
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000917DC File Offset: 0x000907DC
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00091820 File Offset: 0x00090820
		internal int LastFormat
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00091864 File Offset: 0x00090864
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x000918A8 File Offset: 0x000908A8
		internal int LastFont
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x000918EC File Offset: 0x000908EC
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00091930 File Offset: 0x00090930
		internal spr\u215F TextFormatList
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_5C;
					case 3:
						if (value != this.ᜃ)
						{
							num = 4;
							continue;
						}
						return;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							if (false)
							{
							}
							this.ᜃ = value;
							num = 1;
							continue;
						}
						break;
					}
					goto IL_24;
					IL_27:
					num = 0;
					continue;
					IL_24:
					if (value != null)
					{
						goto IL_27;
					}
					return;
				}
				IL_5C:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000919C8 File Offset: 0x000909C8
		[Browsable(false)]
		public uint[] ExtendedPalette
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
				return this.ᜦ;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00091A0C File Offset: 0x00090A0C
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00091A50 File Offset: 0x00090A50
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets Excel sheet name in the result Excel file.")]
		[DefaultValue("Sheet1")]
		public string SheetName
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
				return this.ᜰ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
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
						switch (num)
						{
						case 1:
							this.ᜰ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						if (!(this.ᜰ != value))
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00091AD0 File Offset: 0x00090AD0
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00091B14 File Offset: 0x00090B14
		[Description("Gets or sets Excel worksheets in the result Excel file.")]
		[Editor(typeof(SheetsCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WorkSheets Sheets
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
				return this.\u1718;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.\u1718)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						num = 0;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							if (false)
							{
							}
							this.\u1718 = value;
							num = 1;
							continue;
						}
						break;
					}
					goto IL_24;
					IL_27:
					num = 2;
					continue;
					IL_24:
					if (value != null)
					{
						goto IL_27;
					}
					break;
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00091BAC File Offset: 0x00090BAC
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00091BF0 File Offset: 0x00090BF0
		[Editor(typeof(PicturesCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets pictures in the result Excel file.")]
		public CellPictures Pictures
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
				return this.ᜡ;
			}
			set
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
						num = 3;
						continue;
					case 1:
						return;
					case 3:
						if (value != this.ᜡ)
						{
							num = 4;
							continue;
						}
						return;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							if (false)
							{
							}
							this.ᜡ = value;
							num = 1;
							continue;
						}
						break;
					}
					goto IL_24;
					IL_27:
					num = 0;
					continue;
					IL_24:
					if (value != null)
					{
						goto IL_27;
					}
					break;
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00091C88 File Offset: 0x00090C88
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00091CCC File Offset: 0x00090CCC
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
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						if (value != base.ColumnsWidth)
						{
							goto IL_81;
						}
						return;
					case 2:
						return;
					case 3:
						num = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							base.ColumnsWidth = value;
							num = 2;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_81:
					num = 4;
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00091D64 File Offset: 0x00090D64
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00091DA8 File Offset: 0x00090DA8
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
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
				if (false)
				{
				}
				if (true)
				{
				}
				base.NotTruncatableColumns = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00091DEC File Offset: 0x00090DEC
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00091E30 File Offset: 0x00090E30
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
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
				if (false)
				{
				}
				if (true)
				{
				}
				base.AutoFitColWidth = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00091E74 File Offset: 0x00090E74
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00091EB8 File Offset: 0x00090EB8
		[DefaultValue(false)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoFitTitleWidth
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
				return this.\u1732;
			}
			set
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1732 = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					goto IL_24;
					IL_2D:
					num = 0;
					continue;
					IL_24:
					if (value != this.\u1732)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00091F34 File Offset: 0x00090F34
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00091F78 File Offset: 0x00090F78
		[Description(" Indicates whether automatic detection of cell type is the formula type , when the value of cell start with '=' character.")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoFormula
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
				return this.ᜬ;
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
				this.ᜬ = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00091FBC File Offset: 0x00090FBC
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00092000 File Offset: 0x00091000
		[Description("Gets or sets options of the result Excel document.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SheetOptions SheetOptions
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
				return this.\u1714;
			}
			set
			{
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
							goto IL_81;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.\u1714 = value;
							num = 1;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (value != this.\u1714)
						{
							goto IL_81;
						}
						return;
					case 4:
						num = 2;
						continue;
					}
					if (value != null)
					{
						num = 4;
						continue;
					}
					break;
					IL_81:
					num = 0;
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00092098 File Offset: 0x00091098
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x000920DC File Offset: 0x000910DC
		[Description("Gets or sets column formats of each column.")]
		[Editor(typeof(ColumnFormatsCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ColumnFormats ColumnFormats
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
				return this.\u1715;
			}
			set
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
						num = 3;
						continue;
					case 2:
						return;
					case 3:
						if (value != this.\u1715)
						{
							goto IL_81;
						}
						return;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							this.\u1715 = value;
							num = 2;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_81:
					num = 4;
				}
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00092174 File Offset: 0x00091174
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x000921B8 File Offset: 0x000911B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(CellItemType.None)]
		[Description("Indicates whether StripStyles should be applied to columns or rows of the result Excel sheet.")]
		public CellItemType ItemType
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
				return this.\u1717;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						this.\u1717 = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					goto IL_24;
					IL_2D:
					num = 1;
					continue;
					IL_24:
					if (value != this.\u1717)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00092234 File Offset: 0x00091234
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00092278 File Offset: 0x00091278
		[Description("Gets or sets repeating styles for columns or rows in the result Excel document.")]
		[Editor(typeof(CellItemStylesCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ItemStyles ItemStyles
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
				return this.\u1716;
			}
			set
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.\u1716)
						{
							goto IL_81;
						}
						return;
					case 2:
						num = 0;
						continue;
					case 3:
						return;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							this.\u1716 = value;
							num = 3;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_81:
					num = 4;
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00092310 File Offset: 0x00091310
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00092354 File Offset: 0x00091354
		[Description("Gets or sets hyperlinks in the result Excel document.")]
		[Editor(typeof(HyperlinksCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellHyperlinks Hyperlinks
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
				return this.\u171E;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 4;
						continue;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.\u171E = value;
							num = 2;
							continue;
						}
						break;
					case 4:
						if (value != this.\u171E)
						{
							goto IL_81;
						}
						return;
					}
					if (value != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_81:
					num = 3;
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000923EC File Offset: 0x000913EC
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00092430 File Offset: 0x00091430
		[Description("Gets or sets note in the result Excel document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
		public CellNotes Notes
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
				return this.\u171F;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (value != this.\u171F)
						{
							goto IL_81;
						}
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.\u171F = value;
							num = 4;
							continue;
						}
						break;
					case 4:
						return;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_81:
					num = 3;
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000924C8 File Offset: 0x000914C8
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x0009250C File Offset: 0x0009150C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ChartsCollectionEditor), typeof(UITypeEditor))]
		[Description("Gets or sets chart in the result Excel document.")]
		public Charts Charts
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
				return this.ᜠ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.ᜠ = value;
							num = 2;
							continue;
						}
						break;
					case 4:
						if (value != this.ᜠ)
						{
							goto IL_81;
						}
						return;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_81:
					num = 3;
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000925A4 File Offset: 0x000915A4
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x000925E8 File Offset: 0x000915E8
		[Description("Gets or sets images in the result Excel document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ImagesCollectionEditor), typeof(UITypeEditor))]
		public CellImages Images
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
				return this.ᜢ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						return;
					case 2:
						if (value != this.ᜢ)
						{
							goto IL_81;
						}
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜢ = value;
							num = 1;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_81:
					num = 3;
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00092680 File Offset: 0x00091680
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x000926C4 File Offset: 0x000916C4
		[Description("Gets or sets cell value in the result Excel document.")]
		[Editor(typeof(CellsCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Cells Cells
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
				return this.ᜣ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜣ)
						{
							goto IL_79;
						}
						goto IL_83;
					case 1:
						goto IL_66;
					case 2:
						num = 0;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
							if (false)
							{
							}
							this.ᜣ = value;
							num = 1;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_79:
					num = 4;
				}
				IL_66:
				IL_83:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0009275C File Offset: 0x0009175C
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x000927A0 File Offset: 0x000917A0
		[Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets merged cell in the result Excel document.")]
		public MergedCellList MergedCells
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
				return this.ᜤ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜤ)
						{
							goto IL_81;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.ᜤ = value;
							num = 3;
							continue;
						}
						break;
					case 3:
						return;
					}
					if (value != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_81:
					num = 2;
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00092838 File Offset: 0x00091838
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x0009287C File Offset: 0x0009187C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets or sets background image in the result Excel document.")]
		public CellGraphic Background
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
				return this.ᜥ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.ᜥ = value;
							num = 3;
							continue;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						if (value != this.ᜥ)
						{
							goto IL_81;
						}
						return;
					case 3:
						return;
					}
					if (value != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_81:
					num = 0;
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00092914 File Offset: 0x00091914
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00092958 File Offset: 0x00091958
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets header rows in the result Excel document.")]
		public int HeaderRows
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
				return this.\u171A;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						if (true)
						{
						}
						this.\u171A = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_25;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_25:
					num = 2;
					continue;
					IL_1C:
					if (value != this.\u171A)
					{
						goto IL_25;
					}
					break;
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000929D4 File Offset: 0x000919D4
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00092A18 File Offset: 0x00091A18
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets start column in the result Excel dcoument.")]
		[DefaultValue(0)]
		public byte StartDataCol
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
				return this.\u171B;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						this.\u171B = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					goto IL_24;
					IL_2D:
					num = 1;
					continue;
					IL_24:
					if (value != this.\u171B)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00092A94 File Offset: 0x00091A94
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00092AD8 File Offset: 0x00091AD8
		[Description("Gets or sets footer rows in the result Excel documents.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int FooterRows
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
				return this.\u171C;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.\u171C = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_25;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_25:
					num = 1;
					continue;
					IL_1C:
					if (value != this.\u171C)
					{
						goto IL_25;
					}
					break;
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00092B54 File Offset: 0x00091B54
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00092B98 File Offset: 0x00091B98
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Editor(typeof(CellFileNameEditor), typeof(UITypeEditor))]
		[Browsable(true)]
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

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00092BDC File Offset: 0x00091BDC
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00092C20 File Offset: 0x00091C20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Browsable(true)]
		public bool DataExported
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
				return this.ᜯ;
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
				this.ᜯ = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00092C64 File Offset: 0x00091C64
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00092CA8 File Offset: 0x00091CA8
		[DefaultValue(ExportSource.SqlCommand)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new ExportSource DataSource
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
				return base.DataSource;
			}
			set
			{
				for (;;)
				{
					base.DataSource = value;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_45;
						case 1:
							if (this.Site.DesignMode)
							{
								num = 2;
								continue;
							}
							goto IL_AB;
						case 2:
							this.ᜀ(value, this.SQLCommand, this.DataTable, this.ListView);
							num = 4;
							continue;
						case 3:
							if (this.Site != null)
							{
								num = 0;
								continue;
							}
							goto IL_AB;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_45;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_45:
						num = 1;
					}
				}
				IL_7E:
				if (false)
				{
				}
				IL_AB:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00092D68 File Offset: 0x00091D68
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00092DAC File Offset: 0x00091DAC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		public new IDbCommand SQLCommand
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
				return base.SQLCommand;
			}
			set
			{
				for (;;)
				{
					base.SQLCommand = value;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.Site != null)
							{
								num = 3;
								continue;
							}
							return;
						case 1:
							if (this.Site.DesignMode)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							this.ᜀ(this.DataSource, value, this.DataTable, this.ListView);
							num = 4;
							continue;
						case 3:
							goto IL_45;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_45;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_45:
						num = 1;
					}
				}
				IL_7E:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00092E6C File Offset: 0x00091E6C
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00092EB0 File Offset: 0x00091EB0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		public new DataTable DataTable
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
				return base.DataTable;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4F:
					this.ᜀ(this.DataSource, this.SQLCommand, value, this.ListView);
					num = 2;
					break;
				default:
					if (false)
					{
					}
					goto IL_30;
				}
				for (;;)
				{
					IL_1E:
					switch (num)
					{
					case 0:
						goto IL_4F;
					case 1:
						if (base.DesignMode)
						{
							num = 0;
							continue;
						}
						goto IL_7E;
					case 2:
						goto IL_7C;
					}
					goto IL_30;
				}
				IL_7C:
				IL_7E:
				if (true)
				{
				}
				return;
				IL_30:
				base.DataTable = value;
				num = 1;
				goto IL_1E;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00092F44 File Offset: 0x00091F44
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00092F88 File Offset: 0x00091F88
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new ListView ListView
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
				return base.ListView;
			}
			set
			{
				for (;;)
				{
					base.ListView = value;
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
								goto IL_45;
							default:
								goto IL_7E;
							}
							break;
						case 1:
							this.ᜀ(this.DataSource, this.SQLCommand, this.DataTable, value);
							num = 0;
							continue;
						case 2:
							if (this.Site != null)
							{
								num = 4;
								continue;
							}
							return;
						case 3:
							if (this.Site.DesignMode)
							{
								num = 1;
								continue;
							}
							return;
						case 4:
							goto IL_45;
						}
						break;
						IL_45:
						num = 3;
					}
				}
				IL_7E:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00093048 File Offset: 0x00092048
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0009308C File Offset: 0x0009208C
		[Description("Indicate whether export long char/binary column.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[DefaultValue(false)]
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

		// Token: 0x06000D3C RID: 3388 RVA: 0x000930D0 File Offset: 0x000920D0
		internal void ᜀ(object A_0, XLSDataRowEventArgs A_1)
		{
			int a_ = 1;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.m_advancedDataRow(A_0, A_1);
					num = 2;
					continue;
				case 2:
					return;
				case 3:
					goto IL_54;
				case 4:
					if (this.m_advancedDataRow != null)
					{
						num = 1;
						continue;
					}
					return;
				}
				if (A_1 == null)
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
						num = 3;
						break;
					}
				}
				else
				{
					num = 4;
				}
			}
			IL_54:
			if (true)
			{
			}
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("လᔞ戠䘢䤤䬦氨匪崬䀮䌰䜲༴ശ欸娺吼䰾⑀ɂ⅄ㅆ⡈╊⹌⩎㕐ᙒⵔ❖㙘⥚⥜㩞ՠㅢdѦ٨ᥪ६䍮ݰቲݴ䵶ᱸ", a_));
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00093190 File Offset: 0x00092190
		internal void ᜀ(object A_0, XLSTextEventArgs A_1)
		{
			int a_ = 9;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					this.m_advancedExportText(A_0, A_1);
					num = 0;
					continue;
				case 2:
					goto IL_54;
				case 3:
					if (this.m_advancedExportText != null)
					{
						num = 1;
						continue;
					}
					return;
				}
				if (A_1 == null)
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
						num = 2;
						break;
					}
				}
				else
				{
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_54:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⠤⴦樨个䄬䌮琰䬲䔴堶䬸伺ܼԾፀ≂ⱄ㑆ⱈ੊⥌㥎ぐ㵒㙔㉖㵘ᱚ㡜⭞①᭢ᕤࡦ᭨Ὢ㥬੮॰ݲ奴Ŷᡸॺ䝼᩾", a_));
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00093250 File Offset: 0x00092250
		internal void ᜀ(object A_0, XLSExportRowEventArgs A_1)
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.m_advancedExportRow != null)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					return;
				case 3:
					goto IL_5C;
				case 4:
					this.m_advancedExportRow(A_0, A_1);
					num = 1;
					continue;
				}
				if (A_1 == null)
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
						num = 3;
						break;
					}
				}
				else
				{
					num = 0;
				}
			}
			IL_5C:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("ጝ⨟愡䄣䨥䐧漩含席弯䀱䀳వȷ根崻圽㌿❁Ճ≅㹇⭉≋ⵍ㕏㙑ᙓ㍕㹗㕙⹛㭝╟ᩡᑣ॥ᩧṩ㹫ŭݯ幱ɳ᝵੷䁹᥻", a_));
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00093310 File Offset: 0x00092310
		internal void ᜀ(object A_0, HeaderFooterParamsEventArgs A_1)
		{
			int a_ = 17;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					goto IL_5C;
				case 3:
					this.ᜧ(A_0, A_1);
					num = 1;
					continue;
				case 4:
					if (this.ᜧ != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				if (A_1 == null)
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
						num = 2;
						break;
					}
				}
				else
				{
					num = 4;
				}
			}
			IL_5C:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("‬┮爰嘲头嬶簸䌺䴼倾㍀㝂罄絆ᭈ⩊⑌㱎㑐ᑒご⍖ᅘ㹚㱜㭞Ѡᅢ㕤٦᭨੪lᱮ嵰ղᑴն䍸Ṻ", a_));
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000933D0 File Offset: 0x000923D0
		internal void ᜀ(object A_0, TitleParamsEventArgs A_1)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 2:
					if (this.m_getParams != null)
					{
						num = 3;
						continue;
					}
					goto IL_A5;
				case 3:
					this.m_getParams(A_0, A_1);
					num = 0;
					continue;
				case 4:
					goto IL_54;
				}
				if (A_1 == null)
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
						num = 4;
						break;
					}
				}
				else
				{
					num = 2;
				}
			}
			IL_54:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⸢⼤搦䰨䜪䄬樮䤰䌲娴䔶䴸ĺܼ派⁀⩂㙄≆่⹊㥌౎ぐ⍒⅔㹖㙘㕚൜㹞፠ɢࡤᑦ䕨ᵪ౬ᵮ䭰ᙲ", a_));
			IL_6B:
			IL_A5:
			if (true)
			{
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0009348C File Offset: 0x0009248C
		internal void ᜂ(object A_0, HeaderFooterParamsEventArgs A_1)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 2:
					goto IL_5C;
				case 3:
					if (this.ᜨ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 4:
					this.ᜨ(A_0, A_1);
					num = 0;
					continue;
				}
				if (A_1 == null)
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
						num = 2;
						break;
					}
				}
				else
				{
					num = 3;
				}
			}
			IL_5C:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("Å␭猯圱堳娵紷䈹䰻儽㈿㙁繃籅ᩇ⭉╋㵍㕏ᕑㅓ≕ᩗ㽙㩛ㅝ቟ݡ⁣ݥᱧ୩㱫཭ɯ፱ᥳյ呷౹ᵻ౽멿", a_));
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0009354C File Offset: 0x0009254C
		internal void ᜀ(object A_0, DataParamsEventArgs A_1)
		{
			int a_ = 0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					return;
				case 3:
					if (this.ᜩ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 4:
					this.ᜩ(A_0, A_1);
					num = 1;
					continue;
				}
				if (A_1 == null)
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
						break;
					}
				}
				else
				{
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_54:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("ᄛᐝ挟䜡䠣䨥洧利尫䄭䈯䘱ำవ樷嬹唻䴽┿Ձ⅃㉅ే⭉㡋⽍O㍑♓㝕㕗⥙灛⡝şၡ幣ͥ", a_));
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0009360C File Offset: 0x0009260C
		internal void ᜀ(object A_0, AggregateParamsEventArgs A_1)
		{
			int a_ = 16;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜪ != null)
					{
						num = 3;
						continue;
					}
					return;
				case 1:
					return;
				case 3:
					this.ᜪ(A_0, A_1);
					num = 1;
					continue;
				case 4:
					goto IL_54;
				}
				if (A_1 == null)
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
						num = 4;
						break;
					}
				}
				else
				{
					if (true)
					{
					}
					num = 0;
				}
			}
			IL_54:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("Å␭猯圱堳娵紷䈹䰻儽㈿㙁繃籅ᩇ⭉╋㵍㕏ᕑㅓ≕ᥗ㵙㭛ⱝ՟ագብ൧㩩൫ᱭᅯάݳ婵๷᭹๻䑽", a_));
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000936CC File Offset: 0x000926CC
		internal new void ᜁ(object A_0, HeaderFooterParamsEventArgs A_1)
		{
			int a_ = 17;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜫ(A_0, A_1);
					if (true)
					{
					}
					num = 4;
					continue;
				case 2:
					goto IL_54;
				case 3:
					if (this.ᜫ != null)
					{
						num = 1;
						continue;
					}
					return;
				case 4:
					return;
				}
				if (A_1 == null)
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
						num = 2;
						break;
					}
				}
				else
				{
					num = 3;
				}
			}
			IL_54:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("‬┮爰嘲头嬶簸䌺䴼倾㍀㝂罄絆ᭈ⩊⑌㱎㑐ᑒご⍖὘㑚㉜⭞Ѡᅢ㕤٦᭨੪lᱮ嵰ղᑴն䍸Ṻ", a_));
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0009378C File Offset: 0x0009278C
		internal new void ᜁ(object A_0, int A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_4D;
				case 2:
					this.m_beforeSheet(A_0, A_1);
					num = 1;
					continue;
				}
				IL_1C:
				if (this.m_beforeSheet != null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C;
				default:
					goto IL_63;
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0009380C File Offset: 0x0009280C
		internal void ᜀ(object A_0, int A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					goto IL_4D;
				case 2:
					this.m_afterSheet(A_0, A_1);
					num = 1;
					continue;
				}
				IL_24:
				if (this.m_afterSheet != null)
				{
					num = 2;
					continue;
				}
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24;
				default:
					goto IL_63;
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000D47 RID: 3399 RVA: 0x0009388C File Offset: 0x0009288C
		// (remove) Token: 0x06000D48 RID: 3400 RVA: 0x000938F0 File Offset: 0x000928F0
		[Description("Occur after the export of each source record.")]
		public event XLSDataRowEventHandler AdvancedExportedRecord
		{
			add
			{
				if (true)
				{
				}
				if (this.m_advancedDataRow == null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_28;
						}
					}
					IL_28:
					if (false)
					{
					}
					this.m_advancedDataRow = value;
					return;
				}
				this.m_advancedDataRow = (XLSDataRowEventHandler)Delegate.Combine(this.m_advancedDataRow, value);
			}
			remove
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						this.m_advancedDataRow = (XLSDataRowEventHandler)Delegate.Remove(this.m_advancedDataRow, value);
						num = 2;
						continue;
					case 2:
						goto IL_57;
					}
					IL_1C:
					if (this.m_advancedDataRow != null)
					{
						num = 1;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000D49 RID: 3401 RVA: 0x0009397C File Offset: 0x0009297C
		// (remove) Token: 0x06000D4A RID: 3402 RVA: 0x000939E0 File Offset: 0x000929E0
		[Description("Occur when get export of source string.")]
		public event XLSTextEventHandler AdvancedGetExportText
		{
			add
			{
				if (this.m_advancedExportText == null)
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
					if (true)
					{
					}
					if (false)
					{
					}
					this.m_advancedExportText = value;
					return;
				}
				this.m_advancedExportText = (XLSTextEventHandler)Delegate.Combine(this.m_advancedExportText, value);
			}
			remove
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						goto IL_57;
					case 2:
						this.m_advancedExportText = (XLSTextEventHandler)Delegate.Remove(this.m_advancedExportText, value);
						num = 1;
						continue;
					}
					IL_24:
					if (this.m_advancedExportText != null)
					{
						num = 2;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000D4B RID: 3403 RVA: 0x00093A6C File Offset: 0x00092A6C
		// (remove) Token: 0x06000D4C RID: 3404 RVA: 0x00093AD0 File Offset: 0x00092AD0
		[Description("Occur before the export of each source record.")]
		public event XLSExportRowEventHandler AdvancedBeforeExportRow
		{
			add
			{
				if (this.m_advancedExportRow == null)
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
							break;
						default:
							goto IL_28;
						}
					}
					IL_28:
					if (false)
					{
					}
					this.m_advancedExportRow = value;
					return;
				}
				this.m_advancedExportRow = (XLSExportRowEventHandler)Delegate.Combine(this.m_advancedExportRow, value);
			}
			remove
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4F;
					case 2:
						this.m_advancedExportRow = (XLSExportRowEventHandler)Delegate.Remove(this.m_advancedExportRow, value);
						num = 0;
						continue;
					}
					IL_1C:
					if (this.m_advancedExportRow != null)
					{
						num = 2;
						continue;
					}
					IL_4F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_65;
					}
				}
				IL_65:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000D4D RID: 3405 RVA: 0x00093B5C File Offset: 0x00092B5C
		// (remove) Token: 0x06000D4E RID: 3406 RVA: 0x00093BC0 File Offset: 0x00092BC0
		[Description("Occur when the header cell value.")]
		public event HeaderFooterParamsEventHandler GetHeaderParams
		{
			add
			{
				if (this.ᜧ == null)
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
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜧ = value;
					return;
				}
				this.ᜧ = (HeaderFooterParamsEventHandler)Delegate.Combine(this.ᜧ, value);
			}
			remove
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜧ = (HeaderFooterParamsEventHandler)Delegate.Remove(this.ᜧ, value);
						num = 1;
						continue;
					case 1:
						if (true)
						{
						}
						goto IL_57;
					}
					IL_1C:
					if (this.ᜧ != null)
					{
						num = 0;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000D4F RID: 3407 RVA: 0x00093C4C File Offset: 0x00092C4C
		// (remove) Token: 0x06000D50 RID: 3408 RVA: 0x00093CB0 File Offset: 0x00092CB0
		[Description("Occur when gets column titles value.")]
		public event TitleParamsEventHandler GetTitleParams
		{
			add
			{
				if (this.m_getParams == null)
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
					if (true)
					{
					}
					if (false)
					{
					}
					this.m_getParams = value;
					return;
				}
				this.m_getParams = (TitleParamsEventHandler)Delegate.Combine(this.m_getParams, value);
			}
			remove
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_getParams = (TitleParamsEventHandler)Delegate.Remove(this.m_getParams, value);
						num = 1;
						continue;
					case 1:
						goto IL_4F;
					}
					IL_1C:
					if (this.m_getParams != null)
					{
						num = 0;
						continue;
					}
					IL_4F:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000D51 RID: 3409 RVA: 0x00093D3C File Offset: 0x00092D3C
		// (remove) Token: 0x06000D52 RID: 3410 RVA: 0x00093DA0 File Offset: 0x00092DA0
		[Description("Occur when get footer cell value.")]
		public event HeaderFooterParamsEventHandler GetBeforeDataParams
		{
			add
			{
				if (this.ᜨ == null)
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
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜨ = value;
					return;
				}
				this.ᜨ = (HeaderFooterParamsEventHandler)Delegate.Combine(this.ᜨ, value);
			}
			remove
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_57;
					case 2:
						if (true)
						{
						}
						this.ᜨ = (HeaderFooterParamsEventHandler)Delegate.Remove(this.ᜨ, value);
						num = 1;
						continue;
					}
					IL_1C:
					if (this.ᜨ != null)
					{
						num = 2;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000D53 RID: 3411 RVA: 0x00093E2C File Offset: 0x00092E2C
		// (remove) Token: 0x06000D54 RID: 3412 RVA: 0x00093E90 File Offset: 0x00092E90
		[Description("Occur when gets cells value.")]
		public event DataParamsEventHandler GetDataParams
		{
			add
			{
				if (this.ᜩ == null)
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
					this.ᜩ = value;
					return;
				}
				this.ᜩ = (DataParamsEventHandler)Delegate.Combine(this.ᜩ, value);
			}
			remove
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜩ = (DataParamsEventHandler)Delegate.Remove(this.ᜩ, value);
						num = 1;
						continue;
					case 1:
						goto IL_57;
					}
					IL_1C:
					if (this.ᜩ != null)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000D55 RID: 3413 RVA: 0x00093F1C File Offset: 0x00092F1C
		// (remove) Token: 0x06000D56 RID: 3414 RVA: 0x00093F80 File Offset: 0x00092F80
		[Description("Occur when gets aggregate value.")]
		public event AggregateParamsEventHandler GetAggregateParams
		{
			add
			{
				if (this.ᜪ == null)
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
							break;
						default:
							goto IL_28;
						}
					}
					IL_28:
					if (false)
					{
					}
					this.ᜪ = value;
					return;
				}
				this.ᜪ = (AggregateParamsEventHandler)Delegate.Combine(this.ᜪ, value);
			}
			remove
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						this.ᜪ = (AggregateParamsEventHandler)Delegate.Remove(this.ᜪ, value);
						num = 2;
						continue;
					case 2:
						goto IL_57;
					}
					IL_1C:
					if (this.ᜪ != null)
					{
						num = 1;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000D57 RID: 3415 RVA: 0x0009400C File Offset: 0x0009300C
		// (remove) Token: 0x06000D58 RID: 3416 RVA: 0x00094070 File Offset: 0x00093070
		[Description("Occurs when gets footer cell value.")]
		public event HeaderFooterParamsEventHandler GetFooterParams
		{
			add
			{
				if (this.ᜫ == null)
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
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜫ = value;
					return;
				}
				this.ᜫ = (HeaderFooterParamsEventHandler)Delegate.Combine(this.ᜫ, value);
			}
			remove
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜫ = (HeaderFooterParamsEventHandler)Delegate.Remove(this.ᜫ, value);
						num = 2;
						continue;
					case 2:
						goto IL_57;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.ᜫ != null)
					{
						num = 0;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000D59 RID: 3417 RVA: 0x000940FC File Offset: 0x000930FC
		// (remove) Token: 0x06000D5A RID: 3418 RVA: 0x00094160 File Offset: 0x00093160
		[Description("Occurs before exporting each worksheet.")]
		public event XLSSheetDataEventHandler BeforeExportSheet
		{
			add
			{
				if (this.m_beforeSheet == null)
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
							break;
						default:
							goto IL_28;
						}
					}
					IL_28:
					if (false)
					{
					}
					this.m_beforeSheet = value;
					return;
				}
				this.m_beforeSheet = (XLSSheetDataEventHandler)Delegate.Combine(this.m_beforeSheet, value);
			}
			remove
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_57;
					case 1:
						if (true)
						{
						}
						this.m_beforeSheet = (XLSSheetDataEventHandler)Delegate.Remove(this.m_beforeSheet, value);
						num = 0;
						continue;
					}
					IL_1C:
					if (this.m_beforeSheet != null)
					{
						num = 1;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000D5B RID: 3419 RVA: 0x000941EC File Offset: 0x000931EC
		// (remove) Token: 0x06000D5C RID: 3420 RVA: 0x00094250 File Offset: 0x00093250
		[Description("Occur after exporting each worksheet.")]
		public event XLSSheetDataEventHandler AfterExportSheet
		{
			add
			{
				if (this.m_afterSheet == null)
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
							break;
						default:
							goto IL_28;
						}
					}
					IL_28:
					if (false)
					{
					}
					this.m_afterSheet = value;
					return;
				}
				this.m_afterSheet = (XLSSheetDataEventHandler)Delegate.Combine(this.m_afterSheet, value);
			}
			remove
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_4F;
					case 2:
						this.m_afterSheet = (XLSSheetDataEventHandler)Delegate.Remove(this.m_afterSheet, value);
						num = 1;
						continue;
					}
					IL_1C:
					if (this.m_afterSheet != null)
					{
						num = 2;
						continue;
					}
					IL_4F:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x0400097B RID: 2427
		private new int ᜀ;

		// Token: 0x0400097C RID: 2428
		private new bool ᜁ;

		// Token: 0x0400097D RID: 2429
		private new License ᜂ;

		// Token: 0x0400097E RID: 2430
		private new spr\u215F ᜃ;

		// Token: 0x0400097F RID: 2431
		private new FontList ᜄ;

		// Token: 0x04000980 RID: 2432
		private sprᢁ ᜅ;

		// Token: 0x04000981 RID: 2433
		private spr\u2363 ᜆ;

		// Token: 0x04000982 RID: 2434
		private spr\u1D65 ᜇ;

		// Token: 0x04000983 RID: 2435
		private spr\u2398 ᜈ;

		// Token: 0x04000984 RID: 2436
		private sprᦛ ᜉ;

		// Token: 0x04000985 RID: 2437
		private int ᜊ;

		// Token: 0x04000986 RID: 2438
		private int ᜋ;

		// Token: 0x04000987 RID: 2439
		private int ᜌ;

		// Token: 0x04000988 RID: 2440
		private long \u170D;

		// Token: 0x04000989 RID: 2441
		private ArrayList ᜎ;

		// Token: 0x0400098A RID: 2442
		private int ᜏ;

		// Token: 0x0400098B RID: 2443
		private Hashtable ᜐ;

		// Token: 0x0400098C RID: 2444
		internal Hashtable ᜑ;

		// Token: 0x0400098D RID: 2445
		private sprḗ \u1712;

		// Token: 0x0400098E RID: 2446
		private byte[] \u1713;

		// Token: 0x0400098F RID: 2447
		private SheetOptions \u1714;

		// Token: 0x04000990 RID: 2448
		private ColumnFormats \u1715;

		// Token: 0x04000991 RID: 2449
		private ItemStyles \u1716;

		// Token: 0x04000992 RID: 2450
		private CellItemType \u1717;

		// Token: 0x04000993 RID: 2451
		private WorkSheets \u1718;

		// Token: 0x04000994 RID: 2452
		private XlsExportStage \u1719;

		// Token: 0x04000995 RID: 2453
		private int \u171A;

		// Token: 0x04000996 RID: 2454
		private byte \u171B;

		// Token: 0x04000997 RID: 2455
		private new int \u171C;

		// Token: 0x04000998 RID: 2456
		private new int \u171D;

		// Token: 0x04000999 RID: 2457
		private CellHyperlinks \u171E;

		// Token: 0x0400099A RID: 2458
		private CellNotes \u171F;

		// Token: 0x0400099B RID: 2459
		private Charts ᜠ;

		// Token: 0x0400099C RID: 2460
		private CellPictures ᜡ;

		// Token: 0x0400099D RID: 2461
		private CellImages ᜢ;

		// Token: 0x0400099E RID: 2462
		private Cells ᜣ;

		// Token: 0x0400099F RID: 2463
		private MergedCellList ᜤ;

		// Token: 0x040009A0 RID: 2464
		private CellGraphic ᜥ;

		// Token: 0x040009A1 RID: 2465
		private uint[] ᜦ;

		// Token: 0x040009A2 RID: 2466
		protected XLSDataRowEventHandler m_advancedDataRow;

		// Token: 0x040009A3 RID: 2467
		protected XLSTextEventHandler m_advancedExportText;

		// Token: 0x040009A4 RID: 2468
		protected XLSExportRowEventHandler m_advancedExportRow;

		// Token: 0x040009A5 RID: 2469
		internal HeaderFooterParamsEventHandler ᜧ;

		// Token: 0x040009A6 RID: 2470
		protected TitleParamsEventHandler m_getParams;

		// Token: 0x040009A7 RID: 2471
		internal HeaderFooterParamsEventHandler ᜨ;

		// Token: 0x040009A8 RID: 2472
		internal DataParamsEventHandler ᜩ;

		// Token: 0x040009A9 RID: 2473
		internal AggregateParamsEventHandler ᜪ;

		// Token: 0x040009AA RID: 2474
		internal HeaderFooterParamsEventHandler ᜫ;

		// Token: 0x040009AB RID: 2475
		protected XLSSheetDataEventHandler m_beforeSheet;

		// Token: 0x040009AC RID: 2476
		protected XLSSheetDataEventHandler m_afterSheet;

		// Token: 0x040009AD RID: 2477
		private bool ᜬ;

		// Token: 0x040009AE RID: 2478
		internal bool ᜭ;

		// Token: 0x040009AF RID: 2479
		internal ArrayList ᜮ;

		// Token: 0x040009B0 RID: 2480
		private bool ᜯ;

		// Token: 0x040009B1 RID: 2481
		private string ᜰ;

		// Token: 0x040009B2 RID: 2482
		private int ᜱ;

		// Token: 0x040009B3 RID: 2483
		private bool \u1732;

		// Token: 0x020001BE RID: 446
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private new struct ᜁ
		{
			// Token: 0x040009B4 RID: 2484
			public int ᜀ;

			// Token: 0x040009B5 RID: 2485
			public int ᜁ;

			// Token: 0x040009B6 RID: 2486
			public int ᜂ;

			// Token: 0x040009B7 RID: 2487
			public int ᜃ;

			// Token: 0x040009B8 RID: 2488
			public int ᜄ;

			// Token: 0x040009B9 RID: 2489
			public int ᜅ;

			// Token: 0x040009BA RID: 2490
			public int ᜆ;

			// Token: 0x040009BB RID: 2491
			public int ᜇ;

			// Token: 0x040009BC RID: 2492
			public int ᜈ;

			// Token: 0x040009BD RID: 2493
			public int ᜉ;

			// Token: 0x040009BE RID: 2494
			public int ᜊ;

			// Token: 0x040009BF RID: 2495
			public byte ᜋ;

			// Token: 0x040009C0 RID: 2496
			public byte ᜌ;

			// Token: 0x040009C1 RID: 2497
			public byte \u170D;

			// Token: 0x040009C2 RID: 2498
			public byte ᜎ;

			// Token: 0x040009C3 RID: 2499
			public byte ᜏ;

			// Token: 0x040009C4 RID: 2500
			public byte ᜐ;

			// Token: 0x040009C5 RID: 2501
			public byte ᜑ;

			// Token: 0x040009C6 RID: 2502
			public byte \u1712;

			// Token: 0x040009C7 RID: 2503
			public byte \u1713;
		}

		// Token: 0x020001BF RID: 447
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private new struct ᜂ
		{
			// Token: 0x040009C8 RID: 2504
			public int ᜀ;

			// Token: 0x040009C9 RID: 2505
			public byte ᜁ;

			// Token: 0x040009CA RID: 2506
			public byte ᜂ;

			// Token: 0x040009CB RID: 2507
			public byte ᜃ;

			// Token: 0x040009CC RID: 2508
			public byte ᜄ;

			// Token: 0x040009CD RID: 2509
			public byte ᜅ;

			// Token: 0x040009CE RID: 2510
			public byte ᜆ;

			// Token: 0x040009CF RID: 2511
			public byte ᜇ;

			// Token: 0x040009D0 RID: 2512
			public byte ᜈ;

			// Token: 0x040009D1 RID: 2513
			public byte ᜉ;

			// Token: 0x040009D2 RID: 2514
			public byte ᜊ;
		}

		// Token: 0x020001C0 RID: 448
		private new struct ᜃ
		{
			// Token: 0x040009D3 RID: 2515
			public int ᜀ;

			// Token: 0x040009D4 RID: 2516
			public int ᜁ;

			// Token: 0x040009D5 RID: 2517
			public int ᜂ;

			// Token: 0x040009D6 RID: 2518
			public int ᜃ;
		}

		// Token: 0x020001C1 RID: 449
		private new struct ᜄ
		{
			// Token: 0x040009D7 RID: 2519
			public int ᜀ;

			// Token: 0x040009D8 RID: 2520
			public int ᜁ;
		}

		// Token: 0x020001C2 RID: 450
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private new struct ᜀ
		{
			// Token: 0x06000D5D RID: 3421 RVA: 0x000942DC File Offset: 0x000932DC
			public void ᜀ()
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
				this.ᜀ = 0;
				this.ᜁ.ᜀ = 0;
				this.ᜁ.ᜁ = 0;
				this.ᜁ.ᜂ = 0;
				this.ᜁ.ᜃ = 0;
				this.ᜁ.ᜄ = 0;
				this.ᜁ.ᜅ = 0;
				this.ᜁ.ᜆ = 0;
				this.ᜁ.ᜇ = 0;
				this.ᜁ.ᜈ = 0;
				this.ᜁ.ᜉ = 0;
				this.ᜁ.ᜊ = 0;
				this.ᜁ.ᜋ = 0;
				this.ᜁ.ᜌ = 0;
				this.ᜁ.\u170D = 0;
				this.ᜁ.ᜎ = 0;
				this.ᜁ.ᜏ = 0;
				this.ᜁ.ᜐ = 0;
				this.ᜁ.ᜑ = 0;
				this.ᜁ.\u1712 = 0;
				this.ᜁ.\u1713 = 0;
				this.ᜂ = 0;
				this.ᜃ.ᜀ = 0;
				this.ᜃ.ᜁ = 0;
				this.ᜃ.ᜂ = 0;
				this.ᜃ.ᜃ = 0;
				this.ᜃ.ᜄ = 0;
				this.ᜃ.ᜅ = 0;
				this.ᜃ.ᜆ = 0;
				this.ᜃ.ᜇ = 0;
				this.ᜃ.ᜈ = 0;
				this.ᜃ.ᜉ = 0;
				this.ᜃ.ᜊ = 0;
				this.ᜄ = 0;
				this.ᜅ = 0;
				this.ᜆ = 0;
				this.ᜇ = 0;
				this.ᜈ = 0;
				this.ᜉ = 0;
				this.ᜊ = 0;
				this.ᜋ = 0;
				this.ᜌ = 0;
				this.\u170D = 0;
				this.ᜎ = 0;
				this.ᜏ.ᜀ = 0;
				this.ᜏ.ᜁ = 0;
				this.ᜏ.ᜂ = 0;
				this.ᜏ.ᜃ = 0;
				this.ᜐ = 0;
				this.ᜑ = 0;
				this.\u1712 = 0;
				this.\u1713 = 0;
				this.\u1714.ᜀ = 0;
				this.\u1714.ᜁ = 0;
				this.\u1715.ᜀ = 0;
				this.\u1715.ᜁ = 0;
				this.\u1716.ᜀ = 0;
				this.\u1716.ᜁ = 0;
				this.\u1717.ᜀ = 0;
				this.\u1717.ᜁ = 0;
				this.\u1718 = 0;
				this.\u1719 = 0;
				this.\u171A = 0;
				this.\u171B = 0;
				this.\u171C = (IntPtr)0;
				this.\u171D = (IntPtr)0;
				this.\u171E = (IntPtr)0;
				this.\u171F = (IntPtr)0;
			}

			// Token: 0x040009D9 RID: 2521
			public int ᜀ;

			// Token: 0x040009DA RID: 2522
			public CellExport.ᜁ ᜁ;

			// Token: 0x040009DB RID: 2523
			public byte ᜂ;

			// Token: 0x040009DC RID: 2524
			public CellExport.ᜂ ᜃ;

			// Token: 0x040009DD RID: 2525
			public int ᜄ;

			// Token: 0x040009DE RID: 2526
			public int ᜅ;

			// Token: 0x040009DF RID: 2527
			public int ᜆ;

			// Token: 0x040009E0 RID: 2528
			public int ᜇ;

			// Token: 0x040009E1 RID: 2529
			public int ᜈ;

			// Token: 0x040009E2 RID: 2530
			public int ᜉ;

			// Token: 0x040009E3 RID: 2531
			public int ᜊ;

			// Token: 0x040009E4 RID: 2532
			public int ᜋ;

			// Token: 0x040009E5 RID: 2533
			public int ᜌ;

			// Token: 0x040009E6 RID: 2534
			public int \u170D;

			// Token: 0x040009E7 RID: 2535
			public int ᜎ;

			// Token: 0x040009E8 RID: 2536
			public CellExport.ᜃ ᜏ;

			// Token: 0x040009E9 RID: 2537
			public int ᜐ;

			// Token: 0x040009EA RID: 2538
			public int ᜑ;

			// Token: 0x040009EB RID: 2539
			public int \u1712;

			// Token: 0x040009EC RID: 2540
			public int \u1713;

			// Token: 0x040009ED RID: 2541
			public CellExport.ᜄ \u1714;

			// Token: 0x040009EE RID: 2542
			public CellExport.ᜄ \u1715;

			// Token: 0x040009EF RID: 2543
			public CellExport.ᜄ \u1716;

			// Token: 0x040009F0 RID: 2544
			public CellExport.ᜄ \u1717;

			// Token: 0x040009F1 RID: 2545
			public int \u1718;

			// Token: 0x040009F2 RID: 2546
			public int \u1719;

			// Token: 0x040009F3 RID: 2547
			public int \u171A;

			// Token: 0x040009F4 RID: 2548
			public int \u171B;

			// Token: 0x040009F5 RID: 2549
			public IntPtr \u171C;

			// Token: 0x040009F6 RID: 2550
			public IntPtr \u171D;

			// Token: 0x040009F7 RID: 2551
			public IntPtr \u171E;

			// Token: 0x040009F8 RID: 2552
			public IntPtr \u171F;
		}
	}
}

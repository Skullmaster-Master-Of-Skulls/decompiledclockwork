using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.Clipboard
{
	// Token: 0x0200022E RID: 558
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class ClipboardExport : MemoryExport
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x000B3264 File Offset: 0x000B2264
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
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000B32C0 File Offset: 0x000B22C0
		protected override void Dispose(bool disposing)
		{
			try
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
						goto IL_72;
					case 2:
						this.ᜀ.Dispose();
						this.ᜀ = null;
						num = 1;
						continue;
					case 3:
						goto IL_7A;
					}
					if (this.ᜀ != null)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
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

		// Token: 0x06001086 RID: 4230 RVA: 0x000B336C File Offset: 0x000B236C
		public override void SaveToFile()
		{
			for (;;)
			{
				IL_42:
				spr\u2561.ᜀ = this.ᜁ;
				for (;;)
				{
					IL_4D:
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								num = 1;
								continue;
							case 1:
								if (Environment.UserInteractive)
								{
									if (true)
									{
									}
									num = 3;
									continue;
								}
								goto IL_9C;
							case 2:
								if (this.ᜁ)
								{
									num = 0;
									continue;
								}
								goto IL_9C;
							case 3:
								AboutDataExport.ShowAbout(false);
								num = 4;
								continue;
							case 4:
								goto IL_76;
							}
							goto IL_42;
						}
					}
				}
			}
			IL_76:
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000B341C File Offset: 0x000B241C
		protected override void BeginDataExport()
		{
			for (;;)
			{
				IL_24:
				base.BeginDataExport();
				base.AutoFitColWidth = (this.ExportType == ClipboardExportType.Fixed);
				for (;;)
				{
					IL_39:
					int num = 0;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (base.Title.Length > 0)
							{
								num = 2;
								continue;
							}
							goto IL_83;
						case 1:
							goto IL_D9;
						case 2:
							(base.ᜀ() as spr\u21B2).ᜇ(base.Title);
							(base.ᜀ() as spr\u21B2).ᜀ('-', 80);
							(base.ᜀ() as spr\u21B2).ᜌ();
							num = 5;
							continue;
						case 3:
							if (num2 < this.Header.Count)
							{
								(base.ᜀ() as spr\u21B2).ᜇ(this.Header[num2]);
								num2++;
								num = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_39;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 4:
							goto IL_D9;
						case 5:
							goto IL_83;
						case 6:
							return;
						}
						goto IL_24;
						IL_83:
						num2 = 0;
						num = 1;
						continue;
						IL_D9:
						if (true)
						{
						}
						num = 3;
					}
				}
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000B355C File Offset: 0x000B255C
		protected override void EndDataExport()
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
				IEnumerator enumerator = this.Footer.GetEnumerator();
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
								num = 4;
								continue;
							}
							string a_ = (string)enumerator.Current;
							(base.ᜀ() as spr\u21B2).ᜇ(a_);
							num = 2;
							continue;
						}
						case 3:
							goto IL_AB;
						case 4:
							num = 3;
							continue;
						}
						IL_89:
						num = 0;
						continue;
						goto IL_89;
					}
					IL_AB:;
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
								goto IL_ED;
							case 1:
								goto IL_EB;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_EB:
					IL_ED:;
				}
				break;
			}
			}
			base.EndDataExport();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000B3670 File Offset: 0x000B2670
		protected override string GetColumnTitle(int Index)
		{
			string text;
			for (;;)
			{
				text = base.GetColumnTitle(Index);
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						ColumAlign colAlign;
						switch (colAlign)
						{
						case ColumAlign.Left:
							text = (base.ᜀ() as spr\u21B2).ᜁ(text, ' ', base.ColumnsExport[Index].Width);
							num = 12;
							continue;
						case ColumAlign.Center:
							text = (base.ᜀ() as spr\u21B2).ᜂ(text, ' ', base.ColumnsExport[Index].Width);
							num = 5;
							continue;
						case ColumAlign.Right:
							text = (base.ᜀ() as spr\u21B2).ᜀ(text, ' ', base.ColumnsExport[Index].Width);
							if (true)
							{
							}
							num = 4;
							continue;
						default:
							num = 6;
							continue;
						}
						break;
					}
					case 1:
						goto IL_F3;
					case 2:
						text += this.ᜁ;
						num = 3;
						continue;
					case 3:
						return text;
					case 4:
						goto IL_70;
					case 5:
						goto IL_70;
					case 6:
						num = 13;
						continue;
					case 7:
						num = 11;
						continue;
					case 8:
						if (this.ᜂ > 0)
						{
							num = 10;
							continue;
						}
						return text;
					case 9:
						return text;
					case 10:
						num = 1;
						continue;
					case 11:
						if (Index < base.ColumnsExport.Count - 1)
						{
							num = 2;
							continue;
						}
						return text;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F3;
						default:
							if (false)
							{
							}
							goto IL_70;
						}
						break;
					case 13:
						goto IL_70;
					case 14:
						text = (base.ᜀ() as spr\u21B2).ᜁ(text, ' ', text.Length + this.ᜂ);
						num = 9;
						continue;
					case 15:
					{
						if (this.ᜄ == ClipboardExportType.Separated)
						{
							num = 7;
							continue;
						}
						ColumAlign colAlign = base.ColumnsExport[Index].ColAlign;
						num = 0;
						continue;
					}
					}
					break;
					IL_70:
					num = 8;
					continue;
					IL_F3:
					if (Index >= base.ColumnsExport.Count - 1)
					{
						return text;
					}
					num = 14;
				}
			}
			return text;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000B38CC File Offset: 0x000B28CC
		protected override string GetColumnValue(ColExport ExportColExport)
		{
			string text;
			for (;;)
			{
				text = base.GetColumnValue(ExportColExport);
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_70;
					case 1:
						goto IL_70;
					case 2:
						if (this.ᜂ > 0)
						{
							num = 4;
							continue;
						}
						return text;
					case 3:
						if (ExportColExport.RowExport.Last() != ExportColExport)
						{
							num = 12;
							continue;
						}
						return text;
					case 4:
						num = 10;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F0;
						default:
							if (false)
							{
							}
							goto IL_70;
						}
						break;
					case 6:
						return text;
					case 7:
						goto IL_70;
					case 8:
						num = 3;
						continue;
					case 9:
						num = 1;
						continue;
					case 10:
						goto IL_F0;
					case 11:
					{
						ColumAlign colAlign;
						switch (colAlign)
						{
						case ColumAlign.Left:
							text = (base.ᜀ() as spr\u21B2).ᜁ(text, ' ', base.ColumnsExport[ExportColExport.ColumnIndex].Width);
							num = 5;
							continue;
						case ColumAlign.Center:
							text = (base.ᜀ() as spr\u21B2).ᜂ(text, ' ', base.ColumnsExport[ExportColExport.ColumnIndex].Width);
							num = 0;
							continue;
						case ColumAlign.Right:
							text = (base.ᜀ() as spr\u21B2).ᜀ(text, ' ', base.ColumnsExport[ExportColExport.ColumnIndex].Width);
							num = 7;
							continue;
						default:
							num = 9;
							continue;
						}
						break;
					}
					case 12:
						text += this.ᜁ;
						num = 14;
						continue;
					case 13:
					{
						if (this.ᜄ == ClipboardExportType.Separated)
						{
							num = 8;
							continue;
						}
						ColumAlign colAlign = base.ColumnsExport[ExportColExport.ColumnIndex].ColAlign;
						num = 11;
						continue;
					}
					case 14:
						return text;
					case 15:
						text = (base.ᜀ() as spr\u21B2).ᜁ(text, ' ', text.Length + this.ᜂ);
						num = 6;
						continue;
					}
					break;
					IL_70:
					num = 2;
					continue;
					IL_F0:
					if (ExportColExport.RowExport.Last() == ExportColExport)
					{
						return text;
					}
					num = 15;
				}
			}
			return text;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000B3B38 File Offset: 0x000B2B38
		protected override void WriteRow()
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
			(base.ᜀ() as spr\u21B2).ᜇ(this.GetDataRow(true));
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000B3B8C File Offset: 0x000B2B8C
		protected override void WriteTitleRow()
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
			(base.ᜀ() as spr\u21B2).ᜇ(this.GetCaptionRow());
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000B3BE0 File Offset: 0x000B2BE0
		protected override void WriteBlankRow()
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
			(base.ᜀ() as spr\u21B2).ᜇ(this.GetBlankRow());
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000B3C34 File Offset: 0x000B2C34
		protected override void ShowResult()
		{
			int a_ = 17;
			int num = 3;
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
						if (!this.ᜅ)
						{
							num = 4;
							continue;
						}
						num = 2;
						continue;
					case 1:
						return;
					case 2:
						if (this.ᜃ.Length == 0)
						{
							num = 5;
							continue;
						}
						goto IL_D5;
					case 4:
						goto IL_D2;
					case 5:
						goto IL_B2;
					}
					if (base.Stoped)
					{
						if (true)
						{
						}
						num = 1;
					}
					else
					{
						num = 0;
					}
					break;
				}
			}
			return;
			IL_B2:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氬崮嘰䀲樴琶唸刺䴼崾⹀≂㝄⍆Ὀ≊⡌㡎㑐⅒", a_)));
			IL_D2:
			return;
			try
			{
				IL_D5:
				Process.Start(new ProcessStartInfo(this.ᜃ)
				{
					UseShellExecute = true,
					WindowStyle = ProcessWindowStyle.Normal
				});
				return;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + HyperlinksCollectionEditor.b("‬┮爰弲尴䜶嬸吺尼䴾╀ق㵄㝆♈㥊㥌畎歐R㵔㡖⹘ग़㡜ⱞᑠརᅤ䭦Ὠ੪Ὤ啮", a_));
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x000B3D6C File Offset: 0x000B2D6C
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x000B3DB0 File Offset: 0x000B2DB0
		[Description("Gets or sets clipboard export type.")]
		[DefaultValue(ClipboardExportType.Separated)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ClipboardExportType ExportType
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
				return this.ᜄ;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜄ = value;
							num = 2;
							continue;
						case 2:
							goto IL_6C;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (value == this.ᜄ)
							{
								goto IL_6E;
							}
							num = 0;
							break;
						}
					}
				}
				IL_6C:
				IL_6E:
				base.AutoFitColWidth = (this.ᜄ == ClipboardExportType.Fixed);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x000B3E3C File Offset: 0x000B2E3C
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x000B3E80 File Offset: 0x000B2E80
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the character of exported table columns.")]
		[DefaultValue(",")]
		public string Separator
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
				return this.ᜁ;
			}
			set
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							return;
						case 2:
							this.ᜁ = value;
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (!(value != this.ᜁ))
							{
								return;
							}
							num = 2;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x000B3F00 File Offset: 0x000B2F00
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x000B3F44 File Offset: 0x000B2F44
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets internal spacing of exported table columns.")]
		[DefaultValue(2)]
		public int Spacing
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
				return this.ᜂ;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜂ = value;
							num = 1;
							continue;
						case 1:
							return;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (value == this.ᜂ)
							{
								return;
							}
							num = 0;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x000B3FC0 File Offset: 0x000B2FC0
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x000B4004 File Offset: 0x000B3004
		[DefaultValue("Clipbrd.exe")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the clipboard viewer of data exported.")]
		[Editor(typeof(ClipViewerEditor), typeof(UITypeEditor))]
		public string ClipboardViewer
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
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 1:
							return;
						case 2:
							this.ᜃ = value;
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (!(value != this.ᜃ))
							{
								return;
							}
							num = 2;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x000B4084 File Offset: 0x000B3084
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x000B40C8 File Offset: 0x000B30C8
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x000B410C File Offset: 0x000B310C
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x000B4150 File Offset: 0x000B3150
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		public new StringListCollection NotTruncatableColumns
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x000B4194 File Offset: 0x000B3194
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x000B41D8 File Offset: 0x000B31D8
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public new StringListCollection Header
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
				return base.Header;
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
				base.Header = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x000B421C File Offset: 0x000B321C
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x000B4260 File Offset: 0x000B3260
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public new StringListCollection Footer
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
				return base.Footer;
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
				base.Footer = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x000B42A4 File Offset: 0x000B32A4
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x000B42E8 File Offset: 0x000B32E8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public new bool AddTitles
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
				return base.AddTitles;
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
				base.AddTitles = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000B432C File Offset: 0x000B332C
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x000B4370 File Offset: 0x000B3370
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection Titles
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
				return base.Titles;
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
				base.Titles = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000B43B4 File Offset: 0x000B33B4
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x000B43F8 File Offset: 0x000B33F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public new FormatsExport DataFormats
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
				return base.DataFormats;
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
				base.DataFormats = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000B443C File Offset: 0x000B343C
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x000B4480 File Offset: 0x000B3480
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection CustomFormats
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
				return base.CustomFormats;
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
				base.CustomFormats = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x000B44C4 File Offset: 0x000B34C4
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x000B4508 File Offset: 0x000B3508
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(EncodingType.UTF8)]
		public new EncodingType DataEncoding
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
				return base.DataEncoding;
			}
			set
			{
				if (true)
				{
				}
				for (;;)
				{
					this.m_encodingType = value;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_53:
						switch (value)
						{
						case EncodingType.ASCII:
							goto IL_B4;
						case EncodingType.OEM:
							goto IL_7F;
						case EncodingType.UTF8:
							goto IL_73;
						case EncodingType.UTF16:
							goto IL_A8;
						default:
							num = 1;
							break;
						}
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
						case 0:
							goto IL_53;
						case 1:
							num = 2;
							continue;
						case 2:
							goto IL_A6;
						}
						break;
					}
				}
				IL_73:
				this.m_currEncoding = Encoding.UTF8;
				return;
				IL_7F:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.OEMCodePage);
				return;
				IL_A6:
				this.m_currEncoding = Encoding.UTF8;
				return;
				IL_A8:
				this.m_currEncoding = Encoding.Unicode;
				return;
				IL_B4:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.ANSICodePage);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000B45F0 File Offset: 0x000B35F0
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x000B4634 File Offset: 0x000B3634
		[Description("Indicates whether show content of the clipboard data after data exported.")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool ShowContent
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
						goto IL_64;
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
							this.ᜅ = value;
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_2F:
					num = 1;
					continue;
					IL_1C:
					if (value != this.ᜅ)
					{
						goto IL_2F;
					}
					return;
				}
				IL_64:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x000B46B0 File Offset: 0x000B36B0
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x000B46F4 File Offset: 0x000B36F4
		[Description("Indicate whether export long char/binary column.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[DefaultValue(false)]
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

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060010AD RID: 4269 RVA: 0x000B4738 File Offset: 0x000B3738
		// (remove) Token: 0x060010AE RID: 4270 RVA: 0x000B477C File Offset: 0x000B377C
		public new event DataRowEventHandler FetchedRecord
		{
			add
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
				base.FetchedRecord += value;
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
				base.FetchedRecord -= value;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000B47C0 File Offset: 0x000B37C0
		public ClipboardExport()
		{
			int a_ = 5;
			this.ᜁ = spr\u1C2B.ᡜ;
			this.ᜂ = 2;
			this.ᜃ = HyperlinksCollectionEditor.b("戠伢䰤圦䬨太䤬Į吰䬲倴", a_);
			base..ctor();
		}

		// Token: 0x04000BFB RID: 3067
		private new License ᜀ;

		// Token: 0x04000BFC RID: 3068
		private new string ᜁ;

		// Token: 0x04000BFD RID: 3069
		private new int ᜂ;

		// Token: 0x04000BFE RID: 3070
		private int[] \u2593\u00A2\u008C\u0091;

		// Token: 0x04000BFF RID: 3071
		private new string ᜃ;

		// Token: 0x04000C00 RID: 3072
		private new ClipboardExportType ᜄ;

		// Token: 0x04000C01 RID: 3073
		private int[] \u2460\u0093\u00A3\u00A0;

		// Token: 0x04000C02 RID: 3074
		private long[] \u25D8\u008E\u008B\u0098;

		// Token: 0x04000C03 RID: 3075
		private bool ᜅ;
	}
}

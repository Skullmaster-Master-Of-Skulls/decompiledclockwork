using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Common
{
	// Token: 0x0200015E RID: 350
	public abstract class TextExport : ExportBase
	{
		// Token: 0x060008D2 RID: 2258
		[DllImport("shell32")]
		private static extern int ShellExecute(IntPtr A_0, string A_1, string A_2, string A_3, string A_4, int A_5);

		// Token: 0x060008D3 RID: 2259 RVA: 0x00057D60 File Offset: 0x00056D60
		protected virtual void ShowResult()
		{
			int a_ = 15;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜃ)
					{
						goto IL_126;
					}
					goto IL_134;
				case 1:
					return;
				case 2:
					if (this.ᜂ)
					{
						num = 4;
						continue;
					}
					goto IL_109;
				case 4:
					TextExport.ShellExecute(IntPtr.Zero, HyperlinksCollectionEditor.b("䐪崬䨮弰", a_), this.GetShowedFileName(), string.Empty, string.Empty, 1);
					num = 8;
					continue;
				case 5:
					goto IL_134;
				case 6:
					return;
				case 7:
					if (base.Stoped)
					{
						num = 6;
						continue;
					}
					num = 2;
					continue;
				case 8:
					if (true)
					{
					}
					goto IL_109;
				case 9:
					TextExport.ShellExecute(IntPtr.Zero, HyperlinksCollectionEditor.b("嬪弬䘮弰䜲", a_), this.GetPrintedFileName(), string.Empty, string.Empty, 1);
					num = 5;
					continue;
				}
				if (!Environment.UserInteractive)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
				IL_109:
				num = 0;
				continue;
				IL_126:
				num = 9;
				continue;
				IL_134:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_126;
				default:
					goto IL_14A;
				}
			}
			return;
			IL_14A:
			if (false)
			{
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00057EC0 File Offset: 0x00056EC0
		protected virtual string GetShowedFileName()
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

		// Token: 0x060008D5 RID: 2261 RVA: 0x00057F04 File Offset: 0x00056F04
		protected virtual string GetPrintedFileName()
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

		// Token: 0x060008D6 RID: 2262 RVA: 0x00057F48 File Offset: 0x00056F48
		protected override Type GetWriterType()
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
			return typeof(spr\u21B2);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00057F90 File Offset: 0x00056F90
		internal new spr\u21B2 ᜀ()
		{
			int a_ = 15;
			while (this.\u171C != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u171C as spr\u21B2;
			}
			throw new NullReferenceException(HyperlinksCollectionEditor.b("☪✬笮吰䬲䄴父䄸䬺刼䴾㕀祂罄Fⱈ㽊ᩌ㵎㡐❒ご╖畘ⵚ㱜ⵞ孠㱢ቤᕦhὪ࡬ᵮ", a_));
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00058000 File Offset: 0x00057000
		protected virtual void ExportToStream(Stream Stream)
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
			StreamWriter writer = new StreamWriter(Stream, this.CurrentEncoding);
			this.SaveToMemoryStream(Stream, writer);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00058050 File Offset: 0x00057050
		protected virtual void ExportToFile()
		{
			FileStream fileStream = File.Create(this.ᜁ);
			try
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, this.CurrentEncoding);
				try
				{
					this.SaveToMemoryStream(fileStream, streamWriter);
				}
				finally
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
							goto IL_70;
						case 2:
							((IDisposable)streamWriter).Dispose();
							num = 1;
							continue;
						}
						if (streamWriter == null)
						{
							break;
						}
						num = 2;
					}
					IL_70:;
				}
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)fileStream).Dispose();
						goto IL_A4;
					case 2:
						goto IL_AE;
					}
					if (fileStream != null)
					{
						num = 0;
						continue;
					}
					goto IL_AE;
					IL_A4:
					num = 2;
					continue;
					IL_AE:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						goto IL_C4;
					}
				}
				IL_C4:
				if (false)
				{
				}
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00058144 File Offset: 0x00057144
		public override void SaveToStream(Stream Stream)
		{
			int a_ = 12;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (!base.\u1733())
					{
						goto IL_3B;
					}
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
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_BF;
				}
				if (this.m_exportIfEmpty)
				{
					break;
				}
				num = 0;
			}
			try
			{
				try
				{
					IL_3B:
					this.\u171D = true;
					this.ExportToStream(Stream);
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message + HyperlinksCollectionEditor.b("┧\u2029砫䬭䠯䘱焳丵䠷唹主䨽稿硁ᝃ❅㹇⽉ᡋ⅍͏♑♓㍕㥗㝙灛⡝şၡ幣", a_));
				}
				goto IL_C1;
			}
			finally
			{
				this.\u171D = false;
			}
			return;
			IL_C1:
			base.SaveToStream(Stream);
			return;
			IL_BF:;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00058238 File Offset: 0x00057238
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void SaveToFile()
		{
			int a_ = 6;
			for (;;)
			{
				IL_49:
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_CB:
					num = 12;
					break;
				default:
					if (false)
					{
					}
					base.SaveToFile();
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1A6;
					case 1:
						return;
					case 2:
						if (!this.m_exportIfEmpty)
						{
							num = 9;
							continue;
						}
						goto IL_1AB;
					case 3:
						goto IL_D9;
					case 4:
					{
						string startupPath;
						this.ᜁ = startupPath + '\\' + this.ᜁ;
						num = 3;
						continue;
					}
					case 5:
					{
						string startupPath = Application.StartupPath;
						num = 8;
						continue;
					}
					case 6:
						if (Path.GetDirectoryName(this.ᜁ).Length == 0)
						{
							num = 5;
							continue;
						}
						goto IL_D9;
					case 7:
						goto IL_1CE;
					case 8:
					{
						string startupPath;
						if (startupPath.Length > 0)
						{
							num = 4;
							continue;
						}
						goto IL_D9;
					}
					case 9:
						goto IL_130;
					case 10:
						if (File.Exists(this.ᜁ))
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					case 11:
						if (this.ᜁ.Length == 0)
						{
							num = 7;
							continue;
						}
						num = 10;
						continue;
					case 12:
						goto IL_D9;
					case 13:
						if (base.\u1733())
						{
							num = 1;
							continue;
						}
						goto IL_1AB;
					}
					goto IL_49;
					IL_130:
					num = 13;
					continue;
					try
					{
						IL_D9:
						this.ExportToFile();
						this.ShowResult();
						return;
					}
					catch (Exception ex)
					{
						if (!(ex is DirectoryNotFoundException) && File.Exists(this.FileName))
						{
							File.Delete(this.FileName);
						}
						throw new Exception(ex.Message + HyperlinksCollectionEditor.b("⼡⸣爥䴧利堫欭䠯䈱嬳䐵䰷9ػ洽ℿ㑁⅃ቅ❇౉╋≍㕏繑≓㝕⩗恙", a_));
					}
					goto IL_130;
					IL_1AB:
					num = 11;
				}
				IL_1A6:
				File.Delete(this.ᜁ);
				goto IL_CB;
			}
			return;
			IL_1CE:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挡嘣䄥嬧甩樫䜭尯圱稳圵唷弹礻匽〿㙁㵃", a_)));
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00058474 File Offset: 0x00057474
		protected override void SaveProperties(XMLFile File)
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
			base.SaveProperties(File);
			File.WriteValue(HyperlinksCollectionEditor.b("怦氨攪栬紮瀰缲", a_), HyperlinksCollectionEditor.b("愦䀨䜪䠬愮倰帲倴", a_), this.FileName);
			File.WriteValue(HyperlinksCollectionEditor.b("怦氨攪栬紮瀰缲", a_), HyperlinksCollectionEditor.b("栦夨个䌬礮堰嘲䈴", a_), this.ᜂ.ToString());
			File.WriteValue(HyperlinksCollectionEditor.b("怦氨攪栬紮瀰缲", a_), HyperlinksCollectionEditor.b("眦嬨䈪䌬嬮眰娲头制", a_), this.ᜃ.ToString());
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00058544 File Offset: 0x00057544
		protected override void LoadProperties(XMLFile File)
		{
			int a_ = 16;
			for (;;)
			{
				base.LoadProperties(File);
				this.ᜁ = File.ReadValue(HyperlinksCollectionEditor.b("欫欭縯眱昳眵琷", a_), HyperlinksCollectionEditor.b("樫䜭尯圱稳圵唷弹", a_), this.FileName);
				this.ᜂ = Convert.ToBoolean(File.ReadValue(HyperlinksCollectionEditor.b("欫欭縯眱昳眵琷", a_), HyperlinksCollectionEditor.b("挫席唯就戳張崷䴹", a_), this.ShowFile.ToString()));
				this.ᜃ = Convert.ToBoolean(File.ReadValue(HyperlinksCollectionEditor.b("欫欭縯眱昳眵琷", a_), HyperlinksCollectionEditor.b("簫尭夯就䀳瀵儷嘹夻", a_), this.PrintFile.ToString()));
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						goto IL_168;
					case 3:
						this.ᜄ = ActionType.OpenView;
						num = 2;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_168;
						default:
							if (false)
							{
							}
							this.ᜄ = ActionType.Print;
							num = 1;
							continue;
						}
						break;
					case 5:
						if (this.ᜂ)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_11A;
					}
					break;
					IL_11A:
					num = 0;
					continue;
					IL_168:
					goto IL_11A;
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000586BC File Offset: 0x000576BC
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00058700 File Offset: 0x00057700
		[Browsable(false)]
		[Description("Gets or sets a path and a name of the result file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FileName
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜁ = value;
							num = 0;
							continue;
						}
						break;
					}
					if (!(value != this.ᜁ))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00058780 File Offset: 0x00057780
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x000587C4 File Offset: 0x000577C4
		[Description("This boolean property allows you to show the resulting file immediately after finishing of an export process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		private bool ShowFile
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
				return this.ᜂ;
			}
			set
			{
				int num = 1;
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
							this.ᜂ = value;
							if (true)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (value == this.ᜂ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00058840 File Offset: 0x00057840
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00058884 File Offset: 0x00057884
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("This boolean property allows you to print the resulting file immediately after finishing of an export process.")]
		private bool PrintFile
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
				return this.ᜃ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
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
							this.ᜃ = value;
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_64;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					num = 1;
				}
				IL_64:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00058900 File Offset: 0x00057900
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x000589BC File Offset: 0x000579BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(ActionType.None)]
		[Description("The property allows you to execute a action after data export.")]
		public ActionType ActionAfterExport
		{
			get
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.ᜄ = ActionType.Print;
						num = 3;
						continue;
					case 2:
						if (this.ᜃ)
						{
							num = 1;
							continue;
						}
						goto IL_A7;
					case 3:
						goto IL_6D;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6D;
						default:
							if (false)
							{
							}
							this.ᜄ = ActionType.OpenView;
							num = 0;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (this.ᜂ)
					{
						num = 5;
						continue;
					}
					IL_6F:
					num = 2;
				}
				IL_6D:
				IL_A7:
				return this.ᜄ;
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
				this.ᜄ = value;
				this.ᜂ = (value == ActionType.OpenView);
				this.ᜃ = (value == ActionType.Print);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00058A14 File Offset: 0x00057A14
		[Browsable(false)]
		public new Encoding CurrentEncoding
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
				return base.CurrentEncoding;
			}
		}

		// Token: 0x040006D8 RID: 1752
		private string \u25D8\u00A8\u00AF\u00AF;

		// Token: 0x040006D9 RID: 1753
		private byte \u25D9\u008F\u00A7ª;

		// Token: 0x040006DA RID: 1754
		private new const int ᜀ = 1;

		// Token: 0x040006DB RID: 1755
		private string \u2593\u00A0\u0088\u009D;

		// Token: 0x040006DC RID: 1756
		private new string ᜁ = string.Empty;

		// Token: 0x040006DD RID: 1757
		private new bool ᜂ;

		// Token: 0x040006DE RID: 1758
		private new bool ᜃ;

		// Token: 0x040006DF RID: 1759
		private string \u2609\u008E\u0093\u0081;

		// Token: 0x040006E0 RID: 1760
		private new ActionType ᜄ;
	}
}

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Spire.DataExport.Base;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Delegates;
using Spire.DataExport.Designers;
using Spire.DataExport.EventArgs;
using Spire.DataExport.PropEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Common
{
	// Token: 0x0200015D RID: 349
	[DefaultEvent("BeforeExportRow")]
	[Designer(typeof(ExportDesigner), typeof(IDesigner))]
	public abstract class ExportBase : Component
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00052EAC File Offset: 0x00051EAC
		public ExportBase(IContainer container)
		{
			container.Add(this);
			this.ᜄ();
			this.InitializeVariables();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00052F90 File Offset: 0x00051F90
		public ExportBase()
		{
			this.ᜄ();
			this.InitializeVariables();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00053070 File Offset: 0x00052070
		public void Register(string UserName, string PassWord)
		{
			try
			{
				for (;;)
				{
					spr\u2561.ᜁ = UserName;
					spr\u2561.ᜂ = PassWord;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E3;
						case 1:
							IL_BA:
							this.ᜁ = !spr\u2561.ᜁ(spr\u2561.ᜁ.Trim(), spr\u2561.ᜂ.Trim());
							spr\u2561.ᜀ = this.ᜁ;
							num = 3;
							continue;
						case 2:
							if (spr\u2561.ᜂ.Trim().Length > 0)
							{
								num = 1;
								continue;
							}
							goto IL_BC;
						case 3:
							goto IL_BC;
						case 4:
							num = 2;
							continue;
						case 5:
							if (spr\u2561.ᜁ.Trim().Length > 0)
							{
								if (true)
								{
								}
								num = 4;
								continue;
							}
							goto IL_BC;
						}
						break;
						IL_BC:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BA;
						default:
							if (false)
							{
							}
							num = 0;
							break;
						}
					}
				}
				IL_E3:;
			}
			catch
			{
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00053180 File Offset: 0x00052180
		protected override void Dispose(bool disposing)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.Dispose();
					num = 4;
					continue;
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
						break;
					}
					this.ᜃ();
					num = 2;
					continue;
				case 2:
					if (true)
					{
					}
					if (this.ᜀ != null)
					{
						num = 0;
						continue;
					}
					goto IL_97;
				case 4:
					goto IL_6A;
				}
				if (!disposing)
				{
					break;
				}
				num = 1;
			}
			IL_6A:
			IL_97:
			base.Dispose(disposing);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0005322C File Offset: 0x0005222C
		private void ᜄ()
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
			this.ᜀ = new Container();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00053274 File Offset: 0x00052274
		protected virtual void InitializeVariables()
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
			this.\u1718 = new FormatsExport(this);
			this.ᜃ = new ColumnsExport(this, new NormalFunc(this.NormalString));
			this.ᜄ = new RowExport(this.ᜃ, this.\u1718, this.\u171E, new GetExportFieldData(this.GetColumnValue));
			this.ᜏ = new Options(this);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0005330C File Offset: 0x0005230C
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00053350 File Offset: 0x00052350
		public override ISite Site
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
				return base.Site;
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
				this.ᜃ();
				base.Site = value;
				this.ᜠ = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				this.ᜂ();
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000533BC File Offset: 0x000523BC
		private void ᜃ()
		{
			for (;;)
			{
				this.ᜠ = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						for (;;)
						{
							this.ᜠ.ComponentRemoved -= this.ᜀ;
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_88;
							}
						}
						IL_88:
						if (false)
						{
						}
						num = 0;
						continue;
					case 2:
						if (this.ᜠ != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00053464 File Offset: 0x00052464
		private void ᜂ()
		{
			int num = 0;
			for (;;)
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
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜠ.ComponentRemoved += this.ᜀ;
						goto IL_73;
					}
					if (this.ᜠ != null)
					{
						num = 2;
						continue;
					}
					return;
				}
				IL_73:
				num = 1;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000534F0 File Offset: 0x000524F0
		private void ᜀ(object A_0, ComponentEventArgs A_1)
		{
			int a_ = 3;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1.Component == this.ᜇ)
					{
						goto IL_10B;
					}
					return;
				case 2:
					goto IL_C1;
				case 3:
					if (A_1.Component == this.ᜅ)
					{
						num = 12;
						continue;
					}
					goto IL_172;
				case 4:
					this.ᜇ = null;
					num = 2;
					continue;
				case 5:
					goto IL_16D;
				case 6:
					num = 3;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					if (A_1.Component != this.ᜆ)
					{
						goto IL_7A;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10B;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 9:
					if (A_1.Component is ListView)
					{
						num = 11;
						continue;
					}
					return;
				case 10:
					if (A_1.Component is DataTable)
					{
						num = 7;
						continue;
					}
					goto IL_7A;
				case 11:
					num = 0;
					continue;
				case 12:
					goto IL_ED;
				}
				if (A_1.Component.GetType().GetInterface(HyperlinksCollectionEditor.b("嘞攠䄢昤䠦䐨䘪䰬䄮唰", a_)) != null)
				{
					num = 6;
					continue;
				}
				goto IL_172;
				IL_7A:
				num = 9;
				continue;
				IL_10B:
				num = 4;
				continue;
				IL_172:
				num = 10;
			}
			IL_C1:
			return;
			IL_ED:
			this.ᜅ = null;
			return;
			IL_16D:
			this.ᜆ = null;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0005369C File Offset: 0x0005269C
		private void ᜁ()
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
			spr\u2059.ᜀ(this.\u170D, this.ᜅ, this.ᜆ, this.ᜇ);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000536F4 File Offset: 0x000526F4
		protected void LockControls()
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
			spr\u2059.ᜁ(this.\u170D, this.ᜇ);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00053740 File Offset: 0x00052740
		protected void UnlockControls()
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
			spr\u2059.ᜀ(this.\u170D, this.ᜇ);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0005378C File Offset: 0x0005278C
		protected void First()
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
			spr\u2059.ᜀ(this.\u170D, this.ᜆ, this.ᜇ);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000537E0 File Offset: 0x000527E0
		protected void Next()
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
			spr\u2059.ᜀ(this.\u170D, spr\u2059.ᜀ, this.ᜆ, ref this.ᜂ);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00053838 File Offset: 0x00052838
		protected void Skip(int Count)
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
			spr\u2059.ᜀ(this.\u170D, spr\u2059.ᜀ, this.ᜆ, this.\u171A, this.m_skippedRecord, this, ref this.ᜂ);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0005389C File Offset: 0x0005289C
		protected bool EndOfFile()
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
			return spr\u2059.ᜀ(this.\u170D, this.ᜆ, this.ᜇ, this.RowsCount, this.MaxRows, this.SkipRows);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00053900 File Offset: 0x00052900
		protected bool CanContinue()
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
			bool canContinue = !this.\u1714;
			StopExportEventArgs stopExportEventArgs = new StopExportEventArgs(canContinue);
			this.ᜀ(this, stopExportEventArgs);
			this.\u1714 = !stopExportEventArgs.CanContinue;
			return stopExportEventArgs.CanContinue;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0005396C File Offset: 0x0005296C
		protected virtual string GetCaptionRow()
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder(string.Empty);
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= this.ColumnsExport.Count)
						{
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
						{
							if (false)
							{
							}
							string columnTitle = this.GetColumnTitle(num);
							stringBuilder.Append(columnTitle);
							num++;
							num2 = 3;
							continue;
						}
						}
						break;
					case 1:
						goto IL_2D;
					case 2:
						goto IL_4D;
					case 3:
						goto IL_2F;
					}
					break;
					IL_2F:
					num2 = 0;
					continue;
					IL_2D:
					goto IL_2F;
				}
			}
			IL_4D:
			if (true)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00053A20 File Offset: 0x00052A20
		protected virtual string GetBlankRow()
		{
			int a_ = 18;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return HyperlinksCollectionEditor.b("␭", a_);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00053A74 File Offset: 0x00052A74
		protected virtual string GetColumnTitle(int Index)
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
			return this.NormalString(this.ColumnsExport[Index].Caption);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00053AC8 File Offset: 0x00052AC8
		protected virtual void WriteTitleRow()
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

		// Token: 0x06000860 RID: 2144 RVA: 0x00053B04 File Offset: 0x00052B04
		protected virtual void WriteBlankRow()
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
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00053B40 File Offset: 0x00052B40
		protected virtual Type GetWriterType()
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

		// Token: 0x06000862 RID: 2146 RVA: 0x00053B88 File Offset: 0x00052B88
		internal spr\u1BFE ᜀ()
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
			return this.\u171C;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00053BCC File Offset: 0x00052BCC
		protected virtual void WriteRow()
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

		// Token: 0x06000864 RID: 2148 RVA: 0x00053C08 File Offset: 0x00052C08
		protected virtual void BeginDataExport()
		{
			for (;;)
			{
				this.\u171E = new CultureInfo(this.\u1718.CultureName);
				this.ColumnsExport.Clear();
				this.ColumnsExport.Fill(this.ExportLongColumn);
				this.ᜄ.Clear();
				this.ᜄ.Index.Clear();
				this.ᜄ.Culture = this.\u171E;
				int num = 0;
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.\u171B = Math.Min(this.\u171B, this.ᜇ.Items.Count);
						num2 = 2;
						continue;
					case 1:
					{
						if (num >= this.ColumnsExport.Count)
						{
							num2 = 11;
							continue;
						}
						ColumnExport columnExport = this.ColumnsExport[num];
						this.ᜄ.Add(columnExport.Name, num);
						num++;
						num2 = 3;
						continue;
					}
					case 2:
						goto IL_1C1;
					case 3:
						goto IL_14F;
					case 4:
						return;
					case 5:
						if (this.\u171B <= 0)
						{
							goto IL_1C1;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1EB;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 6:
						this.\u171A = Math.Min(this.\u171A, this.ᜇ.Items.Count);
						if (true)
						{
						}
						num2 = 5;
						continue;
					case 7:
						goto IL_1EB;
					case 8:
						if (this.AutoFitColWidth)
						{
							num2 = 7;
							continue;
						}
						return;
					case 9:
						goto IL_14F;
					case 10:
						if (this.\u170D == ExportSource.ListView)
						{
							num2 = 6;
							continue;
						}
						goto IL_1C1;
					case 11:
						this.\u1714 = false;
						num2 = 10;
						continue;
					}
					break;
					IL_14F:
					num2 = 1;
					continue;
					IL_1C1:
					this.ᜁ(this, new EventArgs());
					num2 = 8;
					continue;
					IL_1EB:
					this.ColumnsExport.AutoCalcColWidth();
					num2 = 4;
				}
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00053E30 File Offset: 0x00052E30
		protected virtual void BeforeExport()
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

		// Token: 0x06000866 RID: 2150 RVA: 0x00053E6C File Offset: 0x00052E6C
		protected virtual void FillExportRow()
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
						num2 = 7;
						continue;
					case 1:
						goto IL_187;
					case 2:
						goto IL_117;
					case 3:
						goto IL_117;
					case 4:
						goto IL_117;
					case 5:
						goto IL_133;
					case 6:
					{
						ExportSource dataSource = this.DataSource;
						if (true)
						{
						}
						num2 = 8;
						continue;
					}
					case 7:
						if (!this.ConvertBinaryToHexString)
						{
							num2 = 6;
							continue;
						}
						goto IL_53;
					case 8:
					{
						ExportSource dataSource;
						switch (dataSource)
						{
						case ExportSource.SqlCommand:
							this.ᜄ.SetBinaryColumnValue(this.ᜃ[num].Name, spr\u2059.ᜀ);
							num2 = 4;
							continue;
						case ExportSource.DataTable:
							this.ᜄ.SetBinaryColumnValue(this.ᜃ[num].Name, spr\u2059.ᜂ);
							num2 = 9;
							continue;
						case ExportSource.ListView:
							this.ᜄ.SetBinaryColumnValue(this.ᜃ[num].Name, this.ListView);
							num2 = 2;
							continue;
						default:
							num2 = 13;
							continue;
						}
						break;
					}
					case 9:
						goto IL_117;
					case 10:
						goto IL_117;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_133;
						default:
							goto IL_1C4;
						}
						break;
					case 12:
						if (num >= this.ᜃ.Count)
						{
							num2 = 11;
							continue;
						}
						num2 = 5;
						continue;
					case 13:
						num2 = 10;
						continue;
					case 14:
						goto IL_187;
					}
					break;
					IL_53:
					string value = spr\u2059.ᜀ(this.\u170D, spr\u2059.ᜀ, this.ᜇ, this.ᜃ, this.\u171E, this.ᜃ.NormalFunc, num, this.ᜂ, this.\u171A, false);
					this.ᜄ.SetValue(this.ᜃ[num].Name, value);
					num2 = 3;
					continue;
					IL_117:
					num++;
					num2 = 14;
					continue;
					IL_187:
					num2 = 12;
					continue;
					IL_133:
					if (this.ᜃ[num].ColExportType != ColExportType.Binary)
					{
						goto IL_53;
					}
					num2 = 0;
				}
			}
			IL_1C4:
			if (false)
			{
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000540C0 File Offset: 0x000530C0
		protected string GetColumnValue(int index, bool needFormat)
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
			return this.ᜄ[index].GetExportedValue(needFormat);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00054110 File Offset: 0x00053110
		protected virtual string GetDataRow(bool NeedFormat)
		{
			if (true)
			{
			}
			StringBuilder stringBuilder;
			for (;;)
			{
				IL_20:
				stringBuilder = new StringBuilder(this.ᜄ.Count);
				int num = 0;
				for (;;)
				{
					IL_33:
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_5B;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_33;
							default:
								if (false)
								{
								}
								goto IL_3D;
							}
							break;
						case 2:
							if (num >= this.ᜄ.Count)
							{
								num2 = 0;
								continue;
							}
							stringBuilder.Append(this.GetColumnValue(num, NeedFormat));
							num++;
							num2 = 1;
							continue;
						case 3:
							goto IL_3D;
						}
						goto IL_20;
						IL_3D:
						num2 = 2;
					}
				}
			}
			IL_5B:
			return stringBuilder.ToString();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000541C4 File Offset: 0x000531C4
		internal bool \u1733()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_EC;
			}
			if (false)
			{
			}
			this.ᜁ();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						spr\u2059.ᜀ = this.ᜅ.ExecuteReader();
						num = 1;
						continue;
					case 1:
						goto IL_82;
					case 3:
						goto IL_A7;
					}
					if (true)
					{
					}
					if (this.DataSource == ExportSource.SqlCommand)
					{
						num = 0;
						continue;
					}
					IL_82:
					this.\u171F = spr\u2059.ᜁ(this.\u170D, this.ᜆ, this.ᜇ);
					num = 3;
				}
				IL_A7:;
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E9;
					case 2:
						spr\u2059.ᜀ.Close();
						num = 0;
						continue;
					}
					if (this.DataSource != ExportSource.SqlCommand)
					{
						break;
					}
					num = 2;
				}
				IL_E9:;
			}
			IL_EC:
			return this.\u171F;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000542D4 File Offset: 0x000532D4
		protected void DoExport()
		{
			for (;;)
			{
				this.ᜁ();
				this.ᜂ = 0;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜏ.DisableControls)
						{
							num = 2;
							continue;
						}
						goto IL_61C;
					case 1:
						if (true)
						{
						}
						Cursor.Current = Cursors.WaitCursor;
						num = 9;
						continue;
					case 2:
						num = 5;
						continue;
					case 3:
						num = 8;
						continue;
					case 4:
						this.LockControls();
						num = 7;
						continue;
					case 5:
						if (Environment.UserInteractive)
						{
							num = 4;
							continue;
						}
						goto IL_61C;
					case 6:
						if (this.ᜏ.WaitCursor)
						{
							num = 3;
							continue;
						}
						goto IL_CF;
					case 7:
						goto IL_61C;
					case 8:
						if (Environment.UserInteractive)
						{
							num = 1;
							continue;
						}
						goto IL_CF;
					case 9:
						goto IL_CF;
					}
					break;
					IL_61C:
					num = 6;
					continue;
					try
					{
						IL_CF:
						for (;;)
						{
							this.BeginDataExport();
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_52B;
								case 1:
									goto IL_536;
								case 2:
									if (this.Stoped)
									{
										num = 0;
										continue;
									}
									try
									{
										num = 9;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_204;
											case 1:
												if (this.DataSource == ExportSource.SqlCommand)
												{
													num = 0;
													continue;
												}
												goto IL_209;
											case 2:
												goto IL_522;
											case 3:
												if (this.ᜏ.InsertRowAfterTitle)
												{
													num = 4;
													continue;
												}
												goto IL_1BD;
											case 4:
												switch ((1 == 1) ? 1 : 0)
												{
												case 0:
												case 2:
													goto IL_204;
												default:
													if (false)
													{
													}
													this.WriteBlankRow();
													num = 6;
													continue;
												}
												break;
											case 5:
												num = 11;
												continue;
											case 6:
												goto IL_1BD;
											case 7:
												this.WriteTitleRow();
												num = 3;
												continue;
											case 8:
												goto IL_209;
											case 10:
												if (this.Stoped)
												{
													num = 5;
													continue;
												}
												this.BeforeExport();
												num = 1;
												continue;
											case 11:
												goto IL_164;
											}
											if (this.ᜑ)
											{
												num = 7;
												continue;
											}
											IL_1BD:
											num = 10;
											continue;
											IL_4EB:
											Monitor.Enter(this);
											spr\u2059.ᜀ = this.ᜅ.ExecuteReader();
											num = 8;
											continue;
											try
											{
												IL_209:
												for (;;)
												{
													this.First();
													num = 17;
													for (;;)
													{
														ExportRowEventArgs exportRowEventArgs;
														switch (num)
														{
														case 0:
															num = 16;
															continue;
														case 1:
															num = 24;
															continue;
														case 2:
															goto IL_429;
														case 3:
															this.WriteRow();
															this.ᜄ(this, this.ᜂ);
															num = 12;
															continue;
														case 4:
															goto IL_495;
														case 5:
															if (!this.EndOfFile())
															{
																num = 22;
																continue;
															}
															goto IL_495;
														case 6:
															goto IL_4A0;
														case 7:
															goto IL_336;
														case 8:
															goto IL_2CB;
														case 9:
															if (this.CanContinue())
															{
																num = 2;
																continue;
															}
															goto IL_495;
														case 10:
															num = 7;
															continue;
														case 11:
															if (spr\u2561.ᜀ)
															{
																num = 1;
																continue;
															}
															goto IL_2CB;
														case 12:
															goto IL_3D8;
														case 13:
															goto IL_406;
														case 14:
															num = 9;
															continue;
														case 15:
															goto IL_401;
														case 16:
															if (this.ᜂ >= this.\u171B)
															{
																num = 4;
																continue;
															}
															goto IL_35E;
														case 17:
															if (this.Stoped)
															{
																num = 23;
																continue;
															}
															this.Skip(this.\u171A);
															num = 18;
															continue;
														case 18:
															if (this.Stoped)
															{
																num = 10;
																continue;
															}
															this.RowsCount = 0;
															num = 13;
															continue;
														case 19:
															if (this.\u171B != 0)
															{
																num = 0;
																continue;
															}
															goto IL_35E;
														case 20:
															goto IL_406;
														case 21:
															if (exportRowEventArgs.Accept)
															{
																num = 3;
																continue;
															}
															goto IL_3D8;
														case 22:
															num = 19;
															continue;
														case 23:
															num = 15;
															continue;
														case 24:
															if (this.ᜂ < this.\u1713)
															{
																num = 8;
																continue;
															}
															goto IL_495;
														case 25:
															if (this.\u1714)
															{
																num = 14;
																continue;
															}
															goto IL_429;
														}
														break;
														IL_2CB:
														num = 25;
														continue;
														IL_35E:
														num = 11;
														continue;
														IL_3D8:
														this.Next();
														Application.DoEvents();
														Thread.Sleep(0);
														num = 20;
														continue;
														IL_406:
														num = 5;
														continue;
														IL_429:
														bool accept = true;
														this.FillExportRow();
														exportRowEventArgs = new ExportRowEventArgs(this.ᜄ, accept);
														this.ᜀ(this, exportRowEventArgs);
														num = 21;
														continue;
														IL_495:
														num = 6;
													}
												}
												IL_336:
												IL_401:
												break;
												IL_4A0:
												goto IL_511;
											}
											finally
											{
												num = 0;
												for (;;)
												{
													switch (num)
													{
													case 1:
														spr\u2059.ᜀ.Close();
														Monitor.Exit(this);
														num = 2;
														continue;
													case 2:
														goto IL_4E8;
													}
													if (this.DataSource != ExportSource.SqlCommand)
													{
														break;
													}
													num = 1;
												}
												IL_4E8:;
											}
											goto IL_4EB;
											IL_511:
											this.AfterExport();
											num = 2;
											continue;
											IL_204:
											goto IL_4EB;
										}
										IL_164:
										return;
										IL_522:
										goto IL_53B;
									}
									finally
									{
										this.EndDataExport();
									}
									goto IL_52B;
									IL_53B:
									num = 3;
									continue;
								case 3:
									goto IL_546;
								}
								break;
								IL_52B:
								num = 1;
							}
						}
						IL_536:
						IL_546:
						return;
					}
					finally
					{
						num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								Cursor.Current = Cursors.Arrow;
								num = 4;
								continue;
							case 1:
								goto IL_5D7;
							case 2:
								if (this.ᜏ.WaitCursor)
								{
									num = 7;
									continue;
								}
								goto IL_61B;
							case 3:
								this.UnlockControls();
								num = 1;
								continue;
							case 4:
								goto IL_5BC;
							case 5:
								if (Environment.UserInteractive)
								{
									num = 3;
									continue;
								}
								goto IL_5D7;
							case 7:
								num = 8;
								continue;
							case 8:
								if (Environment.UserInteractive)
								{
									num = 0;
									continue;
								}
								goto IL_61B;
							case 9:
								num = 5;
								continue;
							}
							if (this.ᜏ.DisableControls)
							{
								num = 9;
								continue;
							}
							IL_5D7:
							num = 2;
						}
						IL_5BC:
						IL_61B:;
					}
					goto IL_61C;
				}
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00054980 File Offset: 0x00053980
		protected virtual void AfterExport()
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

		// Token: 0x0600086C RID: 2156 RVA: 0x000549BC File Offset: 0x000539BC
		protected virtual void EndDataExport()
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
			this.ᜀ(this, new EventArgs());
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00054A04 File Offset: 0x00053A04
		protected virtual void SaveProperties(XMLFile File)
		{
			int a_ = 18;
			switch (0)
			{
			default:
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
				for (;;)
				{
					File.WriteValue(HyperlinksCollectionEditor.b("椭甯簱焳搵礷瘹", a_), HyperlinksCollectionEditor.b("簭唯儱䜳电圷伹刻䨽", a_), this.MaxRows.ToString());
					File.WriteValue(HyperlinksCollectionEditor.b("椭甯簱焳搵礷瘹", a_), HyperlinksCollectionEditor.b("紭嬯嬱䐳搵崷夹伻", a_), this.SkipRows.ToString());
					File.WriteValue(HyperlinksCollectionEditor.b("椭甯簱焳搵礷瘹", a_), HyperlinksCollectionEditor.b("欭帯儱嬳刵儷吹嬻樽㤿㉁⅃", a_), ((int)this.DataEncoding).ToString());
					File.WriteValue(HyperlinksCollectionEditor.b("椭甯簱焳搵礷瘹", a_), HyperlinksCollectionEditor.b("欭䠯䈱嬳䐵䰷椹医䬽㈿⅁⅃", a_), ((int)this.DataSource).ToString());
					File.RemoveSection(HyperlinksCollectionEditor.b("洭缯縱愳笵瘷椹", a_));
					int num = 0;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_230;
						case 2:
							goto IL_230;
						case 3:
							if (num >= this.Columns.Count)
							{
								num2 = 6;
								continue;
							}
							File.WriteValue(HyperlinksCollectionEditor.b("洭缯縱愳笵瘷椹", a_), string.Format(HyperlinksCollectionEditor.b("唭/伱伳ܵ䔷", a_), HyperlinksCollectionEditor.b("洭弯帱䄳嬵嘷", a_), num), this.Columns[num]);
							num++;
							num2 = 4;
							continue;
						case 4:
							goto IL_25B;
						case 5:
							goto IL_25B;
						case 6:
						{
							if (true)
							{
							}
							File.RemoveSection(HyperlinksCollectionEditor.b("怭搯琱紳猵琷縹漻", a_));
							int num3 = 0;
							num2 = 2;
							continue;
						}
						case 7:
						{
							int num3;
							if (num3 >= this.NotTruncatableColumns.Count)
							{
								num2 = 0;
								continue;
							}
							File.WriteValue(HyperlinksCollectionEditor.b("怭搯琱紳猵琷縹漻", a_), string.Format(HyperlinksCollectionEditor.b("唭/伱伳ܵ䔷", a_), HyperlinksCollectionEditor.b("洭弯帱䄳嬵嘷", a_), num3), this.NotTruncatableColumns[num3]);
							num3++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_230:
						num2 = 7;
						continue;
						IL_25B:
						num2 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00054C9C File Offset: 0x00053C9C
		protected virtual void LoadProperties(XMLFile File)
		{
			int a_ = 6;
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					this.\u171B = Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("攡愣栥洧砩洫戭", a_), HyperlinksCollectionEditor.b("瀡䄣䔥嬧椩䌫嬭帯䘱", a_), this.MaxRows.ToString()));
					this.\u171A = Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("攡愣栥洧砩洫戭", a_), HyperlinksCollectionEditor.b("無伣伥堧砩䤫䴭䌯", a_), this.SkipRows.ToString()));
					this.m_encodingType = (EncodingType)Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("攡愣栥洧砩洫戭", a_), HyperlinksCollectionEditor.b("朡䨣䔥䜧丩䔫䀭圯昱䴳䘵崷", a_), ((int)this.DataEncoding).ToString()));
					this.\u170D = (ExportSource)Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("攡愣栥洧砩洫戭", a_), HyperlinksCollectionEditor.b("朡尣嘥䜧堩堫紭弯䜱䘳唵崷", a_), ((int)this.DataSource).ToString()));
					Array array = null;
					this.Columns.Clear();
					File.ReadValues(HyperlinksCollectionEditor.b("愡欣樥紧朩戫紭", a_), ref array);
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_2ED;
						case 1:
						{
							int num2;
							if (num2 >= this.Columns.Count)
							{
								num = 6;
								continue;
							}
							this.Columns[num2] = File.ReadValue(HyperlinksCollectionEditor.b("愡欣樥紧朩戫紭", a_), this.Columns[num2], string.Empty);
							num2++;
							num = 0;
							continue;
						}
						case 2:
							goto IL_251;
						case 3:
							goto IL_2ED;
						case 4:
						{
							int num3;
							if (num3 >= this.NotTruncatableColumns.Count)
							{
								num = 9;
								continue;
							}
							this.NotTruncatableColumns[num3] = File.ReadValue(HyperlinksCollectionEditor.b("氡瀣急愧漩怫樭振", a_), this.NotTruncatableColumns[num3], string.Empty);
							num3++;
							num = 2;
							continue;
						}
						case 5:
							if (array != null)
							{
								num = 11;
								continue;
							}
							return;
						case 6:
							goto IL_1CA;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1EB;
							default:
								if (false)
								{
								}
								if (array != null)
								{
									num = 10;
									continue;
								}
								goto IL_1CA;
							}
							break;
						case 8:
							goto IL_251;
						case 9:
							return;
						case 10:
						{
							this.Columns.SetStrings(array as string[]);
							int num2 = 0;
							num = 3;
							continue;
						}
						case 11:
						{
							this.NotTruncatableColumns.SetStrings(array as string[]);
							int num3 = 0;
							num = 8;
							continue;
						}
						}
						break;
						IL_1EB:
						num = 5;
						continue;
						IL_1CA:
						this.NotTruncatableColumns.Clear();
						File.ReadValues(HyperlinksCollectionEditor.b("氡瀣急愧漩怫樭振", a_), ref array);
						goto IL_1EB;
						IL_251:
						num = 4;
						continue;
						IL_2ED:
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00054FC4 File Offset: 0x00053FC4
		protected virtual bool CharInSpecialCharacters(char Char)
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
			return false;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00055000 File Offset: 0x00054000
		protected virtual void GetTempFileName()
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
			this.m_tempFileName = Path.GetTempFileName();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00055048 File Offset: 0x00054048
		protected void ReadFromFile(FileStream Source, Stream Dest, long Count)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				byte[] buffer = null;
				int num3 = 10;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_14C;
					case 1:
						goto IL_B7;
					case 2:
						return;
					case 3:
						goto IL_109;
					case 4:
						num2 = num;
						num3 = 9;
						continue;
					case 5:
						if (Count > (long)num)
						{
							num3 = 4;
							continue;
						}
						num2 = (int)Count;
						goto IL_6C;
					case 6:
						num = 61440;
						num3 = 0;
						continue;
					case 7:
						Source.Position = 0L;
						Count = Source.Length;
						num3 = 11;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							goto IL_109;
						}
						break;
					case 9:
						goto IL_B7;
					case 10:
						if (Count == 0L)
						{
							if (true)
							{
							}
							num3 = 7;
							continue;
						}
						goto IL_90;
					case 11:
						goto IL_90;
					case 12:
						if (Count > 61440L)
						{
							num3 = 6;
							continue;
						}
						num = (int)Count;
						num3 = 13;
						continue;
					case 13:
						goto IL_14C;
					case 14:
						if (Count == 0L)
						{
							num3 = 2;
							continue;
						}
						num3 = 5;
						continue;
					}
					break;
					IL_6C:
					num3 = 1;
					continue;
					IL_90:
					num3 = 12;
					continue;
					IL_B7:
					Source.Read(buffer, 0, num2);
					Dest.Write(buffer, 0, num2);
					Count -= (long)num2;
					num3 = 3;
					continue;
					IL_109:
					num3 = 14;
					continue;
					IL_14C:
					buffer = new byte[num];
					num3 = 8;
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000551DC File Offset: 0x000541DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void SaveToFile()
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

		// Token: 0x06000873 RID: 2163 RVA: 0x00055218 File Offset: 0x00054218
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void SaveToStream(Stream Stream)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					FileStream fileStream = new FileStream(this.m_tempFileName, FileMode.Open);
					num = 6;
					continue;
				}
				case 2:
					if (File.Exists(this.m_tempFileName))
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_43;
				case 5:
					return;
				case 6:
					try
					{
						FileStream fileStream;
						this.ReadFromFile(fileStream, Stream, 0L);
						Stream.Seek(0L, SeekOrigin.Begin);
						goto IL_87;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							FileStream fileStream;
							switch (num)
							{
							case 0:
								goto IL_145;
							case 2:
								((IDisposable)fileStream).Dispose();
								num = 0;
								continue;
							}
							if (fileStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_145:;
					}
					return;
					IL_87:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_43;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						File.Delete(this.m_tempFileName);
						num = 5;
						continue;
					}
					break;
				case 7:
					if (this.m_tempFileName.Trim().Length > 0)
					{
						num = 3;
						continue;
					}
					return;
				}
				if (this.m_tempFileName != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_43:
				num = 7;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00055380 File Offset: 0x00054380
		protected virtual void SaveToMemoryStream(Stream Stream, TextWriter Writer)
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
				spr\u1BFE spr_u1BFE = this.\u171C = (spr\u1BFE)Activator.CreateInstance(this.GetWriterType(), new object[]
				{
					this,
					Stream,
					Writer
				});
				try
				{
					if (true)
					{
					}
					this.DoExport();
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)spr_u1BFE).Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_9D;
						}
						if (spr_u1BFE == null)
						{
							break;
						}
						num = 0;
					}
					IL_9D:;
				}
				break;
			}
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00055440 File Offset: 0x00054440
		protected void ExportToHttpResponse(string FileName, MemoryStream Stream, string contextType, HttpResponse response, SaveType saveType)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_122;
				case 1:
					if (saveType == SaveType.Attachment)
					{
						num = 5;
						continue;
					}
					response.AddHeader(HyperlinksCollectionEditor.b("䔥䜧䐩堫䬭帯䘱ᤳ刵儷䤹䰻儽㌿⭁ぃ⽅❇⑉", a_), HyperlinksCollectionEditor.b("伥䘧䘩䔫䀭唯ऱᐳᘵ帷匹倻嬽⸿⍁⥃⍅畇", a_) + FileName);
					num = 0;
					continue;
				case 2:
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
					break;
				case 3:
					goto IL_68;
				case 4:
					goto IL_AD;
				case 5:
					response.AddHeader(HyperlinksCollectionEditor.b("䔥䜧䐩堫䬭帯䘱ᤳ刵儷䤹䰻儽㌿⭁ぃ⽅❇⑉", a_), HyperlinksCollectionEditor.b("䜥尧帩䴫䴭堯弱儳堵䰷Ĺ᰻ḽ☿⭁⡃⍅♇⭉⅋⭍浏", a_) + FileName);
					if (true)
					{
					}
					num = 4;
					continue;
				case 6:
					goto IL_14B;
				case 7:
				{
					if (FileName.LastIndexOf(Path.DirectorySeparatorChar) >= 0)
					{
						num = 6;
						continue;
					}
					response.Clear();
					response.ContentType = contextType;
					HttpCachePolicy cache = response.Cache;
					cache.SetExpires(DateTime.Now - TimeSpan.FromMinutes(1.0));
					cache.SetCacheability(HttpCacheability.Private);
					response.AddHeader(HyperlinksCollectionEditor.b("嘥娧䬩䬫䌭儯", a_), HyperlinksCollectionEditor.b("䠥䜧ܩ伫伭匯娱儳", a_));
					num = 1;
					continue;
				}
				}
				if (FileName.Length == 0)
				{
					num = 3;
				}
				else
				{
					num = 7;
				}
			}
			IL_68:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("朥娧䴩弫焭瘯嬱堳匵瘷嬹儻嬽Կ⽁㑃㉅ㅇ", a_)));
			IL_AD:
			IL_122:
			goto IL_1D1;
			IL_14B:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("朥娧䴩弫焭瘯嬱堳匵瘷嬹儻嬽̿ⵁ⩃㉅⥇⍉≋ṍㅏ♑㱓", a_)));
			IL_1D1:
			response.BinaryWrite(Stream.ToArray());
			response.Flush();
			response.Close();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0005563C File Offset: 0x0005463C
		public virtual void Stop()
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
			this.\u1714 = true;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00055680 File Offset: 0x00054680
		internal virtual string NormalString(string S)
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
			return S;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000556BC File Offset: 0x000546BC
		protected virtual string GetColumnValue(ColExport col)
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
			return col.Value;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00055700 File Offset: 0x00054700
		internal void ᜃ(string A_0)
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
			this.LoadProperties(new XMLFile(A_0));
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00055748 File Offset: 0x00054748
		internal void ᜂ(string A_0)
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
			this.SaveProperties(new XMLFile(A_0));
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00055790 File Offset: 0x00054790
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x000557D4 File Offset: 0x000547D4
		protected bool ExportLongColumn
		{
			[CompilerGenerated]
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
				return this.ᜢ;
			}
			[CompilerGenerated]
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
				this.ᜢ = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00055818 File Offset: 0x00054818
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0005585C File Offset: 0x0005485C
		protected virtual bool ConvertBinaryToHexString
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000558A0 File Offset: 0x000548A0
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000558E4 File Offset: 0x000548E4
		protected int RowsCount
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
				return this.ᜂ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						goto IL_5D;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						this.ᜂ = value;
						num = 1;
						continue;
					}
					if (false)
					{
					}
					if (value == this.ᜂ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00055960 File Offset: 0x00054960
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x000559A4 File Offset: 0x000549A4
		[Editor(typeof(ExportColumnsEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data")]
		[Description("Gets or sets the field bindings and display attributes of the columns in the exported file.")]
		public StringListCollection Columns
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
							goto IL_27;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						if (value != this.ᜐ)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						this.ᜐ = value;
						num = 3;
						continue;
					}
					goto IL_24;
					IL_27:
					num = 1;
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00055A3C File Offset: 0x00054A3C
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00055A80 File Offset: 0x00054A80
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Determines whether export when export dataset is empty.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool ExportIfEmpty
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
				return this.m_exportIfEmpty;
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
				this.m_exportIfEmpty = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00055AC4 File Offset: 0x00054AC4
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00055B08 File Offset: 0x00054B08
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Determines the number of records, which are not exported. If SkipRecCount = 0, then all the records are exported.")]
		[DefaultValue(0)]
		public int SkipRows
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
						this.\u171A = value;
						goto IL_5C;
					case 2:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5C:
						if (true)
						{
						}
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == this.\u171A)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00055B84 File Offset: 0x00054B84
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00055BC8 File Offset: 0x00054BC8
		[DefaultValue(0)]
		[Category("Behavior")]
		[Description("Determines the number of rows that exported from the source table. If MaxRows equals 0, then the recrods exported no limited.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int MaxRows
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
				return this.\u171B;
			}
			set
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
						this.\u171B = value;
						goto IL_64;
					case 2:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == this.\u171B)
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00055C44 File Offset: 0x00054C44
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x00055C88 File Offset: 0x00054C88
		[Category("Behavior")]
		[Description("Gets or sets width of each column in the result file is set automatically.")]
		protected bool AutoFitColWidth
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
				return this.\u1712;
			}
			set
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1712 = value;
						goto IL_64;
					case 1:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == this.\u1712)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00055D04 File Offset: 0x00054D04
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00055D48 File Offset: 0x00054D48
		[Category("Behavior")]
		[Description("If this property is True, then Captions are visible in the result file.")]
		protected bool AddTitles
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
				return this.ᜑ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						this.ᜑ = value;
						goto IL_64;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜑ)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00055DC4 File Offset: 0x00054DC4
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00055E08 File Offset: 0x00054E08
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal ColumnsExport ColumnsExport
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜃ = value;
						goto IL_64;
					case 2:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (value == this.ᜃ)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00055E84 File Offset: 0x00054E84
		protected RowExport ExportRowExport
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
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00055EC8 File Offset: 0x00054EC8
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00055F0C File Offset: 0x00054F0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal DataTable SQLCommandSchema
		{
			[CompilerGenerated]
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
				return this.ᜣ;
			}
			[CompilerGenerated]
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
				this.ᜣ = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00055F50 File Offset: 0x00054F50
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x00055F94 File Offset: 0x00054F94
		[Category("Behavior")]
		[Description("Gets or sets Options of data export.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public Options Options
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
				return this.ᜏ;
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
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						if (value != this.ᜏ)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						num = 2;
						continue;
					case 4:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_59;
							}
						}
						IL_59:
						if (false)
						{
						}
						this.ᜏ = value;
						num = 0;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0005602C File Offset: 0x0005502C
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x00056070 File Offset: 0x00055070
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[Browsable(false)]
		[Description("Indicates whether Stop method was called.")]
		public bool Stoped
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.\u1714 = value;
						goto IL_64;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 0;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (value == this.\u1714)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000560EC File Offset: 0x000550EC
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00056130 File Offset: 0x00055130
		[Browsable(false)]
		[Category("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets or sets the text placed before the exproted data.")]
		public StringListCollection Header
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_51;
							}
						}
						IL_51:
						if (false)
						{
						}
						if (true)
						{
						}
						this.\u1715 = value;
						num = 4;
						continue;
					case 1:
						if (value != this.\u1715)
						{
							num = 0;
							continue;
						}
						return;
					case 3:
						num = 1;
						continue;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000561C8 File Offset: 0x000551C8
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0005620C File Offset: 0x0005520C
		[Category("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets or sets the column titles in the result file.")]
		[Browsable(false)]
		public StringListCollection Titles
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.\u1716)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_59;
							}
						}
						IL_59:
						if (false)
						{
						}
						this.\u1716 = value;
						num = 4;
						continue;
					case 3:
						num = 0;
						continue;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000562A4 File Offset: 0x000552A4
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x000562E8 File Offset: 0x000552E8
		[Description("Gets or sets the text placed after the exproted data.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Category("Data")]
		public StringListCollection Footer
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
				return this.\u1717;
			}
			set
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_59;
							}
						}
						IL_59:
						if (false)
						{
						}
						this.\u1717 = value;
						num = 4;
						continue;
					case 2:
						num = 3;
						continue;
					case 3:
						if (value != this.\u1717)
						{
							num = 1;
							continue;
						}
						return;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00056380 File Offset: 0x00055380
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x000563C4 File Offset: 0x000553C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets or sets data format for data exported.")]
		[Browsable(false)]
		[Category("Data")]
		public FormatsExport DataFormats
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
				return this.\u1718;
			}
			set
			{
				int num = 2;
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
					case 3:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_51;
							}
						}
						IL_51:
						if (true)
						{
						}
						if (false)
						{
						}
						this.\u1718 = value;
						num = 1;
						continue;
					case 4:
						num = 0;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0005645C File Offset: 0x0005545C
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x000564A0 File Offset: 0x000554A0
		[Description("Gets or sets a special format for data exported.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Category("Data")]
		public StringListCollection CustomFormats
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
				return this.\u1719;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.\u1719)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						num = 0;
						continue;
					case 3:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_59;
							}
						}
						IL_59:
						if (false)
						{
						}
						this.\u1719 = value;
						num = 1;
						continue;
					}
					if (value == null)
					{
						break;
					}
					if (true)
					{
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00056538 File Offset: 0x00055538
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0005657C File Offset: 0x0005557C
		[Category("Data")]
		[Description("This property determines the title of the exported document.")]
		protected string Title
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
						return;
					case 2:
						this.ᜈ = value;
						goto IL_69;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_69:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (!(value != this.ᜈ))
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000565FC File Offset: 0x000555FC
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x00056640 File Offset: 0x00055640
		[Description("Gets or sets column width of result file.")]
		[Category("Behavior")]
		protected StringListCollection ColumnsWidth
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
				return this.ᜉ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_51;
							}
						}
						IL_51:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜉ = value;
						num = 3;
						continue;
					case 2:
						if (value != this.ᜉ)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						num = 2;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000566D8 File Offset: 0x000556D8
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0005671C File Offset: 0x0005571C
		[Category("Behavior")]
		[Description("Gets or sets the aligments of the exproted columns.")]
		protected StringListCollection ColumnsAlign
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
				return this.ᜊ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value != this.ᜊ)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						return;
					case 3:
						num = 1;
						continue;
					case 4:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_51;
							}
						}
						IL_51:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜊ = value;
						num = 2;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000567B4 File Offset: 0x000557B4
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000567F8 File Offset: 0x000557F8
		[Description("Gets or sets the length of the exported columns.")]
		[Category("Behavior")]
		protected StringListCollection ColumnsLength
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_51;
							}
						}
						IL_51:
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜋ = value;
						num = 3;
						continue;
					case 1:
						num = 4;
						continue;
					case 3:
						return;
					case 4:
						if (value != this.ᜋ)
						{
							num = 0;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00056890 File Offset: 0x00055890
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000568D4 File Offset: 0x000558D4
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		protected StringListCollection NotTruncatableColumns
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
				return this.ᜌ;
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
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_51;
							}
						}
						IL_51:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜌ = value;
						num = 4;
						continue;
					case 3:
						if (value != this.ᜌ)
						{
							num = 1;
							continue;
						}
						return;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0005696C File Offset: 0x0005596C
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000569B0 File Offset: 0x000559B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CultureInfo Culture
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						if (value != this.\u171E)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_59;
							}
						}
						IL_59:
						if (false)
						{
						}
						this.\u171E = value;
						num = 4;
						continue;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00056A48 File Offset: 0x00055A48
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00056A8C File Offset: 0x00055A8C
		[Description("Gets or sets the encoding type of result data exported.")]
		protected EncodingType DataEncoding
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
				return this.m_encodingType;
			}
			set
			{
				for (;;)
				{
					this.m_encodingType = value;
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
								goto IL_B4;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 1:
							goto IL_A6;
						case 2:
							switch (value)
							{
							case EncodingType.ASCII:
								goto IL_B4;
							case EncodingType.OEM:
								goto IL_7F;
							case EncodingType.UTF8:
								goto IL_6B;
							case EncodingType.UTF16:
								goto IL_A8;
							default:
								num = 0;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_6B:
				if (true)
				{
				}
				this.m_currEncoding = Encoding.UTF8;
				return;
				IL_7F:
				this.m_currEncoding = Encoding.GetEncoding(this.\u171E.TextInfo.OEMCodePage);
				return;
				IL_A6:
				this.m_currEncoding = Encoding.UTF8;
				return;
				IL_A8:
				this.m_currEncoding = Encoding.Unicode;
				return;
				IL_B4:
				this.m_currEncoding = Encoding.GetEncoding(this.\u171E.TextInfo.ANSICodePage);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00056B74 File Offset: 0x00055B74
		protected Encoding CurrentEncoding
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7F;
					case 2:
						this.m_currEncoding = Encoding.GetEncoding(this.\u171E.TextInfo.ANSICodePage);
						goto IL_77;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_77:
						num = 0;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (this.m_encodingType != EncodingType.ASCII)
						{
							goto IL_81;
						}
						num = 2;
						break;
					}
				}
				IL_7F:
				IL_81:
				return this.m_currEncoding;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00056C08 File Offset: 0x00055C08
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00056C4C File Offset: 0x00055C4C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(ExportSource.SqlCommand)]
		[Category("Data")]
		[Description("Gets or sets the data source type.")]
		public ExportSource DataSource
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
				return this.\u170D;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u170D = value;
						goto IL_64;
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
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == this.\u170D)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00056CC8 File Offset: 0x00055CC8
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00056D0C File Offset: 0x00055D0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Data")]
		[Description("Gets or sets exported SQL command.")]
		[DefaultValue(null)]
		public IDbCommand SQLCommand
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
				return this.ᜅ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜅ = value;
						goto IL_64;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (value == this.ᜅ)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00056D88 File Offset: 0x00055D88
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00056DCC File Offset: 0x00055DCC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		[Category("Data")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Use this property to determine the exported table, if the ExportSource equals esDataTable.")]
		public DataTable DataTable
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_64;
					case 2:
						this.ᜆ = value;
						goto IL_5C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5C:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜆ)
						{
							goto IL_66;
						}
						num = 2;
						break;
					}
				}
				IL_64:
				IL_66:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00056E48 File Offset: 0x00055E48
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00056E8C File Offset: 0x00055E8C
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Data")]
		[Description("Gets or sets the exported list view.")]
		[DefaultValue(null)]
		public ListView ListView
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
						return;
					case 2:
						this.ᜇ = value;
						goto IL_64;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜇ)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00056F08 File Offset: 0x00055F08
		internal void ᜀ(object A_0, CellParamsEventArgs A_1)
		{
			int a_ = 4;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 1:
					this.m_getCellParams(A_0, A_1);
					num = 0;
					continue;
				case 2:
					goto IL_54;
				case 3:
					if (true)
					{
					}
					if (this.m_getCellParams != null)
					{
						num = 1;
						continue;
					}
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8F;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
					break;
				}
			}
			IL_54:
			goto IL_8F;
			IL_6B:
			return;
			IL_8F:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("ⴟ⠡愣帥堧䔩師娭爯匱䜳匵ȷ9渻弽⤿ㅁ⅃Ņⵇ㹉ཋ⭍㱏㹑ѓ㝕⩗㭙ㅛⵝ䱟ᑡգᑥ剧ཀྵ", a_));
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00056FC4 File Offset: 0x00055FC4
		internal void ᜂ(object A_0, int A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_38:
				if (this.m_fetchedRecord == null)
				{
					return;
				}
				num = 2;
				break;
			default:
				if (false)
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
					return;
				case 2:
					if (true)
					{
					}
					this.m_fetchedRecord(A_0, A_1);
					num = 0;
					continue;
				}
				break;
			}
			goto IL_38;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00057044 File Offset: 0x00056044
		internal void ᜁ(object A_0, EventArgs A_1)
		{
			int a_ = 11;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_61;
				case 1:
					this.m_beginExport(A_0, A_1);
					num = 0;
					continue;
				case 3:
					goto IL_6B;
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_93;
					}
					break;
				}
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				IL_6B:
				if (this.m_beginExport == null)
				{
					break;
				}
				num = 1;
			}
			IL_61:
			return;
			IL_93:
			if (false)
			{
			}
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⨦⌨渪唬弮帰䄲䄴甶堸䠺堼Ծ筀ᅂ⑄⹆㩈⹊ཌ⩎㙐㩒㭔ቖ⅘⭚㉜ⵞᕠ佢፤٦᭨兪࡬", a_));
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00057100 File Offset: 0x00056100
		internal void ᜀ(object A_0, EventArgs A_1)
		{
			int a_ = 4;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_61;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_93;
					}
					break;
				case 3:
					goto IL_6B;
				case 4:
					this.m_endExport(A_0, A_1);
					num = 0;
					continue;
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 3;
				continue;
				IL_6B:
				if (this.m_endExport == null)
				{
					break;
				}
				num = 4;
			}
			IL_61:
			return;
			IL_93:
			if (false)
			{
			}
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("ⴟ⠡愣帥堧䔩師娭爯匱䜳匵ȷ9渻弽⤿ㅁ⅃ͅ♇⹉ो㙍⁏㵑♓≕瑗ⱙ㵛ⱝ婟ݡ", a_));
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000571BC File Offset: 0x000561BC
		internal void ᜃ(object A_0, int A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_38:
				if (this.m_skippedRecord == null)
				{
					goto IL_6B;
				}
				num = 0;
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
				switch (num)
				{
				case 0:
					this.m_skippedRecord(A_0, A_1);
					num = 1;
					continue;
				case 1:
					goto IL_5F;
				}
				break;
			}
			goto IL_38;
			IL_5F:
			IL_6B:
			if (true)
			{
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0005723C File Offset: 0x0005623C
		internal void ᜄ(object A_0, int A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_40:
				if (this.m_exportedRecord == null)
				{
					return;
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (true)
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
					return;
				case 2:
					this.m_exportedRecord(A_0, A_1);
					num = 1;
					continue;
				}
				break;
			}
			goto IL_40;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000572BC File Offset: 0x000562BC
		internal void ᜀ(object A_0, StopExportEventArgs A_1)
		{
			int a_ = 18;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_63;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						goto IL_93;
					}
					break;
				case 3:
					this.m_stop(A_0, A_1);
					num = 4;
					continue;
				case 4:
					goto IL_59;
				}
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
				IL_63:
				if (this.m_stop == null)
				{
					break;
				}
				num = 3;
			}
			IL_59:
			return;
			IL_93:
			if (false)
			{
			}
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⌭㨯眱䰳䘵圷䠹䠻簽ℿㅁ⅃籅片ᡉⵋ❍⍏㝑ݓ≕㝗⩙ᥛ♝ၟൡᙣብ䑧ᱩ൫ᱭ䩯᝱", a_));
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00057378 File Offset: 0x00056378
		internal void ᜀ(object A_0, TextEventArgs A_1)
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_59;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						goto IL_8B;
					}
					break;
				case 3:
					goto IL_63;
				case 4:
					this.m_getText(A_0, A_1);
					num = 0;
					continue;
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 3;
				continue;
				IL_63:
				if (this.m_getText == null)
				{
					break;
				}
				num = 4;
			}
			IL_59:
			return;
			IL_8B:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⠤⴦氨匪崬䀮䌰䜲眴嘶䨸帺ܼԾፀ≂ⱄ㑆ⱈొ⡌㭎ᑐ⭒╔㡖⭘⽚ड़㩞ᥠᝢ䥤ᅦࡨᥪ坬੮", a_));
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00057434 File Offset: 0x00056434
		internal void ᜀ(object A_0, ExportRowEventArgs A_1)
		{
			int a_ = 12;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_93;
					}
					break;
				case 2:
					this.m_beforeExportRow(A_0, A_1);
					num = 4;
					continue;
				case 4:
					goto IL_61;
				}
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				IL_6B:
				if (this.m_beforeExportRow == null)
				{
					break;
				}
				num = 2;
			}
			IL_61:
			return;
			IL_93:
			if (false)
			{
			}
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029椫嘭䀯崱䘳䈵稷嬹伻嬽稿硁ᙃ❅ⅇ㥉⥋్㕏㑑㭓⑕㵗Ὑ⑛⹝ཟၡၣ㑥ݧᵩ䁫ᡭᅯq乳፵", a_));
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060008C0 RID: 2240 RVA: 0x000574F0 File Offset: 0x000564F0
		// (remove) Token: 0x060008C1 RID: 2241 RVA: 0x00057554 File Offset: 0x00056554
		[Description("Occur when the parameters of the record are recieved.")]
		protected event CellParamsEventHandler GetCellParams
		{
			add
			{
				while (this.m_getCellParams == null)
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
						this.m_getCellParams = value;
						return;
					}
				}
				if (true)
				{
				}
				this.m_getCellParams = (CellParamsEventHandler)Delegate.Combine(this.m_getCellParams, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_38:
					if (this.m_getCellParams == null)
					{
						return;
					}
					num = 2;
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
						return;
					case 2:
						if (true)
						{
						}
						this.m_getCellParams = (CellParamsEventHandler)Delegate.Remove(this.m_getCellParams, value);
						num = 1;
						continue;
					}
					break;
				}
				goto IL_38;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060008C2 RID: 2242 RVA: 0x000575E0 File Offset: 0x000565E0
		// (remove) Token: 0x060008C3 RID: 2243 RVA: 0x00057644 File Offset: 0x00056644
		[Description("Occur when calculate the width of every column if AutoFitColWidth property equals true.")]
		protected event DataRowEventHandler FetchedRecord
		{
			add
			{
				while (this.m_fetchedRecord == null)
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
						this.m_fetchedRecord = value;
						return;
					}
				}
				if (true)
				{
				}
				this.m_fetchedRecord = (DataRowEventHandler)Delegate.Combine(this.m_fetchedRecord, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_38:
					if (this.m_fetchedRecord == null)
					{
						return;
					}
					num = 2;
					break;
				default:
					if (false)
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
						return;
					case 2:
						if (true)
						{
						}
						this.m_fetchedRecord = (DataRowEventHandler)Delegate.Remove(this.m_fetchedRecord, value);
						num = 0;
						continue;
					}
					break;
				}
				goto IL_38;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060008C4 RID: 2244 RVA: 0x000576D0 File Offset: 0x000566D0
		// (remove) Token: 0x060008C5 RID: 2245 RVA: 0x00057734 File Offset: 0x00056734
		[Description("Occur when data export before begining.")]
		public event EventHandler BeginExport
		{
			add
			{
				while (this.m_beginExport == null)
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
						this.m_beginExport = value;
						return;
					}
				}
				this.m_beginExport = (EventHandler)Delegate.Combine(this.m_beginExport, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_40:
					if (this.m_beginExport == null)
					{
						return;
					}
					num = 0;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_beginExport = (EventHandler)Delegate.Remove(this.m_beginExport, value);
						num = 1;
						continue;
					case 1:
						return;
					}
					break;
				}
				goto IL_40;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060008C6 RID: 2246 RVA: 0x000577C0 File Offset: 0x000567C0
		// (remove) Token: 0x060008C7 RID: 2247 RVA: 0x00057824 File Offset: 0x00056824
		[Description("Occur when data export after ending.")]
		public event EventHandler EndExport
		{
			add
			{
				while (this.m_endExport == null)
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
						this.m_endExport = value;
						return;
					}
				}
				this.m_endExport = (EventHandler)Delegate.Combine(this.m_endExport, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_38:
					if (true)
					{
					}
					if (this.m_endExport == null)
					{
						return;
					}
					num = 0;
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
					switch (num)
					{
					case 0:
						this.m_endExport = (EventHandler)Delegate.Remove(this.m_endExport, value);
						num = 1;
						continue;
					case 1:
						return;
					}
					break;
				}
				goto IL_38;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060008C8 RID: 2248 RVA: 0x000578B0 File Offset: 0x000568B0
		// (remove) Token: 0x060008C9 RID: 2249 RVA: 0x00057914 File Offset: 0x00056914
		[Description("Occur when source records is skipped.")]
		public event DataRowEventHandler SkippedRecord
		{
			add
			{
				while (this.m_skippedRecord == null)
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
						if (false)
						{
						}
						this.m_skippedRecord = value;
						return;
					}
				}
				this.m_skippedRecord = (DataRowEventHandler)Delegate.Combine(this.m_skippedRecord, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_40:
					if (this.m_skippedRecord == null)
					{
						return;
					}
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 1;
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
						return;
					case 2:
						this.m_skippedRecord = (DataRowEventHandler)Delegate.Remove(this.m_skippedRecord, value);
						num = 0;
						continue;
					}
					break;
				}
				goto IL_40;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060008CA RID: 2250 RVA: 0x000579A0 File Offset: 0x000569A0
		// (remove) Token: 0x060008CB RID: 2251 RVA: 0x00057A04 File Offset: 0x00056A04
		[Description("Occur when after the export of each source record.")]
		public event DataRowEventHandler ExportedRecord
		{
			add
			{
				while (this.m_exportedRecord == null)
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
						this.m_exportedRecord = value;
						return;
					}
				}
				this.m_exportedRecord = (DataRowEventHandler)Delegate.Combine(this.m_exportedRecord, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_38:
					if (this.m_exportedRecord == null)
					{
						return;
					}
					num = 0;
					break;
				default:
					if (false)
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
						if (true)
						{
						}
						this.m_exportedRecord = (DataRowEventHandler)Delegate.Remove(this.m_exportedRecord, value);
						num = 2;
						continue;
					case 2:
						return;
					}
					break;
				}
				goto IL_38;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060008CC RID: 2252 RVA: 0x00057A90 File Offset: 0x00056A90
		// (remove) Token: 0x060008CD RID: 2253 RVA: 0x00057AF4 File Offset: 0x00056AF4
		[Description("Occur when Stop methed calling. ")]
		public event StopExportEventHandler StopExport
		{
			add
			{
				while (this.m_stop == null)
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
						this.m_stop = value;
						return;
					}
				}
				this.m_stop = (StopExportEventHandler)Delegate.Combine(this.m_stop, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_40:
					if (this.m_stop == null)
					{
						return;
					}
					num = 2;
					break;
				default:
					if (true)
					{
					}
					if (false)
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
						return;
					case 2:
						this.m_stop = (StopExportEventHandler)Delegate.Remove(this.m_stop, value);
						num = 0;
						continue;
					}
					break;
				}
				goto IL_40;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060008CE RID: 2254 RVA: 0x00057B80 File Offset: 0x00056B80
		// (remove) Token: 0x060008CF RID: 2255 RVA: 0x00057BE4 File Offset: 0x00056BE4
		[Description("Occur when get export of source string.")]
		public event TextEventHandler GetExportText
		{
			add
			{
				while (this.m_getText == null)
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
						if (false)
						{
						}
						this.m_getText = value;
						return;
					}
				}
				this.m_getText = (TextEventHandler)Delegate.Combine(this.m_getText, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_38:
					if (this.m_getText == null)
					{
						return;
					}
					num = 1;
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
					switch (num)
					{
					case 0:
						goto IL_69;
					case 1:
						this.m_getText = (TextEventHandler)Delegate.Remove(this.m_getText, value);
						num = 0;
						continue;
					}
					break;
				}
				goto IL_38;
				IL_69:
				if (true)
				{
				}
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060008D0 RID: 2256 RVA: 0x00057C70 File Offset: 0x00056C70
		// (remove) Token: 0x060008D1 RID: 2257 RVA: 0x00057CD4 File Offset: 0x00056CD4
		[Description("Occur before the export of each source record.")]
		public event ExportRowEventHandler BeforeExportRow
		{
			add
			{
				while (this.m_beforeExportRow == null)
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
						this.m_beforeExportRow = value;
						return;
					}
				}
				this.m_beforeExportRow = (ExportRowEventHandler)Delegate.Combine(this.m_beforeExportRow, value);
			}
			remove
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_40:
					if (this.m_beforeExportRow == null)
					{
						return;
					}
					num = 0;
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
						this.m_beforeExportRow = (ExportRowEventHandler)Delegate.Remove(this.m_beforeExportRow, value);
						num = 2;
						continue;
					case 2:
						return;
					}
					break;
				}
				goto IL_40;
			}
		}

		// Token: 0x040006A1 RID: 1697
		private bool \u25D9\u0085\u009C\u0095;

		// Token: 0x040006A2 RID: 1698
		private Container ᜀ;

		// Token: 0x040006A3 RID: 1699
		internal bool ᜁ;

		// Token: 0x040006A4 RID: 1700
		private bool[] \u2609\u00AC\u0092\u009E;

		// Token: 0x040006A5 RID: 1701
		private int ᜂ;

		// Token: 0x040006A6 RID: 1702
		private ColumnsExport ᜃ;

		// Token: 0x040006A7 RID: 1703
		private RowExport ᜄ;

		// Token: 0x040006A8 RID: 1704
		private IDbCommand ᜅ;

		// Token: 0x040006A9 RID: 1705
		private DataTable ᜆ;

		// Token: 0x040006AA RID: 1706
		private string \u2593\u0092\u008B\u0093;

		// Token: 0x040006AB RID: 1707
		private ListView ᜇ;

		// Token: 0x040006AC RID: 1708
		private string ᜈ = string.Empty;

		// Token: 0x040006AD RID: 1709
		private StringListCollection ᜉ = new StringListCollection();

		// Token: 0x040006AE RID: 1710
		private StringListCollection ᜊ = new StringListCollection();

		// Token: 0x040006AF RID: 1711
		private StringListCollection ᜋ = new StringListCollection();

		// Token: 0x040006B0 RID: 1712
		private StringListCollection ᜌ = new StringListCollection();

		// Token: 0x040006B1 RID: 1713
		private float[] \u2593\u008C\u0099\u0095;

		// Token: 0x040006B2 RID: 1714
		private ExportSource \u170D;

		// Token: 0x040006B3 RID: 1715
		private StringListCollection ᜎ = new StringListCollection();

		// Token: 0x040006B4 RID: 1716
		private Options ᜏ;

		// Token: 0x040006B5 RID: 1717
		private StringListCollection ᜐ = new StringListCollection();

		// Token: 0x040006B6 RID: 1718
		private bool ᜑ = true;

		// Token: 0x040006B7 RID: 1719
		private bool \u1712;

		// Token: 0x040006B8 RID: 1720
		private int \u1713 = 40;

		// Token: 0x040006B9 RID: 1721
		private bool \u1714;

		// Token: 0x040006BA RID: 1722
		private StringListCollection \u1715 = new StringListCollection();

		// Token: 0x040006BB RID: 1723
		private StringListCollection \u1716 = new StringListCollection();

		// Token: 0x040006BC RID: 1724
		private StringListCollection \u1717 = new StringListCollection();

		// Token: 0x040006BD RID: 1725
		private FormatsExport \u1718;

		// Token: 0x040006BE RID: 1726
		private bool \u2609\u008B\u0081\u00A4;

		// Token: 0x040006BF RID: 1727
		private StringListCollection \u1719 = new StringListCollection();

		// Token: 0x040006C0 RID: 1728
		private int \u171A;

		// Token: 0x040006C1 RID: 1729
		private int \u171B;

		// Token: 0x040006C2 RID: 1730
		internal spr\u1BFE \u171C;

		// Token: 0x040006C3 RID: 1731
		internal bool \u171D;

		// Token: 0x040006C4 RID: 1732
		private CultureInfo \u171E = CultureInfo.CurrentCulture;

		// Token: 0x040006C5 RID: 1733
		private bool \u171F;

		// Token: 0x040006C6 RID: 1734
		private string[] \u25D8\u0089\u009B\u008D;

		// Token: 0x040006C7 RID: 1735
		protected EncodingType m_encodingType = EncodingType.UTF8;

		// Token: 0x040006C8 RID: 1736
		protected Encoding m_currEncoding = Encoding.UTF8;

		// Token: 0x040006C9 RID: 1737
		protected EventHandler m_beginExport;

		// Token: 0x040006CA RID: 1738
		protected EventHandler m_endExport;

		// Token: 0x040006CB RID: 1739
		protected DataRowEventHandler m_skippedRecord;

		// Token: 0x040006CC RID: 1740
		protected DataRowEventHandler m_exportedRecord;

		// Token: 0x040006CD RID: 1741
		protected StopExportEventHandler m_stop;

		// Token: 0x040006CE RID: 1742
		protected TextEventHandler m_getText;

		// Token: 0x040006CF RID: 1743
		protected ExportRowEventHandler m_beforeExportRow;

		// Token: 0x040006D0 RID: 1744
		protected CellParamsEventHandler m_getCellParams;

		// Token: 0x040006D1 RID: 1745
		protected DataRowEventHandler m_fetchedRecord;

		// Token: 0x040006D2 RID: 1746
		protected bool m_exportIfEmpty = true;

		// Token: 0x040006D3 RID: 1747
		protected string m_tempFileName = string.Empty;

		// Token: 0x040006D4 RID: 1748
		private IComponentChangeService ᜠ;

		// Token: 0x040006D5 RID: 1749
		private bool ᜡ = true;

		// Token: 0x040006D6 RID: 1750
		[CompilerGenerated]
		private bool ᜢ;

		// Token: 0x040006D7 RID: 1751
		[CompilerGenerated]
		private DataTable ᜣ;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.Access
{
	// Token: 0x020001EB RID: 491
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class AccessExport : DatabaseExport
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000EE2 RID: 3810 RVA: 0x000A4260 File Offset: 0x000A3260
		// (remove) Token: 0x06000EE3 RID: 3811 RVA: 0x000A42F8 File Offset: 0x000A32F8
		public event TableColumnCreatingEventHandler TableColumnCreating
		{
			add
			{
				for (;;)
				{
					IL_14:
					int num;
					TableColumnCreatingEventHandler tableColumnCreatingEventHandler;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_66:
						num = 0;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						tableColumnCreatingEventHandler = this.ᜆ;
						num = 1;
						break;
					}
					TableColumnCreatingEventHandler tableColumnCreatingEventHandler2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (tableColumnCreatingEventHandler == tableColumnCreatingEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_49;
						case 1:
							goto IL_47;
						case 2:
							return;
						}
						goto IL_14;
					}
					IL_49:
					tableColumnCreatingEventHandler2 = tableColumnCreatingEventHandler;
					TableColumnCreatingEventHandler value2 = (TableColumnCreatingEventHandler)Delegate.Combine(tableColumnCreatingEventHandler2, value);
					tableColumnCreatingEventHandler = Interlocked.CompareExchange<TableColumnCreatingEventHandler>(ref this.ᜆ, value2, tableColumnCreatingEventHandler2);
					goto IL_66;
					IL_47:
					goto IL_49;
				}
			}
			remove
			{
				for (;;)
				{
					IL_14:
					int num;
					TableColumnCreatingEventHandler tableColumnCreatingEventHandler;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_66:
						num = 0;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						tableColumnCreatingEventHandler = this.ᜆ;
						num = 2;
						break;
					}
					TableColumnCreatingEventHandler tableColumnCreatingEventHandler2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (tableColumnCreatingEventHandler == tableColumnCreatingEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_49;
						case 1:
							return;
						case 2:
							goto IL_47;
						}
						goto IL_14;
					}
					IL_49:
					tableColumnCreatingEventHandler2 = tableColumnCreatingEventHandler;
					TableColumnCreatingEventHandler value2 = (TableColumnCreatingEventHandler)Delegate.Remove(tableColumnCreatingEventHandler2, value);
					tableColumnCreatingEventHandler = Interlocked.CompareExchange<TableColumnCreatingEventHandler>(ref this.ᜆ, value2, tableColumnCreatingEventHandler2);
					goto IL_66;
					IL_47:
					goto IL_49;
				}
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000EE4 RID: 3812 RVA: 0x000A4390 File Offset: 0x000A3390
		// (remove) Token: 0x06000EE5 RID: 3813 RVA: 0x000A4428 File Offset: 0x000A3428
		public event TableCreatedEventHandler TableCreated
		{
			add
			{
				for (;;)
				{
					IL_14:
					int num;
					TableCreatedEventHandler tableCreatedEventHandler;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5E:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						tableCreatedEventHandler = this.ᜇ;
						num = 2;
						break;
					}
					TableCreatedEventHandler tableCreatedEventHandler2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (tableCreatedEventHandler == tableCreatedEventHandler2)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_41;
						case 1:
							return;
						case 2:
							goto IL_3F;
						}
						goto IL_14;
					}
					IL_41:
					tableCreatedEventHandler2 = tableCreatedEventHandler;
					TableCreatedEventHandler value2 = (TableCreatedEventHandler)Delegate.Combine(tableCreatedEventHandler2, value);
					tableCreatedEventHandler = Interlocked.CompareExchange<TableCreatedEventHandler>(ref this.ᜇ, value2, tableCreatedEventHandler2);
					goto IL_5E;
					IL_3F:
					goto IL_41;
				}
			}
			remove
			{
				for (;;)
				{
					IL_14:
					int num;
					TableCreatedEventHandler tableCreatedEventHandler;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5E:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						tableCreatedEventHandler = this.ᜇ;
						num = 2;
						break;
					}
					TableCreatedEventHandler tableCreatedEventHandler2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (tableCreatedEventHandler == tableCreatedEventHandler2)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_41;
						case 1:
							return;
						case 2:
							goto IL_3F;
						}
						goto IL_14;
					}
					IL_41:
					tableCreatedEventHandler2 = tableCreatedEventHandler;
					TableCreatedEventHandler value2 = (TableCreatedEventHandler)Delegate.Remove(tableCreatedEventHandler2, value);
					tableCreatedEventHandler = Interlocked.CompareExchange<TableCreatedEventHandler>(ref this.ᜇ, value2, tableCreatedEventHandler2);
					goto IL_5E;
					IL_3F:
					goto IL_41;
				}
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000A44C0 File Offset: 0x000A34C0
		protected override void InitializeVariables()
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜂ);
			base.InitializeVariables();
			this.TableName = HyperlinksCollectionEditor.b("栬圮䄰尲䜴䌶欸帺丼䨾ⵀ㝂", a_);
			this.CreateTable = true;
			this.CreateDatabase = true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000A4548 File Offset: 0x000A3548
		protected override void Dispose(bool disposing)
		{
			try
			{
				int num = 3;
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
						case 0:
							goto IL_72;
						case 1:
							goto IL_56;
						case 2:
							goto IL_7A;
						}
						if (this.ᜂ != null)
						{
							num = 1;
							continue;
						}
						IL_72:
						num = 2;
						continue;
					}
					IL_56:
					this.ᜂ.Dispose();
					this.ᜂ = null;
					num = 0;
				}
				IL_7A:;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000A45F4 File Offset: 0x000A35F4
		public override void SaveToFile()
		{
			for (;;)
			{
				IL_1C:
				spr\u2561.ᜀ = this.ᜁ;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_50;
					case 1:
						AboutDataExport.ShowAbout(false);
						num = 0;
						continue;
					case 2:
						if (Environment.UserInteractive)
						{
							num = 1;
							continue;
						}
						goto IL_9F;
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C;
						}
						if (false)
						{
						}
						num = 2;
						continue;
					case 4:
						if (this.ᜁ)
						{
							num = 3;
							continue;
						}
						goto IL_9F;
					}
					break;
				}
			}
			IL_50:
			IL_9F:
			base.SaveToFile();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000A46A8 File Offset: 0x000A36A8
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

		// Token: 0x06000EEA RID: 3818 RVA: 0x000A46F8 File Offset: 0x000A36F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 0;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("紛渝借両䴣䔥䤧帩䔫䄭帯ᴱ夳刵娷", a_), response, saveType);
			}
			finally
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
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000A47BC File Offset: 0x000A37BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 9;
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
				spr\u2561.ᜀ = this.ᜁ;
				MemoryStream memoryStream = new MemoryStream();
				try
				{
					base.SaveToStream(memoryStream);
					base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䐤圦夨䜪䐬䰮倰䜲尴堶圸ᐺ值嬾⍀", a_), response, SaveType.Attachment);
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A2;
						case 2:
							((IDisposable)memoryStream).Dispose();
							num = 0;
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
				break;
			}
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000A4880 File Offset: 0x000A3880
		[EditorBrowsable(EditorBrowsableState.Never)]
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
			this.SaveToHttpResponse(this.DatabaseName, response);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000A48D4 File Offset: 0x000A38D4
		internal new spr\u20EB ᜀ()
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
			return base.ᜀ() as spr\u20EB;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000A491C File Offset: 0x000A391C
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
			return typeof(spr\u20EB);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000A4964 File Offset: 0x000A3964
		protected override void BeginDataExport()
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder2;
				StringBuilder stringBuilder3;
				ParameterData[] array;
				for (;;)
				{
					base.BeginDataExport();
					StringBuilder stringBuilder = new StringBuilder(base.ColumnsExport.Count);
					stringBuilder2 = new StringBuilder(base.ColumnsExport.Count);
					stringBuilder3 = new StringBuilder(base.ColumnsExport.Count);
					object a_2 = null;
					DataTable a_3 = null;
					ExportSource dataSource = base.DataSource;
					int num = 50;
					for (;;)
					{
						string text;
						TableColumnCreatingEventArgs tableColumnCreatingEventArgs;
						int num2;
						TableCreatedEventArgs e;
						int num3;
						string a_4;
						string text2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_43A;
							default:
							{
								if (false)
								{
								}
								this.GetTempFileName();
								this.ᜄ = this.m_tempFileName;
								this.ᜀ().ᜀ(this.m_tempFileName, true);
								string format;
								this.ᜀ().ᜀ(string.Format(format, this.m_tempFileName), this.TableName, string.Format(HyperlinksCollectionEditor.b("䌟倡䄣䜥尧伩ఫ娭儯倱堳匵ᠷ愹䜻฽㴿ὁ摃湅㍇等ㅋ杍", a_), this.TableName, stringBuilder.ToString()));
								num = 38;
								continue;
							}
							}
							break;
						case 1:
							goto IL_795;
						case 2:
							goto IL_35B;
						case 3:
							text = string.Format(HyperlinksCollectionEditor.b("嬟ሡ夣إ匧ᬩ儫", a_), text, tableColumnCreatingEventArgs.ColumnPropertiesDDL);
							if (true)
							{
							}
							num = 4;
							continue;
						case 4:
							goto IL_A4D;
						case 5:
							goto IL_9F4;
						case 6:
							goto IL_9C5;
						case 7:
							goto IL_9C5;
						case 8:
							this.ᜀ().ᜀ(this.DatabaseName, false);
							num = 21;
							continue;
						case 9:
							num = 41;
							continue;
						case 10:
							if (!string.IsNullOrEmpty(tableColumnCreatingEventArgs.ColumnPropertiesDDL))
							{
								num = 3;
								continue;
							}
							goto IL_A4D;
						case 11:
							num = 61;
							continue;
						case 12:
							goto IL_795;
						case 13:
							if (!base.ColumnsExport[num2].IsBlob)
							{
								num = 11;
								continue;
							}
							goto IL_6A5;
						case 14:
							num = 16;
							continue;
						case 15:
							if (base.ColumnsExport[num2].Length > 255)
							{
								num = 59;
								continue;
							}
							goto IL_635;
						case 16:
							goto IL_9F4;
						case 17:
							goto IL_9F4;
						case 18:
							if (num2 > 0)
							{
								num = 44;
								continue;
							}
							goto IL_5E9;
						case 19:
							goto IL_635;
						case 20:
							goto IL_9F4;
						case 21:
							goto IL_392;
						case 22:
							if (this.ᜆ != null)
							{
								num = 45;
								continue;
							}
							goto IL_35B;
						case 23:
							this.ᜇ(this, e);
							num = 39;
							continue;
						case 24:
							if (this.ᜇ != null)
							{
								num = 23;
								continue;
							}
							goto IL_B41;
						case 25:
							goto IL_795;
						case 26:
							goto IL_795;
						case 27:
							goto IL_230;
						case 28:
						{
							string format = HyperlinksCollectionEditor.b("瀟倡䬣倥䄧丩䤫尭യ缱崳唵䨷唹伻儽☿㙁橃౅ⵇ㹉手ōᱏᝑၓᑕ癗湙牛湝孟♡գብ१䩩㽫ŭկqᝳ፵䕷Ź䱻ͽ", a_) + string.Format(HyperlinksCollectionEditor.b("ᬟ校䄣別ࠧ攩怫欭琯瀱ำ爵夷丹崻尽ℿㅁ⅃晅ᡇ⭉㽋㵍❏㵑♓㉕敗⅙汛⍝", a_), this.ᜃ);
							num = 27;
							continue;
						}
						case 29:
						{
							string format;
							this.ᜀ().ᜀ(string.Format(format, this.DatabaseName), this.TableName, string.Format(HyperlinksCollectionEditor.b("䌟倡䄣䜥尧伩ఫ娭儯倱堳匵ᠷ愹䜻฽㴿ὁ摃湅㍇等ㅋ杍", a_), this.TableName, stringBuilder.ToString()));
							num = 32;
							continue;
						}
						case 30:
							goto IL_795;
						case 31:
							if (this.\u171D)
							{
								num = 0;
								continue;
							}
							this.ᜄ = this.DatabaseName;
							num = 40;
							continue;
						case 32:
							goto IL_91C;
						case 33:
							if (base.DataSource == ExportSource.SqlCommand)
							{
								num = 34;
								continue;
							}
							num3 = base.ColumnsExport[num2].Length;
							num = 36;
							continue;
						case 34:
							num3 = (int)base.ColumnsExport[num2].Size;
							num = 35;
							continue;
						case 35:
							goto IL_98C;
						case 36:
							goto IL_98C;
						case 37:
							if (this.CreateTable)
							{
								num = 29;
								continue;
							}
							goto IL_91C;
						case 38:
							goto IL_91C;
						case 39:
							goto IL_470;
						case 40:
							goto IL_43A;
						case 41:
							a_4 = HyperlinksCollectionEditor.b("䰟䴡䨣䄥尧伩含娭", a_);
							array[num2].Type = OleDbType.VarBinary;
							num = 47;
							continue;
						case 42:
						{
							if (num2 >= base.ColumnsExport.Count)
							{
								num = 52;
								continue;
							}
							text2 = string.Format(HyperlinksCollectionEditor.b("笟夡ᐣ嬥甧", a_), base.ColumnsExport[num2].Name);
							a_4 = string.Empty;
							array[num2].Name = base.ColumnsExport[num2].Name;
							array[num2].Size = 0;
							array[num2].ColumnName = base.ColumnsExport[num2].Name;
							ColExportType colExportType = base.ColumnsExport[num2].ColExportType;
							num = 49;
							continue;
						}
						case 43:
						{
							if (this.ᜃ.Length > 0)
							{
								num = 28;
								continue;
							}
							string format = HyperlinksCollectionEditor.b("瀟倡䬣倥䄧丩䤫尭യ缱崳唵䨷唹伻儽☿㙁橃౅ⵇ㹉手ōᱏᝑၓᑕ癗湙牛湝孟♡գብ१䩩㽫ŭկqᝳ፵䕷Ź䱻ͽ", a_);
							num = 55;
							continue;
						}
						case 44:
							stringBuilder.Append(',');
							stringBuilder2.Append(',');
							stringBuilder3.Append(',');
							num = 46;
							continue;
						case 45:
							this.ᜆ(this, tableColumnCreatingEventArgs);
							num = 2;
							continue;
						case 46:
							goto IL_5E9;
						case 47:
							goto IL_795;
						case 48:
							goto IL_795;
						case 49:
						{
							ColExportType colExportType;
							switch (colExportType)
							{
							case ColExportType.Integer:
								a_4 = HyperlinksCollectionEditor.b("䤟䰡倣䌥伧伩師", a_);
								array[num2].Type = OleDbType.Integer;
								num = 51;
								continue;
							case ColExportType.Bigint:
								a_4 = HyperlinksCollectionEditor.b("䤟䰡倣䌥伧伩師", a_);
								array[num2].Type = OleDbType.BigInt;
								num = 56;
								continue;
							case ColExportType.Float:
								a_4 = HyperlinksCollectionEditor.b("䐟䴡儣䐥䐧伩", a_);
								array[num2].Type = OleDbType.Double;
								num = 12;
								continue;
							case ColExportType.Currency:
								a_4 = HyperlinksCollectionEditor.b("䌟圡嘣吥䴧䐩伫圭", a_);
								array[num2].Type = OleDbType.Currency;
								num = 26;
								continue;
							case ColExportType.DateTime:
							case ColExportType.Time:
								a_4 = HyperlinksCollectionEditor.b("䐟䌡倣䌥尧䌩䄫䬭", a_);
								array[num2].Type = OleDbType.DBTimeStamp;
								num = 54;
								continue;
							case ColExportType.String:
							case ColExportType.Guid:
								num = 13;
								continue;
							case ColExportType.Boolean:
								a_4 = HyperlinksCollectionEditor.b("䰟䴡䌣伥䬧䬩䀫", a_);
								array[num2].Type = OleDbType.Boolean;
								num = 25;
								continue;
							case ColExportType.Binary:
								num3 = 0;
								num = 33;
								continue;
							default:
								num = 9;
								continue;
							}
							break;
						}
						case 50:
							switch (dataSource)
							{
							case ExportSource.SqlCommand:
								a_2 = base.SQLCommand;
								a_3 = base.SQLCommandSchema;
								num = 20;
								continue;
							case ExportSource.DataTable:
								a_2 = base.DataTable;
								num = 5;
								continue;
							case ExportSource.ListView:
								a_2 = base.ListView;
								num = 17;
								continue;
							default:
								num = 14;
								continue;
							}
							break;
						case 51:
							goto IL_795;
						case 52:
						{
							string format = string.Empty;
							num = 43;
							continue;
						}
						case 53:
							a_4 = string.Format(HyperlinksCollectionEditor.b("瘟挡瘣搥愧搩洫簭椯ᨱ伳ص䔷ጹ", a_), num3);
							array[num2].Type = OleDbType.VarBinary;
							num = 48;
							continue;
						case 54:
							goto IL_795;
						case 55:
							goto IL_230;
						case 56:
							goto IL_795;
						case 57:
							goto IL_6A5;
						case 58:
							if (num3 < 511)
							{
								num = 53;
								continue;
							}
							a_4 = HyperlinksCollectionEditor.b("氟洡樣愥樧挩戫漭戯欱", a_);
							array[num2].Type = OleDbType.LongVarBinary;
							num = 1;
							continue;
						case 59:
							base.ColumnsExport[num2].Length = 255;
							num = 19;
							continue;
						case 60:
							goto IL_795;
						case 61:
							if (base.ColumnsExport[num2].IsMemo)
							{
								num = 57;
								continue;
							}
							num = 15;
							continue;
						}
						break;
						IL_230:
						num = 31;
						continue;
						IL_35B:
						text = tableColumnCreatingEventArgs.ColumnDataType;
						num = 10;
						continue;
						IL_392:
						num = 37;
						continue;
						IL_43A:
						if (this.CreateDatabase)
						{
							num = 8;
							continue;
						}
						goto IL_392;
						IL_5E9:
						tableColumnCreatingEventArgs = new TableColumnCreatingEventArgs(base.ColumnsExport[num2].Name, a_4, base.DataSource, a_2, a_3);
						num = 22;
						continue;
						IL_635:
						a_4 = string.Format(HyperlinksCollectionEditor.b("嘟䌡嘣䔥䀧䬩師ح䬯ȱ䤳ἵ", a_), base.ColumnsExport[num2].Length);
						array[num2].Size = base.ColumnsExport[num2].Length;
						array[num2].Type = OleDbType.VarWChar;
						num = 30;
						continue;
						IL_6A5:
						a_4 = HyperlinksCollectionEditor.b("䴟䜡䤣䤥", a_);
						array[num2].Type = OleDbType.VarWChar;
						array[num2].Size = int.MaxValue;
						num = 60;
						continue;
						IL_795:
						num = 18;
						continue;
						IL_91C:
						List<TableColumn> list;
						e = new TableCreatedEventArgs(this.TableName, list, base.DataSource, a_2, a_3);
						num = 24;
						continue;
						IL_98C:
						array[num2].Size = num3;
						num = 58;
						continue;
						IL_9C5:
						num = 42;
						continue;
						IL_9F4:
						array = new ParameterData[base.ColumnsExport.Count];
						list = new List<TableColumn>();
						num2 = 0;
						num = 6;
						continue;
						IL_A4D:
						list.Add(new TableColumn(tableColumnCreatingEventArgs.ColumnName, tableColumnCreatingEventArgs.ColumnDataType, tableColumnCreatingEventArgs.ColumnPropertiesDDL));
						stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("嬟ሡ夣إ匧ᬩ儫", a_), text2, text);
						stringBuilder2.Append(text2);
						stringBuilder3.Append('?');
						num2++;
						num = 7;
					}
				}
				IL_470:
				IL_B41:
				this.ᜅ = true;
				this.ᜀ().ᜀ(this.ᜄ, string.Format(HyperlinksCollectionEditor.b("䤟䰡圣䌥娧帩ఫ䜭帯䘱嬳ᘵ挷䄹఻䌽ᴿ橁㽃睅㕇捉汋㡍ㅏ㹑⅓㍕⭗牙❛汝ᵟ䭡", a_), this.TableName, stringBuilder2.ToString(), stringBuilder3.ToString()), array);
				return;
			}
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000A54F4 File Offset: 0x000A44F4
		public void AlterTable(IEnumerable<string> sqls)
		{
			int a_ = 9;
			while (this.ᜅ)
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
					throw new InvalidOperationException(HyperlinksCollectionEditor.b("昤䠦尨䜪䤬༮弰尲䄴᜶堸场䤼娾㍀捂ㅄ⽆ⱈ歊㥌⹎㍐㽒ご睖㵘⹚⽜㙞འѢ䕤ͦࡨὪ౬佮ᑰ୲մᡶ୸ེᑼᅾ궂", a_));
				}
			}
			this.ᜀ().ᜀ(this.ᜄ, sqls);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000A5568 File Offset: 0x000A4568
		protected override void WriteRow()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 16;
					for (;;)
					{
						byte[] array;
						string text;
						switch (num2)
						{
						case 0:
							num2 = 13;
							continue;
						case 1:
							goto IL_1BB;
						case 2:
							goto IL_B7;
						case 3:
						{
							ColExport colExport = base.ExportRowExport[num];
							array = null;
							int number = base.ColumnsExport[colExport.ColumnIndex].Number;
							ExportSource dataSource = base.DataSource;
							num2 = 5;
							continue;
						}
						case 4:
							text = string.Empty;
							num2 = 15;
							continue;
						case 5:
						{
							ExportSource dataSource;
							switch (dataSource)
							{
							case ExportSource.SqlCommand:
							{
								ColExport colExport;
								IDataReader dataReader = colExport.DataSource as IDataReader;
								num2 = 6;
								continue;
							}
							case ExportSource.DataTable:
							{
								ColExport colExport;
								DataRow dataRow = colExport.DataSource as DataRow;
								int number;
								array = (dataRow[number] as byte[]);
								num2 = 7;
								continue;
							}
							default:
								num2 = 0;
								continue;
							}
							break;
						}
						case 6:
						{
							int number;
							IDataReader dataReader;
							if (!dataReader.IsDBNull(number))
							{
								goto IL_2A0;
							}
							goto IL_84;
						}
						case 7:
							goto IL_84;
						case 8:
							if (base.ExportRowExport[num].IsBinary)
							{
								num2 = 3;
								continue;
							}
							text = base.ExportRowExport[num].GetExportedValue(false);
							num2 = 9;
							continue;
						case 9:
							if (string.Compare(text, this.DataFormats.NullString) == 0)
							{
								num2 = 4;
								continue;
							}
							goto IL_22F;
						case 10:
							goto IL_84;
						case 11:
							goto IL_192;
						case 12:
						{
							int number;
							IDataReader dataReader;
							int num3 = (int)dataReader.GetBytes(number, 0L, null, 0, int.MaxValue);
							array = new byte[num3];
							dataReader.GetBytes(number, 0L, array, 0, num3);
							num2 = 10;
							continue;
						}
						case 13:
							goto IL_84;
						case 14:
							goto IL_B7;
						case 15:
							goto IL_22F;
						case 16:
							goto IL_192;
						case 17:
							if (num >= base.ExportRowExport.Count)
							{
								num2 = 1;
								continue;
							}
							num2 = 8;
							continue;
						}
						break;
						IL_84:
						this.ᜀ().ᜀ(num, array);
						num2 = 2;
						continue;
						IL_B7:
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_2A0:
							num2 = 12;
							continue;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 11;
							continue;
						}
						IL_192:
						num2 = 17;
						continue;
						IL_22F:
						this.ᜀ().ᜀ(num, text);
						num2 = 14;
					}
				}
				IL_1BB:
				this.ᜀ().ᜀ().ExecuteNonQuery();
				return;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000A5838 File Offset: 0x000A4838
		protected override void EndDataExport()
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
						if (this.ᜀ().ᜀ().Connection.State == ConnectionState.Open)
						{
							num = 2;
							continue;
						}
						goto IL_63;
					case 2:
						this.ᜀ().ᜀ().Connection.Close();
						num = 3;
						continue;
					case 3:
						goto IL_63;
					case 4:
						goto IL_80;
					case 5:
						num = 0;
						continue;
					}
					if (this.ᜀ().ᜀ().Connection == null)
					{
						goto IL_DE;
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
						num = 5;
						continue;
					}
					IL_63:
					this.ᜀ().ᜀ().Connection.Dispose();
					num = 4;
				}
			}
			IL_80:
			IL_DE:
			if (true)
			{
			}
			base.EndDataExport();
			this.ᜅ = false;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000A5938 File Offset: 0x000A4938
		internal override string NormalString(string S)
		{
			while (string.Compare(S, this.DataFormats.NullString, true) == 0)
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
					return DBNull.Value.ToString();
				}
			}
			return base.NormalString(S);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x000A599C File Offset: 0x000A499C
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x000A59E0 File Offset: 0x000A49E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Description("The password string of the result MS Access file.")]
		public string Password
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
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						this.ᜃ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (!(value != this.ᜃ))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x000A5A60 File Offset: 0x000A4A60
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x000A5AA4 File Offset: 0x000A4AA4
		[Editor(typeof(AccessDatabaseNameEditor), typeof(UITypeEditor))]
		[Browsable(true)]
		[Description("The database name of the result MS Access file.")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string DatabaseName
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
				return base.DatabaseName;
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
				base.DatabaseName = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000A5AE8 File Offset: 0x000A4AE8
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x000A5B2C File Offset: 0x000A4B2C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("ExportResult")]
		[Description("The table name of the result MS Access to export data.")]
		public new string TableName
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
				return base.TableName;
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
				base.TableName = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000A5B70 File Offset: 0x000A4B70
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x000A5BB4 File Offset: 0x000A4BB4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Description("The CreateDatabase property determinates whether creates the result MS Access database automatically.")]
		public new bool CreateDatabase
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
				return base.CreateDatabase;
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
				base.CreateDatabase = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000A5BF8 File Offset: 0x000A4BF8
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000A5C3C File Offset: 0x000A4C3C
		[Description("The CreateTable property determinates whether creates the result MS Access table automatically.")]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool CreateTable
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
				return base.CreateTable;
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
				base.CreateTable = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000A5C80 File Offset: 0x000A4C80
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x000A5CC4 File Offset: 0x000A4CC4
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000A5D08 File Offset: 0x000A4D08
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000A5D4C File Offset: 0x000A4D4C
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000A5D90 File Offset: 0x000A4D90
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000A5DD4 File Offset: 0x000A4DD4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public new bool AddTitles
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000A5E18 File Offset: 0x000A4E18
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x000A5E5C File Offset: 0x000A4E5C
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				base.DataFormats = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000A5EA0 File Offset: 0x000A4EA0
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x000A5EE4 File Offset: 0x000A4EE4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Browsable(true)]
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base.CustomFormats = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000A5F28 File Offset: 0x000A4F28
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x000A5F64 File Offset: 0x000A4F64
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000A5FA0 File Offset: 0x000A4FA0
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x000A5FE4 File Offset: 0x000A4FE4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Description("Indicate whether export long char/binary column.")]
		[Browsable(true)]
		public new bool ExportLongColumn
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x000A6028 File Offset: 0x000A5028
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x000A606C File Offset: 0x000A506C
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new StringListCollection ColumnsLength
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
				return base.ColumnsLength;
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
				base.ColumnsLength = value;
			}
		}

		// Token: 0x04000B51 RID: 2897
		private new const string ᜀ = "SpireDataExportTemp/";

		// Token: 0x04000B52 RID: 2898
		private int[] \u25D8\u00AF\u00A7\u0085;

		// Token: 0x04000B53 RID: 2899
		private bool[] \u2609\u0092\u0093\u00A8;

		// Token: 0x04000B54 RID: 2900
		private new const string ᜁ = "sde";

		// Token: 0x04000B55 RID: 2901
		private new License ᜂ;

		// Token: 0x04000B56 RID: 2902
		private new string ᜃ = string.Empty;

		// Token: 0x04000B57 RID: 2903
		private string \u25D8\u009E\u0099\u009C;

		// Token: 0x04000B58 RID: 2904
		private new string ᜄ;

		// Token: 0x04000B59 RID: 2905
		private bool ᜅ;

		// Token: 0x04000B5A RID: 2906
		private TableColumnCreatingEventHandler ᜆ;

		// Token: 0x04000B5B RID: 2907
		private bool[] \u2609\u00AF\u009F\u008E;

		// Token: 0x04000B5C RID: 2908
		private bool \u2460\u009C\u008D\u009E;

		// Token: 0x04000B5D RID: 2909
		private byte \u25D9\u009E\u009F\u0089;

		// Token: 0x04000B5E RID: 2910
		private TableCreatedEventHandler ᜇ;
	}
}

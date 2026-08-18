using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AF0 RID: 2800
	internal class Workbook
	{
		// Token: 0x06006903 RID: 26883 RVA: 0x0018A1BC File Offset: 0x001883BC
		static Workbook()
		{
			XF xf = new XF(0, 0, true, 0);
			Workbook.defaultXFKey = Convert.ToBase64String(xf.GetData());
			Font font = new Font();
			Workbook.defaultFontKey = Convert.ToBase64String(font.GetData());
			Workbook.formatTable = new Hashtable(36);
			Workbook.formatTable.Add("General", 0);
			Workbook.formatTable.Add("0", 1);
			Workbook.formatTable.Add("0.00", 2);
			Workbook.formatTable.Add("#,##0", 3);
			Workbook.formatTable.Add("#,##0.00", 4);
			Workbook.formatTable.Add("\"$\"#,##0_);\\(\"$\"#,##0\\)", 5);
			Workbook.formatTable.Add("\"$\"#,##0_);[Red]\\(\"$\"#,##0\\)", 6);
			Workbook.formatTable.Add("\"$\"#,##0.00_);\\(\"$\"#,##0.00\\)", 7);
			Workbook.formatTable.Add("\"$\"#,##0.00_);[Red]\\(\"$\"#,##0.00\\)", 8);
			Workbook.formatTable.Add("0%", 9);
			Workbook.formatTable.Add("0.00%", 10);
			Workbook.formatTable.Add("0.00E+00", 11);
			Workbook.formatTable.Add("#?/?", 12);
			Workbook.formatTable.Add("#??/??", 13);
			Workbook.formatTable.Add("M/D/YY", 14);
			Workbook.formatTable.Add("D-MMM-YY", 15);
			Workbook.formatTable.Add("D-MMM", 16);
			Workbook.formatTable.Add("MMM-YY", 17);
			Workbook.formatTable.Add("h:mm AM/PM", 18);
			Workbook.formatTable.Add("h:mm:ss AM/PM", 19);
			Workbook.formatTable.Add("h:mm", 20);
			Workbook.formatTable.Add("h:mm:ss", 21);
			Workbook.formatTable.Add("M/D/YYYY h:mm", 22);
			Workbook.formatTable.Add("(#,##0_);(#,##0)", 37);
			Workbook.formatTable.Add("(#,##0_);[Red](#,##0)", 38);
			Workbook.formatTable.Add("(#,##0.00_);(#,##0.00)", 39);
			Workbook.formatTable.Add("(#,##0.00_);[Red](#,##0.00)", 40);
			Workbook.formatTable.Add("_(* #,##0_);_(* \\(#,##0\\);_(* \"-\"_);_(@_)", 41);
			Workbook.formatTable.Add("_(\"$\"* #,##0_);_(\"$\"* \\(#,##0\\);_(\"$\"* \"-\"_);_(@_)", 42);
			Workbook.formatTable.Add("_(* #,##0.00_);_(* \\(#,##0.00\\);_(* \"-\"??_);_(@_)", 43);
			Workbook.formatTable.Add("_(\"$\"* #,##0.00_);_(\"$\"* \\(#,##0.00\\);_(\"$\"* \"-\"??_);_(@_)", 44);
			Workbook.formatTable.Add("mm:ss", 45);
			Workbook.formatTable.Add("[h]:mm:ss", 46);
			Workbook.formatTable.Add("mm:ss.0", 47);
			Workbook.formatTable.Add("##0.0E+0", 48);
			Workbook.formatTable.Add("@", 49);
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06006904 RID: 26884 RVA: 0x0018A514 File Offset: 0x00188714
		public Workbook.WorksheetCollection Worksheets
		{
			get
			{
				if (this.worksheets == null)
				{
					this.worksheets = new Workbook.WorksheetCollection(this);
				}
				return this.worksheets;
			}
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x0018A530 File Offset: 0x00188730
		public Worksheet AddWorksheet()
		{
			Worksheet worksheet = new Worksheet();
			this.Worksheets.Add(worksheet);
			return worksheet;
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x06006906 RID: 26886 RVA: 0x0018A550 File Offset: 0x00188750
		public string DefaultFontName
		{
			get
			{
				return new Font().FontName;
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x06006907 RID: 26887 RVA: 0x0018A55C File Offset: 0x0018875C
		public float DefaultFontSize
		{
			get
			{
				Font font = new Font();
				return (float)font.FontSize / 20f;
			}
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x0018A57C File Offset: 0x0018877C
		public void Save(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("Stream is null", "stream");
			}
			foreach (Worksheet worksheet in this.Worksheets)
			{
				worksheet.Write(worksheet.Stream);
			}
			MemoryStream memoryStream = new MemoryStream();
			List<long> list = new List<long>();
			this.WriteWorkBookRecords(memoryStream, list);
			long position = memoryStream.Position;
			int num = (int)position;
			memoryStream.Position = list[0];
			byte[] bytes = BitConverter.GetBytes(num);
			memoryStream.Write(bytes, 0, bytes.Length);
			for (int i = 1; i < list.Count; i++)
			{
				memoryStream.Position = list[i];
				Worksheet worksheet2 = this.Worksheets[i - 1];
				int num2 = (int)worksheet2.Stream.Length;
				num += num2;
				bytes = BitConverter.GetBytes(num);
				memoryStream.Write(bytes, 0, bytes.Length);
			}
			memoryStream.Position = position;
			OLEStructuredStorage.UCOMIStorage ucomistorage = null;
			OLEStructuredStorage.UCOMILockBytes ucomilockBytes = null;
			IStream stream2 = null;
			try
			{
				int grfMode = 134221842;
				int grfMode2 = 18;
				OLEStructuredStorage.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out ucomilockBytes);
				OLEStructuredStorage.StgCreateDocfileOnILockBytes(ucomilockBytes, grfMode, 0, out ucomistorage);
				ucomistorage.CreateStream("Workbook", grfMode2, 0, 0, out stream2);
				if (stream2 != null)
				{
					IntPtr pcbWritten = 0;
					IntPtr pcbWritten2 = 0;
					byte[] array = memoryStream.ToArray();
					stream2.Write(array, array.Length, pcbWritten2);
					for (int j = 0; j < this.Worksheets.Count; j++)
					{
						Stream stream3 = this.Worksheets[j].Stream;
						if (stream3 != null)
						{
							byte[] array2 = new byte[9216];
							stream3.Seek(0L, SeekOrigin.Begin);
							int num4;
							for (long num3 = 0L; num3 < stream3.Length; num3 += (long)num4)
							{
								num4 = stream3.Read(array2, 0, 9216);
								stream2.Write(array2, num4, pcbWritten);
							}
							stream3.Close();
						}
					}
				}
				stream2.Commit(0);
				Marshal.ReleaseComObject(stream2);
				stream2 = null;
				ucomistorage.Commit(0);
				Marshal.ReleaseComObject(ucomistorage);
				ucomistorage = null;
				ucomilockBytes.Flush();
				System.Runtime.InteropServices.ComTypes.STATSTG statstg;
				ucomilockBytes.Stat(out statstg, 0);
				byte[] array3 = new byte[9216];
				int num5 = 0;
				long num6 = 0L;
				while (num6 < statstg.cbSize)
				{
					ucomilockBytes.ReadAt((ulong)num6, array3, 9216, out num5);
					num6 += (long)num5;
					stream.Write(array3, 0, num5);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (stream2 != null)
				{
					Marshal.ReleaseComObject(stream2);
					stream2 = null;
				}
				if (ucomistorage != null)
				{
					Marshal.ReleaseComObject(ucomistorage);
					ucomistorage = null;
				}
				if (ucomilockBytes != null)
				{
					Marshal.ReleaseComObject(ucomilockBytes);
					ucomilockBytes = null;
				}
			}
		}

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06006909 RID: 26889 RVA: 0x0018A880 File Offset: 0x00188A80
		private SSTHelper SSTHelper
		{
			get
			{
				if (this.sstHelper == null)
				{
					this.sstHelper = new SSTHelper();
				}
				return this.sstHelper;
			}
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x0600690A RID: 26890 RVA: 0x0018A89B File Offset: 0x00188A9B
		private Hashtable SharedStrings
		{
			get
			{
				if (this.sharedStrings == null)
				{
					this.sharedStrings = new Hashtable();
				}
				return this.sharedStrings;
			}
		}

		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x0600690B RID: 26891 RVA: 0x0018A8B6 File Offset: 0x00188AB6
		private Hashtable FontRecords
		{
			get
			{
				if (this.fontRecords == null)
				{
					this.fontRecords = new Hashtable();
				}
				return this.fontRecords;
			}
		}

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x0600690C RID: 26892 RVA: 0x0018A8D1 File Offset: 0x00188AD1
		private ArrayList FontList
		{
			get
			{
				if (this.fontList == null)
				{
					this.fontList = new ArrayList();
				}
				return this.fontList;
			}
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x0600690D RID: 26893 RVA: 0x0018A8EC File Offset: 0x00188AEC
		private Hashtable FormatRecords
		{
			get
			{
				if (this.formatRecords == null)
				{
					this.formatRecords = new Hashtable();
				}
				return this.formatRecords;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x0600690E RID: 26894 RVA: 0x0018A907 File Offset: 0x00188B07
		private ArrayList FormatList
		{
			get
			{
				if (this.formatList == null)
				{
					this.formatList = new ArrayList();
				}
				return this.formatList;
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x0600690F RID: 26895 RVA: 0x0018A922 File Offset: 0x00188B22
		private Hashtable XFRecords
		{
			get
			{
				if (this.xfRecords == null)
				{
					this.xfRecords = new Hashtable();
				}
				return this.xfRecords;
			}
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x06006910 RID: 26896 RVA: 0x0018A93D File Offset: 0x00188B3D
		private ArrayList XFList
		{
			get
			{
				if (this.xfList == null)
				{
					this.xfList = new ArrayList();
				}
				return this.xfList;
			}
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0018A958 File Offset: 0x00188B58
		internal int AddStringToList(string sharedString)
		{
			if (string.IsNullOrEmpty(sharedString))
			{
				return -1;
			}
			sharedString = this.ReplaceLineFeed(sharedString);
			return this.SSTHelper.AddString(sharedString, this.SharedStrings);
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0018A98C File Offset: 0x00188B8C
		internal int AddFontRecordToList(Font font)
		{
			if (font == null)
			{
				return -1;
			}
			string text = Convert.ToBase64String(font.GetData());
			if (text == Workbook.defaultFontKey)
			{
				return 0;
			}
			int num = this.AddRecord(this.FontRecords, this.FontList, font, text);
			return num + 5;
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x0018A9D4 File Offset: 0x00188BD4
		internal int AddFormatRecordToList(Format format)
		{
			if (format == null)
			{
				return 0;
			}
			string formatString = format.FormatString;
			if (Workbook.formatTable.Contains(formatString))
			{
				return (int)Workbook.formatTable[formatString];
			}
			int num;
			if (this.FormatRecords.Contains(formatString))
			{
				num = (int)this.FormatRecords[formatString];
				return (int)((Format)this.FormatList[num]).FormatIndex;
			}
			this.userDefinedFormatIndex += 1;
			format.FormatIndex = this.userDefinedFormatIndex;
			num = this.FormatList.Add(format);
			this.FormatRecords.Add(formatString, num);
			return (int)format.FormatIndex;
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x0018AA84 File Offset: 0x00188C84
		internal int AddXFRecordToList(XF xf)
		{
			if (xf == null)
			{
				return 15;
			}
			string text = Convert.ToBase64String(xf.GetData());
			if (Workbook.defaultXFKey == text)
			{
				return 15;
			}
			int num = this.AddRecord(this.XFRecords, this.XFList, xf, text);
			return num + 21;
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x0018AACC File Offset: 0x00188CCC
		private int AddRecord(IDictionary recordTable, IList objectList, IRecord record, string recordKey)
		{
			if (recordTable == null || objectList == null)
			{
				return -1;
			}
			if (recordTable.Contains(recordKey))
			{
				return (int)recordTable[recordKey];
			}
			int num = objectList.Add(record);
			recordTable.Add(recordKey, num);
			return num;
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x0018AB10 File Offset: 0x00188D10
		internal int AddImage(byte[] imageData, string imageName, Escher.RecordType recordType, int workSheetIndex, out int startingSPID, out int dgID)
		{
			if (imageData == null)
			{
				dgID = 0;
				startingSPID = 0;
				return 0;
			}
			Escher.BlipType blipType = Escher.BlipType.MSOBLIPUNKNOWN;
			Escher.BlipSignature blipSignature = Escher.BlipSignature.MSOBIUNKNOWN;
			switch (recordType)
			{
			case Escher.RecordType.MSOFBTBLIP_JPEG:
				blipType = Escher.BlipType.MSOBLIPJPEG;
				blipSignature = Escher.BlipSignature.MSOBIJFIF;
				break;
			case Escher.RecordType.MSOFBTBLIP_GIF:
				blipType = Escher.BlipType.MSOBLIPPNG;
				blipSignature = Escher.BlipSignature.MSOBIPNG;
				break;
			case Escher.RecordType.MSOFBTBLIP_DIB:
				blipType = Escher.BlipType.MSOBLIPDIB;
				blipSignature = Escher.BlipSignature.MSOBIDIB;
				break;
			}
			if (this.drawingGroupContainer == null)
			{
				this.drawingGroupContainer = new Escher.DrawingGroupContainer();
			}
			return this.drawingGroupContainer.AddImage(imageData, imageName, recordType, blipType, blipSignature, workSheetIndex, out startingSPID, out dgID);
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x0018AB90 File Offset: 0x00188D90
		internal BiffCell CreateStringCell(string cellString)
		{
			if (cellString == null)
			{
				return null;
			}
			BiffCell result;
			if (cellString.Length <= 255)
			{
				result = new StringLabelCell(cellString);
			}
			else
			{
				int sstIndex = this.AddStringToList(cellString);
				result = new StringSSTCell(sstIndex);
			}
			return result;
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x0018ABC8 File Offset: 0x00188DC8
		private string ReplaceLineFeed(string textValue)
		{
			if (textValue != null)
			{
				string text = textValue.Replace("\r\n", "\n");
				return text.Replace("\r", "\n");
			}
			return textValue;
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x0018ABFB File Offset: 0x00188DFB
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void OnWorksheetAdded(int index, Worksheet sheet)
		{
			sheet.workbook = this;
			if (string.IsNullOrEmpty(sheet.Name))
			{
				sheet.Name = SR.SheetName + (index + 1);
			}
		}

		// Token: 0x0600691A RID: 26906 RVA: 0x0018AC29 File Offset: 0x00188E29
		private void OnWorksheetRemoved(Worksheet sheet)
		{
			sheet.workbook = null;
		}

		// Token: 0x0600691B RID: 26907 RVA: 0x0018AC34 File Offset: 0x00188E34
		private void WriteWorkBookRecords(Stream stream, ICollection<long> boundSheetAddresses)
		{
			if (stream == null)
			{
				return;
			}
			byte[] data = new BOF().GetData();
			stream.Write(data, 0, data.Length);
			data = new InterfaceHdr().GetData();
			stream.Write(data, 0, data.Length);
			data = new MMS().GetData();
			stream.Write(data, 0, data.Length);
			data = new InterfaceEnd().GetData();
			stream.Write(data, 0, data.Length);
			data = new WriteAccess().GetData();
			stream.Write(data, 0, data.Length);
			data = new CodePage().GetData();
			stream.Write(data, 0, data.Length);
			data = new DSF().GetData();
			stream.Write(data, 0, data.Length);
			data = new XL9File().GetData();
			stream.Write(data, 0, data.Length);
			data = new TabID().GetData();
			stream.Write(data, 0, data.Length);
			data = new FunctionGroupCount().GetData();
			stream.Write(data, 0, data.Length);
			data = new WindowProtect().GetData();
			stream.Write(data, 0, data.Length);
			data = new Protect().GetData();
			stream.Write(data, 0, data.Length);
			data = new PasswordRecord().GetData();
			stream.Write(data, 0, data.Length);
			data = new Prot4Rev().GetData();
			stream.Write(data, 0, data.Length);
			data = new Prot4RevPass().GetData();
			stream.Write(data, 0, data.Length);
			data = new Window1().GetData();
			stream.Write(data, 0, data.Length);
			data = new Backup().GetData();
			stream.Write(data, 0, data.Length);
			data = new HideObj().GetData();
			stream.Write(data, 0, data.Length);
			data = new NineteenFourRecord().GetData();
			stream.Write(data, 0, data.Length);
			data = new Precision().GetData();
			stream.Write(data, 0, data.Length);
			data = new RefreshAll().GetData();
			stream.Write(data, 0, data.Length);
			data = new BookBool().GetData();
			stream.Write(data, 0, data.Length);
			this.AddFontRecords(stream);
			this.AddFormatRecords(stream);
			this.AddXFRecords(stream);
			this.AddStyleRecords(stream);
			data = new UsesElfs().GetData();
			stream.Write(data, 0, data.Length);
			this.AddBoundSheetRecords(stream, boundSheetAddresses);
			if ((this.excelNameTable != null || this.printTitlesNames != null) && this.externSheet != null)
			{
				ushort sheetCount = (ushort)this.Worksheets.Count;
				data = new SupBook(sheetCount).GetData();
				stream.Write(data, 0, data.Length);
				data = this.externSheet.GetData();
				stream.Write(data, 0, data.Length);
				if (this.excelNameList != null)
				{
					foreach (Name name in this.excelNameList)
					{
						data = name.GetData();
						stream.Write(data, 0, data.Length);
					}
					this.excelNameTable = null;
					this.excelNameList = null;
				}
				if (this.printTitlesNames != null)
				{
					foreach (Name name2 in this.printTitlesNames)
					{
						data = name2.GetData();
						stream.Write(data, 0, data.Length);
					}
					this.printTitlesNames = null;
				}
			}
			data = new RecalcID().GetData();
			stream.Write(data, 0, data.Length);
			if (this.drawingGroupContainer != null)
			{
				MsoDrawingGroup msoDrawingGroup = new MsoDrawingGroup(this.drawingGroupContainer);
				msoDrawingGroup.WriteToStream(stream);
			}
			this.CreateSSTandEXTSSTRecords(stream);
			data = new EOF().GetData();
			stream.Write(data, 0, data.Length);
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x0018AFD4 File Offset: 0x001891D4
		private void AddFontRecords(Stream stream)
		{
			if (stream != null)
			{
				byte[] data = new Font().GetData();
				stream.Write(data, 0, data.Length);
				data = new Font().GetData();
				stream.Write(data, 0, data.Length);
				data = new Font().GetData();
				stream.Write(data, 0, data.Length);
				data = new Font().GetData();
				stream.Write(data, 0, data.Length);
				if (this.fontList != null)
				{
					foreach (object obj in this.fontList)
					{
						Font font = (Font)obj;
						data = font.GetData();
						stream.Write(data, 0, data.Length);
					}
				}
				this.fontList = null;
				this.fontRecords = null;
			}
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x0018B0AC File Offset: 0x001892AC
		private void AddFormatRecords(Stream stream)
		{
			if (stream != null)
			{
				byte[] data = new Format(5, "\"$\"#,##0_);\\(\"$\"#,##0\\)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(6, "\"$\"#,##0_);[Red]\\(\"$\"#,##0\\)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(7, "\"$\"#,##0.00_);\\(\"$\"#,##0.00\\)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(8, "\"$\"#,##0.00_);[Red]\\(\"$\"#,##0.00\\)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(42, "_(\"$\"* #,##0_);_(\"$\"* \\(#,##0\\);_(\"$\"* \"-\"_);_(@_)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(41, "_(* #,##0_);_(* \\(#,##0\\);_(* \"-\"_);_(@_)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(44, "_(\"$\"* #,##0.00_);_(\"$\"* \\(#,##0.00\\);_(\"$\"* \"-\"??_);_(@_)").GetData();
				stream.Write(data, 0, data.Length);
				data = new Format(43, "_(* #,##0.00_);_(* \\(#,##0.00\\);_(* \"-\"??_);_(@_)").GetData();
				stream.Write(data, 0, data.Length);
				foreach (object obj in this.FormatList)
				{
					Format format = (Format)obj;
					data = format.GetData();
					stream.Write(data, 0, data.Length);
				}
				this.formatList = null;
				this.formatRecords = null;
			}
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x0018B208 File Offset: 0x00189408
		private void AddXFRecords(Stream stream)
		{
			if (stream != null)
			{
				byte[] data = new XF(0, 0, false, 0).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(2, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(2, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, false, 62464).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(0, 0, true, 0).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 43, false, 63488).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 41, false, 63488).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 44, false, 63488).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 42, false, 63488).GetData();
				stream.Write(data, 0, data.Length);
				data = new XF(1, 9, false, 63488).GetData();
				stream.Write(data, 0, data.Length);
				foreach (object obj in this.XFList)
				{
					XF xf = (XF)obj;
					data = xf.GetData();
					stream.Write(data, 0, data.Length);
				}
				this.xfList = null;
				this.xfRecords = null;
			}
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x0018B4F4 File Offset: 0x001896F4
		private void AddStyleRecords(Stream stream)
		{
			if (stream != null)
			{
				byte[] data = new StyleBIFF(16, 3).GetData();
				stream.Write(data, 0, data.Length);
				data = new StyleBIFF(17, 6).GetData();
				stream.Write(data, 0, data.Length);
				data = new StyleBIFF(18, 4).GetData();
				stream.Write(data, 0, data.Length);
				data = new StyleBIFF(19, 7).GetData();
				stream.Write(data, 0, data.Length);
				data = new StyleBIFF(0, 0).GetData();
				stream.Write(data, 0, data.Length);
				data = new StyleBIFF(20, 5).GetData();
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x0018B59C File Offset: 0x0018979C
		private void AddBoundSheetRecords(Stream stream, ICollection<long> boundSheetAddresses)
		{
			for (int i = 0; i < this.Worksheets.Count; i++)
			{
				Worksheet worksheet = this.Worksheets[i];
				BoundSheet boundSheet = new BoundSheet(worksheet.Name);
				byte[] data = boundSheet.GetData();
				if (boundSheetAddresses != null)
				{
					boundSheetAddresses.Add(stream.Position + 4L);
				}
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x0018B5FC File Offset: 0x001897FC
		private void CreateSSTandEXTSSTRecords(Stream stream)
		{
			if (this.sstHelper != null)
			{
				this.sstHelper.AddLastIndexRecord();
				uint sSTAddress = (uint)stream.Position;
				SST sst = new SST(this.sstHelper);
				INSTINF[] instInfArray = sst.WriteRecordAndGetOffsets(stream, sSTAddress);
				EXTSST extsst = new EXTSST(8, instInfArray);
				extsst.WriteEXTSSTRecord(stream);
			}
			this.sstHelper = null;
			this.sharedStrings = null;
		}

		// Token: 0x04001C46 RID: 7238
		private static readonly string defaultFontKey;

		// Token: 0x04001C47 RID: 7239
		private static readonly string defaultXFKey;

		// Token: 0x04001C48 RID: 7240
		private static readonly Hashtable formatTable;

		// Token: 0x04001C49 RID: 7241
		private Workbook.WorksheetCollection worksheets;

		// Token: 0x04001C4A RID: 7242
		private SSTHelper sstHelper;

		// Token: 0x04001C4B RID: 7243
		private Hashtable sharedStrings;

		// Token: 0x04001C4C RID: 7244
		private Hashtable fontRecords;

		// Token: 0x04001C4D RID: 7245
		private ArrayList fontList;

		// Token: 0x04001C4E RID: 7246
		private Hashtable formatRecords;

		// Token: 0x04001C4F RID: 7247
		private ArrayList formatList;

		// Token: 0x04001C50 RID: 7248
		private ushort userDefinedFormatIndex = 164;

		// Token: 0x04001C51 RID: 7249
		private Hashtable xfRecords;

		// Token: 0x04001C52 RID: 7250
		private ArrayList xfList;

		// Token: 0x04001C53 RID: 7251
		private ExternSheet externSheet;

		// Token: 0x04001C54 RID: 7252
		private List<Name> excelNameList;

		// Token: 0x04001C55 RID: 7253
		private Dictionary<string, int> excelNameTable;

		// Token: 0x04001C56 RID: 7254
		private List<Name> printTitlesNames;

		// Token: 0x04001C57 RID: 7255
		private Escher.DrawingGroupContainer drawingGroupContainer;

		// Token: 0x02000AF1 RID: 2801
		public class WorksheetCollection : Collection<Worksheet>
		{
			// Token: 0x06006923 RID: 26915 RVA: 0x0018B669 File Offset: 0x00189869
			public WorksheetCollection(Workbook owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006924 RID: 26916 RVA: 0x0018B678 File Offset: 0x00189878
			protected override void InsertItem(int index, Worksheet item)
			{
				base.InsertItem(index, item);
				if (item != null)
				{
					this.owner.OnWorksheetAdded(index, item);
				}
			}

			// Token: 0x06006925 RID: 26917 RVA: 0x0018B6A0 File Offset: 0x001898A0
			protected override void RemoveItem(int index)
			{
				Worksheet worksheet = base[index];
				base.RemoveItem(index);
				if (worksheet != null)
				{
					this.owner.OnWorksheetRemoved(worksheet);
				}
			}

			// Token: 0x04001C58 RID: 7256
			private readonly Workbook owner;
		}
	}
}

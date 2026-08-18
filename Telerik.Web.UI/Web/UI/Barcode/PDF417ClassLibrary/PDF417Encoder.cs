using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x0200009C RID: 156
	internal class PDF417Encoder
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		internal List<long> ECCodeWords { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000EFB1 File Offset: 0x0000D1B1
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0000EFB9 File Offset: 0x0000D1B9
		internal List<long> EncodedRawData { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0000EFCA File Offset: 0x0000D1CA
		internal int XRatio { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000EFD3 File Offset: 0x0000D1D3
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000EFDB File Offset: 0x0000D1DB
		internal int YRatio { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		internal int Columns { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000EFF5 File Offset: 0x0000D1F5
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000EFFD File Offset: 0x0000D1FD
		internal int Rows { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000F006 File Offset: 0x0000D206
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000F00E File Offset: 0x0000D20E
		internal int TotalRows { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000F017 File Offset: 0x0000D217
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0000F01F File Offset: 0x0000D21F
		internal int TotalColumns { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000F028 File Offset: 0x0000D228
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0000F030 File Offset: 0x0000D230
		internal int LengthIndicator { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000F039 File Offset: 0x0000D239
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000F041 File Offset: 0x0000D241
		internal int ECCount { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000F04A File Offset: 0x0000D24A
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0000F052 File Offset: 0x0000D252
		internal int ECLevel { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000F05B File Offset: 0x0000D25B
		internal int MaxAvailableDataCount
		{
			get
			{
				return this.Rows * this.Columns - this.ECCount - 1;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000F073 File Offset: 0x0000D273
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000F07B File Offset: 0x0000D27B
		internal bool[,] DataMatrix { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000F084 File Offset: 0x0000D284
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000F08C File Offset: 0x0000D28C
		private EncodingMode CurrentMode { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000F095 File Offset: 0x0000D295
		private int RawDataCount
		{
			get
			{
				return this.Columns * this.Rows - this.ECCount;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public void PopulateMatrix(string text, int errorCorrectionLevel, EncodingMode mode, int ratioX, int ratioY)
		{
			this.EncodedRawData = new List<long>();
			this.XRatio = ratioX;
			this.YRatio = ratioY;
			this.ECLevel = errorCorrectionLevel;
			this.ECCount = SpecificationData.ErrorCorrectionLevels[this.ECLevel].Count;
			if (mode == EncodingMode.Auto)
			{
				text = PDF417Encoder.ValidateTextModeNone(text);
				this.CurrentMode = EncodingMode.Auto;
				int i = 0;
				while (i < text.Length)
				{
					int numberOfDigitsAtPosition = PDF417Encoder.GetNumberOfDigitsAtPosition(text, i);
					if (numberOfDigitsAtPosition >= 13)
					{
						this.EncodeNumeric(text, ref i, numberOfDigitsAtPosition);
					}
					else if (numberOfDigitsAtPosition < 13)
					{
						int numberOfCharsAtPosition = PDF417Encoder.GetNumberOfCharsAtPosition(text, i);
						if (numberOfCharsAtPosition >= 5)
						{
							this.EncodeText(text, ref i, numberOfCharsAtPosition);
						}
						else if (numberOfCharsAtPosition < 5)
						{
							int numberOfBytesAtPosition = PDF417Encoder.GetNumberOfBytesAtPosition(text, i);
							if (numberOfBytesAtPosition == 1 && this.CurrentMode == EncodingMode.Text)
							{
								this.EncodeByte(text, ref i, numberOfBytesAtPosition);
							}
							else
							{
								this.EncodeByte(text, ref i, numberOfBytesAtPosition);
							}
						}
					}
				}
			}
			else if (mode == EncodingMode.Numeric)
			{
				this.EncodeNumericCompleteString(text);
			}
			else if (mode == EncodingMode.Text)
			{
				this.EncodeTextCompleteString(text);
			}
			else
			{
				this.EncodeByteCompleteString(text);
			}
			this.SetSmallestSizeOfMatrix();
			this.VerifyDataLength();
			this.PadData();
			this.SetErrorCorrection();
			this.FillMatrixWithData();
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal static int CalculateLeftRowIndicator(int clusterIndex, int rowNumber, int rows, int columns, int eclevel)
		{
			int result;
			if (clusterIndex == 0)
			{
				result = 30 * ((rowNumber - 1) / 3) + (rows - 1) / 3;
			}
			else if (clusterIndex == 1)
			{
				result = 30 * ((rowNumber - 1) / 3) + eclevel * 3 + (rows - 1) % 3;
			}
			else
			{
				result = 30 * ((rowNumber - 1) / 3) + (columns - 1);
			}
			return result;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000F20C File Offset: 0x0000D40C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal static int CalculateRightRowIndicator(int clusterIndex, int rowNumber, int rows, int columns, int eclevel)
		{
			int result;
			if (clusterIndex == 0)
			{
				result = 30 * ((rowNumber - 1) / 3) + (columns - 1);
			}
			else if (clusterIndex == 1)
			{
				result = 30 * ((rowNumber - 1) / 3) + (rows - 1) / 3;
			}
			else
			{
				result = 30 * ((rowNumber - 1) / 3) + eclevel * 3 + (rows - 1) % 3;
			}
			return result;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000F258 File Offset: 0x0000D458
		private static string ValidateTextModeNone(string text)
		{
			string text2 = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				if (PDF417Encoder.IsCharValid(text[i]))
				{
					text2 += text[i];
				}
			}
			return text2;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		private static string ValidateText(string text)
		{
			string text2 = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				TextModeDefinitionEntry textModeDefinitionEntry = TextMode.FindCharacterInTable((int)text[i]);
				if (textModeDefinitionEntry != null)
				{
					text2 += text[i];
				}
			}
			return text2;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		private static string ValidateByte(string text)
		{
			string text2 = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				if (SpecificationData.ByteModeValues.Contains((int)text[i]))
				{
					text2 += text[i];
				}
			}
			return text2;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000F334 File Offset: 0x0000D534
		private static string ValidateDigits(string text)
		{
			string text2 = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				if (char.IsDigit(text[i]))
				{
					text2 += text[i];
				}
			}
			return text2;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000F37C File Offset: 0x0000D57C
		private static bool IsCharValid(char character)
		{
			int num = PDF417Encoder.GetNumberOfBytesAtPosition(character.ToString(), 0) + PDF417Encoder.GetNumberOfCharsAtPosition(character.ToString(), 0) + PDF417Encoder.GetNumberOfDigitsAtPosition(character.ToString(), 0);
			return num > 0;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000F3BC File Offset: 0x0000D5BC
		private static int GetNumberOfBytesAtPosition(string text, int currentPosition)
		{
			int num = 0;
			while (currentPosition <= text.Length - 1 && PDF417Encoder.GetNumberOfDigitsAtPosition(text, currentPosition) < 13 && PDF417Encoder.GetNumberOfCharsAtPosition(text, currentPosition) < 5 && SpecificationData.ByteModeValues.Contains((int)text[currentPosition]))
			{
				currentPosition++;
				num++;
			}
			return num;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000F40C File Offset: 0x0000D60C
		private static int GetNumberOfCharsAtPosition(string text, int currentPosition)
		{
			int num = 0;
			while (currentPosition <= text.Length - 1 && PDF417Encoder.GetNumberOfDigitsAtPosition(text, currentPosition) < 13 && TextMode.FindCharacterInTable((int)text[currentPosition]) != null)
			{
				currentPosition++;
				num++;
			}
			return num;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000F44C File Offset: 0x0000D64C
		private static int GetNumberOfDigitsAtPosition(string text, int currentPosition)
		{
			int num = 0;
			while (currentPosition <= text.Length - 1 && char.IsDigit(text[currentPosition]))
			{
				currentPosition++;
				num++;
			}
			return num;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000F480 File Offset: 0x0000D680
		private static int CalculateClusterIndex(int rowIndex)
		{
			int num = rowIndex % 3 * 3;
			if (num == 0)
			{
				return 0;
			}
			if (num == 3)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		private static void DetermineNextMode(string text, ref bool shouldApplyNonLatchData)
		{
			int numberOfCharsAtPosition = PDF417Encoder.GetNumberOfCharsAtPosition(text, 0);
			if (numberOfCharsAtPosition < 5)
			{
				int numberOfBytesAtPosition = PDF417Encoder.GetNumberOfBytesAtPosition(text, 0);
				if (numberOfBytesAtPosition != 1)
				{
					shouldApplyNonLatchData = true;
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		private void EncodeNumeric(string text, ref int dataIndex, int numberOfDigitsAtPosition)
		{
			this.CurrentMode = EncodingMode.Numeric;
			this.EncodedRawData.Add(902L);
			this.EncodedRawData.AddRange(NumericMode.EncodeData(text.Substring(dataIndex, numberOfDigitsAtPosition)));
			dataIndex += numberOfDigitsAtPosition;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000F501 File Offset: 0x0000D701
		private void EncodeNumericCompleteString(string text)
		{
			this.CurrentMode = EncodingMode.Numeric;
			text = PDF417Encoder.ValidateDigits(text);
			this.EncodedRawData.Add(902L);
			this.EncodedRawData.AddRange(NumericMode.EncodeData(text));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000F534 File Offset: 0x0000D734
		private void EncodeText(string text, ref int dataIndex, int numberOfTexModeCharsAtPosition)
		{
			TextMode textMode = new TextMode();
			this.CurrentMode = EncodingMode.Text;
			int num = dataIndex + numberOfTexModeCharsAtPosition;
			bool shouldApplyNonLatchData = false;
			if (num < text.Length - 1)
			{
				PDF417Encoder.DetermineNextMode(text.Substring(num), ref shouldApplyNonLatchData);
			}
			this.EncodedRawData.Add(900L);
			this.EncodedRawData.AddRange(textMode.EncodeData(text.Substring(dataIndex, numberOfTexModeCharsAtPosition), shouldApplyNonLatchData));
			dataIndex += numberOfTexModeCharsAtPosition;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		private void EncodeTextCompleteString(string text)
		{
			TextMode textMode = new TextMode();
			this.CurrentMode = EncodingMode.Text;
			text = PDF417Encoder.ValidateText(text);
			this.EncodedRawData.AddRange(textMode.EncodeData(text, false));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000F5D5 File Offset: 0x0000D7D5
		private void EncodeByte(string text, ref int dataIndex, int numberOfBytesAtIndex)
		{
			this.CurrentMode = EncodingMode.Byte;
			this.EncodedRawData.Add(913L);
			this.EncodedRawData.AddRange(ByteMode.EncodeText(text.Substring(dataIndex, numberOfBytesAtIndex)));
			dataIndex += numberOfBytesAtIndex;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000F60E File Offset: 0x0000D80E
		private void EncodeByteCompleteString(string text)
		{
			this.CurrentMode = EncodingMode.Byte;
			text = PDF417Encoder.ValidateByte(text);
			this.EncodedRawData.AddRange(ByteMode.EncodeText(text));
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000F630 File Offset: 0x0000D830
		private void SetSmallestSizeOfMatrix()
		{
			this.Rows = 3;
			this.Columns = 2;
			while (this.Columns * this.Rows < this.EncodedRawData.Count + this.ECCount + 1)
			{
				double num = (double)this.Columns / (double)this.Rows;
				double num2 = (double)this.XRatio / (double)this.YRatio;
				if (num < num2 && this.Columns <= 30)
				{
					if ((this.Columns + 1) * this.Rows > 928)
					{
						break;
					}
					if (this.Columns + 1 < 30)
					{
						this.Columns++;
					}
					else if (this.Rows + 1 < 90)
					{
						this.Rows++;
					}
				}
				else
				{
					if ((this.Rows + 1) * this.Columns > 928)
					{
						break;
					}
					if (this.Rows + 1 < 90)
					{
						this.Rows++;
					}
					else if (this.Columns + 1 < 90)
					{
						this.Columns++;
					}
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000F744 File Offset: 0x0000D944
		private void PadData()
		{
			List<long> list = new List<long>();
			this.LengthIndicator = this.RawDataCount;
			list.Add((long)this.LengthIndicator);
			list.AddRange(this.EncodedRawData);
			while (list.Count < this.LengthIndicator)
			{
				list.Add(900L);
			}
			this.EncodedRawData = list;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000F79F File Offset: 0x0000D99F
		private void SetErrorCorrection()
		{
			this.ECCodeWords = new List<long>();
			this.ECCodeWords.AddRange(ErrorCorrectionGenerator.GenerateErrorCorrectionSequence(this.EncodedRawData, this.ECLevel));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		private void VerifyDataLength()
		{
			while (this.EncodedRawData.Count > this.MaxAvailableDataCount)
			{
				this.EncodedRawData.RemoveAt(this.EncodedRawData.Count - 1);
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		private void FillMatrixWithData()
		{
			this.TotalColumns = 35 + this.Columns * 17 + 34;
			this.TotalRows = this.Rows;
			this.DataMatrix = new bool[this.TotalRows, this.TotalColumns];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.TotalRows; i++)
			{
				int j = 0;
				while (j < this.TotalColumns)
				{
					if (j == 0)
					{
						this.AddStartClusterToRow(i);
						j += 17;
					}
					else if (j == 17)
					{
						this.AddLeftRowIndicatorCluster(i, j);
						j += 17;
					}
					else if (j == this.TotalColumns - 18 - 17)
					{
						this.AddRightRowIndicatorCluster(i, j);
						j += 17;
					}
					else if (j == this.TotalColumns - 18)
					{
						this.AddStopClusterToRow(i);
						j += 18;
					}
					else if (num < this.EncodedRawData.Count && j + 17 < this.TotalColumns)
					{
						this.AddDataCluster(i, j, num);
						j += 17;
						num++;
					}
					else
					{
						this.AddErrorCorrectionCluster(i, j, num2);
						j += 17;
						num2++;
					}
				}
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000F90C File Offset: 0x0000DB0C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddLeftRowIndicatorCluster(int rowIndex, int columnIndex)
		{
			int num = PDF417Encoder.CalculateClusterIndex(rowIndex);
			int index = PDF417Encoder.CalculateLeftRowIndicator(num, rowIndex, this.Rows, this.Columns, this.ECLevel);
			string text = SpecificationData.BarSpaceSequence[index][num].ToString();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = i % 2 == 0;
				for (int j = 0; j < int.Parse(text[i].ToString()); j++)
				{
					this.DataMatrix[rowIndex, columnIndex] = flag;
					columnIndex++;
				}
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddRightRowIndicatorCluster(int rowIndex, int columnIndex)
		{
			int num = PDF417Encoder.CalculateClusterIndex(rowIndex);
			int index = PDF417Encoder.CalculateRightRowIndicator(num, rowIndex, this.Rows, this.Columns, this.ECLevel);
			string text = SpecificationData.BarSpaceSequence[index][num].ToString();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = i % 2 == 0;
				for (int j = 0; j < int.Parse(text[i].ToString()); j++)
				{
					this.DataMatrix[rowIndex, columnIndex] = flag;
					columnIndex++;
				}
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddErrorCorrectionCluster(int rowIndex, int columnIndex, int ecpointer)
		{
			int index = PDF417Encoder.CalculateClusterIndex(rowIndex);
			string text = SpecificationData.BarSpaceSequence[(int)this.ECCodeWords[ecpointer]][index].ToString();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = i % 2 == 0;
				for (int j = 0; j < int.Parse(text[i].ToString()); j++)
				{
					this.DataMatrix[rowIndex, columnIndex] = flag;
					columnIndex++;
				}
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000FADC File Offset: 0x0000DCDC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void AddDataCluster(int rowIndex, int columnIndex, int dataPointer)
		{
			int index = PDF417Encoder.CalculateClusterIndex(rowIndex);
			string text = SpecificationData.BarSpaceSequence[(int)this.EncodedRawData[dataPointer]][index].ToString();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = i % 2 == 0;
				for (int j = 0; j < int.Parse(text[i].ToString()); j++)
				{
					this.DataMatrix[rowIndex, columnIndex] = flag;
					columnIndex++;
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		private void AddStopClusterToRow(int rowIndex)
		{
			int num = this.TotalColumns - 18;
			foreach (Cluster cluster in SpecificationData.Stop)
			{
				for (int i = 0; i < cluster.NumberOfModulesAtPosition; i++)
				{
					this.DataMatrix[rowIndex, num] = cluster.ValueOfModule;
					num++;
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		private void AddStartClusterToRow(int rowIndex)
		{
			int num = 0;
			foreach (Cluster cluster in SpecificationData.Start)
			{
				for (int i = 0; i < cluster.NumberOfModulesAtPosition; i++)
				{
					this.DataMatrix[rowIndex, num] = cluster.ValueOfModule;
					num++;
				}
			}
		}
	}
}

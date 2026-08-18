using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x0200009E RID: 158
	internal class TextMode
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001070B File Offset: 0x0000E90B
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00010713 File Offset: 0x0000E913
		private List<long> FormattedDataInt { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001071C File Offset: 0x0000E91C
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00010724 File Offset: 0x0000E924
		private List<long> CodeWordsDataInt { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001072D File Offset: 0x0000E92D
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00010735 File Offset: 0x0000E935
		private TextSubModes CurrentSymbolSubmode { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001073E File Offset: 0x0000E93E
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00010746 File Offset: 0x0000E946
		private int CurrentSubmodeSwitch { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001074F File Offset: 0x0000E94F
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00010757 File Offset: 0x0000E957
		private int UpperToLowerSwitchCount { get; set; }

		// Token: 0x06000617 RID: 1559 RVA: 0x00010760 File Offset: 0x0000E960
		internal static TextModeDefinitionEntry FindCharacterInTable(int value)
		{
			foreach (TextModeDefinitionEntry textModeDefinitionEntry in SpecificationData.TextSubmodes)
			{
				if (textModeDefinitionEntry.EntryValue == value)
				{
					return textModeDefinitionEntry;
				}
			}
			return null;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000107BC File Offset: 0x0000E9BC
		internal List<long> EncodeData(string textToEncode, bool shouldApplyNonLatchData)
		{
			this.InitializeFormatData(textToEncode);
			if (this.FormattedDataInt.Count % 2 != 0)
			{
				this.PadData(shouldApplyNonLatchData);
			}
			this.InitializeCodeWordsDataInt();
			return this.CodeWordsDataInt;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000107E8 File Offset: 0x0000E9E8
		internal void PadData(bool shouldApplyNonLatchData)
		{
			if (!shouldApplyNonLatchData)
			{
				TextModeDefinitionEntry textModeDefinitionEntry = TextMode.FindCharacterInTable(1005);
				this.FormattedDataInt.Add((long)textModeDefinitionEntry.RowIndex);
				return;
			}
			if (this.CurrentSymbolSubmode != TextSubModes.Punctuation)
			{
				TextModeDefinitionEntry textModeDefinitionEntry2 = TextMode.FindCharacterInTable(1006);
				this.FormattedDataInt.Add((long)textModeDefinitionEntry2.RowIndex);
				return;
			}
			TextModeDefinitionEntry textModeDefinitionEntry3 = TextMode.FindCharacterInTable(1001);
			this.FormattedDataInt.Add((long)textModeDefinitionEntry3.RowIndex);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001085C File Offset: 0x0000EA5C
		internal void InitializeCodeWordsDataInt()
		{
			this.CodeWordsDataInt = new List<long>();
			for (int i = 0; i < this.FormattedDataInt.Count; i += 2)
			{
				long num = this.FormattedDataInt[i];
				long num2 = this.FormattedDataInt[i + 1];
				long item = num * 30L + num2;
				this.CodeWordsDataInt.Add(item);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000108BC File Offset: 0x0000EABC
		internal void InitializeFormatData(string data)
		{
			this.FormattedDataInt = new List<long>();
			this.UpperToLowerSwitchCount = 0;
			int i = 0;
			while (i < data.Length)
			{
				TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(i));
				if (nextValidCharacter != null)
				{
					this.SetCurrentSymbolSubmode(nextValidCharacter);
					if (i == 0)
					{
						if (nextValidCharacter.TypeIndex == 1)
						{
							this.FormattedDataInt.Add(27L);
							this.CurrentSubmodeSwitch = 27;
						}
						else if (nextValidCharacter.TypeIndex == 2)
						{
							this.FormattedDataInt.Add(28L);
							this.CurrentSubmodeSwitch = 28;
						}
						else if (nextValidCharacter.TypeIndex == 3)
						{
							this.FormattedDataInt.Add(29L);
							this.CurrentSubmodeSwitch = 29;
						}
						if (this.IsStringIdentical(data, nextValidCharacter.TypeIndex))
						{
							this.AddRangeToFormattedDataInt(data);
							i += data.Length;
							return;
						}
						int firstSegmentLength = this.GetFirstSegmentLength(data);
						i += firstSegmentLength;
						this.AddRangeToFormattedDataInt(data.Substring(0, firstSegmentLength));
						nextValidCharacter = this.GetNextValidCharacter(data.Substring(i - 1));
						this.SetCurrentSwitch(nextValidCharacter, data, i - 1);
					}
					else
					{
						this.FormattedDataInt.Add((long)nextValidCharacter.RowIndex);
						if (i != data.Length - 1)
						{
							this.SetCurrentSwitch(nextValidCharacter, data, i);
						}
						i++;
					}
				}
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000109F4 File Offset: 0x0000EBF4
		internal TextModeDefinitionEntry GetNextValidCharacter(string remainingData)
		{
			TextModeDefinitionEntry textModeDefinitionEntry = TextMode.FindCharacterInTable((int)remainingData[0]);
			if (textModeDefinitionEntry == null && remainingData.Length > 1)
			{
				remainingData = remainingData.Substring(1);
				return this.GetNextValidCharacter(remainingData);
			}
			return textModeDefinitionEntry;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00010A2C File Offset: 0x0000EC2C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void SetCurrentSwitch(TextModeDefinitionEntry currentEntry, string data, int dataIndex)
		{
			TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(dataIndex + 1));
			if (nextValidCharacter != null)
			{
				if (currentEntry.TypeIndex == 0)
				{
					if ((nextValidCharacter.TypeIndex == 0 || nextValidCharacter.TypeIndex == 1) && this.CurrentSubmodeSwitch != 28 && this.CurrentSubmodeSwitch != 29)
					{
						if (nextValidCharacter.TypeIndex == 1 && this.UpperToLowerSwitchCount >= 1)
						{
							return;
						}
						if (nextValidCharacter.TypeIndex == 1 && this.UpperToLowerSwitchCount == 0)
						{
							this.UpperToLowerSwitchCount++;
						}
						this.FormattedDataInt.Add(27L);
						this.CurrentSubmodeSwitch = 27;
						return;
					}
					else
					{
						if (nextValidCharacter.TypeIndex == 2)
						{
							this.FormattedDataInt.Add(28L);
							this.CurrentSubmodeSwitch = 28;
							return;
						}
						if (nextValidCharacter.TypeIndex == 3)
						{
							this.FormattedDataInt.Add(29L);
							this.CurrentSubmodeSwitch = 29;
							return;
						}
					}
				}
				else if (currentEntry.TypeIndex == 1)
				{
					if (nextValidCharacter.TypeIndex == 0)
					{
						this.FormattedDataInt.Add(27L);
						this.CurrentSubmodeSwitch = 27;
						return;
					}
					if (nextValidCharacter.TypeIndex == 2)
					{
						this.FormattedDataInt.Add(28L);
						this.CurrentSubmodeSwitch = 28;
						return;
					}
					if (nextValidCharacter.TypeIndex == 3)
					{
						this.FormattedDataInt.Add(29L);
						this.CurrentSubmodeSwitch = 29;
						return;
					}
				}
				else if (currentEntry.TypeIndex == 2)
				{
					if (nextValidCharacter.TypeIndex == 0)
					{
						this.FormattedDataInt.Add(28L);
						this.CurrentSubmodeSwitch = 28;
						return;
					}
					if (nextValidCharacter.TypeIndex == 1)
					{
						this.FormattedDataInt.Add(27L);
						this.CurrentSubmodeSwitch = 27;
						return;
					}
					if (nextValidCharacter.TypeIndex == 3)
					{
						this.FormattedDataInt.Add(29L);
						this.CurrentSubmodeSwitch = 29;
						return;
					}
				}
				else
				{
					if (nextValidCharacter.TypeIndex == 1)
					{
						this.FormattedDataInt.Add(27L);
						this.CurrentSubmodeSwitch = 27;
						return;
					}
					if (nextValidCharacter.TypeIndex == 2 && this.CurrentSubmodeSwitch != 28)
					{
						this.FormattedDataInt.Add(28L);
						this.CurrentSubmodeSwitch = 28;
						return;
					}
					if (nextValidCharacter.TypeIndex == 3)
					{
						this.FormattedDataInt.Add(29L);
						this.CurrentSubmodeSwitch = 29;
					}
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010C4C File Offset: 0x0000EE4C
		private int GetFirstSegmentLength(string data)
		{
			int num;
			if (data.Length > 0)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			for (int i = 0; i < data.Length; i++)
			{
				TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(i));
				TextModeDefinitionEntry nextValidCharacter2 = this.GetNextValidCharacter(data.Substring(i + 1));
				if (nextValidCharacter == null || nextValidCharacter2 == null || nextValidCharacter.TypeIndex != nextValidCharacter2.TypeIndex)
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		private void AddRangeToFormattedDataInt(string data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(i));
				this.FormattedDataInt.Add((long)nextValidCharacter.RowIndex);
				if (this.CurrentSubmodeSwitch == 29 && i < data.Length - 1)
				{
					this.FormattedDataInt.Add(29L);
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00010D14 File Offset: 0x0000EF14
		private bool IsStringIdentical(string data, int typeIndex)
		{
			bool result = true;
			for (int i = 0; i < data.Length - 1; i++)
			{
				TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(i));
				TextModeDefinitionEntry nextValidCharacter2 = this.GetNextValidCharacter(data.Substring(i + 1));
				if (nextValidCharacter.TypeIndex != nextValidCharacter2.TypeIndex || nextValidCharacter.TypeIndex != typeIndex || nextValidCharacter2.TypeIndex != typeIndex)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00010D79 File Offset: 0x0000EF79
		private void SetCurrentSymbolSubmode(TextModeDefinitionEntry currentEntry)
		{
			if (currentEntry.TypeIndex == 0)
			{
				this.CurrentSymbolSubmode = TextSubModes.Alpha;
				return;
			}
			if (currentEntry.TypeIndex == 1)
			{
				this.CurrentSymbolSubmode = TextSubModes.Lower;
				return;
			}
			if (currentEntry.TypeIndex == 2)
			{
				this.CurrentSymbolSubmode = TextSubModes.Mixed;
				return;
			}
			this.CurrentSymbolSubmode = TextSubModes.Punctuation;
		}
	}
}

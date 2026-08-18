using System;
using System.Text;
using System.Windows.Forms;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataItem
{
	// Token: 0x0200006E RID: 110
	public class DataItemPs
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001E444 File Offset: 0x0001D444
		public int DataId
		{
			get
			{
				return this.dataId;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001E45C File Offset: 0x0001D45C
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001E474 File Offset: 0x0001D474
		public DataItemPs(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int dataId, int controlId, string controlCaption, int controlCode, string valText, bool valBytesIsEncrypted, byte[] bb, byte[] bbi)
		{
			this.tripleDES = tripleDES;
			this.da = da;
			this.dataId = dataId;
			this.controlId = controlId;
			this.controlCaption = controlCaption;
			this.controlCode = controlCode;
			this.valText = valText;
			this.valBytesIsEncrypted = valBytesIsEncrypted;
			this.bb = bb;
			this.bbi = bbi;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001E4D8 File Offset: 0x0001D4D8
		public string ControlCaptionForDisplay
		{
			get
			{
				int num = this.controlCaption.IndexOf("~~");
				string result;
				if (num == 0)
				{
					result = this.controlCaption.Substring(2);
				}
				else if (num > 0)
				{
					result = this.controlCaption.Substring(0, num);
				}
				else
				{
					result = this.controlCaption;
				}
				return result;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001E538 File Offset: 0x0001D538
		private string GetValueString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.bbi != null && this.bbi.Length > 0)
			{
				string text = this.tripleDES.Decrypt(this.bbi);
				if (text.StartsWith("{\\rtf1"))
				{
					using (RichTextBox richTextBox = new RichTextBox())
					{
						richTextBox.Rtf = text;
						stringBuilder.Append(richTextBox.Text);
					}
				}
				else
				{
					stringBuilder.Append(text);
				}
			}
			else if (this.bb != null && this.bb.Length > 0)
			{
				stringBuilder.Append(ClockWorkCore.BytesToString(this.bb, this.valBytesIsEncrypted, this.tripleDES));
			}
			else if (this.controlCode != 2 && this.controlCode != 700)
			{
				if (this.controlCode != 4)
				{
					if (this.controlCode == 10)
					{
						stringBuilder.Append("<table cellpadding='2' cellspacing='2'>");
						string text2 = this.valText;
						char[] separator = new char[1];
						string[] array = text2.Split(separator);
						foreach (string text3 in array)
						{
							stringBuilder.Append("<tr>");
							string[] array3 = text3.Split(new char[]
							{
								'\t'
							});
							foreach (string arg in array3)
							{
								stringBuilder.Append(string.Format("<td>{0}</td>", arg));
							}
							stringBuilder.Append("</tr>");
						}
						stringBuilder.Append("</table>");
					}
					else
					{
						stringBuilder.Append(this.valText);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001E764 File Offset: 0x0001D764
		public string ToStringHtml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string valueString = this.GetValueString();
			stringBuilder.Append("<b>");
			stringBuilder.Append(this.ControlCaptionForDisplay);
			if (!string.IsNullOrEmpty(valueString))
			{
				stringBuilder.Append(":</b> ");
				stringBuilder.Append(valueString);
			}
			else
			{
				stringBuilder.Append("</b>");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002F1 RID: 753
		private UnivDataAdapter da;

		// Token: 0x040002F2 RID: 754
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040002F3 RID: 755
		private int dataId;

		// Token: 0x040002F4 RID: 756
		private int controlId;

		// Token: 0x040002F5 RID: 757
		private string controlCaption;

		// Token: 0x040002F6 RID: 758
		private int controlCode;

		// Token: 0x040002F7 RID: 759
		private string valText;

		// Token: 0x040002F8 RID: 760
		private bool valBytesIsEncrypted;

		// Token: 0x040002F9 RID: 761
		private byte[] bb;

		// Token: 0x040002FA RID: 762
		private byte[] bbi;
	}
}

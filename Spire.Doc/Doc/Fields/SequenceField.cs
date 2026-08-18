using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x02000520 RID: 1312
	public class SequenceField : Field
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x00404064 File Offset: 0x00403064
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.SeqField;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x004040A4 File Offset: 0x004030A4
		public new string FormattingString
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
				return this.m_formattingString;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x004040E8 File Offset: 0x004030E8
		// (set) Token: 0x060044DA RID: 17626 RVA: 0x0040412C File Offset: 0x0040312C
		public CaptionNumberingFormat NumberFormat
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
				return this.ᜀ;
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x00404170 File Offset: 0x00403170
		// (set) Token: 0x060044DC RID: 17628 RVA: 0x004041B4 File Offset: 0x004031B4
		public string CaptionName
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
				return this.m_fieldValue;
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
				this.m_fieldValue = value;
			}
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x004041F8 File Offset: 0x004031F8
		public SequenceField(IDocument doc) : base(doc)
		{
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x00404214 File Offset: 0x00403214
		protected internal SequenceField(Field field) : base(field.Document)
		{
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x00404234 File Offset: 0x00403234
		protected internal override string ConvertSwitchesToString()
		{
			int a_ = 8;
			string text;
			for (;;)
			{
				text = base.ConvertSwitchesToString();
				CaptionNumberingFormat captionNumberingFormat = this.ᜀ;
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
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return text;
						case 1:
							num = 0;
							continue;
						case 2:
							return text;
						case 3:
							return text;
						case 4:
							switch (captionNumberingFormat)
							{
							case CaptionNumberingFormat.Number:
								text += ClipboardData.b("乭Ɐ塱味㝵⩷㭹㹻㝽썿", a_);
								num = 3;
								continue;
							case CaptionNumberingFormat.Roman:
								text += ClipboardData.b("乭Ɐ塱味⑵㝷㝹㵻ぽ", a_);
								num = 2;
								continue;
							case CaptionNumberingFormat.Alphabetic:
								text += ClipboardData.b("乭Ɐ塱味㝵㑷⩹㑻㽽쉿잁킃쾅쮇", a_);
								num = 5;
								continue;
							default:
								num = 1;
								continue;
							}
							break;
						case 5:
							return text;
						}
						break;
					}
					break;
				}
				}
			}
			return text;
		}

		// Token: 0x04003616 RID: 13846
		private byte \u2609\u008F\u00A2\u00A7;

		// Token: 0x04003617 RID: 13847
		private float \u2460\u0092\u0083\u00AB;

		// Token: 0x04003618 RID: 13848
		private bool \u2460\u00A4\u008D\u0095;

		// Token: 0x04003619 RID: 13849
		private long \u25D9\u008A\u008C\u009F;

		// Token: 0x0400361A RID: 13850
		private new CaptionNumberingFormat ᜀ = (CaptionNumberingFormat)(-1);
	}
}

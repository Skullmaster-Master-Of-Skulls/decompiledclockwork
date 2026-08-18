using System;
using System.Drawing;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x02000041 RID: 65
	public class ConditionalFormatWrapper : CommonWrapper, sprᲖ
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x00026F94 File Offset: 0x00025F94
		private ConditionalFormatWrapper()
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00026FA8 File Offset: 0x00025FA8
		public ConditionalFormatWrapper(CondFormatCollectionWrapper formats, int iIndex)
		{
			int a_ = 6;
			base..ctor();
			if (formats == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("娻儽㈿⽁╃㉅㭇", a_));
			}
			this.ᜀ = formats;
			if (iIndex >= 0)
			{
				if (iIndex < formats.Count)
				{
					this.ᜁ = iIndex;
					return;
				}
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻眽⸿♁⅃㹅", a_));
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00027010 File Offset: 0x00026010
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00027058 File Offset: 0x00026058
		public ConditionalFormatType FormatType
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
				return this.ᜁ().FormatType;
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
				this.BeginUpdate();
				this.ᜁ().FormatType = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000270AC File Offset: 0x000260AC
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x000270F4 File Offset: 0x000260F4
		public ComparisonOperatorType Operator
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
				return this.ᜁ().Operator;
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
				this.BeginUpdate();
				this.ᜁ().Operator = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00027148 File Offset: 0x00026148
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00027190 File Offset: 0x00026190
		public bool IsBold
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
				return this.ᜁ().IsBold;
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
				this.BeginUpdate();
				this.ᜁ().IsBold = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000271E4 File Offset: 0x000261E4
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0002722C File Offset: 0x0002622C
		public bool IsItalic
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
				return this.ᜁ().IsItalic;
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
				this.BeginUpdate();
				this.ᜁ().IsItalic = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00027280 File Offset: 0x00026280
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000272C8 File Offset: 0x000262C8
		public ExcelColors FontKnownColor
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
				return this.ᜁ().FontKnownColor;
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
				this.BeginUpdate();
				this.ᜁ().FontKnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0002731C File Offset: 0x0002631C
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00027364 File Offset: 0x00026364
		public Color FontColor
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
				return this.ᜁ().FontColor;
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
				this.BeginUpdate();
				this.ᜁ().FontColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000273B8 File Offset: 0x000263B8
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00027400 File Offset: 0x00026400
		public FontUnderlineType Underline
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
				return this.ᜁ().Underline;
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
				this.BeginUpdate();
				this.ᜁ().Underline = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00027454 File Offset: 0x00026454
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0002749C File Offset: 0x0002649C
		public bool IsStrikeThrough
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
				return this.ᜁ().IsStrikeThrough;
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
				this.BeginUpdate();
				this.ᜁ().IsStrikeThrough = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x000274F0 File Offset: 0x000264F0
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00027538 File Offset: 0x00026538
		public ExcelColors LeftBorderKnownColor
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
				return this.ᜁ().LeftBorderKnownColor;
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
				this.BeginUpdate();
				this.ᜁ().LeftBorderKnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0002758C File Offset: 0x0002658C
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x000275D4 File Offset: 0x000265D4
		public Color LeftBorderColor
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
				return this.ᜁ().LeftBorderColor;
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
				this.BeginUpdate();
				this.ᜁ().LeftBorderColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00027628 File Offset: 0x00026628
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00027670 File Offset: 0x00026670
		public LineStyleType LeftBorderStyle
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
				return this.ᜁ().LeftBorderStyle;
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
				this.BeginUpdate();
				this.ᜁ().LeftBorderStyle = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x000276C4 File Offset: 0x000266C4
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0002770C File Offset: 0x0002670C
		public ExcelColors RightBorderKnownColor
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
				return this.ᜁ().RightBorderKnownColor;
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
				this.BeginUpdate();
				this.ᜁ().RightBorderKnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00027760 File Offset: 0x00026760
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x000277A8 File Offset: 0x000267A8
		public Color RightBorderColor
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
				return this.ᜁ().RightBorderColor;
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
				this.BeginUpdate();
				this.ᜁ().RightBorderColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000277FC File Offset: 0x000267FC
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00027844 File Offset: 0x00026844
		public LineStyleType RightBorderStyle
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
				return this.ᜁ().RightBorderStyle;
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
				this.BeginUpdate();
				this.ᜁ().RightBorderStyle = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00027898 File Offset: 0x00026898
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000278E0 File Offset: 0x000268E0
		public ExcelColors TopBorderKnownColor
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
				return this.ᜁ().TopBorderKnownColor;
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
				this.BeginUpdate();
				this.ᜁ().TopBorderKnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00027934 File Offset: 0x00026934
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0002797C File Offset: 0x0002697C
		public Color TopBorderColor
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
				return this.ᜁ().TopBorderColor;
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
				this.BeginUpdate();
				this.ᜁ().TopBorderColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000279D0 File Offset: 0x000269D0
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00027A18 File Offset: 0x00026A18
		public LineStyleType TopBorderStyle
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
				return this.ᜁ().TopBorderStyle;
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
				this.BeginUpdate();
				this.ᜁ().TopBorderStyle = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00027A6C File Offset: 0x00026A6C
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00027AB4 File Offset: 0x00026AB4
		public ExcelColors BottomBorderKnownColor
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
				return this.ᜁ().BottomBorderKnownColor;
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
				this.BeginUpdate();
				this.ᜁ().BottomBorderKnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00027B08 File Offset: 0x00026B08
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00027B50 File Offset: 0x00026B50
		public Color BottomBorderColor
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
				return this.ᜁ().BottomBorderColor;
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
				this.BeginUpdate();
				this.ᜁ().BottomBorderColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00027BA4 File Offset: 0x00026BA4
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00027BEC File Offset: 0x00026BEC
		public LineStyleType BottomBorderStyle
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
				return this.ᜁ().BottomBorderStyle;
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
				this.BeginUpdate();
				this.ᜁ().BottomBorderStyle = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00027C40 File Offset: 0x00026C40
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00027C88 File Offset: 0x00026C88
		public string FirstFormula
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
				return this.ᜁ().FirstFormula;
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
				this.BeginUpdate();
				this.ᜁ().FirstFormula = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00027CDC File Offset: 0x00026CDC
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00027D24 File Offset: 0x00026D24
		public string FirstFormulaR1C1
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
				return this.ᜁ().FirstFormulaR1C1;
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
				this.BeginUpdate();
				this.ᜁ().Range = this.ᜅ;
				this.ᜁ().FirstFormulaR1C1 = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00027D88 File Offset: 0x00026D88
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00027DD0 File Offset: 0x00026DD0
		public string SecondFormula
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
				return this.ᜁ().SecondFormula;
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
				this.BeginUpdate();
				this.ᜁ().SecondFormula = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00027E24 File Offset: 0x00026E24
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00027E6C File Offset: 0x00026E6C
		public string SecondFormulaR1C1
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
				return this.ᜁ().SecondFormulaR1C1;
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
				this.BeginUpdate();
				this.ᜁ().Range = this.ᜅ;
				this.ᜁ().SecondFormulaR1C1 = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00027ED0 File Offset: 0x00026ED0
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00027F18 File Offset: 0x00026F18
		public ExcelColors KnownColor
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
				return this.ᜁ().KnownColor;
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
				this.BeginUpdate();
				this.ᜁ().KnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00027F6C File Offset: 0x00026F6C
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00027FB4 File Offset: 0x00026FB4
		public Color Color
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
				return this.ᜁ().Color;
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
				this.BeginUpdate();
				this.ᜁ().Color = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00028008 File Offset: 0x00027008
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00028050 File Offset: 0x00027050
		public ExcelColors BackKnownColor
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
				return this.ᜁ().BackKnownColor;
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
				this.BeginUpdate();
				this.ᜁ().BackKnownColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000280A4 File Offset: 0x000270A4
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x000280EC File Offset: 0x000270EC
		public Color BackColor
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
				return this.ᜁ().BackColor;
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
				this.BeginUpdate();
				this.ᜁ().BackColor = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00028140 File Offset: 0x00027140
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00028188 File Offset: 0x00027188
		public ExcelPatternType FillPattern
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
				return this.ᜁ().FillPattern;
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
				this.BeginUpdate();
				this.ᜁ().FillPattern = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000281DC File Offset: 0x000271DC
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00028224 File Offset: 0x00027224
		public bool IsSuperScript
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
				return this.ᜁ().IsSuperScript;
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
				this.BeginUpdate();
				this.ᜁ().IsSuperScript = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00028278 File Offset: 0x00027278
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x000282C0 File Offset: 0x000272C0
		public bool IsSubScript
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
				return this.ᜁ().IsSubScript;
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
				this.BeginUpdate();
				this.ᜁ().IsSubScript = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00028314 File Offset: 0x00027314
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0002835C File Offset: 0x0002735C
		public bool IsFontFormatPresent
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
				return this.ᜁ().IsFontFormatPresent;
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
				this.BeginUpdate();
				this.ᜁ().IsFontFormatPresent = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000283B0 File Offset: 0x000273B0
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000283F8 File Offset: 0x000273F8
		public bool IsBorderFormatPresent
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
				return this.ᜁ().IsBorderFormatPresent;
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
				this.BeginUpdate();
				this.ᜁ().IsBorderFormatPresent = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0002844C File Offset: 0x0002744C
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00028494 File Offset: 0x00027494
		public bool IsPatternFormatPresent
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
				return this.ᜁ().IsPatternFormatPresent;
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
				this.BeginUpdate();
				this.ᜁ().IsPatternFormatPresent = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000284E8 File Offset: 0x000274E8
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00028530 File Offset: 0x00027530
		public bool IsFontColorPresent
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
				return this.ᜁ().IsFontColorPresent;
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
				this.BeginUpdate();
				this.ᜁ().IsFontColorPresent = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00028584 File Offset: 0x00027584
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000285CC File Offset: 0x000275CC
		public bool IsPatternColorPresent
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
				return this.ᜁ().IsPatternColorPresent;
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
				this.BeginUpdate();
				this.ᜁ().IsPatternColorPresent = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00028620 File Offset: 0x00027620
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00028668 File Offset: 0x00027668
		public bool IsBackgroundColorPresent
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
				return this.ᜁ().IsBackgroundColorPresent;
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
				this.BeginUpdate();
				this.ᜁ().IsBackgroundColorPresent = value;
				this.EndUpdate();
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000286BC File Offset: 0x000276BC
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00028704 File Offset: 0x00027704
		public bool IsLeftBorderModified
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
				return this.ᜁ().IsLeftBorderModified;
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
				this.BeginUpdate();
				this.ᜁ().IsLeftBorderModified = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00028758 File Offset: 0x00027758
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x000287A0 File Offset: 0x000277A0
		public bool IsRightBorderModified
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
				return this.ᜁ().IsRightBorderModified;
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
				this.BeginUpdate();
				this.ᜁ().IsRightBorderModified = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000287F4 File Offset: 0x000277F4
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0002883C File Offset: 0x0002783C
		public bool IsTopBorderModified
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
				return this.ᜁ().IsTopBorderModified;
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
				this.BeginUpdate();
				this.ᜁ().IsTopBorderModified = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00028890 File Offset: 0x00027890
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000288D8 File Offset: 0x000278D8
		public bool IsBottomBorderModified
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
				return this.ᜁ().IsBottomBorderModified;
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
				this.BeginUpdate();
				this.ᜁ().IsBottomBorderModified = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0002892C File Offset: 0x0002792C
		public DataBar DataBar
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A6;
					case 1:
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
							break;
						}
						num = 2;
						continue;
					case 2:
						if (this.ᜂ == null)
						{
							num = 4;
							continue;
						}
						goto IL_C8;
					case 4:
						this.ᜂ = new spr\u2181(this.ᜁ().DataBar.Wrapped as spr\u24CD, this);
						num = 0;
						continue;
					case 5:
						goto IL_78;
					}
					if (this.FormatType == ConditionalFormatType.DataBar)
					{
						num = 1;
					}
					else
					{
						this.ᜂ = null;
						num = 5;
					}
				}
				IL_78:
				IL_A6:
				IL_C8:
				return new DataBar(this.ᜂ);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00028A0C File Offset: 0x00027A0C
		public IconSet IconSet
		{
			get
			{
				if (true)
				{
				}
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ == null)
						{
							num = 1;
							continue;
						}
						goto IL_B0;
					case 1:
						this.ᜃ = new sprᲁ(this);
						num = 2;
						continue;
					case 2:
						goto IL_8E;
					case 4:
						goto IL_78;
					case 5:
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
						num = 0;
						continue;
					}
					if (this.FormatType == ConditionalFormatType.IconSet)
					{
						num = 5;
					}
					else
					{
						this.ᜃ = null;
						num = 4;
					}
				}
				IL_78:
				IL_8E:
				IL_B0:
				return new IconSet(this.ᜃ);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00028AD4 File Offset: 0x00027AD4
		public ColorScale ColorScale
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_91;
					case 1:
						this.ᜄ = new spr\u2073(this);
						num = 0;
						continue;
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
							break;
						}
						num = 5;
						continue;
					case 4:
						goto IL_78;
					case 5:
						if (this.ᜄ == null)
						{
							num = 1;
							continue;
						}
						goto IL_B3;
					}
					if (this.FormatType == ConditionalFormatType.ColorScale)
					{
						num = 2;
					}
					else
					{
						this.ᜄ = null;
						num = 4;
					}
				}
				IL_78:
				IL_91:
				IL_B3:
				return new ColorScale(this.ᜄ);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00028BA0 File Offset: 0x00027BA0
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x00028BE4 File Offset: 0x00027BE4
		internal IXLSRange Range
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜅ = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00028C28 File Offset: 0x00027C28
		internal spr\u1DF5 ReservedHandle
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
				return this.ᜀ.ReservedHandle;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00028C70 File Offset: 0x00027C70
		public object Parent
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
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00028CB4 File Offset: 0x00027CB4
		public override void BeginUpdate()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_36;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_55;
				}
				if (base.BeginCallsCount == 0)
				{
					num = 0;
					continue;
				}
				goto IL_55;
				IL_36:
				this.ᜀ.BeginUpdate();
				num = 2;
				continue;
				IL_55:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_36;
				default:
					goto IL_6B;
				}
			}
			IL_6B:
			if (false)
			{
			}
			base.BeginUpdate();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00028D38 File Offset: 0x00027D38
		public override void EndUpdate()
		{
			for (;;)
			{
				IL_14:
				base.EndUpdate();
				for (;;)
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_51;
						case 1:
							if (base.BeginCallsCount == 0)
							{
								num = 2;
								continue;
							}
							goto IL_6F;
						case 2:
							this.ᜀ.EndUpdate();
							num = 0;
							continue;
						}
						goto IL_14;
					}
					IL_51:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_67;
					}
				}
			}
			IL_67:
			if (false)
			{
			}
			IL_6F:
			if (true)
			{
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00028DBC File Offset: 0x00027DBC
		internal XlsConditionalFormat ᜁ()
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
			return this.ᜀ.GetCondition(this.ᜁ);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00028E08 File Offset: 0x00027E08
		public OColor OColor
		{
			get
			{
				int a_ = 7;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00028E60 File Offset: 0x00027E60
		public OColor BackColorObject
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00028EB8 File Offset: 0x00027EB8
		public OColor TopBorderColorObject
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00028F10 File Offset: 0x00027F10
		public OColor BottomBorderColorObject
		{
			get
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00028F68 File Offset: 0x00027F68
		public OColor LeftBorderColorObject
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00028FC0 File Offset: 0x00027FC0
		public OColor RightBorderColorObject
		{
			get
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
				throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00029018 File Offset: 0x00028018
		public OColor FontColorObject
		{
			get
			{
				int a_ = 7;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00029070 File Offset: 0x00028070
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000290B8 File Offset: 0x000280B8
		public bool IsPatternStyleModified
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
				return this.ᜁ().IsPatternStyleModified;
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
				this.BeginUpdate();
				this.ᜁ().IsPatternStyleModified = value;
				this.EndUpdate();
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0002910C File Offset: 0x0002810C
		Ptg[] sprᲖ.FirstFormulaPtgs
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
				return ((sprᲖ)this.ᜁ()).ᜌ();
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00029154 File Offset: 0x00028154
		Ptg[] sprᲖ.SecondFormulaPtgs
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
				return ((sprᲖ)this.ᜁ()).\u170D();
			}
		}

		// Token: 0x040000CD RID: 205
		private CondFormatCollectionWrapper ᜀ;

		// Token: 0x040000CE RID: 206
		private long[] \u2460\u00A3\u0094\u0087;

		// Token: 0x040000CF RID: 207
		private int ᜁ;

		// Token: 0x040000D0 RID: 208
		private long[] \u2609\u0091\u008D\u0091;

		// Token: 0x040000D1 RID: 209
		private spr\u2181 ᜂ;

		// Token: 0x040000D2 RID: 210
		private string \u2460\u00A5\u0080\u0095;

		// Token: 0x040000D3 RID: 211
		private sprᲁ ᜃ;

		// Token: 0x040000D4 RID: 212
		private float \u25D9\u00A8\u00A1\u00A1;

		// Token: 0x040000D5 RID: 213
		private spr\u2073 ᜄ;

		// Token: 0x040000D6 RID: 214
		private IXLSRange ᜅ;
	}
}

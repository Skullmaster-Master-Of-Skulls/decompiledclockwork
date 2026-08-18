using System;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000164 RID: 356
	public class XlsConditionalFormat : XlsObject, sprᲖ, ICloneParent
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x000A2EC0 File Offset: 0x000A1EC0
		internal XlsConditionalFormat(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ();
			this.ᜁ = (spr\u206F)spr\u175E.ᜀ(TBIFFRecord.CF);
			this.ᜌ();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000A2EF8 File Offset: 0x000A1EF8
		internal XlsConditionalFormat(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, ref int A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000A2F18 File Offset: 0x000A1F18
		internal XlsConditionalFormat(spr\u1DF5 A_0, object A_1, spr\u206F A_2) : this(A_0, A_1)
		{
			this.ᜁ = (spr\u206F)A_2.Clone();
			this.ᜉ();
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000A2F44 File Offset: 0x000A1F44
		internal void ᜀ(BiffRecordRaw[] A_0, ref int A_1)
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
			A_0[A_1].CheckTypeCode(TBIFFRecord.CF);
			this.ᜁ = (spr\u206F)A_0[A_1];
			this.ᜉ();
			A_1++;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000A2FA8 File Offset: 0x000A1FA8
		private void ᜌ()
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
			this.ᜃ = new OColor(ExcelColors.BlackCustom);
			this.ᜃ.AfterChange += this.ᜇ;
			this.ᜄ = new OColor((ExcelColors)65);
			this.ᜄ.AfterChange += this.ᜆ;
			this.ᜅ = new OColor(ExcelColors.Black);
			this.ᜅ.AfterChange += this.ᜃ;
			this.ᜆ = new OColor(ExcelColors.Black);
			this.ᜆ.AfterChange += this.ᜂ;
			this.ᜇ = new OColor(ExcelColors.Black);
			this.ᜇ.AfterChange += this.ᜅ;
			this.ᜈ = new OColor(ExcelColors.Black);
			this.ᜈ.AfterChange += this.ᜄ;
			this.ᜉ = new OColor(ExcelColors.Black);
			this.ᜉ.AfterChange += this.ᜈ;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000A30DC File Offset: 0x000A20DC
		private void ᜋ()
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
			this.ᜃ.ᜀ((ExcelColors)this.ᜁ.\u1713());
			this.ᜄ.ᜀ((ExcelColors)this.ᜁ.\u1714());
			this.ᜅ.ᜀ((ExcelColors)this.ᜁ.ᜂ());
			this.ᜆ.ᜀ((ExcelColors)this.ᜁ.\u1716());
			this.ᜇ.ᜀ((ExcelColors)this.ᜁ.ᜁ());
			this.ᜈ.ᜀ((ExcelColors)this.ᜁ.ᜋ());
			this.ᜉ.ᜀ((ExcelColors)this.ᜁ.\u1715());
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000A31B4 File Offset: 0x000A21B4
		private void ᜊ()
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

		// Token: 0x0600104A RID: 4170 RVA: 0x000A31F0 File Offset: 0x000A21F0
		private void ᜉ()
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
			this.ᜋ();
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000A3234 File Offset: 0x000A2234
		public void SerializeDataToList(RecordArrayList records)
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
			this.ᜀ();
			records.ᜀ(this.ᜁ);
			this.ᜊ();
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000A3288 File Offset: 0x000A2288
		private void ᜈ()
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
			this.ᜁ.ᜁ((uint)((ushort)this.ᜉ.ᜂ(this.ᜂ)));
			this.ᜁ.ᜉ(true);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000A32EC File Offset: 0x000A22EC
		private void ᜇ()
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
			this.ᜁ.ᜂ((ushort)this.ᜃ.ᜂ(this.ᜂ));
			this.ᜁ.ᜊ(true);
			this.ᜁ.ᜁ(true);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000A335C File Offset: 0x000A235C
		private void ᜆ()
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
			this.ᜁ.ᜀ((ushort)this.ᜄ.ᜂ(this.ᜂ));
			this.ᜁ.ᜋ(true);
			this.ᜁ.ᜁ(true);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000A33CC File Offset: 0x000A23CC
		private void ᜅ()
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
			this.ᜁ.ᜂ((uint)this.ᜇ.ᜂ(this.ᜂ));
			this.IsLeftBorderModified = true;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000A342C File Offset: 0x000A242C
		private void ᜄ()
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
			this.ᜁ.ᜀ((uint)this.ᜈ.ᜂ(this.ᜂ));
			this.IsRightBorderModified = true;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000A348C File Offset: 0x000A248C
		private void ᜃ()
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
			this.ᜁ.ᜄ((uint)this.ᜅ.ᜂ(this.ᜂ));
			this.IsTopBorderModified = true;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000A34EC File Offset: 0x000A24EC
		private void ᜂ()
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
			this.ᜁ.ᜅ((uint)this.ᜆ.ᜂ(this.ᜂ));
			this.IsBottomBorderModified = true;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000A354C File Offset: 0x000A254C
		internal void ᜀ(bool[] A_0)
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
			FormulaUtil.ᜀ(this.ᜁ.\u171D(), A_0);
			FormulaUtil.ᜀ(this.ᜁ.ᜊ(), A_0);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000A35AC File Offset: 0x000A25AC
		internal void ᜀ(int[] A_0)
		{
			for (;;)
			{
				Ptg[] a_ = this.ᜁ.\u171D();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.ᜁ.ᜀ(a_);
						goto IL_C7;
					case 1:
						this.ᜁ.ᜁ(a_);
						num = 3;
						continue;
					case 2:
						if (!FormulaUtil.ᜀ(a_, A_0))
						{
							goto IL_8E;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C7;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 3:
						return;
					case 4:
						if (FormulaUtil.ᜀ(a_, A_0))
						{
							num = 1;
							continue;
						}
						return;
					case 5:
						goto IL_8E;
					}
					break;
					IL_8E:
					a_ = this.ᜁ.ᜊ();
					num = 4;
					continue;
					IL_C7:
					num = 5;
				}
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000A3690 File Offset: 0x000A2690
		private void ᜁ()
		{
			int a_ = 16;
			if (true)
			{
			}
			object obj = base.FindParent(typeof(XlsWorkbook));
			if (obj == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_3D;
					}
				}
				IL_3D:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("ᙅ⥇㡉⥋⁍⑏牑㭓㑕㉗㽙㽛⩝䁟šգࡥ٧թᡫ乭ቯ᝱味ၵ᝷ཹቻ᩽깿", a_));
			}
			this.ᜂ = (XlsWorkbook)obj;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x000A370C File Offset: 0x000A270C
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x000A3758 File Offset: 0x000A2758
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
				return this.ᜇ.ᜂ(this.ᜂ);
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
				value = XlsBorder.ColorToExcelColor(value);
				this.ᜇ.SetKnownColor(value);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000A37A8 File Offset: 0x000A27A8
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x000A37F4 File Offset: 0x000A27F4
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
				return this.ᜇ.ᜁ(this.ᜂ);
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
				this.ᜇ.ᜀ(value, this.ᜂ);
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000A3844 File Offset: 0x000A2844
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x000A388C File Offset: 0x000A288C
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
				return this.ᜁ.ᜎ();
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
				this.ᜁ.ᜃ(value);
				this.IsLeftBorderModified = true;
				this.ᜁ.ᜀ(true);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000A38E8 File Offset: 0x000A28E8
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x000A3934 File Offset: 0x000A2934
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
				return this.ᜈ.ᜂ(this.ᜂ);
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
				value = XlsBorder.ColorToExcelColor(value);
				this.ᜈ.SetKnownColor(value);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x000A3984 File Offset: 0x000A2984
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x000A39D0 File Offset: 0x000A29D0
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
				return this.ᜈ.ᜁ(this.ᜂ);
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
				this.ᜈ.ᜀ(value, this.ᜂ);
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x000A3A20 File Offset: 0x000A2A20
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x000A3A68 File Offset: 0x000A2A68
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
				return this.ᜁ.\u1717();
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
				this.ᜁ.ᜀ(value);
				this.IsRightBorderModified = true;
				this.ᜁ.ᜀ(true);
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000A3AC4 File Offset: 0x000A2AC4
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x000A3B10 File Offset: 0x000A2B10
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
				return this.ᜅ.ᜂ(this.ᜂ);
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
				value = XlsBorder.ColorToExcelColor(value);
				this.ᜅ.SetKnownColor(value);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x000A3B60 File Offset: 0x000A2B60
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x000A3BAC File Offset: 0x000A2BAC
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
				return this.ᜅ.ᜁ(this.ᜂ);
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
				this.ᜅ.ᜀ(value, this.ᜂ);
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x000A3BFC File Offset: 0x000A2BFC
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x000A3C44 File Offset: 0x000A2C44
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
				return this.ᜁ.ᜡ();
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
				this.ᜁ.ᜂ(value);
				this.IsTopBorderModified = true;
				this.ᜁ.ᜀ(true);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x000A3CA0 File Offset: 0x000A2CA0
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x000A3CEC File Offset: 0x000A2CEC
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
				return this.ᜆ.ᜂ(this.ᜂ);
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
				value = XlsBorder.ColorToExcelColor(value);
				this.ᜆ.SetKnownColor(value);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x000A3D3C File Offset: 0x000A2D3C
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x000A3D88 File Offset: 0x000A2D88
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
				return this.ᜆ.ᜁ(this.ᜂ);
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
				this.ᜆ.ᜀ(value, this.ᜂ);
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000A3DD8 File Offset: 0x000A2DD8
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x000A3E20 File Offset: 0x000A2E20
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
				return this.ᜁ.ᜄ();
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
				this.ᜁ.ᜁ(value);
				this.IsBottomBorderModified = true;
				this.ᜁ.ᜀ(true);
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000A3E7C File Offset: 0x000A2E7C
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x000A3ECC File Offset: 0x000A2ECC
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
				return this.ᜀ(this.ᜂ.FormulaUtil, true);
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						XlsValidation.ᜀ(true);
						XlsValidation.ᜀ(true);
						Ptg[] a_ = this.ᜂ.FormulaUtil.ᜃ(value);
						XlsValidation.ᜀ(false);
						XlsValidation.ᜀ(false);
						this.ᜁ.ᜀ(a_);
						num = 0;
						continue;
					}
					case 2:
						if (this.FirstFormula != value)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						goto IL_80;
					case 5:
						value = value.Substring(1);
						goto IL_D6;
					}
					if (value[0] == '=')
					{
						num = 5;
						continue;
					}
					IL_80:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_D6:
						num = 3;
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
				}
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000A3FC8 File Offset: 0x000A2FC8
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x000A402C File Offset: 0x000A302C
		public string FirstFormulaR1C1
		{
			get
			{
				Ptg[] array = this.ᜁ.\u171D();
				if (array == null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_27;
						}
					}
					IL_27:
					if (true)
					{
					}
					if (false)
					{
					}
					return null;
				}
				return this.ᜂ.FormulaUtil.ᜀ(array, 0, 0, true, false);
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						value = value.Substring(1);
						goto IL_F4;
					case 2:
					{
						XlsValidation.ᜀ(true);
						Ptg[] a_ = this.ᜂ.FormulaUtil.ᜀ(value, this.\u170D.Worksheet, null, this.\u170D.Row - 1, this.\u170D.Column - 1, true);
						XlsValidation.ᜀ(false);
						this.ᜁ.ᜀ(a_);
						num = 0;
						continue;
					}
					case 4:
						goto IL_9E;
					case 5:
						if (this.FirstFormulaR1C1 != value)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (value[0] == '=')
					{
						num = 1;
						continue;
					}
					IL_9E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_F4:
						num = 4;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x000A4144 File Offset: 0x000A3144
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x000A4194 File Offset: 0x000A3194
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
				return this.ᜀ(this.ᜂ.FormulaUtil, false);
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.SecondFormula != value)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CD;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							Ptg[] a_ = this.ᜂ.FormulaUtil.ᜃ(value);
							this.ᜁ.ᜁ(a_);
							num = 0;
							continue;
						}
						}
						break;
					case 3:
						value = value.Substring(1);
						num = 5;
						continue;
					case 5:
						goto IL_CD;
					}
					if (value[0] == '=')
					{
						num = 3;
						continue;
					}
					IL_93:
					num = 1;
					continue;
					IL_CD:
					goto IL_93;
				}
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000A4270 File Offset: 0x000A3270
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x000A42D4 File Offset: 0x000A32D4
		public string SecondFormulaR1C1
		{
			get
			{
				Ptg[] array = this.ᜁ.ᜊ();
				if (array == null)
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
						return null;
					}
				}
				return this.ᜂ.FormulaUtil.ᜀ(array, 0, 0, true, false);
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						value = value.Substring(1);
						num = 4;
						continue;
					case 2:
						if (this.SecondFormulaR1C1 != value)
						{
							num = 5;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						goto IL_100;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_100;
						default:
						{
							if (false)
							{
							}
							Ptg[] a_ = this.ᜂ.FormulaUtil.ᜀ(value, this.\u170D.Worksheet, null, this.\u170D.Row - 1, this.\u170D.Column - 1, true);
							this.ᜁ.ᜁ(a_);
							num = 3;
							continue;
						}
						}
						break;
					}
					if (true)
					{
					}
					if (value[0] == '=')
					{
						num = 1;
						continue;
					}
					IL_C3:
					num = 2;
					continue;
					IL_100:
					goto IL_C3;
				}
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000A43E4 File Offset: 0x000A33E4
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x000A442C File Offset: 0x000A342C
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
				return this.ᜁ.ᜢ();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch (value)
						{
						case ConditionalFormatType.CellValue:
							goto IL_100;
						case ConditionalFormatType.Formula:
							goto IL_4B;
						case ConditionalFormatType.DataBar:
							goto IL_F4;
						case ConditionalFormatType.IconSet:
							this.ᜋ = new spr\u2345();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case ConditionalFormatType.ColorScale:
							goto IL_3F;
						default:
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					case 3:
						goto IL_83;
					case 4:
						this.ᜁ.ᜀ(value);
						this.ᜊ = null;
						this.ᜋ = null;
						this.ᜌ = null;
						num = 0;
						continue;
					}
					if (this.ᜁ.ᜢ() == value)
					{
						return;
					}
					num = 4;
				}
				IL_3F:
				this.ᜌ = new sprᝠ();
				return;
				IL_4B:
				this.Operator = ComparisonOperatorType.None;
				return;
				IL_83:
				return;
				IL_F4:
				this.ᜊ = new spr\u24CD();
				return;
				IL_100:
				this.Operator = ComparisonOperatorType.Between;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x000A4544 File Offset: 0x000A3544
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x000A458C File Offset: 0x000A358C
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
				return this.ᜁ.ᜨ();
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
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x000A45D4 File Offset: 0x000A35D4
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x000A4624 File Offset: 0x000A3624
		public bool IsBold
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
				return this.ᜁ.\u171F() >= 700;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ.ᜁ(700);
						num = 2;
						continue;
					case 2:
						goto IL_85;
					case 3:
						goto IL_6B;
					}
					if (!value)
					{
						this.ᜁ.ᜁ(400);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
					}
					num = 0;
				}
				IL_6B:
				IL_85:
				if (true)
				{
				}
				this.ᜁ.ᜉ(true);
				this.ᜁ.ᜃ(true);
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000A46D8 File Offset: 0x000A36D8
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x000A4720 File Offset: 0x000A3720
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
				return this.ᜁ.ᜬ();
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
				this.ᜁ.ᜂ(value);
				this.ᜁ.ᜉ(true);
				this.ᜁ.ᜃ(true);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000A4780 File Offset: 0x000A3780
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x000A47CC File Offset: 0x000A37CC
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
				return this.ᜉ.ᜂ(this.ᜂ);
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
				this.ᜉ.SetKnownColor(value);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x000A4814 File Offset: 0x000A3814
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x000A4860 File Offset: 0x000A3860
		public Color FontColor
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
				return this.ᜉ.ᜁ(this.ᜂ);
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
				this.ᜉ.ᜀ(value);
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x000A48A8 File Offset: 0x000A38A8
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x000A48F0 File Offset: 0x000A38F0
		public FontUnderlineType Underline
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
				return this.ᜁ.ᜧ();
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
				this.ᜁ.ᜀ(value);
				this.ᜁ.ᜉ(true);
				this.ᜁ.ᜆ(true);
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x000A4950 File Offset: 0x000A3950
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x000A4998 File Offset: 0x000A3998
		public bool IsStrikeThrough
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
				return this.ᜁ.ᜥ();
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
				this.ᜁ.ᜅ(value);
				this.ᜁ.ᜉ(true);
				this.ᜁ.ᜎ(true);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x000A49F8 File Offset: 0x000A39F8
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x000A4A44 File Offset: 0x000A3A44
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
				return this.ᜁ.ᜉ() == FontVertialAlignmentType.Superscript;
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
							goto IL_AD;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜁ.ᜀ(FontVertialAlignmentType.Baseline);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_6D;
					case 3:
						if (this.IsSuperScript)
						{
							num = 0;
							continue;
						}
						goto IL_AF;
					case 4:
						goto IL_AD;
					case 5:
						this.ᜁ.ᜀ(FontVertialAlignmentType.Superscript);
						num = 4;
						continue;
					}
					if (value)
					{
						num = 5;
					}
					else
					{
						num = 3;
					}
				}
				IL_6D:
				IL_AD:
				IL_AF:
				this.ᜁ.ᜏ(true);
				this.ᜁ.ᜉ(true);
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000A4B18 File Offset: 0x000A3B18
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x000A4B64 File Offset: 0x000A3B64
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
				return this.ᜁ.ᜉ() == FontVertialAlignmentType.Subscript;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ.ᜀ(FontVertialAlignmentType.Subscript);
						num = 3;
						continue;
					case 2:
						goto IL_65;
					case 3:
						goto IL_AA;
					case 4:
						if (this.IsSubScript)
						{
							num = 5;
							continue;
						}
						goto IL_AC;
					case 5:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AA;
						default:
							if (false)
							{
							}
							this.ᜁ.ᜀ(FontVertialAlignmentType.Baseline);
							num = 2;
							continue;
						}
						break;
					}
					if (value)
					{
						num = 0;
					}
					else
					{
						num = 4;
					}
				}
				IL_65:
				IL_AA:
				IL_AC:
				this.ᜁ.ᜏ(true);
				this.ᜁ.ᜉ(true);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x000A4C38 File Offset: 0x000A3C38
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x000A4C84 File Offset: 0x000A3C84
		public ExcelColors KnownColor
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
				return this.ᜃ.ᜂ(this.ᜂ);
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
				this.ᜃ.SetKnownColor(value);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x000A4CCC File Offset: 0x000A3CCC
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x000A4D18 File Offset: 0x000A3D18
		public Color Color
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
				return this.ᜃ.ᜁ(this.ᜂ);
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
				this.ᜃ.ᜀ(value, this.ᜂ);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000A4D68 File Offset: 0x000A3D68
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x000A4DB4 File Offset: 0x000A3DB4
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
				return this.ᜄ.ᜂ(this.ᜂ);
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
				this.ᜄ.SetKnownColor(value);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000A4DFC File Offset: 0x000A3DFC
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x000A4E48 File Offset: 0x000A3E48
		public Color BackColor
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
				return this.ᜄ.ᜁ(this.ᜂ);
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
				this.ᜄ.ᜀ(value, this.ᜂ);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x000A4E98 File Offset: 0x000A3E98
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x000A4EE0 File Offset: 0x000A3EE0
		public ExcelPatternType FillPattern
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
				return this.ᜁ.\u171B();
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
							goto IL_3C;
						default:
							goto IL_88;
						}
						break;
					case 2:
						if (true)
						{
						}
						goto IL_3C;
					}
					if (value != this.ᜁ.\u171B())
					{
						num = 2;
						continue;
					}
					return;
					IL_3C:
					this.ᜁ.ᜀ(value);
					this.ᜁ.ᜇ(true);
					this.ᜁ.ᜁ(true);
					num = 1;
				}
				IL_88:
				if (false)
				{
				}
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x000A4F80 File Offset: 0x000A3F80
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x000A4FC8 File Offset: 0x000A3FC8
		public bool IsFontFormatPresent
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
				return this.ᜁ.ᜑ();
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
				this.ᜁ.ᜉ(value);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x000A5010 File Offset: 0x000A4010
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x000A5058 File Offset: 0x000A4058
		public bool IsBorderFormatPresent
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
				return this.ᜁ.ᜅ();
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
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000A50A0 File Offset: 0x000A40A0
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x000A50E8 File Offset: 0x000A40E8
		public bool IsPatternFormatPresent
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
				return this.ᜁ.\u1718();
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
				this.ᜁ.ᜁ(value);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x000A5130 File Offset: 0x000A4130
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x000A517C File Offset: 0x000A417C
		public bool IsFontColorPresent
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
				return this.ᜁ.\u1715() != uint.MaxValue;
			}
			set
			{
				if (value)
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
						this.ᜁ.ᜁ(0U);
						return;
					}
				}
				this.ᜁ.ᜁ(uint.MaxValue);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x000A51D8 File Offset: 0x000A41D8
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x000A5220 File Offset: 0x000A4220
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
				return this.ᜁ.\u171E();
			}
			set
			{
				for (;;)
				{
					IL_38:
					this.ᜁ.ᜊ(value);
					int num = 1;
					for (;;)
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
							switch (num)
							{
							case 0:
								return;
							case 1:
								if (value)
								{
									goto IL_4F;
								}
								return;
							case 2:
								this.ᜁ.ᜁ(value);
								num = 0;
								continue;
							}
							goto IL_38;
						}
						IL_4F:
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x000A52A8 File Offset: 0x000A42A8
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x000A52F0 File Offset: 0x000A42F0
		public bool IsBackgroundColorPresent
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
				return this.ᜁ.ᜐ();
			}
			set
			{
				for (;;)
				{
					IL_38:
					this.ᜁ.ᜋ(value);
					int num = 2;
					for (;;)
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
							switch (num)
							{
							case 0:
								this.ᜁ.ᜁ(value);
								num = 1;
								continue;
							case 1:
								return;
							case 2:
								if (value)
								{
									goto IL_4F;
								}
								return;
							}
							goto IL_38;
						}
						IL_4F:
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x000A5378 File Offset: 0x000A4378
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x000A53C0 File Offset: 0x000A43C0
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
				return this.ᜁ.\u171A();
			}
			set
			{
				for (;;)
				{
					IL_30:
					if (true)
					{
					}
					this.ᜁ.ᜄ(value);
					int num = 0;
					for (;;)
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
							switch (num)
							{
							case 0:
								if (value)
								{
									goto IL_4F;
								}
								return;
							case 1:
								this.ᜁ.ᜀ(value);
								num = 2;
								continue;
							case 2:
								return;
							}
							goto IL_30;
						}
						IL_4F:
						num = 1;
					}
				}
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000A5448 File Offset: 0x000A4448
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x000A5490 File Offset: 0x000A4490
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
				return this.ᜁ.ᜦ();
			}
			set
			{
				for (;;)
				{
					IL_30:
					this.ᜁ.ᜈ(value);
					if (true)
					{
					}
					int num = 1;
					for (;;)
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
							switch (num)
							{
							case 0:
								return;
							case 1:
								if (value)
								{
									goto IL_4F;
								}
								return;
							case 2:
								this.ᜁ.ᜀ(value);
								num = 0;
								continue;
							}
							goto IL_30;
						}
						IL_4F:
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x000A5518 File Offset: 0x000A4518
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x000A5560 File Offset: 0x000A4560
		public bool IsTopBorderModified
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
				return this.ᜁ.\u1719();
			}
			set
			{
				for (;;)
				{
					IL_30:
					this.ᜁ.ᜌ(value);
					int num = 2;
					for (;;)
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
							switch (num)
							{
							case 0:
								this.ᜁ.ᜀ(value);
								num = 1;
								continue;
							case 1:
								return;
							case 2:
								if (true)
								{
								}
								if (value)
								{
									goto IL_4F;
								}
								return;
							}
							goto IL_30;
						}
						IL_4F:
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x000A55E8 File Offset: 0x000A45E8
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x000A5630 File Offset: 0x000A4630
		public bool IsBottomBorderModified
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
				return this.ᜁ.ᜆ();
			}
			set
			{
				for (;;)
				{
					IL_30:
					this.ᜁ.\u170D(value);
					if (true)
					{
					}
					int num = 0;
					for (;;)
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
							switch (num)
							{
							case 0:
								if (value)
								{
									goto IL_4F;
								}
								return;
							case 1:
								return;
							case 2:
								this.ᜁ.ᜀ(value);
								num = 1;
								continue;
							}
							goto IL_30;
						}
						IL_4F:
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x000A56B8 File Offset: 0x000A46B8
		public DataBar DataBar
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
				return new DataBar(this.ᜊ);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000A5700 File Offset: 0x000A4700
		public IconSet IconSet
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
				return new IconSet(this.ᜋ);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x000A5748 File Offset: 0x000A4748
		public ColorScale ColorScale
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
				return new ColorScale(this.ᜌ);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x000A5790 File Offset: 0x000A4790
		internal spr\u206F Record
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
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x000A57D4 File Offset: 0x000A47D4
		internal XlsWorkbook Workbook
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
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000A5818 File Offset: 0x000A4818
		internal spr\u24CD InnerDataBar
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
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x000A585C File Offset: 0x000A485C
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x000A58A0 File Offset: 0x000A48A0
		internal IXLSRange Range
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u170D = value;
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000A58E4 File Offset: 0x000A48E4
		internal void ᜀ(FormulaUtil A_0, string A_1, bool A_2)
		{
			Ptg[] a_ = A_0.ᜃ(A_1);
			if (A_2)
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
					this.ᜁ.ᜀ(a_);
					return;
				}
			}
			this.ᜁ.ᜁ(a_);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000A5948 File Offset: 0x000A4948
		internal string ᜀ(FormulaUtil A_0, bool A_1)
		{
			int num = 10;
			XlsConditionalFormats xlsConditionalFormats;
			Ptg[] array2;
			for (;;)
			{
				Ptg[] array;
				switch (num)
				{
				case 0:
					goto IL_DE;
				case 1:
					array = this.ᜁ.\u171D();
					goto IL_126;
				case 2:
					num = 11;
					continue;
				case 3:
					goto IL_143;
				case 4:
					if (xlsConditionalFormats != null)
					{
						num = 9;
						continue;
					}
					goto IL_10E;
				case 5:
					goto IL_119;
				case 6:
					goto IL_10E;
				case 7:
					if (xlsConditionalFormats.CellRectangles.Count <= 0)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 8:
					if (array2 == null)
					{
						goto IL_138;
					}
					xlsConditionalFormats = (base.Parent as XlsConditionalFormats);
					num = 4;
					continue;
				case 9:
					num = 7;
					continue;
				case 11:
					array = this.ᜁ.ᜊ();
					goto IL_126;
				}
				if (true)
				{
				}
				if (A_1)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_138;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				IL_10E:
				num = 5;
				continue;
				IL_126:
				array2 = array;
				num = 8;
				continue;
				IL_138:
				num = 3;
			}
			IL_DE:
			Rectangle rectangle = xlsConditionalFormats.CellRectangles[0];
			goto IL_145;
			IL_119:
			rectangle = new Rectangle(0, 0, 0, 0);
			goto IL_145;
			IL_143:
			return null;
			IL_145:
			Rectangle rectangle2 = rectangle;
			return A_0.ᜀ(array2, rectangle2.Top + 1, rectangle2.Left + 1, this.ᜂ.CalculationOptions.R1C1ReferenceMode, false);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000A5AC8 File Offset: 0x000A4AC8
		public void BeginUpdate()
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
			throw new NotImplementedException();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000A5B08 File Offset: 0x000A4B08
		public void EndUpdate()
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
			throw new NotImplementedException();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000A5B48 File Offset: 0x000A4B48
		private void ᜀ()
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
			this.ᜁ.ᜂ((ushort)this.ᜃ.ᜂ(this.ᜂ));
			this.ᜁ.ᜀ((ushort)this.ᜄ.ᜂ(this.ᜂ));
			this.ᜁ.ᜄ((uint)((ushort)this.ᜅ.ᜂ(this.ᜂ)));
			this.ᜁ.ᜅ((uint)((ushort)this.ᜆ.ᜂ(this.ᜂ)));
			this.ᜁ.ᜂ((uint)((ushort)this.ᜇ.ᜂ(this.ᜂ)));
			this.ᜁ.ᜀ((uint)((ushort)this.ᜈ.ᜂ(this.ᜂ)));
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000A5C34 File Offset: 0x000A4C34
		public void UpdateFormula(int iCurIndex, int iSourceIndex, Rectangle sourceRect, int iDestIndex, Rectangle destRect, int row, int column)
		{
			for (;;)
			{
				Ptg[] array = this.ᜁ.\u171D();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (array.Length > 0)
						{
							num = 7;
							continue;
						}
						return;
					case 1:
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
							if (array != null)
							{
								num = 5;
								continue;
							}
							goto IL_8B;
						}
						break;
					case 3:
						array = this.ᜂ.FormulaUtil.ᜀ(array, iCurIndex, iSourceIndex, sourceRect, iDestIndex, destRect, row, column);
						this.ᜁ.ᜀ(array);
						num = 8;
						continue;
					case 4:
						if (array.Length > 0)
						{
							num = 3;
							continue;
						}
						goto IL_8B;
					case 5:
						num = 4;
						continue;
					case 6:
						if (array != null)
						{
							num = 1;
							continue;
						}
						return;
					case 7:
						if (true)
						{
						}
						array = this.ᜂ.FormulaUtil.ᜀ(array, iCurIndex, iSourceIndex, sourceRect, iDestIndex, destRect, row, column);
						this.ᜁ.ᜁ(array);
						num = 9;
						continue;
					case 8:
						goto IL_8B;
					case 9:
						return;
					}
					break;
					IL_8B:
					array = this.ᜁ.ᜊ();
					num = 6;
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000A5D98 File Offset: 0x000A4D98
		public override int GetHashCode()
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
			return this.ᜁ.GetHashCode();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000A5DE0 File Offset: 0x000A4DE0
		public override bool Equals(object obj)
		{
			XlsConditionalFormat xlsConditionalFormat;
			for (;;)
			{
				xlsConditionalFormat = (obj as XlsConditionalFormat);
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 8;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C5;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 2:
						if (this.ᜆ == xlsConditionalFormat.ᜆ)
						{
							num = 10;
							continue;
						}
						return false;
					case 3:
						num = 17;
						continue;
					case 4:
						if (this.ᜃ == xlsConditionalFormat.ᜃ)
						{
							num = 3;
							continue;
						}
						return false;
					case 5:
						if (this.ᜁ.Equals(xlsConditionalFormat.ᜁ))
						{
							num = 13;
							continue;
						}
						return false;
					case 6:
						if (true)
						{
						}
						if (this.ᜌ == null)
						{
							num = 25;
							continue;
						}
						return false;
					case 7:
						num = 24;
						continue;
					case 8:
						if (this.ᜈ == xlsConditionalFormat.ᜈ)
						{
							num = 14;
							continue;
						}
						return false;
					case 9:
						if (this.ᜉ == xlsConditionalFormat.ᜉ)
						{
							num = 7;
							continue;
						}
						return false;
					case 10:
						goto IL_2C5;
					case 11:
						if (xlsConditionalFormat.ᜋ == null)
						{
							num = 22;
							continue;
						}
						return false;
					case 12:
						num = 26;
						continue;
					case 13:
						num = 4;
						continue;
					case 14:
						num = 9;
						continue;
					case 15:
						if (xlsConditionalFormat == null)
						{
							num = 23;
							continue;
						}
						num = 5;
						continue;
					case 16:
						if (spr\u24CD.ᜁ(xlsConditionalFormat.ᜊ, null))
						{
							num = 19;
							continue;
						}
						return false;
					case 17:
						if (this.ᜄ == xlsConditionalFormat.ᜄ)
						{
							num = 12;
							continue;
						}
						return false;
					case 18:
						num = 16;
						continue;
					case 19:
						num = 27;
						continue;
					case 20:
						if (this.ᜇ == xlsConditionalFormat.ᜇ)
						{
							num = 0;
							continue;
						}
						return false;
					case 21:
						num = 2;
						continue;
					case 22:
						num = 6;
						continue;
					case 23:
						return false;
					case 24:
						if (spr\u24CD.ᜁ(this.ᜊ, null))
						{
							num = 18;
							continue;
						}
						return false;
					case 25:
						goto IL_158;
					case 26:
						if (this.ᜅ == xlsConditionalFormat.ᜅ)
						{
							num = 21;
							continue;
						}
						return false;
					case 27:
						if (this.ᜋ == null)
						{
							num = 1;
							continue;
						}
						return false;
					}
					break;
					IL_2C5:
					num = 20;
				}
			}
			return false;
			IL_158:
			return xlsConditionalFormat.ᜌ == null;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000A610C File Offset: 0x000A510C
		public object Clone(object parent)
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
			XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)base.MemberwiseClone();
			xlsConditionalFormat.SetParent(parent);
			xlsConditionalFormat.ᜁ();
			xlsConditionalFormat.ᜁ = (spr\u206F)spr\u1CD3.ᜀ(this.ᜁ);
			xlsConditionalFormat.ᜌ();
			xlsConditionalFormat.ᜃ.ᜀ(this.ᜃ, false);
			xlsConditionalFormat.ᜄ.ᜀ(this.ᜄ, false);
			xlsConditionalFormat.ᜉ.ᜀ(this.ᜉ, false);
			xlsConditionalFormat.ᜇ.ᜀ(this.ᜇ, false);
			xlsConditionalFormat.ᜈ.ᜀ(this.ᜈ, false);
			xlsConditionalFormat.ᜅ.ᜀ(this.ᜅ, false);
			xlsConditionalFormat.ᜆ.ᜀ(this.ᜆ, false);
			xlsConditionalFormat.ᜁ = (spr\u206F)spr\u1CD3.ᜀ(this.ᜁ);
			xlsConditionalFormat.ᜊ = (spr\u24CD)spr\u1CD3.ᜀ(this.ᜊ);
			xlsConditionalFormat.ᜋ = (spr\u2345)spr\u1CD3.ᜀ(this.ᜋ);
			return xlsConditionalFormat;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x000A6240 File Offset: 0x000A5240
		public OColor OColor
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
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x000A6284 File Offset: 0x000A5284
		public OColor BackColorObject
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
				return this.ᜄ;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x000A62C8 File Offset: 0x000A52C8
		public OColor TopBorderColorObject
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
				return this.ᜅ;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000A630C File Offset: 0x000A530C
		public OColor BottomBorderColorObject
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
				return this.ᜆ;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x000A6350 File Offset: 0x000A5350
		public OColor LeftBorderColorObject
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
				return this.ᜇ;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x000A6394 File Offset: 0x000A5394
		public OColor RightBorderColorObject
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
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x000A63D8 File Offset: 0x000A53D8
		public OColor FontColorObject
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
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x000A641C File Offset: 0x000A541C
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x000A6464 File Offset: 0x000A5464
		public bool IsPatternStyleModified
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
				return this.ᜁ.ᜩ();
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
				this.ᜁ.ᜇ(value);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x000A64AC File Offset: 0x000A54AC
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
				return this.ᜁ.\u171D();
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000A64F4 File Offset: 0x000A54F4
		Ptg[] sprᲖ.SecondFormulaPtgs
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
				return this.ᜁ.ᜊ();
			}
		}

		// Token: 0x04000DDF RID: 3551
		private const uint ᜀ = 4294967295U;

		// Token: 0x04000DE0 RID: 3552
		private byte[] \u25D9\u008B\u00AE\u007F;

		// Token: 0x04000DE1 RID: 3553
		private spr\u206F ᜁ;

		// Token: 0x04000DE2 RID: 3554
		private XlsWorkbook ᜂ;

		// Token: 0x04000DE3 RID: 3555
		private OColor ᜃ;

		// Token: 0x04000DE4 RID: 3556
		private byte[] \u25D8\u009E\u0097\u00A5;

		// Token: 0x04000DE5 RID: 3557
		private OColor ᜄ;

		// Token: 0x04000DE6 RID: 3558
		private bool[] \u25D9\u0087\u00AF\u009A;

		// Token: 0x04000DE7 RID: 3559
		private OColor ᜅ;

		// Token: 0x04000DE8 RID: 3560
		private OColor ᜆ;

		// Token: 0x04000DE9 RID: 3561
		private OColor ᜇ;

		// Token: 0x04000DEA RID: 3562
		private OColor ᜈ;

		// Token: 0x04000DEB RID: 3563
		private OColor ᜉ;

		// Token: 0x04000DEC RID: 3564
		private spr\u24CD ᜊ;

		// Token: 0x04000DED RID: 3565
		private spr\u2345 ᜋ;

		// Token: 0x04000DEE RID: 3566
		private sprᝠ ᜌ;

		// Token: 0x04000DEF RID: 3567
		private IXLSRange \u170D;
	}
}

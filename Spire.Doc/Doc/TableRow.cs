using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x02000093 RID: 147
	public class TableRow : DocumentBase, ICompositeObject, spr\u1AB8
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000A454 File Offset: 0x00009454
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000A498 File Offset: 0x00009498
		internal bool IsRowCanSplit
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000A4DC File Offset: 0x000094DC
		public DocumentObjectCollection ChildObjects
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000A520 File Offset: 0x00009520
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.TableRow;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000A560 File Offset: 0x00009560
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000A5A4 File Offset: 0x000095A4
		public CellCollection Cells
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
				return this.ᜀ;
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000A5E8 File Offset: 0x000095E8
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000A63C File Offset: 0x0000963C
		public TableRowHeightType HeightType
		{
			get
			{
				if (this.Height < 0f)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_0D;
					}
					if (false)
					{
					}
					return TableRowHeightType.Exactly;
				}
				IL_0D:
				if (true)
				{
				}
				return this.ᜃ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3E;
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
							if (this.ᜁ.Sprms == null)
							{
								goto IL_3E;
							}
							break;
						}
						num = 5;
						continue;
					case 2:
						return;
					case 4:
						if (true)
						{
						}
						num = 1;
						continue;
					case 5:
						this.ᜁ.HasInvalidSprms = true;
						this.Height *= -1f;
						num = 0;
						continue;
					}
					if (this.HeightType != value)
					{
						num = 4;
						continue;
					}
					break;
					IL_3E:
					this.ᜃ = value;
					num = 2;
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000A714 File Offset: 0x00009714
		public RowFormat RowFormat
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000A758 File Offset: 0x00009758
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000A7A0 File Offset: 0x000097A0
		public float Height
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
				return this.ᜁ.Height;
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
				this.ᜁ.Height = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000A7E8 File Offset: 0x000097E8
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000A82C File Offset: 0x0000982C
		public bool IsHeader
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
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000A870 File Offset: 0x00009870
		internal Table OwnerTable
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
				return base.Owner as Table;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000A8B8 File Offset: 0x000098B8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000A8FC File Offset: 0x000098FC
		internal byte[] DataArray
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜅ = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000A940 File Offset: 0x00009940
		internal CharacterFormat CharacterFormat
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
				return this.ᜂ;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000A984 File Offset: 0x00009984
		internal RowFormat TrackRowFormat
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6F;
					case 2:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_4C;
							}
						}
						IL_4C:
						if (false)
						{
						}
						this.ᜆ = new RowFormat();
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜆ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.ᜆ;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000AA08 File Offset: 0x00009A08
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000AA5C File Offset: 0x00009A5C
		internal bool IsDeleteRevision
		{
			get
			{
				if (this.ᜇ)
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
						return true;
					}
				}
				return this.CharacterFormat.IsDeleteRevision;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000AAA0 File Offset: 0x00009AA0
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000AAF4 File Offset: 0x00009AF4
		internal bool IsInsertRevision
		{
			get
			{
				if (this.ᜈ)
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
						return true;
					}
				}
				return this.CharacterFormat.IsInsertRevision;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000AB38 File Offset: 0x00009B38
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000AB7C File Offset: 0x00009B7C
		internal bool HasTblPrEx
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000ABC0 File Offset: 0x00009BC0
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000AC04 File Offset: 0x00009C04
		internal spr\u204E SDTRow
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜋ = value;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000AC48 File Offset: 0x00009C48
		public TableRow(IDocument document) : base((Document)document, null)
		{
			this.ᜀ = new CellCollection(this);
			this.ᜂ = new CharacterFormat(base.Document);
			this.ᜁ = new RowFormat();
			this.ᜁ.ᜀ(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000ACA0 File Offset: 0x00009CA0
		public new TableRow Clone()
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
			return (TableRow)this.CloneImpl();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000ACE8 File Offset: 0x00009CE8
		public TableCell AddCell()
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
			return this.AddCell(true);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000AD2C File Offset: 0x00009D2C
		public TableCell AddCell(bool isCopyFormat)
		{
			TableCell tableCell;
			for (;;)
			{
				tableCell = new TableCell(base.Document);
				TableRow tableRow = base.PreviousSibling as TableRow;
				TableCell tableCell2 = null;
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						tableCell2 = tableRow.Cells[this.Cells.Count];
						num = 1;
						continue;
					case 1:
						goto IL_1A9;
					case 2:
						tableCell.CellFormat.ImportContainer(tableCell2.CellFormat);
						tableCell.Width = tableCell2.Width;
						num = 8;
						continue;
					case 3:
						if (isCopyFormat)
						{
							num = 14;
							continue;
						}
						goto IL_79;
					case 4:
						goto IL_92;
					case 5:
						if (true)
						{
						}
						if (tableCell2 != null)
						{
							num = 2;
							continue;
						}
						goto IL_79;
					case 6:
						if (isCopyFormat)
						{
							num = 4;
							continue;
						}
						goto IL_1C7;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_92;
						default:
							if (false)
							{
							}
							if (tableCell2 == null)
							{
								num = 13;
								continue;
							}
							goto IL_1C7;
						}
						break;
					case 8:
						goto IL_C9;
					case 9:
						goto IL_111;
					case 10:
						if (tableRow.Cells.Count > this.Cells.Count)
						{
							num = 0;
							continue;
						}
						goto IL_1A9;
					case 11:
						if (tableRow != null)
						{
							num = 12;
							continue;
						}
						goto IL_1A9;
					case 12:
						num = 10;
						continue;
					case 13:
						tableCell.CellFormat.ImportContainer(this.ᜁ);
						num = 9;
						continue;
					case 14:
						num = 5;
						continue;
					}
					break;
					IL_79:
					num = 6;
					continue;
					IL_92:
					num = 7;
					continue;
					IL_1A9:
					num = 3;
				}
			}
			IL_C9:
			IL_111:
			IL_1C7:
			this.Cells.Add(tableCell);
			return tableCell;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000AF10 File Offset: 0x00009F10
		public int GetRowIndex()
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
			return base.ឯ();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000AF54 File Offset: 0x00009F54
		protected override object CloneImpl()
		{
			if (true)
			{
			}
			TableRow tableRow;
			for (;;)
			{
				IL_38:
				tableRow = (TableRow)base.CloneImpl();
				tableRow.ᜂ = new CharacterFormat(base.Document);
				tableRow.ᜂ.ImportContainer(this.CharacterFormat);
				tableRow.ᜁ = new RowFormat(base.Document);
				tableRow.ᜁ.ImportContainer(this.RowFormat);
				tableRow.ᜁ.ᜀ(tableRow);
				for (;;)
				{
					IL_94:
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_94;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								goto IL_E4;
							case 1:
								tableRow.ᜅ = new byte[this.DataArray.Length];
								this.DataArray.CopyTo(tableRow.ᜅ, 0);
								num = 0;
								continue;
							case 2:
								if (this.DataArray != null)
								{
									num = 1;
									continue;
								}
								goto IL_F0;
							}
							goto IL_38;
						}
					}
				}
			}
			IL_E4:
			IL_F0:
			tableRow.ᜀ = new CellCollection(tableRow);
			this.Cells.ᜀ(tableRow.ᜀ);
			return tableRow;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000B070 File Offset: 0x0000A070
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				IL_18:
				int num = 0;
				int count = this.ChildObjects.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_3C;
					case 1:
					{
						if (num >= count)
						{
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						DocumentObject documentObject = this.ChildObjects[num];
						documentObject.CloneRelationsTo(doc, nextOwner);
						num++;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_3C;
					case 3:
						return;
					}
					break;
					IL_3C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000B11C File Offset: 0x0000A11C
		private void ᜁ()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.RowFormat.ᜀ(this);
					num = 1;
					continue;
				case 1:
					return;
				}
				IL_1C:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C;
				default:
					if (false)
					{
					}
					if (this.RowFormat.OwnerBase == this)
					{
						return;
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000B1A4 File Offset: 0x0000A1A4
		internal void ᜊ()
		{
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
						spr\u1B3A.ᜁ(this.RowFormat.RowDescriptor, this);
						spr\u1B3A.ᜀ(this.RowFormat.RowDescriptor, this, true);
						this.RowFormat.ᜎ();
						this.RowFormat.HasInvalidSprms = true;
						this.RowFormat.Sprms = null;
						this.RowFormat.RowDescriptor = null;
						num = 3;
						continue;
					case 1:
						if (true)
						{
						}
						this.OwnerTable.ᜢ = false;
						num = 5;
						continue;
					case 3:
						goto IL_103;
					case 4:
						if (this.OwnerTable != null)
						{
							num = 1;
							continue;
						}
						return;
					case 5:
						return;
					}
					if (this.RowFormat.RowDescriptor != null)
					{
						num = 0;
						continue;
					}
					break;
				}
				IL_86:
				num = 4;
				continue;
				IL_103:
				goto IL_86;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000B2B8 File Offset: 0x0000A2B8
		internal void ᜂ()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 12;
					continue;
				case 1:
					if (this.ᜂ != null)
					{
						num = 11;
						continue;
					}
					return;
				case 2:
					goto IL_181;
				case 3:
					this.ᜁ.Close();
					this.ᜁ = null;
					num = 8;
					continue;
				case 5:
					goto IL_132;
				case 6:
				{
					int count = this.ᜀ.Count;
					int num2 = 0;
					num = 7;
					continue;
				}
				case 7:
					goto IL_181;
				case 8:
					goto IL_C2;
				case 9:
					if (this.ᜁ != null)
					{
						num = 3;
						continue;
					}
					goto IL_C2;
				case 10:
					return;
				case 11:
					this.ᜂ.Close();
					this.ᜂ = null;
					goto IL_174;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_174;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (this.ᜀ.Count > 0)
						{
							num = 6;
							continue;
						}
						goto IL_132;
					}
					break;
				case 13:
					this.ᜀ.Clear();
					this.ᜀ = null;
					num = 5;
					continue;
				case 14:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 13;
						continue;
					}
					TableCell tableCell = this.ᜀ[num2];
					tableCell.ᜅ();
					num2++;
					num = 2;
					continue;
				}
				}
				if (this.ᜀ != null)
				{
					num = 0;
					continue;
				}
				goto IL_132;
				IL_C2:
				num = 1;
				continue;
				IL_132:
				num = 9;
				continue;
				IL_174:
				num = 10;
				continue;
				IL_181:
				num = 14;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000B48C File Offset: 0x0000A48C
		protected override void InitXDLSHolder()
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
			base.XDLSHolder.AddElement(ClipboardData.b("᭷όၻች", a_), this.Cells);
			base.XDLSHolder.AddElement(ClipboardData.b("᭷ቹᵻ౽慎꞉ﾑ", a_), this.CharacterFormat);
			base.XDLSHolder.AddElement(ClipboardData.b("౷᭹ṻች꾁慎揄", a_), this.RowFormat);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000B530 File Offset: 0x0000A530
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					writer.WriteChildBinaryElement(ClipboardData.b("ѬŮհᙲݴ᥶ᡸ᝺偼᭾", a_), this.DataArray);
					num = 0;
					continue;
				}
				IL_25:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_25;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.DataArray == null)
					{
						return;
					}
					num = 2;
					break;
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000B5C8 File Offset: 0x0000A5C8
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 0;
			if (true)
			{
			}
			bool result;
			for (;;)
			{
				IL_41:
				result = base.ReadXmlContent(reader);
				for (;;)
				{
					IL_49:
					int num = 0;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_49;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								if (reader.TagName == ClipboardData.b("ཥ٧ṩ५ᱭṯ፱ᡳ孵ᱷ᭹ࡻώ", a_))
								{
									num = 2;
									continue;
								}
								return result;
							case 1:
								return result;
							case 2:
								this.DataArray = reader.ReadChildBinaryElement();
								result = true;
								num = 1;
								continue;
							}
							goto IL_41;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000B670 File Offset: 0x0000A670
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_F0;
				case 2:
					writer.WriteValue(ClipboardData.b("㡩ͫᥭ㡯᝱ᵳᅵၷ๹", a_), this.Height);
					num = 7;
					continue;
				case 3:
					return;
				case 4:
					if (this.ᜁ.ᜑ())
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 5:
					writer.WriteValue(ClipboardData.b("⍩Ὣ♭ᕯ፱ၳ፵੷", a_), this.IsHeader);
					num = 1;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F0;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (this.Height > 0f)
						{
							num = 2;
							continue;
						}
						goto IL_118;
					}
					break;
				case 7:
					goto IL_EE;
				}
				if (this.IsHeader)
				{
					num = 5;
					continue;
				}
				IL_F0:
				num = 4;
			}
			IL_EE:
			IL_118:
			writer.WriteValue(ClipboardData.b("≩५ݭᝯᩱs≵ŷ੹᥻", a_), this.HeightType);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000B7B4 File Offset: 0x0000A7B4
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 6;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_86;
				case 3:
					goto IL_100;
				case 4:
					this.Height = reader.ReadFloat(ClipboardData.b("㹫ŭݯ㩱ᅳήίቹࡻ", a_));
					num = 3;
					continue;
				case 5:
					if (reader.HasAttribute(ClipboardData.b("⑫୭᥯ᕱᱳɵⱷ͹౻᭽", a_)))
					{
						num = 8;
						continue;
					}
					return;
				case 6:
					this.IsHeader = reader.ReadBoolean(ClipboardData.b("╫ᵭ㡯᝱ᕳትᵷࡹ", a_));
					num = 1;
					continue;
				case 7:
					if (reader.HasAttribute(ClipboardData.b("╫ᵭ㡯᝱ᕳትᵷࡹ", a_)))
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					goto IL_86;
				case 8:
					this.HeightType = (TableRowHeightType)reader.ReadEnum(ClipboardData.b("⑫୭᥯ᕱᱳɵⱷ͹౻᭽", a_), typeof(TableRowHeightType));
					num = 0;
					continue;
				}
				if (!reader.HasAttribute(ClipboardData.b("㹫ŭݯ㩱ᅳήίቹࡻ", a_)))
				{
					goto IL_138;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_100;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_86:
				num = 5;
				continue;
				IL_138:
				num = 7;
				continue;
				IL_100:
				goto IL_138;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000B938 File Offset: 0x0000A938
		protected override void CreateLayoutInfo()
		{
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					this.ᜀ.ᜆ(false);
					num = 4;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						if (false)
						{
						}
						goto IL_44;
					}
					break;
				case 3:
					if (num2 >= this.Cells.Count)
					{
						goto IL_5D;
					}
					num = 7;
					continue;
				case 4:
					goto IL_117;
				case 6:
					goto IL_65;
				case 7:
					if (this.Cells[num2].CellFormat.TextDirection == TextDirection.LeftToRight)
					{
						num = 1;
						continue;
					}
					num2++;
					num = 0;
					continue;
				}
				if (true)
				{
				}
				this.ᜀ = new spr\u2032(this.HeightType == TableRowHeightType.Exactly && (double)((this.Height >= 0f) ? this.Height : (-1f * this.Height)) >= 0.05, this.Height);
				this.ᜀ.ᜆ(true);
				num2 = 0;
				num = 2;
				continue;
				IL_44:
				num = 3;
				continue;
				IL_5D:
				num = 6;
			}
			IL_65:
			IL_117:
			this.ᜀ();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000BAA8 File Offset: 0x0000AAA8
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
			dc.ᜀ(this, ltWidget);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000BAEC File Offset: 0x0000AAEC
		private new void ᜀ()
		{
			switch (0)
			{
			default:
			{
				float leftIndent;
				for (;;)
				{
					IL_23:
					float num = this.RowFormat.CellSpacing / 4f;
					leftIndent = this.RowFormat.LeftIndent;
					for (;;)
					{
						IL_41:
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_161;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_41;
								default:
								{
									if (true)
									{
									}
									if (false)
									{
									}
									Borders borders = this.RowFormat.Borders;
									float num3 = borders.Left.LineWidth / 2f;
									float num4 = borders.Top.LineWidth / 2f;
									float num5 = borders.Right.LineWidth / 2f;
									float num6 = borders.Bottom.LineWidth / 2f;
									spr\u2326 spr_u = this.ᜀ.ᜊ();
									spr_u.ᜂ((double)(num3 + num));
									spr_u.ᜁ((double)(num4 + num));
									spr_u.ᜃ((double)(num5 + num));
									spr_u.ᜀ((double)(num6 + num));
									spr\u2326 spr_u2 = this.ᜀ.ᜋ();
									spr_u2.ᜂ((double)num3);
									spr_u2.ᜁ((double)num4);
									spr_u2.ᜃ((double)num5);
									spr_u2.ᜀ((double)num6);
									num2 = 0;
									continue;
								}
								}
								break;
							case 2:
								if (this.RowFormat.CellSpacing > -1f)
								{
									num2 = 1;
									continue;
								}
								goto IL_16D;
							}
							goto IL_23;
						}
					}
				}
				IL_161:
				IL_16D:
				spr\u2326 spr_u3 = this.ᜀ.ᜋ();
				spr_u3.ᜂ(spr_u3.ᜃ() + (double)leftIndent);
				this.ᜀ.ᜃ(true);
				return;
			}
			}
		}

		// Token: 0x0400093B RID: 2363
		private new CellCollection ᜀ;

		// Token: 0x0400093C RID: 2364
		private RowFormat ᜁ;

		// Token: 0x0400093D RID: 2365
		private string \u2460\u008F\u0088\u00A1;

		// Token: 0x0400093E RID: 2366
		private bool \u2609\u008C\u0099\u00A8;

		// Token: 0x0400093F RID: 2367
		private CharacterFormat ᜂ;

		// Token: 0x04000940 RID: 2368
		private TableRowHeightType ᜃ;

		// Token: 0x04000941 RID: 2369
		private new bool ᜄ;

		// Token: 0x04000942 RID: 2370
		private byte[] ᜅ;

		// Token: 0x04000943 RID: 2371
		private byte[] \u25D8\u0090\u00A4\u00A5;

		// Token: 0x04000944 RID: 2372
		internal RowFormat ᜆ;

		// Token: 0x04000945 RID: 2373
		private bool ᜇ;

		// Token: 0x04000946 RID: 2374
		private bool ᜈ;

		// Token: 0x04000947 RID: 2375
		private byte \u2609\u00AB\u0098\u0095;

		// Token: 0x04000948 RID: 2376
		private bool ᜉ;

		// Token: 0x04000949 RID: 2377
		internal bool ᜊ = true;

		// Token: 0x0400094A RID: 2378
		private spr\u204E ᜋ;
	}
}

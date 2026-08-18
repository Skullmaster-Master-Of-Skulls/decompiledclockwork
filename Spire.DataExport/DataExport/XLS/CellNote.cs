using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001E7 RID: 487
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class CellNote : CustomItem, ICloneable
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x000A30F4 File Offset: 0x000A20F4
		public CellNote()
		{
			this.ᜄ = new StringListCollection();
			this.ᜅ = new CellNoteFormat();
			base..ctor();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000A3120 File Offset: 0x000A2120
		public CellNote(CellNotes Collection)
		{
			int a_ = 0;
			this.ᜄ = new StringListCollection();
			this.ᜅ = new CellNoteFormat();
			base..ctor();
			if (Collection != null)
			{
				this.ᜁ = Collection.Holder;
			}
			if (this.ᜁ != null)
			{
				PropertyInfo property = this.ᜁ.GetType().GetProperty(HyperlinksCollectionEditor.b("匛渝吟䬡䬣䠥嬧", a_));
				if (property != null)
				{
					SheetOptions sheetOptions = (SheetOptions)property.GetValue(this.ᜁ, null);
					if (sheetOptions != null)
					{
						this.ᜅ = (sheetOptions.NoteFormat.Clone() as CellNoteFormat);
					}
				}
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000A31C8 File Offset: 0x000A21C8
		protected override void Dispose(bool Disposing)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (!this.ᜀ)
			{
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							this.ᜅ.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_7A;
						case 3:
							goto IL_89;
						}
						if (Disposing)
						{
							num = 1;
							continue;
						}
						IL_7A:
						this.ᜀ = true;
						num = 3;
					}
					IL_89:;
				}
				finally
				{
					base.Dispose(Disposing);
				}
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000A3278 File Offset: 0x000A2278
		public object Clone()
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
			return new CellNote
			{
				Col = this.Col,
				Row = this.Row,
				Lines = this.Lines,
				Format = this.Format
			};
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x000A32EC File Offset: 0x000A22EC
		internal override void InitCollectionItem()
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

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000A3328 File Offset: 0x000A2328
		internal spr\u1DCA ᜀ()
		{
			int num = 3;
			spr\u1DCA result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					result.ᜃ = (ushort)(this.ᜂ - 1 - 1);
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					goto IL_6B;
				case 2:
					goto IL_92;
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
					if (this.ᜂ - 1 > 1)
					{
						num = 0;
						continue;
					}
					break;
				}
				result.ᜃ = 0;
				num = 1;
			}
			IL_6B:
			IL_92:
			result.ᜁ = (ushort)(this.ᜃ - 1 + 1);
			result.ᜇ = result.ᜃ + 5;
			result.ᜅ = result.ᜁ + 2;
			result.ᜀ = 0;
			result.ᜂ = 0;
			result.ᜄ = 0;
			result.ᜆ = 0;
			result.ᜈ = 0;
			return result;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000A3428 File Offset: 0x000A2428
		public bool IsValid()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					if (this.ᜂ > 0)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return false;
				case 3:
					num = 5;
					continue;
				case 4:
					goto IL_91;
				case 5:
					if (this.ᜃ < 256)
					{
						num = 1;
						continue;
					}
					return false;
				}
				if (this.ᜃ <= 0)
				{
					return false;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			IL_91:
			return this.ᜄ.Count > 0;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000A34F0 File Offset: 0x000A24F0
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 19;
			for (;;)
			{
				File.WriteValue(Section, HyperlinksCollectionEditor.b("氮帰弲䀴娶圸", a_), this.ᜃ.ToString());
				File.WriteValue(Section, HyperlinksCollectionEditor.b("紮帰䐲", a_), this.ᜂ.ToString());
				int num = 0;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A9:
					if (num >= this.ᜄ.Count)
					{
						num2 = 2;
					}
					else
					{
						File.WriteValue(Section + '_' + HyperlinksCollectionEditor.b("挮砰紲瀴搶", a_), string.Format(HyperlinksCollectionEditor.b("䌮堰崲倴䰶स䘺", a_), num), this.ᜄ[num]);
						num++;
						num2 = 1;
					}
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num2 = 3;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_A9;
					case 1:
						goto IL_9E;
					case 2:
						goto IL_C2;
					case 3:
						goto IL_9E;
					}
					break;
					IL_9E:
					num2 = 0;
				}
			}
			IL_C2:
			this.ᜅ.SaveToXmlFile(File, Section);
			File.SaveToFile();
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000A362C File Offset: 0x000A262C
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 12;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜃ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("欧䔩䀫嬭崯就", a_), 0.ToString()));
					this.ᜂ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("稧䔩嬫", a_), 0.ToString()));
					this.ᜄ.Clear();
					Array array = null;
					int num = 6;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (array != null)
							{
								num = 1;
								continue;
							}
							goto IL_1E9;
						case 1:
							goto IL_1BC;
						case 2:
							if (num2 >= this.ᜄ.Count)
							{
								num = 3;
								continue;
							}
							this.ᜄ[num2] = File.ReadValue(Section + '_' + HyperlinksCollectionEditor.b("搧挩戫欭振", a_), this.ᜄ[num2], string.Empty);
							num2++;
							num = 7;
							continue;
						case 3:
							goto IL_1E7;
						case 4:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1BC;
							default:
								if (false)
								{
								}
								File.ReadValues(Section + '_' + HyperlinksCollectionEditor.b("搧挩戫欭振", a_), ref array);
								num = 0;
								continue;
							}
							break;
						case 5:
							goto IL_1BE;
						case 6:
							if (spr\u2059.ᜀ(File, Section + '_' + HyperlinksCollectionEditor.b("搧挩戫欭振", a_)))
							{
								num = 4;
								continue;
							}
							goto IL_1E9;
						case 7:
							goto IL_1BE;
						}
						break;
						IL_1BC:
						this.ᜄ.SetStrings(array as string[]);
						num2 = 0;
						num = 5;
						continue;
						IL_1BE:
						num = 2;
					}
				}
				IL_1E7:
				IL_1E9:
				this.ᜅ.LoadFromXmlFile(File, Section);
				return;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000A3830 File Offset: 0x000A2830
		[Browsable(false)]
		public override ItemType ItemType
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
				return ItemType.Note;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000A386C File Offset: 0x000A286C
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x000A38B0 File Offset: 0x000A28B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the vertical position of the note cell.")]
		[DefaultValue(0)]
		public int Row
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
						case 1:
							return;
						case 2:
							this.ᜂ = value;
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					IL_38:
					if (value != this.ᜂ)
					{
						num = 2;
						continue;
					}
					break;
					goto IL_38;
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x000A392C File Offset: 0x000A292C
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x000A3970 File Offset: 0x000A2970
		[Description("Gets or sets the horizontal position of the note cell.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Col
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
				return this.ᜃ;
			}
			set
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
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 1:
							this.ᜃ = value;
							num = 0;
							continue;
						}
						break;
					}
					IL_40:
					if (value != this.ᜃ)
					{
						num = 1;
						continue;
					}
					break;
					goto IL_40;
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000A39EC File Offset: 0x000A29EC
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000A3A30 File Offset: 0x000A2A30
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Contains the note text.")]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public StringListCollection Lines
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
				int num = 2;
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
							goto IL_40;
						default:
							if (false)
							{
							}
							if (value != this.ᜄ)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 3:
						num = 1;
						continue;
					case 4:
						this.ᜄ = value;
						goto IL_40;
					}
					if (value != null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
					IL_40:
					num = 0;
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000A3AC8 File Offset: 0x000A2AC8
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x000A3B0C File Offset: 0x000A2B0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Defines parameters of displaying the note in the result document.")]
		public CellNoteFormat Format
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
							goto IL_40;
						default:
							if (false)
							{
							}
							if (value != this.ᜅ)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						return;
					case 3:
						num = 0;
						continue;
					case 4:
						this.ᜅ = value;
						goto IL_40;
					}
					if (value != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_40:
					num = 2;
				}
			}
		}

		// Token: 0x04000B30 RID: 2864
		private bool ᜀ;

		// Token: 0x04000B31 RID: 2865
		private object ᜁ;

		// Token: 0x04000B32 RID: 2866
		private int ᜂ;

		// Token: 0x04000B33 RID: 2867
		private int ᜃ;

		// Token: 0x04000B34 RID: 2868
		private bool \u2609\u0092\u007F\u0097;

		// Token: 0x04000B35 RID: 2869
		private int \u25D9\u00AC\u0088\u0095;

		// Token: 0x04000B36 RID: 2870
		private StringListCollection ᜄ;

		// Token: 0x04000B37 RID: 2871
		private byte[] \u25D8\u00AD\u00AF\u00A4;

		// Token: 0x04000B38 RID: 2872
		private CellNoteFormat ᜅ;
	}
}

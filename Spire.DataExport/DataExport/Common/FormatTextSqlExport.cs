using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Common
{
	// Token: 0x0200016B RID: 363
	public abstract class FormatTextSqlExport : AdvancedTextExport
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x00060B90 File Offset: 0x0005FB90
		protected override void SaveProperties(XMLFile File)
		{
			int a_ = 12;
			for (;;)
			{
				base.SaveProperties(File);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("愧䐩堫䬭圯圱䘳", a_), this.DataFormats.Integer);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("渧䘩䌫伭䐯", a_), this.DataFormats.Float);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("簧䌩䄫䬭", a_), this.DataFormats.Time);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("氧䬩堫䬭搯嬱夳匵", a_), this.DataFormats.DateTime);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("欧弩師尭唯就圳伵", a_), this.DataFormats.Currency);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("樧䔩䌫䈭唯匱娳戵䨷伹夻", a_), this.DataFormats.BooleanTrue);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("樧䔩䌫䈭唯匱娳瀵夷嘹伻嬽", a_), this.DataFormats.BooleanFalse);
				File.WriteValue(HyperlinksCollectionEditor.b("渧攩縫挭焯昱朳", a_), HyperlinksCollectionEditor.b("昧弩䀫䈭振䘱䘳張嘷崹", a_), this.DataFormats.NullString);
				File.RemoveSection(HyperlinksCollectionEditor.b("欧缩缫稭缯缱欳瀵眷根焻缽ᐿᅁ", a_));
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_1BB;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						goto IL_1BB;
					case 3:
						if (num >= this.CustomFormats.Count)
						{
							num2 = 1;
							continue;
						}
						File.WriteValue(HyperlinksCollectionEditor.b("欧缩缫稭缯缱欳瀵眷根焻缽ᐿᅁ", a_), string.Format(HyperlinksCollectionEditor.b("匧ᨩ儫唭į伱", a_), HyperlinksCollectionEditor.b("䐧䌩䈫䬭", a_), num), this.CustomFormats[num]);
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					break;
					IL_1BB:
					num2 = 3;
				}
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00060E00 File Offset: 0x0005FE00
		protected override void LoadProperties(XMLFile File)
		{
			int a_ = 13;
			for (;;)
			{
				base.LoadProperties(File);
				this.DataFormats.Integer = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("怨䔪夬䨮嘰嘲䜴", a_), this.DataFormats.Integer);
				this.DataFormats.Float = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("漨䜪䈬丮䔰", a_), this.DataFormats.Float);
				this.DataFormats.Time = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("紨䈪䀬䨮", a_), this.DataFormats.Time);
				this.DataFormats.DateTime = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("洨䨪夬䨮攰娲場制", a_), this.DataFormats.DateTime);
				this.DataFormats.Currency = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("樨帪弬崮吰崲嘴丶", a_), this.DataFormats.Currency);
				this.DataFormats.BooleanTrue = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("欨䐪䈬䌮吰刲嬴挶䬸为堼", a_), this.DataFormats.BooleanTrue);
				this.DataFormats.BooleanFalse = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("欨䐪䈬䌮吰刲嬴然堸场丼娾", a_), this.DataFormats.BooleanFalse);
				this.DataFormats.NullString = File.ReadValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("木帪䄬䌮戰䜲䜴帶圸尺", a_), this.DataFormats.NullString);
				Array array = null;
				this.CustomFormats.Clear();
				File.ReadValues(HyperlinksCollectionEditor.b("樨縪縬笮縰縲樴然瘸椺瀼績ᕀ၂", a_), ref array);
				int num = 5;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_25F;
					case 2:
						goto IL_25F;
					case 3:
						goto IL_26A;
					case 4:
						this.CustomFormats.SetStrings(array as string[]);
						num2 = 0;
						num = 1;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_26A;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (array != null)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					}
					break;
					IL_25F:
					num = 3;
					continue;
					IL_26A:
					if (num2 >= this.CustomFormats.Count)
					{
						num = 0;
					}
					else
					{
						this.CustomFormats[num2] = File.ReadValue(HyperlinksCollectionEditor.b("樨縪縬笮縰縲樴然瘸椺瀼績ᕀ၂", a_), this.CustomFormats[num2], string.Empty);
						num2++;
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x000610F8 File Offset: 0x000600F8
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0006113C File Offset: 0x0006013C
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
				if (false)
				{
				}
				if (true)
				{
				}
				return base.DataFormats;
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
				base.DataFormats = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00061180 File Offset: 0x00060180
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x000611C4 File Offset: 0x000601C4
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
				if (true)
				{
				}
				if (false)
				{
				}
				base.CustomFormats = value;
			}
		}
	}
}

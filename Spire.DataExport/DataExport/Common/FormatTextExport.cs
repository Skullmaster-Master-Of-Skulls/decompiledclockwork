using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Common
{
	// Token: 0x02000160 RID: 352
	public abstract class FormatTextExport : AdvancedTextExport
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00058F80 File Offset: 0x00057F80
		protected override void SaveProperties(XMLFile File)
		{
			int a_ = 13;
			for (;;)
			{
				base.SaveProperties(File);
				File.WriteValue(HyperlinksCollectionEditor.b("渨渪挬樮挰爲礴", a_), HyperlinksCollectionEditor.b("栨䜪䄬䀮䘰朲尴䌶唸帺丼", a_), this.AddTitles.ToString());
				File.RemoveSection(HyperlinksCollectionEditor.b("紨截礬挮琰怲", a_));
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_331;
					case 1:
					{
						int num3;
						if (num3 >= this.CustomFormats.Count)
						{
							goto IL_324;
						}
						File.WriteValue(HyperlinksCollectionEditor.b("樨縪縬笮縰縲樴然瘸椺瀼績ᕀ၂", a_), string.Format(HyperlinksCollectionEditor.b("刨ᬪ倬吮0串", a_), HyperlinksCollectionEditor.b("䔨䈪䌬䨮", a_), num3), this.CustomFormats[num3]);
						num3++;
						if (true)
						{
						}
						num2 = 7;
						continue;
					}
					case 2:
						goto IL_331;
					case 3:
						if (num >= this.Titles.Count)
						{
							num2 = 5;
							continue;
						}
						File.WriteValue(HyperlinksCollectionEditor.b("紨截礬挮琰怲", a_), string.Format(HyperlinksCollectionEditor.b("刨ᬪ倬吮0串", a_), HyperlinksCollectionEditor.b("䔨䈪䌬䨮", a_), num), this.Titles[num]);
						num++;
						num2 = 2;
						continue;
					case 4:
						goto IL_308;
					case 5:
					{
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("怨䔪夬䨮嘰嘲䜴", a_), this.DataFormats.Integer);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("漨䜪䈬丮䔰", a_), this.DataFormats.Float);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("紨䈪䀬䨮", a_), this.DataFormats.Time);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("洨䨪夬䨮攰娲場制", a_), this.DataFormats.DateTime);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("樨帪弬崮吰崲嘴丶", a_), this.DataFormats.Currency);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("欨䐪䈬䌮吰刲嬴挶䬸为堼", a_), this.DataFormats.BooleanTrue);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("欨䐪䈬䌮吰刲嬴然堸场丼娾", a_), this.DataFormats.BooleanFalse);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("木帪䄬䌮戰䜲䜴帶圸尺", a_), this.DataFormats.NullString);
						File.WriteValue(HyperlinksCollectionEditor.b("漨搪缬戮瀰朲昴", a_), HyperlinksCollectionEditor.b("樨帪䄬嬮䐰䄲倴礶堸嘺堼", a_), this.DataFormats.CultureName);
						File.RemoveSection(HyperlinksCollectionEditor.b("樨縪縬笮縰縲樴然瘸椺瀼績ᕀ၂", a_));
						int num3 = 0;
						num2 = 4;
						continue;
					}
					case 6:
						return;
					case 7:
						goto IL_308;
					}
					break;
					IL_308:
					num2 = 1;
					continue;
					IL_324:
					num2 = 6;
					continue;
					IL_331:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_324;
					default:
						if (false)
						{
						}
						num2 = 3;
						break;
					}
				}
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00059308 File Offset: 0x00058308
		protected override void LoadProperties(XMLFile File)
		{
			int a_ = 9;
			switch (0)
			{
			default:
				for (;;)
				{
					base.LoadProperties(File);
					this.AddTitles = Convert.ToBoolean(File.ReadValue(HyperlinksCollectionEditor.b("戤戦木渪缬渮細", a_), HyperlinksCollectionEditor.b("搤䬦䔨䐪娬笮堰䜲头制䨸", a_), this.AddTitles.ToString()));
					Array array = null;
					this.Titles.Clear();
					File.ReadValues(HyperlinksCollectionEditor.b("焤渦紨未栬簮", a_), ref array);
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_43B;
						case 1:
						{
							this.CustomFormats.SetStrings(array as string[]);
							int num2 = 0;
							num = 7;
							continue;
						}
						case 2:
							goto IL_464;
						case 3:
							goto IL_383;
						case 4:
							goto IL_43B;
						case 5:
							return;
						case 6:
						{
							int num2;
							if (num2 >= this.CustomFormats.Count)
							{
								num = 5;
								continue;
							}
							this.CustomFormats[num2] = File.ReadValue(HyperlinksCollectionEditor.b("昤爦稨缪戬戮渰甲稴收琸稺椼氾", a_), this.CustomFormats[num2], string.Empty);
							num2++;
							num = 3;
							continue;
						}
						case 7:
							goto IL_383;
						case 8:
							if (array != null)
							{
								num = 1;
								continue;
							}
							return;
						case 9:
							if (array != null)
							{
								num = 11;
								continue;
							}
							goto IL_F9;
						case 10:
						{
							int num3;
							if (num3 >= this.Titles.Count)
							{
								num = 2;
								continue;
							}
							this.Titles[num3] = File.ReadValue(HyperlinksCollectionEditor.b("焤渦紨未栬簮", a_), this.Titles[num3], string.Empty);
							num3++;
							num = 4;
							continue;
						}
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_464;
							default:
							{
								if (false)
								{
								}
								this.Titles.SetStrings(array as string[]);
								int num3 = 0;
								num = 0;
								continue;
							}
							}
							break;
						}
						break;
						IL_F9:
						if (true)
						{
						}
						this.DataFormats.Integer = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("氤䤦崨个䨬䨮䌰", a_), this.DataFormats.Integer);
						this.DataFormats.Float = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("挤䬦䘨䨪夬", a_), this.DataFormats.Float);
						this.DataFormats.Time = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("焤並䐨个", a_), this.DataFormats.Time);
						this.DataFormats.DateTime = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("愤䘦崨个礬䘮尰嘲", a_), this.DataFormats.DateTime);
						this.DataFormats.Currency = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("昤刦嬨太䠬䄮到䨲", a_), this.DataFormats.Currency);
						this.DataFormats.BooleanTrue = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("朤䠦䘨䜪䠬丮弰朲䜴䈶尸", a_), this.DataFormats.BooleanTrue);
						this.DataFormats.BooleanFalse = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("朤䠦䘨䜪䠬丮弰甲吴嬶䨸帺", a_), this.DataFormats.BooleanFalse);
						this.DataFormats.NullString = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("欤刦䔨䜪縬嬮䌰娲嬴倶", a_), this.DataFormats.NullString);
						this.DataFormats.CultureName = File.ReadValue(HyperlinksCollectionEditor.b("挤栦笨昪氬笮戰", a_), HyperlinksCollectionEditor.b("昤刦䔨弪堬崮吰紲吴娶尸", a_), this.DataFormats.CultureName);
						this.CustomFormats.Clear();
						File.ReadValues(HyperlinksCollectionEditor.b("昤爦稨缪戬戮渰甲稴收琸稺椼氾", a_), ref array);
						num = 8;
						continue;
						IL_464:
						goto IL_F9;
						IL_383:
						num = 6;
						continue;
						IL_43B:
						num = 10;
					}
				}
				return;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00059780 File Offset: 0x00058780
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x000597C4 File Offset: 0x000587C4
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool AddTitles
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
				return base.AddTitles;
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
				base.AddTitles = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00059808 File Offset: 0x00058808
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0005984C File Offset: 0x0005884C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Browsable(true)]
		public new StringListCollection Titles
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
				return base.Titles;
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
				base.Titles = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00059890 File Offset: 0x00058890
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x000598D4 File Offset: 0x000588D4
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public new FormatsExport DataFormats
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
				return base.DataFormats;
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
				base.DataFormats = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00059918 File Offset: 0x00058918
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0005995C File Offset: 0x0005895C
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
				if (false)
				{
				}
				if (true)
				{
				}
				base.CustomFormats = value;
			}
		}
	}
}
